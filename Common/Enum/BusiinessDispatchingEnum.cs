using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    /// <summary>
    /// 该枚举值存在多项并用使用位运算计算并用
    /// </summary>
    public enum BusiinessDispatchingEnum
    {
        /// <summary>
        /// 该枚举值 不允许使用 仅作为占位符使用 
        /// </summary>
        Nomal = 0,

        /// <summary>
        /// 快递
        /// </summary>
        Express = 1,

        /// <summary>
        /// 配送
        /// </summary>
        Dispatching = 2,

        /// <summary>
        /// 自提
        /// </summary>
        TakeTheir = 4,
    }
}
