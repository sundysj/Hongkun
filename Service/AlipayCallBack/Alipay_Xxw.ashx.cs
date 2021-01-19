using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business;
using System.Web.SessionState;
using System.Collections.Specialized;
using System.Text;
using Com.Alipay;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Net.Cache;
using System.Security.Cryptography.X509Certificates;

namespace Service.AlipayCallBack
{
    /// <summary>
    /// Alipay_Xxw 的摘要说明
    /// </summary>
    public class Alipay_Xxw : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                //Business.Alipay_Xxw.Log("收到支付宝通知");
                writeTxt("收到支付宝通知");
                HttpRequest Request = context.Request;
                Dictionary<string, string> sPara = GetRequestPost(context);

                string respcode = Request.Form["trade_status"];
                string respmsg = Request.Form["trade_status"].ToString();
                string orderId = Request.Form["out_trade_no"].ToString();
                string CommunityId = Request.Form["body"].ToString();

                string notify_id = Request.Form["notify_id"];//获取notify_id

                string sign = Request.Form["sign"];//获取sign
                Config c = new Config();
                //初始化参数，并返回配置信息
                bool IsConfig = new Business.Alipay_Xxw().GenerateConfig(CommunityId, out c);
                //c.notify_url = MobileSoft.DBUtility.PubConstant.GetConnectionString("AliPayBackURL");
                string Result = "";
                if (IsConfig == false)
                {
                    throw new Exception("未配置证书文件");
                }

                //Business.Alipay_Xxw.Log("开始验证:" + CommunityId.ToString() + "," + orderId);
                writeTxt("开始验证:" + CommunityId.ToString() + "," + orderId);
                //返回报文中不包含UPOG,表示Server端正确接收交易请求,则需要验证Server端返回报文的签名
                Notify aliNotify = new Notify(c);

                if (aliNotify.GetResponseTxt(notify_id) == "true")
                {
                    //Business.Alipay_Xxw.Log("验证:" + CommunityId.ToString() + "," + orderId + " GetResponseTxt 正确");
                    writeTxt("验证:" + CommunityId.ToString() + "," + orderId + " GetResponseTxt 正确");
         
                    if (aliNotify.GetSignVeryfy(sPara, sign))
                    {
                        //Business.Alipay_Xxw.Log("验证:" + CommunityId.ToString() + "," + orderId + " GetSignVeryfy 正确");
                        writeTxt("验证:" + CommunityId.ToString() + "," + orderId + " GetSignVeryfy 正确");
                        //更新订单返回状态
                        Business.Alipay_Xxw.UpdateProperyOrder(CommunityId, orderId, respcode, respmsg);
                        //已收到请求，无需再发送通知
                        Result = "success";
                        writeTxt("订单交易状态:" + respcode);
                        if (respcode == "TRADE_SUCCESS")
                        {
                            string ReceResult = Business.Alipay_Xxw.ReceProperyOrder(CommunityId, orderId);
                            //Business.Alipay_Xxw.Log("支付宝下账:" + CommunityId.ToString() + "," + orderId + "," + ReceResult);
                            writeTxt("支付宝下账:" + CommunityId.ToString() + "," + orderId + "," + ReceResult);
                            //查询该订单中是否包含有偿报修
                            string IncidentId = Business.Alipay_Xxw.GetAlipayOrderfromFees(orderId);
                            if (IncidentId!="")//如果有，调用APP端，将报事编号传递过去，多个以逗号隔开
                            {
                                //Business.Alipay_Xxw.Log("开始调用业主接口传递报事编号:" +IncidentId);
                                writeTxt("开始调用业主接口传递报事编号:" + IncidentId);
                                string backstr= SendIncidentId(IncidentId);
                                //Business.Alipay_Xxw.Log("调用业主接口后返回:" + backstr);
                                writeTxt("调用业主接口后返回:" + backstr);
                            }
                        }
                    }
                    else
                    {
                        Result = "支付宝验签失败:" + orderId;
                    }
                }
                else
                {
                    Result = "支付宝" + orderId + "订单信息不匹配!";
                }

                //Business.Alipay_Xxw.Log("支付宝流程:" + CommunityId.ToString() + "," + orderId + "," + Result);
                writeTxt("支付宝流程:" + CommunityId.ToString() + "," + orderId + "," + Result);
                context.Response.ContentType = "text/plain";
                context.Response.Write(Result);
            }
            catch (Exception E)
            {
                //Business.Alipay_Xxw.Log(E.Message.ToString());
                writeTxt(E.Message.ToString());
                context.Response.ContentType = "text/plain";
                context.Response.Write(E.Message.ToString());
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public Dictionary<string, string> GetRequestPost(HttpContext context)
        {
            int i = 0;
            SortedDictionary<string, string> sArraytemp = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = context.Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArraytemp.Add(requestItem[i], context.Request.Form[requestItem[i]]);
            }
            Dictionary<string, string> sArray = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> temp in sArraytemp)
            {
                sArray.Add(temp.Key, temp.Value);
            }
            return sArray;
        }

        public string SendIncidentId(string IncidentId)
        {
            string strDoUrl = "http://apiadmins.newhope.cn/Pay/CallBack";
            string strDate = DateTime.Now.ToString("yyyyMMdd");
            string strToken = "20170406OnlinePayment";

            StringBuilder Attribute = new StringBuilder("");
            //apiadmints.newhope.cn/Pay/CallBack ? Attribute =< attributes >< payId > 123456789 </ payId >< result > SUCCESS </ result ></ attributes > &Mac = 2e92028651ceb3d5147ad3d076b31b44
            Attribute.Append("<attributes>");
            Attribute.Append("<payId>"+ IncidentId + "</payId>");
            Attribute.Append("<result>SUCCESS</result>");          
            Attribute.Append("</attributes>");
            string strAttribute = Attribute.ToString();

            string Mac = getMd5Hash(strAttribute+strDate+strToken);
            string strContent = "Attribute=" + strAttribute + "&Mac=" + Mac;
            string strNewUrl = strDoUrl + "?" + strContent;

            string repsTxt = HttpPost(strNewUrl, "");

            return repsTxt;
        }


        #region 发送http
        private string SendHttp(string Url, string Contents)
        {
            #region 发送
            HttpWebRequest request = null;

            byte[] postData;

            Uri uri = new Uri(Url);
            if (uri.Scheme == "https")
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);
            }
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);
            HttpWebRequest.DefaultCachePolicy = policy;

            request = (HttpWebRequest)WebRequest.Create(uri);
            request.AllowAutoRedirect = false;
            request.AllowWriteStreamBuffering = false;
            request.Method = "POST";

            postData = Encoding.GetEncoding("utf-8").GetBytes(Contents);

            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentType = "text/plain;charset = utf-8"; //request.ContentType = "text/plain";
            request.ContentLength = postData.Length;
            request.KeepAlive = false;

            Stream reqStream = request.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();
            #endregion

            #region 响应
            string respText = "";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();

            MemoryStream ms = new MemoryStream();
            byte[] buf = new byte[4096];
            int count;
            while ((count = resStream.Read(buf, 0, buf.Length)) > 0)
            {
                ms.Write(buf, 0, count);
            }
            resStream.Close();
            respText = Encoding.GetEncoding("utf-8").GetString(ms.ToArray());
            #endregion

            return respText;
        }
        #endregion


        #region CheckValidationResult
        private bool CheckValidationResult(
            Object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors
        )
        {
            if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNotAvailable) != SslPolicyErrors.RemoteCertificateNotAvailable)
                return true;
            throw new Exception("SSL验证失败");
        }
        #endregion
        public  string HttpPost(string url, string param)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            string result = "";//返回结果

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;

            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                //request.Timeout = timeout * 1000;

                //设置代理服务器
                //WebProxy proxy = new WebProxy();                          //定义一个网关对象
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);              //网关服务器端口:端口
                //request.Proxy = proxy;

                //设置POST的数据类型和长度
                //request.ContentType = "application/json";

                byte[] data = System.Text.Encoding.UTF8.GetBytes(param);
                request.ContentLength = data.Length;

                //往服务器写入数据
                reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                //获取服务端返回
                response = (HttpWebResponse)request.GetResponse();

                //获取服务端返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
                writeTxt("服务器返回："+result);
            }
            catch (Exception e)
            {
                writeTxt("服务器返回：" + e.ToString());
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }
        public static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object. 
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes        
            // and create a string.        
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data         
            // and format each one as a hexadecimal string.        
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        #region 写txt文本
        public void writeTxt(string msg)
        {
            string filename= DateTime.Now.ToString("yyyyMMdd");

            if (!Directory.Exists(HttpContext.Current.Server.MapPath("./AilpayBackLog")))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("./AilpayBackLog"));
            }
            string path= HttpContext.Current.Server.MapPath("./AilpayBackLog/")+filename+".txt";
            if (File.Exists(path))
            {
                StreamWriter sw = new StreamWriter(path, true);
                sw.WriteLine(DateTime.Now.ToString() + ":" + msg);
                sw.Close();
            }
            else
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                sw.WriteLine(DateTime.Now.ToString()+":"+msg);
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
            }

        }
        #endregion
    }
}