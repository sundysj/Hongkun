using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Business.responseObj
{
    [XmlRoot("billsInfo")]
    public class BillsInfo
    {
        /**
     * 期数
     * 3-3期
     * 6-6期
     * 12-12期
     * 24-24期
     */
       [XmlAttribute(AttributeName = "plan")]
        public String plan { set; get; }

        /**
         * 费率
         */
        [XmlAttribute(AttributeName = "rate")]
        public String rate { set; get; }

        /**
         * 总手续费
         */
        [XmlAttribute(AttributeName = "fee")]
        public String fee { set; get; }

        /**
         * 每期费率
         */
        [XmlAttribute(AttributeName = "planFee")]
        public String planFee { set; get; }

        /**
         * 首期付款
         */
        [XmlAttribute(AttributeName = "firstPay")]
        public String firstPay { set; get; }

        /**
         * 非首期付款
         */
        [XmlAttribute(AttributeName = "laterPay")]
        public String laterPay { set; get; }

        /**
         * 付款总金额
         */
        [XmlAttribute(AttributeName = "total")]
        public String total { set; get; }
    }
}