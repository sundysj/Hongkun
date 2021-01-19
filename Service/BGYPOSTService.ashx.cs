using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business;
using System.Web.SessionState;
namespace Service
{
    /// <summary>
    /// Service 的摘要说明
    /// 自2016年10月18日起，所有APP段接口更新，只新增接口，不修改还接口
    /// </summary>
    public class BGYPOSTService : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            Common.Transfer Trans = new Common.Transfer();

            //密钥类型
            //不同调用者密钥不同
            string MacType = "";

            HttpRequest Request = context.Request;
            Trans.Class = Request["Class"].ToString();
            Trans.Command = Request["Command"].ToString();                         
            Trans.Mac = Request["Mac"].ToString();

            if (Request["ComCode"]!=null)
            {
                MacType = Request["ComCode"].ToString();
            }

            PubContext.OperateRSA(ref Trans,MacType);
            context.Response.ContentType = "text/plain";
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.Write(Trans.Output());
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