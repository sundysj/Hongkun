using System;

namespace App.Model
{
	/// <summary>
	/// 优惠券细类
	/// </summary>
	[Serializable]
	public enum AppCouponType
    {
        /// <summary>
        /// 指定小区物业费抵用券
        /// </summary>
        PrescribedCommunityPropertyFeeVoucher = 1001,

        /// <summary>
        /// 指定小区车位费抵用券
        /// </summary>
        PrescribedCommunityParkingFeeVoucher = 1002,

        /// <summary>
        /// 通用物业费抵用券
        /// </summary>
        UniversalCommunityPropertyFeeVoucher = 1003,

        /// <summary>
        /// 通用车位费抵用券
        /// </summary>
        UniversalCommunityParkingFeeVoucher = 1004,

        /// <summary>
        /// 指定商品抵用券
        /// </summary>
        PrescribedGoodsVoucher = 1005,

        /// <summary>
        /// 指定商品类别抵用券
        /// </summary>
        PrescribedGoodsCategoryVoucher = 1006,

        /// <summary>
        /// 指定店铺抵用券
        /// </summary>
        PrescribedStoreVoucher = 1007,

        /// <summary>
        /// 通用商品抵用券
        /// </summary>
        UniversalGoodsVoucher = 1008,

        /// <summary>
        /// 通用商品类型抵用券
        /// </summary>
        UniversalGoodsCategoryVoucher = 1009,

        /// <summary>
        /// 通用店铺抵用券
        /// </summary>
        UniversalStoreVoucher = 1010,

        /// <summary>
        /// 指定商品折扣券
        /// </summary>
        PrescribedGoodsDiscount = 2005,

        /// <summary>
        /// 指定商品类别折扣券
        /// </summary>
        PrescribedGoodsCategoryDiscount = 2006,

        /// <summary>
        /// 指定店铺折扣券
        /// </summary>
        PrescribedStoreDiscount = 2007,

        /// <summary>
        /// 通用商品折扣券
        /// </summary>
        UniversalGoodsDiscount = 2008,

        /// <summary>
        /// 通用商品类别折扣券
        /// </summary>
        UniversalGoodsCategoryDiscount = 2009,

        /// <summary>
        /// 通用店铺折扣券
        /// </summary>
        UniversalStoreDiscount = 2010,

        /// <summary>
        /// 指定商品满减券
        /// </summary>
        PrescribedGoodsFullReduction = 3005,

        /// <summary>
        /// 指定商品类别满减券
        /// </summary>
        PrescribedGoodsCategoryFullReduction = 3006,

        /// <summary>
        /// 指定店铺满减券
        /// </summary>
        PrescribedStoreFullReduction = 3007,

        /// <summary>
        /// 通用商品满减券
        /// </summary>
        UniversalGoodsFullReduction = 3008,

        /// <summary>
        /// 通用商品类别满减券
        /// </summary>
        UniversalGoodsCategoryFullReduction = 3009,

        /// <summary>
        /// 通用店铺满减券
        /// </summary>
        UniversalStoreFullReduction = 3010,

        /// <summary>
        /// 物业红包
        /// </summary>
        PropertyRedPacket = 4001,

        /// <summary>
        /// 店铺红包
        /// </summary>
        StoreRedPacket = 4002
	}

    /// <summary>
    /// 优惠券细类转换器
    /// </summary>
    public static class AppCouponTypeConverter
    {
        /// <summary>
        /// 转换为<see cref="AppCouponType">枚举
        /// </summary>
        public static AppCouponType Convert(string key)
        {
            switch (key)
            {
                // 抵用券
                case "1001":
                    return AppCouponType.PrescribedCommunityPropertyFeeVoucher;
                case "1002":
                    return AppCouponType.PrescribedCommunityParkingFeeVoucher;
                case "1003":
                    return AppCouponType.UniversalCommunityPropertyFeeVoucher;
                case "1004":
                    return AppCouponType.UniversalCommunityParkingFeeVoucher;
                case "1005":
                    return AppCouponType.PrescribedGoodsVoucher;
                case "1006":
                    return AppCouponType.PrescribedGoodsCategoryVoucher;
                case "1007":
                    return AppCouponType.PrescribedStoreVoucher;
                case "1008":
                    return AppCouponType.UniversalGoodsVoucher;
                case "1009":
                    return AppCouponType.UniversalGoodsCategoryVoucher;
                case "1010":
                    return AppCouponType.UniversalStoreVoucher;

                // 折扣券
                case "2005":
                    return AppCouponType.PrescribedGoodsDiscount;
                case "2006":
                    return AppCouponType.PrescribedGoodsCategoryDiscount;
                case "2007":
                    return AppCouponType.PrescribedStoreDiscount;
                case "2008":
                    return AppCouponType.UniversalGoodsDiscount;
                case "2009":
                    return AppCouponType.UniversalGoodsCategoryDiscount;
                case "2010":
                    return AppCouponType.UniversalStoreDiscount;

                // 满减券
                case "3005":
                    return AppCouponType.PrescribedGoodsFullReduction;
                case "3006":
                    return AppCouponType.PrescribedGoodsCategoryFullReduction;
                case "3007":
                    return AppCouponType.PrescribedStoreFullReduction;
                case "3008":
                    return AppCouponType.UniversalGoodsFullReduction;
                case "3009":
                    return AppCouponType.UniversalGoodsCategoryFullReduction;
                case "3010":
                    return AppCouponType.UniversalStoreFullReduction;

                // 红包
                case "4001":
                    return AppCouponType.PropertyRedPacket;
                case "4002":
                    return AppCouponType.StoreRedPacket;
                default:
                    throw new ArgumentException("不存在该优惠券类别");
            }
        }
    }
}

