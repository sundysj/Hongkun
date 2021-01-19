using System;

namespace Model.Model.Buss
{
    public class Tb_Charge_CebpayOrder
    {
        public string Id { get; set; }

        public int BussId { get; set; }
        public string ReceiptId { get; set; }
        public int PayType { get; set; }
        public string OrderSN { get; set; }
        public decimal Amount { get; set; }
        public decimal SAmount { get; set; }
        public string PayTime { get; set; }

        public string PayResult { get; set; }

        /// <summary>
        /// 0=未收到支付通知,1=收到通知,但是为无效交易,2=已收到通知,交易成功,但交易信息异常,3=已支付已下账,4=已支付但下账失败
        /// </summary>
        public int IsSucc { get; set; }
        public string Memo { get; set; }
        public string CreateTime { get; set; }
    }
}
