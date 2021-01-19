using Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;

namespace Service
{
    /// <summary>
    /// Api 的摘要说明
    /// </summary>
    public class Api : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            try
            {
                Common.Transfer Trans = new Common.Transfer();

                HttpRequest Request = context.Request;
                Dictionary<string, string> param = new Dictionary<string, string>();
                foreach (string key in Request.Form.AllKeys)
                {
                    param.Add(key, Request.Params[key]);
                }
                if (!param.ContainsKey("Class"))
                {
                    context.Response.Write(new ApiResult(false, "缺少参数Class").toJson());
                    return;
                }
                Trans.Class = param["Class"].ToString();
                if (string.IsNullOrEmpty(Trans.Class))
                {
                    context.Response.Write(new ApiResult(false, "Class不能为空").toJson());
                    return;
                }
                if (!param.ContainsKey("Command"))
                {
                    context.Response.Write(new ApiResult(false, "缺少参数Command").toJson());
                    return;
                }
                Trans.Command = param["Command"].ToString();

                if (string.IsNullOrEmpty(Trans.Command))
                {
                    context.Response.Write(new ApiResult(false, "Command不能为空").toJson());
                    return;
                }
                
                string json = JsonConvert.SerializeObject(new { Attribute = param });
                Trans.Attribute = JsonConvert.DeserializeXmlNode(json).OuterXml;
                if (string.IsNullOrEmpty(Trans.Attribute))
                {
                    context.Response.Write(new ApiResult(false, "Attribute不能为空").toJson());
                    return;
                }
                PubInfo Rp = (PubInfo)Assembly.Load("Business").CreateInstance("Business." + Trans.Class);
                var HashString = Trans.Attribute.ToString() + DateTime.Now.ToString("yyyyMMdd") + Rp.Token;
                var Mac = AppPKI.getMd5Hash(HashString);
                Trans.Mac = Mac;
                if (Trans.Class == "Files")
                {
                    new Files().ProcessRequest(context);
                }
                else
                {
                    PubContext.Operate(ref Trans);
                }

                Compress(context);
                context.Response.Write(Trans.Output());
            }
            catch (Exception ex)
            {
                context.Response.Write(new ApiResult(false, ex.Message + Environment.NewLine + ex.StackTrace).toJson());
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void Compress(HttpContext context)
        {
            //查看请求头部
            string acceptEncoding = context?.Request?.Headers["Accept-Encoding"]?.ToString().ToUpperInvariant();
            if (!String.IsNullOrEmpty(acceptEncoding))
            {
                //如果头部里有包含"GZIP”,"DEFLATE",表示你浏览器支持GZIP,DEFLATE压缩
                if (acceptEncoding.Contains("GZIP"))
                {
                    //向输出流头部添加压缩信息
                    context.Response.AppendHeader("Content-encoding", "gzip");
                    context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
                }
                else if (acceptEncoding.Contains("DEFLATE"))
                {
                    //向输出流头部添加压缩信息
                    context.Response.AppendHeader("Content-encoding", "deflate");
                    context.Response.Filter = new DeflateStream(context.Response.Filter, CompressionMode.Compress);
                }
            }
        }
    }
}