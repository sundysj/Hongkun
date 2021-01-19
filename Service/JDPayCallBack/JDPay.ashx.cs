using Business;
using Business.responseObj;
using Business.signature;
using Dapper;
using MobileSoft.Common;
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
using WxPayAPI;

namespace Service.JDPayCallBack
{
    /// <summary>
    /// JDPay 的摘要说明
    /// </summary>
    public class JDPay : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                //接收回调xml信息
                byte[] byts = new byte[context.Request.InputStream.Length];
                context.Request.InputStream.Read(byts, 0, byts.Length);
                string req = Encoding.UTF8.GetString(byts);

                PubInfo.GetLog().Info("鸿坤京东收款通接收内容:" + req);
                if (string.IsNullOrEmpty(req))
                {
                    throw new Exception("接口未获取到任何数据");
                }

                //初步解析接收内容，获取到商家编号
                var res = XMLUtil.decryptResXmlNew<JdPayResponse>(req);
              
                //根据商家编号获取配置信息
                WxPayConfig payConfig = GenerateConfig(res.merchant);
                if(payConfig == null)
                {
                    throw new Exception("根据商家编号没有找到配置信息");
                }
                //根据配置的密钥进行解析数据
                AsynNotifyResponse anyResponse = XMLUtil.decryptResXml<AsynNotifyResponse>(payConfig.APPID, payConfig.KEY, req);

                //判断订单是否成过
                if ("success".Equals(anyResponse.result.desc))
                {
                    DateTime DateNow = DateTime.Now;
                    string orderId = anyResponse.tradeNum;
                    string amt = anyResponse.amount.ToString();
                    // 通过OrderID订单号查询对应订单
                    using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
                    {
                        dynamic OrderInfo = conn.QueryFirstOrDefault("SELECT * FROM Tb_CCBPay_Order WHERE OrderSN = @OrderSN", new { OrderSN = orderId });
                        if (null == OrderInfo)
                        {
                            PubInfo.GetLog().Info("鸿坤京东收款通知内容:订单不存在");
                            return;
                        }
                        if (3 == Convert.ToInt32(OrderInfo.IsSucc))
                        {
                            PubInfo.GetLog().Info("鸿坤京东收款通知内容:订单已下账");
                            return;
                        }

                        // 获取支付配置信息
                        dynamic PayConfig = conn.QueryFirstOrDefault("SELECT * FROM Tb_JDPay_Config WHERE Id = @Id", new { Id = OrderInfo.PayConfigNewId });
                        if (null == PayConfig)
                        {
                            PubInfo.GetLog().Info("鸿坤京东收款通知内容:支付配置不存在");
                            Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, null, 0, req, 2, "支付配置不存在");
                            return;
                        }

                        // 获取小区信息
                        Tb_Community tb_Community = PubInfo.GetCommunity(Convert.ToString(PayConfig.CommunityId));
                        if (null == tb_Community)
                        {
                            PubInfo.GetLog().Info("鸿坤京东收款通知内容:小区配置不存在");
                            Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, null, 0, req, 2, "小区配置不存在");
                            return;
                        }
                        decimal SAmt = Convert.ToDecimal(amt) / 100;
                        // 判断金额是否一致
                        if (Convert.ToDecimal(OrderInfo.Amt) != SAmt)
                        {
                            PubInfo.GetLog().InfoFormat("鸿坤京东收款通知内容:账单金额与实收金额不一致(SAmt={0})", SAmt);
                            Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, null, 0, req, 2, "账单金额与实收金额不一致");
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
                                PubInfo.GetLog().InfoFormat("鸿坤京东收款通知内容:下账失败(订单支付信息有误)(PayData={0})", OrderInfo.PayData);
                                Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, DateNow.ToString(), SAmt, req, 2, "下账失败(订单支付信息有误)");
                                return;
                            }
                            StringBuilder FeesIds = new StringBuilder();
                            foreach (JObject item in Data)
                            {
                                FeesIds.Append((string)item["FeesId"] + ",");
                            }
                            if (PubInfo.ReceFees(erpConnStr, out long ReceID, Convert.ToString(tb_Community.CommID), Convert.ToString(OrderInfo.CustID), Convert.ToString(OrderInfo.RoomID), FeesIds.ToString(), 0.00M, "自助缴费-京东"))
                            {
                                PubInfo.GetLog().InfoFormat("鸿坤京东收款通知内容:下账成功(PayData={0})", OrderInfo.PayData);

                                Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, DateNow.ToString().ToString(), SAmt, req, 3, "下账成功");

                                return;
                            }
                            else
                            {
                                PubInfo.GetLog().InfoFormat("鸿坤京东收款通知内容:下账失败(订单支付信息有误)(PayData={0})", OrderInfo.PayData);
                                Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, DateNow.ToString().ToString(), SAmt, req, 4, "下账失败");
                                return;
                            }

                        }
                        else if (Type == 2)
                        {
                            JObject Data = (JObject)PayData["Data"];
                            if (null == Data)
                            {
                                PubInfo.GetLog().InfoFormat("鸿坤京东收款通知内容:下账失败(订单支付信息有误)(PayData={0})", OrderInfo.PayData);
                                Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, DateNow.ToString(), SAmt, req, 2, "支付金额必须大于0");
                                return;
                            }
                            string CostID = (string)Data["CostID"];

                            decimal Amt = (Decimal)Data["Amt"];
                            if (PubInfo.RecePreFees(erpConnStr, out long ReceID, Convert.ToString(tb_Community.CommID), Convert.ToString(OrderInfo.CustID), Convert.ToString(OrderInfo.RoomID), CostID, Amt, "自助缴费-京东"))
                            {
                                PubInfo.GetLog().InfoFormat("鸿坤京东收款通知内容:下账成功(PayData={0})", OrderInfo.PayData);
                                Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, DateNow.ToString(), SAmt, req, 3, "下账成功");
                                return;
                            }
                            else
                            {
                                PubInfo.GetLog().InfoFormat("鸿坤京东收款通知内容:下账失败(订单支付信息有误)(PayData={0})", OrderInfo.PayData);
                                Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, DateNow.ToString(), SAmt, req, 4, "下账失败");
                                return;
                            }
                        }
                        else
                        {
                            PubInfo.GetLog().InfoFormat("鸿坤京东收款通知内容:下账失败(订单支付信息有误)(PayData={0})", OrderInfo.PayData);
                            Business.HKCCBPay.UpdateOrderInfo(PubConstant.UnifiedContionString, orderId, DateNow.ToString(), SAmt, req, 2, "下账失败(订单支付信息有误)");
                            return;
                        }
                    }
                }
                else
                {
                    PubInfo.GetLog().InfoFormat("鸿坤京东收款通知内容:系统错误,接口返回支付失败。内容:{0}", anyResponse.result.code);
                }
            }
            catch(Exception ex)
            {
                PubInfo.GetLog().InfoFormat("鸿坤京东收款通知内容:系统错误:{0}",ex.Message);
            }
        }

        public WxPayConfig GenerateConfig(string mch_id)
        {
            WxPayConfig wxPayConfig = null;
            IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString.ToString());
            string query = "SELECT Id,mch_id,desKey as appkey,rsaPublicKey as appid,rsaPrivateKey as appsecret FROM Tb_JDPay_Config WHERE mch_id=@CommunityId";
            Tb_WeiXinPayCertificate T = conn.Query<Tb_WeiXinPayCertificate>(query, new { CommunityId = mch_id }).SingleOrDefault();
            if (T != null)
            {
                wxPayConfig = new WxPayConfig();
                wxPayConfig.ID = T.Id.ToString();
                wxPayConfig.APPID = T.appid.ToString();
                wxPayConfig.MCHID = T.mch_id.ToString();
                wxPayConfig.KEY = T.appkey.ToString();
                wxPayConfig.APPSECRET = T.appsecret.ToString();
                wxPayConfig.NOTIFY_URL = Global_Fun.AppWebSettings("JDPay_Notify_New_Url").ToString();
            }
            return wxPayConfig;
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