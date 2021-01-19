using System;

namespace App.Model
{
    /// <summary>
    /// 优惠券大类
    /// </summary>
    [Serializable]
	public enum AppCouponCategory
    {
        /// <summary>
        /// 抵用券
        /// </summary>
        Voucher = 1,

        /// <summary>
        /// 折扣券
        /// </summary>
        Discount = 2,

        /// <summary>
        /// 满减券
        /// </summary>
        FullReduction = 3,

        /// <summary>
        /// 红包
        /// </summary>
        RedPacket = 4,
    }

    /// <summary>
    /// 优惠券大类转换器
    /// </summary>
    public static class AppCouponCategoryConverter
    {
        /// <summary>
        /// 转换为<see cref="AppCouponCategory">枚举
        /// </summary>
        public static AppCouponCategory Convert(string key)
        {
            switch (key)
            {
                case "0001":
                    return AppCouponCategory.Voucher;
                case "0002":
                    return AppCouponCategory.Discount;
                case "0003":
                    return AppCouponCategory.FullReduction;
                case "0004":
                    return AppCouponCategory.RedPacket;
                default:
                    throw new ArgumentException("不存在该优惠券类型");
            }
        }

        /// <summary>
        /// 获取key
        /// </summary>
        public static string GetKey(AppCouponCategory @enum)
        {
            switch (@enum)
            {
                case AppCouponCategory.Voucher:
                    return "0001";
                case AppCouponCategory.Discount:
                    return "0002";
                case AppCouponCategory.FullReduction:
                    return "0003";
                case AppCouponCategory.RedPacket:
                    return "0004";
                default:
                    throw new ArgumentException("不存在该优惠券类型");
            }
        }
    }
}

