using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// 用户积分使用权限控制表
    /// </summary>
    [Serializable]
    public class Tb_Control_AppPoint
    {
        [JsonConverter(typeof(GuidConverter))]
        public Guid IID
        {
            get;
            set;
        }

        /// <summary>
        /// 物业公司编号
        /// </summary>
        [JsonIgnore]
        public Int16 CorpID
        {
            get;
            set;
        }

        /// <summary>
        /// 小区id
        /// </summary>
        public string CommunityID
        {
            get;
            set;
        }

        /// <summary>
        /// 是否允许抵用物业费
        /// </summary>
        public Boolean AllowDeductionPropertyFees
        {
            get;
            set;
        }

        /// <summary>
        /// 是否允许抵用其他物业代收费用
        /// </summary>
        public Boolean AllowDeductionOtherPropertyFees
        {
            get;
            set;
        }

        /// <summary>
        /// 是否允许抵用车位费
        /// </summary>
        public Boolean AllowDeductionParkingFees
        {
            get;
            set;
        }

        /// <summary>
        /// 是否允许抵用商品费用
        /// </summary>
        public Boolean AllowDeductionGoodsFees
        {
            get;
            set;
        }

        /// <summary>
        /// 是否允许每日签到送积分
        /// </summary>
        public Boolean AllowDailyCheckInAward
        {
            get;
            set;
        }

        /// <summary>
        /// 积分兑人民币比例，默认是100:1
        /// </summary>
        public Int16 PointExchangeRatio
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
        public static Tb_Control_AppPoint DefaultControl
        {
            get
            {
                return new Tb_Control_AppPoint()
                {
                    IID = Guid.Empty,
                    CorpID = 1000,
                    CommunityID = Guid.Empty.ToString(),
                    AllowDeductionPropertyFees = false,
                    AllowDeductionParkingFees = false,
                    AllowDeductionOtherPropertyFees = false,
                    AllowDeductionGoodsFees = false,
                    AllowDailyCheckInAward = false,
                    PointExchangeRatio = 100,
                    LastEditTime = DateTime.MinValue,
                    IsEnable = true
                };
            }
        }
    }
}