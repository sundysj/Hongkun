using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace Common
{
    public static class HttpHelper
    {

        //其中参数：postData 为提交参数
        // 形如：pastData=“username=aaa&userpwd=bbb”； 
        // posturl 为提交事件指定的路径
        public static string GetPage(string posturl, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = System.Text.Encoding.GetEncoding("utf-8");
            byte[] data = encoding.GetBytes(postData);
            try
            {
                // 设置参数
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {

                return string.Empty;
            }
        }


        /// <summary>
        /// 将JSON 字符转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T ToJsonObject<T>(this string str)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(str)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

        /// <summary>
        /// json字符串转化为Dictionary
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static Dictionary<string, object> JsonToDictionary(string jsonData)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.MaxJsonLength = Int32.MaxValue;
            try
            {
                return jss.Deserialize<Dictionary<string, object>>(jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 将json字符串反序列化为字典类型
        /// </summary>
        /// <typeparam name="TKey">字典key</typeparam>
        /// <typeparam name="TValue">字典value</typeparam>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>字典数据</returns>
        public static Dictionary<TKey, TValue> DeserializeStringToDictionary<TKey, TValue>(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return new Dictionary<TKey, TValue>();

            Dictionary<TKey, TValue> jsonDict = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(jsonStr);

            return jsonDict;

        }

        /// <summary> 
        /// Json格式转换成键值对，键值对中的Key需要区分大小写 
        /// </summary> 
        /// <param name="JsonData">需要转换的Json文本数据</param> 
        /// <returns></returns> 
        public static Dictionary<string, object> ToDictionary(string JsonData)
        {
            object Data = null;
            Dictionary<string, object> Dic = new Dictionary<string, object>();
            if (JsonData.StartsWith("["))
            {
                //如果目标直接就为数组类型，则将会直接输出一个Key为List的List<Dictionary<string, object>>集合 
                //使用示例List<Dictionary<string, object>> ListDic = (List<Dictionary<string, object>>)Dic["List"]; 
                List<Dictionary<string, object>> List = new List<Dictionary<string, object>>();
                MatchCollection ListMatch = Regex.Matches(JsonData, @"{[\s\S]+?}");//使用正则表达式匹配出JSON数组 
                foreach (Match ListItem in ListMatch)
                {
                    List.Add(ToDictionary(ListItem.ToString()));//递归调用 
                }
                Data = List;
                Dic.Add("List", Data);
            }
            else
            {
                MatchCollection Match = Regex.Matches(JsonData, @"""(.+?)"": {0,1}(\[[\s\S]+?\]|null|"".+?""|-{0,1}\d*)");//使用正则表达式匹配出JSON数据中的键与值 
                foreach (Match item in Match)
                {
                    try
                    {
                        if (item.Groups[2].ToString().StartsWith("["))
                        {
                            //如果目标是数组，将会输出一个Key为当前Json的List<Dictionary<string, object>>集合 
                            //使用示例List<Dictionary<string, object>> ListDic = (List<Dictionary<string, object>>)Dic["Json中的Key"]; 
                            List<Dictionary<string, object>> List = new List<Dictionary<string, object>>();
                            MatchCollection ListMatch = Regex.Matches(item.Groups[2].ToString(), @"{[\s\S]+?}");//使用正则表达式匹配出JSON数组 
                            foreach (Match ListItem in ListMatch)
                            {
                                List.Add(ToDictionary(ListItem.ToString()));//递归调用 
                            }
                            Data = List;
                        }
                        else if (item.Groups[2].ToString().ToLower() == "null") Data = null;//如果数据为null(字符串类型),直接转换成null 
                        else Data = item.Groups[2].ToString(); //数据为数字、字符串中的一类，直接写入 
                        Dic.Add(item.Groups[1].ToString(), Data);
                    }
                    catch { }
                }
            }
            return Dic;
        }

        public static string SerializeToStr(Object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                return jss.Serialize(obj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 中德GET请求测试封装【捷慧通】
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="cid"></param>
        /// <param name="p"></param>
        /// <param name="sn"></param>
        /// <param name="tn"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string SendCarLock(string Url, string cid, string v, string p, string sn, string tn)
        {
            HttpWebResponse response = null;
            string resultstring = string.Empty;
            try
            {

                sn = GetSign(p + tn);
                SetFile("SN:" + sn);
                Encoding encoding = Encoding.GetEncoding("utf-8");
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("cid", cid);
                parameters.Add("v", v);
                parameters.Add("p", p);
                parameters.Add("sn", sn);
                parameters.Add("tn", tn);
                response = CreatePostHttpResponse(Url, parameters, encoding);
                SetFile("parameters:" + parameters.ToString());
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                resultstring = sr.ReadToEnd();

                stream.Close();
                sr.Close();
            }
            catch (Exception exp)
            {
                resultstring = "{\"resultCode\": 1, \"message\": \"" + exp.ToString() + "\"}";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return resultstring;
        }

        /// <summary>
        /// 登录 返回token【捷慧通】
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static string Login(string url, string cid)
        {
            if (url == null || ("").Equals(url))
            {
                return "登录失败！url链接为空！";
            }

            Encoding encoding = Encoding.GetEncoding("utf-8");
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("cid", cid);
            parameters.Add("usr", "880002801001422");
            parameters.Add("psw", "880002801001422");
            HttpWebResponse response = CreatePostHttpResponse(url, parameters, encoding);
            string status = response.StatusCode.ToString();
            if ("OK".Equals(status))
            {
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string json = sr.ReadToEnd();
                string s = "";
                IDictionary<string, object> results = (IDictionary<string, object>)JsonToDictionary(json);
                foreach (KeyValuePair<string, object> item in results)
                {
                    if (item.Key.ToString() == "token")
                    {
                        s = item.Value.ToString();
                        break;
                    }

                }
                return s;
            }
            else
            {
                return "获取失败！:" + status;
            }
        }

        /// <summary>
        /// MD5加密 返回大写
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private static string GetSign(string param)
        {
            return new MobileSoft.Common.DES().Md5(param).ToUpper();
        }

        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, Encoding charset)
        {
            HttpWebRequest request = null;
            request = WebRequest.Create(url) as HttpWebRequest;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=utf8";

            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }



        public static void SetFile(string msg)
        {
            string fileName = "AF" + DateTime.Now.ToString("yyyy-MM-dd");
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            if (Directory.Exists(filePath) == false)
            {
                Directory.CreateDirectory(filePath);
            }
            string fileAbstractPath = filePath + "\\" + fileName + ".txt";
            FileStream fs = new FileStream(fileAbstractPath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入     
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            msg = time + "，" + msg + System.Environment.NewLine;

            sw.Write(msg);
            //清空缓冲区               
            sw.Flush();
            //关闭流               
            sw.Close();
            sw.Dispose();
            fs.Close();
            fs.Dispose();
        }


        public static string Get(string url)
        {
            return Get(url, Encoding.UTF8);
        }
        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Get(string url, Encoding encoding)
        {
            try
            {
                var wc = new WebClient { Encoding = encoding };
                var readStream = wc.OpenRead(url);
                using (var sr = new StreamReader(readStream, encoding))
                {
                    var result = sr.ReadToEnd();
                    return result;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


    }
}
