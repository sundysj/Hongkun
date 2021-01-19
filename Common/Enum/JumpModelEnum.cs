using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public enum JumpModelEnum
    {
        /// <summary>
        /// 该枚举不允许使用 仅作为占位符
        /// </summary>
        Nomal = 0,

        /// <summary>
        /// 
        /// </summary>
        [Description("内部跳转")]
        Interior = 1,

        /// <summary>
        /// 
        /// </summary>
        [Description("网页地址")]
        External = 2,
    }
}
