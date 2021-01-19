using App.Model;
using Business;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using WxPayAPI;

namespace Service.WeiXinPayCallBack
{
    /// <summary>
    /// WeiXinPay 的摘要说明
    /// </summary>
    public class WeiXinPay : IHttpHandler, IRequiresSessionState
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
                Business.Alipay.Log("收到微信通知" + context.Request.RawUrl + "?" + string.Join("&", list));

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
                if (CommunityId.Contains(","))
                {
                    userId = CommunityId.Split(',')[1];
                    CommunityId = CommunityId.Split(',')[0];
                }

                if (IsValidate == false)
                {
                    Business.WeiXinPay.Log("验签失败:" + CommunityId + "," + orderId.ToString());
                    Result = SetNotifyResult("FAIL", Result);
                    context.Response.Write(Result);
                    return;
                }

                Business.WeiXinPay.Log("微信支付验签成功:" + CommunityId + "," + orderId.ToString());

                if (IsValidate == true)
                {
                    //初始化参数
                    new Business.WeiXinPay().GenerateConfig(CommunityId);
                    //更新订单返回状态
                    Business.WeiXinPay.UpdateProperyOrder(CommunityId, orderId, respcode, respmsg);
                    //已收到应答，无需再通知
                    context.Response.Write(SetNotifyResult("SUCCESS", "OK"));
                    if (respcode == "SUCCESS")
                    {
                        string useHistoryId = null;

                        if (!string.IsNullOrEmpty(userId))
                        {
                            // 查询是否使用了积分
                            UsePoint(orderId, userId, out useHistoryId);
                        }

                        decimal total_fee = AppGlobal.StrToDec(WxPostData.GetValue("total_fee").ToString()) / 100.0m;

                        //下账
                        string ReceResult = Business.WeiXinPay.ReceProperyOrder(total_fee, CommunityId, orderId, useHistoryId);

                        if (!string.IsNullOrEmpty(userId))
                        {
                            // 下账成功，赠送积分
                            PresentedPoint(CommunityId, orderId, userId, useHistoryId, total_fee);
                        }

                        Business.WeiXinPay.Log("微信支付下账:" + CommunityId.ToString() + "," + orderId + "," + ReceResult);
                    }

                    Business.WeiXinPay.Log("微信支付流程:" + CommunityId.ToString() + "," + orderId + ",SUCCESS");
                }
            }
            catch (Exception E)
            {
                Business.WeiXinPay.Log(E.Message.ToString());
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
        private void UsePoint(string orderId, string userId, out string useHistoryId)
        {
            useHistoryId = null;
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var trans = conn.BeginTransaction();

                try
                {
                    var orderInfo = conn.Query(@"SELECT a.UseHistoryID,b.UserID FROM Tb_App_Point_UseHistoryOrder a 
                                                LEFT JOIN Tb_App_Point_UseHistory b ON a.UseHistoryID=b.IID
                                                WHERE a.OrderID=@OrderID AND a.Payment='微信'",
                                                new
                                                {
                                                    OrderID = orderId,
                                                    UsableObject = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.PropertyFee)
                                                }, trans).FirstOrDefault();
                    
                    useHistoryId = orderInfo?.UseHistoryID.ToString();

                    if (!string.IsNullOrEmpty(userId))
                    {
                        // 积分生效
                        conn.Execute(@"UPDATE Tb_App_Point_UseHistory SET IsEffect=1 WHERE IID=@IID", new { IID = useHistoryId }, trans);

                        Business.Alipay.Log("微信下账:使用了积分，记录id=" + useHistoryId);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Business.Alipay.Log("微信下账:判断积分异常，" + ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 计算赠送积分
        /// </summary>
        private void PresentedPoint(string communityId, string orderId, string userId, string useHistoryId, decimal paidAmount)
        {
            using (IDbConnection appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                if (appConn.State == ConnectionState.Closed)
                {
                    appConn.Open();
                }

                var trans = appConn.BeginTransaction();

                try
                {
                    decimal propertyMaxDiscountsAmount = 0.0m;  // 物管费最大可抵用金额
                    decimal parkingMaxDiscountsAmount = 0.0m;   // 车位费最大可抵用金额

                    var deductionObject = new List<string>();   // 积分可抵扣对象

                    using (var erpConn = new SqlConnection(PubInfo.GetConnectionStr(PubInfo.GetCommunity(communityId))))
                    {
                        var feesIds = erpConn.Query<long>(@"SELECT FeesId FROM Tb_OL_WeixinPayDetail WHERE PayOrderId=
                                                            (SELECT Id FROM Tb_OL_WeixinPayOrder WHERE out_trade_no=@out_trade_no)",
                                                            new { out_trade_no = orderId });

                        if (feesIds.Count() == 0)
                        {
                            return;
                        }

                        #region 获取积分抵扣规则
                        // 企业编号
                        short corpId = appConn.Query<short>("SELECT CorpID FROM Tb_Community WHERE Id=@CommunityId",
                            new { CommunityId = communityId }, trans).FirstOrDefault();

                        // 积分权限控制
                        var controlInfo = appConn.Query<Tb_Control_AppPoint>(@"SELECT * FROM Tb_Control_AppPoint WHERE CorpID=@CorpID AND IsEnable=1 
                                                                            AND (CommunityID=@CommunityId OR CommunityID IS NULL) ORDER BY CommunityID DESC",
                                                                            new { CorpID = corpId, CommunityId = communityId }, trans).FirstOrDefault();

                        if (controlInfo == null || controlInfo.IsEnable == false)
                        {
                            controlInfo = Tb_Control_AppPoint.DefaultControl;
                            controlInfo.CommunityID = Guid.Empty.ToString();
                        }

                        // 允许抵用物业费
                        if (controlInfo.AllowDeductionPropertyFees)
                        {
                            deductionObject.Add($@"'{AppPointUsableObjectConverter.GetKey(AppPointUsableObject.PropertyFee)}'");
                        }
                        // 允许抵用车位费
                        if (controlInfo.AllowDeductionParkingFees)
                        {
                            deductionObject.Add($@"'{AppPointUsableObjectConverter.GetKey(AppPointUsableObject.ParkingFee)}'");
                        }
                        #endregion

                        // 不计算违约金
                        string sql = $@"SELECT isnull(sum(isnull(DebtsAmount,0)),0) 
                                            FROM view_HSPR_Fees_Filter WHERE FeesID IN({string.Join(",", feesIds)}) AND SysCostSign = 'B0001';
                                        SELECT isnull(sum(isnull(DebtsAmount,0)),0)
                                            FROM view_HSPR_Fees_Filter WHERE FeesID IN({string.Join(",", feesIds)}) AND SysCostSign = 'B0002';";
                        var reader = erpConn.QueryMultiple(sql);

                        // 物业费、车位费实际欠费总额
                        decimal propertyAmount = reader.Read<decimal>().FirstOrDefault();
                        decimal parkingAmount = reader.Read<decimal>().FirstOrDefault();

                        // 积分抵用了部分金额
                        if (!string.IsNullOrEmpty(useHistoryId))
                        {
                            // 积分抵用的金额数量
                            var deductionAmount = appConn.Query<decimal>(@"SELECT isnull(DeductionAmount,0) FROM Tb_App_Point_UseHistory WHERE IID=@IID;",
                                            new { IID = useHistoryId }, trans).FirstOrDefault();

                            // 计算相关抵用的金额
                            if (deductionAmount != 0)
                            {
                                // 积分抵用规则
                                var ruleInfo = appConn.Query($@"SELECT IID,ConditionAmount,DiscountsAmount,DeductionObject,b.Remark AS SysCostSign,StartTime,EndTime 
                                                            FROM Tb_App_Point_PropertyDeductionRule a LEFT JOIN Tb_Dictionary_Point_UsableObject b
                                                            ON a.DeductionObject=b.[Key] 
                                                            WHERE CommunityID=@CommunityId AND DeductionObject IN({string.Join(", ", deductionObject) }) 
                                                            AND getdate() BETWEEN StartTime AND EndTime AND a.IsDelete=0 ORDER BY ConditionAmount,DiscountsAmount",
                                                                  new { CommunityId = communityId }, trans);

                                if (ruleInfo.Count() == 0)
                                {
                                    Business.Alipay.Log("微信下账:计算赠送积分失败，相关积分抵用规则已被禁用");
                                    return;
                                }

                                // 确定物管费可抵用金额
                                if (propertyAmount > 0)
                                {
                                    string key = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.PropertyFee);
                                    foreach (var item in ruleInfo)
                                    {
                                        if (item.DeductionObject == key && propertyAmount >= item.ConditionAmount)
                                        {
                                            propertyMaxDiscountsAmount = item.DiscountsAmount;
                                        }
                                    }
                                }

                                // 确定车位费可抵用金额
                                if (parkingAmount > 0)
                                {
                                    string key = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.ParkingFee);
                                    foreach (var item in ruleInfo)
                                    {
                                        if (item.DeductionObject == key && parkingAmount >= item.ConditionAmount)
                                        {
                                            parkingMaxDiscountsAmount = item.DiscountsAmount;
                                        }
                                    }
                                }

                                // 积分数量不正常
                                if (deductionAmount > (parkingMaxDiscountsAmount + propertyMaxDiscountsAmount))
                                {
                                    Business.Alipay.Log("微信下账:计算赠送积分失败，积分实际抵用金额超出可抵用金额");
                                    return;
                                }
                                else
                                {
                                    decimal tmp = deductionAmount;

                                    // 部分抵扣物业费
                                    if (propertyMaxDiscountsAmount != 0)
                                    {
                                        if (tmp <= propertyMaxDiscountsAmount)
                                        {
                                            propertyMaxDiscountsAmount = tmp;
                                            tmp = 0;
                                        }
                                        else
                                        {
                                            tmp -= propertyMaxDiscountsAmount;
                                        }
                                    }

                                    // 部分抵扣车位费
                                    if (parkingMaxDiscountsAmount != 0 && tmp > 0)
                                    {
                                        if (tmp <= parkingMaxDiscountsAmount)
                                        {
                                            parkingMaxDiscountsAmount = tmp;
                                            tmp = 0;
                                        }
                                        else
                                        {
                                            tmp -= parkingMaxDiscountsAmount;
                                        }
                                    }

                                    if (tmp != 0)
                                    {
                                        Business.Alipay.Log("微信下账:计算赠送积分失败，积分实际抵用金额与可抵用金额不相等，可能是更改了积分抵用规则");
                                        return;
                                    }
                                }
                            }
                        }

                        // 计算要赠送的积分数量
                        new AppPoint().CalcPresentedPointForPropertyFees(communityId, propertyAmount - propertyMaxDiscountsAmount, parkingAmount - parkingMaxDiscountsAmount, out int p1, out int p2);

                        if (p1 == 0 && p2 == 0)
                        {
                            return;
                        }

                        int presentedPoints = p1 + p2;

                        var userPoint = appConn.Query("SELECT * FROM Tb_App_UserPoint WHERE UserID=@UserID", new { UserID = userId }, trans).FirstOrDefault();
                        int balance = 0;

                        if (userPoint == null)
                        {
                            balance = 0;
                            appConn.Execute(@"INSERT INTO Tb_App_UserPoint(UserID, PointBalance) VALUES(@UserID, @PointBalance)",
                            new { UserID = userId, PointBalance = presentedPoints }, trans);
                        }
                        else
                        {
                            balance = userPoint.PointBalance;
                            appConn.Execute("UPDATE Tb_App_UserPoint SET PointBalance=(PointBalance+@PresentedPoints) WHERE UserID=@UserID",
                                new
                                {
                                    PresentedPoints = presentedPoints,
                                    UserID = userId
                                }, trans);
                        }

                        // 赠送历史
                        appConn.Execute(@"INSERT INTO Tb_App_Point_PresentedHistory(UserID, PresentedWay, PresentedPoints, PointBalance, Remark) 
                                        VALUES(@UserID, @PresentedWay, @PresentedPoints, @PointBalance, @Remark)",
                            new
                            {
                                UserID = userId,
                                PresentedWay = AppPointPresentedWayConverter.GetKey(AppPointPresentedWay.PropertyArrearsPayment),
                                PresentedPoints = presentedPoints,
                                PointBalance = balance + presentedPoints,
                                Remark = AppPointPresentedWayConverter.GetValue(AppPointPresentedWay.PropertyArrearsPayment),
                            }, trans);

                        Business.Alipay.Log("微信下账:赠送积分=" + presentedPoints);

                        // 力帆，缴清，额外赠送
                        if (corpId == 2015)
                        {
                            // 查询CustID、RoomID
                            var custInfo = erpConn.Query(@"SELECT CustID,RoomID FROM Tb_HSPR_Fees WHERE FeesID=
                                                              (SELECT TOP 1 FeesId FROM Tb_OL_WeiXinPayDetail WHERE PayOrderId IN
                                                                (SELECT Id FROM Tb_OL_WeiXinPayOrder WHERE out_trade_no=@out_trade_no))",
                                                                new { out_trade_no = orderId }).FirstOrDefault();

                            if (custInfo != null && custInfo.CustID != 0 && custInfo.RoomID != 0)
                            {
                                // 无欠费
                                if (erpConn.Query(@"SELECT * FROM Tb_HSPR_Fees WHERE CustID=@CustID AND RoomID=@RoomID AND isnull(IsCharge,0)=0",
                                    new { CustID = custInfo.CustID, RoomID = custInfo.RoomID }).Count() == 0)
                                {
                                    int extraPoints = new AppPoint().CalcPresentedPointForPayAll();
                                    if (extraPoints > 0)
                                    {
                                        presentedPoints += extraPoints;

                                        // 赠送积分
                                        appConn.Execute(@"UPDATE Tb_App_UserPoint SET PointBalance=(PointBalance+@PresentedPoints) WHERE UserID=@UserID;
                                                      INSERT INTO Tb_App_Point_PresentedHistory(UserID, PresentedWay, PresentedPoints, PointBalance, Remark) 
                                                      VALUES(@UserID, @PresentedWay, @PresentedPoints, @PointBalance, @Remark);",
                                                        new
                                                        {
                                                            UserID = userId,
                                                            PresentedWay = AppPointPresentedWayConverter.GetKey(AppPointPresentedWay.PropertyArrearsPayment),
                                                            PresentedPoints = extraPoints,
                                                            PointBalance = balance + presentedPoints,
                                                            Remark = "物业欠费缴清赠送",
                                                        }, trans);
                                    }
                                }
                            }
                        }
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Business.Alipay.Log("微信下账:计算赠送积分异常，" + ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
        }
    }
}