using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    /// <summary>
    /// 社区服务类型
    /// </summary>
    public enum EnumCommunityServiceType
    {
        /// <summary>
        /// 该值不允许使用 ，仅作为占位符存在
        /// </summary>
        Normal = 0,

        [Description("金融")]
        Financial = 1,

        [Description("健康")]
        Health = 2,

        [Description("养老")]
        Pension = 3,

        [Description("教育")]
        Education = 4,

        [Description("其他")]
        Other = 5,
    }
}
