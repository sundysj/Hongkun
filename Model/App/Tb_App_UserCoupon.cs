using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// 用户优惠券表
    /// </summary>
    [Serializable]
    public class Tb_App_UserCoupon
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
        /// 优惠券主体id
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public Guid DefineID
        {
            get;
            set;
        }

        /// <summary>
        /// 领取时间
        /// </summary>
        public DateTime ReceiveTime
        {
            get;
            set;
        }

        /// <summary>
        /// 是否已使用
        /// </summary>
        public Boolean IsUsed
        {
            get;
            set;
        }

        /// <summary>
        /// 是否已锁定
        /// </summary>
        public Boolean IsLocked
        {
            get;
            set;
        }

        /// <summary>
        /// 是否已过期
        /// </summary>
        public Boolean IsOverdue
        {
            get;
            set;
        }

        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime UsageTime
        {
            get;
            set;
        }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime OverdueTime
        {
            get;
            set;
        }

    }
}