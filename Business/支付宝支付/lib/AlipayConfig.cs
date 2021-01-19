using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;
using Common;
using MobileSoft.Common;

namespace Com.Alipay
{
    /// <summary>
    /// 类名：Config
    /// 功能：基础配置类
    /// 详细：设置帐户有关信息及返回路径
    /// 版本：1.0
    /// 修改日期：2016-06-06
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// </summary>
    public class Config
    {

        //↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

        // 合作身份者ID，签约账号，以2088开头由16位纯数字组成的字符串，查看地址：https://openhome.alipay.com/platform/keyManage.htm?keyType=partner
        public  string partner = "";

        // 收款支付宝账号，以2088开头由16位纯数字组成的字符串，一般情况下收款账号就是签约账号
        public  string seller_id = "";
		
		//商户的私钥,原始格式，RSA公私钥生成：https://doc.open.alipay.com/doc2/detail.htm?spm=a219a.7629140.0.0.nBDxfy&treeId=58&articleId=103242&docType=1
        public  string private_key = "";

        //支付宝的公钥，查看地址：https://b.alipay.com/order/pidAndKey.htm 
        public  string alipay_public_key = "";
        
        // 签名方式
        public  string sign_type = "RSA";

        // 调试用，创建TXT日志文件夹路径，见AlipayCore.cs类中的LogResult(string sWord)打印方法。
        public  string log_path = HttpRuntime.AppDomainAppPath.ToString() + "log/";

        // 字符编码格式 目前支持 gbk 或 utf-8
        public  string input_charset = "utf-8";

        // 支付类型 ，无需修改
        public  string payment_type = "1";

        // 调用的接口名，无需修改
        public  string service = "mobile.securitypay.pay";

        private string _notify_url;
        public string notify_url
        {
            get
            {
                if (string.IsNullOrEmpty(_notify_url))
                {
                    _notify_url = Global_Fun.AppWebSettings("AliPay_Notify_Url").ToString();
                }
                return _notify_url;
            }
            set { _notify_url = value; }
        }

        //public  string notify_url = "http://125.64.16.10:9999/TWInterface/Service/AlipayCallBack/AliPay.ashx";

        public  string extern_token = "";
        
        //↑↑↑↑↑↑↑↑↑↑请在这里配置您的基本信息↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

    }
}