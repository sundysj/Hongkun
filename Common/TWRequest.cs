
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
  public  class TWRequest
    {

        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }

        public static string HttpPost(string url,string param)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            string result = "";//返回结果

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;
            new Logger().WriteLog("接口推送日志", "请求URL:" + url);
            new Logger().WriteLog("接口推送日志", "请求参数:" + param);
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
                request.ContentType = "text/html";//ContentType = "application/x-www-form-urlencoded"
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
                if (result.Contains("result"))
                {
                    result = result.Replace("result", "Result");
                }
                sr.Close();
             
            }
            catch (Exception e)
            {
                new Logger().WriteLog("接口推送日志", "请求报错:" + e.ToString());
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


        public static string HttpPost(string url, string param, string ContentType)
        {

            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            string result = "";//返回结果

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;
            new Logger().WriteLog("接口推送日志", "请求URL:" + url);
            new Logger().WriteLog("接口推送日志", "请求参数:" + param);
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
                request.ContentType = ContentType;
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

                if (result.Contains("result"))
                {
                    result = result.Replace("result", "Result");
                }
                sr.Close();
            }
            catch (Exception e)
            {
             
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


        public static string HttpPost(string url,string xml, bool isUseCert, int timeout)
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

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.Timeout = timeout * 1000;

                //设置代理服务器
                //WebProxy proxy = new WebProxy();                          //定义一个网关对象
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);              //网关服务器端口:端口
                //request.Proxy = proxy;

                //设置POST的数据类型和长度
                request.ContentType = "text/xml";
                byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
                request.ContentLength = data.Length;

                //是否使用证书
                if (isUseCert)
                {
                    //string path = HttpContext.Current.Request.PhysicalApplicationPath;
                    //X509Certificate2 cert = new X509Certificate2(path + new WxPayConfig().SSLCERT_PATH, new WxPayConfig().SSLCERT_PASSWORD);
                    //request.ClientCertificates.Add(cert);
                    //Log.Debug("WxPayApi", "PostXml used cert");
                }

                //往服务器写入数据
                reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                //获取服务端返回
                response = (HttpWebResponse)request.GetResponse();

                //获取服务端返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                if (result.Contains("result"))
                {
                    result = result.Replace("result", "Result");
                }
                sr.Close();
            }
            catch (System.Threading.ThreadAbortException e)
            {
                //Log.Error("HttpService", "Thread - caught ThreadAbortException - resetting.");
                //Log.Error("Exception message: {0}", e.Message);
                //System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                //Log.Error("HttpService", e.ToString());
                //if (e.Status == WebExceptionStatus.ProtocolError)
                //{
                //    Log.Error("HttpService", "StatusCode : " + ((HttpWebResponse)e.Response).StatusCode);
                //    Log.Error("HttpService", "StatusDescription : " + ((HttpWebResponse)e.Response).StatusDescription);
                //}
                //throw new WxPayException(e.ToString());
            }
            catch (Exception e)
            {
                //Log.Error("HttpService", e.ToString());
                //throw new WxPayException(e.ToString());
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


        public static string Post(string posturl, string param)
        {
            string url = posturl;
            HttpHelperT http = new HttpHelperT();
            HttpItem item = new HttpItem()
            {
                //Header = header,
                URL = url,//URL     必需项  
                Method = "POST",//URL     可选项 默认为Get  
                ContentType = "application/x-www-form-urlencoded",
                Timeout = 5000,//连接超时时间     可选项默认为100000  
                ReadWriteTimeout = 5000,//写入Post数据超时时间     可选项默认为30000  
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写  
                Postdata = param,//Post数据     可选项GET时不需要写  
                ResultType = ResultType.String,//返回数据类型，是Byte还是String  
            };
            HttpResult result = http.GetHtml(item);
            return result.Html;
        }

        /// <summary>
        /// 处理http GET请求，返回数据
        /// </summary>
        /// <param name="url">请求的url地址</param>
        /// <returns>http GET成功后返回的数据，失败抛WebException异常</returns>
        public static string HttpGet(string url)
        {
            System.GC.Collect();
            string result = "";

            HttpWebRequest request = null;
            HttpWebResponse response = null;

            //请求url以获取数据
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

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 8000;
                request.Method = "GET";

                //设置代理
                //WebProxy proxy = new WebProxy();
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);
                //request.Proxy = proxy;

                //获取服务器返回
                response = (HttpWebResponse)request.GetResponse();

                //获取HTTP返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();

                if (result.Contains("result"))
                {
                    result = result.Replace("result", "Result");
                }

                sr.Close();
            }
            catch (System.Threading.ThreadAbortException e)
            {
                //Log.Error("HttpService", "Thread - caught ThreadAbortException - resetting.");
                //Log.Error("Exception message: {0}", e.Message);
                //System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                //Log.Error("HttpService", e.ToString());
                //if (e.Status == WebExceptionStatus.ProtocolError)
                //{
                //    Log.Error("HttpService", "StatusCode : " + ((HttpWebResponse)e.Response).StatusCode);
                //    Log.Error("HttpService", "StatusDescription : " + ((HttpWebResponse)e.Response).StatusDescription);
                //}
                //throw new WxPayException(e.ToString());
            }
            catch (Exception e)
            {
                //Log.Error("HttpService", e.ToString());
                //throw new WxPayException(e.ToString());
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
        public static void writeTxt(string msg)
        {

            string path = System.Web.HttpContext.Current.Server.MapPath("./") + "TwRequestErr.txt";
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
    }
}
