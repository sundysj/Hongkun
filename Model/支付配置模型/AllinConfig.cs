using Model.支付配置模型;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.支付配置模型
{
    public class AllinConfig : Config
    {
        /// <summary>
        /// 集团号，可空
        /// </summary>
        public string orgid { get; set; }

        /// <summary>
        /// 应用appid
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string custid { get; set; }
        /// <summary>
        /// MD5密钥
        /// </summary>
        public string appkey { get; set; }
        /// <summary>
        /// 门店号，可空
        /// </summary>
        public string subbranch { get; set; }
        /// <summary>
        /// 天问渠道配置，可空
        /// </summary>
        public string channel { get; set; }
    }
}