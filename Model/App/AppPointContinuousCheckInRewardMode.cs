using System;

namespace App.Model
{
	/// <summary>
	/// 持续签到奖励模式
	/// </summary>
	[Serializable]
	public enum AppPointContinuousCheckInRewardMode
	{
        /// <summary>
        /// 基础积分奖励，S=a1，奖励积分=基础积分
        /// </summary>
		Basal = 1,

        /// <summary>
        /// 基础积分每日递增，S=a1*n，奖励积分=基础积分×持续签到天数
        /// </summary>
        Increase = 2,

        /// <summary>
        /// 积分累加，暂不支持
        /// </summary>
        Accumulate = 3,

        /// <summary>
        /// 物业自定义公式，暂不支持
        /// </summary>
        PropertyDefined = 99
    }

    /// <summary>
    /// 持续签到奖励模式转换器
    /// </summary>
    public static class AppPointContinuousCheckInRewardModeConverter
    {
        /// <summary>
        /// 转换为<see cref="AppPointContinuousCheckInRewardMode"/>枚举
        /// </summary>
        public static AppPointContinuousCheckInRewardMode Convert(string key)
        {
            switch (key)
            {
                case "0001":
                    return AppPointContinuousCheckInRewardMode.Basal;
                case "0002":
                    return AppPointContinuousCheckInRewardMode.Increase;
                case "0003":
                    return AppPointContinuousCheckInRewardMode.Accumulate;
                case "0099":
                    return AppPointContinuousCheckInRewardMode.PropertyDefined;
                default:
                    throw new ArgumentException("不存在该奖励模式");
            }
        }



        /// <summary>
        /// 获取key
        /// </summary>
        public static string GetKey(AppPointContinuousCheckInRewardMode @enum)
        {
            switch (@enum)
            {
                case AppPointContinuousCheckInRewardMode.Basal:
                    return "0001";
                case AppPointContinuousCheckInRewardMode.Increase:
                    return "0002";
                case AppPointContinuousCheckInRewardMode.Accumulate:
                    return "0003";
                case AppPointContinuousCheckInRewardMode.PropertyDefined:
                    return "0099";
                default:
                    throw new ArgumentException("不存在该奖励模式");
            }
        }
    }
}

