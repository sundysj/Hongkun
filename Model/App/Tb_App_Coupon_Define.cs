using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// 优惠券主体信息表
    /// </summary>
    [Serializable]
    public class Tb_App_Coupon_Define
    {
        [JsonConverter(typeof(GuidConverter))]
        public Guid IID
        {
            get;
            set;
        }

        /// <summary>
        /// 优惠券细类
        /// </summary>
        public String Category
        {
            get;
            set;
        }

        /// <summary>
        /// 优惠券细类
        /// </summary>
        public String Type
        {
            get;
            set;
        }

        /// <summary>
        /// 优惠券使用条件，为-1则表示不限制使用条件
        /// </summary>
        public Decimal ConditionAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 优惠券抵用/折扣数
        /// </summary>
        public Decimal DiscountsPoints
        {
            get;
            set;
        }

        /// <summary>
        /// 添加时间
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

        /// <summary>
        /// 是否已删除
        /// </summary>
        public Boolean IsDelete
        {
            get;
            set;
        }
    }
}