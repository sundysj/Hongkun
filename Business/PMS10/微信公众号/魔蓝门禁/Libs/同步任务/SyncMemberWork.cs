using Aop.Api;
using Aop.Api.Model;
using Aop.Api.Request;
using Aop.Api.Response;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianChengEntranceSyncService.Model;
using TianChengEntranceSyncService.Redis;
using TianChengEntranceSyncService.Util;
using Log = TianChengEntranceSyncService.Util.Log;

namespace TianChengEntranceSyncService.同步任务
{
    public class SyncMemberWork
    {
        public static bool Run(MoredianOrg moredianOrg)
        {
            if (null == moredianOrg)
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

                using (IDbConnection conn = new SqlConnection(Config.WChat2020ConnectionStr)
                    , erpConn = new SqlConnection(Config.EntranceConnectionStr))
                {
                    #region 根据成员列表对应的手机号查询WChat2020公众号中绑定的所有房屋列表
                    List<string> MobileList = erpConn.Query<string>("SELECT Mobile FROM Tb_HSPR_Entrance_Member WITH(NOLOCK) GROUP BY Mobile").ToList();
                    if (null == MobileList || MobileList.Count <= 0)
                    {
                        // 如果不存在成员，直接返回，不再进行下一步
                        return false;
                    }
                    // 根据手机号查询对于用户绑定的所有房屋并去重
                    List<string> RoomIDList = conn.Query<string>("SELECT a.RoomID FROM Tb_User_Bind a WITH(NOLOCK) JOIN Tb_User b WITH(NOLOCK) ON a.UserID = b.Id WHERE ISNULL(a.IsDelete,0) = 0 AND b.Mobile IN @MobileList GROUP BY a.RoomID", new { MobileList }).ToList();
                    if (null == RoomIDList || RoomIDList.Count <= 0)
                    {
                        // 如果不存在房屋，直接返回，不再进行下一步
                        return false;
                    }
                    #endregion

                    #region 根据群组查出要关联的房屋信息，通过房屋RoomID查询出要关联的MemberId
                    // 后面要遍历更新权限的内容
                    Dictionary<long, long[]> MemberBindList = new Dictionary<long, long[]>();
                    // 先查询所有的群组信息
                    List<EntranceGroup> EntranceGroupList = erpConn.Query<EntranceGroup>("SELECT Id, CommID, BuildSNum, UnitSNum, GroupId FROM Tb_HSPR_Entrance_Group WITH(NOLOCK)").ToList();
                    if (null == EntranceGroupList || EntranceGroupList.Count <= 0)
                    {
                        // 不存在群组信息，不进行下一步
                        return false;
                    }
                    // 查询所有应该分配权限的房间列表
                    List<EntranceRoom> EntranceRoomList = erpConn.Query<EntranceRoom>("SELECT CommID, BuildSNum, UnitSNum, RoomID FROM view_HSPR_Room_Filter WITH(NOLOCK) WHERE ISNULL(IsDelete, 0) = 0 AND RoomID IN @RoomIDList", new { RoomIDList }).ToList();
                    if (null == EntranceRoomList || EntranceRoomList.Count <= 0)
                    {
                        // 如果不存在房屋，直接返回，不再进行下一步
                        return false;
                    }
                    // 遍历所有群组，将需要更新的权限写入
                    EntranceGroupList.ForEach(item =>
                    {
                        List<EntranceRoom> RoomList;
                        if (item.BuildSNum == 0 && item.UnitSNum == 0)
                        {
                            // 大门
                            RoomList = EntranceRoomList.Where(Room => Room.CommID == item.CommID).ToList();
                        }
                        else
                        {
                            // 单元门
                            RoomList = EntranceRoomList.Where(Room => Room.CommID == item.CommID && Room.BuildSNum == item.BuildSNum && Room.UnitSNum == item.UnitSNum).ToList();
                        }
                        if(null == RoomList || RoomList.Count <= 0)
                        {
                            // 如果没有房屋列表，不进行操作
                            return;
                        }
                        // 根据房屋列表信息，查询对应的手机号
                        List<string> RoomMobileList = conn.Query<string>("SELECT Mobile FROM Tb_User a WITH(NOLOCK) JOIN Tb_User_Bind b WITH(NOLOCK) ON a.Id = b.UserID WHERE ISNULL(b.IsDelete, 0) = 0 AND b.RoomID IN @RoomIDList", new { RoomIDList = RoomList.ConvertAll(room => room.RoomID).ToArray() }).ToList();
                        if (null == RoomList || RoomList.Count <= 0)
                        {
                            // 如果没有手机号列表，不进行操作
                            return;
                        }
                        long[] MemberList = erpConn.Query<long>("SELECT MemberId FROM Tb_HSPR_Entrance_Member WITH(NOLOCK) WHERE Mobile IN @RoomMobileList", new { RoomMobileList }).ToArray();
                        if (null == MemberList || MemberList.Length <= 0)
                        {
                            // 如果没有成员列表，不进行操作
                            return;
                        }
                        MemberBindList.Add(item.GroupId, MemberList);
                    });
                    DateTime DateNow = DateTime.Now;
                    // 开始时间限制必须大于当前时间，所以限定生效时间为10分钟后
                    DateNow = DateNow.AddMinutes(10);
                    long beginDay = DateHelper.Get13TimeStamp(DateNow);
                    // 默认10年
                    long endDay = DateHelper.Get13TimeStamp(DateNow.AddYears(10));
                    // 进行遍历关联
                    Parallel.ForEach(MemberBindList, (item) =>
                    {
                        try
                        {
                            long groupId = item.Key;
                            long[] memberIdList = item.Value;
                            IMoredianApiClient client = new DefaultMoredianApiClient(Config.MoreDian.APIURL);
                            MoredianUpdateGroupRequest request = new MoredianUpdateGroupRequest
                            {
                                moredianGroup = new
                                {
                                    groupId,
                                    allMemberStatus = 0,
                                    dayBeginTime = "00:00",
                                    dayEndTime = "23:59",
                                    beginDay = beginDay,
                                    endDay = endDay,
                                    memberIdList,
                                }
                            };
                            MoredianUpdateGroupResponse response = client.Execute(request, app_token, access_token);
                            // 不处理结果，后续可能会记录绑定关系到数据库
                        }
                        catch (Exception ex)
                        {
                            Log.WriteLog(ex.Message);
                        }
                    });
                    #endregion
                }
                return true;
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
