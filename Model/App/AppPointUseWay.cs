using System;

namespace App.Model
{
	/// <summary>
	/// 积分使用方式
	/// </summary>
	[Serializable]
	public enum AppPointUseWay
	{
        /// <summary>
        /// 物业费抵用
        /// </summary>
        PropertyFeeDeduction = 1,

        /// <summary>
        /// 购物抵用
        /// </summary>
        StoreTradeDeduction = 2,

        /// <summary>
        /// 购物优惠券兑换
        /// </summary>
        StoreCouponExchange = 3,

        /// <summary>
        /// 物业礼品兑换
        /// </summary>
        PropertyGiftExchange = 4,

        /// <summary>
        /// 商家商品兑换
        /// </summary>
        StoreGoodsExchange = 5,

        /// <summary>
        /// 自动过期，暂不支持
        /// </summary>
        AutomaticallyExpire = 99
    }

    /// <summary>
    /// 积分使用方式转换器
    /// </summary>
    public static class AppPointUseWayConverter
    {
        /// <summary>
        /// 转换为<see cref="AppPointUseWay">枚举
        /// </summary>
        public static AppPointUseWay Convert(string key)
        {
            switch (key)
            {
                case "0001":
                    return AppPointUseWay.PropertyFeeDeduction;
                case "0002":
                    return AppPointUseWay.StoreTradeDeduction;
                case "0003":
                    return AppPointUseWay.StoreCouponExchange;
                case "0004":
                    return AppPointUseWay.PropertyGiftExchange;
                case "0005":
                    return AppPointUseWay.StoreGoodsExchange;
                case "0099":
                    return AppPointUseWay.AutomaticallyExpire;
                default:
                    throw new ArgumentException("不存在该使用方式");
            }
        }

        /// <summary>
        /// 获取key
        /// </summary>
        public static string GetKey(AppPointUseWay @enum)
        {
            switch (@enum)
            {
                case AppPointUseWay.PropertyFeeDeduction:
                    return "0001";
                case AppPointUseWay.StoreTradeDeduction:
                    return "0002";
                case AppPointUseWay.StoreCouponExchange:
                    return "0003";
                case AppPointUseWay.PropertyGiftExchange:
                    return "0004";
                case AppPointUseWay.StoreGoodsExchange:
                    return "0005";
                case AppPointUseWay.AutomaticallyExpire:
                    return "0099";
                default:
                    throw new ArgumentException("不存在该使用方式");
            }
        }

        public static string GetValue(AppPointUseWay @enum)
        {
            switch (@enum)
            {
                case AppPointUseWay.PropertyFeeDeduction:
                    return "物业费抵用";
                case AppPointUseWay.StoreTradeDeduction:
                    return "购物抵用";
                case AppPointUseWay.StoreCouponExchange:
                    return "购物优惠券兑换";
                case AppPointUseWay.PropertyGiftExchange:
                    return "物业礼品兑换";
                case AppPointUseWay.StoreGoodsExchange:
                    return "商家商品兑换";
                case AppPointUseWay.AutomaticallyExpire:
                    return "自动过期";
                default:
                    throw new ArgumentException("不存在该使用方式");
            }
        }
    }
}

