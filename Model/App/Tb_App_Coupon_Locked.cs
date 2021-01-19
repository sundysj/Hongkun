using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// 用户优惠券锁定表
    /// </summary>
    [Serializable]
    public class Tb_App_Coupon_Locked
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
        /// 优惠券id
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public Guid CouponID
        {
            get;
            set;
        }

        /// <summary>
        /// 锁定时间
        /// </summary>
        public DateTime LockedTime
        {
            get;
            set;
        }

    }
}