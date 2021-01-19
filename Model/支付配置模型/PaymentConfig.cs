using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.支付配置模型
{
    public class PaymentConfig
    {
        /// <summary>
        /// 微信原生支付、支付宝原生支付、通联支付宝支付、通联微信支付
        /// WChatPay、AliPay、Allin_AliPay、Allin_WChatPay
        /// </summary>
        public string type { get; set; }
        public JObject config { get; set; }
    }
}
