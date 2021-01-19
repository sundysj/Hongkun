using App.Model;
using Dapper;
using log4net;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.OL;
using MobileSoft.Model.Unified;
using swiftpass.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WxPayAPI;

namespace Business
{
    public class WeiXinPay_New : PubInfo
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(WeiXinPay_New));
        /// <summary>
        /// openid用于调用统一下单接口
        /// </summary>
        //public string openid { get; set; }

        public WxPayData unifiedOrderResult { get; set; }

        public WeiXinPay_New() //获取小区、项目信息
        {
            base.Token = "20191029WeiXinPay";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = "false:";

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                //生成订单
                case "GenerateOrder":
                    Trans.Result = GenerateOrder(Row);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GenerateOrder(DataRow row)
        {
            #region 获取参数
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }
            string CommunityId = row["CommunityId"].ToString();

            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return new ApiResult(false, "缺少参数CustID").toJson();
            }
            long CustID = Convert.ToInt64(row["CustID"].ToString());
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return new ApiResult(false, "缺少参数RoomID").toJson();
            }
            long RoomID = Convert.ToInt64(row["RoomID"].ToString());

            if (!row.Table.Columns.Contains("PayData") || string.IsNullOrEmpty(row["PayData"].ToString()))
            {
                return new ApiResult(false, "缺少参数PayData").toJson();
            }
            string PayData = row["PayData"].ToString();
            if (!row.Table.Columns.Contains("PayType") || string.IsNullOrEmpty(row["PayType"].ToString()))
            {
                return new ApiResult(false, "缺少参数PayType").toJson();
            }
            // 默认为微信支付
            //if (!int.TryParse(row["PayType"].ToString(), out int PayType) || (PayType != 0 && PayType != 1))
            //{
                 //int PayType = 0;
            //}
            #endregion

            #region 验证小区是否存在
            Tb_Community tb_Community = GetCommunity(CommunityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "该项目未在运营系统中配置").toJson();
            }

            string erpConnStr = GetConnectionStr(tb_Community);
            #endregion

            #region 获取小区支付配置
            WxPayConfig payConfig = GenerateConfig(CommunityId);
            if (payConfig == null)
            {
                log.Error("支付配置有误");
                return new ApiResult(false, "支付配置有误").toJson();
            }
            #endregion

            #region 检测支付数据格式
            if (!CheckPayData(erpConnStr, CustID, RoomID, PayData, out decimal Amt, out string errMsg, true))
            {
                return new ApiResult(false, errMsg).toJson();
            }
            //Amt = decimal.Parse("0.01");
            decimal orderAmt = Amt;
            Amt= Amt * 100;            
            if (Amt <= 0.00M)
            {
                return new ApiResult(false, "订单已被支付或者支付金额小于0").toJson();
            }
            #endregion

            DateTime DateNow = DateTime.Now;

            string OrderSN = DateNow.ToString("yyyyMMddHHmmssfff") + GetRandomCode(3);

            string strBack = "";
            string strStart = DateTime.Now.ToString("yyyyMMddHHmmss");
            string strEnd = DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss");
            WxPayData data = new WxPayData();
            data.SetValue("body", "鸿坤瑞邦物业管理有限公司-物业缴费");
            data.SetValue("out_trade_no", OrderSN);
            data.SetValue("total_fee", ((int)Amt).ToString());
            data.SetValue("trade_type", "APP");
            data.SetValue("time_start", strStart);
            data.SetValue("time_expire", strEnd);
            data.SetValue("notify_url", payConfig.NOTIFY_URL);

            strBack = $"body=鸿坤瑞邦物业管理有限公司-物业缴费&out_trade_no={OrderSN}&total_fee={Amt}&notify_url={payConfig.NOTIFY_URL}&time_start={strStart}";

            WxPayData result = WxPayApi.UnifiedOrder(data, payConfig);
            if (!result.IsSet("appid") || !result.IsSet("prepay_id") || result.GetValue("prepay_id").ToString() == "")
            {
                log.Error("微信支付时调用服务器统一下单接口失败:");
                return new ApiResult(false, "微信支付失败,请联系客服人员").toJson();
            }
            else
            {
                #region 插入订单表
                using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("PayConfigId", 999999);
                    parameters.Add("OrderSN", OrderSN);
                    parameters.Add("CustID", CustID);
                    parameters.Add("RoomID", RoomID);
                    parameters.Add("PayData", PayData);
                    parameters.Add("Amt", orderAmt);
                    parameters.Add("CreateTime", DateNow.ToString());
                    parameters.Add("PayConfigNewId", payConfig.ID);
                    if (conn.Execute(@"INSERT INTO Tb_CCBPay_Order(PayConfigId, OrderSN, CustID, RoomID, PayData, Amt, CreateTime,PayConfigNewId) 
                                    VALUES(@PayConfigId, @OrderSN, @CustID, @RoomID, @PayData, @Amt, @CreateTime,@PayConfigNewId)", parameters) <= 0)
                    {
                        return new ApiResult(false, "保存订单信息失败,请重试").toJson();
                    }
                }
                #endregion

                data.SetValue("appid", payConfig.APPID);//公众账号ID
                data.SetValue("mch_id", payConfig.MCHID);//商户号
                data.SetValue("prepayid", result.GetValue("prepay_id").ToString());

                //成功后2次签名
                string strNonce_str = WxPayApi.GenerateNonceStr();
                int timestamp = GetTimeStamp(DateTime.Now);
                string SecodeSignStr = $"appid={payConfig.APPID}&noncestr={strNonce_str}&package=Sign=WXPay&partnerid={payConfig.MCHID}&prepayid={result.GetValue("prepay_id").ToString()}&timestamp={timestamp}";
                string sign = data.MakeSignNew(SecodeSignStr,payConfig.KEY);

                strBack += $"&appid={payConfig.APPID}&mch_id={payConfig.MCHID}&prepayid={result.GetValue("prepay_id").ToString()}&package=Sign=WXPay&nonce_str={strNonce_str}&timestamp={timestamp}&sign={sign}";

                log.Info("给APP返回的数据:" + strBack);
                return new ApiResult(true, strBack).toJson();
            }
        }
        private int GetTimeStamp(DateTime dt)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            int timeStamp = Convert.ToInt32((dt - dateStart).TotalSeconds);
            return timeStamp;
        }


        public WxPayConfig GenerateConfig(string CommunityId)
        {
            WxPayConfig wxPayConfig = null;
            IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString.ToString());
            string query = "SELECT * FROM Tb_WeiXinPay_Config WHERE CommunityId=@CommunityId";
            Tb_WeiXinPayCertificate T = conn.Query<Tb_WeiXinPayCertificate>(query, new { CommunityId = CommunityId }).SingleOrDefault();
            if (T != null)
            {
                wxPayConfig = new WxPayConfig();
                wxPayConfig.ID = T.Id.ToString();
                wxPayConfig.APPID = T.appid.ToString();
                wxPayConfig.MCHID = T.mch_id.ToString();
                wxPayConfig.KEY = T.appkey.ToString();
                wxPayConfig.APPSECRET = T.appsecret.ToString();
                wxPayConfig.SSLCERT_PATH = T.SSLCERT_PATH;
                wxPayConfig.SSLCERT_PASSWORD = T.SSLCERT_PASSWORD;
                wxPayConfig.NOTIFY_URL = Global_Fun.AppWebSettings("WechatPay_Notify_New_Url").ToString();
            }
            return wxPayConfig;
        }
    }
}
