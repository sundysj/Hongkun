using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Unified
{
    public class Tb_CMBCPayCertificate
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public string CommunityId { get; set; }

        /// <summary>
        /// 签约时的一级签约商户号
        /// </summary>
        public string platformId { get; set; }

        /// <summary>
        /// 商户签约时的一级签约编码 
        /// </summary>
        public string merchantNo { get; set; }
        /// <summary>
        /// 商户公钥内容
        /// </summary>
        public string cust_cer { get; set; }
        /// <summary>
        /// 商户私钥路径
        /// </summary>
        public string cust_sm2 { get; set; }
        /// <summary>
        /// 商户私钥密码
        /// </summary>
        public string cust_sm2_pwd { get; set; }
        /// <summary>
        /// 民生银行公钥内容
        /// </summary>
        public string cmbc_cer { get; set; }

    }
}
