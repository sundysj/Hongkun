using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using System.IO;
using System.Net;

namespace Common
{
    /// <summary>
    /// httpClient操作远程url工具类
    /// </summary>
    public class HttpClientUtils
    {
        /// <summary>
        /// 通过http获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Dictionary<string, object> doPostDic(string url, string str)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            using (var client = new HttpClient())
            {
                var data = Encoding.ASCII.GetBytes(str);
                HttpResponseMessage messsage = null;
                using (Stream dataStream = new MemoryStream(data ?? new byte[0]))
                {
                    using (HttpContent content = new StreamContent(dataStream))
                    {
                        var task = client.PostAsync(url, content);

                        messsage = task.Result;

                    }
                }
                if (messsage != null && messsage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    using (messsage)
                    {
                        dic.Add("message", messsage.Content.ReadAsStringAsync().Result);
                        if (messsage.Content.Headers.ContentDisposition != null)//下载文件时，获取文件名称
                            dic.Add("fileName", messsage.Content.Headers.ContentDisposition.FileName);
                    }
                }
            }
            return dic;
        }

        #region 原调用Http获取数据和下载文件
        /*
         * 
        /// <summary>
        /// 发送str格式的post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string doPostStr(string url, string str)
        {
            string result = "";
            using (var client = new HttpClient())
            {
                var data = Encoding.ASCII.GetBytes(str);

                HttpResponseMessage messsage = null;
                using (Stream dataStream = new MemoryStream(data ?? new byte[0]))
                {
                    using (HttpContent content = new StreamContent(dataStream))
                    {
                        var task = client.PostAsync(url, content);
                        messsage = task.Result;
                    }
                }
                if (messsage != null && messsage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    using (messsage)
                    {
                        result = messsage.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            return result;
        }
         
         
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="str"></param>
        /// <param name="httpResponse"></param>
        public static Dictionary<string, object> doPostFile(string url, string str)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            using (var client = new HttpClient())
            {
                var data = Encoding.ASCII.GetBytes(str);
                HttpResponseMessage messsage = null;
                using (Stream dataStream = new MemoryStream(data ?? new byte[0]))
                {
                    using (HttpContent content = new StreamContent(dataStream))
                    {
                        var task = client.PostAsync(url, content);

                        messsage = task.Result;

                    }
                }
                if (messsage != null && messsage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    using (messsage)
                    {
                        dic.Add("message", messsage.Content.ReadAsStringAsync().Result);
                        if (messsage.Content.Headers.ContentDisposition != null)
                            dic.Add("fileName", messsage.Content.Headers.ContentDisposition.FileName);
                    }
                }
            }
            return dic;
        }
         */
        #endregion

        /// <summary>
        /// 获得请求报文转换成字符串
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string getRequestBody(HttpRequestBase request)
        {
            string result = "";
            using (Stream st = request.InputStream)
            {
                StreamReader sr = new StreamReader(st, Encoding.UTF8);
                result = sr.ReadToEnd();
            }
            return result;
        }
    }
}
