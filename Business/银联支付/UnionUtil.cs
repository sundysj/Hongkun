using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using com.unionpay.acp.sdk;
using System.Data;
using MobileSoft.Model.Unified;
/**
 * 仅供UPOP联机对账文件下载使用
 * */
namespace Business
{
    public class UnionUtil
    {
        /// <summary>
        /// 得到用于打印页面的html
        /// </summary>
        /// <param name="url"></param>
        /// <param name="req"></param>
        /// <param name="resp"></param>
        public static string GetPrintResult(string url, Dictionary<string, string> req, Dictionary<string, string> resp)
        {
            string result = "=============\n";
            result = result + "地址：" + url + "\n";
            result = result + "请求：" + System.Web.HttpContext.Current.Server.HtmlEncode(SDKUtil.CreateLinkString(req, false, true)).Replace("\n", "\n") + "\n";
            result = result + "应答：" + System.Web.HttpContext.Current.Server.HtmlEncode(SDKUtil.CreateLinkString(resp, false, true)).Replace("\n", "\n") + "\n";
            result = result + "=============\n";
            return result;
        }

        public static string GetConnectionString(DataRow T)
        {
            string strDBServer = T["DBServer"].ToString();
            string strDBName = T["DBName"].ToString();
            string strDBUser = T["DBUser"].ToString();
            string strDBPwd = T["DBPwd"].ToString();
            string ConnStr = string.Format("Connect Timeout=100;Connection Lifetime=60;Max Pool Size=1000;Min Pool Size=0;Pooling=true;data source={0};initial catalog={1};PWD={3};persist security info=False;user id={2};packet size=4096", strDBServer, strDBName, strDBUser, strDBPwd);
            return ConnStr;
        }

        public static string GetConnectionString(Tb_Community T)
        {
            string strDBServer = T.DBServer.ToString();
            string strDBName = T.DBName.ToString();
            string strDBUser = T.DBUser.ToString();
            string strDBPwd = T.DBPwd.ToString();
            string ConnStr = string.Format("Connect Timeout=100;Connection Lifetime=60;Max Pool Size=1000;Min Pool Size=0;Pooling=true;data source={0};initial catalog={1};PWD={3};persist security info=False;user id={2};packet size=4096", strDBServer, strDBName, strDBUser, strDBPwd);
            return ConnStr;
        }
    }
}