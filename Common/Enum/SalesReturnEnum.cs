using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public enum SalesReturnEnum
    {
        /// <summary>
        /// 该值不允许使用 仅作为占位符纯在
        /// </summary>
        Nomal = 0,

        /// <summary>
        /// 可申请
        /// </summary>
        CanApplyFor = 1,

        /// <summary>
        /// 可审核
        /// </summary>
        CanAudit = 2,

        /// <summary>
        /// 可查看
        /// </summary>
        CanLook = 3,


        /// <summary>
        /// 可确认
        /// </summary>
        CanAffirm = 4,
    }
}
