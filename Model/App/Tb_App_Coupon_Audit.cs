using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// 优惠券审核表
    /// </summary>
    [Serializable]
    public class Tb_App_Coupon_Audit
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
        /// 是否审核通过
        /// </summary>
        public Boolean IsAllow
        {
            get;
            set;
        }

        /// <summary>
        /// 备注信息
        /// </summary>
        public String Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime OperateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 操作人员
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public Guid OperateUser
        {
            get;
            set;
        }

    }
}