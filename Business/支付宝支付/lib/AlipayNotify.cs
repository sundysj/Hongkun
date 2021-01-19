using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;

namespace Com.Alipay
{
    /// <summary>
    /// 类名：Notify
    /// 功能：支付宝通知处理类
    /// 详细：处理支付宝各接口通知返回
    /// 版本：1.0
    /// 修改日期：2016-06-06
    /// '说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// //////////////////////注意/////////////////////////////
    /// 调试通知返回时，可查看或改写log日志的写入TXT里的数据，来检查通知返回是否正常 
    /// </summary>
    public class Notify
    {
        #region 字段
        private static string _partner = "";               //合作身份者ID
        private static string alipay_public_key = "";            //支付宝的公钥
        private static string _input_charset = "";         //编码格式
        private static string _sign_type = "";             //签名方式

        //支付宝消息验证地址
        private string Https_veryfy_url = "https://mapi.alipay.com/gateway.do?service=notify_verify&";
        #endregion


        /// <summary>
        /// 构造函数
        /// 从配置文件中初始化变量
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="notify_id">通知验证ID</param>
        public Notify(Config c)
        {
            //初始化基础配置信息
            _partner = c.partner.Trim();
            //alipay_public_key = getPublicKeyStr(Config.alipay_public_key.Trim());
            alipay_public_key = c.alipay_public_key.Trim();
            _input_charset = c.input_charset.Trim().ToLower();//固定
            _sign_type = c.sign_type.Trim().ToUpper();//固定
        }

        /// <summary>
        /// 从文件读取公钥转公钥字符串
        /// </summary>
        /// <param name="Path">公钥文件路径</param>
        public static string getPublicKeyStr(string Path)
        {
            StreamReader sr = new StreamReader(Path);
            string pubkey = sr.ReadToEnd();
            sr.Close();
            if (pubkey != null)
            {
                pubkey = pubkey.Replace("-----BEGIN PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("-----END PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("\r", "");
                pubkey = pubkey.Replace("\n", "");
            }
            return pubkey;
        }
        #region 写txt文本
        public void writeTxt(string msg)
        {

            string path = System.Web.HttpContext.Current.Server.MapPath("./") + "alipayerr.txt";
            if (File.Exists(path))
            {
                StreamWriter sw = new StreamWriter(path, true);
                sw.WriteLine(msg);
                sw.Close();
            }
            else
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                sw.Write(msg);
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
            }

        }
        #endregion
        /// <summary>
        /// 获取返回时的签名验证结果
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="sign">对比的签名结果</param>
        /// <returns>签名验证结果</returns>
        public bool GetSignVeryfy(Dictionary<string, string> inputPara, string sign)
        {
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            //过滤空值、sign与sign_type参数
            sPara = Core.FilterPara(inputPara);
            //获取待签名字符串
            string preSignStr = Core.CreateLinkNotifyString(sPara);
            //获得签名验证结果
            bool isSgin = false;
            if (sign != null && sign != "")
            {
                switch (_sign_type)
                {
                    case "RSA":
                        isSgin = RSA.verify(preSignStr, sign, alipay_public_key, _input_charset);
                        break;
                    default:
                        break;
                }
            }
            return isSgin;
        }

        /// <summary>
        /// 获取是否是支付宝服务器发来的请求的验证结果
        /// </summary>
        /// <param name="notify_id">通知验证ID</param>
        /// <returns>验证结果</returns>
        public string GetResponseTxt(string notify_id)
        {
            string veryfy_url = Https_veryfy_url + "partner=" + _partner + "&notify_id=" + notify_id;

            Business.Alipay.Log("获取是否是支付宝服务器发来的请求的验证结果:" + veryfy_url);

            //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
            string responseTxt = Get_Http(veryfy_url, 120000);

            return responseTxt;
        }

        /// <summary>
        /// 获取远程服务器ATN结果
        /// </summary>
        /// <param name="strUrl">指定URL路径地址</param>
        /// <param name="timeout">超时时间设置</param>
        /// <returns>服务器ATN结果</returns>
        private string Get_Http(string strUrl, int timeout)
        {
            string strResult;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(strUrl);
                myReq.Timeout = timeout;
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.Default);
                StringBuilder strBuilder = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    strBuilder.Append(sr.ReadLine());
                }

                strResult = strBuilder.ToString();
            }
            catch (Exception exp)
            {
                strResult = "错误：" + exp.Message;
            }

            return strResult;
        }
    }
}