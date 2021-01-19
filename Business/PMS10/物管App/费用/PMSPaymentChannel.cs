using Business.PMS10.物管App.费用.Models;

namespace Business.PMS10.物管App.费用
{
    public static class PMSPaymentChannel
    {
        /// <summary>
        /// 支付宝
        /// </summary>
        public static PMSPaymentChannelModel Alipay => new PMSPaymentChannelModel
        {
            Name = "支付宝",
            Value = "alipay",
            Icon = ""
        };

        /// <summary>
        /// 支付宝2.0
        /// </summary>
        public static PMSPaymentChannelModel Alipay2 => new PMSPaymentChannelModel
        {
            Name = "支付宝",
            Value = "alipay_2",
            Icon = ""
        };

        /// <summary>
        /// 微信
        /// </summary>
        public static PMSPaymentChannelModel Wechatpay => new PMSPaymentChannelModel
        {
            Name = "微信",
            Value = "wechatpay",
            Icon = ""
        };

        /// <summary>
        /// 银联云闪付
        /// </summary>
        public static PMSPaymentChannelModel Unionpay => new PMSPaymentChannelModel
        {
            Name = "银联",
            Value = "unionpay",
            Icon = ""
        };

        /// <summary>
        /// 通联支付宝
        /// </summary>
        public static PMSPaymentChannelModel AllinpayAlipay => new PMSPaymentChannelModel
        {
            Name = "支付宝",
            Value = "allinpay_alipay",
            Icon = ""
        };

        /// <summary>
        /// 通联微信
        /// </summary>
        public static PMSPaymentChannelModel AllinpayWechatpay => new PMSPaymentChannelModel
        {
            Name = "微信",
            Value = "allinpay_wechatpay",
            Icon = ""
        };

        /// <summary>
        /// 通联银联
        /// </summary>
        public static PMSPaymentChannelModel AllinpayUnionpay => new PMSPaymentChannelModel
        {
            Name = "银联",
            Value = "allinpay_unionpay",
            Icon = ""
        };

        /// <summary>
        /// 全民付
        /// </summary>
        public static PMSPaymentChannelModel UnionpayQmf => new PMSPaymentChannelModel
        {
            Name = "银联",
            Value = "unionpay_qmf",
            Icon = ""
        };

        /// <summary>
        /// 全民付支付宝
        /// </summary>
        public static PMSPaymentChannelModel UnionpayQmfAlipay => new PMSPaymentChannelModel
        {
            Name = "支付宝",
            Value = "unionpay_qmf_alipay",
            Icon = ""
        };

        /// <summary>
        /// 全民付微信
        /// </summary>
        public static PMSPaymentChannelModel UnionpayQmfWechatpay => new PMSPaymentChannelModel
        {
            Name = "微信",
            Value = "unionpay_qmf_wechatpay",
            Icon = ""
        };
    }
}
