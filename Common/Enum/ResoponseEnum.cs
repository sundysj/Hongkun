using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    /// <summary>
    /// 消息返回异常
    /// </summary>
    public enum ResoponseEnum
    {
        /// <summary>
        /// 该值不允许使用 仅作为占位符
        /// </summary>
        Nomal = 0,

        /// <summary>
        /// 成功
        /// </summary>
        Success = 200,

        /// <summary>
        /// 失败
        /// </summary>
        Failure = 400,

        /// <summary>
        /// 参数错误
        /// </summary>
        FailureParam = 451,

        /// <summary>
        /// 错误
        /// </summary>
        Error = 500,
    }
}
