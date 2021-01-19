using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public enum JumpTypeEnum
    {
        /// <summary>
        /// 该枚举不允许使用 仅作为占位符
        /// </summary>
        Nomal = 0,

        /// <summary>
        /// 
        /// </summary>
        [Description("功能模块")]
        FunctionModule = 1,

        /// <summary>
        /// 
        /// </summary>
        [Description("商品频道")]
        GoodsChannel = 2,

        /// <summary>
        /// 
        /// </summary>
        [Description("服务类别")]
        ClassOfService = 3,

        /// <summary>
        /// 
        /// </summary>
        [Description("商品名称")]
        ProductName = 4,

        /// <summary>
        /// 
        /// </summary>
        [Description("活动名称")]
        ActivityName = 5,
    }
}
