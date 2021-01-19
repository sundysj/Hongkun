using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.支付配置模型.华宇通联
{
    public class Tb_Payment_Order
    {
        public int Id { get; set; }
        public int PayType { get; set; }
        public string OrderSN { get; set; }
        public string NoticeId { get; set; }
        public decimal Amt { get; set; }
        public decimal SAmt { get; set; }
        public string PayResult { get; set; }
        public string PayTime { get; set; }

        /// <summary>
        /// 0=未收到支付通知,1=收到通知,但是为无效交易,2=已收到通知,交易成功,但交易信息异常,3=已支付已下账,4=已支付但下账失败
        /// </summary>
        public int IsSucc { get; set; }
        public string Memo { get; set; }
        public string CreateTime { get; set; }
    }
}
