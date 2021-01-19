using System;
using System.ComponentModel;

namespace App.Model
{
	/// <summary>
	/// 积分赠送方式
	/// </summary>
	[Serializable]
	public enum AppPointPresentedWay
	{
        /// <summary>
        /// 每日签到
        /// </summary>
        [Description("0001")]
        DailyCheckIn = 1,

        /// <summary>
        /// 持续签到奖励
        /// </summary>
        [Description("0002")]
        ContinuousCheckInReward = 2,

        /// <summary>
        /// 物业欠费缴费赠送
        /// </summary>
        [Description("0003")]
        PropertyArrearsPayment = 3,

        /// <summary>
        /// 物业预存缴费赠送
        /// </summary>
        [Description("0004")]
        PropertyPrestorePayment = 4,

        /// <summary>
        /// 购物赠送
        /// </summary>
        [Description("0005")]
        StoreTrade = 5,

        /// <summary>
        /// 物业主动发放
        /// </summary>
        [Description("0099")]
        PropertyPresented = 99
    }


    /// <summary>
    /// 积分赠送方式转换器
    /// </summary>
    public static class AppPointPresentedWayConverter
    {
        /// <summary>
        /// 转换为<see cref="AppPointPresentedWay">枚举
        /// </summary>
        public static AppPointPresentedWay Convert(string key)
        {
            switch (key)
            {
                case "0001":
                    return AppPointPresentedWay.DailyCheckIn;
                case "0002":
                    return AppPointPresentedWay.ContinuousCheckInReward;
                case "0003":
                    return AppPointPresentedWay.PropertyArrearsPayment;
                case "0004":
                    return AppPointPresentedWay.PropertyPrestorePayment;
                case "0005":
                    return AppPointPresentedWay.StoreTrade;
                case "0099":
                    return AppPointPresentedWay.PropertyPresented;
                default:
                    throw new ArgumentException("不存在该赠送方式");
            }
        }

        /// <summary>
        /// 获取key
        /// </summary>
        public static string GetKey(AppPointPresentedWay @enum)
        {
            switch (@enum)
            {
                case AppPointPresentedWay.DailyCheckIn:
                    return "0001";
                case AppPointPresentedWay.ContinuousCheckInReward:
                    return "0002";
                case AppPointPresentedWay.PropertyArrearsPayment:
                    return "0003";
                case AppPointPresentedWay.PropertyPrestorePayment:
                    return "0004";
                case AppPointPresentedWay.StoreTrade:
                    return "0005";
                case AppPointPresentedWay.PropertyPresented:
                    return "0099";
                default:
                    throw new ArgumentException("不存在该赠送方式");
            }
        }

        public static string GetValue(AppPointPresentedWay @enum)
        {
            switch (@enum)
            {
                case AppPointPresentedWay.DailyCheckIn:
                    return "每日签到";
                case AppPointPresentedWay.ContinuousCheckInReward:
                    return "持续签到额外奖励";
                case AppPointPresentedWay.PropertyArrearsPayment:
                    return "物业欠费缴费赠送";
                case AppPointPresentedWay.PropertyPrestorePayment:
                    return "物业预存缴费赠送";
                case AppPointPresentedWay.StoreTrade:
                    return "购物赠送";
                case AppPointPresentedWay.PropertyPresented:
                    return "物业主动发放";
                default:
                    throw new ArgumentException("不存在该赠送方式");
            }
        }
    }
}
