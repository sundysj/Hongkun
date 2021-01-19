using Business;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Service
{
    /// <summary>
    /// JHService  金辉用来更新用户的上线下线状态
    /// </summary>
    public class JHService : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            try
            {

                Common.Transfer Trans = new Common.Transfer();

                HttpRequest Request = context.Request;
                if (!Request.Params.AllKeys.Contains("Class"))
                {
                    context.Response.Write(new ApiResult(false, "缺少参数Class").toJson());
                    return;
                }
                Trans.Class = Request["Class"].ToString();
                if (string.IsNullOrEmpty(Trans.Class))
                {
                    context.Response.Write(new ApiResult(false, "Class不能为空").toJson());
                    return;
                }
                if (!Request.Params.AllKeys.Contains("Command"))
                {
                    context.Response.Write(new ApiResult(false, "缺少参数Command").toJson());
                    return;
                }
                Trans.Command = Request["Command"].ToString();

                if (string.IsNullOrEmpty(Trans.Command))
                {
                    context.Response.Write(new ApiResult(false, "Command不能为空").toJson());
                    return;
                }

                if (!Request.Params.AllKeys.Contains("Mac"))
                {
                    context.Response.Write(new ApiResult(false, "缺少参数Mac").toJson());
                    return;
                }
                Trans.Mac = Request["Mac"].ToString();
                if (string.IsNullOrEmpty(Trans.Mac))
                {
                    context.Response.Write(new ApiResult(false, "Mac不能为空").toJson());
                    return;
                }
                //获取content内容
                System.IO.Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                string ContentStr = System.Text.Encoding.UTF8.GetString(b);
                if (ContentStr != "")
                {
                    Trans.Agreement = ContentStr;
                }
                PubContext.Operate(ref Trans);

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