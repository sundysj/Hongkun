using App.Model;
using Dapper;
using DapperExtensions;
using log4net;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using Model.Model.Buss;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WxPayAPI;

namespace Business
{
    /// <summary>
    /// 力帆商家订单
    /// </summary>
    public class WeiXinPayBuss_lf : PubInfo
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Alipay));

        /// <summary>
        /// 构造器
        /// </summary>
        public WeiXinPayBuss_lf()
        {
            base.Token = "20181221WeiXinPayBuss_lf";
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
                //订单继续支付
                case "GoOnGenerateOrder":
                    Trans.Result = GoOnGenerateOrder(Ds);
                    break;
                //订单确认收款成功
                case "ReceBusinessOrder":
                    Trans.Result = ReceBusinessOrder(Row["OrderId"].ToString(), AppGlobal.StrToInt(Row["RealAmount"].ToString()));
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
        /// 支付Log信息
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
                    Receipt.Method = "微信";
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

                        conn.Execute("UPDATE Tb_Charge_Receipt SET IsPay=@IsPay,PayDate=@PayDate,Method='微信',Amount=@Amount,RealAmount=@RealAmount WHERE Id=@Id",
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
        public WxPayConfig GenerateConfig(string BussId)
        {
            WxPayConfig wxPayConfig = null;
            IDbConnection conn = new SqlConnection(ConnectionDb.GetBusinessConnection());
            string query = "SELECT * FROM Tb_WeiXinPayCertificate WHERE BussId=@BussId";
            Model.Model.Buss.Tb_WeiXinPayCertificate T = conn.Query<Model.Model.Buss.Tb_WeiXinPayCertificate>(query, new { BussId = BussId }).SingleOrDefault();
            if (T != null)
            {
                wxPayConfig = new WxPayConfig();
                wxPayConfig.APPID = T.appid.ToString();
                wxPayConfig.MCHID = T.mch_id.ToString();
                wxPayConfig.KEY = T.appkey.ToString();
                wxPayConfig.APPSECRET = T.appsecret.ToString();
                wxPayConfig.SSLCERT_PATH = T.SSLCERT_PATH;
                wxPayConfig.SSLCERT_PASSWORD = T.SSLCERT_PASSWORD;
                wxPayConfig.NOTIFY_URL = Global_Fun.AppWebSettings("WechatPayBuss_Notify_Url").ToString();
            }
            return wxPayConfig;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BussId"></param>
        /// <param name="out_trade_no"></param>
        /// <param name="txnTime"></param>
        /// <param name="total_fee"></param>
        /// <param name="R"></param>
        /// <param name="prepay_str"></param>
        /// <param name="WPD"></param>
        /// <returns></returns>
        public string GenerateBankOrder(string BussId, string out_trade_no, string txnTime, int total_fee, ref bool R, ref WxPayData WPD, WxPayConfig wxPayConfig, string subject = "")
        {
            R = false;
            //统一下单
            WxPayData data = new WxPayData();
            data.SetValue("body", string.IsNullOrEmpty(subject) ? "商品购买" : subject);
            data.SetValue("attach", BussId.ToString());
            data.SetValue("out_trade_no", out_trade_no);
            data.SetValue("total_fee", total_fee);
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            data.SetValue("trade_type", "APP");
            data.SetValue("notify_url", Global_Fun.AppWebSettings("WechatPayBuss_Notify_Url").ToString());

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
        /// 生成商家订单
        /// </summary>
        public string GenerateBusinessOrder(DataSet Ds, string BussId, string UserId, string txnTime, int userPoints, decimal pointDiscountAmount, string pointUserHistoryId, 
            ref bool IsOk, ref int Amount, ref string OrderId, string Name, string Mobile, string DeliverAddress, WxPayConfig wxPayConfig)
        {
            DataRow Row = Ds.Tables[0].Rows[0];

            int? corpId = null;

            if (Row.Table.Columns.Contains("CorpId") && !string.IsNullOrEmpty(Row["CorpId"].ToString()))
            {
                corpId = AppGlobal.StrToInt(Row["CorpId"].ToString());
            }

            // 生成商家收款订单
            Tb_Charge_Receipt Receipt = new Tb_Charge_Receipt
            {
                Id = Guid.NewGuid().ToString(),
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
                MchId = wxPayConfig.MCHID,
                Partner = wxPayConfig.MCHID,
                PrepayStr = "",
                txnTime = txnTime.ToString(),
                ReturnCode = "",
                ReturnMsg = "",
                Express = "",
                ExpressNum = "",
                Mobile = Mobile,  //联系电话
                DeliverAddress = DeliverAddress, //收货地址
                IsDelete = 0,

                // 积分使用相关
                UsePoints = userPoints,
                PointDiscountAmount = pointDiscountAmount,
                PointUseHistoryID = pointUserHistoryId
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

                decimal totalAmount = 0.0m;             // 商品总价

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
                        var propertyInfo = conn.Query<MobileSoft.Model.Resources.Tb_ResourcesSpecificationsPrice>(sql, new { ShoppingId = DetailRow["ShoppingId"].ToString() });

                        if (propertyInfo.Count() > 0)
                        {
                            foreach (var item in propertyInfo)
                            {
                                price = price + item.Price.Value * ReceiptDetail.Quantity;
                            }
                        }

                        // 订单商品总价追加
                        totalAmount += price;

                        // 插入订单内商品数据
                        conn.Insert(ReceiptDetail);

                        // 删除购物车规格明细
                        //conn.Execute("DELETE FROM Tb_ShoppingDetailed WHERE ShoppingId=@ShoppingId", new { ShoppingId = ReceiptDetail.ShoppingId });
                        // 删除购物车
                        conn.Execute("UPDATE Tb_ShoppingCar SET IsDelete=1 WHERE Id=@ShoppingId", new { ShoppingId = ReceiptDetail.ShoppingId });
                    }
                    else
                    {
                        // 商品已经失效的处理，直接从购物车删除

                        // 删除购物车规格明细
                        //conn.Execute("DELETE FROM Tb_ShoppingDetailed WHERE ShoppingId=@ShoppingId", new { ShoppingId = ReceiptDetail.ShoppingId });

                        // 删除购物车
                        conn.Execute("UPDATE Tb_ShoppingCar SET IsDelete=1WHERE Id=@ShoppingId", new { ShoppingId = ReceiptDetail.ShoppingId });
                    }
                }

                // ref参数处理
                IsOk = true;
                Amount = (int)totalAmount * 100;
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

            string txnTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string UserId = Row["UserId"].ToString();
            string BussId = Row["BussId"].ToString();
            short CorpId = Convert.ToInt16(Row["CorpId"].ToString());

            WxPayConfig wxPayConfig = GenerateConfig(BussId);  
            if (null == wxPayConfig)
            {
                return JSONHelper.FromString(false, "未配置证书文件");
            }

            string Name = Row["Name"].ToString();
            string Mobile = Row["Mobile"].ToString();
            string DeliverAddress = Row["DeliverAddress"].ToString();
            string subject = "";

            int UsePoints = 0;
            if (Row.Table.Columns.Contains("UsePoints") && !string.IsNullOrEmpty(Row["UsePoints"].ToString()))
            {
                UsePoints = AppGlobal.StrToInt(Row["UsePoints"].ToString());
            }

            decimal goodsTotalAmount = 0.0m;                // 商品总价
            decimal goodsSupportPointAmount = 0.0m;         // 商品支持积分的金额
            bool suppprtPoint = false;

            // 商品信息
            using (var bzconn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var bussInfo = bzconn.Query<Tb_System_BusinessCorp>(@"SELECT * FROM Tb_System_BusinessCorp WHERE BussId=@BussId 
                                                                        AND isnull(IsDelete,0)=0 AND isnull(IsClose,'未关闭')='未关闭'",
                                                                        new { BussId = BussId }).FirstOrDefault();

                if (bussInfo != null)
                {
                    subject = bussInfo.BussName + "订单，共" + Ds.Tables["Product"].Rows.Count + "种商品";
                }
                else
                {
                    return new ApiResult(false, "抱歉，该商家信息不存在").toJson();
                }

                string sql = @"SELECT isnull(ResourcesSalePrice,0) AS ResourcesSalePrice,isnull(ResourcesDisCountPrice,0) AS ResourcesDisCountPrice,
                                isnull(IsGroupBuy,'否') AS IsGroupBuy,isnull(GroupBuyPrice,0) AS GroupBuyPrice,GroupBuyEndDate,ResourcesCount,
                                isnull(IsSupportPoint,0) AS IsSupportPoint
                                FROM Tb_Resources_Details 
                                WHERE IsRelease='是' AND IsStopRelease='否' AND isnull(IsDelete,0)=0 AND ResourcesID=@ResourcesID";

                foreach (DataRow item in Ds.Tables["Product"].Rows)
                {
                    var goodsInfo = bzconn.Query(sql, new { ResourcesID = item["Id"] }).FirstOrDefault();
                    var specPrice = bzconn.Query<decimal>(@"SELECT isnull(sum(Price),0) FROM Tb_ResourcesSpecificationsPrice 
                                                            WHERE ResourcesID=@ResourcesID AND SpecId IS NOT NULL 
                                                            AND SpecId IN(SELECT SpecId FROM Tb_ShoppingDetailed WHERE ShoppingId=@ShoppingId);",
                                                            new { ResourcesID = item["Id"], ShoppingId = item["ShoppingId"] }).FirstOrDefault();

                    if (goodsInfo != null)
                    {
                        if ((int)(goodsInfo.ResourcesCount) < AppGlobal.StrToInt(item["Quantity"].ToString()))
                        {
                            return new ApiResult(false, "抱歉，相关商品库存不足").toJson();
                        }

                        // 是在是团购，且在团购有效期间
                        if (goodsInfo.IsGroupBuy == "是" && goodsInfo.GroupBuyEndDate != null && AppGlobal.StrToDate(goodsInfo.GroupBuyEndDate) > DateTime.Now)
                        {
                            goodsTotalAmount += (goodsInfo.GroupBuyPrice + specPrice) * AppGlobal.StrToInt(item["Quantity"].ToString());

                            // 支持积分
                            if (goodsInfo.IsSupportPoint == 1)
                            {
                                suppprtPoint = true;
                                goodsSupportPointAmount += (goodsInfo.GroupBuyPrice + specPrice) * AppGlobal.StrToInt(item["Quantity"].ToString());
                            }
                        }
                        else
                        {
                            goodsTotalAmount += (goodsInfo.ResourcesSalePrice - goodsInfo.ResourcesDisCountPrice + specPrice) * AppGlobal.StrToInt(item["Quantity"].ToString());

                            // 支持积分
                            if (goodsInfo.IsSupportPoint == 1)
                            {
                                suppprtPoint = true;
                                goodsSupportPointAmount += (goodsInfo.ResourcesSalePrice - goodsInfo.ResourcesDisCountPrice + specPrice) * AppGlobal.StrToInt(item["Quantity"].ToString());
                            }
                        }
                    }
                    else
                    {
                        return new ApiResult(false, "抱歉，相关商品已下架").toJson();
                    }
                }
            }

            using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                if (appConn.State == ConnectionState.Closed)
                {
                    appConn.Open();
                }

                var appTrans = appConn.BeginTransaction();

                string useHistoryID = null;

                int Amount = 0;
                decimal deductionAmount = 0.0m;                 // 可抵扣的金额
                int pointBalance = 0;                           // 积分余额

                if (UsePoints > 0)
                {
                    if (suppprtPoint == false)
                    {
                        return new ApiResult(false, "抱歉，订单内所有商品均不支持积分抵用").toJson();
                    }

                    #region 判断积分
                    try
                    {
                        // 要使用的积分是否大于用户积分余额
                        pointBalance = appConn.Query<int>("SELECT PointBalance FROM Tb_App_UserPoint WHERE UserID=@UserID", new { UserID = UserId }, appTrans).FirstOrDefault();
                        if (pointBalance < UsePoints)
                        {
                            return new ApiResult(false, "积分余额不足").toJson();
                        }

                        // 商品积分权限不跟小区挂钩，统一由公司设置
                        var controlInfo = appConn.Query<Tb_Control_AppPoint>(@"SELECT * FROM Tb_Control_AppPoint WHERE CorpID=@CorpID AND CommunityID IS NULL AND IsEnable=1",
                                                                            new { CorpID = CorpId }, appTrans).FirstOrDefault();

                        if (controlInfo == null || controlInfo.IsEnable == false)
                        {
                            controlInfo = Tb_Control_AppPoint.DefaultControl;
                            controlInfo.CommunityID = Guid.Empty.ToString();
                        }

                        // 不允许抵用商品
                        if (controlInfo.AllowDeductionGoodsFees == false)
                        {
                            return new ApiResult(false, "暂不支持积分抵用功能").toJson();
                        }

                        // 积分抵用规则
                        var ruleInfo = appConn.Query($@"SELECT IID,ConditionAmount,DiscountsAmount,DeductionObject,StartTime,EndTime 
                                                        FROM Tb_App_Point_PropertyDeductionRule a LEFT JOIN Tb_Dictionary_Point_UsableObject b
                                                        ON a.DeductionObject=b.[Key] 
                                                        WHERE a.CorpID=@CorpID AND a.CommunityID IS NULL AND a.DeductionObject=@DeductionObject
                                                        AND getdate() BETWEEN StartTime AND EndTime AND a.IsDelete=0 ORDER BY ConditionAmount,DiscountsAmount",
                                                        new { CorpID = CorpId, DeductionObject = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.Goods) }, appTrans);

                        if (ruleInfo.Count() == 0)
                        {
                            return new ApiResult(false, "积分抵用规则未设置或已失效").toJson();
                        }

                        decimal goodsMaxDiscountsAmount = 0.0m;      // 积分可抵用金额

                        // 确定积分可抵用金额
                        if (goodsSupportPointAmount > 0)
                        {
                            foreach (var item in ruleInfo)
                            {
                                if (goodsSupportPointAmount >= item.ConditionAmount)
                                {
                                    goodsMaxDiscountsAmount = item.DiscountsAmount;
                                }
                            }
                        }

                        // 抵用的费用
                        deductionAmount = UsePoints / (decimal)controlInfo.PointExchangeRatio;

                        // 积分数量不正常
                        if (deductionAmount > goodsMaxDiscountsAmount)
                        {
                            return new ApiResult(false, "使用积分超过可抵用最大金额").toJson();
                        }

                        if (UsePoints > 0 && deductionAmount > 0)
                        {
                            useHistoryID = Guid.NewGuid().ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        appTrans.Rollback();
                        return new ApiResult(false, "积分验证失败").toJson();
                    }
                    #endregion
                }

                // 积分数量正常，生成订单
                try
                {
                    // 生成商家系统账单
                    string BussinessResult = GenerateBusinessOrder(Ds, BussId, UserId, txnTime, UsePoints, deductionAmount, useHistoryID,
                        ref IsBusinessOk, ref Amount, ref OrderId, Name, Mobile, DeliverAddress, wxPayConfig);

                    if (IsBusinessOk == true)
                    {
                        // 实际订单金额
                        int RealOrderAmount = Amount - (int)(deductionAmount * 100);

                        // 微信签名订单信息
                        WxPayData Data = new WxPayData();
                        string BankResult = GenerateBankOrder(BussId, OrderId, txnTime, RealOrderAmount, ref IsBankOk, ref Data, wxPayConfig);

                        if (IsBankOk == true)
                        {
                            //向手机端返回银行记录
                            WxPayData result = new WxPayData();
                            result.SetValue("appid", Data.GetValue("appid"));
                            result.SetValue("partnerid", Data.GetValue("mch_id"));
                            result.SetValue("prepayid", Data.GetValue("prepay_id"));
                            result.SetValue("noncestr", Data.GetValue("nonce_str"));
                            result.SetValue("package", "Sign=WXPay");
                            result.SetValue("timestamp", (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
                            result.SetValue("sign", result.MakeSign());
                            result.SetValue("total_fee", RealOrderAmount);

                            // 平台商城不支持积分
                            if (CorpId != 0)
                            {
                                // 计算预赠送积分
                                int p1 = new AppPoint().CalcPresentedPointForGoods(CorpId, (RealOrderAmount / 100.0m));
                                result.SetValue("presented_points", p1);
                            }

                            if (!string.IsNullOrEmpty(useHistoryID))
                            {
                                result.SetValue("out_trade_no", OrderId);
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
                                                        UseWay = AppPointUseWayConverter.GetKey(AppPointUseWay.StoreTradeDeduction),
                                                        PointBalance = pointBalance - UsePoints,
                                                        DeductionAmount = deductionAmount,
                                                        Remark = AppPointUseWayConverter.GetValue(AppPointUseWay.StoreTradeDeduction),
                                                        LockedPoints = UsePoints
                                                    }, appTrans);

                                // 存储积分使用记录与订单关联关系
                                appConn.Execute(@"INSERT INTO Tb_App_Point_UseHistoryOrder(UseHistoryID, OrderID, Payment, UsableObject) 
                                                    VALUES(@UseHistoryID, @OrderID, '微信', @UsableObject)",
                                                    new
                                                    {
                                                        UseHistoryID = useHistoryID,
                                                        OrderID = OrderId,
                                                        UsableObject = AppPointUsableObjectConverter.GetValue(AppPointUsableObject.Goods)
                                                    }, appTrans);
                            }

                            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                            {
                                if (RealOrderAmount == 0)
                                {
                                    int i = conn.Execute(@"UPDATE Tb_Charge_Receipt SET PrepayStr=@prepay_str,Method='微信',IsPay='已付款',ReturnCode='TRADE_FINISHED',
                                                            ReturnMsg='TRADE_FINISHED',PayDate=GetDate(),Amount=@Amount,CouponAmount=0,RealAmount=0,UsePoints=@UsePoints,
                                                            PointDiscountAmount=@Amount,PointUseHistoryID=@PointUseHistoryID WHERE OrderId=@OrderId",
                                                            new
                                                            {
                                                                prepay_str = Data.GetValue("prepay_id").ToString(),
                                                                Amount = Amount,
                                                                UsePoints = UsePoints,
                                                                PointUseHistoryID = useHistoryID,
                                                                OrderId = OrderId
                                                            });
                                    if (i == 1)
                                    {
                                        appConn.Execute(@"UPDATE Tb_App_Point_UseHistory SET IsEffect=1 WHERE IID=@IID;
                                                          DELETE FROM Tb_App_Point_Locked WHERE UseHistoryID=@IID",
                                                            new { IID = useHistoryID }, appTrans);
                                    }
                                }
                                else
                                {
                                    conn.Execute(@"UPDATE Tb_Charge_Receipt SET PrepayStr=@prepay_str,UsePoints=@UsePoints,PointDiscountAmount=@PointDiscountAmount, 
                                                    PointUseHistoryID=@PointUseHistoryID WHERE OrderId = @OrderId",
                                                    new
                                                    {
                                                        prepay_str = Data.GetValue("prepay_id").ToString(),
                                                        UsePoints = UsePoints,
                                                        PointDiscountAmount = deductionAmount,
                                                        PointUseHistoryID = useHistoryID,
                                                        OrderId = OrderId
                                                    });
                                }
                            }

                            appTrans?.Commit();
                            return JSONHelper.FromString(true, result.ToJson());
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
                        return JSONHelper.FromString(false, BussinessResult);
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
        /// 订单继续支付
        /// </summary>
        public string GoOnGenerateOrder(DataSet Ds)
        {
            DataRow Row = Ds.Tables[0].Rows[0];

            bool IsBankOk = false;
            string OrderId = Row["OrderId"].ToString();
            string prepay_str = "";

            int UsePoints = 0;
            if (Row.Table.Columns.Contains("UsePoints") && !string.IsNullOrEmpty(Row["UsePoints"].ToString()))
            {
                UsePoints = AppGlobal.StrToInt(Row["UsePoints"].ToString());
            }

            decimal goodsTotalAmount = 0.0m;                // 商品总价
            decimal goodsSupportPointAmount = 0.0m;         // 商品支持积分的金额

            WxPayConfig wxPayConfig = null;     
            
            Tb_Charge_Receipt orderInfo = null;                         // 订单信息
            IEnumerable<Tb_Charge_ReceiptDetail> orderDetail = null;    // 订单详情
            Tb_System_BusinessCorp bussInfo = null;                     // 商家信息
            short corpId = 0;
            bool suppprtPoint = false;

            // 商品信息
            using (var bzconn = new SqlConnection(PubConstant.BusinessContionString))
            {
                orderInfo = bzconn.Query<Tb_Charge_Receipt>("SELECT * FROM Tb_Charge_Receipt WHERE OrderId=@OrderId", new { OrderId = OrderId }).FirstOrDefault();

                if (orderInfo == null)
                {
                    return new ApiResult(false, "抱歉，该订单信息不存在").toJson();
                }

                bussInfo = bzconn.Query<Tb_System_BusinessCorp>(@"SELECT * FROM Tb_System_BusinessCorp WHERE BussId=@BussId 
                                                                    AND isnull(IsDelete,0)=0 AND isnull(IsClose,'未关闭')='未关闭'",
                                                                    new { BussId = orderInfo.BussId }).FirstOrDefault();

                if (bussInfo == null)
                {
                    return new ApiResult(false, "抱歉，该商家信息不存在").toJson();
                }

                wxPayConfig = GenerateConfig(orderInfo.BussId);
                if (wxPayConfig == null)
                {
                    return JSONHelper.FromString(false, "未配置证书文件");
                }

                string bussNature = bzconn.Query<string>(@"SELECT BussNature FROM Tb_System_BusinessCorp WHERE BussId=@BussId",
                                                            new { BussId = orderInfo.BussId }).FirstOrDefault();
                if (bussNature == null || bussNature == "周边商家")
                {
                    corpId = bzconn.Query<short>(@"SELECT CorpID FROM Unified..Tb_Community WHERE Id=(SELECT CommunityId FROM Tb_System_BusinessConfig WHERE BussId=@BussId)",
                                                    new { BussId = orderInfo.BussId }).FirstOrDefault();
                }
                else if (bussNature == "物管商城")
                {
                    corpId = bzconn.Query<short>(@"SELECT CorpID FROM Tb_System_BusinessCorp_Config WHERE BussId=@BussId",
                        new { BussId = orderInfo.BussId }).FirstOrDefault();
                }
                else if (bussNature == "平台商城")
                {

                }

                orderDetail = bzconn.Query<Tb_Charge_ReceiptDetail>(@"SELECT * FROM Tb_Charge_ReceiptDetail WHERE ReceiptCode=@ReceiptCode",
                                                                        new { ReceiptCode = orderInfo.Id });


                string sql = @"SELECT ResourcesID,isnull(ResourcesSalePrice,0) AS ResourcesSalePrice,isnull(ResourcesDisCountPrice,0) AS ResourcesDisCountPrice,
                                isnull(IsGroupBuy,'否') AS IsGroupBuy,isnull(GroupBuyPrice,0) AS GroupBuyPrice,GroupBuyEndDate,ResourcesCount,
                                isnull(IsSupportPoint,0) AS IsSupportPoint
                                FROM Tb_Resources_Details 
                                WHERE IsRelease='是' AND IsStopRelease='否' AND isnull(IsDelete,0)=0 AND ResourcesID=@ResourcesID";

                foreach (var item in orderDetail)
                {
                    var goodsInfo = bzconn.Query(sql, new { ResourcesID = item.ResourcesID }).FirstOrDefault();
                    var specPrice = bzconn.Query<decimal>(@"SELECT sum(Price) FROM Tb_ResourcesSpecificationsPrice WHERE ResourcesID=@ResourcesID AND SpecId IS NOT NULL 
                                                            AND SpecId IN(SELECT SpecId FROM Tb_ShoppingDetailed WHERE ShoppingId=@ShoppingId);",
                                                            new { ResourcesID = goodsInfo.ResourcesID, ShoppingId = item.ShoppingId }).FirstOrDefault();

                    if (goodsInfo != null)
                    {
                        if ((int)(goodsInfo.ResourcesCount) < item.Quantity)
                        {
                            return new ApiResult(false, "抱歉，相关商品库存不足").toJson();
                        }

                        // 是在是团购，且在团购有效期间
                        if (goodsInfo.IsGroupBuy == "是" && goodsInfo.GroupBuyEndDate != null && AppGlobal.StrToDate(goodsInfo.GroupBuyEndDate) > DateTime.Now)
                        {
                            goodsTotalAmount += ((goodsInfo.GroupBuyPrice + specPrice) * item.Quantity);

                            // 支持积分
                            if (goodsInfo.IsSupportPoint == 1)
                            {
                                suppprtPoint = true;
                                goodsSupportPointAmount += ((goodsInfo.GroupBuyPrice + specPrice) * item.Quantity);
                            }
                        }
                        else
                        {
                            goodsTotalAmount += ((goodsInfo.ResourcesSalePrice - goodsInfo.ResourcesDisCountPrice + specPrice) * item.Quantity);

                            // 支持积分
                            if (goodsInfo.IsSupportPoint == 1)
                            {
                                suppprtPoint = true;
                                goodsSupportPointAmount += ((goodsInfo.ResourcesSalePrice - goodsInfo.ResourcesDisCountPrice + specPrice) * item.Quantity);
                            }
                        }
                    }
                    else
                    {
                        return new ApiResult(false, "抱歉，相关商品已下架").toJson();
                    }
                }
            }

            using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                if (appConn.State == ConnectionState.Closed)
                {
                    appConn.Open();
                }

                var appTrans = appConn.BeginTransaction();

                string useHistoryID = null;
                
                decimal deductionAmount = 0.0m;                 // 可抵扣的金额
                int pointBalance = 0;                           // 积分余额

                if (UsePoints > 0)
                {
                    if (suppprtPoint == false)
                    {
                        return new ApiResult(false, "抱歉，订单内所有商品均不支持积分抵用").toJson();
                    }

                    if (corpId == 0)
                    {
                        return new ApiResult(false, "抱歉，平台商城暂不支持积分抵扣").toJson();
                    }

                    #region 判断积分是否足够
                    try
                    {
                        // 要使用的积分是否大于用户积分余额
                        pointBalance = appConn.Query<int>("SELECT PointBalance FROM Tb_App_UserPoint WHERE UserID=@UserID", new { UserID = orderInfo.UserId }, appTrans).FirstOrDefault();
                        if (pointBalance < UsePoints)
                        {
                            return new ApiResult(false, "积分余额不足").toJson();
                        }

                        // 商品积分权限不跟小区挂钩，统一由公司设置
                        var controlInfo = appConn.Query<Tb_Control_AppPoint>(@"SELECT * FROM Tb_Control_AppPoint WHERE CorpID=@CorpID AND IsEnable=1 AND CommunityID IS NULL",
                                                                            new { CorpID = corpId }, appTrans).FirstOrDefault();

                        if (controlInfo == null || controlInfo.IsEnable == false)
                        {
                            controlInfo = Tb_Control_AppPoint.DefaultControl;
                            controlInfo.CommunityID = Guid.Empty.ToString();
                        }

                        // 不允许抵用商品
                        if (controlInfo.AllowDeductionGoodsFees == false)
                        {
                            return new ApiResult(false, "暂不支持积分抵用功能").toJson();
                        }

                        // 积分抵用规则
                        var ruleInfo = appConn.Query($@"SELECT IID,ConditionAmount,DiscountsAmount,DeductionObject,StartTime,EndTime 
                                                        FROM Tb_App_Point_PropertyDeductionRule a LEFT JOIN Tb_Dictionary_Point_UsableObject b
                                                        ON a.DeductionObject=b.[Key] 
                                                        WHERE a.CorpID=@CorpID AND a.CommunityID IS NULL AND a.DeductionObject=@DeductionObject
                                                        AND getdate() BETWEEN StartTime AND EndTime AND a.IsDelete=0 ORDER BY ConditionAmount,DiscountsAmount",
                                                        new { CorpID = corpId, DeductionObject = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.Goods) }, appTrans);

                        if (ruleInfo.Count() == 0)
                        {
                            return new ApiResult(false, "积分抵用规则未设置或已失效").toJson();
                        }

                        decimal goodsMaxDiscountsAmount = 0.0m;      // 积分可抵用金额

                        // 确定积分可抵用金额
                        if (goodsSupportPointAmount > 0)
                        {
                            foreach (var item in ruleInfo)
                            {
                                if (goodsSupportPointAmount >= item.ConditionAmount)
                                {
                                    goodsMaxDiscountsAmount = item.DiscountsAmount;
                                }
                            }
                        }

                        // 抵用的费用
                        deductionAmount = UsePoints / (decimal)controlInfo.PointExchangeRatio;

                        // 积分数量不正常
                        if (deductionAmount > goodsMaxDiscountsAmount)
                        {
                            return new ApiResult(false, "使用积分超过可抵用最大金额").toJson();
                        }

                        // 积分余额足够
                        useHistoryID = Guid.NewGuid().ToString();
                    }
                    catch (Exception ex)
                    {
                        appTrans.Rollback();
                        return new ApiResult(false, "积分验证失败").toJson();
                    }
                    #endregion
                }

                // 积分数量正常，生成订单
                try
                {
                    decimal RealOrderAmount = goodsTotalAmount - deductionAmount;

                    // 微信签名订单信息
                    WxPayData Data = new WxPayData();
                    string BankResult = GenerateBankOrder(orderInfo.BussId, OrderId, orderInfo.txnTime, (int)(RealOrderAmount * 100), ref IsBankOk, ref Data, wxPayConfig);

                    if (IsBankOk == true)
                    {
                        // 更新订单信息
                        using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                        {
                            conn.Execute(@"UPDATE Tb_Charge_Receipt SET PrepayStr=@prepay_str WHERE OrderId=@OrderId",
                                new { prepay_str = prepay_str, OrderId = orderInfo.OrderId });
                        }

                        //向手机端返回银行记录
                        WxPayData result = new WxPayData();
                        result.SetValue("appid", Data.GetValue("appid"));
                        result.SetValue("partnerid", Data.GetValue("mch_id"));
                        result.SetValue("prepayid", Data.GetValue("prepay_id"));
                        result.SetValue("noncestr", Data.GetValue("nonce_str"));
                        result.SetValue("package", "Sign=WXPay");
                        result.SetValue("total_fee", RealOrderAmount);
                        result.SetValue("timestamp", (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
                        result.SetValue("sign", result.MakeSign());

                        // 平台商城不支持积分
                        if (corpId != 0)
                        {
                            // 计算预赠送积分
                            int p1 = new AppPoint().CalcPresentedPointForGoods(corpId, RealOrderAmount);
                            prepay_str = prepay_str.Insert(prepay_str.Length - 1, ",\"presented_points\":" + p1 + "");
                        }

                        if (!string.IsNullOrEmpty(useHistoryID))
                        {
                            result.SetValue("out_trade_no", OrderId);
                            result.SetValue("point_use_history_id", useHistoryID);

                            appConn.Execute($@"UPDATE Tb_App_UserPoint SET PointBalance=(PointBalance-@UsePoints) WHERE UserID=@UserID;
                                                    INSERT INTO Tb_App_Point_UseHistory(IID, UserID, UseWay, UsePoints, PointBalance, DeductionAmount, Remark)
                                                        VALUES(@UseHistoryID, @UserID, @UseWay, @UsePoints, @PointBalance, @DeductionAmount, @Remark);
                                                    INSERT INTO Tb_App_Point_Locked(UserID, UseHistoryID, LockedPoints) 
                                                        VALUES (@UserID, @UseHistoryID, @LockedPoints);",
                                                new
                                                {
                                                    UsePoints = UsePoints,
                                                    UserID = orderInfo.UserId,
                                                    UseHistoryID = useHistoryID,
                                                    UseWay = AppPointUseWayConverter.GetKey(AppPointUseWay.StoreTradeDeduction),
                                                    PointBalance = pointBalance - UsePoints,
                                                    DeductionAmount = deductionAmount,
                                                    Remark = AppPointUseWayConverter.GetValue(AppPointUseWay.StoreTradeDeduction),
                                                    LockedPoints = UsePoints
                                                }, appTrans);

                            // 存储积分使用记录与订单关联关系
                            appConn.Execute(@"INSERT INTO Tb_App_Point_UseHistoryOrder(UseHistoryID, OrderID, Payment, UsableObject) 
                                                    VALUES(@UseHistoryID, @OrderID, '微信', @UsableObject)",
                                                new
                                                {
                                                    UseHistoryID = useHistoryID,
                                                    OrderID = OrderId,
                                                    UsableObject = AppPointUsableObjectConverter.GetValue(AppPointUsableObject.Goods)
                                                }, appTrans);
                        }

                        using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                        {
                            if (RealOrderAmount == 0)
                            {
                                int i = conn.Execute(@"UPDATE Tb_Charge_Receipt SET PrepayStr=@prepay_str,Method='微信',IsPay='已付款',ReturnCode='TRADE_FINISHED',
                                                            ReturnMsg='TRADE_FINISHED',PayDate=GetDate(),Amount=@Amount,CouponAmount=0,RealAmount=0,UsePoints=@UsePoints,
                                                            PointDiscountAmount=@Amount,PointUseHistoryID=@PointUseHistoryID WHERE OrderId=@OrderId",
                                                        new
                                                        {
                                                            prepay_str = prepay_str.ToString(),
                                                            Amount = goodsTotalAmount,
                                                            UsePoints = UsePoints,
                                                            PointUseHistoryID = useHistoryID,
                                                            OrderId = OrderId
                                                        });
                                if (i == 1)
                                {
                                    appConn.Execute(@"UPDATE Tb_App_Point_UseHistory SET IsEffect=1 WHERE IID=@IID;
                                                          DELETE FROM Tb_App_Point_Locked WHERE UseHistoryID=@IID",
                                                        new { IID = useHistoryID }, appTrans);
                                }
                            }
                            else
                            {
                                conn.Execute(@"UPDATE Tb_Charge_Receipt SET PrepayStr=@prepay_str,UsePoints=@UsePoints,PointDiscountAmount=@PointDiscountAmount, 
                                                    PointUseHistoryID=@PointUseHistoryID WHERE OrderId = @OrderId",
                                                new
                                                {
                                                    prepay_str = prepay_str.ToString(),
                                                    UsePoints = UsePoints,
                                                    PointDiscountAmount = deductionAmount,
                                                    PointUseHistoryID = useHistoryID,
                                                    OrderId = OrderId
                                                });
                            }
                        }

                        appTrans?.Commit();
                        return JSONHelper.FromString(true, prepay_str);
                    }
                    else
                    {
                        appTrans?.Rollback();
                        return JSONHelper.FromString(false, BankResult);
                    }
                }
                catch (Exception ex)
                {
                    appTrans?.Rollback();
                    return JSONHelper.FromString(false, ex.Message + ex.StackTrace);
                }
            }
        }
    }
}