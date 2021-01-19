using Aop.Api;
using Aop.Api.Model;
using Aop.Api.Request;
using Aop.Api.Response;
using Dapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianChengEntranceSyncService.Redis;
using Log = TianChengEntranceSyncService.Util.Log;

namespace TianChengEntranceSyncService.同步任务
{
    public class SyncDeviceWork
    {
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
                using (IDbConnection erpConn = new SqlConnection(Config.EntranceConnectionStr))
                {
                    #region 初始化设备信息
                    // 先查询设备列表，如果不存在设备，后续都不用进行了
                    List<dynamic> DeviceList = erpConn.Query("SELECT Id, DeviceSn, DeviceId, CommID, BuildSNum, UnitSNum FROM Tb_HSPR_Entrance_Device WITH(NOLOCK)").ToList();
                    if (null == DeviceList || DeviceList.Count <= 0)
                    {
                        // 如果不存在设备，直接返回，不再进行下一步
                        return false;
                    }
                    // 先查询未配置DeviceId的列表，去查询DeviceId信息
                    DeviceList = erpConn.Query("SELECT Id, DeviceSn, DeviceId FROM Tb_HSPR_Entrance_Device WITH(NOLOCK) WHERE ISNULL(DeviceId, 0) = 0").ToList();
                    // 检查设备配置信息，如果未获取DeveceId,调用接口查询一下
                    ConcurrentBag<Dictionary<string, string>> CreatedDeviceList = new ConcurrentBag<Dictionary<string, string>>();
                    Parallel.ForEach(new ConcurrentBag<dynamic>(DeviceList), (item) => 
                    {
                        try
                        {
                            string Id = Convert.ToString(item.Id);
                            string DeviceSn = Convert.ToString(item.DeviceSn);
                            IMoredianApiClient client = new DefaultMoredianApiClient(Config.MoreDian.APIURL);
                            MoredianQueryDeviceIdRequest request = new MoredianQueryDeviceIdRequest
                            {
                                deviceSn = DeviceSn
                            };
                            MoredianQueryDeviceIdResponse response = client.Execute(request, app_token, access_token);
                            if (response.IsSucc())
                            {
                                string DeviceId = Convert.ToString(response.deviceId);
                                CreatedDeviceList.Add(new Dictionary<string, string>
                                {
                                    { "DeviceId", DeviceId },
                                    { "DeviceSn", DeviceSn },
                                    { "Id", Id }
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.WriteLog(ex.Message);
                        }
                    });
                    #endregion

                    #region 对查询结果回写进数据库
                    List<Dictionary<string, string>> CreatedDeviceArray = new List<Dictionary<string, string>>(CreatedDeviceList);
                    if(null != CreatedDeviceArray)
                    {
                        CreatedDeviceArray.ForEach(item =>
                        {
                            string DeviceId = item["DeviceId"];
                            string DeviceSn = item["DeviceSn"];
                            string Id = item["Id"];
                            erpConn.Execute("UPDATE Tb_HSPR_Entrance_Device SET DeviceId = @DeviceId WHERE DeviceSn = @DeviceSn AND Id = @Id", new { DeviceId, DeviceSn, Id });
                        });
                    }
                    #endregion
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
