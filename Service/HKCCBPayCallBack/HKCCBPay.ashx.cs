using Business;
using Dapper;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Service.HKCCBPayCallBack
{
    /// <summary>
    /// HKCCBPayCallBack 的摘要说明
    /// </summary>
    public class HKCCBPay : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest Request = context.Request;
            HttpResponse Response = context.Response;
            Response.ContentType = "text/plain";
            DateTime DateNow = DateTime.Now;
            try
            {
                if (!"POST".Equals(Request.HttpMethod.ToUpper()))
                {
                    PubInfo.GetLog().Info("不是鸿坤建行收款通知");
                    return;
                }
                Dictionary<string, string> param = new Dictionary<string, string>();
                foreach (var item in Request.QueryString.AllKeys)
                {
                    param.Add(item, Request.QueryString[item]);
                }
                PubInfo.GetLog().Info("收到鸿坤建行收款通知:" + JsonConvert.SerializeObject(param));
                if (!"Y".Equals(param["SUCCESS"].ToUpper()))
                {
                    PubInfo.GetLog().Info("鸿坤建行收款通知内容:交易状态不是成功状态");
                    return;
                }
                // 通过OrderID订单号查询对应订单
                using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
                {
                    dynamic OrderInfo = conn.QueryFirstOrDefault("SELECT * FROM Tb_CCBPay_Order WHERE OrderSN = @OrderSN", new { OrderSN = param["ORDERID"] });
                    if (null == OrderInfo)
                    {
                        PubInfo.GetLog().Info("鸿坤建行收款通知内容:订单不存在");
                        return;
                    }
                    if (3 == Convert.ToInt32(OrderInfo.IsSucc))
                    {
                        PubInfo.GetLog().Info("鸿坤建行收款通知内容:订单已下账");
                        return;
                    }

                    // 获取支付配置信息
                    dynamic PayConfig = conn.QueryFirstOrDefault("SELECT * FROM Tb_CCBPay_Config WHERE Id = @Id", new { Id = OrderInfo.PayConfigId });
                    if (null == PayConfig)
                    {
                        PubInfo.GetLog().Info("鸿坤建行收款通知内容:支付配置不存在");
                        Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, param["ORDERID"], null, 0, JsonConvert.SerializeObject(param), 2, "支付配置不存在");
                        return;
                    }
                    #region 需要进行验签
                    Dictionary<string, string> tempDic = new Dictionary<string, string>(param);
                    tempDic.Remove("SIGN");
                    string initStr = "";
                    foreach (var item in tempDic)
                    {
                        initStr += string.Format("{0}={1}&", item.Key, item.Value);
                    }
                    initStr = initStr.Trim('&');
                    string sign = param["SIGN"];
                    string pubKey = Convert.ToString(PayConfig.Pub);
                    if (!Business.HKCCBPay.VerifySign(initStr, sign, pubKey)) 
                    {
                        PubInfo.GetLog().Info("鸿坤建行收款通知内容:签名校验失败");
                        return;
                    }
                    #endregion
                    // 获取小区信息
                    Tb_Community tb_Community = PubInfo.GetCommunity(Convert.ToString(PayConfig.CommunityId));
                    if (null == tb_Community)
                    {
                        PubInfo.GetLog().Info("鸿坤建行收款通知内容:小区配置不存在");
                        Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, param["ORDERID"], null, 0, JsonConvert.SerializeObject(param), 2, "小区配置不存在");
                        return;
                    }
                    decimal SAmt = Convert.ToDecimal(param["PAYMENT"]);
                    // 判断金额是否一致
                    if (Convert.ToDecimal(OrderInfo.Amt) != SAmt)
                    {
                        PubInfo.GetLog().InfoFormat("鸿坤建行收款通知内容:账单金额与实收金额不一致(SAmt={0})", SAmt);
                        Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, param["ORDERID"], null, 0, JsonConvert.SerializeObject(param), 2, "账单金额与实收金额不一致");
                        return;
                    }

                    string erpConnStr = PubInfo.GetConnectionStr(tb_Community);

                    JObject PayData = JsonConvert.DeserializeObject<JObject>(Convert.ToString(OrderInfo.PayData));
                    
                    int Type = (int)PayData["Type"];
                    if (Type == 1)
                    {
                        JArray Data = (JArray)PayData["Data"];
                        if (null == Data || Data.Count == 0)
                        {
                            PubInfo.GetLog().InfoFormat("鸿坤建行收款通知内容:下账失败(订单支付信息有误)(PayData={0})", OrderInfo.PayData);
                            Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, param["ORDERID"], DateNow.ToString(), SAmt, JsonConvert.SerializeObject(param), 2, "下账失败(订单支付信息有误)");
                            return;
                        }
                        StringBuilder FeesIds = new StringBuilder();
                        foreach (JObject item in Data)
                        {
                            FeesIds.Append((string)item["FeesId"] + ",");
                        }
                        if (PubInfo.ReceFees(erpConnStr, out long ReceID, Convert.ToString(tb_Community.CommID), Convert.ToString(OrderInfo.CustID), Convert.ToString(OrderInfo.RoomID), FeesIds.ToString(), 0.00M, "自助缴费-微信"))
                        {
                            PubInfo.GetLog().InfoFormat("鸿坤建行收款通知内容:下账成功(PayData={0})", OrderInfo.PayData);

                            Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, param["ORDERID"], DateNow.ToString().ToString(), SAmt, JsonConvert.SerializeObject(param), 3, "下账成功");
                            return;
                        }
                        else
                        {
                            PubInfo.GetLog().InfoFormat("鸿坤建行收款通知内容:下账失败(订单支付信息有误)(PayData={0})", OrderInfo.PayData);
                            Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, param["ORDERID"], DateNow.ToString().ToString(), SAmt, JsonConvert.SerializeObject(param), 4, "下账失败");
                            return;
                        }

                    }
                    else if (Type == 2)
                    {
                        JObject Data = (JObject)PayData["Data"];
                        if (null == Data)
                        {
                            PubInfo.GetLog().InfoFormat("鸿坤建行收款通知内容:下账失败(订单支付信息有误)(PayData={0})", OrderInfo.PayData);
                            Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, param["ORDERID"], DateNow.ToString(), SAmt, JsonConvert.SerializeObject(param), 2, "支付金额必须大于0");
                            return;
                        }
                        string CostID = (string)Data["CostID"];

                        decimal Amt = (Decimal)Data["Amt"];
                        if (PubInfo.RecePreFees(erpConnStr, out long ReceID, Convert.ToString(tb_Community.CommID), Convert.ToString(OrderInfo.CustID), Convert.ToString(OrderInfo.RoomID), CostID, Amt, "自助缴费-微信"))
                        {
                            PubInfo.GetLog().InfoFormat("鸿坤建行收款通知内容:下账成功(PayData={0})", OrderInfo.PayData);
                            Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, param["ORDERID"], DateNow.ToString(), SAmt, JsonConvert.SerializeObject(param), 3, "下账成功");
                            return;
                        }
                        else
                        {
                            PubInfo.GetLog().InfoFormat("鸿坤建行收款通知内容:下账失败(订单支付信息有误)(PayData={0})", OrderInfo.PayData);
                            Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, param["ORDERID"], DateNow.ToString(), SAmt, JsonConvert.SerializeObject(param), 4, "下账失败");
                            return;
                        }
                    }
                    else
                    {
                        PubInfo.GetLog().InfoFormat("鸿坤建行收款通知内容:下账失败(订单支付信息有误)(PayData={0})", OrderInfo.PayData);
                        Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, param["ORDERID"], DateNow.ToString(), SAmt, JsonConvert.SerializeObject(param), 2, "下账失败(订单支付信息有误)");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                PubInfo.GetLog().Error(ex);
                return;
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