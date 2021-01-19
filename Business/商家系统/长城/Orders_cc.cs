using Aop.Api.Domain;
using Common.Enum;
using Dapper;
using DapperExtensions;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Model.Model.Buss;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TWTools.Push;

namespace Business
{
    public partial class Orders
    {
        enum OrderState
        {
            All = 0,

            /// <summary>
            /// 待付款
            /// </summary>
            WaitingPay = 1,

            /// <summary>
            /// 待发货
            /// </summary>
            WaitingSendOut = 2,

            /// <summary>
            /// 待收货
            /// </summary>
            WaitingReceiving = 3,

            /// <summary>
            /// 待评价
            /// </summary>
            WaitingEvaluation = 4,

            /// <summary>
            /// 待退款
            /// </summary>
            WaitingRefund = 11,

            /// <summary>
            /// 已退款
            /// </summary>
            Refunded = 12,

            /// <summary>
            /// 已删除
            /// </summary>
            Deleted = 99,
        }

        /// <summary>
        /// 生成商家订单
        /// </summary>
        public string SubmitOrder(DataSet Ds)
        {
            DataRow Row = Ds.Tables[0].Rows[0];

            string txnTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            string BussId = Row["BussId"].ToString();
            string UserId = Row["UserId"].ToString();
            string Name = Row["Name"].ToString();
            string Mobile = Row["Mobile"].ToString();
            string DeliverAddress = Row["DeliverAddress"].ToString();

            int? corpId = null;
            string communityId = null;
            if (Row.Table.Columns.Contains("CorpId") && !string.IsNullOrEmpty(Row["CorpId"].ToString()))
            {
                corpId = AppGlobal.StrToInt(Row["CorpId"].ToString());
            }
            if (Row.Table.Columns.Contains("CommunityId") && !string.IsNullOrEmpty(Row["CommunityId"].ToString()))
            {
                communityId = Row["CommunityId"].ToString();
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
                ReceiptSign = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(1000, 9999).ToString(),
                Name = Name,
                IsPay = "未付款",
                IsReceive = "未收货",
                IsDeliver = "未发货",
                ReceiptMemo = Row["ReceiptMemo"].ToString(),
                ReceiptType = "通用票据",
                ReceiptDate = DateTime.Now,
                PayDate = null,
                MchId = "",
                Partner = "",
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

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();

                try
                {
                    // 保存订单信息
                    conn.Insert(Receipt, trans);

                    if (!string.IsNullOrEmpty(communityId))
                    {
                        conn.Execute("UPDATE Tb_Charge_Receipt SET CommunityId=@CommunityId WHERE ID=@IID",
                            new { CommunityId = communityId, IID = iid }, trans);
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
                    (SELECT Balance1 = (SELECT Balance FROM Tb_Resources_CouponBalance
                        WHERE UserId = @UserId
                        AND CorpId = @CorpId AND BussId = @BussId),
                        Balance2 = (SELECT Balance FROM Tb_Resources_CouponBalance
                            WHERE UserId = @UserId
                            AND CorpId = @CorpId AND BussId IS NULL)) as x; ",
                        new { UserId = Receipt.UserId, CorpId = corpId, BussId = Receipt.BussId }, trans).FirstOrDefault();

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
                        WHERE isnull(IsDelete, 0) = 0 AND IsRelease = '是' AND IsStopRelease = '否' AND ResourcesID = @ResourcesID";

                        Tb_Resources_Details ResourcesDetail = conn.Query<Tb_Resources_Details>(sql, new
                        {
                            ResourcesID = ReceiptDetail.ResourcesID
                        }, trans).FirstOrDefault();

                        // 商品存在，期间未失效
                        if (ResourcesDetail != null)
                        {
                            // 库存不足，跳过
                            if (ReceiptDetail.Quantity > ResourcesDetail.ResourcesCount)
                            {
                                return JSONHelper.FromString(false, "部分商品库存不足");
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
                            MobileSoft.Model.Resources.Tb_ResourcesSpecificationsPrice propertyInfo = conn.Query<MobileSoft.Model.Resources.Tb_ResourcesSpecificationsPrice>(sql, new { ShoppingId = DetailRow["ShoppingId"].ToString() }, trans).FirstOrDefault();

                            if (propertyInfo != null && propertyInfo.Price.HasValue)
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
                            conn.Insert(ReceiptDetail, trans);
                        }

                        // 删除购物车
                        conn.Execute("UPDATE Tb_ShoppingCar SET IsDelete=1 WHERE Id=@ShoppingId",
                            new
                            {
                                ShoppingId = ReceiptDetail.ShoppingId
                            }, trans);
                    }

                    // 3、更新用户优惠券信息
                    if (totalUseCouponMoney > 0)
                    {
                        conn.Execute("proc_Resources_CouponBalance_Use",
                            new { UserId = Receipt.UserId, CorpId = corpId, BussId = Receipt.BussId, UseMoney = totalUseCouponMoney }, trans, null, CommandType.StoredProcedure);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }

            return new ApiResult(true, new { Id = Receipt.Id, OrderId = Receipt.OrderId }).toJson();
        }

        /// <summary>
        /// 生成商家订单
        /// </summary>
        public string SubmitOrder_Changcheng(DataSet Ds)
        {
            DataRow Row = Ds.Tables[0].Rows[0];

            string txnTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            string BussId = Row["BussId"].ToString();
            string UserId = Row["UserId"].ToString();
            string Name = Row["Name"].ToString();
            string Mobile = Row["Mobile"].ToString();
            string DeliverAddress = Row["DeliverAddress"].ToString();

            int dispatchingType = 0;
            if (Row.Table.Columns.Contains("DispatchingType") && !string.IsNullOrEmpty(Row["DispatchingType"].ToString()))
            {
                int.TryParse(Row["DispatchingType"].ToString(), out dispatchingType);
            }

            String expectedDeliveryTime = String.Empty;
            if (Row.Table.Columns.Contains("ExpectedDeliveryTime") && !string.IsNullOrEmpty(Row["ExpectedDeliveryTime"].ToString()))
            {
                expectedDeliveryTime = Row["ExpectedDeliveryTime"].ToString();
            }

            String province = String.Empty;
            if (Row.Table.Columns.Contains("Province") && !string.IsNullOrEmpty(Row["Province"].ToString()))
            {
                province = Row["Province"].ToString();
            }

            decimal freight = 0M;
            if (Row.Table.Columns.Contains("Freight") && !string.IsNullOrEmpty(Row["Freight"].ToString()) && decimal.TryParse(Row["Freight"].ToString(), out freight))
            {

            }

            int? corpId = null;
            string communityId = null;
            if (Row.Table.Columns.Contains("CorpId") && !string.IsNullOrEmpty(Row["CorpId"].ToString()))
            {
                corpId = AppGlobal.StrToInt(Row["CorpId"].ToString());
            }
            if (Row.Table.Columns.Contains("CommunityId") && !string.IsNullOrEmpty(Row["CommunityId"].ToString()))
            {
                communityId = Row["CommunityId"].ToString();
            }

            var iid = Guid.NewGuid().ToString();
            var alipayBusinessOrder_Changcheng = new AlipayBusinessOrder_Changcheng();
            var config = alipayBusinessOrder_Changcheng.GetGenerateConfig(BussId);
            // 生成商家收款订单
            Tb_Charge_Receipt Receipt = new Tb_Charge_Receipt
            {
                Id = iid,
                CorpId = corpId,
                BussId = BussId.ToString(),
                UserId = UserId,
                OrderId = Guid.NewGuid().ToString(),
                ReceiptSign = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(1000, 9999).ToString(),
                Name = Name,
                IsPay = "未付款",
                IsReceive = "未收货",
                IsDeliver = "未发货",
                ReceiptMemo = Row["ReceiptMemo"].ToString(),
                ReceiptType = "通用票据",
                ReceiptDate = DateTime.Now,
                PayDate = null,
                MchId = config == null ? "" : config.partner,
                Partner = config == null ? "" : config.partner,
                PrepayStr = "",
                txnTime = txnTime.ToString(),
                ReturnCode = "",
                ReturnMsg = "",
                Express = "",
                ExpressNum = "",
                Mobile = Mobile,  //联系电话
                DeliverAddress = province + DeliverAddress, //收货地址
                IsDelete = 0,
                CommunityID = communityId,
                DispatchingType = dispatchingType,
                //Freight = freight//运费
            };

            if (dispatchingType != 0 && dispatchingType == (int)BusiinessDispatchingEnum.Dispatching) Receipt.RequestDeliveryTime = expectedDeliveryTime;
            if (dispatchingType != 0 && dispatchingType == (int)BusiinessDispatchingEnum.TakeTheir) Receipt.EstimatedPickUpTime = expectedDeliveryTime;


            // 是否使用优惠券抵扣
            if (Row.Table.Columns.Contains("UseCoupon"))
            {
                Receipt.IsUseCoupon = AppGlobal.StrToInt(Row["UseCoupon"].ToString());
            }

            List<String> shoppingIdS = new List<String>();
            foreach (DataRow DetailRow in Ds.Tables["Product"].Rows)
            {
                shoppingIdS.Add(DetailRow["ShoppingId"].ToString());
            }

            if (dispatchingType == 1)
            {
                var fee = PMSFreight.GetGetFreight(province, shoppingIdS);
                if (fee == -2) return JSONHelper.FromString(false, "对不起，您的收货地址不在配送范围内");
                if (fee >= 0 && fee != freight) return JSONHelper.FromString(false, "运费不统一");
                Receipt.Freight = fee;
            }

            String moblie = String.Empty;
            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();

                try
                {
                    // 保存订单信息
                    conn.Insert(Receipt, trans);

                    //获取商家的手机号
                    String businessMoblieSql = "  SELECT  BussMobileTel FROM Tb_System_BusinessCorp WHERE bussId=@bussId ";
                    moblie = conn.QueryFirstOrDefault<String>(businessMoblieSql, new { bussId = BussId }, trans);

                    if (!string.IsNullOrEmpty(communityId))
                    {
                        conn.Execute("UPDATE Tb_Charge_Receipt SET CommunityId=@CommunityId WHERE ID=@IID",
                            new { CommunityId = communityId, IID = iid }, trans);
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
                    (SELECT Balance1 = (SELECT Balance FROM Tb_Resources_CouponBalance
                        WHERE UserId = @UserId
                        AND CorpId = @CorpId AND BussId = @BussId),
                        Balance2 = (SELECT Balance FROM Tb_Resources_CouponBalance
                            WHERE UserId = @UserId
                            AND CorpId = @CorpId AND BussId IS NULL)) as x; ",
                        new { UserId = Receipt.UserId, CorpId = corpId, BussId = Receipt.BussId }, trans).FirstOrDefault();

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

                        decimal specificationMoney = 0M;


                        // 商品的单价和可优惠单价
                        string sql = @"SELECT * FROM Tb_Resources_Details 
                        WHERE isnull(IsDelete, 0) = 0 AND IsRelease = '是' AND IsStopRelease = '否' AND ResourcesID = @ResourcesID";

                        Tb_Resources_Details ResourcesDetail = conn.Query<Tb_Resources_Details>(sql, new
                        {
                            ResourcesID = ReceiptDetail.ResourcesID
                        }, trans).FirstOrDefault();

                        #region 根据购物车获取具体的某一个规格属性 ，并使用该规格属性上得价格
                        String sqlStr = @"SELECT A.Price,A.DiscountAmount,A.GroupBuyingPrice FROM  Tb_ResourcesSpecificationsPrice AS A
                                        INNER JOIN  (SELECT B.ResourcesID,C.PropertysId,C.SpecId FROM Tb_ShoppingCar AS B
                                        LEFT JOIN Tb_ShoppingDetailed AS C ON B.Id=C.ShoppingId
                                        WHERE B.Id IS NOT NULL AND B.Id=@Id) AS D  ON  D.ResourcesID=A.ResourcesID AND D.PropertysId =A.PropertyId AND D.SpecId=A.SpecId";

                        var praceInfo = conn.QueryFirstOrDefault(sqlStr, new { Id = DetailRow["ShoppingId"].ToString() }, trans);
                        if (null == praceInfo) return JSONHelper.FromString(false, "商品没有规格属性");

                        ResourcesDetail.ResourcesSalePrice = (decimal)praceInfo.Price;
                        ResourcesDetail.GroupBuyPrice = (decimal)praceInfo.GroupBuyingPrice;
                        ResourcesDetail.ResourcesDisCountPrice = (decimal)praceInfo.DiscountAmount;

                        #endregion

                        // 商品存在，期间未失效
                        if (ResourcesDetail != null)
                        {

                            //现在判断库存 修改为判断规格库存
                            sql = @"SELECT ISNULL(Inventory,0) FROM  Tb_ResourcesSpecificationsPrice AS A
                                    INNER JOIN  (SELECT B.ResourcesID,C.PropertysId,C.SpecId FROM Tb_ShoppingCar AS B
                                    LEFT JOIN Tb_ShoppingDetailed AS C ON B.Id=C.ShoppingId
                                    WHERE B.Id IS NOT NULL AND B.Id=@Id) AS D  ON  D.ResourcesID=A.ResourcesID AND D.PropertysId =A.PropertyId AND D.SpecId=A.SpecId;";
                            var inventory = conn.QueryFirstOrDefault<decimal>(sql, new { Id = ReceiptDetail.ShoppingId },trans);
                            // 库存不足，跳过
                            if (ReceiptDetail.Quantity > inventory)
                            {
                                return JSONHelper.FromString(false, "部分商品库存不足");
                            }

                            //String sqlPrice = @"select b.Price from ( select SpecId,PropertysId from Tb_ShoppingDetailed where  ShoppingId=@ShoppingId) AS A
                            //INNER JOIN (SELECT Price,SpecId,PropertyId FROM View_Tb_ResourcesSpecificationsPrice_Filter  WHERE  ISNULL(ISDELETE,0)=0 AND  SpecId IS NOT NULL AND BussId=@BussId AND ResourcesID =@ResourcesID  ) AS  B  ON B.SpecId=A.SpecId AND B.PropertyId=A.PropertysId";
                            //var priceInfo = conn.QueryFirstOrDefault<decimal?>(sqlPrice, new { ShoppingId = ReceiptDetail.ShoppingId, BussId = BussId, ResourcesID = ReceiptDetail.ResourcesID }, trans);

                            //if (priceInfo.HasValue)
                            //{
                            //    ReceiptDetail.SalesPrice = priceInfo.Value;
                            //    ResourcesDetail.ResourcesSalePrice = priceInfo.Value;
                            //}
                            //else
                            //{
                            //    ReceiptDetail.SalesPrice = ResourcesDetail.ResourcesSalePrice;
                            //}
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
                                //团购价不算优惠价格
                                price = (ResourcesDetail.GroupBuyPrice.Value) * ReceiptDetail.Quantity;
                            }
                            else
                            {
                                price = (ResourcesDetail.ResourcesSalePrice - ResourcesDetail.ResourcesDisCountPrice) * ReceiptDetail.Quantity;
                            }

                            // 2、商品规格追加价格
                            /*
                             时间：2020-06-03  20-11
                             修改人：翟国雄
                             逻辑 现在确定价格之后不再增加所谓得规格价格 如果有规格 直接使用规格价格减去优惠价格即可
                             */


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

                            //if (priceInfo.HasValue)
                            //{
                            //    ReceiptDetail.OffsetMoney = ReceiptDetail.OffsetMoney + specificationMoney;
                            //}

                            // 订单商品总价追加
                            totalAmount += price;

                            // 插入订单内商品数据
                            conn.Insert(ReceiptDetail, trans);
                        }

                        // 删除购物车
                        conn.Execute("UPDATE Tb_ShoppingCar SET IsDelete=1 WHERE Id=@ShoppingId",
                            new
                            {
                                ShoppingId = ReceiptDetail.ShoppingId
                            }, trans);
                    }

                    // 3、更新用户优惠券信息
                    if (totalUseCouponMoney > 0)
                    {
                        conn.Execute("proc_Resources_CouponBalance_Use",
                            new { UserId = Receipt.UserId, CorpId = corpId, BussId = Receipt.BussId, UseMoney = totalUseCouponMoney }, trans, null, CommandType.StoredProcedure);
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }

            //订单信息商家推送
            //OrderInfoPush(corpId, Receipt.Id, moblie);

            return new ApiResult(true, new { Id = Receipt.Id, OrderId = Receipt.OrderId }).toJson();
        }


        public static void OrderInfoPush(int? corpId, String orerId, String moblie)
        {
            try
            {
                if (!corpId.HasValue) return;
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    string appKey, appSecret, appIdentifier;
                    if (Common.Push.GetBusinessAppKeyAndAppSecret(PubConstant.tw2bsConnectionString, corpId.Value.ToString(), out appIdentifier, out appKey, out appSecret))
                    {
                        PushModel pushModel = new PushModel(appKey, appSecret, PushChannel.JPush, 3)
                        {
                            AppIdentifier = corpId.Value.ToString(),
                            Badge = 1,
                            LowerExtraKey = false,
                        };

                        pushModel.Extras.Add("Id", orerId);
                        pushModel.Audience.Category = PushAudienceCategory.Alias;
                        pushModel.Message = string.Format("你有新的订单，点击查看详情。");
                        pushModel.Command = PushCommand.BusinessCorp_Order;
                        pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                        pushModel.Audience.Objects.Add(moblie);
                        TWTools.Push.Push.SendAsync(pushModel);
                    }
                });
            }
            catch (Exception ex) { GetLog().Error(ex.Message); }
        }



        public string GetUserOrders(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户id不能为空");
            }

            var userId = row["UserId"].ToString();
            var state = OrderState.All;
            if (row.Table.Columns.Contains("State") && !string.IsNullOrEmpty(row["State"].ToString()))
            {
                state = (OrderState)AppGlobal.StrToInt(row["State"].ToString());
            }

            var condition = "";
            switch (state)
            {
                case OrderState.WaitingPay:
                    condition = " AND a.IsPay='未付款'";
                    break;
                case OrderState.WaitingSendOut:
                    condition = " AND a.IsPay='已付款' AND isnull(IsDeliver,'未发货')='未发货'";
                    break;
                case OrderState.WaitingReceiving:
                    condition = " AND a.IsPay='已付款' AND isnull(a.IsDeliver,'未发货')='已发货' AND isnull(IsReceive,'未收货')='未收货'";
                    break;
                case OrderState.WaitingEvaluation:
                    condition = " AND a.IsPay='已付款' AND isnull(a.IsDeliver,'未发货')='已发货' AND isnull(IsReceive,'未收货')='已收货'";
                    break;
                case OrderState.WaitingRefund:
                    break;
                case OrderState.Refunded:
                    condition = " AND a.IsPay='已付款' AND isnull(a.HandleState,'已退款')='已退款'";
                    break;
                case OrderState.Deleted:
                    condition = " AND a.IsDelete=1";
                    break;
            }

            var sql = $@"SELECT * FROM
                         (
                            SELECT row_number() OVER (ORDER BY a.ReceiptDate DESC) AS RN,
                                a.Id,b.BussName,isnull(b.BussNature,'周边商家') AS BussNature,a.ReceiptSign,
                                isnull(a.IsPay,'未付款') AS IsPay,
                                isnull(a.IsDeliver,'未发货') AS IsDeliver,
                                isnull(a.IsReceive,'未收货') AS IsReceive,
                                a.HandleState,a.ReceiptDate,
                                (SELECT count(1) FROM Tb_Charge_ReceiptDetail c WHERE c.ReceiptCode=a.Id) AS GoodsCount
                            FROM Tb_Charge_Receipt a
                                INNER JOIN Tb_System_BusinessCorp b ON a.BussId=b.BussId
                            WHERE a.UserId=@UserId { condition }
                         ) AS t
                         WHERE t.RN BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize;";

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var data = conn.Query(sql, new { UserId = userId }).ToList();
                if (data.Count > 0)
                {
                    sql = "";

                    for (int i = 0; i < data.Count; i++)
                    {
                        var orderInfo = data[i];


                    }
                }
            }

            return null;
        }
    }
}
