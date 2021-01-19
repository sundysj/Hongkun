using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.PMS10.物管App.费用.Models
{
    public static class PMSPaymentChannelString
    {
        // 支付宝
        public const string Alipay = "alipay";

        // 微信
        public const string Wechatpay = "wechatpay";

        // 银联
        public const string Unionpay = "unionpay";


        // 全民付
        public const string Unionpay_Qmf = "unionpay_qmf";
        public const string Unionpay_Qmf_Alipay = "unionpay_qmf_alipay";
        public const string Unionpay_Qmf_WechatPay = "unionpay_qmf_wechatpay";

        // 通联
        public const string Allinpay_Union = "allinpay_union";
        public const string Allinpay_Alipay = "allinpay_alipay";
        public const string Allinpay_WechatPay = "allinpay_wechatpay";
    }

}
