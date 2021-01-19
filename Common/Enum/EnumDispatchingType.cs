using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public enum EnumDispatchingType
    {
        /// <summary>
        /// 该值不允许使用 ，仅作为占位符存在
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 快递
        /// </summary>
        [Description("快递")]
        Express = 1,

        /// <summary>
        /// 配送
        /// </summary>
        [Description("配送")]
        Dispatching = 2,

        /// <summary>
        /// 自提
        /// </summary>
        [Description("自提")]
        TakeTheir = 4,

    }
}
