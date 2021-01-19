using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public enum VisitTypeEnum
    {

        /// <summary>
        /// 该值不允许使用 仅作为占位符纯在
        /// </summary>
        Nomal = 0,

        /// <summary>
        /// 亲属
        /// </summary>
        Kinsfolk = 1,

        /// <summary>
        /// 朋友
        /// </summary>
        Friend = 2,

        /// <summary>
        /// 其他
        /// </summary>
        Other = 3,
    }
}
