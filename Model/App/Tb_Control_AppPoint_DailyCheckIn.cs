using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// 用户每日签到积分奖励控制表
    /// </summary>
    [Serializable]
    public class Tb_Control_AppPoint_DailyCheckIn
    {
        /// <summary>
        /// 物业公司编号
        /// </summary>
        [JsonIgnore]
        public Int16 CorpID
        {
            get;
            set;
        }

        public string CommunityID
        {
            get;
            set;
        }

        /// <summary>
        /// 每日签到基础奖励积分数
        /// </summary>
        public Int16 BaseRewardPoints
        {
            get;
            set;
        }

        /// <summary>
        /// 持续签到奖励计算方式
        /// </summary>
        public String ContinuousCheckInRewardMode
        {
            get;
            set;
        }

        /// <summary>
        /// 持续签到最大奖励封顶天数，为-1代表不限制
        /// </summary>
        public Int16 ContinuousCheckInLimitDays
        {
            get;
            set;
        }

        /// <summary>
        /// 持续签到额外奖励积分数
        /// </summary>
        public Int16 ContinuousCheckInAdditionalRewardPoints
        {
            get;
            set;
        }

        /// <summary>
        /// 持续签到额外奖励规定天数，必须大于等于最大奖励封顶天数
        /// </summary>
        public Int16 ContinuousCheckInAdditionalRewardLimitDays
        {
            get;
            set;
        }

        /// <summary>
        /// 最后一次编辑时间
        /// </summary>
        public DateTime LastEditTime
        {
            get;
            set;
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        public Boolean IsEnable
        {
            get;
            set;
        }

        /// <summary>
        /// 默认配置，参见数据库表配置
        /// </summary>
        public static Tb_Control_AppPoint_DailyCheckIn DefaultControl
        {
            get
            {
                return new Tb_Control_AppPoint_DailyCheckIn()
                {
                    CorpID = 1000,
                    CommunityID = "",
                    BaseRewardPoints = 1,
                    ContinuousCheckInRewardMode = "0001",
                    ContinuousCheckInLimitDays = 7,
                    ContinuousCheckInAdditionalRewardPoints = 0,
                    ContinuousCheckInAdditionalRewardLimitDays = 0,
                    LastEditTime = DateTime.MinValue,
                    IsEnable = true
                };
            }
        }
    }
}