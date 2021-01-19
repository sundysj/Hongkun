using Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Service
{
    /// <summary>
    /// HKLogin 的摘要说明
    /// </summary>
    public class HKLogin : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;

            var login = new Business.HKLogin(context.Request);

            var result = login.Valiadate();

            context.Response.Write(result);
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