using App.Model;
using Com.Alipay;
using Dapper;
using log4net;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.OL;
using MobileSoft.Model.Unified;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WxPayAPI;
namespace Business
{
    public class Alipay_lf : PubInfo
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Alipay));
        /// <summary>
        /// openid用于调用统一下单接口
        /// </summary>
        public string openid { get; set; }

        public WxPayData unifiedOrderResult { get; set; }
        public Config c { get; set; }
        public Alipay_lf() //获取小区、项目信息
        {
            base.Token = "20181228Alipay_lf";
            c = new Config();
            c.notify_url = Global_Fun.AppWebSettings("AliPay_Notify_Url").ToString();
        }
        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = "false:";

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                //生成订单
                case "GenerateOrder":
                    Trans.Result = GenerateOrder(Row);
                    break;
                //订单下账
                case "ReceProperyOrder":
                    Trans.Result = ReceProperyOrder(0, Row["CommunityId"].ToString(), Row["OrderId"].ToString(), null);
                    break;
                //刷新订单
                case "RefreshOrder":
                    Trans.Result = RefreshOrder(Row["CommunityId"].ToString(), Row["OrderId"].ToString());
                    break;
                //取消订单
                case "CancelOrder":
                    Trans.Result = CancelOrder(Row["CommunityId"].ToString(), Row["OrderId"].ToString());
                    break;
                //更新订单状态
                case "UpdateProperyOrder":
                    Trans.Result = UpdateProperyOrder(Row["CommunityId"].ToString(), Row["OrderId"].ToString(), Row["respCode"].ToString(), Row["respMsg"].ToString());
                    break;
                //查询银行订单状态
                case "SearchBankOrder":
                    Trans.Result = SearchBankOrder(Row["CommunityId"].ToString(), Row["OrderId"].ToString());
                    break;
                default:
                    break;
            }
        }

        public static void Log(string Info)
        {
            log.Info(Info);
        }

        public string RefreshOrder(string CommunityId, string out_trade_no)
        {
            string ConnStr = GetConnection(CommunityId);
            string BankResult = SearchBankOrder(CommunityId, out_trade_no);
            IDbConnection conn = new SqlConnection(GetConnection(CommunityId));
            string query = "SELECT * FROM Tb_OL_AlipayOrder WHERE out_trade_no=@out_trade_no";
            Tb_OL_AlipayOrder T_Order = conn.Query<Tb_OL_AlipayOrder>(query, new { out_trade_no = out_trade_no }).SingleOrDefault();

            if (BankResult == "TRADE_SUCCESS")
            {
                //更新订单状态
                UpdateProperyOrder(T_Order.CommunityId.ToString(), T_Order.out_trade_no.ToString(), BankResult, "TRADE_SUCCESS");
                if (T_Order.IsSucc.ToString() != "1")
                {
                    //下账
                    ReceProperyOrder(0, T_Order.CommunityId.ToString(), T_Order.out_trade_no.ToString(), null);
                    return JSONHelper.FromString(true, "订单状态已更新并下账成功，请刷新订单");
                }
                else
                {
                    return JSONHelper.FromString(true, "订单状态已更新,请刷新订单");
                }
            }
            else
            {
                //更新订单状态
                UpdateProperyOrder(T_Order.CommunityId.ToString(), T_Order.out_trade_no.ToString(), BankResult, "");
            }

            return "";
        }

        public string CancelOrder(string CommunityId, string out_trade_no)
        {
            try
            {
                string ConnStr = GetConnection(CommunityId);

                string BankResult = SearchBankOrder(CommunityId, out_trade_no);

                if (BankResult == "TRADE_SUCCESS")
                {
                    return JSONHelper.FromString(false, "银行已交易成功,不能取消");
                }

                IDbConnection conn = new SqlConnection(GetConnection(CommunityId));
                string query = "SELECT * FROM Tb_OL_AlipayOrder WHERE out_trade_no=@out_trade_no";
                Tb_OL_AlipayOrder T_Order = conn.Query<Tb_OL_AlipayOrder>(query, new { out_trade_no = out_trade_no }).SingleOrDefault();

                if (T_Order.trade_status.Trim().ToString() == "TRADE_SUCCESS")
                {
                    return JSONHelper.FromString(false, "物业账单银行已付款");
                }
                if (T_Order.IsSucc.ToString() == "1")
                {
                    return JSONHelper.FromString(false, "物业账单已下账");
                }
                SqlParameter[] parameters = {
                    new SqlParameter("@OrderId", SqlDbType.VarChar)
                };
                parameters[0].Value = out_trade_no;
                new DbHelperSQLP(ConnStr).RunProcedure("Proc_OL_AlipayCancelOrder", parameters);
                return JSONHelper.FromString(true, "取消订单成功");
            }
            catch (Exception E)
            {
                return JSONHelper.FromString(false, E.Message.ToString());
            }

        }

        public new static string GetConnection(string CommunityId)
        {
            IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString);
            string query = "SELECT * FROM Tb_Community WHERE Id=@id OR CommID=@id";
            var T = conn.Query<Tb_Community>(query, new { id = CommunityId }).ToList();
            if (T.Count > 0)
            {
                return UnionUtil.GetConnectionString(T[0]).ToString();
            }
            return "";
        }

        public string SearchBankOrder(string CommunityId, string out_trade_no)
        {
            string Result = "";

            bool IsConfig = GenerateConfig(CommunityId);

            IDbConnection conn = new SqlConnection(GetConnection(CommunityId));
            string query = "SELECT * FROM Tb_OL_AlipayOrder WHERE out_trade_no=@out_trade_no";
            Tb_OL_AlipayOrder T_Order = conn.Query<Tb_OL_AlipayOrder>(query, new { out_trade_no = out_trade_no }).SingleOrDefault();

            if (T_Order == null)
            {
                Result = "未找到该物业订单";
                return Result;
            }

            //查找支付宝是否存在该订单

            return Result;
        }

        /// <summary>
        /// 更新物业订单状态
        /// </summary>
        /// <param name="CommunityId"></param>
        /// <param name="OrderId"></param>
        /// <param name="respCode"></param>
        /// <param name="respmsg"></param>
        public static string UpdateProperyOrder(string CommunityId, string out_trade_no, string trade_status, string trade_msg)
        {
            IDbConnection Conn = new SqlConnection(GetConnection(CommunityId));
            string Query = "UPDATE Tb_OL_AlipayOrder SET trade_status=@trade_status,trade_msg=@trade_msg  WHERE out_trade_no = @out_trade_no ";
            Conn.Execute(Query, new { trade_status = trade_status, trade_msg = trade_msg, out_trade_no = out_trade_no });
            return JSONHelper.FromString(true, "1");
        }

        /// <summary>
        /// 物业订单收款
        /// </summary>
        /// <param name="CommunityId"></param>
        /// <param name="OrderId"></param>
        /// <param name="respCode"></param>
        /// <param name="respmsg"></param>
        /// <returns></returns>
        public static string ReceProperyOrder(decimal total_amount, string CommunityId, string out_trade_no, string pointUseHistoryID)
        {
            try
            {
                PubConstant.hmWyglConnectionString = GetConnection(CommunityId);

                using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    var T_Order = conn.Query<Tb_OL_AlipayOrder>(@"SELECT * FROM Tb_OL_AlipayOrder WHERE out_trade_no=@out_trade_no",
                        new { out_trade_no = out_trade_no }).SingleOrDefault();

                    if (T_Order.IsSucc.ToString() == "1")
                    {
                        return "物业订单已下账";
                    }
                }

                ReceFees(total_amount, CommunityId, out_trade_no, pointUseHistoryID);
                return "success";
            }
            catch (Exception E)
            {
                return E.Message.ToString();
            }
        }

        public static string SetNotifyResult(string State, string Msg)
        {
            WxPayData res = new WxPayData();
            res.SetValue("return_code", State);
            res.SetValue("return_msg", Msg);
            return res.ToXml();
        }

        /// <summary>
        /// 收款
        /// </summary>
        /// <param name="CommunityId"></param>
        /// <param name="CustId"></param>
        /// <param name="FeesIds"></param>
        /// <returns></returns>
        public static void ReceFees(decimal total_amount, string CommunityId, string OrderId, string pointUseHistoryID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                 new SqlParameter("@CommunityId", SqlDbType.VarChar){ Value = CommunityId },
                 new SqlParameter("@OrderId", SqlDbType.VarChar){ Value = OrderId}
            };

            IDbTransaction trans = null;

            // 使用了积分
            if (!string.IsNullOrEmpty(pointUseHistoryID))
            {
                using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    trans = conn.BeginTransaction();

                    try
                    {
                        // 获取积分抵用金额
                        decimal pointDeductionAmount = conn.Query<decimal>("SELECT DeductionAmount FROM Tb_App_Point_UseHistory WHERE IID=@IID",
                            new { IID = pointUseHistoryID }, trans).FirstOrDefault();

                        parameters.Add(new SqlParameter("@UsePoint", SqlDbType.Int) { Value = 1 });
                        parameters.Add(new SqlParameter("@TotalAmount", SqlDbType.Decimal) { Value = total_amount });
                        parameters.Add(new SqlParameter("@PointDeductionAmount", SqlDbType.Decimal) { Value = pointDeductionAmount });

                        new DbHelperSQLP(PubConstant.hmWyglConnectionString).RunProcedure("Proc_OL_AliPayReceFees", parameters.ToArray());

                        conn.Execute(@"UPDATE Tb_App_Point_UseHistory SET IsEffect=1 WHERE IID=@IID", new { IID = pointUseHistoryID }, trans);

                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                    }
                }
            }
            else
            {
                new DbHelperSQLP(PubConstant.hmWyglConnectionString).RunProcedure("Proc_OL_AliPayReceFees", parameters.ToArray());
            }

        }

        //查询订单
        public string SearchOrder()
        {
            return "";
        }

        /// <summary>
        /// 生成银行及物业订单
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string GenerateOrder(DataRow Row)
        {
            bool IsBankOk = false;
            bool IsPropertyOk = false;
            string PropertyOrderId = "";
            string prepay_str = "";

            string CommunityId = Row["CommunityId"].ToString();
            string FeesIds = Row["FeesIds"].ToString();
            string txnTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string CustID = Row["CustID"].ToString();

            string UserId = null;
            if (Row.Table.Columns.Contains("UserID") && !string.IsNullOrEmpty(Row["UserID"].ToString()))
            {
                UserId = Row["UserID"].ToString();
            }

            int UsePoints = 0;
            if (Row.Table.Columns.Contains("UsePoints") && !string.IsNullOrEmpty(Row["UsePoints"].ToString()))
            {
                UsePoints = AppGlobal.StrToInt(Row["UsePoints"].ToString());
            }

            //增加FeesIds重复验证
            if (FeesIds == "")
            {
                return JSONHelper.FromString(false, "未选择任何费用");
            }
            string[] feesArray = FeesIds.Split(',').Distinct().ToArray();
            FeesIds = string.Join(",", feesArray.ToArray());

            bool IsConfig = GenerateConfig(CommunityId);
            if (IsConfig == false)
            {
                return JSONHelper.FromString(false, "该小区不支持支付宝支付");
            }

            PubConstant.hmWyglConnectionString = GetConnection(CommunityId);

            using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                if (appConn.State == ConnectionState.Closed)
                {
                    appConn.Open();
                }

                var appTrans = appConn.BeginTransaction();

                // 检查数据
                string useHistoryID = null;

                decimal Amount = 0.0m;                      // 缴费总额
                decimal propertyAmount = 0.0m;              // 物业费缴费总额
                decimal parkingAmount = 0.0m;               // 车位费缴费总额

                decimal deductionAmount = 0.0m;             // 积分可抵扣金额
                decimal propertyMaxDiscountsAmount = 0.0m;  // 物管费最大可抵用金额
                decimal parkingMaxDiscountsAmount = 0.0m;   // 车位费最大可抵用金额

                int pointBalance = 0;                       // 积分余额
                var deductionObject = new List<string>();   // 积分可抵扣对象

                #region 获取积分抵扣规则
                // 要使用的积分是否大于用户积分余额
                pointBalance = appConn.Query<int>("SELECT PointBalance FROM Tb_App_UserPoint WHERE UserID=@UserID", new { UserID = UserId }, appTrans).FirstOrDefault();
                if (pointBalance < UsePoints)
                {
                    return new ApiResult(false, "积分余额不足").toJson();
                }

                // 企业编号
                short corpId = appConn.Query<short>("SELECT CorpID FROM Tb_Community WHERE Id=@CommunityId",
                    new { CommunityId = CommunityId }, appTrans).FirstOrDefault();

                // 积分权限控制
                var controlInfo = appConn.Query<Tb_Control_AppPoint>(@"SELECT * FROM Tb_Control_AppPoint WHERE CorpID=@CorpID AND IsEnable=1 
                                                                            AND (CommunityID=@CommunityId OR CommunityID IS NULL) ORDER BY CommunityID DESC",
                                                                    new { CorpID = corpId, CommunityId = CommunityId }, appTrans).FirstOrDefault();

                if (controlInfo == null || controlInfo.IsEnable == false)
                {
                    controlInfo = Tb_Control_AppPoint.DefaultControl;
                    controlInfo.CommunityID = Guid.Empty.ToString();
                }

                // 允许抵用物业费
                if (controlInfo.AllowDeductionPropertyFees)
                {
                    deductionObject.Add($@"'{AppPointUsableObjectConverter.GetKey(AppPointUsableObject.PropertyFee)}'");
                }
                // 允许抵用车位费
                if (controlInfo.AllowDeductionParkingFees)
                {
                    deductionObject.Add($@"'{AppPointUsableObjectConverter.GetKey(AppPointUsableObject.ParkingFee)}'");
                }
                #endregion  

                #region 获取缴费列表，读取欠费总额
                using (IDbConnection erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    try
                    {
                        IEnumerable<dynamic> arrearsList = erpConn.Query($@"SELECT * FROM view_HSPR_Fees_Filter WHERE FeesID IN({FeesIds})");

                        // 允许抵用物业费
                        if (controlInfo.AllowDeductionPropertyFees)
                        {
                            foreach (dynamic item in arrearsList)
                            {
                                if (item.SysCostSign != null && item.SysCostSign.ToString() == "B0001")
                                {
                                    propertyAmount += item.DebtsAmount;
                                }
                            }
                        }

                        // 允许抵用车位费
                        if (controlInfo.AllowDeductionParkingFees)
                        {
                            foreach (dynamic item in arrearsList)
                            {
                                if (item.SysCostSign != null && item.SysCostSign.ToString() == "B0002")
                                {
                                    parkingAmount += item.DebtsAmount;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        appTrans.Rollback();
                        return new ApiResult(false, "验证欠费列表失败").toJson();
                    }
                }
                #endregion

                if (UsePoints > 0 && !string.IsNullOrEmpty(UserId))
                {
                    #region 判断积分是否足够
                    try
                    {
                        if (deductionObject.Count == 0)
                        {
                            return new ApiResult(false, "暂不支持积分抵用功能").toJson();
                        }

                        // 积分抵用规则
                        var ruleInfo = appConn.Query($@"SELECT IID,ConditionAmount,DiscountsAmount,DeductionObject,b.Remark AS SysCostSign,StartTime,EndTime 
                                        FROM Tb_App_Point_PropertyDeductionRule a LEFT JOIN Tb_Dictionary_Point_UsableObject b
                                        ON a.DeductionObject=b.[Key] 
                                        WHERE CommunityID=@CommunityId AND DeductionObject IN({string.Join(", ", deductionObject) }) 
                                        AND getdate() BETWEEN StartTime AND EndTime AND a.IsDelete=0 ORDER BY ConditionAmount,DiscountsAmount",
                                      new { CommunityId = CommunityId }, appTrans);

                        if (ruleInfo.Count() == 0)
                        {
                            return new ApiResult(false, "积分抵用规则未设置或已失效").toJson();
                        }

                        // 确定物管费可抵用金额
                        if (propertyAmount > 0)
                        {
                            string key = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.PropertyFee);
                            foreach (var item in ruleInfo)
                            {
                                if (item.DeductionObject == key && propertyAmount >= item.ConditionAmount)
                                {
                                    propertyMaxDiscountsAmount = item.DiscountsAmount;
                                }
                            }
                        }

                        // 确定车位费可抵用金额
                        if (parkingAmount > 0)
                        {
                            string key = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.ParkingFee);
                            foreach (var item in ruleInfo)
                            {
                                if (item.DeductionObject == key && parkingAmount >= item.ConditionAmount)
                                {
                                    parkingMaxDiscountsAmount = item.DiscountsAmount;
                                }
                            }
                        }

                        // 要使用积分抵用的金额
                        deductionAmount = UsePoints / (decimal)controlInfo.PointExchangeRatio;

                        // 积分数量不正常
                        if (deductionAmount > (parkingMaxDiscountsAmount + propertyMaxDiscountsAmount))
                        {
                            return new ApiResult(false, "使用积分超过可抵用最大金额").toJson();
                        }
                        else
                        {
                            decimal tmp = deductionAmount;

                            // 部分抵扣物业费
                            if (propertyMaxDiscountsAmount != 0)
                            {
                                if (tmp <= propertyMaxDiscountsAmount)
                                {
                                    propertyMaxDiscountsAmount = tmp;
                                    tmp = 0;
                                }
                                else
                                {
                                    tmp -= propertyMaxDiscountsAmount;
                                }
                            }

                            // 部分抵扣车位费
                            if (parkingMaxDiscountsAmount != 0 && tmp > 0)
                            {
                                if (tmp <= parkingMaxDiscountsAmount)
                                {
                                    parkingMaxDiscountsAmount = tmp;
                                    tmp = 0;
                                }
                                else
                                {
                                    tmp -= parkingMaxDiscountsAmount;
                                }
                            }

                            if (tmp != 0)
                            {
                                return new ApiResult(false, "积分抵用出错").toJson();
                            }
                        }

                        if (UsePoints > 0 && deductionAmount > 0)
                        {
                            useHistoryID = Guid.NewGuid().ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        appTrans.Rollback();
                        return new ApiResult(false, "积分验证失败" + ex.Message + ex.StackTrace).toJson();
                    }
                    #endregion
                }

                // 积分数量正常，生成订单
                try
                {
                    // 生成ERP账单
                    string PropertyResult = GeneratePropertyOrder(CommunityId, FeesIds, txnTime, CustID, ref IsPropertyOk, ref Amount, ref PropertyOrderId);

                    // ERP订单生成成功
                    if (IsPropertyOk == true)
                    {
                        // 应缴总金额
                        Amount = Amount - deductionAmount;

                        // 支付宝签名订单信息
                        string BankResult = GenerateBankOrder(CommunityId, UserId, feesArray.First(), PropertyOrderId, txnTime, Amount, ref IsBankOk, ref prepay_str);

                        if (IsBankOk == true)
                        {
                            // 计算预赠送积分
                            new AppPoint().CalcPresentedPointForPropertyFees(CommunityId, propertyAmount - propertyMaxDiscountsAmount, parkingAmount - parkingMaxDiscountsAmount, out int p1, out int p2);
                            prepay_str = prepay_str.Insert(prepay_str.Length - 1, ",\"presented_points\":" + (p1 + p2) + "");

                            if (!string.IsNullOrEmpty(useHistoryID))
                            {
                                prepay_str = prepay_str.Insert(prepay_str.Length - 1, ",\"deduction_amount\":" + deductionAmount + "");
                                prepay_str = prepay_str.Insert(prepay_str.Length - 1, ",\"point_use_history_id\":\"" + useHistoryID + "\"");

                                appConn.Execute($@"UPDATE Tb_App_UserPoint SET PointBalance=(PointBalance-@UsePoints) WHERE UserID=@UserID;
                                                    INSERT INTO Tb_App_Point_UseHistory(IID, UserID, UseWay, UsePoints, PointBalance, DeductionAmount, Remark)
                                                        VALUES(@UseHistoryID, @UserID, @UseWay, @UsePoints, @PointBalance, @DeductionAmount, @Remark);
                                                    INSERT INTO Tb_App_Point_Locked(UserID, UseHistoryID, LockedPoints) 
                                                        VALUES (@UserID, @UseHistoryID, @LockedPoints);",
                                                    new
                                                    {
                                                        UsePoints = UsePoints,
                                                        UserID = UserId,
                                                        UseHistoryID = useHistoryID,
                                                        UseWay = AppPointUseWayConverter.GetKey(AppPointUseWay.PropertyFeeDeduction),
                                                        PointBalance = pointBalance - UsePoints,
                                                        DeductionAmount = deductionAmount,
                                                        Remark = AppPointUseWayConverter.GetValue(AppPointUseWay.PropertyFeeDeduction),
                                                        LockedPoints = UsePoints
                                                    }, appTrans);

                                string usableObject = string.Join(",", deductionObject.Select(obj => AppPointUsableObjectConverter.GetValue(obj.Replace("'", ""))));

                                // 存储积分使用记录与订单关联关系
                                appConn.Execute(@"INSERT INTO Tb_App_Point_UseHistoryOrder(UseHistoryID, OrderID, Payment, UsableObject) 
                                                    VALUES(@UseHistoryID, @OrderID, '支付宝', @UsableObject)",
                                                    new
                                                    {
                                                        UseHistoryID = useHistoryID,
                                                        OrderID = PropertyOrderId,
                                                        UsableObject = usableObject
                                                    }, appTrans);
                            }

                            using (var erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
                            {
                                // 更新订单
                                erpConn.Execute(@"UPDATE Tb_OL_AlipayOrder SET prepay_str=@prepay_str WHERE out_trade_no = @out_trade_no ",
                                    new { prepay_str = prepay_str.ToString(), out_trade_no = PropertyOrderId });
                            }

                            appTrans?.Commit();
                            return JSONHelper.FromJsonString(true, prepay_str);
                        }
                        else
                        {
                            appTrans?.Rollback();
                            return JSONHelper.FromString(false, BankResult);
                        }
                    }
                    else
                    {
                        appTrans?.Rollback();
                        return JSONHelper.FromString(false, PropertyResult);
                    }
                }
                catch (Exception ex)
                {
                    appTrans?.Rollback();
                    return JSONHelper.FromString(false, ex.Message + ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 配置支付请求参数,根据当前小区获取
        /// </summary>
        /// <param name="Row"></param>
        public bool GenerateConfig(string CommunityId)
        {
            IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString.ToString());
            string query = "SELECT * FROM Tb_AlipayCertifiate WHERE CommunityId=@CommunityId OR CommunityId=(SELECT top 1 id FROM Tb_Community WHERE CommID=@CommunityId)";
            Tb_AlipayCertifiate T = conn.Query<Tb_AlipayCertifiate>(query, new { CommunityId = CommunityId }).SingleOrDefault();

            if (T == null)
            {
                return false;
            }
            else
            {
                c.partner = T.partner.ToString();
                c.seller_id = T.seller_id.ToString();
                c.private_key = T.private_key.ToString();
                c.alipay_public_key = T.alipay_public_key.ToString();
                return true;
            }
        }

        /// <summary>
        /// 配置支付请求参数,根据当前小区获取  【仅供验$_签时使用】
        /// </summary>
        /// <param name="CommunityId"></param>
        /// <param name="c">配置信息</param>
        /// <returns></returns>
        public bool GenerateConfig(string CommunityId, out Config c)
        {
            Config con = new Config();
            IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString.ToString());
            string query = "SELECT * FROM Tb_AlipayCertifiate WHERE CommunityId=@CommunityId OR CommunityId=(SELECT top 1 id FROM Tb_Community WHERE CommID=@CommunityId)";
            Tb_AlipayCertifiate T = conn.Query<Tb_AlipayCertifiate>(query, new { CommunityId = CommunityId }).SingleOrDefault();
            c = con;
            if (T == null)
            {
                return false;
            }
            else
            {
                c.partner = T.partner.ToString();
                c.seller_id = T.seller_id.ToString();
                c.private_key = T.private_key.ToString();
                c.alipay_public_key = T.alipay_public_key.ToString();
                return true;
            }
        }



        /// <summary>
        /// 生成银行订单
        /// </summary>
        /// <param name="merId">商户号</param>
        /// <param name="orderId">订单ID</param>
        /// <param name="txnTime">订单开始时间</param>
        /// <param name="total_fee">总金额分</param>
        /// <param name="R">是否成功生成订单</param>
        /// <returns></returns>
        public string GenerateBankOrder(string CommunityId, string userId, string feesId, string out_trade_no, string txnTime, decimal total_fee, ref bool R, ref string prepay_str)
        {
            Dictionary<string, string> sPara = new Dictionary<string, string>();

            #region 此部分内容,先后顺序不能改变,否则将导致签名错误(支付宝V1版本接口)
            // 顺序严格按照partner,seller_id,out_trade_no,subject,body,total_fee,notify_url,service,payment_type,_input_charset,it_b_pay进行拼接后才能进行签名
            // 顺序不能变,否则会导致签名问题,支付控件提示ALIN10070错误.
            // 支付宝配置指南,生成1024位的RSA密钥,配置支付宝开放平台中的mapi网关产品密钥(RSA1密钥)
            sPara.Add("partner", c.partner.ToString());
            sPara.Add("seller_id", c.seller_id.ToString());
            sPara.Add("out_trade_no", out_trade_no.ToString());

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString.ToString()))
            {
                dynamic commInfo = conn.Query("SELECT * FROM Tb_Community WHERE Id=@CommunityId OR CommID=@CommunityId",
                    new { CommunityId = CommunityId }).FirstOrDefault();

                if (commInfo != null)
                {
                    // 俊发
                    //if (commInfo.CorpID == 1985)
                    //{
                    //    sPara.Add("subject", commInfo.CommName.ToString() + "物管费用");
                    //}
                    //else
                    //{
                    using (var erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
                    {
                        dynamic feesInfo = erpConn.Query(@"SELECT
                            (SELECT isnull(c.CommName,'') FROM Tb_HSPR_Community c WHERE x.CommID=c.CommID) AS CommName,
                            (SELECT isnull(isnull(a.RoomName,a.RoomSign),'') 
                                FROM Tb_HSPR_Room a WHERE a.RoomID=x.RoomID) AS RoomName,
                            (SELECT isnull(b.CustName,'') FROM Tb_HSPR_Customer b WHERE b.CustID=x.CustID) AS CustName
                            FROM Tb_HSPR_Fees x WHERE FeesID=@FeesID;", new { FeesID = feesId }).FirstOrDefault();

                        if (feesInfo != null)
                        {
                            string commName = feesInfo.CommName == null ? "" : feesInfo.CommName.ToString();
                            string roomName = feesInfo.RoomName == null ? "" : feesInfo.RoomName.ToString();
                            string custName = feesInfo.CustName == null ? "" : "，业主：" + feesInfo.CustName.ToString();

                            if (!string.IsNullOrEmpty(roomName))
                            {
                                roomName = roomName.Replace("幢", "-").Replace("栋", "-").Replace("单元", "-").Replace("楼", "-").Replace("号", "-").Replace("房", "-")
                                    .Replace("--", "-");
                            }

                            sPara.Add("subject", $"{commName}{roomName}物管费用{custName}");
                        }
                        else
                        {
                            sPara.Add("subject", "物管费用");
                        }
                    }
                    //}
                }
                else
                {
                    sPara.Add("subject", "物管费用");
                }
            }

            sPara.Add("body", CommunityId + "," + userId);
            sPara.Add("total_fee", total_fee.ToString("#0.00"));
            sPara.Add("notify_url", c.notify_url.ToString());
            sPara.Add("service", c.service.ToString());
            sPara.Add("payment_type", c.payment_type.ToString());//支付类型
            sPara.Add("_input_charset", c.input_charset.ToString());//参数编码字符集
            sPara.Add("it_b_pay", "30m");//未付款交易的超时时间

            #endregion

            //将获取的订单信息，按照“参数=参数值”的模式用“&”字符拼接成字符串.
            string data = Core.CreateLinkString(sPara);

            //使用商户的私钥进行RSA签名，并且把sign做一次urleccode.
            string sign = System.Web.HttpUtility.UrlEncode(RSA.sign(data, c.private_key, c.input_charset));
            sPara.Add("sign", sign);//签名
            sPara.Add("sign_type", c.sign_type);//签名方式

            //拼接请求字符串（注意：不要忘记参数值的引号）.
            data = data + "&sign=\"" + sign + "\"&sign_type=\"" + c.sign_type + "\"";

            log.Info("支付宝订单请求:" + data.ToString());

            //返回给客户端请求.
            //prepay_str = data;

            prepay_str = JSONHelper.FromObject(sPara);
            R = true;
            return "success";
        }

        /// <summary>
        /// 生成物业订单
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="IsOk">是否成功生成物业代收订单</param>
        /// <param name="Amount">订单总金额</param>
        /// <returns></returns>
        public string GeneratePropertyOrder(string CommunityId, string FeesIds, string txnTime, string CustID, ref bool IsOk, ref decimal Amount, ref string PropertyOrderId)
        {
            DataTable CommunityTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query(string.Format("SELECT * FROM Tb_Community WHERE Id='{0}' OR CommID='{0}'", CommunityId.ToString())).Tables[0];
            if (CommunityTable.Rows.Count > 0)
            {
                //连接字符串
                DataRow T = CommunityTable.Rows[0];
                //增加订单验证【检测费用是否存在于未下帐订单明细中】
                //List<string> list = CheckFees(FeesIds, CommunityId);
                //if (list.Count>0)
                //{
                //    string str = "";
                //    foreach (string item in list)
                //    {
                //        str += item + ",";
                //    }
                //    return "费项：" + str + "已支付成功，等待下帐！";
                //}
                //生成物业订单
                //2018-3-19  YFZW  增加合景箐蜜  违约金必缴
                string comstr = T["CommID"].ToString().Substring(0, 4);

                DataSet Ds = null;
                if (comstr == "1862")
                {
                    Ds = PropertyOrder_1862(CustID, FeesIds, T["CommID"].ToString(), c.seller_id.ToString(), txnTime, CommunityId);
                }
                else
                {
                    Ds = PropertyOrder(CustID, FeesIds, T["CommID"].ToString(), c.seller_id.ToString(), txnTime, CommunityId);
                }

                if (Ds.Tables.Count > 0)
                {
                    DataRow DRow = Ds.Tables[0].Rows[0];
                    Amount = AppGlobal.StrToDec(DRow["Amount"].ToString());//总金额
                    IsOk = true;
                    PropertyOrderId = DRow["orderId"].ToString();
                    return "生成" + T["CorpName"] + "物业账单成功";
                }
                else
                {
                    IsOk = false;
                    return "部分费用已缴纳，请重新选择费用";
                }
            }
            else
            {
                //未找到小区
                IsOk = false;
                return "没找到小区";
            }
        }

        /// <summary>
        /// 生成物业订单处理过程
        /// </summary>
        /// <param name="CustId"></param>
        /// <param name="FeesIds"></param>
        /// <param name="CommID"></param>
        /// <param name="merId"></param>
        /// <param name="txnTime"></param>
        /// <returns></returns>
        public DataSet PropertyOrder(string CustId, string FeesIds, string CommID, string merId, string txnTime, string CommunityId)
        {
            string ConnStr = GetConnection(CommunityId);
            SqlParameter[] parameters = {
                    new SqlParameter("@CustId", SqlDbType.VarChar),
                    new SqlParameter("@FeesIds", SqlDbType.VarChar),
                    new SqlParameter("@CommID", SqlDbType.VarChar),
                    new SqlParameter("@merId", SqlDbType.VarChar),
                    new SqlParameter("@txnTime", SqlDbType.VarChar),
                    new SqlParameter("@CommunityId", SqlDbType.VarChar)
            };
            parameters[0].Value = CustId;
            parameters[1].Value = FeesIds;
            parameters[2].Value = CommID;
            parameters[3].Value = merId;
            parameters[4].Value = txnTime;
            parameters[5].Value = CommunityId;
            DataSet Ds = new DbHelperSQLP(ConnStr).RunProcedure("Proc_OL_GenerateOrder_AliPay", parameters, "Ds");
            return Ds;
        }


        public DataSet PropertyOrder_1862(string CustId, string FeesIds, string CommID, string merId, string txnTime, string CommunityId)
        {
            string ConnStr = GetConnection(CommunityId);
            SqlParameter[] parameters = {
                    new SqlParameter("@CustId", SqlDbType.VarChar),
                    new SqlParameter("@FeesIds", SqlDbType.VarChar),
                    new SqlParameter("@CommID", SqlDbType.VarChar),
                    new SqlParameter("@merId", SqlDbType.VarChar),
                    new SqlParameter("@txnTime", SqlDbType.VarChar),
                    new SqlParameter("@CommunityId", SqlDbType.VarChar)
            };
            parameters[0].Value = CustId;
            parameters[1].Value = FeesIds;
            parameters[2].Value = CommID;
            parameters[3].Value = merId;
            parameters[4].Value = txnTime;
            parameters[5].Value = CommunityId;
            DataSet Ds = new DbHelperSQLP(ConnStr).RunProcedure("Proc_OL_GenerateOrder_AliPay_1862", parameters, "Ds");
            return Ds;
        }


        /// <summary>
        /// 检测费用是否存在于未下帐订单明细中
        /// </summary>
        /// <param name="FeesIdS"></param>
        /// <param name="CommunityId"></param>
        /// <returns></returns>
        public List<string> CheckFees(string FeesIdS, string CommunityId)
        {
            string ConnStr = GetConnection(CommunityId);
            IDbConnection con = new SqlConnection(ConnStr);
            string sql = "select f.CostName from Tb_OL_AlipayDetail as d inner join funSplitTabel('" + FeesIdS + "', ',') as c on c.ColName = d.FeesId left join Tb_OL_AlipayOrder as o on o.Id = d.PayOrderId inner join view_HSPR_Fees_Filter as f on f.FeesID = d.FeesId where ISNULL(o.IsSucc,0)= 0";
            List<string> list = con.Query<string>(sql).ToList<string>();
            return list;
        }


    }
}