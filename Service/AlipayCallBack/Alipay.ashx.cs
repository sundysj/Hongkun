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
using System.Data.SqlClient;
using MobileSoft.DBUtility;
using System.Data;
using App.Model;
using Dapper;
using log4net;

namespace Service.AlipayCallBack
{
    /// <summary>
    /// Alipay 的摘要说明
    /// </summary>
    public class Alipay : IHttpHandler, IRequiresSessionState
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Alipay));
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                RecordClientInfo record = new RecordClientInfo();

                List<string> list = new List<string>();
                foreach (var item in context.Request.Params.AllKeys)
                {
                    list.Add($"{item}={context.Request.Params[item]}");
                }
                Business.Alipay.Log("收到支付宝通知" + context.Request.RawUrl + "?" + string.Join("&", list));

                DataTable dAttributeTable = new DataTable();
                dAttributeTable.Columns.Add("CustID", Type.GetType("System.String"));
                DataRow newRow;
                newRow = dAttributeTable.NewRow();
                newRow["CustID"] = "";
                dAttributeTable.Rows.Add(newRow);
                //record.RecordOperationLog(dAttributeTable.Rows[0], context.Request.RawUrl + "?" + string.Join("&", list), "Alipay", "支付宝回调");

                HttpRequest Request = context.Request;
                Dictionary<string, string> sPara = GetRequestPost(context);

                decimal total_amount = AppGlobal.StrToDec(Request.Params["total_fee"].ToString());
                string respcode = Request.Params["trade_status"];
                string respmsg = Request.Params["trade_status"].ToString();
                string orderId = Request.Params["out_trade_no"].ToString();
                string body = Request.Params["body"].ToString();
                string userId = null;
                if (body.Contains(","))
                {
                    userId = body.Split(',')[1];
                    body = body.Split(',')[0];
                }

                string sign = Request.Params["sign"];
                string notify_id = Request.Params["notify_id"];
                string communityId = body;

                Config c = new Config();
                //初始化参数，并返回配置信息
                bool IsConfig = new Business.Alipay().GenerateConfig(communityId, out c);
                if (IsConfig == false)
                {
                    throw new Exception("未配置证书文件");
                }

                Business.Alipay.Log("开始验证:" + communityId.ToString() + "," + orderId);

                string Result = "";
                //返回报文中不包含UPOG,表示Server端正确接收交易请求,则需要验证Server端返回报文的签名
                Notify aliNotify = new Notify(c);

                if (aliNotify.GetResponseTxt(notify_id) == "true")
                {
                    Business.Alipay.Log("验证:" + communityId.ToString() + "," + orderId + " GetResponseTxt 正确");

                    if (aliNotify.GetSignVeryfy(sPara, sign))
                    {
                        Business.Alipay.Log("验证:" + communityId.ToString() + "," + orderId + " GetSignVeryfy 正确");
                                                
                        //已收到请求，无需再发送通知
                        Result = "success";
                        if (respcode == "TRADE_SUCCESS")
                        {
                            //更新订单返回状态
                            Business.Alipay.UpdateProperyOrder(communityId, orderId, respcode, respmsg);

                            string useHistoryId = null;

                            if (!string.IsNullOrEmpty(userId))
                            {
                                // 查询是否使用了积分
                                //UsePoint(orderId, userId, out useHistoryId);
                            }

                            string ReceResult = Business.Alipay.ReceProperyOrder(total_amount, communityId, orderId, useHistoryId);

                            // 下账成功，赠送积分
                            if (ReceResult == "success")
                            {
                                if (!string.IsNullOrEmpty(userId))
                                {
                                   // PresentedPoint(communityId, orderId, userId, useHistoryId, total_amount);
                                }
                            }

                            Business.Alipay.Log("支付宝下账:" + communityId.ToString() + "," + orderId + "," + ReceResult);
                            log.Info("支付宝下账:" + communityId.ToString() + "," + orderId + "," + ReceResult);
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
                    //record.RecordOperationLog(dAttributeTable.Rows[0], context.Request.RawUrl + "?" + string.Join("&", list), "Alipay", "支付宝回调订单信息不匹配");
                }

                Business.Alipay.Log("支付宝流程:" + communityId.ToString() + "," + orderId + "," + Result);
                log.Info("支付宝流程:" + communityId.ToString() + "," + orderId + "," + Result);
                context.Response.ContentType = "text/plain";
                context.Response.Write(Result);
            }
            catch (Exception E)
            {
                Business.Alipay.Log(E.Message.ToString() + Environment.NewLine + E.StackTrace);
                log.Info("支付宝流程:" + E.Message.ToString() + Environment.NewLine + E.StackTrace);
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
                                            WHERE a.OrderID=@OrderID AND a.Payment='支付宝'",
                                            new
                                            {
                                                OrderID = orderId,
                                                UsableObject = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.PropertyFee)
                                            }, trans).FirstOrDefault();

                    useHistoryId = orderInfo?.UseHistoryID.ToString();

                    if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(useHistoryId))
                    {
                        // 积分生效
                        conn.Execute(@"UPDATE Tb_App_Point_UseHistory SET IsEffect=1 WHERE IID=@IID", new { IID = useHistoryId }, trans);

                        Business.Alipay.Log("支付宝下账:使用了积分，记录id=" + useHistoryId);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Business.Alipay.Log("支付宝下账:判断积分异常，" + ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 计算赠送积分
        /// </summary>
        private void PresentedPoint(string communityId, string orderId, string userId, string useHistoryId, decimal paidAmount)
        {
            using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                if (appConn.State == ConnectionState.Closed)
                {
                    appConn.Open();
                }

                var trans = appConn.BeginTransaction();

                short corpId = 0;
                try
                {
                    decimal propertyMaxDiscountsAmount = 0.0m;  // 物管费最大可抵用金额
                    decimal parkingMaxDiscountsAmount = 0.0m;   // 车位费最大可抵用金额

                    var deductionObject = new List<string>();   // 积分可抵扣对象

                    using (var erpConn = new SqlConnection(PubInfo.GetConnectionStr(PubInfo.GetCommunity(communityId))))
                    {
                        var feesIds = erpConn.Query<long>(@"SELECT FeesId FROM Tb_OL_AlipayDetail WHERE PayOrderId=
                                                            (SELECT Id FROM Tb_OL_AlipayOrder WHERE out_trade_no=@out_trade_no)",
                                                            new { out_trade_no = orderId });

                        if (feesIds.Count() == 0)
                        {
                            return;
                        }

                        #region 获取积分抵扣规则
                        // 企业编号
                        corpId = appConn.Query<short>("SELECT CorpID FROM Tb_Community WHERE Id=@CommunityId",
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
                                    Business.Alipay.Log("支付宝下账:计算赠送积分失败，相关积分抵用规则已被禁用");
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
                                    Business.Alipay.Log("支付宝下账:计算赠送积分失败，积分实际抵用金额超出可抵用金额");
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
                                        Business.Alipay.Log("支付宝下账:计算赠送积分失败，积分实际抵用金额与可抵用金额不相等，可能是更改了积分抵用规则");
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

                        Business.Alipay.Log("支付宝下账:赠送积分=" + presentedPoints);

                        // 力帆，缴清，额外赠送
                        if (corpId == 2015)
                        {
                            // 查询CustID、RoomID
                            var custInfo = erpConn.Query(@"SELECT CustID,RoomID FROM Tb_HSPR_Fees WHERE FeesID=
                                                              (SELECT TOP 1 FeesId FROM Tb_OL_AlipayDetail WHERE PayOrderId IN
                                                                (SELECT Id FROM Tb_OL_AlipayOrder WHERE out_trade_no=@out_trade_no))",
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
                    Business.Alipay.Log("支付宝下账:计算赠送积分异常，" + ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
        }
    }
}