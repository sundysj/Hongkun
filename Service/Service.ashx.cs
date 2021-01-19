using Business;
using Common;
using log4net;
using System;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Service
{
    /// <summary>
    /// Service 的摘要说明
    /// 自2016年10月18日起，所有APP段接口更新，只新增接口，不修改还接口
    /// </summary>
    public class Service : IHttpHandler, IRequiresSessionState
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Service));
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

                //获取要执行的类名称
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

                //wlg 20191204 没有地方使用？？？
                if(Request["QYID"] !=null)
                {
                    Trans.QYID = Request["QYID"].ToString();
                }

                //wlg 20191204 没有地方使用？？？
                if (Request["QYUnitType"] != null)
                {
                    Trans.QYUnitType = Request["QYUnitType"].ToString();
                }
                
                //获取命令类型
                Trans.Command = Request["Command"].ToString();

                if (string.IsNullOrEmpty(Trans.Command))
                {
                    context.Response.Write(new ApiResult(false, "Command不能为空").toJson());
                    return;
                }
                if (Request.Params.AllKeys.Contains("Agreement"))
                {
                    Trans.Agreement = HttpUtility.UrlDecode(Request["Agreement"].ToString());//碧桂园 获取协议html代码
                }

                //获取属性xml格式字符串
                if (!Request.Params.AllKeys.Contains("Attribute"))
                {
                    context.Response.Write(new ApiResult(false, "缺少参数Attribute").toJson());
                    return;
                }
                Trans.Attribute = HttpUtility.UrlDecode(Request["Attribute"].ToString());
                if (string.IsNullOrEmpty(Trans.Attribute))
                {
                    context.Response.Write(new ApiResult(false, "Attribute不能为空").toJson());
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

              
                //如果是文件类型
                if (Trans.Class == "Files")
                {
                    new Files().ProcessRequest(context);
                }
                else
                {
                    //wlg 20200110 增加操作日志记录
                    Common.Transfer tranNew = new Transfer()
                    {
                        Result = Trans.Result,
                        Attribute = Trans.Attribute,
                        Command = Trans.Command,
                        Class = Trans.Class,
                        Mac = Trans.Mac,
                        QYID = Trans.QYID,
                        QYUnitType = Trans.QYUnitType
                    };
                    RecordOperationLog(tranNew);

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

        private void RecordOperationLog(Transfer transNew1)
        {
            Common.Transfer tranNew2 = new Transfer()
            {
                Result = transNew1.Result,
                Attribute = transNew1.Attribute,
                Command = transNew1.Command,
                Class = transNew1.Class,
                Mac = transNew1.Mac,
                QYID = transNew1.QYID,
                QYUnitType = transNew1.QYUnitType,
                ClassLog = transNew1.Class,
                CommandLog =transNew1.Command
            };

            tranNew2.Class = "RecordClientInfo";
            tranNew2.Command = "RecordOperationLog";
            var HashString = tranNew2.Attribute.ToString() + DateTime.Now.ToString("yyyyMMdd") + "20200110Client";
            tranNew2.Mac = AppPKI.getMd5Hash(HashString);

            PubContext.Operate(ref tranNew2);
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