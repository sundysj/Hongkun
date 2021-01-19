using System;

namespace App.Model
{
	/// <summary>
	/// 优惠券获取方式
	/// </summary>
	[Serializable]
	public enum AppCouponGetWay
	{
        /// <summary>
        /// 免费领取
        /// </summary>
        FreeReceive = 1,

        /// <summary>
        /// 积分兑换
        /// </summary>
        PointExchange = 2,

        /// <summary>
        /// 物业缴费赠送
        /// </summary>
        PropertyPayment = 3
    }

    /// <summary>
    /// 优惠券获取方式转换器
    /// </summary>
    public static class AppCouponGetWayConverter
    {
        /// <summary>
        /// 转换为<see cref="AppCouponGetWay">枚举
        /// </summary>
        public static AppCouponGetWay Convert(string key)
        {
            switch (key)
            {
                case "0001":
                    return AppCouponGetWay.FreeReceive;
                case "0002":
                    return AppCouponGetWay.PointExchange;
                case "0003":
                    return AppCouponGetWay.PropertyPayment;
                default:
                    throw new ArgumentException("不存在该获取方式");
            }
        }

        /// <summary>
        /// 获取key
        /// </summary>
        public static string GetKey(AppCouponGetWay @enum)
        {
            switch (@enum)
            {
                case AppCouponGetWay.FreeReceive:
                    return "0001";
                case AppCouponGetWay.PointExchange:
                    return "0002";
                case AppCouponGetWay.PropertyPayment:
                    return "0003";
                default:
                    throw new ArgumentException("不存在该获取方式");
            }
        }
    }
}

