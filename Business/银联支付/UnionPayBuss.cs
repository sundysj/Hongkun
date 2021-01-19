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
using System.Linq;
using WxPayAPI;
using log4net;
using Com.Alipay;
using Dapper;
using DapperExtensions;
using Model.Model.Buss;
using MobileSoft.Model.Unified;

namespace Business
{
    /// <summary>
    /// 支付定商家订单
    /// </summary>
    public class UnionPayBuss : PubInfo
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Alipay));

        public string openid { get; set; }
        SDKConfig s { get; set; }
        /// <summary>
        /// 构造器
        /// </summary>
        public UnionPayBuss()
        {
            base.Token = "20160804UnionPayBuss";
            s = new SDKConfig();
            //银联后联地址
            s.appRequestUrl = "https://gateway.95516.com/gateway/api/appTransReq.do";
            s.singleQueryUrl = "https://gateway.95516.com/gateway/api/queryTrans.do";

            //前后端通知地址
            s.frontUrl = "http://125.64.16.10:9999/TWInterface/Service/UnionPayCallBack/PayOk.aspx";
            s.BackUrl = "http://125.64.16.10:9999/TWInterface/Service/UnionPayCallBack/UnionPay.ashx";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = "false:";

            DataSet Ds = base.XmlToDataSet(Trans.Attribute);
            DataRow Row = Ds.Tables[0].Rows[0];

            switch (Trans.Command)
            {
                //创建订单
                case "GenerateOrder":
                    Trans.Result = GenerateOrder(Ds);
                    break;
                //订单确认收款成功
                case "ReceBusinessOrder":
                    Trans.Result = ReceBusinessOrder(Row["OrderId"].ToString());
                    break;
                //订单退款
                case "CancelOrder":
                    Trans.Result = CancelOrder(Row["OrderId"].ToString());
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 支付宝支付Log信息
        /// </summary>
        /// <param name="Info"></param>
        public static void Log(string Info)
        {
            log.Info(Info);
        }

        /// <summary>
        /// 订单退款
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <returns></returns>
        public static string CancelOrder(string out_trade_no)
        {
            string ConnStr = ConnectionDb.GetBusinessConnection();
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="ReturnCode"></param>
        /// <param name="ReturnMsg"></param>
        /// <returns></returns>
        public static string UpdateBusinessOrder(string OrderId, string ReturnCode, string ReturnMsg)
        {
            try
            {
                string ConnStr = ConnectionDb.GetBusinessConnection();

                IDbConnection Conn = new SqlConnection(ConnectionDb.GetBusinessConnection());
                string query = "SELECT * FROM Tb_Charge_Receipt WHERE OrderId=@OrderId";
                Tb_Charge_Receipt T_Order = Conn.Query<Tb_Charge_Receipt>(query, new { OrderId = OrderId }).SingleOrDefault();

                T_Order.ReturnCode = ReturnCode;
                T_Order.ReturnMsg = ReturnMsg;

                Conn.Update(T_Order);
                return "success";
            }
            catch (Exception E)
            {
                return E.Message.ToString();
            }
        }

        /// <summary>
        /// 订单确认收款
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public static string ReceBusinessOrder(string OrderId)
        {
            try
            {
                string ConnStr = ConnectionDb.GetBusinessConnection();
                IDbConnection Conn = new SqlConnection(ConnectionDb.GetBusinessConnection());
                string query = "SELECT * FROM Tb_Charge_Receipt WHERE OrderId=@OrderId";
                Tb_Charge_Receipt T_Order = Conn.Query<Tb_Charge_Receipt>(query, new { OrderId = OrderId }).SingleOrDefault();
                if (T_Order.IsPay.ToString() == "已付款")
                {
                    return "订单已付款";
                }
                //订单确认收款,收款动作写在下面
                T_Order.IsPay = "已付款";
                T_Order.PayDate = DateTime.Now;
                T_Order.Method = "银联";

                Conn.Execute("UPDATE Tb_Charge_Receipt SET IsPay=@IsPay,PayDate=@PayDate,Method='银联' WHERE Id=@Id", new { IsPay = T_Order.IsPay.ToString(), PayDate = T_Order.PayDate.ToString(), Id = T_Order.Id.ToString() });

                return "success";
            }
            catch (Exception E)
            {
                return E.Message.ToString();
            }
        }


        /// <summary>
        /// 配置支付请求参数,根据当前商家获取
        /// </summary>
        /// <param name="BussId"></param>
        public  bool GenerateConfig(string BussId)
        {
            IDbConnection conn = new SqlConnection(ConnectionDb.GetBusinessConnection());
            string query = "SELECT * FROM Tb_UnionPayCertificate WHERE BussId=@BussId";
            Model.Model.Buss.Tb_UnionPayCertificate T = conn.Query<Model.Model.Buss.Tb_UnionPayCertificate>(query, new { BussId = BussId }).SingleOrDefault();

            if (T == null)
            {
                return false;
            }
            else
            {
                //获取配置参数
                s.signCertPath = T.signCertPath.ToString();
                s.signCertPwd = T.signCertPwd.ToString();
                s.validateCertDir = T.validateCertDir.ToString();
                s.encryptCert = T.encryptCert.ToString();
                s.merId = T.merId.ToString();
                s.AccNo = T.accNo.ToString();
                return true;
            }
        }


        /// <summary>
        /// 配置支付请求参数,根据当前商家获取
        /// </summary>
        /// <param name="BussId"></param>
        public bool GenerateConfig(string BussId,out SDKConfig con)
        {
            IDbConnection conn = new SqlConnection(ConnectionDb.GetBusinessConnection());
            string query = "SELECT * FROM Tb_UnionPayCertificate WHERE BussId=@BussId";
            Model.Model.Buss.Tb_UnionPayCertificate T = conn.Query<Model.Model.Buss.Tb_UnionPayCertificate>(query, new { BussId = BussId }).SingleOrDefault();
            SDKConfig c = new SDKConfig();
            con = c;
            if (T == null)
            {
                return false;
            }
            else
            {
                //获取配置参数
                con.signCertPath = T.signCertPath.ToString();
                con.signCertPwd = T.signCertPwd.ToString();
                con.validateCertDir = T.validateCertDir.ToString();
                con.encryptCert = T.encryptCert.ToString();
                con.merId = T.merId.ToString();
                con.AccNo = T.accNo.ToString();
                return true;
            }
        }






        /// <summary>
        /// 生成三方预支付单
        /// </summary>
        /// <param name="BussId"></param>
        /// <param name="out_trade_no"></param>
        /// <param name="txnTime"></param>
        /// <param name="total_fee"></param>
        /// <param name="R"></param>
        /// <param name="prepay_str"></param>
        /// <returns></returns>
        public string GenerateBankOrder(string BussId, string out_trade_no, string txnTime, string total_fee, ref bool R, ref string BankOrderId)
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
            param["orderId"] = out_trade_no;//商户订单号，8-32位数字字母，不能含“-”或“_”
            param["txnTime"] = txnTime;//订单发送时间，格式为YYYYMMDDhhmmss，
            param["txnAmt"] = total_fee;//交易金额，单位分，此处默认取demo演示页面传递的参数

            param["reqReserved"] = BussId.ToString();//自定义信息

            AcpService.Sign(param, System.Text.Encoding.UTF8,s);  // 签名
            string url = s.AppRequestUrl;

            Dictionary<String, String> rspData = AcpService.Post(param, url, System.Text.Encoding.UTF8,s);

            if (rspData.Count != 0)
            {
                if (AcpService.Validate(rspData, System.Text.Encoding.UTF8,s))
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
                        Result = "创建银行订单失败,订单号:" + out_trade_no + ",时间:" + txnTime.ToString() + ",返回码:" + rspData["respMsg"].ToString();
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
        /// 生成商家订单
        /// </summary>
        /// <param name="Ds"></param>
        /// <param name="BussId"></param>
        /// <param name="UserId"></param>
        /// <param name="txnTime"></param>
        /// <param name="IsOk"></param>
        /// <param name="Amount"></param>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public string GenerateBusinessOrder(DataSet Ds, string BussId, string UserId, string txnTime, ref bool IsOk, ref string Amount, ref string OrderId)
        {
            DataRow Row = Ds.Tables[0].Rows[0];

            IDbConnection Conn = new SqlConnection(ConnectionDb.GetBusinessConnection());
            string BusinessOrderId = Guid.NewGuid().ToString();

            //生成商家收款订单
            string ReceiptCode = Guid.NewGuid().ToString();
            Tb_Charge_Receipt EntityReceipt = new Tb_Charge_Receipt();
            EntityReceipt.Id = ReceiptCode;
            EntityReceipt.BussId = BussId.ToString();
            EntityReceipt.OrderId = BusinessOrderId;

            // 是否使用抵扣券
            if (Row.Table.Columns.Contains("UseCoupon"))
            {
                EntityReceipt.IsUseCoupon = AppGlobal.StrToInt(Row["UseCoupon"].ToString());
            }

            OrderId = BusinessOrderId;

            string ReceiptSign = "";//获得订单号
            //获取票据号
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Id", BussId);
            dp.Add("@OrderLength", 32);
            dp.Add("@Num", "", DbType.String, ParameterDirection.Output);
            using (IDbConnection IDbConn = new SqlConnection(ConnectionDb.GetBusinessConnection()))
            {
                IDbConn.Execute("Proc_GetOrderNum", dp, null, null, CommandType.StoredProcedure);
                ReceiptSign = dp.Get<string>("@Num");
            }
            EntityReceipt.ReceiptSign = ReceiptSign.ToString();
            
            EntityReceipt.UserId = UserId.ToString();


            IDbConnection ConnUnified = new SqlConnection(ConnectionDb.GetUnifiedConnectionString());
            string QueryUser = "SELECT * FROM Tb_User WHERE Id=@UserId";
            Tb_User EntityUser = ConnUnified.Query<Tb_User>(QueryUser, new { UserId = UserId }).SingleOrDefault();
            //查找用户名称
            EntityReceipt.Name = EntityUser.Name.ToString();
            EntityReceipt.Mobile = EntityUser.Mobile.ToString();
            EntityReceipt.IsPay = "未付款";
            EntityReceipt.IsReceive = "未收货";

            EntityReceipt.Amount = 0;
            EntityReceipt.ReceiptMemo = Row["ReceiptMemo"].ToString();
            EntityReceipt.ReceiptType = "通用票据";
            EntityReceipt.ReceiptDate = DateTime.Now;
            EntityReceipt.MchId = s.merId;
            EntityReceipt.Partner = s.merId;
            EntityReceipt.PrepayStr = "";
            EntityReceipt.txnTime = txnTime.ToString();
            EntityReceipt.ReturnCode = "";
            EntityReceipt.ReturnMsg = "";
            EntityReceipt.IsDeliver = "未发货";
            EntityReceipt.Express = "";
            EntityReceipt.ExpressNum = "";
            EntityReceipt.DeliverAddress = "";

            EntityReceipt.PayDate = DateTime.Now;

            EntityReceipt.IsDelete = 0;

            Conn.Insert(EntityReceipt);

            decimal TotalAmount = 0.00M;
            //收成商家收款明细

            foreach (DataRow DetailRow in Ds.Tables[1].Rows)
            {
                Tb_Charge_ReceiptDetail EntityReceiptDetail = new Tb_Charge_ReceiptDetail();
                EntityReceiptDetail.RpdCode = Guid.NewGuid().ToString();
                EntityReceiptDetail.ReceiptCode = ReceiptCode;
                EntityReceiptDetail.ResourcesID = DetailRow["Id"].ToString();
                EntityReceiptDetail.Quantity = DataSecurity.StrToInt(DetailRow["Quantity"].ToString());

                string QueryResourcesSql = "SELECT * FROM Tb_Resources_Details WHERE ResourcesID=@ResourcesID";
                Tb_Resources_Details T = Conn.Query<Tb_Resources_Details>(QueryResourcesSql, new { ResourcesID = DetailRow["Id"].ToString() }).SingleOrDefault();

                EntityReceiptDetail.SalesPrice = T.ResourcesSalePrice;
                EntityReceiptDetail.DiscountPrice = T.ResourcesDisCountPrice;
                EntityReceiptDetail.GroupPrice = T.GroupBuyPrice;
                EntityReceiptDetail.DetailAmount = T.ResourcesSalePrice - T.ResourcesDisCountPrice;
                EntityReceiptDetail.RpdMemo = DetailRow["RpdMemo"].ToString();
                Conn.Insert(EntityReceiptDetail);
                //计算订单的总金额
                TotalAmount = TotalAmount + DataSecurity.StrToDecimal(EntityReceiptDetail.DetailAmount.ToString());
            }

            //更新订单总金额
            EntityReceipt.Amount = TotalAmount;
            Amount = Convert.ToString(TotalAmount * 100);
            IsOk = true;
            Conn.Update(EntityReceipt);
            return "生成订单成功";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ds"></param>
        /// <returns></returns>
        public string GenerateOrder(DataSet Ds)
        {
            DataRow Row = Ds.Tables[0].Rows[0];

            bool IsBankOk = false;
            bool IsBusinessOk = false;
            string OrderId = "";
            string prepay_str = "";


            string txnTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string UserId = Row["UserId"].ToString();
            string BussId = Row["BussId"].ToString();

            bool IsConfig = GenerateConfig(BussId);

            string Amount = "0";

            if (IsConfig == false)
            {
                return JSONHelper.FromString(false, "未配置证书文件");
            }
            //生成商家账单
            string BussinessResult = GenerateBusinessOrder(Ds, BussId, UserId, txnTime, ref IsBusinessOk, ref Amount, ref OrderId);
            if (IsBusinessOk == true)
            {
                //生成预支付单，并返回订单ID
                string BankResult = GenerateBankOrder(BussId, OrderId, txnTime, Amount, ref IsBankOk, ref prepay_str);
                if (IsBankOk == false)
                {
                    return JSONHelper.FromString(false, BankResult);
                }
                else
                {
                    //更新订单银行流水号
                    IDbConnection Conn = new SqlConnection(ConnectionDb.GetBusinessConnection());
                    string Query = "UPDATE Tb_Charge_Receipt SET PrepayStr=@prepay_str WHERE OrderId = @OrderId ";
                    Conn.Execute(Query, new { prepay_str = prepay_str.ToString(), OrderId = OrderId });
                    //返回请求字符串
                    return JSONHelper.FromJsonString(true, prepay_str);
                }
            }
            else
            {
                return JSONHelper.FromString(false, BussinessResult);
            }
        }

    }
}