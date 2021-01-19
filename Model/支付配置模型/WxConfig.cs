using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.支付配置模型
{
    public class WxConfig : Config
    {
        /// <summary>
        /// 对应的应用appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }
        
        /// <summary>
        /// 商户apikey
        /// </summary>
        public string appkey { get; set; }

        /// <summary>
        /// 商户p12证书文件完整路径（除php外其他使用）
        /// </summary>
        public string cert_path { get; set; }
    }
}
