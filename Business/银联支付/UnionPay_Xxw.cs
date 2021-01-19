using System;
using MobileSoft.DBUtility;
using MobileSoft.Common;
using System.Data;
using System.Collections.Generic;
using com.unionpay.acp.sdk;
using System.Data.SqlClient;
using MobileSoft.Model.Unified;
using MobileSoft.Model.OL;
using System.Linq;
using Dapper;
using DapperExtensions;
using log4net;

namespace Business
{
   public class UnionPay_Xxw : PubInfo
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UnionPay));

        SDKConfig s { get; set; }
        public UnionPay_Xxw() //获取小区、项目信息
        {
            base.Token = "20170416UnionPay_Xxw";
            s = new SDKConfig();
            //银联后联地址
            s.appRequestUrl = "https://gateway.95516.com/gateway/api/appTransReq.do";
            s.singleQueryUrl = "https://gateway.95516.com/gateway/api/queryTrans.do";

            //前后端通知地址
            s.frontUrl = PubConstant.GetConnectionString("UnionPayFrontURL");
            s.BackUrl = PubConstant.GetConnectionString("UnionPayBackURL");

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
                ////取消下帐
                //case "NoAliUnderAccount":
                //    Trans.Result = NoAliUnderAccount(Row["CommunityId"].ToString(), Row["OrderId"].ToString());
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

        public string RefreshOrder(string CommunityId, string OrderId)
        {
            //构建链接字符串
            string strcon = PubConstant.GetConnectionString("APPConnection");
            string BankResult = SearchBankOrder(CommunityId, OrderId);

            IDbConnection conn = new SqlConnection(strcon);
            string query = "SELECT * FROM Tb_OL_UnionPayOrder WHERE orderId=@OrderId";
            Tb_OL_UnionPayOrder T_Order = conn.Query<Tb_OL_UnionPayOrder>(query, new { orderId = OrderId }).SingleOrDefault();

            if (BankResult == "00")
            {
                //更新订单状态
                UpdateProperyOrder(T_Order.CommunityId.ToString(), T_Order.orderId.ToString(), BankResult, "成功");
                if (T_Order.IsSucc.ToString() != "1")
                {
                    //下账
                    ReceProperyOrder(T_Order.CommunityId.ToString(), T_Order.orderId.ToString());
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
                UpdateProperyOrder(T_Order.CommunityId.ToString(), T_Order.orderId.ToString(), BankResult, "失败");
            }

            return "";
        }

        public string CancelOrder(string CommunityId, string OrderId)
        {
            try
            {
                //构建链接字符串
                string strcon = PubConstant.GetConnectionString("APPConnection");
                Global_Var.CorpSQLConnstr = strcon;
                string BankResult = SearchBankOrder(CommunityId, OrderId);

                if (BankResult == "00")
                {
                    return JSONHelper.FromString(false, "银行已交易成功,不能取消");
                }
                if (BankResult == "01")
                {
                    return JSONHelper.FromString(false, "订单状态异常,请稍后再试");
                }
                IDbConnection conn = new SqlConnection(strcon);
                string query = "SELECT * FROM Tb_OL_UnionPayOrder WHERE orderId=@OrderId";
                Tb_OL_UnionPayOrder T_Order = conn.Query<Tb_OL_UnionPayOrder>(query, new { orderId = OrderId }).SingleOrDefault();

                if (T_Order.respCode == "00")
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
                parameters[0].Value = OrderId;
                new DbHelperSQLP(strcon).RunProcedure("Proc_OL_UnionPayCancelOrder", parameters);
                return JSONHelper.FromString(true, "取消订单成功");
            }
            catch (Exception E)
            {
                return JSONHelper.FromString(false, E.Message.ToString());
            }

        }

      

        public string SearchBankOrder(string CommunityId, string OrderId)
        {
            string Result = "";
            //SDKConfig con = new SDKConfig();
            bool IsConfig = GenerateConfig(CommunityId);
            if (IsConfig == false)
            {
                Result = "未配置证书文件";
                return Result;
            }
            //构建链接字符串
            string strcon = PubConstant.GetConnectionString("APPConnection");
            IDbConnection conn = new SqlConnection(strcon);
            string query = "SELECT * FROM Tb_OL_UnionPayOrder WHERE orderId=@OrderId";
            Tb_OL_UnionPayOrder T_Order = conn.Query<Tb_OL_UnionPayOrder>(query, new { orderId = OrderId }).SingleOrDefault();

            if (T_Order == null)
            {
                Result = "未找到该物业订单";
                return Result;
            }

            Dictionary<string, string> param = new Dictionary<string, string>();

            //以下信息非特殊情况不需要改动
            param["version"] = "5.0.0";//版本号
            param["encoding"] = "UTF-8";//编码方式
            param["certId"] = CertUtil.GetSignCertId(s);//证书ID
            param["signMethod"] = "01";//签名方法
            param["txnType"] = "00";//交易类型
            param["txnSubType"] = "00";//交易子类
            param["bizType"] = "000000";//业务类型
            param["accessType"] = "0";//接入类型
            param["channelType"] = "07";//渠道类型

            //TODO 以下信息需要填写
            param["orderId"] = OrderId.ToString();	//请修改被查询的交易的订单号，8-32位数字字母，不能含“-”或“_”，此处默认取demo演示页面传递的参数
            param["merId"] = T_Order.merId.ToString();//商户代码，请改成自己的测试商户号，此处默认取demo演示页面传递的参数
            param["txnTime"] = T_Order.txnTime.ToString();//请修改被查询的交易的订单发送时间，格式为YYYYMMDDhhmmss，此处默认取demo演示页面传递的参数

            AcpService.Sign(param, System.Text.Encoding.UTF8, s);  // 签名
            string url = s.SingleQueryUrl;

            Dictionary<String, String> rspData = AcpService.Post(param, url, System.Text.Encoding.UTF8, s);

            if (rspData.Count != 0)
            {
                if (AcpService.Validate(rspData, System.Text.Encoding.UTF8, s))
                {
                    //商户端验证返回报文签名成功
                    string respcode = rspData["respCode"]; //其他应答参数也可用此方法获取
                    if ("00" == respcode)
                    {
                        string origRespCode = rspData["origRespCode"]; //其他应答参数也可用此方法获取
                        //处理被查询交易的应答码逻辑
                        if ("00" == origRespCode)
                        {
                            //交易成功，更新商户订单状态
                            Result = "00";
                        }
                        else if ("03" == origRespCode || "04" == origRespCode || "05" == origRespCode)
                        {
                            //需再次发起交易状态查询交易
                            Result = "01";
                        }
                        else
                        {
                            //交易失败
                            Result = "交易失败";
                        }
                    }
                    else if ("03" == respcode || "04" == respcode || "05" == respcode)
                    {
                        //不明原因超时，后续继续发起交易查询。
                        Result = "01";
                    }
                    else
                    {
                        //其他应答码做以失败处理
                        Result = "查询操作失败:" + rspData["respMsg"].ToString();
                    }
                }
            }
            else
            {
                Result = "请求失败";
            }

            return Result;
        }

        /// <summary>
        /// 更新物业订单状态
        /// </summary>
        /// <param name="CommunityId"></param>
        /// <param name="OrderId"></param>
        /// <param name="respCode"></param>
        /// <param name="respmsg"></param>
        public static string UpdateProperyOrder(string CommunityId, string OrderId, string respCode, string respMsg)
        {
            //构建链接字符串
            string strcon = PubConstant.GetConnectionString("APPConnection");
            IDbConnection Conn = new SqlConnection(strcon);
            string Query = "UPDATE Tb_OL_UnionPayOrder SET respCode=@respCode,respMsg=@respMsg  WHERE OrderId = @OrderId ";
            Conn.Execute(Query, new { respCode = respCode, respMsg = respMsg, OrderId = OrderId });
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
        public static string ReceProperyOrder(string CommunityId, string OrderId)
        {
            try
            {
                //构建链接字符串
                string strcon = PubConstant.GetConnectionString("APPConnection");

                IDbConnection conn = new SqlConnection(strcon);
                string query = "SELECT * FROM Tb_OL_UnionPayOrder WHERE orderId=@OrderId";
                Tb_OL_UnionPayOrder T_Order = conn.Query<Tb_OL_UnionPayOrder>(query, new { orderId = OrderId }).SingleOrDefault();
                if (T_Order.IsSucc.ToString() == "1")
                {
                    return "物业账单已下账";
                }
                ReceFees(CommunityId, OrderId);
                return "success";
            }
            catch (Exception E)
            {
                return E.Message.ToString();
            }
        }

        //取消下帐
        public static string NoAliUnderAccount(string CommunityId, string OrderId)
        {
            try
            {
                //构建链接字符串
                string strcon = PubConstant.GetConnectionString("APPConnection");

                IDbConnection conn = new SqlConnection(strcon);
                string query = "SELECT * FROM Tb_OL_UnionPayOrder WHERE orderId=@OrderId";
                Tb_OL_UnionPayOrder T_Order = conn.Query<Tb_OL_UnionPayOrder>(query, new { orderId = OrderId }).SingleOrDefault();
                if (T_Order.IsSucc.ToString() == "0")
                {
                    return "物业账单未下账";
                }
                conn.Dispose();
                conn = new SqlConnection(strcon);

                //更改状态
                T_Order.IsSucc = 1;
                conn.Update<Tb_OL_UnionPayOrder>(T_Order);
                return "success";
            }
            catch (Exception E)
            {
                return E.Message.ToString();
            }
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
            //构建链接字符串
            string strcon = PubConstant.GetConnectionString("APPConnection");
            SqlParameter[] parameters = {
                    new SqlParameter("@CommunityId", SqlDbType.VarChar),
                    new SqlParameter("@OrderId", SqlDbType.VarChar)
            };
            parameters[0].Value = CommunityId;
            parameters[1].Value = OrderId;
            new DbHelperSQLP(strcon).RunProcedure("Proc_OL_UnionPayReceFees", parameters);
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

            //SDKConfig con = new SDKConfig();
            bool IsConfig = GenerateConfig(CommunityId);
            string Amount = "0";

            if (IsConfig == false)
            {
                return JSONHelper.FromString(false, "未配置证书文件");
            }
            //生成物业账单
            string PropertyResult = GeneratePropertyOrder(CommunityId, FeesIds, txnTime, CustID, ref IsPropertyOk, ref Amount, ref PropertyOrderId);
            if (IsPropertyOk == true)
            {
                //生成银行订单,返回银行流水号
                string BankResult = GenerateBankOrder(CommunityId, PropertyOrderId, txnTime, Amount, ref IsBankOk, ref BankOrderId);
                if (IsBankOk == false)
                {
                    return JSONHelper.FromString(false, BankResult);
                }
                else
                {
                    //构建链接字符串
                    string strcon = PubConstant.GetConnectionString("APPConnection");
                    //更新订单银行流水号
                    IDbConnection Conn = new SqlConnection(strcon);
                    string Query = "UPDATE Tb_OL_UnionPayOrder SET Tn=@Tn WHERE OrderId = @OrderId ";
                    Conn.Execute(Query, new { Tn = BankOrderId, OrderId = PropertyOrderId });
                    //向手机端返回银行流水号
                    return JSONHelper.FromString(true, BankOrderId);
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
            //构建链接字符串
            string strcon = PubConstant.GetConnectionString("APPConnection");
            DataTable CertificateTable = new DbHelperSQLP(strcon).Query("SELECT * FROM Tb_UnionPayCertificate WHERE CommunityId='" + DataSecurity.FilteSQLStr(CommunityId) + "'").Tables[0];
            if (CertificateTable.Rows.Count > 0)
            {
                DataRow DRow = CertificateTable.Rows[0];
                //获取配置参数
                s.signCertPath = DRow["signCertPath"].ToString();
                s.signCertPwd = DRow["signCertPwd"].ToString();
                s.validateCertDir = DRow["validateCertDir"].ToString();
                s.encryptCert = DRow["encryptCert"].ToString();
                s.merId = DRow["merId"].ToString();
                s.AccNo = DRow["AccNo"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 配置支付请求参数,根据当前小区获取【仅供验签使用】
        /// </summary>
        /// <param name="Row"></param>
        public bool GenerateConfig(string CommunityId, out SDKConfig con)
        {
            //构建链接字符串
            string strcon = PubConstant.GetConnectionString("APPConnection");
            DataTable CertificateTable = new DbHelperSQLP(strcon).Query("SELECT * FROM Tb_UnionPayCertificate WHERE CommunityId='" + DataSecurity.FilteSQLStr(CommunityId) + "'").Tables[0];
            SDKConfig c = new SDKConfig();
            con = c;
            if (CertificateTable.Rows.Count > 0)
            {
                DataRow DRow = CertificateTable.Rows[0];
                //获取配置参数
                con.signCertPath = DRow["signCertPath"].ToString();
                con.signCertPwd = DRow["signCertPwd"].ToString();
                con.validateCertDir = DRow["validateCertDir"].ToString();
                con.encryptCert = DRow["encryptCert"].ToString();
                con.merId = DRow["merId"].ToString();
                con.AccNo = DRow["AccNo"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 生成银行订单
        /// </summary>
        /// <param name="merId">商户号</param>
        /// <param name="orderId">订单ID</param>
        /// <param name="txnTime">订单时间</param>
        /// <param name="txtAmt">总金额分</param>
        /// <param name="R">是否成功生成银行订单</param>
        /// <returns></returns>
        public string GenerateBankOrder(string CommunityId, string orderId, string txnTime, string txnAmt, ref bool R, ref string BankOrderId)
        {
            string Result = "";
            R = false;
            Dictionary<string, string> param = new Dictionary<string, string>();
            //以下信息非特殊情况不需要改动
            param["version"] = "5.0.0";//版本号
            param["encoding"] = "UTF-8";//编码方式
            param["txnType"] = "01";//交易类型
            param["txnSubType"] = "01";//交易子类
            param["bizType"] = "000201";//业务类型
            param["signMethod"] = "01";//签名方法
            param["channelType"] = "08";//渠道类型
            param["accessType"] = "0";//接入类型
            param["frontUrl"] = s.FrontUrl;  //前台通知地址      
            param["backUrl"] = s.BackUrl;  //后台通知地址
            param["currencyCode"] = "156";//交易币种

            param["merId"] = s.merId.ToString();//商户号
            param["orderId"] = orderId;//商户订单号，8-32位数字字母，不能含“-”或“_”
            param["txnTime"] = txnTime;//订单发送时间，格式为YYYYMMDDhhmmss，
            param["txnAmt"] = txnAmt;//交易金额，单位分，此处默认取demo演示页面传递的参数

            param["reqReserved"] = CommunityId.ToString();//自定义信息

            AcpService.Sign(param, System.Text.Encoding.UTF8, s);  // 签名
            string url = s.AppRequestUrl;

            Dictionary<String, String> rspData = AcpService.Post(param, url, System.Text.Encoding.UTF8, s);

            if (rspData.Count != 0)
            {
                if (AcpService.Validate(rspData, System.Text.Encoding.UTF8, s))
                {
                    string respcode = rspData["respCode"]; //其他应答参数也可用此方法获取
                    if ("00" == respcode)
                    {
                        //返回银行流水号
                        R = true;
                        BankOrderId = rspData["tn"];
                    }
                    else
                    {
                        //其他应答码做以失败处理
                        R = false;
                        Result = "创建银行订单失败,订单号:" + orderId + ",时间:" + txnTime.ToString() + ",返回码:" + rspData["respMsg"].ToString();
                    }
                }
                else
                {
                    R = false;
                    Result = "商户端验证返回报文签名失败";
                }
            }
            else
            {
                R = false;
                Result = "请求失败";
            }

            return Result;
        }

        /// <summary>
        /// 生成物业订单
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="IsOk">是否成功生成物业代收订单</param>
        /// <param name="Amount">订单总金额</param>
        /// <returns></returns>
        public string GeneratePropertyOrder(string CommunityId, string FeesIds, string txnTime, string CustID, ref bool IsOk, ref string Amount, ref string PropertyOrderId)
        {
            string strcon = PubConstant.GetConnectionString("APPConnection");
           
                //生成物业订单
                DataSet Ds = PropertyOrder(CustID, FeesIds, CommunityId, s.merId.ToString(), txnTime, CommunityId);
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
            //构建链接字符串
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
            DataSet Ds = new DbHelperSQLP(strcon).RunProcedure("Proc_OL_GenerateOrder", parameters, "Ds");
            return Ds;
        }


    }
}