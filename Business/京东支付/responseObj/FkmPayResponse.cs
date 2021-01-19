using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Business.responseObj
{
    [XmlRootAttribute("jdpay", Namespace = "", IsNullable = false)]
    public class FkmPayResponse : JdPayBaseResponse
    {
        public String tradeNum { set; get; }

        public String amount { set; get; }

        public String currency { set; get; }

        public String tradeTime { set; get; }

        public String status { set; get; }

        public String note { set; get; }
        
    }
}