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
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;
using Dapper;
using App.Model;

namespace Service.AlipayCallBack
{
    /// <summary>
    /// Alipay 的摘要说明
    /// </summary>
    public class AliPayPrec_lf : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                Business.Alipay.Log("收到支付宝预存通知");

                HttpRequest Request = context.Request;
                Dictionary<string, string> sPara = GetRequestPost(context);

                decimal total_amount = AppGlobal.StrToDec(Request.Params["total_fee"].ToString());
                string respcode = Request.Params["trade_status"];
                string respmsg = Request.Params["trade_status"].ToString();
                string orderId = Request.Params["out_trade_no"].ToString();
                string body = Request.Params["body"].ToString();
                string userId = null;
                int months = 0;
                if (body.Contains(","))
                {
                    months = AppGlobal.StrToInt(body.Split(',')[2]);
                    userId = body.Split(',')[1];
                    body = body.Split(',')[0];
                }

                string sign = Request.Params["sign"];
                string notify_id = Request.Params["notify_id"];
                string CommunityId = body;

                Config c = new Config();
                //初始化参数
                bool IsConfig = new Business.AlipayPrec_lf().GenerateConfig(CommunityId, out c);

                string Result = "";
                if (IsConfig == false)
                {
                    throw new Exception("未配置证书文件" + CommunityId);
                }

                Business.AlipayPrec_lf.Log("开始验证:" + CommunityId.ToString() + "," + orderId);

                //返回报文中不包含UPOG,表示Server端正确接收交易请求,则需要验证Server端返回报文的签名
                Notify aliNotify = new Notify(c);

                if (aliNotify.GetResponseTxt(notify_id) == "true")
                {
                    Business.AlipayPrec_lf.Log("验证:" + CommunityId.ToString() + "," + orderId + " GetResponseTxt 正确");

                    if (aliNotify.GetSignVeryfy(sPara, sign))
                    {
                        Business.AlipayPrec_lf.Log("验证:" + CommunityId.ToString() + "," + orderId + " GetSignVeryfy 正确");

                        //更新订单返回状态
                        Business.AlipayPrec_lf.UpdateProperyOrder(CommunityId, orderId, respcode, respmsg);
                        //已收到请求，无需再发送通知
                        Result = "success";
                        if (respcode == "TRADE_SUCCESS")
                        {
                            string ReceResult = Business.AlipayPrec_lf.ReceProperyOrder(CommunityId, orderId);
                            if (ReceResult == "success")
                            {
                                // 下账成功，送积分
                                PresentedPoint(userId, months);
                            }
                            Business.AlipayPrec_lf.Log("支付宝下账:" + CommunityId.ToString() + "," + orderId + "," + ReceResult);
                        }
                    }
                    else
                    {
                        Result = "支付宝验签失败:" + orderId;
                    }
                }
                else
                {
                    Result = "支付宝" + orderId + "订单信息不匹配!";
                }

                Business.AlipayPrec_lf.Log("支付宝流程:" + CommunityId.ToString() + "," + orderId + "," + Result);
                context.Response.ContentType = "text/plain";
                context.Response.Write(Result);
            }
            catch (Exception E)
            {
                Business.AlipayPrec_lf.Log(E.Message.ToString());
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

        /// <summary>
        /// 赠送积分
        /// </summary>
        public void PresentedPoint(string UserId, int Months)
        {
            int presentedPoint = new AppPoint().CalcPresentedPointForPrec_lf(Months);
            if (presentedPoint == 0)
            {
                return;
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                var trans = conn.BeginTransaction();

                try
                {
                    int pointBalance = 0;

                    var resultSet = conn.Query("SELECT * FROM Tb_App_UserPoint WHERE UserID=@UserID", new { UserID = UserId }, trans);

                    // 用户积分记录
                    if (resultSet.Count() == 0)
                    {
                        // 用户积分
                        conn.Execute(@"INSERT INTO Tb_App_UserPoint(UserID, PointBalance) VALUES(@UserID, @PointBalance)",
                            new { UserID = UserId, PointBalance = presentedPoint }, trans);
                    }
                    else
                    {
                        pointBalance = resultSet.FirstOrDefault().PointBalance + presentedPoint;
                        // 5、更新积分余额
                        conn.Execute("UPDATE Tb_App_UserPoint SET PointBalance=(PointBalance+@PresentedPoint) WHERE UserID=@UserID",
                            new
                            {
                                PresentedPoint = presentedPoint,
                                UserID = UserId
                            }, trans);
                    }

                    // 赠送历史
                    conn.Execute(@"INSERT INTO Tb_App_Point_PresentedHistory(UserID, PresentedWay, PresentedPoints, PointBalance, Remark) 
                                        VALUES(@UserID, @PresentedWay, @PresentedPoints, @PointBalance, @PresentedName)",
                        new
                        {
                            UserID = UserId,
                            PresentedWay = AppPointPresentedWayConverter.GetKey(AppPointPresentedWay.PropertyPrestorePayment),
                            PresentedPoints = presentedPoint,
                            PointBalance = pointBalance,
                            PresentedName = AppPointPresentedWayConverter.GetValue(AppPointPresentedWay.PropertyPrestorePayment)
                        }, trans);

                    trans.Commit();
                    Business.Alipay.Log("支付宝预存下账:赠送积分=" + presentedPoint);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Business.Alipay.Log("支付宝预存下账:赠送积分异常，" + ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
        }
    }
}