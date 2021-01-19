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
    /// AlipayBuss 的摘要说明
    /// </summary>
    public class AlipayBuss_lf : IHttpHandler, IRequiresSessionState
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
                Business.Alipay.Log("购物收到支付宝通知" + context.Request.RawUrl + "?" + string.Join("&", list));

                HttpRequest Request = context.Request;
                Dictionary<string, string> sPara = GetRequestPost(context);

                decimal total_amount = AppGlobal.StrToDec(Request.Params["total_fee"].ToString());
                string respcode = Request.Params["trade_status"];
                string respmsg = Request.Params["trade_status"].ToString();
                string orderId = Request.Params["out_trade_no"].ToString();
                string body = Request.Params["body"].ToString();
                string sign = Request.Params["sign"];
                string notify_id = Request.Params["notify_id"];
                string bussId = body;

                string Result = "";

                Config c = new Config();
                bool IsConfig = new AlipayBusinessOrder_lf().GenerateConfig(bussId, out c);
                if (IsConfig == false)
                {
                    throw new Exception("未配置证书文件");
                }

                Business.AlipayBusinessOrder_lf.Log("开始验证:" + bussId.ToString() + "," + orderId);

                //返回报文中不包含UPOG,表示Server端正确接收交易请求,则需要验证Server端返回报文的签名
                Notify aliNotify = new Notify(c);

                if (aliNotify.GetResponseTxt(notify_id) == "true")
                {
                    Business.AlipayBusinessOrder_lf.Log("验证:" + bussId.ToString() + "," + orderId + " GetResponseTxt 正确");

                    if (aliNotify.GetSignVeryfy(sPara, sign))
                    {
                        Business.AlipayBusinessOrder_lf.Log("验证:" + bussId.ToString() + "," + orderId + " GetSignVeryfy 正确");

                        //更新订单返回状态
                        Business.AlipayBusinessOrder_lf.UpdateBusinessOrder(orderId, respcode, respmsg);
                        //已收到请求，无需再发送通知
                        Result = "success";
                        if (respcode == "TRADE_SUCCESS")
                        {
                            string ReceResult = Business.AlipayBusinessOrder_lf.ReceBusinessOrder(orderId, total_amount);

                            // 下账成功，操作积分
                            if (ReceResult == "success")
                            {
                                UsePoint(orderId);
                                PresentedPoint(bussId, orderId, total_amount);
                            }
                            Business.AlipayBusinessOrder_lf.Log("支付宝购物下账:" + bussId.ToString() + "," + orderId + "," + ReceResult);
                        }
                    }
                    else
                    {
                        Result = "支付宝购物验签失败:" + orderId;
                    }
                }
                else
                {
                    Result = "支付宝" + orderId + "订单信息不匹配!";
                }

                Business.Alipay.Log("支付宝购物流程:" + bussId.ToString() + "," + orderId + "," + Result);
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
                                                WHERE a.OrderID=@OrderID AND a.Payment='支付宝'",
                                                new
                                                {
                                                    OrderID = orderId,
                                                    UsableObject = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.Goods)
                                                }).FirstOrDefault();

                    if (orderInfo != null)
                    {
                        // 积分生效
                        conn.Execute(@"UPDATE Tb_App_Point_UseHistory SET IsEffect=1 WHERE IID=@IID", new { IID = orderInfo.UseHistoryID.ToString() });

                        Business.Alipay.Log("支付宝购物:使用了积分，记录id=" + orderInfo.UseHistoryID.ToString());
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
                            Business.Alipay.Log("支付宝购物:未赠送积分，平台商城不赠送积分");
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
                            Business.Alipay.Log("支付宝购物:未赠送积分，未找到用户信息");
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
                            conn.Execute(@"INSERT INTO Tb_App_Point_PresentedHistory(UserID,PresentedWay,PresentedPoints,PointBalance,Remark) 
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