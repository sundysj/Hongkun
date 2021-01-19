using System;
using MobileSoft.DBUtility;
using MobileSoft.Common;
using System.Data;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using com.unionpay.acp.sdk;
using System.Data.SqlClient;
using Dapper;
using MobileSoft.Model.Unified;
using MobileSoft.Model.OL;
using System.Linq;
using WxPayAPI;
using log4net;
using Dapper;
using DapperExtensions;


namespace Business
{
    public class WeiXinPayPrec_lf : PubInfo
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(WeiXinPayPrec));
        /// <summary>
        /// openid用于调用统一下单接口
        /// </summary>

        public WxPayData unifiedOrderResult { get; set; }
        public WeiXinPayPrec_lf() //获取小区、项目信息
        {
            base.Token = "20190102WeiXinPayPrec_lf";
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
                    Trans.Result = ReceProperyOrder(Row["CommunityId"].ToString(), Row["OrderId"].ToString());
                    break;
                //取消下帐
                case "NoReceProperyOrder":
                    Trans.Result = NoReceProperyOrder(Row["CommunityId"].ToString(), Row["OrderId"].ToString());
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
            string query = "SELECT * FROM Tb_OL_WeiXinOrder WHERE out_trade_no=@out_trade_no";
            Tb_OL_WeiXinPayOrder T_Order = conn.Query<Tb_OL_WeiXinPayOrder>(query, new { out_trade_no = out_trade_no }).SingleOrDefault();

            if (BankResult == "00")
            {
                //更新订单状态
                UpdateProperyOrder(T_Order.CommunityId.ToString(), T_Order.out_trade_no.ToString(), BankResult, "成功");
                if (T_Order.IsSucc.ToString() != "1")
                {
                    //下账
                    ReceProperyOrder(T_Order.CommunityId.ToString(), T_Order.out_trade_no.ToString());
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

        public static string GetConnection(string CommunityId)
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
        public static string ReceProperyOrder(string CommunityId, string out_trade_no)
        {
            try
            {
                PubConstant.hmWyglConnectionString = GetConnection(CommunityId);
                Global_Var.CorpSQLConnstr = PubConstant.hmWyglConnectionString;

                IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                string query = "SELECT * FROM Tb_OL_WeiXinPayOrder WHERE out_trade_no=@out_trade_no";
                Tb_OL_WeiXinPayOrder T_Order = conn.Query<Tb_OL_WeiXinPayOrder>(query, new { out_trade_no = out_trade_no }).SingleOrDefault();
                if (T_Order.IsSucc.ToString() == "1")
                {
                    return SetNotifyResult("FAIL", "物业账单已下账");
                }

                string result = ReceCost(CommunityId, out_trade_no);
                if (string.IsNullOrEmpty(result))
                {
                    return SetNotifyResult("SUCCESS", "已下账");
                }

                return result;
            }
            catch (Exception E)
            {
                return SetNotifyResult("FAIL", E.Message.ToString() + E.InnerException + E.StackTrace);
            }
        }

        public static string SetNotifyResult(string State, string Msg)
        {
            WxPayData res = new WxPayData();
            res.SetValue("return_code", State);
            res.SetValue("return_msg", Msg);
            return res.ToXml();
        }

        public static string ReceCost(string CommunityId, string out_trade_no)
        {
            IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString);
            string query = "SELECT * FROM Tb_Community WHERE Id=@id";
            var T = conn.Query<Tb_Community>(query, new { id = CommunityId }).FirstOrDefault();
            if (T != null)
            {
                PubConstant.hmWyglConnectionString = GetConnectionStr(T);
                Global_Var.CorpSQLConnstr = PubConstant.hmWyglConnectionString;
            }
            conn.Dispose();

            //查询账单信息
            conn = new SqlConnection(PubConstant.hmWyglConnectionString);
            query = "SELECT * FROM Tb_OL_WeiXinPayOrder WHERE out_trade_no=@out_trade_no";
            Tb_OL_WeiXinPayOrder T_Order = conn.Query<Tb_OL_WeiXinPayOrder>(query, new { out_trade_no = out_trade_no }).SingleOrDefault();

            query = "SELECT * FROM Tb_OL_WeiXinPayDetail_Prec WHERE PayOrderId=@PayOrderId";
            Tb_OL_WeiXinPayDetail_Prec T_Prec = conn.Query<Tb_OL_WeiXinPayDetail_Prec>(query, new { PayOrderId = T_Order.Id.ToString() }).SingleOrDefault();

            //预存收款
            string strUserCode = "_Sys_";
            string Result = "";
            string ErrorMsg = "";
            string chargeMode = "微信";
            if (T.CorpID == 1973)
            {
                chargeMode = "自助缴费-微信";
            }
            long iReceID = 0;
            decimal PrecAmount = Convert.ToDecimal(T_Prec.DueAmount);

            ReceFeesPrec.ReceivePrecFees(AppGlobal.StrToInt(T_Order.CommID.ToString()), T_Order.CustId, T_Prec.RoomID, T_Prec.CostId.ToString(), chargeMode, PrecAmount, strUserCode, ref Result, ref ErrorMsg, ref iReceID);

            if (string.IsNullOrEmpty(ErrorMsg))
            {
                //更新账单信息
                T_Order.IsSucc = 1;
                conn.Update(T_Order);

                T_Prec.PaidAmount = T_Prec.DueAmount;
                conn.Update(T_Prec);
            }

            return iReceID + Result + ErrorMsg;
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
            string CostID = Row["CostID"].ToString();
            string StanID = Row["StanID"].ToString();
            string HandID = Row["HandID"].ToString();
            string RoomID = Row["RoomID"].ToString();
            string txnTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string CustID = Row["CustID"].ToString();
            string UserID = Row["UserID"].ToString();
            int Months = AppGlobal.StrToInt(Row["Months"].ToString());
            if (Months == 0)
            {
                return JSONHelper.FromString(false, "请选择预存月数");
            }

            //2017-06-05添加可选参数openId
            //用于区分是否来自于微信H5支付
            //默认为空
            string openId = Row.Table.Columns.Contains("openId") ? Row["openId"].ToString() : "";
            WxPayConfig wxPayConfig = GenerateConfig(CommunityId);

            if (null == wxPayConfig)
            {
                return JSONHelper.FromString(false, "未配置证书文件");
            }

            PubConstant.hmWyglConnectionString = GetConnection(CommunityId);
            Global_Var.CorpSQLConnstr = PubConstant.hmWyglConnectionString;

            string Amount = "0";

            //生成物业账单
            string PropertyResult = GeneratePropertyOrder(CommunityId, CostID, StanID, HandID, RoomID, txnTime, CustID, Months, ref IsPropertyOk, ref Amount, ref PropertyOrderId, wxPayConfig);
            if (IsPropertyOk == true)
            {
                //生成银行订单,返回银行流水号
                WxPayData Data = new WxPayData();
                string BankResult = GenerateBankOrder(CommunityId, CustID, RoomID, UserID, Months, PropertyOrderId, txnTime, Amount, ref IsBankOk, ref BankOrderId, ref Data, wxPayConfig, openId);
                if (IsBankOk == false)
                {
                    return JSONHelper.FromString(false, BankResult);
                }
                else
                {
                    //更新订单银行流水号
                    IDbConnection Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                    string Query = "UPDATE Tb_OL_WeiXinPayOrder SET prepay_id=@prepay_id WHERE out_trade_no = @out_trade_no ";
                    Conn.Execute(Query, new { prepay_id = Data.GetValue("prepay_id").ToString(), out_trade_no = PropertyOrderId });
                    //向手机端返回银行记录
                    WxPayData result = new WxPayData();
                    result.SetValue("appid", Data.GetValue("appid"));
                    result.SetValue("partnerid", Data.GetValue("mch_id"));
                    result.SetValue("prepayid", Data.GetValue("prepay_id"));
                    result.SetValue("noncestr", Data.GetValue("nonce_str"));
                    result.SetValue("package", "Sign=WXPay");
                    result.SetValue("timestamp", (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
                    result.SetValue("sign", result.MakeSign());

                    int presentedPoint = new AppPoint().CalcPresentedPointForPrec_lf(Months);
                    result.SetValue("presented_points", presentedPoint);
                    result.SetValue("out_trade_no", PropertyOrderId);

                    return JSONHelper.FromJsonString(true, result.ToJson());
                }
            }
            else
            {
                return JSONHelper.FromString(false, PropertyResult);
            }
        }

        /// <summary>
        /// 配置支付请求参数,根据当前小区获取
        /// </summary>
        /// <param name="Row"></param>
        public WxPayConfig GenerateConfig(string CommunityId)
        {
            WxPayConfig wxPayConfig = null;
            IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString);
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
                wxPayConfig.NOTIFY_URL = Global_Fun.AppWebSettings("WechatPayPrec_Notify_Url").ToString();
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
        public string GenerateBankOrder(string CommunityId, string CustID, string RoomID, string UserID, int Months, string out_trade_no, string txnTime, string total_fee,
            ref bool R, ref string BankOrderId, ref WxPayData WPD, WxPayConfig wxPayConfig, string openId = "")
        {
            DataTable CommunityTable = new DbHelperSQLP(PubConstant.UnifiedContionString).Query(string.Format("SELECT * FROM Tb_Community WHERE Id='{0}' OR CommID='{0}'", CommunityId.ToString())).Tables[0];

            R = false;
            //统一下单
            WxPayData data = new WxPayData();

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                dynamic commInfo = conn.Query("SELECT * FROM Tb_Community WHERE Id=@CommunityId OR CommID=@CommunityId",
                    new { CommunityId = CommunityId }).FirstOrDefault();

                if (commInfo != null)
                {
                    using (var erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
                    {
                        dynamic roomInfo = erpConn.Query(@"SELECT 
                                (SELECT isnull(c.CommName,'') FROM Tb_HSPR_Community c WHERE 
                                    c.CommID=(SELECT d.CommID FROM Tb_HSPR_Room d WHERE d.RoomID=x.RoomID)) AS CommName,
                                (SELECT isnull(isnull(e.RoomName,e.RoomSign),'') FROM Tb_HSPR_Room e WHERE e.RoomID=x.RoomID) AS RoomName,
                                (SELECT isnull(b.CustName,'') FROM Tb_HSPR_Customer b WHERE b.CustID=x.CustID) AS CustName
                                FROM Tb_HSPR_CustomerLive x WHERE x.RoomID=@RoomID AND x.IsDelLive=0 AND x.LiveType=1;",
                        new { RoomID = RoomID }).FirstOrDefault();

                        if (roomInfo != null)
                        {
                            string commName = roomInfo.CommName == null ? "" : roomInfo.CommName.ToString();
                            string roomName = roomInfo.RoomName == null ? "" : roomInfo.RoomName.ToString();
                            string custName = roomInfo.CustName == null ? "" : "，业主：" + roomInfo.CustName.ToString();

                            if (!string.IsNullOrEmpty(roomName))
                            {
                                roomName = roomName.Replace("幢", "-").Replace("栋", "-").Replace("单元", "-").Replace("楼", "-").Replace("号", "-").Replace("房", "-")
                                    .Replace("--", "-");
                            }

                            data.SetValue("body", $"{commName}{roomName}预存费用{custName}");
                        }
                        else
                        {
                            data.SetValue("body", "预存费用");
                        }
                    }
                }
                else
                {
                    data.SetValue("body", "预存费用");
                }
            }

            data.SetValue("attach", CommunityId + "," + UserID + "," + Months);
            data.SetValue("out_trade_no", out_trade_no);
            data.SetValue("total_fee", total_fee);
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            data.SetValue("notify_url", Global_Fun.AppWebSettings("WechatPayPrec_Notify_Url").ToString());
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
                return "UnifiedOrder response error";
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
        public string GeneratePropertyOrder(string CommunityId, string CostID, string StanID, string HandID, string RoomID, string txnTime, string CustID, int months,
            ref bool IsOk, ref string Amount, ref string PropertyOrderId, WxPayConfig wxPayConfig)
        {
            Tb_Community community;
            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                community = conn.Query<Tb_Community>("SELECT * FROM Tb_Community WHERE Id=@Id", new { Id = CommunityId }).FirstOrDefault();
            }

            if (community != null)
            {
                // 应缴总金额
                if (CalcCostStanSettingAmountWithMonths(community.CommID, CustID, RoomID, CostID, StanID, HandID, months, out decimal totalAmount) == false)
                {
                    IsOk = false;
                    return "计算预缴费用失败";
                }
                totalAmount = totalAmount * 100;

                //生成物业订单
                DataSet Ds = PropertyOrder(CustID, CostID, community.CommID, RoomID, wxPayConfig.MCHID.ToString(), txnTime, CommunityId, totalAmount.ToString());
                if (Ds.Tables.Count > 0)
                {
                    DataRow DRow = Ds.Tables[0].Rows[0];
                    Amount = ((int)totalAmount).ToString();//总金额
                    IsOk = true;
                    PropertyOrderId = DRow["orderId"].ToString();
                    return "生成物业账单成功";
                }
                else
                {
                    IsOk = false;
                    return "生成物业账单失败,请检查选择的费用是否未提交,如若有未交款费项的订单,请先取消订单";
                }
            }
            else
            {
                //未找到小区
                IsOk = false;
                return "没找到小区";
            }
        }

        public bool CalcCostStanSettingAmountWithMonths(string CommID, string CustID, string RoomID, string CostID, string StanID, string HandID, int months, out decimal totalAmount)
        {
            totalAmount = 0;
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 计算费用开始时间
                string beginDateStr = conn.Query<string>("Proc_HSPR_Fees_CalcBeginDateFilter", new
                {
                    CommID = CommID,
                    CustID = CustID,
                    RoomID = RoomID,
                    CostID = CostID,
                    HandID = HandID
                }, null, false, null, CommandType.StoredProcedure).FirstOrDefault();

                if (string.IsNullOrEmpty(beginDateStr))
                {
                    beginDateStr = DateTime.Now.ToString();
                }

                DateTime FeesStateDate = Convert.ToDateTime(beginDateStr);
                DateTime FeesEndDate = FeesStateDate.AddMonths(months);

                //// 计算应缴费用
                //dynamic info = conn.Query("Proc_HSPR_Fees_CalcAmount", new
                //{
                //    CommID = CommID,
                //    CustID = CustID,
                //    RoomID = RoomID,
                //    HandID = HandID,
                //    CostID = CostID,
                //    StanID = StanID,
                //    FeesStateDate = FeesStateDate,
                //    FeesEndDate = FeesEndDate,
                //    Amount = 0,
                //    Amount2 = 0
                //}, null, false, null, CommandType.StoredProcedure).FirstOrDefault();

                //if (null == info)
                //{
                //    return false;
                //}
                //totalAmount = info.DueAmount;
                //return true;

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(" and CommID={0}", CommID);
                sb.AppendFormat(" and CustID={0}", CustID);
                sb.AppendFormat(" and RoomID={0}", RoomID);
                sb.AppendFormat(" and CostID={0}", CostID);

                SqlParameter[] parameters = {
                    new SqlParameter("@SQLEx", SqlDbType.VarChar, 255)
                    };
                parameters[0].Value = sb.ToString();

                DataSet Ds = new DbHelperSQLP(PubConstant.hmWyglConnectionString).RunProcedure("Proc_HSPR_CostStanSetting_Filter", parameters, "RetDataSet");

                if (Ds == null || Ds.Tables.Count <= 0)
                {
                    return false;
                }

                try
                {
                    DateTime date1 = FeesStateDate;
                    DateTime date2 = FeesEndDate;
                    date2 = date2.AddMonths(1).AddDays(-1);

                    while (date1 < date2)
                    {
                        SqlParameter[] paramete = {
                            new SqlParameter("@CommID",  SqlDbType.Int),
                            new SqlParameter("@CustID",  SqlDbType.BigInt),
                            new SqlParameter("@RoomID",  SqlDbType.BigInt),

                            new SqlParameter("@HandID",  SqlDbType.BigInt),
                            new SqlParameter("@CostID",  SqlDbType.BigInt),
                            new SqlParameter("@StanID",  SqlDbType.BigInt),

                            new SqlParameter("@FeesStateDate",  SqlDbType.DateTime),
                            new SqlParameter("@FeesEndDate",  SqlDbType.DateTime),
                            new SqlParameter("@Amount",  SqlDbType.Decimal),

                            new SqlParameter("@Amount2",  SqlDbType.Decimal)
                        };
                        paramete[0].Value = CommID;
                        paramete[1].Value = CustID;
                        paramete[2].Value = RoomID;
                        paramete[3].Value = 0;
                        paramete[4].Value = CostID;

                        paramete[5].Value = StanID;
                        paramete[6].Value = date1;
                        paramete[7].Value = date1.AddMonths(months).AddDays(-1);
                        paramete[8].Value = 0;
                        paramete[9].Value = 1;

                        DataTable dTableCalc = new DbHelperSQLP(PubConstant.hmWyglConnectionString).RunProcedure("Proc_HSPR_Fees_CalcAmount", paramete, "RetDataSet").Tables[0];

                        if (dTableCalc.Rows.Count > 0)
                        {
                            DataRow DRowCalc = dTableCalc.Rows[0];
                            totalAmount = AppGlobal.StrToDec(DRowCalc["DueAmount"].ToString());
                            dTableCalc.Dispose();

                            return true;
                        }
                        dTableCalc.Dispose();
                        date1 = date1.AddMonths(1);
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }

                return true;
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
        public DataSet PropertyOrder(string CustId, string CostID, string CommID, string RoomID, string merId, string txnTime, string CommunityId, string PrecAmount)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@CustId", SqlDbType.VarChar),
                    new SqlParameter("@CostID", SqlDbType.VarChar),
                    new SqlParameter("@CommID", SqlDbType.VarChar),
                    new SqlParameter("@RoomID", SqlDbType.VarChar),
                    new SqlParameter("@merId", SqlDbType.VarChar),
                    new SqlParameter("@txnTime", SqlDbType.VarChar),
                    new SqlParameter("@CommunityId", SqlDbType.VarChar),
                    new SqlParameter("@PrecAmount", SqlDbType.VarChar)
            };
            parameters[0].Value = CustId;
            parameters[1].Value = CostID;
            parameters[2].Value = CommID;
            parameters[3].Value = RoomID;
            parameters[4].Value = merId;
            parameters[5].Value = txnTime;
            parameters[6].Value = CommunityId;
            parameters[7].Value = PrecAmount;
            DataSet Ds = new DbHelperSQLP(PubConstant.hmWyglConnectionString).RunProcedure("Proc_OL_GenerateOrder_WeiXin_Prec", parameters, "Ds");
            return Ds;
        }


        /// <summary>
        /// 取消下帐
        /// </summary>
        /// <param name="CommunityId"></param>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public static string NoReceProperyOrder(string CommunityId, string OrderId)
        {
            try
            {
                string ConnStr = GetConnection(CommunityId);

                IDbConnection conn = new SqlConnection(ConnStr);
                string query = "SELECT * FROM Tb_OL_WeiXinPayOrder WHERE out_trade_no=@OrderId";
                Tb_OL_WeiXinPayOrder T_Order = conn.Query<Tb_OL_WeiXinPayOrder>(query, new { orderId = OrderId }).SingleOrDefault();
                if (T_Order.IsSucc.ToString() == "0")
                {
                    return "物业账单未下账";
                }
                conn.Dispose();
                conn = new SqlConnection(ConnStr);

                //更改状态
                T_Order.IsSucc = 0;
                conn.Update<Tb_OL_WeiXinPayOrder>(T_Order);
                return "success";
            }
            catch (Exception E)
            {
                return E.Message.ToString();
            }
        }





    }
}
