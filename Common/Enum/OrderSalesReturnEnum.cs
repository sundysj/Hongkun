using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public enum OrderSalesReturnEnum
    {
        /// <summary>
        /// 该枚举不允许使用，仅作为占位符使用
        /// </summary>
        Nomal = 0,

        /// <summary>
        /// 申请
        /// </summary>
        ApplyFor = 1,

        /// <summary>
        /// 同意
        /// </summary>
        Agree = 2,

        /// <summary>
        /// 拒绝
        /// </summary>
        Refuse = 3,

        /// <summary>
        /// 退货
        /// </summary>
        SalesReturn = 4,

        /// <summary>
        /// 收货
        /// </summary>
        Receiving = 5,

        /// <summary>
        /// 退款
        /// </summary>
        Refund=6,
    }
}
