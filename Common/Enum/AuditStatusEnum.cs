using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public enum AuditStatusEnum
    {
        /// <summary>
        /// 该值不允许使用 仅作为占位符纯在
        /// </summary>
        Nomal = 0,

        Wait = 1,

        Success = 2,

        Failure = 3,

        /// <summary>
        /// 过期 同一商家多次申请修改 一最后一次为准 其他的全部删除
        /// </summary>
        overdue = 4,
    }
}
