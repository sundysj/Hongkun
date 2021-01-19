using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business;
using System.Web.SessionState;
using System.Collections.Specialized;
using System.Text;
using Com.Alipay;
using MobileSoft.Common;

namespace Service.AlipayCallBack
{
    /// <summary>
    /// AlipayBuss 的摘要说明
    /// </summary>
    public class AlipayBuss : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest Request = context.Request;
            Dictionary<string, string> sPara = GetRequestPost(context);

            Business.AlipayBusinessOrder.Log("收到支付宝通知");
            Business.AlipayBusinessOrder.Log(Request.Url.AbsoluteUri);

            foreach (var key in sPara.Keys)
            {
                Business.AlipayBusinessOrder.Log(string.Format("key={0}, value={1}", key, sPara[key]));
            }

            try
            {
                string respcode = Request.Form["trade_status"];
                string respmsg = Request.Form["trade_status"].ToString();
                string orderId = Request.Form["out_trade_no"].ToString();
                string CommunityId = Request.Form["body"].ToString();
                decimal realAmount = AppGlobal.StrToDec(Request.Form["total_fee"].ToString());

                string notify_id = Request.Form["notify_id"];//获取notify_id

                string sign = Request.Form["sign"];//获取sign

                Config c = new Config();
                //初始化参数
                bool IsConfig = new AlipayBusinessOrder().GenerateConfig(CommunityId,out c);
                
                string Result = "";
                if (IsConfig == false)
                {
                    throw new Exception("未配置证书文件");
                }

                Business.AlipayBusinessOrder.Log("开始验证:" + CommunityId.ToString() + "," + orderId);

                //返回报文中不包含UPOG,表示Server端正确接收交易请求,则需要验证Server端返回报文的签名
                Notify aliNotify = new Notify(c);

                if (aliNotify.GetResponseTxt(notify_id) == "true")
                {
                    Business.AlipayBusinessOrder.Log("验证:" + CommunityId.ToString() + "," + orderId + " GetResponseTxt 正确");

                    if (aliNotify.GetSignVeryfy(sPara, sign))
                    {
                        Business.AlipayBusinessOrder.Log("验证:" + CommunityId.ToString() + "," + orderId + " GetSignVeryfy 正确");

                        //更新订单返回状态
                        Business.AlipayBusinessOrder.UpdateBusinessOrder(orderId, respcode, respmsg);
                        //已收到请求，无需再发送通知
                        Result = "success";
                        if (respcode == "TRADE_SUCCESS")
                        {
                            string ReceResult = Business.AlipayBusinessOrder.ReceBusinessOrder(orderId, realAmount);
                            Business.AlipayBusinessOrder.Log("支付宝商家下账:" + CommunityId.ToString() + "," + orderId + "," + ReceResult);
                        }
                    }
                    else
                    {
                        Result = "支付宝商家验签失败:" + orderId;
                    }
                }
                else
                {
                    Result = "支付宝" + orderId + "订单信息不匹配!";
                }

                Business.Alipay.Log("支付宝流程:" + CommunityId.ToString() + "," + orderId + "," + Result);
                context.Response.ContentType = "text/plain";
                context.Response.Write(Result);
            }
            catch (Exception E)
            {
                Business.Alipay.Log(E.Message.ToString() + E.StackTrace + E.Source);
                context.Response.ContentType = "text/plain";
                context.Response.Write(E.Message.ToString());
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public Dictionary<string, string> GetRequestPost(HttpContext context)
        {
            int i = 0;
            SortedDictionary<string, string> sArraytemp = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = context.Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArraytemp.Add(requestItem[i], context.Request.Form[requestItem[i]]);
            }
            Dictionary<string, string> sArray = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> temp in sArraytemp)
            {
                sArray.Add(temp.Key, temp.Value);
            }
            return sArray;
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