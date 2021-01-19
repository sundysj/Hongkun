using Common;
using Dapper;
using DotNet4.Utilities;
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
using HttpHelper = DotNet4.Utilities.HttpHelper;
using HttpItem = DotNet4.Utilities.HttpItem;
using HttpResult = DotNet4.Utilities.HttpResult;
using ResultType = DotNet4.Utilities.ResultType;

namespace Business
{
    public class LiLingEntrance : PubInfo
    {
        public LiLingEntrance()
        {
            Token = "20190528LingLingEntrance";
        }

        public override void Operate(ref Transfer Trans)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            try
            {
                switch (Trans.Command)
                {
                    case "GetDoorList":
                        Trans.Result = GetDoorList(Row);      // 获取有权限开的门
                        break;
                    default:
                        Trans.Result = new ApiResult(false, "接口不存在").toJson();
                        break;
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                Trans.Result = new ApiResult(false, ex.Message + Environment.CommandLine + ex.StackTrace).toJson();
            }
        }

        /// <summary>
        /// 获取有权限的门禁列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetDoorList(DataRow row)
        {
            #region 获取基本传参
            string CommID = string.Empty;
            if (row.Table.Columns.Contains("CommID"))
            {
                CommID = row["CommID"].ToString();
            }
            string RoomID = string.Empty;
            if (row.Table.Columns.Contains("RoomID"))
            {
                RoomID = row["RoomID"].ToString();
            }
            #endregion
            #region 获取小区配置和房间配置
            Tb_Community tb_Community = GetCommunityByCommID(CommID);
            if (null == tb_Community)
            {
                return new ApiResult(false, "未配置该小区").toJson();
            }
            string erpConnStr = GetConnectionStr(tb_Community);
            LiLingEntranceConfig lilingEntranceConfig = null;
            dynamic RoomConfig = null;
            using (IDbConnection conn = new SqlConnection(erpConnStr))
            {
                dynamic Config = conn.QueryFirstOrDefault("SELECT * FROM Tb_LiLing_Entrance_Config WHERE CommID = @CommID", new { CommID });
                if (null == Config)
                {
                    return new ApiResult(false, "该小区未配置门禁信息").toJson();
                }
                try
                {
                    lilingEntranceConfig = JsonConvert.DeserializeObject<LiLingEntranceConfig>(Convert.ToString(Config.Config));
                }
                catch (Exception)
                {
                    return new ApiResult(false, "门禁配置信息有误").toJson();
                }
                if (null == lilingEntranceConfig)
                {
                    return new ApiResult(false, "门禁配置信息为空").toJson();
                }
                RoomConfig = conn.QueryFirstOrDefault("SELECT CallNum,BlueNum,QRCodeNum FROM Tb_LiLing_Entrance_Room WHERE CommID = @CommID AND RoomID = @RoomID", new { CommID, RoomID });
                if(null == RoomConfig)
                {
                    return new ApiResult(false, "房间权限配置信息为空").toJson();
                }
            }
            string SERVER_URL = lilingEntranceConfig.ServerUrl;
            string CLIENT_USERNAME = lilingEntranceConfig.ClientUserName;
            string CLIENT_OSTYPE = lilingEntranceConfig.ClientOSType;
            string CLIENT_ID = lilingEntranceConfig.ClientID;
            string CLIENT_SECRET = lilingEntranceConfig.ClientSecret;
            #endregion

            #region 获取AccessToken
            string access_token = string.Empty;
            string sessionId = string.Empty;
            string Cookie = string.Empty;
            try
            {
                string param = $"client_id={CLIENT_ID}&client_secret={CLIENT_SECRET}&grant_type=client_credentials&userName={CLIENT_USERNAME}&osType={CLIENT_OSTYPE}";
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = SERVER_URL + "/rest/accesstoken/getAccessToken",//URL     必需项  
                    Method = "POST",//URL     可选项 默认为Get  
                    Timeout = 3000,//连接超时时间     可选项默认为100000  
                    ReadWriteTimeout = 3000,//写入Post数据超时时间     可选项默认为30000  
                    IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                    ContentType = "application/x-www-form-urlencoded",
                    PostEncoding = Encoding.UTF8,
                    Cookie = Cookie,
                    Postdata = param,//Post数据     可选项GET时不需要写  
                    ResultType = ResultType.String,//返回数据类型，是Byte还是String  
                    ProtocolVersion = HttpVersion.Version11,//获取或设置用于请求的 HTTP 版本。默认为 System.Net.HttpVersion.Version11  
                };
                HttpResult result = http.GetHtml(item);
                JObject jObj = JsonConvert.DeserializeObject<JObject>(result.Html);
                if (!"1".Equals(jObj["code"].ToString()) || !"success".Equals(jObj["state"].ToString()))
                {
                    GetLog().Debug("LiLingEntrance:获取AccessToken失败:" + JsonConvert.SerializeObject(jObj));
                    return new ApiResult(false, "获取AccessToken失败,请重试").toJson();
                }
                access_token = (string)jObj["access_token"];
                sessionId = (string)jObj["sessionId"];
                Cookie = $"JSESSIONID={sessionId}";
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new ApiResult(false, "获取AccessToken异常,请重试").toJson();
            }
            #endregion

            #region 获取本项目蓝牙门禁信息
            string neigh_no = lilingEntranceConfig.NeighStructure;
            List<string> DoorList = new List<string>();
            try
            {
                string param = $"access_token={access_token}&neigh_no={neigh_no}";
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = SERVER_URL + "/rest/api/third/getDoorList",//URL     必需项  
                    Method = "POST",//URL     可选项 默认为Get  
                    Timeout = 3000,//连接超时时间     可选项默认为100000  
                    ReadWriteTimeout = 3000,//写入Post数据超时时间     可选项默认为30000  
                    IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                    ContentType = "application/x-www-form-urlencoded",
                    PostEncoding = Encoding.UTF8,
                    Cookie = Cookie,
                    Postdata = param,//Post数据     可选项GET时不需要写  
                    ResultType = ResultType.String,//返回数据类型，是Byte还是String  
                    ProtocolVersion = HttpVersion.Version11,//获取或设置用于请求的 HTTP 版本。默认为 System.Net.HttpVersion.Version11  
                };
                HttpResult result = http.GetHtml(item);
                JObject jObj = JsonConvert.DeserializeObject<JObject>(result.Html);
                if (!"1".Equals(jObj["code"].ToString()) || !"success".Equals(jObj["state"].ToString()))
                {
                    GetLog().Debug("LiLingEntrance:获取小区门禁列表失败:" + JsonConvert.SerializeObject(jObj));
                    return new ApiResult(false, "获取小区门禁列表失败,请重试").toJson();
                }
                JArray door_list = (JArray)jObj["door_list"];
                if(null != door_list)
                {
                    foreach (JObject door in door_list)
                    {
                        DoorList.Add(door["door_name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new ApiResult(false, "获取小区门禁列表异常,请重试").toJson();
            }
            #endregion

            return new ApiResult(true, new { RoomConfig = RoomConfig, DoorList = DoorList }).toJson();
        }
    }
}
