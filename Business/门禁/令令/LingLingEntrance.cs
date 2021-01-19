using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Business
{
    public class LingLingEntrance : PubInfo
    {
        private static string UrlPrefix = "http://llkmc.izhihuicheng.net:8889";
        protected string LL_Signature = "7e321cf4-9f6f-43e4-b183-c460003829c3";
        protected string LL_Token = "1552529343738";
        protected string LL_OpenToken = "7A57DC4EFD8CC6E333E5CC2BFA11144A";

        public LingLingEntrance()
        {
            Token = "20190528LingLingEntrance";
        }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            try
            {
                switch (Trans.Command)
                {
                    case "GetDeviceList":
                        Trans.Result = GetDeviceList(Row);      // 获取有权限开的门
                        break;
                    case "GetSDKKey":
                        Trans.Result = GetSDKKey(Row);          // 生成 APP SDK 所使用的开门密钥，该开门密钥可以放到 SDK 开门使用，可以生成二维码
                        break;
                    case "GetSDKKeysAndActionId":
                        Trans.Result = GetSDKKeysAndActionId(Row);  // 批量生成开门密钥
                        break;
                    case "GetActionId":
                        Trans.Result = GetActionId(Row);        // 获取指令id，用指令id标识相关动作，如开门、派梯等
                        break;
                    case "RemoteOpenDoor":
                        Trans.Result = RemoteOpenDoor(Row);     // 远程开门
                        break;
                    case "VisitorQRCode":
                        Trans.Result = VisitorQRCode(Row);      // 访客二维码
                        break;
                    case "CustomerQRCode":
                        Trans.Result = CustomerQRCode(Row);     // 业主二维码
                        break;
                }
            }
            catch (Exception ex)
            {
                TWLogger.Info(ex.Message + "\r\n" + ex.StackTrace);
                Trans.Result = new ApiResult(false, ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source).toJson();
            }
        }

        /// <summary>
        /// 获取有权限开的门
        /// </summary>
        protected virtual string GetDeviceList(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户id不能为空");
            }

            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            var userId = row["UserID"].ToString();
            var commId = row["CommID"].ToString();

            using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT * FROM Tb_Community WHERE Id=convert(nvarchar(36),@CommID) OR convert(nvarchar(36),CommID)=@CommID";
                var community = appConn.Query<Tb_Community>(sql, new { CommID = commId, UserId = userId }).FirstOrDefault();

                if (community == null)
                {
                    return JSONHelper.FromString(false, "未找到小区");
                }

                sql = @"SELECT RoomID FROM Tb_User_Relation WHERE CommunityId=@CommunityId AND UserId=@UserId AND isnull(Locked,0)=0";
                var rooms = appConn.Query<long>(sql, new { CommunityId = community.Id, UserId = userId });

                if (null == rooms || rooms.Count() <= 0)
                {
                    return JSONHelper.FromString(false, "您还未绑定房屋，请先绑定");
                }

                sql = $@"SELECT DeviceId,DeviceName,DeviceCode FROM Tb_HSPR_KeyDoorDeviceSetting 
                        WHERE CommID=@CommID AND DoorType=1 AND isnull(IsDelete,0)=0
                        UNION
                        SELECT DeviceId,DeviceName,DeviceCode FROM Tb_HSPR_KeyDoorDeviceSetting 
                        WHERE CommID=@CommID AND DoorType=2 AND isnull(IsDelete,0)=0
                        AND BuildSNum IN
                        (
                            SELECT BuildSNum FROM Tb_HSPR_Room WHERE CommID=@CommID AND RoomID IN @RoomIDList
                        )";

                using (var erpConn = new SqlConnection(GetConnectionStr(community)))
                {
                    var resultSet = erpConn.Query(sql, new { CommID = community.CommID, UserId = userId, RoomIDList = rooms.ToArray() });
                    return new ApiResult(true, resultSet).toJson();
                }
            }
        }

        /// <summary>
        /// 生成 APP SDK 所使用的开门密钥，该开门密钥可以放到 SDK 开门使用，可以生成二维码
        /// </summary>
        private string GetSDKKey(DataRow row)
        {
            if (!row.Table.Columns.Contains("DeviceIds") || string.IsNullOrEmpty(row["DeviceIds"].ToString()))
            {
                return JSONHelper.FromString(false, "设备列表不能为空");
            }

            var list = GetSDKKey(row["DeviceIds"].ToString().Split(',').ToList());
            if (list == null || list.Count == 0)
                return JSONHelper.FromString(false, "未登记门禁设备或无开门权限");

            return new ApiResult(true, list).toJson();
        }

        private List<object> GetSDKKey(List<string> devices)
        {
            var command = "/cgi-bin/key/makeSdkKey/" + LL_OpenToken;
            var MESSAGE = new
            {
                requestParam = new
                {
                    deviceIds = devices,
                },
                header = new
                {
                    signature = LL_Signature,
                    token = LL_Token
                }
            };

            var jresult = Request(command, JsonConvert.SerializeObject(MESSAGE));

            if (jresult.HasValues == false)
                return null;

            var list = new List<object>();
            foreach (JProperty item in jresult)
            {
                list.Add(new { DeviceID = item.Name, SDKKey = item.Value.ToString() });
            }
            return list;
        }

        /// <summary>
        /// 批量生成开门密钥
        /// </summary>
        private string GetSDKKeysAndActionId(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户id不能为空");
            }

            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            var userId = row["UserID"].ToString();
            var commId = row["CommID"].ToString();

            using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT * FROM Tb_Community WHERE Id=convert(nvarchar(36),@CommID) OR convert(nvarchar(36),CommID)=@CommID";
                var community = appConn.Query<Tb_Community>(sql, new { CommID = commId, UserId = userId }).FirstOrDefault();

                if (community == null)
                {
                    return JSONHelper.FromString(false, "未找到小区");
                }

                sql = @"SELECT RoomID FROM Tb_User_Relation WHERE CommunityId=@CommunityId AND UserId=@UserId AND isnull(Locked,0)=0";
                var rooms = appConn.Query<long>(sql, new { CommunityId = community.Id, UserId = userId });

                if (null == rooms || rooms.Count() <= 0)
                {
                    return JSONHelper.FromString(false, "您还未绑定房屋，请先绑定");
                }

                sql = $@"SELECT DeviceId FROM Tb_HSPR_KeyDoorDeviceSetting 
                        WHERE CommID=@CommID AND DoorType=1 AND isnull(IsDelete,0)=0
                        UNION
                        SELECT DeviceId FROM Tb_HSPR_KeyDoorDeviceSetting a
                        WHERE CommID=@CommID AND DoorType=2 AND isnull(IsDelete,0)=0 AND isnull(RoomIDs,'')<>''
                        AND (SELECT count(1) FROM (
		                        SELECT Value FROM funSplit(a.RoomIDs,',') 
		                        InterSect 
		                        SELECT Value FROM funSplit(@RoomIDs,',')
	                        ) as t)>0";

                using (var erpConn = new SqlConnection(GetConnectionStr(community)))
                {
                    var devices = erpConn.Query<string>(sql, new { CommID = community.CommID, UserId = userId, RoomIDs = string.Join(",", rooms) });
                    if (devices.Count() == 0)
                        return JSONHelper.FromString(false, "未登记门禁设备或无开门权限");

                    var list = GetSDKKey(devices.ToList());
                    if (list == null || list.Count == 0)
                        return JSONHelper.FromString(false, "获取开门钥匙失败");

                    return new ApiResult(true, new { SDKKeys = list, ActionId = GetActionId() }).toJson();
                }
            }
        }

        /// <summary>
        /// 获取指令id，用指令id标识相关动作，如开门、派梯等
        /// </summary>
        private string GetActionId(DataRow row)
        {
            var actionId = GetActionId();
            if (actionId == null)
                return JSONHelper.FromString(false, "获取开门指令失败");

            return JSONHelper.FromString(true, actionId);
        }

        private string GetActionId()
        {
            var command = "/cgi-bin/qrcode/getLingLingId/" + LL_OpenToken;
            var MESSAGE = new
            {
                requestParam = new
                {

                },
                header = new
                {
                    signature = LL_Signature,
                    token = LL_Token
                }
            };

            var jresult = (JObject)Request(command, JsonConvert.SerializeObject(MESSAGE));
            if (jresult == null)
                return null;

            return jresult.GetValue("lingLingId").ToString();
        }

        /// <summary>
        /// 生成业主开门二维码
        /// </summary>
        private string CustomerQRCode(DataRow row)
        {
            if (!row.Table.Columns.Contains("SDKKeys") || string.IsNullOrEmpty(row["SDKKeys"].ToString()))
            {
                return JSONHelper.FromString(false, "开门密钥不能为空");
            }
            if (!row.Table.Columns.Contains("ActionId") || string.IsNullOrEmpty(row["ActionId"].ToString()))
            {
                return JSONHelper.FromString(false, "指令id不能为空");
            }

            var sdkKeys = row["SDKKeys"].ToString();
            var actionId = row["ActionId"].ToString();

            var command = "/cgi-bin/qrcode/addOwnerQrCode/" + LL_OpenToken;
            var MESSAGE = new
            {
                requestParam = new
                {
                    lingLingId = actionId,
                    sdkKeys = sdkKeys.Split(','),
                    endTime = 10,
                    effecNumber = 1,
                    strKey = "12345678"
                },
                header = new
                {
                    signature = LL_Signature,
                    token = LL_Token
                }
            };

            var jresult = Request(command, JsonConvert.SerializeObject(MESSAGE));
            if (jresult == null)
                return JSONHelper.FromString(false, "生成开门二维码失败");

            var qrcodeKey = jresult["qrcodeKey"].ToString();
            return JSONHelper.FromString(true, qrcodeKey);
        }

        /// <summary>
        /// 生成访客开门二维码
        /// </summary>
        private string VisitorQRCode(DataRow row)
        {
            if (!row.Table.Columns.Contains("SDKKeys") || string.IsNullOrEmpty(row["SDKKeys"].ToString()))
            {
                return JSONHelper.FromString(false, "开门密钥不能为空");
            }
            if (!row.Table.Columns.Contains("ActionId") || string.IsNullOrEmpty(row["ActionId"].ToString()))
            {
                return JSONHelper.FromString(false, "指令id不能为空");
            }

            var sdkKeys = row["SDKKeys"].ToString();
            var actionId = row["ActionId"].ToString();

            var endTime = 10;
            if (row.Table.Columns.Contains("EndTime") && !string.IsNullOrEmpty(row["EndTime"].ToString()))
            {
                endTime = AppGlobal.StrToInt(row["EndTime"].ToString());
            }

            var command = "/cgi-bin/qrcode/addVisitorQrCode/" + LL_OpenToken;
            var MESSAGE = new
            {
                requestParam = new
                {
                    lingLingId = actionId,
                    sdkKeys = sdkKeys.Split(','),
                    startTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    endTime = endTime,
                    effecNumber = 6,
                    strKey = "12345678"
                },
                header = new
                {
                    signature = LL_Signature,
                    token = LL_Token
                }
            };

            var jresult = Request(command, JsonConvert.SerializeObject(MESSAGE));
            if (jresult == null)
                return JSONHelper.FromString(false, "生成访客二维码失败");

            var obj = new
            {
                CodeId = jresult["codeId"].ToString(),
                QRCodeKey = jresult["qrcodeKey"].ToString()
            };
            return new ApiResult(true, obj).toJson();
        }

        /// <summary>
        /// 远程开门
        /// </summary>
        private string RemoteOpenDoor(DataRow row)
        {
            if (!row.Table.Columns.Contains("SDKKey") || string.IsNullOrEmpty(row["SDKKey"].ToString()))
            {
                return JSONHelper.FromString(false, "开门密钥不能为空");
            }

            var sdkKey = row["SDKKey"].ToString();

            var command = "/cgi-bin/key/remoteOpenDoor/" + LL_OpenToken;
            var MESSAGE = new
            {
                requestParam = new
                {
                    sdkKey = sdkKey,
                },
                header = new
                {
                    signature = LL_Signature,
                    token = LL_Token
                }
            };

            var jresult = Request(command, JsonConvert.SerializeObject(MESSAGE));
            if (jresult == null)
                return JSONHelper.FromString(false, "开门失败");

            return JSONHelper.FromString(true, jresult.ToString());
        }

        public JToken Request(string command, string MESSAGE, string method = "POST")
        {
            var url = UrlPrefix + command + $"?MESSAGE={MESSAGE}";
            WebRequest request = WebRequest.Create(url);
            request.Method = method;
            request.ContentType = "text/xml";

            WebResponse wResp = request.GetResponse();
            Stream respStream = wResp.GetResponseStream();

            using (StreamReader reader = new StreamReader(respStream, Encoding.GetEncoding("UTF-8")))
            {
                var responseResult = reader.ReadToEnd();

                if (string.IsNullOrEmpty(responseResult) == false)
                {
                    var status = default(JToken);

                    var jobj = (JObject)JsonConvert.DeserializeObject(responseResult);
                    if (jobj.HasValues && jobj.TryGetValue("statusCode", out status))
                    {
                        return jobj.GetValue("responseResult");
                    }
                }
            }

            return null;
        }
    }
}
