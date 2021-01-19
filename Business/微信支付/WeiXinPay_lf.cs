using App.Model;
using Dapper;
using log4net;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.OL;
using MobileSoft.Model.Unified;
using swiftpass.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WxPayAPI;

namespace Business
{
    public class WeiXinPay_lf : PubInfo
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(WeiXinPay));
        /// <summary>
        /// openid用于调用统一下单接口
        /// </summary>
        //public string openid { get; set; }

        public WxPayData unifiedOrderResult { get; set; }

        public WeiXinPay_lf() //获取小区、项目信息
        {
            base.Token = "20181228WeiXinPay_lf";
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
                //生成威富通微信订单
                case "GenerateWftOrder":
                    Trans.Result = GenerateWftOrder(Row);
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


        /// <summary>
        /// 威富通支付接口,不存数据库,仅做支付用
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GenerateWftOrder(DataRow Row)
        {
            string CommunityId = Row["CommunityId"].ToString();
            string FeesIds = Row["FeesIds"].ToString();
            string txnTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string CustID = Row["CustID"].ToString();
            //2017-06-05添加可选参数openId
            //用于区分是否来自于微信H5支付
            //默认为空
            if (!Row.Table.Columns.Contains("openId"))
            {
                return JSONHelper.FromString(false, "请通过微信访问！");
            }
            string openId = Row["openId"].ToString();
            //增加FeesIds重复验证
            if (FeesIds == "")
            {
                return JSONHelper.FromString(false, "未选择费用！");
            }
            string[] FeesStr = FeesIds.Split(',');
            HashSet<string> h = new HashSet<string>();
            for (int i = 0; i < FeesStr.Length; i++)
            {
                h.Add(FeesStr[i]);
            }
            if (h.Count != FeesStr.Length)
            {
                return JSONHelper.FromString(false, "费用重复！");
            }
            DataTable CommunityTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query("SELECT * FROM Tb_Community WHERE Id='" + CommunityId.ToString() + "'").Tables[0];
            if (null == CommunityTable || CommunityTable.Rows.Count <= 0)
            {
                return JSONHelper.FromString(false, "获取数据库信息失败！");
            }
            //连接字符串
            string connStr = UnionUtil.GetConnectionString(CommunityTable.Rows[0]);
            double Amount = 0.00d;

            using (IDbConnection conn = new SqlConnection(connStr))
            {
                Amount = conn.ExecuteScalar<double>("SELECT ISNULL(SUM(DebtsAmount),0)+ISNULL(SUM(DebtsLateAmount),0) AS Amount FROM Tb_HSPR_Fees WHERE FeesID IN (" + FeesIds + ")", null, null, null, CommandType.Text);
            }
            if (Amount <= 0)
            {
                return JSONHelper.FromString(false, "缴费金额必须大于0！");
            }
            //把金额转换为分
            int fen = (int)Amount * 100;
            //商户订单号,当前10位时间戳+16位随机字符
            string out_trade_no = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000) + Utils.Nmrandom();
            RequestHandler reqHandler = new RequestHandler(null);
            reqHandler.setGateUrl("https://pay.swiftpass.cn/pay/gateway");
            reqHandler.setKey("9d101c97133837e13dde2d32a5054abb");
            reqHandler.setParameter("out_trade_no", out_trade_no);//商户订单号
            reqHandler.setParameter("body", "物管费");//商品描述
            reqHandler.setParameter("attach", CommunityId);//附加信息
            reqHandler.setParameter("total_fee", "1");//总金额
            reqHandler.setParameter("mch_create_ip", "125.64.16.10");//终端IP
            reqHandler.setParameter("time_start", ""); //订单生成时间
            reqHandler.setParameter("time_expire", "");//订单超时时间
            reqHandler.setParameter("service", "pay.weixin.jspay");//接口类型：pay.weixin.jspay
            reqHandler.setParameter("mch_id", "7551000001");//必填项，商户号，由平台分配
            reqHandler.setParameter("version", "1.0");//接口版本号
            reqHandler.setParameter("notify_url", "http://125.64.16.10:9999/TwInterface/Service/WeiXinPayCallBack/WeiXinWftPay.ashx");
            //通知地址，必填项，接收平台通知的URL，需给绝对路径，255字符内;此URL要保证外网能访问   
            reqHandler.setParameter("nonce_str", Utils.random());//随机字符串，必填项，不长于 32 位
            reqHandler.setParameter("charset", "UTF-8");//字符集
            reqHandler.setParameter("sign_type", "MD5");//签名方式
            reqHandler.setParameter("is_raw", "1");//原生JS值
            reqHandler.setParameter("device_info", "");//终端设备号
            reqHandler.setParameter("sub_openid", "");//测试账号不传值,此处默认给空值。正式账号必须传openid值，获取openid值指导文档地址：http://www.cnblogs.com/txw1958/p/weixin76-user-info.html
            reqHandler.setParameter("callback_url", "");//前台地址  交易完成后跳转的 URL，需给绝对路径，255字 符 内 格 式如:http://wap.tenpay.com/callback.asp
            reqHandler.setParameter("goods_tag", "");//商品标记                
            reqHandler.createSign();//创建签名
                                    //以上参数进行签名
            string data = Utils.toXml(reqHandler.getAllParameters());//生成XML报文
            Dictionary<string, string> reqContent = new Dictionary<string, string>();
            reqContent.Add("url", reqHandler.getGateUrl());
            reqContent.Add("data", data);
            PayHttpClient pay = new PayHttpClient();
            pay.setReqContent(reqContent);
            if (!pay.call())
            {
                return JSONHelper.FromString(false, pay.getErrInfo());
            }
            ClientResponseHandler resHandler = new ClientResponseHandler();
            resHandler.setContent(pay.getResContent());
            resHandler.setKey("9d101c97133837e13dde2d32a5054abb");
            Hashtable param = resHandler.getAllParameters();
            if (!resHandler.isTenpaySign())
            {
                return JSONHelper.FromString(false, param["message"].ToString());
            }
            //当返回状态与业务结果都为0时才返回，其它结果请查看接口文档
            if (int.Parse(param["status"].ToString()) != 0 || int.Parse(param["result_code"].ToString()) != 0)
            {
                return JSONHelper.FromString(false, param["err_msg"].ToString());
            }
            return JSONHelper.FromJsonString(true, param["pay_info"].ToString());
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
            string query = "SELECT * FROM Tb_OL_WeiXinOrder WHERE out_trade_no=@out_trade_no";
            Tb_OL_WeiXinPayOrder T_Order = conn.Query<Tb_OL_WeiXinPayOrder>(query, new { out_trade_no = out_trade_no }).SingleOrDefault();

            if (BankResult == "00")
            {
                //更新订单状态
                UpdateProperyOrder(T_Order.CommunityId.ToString(), T_Order.out_trade_no.ToString(), BankResult, "成功");
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
                UpdateProperyOrder(T_Order.CommunityId.ToString(), T_Order.out_trade_no.ToString(), BankResult, "失败");
            }

            return "";
        }

        public string CancelOrder(string CommunityId, string out_trade_no)
        {
            try
            {
                string ConnStr = GetConnection(CommunityId);
                Global_Var.CorpSQLConnstr = ConnStr;
                string BankResult = SearchBankOrder(CommunityId, out_trade_no);

                if (BankResult == "SUCCESS")
                {
                    return JSONHelper.FromString(false, "银行已交易成功,不能取消");
                }

                IDbConnection conn = new SqlConnection(GetConnection(CommunityId));
                string query = "SELECT * FROM Tb_OL_WeiXinPayOrder WHERE out_trade_no=@out_trade_no";
                Tb_OL_WeiXinPayOrder T_Order = conn.Query<Tb_OL_WeiXinPayOrder>(query, new { out_trade_no = out_trade_no }).SingleOrDefault();

                if (T_Order.return_code.Trim().ToString() == "SUCCESS")
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
                new DbHelperSQLP(ConnStr).RunProcedure("Proc_OL_WeiXinPayCancelOrder", parameters);
                return JSONHelper.FromString(true, "取消订单成功");
            }
            catch (Exception E)
            {
                return JSONHelper.FromString(false, E.Message.ToString());
            }

        }

        public new static string GetConnection(string CommunityId)
        {
            IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString.ToString());
            string query = "SELECT * FROM Tb_Community WHERE Id=@id";
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

            WxPayConfig wxPayConfig = GenerateConfig(CommunityId);

            IDbConnection conn = new SqlConnection(GetConnection(CommunityId));
            string query = "SELECT * FROM Tb_OL_WeiXinPayOrder WHERE out_trade_no=@out_trade_no";
            Tb_OL_WeiXinPayOrder T_Order = conn.Query<Tb_OL_WeiXinPayOrder>(query, new { out_trade_no = out_trade_no }).SingleOrDefault();

            if (T_Order == null)
            {
                Result = "未找到该物业订单";
                return Result;
            }

            WxPayData data = new WxPayData();
            data.SetValue("out_trade_no", out_trade_no);
            WxPayData BankResult = WxPayApi.OrderQuery(data);//提交订单查询请求给API，接收返回数据
            Result = BankResult.GetValue("trade_state").ToString();
            return Result;
        }

        /// <summary>
        /// 更新物业订单状态
        /// </summary>
        /// <param name="CommunityId"></param>
        /// <param name="OrderId"></param>
        /// <param name="respCode"></param>
        /// <param name="respmsg"></param>
        public static string UpdateProperyOrder(string CommunityId, string out_trade_no, string return_code, string return_msg)
        {
            IDbConnection Conn = new SqlConnection(GetConnection(CommunityId));
            string Query = "UPDATE Tb_OL_WeiXinPayOrder SET return_code=@return_code,return_msg=@return_msg  WHERE out_trade_no = @out_trade_no ";
            Conn.Execute(Query, new { return_code = return_code, return_msg = return_msg, out_trade_no = out_trade_no });
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
                    var T_Order = conn.Query<Tb_OL_WeiXinPayOrder>("SELECT * FROM Tb_OL_WeiXinPayOrder WHERE out_trade_no=@out_trade_no",
                        new { out_trade_no = out_trade_no }).SingleOrDefault();

                    if (T_Order.IsSucc.ToString() == "1")
                    {
                        return SetNotifyResult("FAIL", "物业账单已下账");
                    }
                }

                ReceFees(total_amount, CommunityId, out_trade_no, pointUseHistoryID);
                return SetNotifyResult("SUCCESS", "已下账");
            }
            catch (Exception E)
            {
                return SetNotifyResult("FAIL", E.Message.ToString());
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

                        conn.Execute(@"UPDATE Tb_App_Point_UseHistory SET IsEffect=1 WHERE IID=@IID", new { IID = pointUseHistoryID }, trans);

                        parameters.Add(new SqlParameter("@UsePoint", SqlDbType.Int) { Value = 1 });
                        parameters.Add(new SqlParameter("@TotalAmount", SqlDbType.Decimal) { Value = total_amount });
                        parameters.Add(new SqlParameter("@PointDeductionAmount", SqlDbType.Decimal) { Value = pointDeductionAmount });

                        new DbHelperSQLP(PubConstant.hmWyglConnectionString).RunProcedure("Proc_OL_WeiXinPayReceFees", parameters.ToArray());

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
                new DbHelperSQLP(PubConstant.hmWyglConnectionString).RunProcedure("Proc_OL_WeiXinPayReceFees", parameters.ToArray());
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
            string BankOrderId = "";

            string CommunityId = Row["CommunityId"].ToString();
            string FeesIds = Row["FeesIds"].ToString();
            string txnTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string CustID = Row["CustID"].ToString();
            //2017-06-05添加可选参数openId
            //用于区分是否来自于微信H5支付
            //默认为空
            string openId = Row.Table.Columns.Contains("openId") ? Row["openId"].ToString() : "";

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

            WxPayConfig wxPayConfig = GenerateConfig(CommunityId);
            if (null == wxPayConfig)
            {
                return JSONHelper.FromString(false, "该小区不支持微信支付");
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

                int Amount = 0;                             // 缴费总额
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
                    controlInfo.CorpID = corpId;
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
                    catch (Exception)
                    {
                        appTrans.Rollback();
                        return new ApiResult(false, "积分验证失败").toJson();
                    }
                    #endregion
                }

                // 积分数量正常，生成订单
                try
                {
                    // 生成ERP账单
                    string PropertyResult = GeneratePropertyOrder(CommunityId, FeesIds, txnTime, CustID, ref IsPropertyOk, ref Amount, ref PropertyOrderId, wxPayConfig);

                    // ERP订单生成成功
                    if (IsPropertyOk == true)
                    {
                        // 应缴总金额
                        Amount = Amount - (int)(deductionAmount * 100);

                        // 微信签名订单信息
                        WxPayData Data = new WxPayData();
                        string BankResult = GenerateBankOrder(CommunityId, UserId, feesArray.First(), PropertyOrderId, txnTime, Amount, ref IsBankOk, ref BankOrderId, ref Data, wxPayConfig, openId);

                        if (IsBankOk == true)
                        {
                            // 微信订单信息
                            WxPayData result = new WxPayData();
                            result.SetValue("appid", Data.GetValue("appid"));
                            result.SetValue("partnerid", Data.GetValue("mch_id"));
                            result.SetValue("prepayid", Data.GetValue("prepay_id"));
                            result.SetValue("noncestr", Data.GetValue("nonce_str"));
                            result.SetValue("package", "Sign=WXPay");
                            result.SetValue("timestamp", (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
                            result.SetValue("sign", result.MakeSign());

                            // 计算预赠送积分
                            new AppPoint().CalcPresentedPointForPropertyFees(CommunityId, propertyAmount - propertyMaxDiscountsAmount, parkingAmount - parkingMaxDiscountsAmount, out int p1, out int p2);
                            result.SetValue("presented_points", (p1 + p2));

                            if (!string.IsNullOrEmpty(useHistoryID))
                            {
                                result.SetValue("out_trade_no", PropertyOrderId);
                                result.SetValue("deduction_amount", deductionAmount);
                                result.SetValue("point_use_history_id", useHistoryID);

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
                                                    VALUES(@UseHistoryID, @OrderID, '微信', @UsableObject)",
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
                                erpConn.Execute(@"UPDATE Tb_OL_WeiXinPayOrder SET prepay_id=@prepay_id WHERE out_trade_no = @out_trade_no ",
                                    new { prepay_id = Data.GetValue("prepay_id").ToString(), out_trade_no = PropertyOrderId });
                            }

                            appTrans?.Commit();
                            return JSONHelper.FromJsonString(true, result.ToJson());
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
        public WxPayConfig GenerateConfig(string CommunityId)
        {
            WxPayConfig wxPayConfig = null;
            IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString.ToString());
            string query = "SELECT * FROM Tb_WeiXinPayCertificate WHERE CommunityId=@CommunityId";
            Tb_WeiXinPayCertificate T = conn.Query<Tb_WeiXinPayCertificate>(query, new { CommunityId = CommunityId }).SingleOrDefault();
            if (T != null)
            {
                wxPayConfig = new WxPayConfig();
                wxPayConfig.APPID = T.appid.ToString();
                wxPayConfig.MCHID = T.mch_id.ToString();
                wxPayConfig.KEY = T.appkey.ToString();
                wxPayConfig.APPSECRET = T.appsecret.ToString();
                wxPayConfig.SSLCERT_PATH = T.SSLCERT_PATH;
                wxPayConfig.SSLCERT_PASSWORD = T.SSLCERT_PASSWORD;
                wxPayConfig.NOTIFY_URL = Global_Fun.AppWebSettings("WechatPay_Notify_Url").ToString();
            }
            return wxPayConfig;
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
        public string GenerateBankOrder(string CommunityId, string UserId, string feesId, string out_trade_no, string txnTime, int total_fee,
            ref bool R, ref string BankOrderId, ref WxPayData WPD, WxPayConfig wxPayConfig, string openId = "")
        {
            R = false;
            //统一下单
            WxPayData data = new WxPayData();

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString.ToString()))
            {
                dynamic commInfo = conn.Query("SELECT * FROM Tb_Community WHERE Id=@CommunityId OR CommID=@CommunityId",
                    new { CommunityId = CommunityId }).FirstOrDefault();

                if (commInfo != null)
                {
                    // 俊发
                    //if (commInfo.CorpID == 1985)
                    //{
                    //    data.SetValue("body", commInfo.CommName.ToString() + "物管费用");
                    //}
                    //else
                    //{
                    using (var erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
                    {
                        dynamic feesInfo = erpConn.Query(@"SELECT
                            (SELECT isnull(c.CommName,'') FROM Tb_HSPR_Community c WHERE x.CommID=c.CommID) AS CommName,
                            (SELECT isnull(isnull(a.RoomName,a.RoomSign),'') FROM Tb_HSPR_Room a WHERE a.RoomID=x.RoomID) AS RoomName,
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

                            data.SetValue("body", $"{commName}{roomName}物管费用{custName}");
                        }
                        else
                        {
                            data.SetValue("body", "物管费用");
                        }
                    }
                }
                //}
                else
                {
                    data.SetValue("body", "物管费用");
                }
            }

            // 是否有使用积分
            data.SetValue("attach", CommunityId + "," + UserId);

            data.SetValue("out_trade_no", out_trade_no);
            data.SetValue("total_fee", total_fee);
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            data.SetValue("notify_url", Global_Fun.AppWebSettings("WechatPay_Notify_Url").ToString());

            //2017-06-05
            //修改人:敬志强
            //修改内容:新增openid字段判断
            //如果没有openid即视为APP支付
            //有openid即是微信公众号H5支付
            if (string.IsNullOrEmpty(openId))
            {
                data.SetValue("trade_type", "APP");
            }
            else
            {
                data.SetValue("trade_type", "JSAPI");
                data.SetValue("openid", openId);
            }

            WxPayData result = WxPayApi.UnifiedOrder(data, wxPayConfig);
            if (!result.IsSet("appid") || !result.IsSet("prepay_id") || result.GetValue("prepay_id").ToString() == "")
            {
                R = false;
                return result.ToXml();
            }
            R = true;
            WPD = result;
            return "SUCCESS";
        }

        /// <summary>
        /// 生成物业订单
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="IsOk">是否成功生成物业代收订单</param>
        /// <param name="Amount">订单总金额</param>
        /// <returns></returns>
        public string GeneratePropertyOrder(string CommunityId, string FeesIds, string txnTime, string CustID, ref bool IsOk, ref int Amount,
            ref string PropertyOrderId, WxPayConfig wxPayConfig)
        {
            DataTable CommunityTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query("SELECT * FROM Tb_Community WHERE Id='" + CommunityId.ToString() + "'").Tables[0];
            if (CommunityTable.Rows.Count > 0)
            {
                //连接字符串
                DataRow T = CommunityTable.Rows[0];
                Global_Var.CorpSQLConnstr = UnionUtil.GetConnectionString(T);
                //生成物业订单
                DataSet Ds = PropertyOrder(CustID, FeesIds, T["CommID"].ToString(), wxPayConfig.MCHID.ToString().ToString(), txnTime, CommunityId);
                if (Ds.Tables.Count > 0)
                {
                    DataRow DRow = Ds.Tables[0].Rows[0];
                    Amount = AppGlobal.StrToInt(DRow["Amount"].ToString());//总金额
                    IsOk = true;
                    PropertyOrderId = DRow["orderId"].ToString();
                    return "生成物业账单成功";
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
            DataSet Ds = new DbHelperSQLP(ConnStr).RunProcedure("Proc_OL_GenerateOrder_WeiXin", parameters, "Ds");
            return Ds;
        }


    }
}
