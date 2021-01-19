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


namespace Business
{
   public class WeiXinPay_Xxw : PubInfo
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(WeiXinPay));
        /// <summary>
        /// openid用于调用统一下单接口
        /// </summary>
        public WeiXinPay_Xxw() //获取小区、项目信息
        {
            base.Token = "20170416WeiXinPay_Xxw";
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
                ////刷新订单
                //case "RefreshOrder":
                //    Trans.Result = RefreshOrder(Row["CommunityId"].ToString(), Row["OrderId"].ToString());
                //    break;
                ////取消订单
                //case "CancelOrder":
                //    Trans.Result = CancelOrder(Row["CommunityId"].ToString(), Row["OrderId"].ToString());
                //    break;
                //更新订单状态
                case "UpdateProperyOrder":
                    Trans.Result = UpdateProperyOrder(Row["CommunityId"].ToString(), Row["OrderId"].ToString(), Row["respCode"].ToString(), Row["respMsg"].ToString());
                    break;
                ////查询银行订单状态
                //case "SearchBankOrder":
                //    Trans.Result = SearchBankOrder(Row["CommunityId"].ToString(), Row["OrderId"].ToString());
                //    break;
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
            string strcon = PubConstant.GetConnectionString("APPConnection");
            string BankResult = SearchBankOrder(CommunityId, out_trade_no);
            IDbConnection conn = new SqlConnection(strcon);
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
                string strcon = PubConstant.GetConnectionString("APPConnection");
                Global_Var.CorpSQLConnstr = strcon;
                string BankResult = SearchBankOrder(CommunityId, out_trade_no);

                if (BankResult == "SUCCESS")
                {
                    return JSONHelper.FromString(false, "银行已交易成功,不能取消");
                }

                IDbConnection conn = new SqlConnection(strcon);
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
                new DbHelperSQLP(strcon).RunProcedure("Proc_OL_WeiXinPayCancelOrder", parameters);
                return JSONHelper.FromString(true, "取消订单成功");
            }
            catch (Exception E)
            {
                return JSONHelper.FromString(false, E.Message.ToString());
            }

        }

        public static string GetConnection(string CommunityId)
        {
            string strcon = PubConstant.GetConnectionString("APPConnection");
            IDbConnection conn = new SqlConnection(strcon);
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
            string strcon = PubConstant.GetConnectionString("APPConnection");
            IDbConnection conn = new SqlConnection(strcon);
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
            string strcon = PubConstant.GetConnectionString("APPConnection");
            IDbConnection Conn = new SqlConnection(strcon);
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
                string strcon = PubConstant.GetConnectionString("APPConnection");

                IDbConnection conn = new SqlConnection(strcon);
                string query = "SELECT * FROM Tb_OL_WeiXinPayOrder WHERE out_trade_no=@out_trade_no";
                Tb_OL_WeiXinPayOrder T_Order = conn.Query<Tb_OL_WeiXinPayOrder>(query, new { out_trade_no = out_trade_no }).SingleOrDefault();
                if (T_Order.IsSucc.ToString() == "1")
                {
                    return SetNotifyResult("FAIL", "物业账单已下账");
                }
                ReceFees(CommunityId, out_trade_no);

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
        public static void ReceFees(string CommunityId, string OrderId)
        {
            string strcon = PubConstant.GetConnectionString("APPConnection");
            SqlParameter[] parameters = {
                    new SqlParameter("@CommunityId", SqlDbType.VarChar),
                    new SqlParameter("@OrderId", SqlDbType.VarChar)
            };
            parameters[0].Value = CommunityId;
            parameters[1].Value = OrderId;
            new DbHelperSQLP(strcon).RunProcedure("Proc_OL_WeiXinPayReceFees", parameters);
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

            WxPayConfig wxPayConfig = GenerateConfig(CommunityId);
            string Amount = "0";

            if (null== wxPayConfig)
            {
                return JSONHelper.FromString(false, "未配置证书文件");
            }
            //生成物业账单
            string PropertyResult = GeneratePropertyOrder(CommunityId, FeesIds, txnTime, CustID, ref IsPropertyOk, ref Amount, ref PropertyOrderId, wxPayConfig);
            if (IsPropertyOk == true)
            {
                //生成银行订单,返回银行流水号
                WxPayData Data = new WxPayData();
                string BankResult = GenerateBankOrder(CommunityId, PropertyOrderId, txnTime, Amount, ref IsBankOk, ref BankOrderId, ref Data, wxPayConfig);
                if (IsBankOk == false)
                {
                    return JSONHelper.FromString(false, BankResult);
                }
                else
                {
                    string strcon = PubConstant.GetConnectionString("APPConnection");
                    //更新订单银行流水号
                    IDbConnection Conn = new SqlConnection(strcon);
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
            string strcon = PubConstant.GetConnectionString("APPConnection");
            IDbConnection conn = new SqlConnection(strcon);
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
                wxPayConfig.NOTIFY_URL = PubConstant.GetConnectionString("WeiXinPayBackURL");
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
        public string GenerateBankOrder(string CommunityId, string out_trade_no, string txnTime, string total_fee, ref bool R, ref string BankOrderId, ref WxPayData WPD,WxPayConfig wxPayConfig)
        {
            R = false;
            //统一下单
            WxPayData data = new WxPayData();
            data.SetValue("body", "E享-物管费冲抵");
            data.SetValue("attach", CommunityId.ToString());
            data.SetValue("out_trade_no", out_trade_no);
            data.SetValue("total_fee", total_fee);
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            data.SetValue("trade_type", "APP");

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
        public string GeneratePropertyOrder(string CommunityId, string FeesIds, string txnTime, string CustID, ref bool IsOk, ref string Amount, ref string PropertyOrderId,WxPayConfig wxPayConfig)
        {
            string strcon = PubConstant.GetConnectionString("APPConnection");
            Global_Var.CorpSQLConnstr = strcon;
                //生成物业订单
                DataSet Ds = PropertyOrder(CustID, FeesIds, CommunityId, wxPayConfig.MCHID.ToString().ToString(), txnTime, CommunityId);
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
            string strcon = PubConstant.GetConnectionString("APPConnection");
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
            DataSet Ds = new DbHelperSQLP(strcon).RunProcedure("Proc_OL_GenerateOrder_WeiXin", parameters, "Ds");
            return Ds;
        }


    }
}
