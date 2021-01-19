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
using Com.Alipay;
using DapperExtensions;

namespace Business
{
    public class AlipayPrec_MY : PubInfo
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AlipayPrec));
        /// <summary>
        /// openid用于调用统一下单接口
        /// </summary>
        public string openid { get; set; }

        public Config c { get; set; }

        public AlipayPrec_MY() //获取小区、项目信息
        {
            base.Token = "20160804AlipayPrec";
            c = new Config();
            c.notify_url = Global_Fun.AppWebSettings("AliPayPrec_Notify_Url").ToString();
            //c.notify_url = "http://125.64.16.10:9999/TWInterface/Service/AlipayCallBack/AliPayPrec.ashx";
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

        public static string GetConnection(string CommunityId)
        {
            //IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString.ToString());
            //string query = "SELECT * FROM Tb_Community WHERE Id=@id";
            //var T = conn.Query<Tb_Community>(query, new { id = CommunityId }).ToList();
            //if (T.Count > 0)
            //{
            //    return UnionUtil.GetConnectionString(T[0]).ToString();
            //}
            //return "";
            return Global_Fun.GetConnectionString("");
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
                string ConnStr = GetConnection(CommunityId);

                IDbConnection conn = new SqlConnection(GetConnection(CommunityId));
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

                IDbConnection conn = new SqlConnection(GetConnection(CommunityId));
                string query = "SELECT * FROM Tb_OL_AlipayOrder WHERE out_trade_no=@OrderId";
                Tb_OL_AlipayOrder T_Order = conn.Query<Tb_OL_AlipayOrder>(query, new { orderId = OrderId }).SingleOrDefault();
                if (T_Order.IsSucc.ToString() == "0")
                {
                    return "物业账单未下账";
                }
                conn.Dispose();
                conn = new SqlConnection(GetConnection(CommunityId));

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
            string ConnStr = GetConnection(CommunityId);
            //公司数据库连接字符串
            Global_Var.CorpSQLConnstr = ConnStr;
            //查询账单信息
            IDbConnection conn = new SqlConnection(ConnStr);
            string query = "SELECT * FROM Tb_OL_AliPayOrder WHERE out_trade_no=@out_trade_no";
            Tb_OL_AlipayOrder T_Order = conn.Query<Tb_OL_AlipayOrder>(query, new { out_trade_no = out_trade_no }).SingleOrDefault();

            query = "SELECT * FROM Tb_OL_AliPayDetail_Prec WHERE PayOrderId=@PayOrderId";
            Tb_OL_AliPayDetail_Prec T_Prec = conn.Query<Tb_OL_AliPayDetail_Prec>(query, new { PayOrderId = T_Order.Id.ToString() }).SingleOrDefault();

            //预存收款
            string strUserCode = "_Sys_";
            string Result = "";
            string ErrorMsg = "";
            long iReceID = 0;
            decimal PrecAmount = Convert.ToDecimal(T_Prec.DueAmount);

            ReceFeesPrec.ReceivePrecFees(AppGlobal.StrToInt(T_Order.CommID.ToString()), T_Order.CustId, T_Prec.RoomID, T_Prec.CostId.ToString(), "自助缴费", PrecAmount, strUserCode, ref Result, ref ErrorMsg, ref iReceID);

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
            string RoomID = Row["RoomID"].ToString();
            string txnTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string CustID = Row["CustID"].ToString();
            string Amount = Row["Amount"].ToString();



            bool IsConfig = GenerateConfig(CommunityId);

            if (IsConfig == false)
            {
                return JSONHelper.FromString(false, "未配置证书文件");
            }
            //生成物业账单
            string PropertyResult = GeneratePropertyOrder(CommunityId, CostID, RoomID, txnTime, CustID, ref IsPropertyOk, ref Amount, ref PropertyOrderId);
            if (IsPropertyOk == true)
            {
                //返回签名的订单信息
                string BankResult = GenerateBankOrder(CommunityId, PropertyOrderId, txnTime, Amount, ref IsBankOk, ref prepay_str);
                if (IsBankOk == false)
                {
                    return JSONHelper.FromString(false, BankResult);
                }
                else
                {
                    //更新订单银行流水号
                    IDbConnection Conn = new SqlConnection(GetConnection(CommunityId));
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
            IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString.ToString());
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
            IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString.ToString());
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
        public string GenerateBankOrder(string CommunityId, string out_trade_no, string txnTime, string total_fee, ref bool R, ref string prepay_str)
        {
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            sPara.Add("partner", c.partner.ToString());
            sPara.Add("seller_id", c.seller_id.ToString());
            sPara.Add("out_trade_no", out_trade_no.ToString());
            sPara.Add("subject", "预存费用");
            sPara.Add("body", CommunityId);
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
        public string GeneratePropertyOrder(string CommunityId, string CostID, string RoomID, string txnTime, string CustID, ref bool IsOk, ref string Amount, ref string PropertyOrderId)
        {
            DataTable CommunityTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query("SELECT * FROM Tb_Community WHERE Id='" + CommunityId.ToString() + "'").Tables[0];
            if (CommunityTable.Rows.Count > 0)
            {
                //连接字符串
                DataRow T = CommunityTable.Rows[0];

                //生成物业订单
                DataSet Ds = PropertyOrder(CustID, CostID, T["CommID"].ToString(), RoomID, c.partner, txnTime, CommunityId, Amount);
                if (Ds.Tables.Count > 0)
                {
                    DataRow DRow = Ds.Tables[0].Rows[0];
                    Amount = DRow["Amount"].ToString();//总金额
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
            string ConnStr = GetConnection(CommunityId);
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
            DataSet Ds = new DbHelperSQLP(ConnStr).RunProcedure("Proc_OL_GenerateOrder_AliPay_Prec", parameters, "Ds");

            return Ds;
        }

    }
}