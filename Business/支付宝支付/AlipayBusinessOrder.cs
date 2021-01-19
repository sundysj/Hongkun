using Com.Alipay;
using Dapper;
using DapperExtensions;
using log4net;
using MobileSoft.Common;
using Model.Model.Buss;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Business
{
    /// <summary>
    /// 支付定商家订单
    /// </summary>
    public class AlipayBusinessOrder : PubInfo
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AlipayBusinessOrder));

        public string openid { get; set; }
        public Config c { get; set; }
        /// <summary>
        /// 构造器
        /// </summary>
        public AlipayBusinessOrder()
        {
            base.Token = "20160804AlipayBusinessOrder";
            c = new Config();
            c.notify_url = Global_Fun.AppWebSettings("AliPayBuss_Notify_Url").ToString();
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
                //创建线下订单
                case "GenerateOfflineOrder":
                    Trans.Result = GenerateOfflineOrder(Ds);
                    break;
                //订单继续支付
                case "GoOnGenerateOrder":
                    Trans.Result = GoOnGenerateOrder(Ds);
                    break;
                //订单确认收款成功
                case "ReceBusinessOrder":
                    Trans.Result = ReceBusinessOrder(Row["OrderId"].ToString(), AppGlobal.StrToDec(Row["RealAmount"].ToString()));
                    break;
                //订单退款
                case "CancelOrder":
                    Trans.Result = CancelOrder(Row["OrderId"].ToString());
                    break;
                default:
                    break;
            }
        }

        private string GenerateOfflineOrder(DataSet ds) {
            DataRow Row = ds.Tables[0].Rows[0];

            string prepay_str = "";
            string txnTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string orderId = "";

            decimal Amount = 0.0m;
            decimal CouponAmount = 0.0m;

            string BussId = Row["BussId"].ToString();
            string UserId = Row["UserId"].ToString();
            bool IsBusinessOk = false;
            string Name = Row["Name"].ToString();
            string Mobile = Row["Mobile"].ToString();
            string DeliverAddress = Row["DeliverAddress"].ToString();

            // 俊发需求5896
            string communityId = null;
            if (Row.Table.Columns.Contains("CommunityId") && !string.IsNullOrEmpty(Row["CommunityId"].ToString()))
            {
                communityId = Row["CommunityId"].ToString();
            }

            //生成商家账单
            string BussinessResult = GenerateBusinessOrder(ds, communityId, BussId, UserId, txnTime, ref IsBusinessOk, ref Amount, ref CouponAmount, ref orderId, Name, Mobile, DeliverAddress);
            if (!IsBusinessOk)
            {
                return new ApiResult(false, BussinessResult).toJson();
            }

            using (IDbConnection conn = new SqlConnection(ConnectionDb.GetBusinessConnection()))
            {

                string sql = string.Format(@"UPDATE Tb_Charge_Receipt SET PrepayStr=@prepay_str,Method='线下支付',IsPay='已付款',ReturnCode='TRADE_FINISHED',ReturnMsg='TRADE_FINISHED',PayDate=GetDate(),Amount={0},CouponAmount={1},RealAmount=0 WHERE OrderId = @OrderId", Amount, CouponAmount);
                conn.Execute(sql, new { prepay_str = prepay_str.ToString(), OrderId = orderId });
            }
            return new ApiResult(true, "下单成功").toJson();
        }
        /// <summary>
        /// 支付宝支付Log信息
        /// </summary>
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
        public static string ReceBusinessOrder(string OrderId, decimal realAmount)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnectionDb.GetBusinessConnection()))
                {
                    string sql = "SELECT * FROM Tb_Charge_Receipt WHERE OrderId=@OrderId";
                    Tb_Charge_Receipt Receipt = conn.Query<Tb_Charge_Receipt>(sql, new { OrderId = OrderId }).SingleOrDefault();
                    if (Receipt.IsPay.ToString() == "已付款")
                    {
                        return "订单已付款";
                    }

                    //订单确认收款,收款动作写在下面
                    Receipt.IsPay = "已付款";
                    Receipt.PayDate = DateTime.Now;
                    Receipt.Method = "支付宝";
                    Receipt.RealAmount = realAmount;

                    // 对于没有使用优惠券的，直接设置Amount
                    if (Receipt.IsUseCoupon <= 0)
                    {
                        conn.Execute("UPDATE Tb_Charge_Receipt SET IsPay=@IsPay,PayDate=@PayDate,Method='支付宝',Amount=@RealAmount,RealAmount=@RealAmount WHERE Id=@Id",
                            new
                            {
                                IsPay = Receipt.IsPay.ToString(),
                                PayDate = Receipt.PayDate.ToString(),
                                RealAmount = Receipt.RealAmount,
                                Id = Receipt.Id.ToString()
                            });

                        IEnumerable<Tb_Charge_ReceiptDetail> ReceiptDetails = conn.Query<Tb_Charge_ReceiptDetail>(@"SELECT * FROM Tb_Charge_ReceiptDetail WHERE ReceiptCode=@ReceiptCode", new { ReceiptCode = Receipt.Id });

                        foreach (Tb_Charge_ReceiptDetail item in ReceiptDetails)
                        {
                            Tb_Resources_Details resources = conn.Query<Tb_Resources_Details>("SELECT * FROM Tb_Resources_Details WHERE ResourcesID=@ResourcesID", new { ResourcesID = item.ResourcesID }).FirstOrDefault();

                            if (resources != null)
                            {
                                decimal detailAmount = 0.0m;
                                if (resources.IsGroupBuy == "是" && resources.GroupBuyEndDate.HasValue && resources.GroupBuyEndDate > DateTime.Now)
                                    detailAmount = resources.GroupBuyPrice.Value * item.Quantity;
                                else
                                    detailAmount = (resources.ResourcesSalePrice - resources.ResourcesDisCountPrice) * item.Quantity;

                                conn.Execute(@"UPDATE Tb_Charge_ReceiptDetail SET 
                                                SalesPrice=@SalesPrice,
                                                DiscountPrice=@DiscountPrice,
                                                GroupPrice=@GroupPrice,
                                                DetailAmount=@DetailAmount,
                                                OffsetMoney=0 
                                                WHERE ResourcesID=@ResourcesID AND ReceiptCode=@ReceiptCode",
                                                new
                                                {
                                                    SalesPrice = resources.ResourcesSalePrice,
                                                    DiscountPrice = resources.ResourcesDisCountPrice,
                                                    GroupPrice = ((resources.IsGroupBuy == "是" && resources.GroupBuyEndDate.HasValue && resources.GroupBuyEndDate > DateTime.Now) ? resources.GroupBuyPrice : null),
                                                    DetailAmount = detailAmount,
                                                    ResourcesID = resources.ResourcesID,
                                                    ReceiptCode = item.ReceiptCode
                                                });

                                // 减库存
                                conn.Execute("UPDATE Tb_Resources_Details SET ResourcesCount=(ResourcesCount-@Count) WHERE ResourcesID=@ResourcesID",
                                    new { Count = AppGlobal.StrToLong(item.Quantity.ToString()), ResourcesID = resources.ResourcesID });
                            }
                        }
                    }
                    else
                    {
                        IEnumerable<Tb_Charge_ReceiptDetail> ReceiptDetails = conn.Query<Tb_Charge_ReceiptDetail>(@"SELECT * FROM Tb_Charge_ReceiptDetail WHERE ReceiptCode=@ReceiptCode", new { ReceiptCode = Receipt.Id });

                        IEnumerable<Tb_Resources_Details> resources = conn.Query<Tb_Resources_Details>(@"SELECT * FROM Tb_Resources_Details 
                            WHERE ResourcesID in(SELECT ResourcesID FROM Tb_Charge_ReceiptDetail WHERE ReceiptCode=@ReceiptCode)", new { ReceiptCode = Receipt.Id });

                        decimal totalAmount = 0.0m;

                        // 商品总价，此处计算的是不算优惠券时的总价
                        // 此时Tb_Charge_ReceiptDetail.DetailAmount保存的是未计算优惠券时的价格，最终价格需要另行计算
                        foreach (Tb_Resources_Details item in resources)
                        {
                            foreach (Tb_Charge_ReceiptDetail receiptDetail in ReceiptDetails)
                            {
                                if (receiptDetail.ResourcesID == item.ResourcesID)
                                {
                                    // 团购
                                    if (item.IsGroupBuy == "是" && item.GroupBuyEndDate.HasValue && item.GroupBuyEndDate > DateTime.Now)
                                    {
                                        receiptDetail.DetailAmount = (item.GroupBuyPrice.Value * receiptDetail.Quantity);
                                    }
                                    else
                                    {
                                        receiptDetail.DetailAmount = (item.ResourcesSalePrice - item.ResourcesDisCountPrice) * receiptDetail.Quantity;
                                    }
                                    totalAmount += receiptDetail.DetailAmount.Value;
                                    break;
                                }
                            }
                        }

                        conn.Execute("UPDATE Tb_Charge_Receipt SET IsPay=@IsPay,PayDate=@PayDate,Method='支付宝',Amount=@Amount,RealAmount=@RealAmount WHERE Id=@Id",
                            new
                            {
                                IsPay = Receipt.IsPay.ToString(),
                                PayDate = Receipt.PayDate.ToString(),
                                Amount = totalAmount,
                                RealAmount = Receipt.RealAmount,
                                Id = Receipt.Id.ToString()
                            });

                        // 本次使用的抵扣券金额
                        //decimal couponMoney = totalAmount - realAmount;
                        //decimal tempUsedCouponMoney = 0.0m;// 暂时没有用处，判断本次使用的抵扣券是否和订单详细记录里面使用的抵扣券价格一样

                        foreach (Tb_Resources_Details item in resources)
                        {
                            foreach (Tb_Charge_ReceiptDetail receiptDetail in ReceiptDetails)
                            {
                                if (receiptDetail.ResourcesID == item.ResourcesID)
                                {
                                    //if (receiptDetail.OffsetMoney.HasValue && receiptDetail.OffsetMoney.Value > 0)
                                    //{
                                    //    tempUsedCouponMoney += receiptDetail.OffsetMoney.Value;
                                    //}
                                    //if (receiptDetail.OffsetMoney2.HasValue && receiptDetail.OffsetMoney2.Value > 0)
                                    //{
                                    //    tempUsedCouponMoney += receiptDetail.OffsetMoney2.Value;
                                    //}
                                    //else
                                    //{
                                    //    receiptDetail.OffsetMoney = 0.0m;
                                    //}

                                    // 更新订单详情表商品付费金额信息
                                    conn.Execute(@"UPDATE Tb_Charge_ReceiptDetail SET 
                                                SalesPrice=@SalesPrice,
                                                DiscountPrice=@DiscountPrice,
                                                GroupPrice=@GroupPrice,
                                                DetailAmount=@DetailAmount 
                                                WHERE ResourcesID=@ResourcesID AND ReceiptCode=@ReceiptCode",
                                        new
                                        {
                                            SalesPrice = item.ResourcesSalePrice,
                                            DiscountPrice = item.ResourcesDisCountPrice,
                                            GroupPrice = ((item.IsGroupBuy == "是" && item.GroupBuyEndDate.HasValue && item.GroupBuyEndDate > DateTime.Now) ? item.GroupBuyPrice : null),
                                            DetailAmount = receiptDetail.DetailAmount.Value - receiptDetail.OffsetMoney.Value,
                                            ResourcesID = receiptDetail.ResourcesID,
                                            ReceiptCode = receiptDetail.ReceiptCode
                                        });

                                    // 减库存
                                    conn.Execute("UPDATE Tb_Resources_Details SET ResourcesCount=(ResourcesCount-@Count) WHERE ResourcesID=@ResourcesID",
                                        new { Count = AppGlobal.StrToLong(receiptDetail.Quantity.ToString()), ResourcesID = item.ResourcesID });

                                    break;
                                }
                            }
                        }
                    }
                    return "success";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary>
        /// 配置支付请求参数,根据当前商家获取
        /// </summary>
        /// <param name="BussId"></param>
        public bool GenerateConfig(string BussId)
        {
            IDbConnection conn = new SqlConnection(ConnectionDb.GetBusinessConnection());
            string query = "SELECT * FROM Tb_AlipayCertifiate WHERE BussId=@BussId";
            Model.Model.Buss.Tb_AlipayCertifiate T = conn.Query<Model.Model.Buss.Tb_AlipayCertifiate>(query, new { BussId = BussId }).SingleOrDefault();

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
        /// 配置支付请求参数,根据当前商家获取
        /// </summary>
        /// <param name="BussId"></param>
        public bool GenerateConfig(string BussId, out Config c)
        {
            IDbConnection conn = new SqlConnection(ConnectionDb.GetBusinessConnection());
            string query = "SELECT * FROM Tb_AlipayCertifiate WHERE BussId=@BussId";
            Model.Model.Buss.Tb_AlipayCertifiate T = conn.Query<Model.Model.Buss.Tb_AlipayCertifiate>(query, new { BussId = BussId }).SingleOrDefault();
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
        /// 生成三方预支付单
        /// </summary>
        /// <param name="BussId"></param>
        /// <param name="out_trade_no"></param>
        /// <param name="txnTime"></param>
        /// <param name="total_fee"></param>
        /// <param name="R"></param>
        /// <param name="prepay_str"></param>
        /// <returns></returns>
        public string GenerateBankOrder(string BussId, string out_trade_no, string txnTime, decimal total_fee, ref bool R, ref string prepay_str, string subject = "")
        {
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            sPara.Add("partner", c.partner.ToString());
            sPara.Add("seller_id", c.seller_id.ToString());
            sPara.Add("out_trade_no", out_trade_no.ToString());
            sPara.Add("subject", string.IsNullOrEmpty(subject) ? "商品购买" : subject);
            sPara.Add("body", BussId);
            sPara.Add("total_fee", total_fee.ToString("#0.00"));
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
        /// 生成商家订单
        /// </summary>
        public string GenerateBusinessOrder(DataSet Ds, string communityId, string BussId, string UserId, string txnTime, ref bool IsOk, ref decimal Amount, ref decimal CouponAmount, ref string OrderId, string Name, string Mobile, string DeliverAddress)
        {
            DataRow Row = Ds.Tables[0].Rows[0];

            int? corpId = null;

            if (Row.Table.Columns.Contains("CorpId") && !string.IsNullOrEmpty(Row["CorpId"].ToString()))
            {
                corpId = AppGlobal.StrToInt(Row["CorpId"].ToString());
            }

            var iid = Guid.NewGuid().ToString();

            // 生成商家收款订单
            Tb_Charge_Receipt Receipt = new Tb_Charge_Receipt
            {
                Id = iid,
                CorpId = corpId,
                BussId = BussId.ToString(),
                UserId = UserId,
                OrderId = Guid.NewGuid().ToString(),
                Name = Name,
                IsPay = "未付款",
                IsReceive = "未收货",
                IsDeliver = "未发货",
                ReceiptMemo = Row["ReceiptMemo"].ToString(),
                ReceiptType = "通用票据",
                ReceiptDate = DateTime.Now,
                PayDate = null,
                MchId = c.partner,
                Partner = c.partner,
                PrepayStr = "",
                txnTime = txnTime.ToString(),
                ReturnCode = "",
                ReturnMsg = "",
                Express = "",
                ExpressNum = "",
                Mobile = Mobile,  //联系电话
                DeliverAddress = DeliverAddress, //收货地址
                IsDelete = 0
            };

            // 是否使用优惠券抵扣
            if (Row.Table.Columns.Contains("UseCoupon"))
            {
                Receipt.IsUseCoupon = AppGlobal.StrToInt(Row["UseCoupon"].ToString());
            }


            using (IDbConnection conn = new SqlConnection(ConnectionDb.GetBusinessConnection()))
            {
                // 生成订单票据号
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@Id", BussId);
                dp.Add("@OrderLength", 32);
                dp.Add("@Num", "", DbType.String, ParameterDirection.Output);

                conn.Execute("Proc_GetOrderNum", dp, null, null, CommandType.StoredProcedure);
                Receipt.ReceiptSign = dp.Get<string>("@Num");
                // 保存订单信息
                conn.Insert(Receipt);

                if (!string.IsNullOrEmpty(communityId))
                {
                    conn.Execute("UPDATE Tb_Charge_Receipt SET CommunityId=@CommunityId WHERE ID=@IID",
                        new { CommunityId = communityId, IID = iid });
                }

                /*
                 *  计算此时的订单金额，此时不应该保存商品的价格信息；
                 *  如果用户不付款，且订单未取消期间，商家更改了商品单价或可优惠价或规格追加价，付款时则需要重新计算
                 *  
                 *  1、获取用户在该商家的可用优惠券余额
                 *  2、获取商品此时的单价和可优惠价，对应的规格型号的追加单价
                 *  3、计算当前订单内的商品是否支持优惠券抵扣，如果支持，计算最多能抵扣的价格并更新优惠券余额
                 */

                // 1、获取用户优惠券余额
                dynamic balanceInfo = conn.Query<dynamic>(@"SELECT isnull(Balance1,0) AS Balance1,isnull(Balance2,0) AS Balance2
                                                            FROM
                                                              (SELECT Balance1=(SELECT Balance FROM Tb_Resources_CouponBalance
                                                              WHERE UserId=@UserId
                                                                    AND CorpId=@CorpId AND BussId=@BussId),
                                                                    Balance2=(SELECT Balance FROM Tb_Resources_CouponBalance
                                                              WHERE UserId=@UserId
                                                                    AND CorpId=@CorpId AND BussId IS NULL)) as x;",
                    new { UserId = Receipt.UserId, CorpId = corpId, BussId = Receipt.BussId }).FirstOrDefault();

                decimal totalAmount = 0.0m;             // 商品总价
                decimal userCouponBalance = 0.0m;       // 用户可用优惠券余额
                decimal totalUseCouponMoney = 0.0m;     // 当前能使用优惠券抵扣的钱

                decimal bussBalance = 0.0m; // 商家专用券余额
                decimal corpBalance = 0.0m; // 物管通用券余额

                if (balanceInfo != null)
                {
                    bussBalance = balanceInfo.Balance1;
                    corpBalance = balanceInfo.Balance2;
                    userCouponBalance = bussBalance + corpBalance;
                }

                // 2、获取商品价格信息
                foreach (DataRow DetailRow in Ds.Tables["Product"].Rows)
                {
                    // 订单商品详情
                    Tb_Charge_ReceiptDetail ReceiptDetail = new Tb_Charge_ReceiptDetail();
                    ReceiptDetail.RpdCode = Guid.NewGuid().ToString();
                    ReceiptDetail.ReceiptCode = Receipt.Id;
                    ReceiptDetail.ShoppingId = DetailRow["ShoppingId"].ToString();
                    ReceiptDetail.ResourcesID = DetailRow["Id"].ToString();
                    ReceiptDetail.RpdMemo = DetailRow["RpdMemo"].ToString();
                    ReceiptDetail.Quantity = DataSecurity.StrToInt(DetailRow["Quantity"].ToString());
                    ReceiptDetail.OffsetMoney = 0.0m;
                    ReceiptDetail.OffsetMoney2 = 0.0m;

                    // 商品的单价和可优惠单价
                    string sql = @"SELECT * FROM Tb_Resources_Details 
                                   WHERE isnull(IsDelete,0)=0 AND IsRelease='是' AND IsStopRelease='否' AND ResourcesID=@ResourcesID";
                    Tb_Resources_Details ResourcesDetail = conn.Query<Tb_Resources_Details>(sql, new { ResourcesID = ReceiptDetail.ResourcesID }).FirstOrDefault();

                    // 商品存在，期间未失效
                    if (ResourcesDetail != null)
                    {
                        // 库存不足，跳过
                        if (ReceiptDetail.Quantity > ResourcesDetail.ResourcesCount)
                        {
                            continue;
                        }

                        ReceiptDetail.SalesPrice = ResourcesDetail.ResourcesSalePrice;
                        ReceiptDetail.DiscountPrice = ResourcesDetail.ResourcesDisCountPrice;

                        bool IsInGroupBuyTime = false;

                        if (ResourcesDetail.IsGroupBuy == "是")
                        {
                            if (ResourcesDetail.GroupBuyStartDate.HasValue && ResourcesDetail.GroupBuyStartDate.Value <= DateTime.Now &&
                                ResourcesDetail.GroupBuyEndDate.HasValue && ResourcesDetail.GroupBuyEndDate.Value >= DateTime.Now)
                            {
                                IsInGroupBuyTime = true;
                                ReceiptDetail.GroupPrice = ResourcesDetail.GroupBuyPrice;
                            }
                        }

                        // 1、商品单价
                        decimal price = 0.0m;

                        if (IsInGroupBuyTime)
                        {
                            price = (ResourcesDetail.GroupBuyPrice.Value - ResourcesDetail.ResourcesDisCountPrice) * ReceiptDetail.Quantity;
                        }
                        else
                        {
                            price = (ResourcesDetail.ResourcesSalePrice - ResourcesDetail.ResourcesDisCountPrice) * ReceiptDetail.Quantity;
                        }

                        // 2、商品规格追加价格
                        sql = "SELECT * FROM View_Tb_ResourcesSpecificationsPrice WHERE ShoppingId=@ShoppingId";
                        MobileSoft.Model.Resources.Tb_ResourcesSpecificationsPrice propertyInfo = conn.Query<MobileSoft.Model.Resources.Tb_ResourcesSpecificationsPrice>(sql, new { ShoppingId = DetailRow["ShoppingId"].ToString() }).FirstOrDefault();

                        if (propertyInfo != null)
                        {
                            price = price + propertyInfo.Price.Value * ReceiptDetail.Quantity;
                        }

                        // 当前商品需要抵扣的金额
                        decimal currCouponMoney = 0.0m;

                        string isSupportCoupon = "";
                        // 3、商品支持抵扣券 并且 还有用户还有足够的优惠券余额
                        if (ResourcesDetail.IsSupportCoupon == "1")
                        {
                            isSupportCoupon = "是";
                        }
                        else
                        {
                            isSupportCoupon = "否";
                        }
                        if (isSupportCoupon == "是" &&
                            Receipt.IsUseCoupon.HasValue &&
                            Receipt.IsUseCoupon.Value > 0 &&
                            userCouponBalance > 0)
                        {
                            // 计算当前商品需要抵扣的金额
                            currCouponMoney = (ResourcesDetail.MaximumCouponMoney.HasValue ? ResourcesDetail.MaximumCouponMoney.Value : 0) * ReceiptDetail.Quantity;

                            if (bussBalance > 0)
                            {
                                if (bussBalance >= currCouponMoney)
                                {
                                    // 优惠券使用明细
                                    ReceiptDetail.OffsetMoney = currCouponMoney;
                                    ReceiptDetail.OffsetMoney2 = 0.0m;

                                    totalUseCouponMoney += currCouponMoney;

                                    bussBalance -= currCouponMoney;
                                    userCouponBalance -= currCouponMoney;
                                }
                                else
                                {
                                    // 先用专用券抵扣
                                    ReceiptDetail.OffsetMoney = bussBalance;

                                    totalUseCouponMoney += bussBalance;

                                    userCouponBalance -= bussBalance;

                                    // 专用券不够差额
                                    decimal differ = currCouponMoney - bussBalance;

                                    // 专用券不够，通用券余额大于0
                                    if (corpBalance > 0)
                                    {
                                        // 通用券足够抵扣
                                        if (corpBalance >= differ)
                                        {
                                            ReceiptDetail.OffsetMoney2 = differ;

                                            totalUseCouponMoney += differ;

                                            corpBalance -= differ;
                                            userCouponBalance -= differ;
                                        }
                                        // 通用券不足以抵扣
                                        else
                                        {
                                            ReceiptDetail.OffsetMoney2 = corpBalance;

                                            totalUseCouponMoney += corpBalance;

                                            userCouponBalance -= corpBalance;
                                            corpBalance = 0;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // 没有专用券，但是有通用券
                                if (corpBalance > 0)
                                {
                                    // 通用券足够抵扣
                                    if (corpBalance > currCouponMoney)
                                    {
                                        ReceiptDetail.OffsetMoney2 = currCouponMoney;

                                        totalUseCouponMoney += currCouponMoney;

                                        corpBalance -= currCouponMoney;
                                        userCouponBalance -= currCouponMoney;
                                    }
                                    // 通用券不足以抵扣
                                    else
                                    {
                                        ReceiptDetail.OffsetMoney2 = corpBalance;

                                        totalUseCouponMoney += corpBalance;

                                        userCouponBalance -= corpBalance;
                                        corpBalance = 0;
                                    }
                                }
                            }
                        }

                        // 订单商品总价追加
                        totalAmount += price;

                        // 插入订单内商品数据
                        conn.Insert(ReceiptDetail);

                        // 删除购物车规格明细
                        conn.Execute("DELETE FROM Tb_ShoppingDetailed WHERE ShoppingId=@ShoppingId", new { ShoppingId = ReceiptDetail.ShoppingId });
                        // 删除购物车
                        conn.Execute("DELETE FROM Tb_ShoppingCar WHERE Id=@ShoppingId", new { ShoppingId = ReceiptDetail.ShoppingId });
                    }
                    else
                    {
                        // 商品已经失效的处理，直接从购物车删除

                        // 删除购物车规格明细
                        conn.Execute("DELETE FROM Tb_ShoppingDetailed WHERE ShoppingId=@ShoppingId", new { ShoppingId = ReceiptDetail.ShoppingId });

                        // 删除购物车
                        conn.Execute("DELETE FROM Tb_ShoppingCar WHERE Id=@ShoppingId", new { ShoppingId = ReceiptDetail.ShoppingId });
                    }
                }

                // 3、更新用户优惠券信息
                if (totalUseCouponMoney > 0)
                {
                    conn.Execute("proc_Resources_CouponBalance_Use",
                        new { UserId = Receipt.UserId, CorpId = corpId, BussId = Receipt.BussId, UseMoney = totalUseCouponMoney },
                        null, null, CommandType.StoredProcedure);
                }

                // ref参数处理
                IsOk = true;
                Amount = totalAmount;
                CouponAmount = totalUseCouponMoney;
                OrderId = Receipt.OrderId;
            }

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
            string BussId = Row["BussId"].ToString();
            string UserId = Row["UserId"].ToString();

            bool IsConfig = GenerateConfig(BussId);
            if (IsConfig == false)
            {
                return JSONHelper.FromString(false, "未配置证书文件");
            }

            string Name = Row["Name"].ToString();
            string Mobile = Row["Mobile"].ToString();
            string DeliverAddress = Row["DeliverAddress"].ToString();
            string subject = "";
            decimal Amount = 0.0m;
            decimal CouponAmount = 0.0m;

            // 俊发需求5896
            string communityId = null;
            if (Row.Table.Columns.Contains("CommunityId") && !string.IsNullOrEmpty(Row["CommunityId"].ToString()))
            {
                communityId = Row["CommunityId"].ToString();
            }

            //生成商家账单
            string BussinessResult = GenerateBusinessOrder(Ds, communityId, BussId, UserId, txnTime, ref IsBusinessOk, ref Amount, ref CouponAmount, ref OrderId, Name, Mobile, DeliverAddress);

            if (IsBusinessOk == true)
            {
                using (IDbConnection Conn = new SqlConnection(ConnectionDb.GetBusinessConnection()))
                {
                    Tb_System_BusinessCorp bussInfo = Conn.Query<Tb_System_BusinessCorp>(@"SELECT * FROM Tb_System_BusinessCorp WHERE BussId=@BussId",
                        new { BussId = BussId }).FirstOrDefault();

                    if (bussInfo != null)
                    {
                        subject = bussInfo.BussName + "订单，共" + Ds.Tables["Product"].Rows.Count + "种商品";
                    }
                }

                //生成预支付单，并返回订单ID
                string BankResult = GenerateBankOrder(BussId, OrderId, txnTime, (Amount - CouponAmount), ref IsBankOk, ref prepay_str, subject);
                if (IsBankOk == false)
                {
                    return JSONHelper.FromString(false, BankResult);
                }
                else
                {
                    using (IDbConnection conn = new SqlConnection(ConnectionDb.GetBusinessConnection()))
                    {
                        string sql;
                        if ((Amount - CouponAmount) == 0)
                        {
                            sql = string.Format(@"UPDATE Tb_Charge_Receipt SET PrepayStr=@prepay_str,Method='支付宝',IsPay='已付款',ReturnCode='TRADE_FINISHED',ReturnMsg='TRADE_FINISHED',PayDate=GetDate(),Amount={0},CouponAmount={1},RealAmount=0 WHERE OrderId = @OrderId", Amount, CouponAmount);
                        }
                        else
                        {
                            sql = @"UPDATE Tb_Charge_Receipt SET PrepayStr=@prepay_str WHERE OrderId = @OrderId";
                        }

                        conn.Execute(sql, new { prepay_str = prepay_str.ToString(), OrderId = OrderId });
                    }

                    //返回请求字符串
                    return JSONHelper.FromJsonString(true, prepay_str);
                }
            }
            else
            {
                return JSONHelper.FromString(false, BussinessResult);
            }
        }

        /// <summary>
        /// 订单继续支付
        /// </summary>
        public string GoOnGenerateOrder(DataSet Ds)
        {
            DataRow Row = Ds.Tables[0].Rows[0];

            bool IsBankOk = false;
            bool IsBusinessOk = true;//商家订单是否生成成功
            string OrderId = Row["OrderId"].ToString();
            string prepay_str = "";


            //取得上一次商家订单信息
            IDbConnection ConnOrder = new SqlConnection(ConnectionDb.GetBusinessConnection());
            string OrderQuery = "SELECT * FROM Tb_Charge_Receipt WHERE OrderId=@OrderId";
            Model.Model.Buss.Tb_Charge_Receipt TOrder = ConnOrder.Query<Model.Model.Buss.Tb_Charge_Receipt>(OrderQuery, new { OrderId = OrderId }).SingleOrDefault();

            if (TOrder != null)
            {
                IsBusinessOk = true;
            }
            string txnTime = TOrder.txnTime.ToString();
            string BussId = TOrder.BussId.ToString();
            bool IsConfig = GenerateConfig(BussId);
            if (IsConfig == false)
            {
                return JSONHelper.FromString(false, "未配置证书文件");
            }

            decimal realAmount = 0.0m;

            string sql = @"SELECT * FROM Tb_Charge_ReceiptDetail WHERE ReceiptCode=@ReceiptCode";

            IEnumerable<Tb_Charge_ReceiptDetail> ReceiptDetails = ConnOrder.Query<Tb_Charge_ReceiptDetail>(sql, new { ReceiptCode = TOrder.Id });

            foreach (Tb_Charge_ReceiptDetail item in ReceiptDetails)
            {
                // 计算此时应该多少钱
                sql = @"SELECT * FROM Tb_Resources_Details WHERE ResourcesID=@ResourcesID";
                Tb_Resources_Details resources = ConnOrder.Query<Tb_Resources_Details>(sql, new { ResourcesID = item.ResourcesID }).FirstOrDefault();

                if (resources != null)
                {
                    if (resources.IsGroupBuy == "是" && resources.GroupBuyEndDate.HasValue && resources.GroupBuyEndDate.Value > DateTime.Now)
                    {
                        realAmount += (resources.GroupBuyPrice.Value + item.Quantity);
                    }
                    else
                    {
                        realAmount += (resources.ResourcesSalePrice - resources.ResourcesDisCountPrice) * item.Quantity;
                    }

                    if (item.OffsetMoney.HasValue)
                    {
                        realAmount -= item.OffsetMoney.Value;
                    }

                    if (item.OffsetMoney2.HasValue)
                    {
                        realAmount -= item.OffsetMoney2.Value;
                    }
                }
            }

            if (IsBusinessOk == true)
            {
                sql = "SELECT * FROM Tb_System_BusinessCorp WHERE BussId=@BussId";
                Tb_System_BusinessCorp bussInfo = ConnOrder.Query<Tb_System_BusinessCorp>(sql, new { BussId = TOrder.BussId }).FirstOrDefault();

                //生成预支付单，并返回订单ID
                string BankResult = GenerateBankOrder(BussId, OrderId, txnTime, realAmount, ref IsBankOk, ref prepay_str, bussInfo.BussName + "订单，共" + ReceiptDetails.Count() + "种商品");
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
            return JSONHelper.FromString(false, "生成订单失败");
        }

    }
}