using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Business
{
    public class HouseInspectionManage : PubInfo
    {
        public HouseInspectionManage()
        {
            base.Token = "20180118HouseInspectionManage";
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
                case "GetHousingProjList":
                    Trans.Result = GetHousingProjList(Row); // 获取验房项目
                    break;
                case "GetOccupationList":
                    Trans.Result = GetOccupationList(Row); // 获取入伙待办、进场待办，入伙:Type=1，进场:Type=2,退场=3
                    break;
                case "GetHouseInspectionReviewList":
                    Trans.Result = GetHouseInspectionReviewList(Row); // 获取复验待办列表
                    break;
                case "SaveHousingInsp":
                    Trans.Result = SaveHousingInsp(Row); // 保存验房登记信息
                    break;
                case "SaveHouseInspectionReview":
                    Trans.Result = SaveHouseInspectionReview(Row); // 复验确认
                    break;
                case "GetDecorateAcceptInfo":// 获取装修验收信息
                    Trans.Result = GetDecorateAcceptInfo(Row);
                    break;
                case "GetDecorateCostList":// 获取装修验收信息
                    Trans.Result = GetDecorateCostList(Row);
                    break;
                case "SaveDecorateInfo": //装修验收登记
                    Trans.Result = SaveDecorateInfo(Row);
                    break;
                default:
                    break;
            }
        }

        private string SaveDecorateInfo(DataRow row)
        {
            #region 参数校验
            if (!row.Table.Columns.Contains("RenoID"))
            {
                return new ApiResult(false, "缺少参数RenoID").toJson();
            }
            string RenoID = row["RenoID"].ToString();

            if (!row.Table.Columns.Contains("AcceptViews"))
            {
                return new ApiResult(false, "缺少参数AcceptViews").toJson();
            }
            string AcceptViews = row["AcceptViews"].ToString();

            if (!row.Table.Columns.Contains("CostID"))
            {
                return new ApiResult(false, "缺少参数CostID").toJson();
            }
            string CostID = row["CostID"].ToString();
            if (!row.Table.Columns.Contains("FeesAmount"))
            {
                return new ApiResult(false, "缺少参数FeesAmount").toJson();
            }
            if (!decimal.TryParse(row["FeesAmount"].ToString(), out decimal FeesAmount))
            {
                FeesAmount = 0.00M;
            }
            if (!row.Table.Columns.Contains("CostName"))
            {
                return new ApiResult(false, "缺少参数CostName").toJson();
            }
            string CostName = row["CostName"].ToString();

            if (!string.IsNullOrEmpty(CostID))
            {
                if (FeesAmount <= 0)
                {
                    return new ApiResult(false, "金额不能小于等于0").toJson();
                }
            }
            if (!row.Table.Columns.Contains("Memo"))
            {
                return new ApiResult(false, "缺少参数Memo").toJson();
            }
            string Memo = row["Memo"].ToString();
            if (!row.Table.Columns.Contains("IsQualified"))
            {
                return new ApiResult(false, "缺少参数IsQualified").toJson();
            }
            if (!int.TryParse(row["IsQualified"].ToString(), out int IsQualified))
            {
                IsQualified = 0;
            }
            if (0 != IsQualified && 1 != IsQualified)
            {
                IsQualified = 0;
            }
            if (!row.Table.Columns.Contains("Images"))
            {
                return new ApiResult(false, "缺少参数Images").toJson();
            }
            string Images = row["Images"].ToString().Trim().Trim(',');

            DateTime DateNow = DateTime.Now;
            #endregion

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();
                try
                {
                    dynamic RenoInfo = conn.QueryFirstOrDefault("SELECT * FROM view_HSPR_RenoCust_Filter WHERE RenoID = @RenoID ", new { RenoID }, trans);
                    if (null == RenoInfo)
                    {
                        trans.Rollback();
                        return new ApiResult(false, "没有找到该装修办理记录").toJson();
                    }
                    int AcceptSNum = conn.Query<int>("SELECT * FROM Tb_HSPR_RenoCustAccept WHERE RenoID = @RenoID", new { RenoID },trans).Count() + 1;

                    int CommID = Convert.ToInt32(RenoInfo.CommID);

                    if (!string.IsNullOrEmpty(CostID))
                    {
                        long CustID = Convert.ToInt64(RenoInfo.CustID);
                        long RoomID = Convert.ToInt64(RenoInfo.RoomID);
                        long FeesID = 0;
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("CommID", CommID);
                        parameters.Add("FeesID", FeesID, DbType.Int64, ParameterDirection.Output);
                        conn.Execute("Proc_HSPR_Fees_GetNextID", parameters, trans, null, CommandType.StoredProcedure);
                        FeesID = parameters.Get<long>("FeesID");

                        string sql = "INSERT INTO Tb_HSPR_Fees(FeesID,CommID,CostID,CustID,RoomID,FeesDueDate,FeesStateDate,FeesEndDate,DueAmount,DebtsAmount,AccountsDueDate,FeesChargeDate,FeesMemo,CalcAmount2,IsCharge)";
                        sql += " VALUES(@FeesID,@CommID,@CostID,@CustID,@RoomID,GETDATE(),CONVERT(VARCHAR(100), DATEADD(d,-day(GETDATE())+1,GETDATE()), 23),CONVERT(VARCHAR(100), DATEADD(d,-day(GETDATE()),DATEADD(m,1,GETDATE())), 23),@FeesAmount,@FeesAmount,GETDATE(),GETDATE(),'装修验收入账',1,0)";
                        if (conn.Execute(sql, new { FeesID, CommID, CostID, CustID, RoomID, FeesAmount }, trans, null, CommandType.Text) <= 0)
                        {
                            trans.Rollback();
                            return new ApiResult(false, "添加费用失败,请重试").toJson();
                        }
                    }
                    
                    if (conn.Execute("Proc_HSPR_RenoCustAccept_Insert", new { @IID = "", CommID, RenoID = Convert.ToInt64(RenoInfo.RenoID), AcceptDep = "", AcceptMan = Global_Var.LoginUserName, AcceptDate = DateNow.ToString(), AcceptViews, Memo, AcceptSNum, IsQualified, CostID, CostName, FeesAmount }, trans, null, CommandType.StoredProcedure) <= 0)
                    {
                        trans.Rollback();
                        return new ApiResult(false, "验收失败,请重试").toJson();
                    }
                    if (!string.IsNullOrEmpty(Images))
                    {
                        dynamic RenoCustAccept = conn.QueryFirstOrDefault("SELECT * FROM Tb_HSPR_RenoCustAccept WHERE CommID = @CommID AND RenoID = @RenoID AND AcceptDate = @AcceptDate AND IsQualified = @IsQualified AND AcceptMan = @AcceptMan", new { CommID, RenoID, AcceptDate = DateNow.ToString(), IsQualified, AcceptMan = Global_Var.LoginUserName }, trans);
                        if (null == RenoCustAccept)
                        {
                            trans.Rollback();
                            return new ApiResult(false, "验收失败(文件保存失败),请重试").toJson();
                        }
                        string ProjID = Convert.ToString(RenoCustAccept.IID);
                        foreach (var item in Images.Split(',')) 
                        {
                            if (string.IsNullOrEmpty(item))
                            {
                                continue;
                            }
                            string FilePath = item.Replace("\\", "/"); ;
                            string FileName = FilePath.Substring(FilePath.LastIndexOf("/"), FilePath.Length - FilePath.LastIndexOf("/"));
                            string Fix = FileName.Substring(FileName.LastIndexOf('.'), FileName.Length - FileName.LastIndexOf('.'));
                            if (conn.Execute("INSERT INTO Tb_HSPR_HousingFiles VALUES(NEWID(),@CommID,@InspID,@ProjID,@FilePath,@Sort,@FileName,@RoomID,@AddTime,@AddUser,@Fix,@Classification)", new { CommID, InspID = "", ProjID, FilePath, Sort = 0, FileName,RoomID = "", AddTime = DateNow.ToString(), AddUser = Global_Var.LoginUserCode, Fix, Classification = "费用详情附件" }, trans) <= 0)
                            {
                                trans.Rollback();
                                return new ApiResult(false, "验收失败(文件保存失败),请重试").toJson();
                            }
                        }
                    }
                    trans.Commit();
                    return new ApiResult(true, "验收成功").toJson();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    GetLog().Error(ex);
                    return new ApiResult(false, "接口抛出了一个异常").toJson();
                }
            }
        }
        private string GetDecorateCostList(DataRow row)
        {
            if (!row.Table.Columns.Contains("RenoID"))
            {
                return new ApiResult(false, "缺少参数RenoID").toJson();
            }
            string RenoID = row["RenoID"].ToString();
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dynamic RenoInfo = conn.QueryFirstOrDefault("SELECT * FROM view_HSPR_RenoCust_Filter WHERE RenoID = @RenoID ", new { RenoID });
                if (null == RenoInfo)
                {
                    return new ApiResult(false, "没有找到该装修办理记录").toJson();
                }
                int CommID = Convert.ToInt32(RenoInfo.CommID);
                List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();
                DataTable dt = conn.ExecuteReader("Proc_HSPR_CorpCostItem_GetCommNodes", new { CommID = CommID, CostName = "", SysCostSign = "", CostGeneType = 0, IsHis = 0, CostType = 0 }, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
                if (null != dt && dt.Rows.Count > 0)
                {
                    #region 移除不必要的列
                    if (dt.Columns.Contains("CorpCostID"))
                    {
                        dt.Columns.Remove("CorpCostID");
                    }

                    if (dt.Columns.Contains("CostSNum"))
                    {
                        dt.Columns.Remove("CostSNum");
                    }

                    if (dt.Columns.Contains("CostType"))
                    {
                        dt.Columns.Remove("CostType");
                    }

                    if (dt.Columns.Contains("IsTreeRoot"))
                    {
                        dt.Columns.Remove("IsTreeRoot");
                    }
                    #endregion
                    Dictionary<string, object> dictionary;
                    foreach (DataRow item in dt.Rows)
                    {
                        dictionary = new Dictionary<string, object>();
                        foreach (DataColumn colum in dt.Columns)
                        {
                            dictionary.Add(colum.ColumnName, item[colum.ColumnName]);
                        }
                        resultList.Add(dictionary);
                    }
                }
                return new ApiResult(true, resultList).toJson();
            }
        }

        private string GetDecorateAcceptInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("RenoID"))
            {
                return new ApiResult(false, "缺少参数RenoID").toJson();
            }
            string RenoID = row["RenoID"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dynamic RenoInfo = conn.QueryFirstOrDefault("SELECT * FROM view_HSPR_RenoCust_Filter WHERE RenoID = @RenoID ", new { RenoID });
                if (null == RenoInfo)
                {
                    return new ApiResult(false, "没有找到该装修办理记录").toJson();
                }
                string RenoStatus = Convert.ToString(RenoInfo.RenoStatus);
                if (!"装修".Equals(RenoStatus))
                {
                    return new ApiResult(false, "仅装修状态可以进行登记,当前装修办理记录状态为:" + RenoStatus).toJson();
                }
                return new ApiResult(true, new { RenoID = RenoInfo.RenoID,RoomName = string.IsNullOrEmpty(Convert.ToString(RenoInfo.RoomName))? Convert.ToString(RenoInfo.RoomSign): Convert.ToString(RenoInfo.RoomName),CustName = Convert.ToString(RenoInfo.CustName) }).toJson();
            }
        }
        /// <summary>
        /// 获取验房项目、验房标准
        /// </summary>
        private string GetHousingProjList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            
            string commID = row["CommID"].ToString();

            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = @"SELECT ProjID,HS_Projects,HS_Standards FROM Tb_HSPR_HousingProj WHERE CommID=@CommID";
                IEnumerable<dynamic> resultSet = con.Query(sql, new { CommID = commID });
                return new ApiResult(true, resultSet).toJson();
            }
        }

        /// <summary>
        /// 获取入伙待办、进场待办，入伙:Type=1，进场:Type=2
        /// </summary>
        private string GetOccupationList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("Type") || string.IsNullOrEmpty(row["Type"].ToString()))
            {
                return JSONHelper.FromString(false, "Type不能为空");
            }

            string commID = row["CommID"].ToString();
            string type = row["Type"].ToString();

            string sql = string.Empty;
            if (type == "1")
            {
                // 入伙待办
                sql = @"SELECT ('OccuID='+OccupationId) AS Id, RoomSign, CustName, RoomID, CustID, MobilePhone, PaperCode 
                            FROM View_Tb_Occupation_OccupationBacklog
                            WHERE OccupationState='待入伙' AND CommID={0} AND ISNULL(IsDelete,0)=0";
            }
            else if (type == "2")
            {
                // 进场待办，关联合同
                sql = @"SELECT ('ContID='+convert(NVARCHAR(36), x.ContID)) AS Id,x.RoomSign, x.CustName, 
                                x.CustomerLiveRoomID AS RoomID, x.CustID, y.MobilePhone, y.PaperCode
                            FROM view_HSPR_GetReadyList_Filter x LEFT JOIN Tb_HSPR_Customer y ON x.CustID=y.CustID
                            WHERE ISNULL(ApproachState, '')='' AND x.CommID={0} AND ISNULL(x.IsDelete,0)=0";
            }
            else if (type == "3")
            {
                // 退场待办，关联合同
                sql = @"SELECT ('WithdrawalID='+CONVERT(NVARCHAR(36), Id)) AS Id,RoomSign,CustName,RoomID, CustID, MobilePhone, PaperCode FROM VIEW_Tb_Customer_Withdrawal WHERE State = '待退场' AND CommID = '{0}'";
            }
            else
            {
                return new ApiResult(false, "未知任务类型").toJson();
            }

            sql = string.Format(sql, commID);

            int PageIndex = 1;
            int PageSize = 10;
            if (row.Table.Columns.Contains("PageIndex"))
            {
                PageIndex = Global_Fun.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize"))
            {
                PageSize = Global_Fun.StrToInt(row["PageSize"].ToString());
            }
            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FldName", "*");
                parameters.Add("@PageSize", PageSize);
                parameters.Add("@PageIndex", PageIndex);
                parameters.Add("@FldSort", "RoomID");
                parameters.Add("@Sort", 0);
                parameters.Add("@StrCondition", sql);
                parameters.Add("@Id", "Id");
                parameters.Add("@PageCount", 0, DbType.Int32, ParameterDirection.Output);
                parameters.Add("@Counts", 0, DbType.Int32, ParameterDirection.Output);

                IEnumerable<dynamic> resultSet = con.Query<dynamic>("Proc_System_TurnPage", parameters, null, false, null, CommandType.StoredProcedure);

                int PageCount = parameters.Get<int>("@PageCount");
                int Counts = parameters.Get<int>("@Counts");

                if (resultSet.Count() > 0)
                {
                    List<dynamic> result = new List<dynamic>();
                    using (IDbConnection con2 = new SqlConnection(PubConstant.hmWyglConnectionString))
                    {
                        foreach (dynamic item in resultSet)
                        {
                            // 获取验房记录
                            sql = @"SELECT * FROM Tb_HSPR_HousingInsp WHERE CustID=@CustID AND RoomID=@RoomID ORDER BY InspDate DESC";

                            IEnumerable<dynamic> historySet = con2.Query(sql, new { CustID = Convert.ToInt64(item.CustID), RoomID = Convert.ToInt64(item.RoomID) });

                            item.History = historySet;

                            if (historySet.Count() > 0)
                            {
                                foreach (var historyItem in historySet)
                                {
                                    sql = @"SELECT a.*, ((SELECT stuff((SELECT ','+FilePath FROM Tb_HSPR_HousingFiles 
                                                    WHERE InspID=@InspID AND ProjID=a.ProjID FOR XML PATH('')),1,1,''))) AS FilePath
                                        FROM Tb_HSPR_HousingInspDetail a WHERE a.InspID=@InspID ORDER BY ProjID";

                                    IEnumerable<dynamic> historyDetailSet = con2.Query(sql, new { InspID = Convert.ToString(historyItem.InspID) });

                                    historyItem.Detail = historyDetailSet;
                                }
                            }

                            result.Add(item);
                        }

                        string resultString = new ApiResult(true, result).toJson();
                        resultString = resultString.Insert(resultString.Length - 1, string.Format(",\"PageCount\":{0}", PageCount));
                        return resultString;
                    }
                }
                return JSONHelper.FromDataTable(new DataTable());
            }
        }

        /// <summary>
        /// 获取复验待办列表
        /// </summary>
        private string GetHouseInspectionReviewList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            
            string commID = row["CommID"].ToString();

            int PageIndex = 1;
            int PageSize = 10;
            if (row.Table.Columns.Contains("PageIndex"))
            {
                PageIndex = Global_Fun.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize"))
            {
                PageSize = Global_Fun.StrToInt(row["PageSize"].ToString());
            }

            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = @"SELECT b.IID,(SELECT stuff((SELECT ','+FilePath FROM Tb_HSPR_HousingFiles f WHERE f.InspID = CAST(b.InspID AS VARCHAR(50)) AND f.ProjID = CAST(b.ProjID AS VARCHAR(50)) FOR XML PATH ('')),1,1,'')) AS FilePath,
                                  c.HS_Projects AS ProjName,c.HS_Standards AS ProjStandard,
                                  a.IncidentID,a.IncidentNum,a.DispMan,a.DispDate,a.DealMan,a.MainEndDate,
                                  a.RoomID,e.RoomSign,d.InspRepr,d.AccoRepr,d.InspDate,isnull(b.Review,0) AS Review,g.CustName,g.MobilePhone  
                                FROM Tb_HSPR_IncidentAccept a
                                LEFT JOIN Tb_HSPR_HousingInspDetail b ON b.IID=a.HousingInspDetailID
                                LEFT JOIN Tb_HSPR_HousingProj c ON b.ProjID=c.ProjID
                                LEFT JOIN Tb_HSPR_HousingInsp d ON b.InspID=d.InspID
                                LEFT JOIN Tb_HSPR_Room e ON a.RoomID=e.RoomID
                                LEFT JOIN Tb_HSPR_Customer g ON d.CustID=g.CustID
                                WHERE isnull(a.DealState,0)=1 AND a.CommID=@CommID
                                    AND a.HousingInspDetailID IS NOT NULL 
                                    AND (isnull(b.Review,0)=0 OR b.Review=2)";
                IEnumerable<dynamic> resultSet = con.Query(sql, new { CommID = commID });
                return new ApiResult(true, resultSet).toJson();
            }
        }

        /// <summary>
        /// 保存验房登记信息
        /// </summary>
        private string SaveHousingInsp(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            
            string commID = row["CommID"].ToString();

            JArray inspList = (JArray)JsonConvert.DeserializeObject(row["Data"].ToString().Trim());//获得JsonObject对象

            if (null == inspList)
            {
                return new ApiResult(true, "InspList为空").toJson();
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                List<string> inspIds = new List<string>();

                foreach (JObject item in inspList)
                {
                    // 获取InspID
                    string InspID = conn.Query<string>("Proc_HSPR_HousingInsp_GetMaxNum", 
                        new { CommID = commID, SQLEx = "" }, null, true, null, CommandType.StoredProcedure).FirstOrDefault();

                    // 如果为空(获取失败),就跳过 不保存该记录
                    if (string.IsNullOrEmpty(InspID))
                    {
                        continue;
                    }

                    inspIds.Add(InspID + "|" + item["InspID"].ToString());

                    string CustID = item["CustID"].ToString();
                    string RoomID = item["RoomID"].ToString();
                    string InspDate = item["InspDate"].ToString();
                    string InspRepr = item["InspRepr"].ToString();
                    string AccoRepr = item["AccoRepr"].ToString();
                    string RectificationState = "";
                    if (item.ContainsKey("RectificationState"))
                    {
                        RectificationState = item["RectificationState"].ToString();
                    }
                    int CustPageType = 0;
                    if (item.ContainsKey("CustPageType"))
                    {
                        CustPageType = (int)item["CustPageType"];
                    }
                    int result = 0;

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("InspID", InspID);
                    parameters.Add("CommID", commID);
                    parameters.Add("CustID", CustID);
                    parameters.Add("RoomID", RoomID);
                    parameters.Add("InspDate", InspDate);
                    parameters.Add("InspRepr", InspRepr);
                    parameters.Add("AccoRepr", AccoRepr);
                    parameters.Add("Memo", "");
                    parameters.Add("Rectification", "");
                    parameters.Add("Processing", "");
                    parameters.Add("RectificationState", RectificationState);
                    parameters.Add("CustPageType", CustPageType);
                    result = conn.Execute("Proc_HSPR_HousingInsp_Insert", parameters, null, null, CommandType.StoredProcedure);

                    if (result <= 0)
                    {
                        continue;
                    }

                    JArray housingProjCheckResultList = (JArray)item["HousingProjCheckResultList"];

                    // 插入验房项目标准检查结果
                    if (null != housingProjCheckResultList)
                    {
                        foreach (JObject item2 in housingProjCheckResultList)
                        {
                            string ProjID = item2["ProjID"].ToString();
                            string RectContent = item2["RectContent"].ToString();
                            string Memo = item2["Memo"].ToString();
                            string IncidentID = "";
                            if (item2.ContainsKey("IncidentID")) {
                                IncidentID = item2["IncidentID"].ToString();
                            }
                            parameters = new DynamicParameters();
                            parameters.Add("InspID", InspID);
                            parameters.Add("ProjID", ProjID);
                            parameters.Add("RectContent", RectContent);
                            parameters.Add("Memo", Memo);

                            if (!string.IsNullOrEmpty(IncidentID) && !"0".Equals(IncidentID))
                            {
                                parameters.Add("IncidentID", IncidentID);
                            } 
                            conn.Execute("Proc_HSPR_HousingInspDetail_InsUpdate", parameters, null, null, CommandType.StoredProcedure);
                        }
                    }
                    else
                    {
                        return JSONHelper.FromString(false, "插入验房项目标准检查结果失败");
                    }
                }

                return new ApiResult(true, string.Join(",", inspIds.ToArray())).toJson();
            }
        }

        /// <summary>
        /// 复验确认
        /// </summary>
        private string SaveHouseInspectionReview(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("IID") || string.IsNullOrEmpty(row["IID"].ToString()))
            {
                return JSONHelper.FromString(false, "验房项目不能为空");
            }

            if (!row.Table.Columns.Contains("Review") || string.IsNullOrEmpty(row["Review"].ToString()))
            {
                return JSONHelper.FromString(false, "验房项目不能为空");
            }
            
            string commID = row["CommID"].ToString();
            string iid = row["IID"].ToString();
            string result = row["Review"].ToString();

            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = @"UPDATE Tb_HSPR_HousingInspDetail SET Review=@Result WHERE IID=@IID";
                con.Execute(sql, new { Result = result, IID = iid });
                return JSONHelper.FromString(true, "保存成功");
            }
        }
    }
}
