using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    /// <summary>
    /// 结果消息
    /// </summary>
    [Serializable]
    public class ExecResult
    {
        public ExecResult(int code, string msg,  ErrType errType = ErrType.Normal, object data = null)
        {
            this.code = code;
            this.msg = msg;
            this.data = data;
            this.errType = errType;
        }
        /// <summary>
        /// 执行结果
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 错误类型
        /// </summary>
        public ErrType errType { get; set; } = ErrType.Verification;
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object data { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public enum ErrType
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 验证错误
        /// </summary>
        Verification = 1,
        /// <summary>
        /// 并发错误
        /// </summary>
        Concurrent = 2,
        /// <summary>
        /// 系统错误
        /// </summary>
        System = 3
    }
}
