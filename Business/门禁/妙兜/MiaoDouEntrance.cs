using Common;
using Dapper;
using MobileSoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;

namespace Business
{
    /// <summary>
    /// 妙兜门禁
    /// </summary>
    public class MiaoDouEntrance : PubInfo
    {
        protected static string UrlPrefix = "http://121.40.204.191:18080";
        protected string app_key = null;
        protected string app_num = null;

        public MiaoDouEntrance()
        {
            base.Token = @"20181127MiaoDouEntrance";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            if (Row.Table.Columns.Contains("AppKey") && Row.Table.Columns.Contains("AppNum"))
            {
                app_key = Row["AppKey"].ToString();
                app_num = Row["AppNum"].ToString();
            }

            if (string.IsNullOrEmpty(app_key) || string.IsNullOrEmpty(app_num))
            {
                Trans.Result = JSONHelper.FromString(false, "未配置AppKey或AppNum");
                return;
            }

            switch (Trans.Command)
            {
                case "getUserKey":
                    Trans.Result = GetUserKeys(Row);//获取用户开门钥匙
                    break;
                case "addCommunity":
                    Trans.Result = AddCommunity(Row);//添加小区
                    break;
                case "getCommunity":
                    Trans.Result = GetCommunity(Row);//获取小区
                    break;
                case "queryDevice":
                    Trans.Result = QueryDevice(Row);//查询设备门禁PID
                    break;
                case "installLock":
                    Trans.Result = InstallLock(Row);//安装门禁登记
                    break;
                case "applyKeys":
                    Trans.Result = ApplyKeys(Row);//申请用户钥匙凭证
                    break;
                case "wx_bindDevice":
                    Trans.Result = WX_BindDevice(Row);//绑定微信用户
                    break;
                case "wx_getInfoKey":
                    Trans.Result = WX_GetInfoKey(Row);//获取微信开门相关信息
                    break;
                case "wx_createMKey":
                    Trans.Result = WX_CreateMKey(Row);//生成访客钥匙
                    break;
                case "wx_activeMkey":
                    Trans.Result = WX_ActiveMkey(Row);//激活访客钥匙
                    break;
                case "wx_synDeviceService":
                    Trans.Result = WX_SyncDeviceService(Row);//激活访客钥匙
                    break;
                case "wx_OpenDoor":
                    Trans.Result = WX_OpenDoor(Row);//获取微信开门密钥
                    break;
                case "CreateShareKey":
                    Trans.Result = CreateShareKey(Row);//获取微信开门密钥
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 创建分享钥匙记录
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string CreateShareKey(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户ID不能为空");
            }
            string UserID = row["UserID"].ToString();
            if (!row.Table.Columns.Contains("KID") || string.IsNullOrEmpty(row["KID"].ToString()))
            {
                return JSONHelper.FromString(false, "钥匙ID不能为空");
            }
            string Kid = row["KID"].ToString();
            if (!row.Table.Columns.Contains("expireTime") || string.IsNullOrEmpty(row["expireTime"].ToString()))
            {
                return JSONHelper.FromString(false, "有效期不能为空");
            }
            string InviteUserName = string.Empty;
            if (row.Table.Columns.Contains("InviteUserName"))
            {
                InviteUserName = row["InviteUserName"].ToString();
            }
            string InviteUserMobile = string.Empty;
            if (row.Table.Columns.Contains("InviteUserMobile"))
            {
                InviteUserMobile = row["InviteUserMobile"].ToString();
            }
            DateTime expireTime = DateTime.ParseExact(row["expireTime"].ToString(), "yyyyMMddHHmmss", CultureInfo.CurrentCulture);
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }
            string CommID = row["CommID"].ToString();

            string useDefaultUrl = "";
            if (row.Table.Columns.Contains("UseDefaultUrl") && !string.IsNullOrEmpty(row["UseDefaultUrl"].ToString()))
            {
                useDefaultUrl = row["UseDefaultUrl"].ToString();
                if (useDefaultUrl == "1")
                {
                    useDefaultUrl = ConfigurationManager.AppSettings["MiaoDouWeChatShareUrl"];
                }
            }

            IDbConnection conn = new SqlConnection(AppGlobal.GetConnectionString("EntranceConnectionString"));
            DynamicParameters param = new DynamicParameters();
            param.Add("ID", null, DbType.String, ParameterDirection.Output, 36);
            param.Add("Uid", UserID, DbType.String);
            param.Add("Kid", Kid, DbType.String);
            param.Add("InviteUserName", InviteUserName, DbType.String);
            param.Add("InviteUserMobile", InviteUserMobile, DbType.String); 
            param.Add("CreateTime", DateTime.Now, DbType.DateTime);
            param.Add("ExpireTime", expireTime, DbType.DateTime);
            int ret = conn.Execute("Tb_HSPR_Visitor_Record_Add", param, null, null, CommandType.StoredProcedure);
            string id = param.Get<string>("ID");
            if (ret > 0)
            {
                return "{\"Result\":\"true\", \"data\": \"" + useDefaultUrl + id + "\"}";
            }
            return "{\"Result\":\"false\", \"data\": \"分享失败\"}";
        }



        /// <summary>
        /// 获取用户开门钥匙
        /// </summary>
        /// UserID 用户手机号
        /// CommID 小区编号
        /// <returns></returns>
        public string GetUserKeys(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户ID不能为空");
            }

            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }

            string userId = row["UserID"].ToString();
            string commId = row["CommID"].ToString();

            using (IDbConnection conn = new SqlConnection(AppGlobal.GetConnectionString("EntranceConnectionString")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", userId);
                parameters.Add("@CommID", commId);
                DataTable dt = conn.ExecuteReader(@"SELECT * FROM Tb_HSPR_Key WHERE UserID=@UserID AND CommID=@CommID", parameters).ToDataSet().Tables[0];
                return JSONHelper.FromString(dt);
            }
        }

        /// <summary>
        /// 添加小区
        /// </summary>
        /// <param name="row"></param>
        /// Region          行政区域
        /// CorpID、CommID、CommName
        /// 返回
        /// true            departid  小区编号【妙兜方】
        /// false           ""  错误信息
        /// <returns></returns>

        public string AddCommunity(DataRow row)
        {
            if (!row.Table.Columns.Contains("Region") || string.IsNullOrEmpty(row["Region"].ToString()))
            {
                return JSONHelper.FromString(false, "行政区域不能为空");
            }

            //if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            //{
            //    return JSONHelper.FromString(false, "物业公司编码不能为空");
            //}

            //if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            //{
            //    return JSONHelper.FromString(false, "小区ID不能为空");
            //}

            if (!row.Table.Columns.Contains("CommName") || string.IsNullOrEmpty(row["CommName"].ToString()))
            {
                return JSONHelper.FromString(false, "小区名称不能为空");
            }
            string Url = UrlPrefix + "/mdserver/service/addCommunity";

            System.Net.HttpWebResponse response = null;
            string resultstring = string.Empty;

            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("app_key", app_key);
                parameters.Add("agt_num", app_num);
                parameters.Add("depart_name", row["CommName"].ToString());
                parameters.Add("city_code", row["Region"].ToString());

                response = HttpHelper.CreatePostHttpResponse(Url, parameters, encoding);

                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                resultstring = sr.ReadToEnd();

                stream.Close();
                sr.Close();

                IDictionary<string, object> results = (IDictionary<string, object>)HttpHelper.JsonToDictionary(resultstring);
                if (results["status"].ToString() == "success")
                {
                    resultstring = "{\"Result\":\"true\", \"data\": " + HttpHelper.SerializeToStr(results["data"]) + "}";
                }
                else
                {
                    resultstring = "{\"Result\":\"false\", \"data\": " + HttpHelper.SerializeToStr(results["msg"]) + "}";
                }
            }
            catch (Exception exp)
            {
                resultstring = "{\"Result\":\"false\", \"data\": \"" + exp.ToString() + "\"}";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return resultstring;

        }
        /// <summary>
        /// 获取小区
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string GetCommunity(DataRow row)
        {
            string departId = "";
            if (row.Table.Columns.Contains("departId"))
            {
                departId = row["departId"].ToString();
            }
            string Url = UrlPrefix + "/mdserver/service/getCommunity";

            System.Net.HttpWebResponse response = null;
            string resultstring = string.Empty;
            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("app_key", app_key);
                parameters.Add("agt_num", app_num);
                if (departId != "")
                {
                    parameters.Add("departId", departId);
                }

                response = HttpHelper.CreatePostHttpResponse(Url, parameters, encoding);

                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                resultstring = sr.ReadToEnd();

                stream.Close();
                sr.Close();

                IDictionary<string, object> results = (IDictionary<string, object>)HttpHelper.JsonToDictionary(resultstring);
                if (results["status"].ToString() == "success")
                {
                    resultstring = "{\"Result\":\"true\", \"data\": " + HttpHelper.SerializeToStr(results["data"]) + "}";
                }
                else
                {
                    resultstring = "{\"Result\":\"false\", \"data\": " + HttpHelper.SerializeToStr(results["msg"]) + "}";
                }
            }
            catch (Exception exp)
            {
                resultstring = "{\"Result\":\"false\", \"data\": \"" + exp.ToString() + "\"}";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return resultstring;
        }

        /// <summary>
        /// 查询设备门禁PID
        /// </summary>
        /// pid PID编号 【选填】
        /// <param name="row"></param>
        /// <returns></returns>
        public string QueryDevice(DataRow row)
        {
            string pid = "";
            if (row.Table.Columns.Contains("pid"))
            {
                pid = row["pid"].ToString();
            }
            string Url = UrlPrefix + "/mdserver/service/queryDevice";

            System.Net.HttpWebResponse response = null;
            string resultstring = string.Empty;

            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("app_key", app_key);
                parameters.Add("agt_num", app_num);
                if (pid != "")
                {
                    parameters.Add("pid", pid);
                }


                response = HttpHelper.CreatePostHttpResponse(Url, parameters, encoding);

                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                resultstring = sr.ReadToEnd();
                stream.Close();
                sr.Close();
                IDictionary<string, object> results = (IDictionary<string, object>)HttpHelper.JsonToDictionary(resultstring);
                if (results["status"].ToString() == "success")
                {
                    resultstring = "{\"Result\":\"true\", \"data\": " + HttpHelper.SerializeToStr(results["data"]) + "}";
                }
                else
                {
                    resultstring = "{\"Result\":\"false\", \"data\": " + HttpHelper.SerializeToStr(results["msg"]) + "}";
                }
            }
            catch (Exception exp)
            {
                resultstring = "{\"Result\":\"false\", \"data\": \"" + exp.ToString() + "\"}";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return resultstring;
        }
        /// <summary>
        /// 安装门禁等级接口
        /// pId 门禁编号
        /// departId 小区编号
        /// install_Lock_Name 安装门禁名称
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string InstallLock(DataRow row)
        {

            if (!row.Table.Columns.Contains("pId") || string.IsNullOrEmpty(row["pId"].ToString()))
            {
                return JSONHelper.FromString(false, "门禁编号不能为空");
            }
            if (!row.Table.Columns.Contains("departId") || string.IsNullOrEmpty(row["departId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("install_Lock_Name") || string.IsNullOrEmpty(row["install_Lock_Name"].ToString()))
            {
                return JSONHelper.FromString(false, "安装门禁名称不能为空");
            }
            string pid = row["pId"].ToString();
            string departid = row["departId"].ToString();
            string install_lock_name = row["install_Lock_Name"].ToString();

            string Url = UrlPrefix + "/mdserver/service/installLock";

            System.Net.HttpWebResponse response = null;
            string resultstring = string.Empty;

            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("app_key", app_key);
                parameters.Add("agt_num", app_num);
                parameters.Add("pid", pid);
                parameters.Add("departid", departid);
                parameters.Add("install_lock_name", install_lock_name);

                response = HttpHelper.CreatePostHttpResponse(Url, parameters, encoding);

                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                resultstring = sr.ReadToEnd();

                stream.Close();
                sr.Close();
                IDictionary<string, object> results = (IDictionary<string, object>)HttpHelper.JsonToDictionary(resultstring);
                if (results["status"].ToString() == "success")
                {
                    resultstring = "{\"Result\":\"true\", \"data\": " + HttpHelper.SerializeToStr(results["data"]) + "}";
                }
                else
                {

                    resultstring = "{\"Result\":\"false\", \"data\": " + HttpHelper.SerializeToStr(results["msg"]) + "}";
                }
            }
            catch (Exception exp)
            {
                resultstring = "{\"Result\":\"false\", \"data\": \"" + exp.ToString() + "\"}";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return resultstring;
        }

        /// <summary>
        /// 申请用户钥匙凭证
        ///  pId 门禁编号
        ///  user_Id 用户手机号
        ///  validity 钥匙有效期 格式示例20150902，（4位）年份（2位）月份（2位）日期
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string ApplyKeys(DataRow row)
        {
            if (!row.Table.Columns.Contains("pId") || string.IsNullOrEmpty(row["pId"].ToString()))
            {
                return JSONHelper.FromString(false, "门禁编号不能为空");
            }
            if (!row.Table.Columns.Contains("user_Id") || string.IsNullOrEmpty(row["user_Id"].ToString()))
            {
                return JSONHelper.FromString(false, "用户手机号不能为空");
            }
            if (!row.Table.Columns.Contains("validity") || string.IsNullOrEmpty(row["validity"].ToString()))
            {
                return JSONHelper.FromString(false, "钥匙有效期不能为空");
            }
            string pid = row["pId"].ToString();
            string user_id = row["user_Id"].ToString();
            string validity = row["validity"].ToString();

            string Url = UrlPrefix + "/mdserver/service/qryKeys";

            System.Net.HttpWebResponse response = null;
            string resultstring = string.Empty;

            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("app_key", app_key);
                parameters.Add("agt_num", app_num);
                parameters.Add("pid", pid);
                parameters.Add("user_id", user_id);
                parameters.Add("validity", validity);

                response = HttpHelper.CreatePostHttpResponse(Url, parameters, encoding);

                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                resultstring = sr.ReadToEnd();

                stream.Close();
                sr.Close();
                IDictionary<string, object> results = (IDictionary<string, object>)HttpHelper.JsonToDictionary(resultstring);
                if (results["status"].ToString() == "success")
                {
                    resultstring = "{\"Result\":\"true\", \"data\": " + HttpHelper.SerializeToStr(results["msg"]) + "}";
                }
                else
                {

                    resultstring = "{\"Result\":\"false\", \"data\": " + HttpHelper.SerializeToStr(results["msg"]) + "}";
                }
            }
            catch (Exception exp)
            {
                resultstring = "{\"Result\":\"false\", \"data\": \"" + exp.ToString() + "\"}";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return resultstring;
        }

        /// <summary>
        /// 绑定微信用户
        /// openId
        /// device_Id 门禁设备ID
        /// ticket 绑定设备ticket
        /// access_Token
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string WX_BindDevice(DataRow row)
        {
            if (!row.Table.Columns.Contains("openId") || string.IsNullOrEmpty(row["openId"].ToString()))
            {
                return JSONHelper.FromString(false, "openId不能为空");
            }
            if (!row.Table.Columns.Contains("device_Id") || string.IsNullOrEmpty(row["device_Id"].ToString()))
            {
                return JSONHelper.FromString(false, "门禁设备ID不能为空");
            }
            if (!row.Table.Columns.Contains("ticket") || string.IsNullOrEmpty(row["ticket"].ToString()))
            {
                return JSONHelper.FromString(false, "绑定设备ticket不能为空");
            }
            if (!row.Table.Columns.Contains("access_Token") || string.IsNullOrEmpty(row["access_Token"].ToString()))
            {
                return JSONHelper.FromString(false, "公众号凭证不能为空");
            }
            string openid = row["openId"].ToString();
            string device_id = row["device_Id"].ToString();
            string ticket = row["ticket"].ToString();
            string access_token = row["access_Token"].ToString();

            string Url = UrlPrefix + "/mdserver/pservice/bindDevice.aspx";

            System.Net.HttpWebResponse response = null;
            string resultstring = string.Empty;

            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("app_key", app_key);
                parameters.Add("agt_num", app_num);
                parameters.Add("openid", openid);
                parameters.Add("device_id", device_id);
                parameters.Add("ticket", ticket);
                parameters.Add("access_token", access_token);


                response = HttpHelper.CreatePostHttpResponse(Url, parameters, encoding);

                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                resultstring = sr.ReadToEnd();
                stream.Close();
                sr.Close();
                IDictionary<string, object> results = (IDictionary<string, object>)HttpHelper.JsonToDictionary(resultstring);
                if (results["status"].ToString() == "success")
                {
                    resultstring = "{\"Result\":\"true\", \"data\": " + HttpHelper.SerializeToStr(results["msg"]) + "}";
                }
                else
                {
                    resultstring = "{\"Result\":\"false\", \"data\": " + HttpHelper.SerializeToStr(results["msg"]) + "}";
                }
            }
            catch (Exception exp)
            {
                resultstring = "{\"Result\":\"false\", \"data\": \"" + exp.ToString() + "\"}";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return resultstring;
        }

        /// <summary>
        /// 获取微信开门相关信息
        /// gId  微信公众号原始编码
        /// kType 钥匙类型： 0 是业主钥匙，1是访客钥匙
        /// kSid 钥匙id
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string WX_GetInfoKey(DataRow row)
        {
            if (!row.Table.Columns.Contains("gId") || string.IsNullOrEmpty(row["gId"].ToString()))
            {
                return JSONHelper.FromString(false, "公众号编码不能为空");
            }
            if (!row.Table.Columns.Contains("kType") || string.IsNullOrEmpty(row["kType"].ToString()))
            {
                return JSONHelper.FromString(false, "钥匙类型不能为空");
            }
            if (!row.Table.Columns.Contains("kSid") || string.IsNullOrEmpty(row["kSid"].ToString()))
            {
                return JSONHelper.FromString(false, "钥匙ID不能为空");
            }

            string gid = row["gId"].ToString();
            string ktype = row["kType"].ToString();
            string ksid = row["kSid"].ToString();

            string Url = UrlPrefix + "/mdserver/pservice/getInfo4KeyService.aspx";

            System.Net.HttpWebResponse response = null;
            string resultstring = string.Empty;

            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("app_key", app_key);
                parameters.Add("agt_num", app_num);
                parameters.Add("gid", gid);
                parameters.Add("ktype", ktype);
                parameters.Add("ksid", ksid);

                response = HttpHelper.CreatePostHttpResponse(Url, parameters, encoding);

                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                resultstring = sr.ReadToEnd();
                stream.Close();
                sr.Close();
                IDictionary<string, object> results = (IDictionary<string, object>)HttpHelper.JsonToDictionary(resultstring);
                if (results["status"].ToString() == "success")
                {
                    resultstring = "{\"Result\":\"true\", \"data\": " + HttpHelper.SerializeToStr(results["wx_key"]) + "}";
                }
                else
                {
                    resultstring = "{\"Result\":\"false\", \"data\": " + HttpHelper.SerializeToStr(results["msg"]) + "}";
                }
            }
            catch (Exception exp)
            {
                resultstring = "{\"Result\":\"false\", \"data\": \"" + exp.ToString() + "\"}";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return resultstring;
        }

        /// <summary>
        /// 生成访客钥匙
        /// pUId 用户手机号
        /// pId 门禁编号
        /// expireTime 有效期截止时间，格式为年月日时分秒（yyyyMMddhhmmss）
        /// usePlatform  【选填】是否使用妙兜提供的公众号.默认为不使用妙兜提供的公众号.1:需要使用妙兜提供的公众号
        /// gId 【选填】公众号原始id，如果合作方使用自己的微信公众号则必须填
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string WX_CreateMKey(DataRow row)
        {
            string useplathform = "";
            string gid = "";
            if (!row.Table.Columns.Contains("pUId") || string.IsNullOrEmpty(row["pUId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户手机号不能为空");
            }
            if (!row.Table.Columns.Contains("pId") || string.IsNullOrEmpty(row["pId"].ToString()))
            {
                return JSONHelper.FromString(false, "门禁编号不能为空");
            }
            if (!row.Table.Columns.Contains("expireTime") || string.IsNullOrEmpty(row["expireTime"].ToString()))
            {
                return JSONHelper.FromString(false, "截止日期不能为空");
            }
            if (!row.Table.Columns.Contains("usePlatform") || string.IsNullOrEmpty(row["usePlatform"].ToString()))
            {
                useplathform = row["usePlatform"].ToString();
            }
            if (!row.Table.Columns.Contains("gId") || string.IsNullOrEmpty(row["gId"].ToString()))
            {
                gid = row["gId"].ToString();
            }
            string puid = row["pUId"].ToString();
            string pid = row["pId"].ToString();
            string expiretime = row["expireTime"].ToString();

            string Url = UrlPrefix + "/mdserver/pservice/createMKeyService.aspx";

            System.Net.HttpWebResponse response = null;
            string resultstring = string.Empty;

            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("app_key", app_key);
                parameters.Add("agt_num", app_num);
                parameters.Add("puid", puid);
                parameters.Add("gid", gid);
                parameters.Add("expiretime", expiretime);
                parameters.Add("mtype", "1");
                if (useplathform != "")
                {
                    parameters.Add("useplathform", useplathform);
                }
                if (gid != "")
                {
                    parameters.Add("gid", gid);
                }

                response = HttpHelper.CreatePostHttpResponse(Url, parameters, encoding);

                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                resultstring = sr.ReadToEnd();
                stream.Close();
                sr.Close();
                IDictionary<string, object> results = (IDictionary<string, object>)HttpHelper.JsonToDictionary(resultstring);
                if (results["status"].ToString() == "success")
                {
                    resultstring = "{\"Result\":\"true\", \"data\": " + HttpHelper.SerializeToStr(results["msg"]) + "}";
                }
                else
                {
                    resultstring = "{\"Result\":\"false\", \"data\": " + HttpHelper.SerializeToStr(results["msg"]) + "}";
                }
            }
            catch (Exception exp)
            {
                resultstring = "{\"Result\":\"false\", \"data\": \"" + exp.ToString() + "\"}";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return resultstring;
        }

        /// <summary>
        /// 激活方可钥匙
        /// gId 微信公众号原始id
        /// openId 微信公众号用户openId
        /// kSid 原钥匙id
        /// Key_Secret 钥匙分享密钥
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string WX_ActiveMkey(DataRow row)
        {
            if (!row.Table.Columns.Contains("gId") || string.IsNullOrEmpty(row["gId"].ToString()))
            {
                return JSONHelper.FromString(false, "公众号原始Id不能为空");
            }
            if (!row.Table.Columns.Contains("openId") || string.IsNullOrEmpty(row["openId"].ToString()))
            {
                return JSONHelper.FromString(false, "OpenId不能为空");
            }
            if (!row.Table.Columns.Contains("kSid") || string.IsNullOrEmpty(row["kSid"].ToString()))
            {
                return JSONHelper.FromString(false, "钥匙ID不能为空");
            }
            if (!row.Table.Columns.Contains("key_Secret") || string.IsNullOrEmpty(row["key_Secret"].ToString()))
            {
                return JSONHelper.FromString(false, "分享密钥不能为空");
            }

            string gid = row["gid"].ToString();
            string ksid = row["ksid"].ToString();
            string openid = row["openid"].ToString();
            string key_secret = row["key_secret"].ToString();

            string Url = UrlPrefix + "/mdserver/pservice/activeMkeyService.aspx";

            System.Net.HttpWebResponse response = null;
            string resultstring = string.Empty;

            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("app_key", app_key);
                parameters.Add("agt_num", app_num);
                parameters.Add("gid", gid);
                parameters.Add("ksid", ksid);
                parameters.Add("openid", openid);
                parameters.Add("key_secret", key_secret);
                parameters.Add("mtype", "1");

                response = HttpHelper.CreatePostHttpResponse(Url, parameters, encoding);

                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                resultstring = sr.ReadToEnd();
                stream.Close();
                sr.Close();
                IDictionary<string, object> results = (IDictionary<string, object>)HttpHelper.JsonToDictionary(resultstring);
                if (results["status"].ToString() == "success")
                {
                    resultstring = "{\"Result\":\"true\", \"data\": " + HttpHelper.SerializeToStr(results["msg"]) + "}";
                }
                else
                {
                    resultstring = "{\"Result\":\"false\", \"data\": " + HttpHelper.SerializeToStr(results["msg"]) + "}";
                }
            }
            catch (Exception exp)
            {
                resultstring = "{\"Result\":\"false\", \"data\": \"" + exp.ToString() + "\"}";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return resultstring;
        }
        /// <summary>
        /// 分配设备到微信公众号
        /// gId 需要分配的公众号原始id
        /// pId 门禁编号
        /// access_Token   公众号调用各接口时的凭证
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string WX_SyncDeviceService(DataRow row)
        {
            if (!row.Table.Columns.Contains("gId") || string.IsNullOrEmpty(row["gId"].ToString()))
            {
                return JSONHelper.FromString(false, "公众号原始Id不能为空");
            }
            if (!row.Table.Columns.Contains("pId") || string.IsNullOrEmpty(row["pId"].ToString()))
            {
                return JSONHelper.FromString(false, "门禁编号不能为空");
            }
            if (!row.Table.Columns.Contains("access_Token") || string.IsNullOrEmpty(row["access_Token"].ToString()))
            {
                return JSONHelper.FromString(false, "Access_Token不能为空");
            }

            string gid = row["gid"].ToString();
            string pid = row["pid"].ToString();
            string access_token = row["access_token"].ToString();

            string Url = UrlPrefix + "/mdserver/pservice/syncDeviceService.aspx";

            System.Net.HttpWebResponse response = null;
            string resultstring = string.Empty;

            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("app_key", app_key);
                parameters.Add("agt_num", app_num);
                parameters.Add("gid", gid);
                parameters.Add("pid", pid);
                parameters.Add("access_token", access_token);

                response = HttpHelper.CreatePostHttpResponse(Url, parameters, encoding);

                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                resultstring = sr.ReadToEnd();
                stream.Close();
                sr.Close();
                IDictionary<string, object> results = (IDictionary<string, object>)HttpHelper.JsonToDictionary(resultstring);
                if (results["status"].ToString() == "success")
                {
                    //请求成功,返回公众号设备ID
                    resultstring = "{\"Result\":\"true\", \"data\": " + HttpHelper.SerializeToStr(results["device_id"]) + "}";
                }
                else
                {
                    resultstring = "{\"Result\":\"false\", \"data\": " + HttpHelper.SerializeToStr(results["msg"]) + "}";
                }
            }
            catch (Exception exp)
            {
                resultstring = "{\"Result\":\"false\", \"data\": \"" + exp.ToString() + "\"}";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return resultstring;
        }

        /// <summary>
        /// 获取微信开门秘钥
        /// gId  微信公众号原始编号
        /// openId  微信公众号用户openid
        /// auId  用户手机号
        /// kType 钥匙类型：0是业主钥匙，1是访客钥匙
        /// kSid  原钥匙id
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string WX_OpenDoor(DataRow row)
        {
            if (!row.Table.Columns.Contains("gId") || string.IsNullOrEmpty(row["gId"].ToString()))
            {
                return JSONHelper.FromString(false, "公众号原始Id不能为空");
            }
            if (!row.Table.Columns.Contains("openId") || string.IsNullOrEmpty(row["openId"].ToString()))
            {
                return JSONHelper.FromString(false, "OpenId不能为空");
            }
            if (!row.Table.Columns.Contains("auId") || string.IsNullOrEmpty(row["auId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户手机号不能为空");
            }
            if (!row.Table.Columns.Contains("kType") || string.IsNullOrEmpty(row["kType"].ToString()))
            {
                return JSONHelper.FromString(false, "钥匙类型不能为空");
            }
            if (!row.Table.Columns.Contains("kSid") || string.IsNullOrEmpty(row["kSid"].ToString()))
            {
                return JSONHelper.FromString(false, "钥匙id不能为空");
            }
            string gid = row["gid"].ToString();
            string openid = row["openId"].ToString();
            string auid = row["auId"].ToString();
            string ktype = row["kType"].ToString();
            string ksid = row["kSid"].ToString();

            string Url = UrlPrefix + "/mdserver/pservice/syncDeviceService.aspx";

            System.Net.HttpWebResponse response = null;
            string resultstring = string.Empty;

            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("app_key", app_key);
                parameters.Add("agt_num", app_num);
                parameters.Add("gid", gid);
                parameters.Add("openid", openid);
                parameters.Add("ktype", ktype);
                parameters.Add("ksid", ksid);

                response = HttpHelper.CreatePostHttpResponse(Url, parameters, encoding);

                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                resultstring = sr.ReadToEnd();
                stream.Close();
                sr.Close();
                IDictionary<string, object> results = (IDictionary<string, object>)HttpHelper.JsonToDictionary(resultstring);
                if (results["status"].ToString() == "success")
                {
                    resultstring = "{\"Result\":\"true\", \"data\": " + HttpHelper.SerializeToStr(results["msg"]) + "}";
                }
                else
                {
                    resultstring = "{\"Result\":\"false\", \"data\": " + HttpHelper.SerializeToStr(results["msg"]) + "}";
                }
            }
            catch (Exception exp)
            {
                resultstring = "{\"Result\":\"false\", \"data\": \"" + exp.ToString() + "\"}";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return resultstring;

        }
    }
}
