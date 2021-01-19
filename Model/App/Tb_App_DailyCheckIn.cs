using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// 每日签到记录表
    /// </summary>
    [Serializable]
    public class Tb_App_DailyCheckIn
    {
        [JsonConverter(typeof(GuidConverter))]
        public Guid IID
        {
            get;
            set;
        }

        /// <summary>
        /// 用户id
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public Guid UserID
        {
            get;
            set;
        }

        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime CheckInTime
        {
            get;
            set;
        }

        /// <summary>
        /// 奖励积分数量
        /// </summary>
        public Int16 RewardPoints
        {
            get;
            set;
        }

        /// <summary>
        /// 额外奖励积分
        /// </summary>
        public Int16 AdditionalRewardPoints
        {
            get;
            set;
        }

        /// <summary>
        /// 是否额外奖励积分
        /// </summary>
        public Boolean IsAdditionalReward
        {
            get;
            set;
        }
    }
}