using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// 优惠券主体信息详情表
    /// </summary>
    [Serializable]
    public class Tb_App_Coupon_Define_Detail
    {
        [JsonConverter(typeof(GuidConverter))]
        public Guid IID
        {
            get;
            set;
        }

        /// <summary>
        /// 优惠券主体信息id
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public Guid DefineID
        {
            get;
            set;
        }

        /// <summary>
        /// 本次发放数量，为-1则表示不限制
        /// </summary>
        public Int32 Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// 优惠券获取方式
        /// </summary>
        public String GetWay
        {
            get;
            set;
        }

        /// <summary>
        /// 优惠券兑换领取开始时间
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 优惠券兑换领取结束时间
        /// </summary>
        public DateTime EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// 优惠券自领取后有效时间，为-1则表示不过期
        /// </summary>
        public Int16 OverdueDays
        {
            get;
            set;
        }

        /// <summary>
        /// 兑换优惠券所需积分，当且仅当获取方式为积分兑换时有效
        /// </summary>
        public Int32 RequiredPoints
        {
            get;
            set;
        }

        /// <summary>
        /// 每人最多领取多少次，为-1代表不限制领取次数
        /// </summary>
        public Int16 GetLimit
        {
            get;
            set;
        }

    }
}