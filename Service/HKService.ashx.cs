using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Service
{
    /// <summary>
    /// HKService 好停车同步车辆临停记录的摘要说明
    /// </summary>
    public class HKService : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {

                Common.Transfer Trans = new Common.Transfer();
                HttpRequest Request = context.Request;
          
                Trans.Class = "HKParkCostInfo";
                Trans.Command = "TempPayFees";
                //获取content内容
                System.IO.Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                string ContentStr = System.Text.Encoding.UTF8.GetString(b);
                if (ContentStr != "")
                {
                    Trans.Attribute = ContentStr;
                }
                PubContext.Operate(ref Trans);

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
    }
}