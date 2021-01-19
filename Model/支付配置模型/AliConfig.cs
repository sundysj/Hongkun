using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Model.支付配置模型
{
    /// <summary>
    /// 支付宝支付模型
    /// </summary>
    public class AliConfig : Config
    {
        public string appid { get; set; }
        public string app_private_key { get; set; }
        public string alipay_public_key { get; set; }
        public string seller_id { get; set; }
    }
}
