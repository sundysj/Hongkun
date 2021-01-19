using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WxPayAPI;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using MobileSoft.DBUtility;
using Dapper;
using Business;
using MobileSoft.Model.Unified;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Service.WeiXinPayCallBack
{
    /// <summary>
    /// WeiXinPay_New 的摘要说明
    /// </summary>
    public class WeiXinPay_New : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                DateTime DateNow = DateTime.Now;
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
                //string CommunityId = "";
                string amt = "";

                //返回报文中不包含UPOG,表示Server端正确接收交易请求,则需要验证Server端返回报文的签名
                bool IsValidate = false;
                WxPayData WxPostData = new WxPayData();
                string Result = Notify.NotifyDataFromContext(context, ref IsValidate, ref WxPostData);

                respcode = WxPostData.GetValue("result_code").ToString();
                respmsg = WxPostData.GetValue("result_code").ToString();
                orderId = WxPostData.GetValue("out_trade_no").ToString();
                //CommunityId = WxPostData.GetValue("attach").ToString();
                amt = WxPostData.GetValue("total_fee").ToString();
                //string userId = null;
                //if (CommunityId.Contains(","))
                //{
                //    userId = CommunityId.Split(',')[1];
                //    CommunityId = CommunityId.Split(',')[0];
                //}

                if (IsValidate == false)
                {
                    //Business.WeiXinPay.Log("验签失败:" + CommunityId + "," + orderId.ToString());
                    Business.WeiXinPay.Log("验签失败:" + orderId.ToString());
                    Result = SetNotifyResult("FAIL", Result);
                    context.Response.Write(Result);
                    return;
                }

                Business.WeiXinPay.Log("微信支付验签成功:" + orderId.ToString());

                if (IsValidate == true)
                {
                    // 通过OrderID订单号查询对应订单
                    using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
                    {
                        dynamic OrderInfo = conn.QueryFirstOrDefault("SELECT * FROM Tb_CCBPay_Order WHERE OrderSN = @OrderSN", new { OrderSN = orderId });
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
                        dynamic PayConfig = conn.QueryFirstOrDefault("SELECT * FROM Tb_WeiXinPay_Config WHERE Id = @Id", new { Id = OrderInfo.PayConfigNewId });
                        if (null == PayConfig)
                        {
                            PubInfo.GetLog().Info("鸿坤建行收款通知内容:支付配置不存在");
                            Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, null, 0, JsonConvert.SerializeObject(WxPostData), 2, "支付配置不存在");
                            return;
                        }

                        // 获取小区信息
                        Tb_Community tb_Community = PubInfo.GetCommunity(Convert.ToString(PayConfig.CommunityId));
                        if (null == tb_Community)
                        {
                            PubInfo.GetLog().Info("鸿坤建行收款通知内容:小区配置不存在");
                            Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, null, 0, JsonConvert.SerializeObject(WxPostData), 2, "小区配置不存在");
                            return;
                        }
                        decimal SAmt = Convert.ToDecimal(amt) / 100;
                        // 判断金额是否一致
                        if (Convert.ToDecimal(OrderInfo.Amt) != SAmt)
                        {
                            PubInfo.GetLog().InfoFormat("鸿坤建行收款通知内容:账单金额与实收金额不一致(SAmt={0})", SAmt);
                            Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, null, 0, JsonConvert.SerializeObject(WxPostData), 2, "账单金额与实收金额不一致");
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
                                Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, DateNow.ToString(), SAmt, JsonConvert.SerializeObject(WxPostData), 2, "下账失败(订单支付信息有误)");
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

                                Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, DateNow.ToString().ToString(), SAmt, JsonConvert.SerializeObject(WxPostData), 3, "下账成功");
                                return;
                            }
                            else
                            {
                                PubInfo.GetLog().InfoFormat("鸿坤建行收款通知内容:下账失败(订单支付信息有误)(PayData={0})", OrderInfo.PayData);
                                Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, DateNow.ToString().ToString(), SAmt, JsonConvert.SerializeObject(WxPostData), 4, "下账失败");
                                return;
                            }

                        }
                        else if (Type == 2)
                        {
                            JObject Data = (JObject)PayData["Data"];
                            if (null == Data)
                            {
                                PubInfo.GetLog().InfoFormat("鸿坤建行收款通知内容:下账失败(订单支付信息有误)(PayData={0})", OrderInfo.PayData);
                                Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, DateNow.ToString(), SAmt, JsonConvert.SerializeObject(WxPostData), 2, "支付金额必须大于0");
                                return;
                            }
                            string CostID = (string)Data["CostID"];

                            decimal Amt = (Decimal)Data["Amt"];
                            if (PubInfo.RecePreFees(erpConnStr, out long ReceID, Convert.ToString(tb_Community.CommID), Convert.ToString(OrderInfo.CustID), Convert.ToString(OrderInfo.RoomID), CostID, Amt, "自助缴费-微信"))
                            {
                                PubInfo.GetLog().InfoFormat("鸿坤建行收款通知内容:下账成功(PayData={0})", OrderInfo.PayData);
                                Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, DateNow.ToString(), SAmt, JsonConvert.SerializeObject(WxPostData), 3, "下账成功");
                                return;
                            }
                            else
                            {
                                PubInfo.GetLog().InfoFormat("鸿坤建行收款通知内容:下账失败(订单支付信息有误)(PayData={0})", OrderInfo.PayData);
                                Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, DateNow.ToString(), SAmt, JsonConvert.SerializeObject(WxPostData), 4, "下账失败");
                                return;
                            }
                        }
                        else
                        {
                            PubInfo.GetLog().InfoFormat("鸿坤建行收款通知内容:下账失败(订单支付信息有误)(PayData={0})", OrderInfo.PayData);
                            Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, DateNow.ToString(), SAmt, JsonConvert.SerializeObject(WxPostData), 2, "下账失败(订单支付信息有误)");
                            return;
                        }
                    }
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
    }
}