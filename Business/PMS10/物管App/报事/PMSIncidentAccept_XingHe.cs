using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Business.WChat2020;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Dapper.SqlMapper;

namespace Business
{
    public partial class PMSIncidentAccept
    {
        /// <summary>
        /// 星河工单来源 0 商置 1产业 2物业左邻右家
        /// </summary>
        /// <param name="CommID"></param>
        /// <param name="RoomID"></param>
        /// <param name="IncidentID"></param>
        /// <param name="order_cont"></param>
        /// <param name="user_phone"></param>
        /// <param name="user_name"></param>
        private void XingHeIncidentPush(int CommID, long RoomID, string IncidentID, string order_cont, string user_phone, string user_name, string imgs)
        {
            var hmWyglConnectionString = PubConstant.hmWyglConnectionString;

            Task.Run(() =>
            {
                try
                {
                    Log($"准备推送星河工单：{CommID},{RoomID},{IncidentID},{order_cont},{user_phone},{user_name},{imgs}", "XingHeIncidentPush\\");
                    // 查询产业类型（其他情况报错，就代表没这个字段，不推）
                    using (var conn = new SqlConnection(hmWyglConnectionString))
                    {
                        string OrganCode = string.Empty;
                        // 1=产业系统，其他系统暂时不推送
                        int WorkOrderSystem = -1;
                        try
                        {
                            WorkOrderSystem = conn.QueryFirstOrDefault<int>("SELECT ISNULL(WorkOrderSystem,-1) AS WorkOrderSystem FROM Tb_HSPR_Community WHERE ISNULL(IsDelete,0) = 0 AND CommID = @CommID", new { CommID });
                        }
                        catch (Exception ex)
                        {
                            GetLog().Error(ex);
                            return;
                        }
                        string order_number = string.Empty;
                        if (WorkOrderSystem == 0)
                        {
                            string orgId = conn.QueryFirstOrDefault<string>("SELECT WorkOrderCommID FROM Tb_HSPR_Community WHERE ISNULL(IsDelete,0) = 0 AND CommID = @CommID", new { CommID });
                            dynamic room_info = conn.QueryFirstOrDefault("SELECT CommName,BuildName,UnitName,FloorSNum,RoomSNum,RoomName FROM view_HSPR_Room_Filter WHERE ISNULL(IsDelete,0) = 0 AND CommID = @CommID AND RoomID = @RoomID", new { CommID, RoomID });
                            string buiName = string.Empty;
                            string unitName = string.Empty;
                            string roomName = string.Empty;
                            string name = user_name;
                            string phone = user_phone;
                            string content = order_cont;
                            if (null == room_info)
                            {
                                buiName = "无";
                                unitName = "无";
                                roomName = "无";
                            }
                            else
                            {
                                buiName = Convert.ToString(room_info.BuildName);
                                unitName = Convert.ToString(room_info.UnitName);
                                roomName = Convert.ToString(room_info.RoomName);
                            }
                            // 项目+楼栋+楼层+房号
                            if (!XingHeIncidentShangZhiPush(orgId, buiName, unitName, roomName, name, phone, content, imgs, out order_number))
                            {
                                order_number = string.Empty;
                            }
                            Log($"推送星河商置工单结果:{order_number}", "XingHeIncidentPush\\");
                        }
                        else if (WorkOrderSystem == 1)
                        {
                            string orgId = conn.QueryFirstOrDefault<string>("SELECT WorkOrderCommID FROM Tb_HSPR_Community WHERE ISNULL(IsDelete,0) = 0 AND CommID = @CommID", new { CommID });
                            dynamic room_info = conn.QueryFirstOrDefault("SELECT CommName,BuildName,FloorSNum,RoomSNum,RoomName FROM view_HSPR_Room_Filter WHERE ISNULL(IsDelete,0) = 0 AND CommID = @CommID AND RoomID = @RoomID", new { CommID, RoomID });
                            string order_address = string.Empty;
                            if (null == room_info)
                            {
                                order_address = "未查询到房屋名称信息";
                            }
                            else
                            {
                                order_address = $"{room_info.CommName}-{room_info.BuildName}-{room_info.FloorSNum}-{room_info.RoomName}";
                            }
                            // 项目+楼栋+楼层+房号
                            if (!XingHeIncidentChanyePush(orgId, order_address, order_cont, user_phone, user_name, out order_number))
                            {
                                order_number = string.Empty;
                            }
                            Log($"推送星河产业工单结果:{order_number}", "XingHeIncidentPush\\");
                        }
                        //2物业左邻右家
                        else if (WorkOrderSystem == 2)
                        {
                            string orgId = conn.QueryFirstOrDefault<string>("SELECT WorkOrderCommID FROM Tb_HSPR_Community WHERE ISNULL(IsDelete,0) = 0 AND CommID = @CommID", new { CommID });
                            dynamic room_info = conn.QueryFirstOrDefault("SELECT CommName,BuildName,FloorSNum,RoomSNum,RoomName FROM view_HSPR_Room_Filter WHERE ISNULL(IsDelete,0) = 0 AND CommID = @CommID AND RoomID = @RoomID", new { CommID, RoomID });
                            string order_address = string.Empty;
                            if (null == room_info)
                            {
                                order_address = "未查询到房屋名称信息";
                            }
                            else
                            {
                                order_address = $"{room_info.CommName}-{room_info.BuildName}-{room_info.FloorSNum}-{room_info.RoomName}";
                            }
                            // 项目+楼栋+楼层+房号
                            if (!XingHeIncidentZuolinYouShePush(orgId, order_address, order_cont, user_phone, user_name, out order_number))
                            {
                                order_number = string.Empty;
                            }
                            Log($"推送星河物业左邻右家工单结果:{order_number}", "XingHeIncidentZuolinYouShePush\\");
                        }

                        if (string.IsNullOrEmpty(order_number) || !SaveIncidentInfo(hmWyglConnectionString, OrganCode, Convert.ToString(CommID), IncidentID, order_number, WorkOrderSystem))
                        {
                            Log("推送失败或保存失败", "XingHeIncidentPush\\");
                            return;
                        }
                        Log("推送工单完成", "XingHeIncidentPush\\");
                    }

                }
                catch (Exception ex)
                {
                    Log(ex.Message, "XingHeIncidentPush\\");
                }
            });
        }

        private bool XingHeIncidentShangZhiPush(string orgId, string buiName, string unitName, string roomName, string name, string phone, string content, string imgs, out string order_number)
        {
            order_number = string.Empty;
            try
            {
                string key = "2d9h!ncehrateKey";
                string iv = "A-16-Byte-String";
                string url = $"{Global_Fun.AppWebSettings("XinHeIncidentPush_ShangZhi")}/serviceorderThirdparty/addServiceOrderByTianWen";
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("orgId", orgId);
                data.Add("buiName", buiName);
                data.Add("unitName", unitName);
                data.Add("roomName", roomName);
                data.Add("cusType", "5");
                data.Add("sourceType", "2");
                data.Add("serviceType", "1");
                data.Add("userId", phone);
                data.Add("phone", phone);
                data.Add("name", name);
                data.Add("servicecontent", content);
                data.Add("imageUrls", imgs);
                string encryptStr = JsonConvert.SerializeObject(data);
                encryptStr = AESEncrypt(encryptStr, key, iv);
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = $"{url}",//URL     必需项    
                    Method = "POST",//URL     可选项 默认为Get
                    ContentType = "application/x-www-form-urlencoded",
                    PostEncoding = Encoding.UTF8,
                    Encoding = Encoding.UTF8,
                    Postdata = $"encryptStr={System.Web.HttpUtility.UrlEncode(encryptStr)}",
                    Timeout = 5000,
                    ResultType = ResultType.String
                };
                HttpResult result = http.GetHtml(item);
                Log(result.Html, "XingHeIncidentShangZhiPush\\");
                string html = result.Html;
                JObject jObj = null;
                try
                {
                    jObj = JsonConvert.DeserializeObject<JObject>(html);
                }
                catch (Exception)
                {
                    return false;
                }
                if (null == jObj)
                {
                    return false;
                }
                if (!jObj.ContainsKey("result"))
                {
                    return false;
                }
                if (!(bool)jObj["result"])
                {
                    return false;
                }
                if (!jObj.ContainsKey("data"))
                {
                    return false;
                }
                order_number = (string)jObj["data"];
                return true;
            }
            catch (Exception ex)
            {
                Log(ex.Message, "XingHeIncidentShangZhiPush\\");
                return false;
            }
        }

        #region AES加解密
        #region AES加密
        public static string AESEncrypt(string input, string key, string iv)
        {
            if (string.IsNullOrEmpty(input)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(input);
            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = System.Security.Cryptography.CipherMode.CBC,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7,
                IV = Encoding.UTF8.GetBytes(iv)
            };
            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        #endregion
        #region AES解密
        public static string AESDecrypt(string input, string key, string iv)
        {
            if (string.IsNullOrEmpty(input)) return null;
            Byte[] toEncryptArray = Convert.FromBase64String(input);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = System.Security.Cryptography.CipherMode.CBC,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7,
                IV = Encoding.UTF8.GetBytes(iv)
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor(rm.Key, rm.IV);
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }
        #endregion
        #endregion

        /// <summary>
        /// 星河产业推送
        /// </summary>
        /// <returns></returns>
        public bool XingHeIncidentChanyePush(string zone_id, string order_address, string order_cont, string user_phone, string user_name, out string order_number)
        {
            order_number = string.Empty;
            try
            {
                string url = $"{Global_Fun.AppWebSettings("XinHeIncidentPush_ChanYe")}";
                string path = "/group/order/add";
                string secret_key = "SDFDCDE12DD";
                SortedDictionary<string, string> values = new SortedDictionary<string, string>();
                values.Add("zone_id", zone_id);
                values.Add("order_address", order_address);
                values.Add("order_cont", order_cont);
                values.Add("type", "0");
                values.Add("user_phone", user_phone);
                values.Add("user_name", user_name);
                string str = string.Empty;
                foreach (var value in values)
                {
                    str += $"{value.Key}={value.Value}&";
                }
                string sign = $"POST&{path}&{str}{secret_key}";
                sign = unionqmf.utils.UnionPaySdk.MD5(sign).ToLower();
                str += $"sign={sign}";
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = $"{url}{path}",//URL     必需项    
                    Method = "POST",//URL     可选项 默认为Get
                    ContentType = "application/x-www-form-urlencoded",
                    PostEncoding = Encoding.UTF8,
                    Postdata = str,
                    Encoding = Encoding.UTF8,
                    ResultType = ResultType.String
                };
                HttpResult result = http.GetHtml(item);
                Log(result.Html, "XingHeIncidentChanyePush\\");
                string html = result.Html;
                JObject jObj = null;
                try
                {
                    jObj = JsonConvert.DeserializeObject<JObject>(html);
                }
                catch (Exception)
                {
                    return false;
                }
                if (null == jObj)
                {
                    return false;
                }
                if (!jObj.ContainsKey("s"))
                {
                    return false;
                }
                if ((int)jObj["s"] != 1)
                {
                    return false;
                }
                if (!jObj.ContainsKey("d"))
                {
                    return false;
                }
                jObj = (JObject)jObj["d"];
                if (!jObj.ContainsKey("order_number"))
                {
                    return false;
                }
                order_number = (string)jObj["order_number"];
                return true;
            }
            catch (Exception ex)
            {
                Log(ex.Message, "XingHeIncidentChanyePush\\");
                return false;
            }
        }


        /// <summary>
        /// 星河产业推送
        /// </summary>
        /// <returns></returns>
        public bool XingHeIncidentZuolinYouShePush(string zone_id, string order_address, string order_cont, string user_phone, string user_name, out string order_number)
        {
            order_number = string.Empty;
            //try
            //{
            //    string url = $"{Global_Fun.AppWebSettings("XHZuoLinYouShe")}";
            //    var requestParam = new
            //    {
            //        flag = "CreateCustomerService",
            //        user_name = user_name,
            //        user_phone = user_phone,
            //        add_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            //        order_cont = order_cont,
            //        order_address = order_address,
            //        org_number = zone_id
            //    };
            //    var result = Common.HttpUtillib.GeneralHttpPostStr(url, requestParam);
            //    var httpResult = JsonConvert.DeserializeObject<XHBaseResponseModel<String>>(result);
            //    if (httpResult.Result) order_number = httpResult.Data;
            //    return httpResult.Result;
            //}
            //catch (Exception ex)
            //{
            //    Log(ex.Message, "XingHeIncidentChanyePush\\");
            //}
            return false;
        }



        /// <summary>
        /// 保存报事结果
        /// </summary>
        /// <param name="OrganCode"></param>
        /// <param name="CommID"></param>
        /// <param name="IncidentID"></param>
        /// <param name="OrgIncidentID"></param>
        /// <param name="OrgIncidentSource">星河工单来源 0 商置 1产业 2物业左邻右家</param>
        /// <returns></returns>
        public bool SaveIncidentInfo(string erpConnStr, string OrganCode, string CommID, string IncidentID, string OrgIncidentID, int OrgIncidentSource)
        {

            try
            {
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    if (conn.Execute("INSERT INTO Tb_XH_IncidentConnect(ID,OrganCode,CommID,IncidentID,OrgIncidentID,OrgIncidentSource) VALUES(NEWID(),@OrganCode,@CommID,@IncidentID,@OrgIncidentID,@OrgIncidentSource)", new { OrganCode, CommID, IncidentID, OrgIncidentID, OrgIncidentSource }) <= 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message, "XingHeIncidentPush\\");
                return false;
            }
        }
    }
}
