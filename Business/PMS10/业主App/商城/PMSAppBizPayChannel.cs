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
    public class PMSAppBizPayChannel : PubInfo
    {
        public PMSAppBizPayChannel()
        {
            base.Token = "20200618PMSAppBizPayChannel";
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
                    case "GetBussPayChannel":
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
            if (!row.Table.Columns.Contains("BussID") || string.IsNullOrEmpty(row["BussID"].AsString()))
            {
                return new ApiResult(false, "缺少参数BussID").toJson();
            }

            var bussId = AppGlobal.StrToInt(row["BussID"].AsString());

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = @"SELECT * FROM Tb_AlipayCertifiate WHERE BussID=@Id AND isnull(IsEnable,0)=0;       /* 支付宝 */
                            SELECT * FROM Tb_WeiXinPayCertificate WHERE BussID=@Id AND isnull(IsEnable,0)=0;   /* 微信 */
                            SELECT * FROM Tb_UnionPayCertificate WHERE BussID=@Id AND isnull(IsEnable,0)=0;    /* 银联云闪付 */";

                var reader = conn.QueryMultiple(sql, new { Id = bussId });
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

                var hasTable = conn.Query<int>(sql).FirstOrDefault();
                if (hasTable == 1)
                {
                    sql = @"SELECT * FROM Tb_UnionQmf_Config 
                            WHERE BussID=@Id AND UnionQmfMethodId=(SELECT TOP 1 Id FROM Tb_UnionQmf_Method WHERE MsgType='trade.precreate');

                            SELECT * FROM Tb_UnionQmf_Config 
                            WHERE BussID=@Id AND UnionQmfMethodId IN
                            (
                                SELECT Id FROM Tb_UnionQmf_Method WHERE MsgType IN('wx.appPreOrder','wx.unifiedOrder')
                            );

                            SELECT * FROM Tb_UnionQmf_Config 
                            WHERE BussID=@Id AND UnionQmfMethodId=
                            (
                                SELECT TOP 1 Id FROM Tb_UnionQmf_Method WHERE MsgType IN('wx.unifiedOrder.MINI','wx.appPreOrder.MINI')
                            );";

                    reader = conn.QueryMultiple(sql, new { Id = bussId });
                    supportUnionAlipay = reader.Read().Count() > 0;
                    supportUnionWechat = reader.Read().Count() > 0;
                    supportUnionWechatMini = reader.Read().Count() > 0;
                }

                // 通联
                var supportAllInPayAlipay = false;
                var supportAllInPayWechat = false;

                sql = @"IF OBJECT_ID(N'Tb_Payment_Config',N'U') IS NOT NULL
                                    SELECT 1;
                                ELSE
                                    SELECT 0;";
                hasTable = conn.Query<int>(sql).FirstOrDefault();
                if (hasTable == 1)
                {
                    sql = $"SELECT Config FROM Tb_Payment_Config WITH(NOLOCK) WHERE BussID=@BussID;";

                    var config = conn.Query<string>(sql, new { BussID = bussId }).FirstOrDefault();
                    if (config != null)
                    {
                        supportAllInPayAlipay = true;
                        supportAllInPayWechat = true;
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
