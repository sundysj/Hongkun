using Business.PMS10.业主App.缴费.Model;
using Business.PMS10.报事.Models;
using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Model.支付配置模型;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using static Dapper.SqlMapper;


namespace Business
{
    internal class PayChannelString
    {
        // 支付宝
        public const string Alipay = "alipay";

        // 微信
        public const string WechatPay = "wechatpay";

        // 银联
        public const string UnionPay = "unionpay";

        // 全民付
        public const string UnionPay_Alipay = "unionpay_alipay";
        public const string UnionPay_WechatPay = "unionpay_wechatpay";
        public const string UnionPay_WechatPay_Mini = "unionpay_wechatpay_mini";

        // 通联
        public const string AllInPay = "allinpay";
        public const string AllInPay_Alipay = "allinpay_alipay";
        public const string AllInPay_WechatPay = "allinpay_wechatpay";
    }

    public class PMSAppPayChannel : PubInfo
    {
        public PMSAppPayChannel()
        {
            base.Token = "20191127PMSAppPayChannel";
        }

        public override void Operate(ref Transfer Trans)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            //防止未捕获异常出现
            try
            {
                switch (Trans.Command)
                {
                    case "GetPayChannel":
                        Trans.Result = GetPayChannel(Row);
                        break;
                    default:
                        Trans.Result = new ApiResult(false, "未知错误").toJson();
                        break;
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace + Environment.NewLine + ex.Source);
                Trans.Result = new ApiResult(false, ex.Message + ex.StackTrace).toJson();
            }
        }

        /// <summary>
        /// 获取支付通道
        /// </summary>
        private string GetPayChannel(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }

            var communityId = row["CommunityId"].AsString();
            var community = GetCommunity(communityId);

            using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT * FROM Tb_AlipayCertifiate WHERE CommunityId=@Id AND isnull(IsEnable,0)=0;       /* 支付宝 */
                            SELECT * FROM Tb_WeiXinPayCertificate WHERE CommunityId=@Id AND isnull(IsEnable,0)=0;   /* 微信 */
                            SELECT * FROM Tb_UnionPayCertificate WHERE CommunityId=@Id AND isnull(IsEnable,0)=0;    /* 银联云闪付 */";

                var reader = appConn.QueryMultiple(sql, new { Id = communityId });
                var supportAlipay = reader.Read().Count() > 0;
                var supportWechat = reader.Read().Count() > 0;
                var supportUnion = reader.Read().Count() > 0;

                // 银联全民付
                var supportUnionAlipay = false;
                var supportUnionWechat = false;
                var supportUnionWechatMini = false;
                sql = @"IF OBJECT_ID(N'Tb_UnionQmf_Config',N'U') IS NOT NULL
                            SELECT 1;
                        ELSE
                            SELECT 0;";

                var hasTable = appConn.Query<int>(sql).FirstOrDefault();
                if (hasTable == 1)
                {
                    sql = @"SELECT * FROM Tb_UnionQmf_Config WHERE CommunityId=@Id 
                                AND UnionQmfMethodId=(SELECT TOP 1 Id FROM Tb_UnionQmf_Method WHERE MsgType='trade.precreate');
                            SELECT * FROM Tb_UnionQmf_Config WHERE CommunityId=@Id 
                                AND UnionQmfMethodId IN(SELECT Id FROM Tb_UnionQmf_Method 
                                    WHERE MsgType IN('wx.appPreOrder','wx.unifiedOrder'));
                            SELECT * FROM Tb_UnionQmf_Config WHERE CommunityId=@Id 
                                AND UnionQmfMethodId=(SELECT TOP 1 Id FROM Tb_UnionQmf_Method 
                                    WHERE MsgType IN('wx.unifiedOrder.MINI','wx.appPreOrder.MINI'));";

                    reader = appConn.QueryMultiple(sql, new { Id = communityId });
                    supportUnionAlipay = reader.Read().Count() > 0;
                    supportUnionWechat = reader.Read().Count() > 0;
                    supportUnionWechatMini = reader.Read().Count() > 0;
                }

                // 通联
                var supportAllInPayAlipay = false;
                var supportAllInPayWechat = false;

                if (community != null)
                {
                    using (var erpConn = new SqlConnection(GetConnectionStr(community)))
                    {
                        sql = @"IF OBJECT_ID(N'Tb_Payment_Config',N'U') IS NOT NULL
                                    SELECT 1;
                                ELSE
                                    SELECT 0;";
                        hasTable = erpConn.Query<int>(sql).FirstOrDefault();
                        if (hasTable == 1)
                        {
                            // 一个CommID肯定只有一个配置项
                            sql = $"SELECT Config FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID";
                            var config = erpConn.QueryFirstOrDefault<string>(sql, new { CommID = community.CommID });
                            if (config != null)
                            {
                                // 原来的存储单个配置文件
                                try
                                {
                                    var obj = JsonConvert.DeserializeObject<AllinConfig>(config);
                                    if (obj.channel != null)
                                    {
                                        if (obj.channel.ToLower().Contains("allinpay_alipay"))
                                        {
                                            supportAllInPayAlipay = true;
                                        }
                                        if (obj.channel.ToLower().Contains("allinpay_wechat"))
                                        {
                                            supportAllInPayWechat = true;
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                                // 新的方式，Config存储多个配置
                                try
                                {
                                    // ERP的配置表，要求存储一个Json数组，用于配置支持不同支付方式
                                    // 配置项要求存储一个
                                    List<PaymentConfig> configs = JsonConvert.DeserializeObject<List<PaymentConfig>>(config);
                                    if (null != configs && configs.Count > 0)
                                    {
                                        if (null != configs.Find(item => item.type == "AliPay"))
                                        {
                                            supportAlipay = true;
                                        }
                                        if (null != configs.Find(item => item.type == "WChatPay"))
                                        {
                                            supportWechat = true;
                                        }
                                        if (null != configs.Find(item => item.type == "Allin_AliPay"))
                                        {
                                            supportAllInPayAlipay = true;
                                        }
                                        if (null != configs.Find(item => item.type == "Allin_WChatPay"))
                                        {
                                            supportAllInPayWechat = true;
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                    }
                }

                var list = new List<string>();
                if (supportAlipay)
                    list.Add(PayChannelString.Alipay);
                if (supportWechat)
                    list.Add(PayChannelString.WechatPay);
                if (supportUnion)
                    list.Add(PayChannelString.UnionPay);
                if (supportUnionAlipay)
                    list.Add(PayChannelString.UnionPay_Alipay);
                if (supportUnionWechat)
                    list.Add(PayChannelString.UnionPay_WechatPay);
                if (supportUnionWechatMini)
                    list.Add(PayChannelString.UnionPay_WechatPay_Mini);
                if (supportAllInPayAlipay)
                    list.Add(PayChannelString.AllInPay_Alipay);
                if (supportAllInPayWechat)
                    list.Add(PayChannelString.AllInPay_WechatPay);

                return new ApiResult(true, list).toJson();
            }
        }
    }
}
