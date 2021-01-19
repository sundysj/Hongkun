using Dapper;
using log4net;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class RecordClientInfo : PubInfo
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RecordClientInfo));

        public RecordClientInfo() 
        {
            base.Token = "20200110Client";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            try
            {
                Trans.Result = "false:";

                log.Error("记录客户端信息接收到内容:" + Trans.Attribute);

                DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];

                switch (Trans.Command)
                {
                    //生成订单
                    case "Record":
                        Trans.Result = RecordInfo(Row);
                        break;
                    case "RecordOperationLog":
                        Trans.Result = RecordOperationLog(Row, Trans.Attribute, Trans.ClassLog, Trans.CommandLog);
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                log.Error("记录客户端信息错误:" + ex);
            }
        }

        public string RecordOperationLog(DataRow row,string Attribute,string ClassLog, string CommandLog)
        {
            string CustID = "";
            if (row.Table.Columns.Contains("CustID") && !string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                CustID = row["CustID"].ToString();
            }

            #region 插入操作日志表
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ID", Guid.NewGuid());
                parameters.Add("CreateDate", DateTime.Now);
                parameters.Add("ClassName", ClassLog);
                parameters.Add("CommandName", CommandLog);
                parameters.Add("Remark", Attribute);
                parameters.Add("CustID", CustID);
                if (conn.Execute(@"INSERT INTO Tb_Operation_Log(ID, CreateDate, ClassName, CommandName, Remark,CustID) 
                                    VALUES(@ID, @CreateDate, @ClassName, @CommandName, @Remark,@CustID)", parameters) <= 0)
                {
                    return new ApiResult(false, "客户端信息保存失败,请重试").toJson();
                }
            }
            #endregion

            return new ApiResult(true, "保存成功").toJson();
        }

        private string RecordInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("RecordData") || string.IsNullOrEmpty(row["RecordData"].ToString()))
            {
                return new ApiResult(false, "缺少参数RecordData").toJson();
            }
            string recordData = row["RecordData"].ToString();

            string[] dataArray = recordData.Split(';');

            //string strMobile = "";
            string strMobileType = "";
            string strOsVersion = "";
            string strBlueInfo = "";
            string strAccount = "";
            string strTimestamp = "";
            string strAppVersion = "";
            string strRoomInfo = "";
            string strPlatform = "";

            for(int i = 0; i < dataArray.Length; i++)
            {
                string[] strInfo = dataArray[i].Split(':');
                switch (strInfo[0])
                {
                    case "mobile_model":
                        strMobileType = strInfo[1];
                        break;
                    case "OS_version":
                        strOsVersion = strInfo[1];
                        break;
                    case "bt_info":
                        strBlueInfo = strInfo[1];
                        break;
                    case "account":
                        strAccount = strInfo[1];
                        break;
                    case "timestamp":
                        strTimestamp = strInfo[1];
                        break;
                    case "app_version":
                        strAppVersion = strInfo[1];
                        break;
                    case "room_info":
                        strRoomInfo = strInfo[1];
                        break;
                    case "platform":
                        strPlatform = strInfo[1];
                        break;
                }
            }

            #region 插入订单表
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ID", Guid.NewGuid());
                parameters.Add("CreateDate", DateTime.Now);
                parameters.Add("MobileType", strMobileType);
                parameters.Add("OsVersion", strOsVersion);
                parameters.Add("BlueInfo", strBlueInfo);
                parameters.Add("Account", strAccount);
                parameters.Add("Timestamp", strTimestamp);
                parameters.Add("AppVersion", strAppVersion);
                parameters.Add("RoomInfo", strRoomInfo);
                parameters.Add("Platform", strPlatform);
                if (conn.Execute(@"INSERT INTO Tb_Client_Info(ID, CreateDate, MobileType, OsVersion, BlueInfo, Account, Timestamp,AppVersion,RoomInfo,Platform) 
                                    VALUES(@ID, @CreateDate, @MobileType, @OsVersion, @BlueInfo, @Account, @Timestamp,@AppVersion,@RoomInfo,@Platform)", parameters) <= 0)
                {
                    return new ApiResult(false, "客户端信息保存失败,请重试").toJson();
                }
            }
            #endregion

            return new ApiResult(true, "保存成功").toJson();
        }
    }
}
