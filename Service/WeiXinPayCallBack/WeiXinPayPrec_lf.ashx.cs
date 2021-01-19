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
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;
using Dapper;
using App.Model;

namespace Service.WeiXinPayCallBack
{
    /// <summary>
    /// WeiXinPay 的摘要说明
    /// </summary>
    public class WeiXinPayPrec_lf : IHttpHandler, IRequiresSessionState
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
                string CommunityId = "";

                //返回报文中不包含UPOG,表示Server端正确接收交易请求,则需要验证Server端返回报文的签名
                bool IsValidate = false;
                WxPayData WxPostData = new WxPayData();
                string Result = Notify.NotifyDataFromContext(context, ref IsValidate, ref WxPostData);

                respcode = WxPostData.GetValue("result_code").ToString();
                respmsg = WxPostData.GetValue("result_code").ToString();
                orderId = WxPostData.GetValue("out_trade_no").ToString();
                CommunityId = WxPostData.GetValue("attach").ToString();
                string userId = null;
                int months = 0;
                if (CommunityId.Contains(","))
                {
                    months = AppGlobal.StrToInt(CommunityId.Split(',')[2]);
                    userId = CommunityId.Split(',')[1];
                    CommunityId = CommunityId.Split(',')[0];
                }

                if (IsValidate == false)
                {
                    Business.WeiXinPayPrec.Log("验签失败:" + CommunityId + "," + orderId.ToString());
                    Result = SetNotifyResult("FAIL", Result);
                    context.Response.Write(Result);
                    return;
                }

                Business.WeiXinPayPrec.Log("微信支付验签成功:" + CommunityId + "," + orderId.ToString());

                if (IsValidate == true)
                {
                    //初始化参数
                    new Business.WeiXinPayPrec().GenerateConfig(CommunityId);
                    //更新订单返回状态
                    Business.WeiXinPayPrec.UpdateProperyOrder(CommunityId, orderId, respcode, respmsg);
                    //已收到应答，无需再通知
                    context.Response.Write(SetNotifyResult("SUCCESS", "OK"));
                    if (respcode == "SUCCESS")
                    {
                        //下账
                        string ReceResult = Business.WeiXinPayPrec.ReceProperyOrder(CommunityId, orderId);

                        // 下账成功，送积分
                        PresentedPoint(userId, months);

                        Business.WeiXinPayPrec.Log("微信支付预存下账:" + CommunityId.ToString() + "," + orderId + "," + ReceResult);
                    }

                    Business.WeiXinPayPrec.Log("微信支付流程:" + CommunityId.ToString() + "," + orderId + ",SUCCESS");
                }
            }
            catch (Exception E)
            {
                Business.WeiXinPayPrec.Log(E.Message.ToString());
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
                    Business.Alipay.Log("微信预存下账:赠送积分=" + presentedPoint);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Business.Alipay.Log("微信预存下账:赠送积分异常，" + ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
        }
    }
}