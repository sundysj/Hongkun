using App.Model;
using Business.PMS10.业主App.缴费.Model;
using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static Dapper.SqlMapper;


namespace Business
{
    public class PMSAppFees : PubInfo
    {
        public PMSAppFees()
        {
            base.Token = "20191205PMSAppFees";
        }

        public override void Operate(ref Transfer Trans)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            //防止未捕获异常出现
            try
            {
                switch (Trans.Command)
                {
                    case "GetArrearsFees":              // 获取欠费列表
                        Trans.Result = GetArrearsFees(Row);
                        break;
                    case "GetFeesHistoryList":          // 获取历史账单列表
                        Trans.Result = GetFeesHistoryList(Row);
                        break;
                    case "GetFeesStatistics":           // 获取费用统计信息
                        Trans.Result = GetFeesStatistics(Row);
                        break;
                    case "GetFeesPaidHistory":          // 获取实收记录
                        Trans.Result = GetFeesPaidHistory(Row);
                        break;
                    case "GetPrecPaidHistory":          // 获取预存记录
                        Trans.Result = GetPrecPaidHistory(Row);
                        break;
                    case "GetPaymentHistory":          // 获取历史账单列表（显示票据号）
                        Trans.Result = GetPaymentHistory(Row);
                        break;
                    case "GetPaymentHistoryDetail":    // 获取历史账单详情（显示电子票据）
                        Trans.Result = GetPaymentHistoryDetail(Row);
                        break;
                    default:
                        Trans.Result = new ApiResult(false, "未知错误").toJson();
                        break;
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace + Environment.NewLine + ex.Source);
                Trans.Result = new ApiResult(false, ex.Message + ex.StackTrace).toJson();
            }
        }

        private string GetPaymentHistoryDetail(DataRow row)
        {

            #region 获取参数并简单校验
            string CommID = string.Empty;
            if (!row.Table.Columns.Contains("CommID"))
            {
                return new ApiResult(false, "小区编码不能为空").toJson();
            }
            CommID = row["CommID"].ToString();
            Tb_Community community = GetCommunity(CommID);
            if (null == community)
            {
                return new ApiResult(false, "未查找到小区信息").toJson();
            }
            CommID = community.CommID;
            string ReceID = string.Empty;
            if (row.Table.Columns.Contains("OrderSN"))
            {
                ReceID = row["OrderSN"].ToString();
            }
            PubConstant.tw2bsConnectionString = Global_Fun.Tw2bsConnectionString(Global_Fun.GetNetType(community.DBServer));
            PubConstant.hmWyglConnectionString = GetConnectionStr(community);
            var chargeConnectionString = Global_Fun.BurstConnectionString(Convert.ToInt32(CommID), Global_Fun.BURST_TYPE_CHARGE);
            #endregion
            using (IDbConnection conn = new SqlConnection(chargeConnectionString))
            {
                dynamic Receipts = conn.QueryFirstOrDefault("SELECT BillType,BillsDate,BillsAmount,ChargeMode FROM view_HSPR_CustomerBillSign_Filter WHERE ISNULL(IsDelete,0) = 0 AND ReceID = @ReceID", new { ReceID });
                if (null == Receipts)
                {
                    return new ApiResult(false, "订单不存在").toJson();
                }
                int BillType = Convert.ToInt32(Receipts.BillType);
                string BillsDate = Convert.ToString(Receipts.BillsDate);
                decimal BillsAmount = Convert.ToDecimal(Receipts.BillsAmount);
                string ChargeMode = Convert.ToString(Receipts.ChargeMode);
                Dictionary<string, object> result = new Dictionary<string, object>();
                List<Dictionary<string, string>> FeesInfoList = new List<Dictionary<string, string>>();
                if (BillType == 1)
                {
                    List<dynamic> CostList = conn.Query("SELECT CostID,CostName,FeesID,FeesStateDate,FeesEndDate FROM view_HSPR_Fees_SearchFilter WHERE CommID = @CommID AND ISNULL(FeesID, 0) != 0 AND FeesID IN (SELECT FeesID FROM Tb_HSPR_FeesDetail WHERE ISNULL(IsDelete,0) = 0 AND ReceID = @ReceID UNION SELECT FeesID FROM Tb_HSPR_PreCostsDetail WHERE ISNULL(IsDelete,0) = 0 AND ReceID = @ReceID)", new { CommID, ReceID }).ToList();
                    if (null == CostList || CostList.Count <= 0)
                    {
                        return new ApiResult(false, "订单数据错误").toJson();
                    }
                    foreach (var CostGroup in CostList.GroupBy(item => Convert.ToString(item.CostName)))
                    {
                        if (null == CostGroup || CostGroup.Count() == 0)
                        {
                            continue;
                        }
                        #region 分组后,取费用最早的开始时间,和最晚的结束时间
                        DateTime FeesStateDate, FeesEndDate;
                        FeesStateDate = DateTime.MaxValue;
                        FeesEndDate = DateTime.MinValue;
                        string CostID = "";
                        string CostName = "";
                        List<string> FeesIDList = new List<string>();
                        foreach (var item in CostGroup)
                        {
                            FeesIDList.Add(Convert.ToString(item.FeesID));
                            if (string.IsNullOrEmpty(CostID))
                            {
                                CostID = Convert.ToString(item.CostID);
                            }
                            if (string.IsNullOrEmpty(CostName))
                            {
                                CostName = Convert.ToString(item.CostName);
                            }
                            if (DateTime.TryParse(Convert.ToString(item.FeesStateDate), out DateTime startTime))
                            {
                                if (FeesStateDate.CompareTo(startTime) > 0)
                                {
                                    FeesStateDate = startTime;
                                }
                            }
                            else
                            {
                                FeesStateDate = DateTime.Now;
                            }

                            if (DateTime.TryParse(Convert.ToString(item.FeesEndDate), out DateTime endTime))
                            {
                                if (FeesEndDate.CompareTo(endTime) < 0)
                                {
                                    FeesEndDate = endTime;
                                }
                            }
                            else
                            {
                                FeesEndDate = DateTime.Now;
                            }
                        }
                        #endregion
                        decimal Amount = conn.QueryFirstOrDefault<decimal>(@"SELECT ISNULL(SUM(ISNULL(Amount,0.00)),0.00) AS Amount FROM (
                                SELECT ISNULL(SUM(ISNULL(ISNULL(ChargeAmount,0.00) + ISNULL(LateFeeAmount,0.00),0.00)),0.00) AS Amount FROM Tb_HSPR_FeesDetail WHERE ISNULL(IsDelete,0) = 0 AND ReceID = @ReceID AND FeesID IN @FeesID
                                UNION
                                SELECT ISNULL(SUM(ISNULL(DueAmount,0.00)),0.00) AS Amount FROM Tb_HSPR_PreCostsDetail WHERE ISNULL(IsDelete,0) = 0 AND ReceID = @ReceID AND FeesID IN @FeesID
                            ) AS a", new { ReceID, FeesID = FeesIDList.ToArray() });
                        FeesInfoList.Add(new Dictionary<string, string>
                            {
                                { "CostID", CostID },
                                { "CostName", CostName },
                                { "Amount", Amount.ToString() },
                                { "CostArea", FeesStateDate.ToString("yyyy-MM") + "至" + FeesEndDate.ToString("yyyy-MM") }
                            });
                    }
                }
                if (BillType == 2)
                {
                    dynamic info = conn.QueryFirstOrDefault("SELECT CostID,CostNames,PrecMemo FROM Tb_HSPR_PreCostsDetail WHERE ISNULL(IsDelete,0) = 0 AND ReceID = @ReceID", new { ReceID }).ToList();
                    if (null != info)
                    {
                        string CostID = Convert.ToString(info.CostID);
                        string CostName = Convert.ToString(info.CostNames);
                        string PrecMemo = Convert.ToString(info.PrecMemo);
                        string PrecAmount = Convert.ToString(info.PrecAmount);
                        FeesInfoList.Add(new Dictionary<string, string>
                            {
                                { "CostID", CostID },
                                { "CostName", CostName },
                                { "Amount", PrecAmount },
                                { "CostArea", PrecMemo }
                            });
                    }
                }
                string ElectronicInvoice = string.Empty;
                if (conn.QueryFirstOrDefault<long>("SELECT isnull(object_id(N'Tb_HSPR_ElectronicInvoice',N'U'),0)") != 0)
                {
                    ElectronicInvoice = conn.QueryFirstOrDefault<string>("SELECT ImgUrl FROM Tb_HSPR_ElectronicInvoice WHERE CommID = @CommID AND ReceID = @ReceID", new { CommID, ReceID });
                }
                result.Add("Amount", BillsAmount);
                result.Add("Status", "交易成功");
                result.Add("PayType", ChargeMode);
                result.Add("CreateTime", BillsDate);
                result.Add("OrderSN", ReceID);
                result.Add("FeesDetail", FeesInfoList);
                result.Add("ElectronicInvoice", ElectronicInvoice);
                return new ApiResult(true, result).toJson();
            }
        }

        /// <summary>
        /// 获取历史账单列表（显示票据号）
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetPaymentHistory(DataRow row)
        {
            #region 获取参数并简单校验
            string CommID = string.Empty;
            if (!row.Table.Columns.Contains("CommID"))
            {
                return new ApiResult(false, "小区编码不能为空").toJson();
            }
            CommID = row["CommID"].ToString();
            Tb_Community community = GetCommunity(CommID);
            if (null == community)
            {
                return new ApiResult(false, "未查找到小区信息").toJson();
            }
            CommID = community.CommID;
            if (!row.Table.Columns.Contains("CustID") || !long.TryParse(row["CustID"].ToString(), out long CustID))
            {
                CustID = 0;
            }
            if (!row.Table.Columns.Contains("RoomID") || !long.TryParse(row["RoomID"].ToString(), out long RoomID))
            {
                RoomID = 0;
            }
            if (!row.Table.Columns.Contains("Page") || !int.TryParse(row["Page"].ToString(), out int Page))
            {
                Page = 1;
            }
            if (Page <= 0)
            {
                Page = 1;
            }
            if (!row.Table.Columns.Contains("Size") || !int.TryParse(row["Size"].ToString(), out int Size))
            {
                Size = 10;
            }
            if (Size <= 0)
            {
                Size = 10;
            }
            int Start = (Page - 1) * Size;
            int End = Page * Size;

            PubConstant.tw2bsConnectionString = Global_Fun.Tw2bsConnectionString(Global_Fun.GetNetType(community.DBServer));
            PubConstant.hmWyglConnectionString = GetConnectionStr(community);
            var chargeConnectionString = Global_Fun.BurstConnectionString(Convert.ToInt32(CommID), Global_Fun.BURST_TYPE_CHARGE);
            #endregion
            using (IDbConnection conn = new SqlConnection(chargeConnectionString))
            {
                List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();
                DateTime BeginDate = new DateTime(1970, 1, 1);
                #region 查询可查询账单开始时间设置
                try
                {
                    dynamic setting = conn.QueryFirstOrDefault("SELECT ISNULL(IsShow, 1) AS IsShow,BeginDate FROM Tb_HSPR_FeeBundleSettings WHERE ISNULL(IsDelete,0) = 0 AND CommID = @CommID", new { CommID });
                    if (null != setting)
                    {
                        if (Convert.ToInt32(setting.IsShow) == 0)
                        {
                            return new ApiResult(true, new { Data = resultList, Count = 0, Page = 0 }).toJson();
                        }
                        if (!DateTime.TryParse(Convert.ToString(setting.BeginDate), out BeginDate))
                        {
                            BeginDate = new DateTime(1970, 1, 1);
                        }
                    }
                }
                catch (Exception)
                {
                }
                #endregion
                conn.Execute("Proc_HSPR_CustomerBillSign_Cre", new { CommID, CustID, RoomID }, null, null, CommandType.StoredProcedure);
                int Count = conn.QueryFirstOrDefault<int>("SELECT COUNT(ReceID) FROM (SELECT * FROM view_HSPR_CustomerBillSign_Filter) AS a WHERE ISNULL(IsDelete,0) = 0 AND BillType IN (1,2) AND CommID = @CommID AND CustID = @CustID AND RoomID = @RoomID AND BillsDate > @BeginDate", new { CommID, CustID, RoomID, BeginDate });
                int PageRes = Count % Size > 0 ? (Count / Size) + 1 : Count / Size;
                int CountRes = Count;
                List<dynamic> list = conn.Query("SELECT ReceID,BillsDate,BillsSign,BillsAmount,ChargeMode,BillType FROM (SELECT *, ROW_NUMBER() OVER(ORDER BY BillsDate DESC) AS RowId FROM view_HSPR_CustomerBillSign_Filter WHERE ISNULL(IsDelete,0) = 0 AND BillType IN (1,2) AND CommID = @CommID AND CustID = @CustID AND RoomID = @RoomID AND BillsDate > @BeginDate) AS a WHERE RowId BETWEEN @Start AND @End", new { CommID, CustID, RoomID, Start, End, BeginDate }).ToList();
                if (null != list && list.Count > 0)
                {
                    foreach (dynamic item in list)
                    {
                        try
                        {
                            int BillType = Convert.ToInt32(item.BillType);
                            string ReceID = Convert.ToString(item.ReceID);
                            string BillsSign = Convert.ToString(item.BillsSign);
                            string BillsDate = Convert.ToString(item.BillsDate);
                            decimal BillsAmount = Convert.ToDecimal(item.BillsAmount);
                            string ChargeMode = Convert.ToString(item.ChargeMode);
                            if (BillType == 1)
                            {
                                #region 实收查实收表，不能查预存
                                // 查询费项名称和区间
                                List<string> FeesIDList = conn.Query<string>("SELECT FeesID FROM Tb_HSPR_FeesDetail WHERE ISNULL(IsDelete,0) = 0 AND ReceID = @ReceID UNION SELECT FeesID FROM Tb_HSPR_PreCostsDetail WHERE ISNULL(IsDelete,0) = 0 AND ReceID = @ReceID", new { ReceID }).ToList();
                                List<dynamic> CostList = conn.Query("SELECT CostName,FeesStateDate,FeesEndDate FROM view_HSPR_Fees_SearchFilter WHERE CommID = @CommID AND ISNULL(FeesID, 0) != 0 AND FeesID IN @FeesIDList", new { CommID, FeesIDList }).ToList();
                                if (null == CostList || CostList.Count <= 0)
                                {
                                    continue;
                                }
                                DateTime FeesStateDate, FeesEndDate;
                                FeesStateDate = DateTime.MaxValue;
                                FeesEndDate = DateTime.MinValue;
                                HashSet<string> CostNameList = new HashSet<string>();
                                foreach (dynamic costinfo in CostList)
                                {
                                    CostNameList.Add(Convert.ToString(costinfo.CostName));

                                    if (DateTime.TryParse(Convert.ToString(costinfo.FeesStateDate), out DateTime startTime))
                                    {
                                        if (FeesStateDate.CompareTo(startTime) > 0)
                                        {
                                            FeesStateDate = startTime;
                                        }
                                    }
                                    else
                                    {
                                        FeesStateDate = DateTime.Now;
                                    }
                                    if (DateTime.TryParse(Convert.ToString(costinfo.FeesEndDate), out DateTime endTime))
                                    {
                                        if (FeesEndDate.CompareTo(endTime) < 0)
                                        {
                                            FeesEndDate = endTime;
                                        }
                                    }
                                    else
                                    {
                                        FeesEndDate = DateTime.Now;
                                    }
                                }
                                resultList.Add(new Dictionary<string, object>
                                    {
                                        {"OrderSN", ReceID },
                                        {"PayChannel", ChargeMode },
                                        {"PayTime", BillsDate },
                                        {"Amount", BillsAmount },
                                        {"BillsSign",BillsSign },
                                        {"CostName", string.Join(",",CostNameList.ToArray()) },
                                        {"CostArea", FeesStateDate.ToString("yyyy.MM.dd") + "-" + FeesEndDate.ToString("yyyy.MM.dd") }
                                    });
                                #endregion
                            }
                            if (BillType == 2)
                            {
                                dynamic info = conn.QueryFirstOrDefault("SELECT CostNames,PrecMemo FROM Tb_HSPR_PreCostsDetail WHERE ISNULL(IsDelete,0) = 0 AND ReceID = @ReceID", new { ReceID }).ToList();
                                if (null == info)
                                {
                                    continue;
                                }
                                string CostName = Convert.ToString(info.CostNames);
                                string PrecMemo = Convert.ToString(info.PrecMemo);
                                // 预存查预存的费用信息
                                resultList.Add(new Dictionary<string, object>
                                    {
                                        {"OrderSN", ReceID },
                                        {"PayChannel", ChargeMode },
                                        {"PayTime", BillsDate },
                                        {"Amount", BillsAmount },
                                        {"BillsSign",BillsSign },
                                        {"CostName", CostName },
                                        {"CostArea", PrecMemo }
                                    });
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
                return new ApiResult(true, new { Data = resultList, Count = CountRes, Page = PageRes }).toJson();
            }
        }

        /// <summary>
        /// 获取费用统计信息
        /// </summary>
        private string GetFeesStatistics(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].AsString()))
            {
                return new ApiResult(false, "未指定欠费主体").toJson();
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房间编码不能为空");
            }

            var communityId = row["CommunityId"].AsString();
            var custId = AppGlobal.StrToLong(row["CustID"].AsString());
            var roomId = AppGlobal.StrToLong(row["RoomID"].AsString());

            var community = GetCommunity(communityId);
            if (community == null)
                return JSONHelper.FromString(false, "未查找到小区信息");

            var commId = AppGlobal.StrToInt(community.CommID);
            PubConstant.tw2bsConnectionString = Global_Fun.Tw2bsConnectionString(Global_Fun.GetNetType(community.DBServer));
            PubConstant.hmWyglConnectionString = GetConnectionStr(community);
            var chargeConnectionString = Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CHARGE);

            var shieldCost = "";

            // 查询需要屏蔽的费用
            using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT isnull(object_id(N'Tb_Control_AppCostItem',N'U'),0);";
                if (appConn.Query<long>(sql).FirstOrDefault() != 0)
                {
                    sql = @"SELECT CorpCostID FROM Tb_Control_AppCostItem WHERE CorpID=@CorpID AND CommunityID=@Id AND AllowShow=0;
                            SELECT CorpCostID FROM Tb_Control_AppCostItem WHERE CorpID=@CorpID AND isnull(CommunityID,'')='' AND AllowShow=0;";

                    var reader = appConn.QueryMultiple(sql, new { CorpID = community.CorpID, Id = community.Id });
                    var data1 = reader.Read<string>().FirstOrDefault();
                    var data2 = reader.Read<string>().FirstOrDefault();

                    var data = data1 ?? data2;

                    if (!string.IsNullOrEmpty(data))
                    {
                        using (var erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
                        {
                            sql = @"SELECT CostCode FROM Tb_HSPR_CorpCostItem WHERE CorpCostID IN (SELECT Value FROM SplitString(@Data,',',1))";

                            var costCodes = erpConn.Query<string>(sql, new { Data = data }).Select(obj => $"'{obj}'");

                            shieldCost = $@"AND CostCode NOT IN ({ string.Join(",", costCodes.ToArray()) })";
                        }
                    }
                }
            }

            using (var chargeConn = new SqlConnection(chargeConnectionString))
            {
                var sql = @"SELECT isnull(sum(isnull(a.PrecAmount,0.00)),0.00) AS PrecAmount  
                            FROM Tb_HSPR_PreCosts a 
                            WHERE a.CustID=@CustID AND a.RoomID=@RoomID AND a.PrecAmount>0 AND a.IsPrec=1;";

                // 预存余额
                var precAmount = chargeConn.Query<decimal>(sql, new { CustID = custId, RoomID = roomId }).FirstOrDefault();

                sql = $@"/* 欠费未缴清 */
                         SELECT isnull(sum(isnull(DebtsAmount,0) + isnull(LateFeeAmount,0)),0) AS DebtsAmount
                         FROM (
                                 SELECT DebtsAmount,CostCode,
                                        CASE WHEN isnull(x.DebtsLateAmount, 0)>0
                                            THEN dbo.funGetLateFeeDebts(x.CommID,x.FeesID,isnull(x.DebtsLateAmount, 0))
                                            ELSE 0.0 END AS LateFeeAmount
                                 FROM Tb_HSPR_Fees x
                                 LEFT JOIN { community.DBName }.dbo.Tb_HSPR_CostItem y ON x.CommID=y.CommID AND x.CostID=y.CostID
                                 WHERE x.CustID=@CustID AND x.RoomID=@RoomID {shieldCost} 
                                   AND isnull(x.IsCharge, 0)=0 AND isnull(x.IsBank, 0)=0 AND isnull(x.IsPrec, 0)=0 AND isnull(IsFreeze, 0)=0
                             )AS t
                         /* 仅剩违约金 */
                         /*
                         SELECT isnull(x.LateFeeAmount,0.00)
                         FROM view_HSPR_Fees_SearchFilter x 
                         WHERE x.CustID=@CustID AND x.RoomID=@RoomID {shieldCost} 
                         AND isnull(x.IsCharge,0)=1 AND isnull(x.IsBank,0)=0 AND isnull(x.IsPrec,0)=0 AND isnull(IsFreeze,0)=0 
                         AND isnull(x.LateFeeAmount,0.00)>0;
                         */";

                // 未缴费用，含违约金
                var reader = chargeConn.QueryMultiple(sql, new { CustID = custId, RoomID = roomId });
                var debtsAmount = reader.Read<decimal>().FirstOrDefault();
                //var lateFeeAmount = reader.Read<decimal>().FirstOrDefault();
                var lateFeeAmount = 0;

                return new ApiResult(true, new { PrecAmount = precAmount, DebtsAmount = debtsAmount + lateFeeAmount, BillingCycle = 1 }).toJson();
            }
        }

        /// <summary>
        /// 获取欠费列表
        /// </summary>
        private string GetArrearsFees(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].AsString()))
            {
                return new ApiResult(false, "用户信息错误").toJson();
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "请选择房屋所在项目").toJson();
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].AsString()))
            {
                return new ApiResult(false, "未指定欠费主体").toJson();
            }

            var userId = row["UserId"].AsString();
            var communityId = row["CommunityId"].AsString();
            var custId = AppGlobal.StrToLong(row["CustID"].AsString());
            var roomId = 0L;

            if (row.Table.Columns.Contains("RoomID") && !string.IsNullOrEmpty(row["RoomID"].AsString()))
            {
                roomId = AppGlobal.StrToLong(row["RoomID"].AsString());
            }

            var community = GetCommunity(communityId);
            if (community == null)
                return JSONHelper.FromString(false, "未查找到小区信息");

            var commId = AppGlobal.StrToInt(community.CommID);
            PubConstant.tw2bsConnectionString = Global_Fun.Tw2bsConnectionString(Global_Fun.GetNetType(community.DBServer));
            PubConstant.hmWyglConnectionString = GetConnectionStr(community);
            var chargeConnectionString = Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CHARGE);

            List<string> BindCostList = new List<string>();
            JObject PaymentCycleSetting = new JObject();
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT isnull(object_id(N'Tb_HSPR_PaymentBind',N'U'),0)";
                if (conn.QueryFirstOrDefault<long>(sql) != 0)
                {
                    BindCostList = conn.Query<string>("SELECT CostID FROM Tb_HSPR_PaymentBind WHERE CommID = @CommID AND ISNULL(IsDelete,0) = 0", new { CommID = commId }).ToList();
                }
                sql = @"SELECT isnull(object_id(N'Tb_HSPR_PaymentBindingDateModelSet',N'U'),0)";
                if (conn.QueryFirstOrDefault<long>(sql) != 0)
                {
                    // 需求9759，视图新增返回参数IsHis，用于判断是否需要缴纳历史欠费
                    var Setting = conn.QueryFirstOrDefault("SELECT CheckBoxID,MonthNum,IsHis FROM View_HSPR_PaymentBindingDateModelSet_Filter WHERE CommID = @CommID", new { CommID = commId });
                    // Type值：0=默认全选，1=任意选择，2=按月份数量一次性整月选择，3=一次性至少缴纳几个月费用
                    int Type = 0;
                    int MonthNum = 0;
                    if (null != Setting)
                    {
                        int CheckBoxID = 1;
                        if (Setting.CheckBoxID != null)
                        {
                            CheckBoxID = Convert.ToInt32(Setting.CheckBoxID);
                        }
                        if (Setting.MonthNum != null)
                        {
                            MonthNum = Convert.ToInt32(Setting.MonthNum);
                        }
                        switch (CheckBoxID)
                        {
                            case 1:
                                {
                                    Type = 0;
                                    MonthNum = 0;
                                }
                                break;
                            case 2:
                                {
                                    Type = 1;
                                    MonthNum = 0;
                                }
                                break;
                            case 4:
                                {
                                    Type = 2;
                                    MonthNum = 1;
                                }
                                break;
                            case 6:
                                {
                                    Type = 2;
                                    MonthNum = 3;
                                }
                                break;
                            case 8:
                                {
                                    Type = 2;
                                    MonthNum = 6;
                                }
                                break;
                            case 10:
                                {
                                    Type = 2;
                                    MonthNum = 12;
                                }
                                break;
                            case 12:
                                {
                                    Type = 3;
                                }
                                break;
                            default:
                                {
                                    Type = 0;
                                    MonthNum = 0;
                                }
                                break;
                        }
                    }
                    PaymentCycleSetting.Add("Type", Type);
                    PaymentCycleSetting.Add("MonthNum", MonthNum);
                    PaymentCycleSetting.Add("IsHis", Setting != null ? Setting.IsHis : 1);
                }
            }
            var shieldCost = "";
            // 查询需要屏蔽的费用
            using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT isnull(object_id(N'Tb_Control_AppCostItem',N'U'),0)";
                // 优先取ERP
                if (string.IsNullOrEmpty(shieldCost) && appConn.Query<long>(sql).FirstOrDefault() != 0)
                {
                    sql = @"SELECT CorpCostID FROM Tb_Control_AppCostItem WHERE CorpID=@CorpID AND CommunityID=@Id AND AllowShow=0;
                            SELECT CorpCostID FROM Tb_Control_AppCostItem WHERE CorpID=@CorpID AND isnull(CommunityID,'')='' AND AllowShow=0;";

                    var reader = appConn.QueryMultiple(sql, new { CorpID = community.CorpID, Id = community.Id });
                    var data1 = reader.Read<string>().FirstOrDefault();
                    var data2 = reader.Read<string>().FirstOrDefault();

                    var data = data1 ?? data2;

                    if (!string.IsNullOrEmpty(data))
                    {
                        using (var erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
                        {
                            sql = $@"SELECT CostCode FROM Tb_HSPR_CorpCostItem 
                                    WHERE CorpCostID IN
                                    (
                                        SELECT Value FROM SplitString('{data}',',',1)
                                    )";

                            var costCodes = erpConn.Query<string>(sql).Select(obj => $"'{obj}'");

                            shieldCost = $@"AND CostCode NOT IN ({ string.Join(",", costCodes.ToArray()) })";
                        }
                    }
                }
            }

            using (var chargeConn = new SqlConnection(chargeConnectionString))
            {
                var list = new List<PMSAppFeesCostSimpleModel>();

                var sql = $@"/* 欠费未缴清 */
                            SELECT FeesID,x.CostID,CostName,isnull(SysCostSign,'Unknown') AS SysCostSign,
                                isnull(DueAmount,0.00) AS DueAmount,
                                isnull(DebtsAmount,0.00) AS DebtsAmount,
                                CASE WHEN isnull(x.DebtsLateAmount, 0)>0
                                    THEN dbo.funGetLateFeeDebts(x.CommID,x.FeesID,isnull(x.DebtsLateAmount, 0))
                                ELSE 0.0 END AS LateFeeAmount,
                                convert(varchar(10),isnull(FeesDueDate,getdate()),120) AS FeesDueDate
                            FROM Tb_HSPR_Fees x
                            LEFT JOIN { community.DBName }.dbo.Tb_HSPR_CostItem y ON x.CostID=y.CostID 
                            WHERE x.CustID=@CustID AND x.RoomID=@RoomID 
                            AND isnull(x.IsCharge,0)=0 AND isnull(x.IsBank,0)=0 AND isnull(x.IsPrec,0)=0 AND isnull(IsFreeze,0)=0 
                            { shieldCost } 
                            ORDER BY SysCostSign ASC,FeesDueDate DESC;

                            /* 仅剩违约金 */
                            /*
                            SELECT FeesID,CostID,CostName,isnull(SysCostSign,'Unknown') AS SysCostSign,
                                isnull(DueAmount,0.00) AS DueAmount,
                                isnull(DebtsAmount,0.00) AS DebtsAmount,
                                isnull(LateFeeAmount,0.00) AS LateFeeAmount,
                                convert(varchar(10),isnull(FeesDueDate,getdate()),120) AS FeesDueDate
                            FROM view_HSPR_Fees_SearchFilter x
                            WHERE x.CustID=@CustID AND x.RoomID=@RoomID 
                            AND isnull(x.IsCharge,0)=1 AND isnull(x.IsBank,0)=0 AND isnull(x.IsPrec,0)=0 AND isnull(IsFreeze,0)=0 
                            AND isnull(x.LateFeeAmount,0.0)>0 
                            { shieldCost } 
                            ORDER BY SysCostSign ASC,FeesDueDate DESC;
                            */";

                var reader = chargeConn.QueryMultiple(sql, new { CommID = community.CommID, RoomID = roomId, CustID = custId });
                var data1 = reader.Read();
                //var data2 = reader.Read();

                var data = data1.ToList();
                for (int i = 0; i < data.Count; i++)
                {
                    var feesInfo = data[i];

                    var tmp = list.Find(obj => obj.CostID == feesInfo.CostID);
                    if (tmp == null)
                    {
                        tmp = new PMSAppFeesCostSimpleModel()
                        {
                            CostID = feesInfo.CostID,
                            CostName = feesInfo.CostName,
                            SysCostSign = feesInfo.SysCostSign
                        };
                        tmp.Fees = new List<PMSAppFeesSimpleModel>();
                        list.Add(tmp);

                        if (tmp.SysCostSign == "Unknown")
                        {
                            tmp.SysCostSign = null;
                        }
                    }

                    var model = new PMSAppFeesSimpleModel()
                    {
                        FeesID = feesInfo.FeesID,
                        DueAmount = feesInfo.DueAmount,
                        DebtsAmount = feesInfo.DebtsAmount,
                        LateFeeAmount = feesInfo.LateFeeAmount,
                        FeesDueDate = feesInfo.FeesDueDate
                    };

                    tmp.TotalDueAmount += model.DueAmount;
                    tmp.TotalDebtsAmount += model.DebtsAmount;
                    tmp.TotalLateFeeAmount += model.LateFeeAmount;

                    if (feesInfo.SysCostSign == "B0001")
                    {
                        tmp.Expanded = 1;
                    }

                    tmp.Fees.Add(model);
                }

                using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
                {
                    // 读取积分信息
                    sql = @"SELECT * FROM Tb_Control_AppPoint WHERE CommunityID=@CommunityId AND isnull(CommunityID,'')<>'' AND IsEnable=1
                            UNION ALL 
                            SELECT * FROM Tb_Control_AppPoint WHERE CorpID=@CorpId AND isnull(CommunityID,'')='' AND IsEnable=1";

                    var controlInfo = appConn.Query<Tb_Control_AppPoint>(sql, new { CommunityId = communityId, CorpId = community.CorpID }).FirstOrDefault();
                    if (controlInfo == null || controlInfo.IsEnable == false)
                    {
                        controlInfo = Tb_Control_AppPoint.DefaultControl;
                    }

                    // 用户积分余额
                    sql = @"SELECT PointBalance FROM Tb_App_UserPoint WHERE UserID=@UserID";
                    var balance = appConn.Query<int>(sql, new { UserID = userId }).FirstOrDefault();

                    var costSign = new List<string>();
                    // 允许抵用物业费
                    if (controlInfo.AllowDeductionPropertyFees)
                    {
                        costSign.Add("B0001");
                    }
                    // 允许抵用车位费
                    if (controlInfo.AllowDeductionParkingFees)
                    {
                        costSign.Add("B0002");
                    }
                    return new ApiResult(true, new
                    {
                        ArrearsFees = list,
                        BindCostList,
                        PaymentCycleSetting,
                        Points = new
                        {
                            UserPointBalance = balance,
                            AllowDeductionPropertyFees = controlInfo.AllowDeductionPropertyFees,
                            AllowDeductionParkingFees = controlInfo.AllowDeductionParkingFees,
                            AllowDeductionOtherPropertyFees = controlInfo.AllowDeductionOtherPropertyFees,
                            PointExchangeRatio = controlInfo.PointExchangeRatio,
                            SysCostSign = costSign
                        },

                    }).toJson();
                }
            }
        }


        /// <summary>
        /// 获取历史账单列表
        /// </summary>
        private string GetFeesHistoryList(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].AsString()))
            {
                return new ApiResult(false, "用户信息错误").toJson();
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "请选择房屋所在项目").toJson();
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].AsString()))
            {
                return new ApiResult(false, "未指定欠费主体").toJson();
            }

            var userId = row["UserId"].AsString();
            var communityId = row["CommunityId"].AsString();
            var custId = AppGlobal.StrToLong(row["CustID"].AsString());
            var roomId = 0L;

            if (row.Table.Columns.Contains("RoomID") && !string.IsNullOrEmpty(row["RoomID"].AsString()))
            {
                roomId = AppGlobal.StrToLong(row["RoomID"].AsString());
            }

            var community = GetCommunity(communityId);
            if (community == null)
                return JSONHelper.FromString(false, "未查找到小区信息");

            var commId = AppGlobal.StrToInt(community.CommID);
            PubConstant.tw2bsConnectionString = Global_Fun.Tw2bsConnectionString(Global_Fun.GetNetType(community.DBServer));
            PubConstant.hmWyglConnectionString = GetConnectionStr(community);
            var chargeConnectionString = Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CHARGE);

            var shieldCost = "";

            // 查询需要屏蔽的费用
            using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT isnull(object_id(N'Tb_Control_AppCostItem',N'U'),0)";
                if (appConn.Query<long>(sql).FirstOrDefault() != 0)
                {
                    sql = @"SELECT CorpCostID FROM Tb_Control_AppCostItem WHERE CorpID=@CorpID AND CommunityID=@Id AND AllowShow=0;
                            SELECT CorpCostID FROM Tb_Control_AppCostItem WHERE CorpID=@CorpID AND isnull(CommunityID,'')='' AND AllowShow=0;";

                    var reader = appConn.QueryMultiple(sql, new { CorpID = community.CorpID, Id = community.Id });
                    var data1 = reader.Read<string>().FirstOrDefault();
                    var data2 = reader.Read<string>().FirstOrDefault();

                    var data = data1 ?? data2;

                    if (!string.IsNullOrEmpty(data))
                    {
                        using (var erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
                        {
                            sql = $@"SELECT CostCode FROM Tb_HSPR_CorpCostItem 
                                    WHERE CorpCostID IN
                                    (
                                        SELECT Value FROM SplitString('{data}',',',1)
                                    )";

                            var costCodes = erpConn.Query<string>(sql).Select(obj => $"'{obj}'");

                            shieldCost = $@"AND CostCode NOT IN ({ string.Join(",", costCodes.ToArray()) })";
                        }
                    }
                }
            }

            using (var chargeConn = new SqlConnection(chargeConnectionString))
            {
                var list = new List<PMSAppFeesCostSimpleModel>();

                var sql = $@"/* 欠费已缴清 */
                            SELECT FeesID,x.CostID,CostName,
                                isnull(PaidAmount,0.00) AS PaidAmount,
                                isnull(PrecAmount, 0.00) AS PrecAmount,
                                isnull(SysCostSign,'Unknown') AS SysCostSign,
                                isnull(DueAmount,0.00) AS DueAmount,
                                isnull(DebtsAmount,0.00) AS DebtsAmount,
                                CASE WHEN isnull(x.DebtsLateAmount, 0)>0
                                    THEN dbo.funGetLateFeeDebts(x.CommID,x.FeesID,isnull(x.DebtsLateAmount,0))
                                ELSE 0.0 END AS LateFeeAmount,
                                convert(varchar(10),isnull(FeesDueDate,getdate()),120) AS FeesDueDate
                            FROM Tb_HSPR_Fees x
                            LEFT JOIN { community.DBName }.dbo.Tb_HSPR_CostItem y ON x.CostID=y.CostID 
                            WHERE /*x.CustID=@CustID AND*/ x.RoomID=@RoomID 
                            AND (isnull(x.IsCharge,0)=1 OR isnull(x.IsPrec,0)=1)
                            { shieldCost } 
                            ORDER BY SysCostSign ASC,FeesDueDate DESC;";

                var data = chargeConn.Query(sql, new { CommID = community.CommID, RoomID = roomId/*, CustID = custId*/ });

                for (int i = 0; i < data.Count(); i++)
                {
                    var feesInfo = data.ToList()[i];

                    var tmp = list.Find(obj => obj.CostID == feesInfo.CostID);
                    if (tmp == null)
                    {
                        tmp = new PMSAppFeesCostSimpleModel()
                        {
                            CostID = feesInfo.CostID,
                            CostName = feesInfo.CostName,
                            SysCostSign = feesInfo.SysCostSign
                        };
                        tmp.Fees = new List<PMSAppFeesSimpleModel>();
                        list.Add(tmp);

                        if (tmp.SysCostSign == "Unknown")
                        {
                            tmp.SysCostSign = null;
                        }
                    }

                    var model = new PMSAppFeesSimpleModel()
                    {
                        FeesID = feesInfo.FeesID,
                        DueAmount = feesInfo.DueAmount,
                        DebtsAmount = feesInfo.DebtsAmount,
                        LateFeeAmount = feesInfo.LateFeeAmount,
                        FeesDueDate = feesInfo.FeesDueDate,
                        PaidAmount = feesInfo.PaidAmount,
                        PrecAmount = feesInfo.PrecAmount
                    };

                    tmp.TotalDueAmount += model.DueAmount;
                    tmp.TotalDebtsAmount += model.DebtsAmount;
                    tmp.TotalLateFeeAmount += model.LateFeeAmount;
                    tmp.TotalPaidAmount += model.PaidAmount;
                    tmp.TotalPrecAmount += model.PrecAmount;

                    if (feesInfo.SysCostSign == "B0001")
                    {
                        tmp.Expanded = 1;
                    }

                    tmp.Fees.Add(model);
                }

                return new ApiResult(true, new
                {
                    ArrearsFees = list
                }).toJson();
            }
        }


        /// <summary>
        /// 获取实收记录
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetFeesPaidHistory(DataRow row)
        {
            return "";
        }

        /// <summary>
        /// 获取预存记录
        /// </summary>
        private string GetPrecPaidHistory(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "请选择房屋所在项目").toJson();
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].AsString()))
            {
                return new ApiResult(false, "未指定欠费主体").toJson();
            }

            var communityId = row["CommunityId"].AsString();
            var custId = AppGlobal.StrToLong(row["CustID"].AsString());
            var roomId = 0L;

            if (row.Table.Columns.Contains("RoomID") && !string.IsNullOrEmpty(row["RoomID"].AsString()))
            {
                roomId = AppGlobal.StrToLong(row["RoomID"].AsString());
            }

            var pageSize = 10;
            var pageIndex = 1;
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].AsString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].AsString());
                pageIndex = (pageIndex <= 0 ? 1 : pageIndex);
            }

            var community = GetCommunity(communityId);
            if (community == null)
                return JSONHelper.FromString(false, "未查找到小区信息");

            var commId = AppGlobal.StrToInt(community.CommID);
            PubConstant.tw2bsConnectionString = Global_Fun.Tw2bsConnectionString(Global_Fun.GetNetType(community.DBServer));
            PubConstant.hmWyglConnectionString = GetConnectionStr(community);
            var chargeConnectionString = Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CHARGE);

            using (var chargeConn = new SqlConnection(chargeConnectionString))
            {
                var sql = $@"SELECT * FROM
                            (
                                SELECT a.BillsDate,a.PrecAmount,
                                    stuff((
                                            SELECT ',' + CostName
                                            FROM Tb_HSPR_PreCostsDetail x
                                                    LEFT JOIN {community.DBName}.dbo.Tb_HSPR_CostItem y ON x.CostID=y.CostID
                                            WHERE x.ReceID=a.ReceID
                                            AND isnull(x.IsDelete,0) = 0
                                            FOR XML PATH ('')
                                        ), 1, 1, '') AS CostNames,
                                    row_number() OVER (ORDER BY a.BillsDate DESC) AS RID
                                FROM Tb_HSPR_PreCostsReceipts a
                                WHERE a.CustID=@CustID AND a.RoomID=@RoomID
                            ) AS t
                            WHERE t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize);";

                var data = chargeConn.Query(sql, new { CustID = custId, RoomID = roomId, PageSize = pageSize, PageIndex = pageIndex });

                return new ApiResult(true, data).toJson();
            }
        }


        /// <summary>
        /// 获取预存费项列表
        /// </summary>
        private string GetPrecCostList(DataRow row)
        {
            return "";
        }
    }
}
