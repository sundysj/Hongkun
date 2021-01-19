namespace Business.PMS10.物管App.费用.Models
{
    /// <summary>
    /// 支付通道模型
    /// </summary>
    public class PMSPaymentChannelModel
    {
        /// <summary>
        /// 支付通道名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 支付通道标识
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 支付通道图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 支付模式
        /// </summary>
        public string Mode { get; set; }
    }
}
