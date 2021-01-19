using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business;
using System.Web.SessionState;
using System.Collections.Specialized;
using System.Text;
using com.unionpay.acp.sdk;

namespace Service.CallBack
{
    /// <summary>
    /// UnionPay 的摘要说明
    /// </summary>
    public class UnionPayPrec : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                HttpRequest Request = context.Request;
                Dictionary<string, string> resData = new Dictionary<string, string>();
                NameValueCollection coll = Request.Form;
                string[] requestItem = coll.AllKeys;
                for (int i = 0; i < requestItem.Length; i++)
                {
                    resData.Add(requestItem[i], Request.Form[requestItem[i]]);
                }
                string respcode = resData["respCode"];
                string respmsg = resData["respMsg"].ToString();
                string orderId = resData["orderId"].ToString();
                string CommunityId = resData["reqReserved"].ToString();

                string Result = "";

                SDKConfig con = new SDKConfig();
                //初始化参数
                bool IsConfig = new Business.UnionPayPrec().GenerateConfig(CommunityId,out con);
                             
                if (IsConfig == false)
                {
                    throw new Exception("未配置证书文件");
                }

                Business.UnionPayPrec.Log("银联开始验签:" + CommunityId.ToString() + "," + orderId);
                //返回报文中不包含UPOG,表示Server端正确接收交易请求,则需要验证Server端返回报文的签名
                if (com.unionpay.acp.sdk.AcpService.Validate(resData, System.Text.Encoding.UTF8,con))
                {
                   
                    //更新订单返回状态
                    Business.UnionPayPrec.UpdateProperyOrder(CommunityId, orderId, respcode, respmsg);
                    //已收到应答无需再通知
                    Result = "00";
                    if (respcode == "00")
                    {
                        //下账
                        string ReceResult = Business.UnionPayPrec.ReceProperyOrder(CommunityId, orderId);
                        Business.UnionPayPrec.Log("银联预存下账:" + CommunityId.ToString() + "," + orderId + "," + ReceResult);
                    }
                }
                else
                {
                    Business.UnionPayPrec.Log("银联验签失败:" + CommunityId.ToString() + "," + orderId);
                }

                Business.UnionPayPrec.Log("银联流程:" + CommunityId.ToString() + "," + orderId + "," + Result);
                context.Response.ContentType = "text/plain";
                context.Response.Write(Result);
            }
            catch (Exception E)
            {
                Business.UnionPayPrec.Log(E.Message.ToString());
                context.Response.ContentType = "text/plain";
                context.Response.Write(E.Message.ToString());
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