using allinpay.utils;
using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Model.支付配置模型;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using swiftpass.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Business.PMS10.业主App.缴费.通联
{
    public class PMSFeesAllinpayManage
    {
        public string GenerateOrder(DataRow row)
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
            if (!row.Table.Columns.Contains("PayChannel") || string.IsNullOrEmpty(row["PayChannel"].ToString()))
            {
                return new ApiResult(false, "参数PayChannel有误").toJson();
            }

            var payChannel = row["PayChannel"].ToString();
            var payType = 0;
            if (payChannel.ToLower() == "allinpay_alipay")
            {
                payChannel = "A03";
                payType = 1;
            }
            else if (payChannel.ToLower() == "allinpay_wechatpay")
            {
                payChannel = "W06";
                payType = 2;
            }
            else
                return new ApiResult(false, "参数PayChannel有误").toJson();

            #endregion

            var community = PubInfo.GetCommunity(CommunityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }

            int CommID = AppGlobal.StrToInt(community.CommID);
            PubConstant.hmWyglConnectionString = PubInfo.GetConnectionStr(community);

            // 获取对应支付配置
            AllinConfig allinConfig;
            using (IDbConnection erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dynamic tb_Payment_Config = erpConn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID", new { CommID });
                if (null == tb_Payment_Config)
                {
                    return new ApiResult(false, "该项目未开通对应支付方式").toJson();
                }
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

                #region 计算金额
                if (!row.Table.Columns.Contains("PayData") || string.IsNullOrEmpty(row["PayData"].ToString()))
                {
                    return new ApiResult(false, "缺少参数PayData").toJson();
                }
                string PayData = row["PayData"].ToString();
                if (!PubInfo.CheckPayData(Global_Fun.BurstConnectionString(CommID, Global_Fun.BURST_TYPE_CHARGE), Convert.ToInt64(CustID), Convert.ToInt64(RoomID), PayData, out decimal Amt, out string errMsg, true, false, !"1940".Equals(Global_Var.LoginCorpID)))
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
                #region 修改收款方式
                if (erpConn.QueryFirstOrDefault<int>("SELECT COUNT(1) FROM syscolumns WHERE id=object_id('Tb_Notice') AND name = 'ChargeMode'") > 0)
                {
                    erpConn.Execute("UPDATE Tb_Notice SET ChargeMode = @ChargeMode WHERE Id = @Id", new { ChargeMode, Id = NoticeId });
                }
                #endregion
                #region 请求通联微信支付
                DateTime dateNow = DateTime.Now;
                string OrderSN = dateNow.ToString("yyyyMMddHHmmssfff") + Utils.BuildRandomStr(3);

                #region 获取对应类型的下账地址
                string PaymentNotifyUrl = string.Empty;
                if (Type == 1)
                {
                    PaymentNotifyUrl = AppGlobal.GetAppSetting("AllinPay_Notify_Url") + "?CommID=" + CommID;
                }
                else
                {
                    PaymentNotifyUrl = AppGlobal.GetAppSetting("AllinPay_Prec_Notify_Url") + "?CommID=" + CommID;
                }
                #endregion

                Dictionary<string, string> param;
                try
                {
                    param = SybWxPayService.Pay(Convert.ToInt64(Amt * 100), OrderSN, payChannel, FeesMemo, RoomSign, "", "", PaymentNotifyUrl, "", "", "", "", allinConfig.orgid, allinConfig.appid, allinConfig.custid, allinConfig.appkey, allinConfig.subbranch);

                    if (param == null)
                    {
                        return new ApiResult(false, "生成支付订单失败,请重试").toJson();
                    }
                }
#pragma warning disable CS0168 // 声明了变量“ex”，但从未使用过
                catch (Exception ex)
#pragma warning restore CS0168 // 声明了变量“ex”，但从未使用过
                {
                    return new ApiResult(false, "生成支付订单失败,请重试").toJson();
                }
                #endregion
                if (erpConn.Execute(@"INSERT INTO Tb_Payment_Order(PayType, OrderSN, NoticeId, Amt, CreateTime) 
                                            VALUES(@PayType, @OrderSN, @NoticeId, @Amt, @CreateTime)",
                                        new { PayType = payType, OrderSN = OrderSN, NoticeId = NoticeId, Amt = Amt, CreateTime = dateNow }) <= 0)
                {
                    return new ApiResult(false, "生成支付订单失败,请重试(1003)").toJson();
                }
                return new ApiResult(true, new { OrderSN = OrderSN, QrCode = param["payinfo"].ToString() }).toJson();
            }
        }

        public string QueryOrderStatus(DataRow row)
        {
            #region 获取基本参数
            string OrderSN = string.Empty;
            if (row.Table.Columns.Contains("OrderSN"))
            {
                OrderSN = row["OrderSN"].ToString();
            }
            #endregion
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dynamic OrderInfo = conn.QueryFirstOrDefault("SELECT OrderSN, IsSucc, Memo FROM Tb_Payment_Order WITH(NOLOCK) WHERE OrderSN = @OrderSN", new { OrderSN });
                if (null == OrderInfo)
                {
                    return new ApiResult(false, "支付订单不存在").toJson();
                }
                if (Convert.ToInt32(OrderInfo.IsSucc) == 0)
                {
                    return new ApiResult(false, "暂未下账").toJson();
                }
                string Memo = Convert.ToString(OrderInfo.Memo);
                if (!long.TryParse(Memo, out long ReceID))
                {
                    return new ApiResult(true, Memo).ToString();
                }
                return new ApiResult(true, "下账成功").ToString();
            }
        }
    }
}
