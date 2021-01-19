using Com.Alipay;
using Dapper;
using DapperExtensions;
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
using System.Text;
using WxPayAPI;

namespace Business
{
    public class AlipayPrec_lf : PubInfo
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AlipayPrec));
        /// <summary>
        /// openid用于调用统一下单接口
        /// </summary>
        public string openid { get; set; }

        public Config c { get; set; }

        public AlipayPrec_lf() //获取小区、项目信息
        {
            base.Token = "20190102AlipayPrec_lf";
            c = new Config();
            c.notify_url = Global_Fun.AppWebSettings("AliPayPrec_Notify_Url").ToString();
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
                //刷新订单
                case "RefreshOrder":
                    Trans.Result = RefreshOrder(Row["CommunityId"].ToString(), Row["OrderId"].ToString());
                    break;
                //取消订单
                case "CancelOrder":
                    Trans.Result = CancelOrder(Row["CommunityId"].ToString(), Row["OrderId"].ToString());
                    break;
                //取消下帐
                case "NoReceProperyOrder":
                    Trans.Result = NoReceProperyOrder(Row["CommunityId"].ToString(), Row["OrderId"].ToString());
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
            IDbConnection conn = new SqlConnection(ConnStr);
            string query = "SELECT * FROM Tb_OL_AlipayOrder WHERE out_trade_no=@out_trade_no";
            Tb_OL_AlipayOrder T_Order = conn.Query<Tb_OL_AlipayOrder>(query, new { out_trade_no = out_trade_no }).SingleOrDefault();

            if (BankResult == "TRADE_SUCCESS")
            {
                //更新订单状态
                UpdateProperyOrder(T_Order.CommunityId.ToString(), T_Order.out_trade_no.ToString(), BankResult, "TRADE_SUCCESS");
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

                IDbConnection conn = new SqlConnection(ConnStr);
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
        public static string ReceProperyOrder(string CommunityId, string out_trade_no)
        {
            try
            {
                PubConstant.hmWyglConnectionString = GetConnection(CommunityId);
                Global_Var.CorpSQLConnstr = PubConstant.hmWyglConnectionString;

                IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                string query = "SELECT * FROM Tb_OL_AlipayOrder WHERE out_trade_no=@out_trade_no";
                Tb_OL_AlipayOrder T_Order = conn.Query<Tb_OL_AlipayOrder>(query, new { out_trade_no = out_trade_no }).SingleOrDefault();
                if (T_Order.IsSucc.ToString() == "1")
                {
                    return "物业订单已下账";
                }
                ReceCost(CommunityId, out_trade_no);
                return "success";
            }
            catch (Exception E)
            {
                return E.Message.ToString();
            }
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
                string query = "SELECT * FROM Tb_OL_AlipayOrder WHERE out_trade_no=@OrderId";
                Tb_OL_AlipayOrder T_Order = conn.Query<Tb_OL_AlipayOrder>(query, new { orderId = OrderId }).SingleOrDefault();
                if (T_Order.IsSucc.ToString() == "0")
                {
                    return "物业账单未下账";
                }
                conn.Dispose();
                conn = new SqlConnection(ConnStr);

                //更改状态
                T_Order.IsSucc = 0;
                conn.Update<Tb_OL_AlipayOrder>(T_Order);
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

        public static void ReceCost(string CommunityId, string out_trade_no)
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
            //公司数据库连接字符串

            //查询账单信息
            conn = new SqlConnection(PubConstant.hmWyglConnectionString);
            query = "SELECT * FROM Tb_OL_AliPayOrder WHERE out_trade_no=@out_trade_no";
            Tb_OL_AlipayOrder T_Order = conn.Query<Tb_OL_AlipayOrder>(query, new { out_trade_no = out_trade_no }).SingleOrDefault();

            query = "SELECT * FROM Tb_OL_AliPayDetail_Prec WHERE PayOrderId=@PayOrderId";
            Tb_OL_AliPayDetail_Prec T_Prec = conn.Query<Tb_OL_AliPayDetail_Prec>(query, new { PayOrderId = T_Order.Id.ToString() }).SingleOrDefault();

            //预存收款
            string strUserCode = "_Sys_";
            string Result = "";
            string ErrorMsg = "";
            string chargeMode = "支付宝";
            if (T.CorpID == 1973)
            {
                chargeMode = "自助缴费-支付宝";
            }

            long iReceID = 0;
            decimal PrecAmount = Convert.ToDecimal(T_Prec.DueAmount);

            ReceFeesPrec.ReceivePrecFees(AppGlobal.StrToInt(T_Order.CommID.ToString()), T_Order.CustId, T_Prec.RoomID, T_Prec.CostId.ToString(), chargeMode, PrecAmount, strUserCode, ref Result, ref ErrorMsg, ref iReceID);

            //更新账单信息
            T_Order.IsSucc = 1;
            conn.Update(T_Order);

            T_Prec.PaidAmount = T_Prec.DueAmount;
            conn.Update(T_Prec);
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

            bool IsConfig = GenerateConfig(CommunityId);

            if (IsConfig == false)
            {
                return JSONHelper.FromString(false, "未配置证书文件");
            }

            PubConstant.hmWyglConnectionString = GetConnection(CommunityId);
            Global_Var.CorpSQLConnstr = PubConstant.hmWyglConnectionString;

            string Amount = "0";

            //生成物业账单
            string PropertyResult = GeneratePropertyOrder(CommunityId, CostID, StanID, HandID, RoomID, txnTime, CustID, Months, ref IsPropertyOk, ref Amount, ref PropertyOrderId);
            if (IsPropertyOk == true)
            {
                //返回签名的订单信息
                string BankResult = GenerateBankOrder(CommunityId, CustID, RoomID, UserID, Months, PropertyOrderId, txnTime, Amount, ref IsBankOk, ref prepay_str);
                if (IsBankOk == false)
                {
                    return JSONHelper.FromString(false, BankResult);
                }
                else
                {
                    int presentedPoint = new AppPoint().CalcPresentedPointForPrec_lf(Months);
                    prepay_str = prepay_str.Insert(prepay_str.Length - 1, ",\"presented_points\":" + presentedPoint + "");

                    //更新订单银行流水号
                    IDbConnection Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                    string Query = "UPDATE Tb_OL_AlipayOrder SET prepay_str=@prepay_str WHERE out_trade_no = @out_trade_no ";
                    Conn.Execute(Query, new { prepay_str = prepay_str.ToString(), out_trade_no = PropertyOrderId });
                    //返回请求字符串
                    return JSONHelper.FromJsonString(true, prepay_str);
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
        public bool GenerateConfig(string CommunityId)
        {
            IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString);
            string query = "SELECT * FROM Tb_AlipayCertifiate WHERE CommunityId=@CommunityId";
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
        /// 配置支付请求参数,根据当前小区获取
        /// </summary>
        /// <param name="Row"></param>
        public bool GenerateConfig(string CommunityId, out Config c)
        {
            IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString);
            string query = "SELECT * FROM Tb_AlipayCertifiate WHERE CommunityId=@CommunityId";
            Tb_AlipayCertifiate T = conn.Query<Tb_AlipayCertifiate>(query, new { CommunityId = CommunityId }).SingleOrDefault();
            Config con = new Config();
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
        public string GenerateBankOrder(string CommunityId, string CustID, string RoomID, string UserID, int Months, string out_trade_no, string txnTime, string total_fee,
            ref bool R, ref string prepay_str)
        {
            DataTable CommunityTable = new DbHelperSQLP(PubConstant.UnifiedContionString).Query(string.Format("SELECT * FROM Tb_Community WHERE Id='{0}' OR CommID='{0}'", CommunityId.ToString())).Tables[0];

            Dictionary<string, string> sPara = new Dictionary<string, string>();
            sPara.Add("partner", c.partner.ToString());
            sPara.Add("seller_id", c.seller_id.ToString());
            sPara.Add("out_trade_no", out_trade_no.ToString());

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

                            sPara.Add("subject", $"{commName}{roomName}预存费用{custName}");
                        }
                        else
                        {
                            sPara.Add("subject", "预存费用");
                        }
                    }
                }
                else
                {
                    sPara.Add("subject", "预存费用");
                }
            }

            sPara.Add("body", CommunityId + "," + UserID + "," + Months);
            sPara.Add("total_fee", total_fee.ToString());
            sPara.Add("notify_url", c.notify_url.ToString());
            sPara.Add("service", c.service.ToString());
            sPara.Add("payment_type", c.payment_type.ToString());//支付类型
            sPara.Add("_input_charset", c.input_charset.ToString());//参数编码字符集
            sPara.Add("it_b_pay", "30m");//未付款交易的超时时间

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
        public string GeneratePropertyOrder(string CommunityId, string CostID, string StanID, string HandID, string RoomID, string txnTime, string CustID, int months,
            ref bool IsOk, ref string Amount, ref string PropertyOrderId)
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

                //生成物业订单
                DataSet Ds = PropertyOrder(CustID, CostID, community.CommID, RoomID, c.partner, txnTime, CommunityId, totalAmount.ToString());
                if (Ds.Tables.Count > 0)
                {
                    DataRow DRow = Ds.Tables[0].Rows[0];
                    Amount = totalAmount.ToString();//总金额
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
            DataSet Ds = new DbHelperSQLP(PubConstant.hmWyglConnectionString).RunProcedure("Proc_OL_GenerateOrder_AliPay_Prec", parameters, "Ds");

            return Ds;
        }

    }
}
