using CCBSign;
using Common;
using Dapper;
using log4net;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Business
{
    public class HKCCBPay : PubInfo
    {
        private ILog log;
        public HKCCBPay()
        {
            base.Token = "20180706LePos";
            log = LogManager.GetLogger(typeof(HKCCBPay));
        }

        public override void Operate(ref Transfer Trans)
        {
            try
            {
                DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];
                switch (Trans.Command)
                {
                    //生成订单
                    case "OnPay":
                        Trans.Result = OnPay(Row);
                        break;
                    default:
                        Trans.Result = new ApiResult(false, "接口不存在").toJson();
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                Trans.Result = new ApiResult(false, "接口抛出了一个异常").toJson();
            }
        }

        private string OnPay(DataRow row)
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
            if (!int.TryParse(row["PayType"].ToString(), out int PayType) || (PayType != 0 && PayType != 1))
            {
                PayType = 0;
            }
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
            dynamic payConfig;
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                payConfig = conn.QueryFirstOrDefault("SELECT * FROM Tb_CCBPay_Config WHERE CommunityId = @CommunityId", new { CommunityId = tb_Community.Id });
                if (null == payConfig)
                {
                    return new ApiResult(false, "该小区未配置支付信息").toJson();
                }
            }
            #endregion

            #region 检测支付数据格式
            if (!CheckPayData(erpConnStr, CustID, RoomID, PayData, out decimal Amt, out string errMsg, true))
            {
                return new ApiResult(false, errMsg).toJson();
            }
            if (Amt <= 0.00M)
            {
                return new ApiResult(false, "订单已被支付或者支付金额小于0").toJson();
            }
            #endregion

            DateTime DateNow = DateTime.Now;

            string OrderSN = DateNow.ToString("yyyyMMddHHmmssfff") + GetRandomCode(3);
            
            Dictionary<string, string> resultDic = new Dictionary<string, string>();
            resultDic.Add("MERCHANTID", Convert.ToString(payConfig.MerchantId));
            resultDic.Add("POSID", Convert.ToString(payConfig.PosId));
            resultDic.Add("BRANCHID", Convert.ToString(payConfig.BranchId));
            resultDic.Add("ORDERID", OrderSN);
            resultDic.Add("PAYMENT", Convert.ToString(Amt));
            resultDic.Add("CURCODE", "01");
            resultDic.Add("TXCODE", Convert.ToString(payConfig.TxCode));
            resultDic.Add("REMARK1", "");
            resultDic.Add("REMARK2", "");
            resultDic.Add("TYPE", Convert.ToString(payConfig.Type));
            
            string pub = Convert.ToString(payConfig.Pub);
            if (string.IsNullOrEmpty(pub) || pub.Length < 30)
            {
                log.Error("支付配置PUB有误:" + pub);
                return new ApiResult(false, "支付配置PUB有误").toJson();
            }
            else
            {
                pub = pub.Substring(pub.Length - 30, 30);
            }
            resultDic.Add("PUB", pub);
            resultDic.Add("GATEWAY", 0 == PayType ? "" : "UnionPay");
            resultDic.Add("CLIENTIP", "");
            resultDic.Add("REGINFO", "");
            resultDic.Add("PROINFO", "");
            resultDic.Add("REFERER", "");
            resultDic.Add("THIRDAPPINFO", string.Format("comccbpay{0}{1}", Convert.ToString(payConfig.MerchantId), "hkccbpay"));

            string signStr = "";
            foreach (var item in resultDic)
            {
                signStr += string.Format("{0}={1}&", item.Key, item.Value);
            }
            signStr = signStr.Remove(signStr.Length - 1, 1);
            signStr += "&MAC=" + AppPKI.getMd5Hash(signStr);

            #region 插入订单表
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("PayConfigId", payConfig.Id);
                parameters.Add("OrderSN", OrderSN);
                parameters.Add("CustID", CustID);
                parameters.Add("RoomID", RoomID);
                parameters.Add("PayData", PayData);
                parameters.Add("Amt", Amt);
                parameters.Add("CreateTime", DateNow.ToString());
                if (conn.Execute(@"INSERT INTO Tb_CCBPay_Order(PayConfigId, OrderSN, CustID, RoomID, PayData, Amt, CreateTime) 
                                    VALUES(@PayConfigId, @OrderSN, @CustID, @RoomID, @PayData, @Amt, @CreateTime)", parameters) <= 0)
                {
                    return new ApiResult(false, "保存订单信息失败,请重试").toJson();
                }
            }
            #endregion
            return new ApiResult(true, signStr).toJson();
        }

        /// <summary>
        /// 验签
        /// </summary>
        /// <param name="initStr"></param>
        /// <param name="sign"></param>
        /// <param name="pubKey"></param>
        /// <returns></returns>
        public static bool VerifySign(string initStr, string sign, string pubKey)
        {
            RSASig sig = new RSASig();
            sig.setPublicKey(pubKey);
            return sig.verifySigature(sign, initStr);
        }

        /// <summary>
        /// 更新订单信息
        /// </summary>
        /// <param name="connStr">数据库链接字符串</param>
        /// <param name="OrderNo">订单编号</param>
        /// <param name="PayTime">支付时间(DateTime.toString()),未支付传NULL</param>
        /// <param name="SAmt">支付金额,未支付传NULL</param>
        /// <param name="PayResult">交易结果</param>
        /// <param name="IsSucc">交易状态,0=未收到支付通知,1=收到通知,但是为无效交易,2=已收到通知,交易成功,但交易信息异常,3=已支付已下账,4=已支付但下账失败</param>
        /// <param name="Memo">订单备注</param>
        /// <returns></returns>
        public static bool UpdateOrderInfo(string connStr, string OrderSN, string PayTime, decimal SAmt, string PayResult, int IsSucc, string Memo = null)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    return conn.Execute("UPDATE Tb_CCBPay_Order SET PayTime = @PayTime, SAmt = @SAmt, PayResult = @PayResult, IsSucc = @IsSucc, Memo = @Memo WHERE OrderSN = @OrderSN AND IsSucc != 3", new { PayTime = PayTime, SAmt = SAmt, PayResult = PayResult, IsSucc = IsSucc, Memo = Memo, OrderSN = OrderSN }) > 0;
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex);
                return false;
            }
        }
    }
}
