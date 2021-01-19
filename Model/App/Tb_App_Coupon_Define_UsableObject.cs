using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// 优惠券可用对象表
    /// </summary>
    [Serializable]
    public class Tb_App_Coupon_Define_UsableObject
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
        /// 物业公司编号
        /// </summary>
        public Int16 CorpID
        {
            get;
            set;
        }

        /// <summary>
        /// 优惠券可用小区id
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public Guid CommunityID
        {
            get;
            set;
        }

        /// <summary>
        /// 优惠券可用商家id
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public Guid StoreID
        {
            get;
            set;
        }

    }
}