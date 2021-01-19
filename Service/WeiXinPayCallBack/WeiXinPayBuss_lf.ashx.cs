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
    /// WeiXinPayBuss 的摘要说明
    /// </summary>
    public class WeiXinPayBuss_lf : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                List<string> list = new List<string>();
                foreach (var item in context.Request.Params.AllKeys)
                {
                    list.Add($"{item}={context.Request.Params[item]}");
                }
                Business.WeiXinPayBuss_lf.Log("收到微信通知" + context.Request.RawUrl + "?" + string.Join("&", list));

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
                    Business.WeiXinPayBuss_lf.Log("验签失败:" + BussId + "," + orderId.ToString());
                    Result = SetNotifyResult("FAIL", Result);
                    context.Response.Write(Result);
                    return;
                }

                Business.WeiXinPayBuss_lf.Log("微信支付验签成功:" + BussId + "," + orderId.ToString());

                if (IsValidate == true)
                {
                    //初始化参数
                    new Business.WeiXinPayBuss_lf().GenerateConfig(BussId);
                    //更新订单返回状态
                    Business.WeiXinPayBuss_lf.UpdateBusinessOrder(orderId, respcode, respmsg);
                    //已收到应答，无需再通知
                    context.Response.Write(SetNotifyResult("SUCCESS", "OK"));
                    if (respcode == "SUCCESS")
                    {
                        decimal total_fee = AppGlobal.StrToDec(WxPostData.GetValue("total_fee").ToString()) / 100.0m;

                        //下账
                        string ReceResult = Business.WeiXinPayBuss_lf.ReceBusinessOrder(orderId, total_fee);

                        // 下账成功，操作积分
                        if (ReceResult == "success")
                        {
                            UsePoint(orderId);
                            PresentedPoint(BussId, orderId, total_fee);
                        }

                        Business.WeiXinPayBuss_lf.Log("微信购物下账:" + BussId.ToString() + "," + orderId + "," + ReceResult);
                    }

                    Business.WeiXinPayBuss.Log("微信购物流程:" + BussId.ToString() + "," + orderId + ",SUCCESS");
                }
            }
            catch (Exception E)
            {
                Business.WeiXinPayBuss_lf.Log(E.Message.ToString());
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
        /// 是否使用了积分
        /// </summary>
        private void UsePoint(string orderId)
        {
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                try
                {
                    var orderInfo = conn.Query(@"SELECT a.UseHistoryID,b.UserID FROM Tb_App_Point_UseHistoryOrder a 
                                            LEFT JOIN Tb_App_Point_UseHistory b ON a.UseHistoryID=b.IID
                                            WHERE a.OrderID=@OrderID AND a.Payment='微信' AND a.UsableObject=@UsableObject",
                                            new
                                            {
                                                OrderID = orderId,
                                                UsableObject = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.Goods)
                                            }).FirstOrDefault();

                    if (orderInfo != null)
                    {
                        // 积分生效
                        conn.Execute(@"UPDATE Tb_App_Point_UseHistory SET IsEffect=1 WHERE IID=@IID", new { IID = orderInfo?.UseHistoryID.ToString() });

                        Business.Alipay.Log("微信购物:使用了积分，记录id=" + orderInfo?.UseHistoryID.ToString());
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// 计算赠送积分
        /// </summary>
        private void PresentedPoint(string bussId, string orderId, decimal paidAmount)
        {
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var trans = conn.BeginTransaction();

                try
                {
                    int corpId = 0;
                    string userId = null;
                    using (var bzconn = new SqlConnection(PubConstant.BusinessContionString))
                    {
                        var sql = @"SELECT BussNature FROM Tb_System_BusinessCorp WHERE BussId=@BussId;
                                    SELECT CorpID FROM Unified..Tb_Community WHERE Id=(SELECT CommunityId FROM Tb_System_BusinessConfig 
                                                                                    WHERE BussId=@BussId);
                                    SELECT CorpID FROM Tb_System_BusinessCorp_Config WHERE BussId=@BussId";

                        var reader = bzconn.QueryMultiple(sql, new { BussId = bussId });

                        var bussNature = reader.Read<string>().FirstOrDefault();
                        if (bussNature == "平台商城")
                        {
                            Business.Alipay.Log("微信购物:未赠送积分，平台商城不赠送积分");
                            return;
                        }

                        var nature_1 = reader.Read<int>().FirstOrDefault();
                        var nature_2 = reader.Read<int>().FirstOrDefault();

                        if (nature_1 != 0)
                        {
                            corpId = nature_1;
                        }
                        if (nature_2 != 0)
                        {
                            corpId = nature_2;
                        }

                        userId = bzconn.Query<string>(@"SELECT UserId FROM Tb_Charge_Receipt WHERE OrderId=@OrderId", new { OrderId = orderId }).FirstOrDefault();
                        if (string.IsNullOrEmpty(userId))
                        {
                            Business.Alipay.Log("微信购物:未赠送积分，未找到用户信息");
                            return;
                        }
                    }

                    var rules = conn.Query<Tb_App_Point_PropertyPresentedRule>(@"SELECT * FROM Tb_App_Point_PropertyPresentedRule 
                                                                                WHERE CorpID=@CorpID AND CommunityID IS NULL AND PresentedObject=@PresentedObject AND IsDelete=0 
                                                                                AND getdate() BETWEEN StartTime AND EndTime ORDER BY ConditionAmount",
                                        new
                                        {
                                            CorpID = corpId,
                                            PresentedObject = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.Goods)
                                        }, trans);

                    if (rules.Count() > 0)
                    {
                        Tb_App_Point_PropertyPresentedRule current = null;
                        foreach (var item in rules)
                        {
                            if (paidAmount >= item.ConditionAmount)
                            {
                                current = item;
                            }
                        }

                        if (current != null)
                        {
                            

                            var userPoint = conn.Query("SELECT * FROM Tb_App_UserPoint WHERE UserID=@UserID", new { UserID = userId }, trans).FirstOrDefault();
                            int balance = 0;

                            if (userPoint == null)
                            {
                                balance = 0;
                                conn.Execute(@"INSERT INTO Tb_App_UserPoint(UserID, PointBalance) VALUES(@UserID, @PointBalance)",
                                new { UserID = userId, PointBalance = current.PresentedPoints }, trans);
                            }
                            else
                            {
                                balance = userPoint.PointBalance;
                                conn.Execute("UPDATE Tb_App_UserPoint SET PointBalance=(PointBalance+@PresentedPoints) WHERE UserID=@UserID",
                                    new
                                    {
                                        PresentedPoints = current.PresentedPoints,
                                        UserID = userId
                                    }, trans);
                            }

                            // 赠送历史
                            conn.Execute(@"INSERT INTO Tb_App_Point_PresentedHistory(UserID, PresentedWay, PresentedPoints, PointBalance, Remark) 
                                        VALUES(@UserID, @PresentedWay, @PresentedPoints, @PointBalance, @Remark)",
                                new
                                {
                                    UserID = userId,
                                    PresentedWay = AppPointPresentedWayConverter.GetKey(AppPointPresentedWay.StoreTrade),
                                    PresentedPoints = current.PresentedPoints,
                                    PointBalance = balance + current.PresentedPoints,
                                    Remark = AppPointPresentedWayConverter.GetValue(AppPointPresentedWay.StoreTrade),
                                }, trans);
                        }
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Business.Alipay.Log("支付宝购物:计算赠送积分异常，" + ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
        }
    }
}