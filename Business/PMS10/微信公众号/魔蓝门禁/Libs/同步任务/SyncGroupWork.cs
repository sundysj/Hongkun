using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Aop.Api.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianChengEntranceSyncService.Redis;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Security.Policy;
using Log = TianChengEntranceSyncService.Util.Log;
using TianChengEntranceSyncService.Util;

namespace TianChengEntranceSyncService.同步任务
{
    public class SyncGroupWork
    {

        /// <summary>
        /// 外部串行控制，不需要再开线程
        /// </summary>
        /// <returns>是否继续执行后续任务</returns>
        public static bool Run(MoredianOrg moredianOrg)
        {
            if(null == moredianOrg)
            {
                return false;
            }
            try
            {

                #region 获取AppToken
                string app_token = AppTokenRedis.GetAppToken();
                if (string.IsNullOrEmpty(app_token))
                {
                    return false;
                }
                #endregion

                #region 获取AccessToken
                string access_token = AccessTokenRedis.GetAccessToken(Convert.ToString(moredianOrg.orgId), moredianOrg.orgAuthKey);
                if (string.IsNullOrEmpty(access_token))
                {
                    return false;
                }
                #endregion

                /* 2.按照规则初始化群组结构*/
                /* 2.1需要先判断是否存在成员，存在成员的话，根据成员手机号，查询WChat2020的Tb_User_Bind表绑定关系，根据绑定关系进行分类，聚合楼栋单元信息，进行创建群组以及权限分配*/
                using (IDbConnection conn = new SqlConnection(Config.WChat2020ConnectionStr)
                    , erpConn = new SqlConnection(Config.EntranceConnectionStr))
                {
                    // 需要添加的群组列表
                    List<Dictionary<string, string>> NeedAddGroupList = new List<Dictionary<string, string>>();

                    #region 建立每个小区的大门群组，如果数据库已存在，就不再创建了
                    List<dynamic> CommGroupInfoList = erpConn.Query("SELECT CommID, CommName FROM Tb_HSPR_Community WITH(NOLOCK) WHERE ISNULL(IsDelete,0) = 0").ToList();
                    if(null != CommGroupInfoList)
                    {
                        CommGroupInfoList.ForEach((item) =>
                        {
                            string CommID = Convert.ToString(item.CommID);
                            string CommName = Convert.ToString(item.CommName);
                            string BuildSNum = "0";
                            string UnitSNum = "0";
                            // 如果已经创建过了，就不再创建了
                            if (erpConn.QueryFirstOrDefault<int>("SELECT COUNT(1) FROM Tb_HSPR_Entrance_Group WITH(NOLOCK) WHERE CommID = @CommID AND ISNULL(BuildSNum,0) = 0 AND ISNULL(UnitSNum,0) = 0", new { CommID }) <= 0)
                            {
                                NeedAddGroupList.Add(new Dictionary<string, string>
                                {
                                    { "CommID", CommID },
                                    { "CommName", CommName },
                                    { "BuildSNum", BuildSNum },
                                    { "UnitSNum", UnitSNum },
                                });
                            }
                        });
                    }
                    #endregion

                    #region 建立每个小区的单元群组
                    // 查询系统上所有的CommID，CommName, BuildSNum，UnitSNum
                    List<dynamic> GroupInfoList = erpConn.Query("SELECT CommID,CommName,BuildSNum,UnitSNum FROM view_HSPR_Room_Filter WITH(NOLOCK) WHERE ISNULL(IsDelete,0) = 0 GROUP BY CommID,CommName,BuildSNum,UnitSNum").ToList();
                    if (null != GroupInfoList)
                    {
                        GroupInfoList.ForEach(item =>
                        {
                            string CommID = Convert.ToString(item.CommID);
                            string CommName = Convert.ToString(item.CommName);
                            string BuildSNum = Convert.ToString(item.BuildSNum);
                            string UnitSNum = Convert.ToString(item.UnitSNum);
                            // 如果已经创建过了，就不再创建了
                            if (erpConn.QueryFirstOrDefault<int>("SELECT COUNT(1) FROM Tb_HSPR_Entrance_Group WITH(NOLOCK) WHERE CommID = @CommID AND BuildSNum = @BuildSNum AND UnitSNum = @UnitSNum", new { CommID, BuildSNum, UnitSNum }) <= 0)
                            {
                                NeedAddGroupList.Add(new Dictionary<string, string>
                                {
                                    { "CommID", CommID },
                                    { "CommName", CommName },
                                    { "BuildSNum", BuildSNum },
                                    { "UnitSNum", UnitSNum },
                                });
                            }
                        });
                    }
                    #endregion

                    #region 批量创建群组
                    ConcurrentBag<Dictionary<string, string>> CreatedGroupList = new ConcurrentBag<Dictionary<string, string>>();
                    DateTime DateNow = DateTime.Now;
                    // 开始时间限制必须大于当前时间，所以限定生效时间为10分钟后
                    DateNow = DateNow.AddMinutes(10);
                    long beginDay = DateHelper.Get13TimeStamp(DateNow);
                    // 默认10年
                    long endDay = DateHelper.Get13TimeStamp(DateNow.AddYears(10));
                    Parallel.ForEach(new ConcurrentBag<Dictionary<string,string>>(NeedAddGroupList), (item) =>
                    {
                        try
                        {
                            string CommID = item["CommID"];
                            string CommName = item["CommName"];
                            string BuildSNum = item["BuildSNum"];
                            string UnitSNum = item["UnitSNum"];
                            string groupName = $"{CommName}";
                            if (!"0".Equals(BuildSNum) || !"0".Equals(UnitSNum))
                            {
                                groupName += $"-{BuildSNum}-{UnitSNum}";
                            }
                            IMoredianApiClient client = new DefaultMoredianApiClient(Config.MoreDian.APIURL);
                            MoredianCreateGroupRequest request = new MoredianCreateGroupRequest
                            {
                                moredianGroup = new MoredianGroup
                                {
                                    groupName = groupName,
                                    allMemberStatus = 1,
                                    dayBeginTime = "00:00",
                                    dayEndTime = "00:01",
                                    beginDay = beginDay,
                                    endDay = endDay
                                }
                            };
                            MoredianCreateGroupResponse response = client.Execute(request, app_token, access_token);
                            if (response.IsSucc())
                            {
                                string GroupId = Convert.ToString(response.groupId);
                                CreatedGroupList.Add(new Dictionary<string, string>
                                {
                                    { "CommID", CommID },
                                    { "BuildSNum", BuildSNum },
                                    { "UnitSNum", UnitSNum },
                                    { "GroupId", GroupId }
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.WriteLog(ex.Message);
                        }
                    });
                    #endregion

                    #region 对创建结果回写进数据库
                    List<Dictionary<string, string>> CreatedGroupArray = new List<Dictionary<string, string>>(CreatedGroupList);
                    if (null != CreatedGroupArray)
                    {
                        CreatedGroupArray.ForEach(item =>
                        {
                            string CommID = item["CommID"];
                            string BuildSNum = item["BuildSNum"];
                            string UnitSNum = item["UnitSNum"];
                            string GroupId = item["GroupId"];
                            erpConn.Execute("INSERT INTO Tb_HSPR_Entrance_Group(CommID, BuildSNum, UnitSNum, GroupId) VALUES (@CommID, @BuildSNum, @UnitSNum, @GroupId)", new { CommID, BuildSNum, UnitSNum, GroupId });
                        });
                    }
                    #endregion
                    // 群组创建完成

                    #region 更新设备与群组的关系
                    Dictionary<long, long[]> DeviceBind = new Dictionary<long, long[]>();
                    // 查询已更新设备ID的设备信息
                    List<long> DeviceList = erpConn.Query<long>("SELECT DeviceId FROM Tb_HSPR_Entrance_Device WITH(NOLOCK) WHERE ISNULL(DeviceId, 0) != 0").ToList();
                    if(null != DeviceList)
                    {
                        DeviceList.ForEach(DeviceId =>
                        {
                            long[] GroupIdList = erpConn.Query<long>("SELECT b.GroupId FROM Tb_HSPR_Entrance_Device a WITH(NOLOCK) LEFT JOIN Tb_HSPR_Entrance_Group b ON a.CommID = b.CommID AND a.BuildSNum = b.BuildSNum AND a.UnitSNum = b.UnitSNum WHERE a.DeviceId = @DeviceId", new { DeviceId }).ToArray();
                            if(null != GroupIdList && GroupIdList.Length > 0)
                            {
                                DeviceBind.Add(DeviceId, GroupIdList);
                            }
                        });
                        
                    }
                    Parallel.ForEach(DeviceBind, (item) =>
                    {
                        try
                        {
                            long deviceId = item.Key;
                            long[] groupIdList = item.Value;
                            IMoredianApiClient client = new DefaultMoredianApiClient(Config.MoreDian.APIURL);
                            MoredianUpdateDeviceBindRequest request = new MoredianUpdateDeviceBindRequest
                            {
                                deviceId = deviceId,
                                groupIdList = groupIdList
                            };
                            MoredianUpdateDeviceBindResponse response = client.Execute(request, app_token, access_token);
                            // 不处理结果，后续可能会记录绑定关系到数据库
                        }
                        catch (Exception ex)
                        {
                            Log.WriteLog(ex.Message);
                        }
                    });
                    #endregion
                    // 后续绑定成员的时候进行更新群组
                    return true;
                }
            }
            catch (Exception ex)
            {
                // 内部会往外部抛异常，由外层统一接收处理
                Log.WriteLog(ex.Message);
                return false;
            }
        }
    }
}
