using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Service.Webhook
{
    /// <summary>
    /// Bugly 的摘要说明
    /// </summary>
    public class Bugly : IHttpHandler
    {
        private static ILog log;

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest Request = context.Request;
            HttpResponse Response = context.Response;
            context.Response.ContentType = "text/plain";
            try
            {
                if (!"POST".Equals(Request.HttpMethod.ToUpper()))
                {
                    GetLog().Info("不是POST通知");
                    return;
                }
                string json = "";
                #region 接收json数据
                Stream stream = Request.InputStream;
                if (null != stream && stream.Length > 0)
                {
                    StreamReader streamReader = new StreamReader(stream);
                    json = streamReader.ReadToEnd();
                    streamReader.Close();
                    streamReader.Dispose();
                    stream.Close();
                    stream.Dispose();
                }
                #endregion

                GetLog().Info("BUGLY:" + json);
            }
            catch (Exception ex)
            {
                GetLog().Error(ex);
                return;
            }
        }
        public static ILog GetLog()
        {
            if (null == log)
            {
                log = LogManager.GetLogger(typeof(Bugly));
            }
            return log;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}