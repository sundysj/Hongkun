using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business;
using System.Web.SessionState;
using System.Collections.Specialized;
using System.Text;
using WxPayAPI;
using MobileSoft.Common;

namespace Service.WeiXinPayCallBack
{
    /// <summary>
    /// WeiXinPayBuss 的摘要说明
    /// </summary>
    public class WeiXinPayBuss : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                HttpRequest Request = context.Request;
                context.Response.ContentType = "text/plain";

                string respcode = "";
                string respmsg = "";
                string orderId = "";
                string BussId = "";

                //返回报文中不包含UPOG,表示Server端正确接收交易请求,则需要验证Server端返回报文的签名
                bool IsValidate = false;
                WxPayData WxPostData = new WxPayData();
                string Result = Notify.NotifyDataFromContext(context, ref IsValidate, ref WxPostData);

                respcode = WxPostData.GetValue("result_code").ToString();
                respmsg = WxPostData.GetValue("result_code").ToString();
                orderId = WxPostData.GetValue("out_trade_no").ToString();
                BussId = WxPostData.GetValue("attach").ToString();

                if (IsValidate == false)
                {
                    Business.WeiXinPayBuss.Log("验签失败:" + BussId + "," + orderId.ToString());
                    Result = SetNotifyResult("FAIL", Result);
                    context.Response.Write(Result);
                    return;
                }

                Business.WeiXinPayBuss.Log("微信支付验签成功:" + BussId + "," + orderId.ToString());

                if (IsValidate == true)
                {
                    //初始化参数
                    new Business.WeiXinPayBuss().GenerateConfig(BussId);
                    //更新订单返回状态
                    Business.WeiXinPayBuss.UpdateBusinessOrder(orderId, respcode, respmsg);
                    //已收到应答，无需再通知
                    context.Response.Write(SetNotifyResult("SUCCESS", "OK"));
                    if (respcode == "SUCCESS")
                    {
                        int totalFee = AppGlobal.StrToInt(WxPostData.GetValue("total_fee").ToString());
                        //下账
                        string ReceResult = Business.WeiXinPayBuss.ReceBusinessOrder(orderId, totalFee);
                        Business.WeiXinPayBuss.Log("微信支付下账:" + BussId.ToString() + "," + orderId + "," + ReceResult);
                    }

                    Business.WeiXinPayBuss.Log("微信支付流程:" + BussId.ToString() + "," + orderId + ",SUCCESS");
                }
            }
            catch (Exception E)
            {
                Business.WeiXinPayBuss.Log(E.Message.ToString());
                context.Response.ContentType = "text/plain";
                context.Response.Write(SetNotifyResult("FAIL", E.Message.ToString()));
            }
        }

        public static string SetNotifyResult(string State, string Msg)
        {
            WxPayData res = new WxPayData();
            res.SetValue("return_code", State);
            res.SetValue("return_msg", Msg);
            return res.ToXml();
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