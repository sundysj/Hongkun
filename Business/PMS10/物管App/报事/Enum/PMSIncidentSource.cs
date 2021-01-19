using System;
using System.ComponentModel;

namespace Business.PMS10.物管App.报事.Enum
{
    /// <summary>
    /// 报事来源
    /// </summary>
    [Flags]
    public enum PMSIncidentSource
    {
        /// <summary>
        /// 客户报事
        /// </summary>
        [Description("客户报事")]
        FromCustomer = 1 << 0,

        /// <summary>
        /// 自查报事
        /// </summary>
        [Description("自查报事")]
        FromEmployee = 1 << 1
    }
}
