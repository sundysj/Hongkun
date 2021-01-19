using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Business.responseObj
{
    [XmlRootAttribute("jdpay", Namespace = "", IsNullable = false)]
    public class BtStrategyResponse : JdPayBaseResponse
    {
        /**
        * 交易流水  数字或字母
        */
        public String tradeNum { set; get; }
        /**
        * 白条分期列表  
        */
        [XmlElement("billsInfoList")]
        public List<BillsInfo> billsInfoList { set; get; }
    }
}