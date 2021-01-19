using Business.httpUtil;
using Business.JDPayAPI;
using Business.PropertyUtil;
using Business.responseObj;
using Business.signature;
using Dapper;
using log4net;
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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WxPayAPI;

namespace Business
{
    public class JDPay : PubInfo
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(JDPay));
        /// <summary>
        /// openid用于调用统一下单接口
        /// </summary>
        //public string openid { get; set; }
        public CreateOrderResponse response
        {
            get { return res; }
        }
        private CreateOrderResponse res;


        public WxPayData unifiedOrderResult { get; set; }

        public JDPay() //获取小区、项目信息
        {
            base.Token = "20200103JDPay";
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
            Amt = Amt * 100;
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

            SortedDictionary<String, String> orderInfoDic = new SortedDictionary<string, string>();
            orderInfoDic.AddOrPeplace("version", "V2.0");
            orderInfoDic.AddOrPeplace("merchant", payConfig.MCHID.Trim());
            orderInfoDic.AddOrPeplace("tradeNum", OrderSN);
            orderInfoDic.AddOrPeplace("tradeName", "鸿坤瑞邦物业管理有限公司-物业缴费");
            orderInfoDic.AddOrPeplace("tradeTime", strStart);
            orderInfoDic.AddOrPeplace("amount", ((int)Amt).ToString().Trim());
            orderInfoDic.AddOrPeplace("orderType", "1");
            orderInfoDic.AddOrPeplace("currency", "CNY");
            orderInfoDic.AddOrPeplace("notifyUrl", payConfig.NOTIFY_URL.Trim());
            orderInfoDic.AddOrPeplace("userId", CustID.ToString().Trim());
            orderInfoDic.AddOrPeplace("userType", "BIZ");       
            orderInfoDic.AddOrPeplace("expireTime", "600");

            //获取风控信息
            RiskInfo risk = GetRiskInfo(CommunityId, CustID.ToString(), RoomID.ToString(), erpConnStr);
            if(risk != null)
            {
                orderInfoDic.AddOrPeplace("riskInfo", JsonConvert.SerializeObject(risk));
            }

            //获取支付详细信息
            List<GoodsInfo> googsInfoList = new List<GoodsInfo>();
            JObject PayDataObj = JObject.Parse(PayData);            
            int Type = (int)PayDataObj["Type"];
            if(Type == 1)
            {
                //实付
                JArray Data = (JArray)PayDataObj["Data"];
                string strFeeds = "";
                foreach (JObject item in Data)
                {
                    strFeeds += $"{(string)item["FeesId"]},";
                }

                strFeeds = strFeeds.TrimEnd(',');
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    string QuerySql = $"select c.CostID,c.CostName from tb_hspr_fees f inner join Tb_HSPR_CostItem c on f.CostID = c.CostID where f.FeesID in ({strFeeds})";
                    var costInfo = conn.Query(QuerySql);
                    foreach (var vc in costInfo)
                    {
                        GoodsInfo gd = new GoodsInfo()
                        {
                            id = vc.CostID+"",
                            name = vc.CostName
                        };
                        googsInfoList.Add(gd);
                    }
                }
            }
            if (Type == 2)
            {
                //预付
                JObject Data = (JObject)PayDataObj["Data"];
                string CostID = (string)Data["CostID"];
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    dynamic costInfo = conn.QueryFirstOrDefault<dynamic>("SELECT CostID, CostName FROM view_HSPR_CostStanSetting_Filter WHERE CustID = @CustID " + (0 == RoomID ? "" : " AND RoomID = @RoomID ") + " AND CostID= @CostID GROUP BY CostID, CostName", new { CustID = CustID, RoomID = RoomID, CostID = CostID });
                    GoodsInfo gd = new GoodsInfo()
                    {
                        id = costInfo.CostID+"",
                        name = costInfo.CostName
                    };

                    googsInfoList.Add(gd);
                }
            }

            orderInfoDic.AddOrPeplace("goodsInfo", JsonConvert.SerializeObject(googsInfoList));


            WxPayData data = new WxPayData();
                       
            String reqXmlStr = XMLUtil.encryptReqXml(payConfig.APPSECRET, payConfig.KEY, orderInfoDic);
            String refundUrl = PropertyUtils.getProperty("wepay.server.uniorder.url");
            String resultJsonData = HttpUtil.postRest(refundUrl, reqXmlStr);
            log.Error("京东支付第二步，返回结果:"+ resultJsonData);
            res = XMLUtil.decryptResXml<CreateOrderResponse>(payConfig.APPID, payConfig.KEY, resultJsonData);
            if (res.result.code !="000000")
            {
                log.Error("京东支付时调用服务器统一下单接口失败:");
                return new ApiResult(false, "京东支付失败,请联系客服人员").toJson();
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
                    if (conn.Execute(@"INSERT INTO Tb_CCBPay_Order(PayConfigId, OrderSN, CustID, RoomID, PayData, Amt, CreateTime,PayConfigNewId,orderType) 
                                    VALUES(@PayConfigId, @OrderSN, @CustID, @RoomID, @PayData, @Amt, @CreateTime,@PayConfigNewId,2)", parameters) <= 0)
                    {
                        return new ApiResult(false, "保存订单信息失败,请重试").toJson();
                    }
                }
                #endregion

                string signData = "";
                try
                {
                    //MD5加密
                    string needMd5Str = "merchant=" + payConfig.MCHID + "&orderId=" + res.orderId + "&key=" + payConfig.SSLCERT_PASSWORD;
                    var md5 = MD5.Create();
                    var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(needMd5Str));
                    var sb = new StringBuilder();
                    foreach (byte b in bs)
                    {
                        sb.Append(b.ToString("x2"));
                    }
                    //所有字符转为大写
                    signData = sb.ToString().ToLower();
                }
                catch(Exception ex)
                {

                }

                strBack += $"out_trade_no={OrderSN}&orderId={res.orderId}&merchant={payConfig.MCHID}&signData={signData}";

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
            string query = "SELECT Id,mch_id,desKey as appkey,rsaPublicKey as appid,rsaPrivateKey as appsecret,md5Key as SSLCERT_PASSWORD FROM Tb_JDPay_Config WHERE CommunityId=@CommunityId";
            Tb_WeiXinPayCertificate T = conn.Query<Tb_WeiXinPayCertificate>(query, new { CommunityId = CommunityId }).SingleOrDefault();
            if (T != null)
            {
                wxPayConfig = new WxPayConfig();
                wxPayConfig.ID = T.Id.ToString();
                wxPayConfig.APPID = T.appid.ToString();
                wxPayConfig.MCHID = T.mch_id.ToString();
                wxPayConfig.KEY = T.appkey.ToString();
                wxPayConfig.APPSECRET = T.appsecret.ToString();
                wxPayConfig.SSLCERT_PASSWORD = T.SSLCERT_PASSWORD.ToString();
                wxPayConfig.NOTIFY_URL = Global_Fun.AppWebSettings("JDPay_Notify_Url").ToString();
            }
            return wxPayConfig;
        }

        public RiskInfo GetRiskInfo(string CommunityId,string custId,string roomId,string connStr)
        {            
            IDbConnection conn = new SqlConnection(connStr);
            string query = "SELECT CommName as omName,RoomName as omId,CustName as ownerName FROM view_HSPR_Room_Filter WHERE IsDelete = 0 and CommID=@CommunityId and RoomID=@RoomId and CustID=@CustId";
            RiskInfo T = conn.Query<RiskInfo>(query, new { CommunityId = roomId.Substring(0,6), RoomId=roomId,CustId=custId }).SingleOrDefault();

            return T;
        }
    }

    public class RiskInfo
    {
        public string omId { get; set; }

        public string omName { get; set; }

        public string ownerName { get; set; }
    }

    public class GoodsInfo
    {
        public string id { get; set; }

        public string name { get; set; }

    }
}
