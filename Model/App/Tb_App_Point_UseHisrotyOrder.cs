using Newtonsoft.Json;
using System;

namespace App.Model
{
    /// <summary>
    /// 积分使用记录关联订单表
    /// </summary>
    [Serializable]
    public class Tb_App_Point_UseHisrotyOrder
    {
        [JsonConverter(typeof(GuidConverter))]
        public Guid IID
        {
            get;
            set;
        }

        /// <summary>
        /// 积分使用记录
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public Guid UseHistoryID
        {
            get;
            set;
        }

        /// <summary>
        /// 积分使用记录
        /// </summary>
        public String OrderID
        {
            get;
            set;
        }

        /// <summary>
        /// 支付方式
        /// </summary>
        public String Payment
        {
            get;
            set;
        }

        /// <summary>
        /// 积分对象
        /// </summary>
        public String UsableObject
        {
            get;
            set;
        }
    }
}
