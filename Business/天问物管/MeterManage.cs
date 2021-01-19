using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    /// <summary>
    /// 抄表
    /// </summary>
    public class MeterManage : PubInfo
    {
        public MeterManage()
        {
            base.Token = "20170807MeterManage";
        }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            //验证登录
            if (!new Login().isLogin(ref Trans))
                return;

            switch (Trans.Command)
            {
                case "DownloadBuildUnitCustMeterHistory":       // 房间表抄表离线下载
                    Trans.Result = DownloadBuildUnitCustMeterHistory(Row);
                    break;
                case "GetCustMeterHistory":
                    Trans.Result = GetCustMeterHistory(Row);    // 房间表抄表历史
                    break;
                case "GetCustMeterHistoryCustomer":
                    Trans.Result = GetCustMeterHistoryCustomer(Row); // 房间表历史客户
                    break;
                case "CustMeterInsUpdate":
                    Trans.Result = CustMeterInsUpdate(Row);     // 新增抄表数据
                    break;
                case "PublicMeterList":
                    Trans.Result = PublicMeterList(Row);        // 获取公区表列表
                    break;
                case "GetPublicMeterHistory":
                    Trans.Result = GetPublicMeterHistory(Row);  // 公区表抄表历史
                    break;
                case "PublicMeterInsUpdate":
                    Trans.Result = PublicMeterInsUpdate(Row);   // 公区表抄表
                    break;
                case "GetTableBoxMeterList":
                    Trans.Result = GetTableBoxMeterList(Row);       //华南城,表箱抄表
                    break;
                case "SaveTableBoxMeter":
                    Trans.Result = SaveTableBoxMeter(Row);      //华南城,保存表箱抄表(单表)
                    break;
                case "SwitchRoomMeter":
                    Trans.Result = SwitchRoomMeter(Row);         // 抄表切换房屋
                    break;
                case "SwitchPublicMeter":
                    Trans.Result = SwitchPublicMeter(Row);       // 抄表切换公区表
                    break;
                case "GetBuildRoomUnreadMeters":
                    Trans.Result = GetBuildRoomUnreadMeters(Row).Result;       // 获取楼栋、单元、房屋、未抄表信息
                    break;
                case "GetMultipleBuildRoomUnreadMeters":
                    Trans.Result = GetMultipleBuildRoomUnreadMeters(Row).Result;       // 获取楼栋、单元、房屋、未抄表信息
                    break;

                default:
                    break;
            }
        }

        private string DownloadBuildUnitCustMeterHistory(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }
            if (!row.Table.Columns.Contains("BuildSNum") || string.IsNullOrEmpty(row["BuildSNum"].ToString()))
            {
                return JSONHelper.FromString(false, "楼栋编号不能为空");
            }
            if (!row.Table.Columns.Contains("UnitSNum") || string.IsNullOrEmpty(row["UnitSNum"].ToString()))
            {
                return JSONHelper.FromString(false, "单元编号不能为空");
            }

            string CommID = row["CommID"].ToString();
            string BuildSNum = row["BuildSNum"].ToString();
            string UnitSNum = row["UnitSNum"].ToString();


            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 房间表
                string sql = @"SELECT DISTINCT a.MeterID,a.MeterSign,a.MeterName,a.MeterType,a.Ratio,d.CustID AS OwnerCustID, e.CustName AS OwnerCustName,
                                a.RoomID,isnull(c.RoomName,c.RoomSign) AS RoomSign,c.BuildSNum,c.UnitSNum,c.FloorSNum,
                                isnull(b.RestID,0) AS RestID,b.ListDate,isnull(b.StartDegree,0) AS StartDegree,isnull(b.EndDegree,0) AS EndDegree,
                                isnull(b.Dosage,0) AS Dosage,isnull(b.OldDosage,0) AS OldDosage,isnull(b.ExtraDosage,0) AS ExtraDosage,
                                isnull(b.TotalDosage,0) AS TotalDosage,
                                isnull(b.Price,0) AS Price,isnull(b.Amount,0) AS Amount,isnull(b.MeterHint,0) AS MeterHint,b.IsAudit,a.IsReverse
                            FROM Tb_HSPR_CustomerMeter a LEFT JOIN view_HSPR_CustomerMeterResult_Filter b ON a.MeterID=b.MeterID
                            LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID
                            LEFT JOIN Tb_HSPR_CustomerLive d ON a.RoomID=d.RoomID AND isnull(d.IsDelLive,0)=0 AND IsActive=1
                            LEFT JOIN Tb_HSPR_Customer e ON e.CustID=d.CustID
                            WHERE not exists(SELECT * FROM view_HSPR_CustomerMeterResult_Filter x WHERE x.MeterID=a.MeterID AND b.ListDate<x.ListDate) 
                                AND isnull(a.IsDelete,0)=0 AND a.CommID=@CommID AND c.BuildSNum=@BuildSNum AND c.UnitSNum=@UnitSNum
                                ORDER BY a.RoomID";

                // 中集
                if (Global_Var.LoginCorpID == "1953")
                {
                    sql = @"SELECT DISTINCT a.MeterID,a.MeterSign,a.MeterName,a.MeterType,a.Ratio,d.CustID AS OwnerCustID, e.CustName AS OwnerCustName,
                                a.RoomID,isnull(c.RoomSign,c.RoomName) AS RoomSign,c.BuildSNum,c.UnitSNum,c.FloorSNum,
                                isnull(b.RestID,0) AS RestID,b.ListDate,isnull(b.StartDegree,0) AS StartDegree,isnull(b.EndDegree,0) AS EndDegree,
                                isnull(b.Dosage,0) AS Dosage,isnull(b.OldDosage,0) AS OldDosage,isnull(b.ExtraDosage,0) AS ExtraDosage,
                                isnull(b.TotalDosage,0) AS TotalDosage,
                                isnull(b.Price,0) AS Price,isnull(b.Amount,0) AS Amount,isnull(b.MeterHint,0) AS MeterHint,b.IsAudit,a.IsReverse
                            FROM Tb_HSPR_CustomerMeter a LEFT JOIN view_HSPR_CustomerMeterResult_Filter b ON a.MeterID=b.MeterID
                            LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID
                            LEFT JOIN Tb_HSPR_CustomerLive d ON a.RoomID=d.RoomID AND isnull(d.IsDelLive,0)=0 AND IsActive=1
                            LEFT JOIN Tb_HSPR_Customer e ON e.CustID=d.CustID
                            WHERE not exists(SELECT * FROM view_HSPR_CustomerMeterResult_Filter x WHERE x.MeterID=a.MeterID AND b.ListDate<x.ListDate) 
                                AND isnull(a.IsDelete,0)=0 AND a.CommID=@CommID AND c.BuildSNum=@BuildSNum AND c.UnitSNum=@UnitSNum
                                ORDER BY a.RoomID";
                }

                return new ApiResult(true, conn.Query(sql, new
                {
                    CommID = CommID,
                    BuildSNum = BuildSNum,
                    UnitSNum = UnitSNum
                })).toJson();
            }
        }

        private string SaveTableBoxMeter(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少参数CommID");
            }
            if (!row.Table.Columns.Contains("MeterID") || string.IsNullOrEmpty(row["MeterID"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少参数MeterID");
            }
            if (!row.Table.Columns.Contains("StartDegree") || string.IsNullOrEmpty(row["StartDegree"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少参数StartDegree");
            }
            if (!row.Table.Columns.Contains("EndDegree") || string.IsNullOrEmpty(row["EndDegree"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少参数EndDegree");
            }
            string CommID = row["CommID"].ToString();
            long MeterID = Global_Fun.StrToLong(row["MeterID"].ToString());
            float StartDegree;
            if (!float.TryParse(row["StartDegree"].ToString(), out StartDegree))
            {
                return JSONHelper.FromString(false, "请输入正确的起止数");
            }
            float EndDegree;
            if (!float.TryParse(row["EndDegree"].ToString(), out EndDegree))
            {
                return JSONHelper.FromString(false, "请输入正确的起止数");
            }
            int result = 0;
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("MeterID", MeterID);
                parameters.Add("ListDate", DateTime.Now);
                parameters.Add("MeterStyle", 0);
                parameters.Add("OwnerCustID", 0);
                parameters.Add("StartDegree", StartDegree);
                parameters.Add("EndDegree", EndDegree);
                parameters.Add("NewStartDegree", 0);
                parameters.Add("NewEndDegree", 0);
                parameters.Add("ExtraDosage", 0);
                parameters.Add("UserCode", Global_Var.LoginUserCode);
                result = conn.Execute("Proc_HSPR_CustomerMeterResult_InsUpdate", parameters, null, null, CommandType.StoredProcedure);
            }
            if (result == 0)
            {
                return new ApiResult(false, "抄表失败,请重试").toJson();
            }
            return new ApiResult(true, "抄表成功").toJson();
        }

        private string GetTableBoxMeterList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少参数CommID");
            }
            if (!row.Table.Columns.Contains("MeterBoxID") || string.IsNullOrEmpty(row["MeterBoxID"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少参数MeterBoxID");
            }
            string CommID = row["CommID"].ToString();
            string MeterBoxID = row["MeterBoxID"].ToString();

            DataTable dt = null;
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dt = conn.ExecuteReader("SELECT * FROM view_HSPR_CustomerMeter_Filter WHERE IsDelete = 0 AND CommID = @CommID AND MeterBoxID = @MeterBoxID", new { CommID = CommID, MeterBoxID = MeterBoxID }, null, null, CommandType.Text).ToDataSet().Tables[0];
            }
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (null != dt && dt.Rows.Count > 0)
            {
                Dictionary<string, object> dic;
                foreach (DataRow item in dt.Rows)
                {
                    dic = new Dictionary<string, object>
                    {
                        { "MeterID", item["MeterID"] }                              //表ID
                        ,{ "RoomSign", item["RoomSign"] }                           //房屋ID
                        ,{ "MeterSign", item["MeterSign"] }                         //表记编号
                        ,{ "MeterName", item["MeterName"] }                         //表记名称
                        ,{ "InitAmount", item["InitAmount"] }                       //初始读数
                        ,{ "MeterType", item["MeterType"] }                         //表记类型ID
                        ,{ "MeterBoxID", item["MeterBoxID"] }                       //表箱ID
                        ,{ "BoxNumber", item["BoxNumber"] }                         //转表基数
                        ,{ "InstallLocation", item["InstallLocation"] }             //安装位置
                        ,{ "MeterTypeName", item["MeterTypeName"] }                 //表记类型名称
                        ,{ "MeterBoxName", item["MeterBoxName"] }                   //表箱名称
                        ,{ "LastEndDegree", item["LastEndDegree"] }                 //表箱名称
                    };
                    list.Add(dic);
                }
            }
            return new ApiResult(true, list).toJson();


        }

        /// <summary>
        /// 房间表抄表历史
        /// </summary>
        public string GetCustMeterHistory(DataRow row)
        {
            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }

            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }

            string corpId = row["CorpID"].ToString();
            string roomId = row["RoomID"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = @"SELECT DISTINCT a.MeterID,a.MeterSign,a.MeterName,a.MeterType,a.Ratio,d.CustID AS OwnerCustID, e.CustName AS OwnerCustName,
                                a.RoomID,isnull(c.RoomName,c.RoomSign) AS RoomSign,
                                isnull(b.RestID,0) AS RestID,b.ListDate,
                                CASE a.IsReverse WHEN 0 THEN isnull(b.StartDegree,0.0) ELSE isnull(b.StartDegree,a.InitAmount) END AS StartDegree,
								CASE a.IsReverse WHEN 0 THEN isnull(b.EndDegree,a.InitAmount) ELSE isnull(b.EndDegree,0.0) END AS EndDegree, 
                                isnull(b.Dosage,0) AS Dosage,isnull(b.OldDosage,0) AS OldDosage,isnull(b.ExtraDosage,0) AS ExtraDosage,
                                isnull(b.TotalDosage,0) AS TotalDosage,
                                isnull(f.StanAmount,0) AS Price,isnull(b.Amount,0) AS Amount,isnull(b.MeterHint,0) AS MeterHint,b.IsAudit,a.IsReverse
                            FROM Tb_HSPR_CustomerMeter a 
                            LEFT JOIN view_HSPR_CustomerMeterResult_Filter b ON a.MeterID=b.MeterID
                            LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID
                            LEFT JOIN Tb_HSPR_CustomerLive d ON a.RoomID=d.RoomID AND isnull(d.IsDelLive,0)=0 AND IsActive=1
                            LEFT JOIN Tb_HSPR_Customer e ON e.CustID=d.CustID
							LEFT JOIN Tb_HSPR_CostStandard f ON a.StanID=f.StanID
                            WHERE not exists(SELECT * from view_HSPR_CustomerMeterResult_Filter c where c.MeterID=a.MeterID
                                AND b.ListDate<c.ListDate) AND isnull(a.IsDelete,0)= 0 AND a.RoomID=@RoomID";

                // 中集
                if (corpId == "1953")
                {
                    sql = @"SELECT DISTINCT a.MeterID,a.MeterSign,a.MeterName,a.MeterType,a.Ratio,d.CustID AS OwnerCustID, e.CustName AS OwnerCustName,
                                a.RoomID,isnull(c.RoomSign,c.RoomName) AS RoomSign,
                                isnull(b.RestID,0) AS RestID,b.ListDate,
                                CASE a.IsReverse WHEN 0 THEN isnull(b.StartDegree,0.0) ELSE isnull(b.StartDegree,a.InitAmount) END AS StartDegree,
								CASE a.IsReverse WHEN 0 THEN isnull(b.EndDegree,a.InitAmount) ELSE isnull(b.EndDegree,0.0) END AS EndDegree, 
                                isnull(b.Dosage,0) AS Dosage,isnull(b.OldDosage,0) AS OldDosage,isnull(b.ExtraDosage,0) AS ExtraDosage,
                                isnull(b.TotalDosage,0) AS TotalDosage,
                                isnull(f.StanAmount,0) AS Price,isnull(b.Amount,0) AS Amount,isnull(b.MeterHint,0) AS MeterHint,b.IsAudit,a.IsReverse
                            FROM Tb_HSPR_CustomerMeter a 
                            LEFT JOIN view_HSPR_CustomerMeterResult_Filter b ON a.MeterID=b.MeterID
                            LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID
                            LEFT JOIN Tb_HSPR_CustomerLive d ON a.RoomID=d.RoomID AND isnull(d.IsDelLive,0)=0 AND IsActive=1
                            LEFT JOIN Tb_HSPR_Customer e ON e.CustID=d.CustID
							LEFT JOIN Tb_HSPR_CostStandard f ON a.StanID=f.StanID
                            WHERE not exists(SELECT * FROM view_HSPR_CustomerMeterResult_Filter c WHERE c.MeterID=a.MeterID
                                AND b.ListDate<c.ListDate) AND isnull(a.IsDelete,0)= 0 AND a.RoomID=@RoomID";
                }

                IEnumerable<dynamic> resultSet = conn.Query(sql, new { RoomID = roomId });

                return new ApiResult(true, resultSet).toJson();
            }
        }

        /// <summary>
        /// 房间表历史客户
        /// </summary>
        public string GetCustMeterHistoryCustomer(DataRow row)
        {
            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }

            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }

            string corpId = row["CorpID"].ToString();
            string roomId = row["RoomID"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                //中集需求5246 1=业主 2=租户 3=临时客户
                string sql = @"SELECT DISTINCT x.CustID,y.CustName,isnull(y.MobilePhone,y.LinkmanTel) AS MobilePhone,x.LiveType
                                FROM Tb_HSPR_CustomerLive x LEFT JOIN Tb_HSPR_Customer y ON x.CustID=y.CustID
                                WHERE x.RoomID=@RoomID";

                IEnumerable<dynamic> resultSet = conn.Query(sql, new { RoomID = roomId });

                return new ApiResult(true, resultSet).toJson();
            }
        }

        /// <summary>
        /// 新增抄表数据
        /// </summary>
        public string CustMeterInsUpdate(DataRow row)
        {
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }

            return this.MeterInsUpdate(row, "Proc_HSPR_CustomerMeterResult_InsUpdate");
        }

        /// <summary>
        /// 获取公区表列表
        /// </summary>
        public string PublicMeterList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }

            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }

            string corpId = row["CorpID"].ToString();
            string commId = row["CommID"].ToString();

            string sql = $@"SELECT MeterID,MeterSign,MeterName,MeterType,Ratio,IsReverse,Location  
                            FROM Tb_HSPR_PublicMeter WHERE isnull(IsDelete, 0)=0 AND CommID={commId} ORDER BY MeterSNum";

            using (var conn = new SqlConnection(Global_Fun.BurstConnectionString(AppGlobal.StrToInt(commId), Global_Fun.BURST_TYPE_CHARGE)))
            {
                return JSONHelper.FromString(conn.ExecuteReader(sql).ToDataSet().Tables[0]);
            }
        }

        /// <summary>
        /// 公区表抄表历史
        /// </summary>
        public string GetPublicMeterHistory(DataRow row)
        {
            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }

            if (!row.Table.Columns.Contains("MeterID") || string.IsNullOrEmpty(row["MeterID"].ToString()))
            {
                return JSONHelper.FromString(false, "表计编号不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "CommID不能为空");
            }

            var corpId = row["CorpID"].ToString();
            var meterId = row["MeterID"].ToString();
            var commId = row["CommID"].ToString();

            var sql = @"SELECT DISTINCT a.MeterID,a.MeterSign,a.MeterName,a.MeterType,a.Ratio,
                            isnull(a.Location,'') AS Location,
                            isnull(b.RestID,'') AS RestID,
                            isnull(b.ListDate,'') AS ListDate,
                            CASE a.IsReverse WHEN 0 THEN isnull(b.StartDegree,0.0) ELSE isnull(b.StartDegree,a.InitAmount) END AS StartDegree,
							CASE a.IsReverse WHEN 0 THEN isnull(b.EndDegree,a.InitAmount) ELSE isnull(b.EndDegree,0.0) END AS EndDegree, 
                            isnull(b.Dosage,0) AS Dosage,
                            isnull(b.OldDosage,0) AS OldDosage,
                            isnull(b.ExtraDosage,0) AS ExtraDosage,
                            isnull(c.StanAmount,0) AS Price,
                            isnull(b.Amount,0) AS Amount,
                            isnull(b.MeterHint,0) AS MeterHint,
                            isnull(b.IsAudit,0) AS IsAudit,
                            isnull(b.IsReverse,0) AS IsReverse
                        FROM Tb_HSPR_PublicMeter a 
                        LEFT OUTER JOIN view_HSPR_PublicMeterResult_Filter b ON a.MeterID=b.MeterID
						LEFT JOIN Tb_HSPR_CostStandard c ON a.StanID=c.StanID
                        WHERE not exists(select * from view_HSPR_PublicMeterResult_Filter c 
                                            where c.MeterID = a.MeterID AND b.ListDate < c.ListDate) 
                        AND isnull(a.IsDelete, 0)= 0 AND a.MeterID=" + meterId;

            using (var conn = new SqlConnection(Global_Fun.BurstConnectionString(AppGlobal.StrToInt(commId), Global_Fun.BURST_TYPE_CHARGE)))
            {
                return JSONHelper.FromString(conn.ExecuteReader(sql).ToDataSet().Tables[0]);
            }
        }

        /// <summary>
        /// 公区表抄表
        /// </summary>
        public string PublicMeterInsUpdate(DataRow row)
        {
            return this.MeterInsUpdate(row, "Proc_HSPR_PublicMeterResult_InsUpdate");
        }

        /// <summary>
        /// 抄表
        /// </summary>
        private string MeterInsUpdate(DataRow row, string storedProcedureName)
        {
            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }
            if (!row.Table.Columns.Contains("MeterID") || string.IsNullOrEmpty(row["MeterID"].ToString()))
            {
                return JSONHelper.FromString(false, "表计编号不能为空");
            }

            string meterId = row["MeterID"].ToString();

            string fileUrl = "";//平安需求3487 记录抄表图片

            decimal startDegree = 0;
            decimal endDegree = 0;
            decimal newStartDegree = 0;
            decimal newEndDegree = 0;
            decimal extraDosage = 0;

            if (row.Table.Columns.Contains("FileUrl") && !string.IsNullOrEmpty(row["FileUrl"].ToString()))
            {
                fileUrl = row["FileUrl"].ToString();
            }

            if (row.Table.Columns.Contains("StartDegree") && !string.IsNullOrEmpty(row["StartDegree"].ToString()))
            {
                startDegree = AppGlobal.StrToDec(row["StartDegree"].ToString());
            }

            if (row.Table.Columns.Contains("EndDegree") && !string.IsNullOrEmpty(row["EndDegree"].ToString()))
            {
                endDegree = AppGlobal.StrToDec(row["EndDegree"].ToString());
            }

            if (row.Table.Columns.Contains("NewStartDegree") && !string.IsNullOrEmpty(row["NewStartDegree"].ToString()))
            {
                newStartDegree = AppGlobal.StrToDec(row["NewStartDegree"].ToString());
            }

            if (row.Table.Columns.Contains("NewEndDegree") && !string.IsNullOrEmpty(row["NewEndDegree"].ToString()))
            {
                newEndDegree = AppGlobal.StrToDec(row["NewEndDegree"].ToString());
            }

            if (row.Table.Columns.Contains("ExtraDosage") && !string.IsNullOrEmpty(row["ExtraDosage"].ToString()))
            {
                extraDosage = AppGlobal.StrToDec(row["ExtraDosage"].ToString());
            }

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MeterID", meterId);
            parameters.Add("@ListDate", DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            parameters.Add("@MeterStyle", 0);
            parameters.Add("@StartDegree", startDegree);
            parameters.Add("@EndDegree", endDegree);
            parameters.Add("@NewStartDegree", newStartDegree);
            parameters.Add("@NewEndDegree", newEndDegree);
            parameters.Add("@ExtraDosage", extraDosage);
            parameters.Add("@UserCode", Global_Var.LoginUserCode);

            //if (Global_Var.LoginCorpID == "1906")
            //{
            //    parameters.Add("@ImgUrl", fileUrl);
            //}

            if (row.Table.Columns.Contains("CustID") && !string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                parameters.Add("@OwnerCustID", row["CustID"].ToString());
            }

            using (var con = new SqlConnection(Global_Fun.BurstConnectionString(AppGlobal.StrToInt(meterId.Substring(0,6)), Global_Fun.BURST_TYPE_CHARGE)))
            {
                int a = con.Execute(storedProcedureName, parameters, null, null, CommandType.StoredProcedure);
                return JSONHelper.FromString(true, "操作成功");
            }
        }

        private string SwitchRoomMeter(DataRow row)
        {
            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }

            if (!row.Table.Columns.Contains("Next") || string.IsNullOrEmpty(row["Next"].ToString()))
            {
                return JSONHelper.FromString(false, "Next参数不能为空");
            }

            string corpId = row["CorpID"].ToString();
            string commId = row["CommID"].ToString();
            string roomId = row["RoomID"].ToString();
            string next = row["Next"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = @"SELECT TOP 1 a.RoomID FROM Tb_HSPR_Room a WHERE isnull(a.IsDelete,0)=0 AND a.CommID=@CommID AND a.RoomID>@RoomID
                                AND (SELECT count(0) FROM Tb_HSPR_CustomerMeter b WHERE a.RoomID=b.RoomID)>0 ORDER BY a.RoomID ASC";

                if (next != "1")
                {
                    sql = @"SELECT TOP 1 a.RoomID FROM Tb_HSPR_Room a WHERE isnull(a.IsDelete,0)=0 AND a.CommID=@CommID AND a.RoomID<@RoomID
                                AND (SELECT count(0) FROM Tb_HSPR_CustomerMeter b WHERE a.RoomID=b.RoomID)>0 ORDER BY a.RoomID DESC";
                }

                long room = conn.Query<long>(sql, new { CommID = commId, RoomID = roomId }).FirstOrDefault();

                if (room != 0)
                {
                    sql = @"SELECT DISTINCT a.MeterID,a.MeterSign,a.MeterName,a.MeterType,a.Ratio,d.CustID AS OwnerCustID, e.CustName AS OwnerCustName,
                                a.RoomID,isnull(c.RoomName,c.RoomSign) AS RoomSign,
                                isnull(b.RestID,0) AS RestID,b.ListDate,isnull(b.StartDegree,0) AS StartDegree,isnull(b.EndDegree,0) AS EndDegree,
                                isnull(b.Dosage,0) AS Dosage,isnull(b.OldDosage,0) AS OldDosage,isnull(b.ExtraDosage,0) AS ExtraDosage,
                                isnull(b.TotalDosage,0) AS TotalDosage,
                                isnull(b.Price,0) AS Price,isnull(b.Amount,0) AS Amount,isnull(b.MeterHint,0) AS MeterHint,b.IsAudit,a.IsReverse
                            FROM Tb_HSPR_CustomerMeter a LEFT JOIN view_HSPR_CustomerMeterResult_Filter b ON a.MeterID=b.MeterID
                            LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID
                            LEFT JOIN Tb_HSPR_CustomerLive d ON a.RoomID=d.RoomID AND isnull(d.IsDelLive,0)=0 AND IsActive=1
                            LEFT JOIN Tb_HSPR_Customer e ON e.CustID=d.CustID
                            WHERE not exists(select * from view_HSPR_CustomerMeterResult_Filter c where c.MeterID=a.MeterID
                                AND b.ListDate<c.ListDate) AND isnull(a.IsDelete,0)= 0 AND a.RoomID=@RoomID";

                    if (corpId == "1953")
                    {
                        sql = @"SELECT DISTINCT a.MeterID,a.MeterSign,a.MeterName,a.MeterType,a.Ratio,d.CustID AS OwnerCustID, e.CustName AS OwnerCustName,
                                a.RoomID,isnull(c.RoomSign,c.RoomName) AS RoomSign,
                                isnull(b.RestID,0) AS RestID,b.ListDate,isnull(b.StartDegree,0) AS StartDegree,isnull(b.EndDegree,0) AS EndDegree,
                                isnull(b.Dosage,0) AS Dosage,isnull(b.OldDosage,0) AS OldDosage,isnull(b.ExtraDosage,0) AS ExtraDosage,
                                isnull(b.TotalDosage,0) AS TotalDosage,
                                isnull(b.Price,0) AS Price,isnull(b.Amount,0) AS Amount,isnull(b.MeterHint,0) AS MeterHint,b.IsAudit,a.IsReverse
                            FROM Tb_HSPR_CustomerMeter a LEFT JOIN view_HSPR_CustomerMeterResult_Filter b ON a.MeterID=b.MeterID
                            LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID
                            LEFT JOIN Tb_HSPR_CustomerLive d ON a.RoomID=d.RoomID AND isnull(d.IsDelLive,0)=0 AND IsActive=1
                            LEFT JOIN Tb_HSPR_Customer e ON e.CustID=d.CustID
                            WHERE not exists(select * from view_HSPR_CustomerMeterResult_Filter c where c.MeterID=a.MeterID
                                AND b.ListDate<c.ListDate) AND isnull(a.IsDelete,0)= 0 AND a.RoomID=@RoomID";
                    }

                    var resultSet = conn.Query(sql, new { RoomID = room });

                    //if (resultSet.Count() == 0)
                    //{
                    //    dynamic roomInfo = conn.Query(@"SELECT a.RoomID,isnull(a.RoomName,a.RoomSign) AS RoomSign,b.CustID AS OwnerCustID,c.CustName as OwnerCustName
                    //                                    FROM Tb_HSPR_Room a LEFT OUTER JOIN Tb_HSPR_CustomerLive b ON a.RoomID = b.RoomID 
                    //                                        AND isnull(b.IsDelLive, 0) = 0 AND b.LiveType=1
                    //                                      LEFT OUTER JOIN Tb_HSPR_Customer c ON b.CustID = c.CustID
                    //                                    WHERE a.RoomID =@RoomID",
                    //                                    new { RoomID = room }).FirstOrDefault();
                    //    return new ApiResult(false, roomInfo).toJson();
                    //}
                    //else
                    //{
                    return new ApiResult(true, resultSet).toJson();
                    //}
                }

                return JSONHelper.FromString(false, "没有更多了");
            }
        }

        private string SwitchPublicMeter(DataRow row)
        {
            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("MeterID") || string.IsNullOrEmpty(row["MeterID"].ToString()))
            {
                return JSONHelper.FromString(false, "表计编号不能为空");
            }

            if (!row.Table.Columns.Contains("Next") || string.IsNullOrEmpty(row["Next"].ToString()))
            {
                return JSONHelper.FromString(false, "Next参数不能为空");
            }

            string corpId = row["CorpID"].ToString();
            string commID = row["CommID"].ToString();
            string meterId = row["MeterID"].ToString();
            string next = row["Next"].ToString();

            using (var con = new SqlConnection(Global_Fun.BurstConnectionString(AppGlobal.StrToInt(meterId.Substring(0, 6)), Global_Fun.BURST_TYPE_CHARGE)))
            {
                string sql = @"SELECT DISTINCT TOP 1 a.MeterID,a.MeterSign,a.MeterName,a.MeterType,a.CommID,a.Ratio,a.Location,b.RestID,b.ListDate,
                                b.StartDegree,b.EndDegree,b.Dosage,b.OldDosage,b.ExtraDosage,b.TotalDosage,b.Price,b.Amount,b.MeterHint,b.IsAudit,b.IsReverse
                                FROM Tb_HSPR_PublicMeter a LEFT OUTER JOIN view_HSPR_PublicMeterResult_Filter b ON a.MeterID=b.MeterID
                                WHERE not exists(select * from view_HSPR_PublicMeterResult_Filter c WHERE c.MeterID=a.MeterID AND b.ListDate<c.ListDate) AND isnull(a.IsDelete,0)=0
                                AND a.MeterID>@MeterID AND a.CommID=@CommID ORDER BY a.MeterID ASC;";

                if (next != "1")
                {
                    sql = @"SELECT DISTINCT TOP 1 a.MeterID,a.MeterSign,a.MeterName,a.MeterType,a.CommID,a.Ratio,a.Location,b.RestID,b.ListDate,
                                b.StartDegree,b.EndDegree,b.Dosage,b.OldDosage,b.ExtraDosage,b.TotalDosage,b.Price,b.Amount,b.MeterHint,b.IsAudit,b.IsReverse
                                FROM Tb_HSPR_PublicMeter a LEFT OUTER JOIN view_HSPR_PublicMeterResult_Filter b ON a.MeterID=b.MeterID
                                WHERE not exists(select * from view_HSPR_PublicMeterResult_Filter c WHERE c.MeterID=a.MeterID AND b.ListDate<c.ListDate) AND isnull(a.IsDelete,0)=0
                                AND a.MeterID<@MeterID AND a.CommID=@CommID ORDER BY a.MeterID DESC;";

                }

                return new ApiResult(true, con.Query(sql, new { MeterID = meterId, CommID = commID })).toJson();
            }
        }

        /// <summary>
        /// 获取小区下楼栋列表，包含单元，楼层、楼层下房屋及未抄表数量
        /// </summary>
        private async Task<string> GetBuildRoomUnreadMeters(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }

            if (!row.Table.Columns.Contains("BuildSNum") || string.IsNullOrEmpty(row["BuildSNum"].ToString()))
            {
                return JSONHelper.FromString(false, "楼栋不能为空");
            }



            string commId = row["CommID"].ToString();
            string buildSNum = row["BuildSNum"].ToString();
            string unitSNum = null;
            string sql;

            if (row.Table.Columns.Contains("UnitSNum") && !string.IsNullOrEmpty(row["UnitSNum"].ToString()))
            {
                unitSNum = row["UnitSNum"].ToString();
            }


            sql = $@"SELECT BuildName,BuildSNum FROM Tb_HSPR_Building 
                                WHERE isnull(IsDelete,0)=0 
                                AND CommID={commId} 
                                AND BuildSNum = {buildSNum}
                            ORDER BY BuildName;

                            SELECT DISTINCT UnitSNum, BuildSNum FROM Tb_HSPR_Room 
                                WHERE isnull(IsDelete,0)=0 
                                AND CommID={commId}
                                AND BuildSNum = {buildSNum}
                                {((unitSNum == null) ? "" : " AND UnitSNum='" + unitSNum + "'")}
                            ORDER BY BuildSNum, UnitSNum;

                            SELECT DISTINCT FloorSNum, UnitSNum, BuildSNum FROM Tb_HSPR_Room WHERE isnull(IsDelete, 0)=0 
                                AND UnitSNum IS NOT NULL AND FloorSNum IS NOT NULL 
                                AND CommID={commId}
                                AND BuildSNum = {buildSNum}
                                {((unitSNum == null) ? "" : " AND UnitSNum='" + unitSNum + "'")}
                            ORDER BY BuildSNum, UnitSNum, FloorSNum;

                            SELECT A.MeterID,
                                    A.MeterName,
                                    A.RoomID,
	(SELECT Top 1 StartDegree FROM Tb_HSPR_CustomerMeterResult WHERE MeterID= A.MeterID ORDER BY LastListDate DESC) AS StartDegree,
	(SELECT Top 1 EndDegree FROM Tb_HSPR_CustomerMeterResult WHERE MeterID= A.MeterID ORDER BY LastListDate DESC) AS EndDegree
                                    FROM Tb_HSPR_CustomerMeter A
                                WHERE MeterID 
                                    NOT IN (SELECT MeterID FROM Tb_HSPR_CustomerMeterResult
                                    WHERE ListDate>=convert(varchar(10),dateadd(day,-datepart(day,GETDATE())+1,GETDATE()),23)+' 00:00:00')
                                AND CommID={commId} 
                                AND RoomID IN(SELECT RoomID FROM Tb_HSPR_Room WHERE 
                                    CommID={commId}
                                AND BuildSNum = {buildSNum}
                                {((unitSNum == null) ? "" : " AND UnitSNum='" + unitSNum + "'")})";

            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var queryResult = con.QueryMultiple(sql);

                var building = queryResult.Read();
                var unit = queryResult.Read();
                var floor = queryResult.Read();
                var meters = queryResult.Read();

                var result = con.Query(@"SELECT a.RoomID,a.RoomSign,a.RoomName,b.CustID,c.CustName,c.MobilePhone,
                            a.BuildSNum,a.UnitSNum,a.FloorSNum,d.BuildName
                            FROM Tb_HSPR_Room a
                            LEFT JOIN Tb_HSPR_CustomerLive b ON a.RoomID=b.RoomID
                            LEFT JOIN Tb_HSPR_Customer c ON b.CustID=c.CustID
                            LEFT JOIN Tb_HSPR_Building d ON a.CommID=d.CommID 
                            AND d.BuildSNum=a.BuildSNum 
                            AND d.UnitNum = a.UnitSNum
                            WHERE isnull(b.IsDelLive,0)=0 AND b.IsActive=1 AND isnull(c.IsDelete,0)=0 
                            AND isnull(a.IsDelete, 0)=0 AND a.CommID=@CommID", new { CommID = commId });

                var tempBuilding = new List<object>();

                await Task.Run(() =>
                {
                    if (building.Count() > 0)
                    {
                        // 楼栋
                        foreach (dynamic buildingItem in building)
                        {
                            var tempUnit = new List<object>();
                            // 单元
                            foreach (var unitItem in unit)
                            {
                                if (unitItem.BuildSNum == buildingItem.BuildSNum)
                                {
                                    // 单元下楼层
                                    var tempFloor = floor.Where<dynamic>(u => (u.BuildSNum == unitItem.BuildSNum && u.UnitSNum == unitItem.UnitSNum));
                                    var floorsList = tempFloor.Select((p) =>
                                    {
                                        return new
                                        {
                                            FloorSNum = p.FloorSNum,
                                            Rooms = result
                                            .Where(r => r.BuildSNum == unitItem.BuildSNum && r.UnitSNum == unitItem.UnitSNum && r.FloorSNum == p.FloorSNum)
                                            .Select(r => new
                                            {
                                                RoomID = r.RoomID,
                                                RoomSign = r.RoomSign,
                                                RoomName = r.RoomName,
                                                CustID = r.CustID,
                                                CustName = r.CustName,
                                                MobilePhone = r.MobilePhone,
                                                BuildSNum = r.BuildSNum,
                                                UnitSNum = r.UnitSNum,
                                                FloorSNum = r.FloorSNum,
                                                BuildName = r.BuildName,
                                                UnreadMeter = meters.Where(m => m.RoomID == r.RoomID)
                                                                    .Select(m => new { MeterID = m.MeterID, MeterName = m.MeterName })
                                            }),
                                        };
                                    });

                                    tempUnit.Add(new { UnitSNum = unitItem.UnitSNum, Floors = floorsList });
                                }
                            }

                            tempBuilding.Add(new { BuildName = buildingItem.BuildName, BuildSNum = buildingItem.BuildSNum, Unit = tempUnit });
                        }
                    }
                });

                return new ApiResult(true, tempBuilding).toJson();
            }
        }

        private async Task<string> GetMultipleBuildRoomUnreadMeters(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }

            if (!row.Table.Columns.Contains("QueryInfo") || string.IsNullOrEmpty(row["QueryInfo"].ToString()))
            {
                return JSONHelper.FromString(false, "楼栋单元信息不能为空");
            }

            string commId = row["CommID"].ToString();
            string queryInfo = row["QueryInfo"].ToString();
            string sql;
            string connStr = PubConstant.hmWyglConnectionString;
            int meterType = 0;//1 水表 2电表 3 气表
            int isRegional = 0;//是否查询公区
            var tempBuilding = new List<object>();//

            if (row.Table.Columns.Contains("MeterType") && !string.IsNullOrEmpty(row["MeterType"].ToString()))
            {
                meterType = AppGlobal.StrToInt(row["MeterType"].ToString());
            }

            if (row.Table.Columns.Contains("IsRegional") && !string.IsNullOrEmpty(row["IsRegional"].ToString()))
            {
                isRegional = AppGlobal.StrToInt(row["IsRegional"].ToString());
            }

            try
            {
                var queryList = queryInfo.Split('|');
                if (queryList.Count() > 0)
                {
                    foreach (string temp in queryList)
                    {
                        var tempObject = temp.Split(',');
                        if (tempObject.Count() > 0)
                        {
                            var tempBuild = "";
                            var tempUnit = "";
                            for (int i = 0; i < tempObject.Count(); i++)
                            {

                                if (i == 0)//第0位默认为BuildSNum
                                {
                                    tempBuild = tempObject[i];
                                }
                                else
                                {
                                    tempUnit = tempObject[i];

                                    sql = $@"SELECT BuildName,BuildSNum FROM Tb_HSPR_Building 
                                            WHERE isnull(IsDelete,0)=0 AND CommID={commId} AND BuildSNum={tempBuild} ORDER BY BuildName;

                                            SELECT DISTINCT UnitSNum, BuildSNum FROM Tb_HSPR_Room 
                                            WHERE isnull(IsDelete,0)=0 AND CommID={commId} AND BuildSNum={tempBuild}
                                                {((tempUnit == null) ? "" : " AND UnitSNum='" + tempUnit + "'")}
                                            ORDER BY BuildSNum, UnitSNum;

                                            SELECT DISTINCT FloorSNum, UnitSNum, BuildSNum FROM Tb_HSPR_Room 
                                            WHERE isnull(IsDelete, 0)=0 AND UnitSNum IS NOT NULL AND FloorSNum IS NOT NULL 
                                                AND CommID={commId} AND BuildSNum = {tempBuild}
                                                {((tempUnit == null) ? "" : " AND UnitSNum='" + tempUnit + "'")}
                                            ORDER BY BuildSNum, UnitSNum, FloorSNum;

                                            SELECT A.MeterID, A.MeterName, A.RoomID,A.MeterType AS MeterType,
	                                            (SELECT Top 1 StartDegree FROM Tb_HSPR_CustomerMeterResult WHERE MeterID=A.MeterID ORDER BY LastListDate DESC) AS StartDegree,
	                                            (SELECT Top 1 EndDegree FROM Tb_HSPR_CustomerMeterResult WHERE MeterID=A.MeterID ORDER BY LastListDate DESC) AS EndDegree
                                            FROM Tb_HSPR_CustomerMeter A
                                            WHERE MeterID NOT IN (SELECT MeterID FROM Tb_HSPR_CustomerMeterResult
                                                                    WHERE ListDate>=convert(varchar(10),dateadd(day,-datepart(day,GETDATE())+1,GETDATE()),23)+' 00:00:00')
                                                AND CommID={commId} AND RoomID IN(SELECT RoomID FROM Tb_HSPR_Room WHERE CommID={commId} AND BuildSNum = {tempBuild}
                                                {((tempUnit == null) ? "" : " AND UnitSNum='" + tempUnit + "'")}
                                                {((meterType == 0) ? "" : " AND MeterType='" + meterType + "'")}
                                                {((isRegional == 0) ? "" : " AND RoomID IS NULL")})";

                                    using (IDbConnection con = new SqlConnection(connStr))
                                    {
                                        var queryResult = con.QueryMultiple(sql);

                                        var building = queryResult.Read();
                                        var unit = queryResult.Read();
                                        var floor = queryResult.Read();
                                        var meters = queryResult.Read();

                                        var result = con.Query(@"SELECT a.RoomID,a.RoomSign,a.RoomName,b.CustID,c.CustName,c.MobilePhone,
                                                                a.BuildSNum,a.UnitSNum,a.FloorSNum,d.BuildName
                                                                FROM Tb_HSPR_Room a
                                                                LEFT JOIN Tb_HSPR_CustomerLive b ON a.RoomID=b.RoomID
                                                                LEFT JOIN Tb_HSPR_Customer c ON b.CustID=c.CustID
                                                                LEFT JOIN Tb_HSPR_Building d ON a.CommID=d.CommID 
                                                                AND d.BuildSNum=a.BuildSNum 
                                                                AND d.UnitNum = a.UnitSNum
                                                                WHERE isnull(b.IsDelLive,0)=0 AND b.IsActive=1 AND isnull(c.IsDelete,0)=0 
                                                                AND isnull(a.IsDelete, 0)=0 AND a.CommID=@CommID", new { CommID = commId });



                                        await Task.Run(() =>
                                        {
                                            if (building.Count() > 0)
                                            {
                                                // 楼栋
                                                foreach (dynamic buildingItem in building)
                                                {
                                                    var tempUnitList = new List<object>();
                                                    // 单元
                                                    foreach (var unitItem in unit)
                                                    {
                                                        if (unitItem.BuildSNum == buildingItem.BuildSNum)
                                                        {
                                                            // 单元下楼层
                                                            var tempFloor = floor.Where<dynamic>(u => (u.BuildSNum == unitItem.BuildSNum && u.UnitSNum == unitItem.UnitSNum));
                                                            var floorsList = tempFloor.Select((p) =>
                                                            {
                                                                return new
                                                                {
                                                                    FloorSNum = p.FloorSNum,
                                                                    Rooms = result
                                                                    .Where(r => r.BuildSNum == unitItem.BuildSNum && r.UnitSNum == unitItem.UnitSNum && r.FloorSNum == p.FloorSNum)
                                                                    .Select(r => new
                                                                    {
                                                                        RoomID = r.RoomID,
                                                                        RoomSign = r.RoomSign,
                                                                        RoomName = r.RoomName,
                                                                        CustID = r.CustID,
                                                                        CustName = r.CustName,
                                                                        MobilePhone = r.MobilePhone,
                                                                        BuildSNum = r.BuildSNum,
                                                                        UnitSNum = r.UnitSNum,
                                                                        FloorSNum = r.FloorSNum,
                                                                        BuildName = r.BuildName,
                                                                        UnreadMeter = meters.Where(m => m.RoomID == r.RoomID)
                                                                                            .Select(m => new
                                                                                            {
                                                                                                MeterID = m.MeterID,
                                                                                                MeterName = m.MeterName,
                                                                                                StartDegree = m.StartDegree,
                                                                                                EndDegree = m.EndDegree,
                                                                                                MeterType = m.MeterType
                                                                                            })
                                                                    }),
                                                                };
                                                            });

                                                            tempUnitList.Add(new { UnitSNum = unitItem.UnitSNum, Floors = floorsList });
                                                        }
                                                    }

                                                    tempBuilding.Add(new { BuildName = buildingItem.BuildName, BuildSNum = buildingItem.BuildSNum, Unit = tempUnitList });
                                                }
                                            }
                                        });

                                    }
                                }

                            }
                        }

                    }
                }
                return new ApiResult(true, tempBuilding).toJson();
            }
            catch (Exception e)
            {
                return new ApiResult(false, e.ToString()).toJson();
            }


        }
    }
}
