using allinpay.utils;
using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Model.支付配置模型;
using Model.支付配置模型.华宇通联;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using swiftpass.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
//using WxPayAPI;
using wxpay.utils;


namespace Business
{
    public class PMSAppPay : PubInfo
    {
        public PMSAppPay()
        {
            base.Token = "20191210PMSAppPay";
        }

        public override void Operate(ref Transfer Trans)
        {
            try
            {
                DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];

                switch (Trans.Command)
                {

                    #region 缴费相关
                    case "OnPay":
                        Trans.Result = OnPay(Row);
                        break;
                    case "QueryOrderStatus":
                        Trans.Result = QueryOrderStatus(Row);
                        break;
                    case "ScanPay":
                        Trans.Result = ScanPay(Row);
                        break;
                    case "QueryOrder":
                        Trans.Result = QueryOrder(Row);
                        break;
                    #endregion

                    default:
                        Trans.Result = new ApiResult(false, "接口不存在").toJson();
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                Trans.Result = new ApiResult(false, ex.Message + Environment.CommandLine + ex.StackTrace).toJson();
            }
        }

        private string QueryOrder(DataRow row)
        {
            #region 获取基本参数
            string OrderSN = string.Empty;
            if (row.Table.Columns.Contains("OrderSN"))
            {
                OrderSN = row["OrderSN"].ToString();
            }
            string CommunityId = string.Empty;
            if (row.Table.Columns.Contains("CommunityId"))
            {
                CommunityId = row["CommunityId"].ToString();
            }
            var community = GetCommunity(CommunityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }

            int CommID = AppGlobal.StrToInt(community.CommID);
            PubConstant.hmWyglConnectionString = GetConnectionStr(community);
            #endregion
            #region 查询数据库订单信息
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dynamic OrderInfo = conn.QueryFirstOrDefault("SELECT * FROM Tb_Payment_Order WITH(NOLOCK) WHERE OrderSN = @OrderSN", new { OrderSN });
                if (null == OrderInfo)
                {
                    return new ApiResult(false, "支付订单不存在").toJson();
                }
                dynamic tbNotice = conn.QueryFirstOrDefault("SELECT * FROM Tb_Notice WITH(NOLOCK) WHERE Id = @Id", new { Id = OrderInfo.NoticeId });
                if (null == tbNotice)
                {
                    return new ApiResult(false, "交易订单不存在").toJson();
                }
                CommID = Convert.ToString(tbNotice.CommID);
            }
            #region 获取对应支付配置
            AllinConfig allinConfig;
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dynamic tb_Payment_Config = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID", new { CommID });
                if (null == tb_Payment_Config)
                {
                    return new ApiResult(false, "该项目未开通对应支付方式,请联系管理员").toJson();
                }
                try
                {
                    allinConfig = JsonConvert.DeserializeObject<AllinConfig>(tb_Payment_Config.Config);
                    if (null == allinConfig)
                    {
                        return new ApiResult(false, "该项目支付类型对应配置有误,请联系管理员").toJson();
                    }
                }
                catch (Exception)
                {
                    return new ApiResult(false, "该项目支付类型对应配置有误,请联系管理员").toJson();
                }
            }
            #endregion
            #endregion
            #region 请求通联查询
            Dictionary<string, string> param;
            try
            {
                param = SybWxPayService.Query(OrderSN, "", allinConfig.orgid, allinConfig.appid, allinConfig.custid, allinConfig.appkey);
                return new ApiResult(true, param).toJson();
            }
            catch (Exception ex)
            {
                GetLog().Error("OnPay", ex);
                return new ApiResult(false, ex.Message).toJson();
            }
            #endregion
        }
        private string ScanPay(DataRow row)
        {

            #region 获取基本参数
            string CommID = string.Empty;
            if (row.Table.Columns.Contains("CommID"))
            {
                CommID = row["CommID"].ToString();
            }
            string RoomID = string.Empty;
            if (row.Table.Columns.Contains("RoomID"))
            {
                RoomID = row["RoomID"].ToString();
            }
            string CustID = string.Empty;
            if (row.Table.Columns.Contains("CustID"))
            {
                CustID = row["CustID"].ToString();
            }
            string AuthCode = string.Empty;
            if (row.Table.Columns.Contains("AuthCode"))
            {
                AuthCode = row["AuthCode"].ToString();
            }
            #endregion

            #region 获取对应支付配置
            AllinConfig allinConfig;
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dynamic tb_Payment_Config = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID", new { CommID });
                if (null == tb_Payment_Config)
                {
                    return new ApiResult(false, "该项目未开通对应支付方式,请联系管理员").toJson();
                }
                try
                {
                    allinConfig = JsonConvert.DeserializeObject<AllinConfig>(tb_Payment_Config.Config);
                    if (null == allinConfig)
                    {
                        return new ApiResult(false, "该项目支付类型对应配置有误,请联系管理员").toJson();
                    }
                }
                catch (Exception)
                {
                    return new ApiResult(false, "该项目支付类型对应配置有误,请联系管理员").toJson();
                }
            }
            #endregion

            #region 计算金额
            if (!row.Table.Columns.Contains("PayData") || string.IsNullOrEmpty(row["PayData"].ToString()))
            {
                return new ApiResult(false, "缺少参数PayData").toJson();
            }
            string PayData = row["PayData"].ToString();
            if (!CheckPayData(Global_Fun.BurstConnectionString(AppGlobal.StrToInt(CommID), Global_Fun.BURST_TYPE_CHARGE), Convert.ToInt64(CustID), Convert.ToInt64(RoomID), PayData, out decimal Amt, out string errMsg, true, false, !"1940".Equals(Global_Var.LoginCorpID)))
            {
                return new ApiResult(false, errMsg).toJson();
            }
            if (Amt <= 0.00M)
            {
                return new ApiResult(false, "金额必须大于0").toJson();
            }
            #endregion

            JObject PayDataObj = JObject.Parse(PayData);
            int Type = (int)PayDataObj["Type"];
            #region 查询项目名称和房屋编号,拼接费用备注
            string FeesMemo = string.Empty;
            string RoomSign = string.Empty;
            if (Type == 1)
            {
                FeesMemo = "物业综合费用缴纳";
                using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    string CommName = conn.QueryFirstOrDefault<string>("SELECT CommName FROM Tb_HSPR_Community WHERE CommID = @CommID", new { CommID });
                    if (string.IsNullOrEmpty(CommName))
                    {
                        CommName = Convert.ToString(CommID);
                    }
                    RoomSign = conn.QueryFirstOrDefault<string>("SELECT ISNULL(RoomSign,RoomName) AS RoomSign FROM Tb_HSPR_Room WHERE RoomID = @RoomID", new { RoomID });
                    if (string.IsNullOrEmpty(RoomSign))
                    {
                        RoomSign = Convert.ToString(RoomID);
                    }

                    FeesMemo += string.Format("-{0}-{1}", CommName, RoomSign);
                }
            }
            else
            {
                using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    string CommName = conn.QueryFirstOrDefault<string>("SELECT CommName FROM Tb_HSPR_Community WHERE CommID = @CommID", new { CommID });
                    if (string.IsNullOrEmpty(CommName))
                    {
                        CommName = Convert.ToString(CommID);
                    }
                    RoomSign = conn.QueryFirstOrDefault<string>("SELECT ISNULL(RoomSign,RoomName) AS RoomSign FROM Tb_HSPR_Room WHERE RoomID = @RoomID", new { RoomID });
                    if (string.IsNullOrEmpty(RoomSign))
                    {
                        RoomSign = Convert.ToString(RoomID);
                    }

                    FeesMemo += string.Format("-{0}-{1}", CommName, RoomSign);
                }
            }
            #endregion

            #region 生成订单
            string NoticeId = Guid.NewGuid().ToString();
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                if (conn.Execute("INSERT INTO Tb_Notice(Id, CommID, RoomID, CustID, PayData, CreateTime) VALUES(@Id, @CommID, @RoomID, @CustID, @PayData, @CreateTime)", new { Id = NoticeId, CommID, RoomID, CustID, PayData, CreateTime = DateTime.Now.ToString() }) <= 0)
                {
                    return new ApiResult(false, "创建收款订单失败,请重试").toJson();
                }
                string ChargeMode = "前台扫码缴费";
                #region 修改收款方式
                if (conn.QueryFirstOrDefault<int>("SELECT COUNT(1) FROM syscolumns WHERE id=object_id('Tb_Notice') AND name = 'ChargeMode'") > 0)
                {
                    conn.Execute("UPDATE Tb_Notice SET ChargeMode = @ChargeMode WHERE Id = @Id", new { ChargeMode, Id = NoticeId });
                }
                #endregion
                DateTime dateNow = DateTime.Now;
                string OrderSN = dateNow.ToString("yyyyMMddHHmmssfff") + Utils.BuildRandomStr(3);

                if (conn.Execute("INSERT INTO Tb_Payment_Order(PayType, OrderSN, NoticeId, Amt, CreateTime) VALUES(@PayType, @OrderSN, @NoticeId, @Amt, @CreateTime)", new { PayType = 4, OrderSN = OrderSN, NoticeId = NoticeId, Amt = Amt, CreateTime = dateNow }) <= 0)
                {
                    return new ApiResult(false, "创建支付订单失败,请重试").toJson();
                }

                #region 请求通联微信支付
                Dictionary<string, string> param;
                try
                {
                    param = SybWxPayService.ScanPay(Convert.ToInt64(Amt * 100), OrderSN, FeesMemo, RoomSign, AuthCode, "", "", "", "", allinConfig.orgid, allinConfig.appid, allinConfig.custid, allinConfig.appkey);
                }
                catch (Exception ex)
                {
                    GetLog().Error("OnPay", ex);
                    return new ApiResult(false, ex.Message).toJson();
                }
                #endregion
                return new ApiResult(true, param).toJson();
            }
            #endregion
        }


        private string OnPay(DataRow row)
        {
            #region 获取基本参数
            string CommunityId = string.Empty;
            if (row.Table.Columns.Contains("CommunityId"))
            {
                CommunityId = row["CommunityId"].ToString();
            }
            string RoomID = string.Empty;
            if (row.Table.Columns.Contains("RoomID"))
            {
                RoomID = row["RoomID"].ToString();
            }
            string CustID = string.Empty;
            if (row.Table.Columns.Contains("CustID"))
            {
                CustID = row["CustID"].ToString();
            }
            string OpenID = string.Empty;
            if (row.Table.Columns.Contains("OpenID"))
            {
                OpenID = row["OpenID"].ToString();
            }
            if (!row.Table.Columns.Contains("PayChannel") || string.IsNullOrEmpty(row["PayChannel"].ToString()))
            {
                return new ApiResult(false, "参数PayChannel有误").toJson();
            }
            var community = GetCommunity(CommunityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }

            int CommID = AppGlobal.StrToInt(community.CommID);
            PubConstant.hmWyglConnectionString = GetConnectionStr(community);

            var payChannel = row["PayChannel"].ToString();
            var payType = 0;

            switch (payChannel.ToLower())
            {
                case PayChannelString.Alipay:
                    payType = 1;
                    break;
                case PayChannelString.WechatPay:
                    payType = 2;
                    break;
                case PayChannelString.AllInPay_Alipay:
                    payType = 1;
                    break;
                case PayChannelString.AllInPay_WechatPay:
                    payType = 2;
                    break;
                default:
                    return new ApiResult(false, "参数payChannel有误").toJson();
            }
            if(payType == 2)
            {
                if (payChannel.ToLower().Equals(PayChannelString.AllInPay_WechatPay) && string.IsNullOrEmpty(OpenID))
                {
                    return new ApiResult(false, "参数OpenID不能为空").toJson();
                }
            }
            
            #endregion

            using (IDbConnection erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dynamic tb_Payment_Config = erpConn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID", new { CommID });
                if (null == tb_Payment_Config)
                {
                    return new ApiResult(false, "该项目未开通对应支付方式").toJson();
                }
                // 旧方式获取对应支付配置
                AllinConfig allinConfig = null;
                // 新方式获取支付配置
                PaymentConfig paymentConfig = null;
                if (payChannel == PayChannelString.AllInPay_Alipay || payChannel == PayChannelString.AllInPay_WechatPay)
                {
                    try
                    {
                        allinConfig = JsonConvert.DeserializeObject<AllinConfig>(tb_Payment_Config.Config);
                        if (null == allinConfig)
                        {
                            return new ApiResult(false, "该项目支付类型对应配置有误").toJson();
                        }
                    }
                    catch (Exception)
                    {
                        return new ApiResult(false, "该项目支付类型对应配置有误").toJson();
                    }
                }
                else
                {
                    // 新的方式，Config存储多个配置
                    try
                    {
                        // ERP的配置表，要求存储一个Json数组，用于配置支持不同支付方式
                        // 配置项要求存储一个
                        List<PaymentConfig> configs = JsonConvert.DeserializeObject<List<PaymentConfig>>(tb_Payment_Config.Config);
                        if (null == configs || configs.Count <= 0)
                        {
                            return new ApiResult(false, "该项目支付类型对应配置有误").toJson();
                        }
                        if (payChannel == PayChannelString.Alipay)
                        {
                            paymentConfig = configs.Find(item => item.type == "AliPay");
                        }
                        if (payChannel == PayChannelString.WechatPay)
                        {
                            paymentConfig = configs.Find(item => item.type == "WChatPay");
                        }
                        if (null == paymentConfig)
                        {
                            return new ApiResult(false, "该项目支付类型对应配置有误").toJson();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                #region 计算金额
                if (!row.Table.Columns.Contains("PayData") || string.IsNullOrEmpty(row["PayData"].ToString()))
                {
                    return new ApiResult(false, "缺少参数PayData").toJson();
                }
                string PayData = row["PayData"].ToString();
                if (!CheckPayData(Global_Fun.BurstConnectionString(CommID, Global_Fun.BURST_TYPE_CHARGE), Convert.ToInt64(CustID), Convert.ToInt64(RoomID), PayData, out decimal Amt, out string errMsg, true, false, !"1940".Equals(Global_Var.LoginCorpID)))
                {
                    return new ApiResult(false, errMsg).toJson();
                }
                if (Amt <= 0.00M)
                {
                    return new ApiResult(false, "金额必须大于0").toJson();
                }
                #endregion

                JObject PayDataObj = JObject.Parse(PayData);
                int Type = (int)PayDataObj["Type"];

                #region 查询项目名称和房屋编号,拼接费用备注
                string FeesMemo = string.Empty;
                string RoomSign = string.Empty;
                if (Type == 1)
                {
                    FeesMemo = "物业综合费用缴纳";
                    string CommName = erpConn.QueryFirstOrDefault<string>("SELECT CommName FROM Tb_HSPR_Community WHERE CommID = @CommID", new { CommID });
                    if (string.IsNullOrEmpty(CommName))
                    {
                        CommName = Convert.ToString(CommID);
                    }
                    RoomSign = erpConn.QueryFirstOrDefault<string>("SELECT ISNULL(RoomSign,RoomName) AS RoomSign FROM Tb_HSPR_Room WHERE RoomID = @RoomID", new { RoomID });
                    if (string.IsNullOrEmpty(RoomSign))
                    {
                        RoomSign = Convert.ToString(RoomID);
                    }

                    FeesMemo += string.Format("-{0}-{1}", CommName, RoomSign);
                }
                else
                {
                    FeesMemo = "物业综合费用预存";
                    string CommName = erpConn.QueryFirstOrDefault<string>("SELECT CommName FROM Tb_HSPR_Community WHERE CommID = @CommID", new { CommID });
                    if (string.IsNullOrEmpty(CommName))
                    {
                        CommName = Convert.ToString(CommID);
                    }
                    RoomSign = erpConn.QueryFirstOrDefault<string>("SELECT ISNULL(RoomSign,RoomName) AS RoomSign FROM Tb_HSPR_Room WHERE RoomID = @RoomID", new { RoomID });
                    if (string.IsNullOrEmpty(RoomSign))
                    {
                        RoomSign = Convert.ToString(RoomID);
                    }

                    FeesMemo += string.Format("-{0}-{1}", CommName, RoomSign);
                }
                #endregion

                string NoticeId = Guid.NewGuid().ToString();

                // 生成订单
                if (erpConn.Execute("INSERT INTO Tb_Notice(Id, CommID, RoomID, CustID, PayData, CreateTime) VALUES(@Id, @CommID, @RoomID, @CustID, @PayData, @CreateTime)", new { Id = NoticeId, CommID, RoomID, CustID, PayData, CreateTime = DateTime.Now.ToString() }) <= 0)
                {
                    return new ApiResult(false, "创建收款订单失败,请重试").toJson();
                }
                string ChargeMode = "业主APP缴费";
                if(payChannel == PayChannelString.AllInPay_Alipay)
                {
                    ChargeMode = "通联_业主APP（支付宝）";
                }
                else if (payChannel == PayChannelString.AllInPay_WechatPay)
                {
                    ChargeMode = "通联_业主APP（微信）";
                }
                else
                {
                    ChargeMode = "通联_业主APP";
                }
                #region 修改收款方式
                if (erpConn.QueryFirstOrDefault<int>("SELECT COUNT(1) FROM syscolumns WHERE id=object_id('Tb_Notice') AND name = 'ChargeMode'") > 0)
                {
                    erpConn.Execute("UPDATE Tb_Notice SET ChargeMode = @ChargeMode WHERE Id = @Id", new { ChargeMode, Id = NoticeId });
                }
                #endregion
                DateTime dateNow = DateTime.Now;
                string OrderSN = dateNow.ToString("yyyyMMddHHmmssfff") + Utils.BuildRandomStr(3);
                string PaymentNotifyUrl = string.Empty;

                Dictionary<string, string> param = null;
                if (payChannel == PayChannelString.AllInPay_Alipay || payChannel == PayChannelString.AllInPay_WechatPay)
                {
                    #region 请求通联支付
                    #region 获取对应类型的下账地址
                    if (Type == 1)
                    {
                        PaymentNotifyUrl = AppGlobal.GetAppSetting("AllinPay_Notify_Url") + "?CommID=" + CommID;
                    }
                    else
                    {
                        PaymentNotifyUrl = AppGlobal.GetAppSetting("AllinPay_Prec_Notify_Url") + "?CommID=" + CommID;
                    }
                    #endregion
                    try
                    {
                        param = SybWxPayService.Pay(Convert.ToInt64(Amt * 100), OrderSN, payChannel == PayChannelString.AllInPay_Alipay ? "A01" : "W06", FeesMemo, RoomSign, OpenID, "", PaymentNotifyUrl, "", "", "", "", allinConfig.orgid, allinConfig.appid, allinConfig.custid, allinConfig.appkey, allinConfig.subbranch);

                        if (param == null || !param.ContainsKey("payinfo"))
                        {
                            GetLog().Error("OnPay:" + JsonConvert.SerializeObject(param));
                            return new ApiResult(false, "生成支付订单失败,请重试").toJson();
                        }
                    }
                    catch (Exception ex)
                    {
                        GetLog().Error("OnPay", ex);
                        return new ApiResult(false, "生成支付订单失败,请重试").toJson();
                    }
                    if (erpConn.Execute(@"INSERT INTO Tb_Payment_Order(PayType, OrderSN, NoticeId, Amt, CreateTime) 
                                            VALUES(@PayType, @OrderSN, @NoticeId, @Amt, @CreateTime)",
                                        new { PayType = payType, OrderSN = OrderSN, NoticeId = NoticeId, Amt = Amt, CreateTime = dateNow }) <= 0)
                    {

                        return new ApiResult(false, "生成支付订单失败,请重试(1003)").toJson();
                    }
                    return new ApiResult(true, new { OrderSN = OrderSN, QrCode = param["payinfo"].ToString() }).toJson();
                    #endregion
                }
                if (payChannel == PayChannelString.Alipay)
                {
                    AliConfig aliConfig = null;
                    try
                    {
                        aliConfig = Config.GetConfig<AliConfig>(paymentConfig.config);
                        if (null == aliConfig)
                        {
                            return new ApiResult(false, "该项目支付类型对应配置有误").toJson();
                        }
                    }
                    catch (Exception)
                    {
                        return new ApiResult(false, "该项目支付类型对应配置有误").toJson();
                    }
                    #region 请求支付宝官方支付
                    #region 获取对应类型的下账地址
                    PaymentNotifyUrl = AppGlobal.GetAppSetting("AliPay_Notify_Url");
                    #endregion
                    AlipayTradeAppPayResponse response = null;
                    try
                    {
                        JObject BizContent = new JObject();
                        //要求15分钟内支付
                        BizContent.Add("timeout_express", "15m");
                        BizContent.Add("total_amount", Amt);
                        BizContent.Add("body", FeesMemo);
                        BizContent.Add("subject", FeesMemo);
                        BizContent.Add("out_trade_no", OrderSN);
                        IAopClient client = new DefaultAopClient("https://openapi.alipay.com/gateway.do", aliConfig.appid, aliConfig.app_private_key, "json", "1.0", "RSA2", aliConfig.alipay_public_key, "UTF-8", false);
                        AlipayTradeAppPayRequest request = new AlipayTradeAppPayRequest
                        {
                            BizContent = JsonConvert.SerializeObject(BizContent),
                        };
                        request.SetNotifyUrl(PaymentNotifyUrl);
                        response = client.SdkExecute(request);
                    }
                    catch (Exception ex)
                    {
                        Log(ex.Message, "AliPayLogs\\");
                        GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                        return new ApiResult(false, "请求订单失败,请重试").toJson();
                    }
                    if (erpConn.Execute("INSERT INTO Tb_Payment_Order(PayType, OrderSN, NoticeId, Amt, CreateTime) VALUES(@PayType, @OrderSN, @NoticeId, @Amt, @CreateTime)", new { PayType = 1, OrderSN, NoticeId = NoticeId, Amt = Amt, CreateTime = dateNow }) <= 0)
                    {
                        return new ApiResult(false, "生成订单失败").toJson();
                    }
                    return new ApiResult(true, new { OrderSN = OrderSN, QrCode = response.Body }).toJson();
                    #endregion
                }
                if (payChannel == PayChannelString.WechatPay)
                {
                    WxConfig wxConfig = null;
                    try
                    {
                        wxConfig = Config.GetConfig<WxConfig>(paymentConfig.config);
                        if (null == wxConfig)
                        {
                            return new ApiResult(false, "该项目支付类型对应配置有误").toJson();
                        }
                    }
                    catch (Exception)
                    {
                        return new ApiResult(false, "该项目支付类型对应配置有误").toJson();
                    }

                    #region 请求微信官方支付
                    #region 获取对应类型的下账地址
                    PaymentNotifyUrl = AppGlobal.GetAppSetting("WxPay_Notify_Url");
                    #endregion
                    WxPayData wxPayData = new WxPayData();
                    wxPayData.SetValue("appid", wxConfig.appid);
                    wxPayData.SetValue("body", FeesMemo);
                    wxPayData.SetValue("mch_id", wxConfig.mch_id);
                    wxPayData.SetValue("nonce_str", WxPayApi.GenerateNonceStr());
                    wxPayData.SetValue("notify_url", PaymentNotifyUrl);
                    wxPayData.SetValue("out_trade_no", OrderSN);
                    wxPayData.SetValue("spbill_create_ip", "8.8.8.8");
                    wxPayData.SetValue("total_fee", Convert.ToInt32(Amt * 100));
                    wxPayData.SetValue("trade_type", "APP");
                    wxPayData.SetValue("sign_type", wxpay.utils.WxPayData.SIGN_TYPE_HMAC_SHA256);
                    wxPayData.SetValue("sign", wxPayData.MakeSign(wxConfig.appkey));
                    try
                    {
                        wxPayData = WxPayApi.UnifiedOrder(wxPayData);
                    }
                    catch (Exception)
                    {
                        return new ApiResult(false, "请求超时,请重试").toJson();
                    }
                    if (!wxPayData.IsSet("return_code") || !"SUCCESS".Equals(wxPayData.GetValue("return_code").ToString()))
                    {
                        return new ApiResult(false, "请求支付订单失败").toJson();
                    }
                    if (!wxPayData.IsSet("result_code") || !"SUCCESS".Equals(wxPayData.GetValue("result_code").ToString()))
                    {
                        return new ApiResult(false, "请求支付订单失败").toJson();
                    }
                    if (!wxPayData.IsSet("prepay_id"))
                    {
                        return new ApiResult(false, "请求支付订单失败").toJson();
                    }
                    string prepay_id = wxPayData.GetValue("prepay_id").ToString();
                    if (erpConn.Execute("INSERT INTO Tb_Payment_Order(PayType, OrderSN, NoticeId, Amt, CreateTime) VALUES(@PayType, @OrderSN, @NoticeId, @Amt, @CreateTime)", new { PayType = 2, OrderSN, NoticeId = NoticeId, Amt = Amt, CreateTime = dateNow }) <= 0)
                    {
                        return new ApiResult(false, "生成订单失败").toJson();
                    }
                    WxPayData result = new WxPayData();
                    result.SetValue("appid", wxPayData.GetValue("appid").ToString());
                    result.SetValue("partnerid", wxPayData.GetValue("mch_id").ToString());
                    result.SetValue("prepayid", prepay_id);
                    result.SetValue("package", "Sign=WXPay");
                    result.SetValue("noncestr", wxPayData.GetValue("nonce_str").ToString());
                    result.SetValue("timestamp", WxPayApi.GenerateTimeStamp());
                    result.SetValue("sign", result.MakeSign(wxpay.utils.WxPayData.SIGN_TYPE_HMAC_SHA256,wxConfig.appkey));
                    JObject jObj = JObject.Parse(result.ToJson());
                    return new ApiResult(true, new { OrderSN = OrderSN, QrCode = jObj }).toJson();
                    #endregion
                }
                return new ApiResult(false, "不支持的支付方式").toJson();
            }
        }

        private string QueryOrderStatus(DataRow row)
        {
            #region 获取基本参数
            string OrderSN = string.Empty;
            if (row.Table.Columns.Contains("OrderSN"))
            {
                OrderSN = row["OrderSN"].ToString();
            }
            if (string.IsNullOrEmpty(OrderSN))
            {
                return new ApiResult(true, "支付订单不存在").toJson();
            }
            string CommunityId = string.Empty;
            if (row.Table.Columns.Contains("CommunityId"))
            {
                CommunityId = row["CommunityId"].ToString();
            }
            var community = GetCommunity(CommunityId);
            if (community == null)
            {
                return JSONHelper.FromString(true, "未查询到小区信息");
            }

            int CommID = AppGlobal.StrToInt(community.CommID);
            PubConstant.hmWyglConnectionString = GetConnectionStr(community);
            #endregion
            #region 查询订单信息
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                Tb_Payment_Order tb_Payment_Order = conn.QueryFirstOrDefault<Tb_Payment_Order>("SELECT * FROM Tb_Payment_Order WITH(NOLOCK) WHERE OrderSN = @OrderSN", new { OrderSN });
                if (null == tb_Payment_Order)
                {
                    return new ApiResult(true, "支付订单不存在").toJson();
                }
                if (tb_Payment_Order.IsSucc == 5)
                {
                    return new ApiResult(true, "订单超时未支付").toJson();
                }
                if (tb_Payment_Order.IsSucc == 4)
                {
                    return new ApiResult(true, "下账失败,请联系物业核实").toJson();
                }
                if (tb_Payment_Order.IsSucc == 3)
                {
                    return new ApiResult(true, "下账成功").toJson();
                }
                if (tb_Payment_Order.IsSucc == 2)
                {
                    return new ApiResult(true, "下账失败,支付数据异常,请联系物业核实").toJson();
                }
                if (tb_Payment_Order.IsSucc == 1)
                {
                    return new ApiResult(true, "下账失败,支付数据异常,请联系物业核实").toJson();
                }
                // 未下账需要实时查一下状态
                if (tb_Payment_Order.IsSucc == 0)
                {
                    return new ApiResult(false, "暂未下账").toJson();
                }
                return new ApiResult(true, "支付状态异常").toJson();
            }

            #endregion
        }
    }
}