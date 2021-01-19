using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using allinpay.utils;
using Business.WChat2020.Libs;
using Business.WChat2020.Model;
using Common;
using Common.Extenions;
using Dapper;
using DapperExtensions;
using fjnxpay.utils;
using log4net;
using mixuespay.utils;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Model.支付配置模型;
using Model.支付配置模型.华宇通联;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using swiftpass.utils;
using unionqmf.utils;
using wxpay.utils;

namespace Business.WChat2020
{
    public partial class Wx_PMS : PubInfo
    {
        private static readonly string REDIS_KEY_SMSID = "smsid_{0}";
        private static readonly string REDIS_KEY_SMSCODE = "smscode_{0}_{1}";
        private string CorpID = string.Empty;
        private string Domain = string.Empty;
        private string erpConnStr = string.Empty;
        private Tb_Corp_Config tb_Corp_Config;
        private readonly StackExchange.Redis.IDatabase mRedisDB = RedisHelper.RedisClient.GetDatabase();
        public Wx_PMS()
        {
            base.Token = "20191105Wx";
        }
        public override void Operate(ref Transfer Trans)
        {
            try
            {
                DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];
                if (Row.Table.Columns.Contains("Domain"))
                {
                    Domain = Row["Domain"].ToString();
                }
                // 微信端必须带CorpID，才能通过CorpID去查询对应系统的ERP连接字符串
                if (!Row.Table.Columns.Contains("CorpID"))
                {
                    Trans.Result = new WxResponse(0, "系统未配置", null).toJson();
                    return;
                }
                #region 根据CorpID查询系统的连接字符串
                CorpID = Row["CorpID"].ToString();
                {
                    try
                    {
                        using (IDbConnection conn = new SqlConnection(PubConstant.tw2bsConnectionString)
                            , wchatConn = new SqlConnection(PubConstant.WChat2020ConnectionString))
                        {
                            dynamic ServiceInfo = conn.QueryFirstOrDefault("SELECT DBServer,DBName,DBUser,DBPwd FROM Tb_System_Corp WHERE CorpID = @CorpID", new { CorpID });
                            if (null == ServiceInfo)
                            {
                                Trans.Result = new WxResponse(0, "系统不存在", null).toJson();
                                return;
                            }
                            erpConnStr = $"Pooling=false;Data Source={ServiceInfo.DBServer};Initial Catalog={ServiceInfo.DBName};User ID={ServiceInfo.DBUser};Password={ServiceInfo.DBPwd}";
                            tb_Corp_Config = wchatConn.QueryFirstOrDefault<Tb_Corp_Config>("SELECT * FROM Tb_Corp_Config WHERE CorpID = @CorpID", new { CorpID });
                            if (null == tb_Corp_Config)
                            {
                                Trans.Result = new WxResponse(0, "功能未开通", null).toJson();
                                return;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Trans.Result = new WxResponse(0, "查询系统信息失败", null).toJson();
                        return;
                    }
                }
                #endregion
                switch (Trans.Command)
                {
                    #region 登录相关
                    case "GetAuthorizeUrl":
                        Trans.Result = GetAuthorizeUrl(Row);
                        break;
                    case "Code2UserInfo":
                        Trans.Result = Code2UserInfo(Row);
                        break;
                    case "SendVCode":
                        Trans.Result = SendVCode(Row);
                        break;
                    case "LoginByVCode":
                        Trans.Result = LoginByVCode(Row);
                        break;
                    #endregion
                    #region 用户基本信息
                    case "GetRoomListByMobile":
                        Trans.Result = GetRoomListByMobile(Row);
                        break;
                    case "SetDefaultRoom":
                        Trans.Result = SetDefaultRoom(Row);
                        break;
                    #endregion
                    #region 新闻公告
                    case "GetNewsList":
                        Trans.Result = GetNewsList(Row);
                        break;
                    case "GetNewsDetail":
                        Trans.Result = GetNewsDetail(Row);
                        break;
                    #endregion
                    #region 支付相关
                    case "GetPaymentCostList":
                        Trans.Result = GetPaymentCostList(Row);
                        break;
                    case "GetPaymentHistory":
                        Trans.Result = GetPaymentHistory(Row);
                        break;
                    case "GetPaymentFeesList":
                        Trans.Result = GetPaymentFeesList(Row);
                        break;
                    case "GetPaymentFeesList_v2":
                        Trans.Result = GetPaymentFeesList_v2(Row);
                        break;
                    case "GetPaymentHistory_v2":
                        Trans.Result = GetPaymentHistory_v2(Row);
                        break;
                    case "GetPaymentHistoryDetail_v2":
                        Trans.Result = GetPaymentHistoryDetail_v2(Row);
                        break;
                    case "CreateOrder_WftPay":
                        Trans.Result = CreateOrder_WftPay(Row);
                        break;
                    case "QueryOrder_WftPay":
                        Trans.Result = QueryOrder_WftPay(Row);
                        break;
                    case "CreateOrder_UnionQmfPay":
                        Trans.Result = CreateOrder_UnionQmfPay(Row);
                        break;
                    case "QueryOrder_UnionQmfPay":
                        Trans.Result = QueryOrder_UnionQmfPay(Row);
                        break;
                    case "CreateOrder_AllinPay":
                        Trans.Result = CreateOrder_AllinPay(Row);
                        break;
                    case "QueryOrder_AllinPay":
                        Trans.Result = QueryOrder_AllinPay(Row);
                        break;
                    case "CreateOrder_WChatPay":
                        Trans.Result = CreateOrder_WChatPay(Row);
                        break;
                    case "QueryOrder_WChatPay":
                        Trans.Result = QueryOrder_WChatPay(Row);
                        break;
                    case "CreateOrder_MixuesPay":
                        Trans.Result = CreateOrder_MixuesPay(Row);
                        break;
                    case "QueryOrder_MixuesPay":
                        Trans.Result = QueryOrder_MixuesPay(Row);
                        break;
                    case "CreateOrder_UnionTmfPay":
                        Trans.Result = CreateOrder_UnionTmfPay(Row);
                        break;
                    case "QueryOrder_UnionTmfPay":
                        Trans.Result = QueryOrder_UnionTmfPay(Row);
                        break;
                    case "CreateOrder_FjnxPay":
                        Trans.Result = CreateOrder_FjnxPay(Row);
                        break;
                    case "QueryOrder_FjnxPay":
                        Trans.Result = QueryOrder_FjnxPay(Row);
                        break;
                    #endregion
                    #region 报事相关
                    case "GetIncidentConfig":
                        Trans.Result = GetIncidentConfig(Row);
                        break;
                    case "UploadImage":
                        Trans.Result = UploadImage(Row);
                        break;
                    case "SubmitIncident":
                        Trans.Result = SubmitIncident(Row);
                        break;
                    case "GetIncidentList":
                        Trans.Result = GetIncidentList(Row);
                        break;
                    case "GetIncidentDetail":
                        Trans.Result = GetIncidentDetail(Row);
                        break;
                    case "SubmitIncidentEval":
                        Trans.Result = SubmitIncidentEval(Row);
                        break;
                    #endregion
                    #region 成员邀请
                    case "GetHouseHoldList":
                        Trans.Result = GetHouseHoldList(Row);
                        break;
                    case "RemoveHouseHold":
                        Trans.Result = RemoveHouseHold(Row);
                        break;
                    case "CreateHouseHold":
                        Trans.Result = CreateHouseHold(Row);
                        break;
                    #endregion
                    #region 我的管家
                    case "GetHouseKeeperList":
                        Trans.Result = GetHouseKeeperList(Row);
                        break;
                    #endregion
                    #region 我的发票
                    case "GetInvoiceList":
                        Trans.Result = GetInvoiceList(Row);
                        break;
                    case "SubmitInvoice":
                        Trans.Result = SubmitInvoice(Row);
                        break;
                    #endregion
                    #region 天城魔蓝门禁
                    case "GetMoredianMember":
                        Trans.Result = GetMoredianMember(Row);
                        break;
                    case "SubmitMoredianMember":
                        Trans.Result = SubmitMoredianMember(Row);
                        break;
                    case "GetRemeteDeviceList":
                        Trans.Result = GetRemeteDeviceList(Row);
                        break;
                    case "RemoteOpenDoor":
                        Trans.Result = RemoteOpenDoor(Row);
                        break;
                    case "CreateVisitor":
                        Trans.Result = CreateVisitor(Row);
                        break;
                    case "GetVisitorInfo":
                        Trans.Result = GetVisitorInfo(Row);
                        break;
                    #endregion
                    default:
                        Trans.Result = new WxResponse(0, "非法操作", null).toJson();
                        break;
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                Trans.Result = new WxResponse(0, "响应异常", null).toJson();
            }
        }

        #region 我的发票
        private string SubmitInvoice(DataRow row)
        {
            try
            {

                #region 获取参数并简单校验
                string CommID = string.Empty;
                if (row.Table.Columns.Contains("CommID"))
                {
                    CommID = row["CommID"].ToString();
                }
                if (string.IsNullOrEmpty(CommID))
                {
                    return new WxResponse(0, "请选择默认房屋(1001)", null).toJson();
                }
                string CustID = string.Empty;
                if (row.Table.Columns.Contains("CustID"))
                {
                    CustID = row["CustID"].ToString();
                }
                if (string.IsNullOrEmpty(CustID))
                {
                    return new WxResponse(0, "请选择默认房屋(1002)", null).toJson();
                }
                string RoomID = string.Empty;
                if (row.Table.Columns.Contains("RoomID"))
                {
                    RoomID = row["RoomID"].ToString();
                }
                if (string.IsNullOrEmpty(RoomID))
                {
                    return new WxResponse(0, "请选择默认房屋(1003)", null).toJson();
                }
                string FeesIDs = string.Empty;
                if (row.Table.Columns.Contains("FeesIDs"))
                {
                    FeesIDs = row["FeesIDs"].ToString();
                }
                if (string.IsNullOrEmpty(FeesIDs))
                {
                    return new WxResponse(0, "开票信息有误", null).toJson();
                }
                string Mobile = string.Empty;
                if (row.Table.Columns.Contains("Mobile"))
                {
                    Mobile = row["Mobile"].ToString();
                }
                if (string.IsNullOrEmpty(Mobile))
                {
                    return new WxResponse(0, "请填写邮箱/手机号用于接收发票", null).toJson();
                }
                #endregion

                if (!new EInvoice_SL().SubmitInvoice(CommID, RoomID, CustID, FeesIDs, Mobile, out string msg))
                {
                    return new WxResponse(0, msg, null).toJson();
                }
                return new WxResponse(200, "开票成功", JObject.Parse(msg)).toJson();
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        private string GetInvoiceList(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string CommID = string.Empty;
                if (row.Table.Columns.Contains("CommID"))
                {
                    CommID = row["CommID"].ToString();
                }
                if (string.IsNullOrEmpty(CommID))
                {
                    return new WxResponse(0, "请选择默认房屋(1001)", null).toJson();
                }
                string CustID = string.Empty;
                if (row.Table.Columns.Contains("CustID"))
                {
                    CustID = row["CustID"].ToString();
                }
                if (string.IsNullOrEmpty(CustID))
                {
                    return new WxResponse(0, "请选择默认房屋(1002)", null).toJson();
                }
                string RoomID = string.Empty;
                if (row.Table.Columns.Contains("RoomID"))
                {
                    RoomID = row["RoomID"].ToString();
                }
                if (string.IsNullOrEmpty(RoomID))
                {
                    return new WxResponse(0, "请选择默认房屋(1003)", null).toJson();
                }
                if (!row.Table.Columns.Contains("Type") || !int.TryParse(row["Type"].ToString(), out int Type))
                {
                    Type = 0;
                }
                if (0 != Type && 1 != Type)
                {
                    Type = 0;
                }
                #endregion

                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();
                    if (Type == 0)
                    {
                        string sql = @"SELECT b.FeesID,c.CostName,(ISNULL(a.ChargeAmount,0)+ISNULL(a.LateFeeAmount,0)) AS Amount,a.ChargeDate,b.FeesDueDate FROM Tb_HSPR_FeesDetail a JOIN Tb_HSPR_Fees b ON b.FeesID = a.FeesID JOIN Tb_HSPR_CostItem c ON c.CostID = a.CostID AND c.CommID = a.CommID 
                                WHERE ISNULL(a.IsDelete,0) = 0 AND a.CommID = @CommID AND a.CustID = @CustID AND a.RoomID = @RoomID";
                        List<dynamic> list = conn.Query(sql, new { CommID, CustID, RoomID }).ToList();

                        foreach (dynamic item in list)
                        {
                            resultList.Add(new Dictionary<string, object>
                        {
                            {"FeesID", Convert.ToString(item.FeesID) },
                            {"CostName", Convert.ToString(item.CostName) },
                            {"Amount", Convert.ToDecimal(item.Amount) },
                            {"ChargeDate", Convert.ToString(item.ChargeDate) },
                            {"FeesDueDate", Convert.ToString(item.FeesDueDate) },
                        });
                        }
                    }
                    else
                    {
                        // 获取已开票数据
                    }
                    return new WxResponse(200, "获取成功", resultList).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        #endregion
        #region 成员邀请

        /// <summary>
        /// 获取公众号二维码地址
        /// </summary>
        /// <returns></returns>
        private string GetMPQrCode()
        {
            string QrCode = string.Empty;
            try
            {
                WxCache wxCache = new WxCache(RedisHelper.RedisClient.GetDatabase(), GetLog());
                if (!WxApi.WxPlatform.GetAuthorizerInfo(wxCache.GetComponentAccessToken(WxPlatformSetting.WxPlatformAppID, WxPlatformSetting.WxPlatformAppSecret), WxPlatformSetting.WxPlatformAppID, tb_Corp_Config.AppID, out string msg))
                {
                    GetLog().Error(msg);
                }
                else
                {
                    JObject jObj = JObject.Parse(msg);
                    if (jObj.ContainsKey("authorizer_info"))
                    {
                        jObj = (JObject)jObj["authorizer_info"];
                        if (jObj.ContainsKey("qrcode_url"))
                        {
                            QrCode = jObj["qrcode_url"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
            }
            return QrCode;
        }
        /// <summary>
        /// 添加家庭成员
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string CreateHouseHold(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string CommID = string.Empty;
                if (row.Table.Columns.Contains("CommID"))
                {
                    CommID = row["CommID"].ToString();
                }
                if (string.IsNullOrEmpty(CommID))
                {
                    return new WxResponse(0, "请选择默认房屋(1001)", null).toJson();
                }
                string CustID = string.Empty;
                if (row.Table.Columns.Contains("CustID"))
                {
                    CustID = row["CustID"].ToString();
                }
                if (string.IsNullOrEmpty(CustID))
                {
                    return new WxResponse(0, "请选择默认房屋(1002)", null).toJson();
                }
                string RoomID = string.Empty;
                if (row.Table.Columns.Contains("RoomID"))
                {
                    RoomID = row["RoomID"].ToString();
                }
                if (string.IsNullOrEmpty(RoomID))
                {
                    return new WxResponse(0, "请选择默认房屋(1003)", null).toJson();
                }
                string UserName = string.Empty;
                if (row.Table.Columns.Contains("UserName"))
                {
                    UserName = row["UserName"].ToString();
                }
                if (string.IsNullOrEmpty(UserName))
                {
                    return new WxResponse(0, "请填写成员名称", null).toJson();
                }
                string Mobile = string.Empty;
                if (row.Table.Columns.Contains("Mobile"))
                {
                    Mobile = row["Mobile"].ToString();
                }
                string Type = string.Empty;
                if (row.Table.Columns.Contains("Type"))
                {
                    Type = row["Type"].ToString();
                }
                else
                {
                    Type = "0030";
                }
                string Relationship = Type;
                string Memo = "微信公众号";
                string MemberCode = null;
                string RelationID = null;
                string InquirePwd = "123";
                #endregion
                using (IDbConnection erpConn = new SqlConnection(erpConnStr),
                    conn = new SqlConnection(PubConstant.WChat2020ConnectionString))
                {
                    erpConn.Open();
                    var trans = erpConn.BeginTransaction();
                    try
                    {
                        if (string.IsNullOrEmpty(Mobile))
                        {
                            if(erpConn.QueryFirstOrDefault<int>("SELECT ISNULL(OBJECT_ID('Tb_HSPR_Entrance_Mobile'),0)",null,trans) <= 0)
                            {
                                trans.Rollback();
                                return new WxResponse(0, "成员手机号不能为空", null).toJson();
                            }
                            long MobileNum = erpConn.QueryFirstOrDefault<long>("SELECT Mobile FROM Tb_HSPR_Entrance_Mobile WITH(NOLOCK) ORDER BY Id DESC", null, trans);
                            if (MobileNum <= 14000000000)
                            {
                                MobileNum = 14000000000;
                            }
                            MobileNum += 1;
                            if(erpConn.Execute("INSERT INTO Tb_HSPR_Entrance_Mobile(Mobile) VALUES(@Mobile)", new { Mobile = MobileNum }, trans) <= 0)
                            {
                                trans.Rollback();
                                return new WxResponse(0, "邀请失败,请重试", null).toJson();
                            }
                            Mobile = Convert.ToString(MobileNum);
                        }
                        // 泰禾限制邀请成员不能超过5个
                        if ("1970".Equals(CorpID))
                        {
                            if (erpConn.Query("SELECT * FROM Tb_HSPR_Household WHERE ISNULL(IsDelete,0) = 0 AND CommID = @CommID AND RoomID = @RoomID AND CustID = @CustID", new { CommID, RoomID, CustID }, trans).Count() >= 5)
                            {
                                trans.Rollback();
                                return new WxResponse(0, "邀请成员不能超过5个", null).toJson();
                            }
                        }
                        dynamic info = erpConn.QueryFirstOrDefault("SELECT * FROM Tb_HSPR_Household WHERE CommID = @CommID AND RoomID = @RoomID AND CustID = @CustID AND MobilePhone = @MobilePhone", new { CommID, RoomID, CustID, MobilePhone = Mobile }, trans);
                        long HoldID = 0;
                        if (null != info)
                        {
                            if (Convert.ToInt32(info.IsDelete) == 0)
                            {
                                return new WxResponse(200, "操作成功", GetMPQrCode()).toJson();
                            }
                            HoldID = Convert.ToInt64(info.HoldID);
                            if (erpConn.Execute("UPDATE Tb_HSPR_Household SET CommID = @CommID,CustID = @CustID,RoomID = @RoomID,Surname = @Name,Name = @Name,MobilePhone = @MobilePhone,Relationship = @Relationship,Memo = @Memo,Linkman = @Name,MemberCode = @MemberCode,MemberName = @Name, IsDelete = 0,SynchFlag = 0 WHERE HoldID = @HoldID",
                                new { CommID, CustID, RoomID, Name = UserName, MobilePhone = Mobile, Relationship, Memo, MemberCode, HoldID }, trans) <= 0)
                            {
                                trans.Rollback();
                                return new WxResponse(0, "操作失败", null).toJson();
                            }
                            trans.Commit();
                            return new WxResponse(200, "操作成功", GetMPQrCode()).toJson();
                        }
                        HoldID = erpConn.QueryFirstOrDefault<long>("SELECT ISNULL(MAX(HoldID) + 1, (@CommID * CAST(100000000 AS BIGINT) + 1)) FROM Tb_HSPR_Household  WHERE CommID = @CommID", new { CommID }, trans);
                        if (erpConn.Execute(@"INSERT INTO Tb_HSPR_Household(HoldID, RelationID, CommID, CustID, RoomID, Surname, [Name], MobilePhone, Relationship,  
                              InquirePwd, Memo, MemberName, MemberCode, Linkman, LinkManTel, IsDelete, SynchFlag)
                             VALUES(@HoldID, @RelationID, @CommID, @CustID, @RoomID, @Name, @Name, @MobilePhone, @Relationship, @InquirePwd, @Memo,
                                @Name, @MemberCode, @Name, @MobilePhone, 0, 0)", new { HoldID, RelationID, CommID, CustID, RoomID, Name = UserName, MobilePhone = Mobile, Relationship, InquirePwd, Memo, MemberCode, }, trans) <= 0)
                        {
                            trans.Rollback();
                            return new WxResponse(0, "操作失败", null).toJson();
                        }
                        erpConn.Execute("Proc_HSPR_Telephone_InsUpate", new { CommID, CustID, HoldID, TelSource = 1 }, trans, null, CommandType.StoredProcedure);
                        trans.Commit();
                        return new WxResponse(200, "操作成功", GetMPQrCode()).toJson();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                        return new WxResponse(0, "操作失败", null).toJson();
                    }
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        /// <summary>
        /// 移除家庭成员
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string RemoveHouseHold(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string CommID = string.Empty;
                if (row.Table.Columns.Contains("CommID"))
                {
                    CommID = row["CommID"].ToString();
                }
                if (string.IsNullOrEmpty(CommID))
                {
                    return new WxResponse(0, "请选择默认房屋(1001)", null).toJson();
                }
                string CustID = string.Empty;
                if (row.Table.Columns.Contains("CustID"))
                {
                    CustID = row["CustID"].ToString();
                }
                if (string.IsNullOrEmpty(CustID))
                {
                    return new WxResponse(0, "请选择默认房屋(1002)", null).toJson();
                }
                string RoomID = string.Empty;
                if (row.Table.Columns.Contains("RoomID"))
                {
                    RoomID = row["RoomID"].ToString();
                }
                if (string.IsNullOrEmpty(RoomID))
                {
                    return new WxResponse(0, "请选择默认房屋(1003)", null).toJson();
                }
                string HoldID = string.Empty;
                if (row.Table.Columns.Contains("HoldID"))
                {
                    HoldID = row["HoldID"].ToString();
                }
                if (string.IsNullOrEmpty(HoldID))
                {
                    return new WxResponse(0, "请选择要移除的成员", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    if (conn.Execute("UPDATE Tb_HSPR_Household SET IsDelete = 1 WHERE CommID = @CommID AND RoomID = @RoomID AND CustID = @CustID AND HoldID = @HoldID AND ISNULL(IsDelete,0) = 0", new { CommID, RoomID, CustID, HoldID }) <= 0)
                    {
                        return new WxResponse(0, "移除失败", null).toJson();
                    }
                    return new WxResponse(200, "操作成功", null).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        /// <summary>
        /// 获取家庭成员列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetHouseHoldList(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string CommID = string.Empty;
                if (row.Table.Columns.Contains("CommID"))
                {
                    CommID = row["CommID"].ToString();
                }
                if (string.IsNullOrEmpty(CommID))
                {
                    return new WxResponse(0, "请选择默认房屋(1001)", null).toJson();
                }
                string CustID = string.Empty;
                if (row.Table.Columns.Contains("CustID"))
                {
                    CustID = row["CustID"].ToString();
                }
                if (string.IsNullOrEmpty(CustID))
                {
                    return new WxResponse(0, "请选择默认房屋(1002)", null).toJson();
                }
                string RoomID = string.Empty;
                if (row.Table.Columns.Contains("RoomID"))
                {
                    RoomID = row["RoomID"].ToString();
                }
                if (string.IsNullOrEmpty(RoomID))
                {
                    return new WxResponse(0, "请选择默认房屋(1003)", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    List<dynamic> list = conn.Query("SELECT CAST(HoldID AS VARCHAR) AS HoldID,Name,CAST(MobilePhone AS VARCHAR) AS MobilePhone, ISNULL(IsDelete,0) AS IsDelete FROM Tb_HSPR_Household WHERE CommID = @CommID AND CustID = @CustID AND RoomID = @RoomID", new { CommID, CustID, RoomID }).ToList();
                    return new WxResponse(200, "获取成功", list).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        #endregion

        #region 我的管家
        private string GetHouseKeeperList(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string RoomID = string.Empty;
                if (row.Table.Columns.Contains("RoomID"))
                {
                    RoomID = row["RoomID"].ToString();
                }
                if (string.IsNullOrEmpty(RoomID))
                {
                    return new WxResponse(0, "房产信息有误", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    dynamic roominfo = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_HSPR_Room WHERE ISNULL(IsDelete, 0) = 0 AND RoomID = @RoomID", new { RoomID });
                    if (null == roominfo)
                    {
                        return new WxResponse(0, "房产信息有误", null).toJson();
                    }
                    string CommID = Convert.ToString(roominfo.CommID);
                    string BuildSNum = Convert.ToString(roominfo.BuildSNum);
                    List<dynamic> list = conn.Query(@"SELECT a.UserCode,a.UserName,ISNULL(b.Tel,a.MobileTel) AS MobileTel ,a.HeadImg 
                                                        FROM Tb_Sys_User a LEFT JOIN Tb_HSPR_BuildHousekeeper b ON a.UserCode = b.UserCode WHERE b.CommID = @CommID AND b.BuildSNum = @BuildSNum ", new { CommID, BuildSNum }).ToList();
                    return new WxResponse(200, "获取成功", list).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        #endregion
        #region 报事

        private string SubmitIncidentEval(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string IncidentID = string.Empty;
                if (row.Table.Columns.Contains("IncidentID"))
                {
                    IncidentID = row["IncidentID"].ToString();
                }
                if (string.IsNullOrEmpty(IncidentID))
                {
                    return new WxResponse(0, "未找到对应报事信息", null).toJson();
                }
                if (!row.Table.Columns.Contains("Eval") || !int.TryParse(row["Eval"].ToString(), out int Eval))
                {
                    return new WxResponse(0, "请为服务评分", null).toJson();
                }
                if (Eval < 1 || Eval > 5)
                {
                    return new WxResponse(0, "评分有误", null).toJson();
                }
                string ReplyContent = string.Empty;
                if (row.Table.Columns.Contains("ReplyContent"))
                {
                    ReplyContent = row["ReplyContent"].ToString();
                }
                if (string.IsNullOrEmpty(ReplyContent))
                {
                    return new WxResponse(0, "评价内容不能为空", null).toJson();
                }
                string ServiceQuality;
                switch (Eval)
                {
                    case 1:
                        ServiceQuality = "非常不满意";
                        break;
                    case 2:
                        ServiceQuality = "不满意";
                        break;
                    case 3:
                        ServiceQuality = "一般";
                        break;
                    case 4:
                        ServiceQuality = "满意";
                        break;
                    case 5:
                        ServiceQuality = "非常满意";
                        break;
                    default:
                        return new WxResponse(0, "请为服务评分", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    dynamic incident = conn.QueryFirstOrDefault<dynamic>("SELECT IncidentID,CommID FROM view_HSPR_IncidentNewJH_Search_Filter WHERE IncidentID = @IncidentID", new { IncidentID });
                    if (null == incident)
                    {
                        return new WxResponse(0, "该报事不存在", null).toJson();
                    }
                    int CommID = Convert.ToInt32(incident.CommID);
                    try
                    {
                        // 部分没有关闭功能
                        dynamic incident_close = conn.QueryFirstOrDefault<dynamic>("SELECT IncidentID, IsClose, CommID ,ReplyDate FROM view_HSPR_IncidentNewJH_Search_Filter WHERE IncidentID = @IncidentID", new { IncidentID });
                        if (null == incident_close)
                        {
                            return new WxResponse(0, "该报事不存在", null).ToString();
                        }
                        if (null != incident_close.ReplyDate)
                        {
                            return new WxResponse(0, "该报事已关闭", null).ToString();
                        }
                        if (conn.Execute("Proc_HSPR_IncidentAccept_IncidentClose", new { CommID, IncidentID, CloseUserCode = "", CloseUser = "", CloseSituation = "客户通过微信主动关闭", CloseType = 0, IsClose = 1, NoNormalCloseReasons = "" }, null, null, CommandType.StoredProcedure) <= 0)
                        {
                            return new WxResponse(0, "关闭失败,请重试", null).ToString();
                        }
                    }
                    catch (Exception)
                    {
                    }
                    
                    string IncidentMan = Convert.ToString(incident.IncidentMan);
                    string ReplyType = null;
                    string ReplyManCode = null;
                    string ReplyMan = "";
                    string ReplyWay = null;
                    //川仪缺少ReplySituationCode
                    if (conn.Execute("Proc_HSPR_IncidentReply_Insert", new { CommID, IncidentID, ReplyType, ReplyManCode, ReplyMan, ReplyDate = DateTime.Now, ReplyContent, ServiceQuality, ReplyWay, ReplyResult = "1" , ReplySituationCode =Global_Var.LoginUserCode}, null, null, CommandType.StoredProcedure) <= 0)
                    {
                        return new WxResponse(0, "评价失败,请重试", null).toJson();
                    }
                    return new WxResponse(200, "评价成功", null).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常"+ex.Message, null).toJson();
            }
        }
        /// <summary>
        /// 获取报事详情
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetIncidentDetail(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string IncidentID = string.Empty;
                if (row.Table.Columns.Contains("IncidentID"))
                {
                    IncidentID = row["IncidentID"].ToString();
                }
                if (string.IsNullOrEmpty(IncidentID))
                {
                    return new WxResponse(0, "未找到对应报事信息", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    dynamic incident = conn.QueryFirstOrDefault("SELECT * FROM view_HSPR_IncidentProcessing_Filter WHERE IncidentID = @IncidentID", new { IncidentID });
                    if (null == incident)
                    {
                        return new WxResponse(0, "未找到对应报事信息", null).toJson();
                    }
                    #region 查询报事图片
                    HashSet<string> ImageList = GetIncidentImageList(erpConnStr, IncidentID);
                    string IncidentImgs = string.Join(",", ImageList.ToArray());
                    #endregion

                    #region 获取报事状态
                    int DispType = Convert.ToInt32(incident.DispType);
                    int DealState = Convert.ToInt32(incident.DealState);
                    string IncidentReply = Convert.ToString(incident.IncidentReply);
                    string IncidentState = "未分派";
                    if (DispType == 1)
                    {
                        IncidentState = "已分派";
                    }
                    if (DispType == 1 && DealState == 1)
                    {
                        IncidentState = "已完成";
                    }
                    if (DealState == 1 && IncidentReply == "未回访")
                    {
                        IncidentState = "未回访";
                    }

                    if (DealState == 1 && IncidentReply == "已回访")
                    {
                        IncidentState = "已回访";
                    }
                    #endregion

                    #region 获取业主报事位置
                    string IncidentLocation = string.Empty;
                    string IncidentPlace = Convert.ToString(incident.IncidentPlace);
                    string CommName = Convert.ToString(incident.CommName);
                    if ("户内".Equals(IncidentPlace))
                    {
                        string RoomName = Convert.ToString(incident.RoomName);
                        IncidentLocation = CommName + " " + RoomName;
                    }
                    else
                    {
                        IncidentLocation = CommName + " 公共区域";
                    }
                    #endregion

                    #region 创建任务节点
                    List<object> IncidentWorkNode = new List<object>();
                    #region 创建任务
                    {
                        string IncidentMan = Convert.ToString(incident.IncidentMan);
                        // 后续可以切换为报事来源字段
                        string IncidentMode = Convert.ToString(incident.IncidentMode);
                        string IncidentDate = Convert.ToString(incident.IncidentDate);
                        string Content = $"【{IncidentMan}】通过“{IncidentMode}”提交了任务";
                        string DateTime = IncidentDate;
                        IncidentWorkNode.Add(CreateIncidentNode(Content, DateTime, 1));
                    }
                    #endregion
                    #region 分派任务
                    {
                        if (IncidentState != "未分派")
                        {
                            string DispMan = Convert.ToString(incident.DispMan);
                            string DealMan = Convert.ToString(incident.DealMan);
                            string DispDate = Convert.ToString(incident.DispDate);
                            string Content = $"【{DispMan}】已将工单分派给【{DealMan}】";
                            string DateTime = DispDate;
                            IncidentWorkNode.Add(CreateIncidentNode(Content, DateTime, 2));
                        }

                    }
                    #endregion
                    #region 处理任务
                    {
                        if (IncidentState != "未分派")
                        {
                            string DealMan = Convert.ToString(incident.DealMan);
                            string DispDate = Convert.ToString(incident.DispDate);
                            string Content = $"【{DealMan}】开始处理";
                            string DateTime = DispDate;
                            IncidentWorkNode.Add(CreateIncidentNode(Content, DateTime, 3));
                        }
                    }
                    #endregion
                    #region 处理完成
                    {
                        if (IncidentState != "未分派" && IncidentState != "已分派")
                        {
                            string DealMan = Convert.ToString(incident.DealMan);
                            string MainEndDate = Convert.ToString(incident.MainEndDate);
                            string Content = $"【{DealMan}】处理完毕";
                            string DateTime = MainEndDate;
                            IncidentWorkNode.Add(CreateIncidentNode(Content, DateTime, 4));
                        }
                    }
                    #endregion
                    #region 评价完成
                    {
                        if (IncidentState == "已回访")
                        {
                            string IncidentMan = Convert.ToString(incident.IncidentMan);
                            string ReplyDate = Convert.ToString(incident.ReplyDate);
                            string Content = $"【{IncidentMan}】已作出评价";
                            string DateTime = ReplyDate;
                            IncidentWorkNode.Add(CreateIncidentNode(Content, DateTime, 5));
                        }
                    }
                    #endregion
                    #endregion

                    #region 获取处理人
                    string IncidentDealMan = Convert.ToString(incident.DealMan);
                    #endregion
                    #region 获取评价
                    string ServiceQuality = Convert.ToString(incident.ServiceQuality);
                    string ReplyContent = Convert.ToString(incident.ReplyContent);
                    int IncidentEval = 0;
                    switch (ServiceQuality)
                    {
                        case "非常不满意":
                            IncidentEval = 1;
                            break;
                        case "不满意":
                            IncidentEval = 2;
                            break;
                        case "一般":
                            IncidentEval = 3;
                            break;
                        case "满意":
                            IncidentEval = 4;
                            break;
                        case "非常满意":
                            IncidentEval = 5;
                            break;
                        default:
                            IncidentEval = 0;
                            break;
                    }
                    string IncidentEvalContent = ReplyContent;

                    #endregion
                    Dictionary<string, object> incidentInfo = new Dictionary<string, object>();
                    incidentInfo.Add("IncidentID", Convert.ToString(incident.IncidentID));
                    incidentInfo.Add("IncidentNum", Convert.ToString(incident.IncidentNum));
                    incidentInfo.Add("IncidentDate", Convert.ToString(incident.IncidentDate));
                    incidentInfo.Add("IncidentImgs", IncidentImgs);
                    incidentInfo.Add("IncidentContent", Convert.ToString(incident.IncidentContent));
                    incidentInfo.Add("IncidentState", IncidentState);
                    incidentInfo.Add("IncidentLocation", IncidentLocation);
                    incidentInfo.Add("IncidentDealMan", IncidentDealMan);
                    incidentInfo.Add("IncidentWorkNode", IncidentWorkNode);
                    incidentInfo.Add("IncidentEvalContent", IncidentEvalContent);
                    incidentInfo.Add("IncidentEval", IncidentEval);
                    return new WxResponse(200, "获取成功", incidentInfo).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }

        private object CreateIncidentNode(string Content, string DateTime, int Sort)
        {
            return new { Content, DateTime, Sort };
        }
        /// <summary>
        /// 获取报事图片
        /// </summary>
        /// <param name="erpConnStr"></param>
        /// <param name="IncidentID"></param>
        /// <returns></returns>
        private HashSet<string> GetIncidentImageList(string erpConnStr, string IncidentID)
        {
            HashSet<string> ImageList = new HashSet<string>();
            try
            {
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    string IncidentImgs = conn.QueryFirstOrDefault<string>("SELECT IncidentImgs FROM Tb_HSPR_IncidentAccept WHERE IncidentID = @IncidentID", new { IncidentID });
                    if (!string.IsNullOrEmpty(IncidentImgs))
                    {
                        foreach (var item in IncidentImgs.Split(','))
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                ImageList.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            try
            {
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    string IncidentImgs = conn.QueryFirstOrDefault<string>("SELECT(SELECT CAST((xxx.FilPath+xxx.AdjunctCode+xxx.FileExName) AS NVARCHAR(MAX))+',' FROM Tb_HSPR_IncidentAdjunct xxx WHERE AdjunctName = '受理图片' AND IncidentID = @IncidentID FOR XML PATH(''))", new { IncidentID });
                    if (!string.IsNullOrEmpty(IncidentImgs))
                    {
                        foreach (var item in IncidentImgs.Split(','))
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                ImageList.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return ImageList;
        }
        /// <summary>
        /// 获取报事历史
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetIncidentList(DataRow row)
        {
            try
            {

                #region 获取参数并简单校验
                string CommID = string.Empty;
                if (row.Table.Columns.Contains("CommID"))
                {
                    CommID = row["CommID"].ToString();
                }
                if (string.IsNullOrEmpty(CommID))
                {
                    return new WxResponse(0, "请选择默认房屋(1001)", null).toJson();
                }
                string CustID = string.Empty;
                if (row.Table.Columns.Contains("CustID"))
                {
                    CustID = row["CustID"].ToString();
                }
                if (string.IsNullOrEmpty(CustID))
                {
                    return new WxResponse(0, "请选择默认房屋(1002)", null).toJson();
                }
                string RoomID = string.Empty;
                if (row.Table.Columns.Contains("RoomID"))
                {
                    RoomID = row["RoomID"].ToString();
                }
                if (string.IsNullOrEmpty(RoomID))
                {
                    return new WxResponse(0, "请选择默认房屋(1003)", null).toJson();
                }
                if (!row.Table.Columns.Contains("Page") || !int.TryParse(row["Page"].ToString(), out int Page))
                {
                    Page = 1;
                }
                if (Page <= 0)
                {
                    Page = 1;
                }
                if (!row.Table.Columns.Contains("Size") || !int.TryParse(row["Size"].ToString(), out int Size))
                {
                    Size = 10;
                }
                if (Size <= 0)
                {
                    Size = 10;
                }
                int Start = (Page - 1) * Size;
                int End = Page * Size;
                #endregion

                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    int Count = conn.QueryFirstOrDefault<int>("SELECT COUNT(IncidentID) FROM view_HSPR_IncidentProcessing_Filter WHERE ISNULL(IsDelete,0) = 0 AND CommID = @CommID AND CustID = @CustID AND RoomID = @RoomID", new { CommID, CustID, RoomID });
                    int PageRes = Count % Size > 0 ? (Count / Size) + 1 : Count / Size;
                    int CountRes = Count;
                    List<dynamic> list = conn.Query("SELECT IncidentID,IncidentContent,ISNULL(DispType,0) AS DispType,ISNULL(DealState,0) AS DealState,State,IncidentReply,IncidentDate  FROM (SELECT *, ROW_NUMBER() OVER(ORDER BY IncidentDate DESC) AS RowId FROM view_HSPR_IncidentProcessing_Filter WHERE ISNULL(IsDelete,0) = 0 AND CommID = @CommID AND CustID = @CustID AND RoomID = @RoomID) AS a WHERE RowId BETWEEN @Start AND @End", new { CommID, CustID, RoomID, Start, End }).ToList();
                    List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();
                    foreach (dynamic item in list)
                    {
                        int DispType = Convert.ToInt32(item.DispType);
                        int DealState = Convert.ToInt32(item.DealState);
                        string IncidentReply = Convert.ToString(item.IncidentReply);
                        string IncidentState = "未分派";
                        if (DispType == 1)
                        {
                            IncidentState = "已分派";
                        }
                        if (DispType == 1 && DealState == 1)
                        {
                            IncidentState = "已完成";
                        }
                        if (DealState == 1 && IncidentReply == "未回访")
                        {
                            IncidentState = "未回访";
                        }

                        if (DealState == 1 && IncidentReply == "已回访")
                        {
                            IncidentState = "已回访";
                        }
                        resultList.Add(new Dictionary<string, object>
                        {
                            {"IncidentID", Convert.ToString(item.IncidentID) },
                            {"IncidentContent", item.IncidentContent },
                            {"IncidentDate", item.IncidentDate },
                            {"IncidentState", IncidentState },
                        });
                    }
                    return new WxResponse(200, "获取成功", new { Data = resultList, Count = CountRes, Page = PageRes }).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常" + ex.Message, null).toJson();
            }
        }

        /// <summary>
        /// 提交报事（公区户内都在此处提交）
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string SubmitIncident(DataRow row)
        {

            try
            {
                #region 获取参数并简单校验
                string CommID = string.Empty;
                if (row.Table.Columns.Contains("CommID"))
                {
                    CommID = row["CommID"].ToString();
                }
                if (string.IsNullOrEmpty(CommID))
                {
                    return new WxResponse(0, "请选择默认房屋(1001)", null).toJson();
                }
                string CustID = string.Empty;
                if (row.Table.Columns.Contains("CustID"))
                {
                    CustID = row["CustID"].ToString();
                }
                if (string.IsNullOrEmpty(CustID))
                {
                    return new WxResponse(0, "请选择默认房屋(1002)", null).toJson();
                }
                string RoomID = string.Empty;
                if (row.Table.Columns.Contains("RoomID"))
                {
                    RoomID = row["RoomID"].ToString();
                }
                if (string.IsNullOrEmpty(RoomID))
                {
                    return new WxResponse(0, "请选择默认房屋(1003)", null).toJson();
                }
                // 0 = 户内，1=公共区域
                if (!row.Table.Columns.Contains("IncidentType") || !int.TryParse(row["IncidentType"].ToString(), out int IncidentType))
                {
                    IncidentType = 0;
                }
                if (IncidentType != 0 && IncidentType != 1)
                {
                    return new WxResponse(0, " 报事类型有误", null).toJson();
                }
                string IncidentContent = string.Empty;
                if (row.Table.Columns.Contains("IncidentContent"))
                {
                    IncidentContent = row["IncidentContent"].ToString();
                }
                if (string.IsNullOrEmpty(IncidentContent))
                {
                    return new WxResponse(0, "问题详情不能为空", null).toJson();
                }
                string IncidentMan = string.Empty;
                if (row.Table.Columns.Contains("IncidentMan"))
                {
                    IncidentMan = row["IncidentMan"].ToString();
                }
                if (string.IsNullOrEmpty(IncidentMan))
                {
                    return new WxResponse(0, " 联系人不能为空", null).toJson();
                }
                string IncidentMobile = string.Empty;
                if (row.Table.Columns.Contains("IncidentMobile"))
                {
                    IncidentMobile = row["IncidentMobile"].ToString();
                }
                if (string.IsNullOrEmpty(IncidentMobile))
                {
                    return new WxResponse(0, " 联系电话不能为空", null).toJson();
                }
                if (!row.Table.Columns.Contains("ReserveDate") || !DateTime.TryParse(row["ReserveDate"].ToString(), out DateTime ReserveDate))
                {
                    return new WxResponse(0, "上门时间格式有误", null).toJson();
                }
                string RegionalID = string.Empty;
                if (row.Table.Columns.Contains("RegionalID"))
                {
                    RegionalID = row["RegionalID"].ToString();
                }
                string Images = string.Empty;
                if (row.Table.Columns.Contains("Images"))
                {
                    Images = row["Images"].ToString();
                }
                string CorpTypeID = string.Empty;
                if (row.Table.Columns.Contains("CorpTypeID"))
                {
                    CorpTypeID = row["CorpTypeID"].ToString();
                }
                // 如果前端传递了报事类别，就以前端报事类别为准（兼容老版本）
                if (string.IsNullOrEmpty(CorpTypeID))
                {
                    if (IncidentType == 0)
                    {
                        CorpTypeID = Global_Fun.AppWebSettings("IncidentIndoorTypeCode");
                    }
                    else
                    {
                        CorpTypeID = Global_Fun.AppWebSettings("IncidentPublicTypeCode");
                    }
                }
                if (string.IsNullOrEmpty(CorpTypeID))
                {
                    return new WxResponse(0, "系统未配置报事类别,请联系物业", null).toJson();
                }
                #endregion

                #region 准备报事数据
                DateTime DateNow = DateTime.Now;
                DateTime IncidentDate = DateNow;
                // 如果当前时间晚于要求处理时间,那么将要求处理时间改为当前时间(要求处理时间不能早于报事时间)
                if (IncidentDate.CompareTo(ReserveDate) > 0)
                {
                    ReserveDate = IncidentDate;
                }
                string Phone = IncidentMobile;
                // 提交时间
                DateTime AdmiDate = DateNow;
                // 公区位置ID，需要配置成默认值
                // 是否删除
                int IsDelete = 0;
                // 报事图片
                string IncidentImgs = Images;
                // 提交人是业主名字
                string AdmiMan = IncidentMan;
                // 存储在WebConfig文件中的预设户内报事类别TypeID
                // 报事区域,公区/户内
                string IncidentPlace = IncidentType == 0 ? "户内" : "公区";
                string Duty = "物业类";
                int DrClass = 1;
                // 报事来源，需要写入配置
                string IncidentSource = Global_Fun.AppWebSettings("IncidentSource");
                if (string.IsNullOrEmpty(IncidentSource))
                {
                    return new WxResponse(0, "系统未配置报事来源,请联系物业", null).toJson();
                }

                // 报事方式,需要写入配置
                string IncidentMode = Global_Fun.AppWebSettings("IncidentMode");
                if (string.IsNullOrEmpty(IncidentSource))
                {
                    return new WxResponse(0, "系统未配置报事方式,请联系物业", null).toJson();
                }
                string TypeID = string.Empty;
                decimal DealLimit = 0.00M;
                decimal DispLimit = 0.00M;
                string BigCorpTypeID;
                string BigCorpTypeCode;
                #region 报事数据校验
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    #region 对应业主房屋身份有效性校验
                    dynamic customer = conn.QueryFirstOrDefault("SELECT * FROM Tb_HSPR_Customer WHERE ISNULL(IsDelete,0) = 0 AND CustID = @CustID", new { CustID });
                    if (null == customer)
                    {
                        return new WxResponse(0, "房屋信息不存在,请联系物业", null).toJson();
                    }
                    dynamic roominfo = conn.QueryFirstOrDefault("SELECT * FROM Tb_HSPR_Room WHERE ISNULL(IsDelete,0) = 0 AND RoomID = @RoomID", new { RoomID });
                    if (null == roominfo)
                    {
                        return new WxResponse(0, "房屋信息不存在,请联系物业", null).toJson();
                    }
                    #endregion
                    #region 报事内容重复校验
                    dynamic info = conn.QueryFirstOrDefault("SELECT * FROM Tb_HSPR_IncidentAccept WHERE CommID = @CommID AND IncidentMan = @IncidentMan AND IncidentContent = @IncidentContent AND Phone = @Phone", new { CommID, IncidentMan, IncidentContent, Phone });
                    if (null != info)
                    {
                        return new WxResponse(200, "提交已成功", null).toJson();
                    }
                    #endregion
                    #region 查询报事类别相关信息
                    // AND ISNULL(IsEnabled, 1) = 1 
                    // 取消了这个限制条件，10.0存储这个字段的时候存反了，后续可能会调整
                    dynamic type = conn.QueryFirstOrDefault<dynamic>("SELECT CorpTypeID,TypeCode,DealLimit,DealLimit2,DispLimit FROM Tb_HSPR_IncidentType WHERE CommID = @CommID AND Duty = @Duty AND CorpTypeID = @CorpTypeID", new { CommID, Duty, CorpTypeID });
                    if (null == type)
                    {
                        return new WxResponse(0, "系统配置报事CorpTypeID有误,请联系物业", null).toJson();
                    }
                    TypeID = Convert.ToString(type.CorpTypeID);
                    BigCorpTypeID = Convert.ToString(type.CorpTypeID);
                    BigCorpTypeCode = Convert.ToString(type.TypeCode);
                    if ("公区".Equals(IncidentPlace))
                    {
                        DealLimit = Convert.ToDecimal(type.DealLimit2);
                    }
                    else
                    {
                        DealLimit = Convert.ToDecimal(type.DealLimit);
                    }
                    DispLimit = Convert.ToDecimal(type.DispLimit);
                    #endregion
                    #region 验证公区位置
                    if ("公共区域".Equals(IncidentPlace))
                    {
                        dynamic IncidentRegional = conn.QueryFirstOrDefault("SELECT * FROM Tb_HSPR_IncidentRegional WHERE ISNULL(IsDelete,0) = 0 AND RegionalID = @RegionalID", new { RegionalID });
                        if (null == IncidentRegional)
                        {
                            return new WxResponse(0, "所选的公区位置不存在,请联系物业", null).toJson();
                        }
                    }
                    #endregion
                }
                #endregion

                string IncidentID = "";
                #endregion

                #region 进行报事
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    conn.Open();
                    var trans = conn.BeginTransaction();
                    try
                    {
                        #region 生成报事编号
                        string IncidentNum = conn.QueryFirstOrDefault<string>("Proc_HSPR_IncidentAssigned_GetCoordinateNum", new { CommID = CommID, IncidentType = 3, IncidentHead = "" }, trans, null, CommandType.StoredProcedure);
                        if (string.IsNullOrEmpty(IncidentNum))
                        {
                            trans.Rollback();
                            return new WxResponse(0, "生成报事编号失败,请重试", null).toJson();
                        }
                        #endregion
                        #region 参数准备
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@IncidentID", IncidentID);
                        parameters.Add("@DrClass", DrClass);
                        parameters.Add("@CommID", CommID);
                        parameters.Add("@CustID", CustID);
                        parameters.Add("@RoomID", RoomID);
                        parameters.Add("@TypeID", TypeID);
                        parameters.Add("@IncidentNum", IncidentNum);
                        parameters.Add("@IncidentPlace", IncidentPlace);
                        parameters.Add("@IncidentMan", IncidentMan);
                        parameters.Add("@IncidentDate", DateNow);
                        parameters.Add("@IncidentMode", IncidentMode);
                        parameters.Add("@DealLimit", DealLimit);

                        parameters.Add("@DispLimit", DispLimit);
                        parameters.Add("@IncidentContent", IncidentContent);
                        parameters.Add("@ReserveDate", ReserveDate);
                        parameters.Add("@ReserveLimit", null);
                        parameters.Add("@Phone", Phone);
                        parameters.Add("@AdmiMan", AdmiMan);
                        parameters.Add("@AdmiDate", AdmiDate);
                        parameters.Add("@IsDelete", IsDelete);
                        parameters.Add("@RegionalID", RegionalID);
                        parameters.Add("@DispType", 0);
                        parameters.Add("@IncidentImgs", IncidentImgs);
                        parameters.Add("@Duty", Duty);

                        parameters.Add("@IncidentSource", IncidentSource);
                        parameters.Add("@BigCorpTypeID", BigCorpTypeID);
                        parameters.Add("@BigCorpTypeCode", BigCorpTypeCode);
                        parameters.Add("@FineCorpTypeID", null);
                        parameters.Add("@FineCorpTypeCode", null);
                        parameters.Add("@RatedWorkHour", null);
                        parameters.Add("@RatedWorkHourNumber", null);
                        parameters.Add("@KPIRatio", 0);
                        parameters.Add("@EqId", null);
                        parameters.Add("@IsTouSu", 0);
                        parameters.Add("@TaskEqId", null);
                        parameters.Add("@IsSupplementary", null);
                        parameters.Add("@AdmiManUserCode", null);
                        parameters.Add("@NIncidentID", null, DbType.Int64, ParameterDirection.Output);
                        #endregion
                        if (conn.Execute("proc_hspr_IncidentAccept_InsertJH", parameters, trans, null, CommandType.StoredProcedure) <= 0)
                        {
                            trans.Rollback();
                            return new WxResponse(0, "报事失败,请重试", null).toJson();
                        }
                        trans.Commit();
                        IncidentAcceptPush.SynchPushIndoorIncident_wx(erpConnStr, CorpID, IncidentID);
                        return new WxResponse(200, "提交成功", null).toJson();
                    }
                    catch (Exception ex)
                    {
                        GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                        trans.Rollback();
                        return new WxResponse(0, "报事失败,请重试", null).toJson();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        /// <summary>
        /// 上传单个图片
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string UploadImage(DataRow row)
        {
            try
            {
                if (null == httpContext)
                {
                    return new WxResponse(0, "请求异常", null).toJson();
                }
                HttpFileCollection files = httpContext.Request.Files;
                if (httpContext.Request.Files.Count == 0)
                {
                    return new WxResponse(0, "请上传附件", null).toJson();
                }
                string IncidentAcceptImageSavePath = Global_Fun.AppWebSettings("IncidentAcceptImageSavePath");
                if (!Directory.Exists(IncidentAcceptImageSavePath))
                {
                    Directory.CreateDirectory(IncidentAcceptImageSavePath);
                }
                HttpPostedFile file = files[0];
                //文件名规则(4位随机数字组合+当前时间)
                string fileName = GetRandomCode(4) + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
                string filePath = IncidentAcceptImageSavePath;
                file.SaveAs(filePath + fileName);
                string IncidentAcceptImageUrl = Global_Fun.AppWebSettings("IncidentAcceptImageUrl");
                string fileUrl = IncidentAcceptImageUrl + fileName;
                return new WxResponse(200, "上传成功", fileUrl).toJson();
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }


        /// <summary>
        /// 获取报事配置信息（后续可能会考虑把默认报事类别做到数据库）
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetIncidentConfig(DataRow row)
        {
            try
            {

                #region 获取参数并简单校验
                string CommID = string.Empty;
                if (row.Table.Columns.Contains("CommID"))
                {
                    CommID = row["CommID"].ToString();
                }
                #endregion
                Dictionary<string, object> result = new Dictionary<string, object>();
                #region 查询报事公区位置信息
                List<Dictionary<string, string>> IncidentRegionalList = new List<Dictionary<string, string>>();
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    List<dynamic> list = conn.Query("SELECT RegionalID,RegionalPlace FROM Tb_HSPR_IncidentRegional WHERE ISNULL(IsDelete,0) = 0 AND CommID = @CommID", new { CommID }).ToList();
                    foreach (var item in list)
                    {
                        IncidentRegionalList.Add(new Dictionary<string, string>
                        {
                            {"RegionalID", Convert.ToString(item.RegionalID) },
                            {"RegionalPlace", Convert.ToString(item.RegionalPlace) }
                        });
                    }
                }
                result.Add("IncidentRegionalList", IncidentRegionalList);
                #endregion
                return new WxResponse(200, "获取成功", result).toJson();
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }

        #endregion

        #region 支付页面

        private static object syncLocker = new object();
        #region 银联全民付
        private string CreateOrder_UnionQmfPay(DataRow row)
        {
            try
            {
                #region 获取参数
                string OpenID = string.Empty;
                if (row.Table.Columns.Contains("OpenID"))
                {
                    OpenID = row["OpenID"].ToString();
                }
                string MsgType = string.Empty;
                if (row.Table.Columns.Contains("MsgType"))
                {
                    MsgType = row["MsgType"].ToString();
                }
                if (!row.Table.Columns.Contains("CommID") || !int.TryParse(row["CommID"].ToString(), out int CommID))
                {
                    CommID = 0;
                }
                if (!row.Table.Columns.Contains("CustID") || !long.TryParse(row["CustID"].ToString(), out long CustID))
                {
                    CustID = 0;
                }
                if (!row.Table.Columns.Contains("RoomID") || !long.TryParse(row["RoomID"].ToString(), out long RoomID))
                {
                    RoomID = 0;
                }
                #endregion
                #region 计算金额
                if (!row.Table.Columns.Contains("PayData") || string.IsNullOrEmpty(row["PayData"].ToString()))
                {
                    return new WxResponse(0, "缺少参数PayData", null).toJson();
                }
                string PayData = row["PayData"].ToString();
                if (!CheckPayData(Global_Fun.BurstConnectionString(CommID, Global_Fun.BURST_TYPE_CHARGE, true), CustID, RoomID, PayData, out decimal Amt, out string errMsg, true, false, true))
                {
                    return new WxResponse(0, errMsg, null).toJson();
                }
                if (Amt <= 0.00M)
                {
                    return new WxResponse(0, "金额必须大于0", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString),
                        erpConn = new SqlConnection(erpConnStr))
                {
                    dynamic tb_Payment_Config = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID", new { CommID });
                    if (null == tb_Payment_Config)
                    {
                        return new WxResponse(0, "该项目未开通对应支付方式", null).toJson();
                    }
                    #region 读取支付配置
                    UnionQmfConfig unionQmfConfig;
                    try
                    {
                        unionQmfConfig = JsonConvert.DeserializeObject<UnionQmfConfig>(tb_Payment_Config.Config);
                        if (null == unionQmfConfig)
                        {
                            return new WxResponse(0, "该项目支付类型对应配置有误", null).toJson();
                        }
                    }
                    catch (Exception)
                    {
                        return new WxResponse(0, "该项目支付类型对应配置有误", null).toJson();
                    }
                    #endregion
                    #region 创建订单
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
                    if (conn.Execute("INSERT INTO Tb_Notice(Id, CommID, RoomID, CustID, PayData, CreateTime) VALUES(@Id, @CommID, @RoomID, @CustID, @PayData, @CreateTime)", new { Id = NoticeId, CommID, RoomID, CustID, PayData, CreateTime = DateTime.Now.ToString() }) <= 0)
                    {
                        return new WxResponse(0, "创建收款订单失败,请重试", null).toJson();
                    }
                    string ChargeMode = "WXPay.jsPay".Equals(MsgType) ? "微信公众号支付" : "支付宝服务窗支付";
                    #region 修改收款方式
                    if (conn.QueryFirstOrDefault<int>("SELECT COUNT(1) FROM syscolumns WHERE id=object_id('Tb_Notice') AND name = 'ChargeMode'") > 0)
                    {
                        conn.Execute("UPDATE Tb_Notice SET ChargeMode = @ChargeMode WHERE Id = @Id", new { ChargeMode, Id = NoticeId });
                    }
                    #endregion
                    #region 请求银联全民付微信支付
                    DateTime dateNow = DateTime.Now;
                    string OrderSN = unionQmfConfig.SysId + dateNow.ToString("yyyyMMddHHmmssfff") + Utils.BuildRandomStr(3);

                    #region 获取对应类型的下账地址
                    string PaymentNotifyUrl = AppGlobal.GetAppSetting("UnionQmfPay_Notify_Url") + "?CommID=" + CommID;

                    // Back = 0时，停留在当前页面
                    string PaymentReturnUrl = Domain + "/payment/query?Back=" + ("WXPay.jsPay".Equals(MsgType) ? "-2" : "-1") + "&OrderSN=" + OrderSN;
                    #endregion
                    UnionPaySdk unionPaySdk = new UnionPaySdk();
                    if (!unionPaySdk.RequestPayOrder(unionQmfConfig.MsgSrc, MsgType, unionQmfConfig.Mid, unionQmfConfig.Tid, unionQmfConfig.InstMid, unionQmfConfig.SignKey, Convert.ToInt32(Amt * 100), OrderSN, out string orderInfo, PaymentNotifyUrl, "", true, OpenID, PaymentReturnUrl, FeesMemo))
                    {
                        return new WxResponse(0, "创建订单失败", orderInfo).toJson();
                    }
                    // 支付宝PayType = 1，微信PayType=2
                    if (conn.Execute("INSERT INTO Tb_Payment_Order(PayType, OrderSN, NoticeId, Amt, CreateTime) VALUES(@PayType, @OrderSN, @NoticeId, @Amt, @CreateTime)", new { PayType = "WXPay.jsPay".Equals(MsgType) ? 2 : 1, OrderSN, NoticeId = NoticeId, Amt = Amt, CreateTime = dateNow }) <= 0)
                    {
                        return new WxResponse(0, "创建订单失败", orderInfo).toJson();
                    }
                    JObject jObj = new JObject();
                    jObj.Add("OrderSN", OrderSN);
                    jObj.Add("OrderInfo", orderInfo);
                    return new WxResponse(200, "创建订单成功", jObj).toJson();
                    #endregion
                    #endregion
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        private string QueryOrder_UnionQmfPay(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string OrderSN = string.Empty;
                if (row.Table.Columns.Contains("OrderSN"))
                {
                    OrderSN = row["OrderSN"].ToString();
                }
                if (string.IsNullOrEmpty(OrderSN))
                {
                    return new WxResponse(101, "支付订单不存在", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString))
                {
                    Tb_Payment_Order tb_Payment_Order = conn.QueryFirstOrDefault<Tb_Payment_Order>("SELECT * FROM Tb_Payment_Order WITH(NOLOCK) WHERE OrderSN = @OrderSN", new { OrderSN });
                    if (null == tb_Payment_Order)
                    {
                        return new WxResponse(101, "支付订单不存在", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 5)
                    {
                        return new WxResponse(101, "订单超时未支付", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 4)
                    {
                        return new WxResponse(101, "下账失败,请联系物业核实", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 3)
                    {
                        return new WxResponse(200, "下账成功", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 2)
                    {
                        return new WxResponse(101, "下账失败,支付数据异常,请联系物业核实", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 1)
                    {
                        return new WxResponse(101, "下账失败,支付数据异常,请联系物业核实", null).toJson();
                    }
                    // 未下账需要实时查一下状态
                    if (tb_Payment_Order.IsSucc == 0)
                    {
                        #region 查询通知单是否存在
                        Tb_Notice tb_Notice = conn.QueryFirstOrDefault<Tb_Notice>("SELECT * FROM Tb_Notice WITH(NOLOCK) WHERE Id = @Id", new { Id = tb_Payment_Order.NoticeId });
                        if (null == tb_Notice)
                        {
                            return new WxResponse(101, "通知单不存在", null).toJson();
                        }
                        #endregion
                        #region 查询支付配置是否存在并解析
                        dynamic tb_Payment_Config = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID", new { tb_Notice.CommID });
                        if (null == tb_Payment_Config)
                        {
                            return new WxResponse(101, "支付配置不存在", null).toJson();
                        }
                        #region 读取支付配置
                        UnionQmfConfig unionQmfConfig;
                        try
                        {
                            unionQmfConfig = JsonConvert.DeserializeObject<UnionQmfConfig>(tb_Payment_Config.Config);
                            if (null == unionQmfConfig)
                            {
                                return new WxResponse(101, "该项目支付类型对应配置有误", null).toJson();
                            }
                        }
                        catch (Exception)
                        {
                            return new WxResponse(101, "该项目支付类型对应配置有误", null).toJson();
                        }
                        #endregion
                        #endregion
                        #region 查询订单状态
                        try
                        {
                            UnionPaySdk unionPaySdk = new UnionPaySdk();
                            if (!unionPaySdk.QueryOrder(unionQmfConfig.MsgSrc, unionQmfConfig.Mid, unionQmfConfig.Tid, unionQmfConfig.InstMid, unionQmfConfig.SignKey, out string orderinfo, tb_Payment_Order.OrderSN))
                            {
                                Business.PubInfo.GetLog().Debug($"主动查询下账:查询失败({orderinfo})");
                                return new WxResponse(0, "请求查询失败", null).toJson();
                            }
                            Dictionary<string, object> resParam = JsonConvert.DeserializeObject<Dictionary<string, object>>(orderinfo);
                            if (!ReceFee_UnionQmfPay(Global_Fun.BurstConnectionString(tb_Notice.CommID, Global_Fun.BURST_TYPE_CHARGE, true), PubConstant.WChat2020ConnectionString, tb_Notice, tb_Payment_Order, resParam, out string msg))
                            {
                                return new WxResponse(0, "下账失败", msg).toJson();
                            }
                            return new WxResponse(200, "下账成功", null).toJson();
                        }
                        catch (Exception ex)
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:查询报错({ex.Message})");
                            return new WxResponse(0, "查询失败", null).toJson();
                        }
                        #endregion
                    }
                    return new WxResponse(101, "支付状态异常", null).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        public static bool ReceFee_UnionQmfPay(string erpConnStr, string wchat2020ConnStr, Tb_Notice tb_Notice, Tb_Payment_Order tb_Payment_Order, Dictionary<string, object> resParam, out string msg)
        {
            bool ChargeLateFee = true;
            lock (syncLocker)
            {
                try
                {
                    using (IDbConnection conn = new SqlConnection(wchat2020ConnStr))
                    {
                        // 因为外部存在订单状态已被更改的情况，所以此处我们再查询一次
                        tb_Payment_Order = conn.QueryFirstOrDefault<Tb_Payment_Order>("SELECT * FROM Tb_Payment_Order WITH(NOLOCK) WHERE OrderSN = @OrderSN", new { OrderSN = tb_Payment_Order.OrderSN });
                        if (null == tb_Payment_Order)
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:订单不存在");
                            msg = "订单不存在";
                            return false;
                        }
                        string status = resParam.ContainsKey("status") ? resParam["status"].ToString() : "";
                        string payTime = resParam.ContainsKey("payTime") ? resParam["payTime"].ToString() : DateTime.Now.ToString();
                        if (!DateTime.TryParse(payTime, out DateTime PayDateTime))
                        {
                            PayDateTime = DateTime.Now;
                        }

                        // 如果交易已关闭标记订单状态为5，已超时,如果订单为等待支付，并且payTime超过了30分钟，标记为5已超时
                        if ("TRADE_CLOSED".Equals(status) || ("WAIT_BUYER_PAY".Equals(status) && DateTime.Now.AddMinutes(-30).CompareTo(PayDateTime) > 0))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:订单已关闭,视为未支付");
                            tb_Payment_Order.IsSucc = 5;
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "订单超时未支付";
                            conn.Update(tb_Payment_Order);
                            msg = "订单超时未支付";
                            return false;
                        }
                        // 等待交易结果
                        if (!"TRADE_SUCCESS".Equals(status))
                        {
                            msg = "交易处理中";
                            return false;
                        }
                        #region 返回参数判断并进行下账参数
                        if (!resParam.ContainsKey("totalAmount"))
                        {
                            tb_Payment_Order.IsSucc = 1;
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "下账通知不包含totalAmount";
                            conn.Update(tb_Payment_Order);
                            Business.PubInfo.GetLog().Error("下账通知不包含totalAmount:" + JsonConvert.SerializeObject(resParam));
                            msg = "下账通知不包含totalAmount";
                            return false;
                        }
                        long total_fee = Convert.ToInt64(resParam["totalAmount"].ToString());

                        if (total_fee != Convert.ToInt64(tb_Payment_Order.Amt * 100))
                        {
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "支付金额与订单金额不相等";
                            conn.Update(tb_Payment_Order);
                            Business.PubInfo.GetLog().Error("支付金额与订单金额不相等:" + JsonConvert.SerializeObject(resParam));
                            msg = "支付金额与订单金额不相等";
                            return false;
                        }
                        if (!Business.PubInfo.CheckPayData(erpConnStr, tb_Notice.CustID, tb_Notice.RoomID, tb_Notice.PayData, out decimal Amt, out string errMsg, false, false, ChargeLateFee))
                        {
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "CheckPayData失败(" + errMsg + ")";
                            conn.Update(tb_Payment_Order);
                            Business.PubInfo.GetLog().Error("CheckPayData失败(" + errMsg + "):" + JsonConvert.SerializeObject(resParam));
                            msg = "CheckPayData失败(" + errMsg + ")";
                            return false;
                        }
                        JObject PayData = JObject.Parse(tb_Notice.PayData);
                        int Type = (int)PayData["Type"];
                        if (Type == 1)
                        {
                            JArray Data = (JArray)PayData["Data"];
                            List<string> FeesIDList = new List<string>();
                            foreach (JObject item in Data)
                            {
                                FeesIDList.Add(item["FeesId"].ToString());
                            }
                            if (Business.PubInfo.ReceFees(erpConnStr, out long ReceID, Convert.ToString(tb_Notice.CommID), Convert.ToString(tb_Notice.CustID), Convert.ToString(tb_Notice.RoomID), string.Join(",", FeesIDList.ToArray()), 0.00M, tb_Notice.ChargeMode, tb_Payment_Order.OrderSN, tb_Notice.CreateUser, ChargeLateFee ? 1 : 0))
                            {
                                tb_Payment_Order.IsSucc = 3;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = Convert.ToString(ReceID);
                                conn.Update(tb_Payment_Order);
                                msg = "下账成功";
                                return true;
                            }
                            else
                            {
                                tb_Payment_Order.IsSucc = 4;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = "下账失败";
                                conn.Update(tb_Payment_Order);
                                msg = "下账失败";
                                return true;
                            }

                        }
                        else
                        {
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "下账失败(订单支付信息有误)";
                            conn.Update(tb_Payment_Order);
                            msg = "订单支付信息有误";
                            return false;
                        }

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    Business.PubInfo.GetLog().Error("ReceFees抛出了一个异常:", ex);
                    msg = "下账异常";
                    return false;
                }
            }
        }
        #endregion

        #region 威富通支付
        private string CreateOrder_WftPay(DataRow row)
        {
            try
            {
                #region 获取参数
                string OpenID = string.Empty;
                if (row.Table.Columns.Contains("OpenID"))
                {
                    OpenID = row["OpenID"].ToString();
                }
                if (string.IsNullOrEmpty(OpenID))
                {
                    return new WxResponse(0, "用户未授权", null).toJson();
                }
                if (!row.Table.Columns.Contains("CommID") || !int.TryParse(row["CommID"].ToString(), out int CommID))
                {
                    CommID = 0;
                }
                if (!row.Table.Columns.Contains("CustID") || !long.TryParse(row["CustID"].ToString(), out long CustID))
                {
                    CustID = 0;
                }
                if (!row.Table.Columns.Contains("RoomID") || !long.TryParse(row["RoomID"].ToString(), out long RoomID))
                {
                    RoomID = 0;
                }
                #endregion
                #region 计算金额
                if (!row.Table.Columns.Contains("PayData") || string.IsNullOrEmpty(row["PayData"].ToString()))
                {
                    return new WxResponse(0, "缺少参数PayData", null).toJson();
                }
                string PayData = row["PayData"].ToString();
                if (!CheckPayData(Global_Fun.BurstConnectionString(CommID, Global_Fun.BURST_TYPE_CHARGE, true), CustID, RoomID, PayData, out decimal Amt, out string errMsg, true, false, true))
                {
                    return new WxResponse(0, errMsg, null).toJson();
                }
                if (Amt <= 0.00M)
                {
                    return new WxResponse(0, "金额必须大于0", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString),
                    erpConn = new SqlConnection(erpConnStr))
                {
                    dynamic tb_Payment_Config = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID", new { CommID });
                    if (null == tb_Payment_Config)
                    {
                        return new WxResponse(0, "该项目未开通对应支付方式", null).toJson();
                    }
                    #region 读取支付配置
                    WftConfig wftConfig;
                    try
                    {
                        wftConfig = JsonConvert.DeserializeObject<WftConfig>(tb_Payment_Config.Config);
                        if (null == wftConfig)
                        {
                            return new WxResponse(0, "该项目支付类型对应配置有误", null).toJson();
                        }
                    }
                    catch (Exception)
                    {
                        return new WxResponse(0, "该项目支付类型对应配置有误", null).toJson();
                    }
                    #endregion
                    #region 创建订单
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
                    if (conn.Execute("INSERT INTO Tb_Notice(Id, CommID, RoomID, CustID, PayData, CreateTime) VALUES(@Id, @CommID, @RoomID, @CustID, @PayData, @CreateTime)", new { Id = NoticeId, CommID, RoomID, CustID, PayData, CreateTime = DateTime.Now.ToString() }) <= 0)
                    {
                        return new WxResponse(0, "创建收款订单失败,请重试", null).toJson();
                    }
                    string ChargeMode = "微信公众号支付";
                    #region 修改收款方式
                    if (conn.QueryFirstOrDefault<int>("SELECT COUNT(1) FROM syscolumns WHERE id=object_id('Tb_Notice') AND name = 'ChargeMode'") > 0)
                    {
                        conn.Execute("UPDATE Tb_Notice SET ChargeMode = @ChargeMode WHERE Id = @Id", new { ChargeMode, Id = NoticeId });
                    }
                    #endregion
                    #region 请求威富通微信支付
                    DateTime dateNow = DateTime.Now;
                    string OrderSN = dateNow.ToString("yyyyMMddHHmmssfff") + Utils.BuildRandomStr(3);

                    #region 获取对应类型的下账地址
                    string PaymentNotifyUrl = AppGlobal.GetAppSetting("WftPay_Notify_Url") + "?CommID=" + CommID;
                    #endregion

                    //把金额转换为分
                    //商户订单号,当前10位时间戳+16位随机字符
                    RequestHandler reqHandler = new RequestHandler(null);
                    reqHandler.setGateUrl("https://pay.swiftpass.cn/pay/gateway");
                    reqHandler.setParameter("service", "pay.weixin.jspay");
                    reqHandler.setParameter("mch_id", wftConfig.MerchantID);
                    reqHandler.setParameter("is_raw", "1");
                    reqHandler.setParameter("out_trade_no", OrderSN);
                    reqHandler.setParameter("body", FeesMemo);
                    reqHandler.setParameter("sub_openid", OpenID);
                    reqHandler.setParameter("sub_appid", tb_Corp_Config.AppID);
                    reqHandler.setParameter("attach", NoticeId);
                    reqHandler.setParameter("total_fee", Convert.ToString(Convert.ToInt64(Amt * 100)));
                    reqHandler.setParameter("mch_create_ip", AppGlobal.GetAppSetting("WftPayCreateIP"));//终端IP
                    reqHandler.setParameter("notify_url", PaymentNotifyUrl);

                    // 限制订单有效期30分钟
                    reqHandler.setParameter("time_start", dateNow.ToString("yyyyMMddHHmmss"));
                    reqHandler.setParameter("time_expire", dateNow.AddMinutes(30).ToString("yyyyMMddHHmmss"));
                    reqHandler.setParameter("nonce_str", Utils.random());
                    reqHandler.setKey(wftConfig.SignKey);
                    reqHandler.createSign();
                    string data = Utils.toXml(reqHandler.getAllParameters());//生成XML报文
                    Dictionary<string, string> reqContent = new Dictionary<string, string>
                    {
                        { "url", reqHandler.getGateUrl() },
                        { "data", data }
                    };
                    PayHttpClient pay = new PayHttpClient();
                    pay.setReqContent(reqContent);
                    if (!pay.call())
                    {
                        return new WxResponse(0, "签名校验错误", pay.getErrInfo()).toJson();
                    }
                    ClientResponseHandler resHandler = new ClientResponseHandler();
                    resHandler.setContent(pay.getResContent());
                    resHandler.setKey(wftConfig.SignKey);
                    Hashtable param = resHandler.getAllParameters();
                    if (!resHandler.isTenpaySign())
                    {
                        if (param.ContainsKey("message"))
                        {
                            return new WxResponse(0, "签名校验错误", param["message"].ToString()).toJson();
                        }
                        return new WxResponse(0, "签名校验错误", null).toJson();
                    }
                    if (!param.ContainsKey("status"))
                    {
                        return new WxResponse(0, "生成支付订单失败", param["message"].ToString()).toJson();
                    }
                    //当返回状态与业务结果都为0时才返回，其它结果请查看接口文档
                    if (int.Parse(param["status"].ToString()) != 0 || int.Parse(param["result_code"].ToString()) != 0)
                    {
                        return new WxResponse(0, "生成支付订单失败", param["err_msg"].ToString()).toJson();
                    }
                    if (conn.Execute("INSERT INTO Tb_Payment_Order(PayType, OrderSN, NoticeId, Amt, CreateTime) VALUES(@PayType, @OrderSN, @NoticeId, @Amt, @CreateTime)", new { PayType = 2, OrderSN, NoticeId = NoticeId, Amt = Amt, CreateTime = dateNow }) <= 0)
                    {
                        return new WxResponse(0, "创建订单失败", param["pay_info"].ToString()).toJson();
                    }
                    JObject jObj = JObject.Parse(param["pay_info"].ToString());
                    if (null == jObj)
                    {
                        return new WxResponse(0, "创建订单失败", param["pay_info"].ToString()).toJson();
                    }
                    jObj.Add("OrderSN", OrderSN);
                    return new WxResponse(200, "创建订单成功", jObj).toJson();
                    #endregion
                    #endregion
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        private string QueryOrder_WftPay(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string OrderSN = string.Empty;
                if (row.Table.Columns.Contains("OrderSN"))
                {
                    OrderSN = row["OrderSN"].ToString();
                }
                if (string.IsNullOrEmpty(OrderSN))
                {
                    return new WxResponse(101, "支付订单不存在", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString))
                {
                    Tb_Payment_Order tb_Payment_Order = conn.QueryFirstOrDefault<Tb_Payment_Order>("SELECT * FROM Tb_Payment_Order WITH(NOLOCK) WHERE OrderSN = @OrderSN", new { OrderSN });
                    if (null == tb_Payment_Order)
                    {
                        return new WxResponse(101, "支付订单不存在", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 5)
                    {
                        return new WxResponse(101, "订单超时未支付", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 4)
                    {
                        return new WxResponse(101, "下账失败,请联系物业核实", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 3)
                    {
                        return new WxResponse(200, "下账成功", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 2)
                    {
                        return new WxResponse(101, "下账失败,支付数据异常,请联系物业核实", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 1)
                    {
                        return new WxResponse(101, "下账失败,支付数据异常,请联系物业核实", null).toJson();
                    }
                    // 未下账需要实时查一下状态
                    if (tb_Payment_Order.IsSucc == 0)
                    {
                        #region 查询通知单是否存在
                        Tb_Notice tb_Notice = conn.QueryFirstOrDefault<Tb_Notice>("SELECT * FROM Tb_Notice WITH(NOLOCK) WHERE Id = @Id", new { Id = tb_Payment_Order.NoticeId });
                        if (null == tb_Notice)
                        {
                            return new WxResponse(101, "通知单不存在", null).toJson();
                        }
                        #endregion
                        #region 查询支付配置是否存在并解析
                        dynamic tb_Payment_Config = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID", new { tb_Notice.CommID });
                        if (null == tb_Payment_Config)
                        {
                            return new WxResponse(101, "支付配置不存在", null).toJson();
                        }
                        WftConfig wftConfig = null;
                        try
                        {
                            wftConfig = JsonConvert.DeserializeObject<WftConfig>(tb_Payment_Config.Config);
                            if (null == wftConfig)
                            {
                                return new WxResponse(101, "支付配置有误", null).toJson();
                            }
                        }
                        catch (Exception)
                        {
                            return new WxResponse(101, "支付配置有误", null).toJson();
                        }
                        #endregion
                        #region 查询订单状态
                        try
                        {
                            RequestHandler reqHandler = new RequestHandler(null);
                            reqHandler.setGateUrl("https://pay.swiftpass.cn/pay/gateway");
                            reqHandler.setParameter("service", "unified.trade.query");
                            reqHandler.setParameter("mch_id", wftConfig.MerchantID);
                            reqHandler.setParameter("out_trade_no", tb_Payment_Order.OrderSN);
                            reqHandler.setParameter("nonce_str", Utils.random());
                            reqHandler.setKey(wftConfig.SignKey);
                            reqHandler.createSign();
                            string data = Utils.toXml(reqHandler.getAllParameters());//生成XML报文
                            Dictionary<string, string> reqContent = new Dictionary<string, string>
                    {
                        { "url", reqHandler.getGateUrl() },
                        { "data", data }
                    };
                            PayHttpClient pay = new PayHttpClient();
                            pay.setReqContent(reqContent);
                            if (!pay.call())
                            {
                                return new WxResponse(0, "请求查询失败", null).toJson();
                            }
                            ClientResponseHandler resHandler = new ClientResponseHandler();
                            resHandler.setContent(pay.getResContent());
                            resHandler.setKey(wftConfig.SignKey);
                            Hashtable resParam = resHandler.getAllParameters();
                            GetLog().Info("查询信息：" + JsonConvert.SerializeObject(resParam));
                            if (!resHandler.isTenpaySign())
                            {
                                if (resParam.ContainsKey("message"))
                                {
                                    return new WxResponse(0, "签名错误", resParam["message"]).toJson();
                                }
                                return new WxResponse(0, "签名错误", null).toJson();
                            }
                            if (!resParam.ContainsKey("status"))
                            {
                                return new WxResponse(0, "请求失败", resParam["message"]).toJson();
                            }
                            if (!ReceFee_WftPay(Global_Fun.BurstConnectionString(tb_Notice.CommID, Global_Fun.BURST_TYPE_CHARGE, true), PubConstant.WChat2020ConnectionString, tb_Notice, tb_Payment_Order, resParam, out string msg))
                            {
                                return new WxResponse(0, "下账失败", msg).toJson();
                            }
                            return new WxResponse(200, "下账成功", null).toJson();
                        }
                        catch (Exception ex)
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:查询报错({ex.Message})");
                            return new WxResponse(0, "查询失败", null).toJson();
                        }
                        #endregion
                    }
                    return new WxResponse(101, "支付状态异常", null).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        /// <summary>
        /// 升龙专用支付成功推送消息
        /// </summary>
        /// <param name="result"></param>
        /// <param name="openid"></param>
        private static void SendMpTemplateMessageBy1824(string erpConnStr, Tb_Notice tb_Notice, Tb_Payment_Order tb_Payment_Order, string openid)
        {
            Task.Run(() =>
            {
                #region 查询项目名称和房屋编号,拼接费用备注
                string FeesMemo = string.Empty;
                string RoomSign = string.Empty;
                string CommName = string.Empty;
                Tb_Corp_Config tb_Corp_Config = null;
                using (IDbConnection erpConn = new SqlConnection(erpConnStr),
                    conn = new SqlConnection(PubConstant.WChat2020ConnectionString))
                {
                    string CorpID = erpConn.QueryFirstOrDefault<string>("SELECT CorpID FROM Tb_HSPR_Community WHERE CommID = @CommID", new { tb_Notice.CommID });
                    if ("1824".Equals(CorpID))
                    {
                        return;
                    }
                    tb_Corp_Config = conn.QueryFirstOrDefault<Tb_Corp_Config>("SELECT * FROM Tb_Corp_Config WHERE CorpID = @CorpID", new { CorpID });
                    if (null == tb_Corp_Config)
                    {
                        return;
                    }
                    JObject PayDataObj = JObject.Parse(tb_Notice.PayData);
                    int Type = (int)PayDataObj["Type"];
                    if (Type == 1)
                    {
                        FeesMemo = "物业综合费用缴纳";
                    }
                    else
                    {
                        FeesMemo = "物业综合费用预存";
                    }
                    CommName = erpConn.QueryFirstOrDefault<string>("SELECT CommName FROM Tb_HSPR_Community WHERE CommID = @CommID", new { tb_Notice.CommID });
                    if (string.IsNullOrEmpty(CommName))
                    {
                        CommName = Convert.ToString(tb_Notice.CommID);
                    }
                    RoomSign = erpConn.QueryFirstOrDefault<string>("SELECT ISNULL(RoomSign,RoomName) AS RoomSign FROM Tb_HSPR_Room WHERE RoomID = @RoomID", new { tb_Notice.RoomID });
                    if (string.IsNullOrEmpty(RoomSign))
                    {
                        RoomSign = Convert.ToString(tb_Notice.RoomID);
                    }
                    FeesMemo += string.Format("-{0}-{1}", CommName, RoomSign);
                }
                #endregion

                WxCache wxCache = new WxCache(RedisHelper.RedisClient.GetDatabase(), GetLog());
                string access_token = wxCache.GetComponentApiAccessToken(WxPlatformSetting.WxPlatformAppID, WxPlatformSetting.WxPlatformAppSecret, tb_Corp_Config.AppID);
                if (string.IsNullOrEmpty(access_token))
                {
                    GetLog().Debug("发送支付推送消息失败，获取AccessToken失败");
                    return;
                }
                Dictionary<string, object> PostData = new Dictionary<string, object>();
                PostData.Add("touser", openid);
                PostData.Add("template_id", "3NirnVK091HDK-oB5W-qqtoTBmZpR32nZD0tGvUQyV0");
                PostData.Add("data", new
                {
                    first = new { value = "您好，本次缴费成功" },
                    keyword1 = new { value = $"{tb_Payment_Order.OrderSN}" },
                    keyword2 = new { value = $"{CommName}" },
                    keyword3 = new { value = $"{RoomSign}" },
                    keyword4 = new { value = $"{FeesMemo}" },
                    keyword5 = new { value = $"{tb_Payment_Order.SAmt}" },
                    remark = new { value = "感谢您对我们的大力支持。" }
                });
                WxPlatform.SendMpTemplateMessage(access_token, JsonConvert.SerializeObject(PostData), out string msg);
            });
        }

        public static bool ReceFee_WftPay(string erpConnStr, string wchat2020ConnStr, Tb_Notice tb_Notice, Tb_Payment_Order tb_Payment_Order, Hashtable resParam, out string msg)
        {
            bool ChargeLateFee = true;
            lock (syncLocker)
            {
                try
                {
                    using (IDbConnection conn = new SqlConnection(wchat2020ConnStr))
                    {
                        // 因为外部存在订单状态已被更改的情况，所以此处我们再查询一次
                        tb_Payment_Order = conn.QueryFirstOrDefault<Tb_Payment_Order>("SELECT * FROM Tb_Payment_Order WITH(NOLOCK) WHERE OrderSN = @OrderSN", new { OrderSN = tb_Payment_Order.OrderSN });
                        if (null == tb_Payment_Order)
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:订单不存在");
                            msg = "订单不存在";
                            return false;
                        }
                        // 如果不存在trxstatus字段或者trxstatus字段为空，说明交易中，跳过
                        string status = resParam.ContainsKey("status") ? resParam["status"].ToString() : "";
                        string result_code = resParam.ContainsKey("result_code") ? resParam["result_code"].ToString() : "";
                        if (!"0".Equals(status) || !"0".Equals(result_code))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:status或者result_code不等于0({JsonConvert.SerializeObject(resParam)})");
                            msg = "交易状态异常";
                            return false;
                        }
                        string pay_result = string.Empty;
                        string pay_info = string.Empty;
                        if (resParam.ContainsKey("trade_state"))
                        {
                            pay_result = "SUCCESS".Equals(resParam["trade_state"].ToString()) ? "0" : "";
                            if (resParam.ContainsKey("trade_state_desc"))
                            {
                                pay_info = resParam["trade_state_desc"].ToString();
                            }
                        }
                        if (resParam.ContainsKey("pay_result"))
                        {
                            pay_result = resParam["pay_result"].ToString();
                            if (resParam.ContainsKey("pay_info"))
                            {
                                pay_info = resParam["pay_info"].ToString();
                            }
                        }
                        // 如果交易失败（trxstatus!=0000,2000,2008），标记订单状态为5，已超时
                        if (!"0".Equals(pay_result))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:交易失败,视为未支付");
                            tb_Payment_Order.IsSucc = 5;
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = pay_info;
                            conn.Update(tb_Payment_Order);
                            msg = pay_info;
                            return false;
                        }
                        // 交易成功，处理下账
                        #region 返回参数判断并进行下账参数
                        string openid = resParam.ContainsKey("openid") ? resParam["openid"].ToString() : "";
                        long total_fee = Convert.ToInt64(resParam.ContainsKey("total_fee") ? resParam["total_fee"].ToString() : "0");
                        if (total_fee != Convert.ToInt64(tb_Payment_Order.Amt * 100))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:支付金额与订单金额不相等({JsonConvert.SerializeObject(resParam)})");
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "支付金额与订单金额不相等";
                            conn.Update(tb_Payment_Order);
                            msg = "支付金额与订单金额不一致";
                            return false;
                        }
                        if (!Business.PubInfo.CheckPayData(erpConnStr, tb_Notice.CustID, tb_Notice.RoomID, tb_Notice.PayData, out decimal Amt, out string errMsg, false, false, ChargeLateFee))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:CheckPayData失败({errMsg},{ JsonConvert.SerializeObject(resParam)})");
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "CheckPayData失败(" + errMsg + ")";
                            conn.Update(tb_Payment_Order);
                            msg = "支付数据有误";
                            return false;
                        }

                        JObject PayData = JObject.Parse(tb_Notice.PayData);
                        int Type = (int)PayData["Type"];
                        if (Type == 1)
                        {
                            JArray Data = (JArray)PayData["Data"];
                            StringBuilder FeesIds = new StringBuilder();
                            foreach (JObject item in Data)
                            {
                                FeesIds.Append((string)item["FeesId"] + ",");
                            }
                            if (Business.PubInfo.ReceFees(erpConnStr, out long ReceID, Convert.ToString(tb_Notice.CommID), Convert.ToString(tb_Notice.CustID), Convert.ToString(tb_Notice.RoomID), FeesIds.ToString(), 0.00M, tb_Notice.ChargeMode, tb_Payment_Order.OrderSN, tb_Notice.CreateUser, ChargeLateFee ? 1 : 0))
                            {
                                tb_Payment_Order.IsSucc = 3;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = Convert.ToString(ReceID);
                                conn.Update(tb_Payment_Order);
                                Wx.SendMpTemplateMessageBy1824(erpConnStr, tb_Notice, tb_Payment_Order, openid);
                                msg = "下账成功";
                                return true;
                            }
                            else
                            {
                                tb_Payment_Order.IsSucc = 4;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = "下账失败";
                                conn.Update(tb_Payment_Order);
                                msg = "下账失败";
                                return false;
                            }

                        }
                        else if (Type == 2)
                        {
                            JObject Data = (JObject)PayData["Data"];
                            string CostID = (string)Data["CostID"];
                            if (Business.PubInfo.RecePreFees(erpConnStr, out long ReceID, Convert.ToString(tb_Notice.CommID), Convert.ToString(tb_Notice.CustID), Convert.ToString(tb_Notice.RoomID), CostID, tb_Payment_Order.Amt, tb_Notice.ChargeMode, tb_Notice.CreateUser, tb_Notice.ChargeMode))
                            {
                                tb_Payment_Order.IsSucc = 3;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = Convert.ToString(ReceID);
                                conn.Update(tb_Payment_Order);
                                Wx.SendMpTemplateMessageBy1824(erpConnStr, tb_Notice, tb_Payment_Order, openid);
                                msg = "下账成功";
                                return true;
                            }
                            else
                            {
                                tb_Payment_Order.IsSucc = 4;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = "下账失败";
                                conn.Update(tb_Payment_Order);
                                msg = "下账失败";
                                return false;
                            }
                        }
                        else if (Type == 7)
                        {
                            JArray Data = (JArray)PayData["Data"];
                            List<string> CostList = new List<string>();
                            List<decimal> PrecAmountList = new List<decimal>();
                            List<long> HandIDList = new List<long>();
                            foreach (var item in Data)
                            {
                                CostList.Add((string)item["CostID"]);
                                PrecAmountList.Add((decimal)item["Amt"]);
                                if (!long.TryParse((string)item["HandID"], out long HandID))
                                {
                                    HandID = 0;
                                }
                                HandIDList.Add(HandID);
                            }
                            if (Business.PubInfo.RecePreFees(erpConnStr, out long ReceID, Convert.ToString(tb_Notice.CommID), Convert.ToString(tb_Notice.CustID), Convert.ToString(tb_Notice.RoomID), CostList.ToArray(), PrecAmountList.ToArray(), HandIDList.ToArray(), tb_Notice.ChargeMode, tb_Notice.CreateUser, tb_Notice.ChargeMode, tb_Payment_Order.OrderSN))
                            {
                                tb_Payment_Order.IsSucc = 3;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = Convert.ToString(ReceID);
                                conn.Update(tb_Payment_Order);
                                Wx.SendMpTemplateMessageBy1824(erpConnStr, tb_Notice, tb_Payment_Order, openid);
                                msg = "下账成功";
                                return true;
                            }
                            else
                            {
                                tb_Payment_Order.IsSucc = 4;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = "下账失败";
                                conn.Update(tb_Payment_Order);
                                msg = "下账失败";
                                return false;
                            }
                        }
                        else
                        {
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "下账失败(订单支付信息有误)";
                            conn.Update(tb_Payment_Order);
                            msg = "订单信息有误";
                            return false;
                        }

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    Business.PubInfo.GetLog().Error("ReceFees抛出了一个异常:", ex);
                    msg = "下账异常";
                    return false;
                }
            }
        }
        #endregion

        #region 福建农信支付
        private string CreateOrder_FjnxPay(DataRow row)
        {
            try
            {
                #region 获取参数
                string OpenID = string.Empty;
                if (row.Table.Columns.Contains("OpenID"))
                {
                    OpenID = row["OpenID"].ToString();
                }
                if (string.IsNullOrEmpty(OpenID))
                {
                    return new WxResponse(0, "用户未授权", null).toJson();
                }
                if (!row.Table.Columns.Contains("CommID") || !int.TryParse(row["CommID"].ToString(), out int CommID))
                {
                    CommID = 0;
                }
                if (!row.Table.Columns.Contains("CustID") || !long.TryParse(row["CustID"].ToString(), out long CustID))
                {
                    CustID = 0;
                }
                if (!row.Table.Columns.Contains("RoomID") || !long.TryParse(row["RoomID"].ToString(), out long RoomID))
                {
                    RoomID = 0;
                }
                #endregion
                #region 计算金额
                if (!row.Table.Columns.Contains("PayData") || string.IsNullOrEmpty(row["PayData"].ToString()))
                {
                    return new WxResponse(0, "缺少参数PayData", null).toJson();
                }
                string PayData = row["PayData"].ToString();
                if (!CheckPayData(Global_Fun.BurstConnectionString(CommID, Global_Fun.BURST_TYPE_CHARGE, true), CustID, RoomID, PayData, out decimal Amt, out string errMsg, true, false, true))
                {
                    return new WxResponse(0, errMsg, null).toJson();
                }
                if (Amt <= 0.00M)
                {
                    return new WxResponse(0, "金额必须大于0", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString),
                    erpConn = new SqlConnection(erpConnStr))
                {
                    dynamic tb_Payment_Config = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID", new { CommID });
                    if (null == tb_Payment_Config)
                    {
                        return new WxResponse(0, "该项目未开通对应支付方式", null).toJson();
                    }
                    #region 读取支付配置
                    FjnxConfig fjnxConfig;
                    try
                    {
                        fjnxConfig = JsonConvert.DeserializeObject<FjnxConfig>(tb_Payment_Config.Config);
                        if (null == fjnxConfig)
                        {
                            return new WxResponse(0, "该项目支付类型对应配置有误", null).toJson();
                        }
                    }
                    catch (Exception)
                    {
                        return new WxResponse(0, "该项目支付类型对应配置有误", null).toJson();
                    }
                    #endregion
                    #region 创建订单
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
                    if (conn.Execute("INSERT INTO Tb_Notice(Id, CommID, RoomID, CustID, PayData, CreateTime) VALUES(@Id, @CommID, @RoomID, @CustID, @PayData, @CreateTime)", new { Id = NoticeId, CommID, RoomID, CustID, PayData, CreateTime = DateTime.Now.ToString() }) <= 0)
                    {
                        return new WxResponse(0, "创建收款订单失败,请重试", null).toJson();
                    }
                    string ChargeMode = "微信公众号支付";
                    #region 修改收款方式
                    if (conn.QueryFirstOrDefault<int>("SELECT COUNT(1) FROM syscolumns WHERE id=object_id('Tb_Notice') AND name = 'ChargeMode'") > 0)
                    {
                        conn.Execute("UPDATE Tb_Notice SET ChargeMode = @ChargeMode WHERE Id = @Id", new { ChargeMode, Id = NoticeId });
                    }
                    #endregion
                    #region 请求农信微信支付
                    DateTime dateNow = DateTime.Now;
                    string OrderSN = $"{fjnxConfig.MerInstId}{dateNow.ToString("yyyyMMddHHmmssfff")}{Utils.BuildRandomStr(3)}";
                    if(!FjnxPayServive.Pay(fjnxConfig.MerchantId, fjnxConfig.MerInstId, fjnxConfig.TerminalId, tb_Corp_Config.AppID, OpenID, fjnxConfig.PrivateCertPath, fjnxConfig.PrivateCertPwd, fjnxConfig.PublicCertPath, OrderSN, Amt, FeesMemo, out string msg))
                    {
                        return new WxResponse(0, "生成支付订单失败,请重试", msg).toJson();
                    }
                    JObject jObj = JsonConvert.DeserializeObject<JObject>(msg);
                    jObj = (JObject)jObj["Message"];
                    if (!jObj.ContainsKey("PayInfo"))
                    {
                        return new WxResponse(0, "生成支付订单失败,请重试", jObj).toJson();
                    }
                    jObj = (JObject)jObj["PayInfo"];
                    if (!jObj.ContainsKey("Map"))
                    {
                        return new WxResponse(0, "生成支付订单失败,请重试", jObj).toJson();
                    }
                    jObj = (JObject)jObj["Map"];
                    if (conn.Execute("INSERT INTO Tb_Payment_Order(PayType, OrderSN, NoticeId, Amt, CreateTime) VALUES(@PayType, @OrderSN, @NoticeId, @Amt, @CreateTime)", new { PayType = 2, OrderSN, NoticeId = NoticeId, Amt = Amt, CreateTime = dateNow }) <= 0)
                    {
                        return new WxResponse(0, "创建订单失败", jObj).toJson();
                    }
                    jObj.Add("OrderSN", OrderSN);
                    return new WxResponse(200, "创建订单成功", jObj).toJson();
                    #endregion
                    #endregion
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        private string QueryOrder_FjnxPay(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string OrderSN = string.Empty;
                if (row.Table.Columns.Contains("OrderSN"))
                {
                    OrderSN = row["OrderSN"].ToString();
                }
                if (string.IsNullOrEmpty(OrderSN))
                {
                    return new WxResponse(101, "支付订单不存在", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString))
                {
                    Tb_Payment_Order tb_Payment_Order = conn.QueryFirstOrDefault<Tb_Payment_Order>("SELECT * FROM Tb_Payment_Order WITH(NOLOCK) WHERE OrderSN = @OrderSN", new { OrderSN });
                    if (null == tb_Payment_Order)
                    {
                        return new WxResponse(101, "支付订单不存在", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 5)
                    {
                        return new WxResponse(101, "订单超时未支付", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 4)
                    {
                        return new WxResponse(101, "下账失败,请联系物业核实", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 3)
                    {
                        return new WxResponse(200, "下账成功", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 2)
                    {
                        return new WxResponse(101, "下账失败,支付数据异常,请联系物业核实", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 1)
                    {
                        return new WxResponse(101, "下账失败,支付数据异常,请联系物业核实", null).toJson();
                    }
                    // 未下账需要实时查一下状态
                    if (tb_Payment_Order.IsSucc == 0)
                    {
                        #region 查询通知单是否存在
                        Tb_Notice tb_Notice = conn.QueryFirstOrDefault<Tb_Notice>("SELECT * FROM Tb_Notice WITH(NOLOCK) WHERE Id = @Id", new { Id = tb_Payment_Order.NoticeId });
                        if (null == tb_Notice)
                        {
                            return new WxResponse(101, "通知单不存在", null).toJson();
                        }
                        #endregion
                        #region 查询支付配置是否存在并解析
                        dynamic tb_Payment_Config = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID", new { tb_Notice.CommID });
                        if (null == tb_Payment_Config)
                        {
                            return new WxResponse(101, "支付配置不存在", null).toJson();
                        }
                        #region 读取支付配置
                        FjnxConfig fjnxConfig;
                        try
                        {
                            fjnxConfig = JsonConvert.DeserializeObject<FjnxConfig>(tb_Payment_Config.Config);
                            if (null == fjnxConfig)
                            {
                                return new WxResponse(101, "该项目支付类型对应配置有误", null).toJson();
                            }
                        }
                        catch (Exception)
                        {
                            return new WxResponse(101, "该项目支付类型对应配置有误", null).toJson();
                        }
                        #endregion
                        #endregion
                        #region 查询订单状态
                        if(!FjnxPayServive.Query(fjnxConfig.MerchantId, fjnxConfig.MerInstId, fjnxConfig.TerminalId, fjnxConfig.PrivateCertPath, fjnxConfig.PrivateCertPwd, fjnxConfig.PublicCertPath, OrderSN, out string msg))
                        {
                            return new WxResponse(0, "查询失败", null).toJson();
                        }
                        JObject jObj = JsonConvert.DeserializeObject<JObject>(msg);
                        jObj = (JObject)jObj["Message"];
                        try
                        {
                            if (!ReceFee_FjnxPay(Global_Fun.BurstConnectionString(tb_Notice.CommID, Global_Fun.BURST_TYPE_CHARGE, true), PubConstant.WChat2020ConnectionString, tb_Notice, tb_Payment_Order, jObj, out msg))
                            {
                                return new WxResponse(0, "下账失败", msg).toJson();
                            }
                            return new WxResponse(200, "下账成功", null).toJson();
                        }
                        catch (Exception ex)
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:查询报错({ex.Message})");
                            return new WxResponse(0, "查询失败", null).toJson();
                        }
                        #endregion
                    }
                    return new WxResponse(101, "支付状态异常", null).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        public static bool ReceFee_FjnxPay(string erpConnStr, string wchat2020ConnStr, Tb_Notice tb_Notice, Tb_Payment_Order tb_Payment_Order, JObject jObj, out string msg)
        {
            bool ChargeLateFee = true;
            lock (syncLocker)
            {
                try
                {
                    using (IDbConnection conn = new SqlConnection(wchat2020ConnStr))
                    {
                        // 因为外部存在订单状态已被更改的情况，所以此处我们再查询一次
                        tb_Payment_Order = conn.QueryFirstOrDefault<Tb_Payment_Order>("SELECT * FROM Tb_Payment_Order WITH(NOLOCK) WHERE OrderSN = @OrderSN", new { OrderSN = tb_Payment_Order.OrderSN });
                        if (null == tb_Payment_Order)
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:订单不存在");
                            msg = "订单不存在";
                            return false;
                        }
                        string trxstatus = jObj.ContainsKey("TxnSta") ? jObj["TxnSta"].ToString() : "";
                        // 如果交易状态不是0000，跳过，等待交易结果
                        if (string.IsNullOrEmpty(trxstatus))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:trxstatus为空({JsonConvert.SerializeObject(jObj)})");
                            msg = "交易处理中";
                            return false;
                        }
                        // 如果交易失败（trxstatus!=0000,2000,2008），标记订单状态为5，已超时
                        if (!"00".Equals(trxstatus) && !"01".Equals(trxstatus) && !"99".Equals(trxstatus))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:订单已关闭,视为未支付");
                            tb_Payment_Order.IsSucc = 5;
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(jObj);
                            tb_Payment_Order.Memo = "订单超时未支付";
                            conn.Update(tb_Payment_Order);
                            msg = "订单超时未支付";
                            return false;
                        }
                        // 如果交易状态不是0000，跳过，等待交易结果
                        if (!"01".Equals(trxstatus))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:trxstatus为空({JsonConvert.SerializeObject(jObj)})");
                            msg = "交易处理中";
                            return false;
                        }
                        #region 返回参数判断并进行下账参数
                        if (!jObj.ContainsKey("TxnAmt"))
                        {
                            tb_Payment_Order.IsSucc = 1;
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(jObj);
                            tb_Payment_Order.Memo = "下账通知不包含totalAmount";
                            conn.Update(tb_Payment_Order);
                            Business.PubInfo.GetLog().Error("下账通知不包含totalAmount:" + JsonConvert.SerializeObject(jObj));
                            msg = "下账通知不包含totalAmount";
                            return false;
                        }
                        long total_fee = Convert.ToInt64(Convert.ToDecimal(jObj["TxnAmt"].ToString()) * 100);
                        if (total_fee != Convert.ToInt64(tb_Payment_Order.Amt * 100))
                        {
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(jObj);
                            tb_Payment_Order.Memo = "支付金额与订单金额不相等";
                            conn.Update(tb_Payment_Order);
                            Business.PubInfo.GetLog().Error("支付金额与订单金额不相等:" + JsonConvert.SerializeObject(jObj));
                            msg = "支付金额与订单金额不相等";
                            return false;
                        }
                        if (!Business.PubInfo.CheckPayData(erpConnStr, tb_Notice.CustID, tb_Notice.RoomID, tb_Notice.PayData, out decimal Amt, out string errMsg, false, false, ChargeLateFee))
                        {
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(jObj);
                            tb_Payment_Order.Memo = "CheckPayData失败(" + errMsg + ")";
                            conn.Update(tb_Payment_Order);
                            Business.PubInfo.GetLog().Error("CheckPayData失败(" + errMsg + "):" + JsonConvert.SerializeObject(jObj));
                            msg = "CheckPayData失败(" + errMsg + ")";
                            return false;
                        }
                        JObject PayData = JObject.Parse(tb_Notice.PayData);
                        int Type = (int)PayData["Type"];
                        if (Type == 1)
                        {
                            JArray Data = (JArray)PayData["Data"];
                            List<string> FeesIDList = new List<string>();
                            foreach (JObject item in Data)
                            {
                                FeesIDList.Add(item["FeesId"].ToString());
                            }
                            if (Business.PubInfo.ReceFees(erpConnStr, out long ReceID, Convert.ToString(tb_Notice.CommID), Convert.ToString(tb_Notice.CustID), Convert.ToString(tb_Notice.RoomID), string.Join(",", FeesIDList.ToArray()), 0.00M, tb_Notice.ChargeMode, tb_Payment_Order.OrderSN, tb_Notice.CreateUser, ChargeLateFee ? 1 : 0))
                            {
                                tb_Payment_Order.IsSucc = 3;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(jObj);
                                tb_Payment_Order.Memo = Convert.ToString(ReceID);
                                conn.Update(tb_Payment_Order);
                                msg = "下账成功";
                                return true;
                            }
                            else
                            {
                                tb_Payment_Order.IsSucc = 4;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(jObj);
                                tb_Payment_Order.Memo = "下账失败";
                                conn.Update(tb_Payment_Order);
                                msg = "下账失败";
                                return false;
                            }

                        }
                        else
                        {
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(jObj);
                            tb_Payment_Order.Memo = "下账失败(订单支付信息有误)";
                            conn.Update(tb_Payment_Order);
                            msg = "订单支付信息有误";
                            return false;
                        }

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    Business.PubInfo.GetLog().Error("ReceFees抛出了一个异常:", ex);
                    msg = "下账异常";
                    return false;
                }
            }
        }
        #endregion
        #region 通联支付
        private string CreateOrder_AllinPay(DataRow row)
        {
            try
            {
                #region 获取参数
                string OpenID = string.Empty;
                if (row.Table.Columns.Contains("OpenID"))
                {
                    OpenID = row["OpenID"].ToString();
                }
                if (string.IsNullOrEmpty(OpenID))
                {
                    return new WxResponse(0, "用户未授权", null).toJson();
                }
                if (!row.Table.Columns.Contains("CommID") || !int.TryParse(row["CommID"].ToString(), out int CommID))
                {
                    CommID = 0;
                }
                if (!row.Table.Columns.Contains("CustID") || !long.TryParse(row["CustID"].ToString(), out long CustID))
                {
                    CustID = 0;
                }
                if (!row.Table.Columns.Contains("RoomID") || !long.TryParse(row["RoomID"].ToString(), out long RoomID))
                {
                    RoomID = 0;
                }
                #endregion
                #region 计算金额
                if (!row.Table.Columns.Contains("PayData") || string.IsNullOrEmpty(row["PayData"].ToString()))
                {
                    return new WxResponse(0, "缺少参数PayData", null).toJson();
                }
                string PayData = row["PayData"].ToString();
                if (!CheckPayData(Global_Fun.BurstConnectionString(CommID, Global_Fun.BURST_TYPE_CHARGE, true), CustID, RoomID, PayData, out decimal Amt, out string errMsg, true, false, true))
                {
                    return new WxResponse(0, errMsg, null).toJson();
                }
                if (Amt <= 0.00M)
                {
                    return new WxResponse(0, "金额必须大于0", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString),
                    erpConn = new SqlConnection(erpConnStr))
                {
                    dynamic tb_Payment_Config = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID", new { CommID });
                    if (null == tb_Payment_Config)
                    {
                        return new WxResponse(0, "该项目未开通对应支付方式", null).toJson();
                    }
                    #region 读取支付配置
                    AllinConfig allinConfig;
                    try
                    {
                        allinConfig = JsonConvert.DeserializeObject<AllinConfig>(tb_Payment_Config.Config);
                        if (null == allinConfig)
                        {
                            return new WxResponse(0, "该项目支付类型对应配置有误", null).toJson();
                        }
                    }
                    catch (Exception)
                    {
                        return new WxResponse(0, "该项目支付类型对应配置有误", null).toJson();
                    }
                    #endregion
                    #region 创建订单
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
                    if (conn.Execute("INSERT INTO Tb_Notice(Id, CommID, RoomID, CustID, PayData, CreateTime) VALUES(@Id, @CommID, @RoomID, @CustID, @PayData, @CreateTime)", new { Id = NoticeId, CommID, RoomID, CustID, PayData, CreateTime = DateTime.Now.ToString() }) <= 0)
                    {
                        return new WxResponse(0, "创建收款订单失败,请重试", null).toJson();
                    }
                    string ChargeMode = "微信公众号支付";
                    #region 修改收款方式
                    if (conn.QueryFirstOrDefault<int>("SELECT COUNT(1) FROM syscolumns WHERE id=object_id('Tb_Notice') AND name = 'ChargeMode'") > 0)
                    {
                        conn.Execute("UPDATE Tb_Notice SET ChargeMode = @ChargeMode WHERE Id = @Id", new { ChargeMode, Id = NoticeId });
                    }
                    #endregion
                    #region 请求通联微信支付
                    DateTime dateNow = DateTime.Now;
                    string OrderSN = dateNow.ToString("yyyyMMddHHmmssfff") + Utils.BuildRandomStr(3);

                    #region 获取对应类型的下账地址
                    string PaymentNotifyUrl = AppGlobal.GetAppSetting("AllinPay_Notify_Url") + "?CommID=" + CommID;
                    #endregion

                    Dictionary<string, string> param;
                    try
                    {
                        param = SybWxPayService.Pay(Convert.ToInt64(Amt * 100), OrderSN, "W02", FeesMemo, RoomSign, OpenID, "", PaymentNotifyUrl, "", "", "", "", allinConfig.orgid, allinConfig.appid, allinConfig.custid, allinConfig.appkey, allinConfig.subbranch);
                    }
                    catch (Exception ex)
                    {
                        return new WxResponse(0, "生成支付订单失败,请重试", ex.Message).toJson();
                    }
                    if (!param.ContainsKey("payinfo"))
                    {
                        return new WxResponse(0, "生成支付订单失败,请重试", param).toJson();
                    }
                    string orderInfo = param["payinfo"].ToString();
                    if (conn.Execute("INSERT INTO Tb_Payment_Order(PayType, OrderSN, NoticeId, Amt, CreateTime) VALUES(@PayType, @OrderSN, @NoticeId, @Amt, @CreateTime)", new { PayType = 2, OrderSN, NoticeId = NoticeId, Amt = Amt, CreateTime = dateNow }) <= 0)
                    {
                        return new WxResponse(0, "创建订单失败", orderInfo).toJson();
                    }
                    JObject jObj = null;
                    try
                    {
                        jObj = JObject.Parse(orderInfo);
                        jObj.Add("OrderSN", OrderSN);
                    }
                    catch (Exception)
                    {
                        return new WxResponse(0, "支付数据有误", jObj).toJson();
                    }
                    return new WxResponse(200, "创建订单成功", jObj).toJson();
                    #endregion
                    #endregion
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        private string QueryOrder_AllinPay(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string OrderSN = string.Empty;
                if (row.Table.Columns.Contains("OrderSN"))
                {
                    OrderSN = row["OrderSN"].ToString();
                }
                if (string.IsNullOrEmpty(OrderSN))
                {
                    return new WxResponse(101, "支付订单不存在", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString))
                {
                    Tb_Payment_Order tb_Payment_Order = conn.QueryFirstOrDefault<Tb_Payment_Order>("SELECT * FROM Tb_Payment_Order WITH(NOLOCK) WHERE OrderSN = @OrderSN", new { OrderSN });
                    if (null == tb_Payment_Order)
                    {
                        return new WxResponse(101, "支付订单不存在", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 5)
                    {
                        return new WxResponse(101, "订单超时未支付", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 4)
                    {
                        return new WxResponse(101, "下账失败,请联系物业核实", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 3)
                    {
                        return new WxResponse(200, "下账成功", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 2)
                    {
                        return new WxResponse(101, "下账失败,支付数据异常,请联系物业核实", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 1)
                    {
                        return new WxResponse(101, "下账失败,支付数据异常,请联系物业核实", null).toJson();
                    }
                    // 未下账需要实时查一下状态
                    if (tb_Payment_Order.IsSucc == 0)
                    {
                        #region 查询通知单是否存在
                        Tb_Notice tb_Notice = conn.QueryFirstOrDefault<Tb_Notice>("SELECT * FROM Tb_Notice WITH(NOLOCK) WHERE Id = @Id", new { Id = tb_Payment_Order.NoticeId });
                        if (null == tb_Notice)
                        {
                            return new WxResponse(101, "通知单不存在", null).toJson();
                        }
                        #endregion
                        #region 查询支付配置是否存在并解析
                        dynamic tb_Payment_Config = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID", new { tb_Notice.CommID });
                        if (null == tb_Payment_Config)
                        {
                            return new WxResponse(101, "支付配置不存在", null).toJson();
                        }
                        #region 读取支付配置
                        AllinConfig allinConfig;
                        try
                        {
                            allinConfig = JsonConvert.DeserializeObject<AllinConfig>(tb_Payment_Config.Config);
                            if (null == allinConfig)
                            {
                                return new WxResponse(101, "该项目支付类型对应配置有误", null).toJson();
                            }
                        }
                        catch (Exception)
                        {
                            return new WxResponse(101, "该项目支付类型对应配置有误", null).toJson();
                        }
                        #endregion
                        #endregion
                        #region 查询订单状态
                        try
                        {

                            Dictionary<string, string> resParam = SybWxPayService.Query(tb_Payment_Order.OrderSN, "", allinConfig.orgid, allinConfig.appid, allinConfig.custid, allinConfig.appkey);
                            if (!ReceFee_AllinPay(Global_Fun.BurstConnectionString(tb_Notice.CommID, Global_Fun.BURST_TYPE_CHARGE, true), PubConstant.WChat2020ConnectionString, tb_Notice, tb_Payment_Order, resParam, out string msg))
                            {
                                return new WxResponse(0, "下账失败", msg).toJson();
                            }
                            return new WxResponse(200, "下账成功", null).toJson();
                        }
                        catch (Exception ex)
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:查询报错({ex.Message})");
                            return new WxResponse(0, "查询失败", null).toJson();
                        }
                        #endregion
                    }
                    return new WxResponse(101, "支付状态异常", null).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        public static bool ReceFee_AllinPay(string erpConnStr, string wchat2020ConnStr, Tb_Notice tb_Notice, Tb_Payment_Order tb_Payment_Order, Dictionary<string, string> resParam, out string msg)
        {
            bool ChargeLateFee = true;
            lock (syncLocker)
            {
                try
                {
                    using (IDbConnection conn = new SqlConnection(wchat2020ConnStr))
                    {
                        // 因为外部存在订单状态已被更改的情况，所以此处我们再查询一次
                        tb_Payment_Order = conn.QueryFirstOrDefault<Tb_Payment_Order>("SELECT * FROM Tb_Payment_Order WITH(NOLOCK) WHERE OrderSN = @OrderSN", new { OrderSN = tb_Payment_Order.OrderSN });
                        if (null == tb_Payment_Order)
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:订单不存在");
                            msg = "订单不存在";
                            return false;
                        }
                        string trxstatus = resParam.ContainsKey("trxstatus") ? resParam["trxstatus"].ToString() : "";
                        // 如果交易状态不是0000，跳过，等待交易结果
                        if (string.IsNullOrEmpty(trxstatus))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:trxstatus为空({JsonConvert.SerializeObject(resParam)})");
                            msg = "交易处理中";
                            return false;
                        }
                        // 如果交易失败（trxstatus!=0000,2000,2008），标记订单状态为5，已超时
                        if (!"0000".Equals(trxstatus) && !"2000".Equals(trxstatus) && !"2008".Equals(trxstatus))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:订单已关闭,视为未支付");
                            tb_Payment_Order.IsSucc = 5;
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "订单超时未支付";
                            conn.Update(tb_Payment_Order);
                            msg = "订单超时未支付";
                            return false;
                        }
                        // 如果交易状态不是0000，跳过，等待交易结果
                        if (!"0000".Equals(trxstatus))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:trxstatus为空({JsonConvert.SerializeObject(resParam)})");
                            msg = "交易处理中";
                            return false;
                        }
                        #region 返回参数判断并进行下账参数
                        if (!resParam.ContainsKey("trxamt"))
                        {
                            tb_Payment_Order.IsSucc = 1;
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "下账通知不包含totalAmount";
                            conn.Update(tb_Payment_Order);
                            Business.PubInfo.GetLog().Error("下账通知不包含totalAmount:" + JsonConvert.SerializeObject(resParam));
                            msg = "下账通知不包含totalAmount";
                            return false;
                        }
                        long total_fee = Convert.ToInt64(resParam["trxamt"].ToString());

                        if (total_fee != Convert.ToInt64(tb_Payment_Order.Amt * 100))
                        {
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "支付金额与订单金额不相等";
                            conn.Update(tb_Payment_Order);
                            Business.PubInfo.GetLog().Error("支付金额与订单金额不相等:" + JsonConvert.SerializeObject(resParam));
                            msg = "支付金额与订单金额不相等";
                            return false;
                        }
                        if (!Business.PubInfo.CheckPayData(erpConnStr, tb_Notice.CustID, tb_Notice.RoomID, tb_Notice.PayData, out decimal Amt, out string errMsg, false, false, ChargeLateFee))
                        {
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "CheckPayData失败(" + errMsg + ")";
                            conn.Update(tb_Payment_Order);
                            Business.PubInfo.GetLog().Error("CheckPayData失败(" + errMsg + "):" + JsonConvert.SerializeObject(resParam));
                            msg = "CheckPayData失败(" + errMsg + ")";
                            return false;
                        }
                        JObject PayData = JObject.Parse(tb_Notice.PayData);
                        int Type = (int)PayData["Type"];
                        if (Type == 1)
                        {
                            JArray Data = (JArray)PayData["Data"];
                            List<string> FeesIDList = new List<string>();
                            foreach (JObject item in Data)
                            {
                                FeesIDList.Add(item["FeesId"].ToString());
                            }
                            if (Business.PubInfo.ReceFees(erpConnStr, out long ReceID, Convert.ToString(tb_Notice.CommID), Convert.ToString(tb_Notice.CustID), Convert.ToString(tb_Notice.RoomID), string.Join(",", FeesIDList.ToArray()), 0.00M, tb_Notice.ChargeMode, tb_Payment_Order.OrderSN, tb_Notice.CreateUser, ChargeLateFee ? 1 : 0))
                            {
                                tb_Payment_Order.IsSucc = 3;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = Convert.ToString(ReceID);
                                conn.Update(tb_Payment_Order);
                                msg = "下账成功";
                                return true;
                            }
                            else
                            {
                                tb_Payment_Order.IsSucc = 4;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = "下账失败";
                                conn.Update(tb_Payment_Order);
                                msg = "下账失败";
                                return true;
                            }

                        }
                        else
                        {
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "下账失败(订单支付信息有误)";
                            conn.Update(tb_Payment_Order);
                            msg = "订单支付信息有误";
                            return false;
                        }

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    Business.PubInfo.GetLog().Error("ReceFees抛出了一个异常:", ex);
                    msg = "下账异常";
                    return false;
                }
            }
        }
        #endregion

        #region 微信官方支付
        private string CreateOrder_WChatPay(DataRow row)
        {
            try
            {
                #region 获取参数
                string OpenID = string.Empty;
                if (row.Table.Columns.Contains("OpenID"))
                {
                    OpenID = row["OpenID"].ToString();
                }
                if (string.IsNullOrEmpty(OpenID))
                {
                    return new WxResponse(0, "用户未授权", null).toJson();
                }
                if (!row.Table.Columns.Contains("CommID") || !int.TryParse(row["CommID"].ToString(), out int CommID))
                {
                    CommID = 0;
                }
                if (!row.Table.Columns.Contains("CustID") || !long.TryParse(row["CustID"].ToString(), out long CustID))
                {
                    CustID = 0;
                }
                if (!row.Table.Columns.Contains("RoomID") || !long.TryParse(row["RoomID"].ToString(), out long RoomID))
                {
                    RoomID = 0;
                }
                #endregion
                #region 计算金额
                if (!row.Table.Columns.Contains("PayData") || string.IsNullOrEmpty(row["PayData"].ToString()))
                {
                    return new WxResponse(0, "缺少参数PayData", null).toJson();
                }
                string PayData = row["PayData"].ToString();
                if (!CheckPayData(Global_Fun.BurstConnectionString(CommID, Global_Fun.BURST_TYPE_CHARGE, true), CustID, RoomID, PayData, out decimal Amt, out string errMsg, true, false, true))
                {
                    return new WxResponse(0, errMsg, null).toJson();
                }
                if (Amt <= 0.00M)
                {
                    return new WxResponse(0, "金额必须大于0", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString),
                    erpConn = new SqlConnection(erpConnStr))
                {
                    dynamic tb_Payment_Config = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID", new { CommID });
                    if (null == tb_Payment_Config)
                    {
                        return new WxResponse(0, "该项目未开通对应支付方式", null).toJson();
                    }
                    #region 读取支付配置
                    WxConfig wxConfig;
                    try
                    {
                        wxConfig = JsonConvert.DeserializeObject<WxConfig>(tb_Payment_Config.Config);
                        if (null == wxConfig)
                        {
                            return new WxResponse(0, "该项目支付类型对应配置有误", null).toJson();
                        }
                    }
                    catch (Exception)
                    {
                        return new WxResponse(0, "该项目支付类型对应配置有误", null).toJson();
                    }
                    #endregion
                    #region 创建订单
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
                    if (conn.Execute("INSERT INTO Tb_Notice(Id, CommID, RoomID, CustID, PayData, CreateTime) VALUES(@Id, @CommID, @RoomID, @CustID, @PayData, @CreateTime)", new { Id = NoticeId, CommID, RoomID, CustID, PayData, CreateTime = DateTime.Now.ToString() }) <= 0)
                    {
                        return new WxResponse(0, "创建收款订单失败,请重试", null).toJson();
                    }
                    string ChargeMode = "微信公众号支付";
                    #region 修改收款方式
                    if (conn.QueryFirstOrDefault<int>("SELECT COUNT(1) FROM syscolumns WHERE id=object_id('Tb_Notice') AND name = 'ChargeMode'") > 0)
                    {
                        conn.Execute("UPDATE Tb_Notice SET ChargeMode = @ChargeMode WHERE Id = @Id", new { ChargeMode, Id = NoticeId });
                    }
                    #endregion
                    #region 请求微信官方支付
                    DateTime dateNow = DateTime.Now;
                    string OrderSN = dateNow.ToString("yyyyMMddHHmmssfff") + Utils.BuildRandomStr(3);

                    #region 获取对应类型的下账地址
                    string PaymentNotifyUrl = AppGlobal.GetAppSetting("WxPay_Notify_Url") + "?CommID=" + CommID;
                    #endregion
                    WxPayData wxPayData = new WxPayData();
                    wxPayData.SetValue("out_trade_no", OrderSN);
                    wxPayData.SetValue("body", FeesMemo);
                    wxPayData.SetValue("total_fee", Convert.ToInt32(Amt * 100));
                    wxPayData.SetValue("trade_type", "JSAPI");
                    wxPayData.SetValue("openid", OpenID);
                    wxPayData.SetValue("notify_url", PaymentNotifyUrl);
                    wxPayData.SetValue("appid", tb_Corp_Config.AppID);
                    wxPayData.SetValue("mch_id", wxConfig.mch_id);
                    wxPayData.SetValue("spbill_create_ip", "8.8.8.8");
                    wxPayData.SetValue("nonce_str", WxPayApi.GenerateNonceStr());
                    wxPayData.SetValue("sign_type", WxPayData.SIGN_TYPE_HMAC_SHA256);
                    wxPayData.SetValue("sign", wxPayData.MakeSign(wxConfig.appkey));
                    try
                    {
                        wxPayData = WxPayApi.UnifiedOrder(wxPayData);
                    }
                    catch (Exception)
                    {
                        return new WxResponse(0, "请求超时,请重试", null).toJson();
                    }
                    new Logger().WriteLog("微信支付日志", "" + wxPayData.ToJson());
                    if (!wxPayData.IsSet("return_code") || !"SUCCESS".Equals(wxPayData.GetValue("return_code").ToString()))
                    {
                        return new WxResponse(0, "请求支付订单失败", wxPayData.IsSet("return_msg") ? wxPayData.GetValue("return_msg").ToString() : "").toJson();
                    }
                    if (!wxPayData.IsSet("result_code") || !"SUCCESS".Equals(wxPayData.GetValue("result_code").ToString()))
                    {
                        return new WxResponse(0, "请求支付订单失败", wxPayData.IsSet("err_code") ? wxPayData.GetValue("err_code").ToString() : "").toJson();
                    }
                    if (!wxPayData.IsSet("prepay_id"))
                    {
                        return new WxResponse(0, "请求支付订单失败", "未找到prepay_id").toJson();
                    }
                    string prepay_id = wxPayData.GetValue("prepay_id").ToString();
                    if (conn.Execute("INSERT INTO Tb_Payment_Order(PayType, OrderSN, NoticeId, Amt, CreateTime) VALUES(@PayType, @OrderSN, @NoticeId, @Amt, @CreateTime)", new { PayType = 2, OrderSN, NoticeId = NoticeId, Amt = Amt, CreateTime = dateNow }) <= 0)
                    {
                        return new WxResponse(0, "生成订单失败", null).toJson();
                    }
                    WxPayData result = new WxPayData();
                    result.SetValue("appId", tb_Corp_Config.AppID);
                    result.SetValue("timeStamp", WxPayApi.GenerateTimeStamp());
                    result.SetValue("nonceStr", WxPayApi.GenerateNonceStr());
                    result.SetValue("package", $"prepay_id={prepay_id}");
                    result.SetValue("signType", WxPayData.SIGN_TYPE_HMAC_SHA256);
                    result.SetValue("paySign", result.MakeSign(wxConfig.appkey));
                    JObject jObj = JObject.Parse(result.ToJson());
                    jObj.Add("OrderSN", OrderSN);
                    return new WxResponse(200, "创建订单成功", jObj).toJson();
                    #endregion
                    #endregion
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        private string QueryOrder_WChatPay(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string OrderSN = string.Empty;
                if (row.Table.Columns.Contains("OrderSN"))
                {
                    OrderSN = row["OrderSN"].ToString();
                }
                if (string.IsNullOrEmpty(OrderSN))
                {
                    return new WxResponse(101, "支付订单不存在", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString))
                {
                    Tb_Payment_Order tb_Payment_Order = conn.QueryFirstOrDefault<Tb_Payment_Order>("SELECT * FROM Tb_Payment_Order WITH(NOLOCK) WHERE OrderSN = @OrderSN", new { OrderSN });
                    if (null == tb_Payment_Order)
                    {
                        return new WxResponse(101, "支付订单不存在", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 5)
                    {
                        return new WxResponse(101, "订单超时未支付", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 4)
                    {
                        return new WxResponse(101, "下账失败,请联系物业核实", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 3)
                    {
                        return new WxResponse(200, "下账成功", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 2)
                    {
                        return new WxResponse(101, "下账失败,支付数据异常,请联系物业核实", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 1)
                    {
                        return new WxResponse(101, "下账失败,支付数据异常,请联系物业核实", null).toJson();
                    }
                    // 未下账需要实时查一下状态
                    if (tb_Payment_Order.IsSucc == 0)
                    {
                        #region 查询通知单是否存在
                        Tb_Notice tb_Notice = conn.QueryFirstOrDefault<Tb_Notice>("SELECT * FROM Tb_Notice WITH(NOLOCK) WHERE Id = @Id", new { Id = tb_Payment_Order.NoticeId });
                        if (null == tb_Notice)
                        {
                            return new WxResponse(101, "通知单不存在", null).toJson();
                        }
                        #endregion
                        #region 查询支付配置是否存在并解析
                        dynamic tb_Payment_Config = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID", new { tb_Notice.CommID });
                        if (null == tb_Payment_Config)
                        {
                            return new WxResponse(101, "支付配置不存在", null).toJson();
                        }
                        WxConfig wxConfig = null;
                        try
                        {
                            wxConfig = JsonConvert.DeserializeObject<WxConfig>(tb_Payment_Config.Config);
                            if (null == wxConfig)
                            {
                                return new WxResponse(101, "支付配置有误", null).toJson();
                            }
                        }
                        catch (Exception)
                        {
                            return new WxResponse(101, "支付配置有误", null).toJson();
                        }
                        #endregion
                        #region 查询订单状态
                        WxPayData wxPayData = new WxPayData();
                        wxPayData.SetValue("out_trade_no", OrderSN);
                        wxPayData.SetValue("appid", tb_Corp_Config.AppID);
                        wxPayData.SetValue("mch_id", wxConfig.mch_id);
                        wxPayData.SetValue("nonce_str", WxPayApi.GenerateNonceStr());
                        wxPayData.SetValue("sign_type", WxPayData.SIGN_TYPE_HMAC_SHA256);
                        wxPayData.SetValue("sign", wxPayData.MakeSign(wxConfig.appkey));
                        try
                        {
                            wxPayData = WxPayApi.OrderQuery(wxPayData);
                        }
                        catch (Exception)
                        {
                            return new WxResponse(0, "请求超时,请重试", null).toJson();
                        }
                        if (!ReceFee_WChatPay(Global_Fun.BurstConnectionString(tb_Notice.CommID, Global_Fun.BURST_TYPE_CHARGE, true), PubConstant.WChat2020ConnectionString, tb_Notice, tb_Payment_Order, wxPayData.GetValues(), out string msg))
                        {
                            return new WxResponse(0, "下账失败", msg).toJson();
                        }
                        return new WxResponse(200, "下账成功", null).toJson();
                        #endregion
                    }
                    return new WxResponse(101, "支付状态异常", null).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        public static bool ReceFee_WChatPay(string erpConnStr, string wchat2020ConnStr, Tb_Notice tb_Notice, Tb_Payment_Order tb_Payment_Order, SortedDictionary<string, object> resParam, out string msg)
        {
            bool ChargeLateFee = true;
            lock (syncLocker)
            {
                try
                {
                    using (IDbConnection conn = new SqlConnection(wchat2020ConnStr))
                    {
                        // 因为外部存在订单状态已被更改的情况，所以此处我们再查询一次
                        tb_Payment_Order = conn.QueryFirstOrDefault<Tb_Payment_Order>("SELECT * FROM Tb_Payment_Order WITH(NOLOCK) WHERE OrderSN = @OrderSN", new { OrderSN = tb_Payment_Order.OrderSN });
                        if (null == tb_Payment_Order)
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:订单不存在");
                            msg = "订单不存在";
                            return false;
                        }
                        // 如果不存在trxstatus字段或者trxstatus字段为空，说明交易中，跳过
                        string return_code = resParam.ContainsKey("return_code") ? resParam["return_code"].ToString() : "";
                        string result_code = resParam.ContainsKey("result_code") ? resParam["result_code"].ToString() : "";
                        if (!"SUCCESS".Equals(return_code) || !"SUCCESS".Equals(result_code))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:return_code或者result_code不等于SUCCESS({JsonConvert.SerializeObject(resParam)})");
                            msg = "交易状态异常";
                            return false;
                        }
                        string trade_state = resParam.ContainsKey("trade_state") ? resParam["trade_state"].ToString() : "";
                        string pay_info = resParam.ContainsKey("trade_state_desc") ? resParam["trade_state_desc"].ToString() : trade_state;
                        // 如果交易失败（trade_state!=SUCCESS,USERPAYING），标记订单状态为5，已超时
                        if (!"SUCCESS".Equals(trade_state) && !"USERPAYING".Equals(trade_state))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:交易失败,视为未支付");
                            tb_Payment_Order.IsSucc = 5;
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = pay_info;
                            conn.Update(tb_Payment_Order);
                            msg = pay_info;
                            return false;
                        }
                        // 交易成功，处理下账
                        #region 返回参数判断并进行下账参数
                        long total_fee = Convert.ToInt64(resParam.ContainsKey("total_fee") ? resParam["total_fee"].ToString() : "0");
                        if (total_fee != Convert.ToInt64(tb_Payment_Order.Amt * 100))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:支付金额与订单金额不相等({JsonConvert.SerializeObject(resParam)})");
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "支付金额与订单金额不相等";
                            conn.Update(tb_Payment_Order);
                            msg = "支付金额与订单金额不一致";
                            return false;
                        }
                        if (!Business.PubInfo.CheckPayData(erpConnStr, tb_Notice.CustID, tb_Notice.RoomID, tb_Notice.PayData, out decimal Amt, out string errMsg, false, false, ChargeLateFee))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:CheckPayData失败({errMsg},{ JsonConvert.SerializeObject(resParam)})");
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "CheckPayData失败(" + errMsg + ")";
                            conn.Update(tb_Payment_Order);
                            msg = "支付数据有误";
                            return false;
                        }
                        JObject PayData = JObject.Parse(tb_Notice.PayData);
                        int Type = (int)PayData["Type"];
                        if (Type == 1)
                        {
                            JArray Data = (JArray)PayData["Data"];
                            StringBuilder FeesIds = new StringBuilder();
                            foreach (JObject item in Data)
                            {
                                FeesIds.Append((string)item["FeesId"] + ",");
                            }
                            if (Business.PubInfo.ReceFees(erpConnStr, out long ReceID, Convert.ToString(tb_Notice.CommID), Convert.ToString(tb_Notice.CustID), Convert.ToString(tb_Notice.RoomID), FeesIds.ToString(), 0.00M, tb_Notice.ChargeMode, tb_Payment_Order.OrderSN, tb_Notice.CreateUser, ChargeLateFee ? 1 : 0))
                            {
                                tb_Payment_Order.IsSucc = 3;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = Convert.ToString(ReceID);
                                conn.Update(tb_Payment_Order);
                                msg = "下账成功";
                                return true;
                            }
                            else
                            {
                                tb_Payment_Order.IsSucc = 4;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = "下账失败";
                                conn.Update(tb_Payment_Order);
                                msg = "下账失败";
                                return false;
                            }

                        }
                        else if (Type == 2)
                        {
                            JObject Data = (JObject)PayData["Data"];
                            string CostID = (string)Data["CostID"];
                            if (Business.PubInfo.RecePreFees(erpConnStr, out long ReceID, Convert.ToString(tb_Notice.CommID), Convert.ToString(tb_Notice.CustID), Convert.ToString(tb_Notice.RoomID), CostID, tb_Payment_Order.Amt, tb_Notice.ChargeMode, tb_Notice.CreateUser, tb_Notice.ChargeMode, 0, tb_Payment_Order.OrderSN))
                            {
                                tb_Payment_Order.IsSucc = 3;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = Convert.ToString(ReceID);
                                conn.Update(tb_Payment_Order);
                                msg = "下账成功";
                                return true;
                            }
                            else
                            {
                                tb_Payment_Order.IsSucc = 4;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = "下账失败";
                                conn.Update(tb_Payment_Order);
                                msg = "下账失败";
                                return false;
                            }
                        }
                        else if (Type == 7)
                        {
                            JArray Data = (JArray)PayData["Data"];
                            List<string> CostList = new List<string>();
                            List<decimal> PrecAmountList = new List<decimal>();
                            List<long> HandIDList = new List<long>();
                            foreach (var item in Data)
                            {
                                CostList.Add((string)item["CostID"]);
                                PrecAmountList.Add((decimal)item["Amt"]);
                                if (!long.TryParse((string)item["HandID"], out long HandID))
                                {
                                    HandID = 0;
                                }
                                HandIDList.Add(HandID);
                            }
                            if (Business.PubInfo.RecePreFees(erpConnStr, out long ReceID, Convert.ToString(tb_Notice.CommID), Convert.ToString(tb_Notice.CustID), Convert.ToString(tb_Notice.RoomID), CostList.ToArray(), PrecAmountList.ToArray(), HandIDList.ToArray(), tb_Notice.ChargeMode, tb_Notice.CreateUser, tb_Notice.ChargeMode, tb_Payment_Order.OrderSN))
                            {
                                tb_Payment_Order.IsSucc = 3;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = Convert.ToString(ReceID);
                                conn.Update(tb_Payment_Order);
                                msg = "下账成功";
                                return true;
                            }
                            else
                            {
                                tb_Payment_Order.IsSucc = 4;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = "下账失败";
                                conn.Update(tb_Payment_Order);
                                msg = "下账失败";
                                return false;
                            }
                        }
                        else
                        {
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "下账失败(订单支付信息有误)";
                            conn.Update(tb_Payment_Order);
                            msg = "订单信息有误";
                            return false;
                        }

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    Business.PubInfo.GetLog().Error("ReceFees抛出了一个异常:", ex);
                    msg = "下账异常";
                    return false;
                }
            }
        }
        #endregion


        #region 闪惠支付（凯美用到）
        private string CreateOrder_MixuesPay(DataRow row)
        {
            try
            {
                #region 获取参数
                string OpenID = string.Empty;
                if (row.Table.Columns.Contains("OpenID"))
                {
                    OpenID = row["OpenID"].ToString();
                }
                if (string.IsNullOrEmpty(OpenID))
                {
                    return new WxResponse(0, "用户未授权", null).toJson();
                }
                if (!row.Table.Columns.Contains("CommID") || !int.TryParse(row["CommID"].ToString(), out int CommID))
                {
                    CommID = 0;
                }
                if (!row.Table.Columns.Contains("CustID") || !long.TryParse(row["CustID"].ToString(), out long CustID))
                {
                    CustID = 0;
                }
                if (!row.Table.Columns.Contains("RoomID") || !long.TryParse(row["RoomID"].ToString(), out long RoomID))
                {
                    RoomID = 0;
                }
                #endregion
                #region 计算金额
                if (!row.Table.Columns.Contains("PayData") || string.IsNullOrEmpty(row["PayData"].ToString()))
                {
                    return new WxResponse(0, "缺少参数PayData", null).toJson();
                }
                string PayData = row["PayData"].ToString();
                if (!CheckPayData(Global_Fun.BurstConnectionString(CommID, Global_Fun.BURST_TYPE_CHARGE, true), CustID, RoomID, PayData, out decimal Amt, out string errMsg, true, false, true))
                {
                    return new WxResponse(0, errMsg, null).toJson();
                }
                if (Amt <= 0.00M)
                {
                    return new WxResponse(0, "金额必须大于0", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString),
                    erpConn = new SqlConnection(erpConnStr))
                {
                    dynamic tb_Payment_Config = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID", new { CommID });
                    if (null == tb_Payment_Config)
                    {
                        return new WxResponse(0, "该项目未开通对应支付方式", null).toJson();
                    }
                    #region 读取支付配置
                    MixuesConfig mixuesConfig;
                    try
                    {
                        mixuesConfig = JsonConvert.DeserializeObject<MixuesConfig>(tb_Payment_Config.Config);
                        if (null == mixuesConfig)
                        {
                            return new WxResponse(0, "该项目支付类型对应配置有误", null).toJson();
                        }
                    }
                    catch (Exception)
                    {
                        return new WxResponse(0, "该项目支付类型对应配置有误", null).toJson();
                    }
                    #endregion
                    #region 创建订单
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
                    if (conn.Execute("INSERT INTO Tb_Notice(Id, CommID, RoomID, CustID, PayData, CreateTime) VALUES(@Id, @CommID, @RoomID, @CustID, @PayData, @CreateTime)", new { Id = NoticeId, CommID, RoomID, CustID, PayData, CreateTime = DateTime.Now.ToString() }) <= 0)
                    {
                        return new WxResponse(0, "创建收款订单失败,请重试", null).toJson();
                    }
                    string ChargeMode = "微信公众号支付";
                    #region 修改收款方式
                    if (conn.QueryFirstOrDefault<int>("SELECT COUNT(1) FROM syscolumns WHERE id=object_id('Tb_Notice') AND name = 'ChargeMode'") > 0)
                    {
                        conn.Execute("UPDATE Tb_Notice SET ChargeMode = @ChargeMode WHERE Id = @Id", new { ChargeMode, Id = NoticeId });
                    }
                    #endregion
                    #region 请求闪惠微信支付
                    DateTime dateNow = DateTime.Now;
                    string OrderSN = dateNow.ToString("yyyyMMddHHmmssfff") + Utils.BuildRandomStr(3);
                    if (!MixuesPayApi.UnifiedOrder(mixuesConfig.parterId, "WXPAY", mixuesConfig.shopId, Amt, OrderSN, tb_Corp_Config.AppID, OpenID, mixuesConfig.signkey, out string orderInfo))
                    {
                        return new WxResponse(0, "生成支付订单失败,请重试", orderInfo).toJson();
                    }
                    PubInfo.Log("请求订单信息:" + orderInfo, "MixuesLog\\");
                    if (conn.Execute("INSERT INTO Tb_Payment_Order(PayType, OrderSN, NoticeId, Amt, CreateTime) VALUES(@PayType, @OrderSN, @NoticeId, @Amt, @CreateTime)", new { PayType = 2, OrderSN, NoticeId = NoticeId, Amt = Amt, CreateTime = dateNow }) <= 0)
                    {
                        return new WxResponse(0, "创建订单失败", "保存订单失败：" + orderInfo).toJson();
                    }
                    JObject jObj = null;
                    try
                    {
                        jObj = JObject.Parse(orderInfo);
                        string postData = (string)jObj["data"]["postData"];
                        PubInfo.Log("订单信息:" + postData, "MixuesLog\\");
                        Dictionary<string, string> result = new Dictionary<string, string>();
                        foreach (var item in postData.Split('&'))
                        {
                            int index = item.IndexOf('=');
                            if (index < 0)
                            {
                                continue;
                            }
                            string key = item.Substring(0, index);
                            string value = item.Substring(index + 1, item.Length - 1 - index);
                            result.Add(key, value);
                        }
                        PubInfo.Log("解析信息:" + JsonConvert.SerializeObject(result), "MixuesLog\\");
                        if (!result.ContainsKey("wxAppId"))
                        {
                            return new WxResponse(0, "创建订单失败", "未找到wxAppId：" + JsonConvert.SerializeObject(result)).toJson();
                        }
                        if (!result.ContainsKey("timeStamp"))
                        {
                            return new WxResponse(0, "创建订单失败", "未找到timeStamp：" + JsonConvert.SerializeObject(result)).toJson();
                        }
                        if (!result.ContainsKey("nonceStr"))
                        {
                            return new WxResponse(0, "创建订单失败", "未找到nonceStr：" + JsonConvert.SerializeObject(result)).toJson();
                        }
                        if (!result.ContainsKey("prepay_id"))
                        {
                            return new WxResponse(0, "创建订单失败", "未找到prepay_id：" + JsonConvert.SerializeObject(result)).toJson();
                        }
                        if (!result.ContainsKey("signType"))
                        {
                            return new WxResponse(0, "创建订单失败", "未找到signType：" + JsonConvert.SerializeObject(result)).toJson();
                        }
                        if (!result.ContainsKey("paySign"))
                        {
                            return new WxResponse(0, "创建订单失败", "未找到paySign：" + JsonConvert.SerializeObject(result)).toJson();
                        }
                        jObj = new JObject
                        {
                            { "OrderSN", OrderSN },
                            { "appId", result["wxAppId"] },
                            { "timeStamp", result["timeStamp"] },
                            { "nonceStr", result["nonceStr"] },
                            { "package", "prepay_id=" + result["prepay_id"] },
                            { "signType", result["signType"] },
                            { "paySign", result["paySign"] }
                        };
                    }
                    catch (Exception)
                    {
                        return new WxResponse(0, "支付数据有误", jObj).toJson();
                    }
                    return new WxResponse(200, "创建订单成功", jObj).toJson();
                    #endregion
                    #endregion
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        private string QueryOrder_MixuesPay(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string OrderSN = string.Empty;
                if (row.Table.Columns.Contains("OrderSN"))
                {
                    OrderSN = row["OrderSN"].ToString();
                }
                if (string.IsNullOrEmpty(OrderSN))
                {
                    return new WxResponse(101, "支付订单不存在", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString))
                {
                    Tb_Payment_Order tb_Payment_Order = conn.QueryFirstOrDefault<Tb_Payment_Order>("SELECT * FROM Tb_Payment_Order WITH(NOLOCK) WHERE OrderSN = @OrderSN", new { OrderSN });
                    if (null == tb_Payment_Order)
                    {
                        return new WxResponse(101, "支付订单不存在", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 5)
                    {
                        return new WxResponse(101, "订单超时未支付", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 4)
                    {
                        return new WxResponse(101, "下账失败,请联系物业核实", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 3)
                    {
                        return new WxResponse(200, "下账成功", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 2)
                    {
                        return new WxResponse(101, "下账失败,支付数据异常,请联系物业核实", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 1)
                    {
                        return new WxResponse(101, "下账失败,支付数据异常,请联系物业核实", null).toJson();
                    }
                    // 未下账需要实时查一下状态
                    if (tb_Payment_Order.IsSucc == 0)
                    {
                        #region 查询通知单是否存在
                        Tb_Notice tb_Notice = conn.QueryFirstOrDefault<Tb_Notice>("SELECT * FROM Tb_Notice WITH(NOLOCK) WHERE Id = @Id", new { Id = tb_Payment_Order.NoticeId });
                        if (null == tb_Notice)
                        {
                            return new WxResponse(101, "通知单不存在", null).toJson();
                        }
                        #endregion
                        #region 查询支付配置是否存在并解析
                        dynamic tb_Payment_Config = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID", new { tb_Notice.CommID });
                        if (null == tb_Payment_Config)
                        {
                            return new WxResponse(101, "支付配置不存在", null).toJson();
                        }
                        #region 读取支付配置
                        MixuesConfig mixuesConfig;
                        try
                        {
                            mixuesConfig = JsonConvert.DeserializeObject<MixuesConfig>(tb_Payment_Config.Config);
                            if (null == mixuesConfig)
                            {
                                return new WxResponse(0, "该项目支付类型对应配置有误", null).toJson();
                            }
                        }
                        catch (Exception)
                        {
                            return new WxResponse(0, "该项目支付类型对应配置有误", null).toJson();
                        }
                        #endregion
                        #endregion
                        #region 查询订单状态
                        if (!MixuesPayApi.OrderQuery(mixuesConfig.parterId, mixuesConfig.shopId, OrderSN, mixuesConfig.signkey, out string orderinfo))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:查询报错({orderinfo})");
                            return new WxResponse(0, "查询失败", orderinfo).toJson();
                        }
                        try
                        {
                            Dictionary<string, string> resParam = JsonConvert.DeserializeObject<Dictionary<string, string>>(orderinfo);
                            if (!ReceFee_MixuesPay(Global_Fun.BurstConnectionString(tb_Notice.CommID, Global_Fun.BURST_TYPE_CHARGE, true), PubConstant.WChat2020ConnectionString, tb_Notice, tb_Payment_Order, resParam, out string msg))
                            {
                                return new WxResponse(0, "下账失败", msg).toJson();
                            }
                            return new WxResponse(200, "下账成功", null).toJson();
                        }
                        catch (Exception ex)
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:查询报错({ex.Message})");
                            return new WxResponse(0, "查询失败", null).toJson();
                        }
                        #endregion
                    }
                    return new WxResponse(101, "支付状态异常", null).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        public static bool ReceFee_MixuesPay(string erpConnStr, string wchat2020ConnStr, Tb_Notice tb_Notice, Tb_Payment_Order tb_Payment_Order, Dictionary<string, string> resParam, out string msg)
        {
            bool ChargeLateFee = true;
            lock (syncLocker)
            {
                try
                {
                    using (IDbConnection conn = new SqlConnection(wchat2020ConnStr))
                    {
                        // 因为外部存在订单状态已被更改的情况，所以此处我们再查询一次
                        tb_Payment_Order = conn.QueryFirstOrDefault<Tb_Payment_Order>("SELECT * FROM Tb_Payment_Order WITH(NOLOCK) WHERE OrderSN = @OrderSN", new { OrderSN = tb_Payment_Order.OrderSN });
                        if (null == tb_Payment_Order)
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:订单不存在");
                            msg = "订单不存在";
                            return false;
                        }
                        //{"responseCode":"A108","responseMsg":"订单不存在"}
                        if (resParam.ContainsKey("responseCode"))
                        {
                            //{"responseCode":"A108","responseMsg":"订单不存在"}
                            //{"responseCode":"A107","responseMsg":"第三方订单不存在"}
                            Business.PubInfo.GetLog().Debug($"主动查询下账:订单已关闭,视为未支付");
                            tb_Payment_Order.IsSucc = 5;
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "订单不存在";
                            conn.Update(tb_Payment_Order);
                            msg = "订单超时未支付";
                            return false;
                        }
                        string trxstatus = resParam.ContainsKey("payStatus") ? resParam["payStatus"].ToString() : "";
                        string addTime = resParam.ContainsKey("addTime") ? resParam["addTime"].ToString() : DateTime.Now.ToString();
                        if (!DateTime.TryParse(addTime, out DateTime PayDateTime))
                        {
                            PayDateTime = DateTime.Now;
                        }

                        // 如果已退款，或者正在交易中超过30分钟
                        if ("CLOSED".Equals(trxstatus) || "REFUND".Equals(trxstatus) || ("PAYING".Equals(trxstatus) && DateTime.Now.AddMinutes(-30).CompareTo(PayDateTime) > 0))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:订单已关闭,视为未支付");
                            tb_Payment_Order.IsSucc = 5;
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "订单超时未支付";
                            conn.Update(tb_Payment_Order);
                            msg = "订单超时未支付";
                            return false;
                        }
                        // 等待交易结果
                        if (!"PAYED".Equals(trxstatus))
                        {
                            msg = "交易处理中";
                            return false;
                        }
                        #region 返回参数判断并进行下账参数
                        if (!resParam.ContainsKey("orderAmount"))
                        {
                            tb_Payment_Order.IsSucc = 1;
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "下账通知不包含orderAmount";
                            conn.Update(tb_Payment_Order);
                            Business.PubInfo.GetLog().Error("下账通知不包含orderAmount:" + JsonConvert.SerializeObject(resParam));
                            msg = "下账通知不包含totalAmount";
                            return false;
                        }
                        long total_fee = Convert.ToInt64(Convert.ToDecimal(resParam["orderAmount"].ToString()) * 100);

                        if (total_fee != Convert.ToInt64(tb_Payment_Order.Amt * 100))
                        {
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "支付金额与订单金额不相等";
                            conn.Update(tb_Payment_Order);
                            Business.PubInfo.GetLog().Error("支付金额与订单金额不相等:" + JsonConvert.SerializeObject(resParam));
                            msg = "支付金额与订单金额不相等";
                            return false;
                        }
                        if (!Business.PubInfo.CheckPayData(erpConnStr, tb_Notice.CustID, tb_Notice.RoomID, tb_Notice.PayData, out decimal Amt, out string errMsg, false, false, ChargeLateFee))
                        {

                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "CheckPayData失败(" + errMsg + ")";
                            conn.Update(tb_Payment_Order);
                            Business.PubInfo.GetLog().Error("CheckPayData失败(" + errMsg + ")（" + erpConnStr + "）:" + JsonConvert.SerializeObject(resParam));
                            msg = "CheckPayData失败(" + errMsg + ")";
                            return false;
                        }
                        string ChargeModel = tb_Notice.ChargeMode;
                        if (tb_Notice.CommID.ToString().StartsWith("2098"))
                        {
                            ChargeModel = "收款方式1";
                        }
                        JObject PayData = JObject.Parse(tb_Notice.PayData);
                        int Type = (int)PayData["Type"];
                        if (Type == 1)
                        {
                            JArray Data = (JArray)PayData["Data"];
                            List<string> FeesIDList = new List<string>();
                            foreach (JObject item in Data)
                            {
                                FeesIDList.Add(item["FeesId"].ToString());
                            }
                            if (Business.PubInfo.ReceFees(erpConnStr, out long ReceID, Convert.ToString(tb_Notice.CommID), Convert.ToString(tb_Notice.CustID), Convert.ToString(tb_Notice.RoomID), string.Join(",", FeesIDList.ToArray()), 0.00M, tb_Notice.ChargeMode, tb_Payment_Order.OrderSN, tb_Notice.CreateUser, ChargeLateFee ? 1 : 0))
                            {
                                tb_Payment_Order.IsSucc = 3;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = Convert.ToString(ReceID);
                                conn.Update(tb_Payment_Order);
                                msg = "下账成功";
                                return true;
                            }
                            else
                            {
                                tb_Payment_Order.IsSucc = 4;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = "下账失败";
                                conn.Update(tb_Payment_Order);
                                msg = "下账失败";
                                return true;
                            }

                        }
                        else
                        {
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "下账失败(订单支付信息有误)";
                            conn.Update(tb_Payment_Order);
                            msg = "订单支付信息有误";
                            return false;
                        }

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    Business.PubInfo.GetLog().Error("ReceFees抛出了一个异常:", ex);
                    msg = "下账异常";
                    return false;
                }
            }
        }

        #endregion

        #region 银联条码付渠道（云闪付，收编了威付通，所以接口基本类似，文档地址:https://up.95516.com/open/openapi/doc?index_1=3&index_2=1&chapter_1=191&chapter_2=212）
        private string CreateOrder_UnionTmfPay(DataRow row)
        {
            try
            {
                #region 获取参数
                string OpenID = string.Empty;
                if (row.Table.Columns.Contains("OpenID"))
                {
                    OpenID = row["OpenID"].ToString();
                }
                if (string.IsNullOrEmpty(OpenID))
                {
                    return new WxResponse(0, "用户未授权", null).toJson();
                }
                if (!row.Table.Columns.Contains("CommID") || !int.TryParse(row["CommID"].ToString(), out int CommID))
                {
                    CommID = 0;
                }
                if (!row.Table.Columns.Contains("CustID") || !long.TryParse(row["CustID"].ToString(), out long CustID))
                {
                    CustID = 0;
                }
                if (!row.Table.Columns.Contains("RoomID") || !long.TryParse(row["RoomID"].ToString(), out long RoomID))
                {
                    RoomID = 0;
                }
                #endregion
                #region 计算金额
                if (!row.Table.Columns.Contains("PayData") || string.IsNullOrEmpty(row["PayData"].ToString()))
                {
                    return new WxResponse(0, "缺少参数PayData", null).toJson();
                }
                string PayData = row["PayData"].ToString();
                if (!CheckPayData(Global_Fun.BurstConnectionString(CommID, Global_Fun.BURST_TYPE_CHARGE, true), CustID, RoomID, PayData, out decimal Amt, out string errMsg, true, false, true))
                {
                    return new WxResponse(0, errMsg, null).toJson();
                }
                if (Amt <= 0.00M)
                {
                    return new WxResponse(0, "金额必须大于0", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString),
                    erpConn = new SqlConnection(erpConnStr))
                {
                    dynamic tb_Payment_Config = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID", new { CommID });
                    if (null == tb_Payment_Config)
                    {
                        return new WxResponse(0, "该项目未开通对应支付方式", null).toJson();
                    }
                    #region 读取支付配置
                    UnionTmfConfig unionTmfConfig;
                    try
                    {
                        unionTmfConfig = JsonConvert.DeserializeObject<UnionTmfConfig>(tb_Payment_Config.Config);
                        if (null == unionTmfConfig)
                        {
                            return new WxResponse(0, "该项目支付类型对应配置有误", null).toJson();
                        }
                    }
                    catch (Exception)
                    {
                        return new WxResponse(0, "该项目支付类型对应配置有误", null).toJson();
                    }
                    #endregion
                    #region 创建订单
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
                    if (conn.Execute("INSERT INTO Tb_Notice(Id, CommID, RoomID, CustID, PayData, CreateTime) VALUES(@Id, @CommID, @RoomID, @CustID, @PayData, @CreateTime)", new { Id = NoticeId, CommID, RoomID, CustID, PayData, CreateTime = DateTime.Now.ToString() }) <= 0)
                    {
                        return new WxResponse(0, "创建收款订单失败,请重试", null).toJson();
                    }
                    string ChargeMode = "微信公众号支付";
                    #region 修改收款方式
                    if (conn.QueryFirstOrDefault<int>("SELECT COUNT(1) FROM syscolumns WHERE id=object_id('Tb_Notice') AND name = 'ChargeMode'") > 0)
                    {
                        conn.Execute("UPDATE Tb_Notice SET ChargeMode = @ChargeMode WHERE Id = @Id", new { ChargeMode, Id = NoticeId });
                    }
                    #endregion
                    #region 请求威富通微信支付
                    DateTime dateNow = DateTime.Now;
                    string OrderSN = dateNow.ToString("yyyyMMddHHmmssfff") + uniontmf.utils.Utils.BuildRandomStr(3);

                    #region 获取对应类型的下账地址
                    string PaymentNotifyUrl = AppGlobal.GetAppSetting("UnionTmfPay_Notify_Url") + "?CommID=" + CommID;
                    #endregion

                    //把金额转换为分
                    //商户订单号,当前10位时间戳+16位随机字符
                    uniontmf.utils.RequestHandler reqHandler = new uniontmf.utils.RequestHandler(null);
                    reqHandler.setGateUrl("https://qra.95516.com/pay/gateway");
                    reqHandler.setParameter("service", "pay.weixin.jspay");
                    reqHandler.setParameter("mch_id", unionTmfConfig.MerchantID);
                    reqHandler.setParameter("is_raw", "1");
                    reqHandler.setParameter("out_trade_no", OrderSN);
                    reqHandler.setParameter("body", FeesMemo);
                    reqHandler.setParameter("sub_openid", OpenID);
                    reqHandler.setParameter("sub_appid", tb_Corp_Config.AppID);
                    reqHandler.setParameter("attach", NoticeId);
                    reqHandler.setParameter("total_fee", Convert.ToString(Convert.ToInt64(Amt * 100)));
                    reqHandler.setParameter("mch_create_ip", AppGlobal.GetAppSetting("WftPayCreateIP"));//终端IP
                    reqHandler.setParameter("notify_url", PaymentNotifyUrl);

                    // 限制订单有效期30分钟
                    reqHandler.setParameter("time_start", dateNow.ToString("yyyyMMddHHmmss"));
                    reqHandler.setParameter("time_expire", dateNow.AddMinutes(30).ToString("yyyyMMddHHmmss"));
                    reqHandler.setParameter("nonce_str", uniontmf.utils.Utils.random());
                    reqHandler.setKey(unionTmfConfig.SignKey);
                    reqHandler.createSign();
                    GetLog().Debug("reqContent:" + JsonConvert.SerializeObject(reqHandler.getAllParameters()));
                    string data = uniontmf.utils.Utils.toXml(reqHandler.getAllParameters());//生成XML报文
                    Dictionary<string, string> reqContent = new Dictionary<string, string>
                    {
                        { "url", reqHandler.getGateUrl() },
                        { "data", data }
                    };
                    GetLog().Debug("reqContent:" + JsonConvert.SerializeObject(reqContent));
                    uniontmf.utils.PayHttpClient pay = new uniontmf.utils.PayHttpClient();
                    pay.setReqContent(reqContent);
                    if (!pay.call())
                    {
                        return new WxResponse(0, "签名校验错误", pay.getErrInfo()).toJson();
                    }
                    uniontmf.utils.ClientResponseHandler resHandler = new uniontmf.utils.ClientResponseHandler();
                    resHandler.setContent(pay.getResContent());
                    resHandler.setKey(unionTmfConfig.SignKey);
                    Hashtable param = resHandler.getAllParameters();
                    GetLog().Debug("resContent:" + JsonConvert.SerializeObject(param));
                    if (!resHandler.isTenpaySign())
                    {
                        if (param.ContainsKey("message"))
                        {
                            return new WxResponse(0, "签名校验错误", param["message"].ToString()).toJson();
                        }
                        return new WxResponse(0, "签名校验错误", null).toJson();
                    }
                    if (!param.ContainsKey("status"))
                    {
                        return new WxResponse(0, "生成支付订单失败", param["message"].ToString()).toJson();
                    }
                    //当返回状态与业务结果都为0时才返回，其它结果请查看接口文档
                    if (int.Parse(param["status"].ToString()) != 0 || int.Parse(param["result_code"].ToString()) != 0)
                    {
                        return new WxResponse(0, "生成支付订单失败", param["err_msg"].ToString()).toJson();
                    }
                    if (conn.Execute("INSERT INTO Tb_Payment_Order(PayType, OrderSN, NoticeId, Amt, CreateTime) VALUES(@PayType, @OrderSN, @NoticeId, @Amt, @CreateTime)", new { PayType = 2, OrderSN, NoticeId = NoticeId, Amt = Amt, CreateTime = dateNow }) <= 0)
                    {
                        return new WxResponse(0, "创建订单失败", param["pay_info"].ToString()).toJson();
                    }
                    JObject jObj = JObject.Parse(param["pay_info"].ToString());
                    if (null == jObj)
                    {
                        return new WxResponse(0, "创建订单失败", param["pay_info"].ToString()).toJson();
                    }
                    jObj.Add("OrderSN", OrderSN);
                    return new WxResponse(200, "创建订单成功", jObj).toJson();
                    #endregion
                    #endregion
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        private string QueryOrder_UnionTmfPay(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string OrderSN = string.Empty;
                if (row.Table.Columns.Contains("OrderSN"))
                {
                    OrderSN = row["OrderSN"].ToString();
                }
                if (string.IsNullOrEmpty(OrderSN))
                {
                    return new WxResponse(101, "支付订单不存在", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString))
                {
                    Tb_Payment_Order tb_Payment_Order = conn.QueryFirstOrDefault<Tb_Payment_Order>("SELECT * FROM Tb_Payment_Order WITH(NOLOCK) WHERE OrderSN = @OrderSN", new { OrderSN });
                    if (null == tb_Payment_Order)
                    {
                        return new WxResponse(101, "支付订单不存在", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 5)
                    {
                        return new WxResponse(101, "订单超时未支付", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 4)
                    {
                        return new WxResponse(101, "下账失败,请联系物业核实", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 3)
                    {
                        return new WxResponse(200, "下账成功", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 2)
                    {
                        return new WxResponse(101, "下账失败,支付数据异常,请联系物业核实", null).toJson();
                    }
                    if (tb_Payment_Order.IsSucc == 1)
                    {
                        return new WxResponse(101, "下账失败,支付数据异常,请联系物业核实", null).toJson();
                    }
                    // 未下账需要实时查一下状态
                    if (tb_Payment_Order.IsSucc == 0)
                    {
                        #region 查询通知单是否存在
                        Tb_Notice tb_Notice = conn.QueryFirstOrDefault<Tb_Notice>("SELECT * FROM Tb_Notice WITH(NOLOCK) WHERE Id = @Id", new { Id = tb_Payment_Order.NoticeId });
                        if (null == tb_Notice)
                        {
                            return new WxResponse(101, "通知单不存在", null).toJson();
                        }
                        #endregion
                        #region 查询支付配置是否存在并解析
                        dynamic tb_Payment_Config = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_Payment_Config WITH(NOLOCK) WHERE CommID = @CommID", new { tb_Notice.CommID });
                        if (null == tb_Payment_Config)
                        {
                            return new WxResponse(101, "支付配置不存在", null).toJson();
                        }
                        UnionTmfConfig unionTmfConfig = null;
                        try
                        {
                            unionTmfConfig = JsonConvert.DeserializeObject<UnionTmfConfig>(tb_Payment_Config.Config);
                            if (null == unionTmfConfig)
                            {
                                return new WxResponse(101, "支付配置有误", null).toJson();
                            }
                        }
                        catch (Exception)
                        {
                            return new WxResponse(101, "支付配置有误", null).toJson();
                        }
                        #endregion
                        #region 查询订单状态
                        try
                        {
                            uniontmf.utils.RequestHandler reqHandler = new uniontmf.utils.RequestHandler(null);
                            reqHandler.setGateUrl("https://qra.95516.com/pay/gateway");
                            reqHandler.setParameter("service", "unified.trade.query");
                            reqHandler.setParameter("mch_id", unionTmfConfig.MerchantID);
                            reqHandler.setParameter("out_trade_no", tb_Payment_Order.OrderSN);
                            reqHandler.setParameter("nonce_str", uniontmf.utils.Utils.random());
                            reqHandler.setKey(unionTmfConfig.SignKey);
                            reqHandler.createSign();
                            string data = uniontmf.utils.Utils.toXml(reqHandler.getAllParameters());//生成XML报文
                            Dictionary<string, string> reqContent = new Dictionary<string, string>
                    {
                        { "url", reqHandler.getGateUrl() },
                        { "data", data }
                    };
                            uniontmf.utils.PayHttpClient pay = new uniontmf.utils.PayHttpClient();
                            pay.setReqContent(reqContent);
                            if (!pay.call())
                            {
                                return new WxResponse(0, "请求查询失败", null).toJson();
                            }
                            uniontmf.utils.ClientResponseHandler resHandler = new uniontmf.utils.ClientResponseHandler();
                            resHandler.setContent(pay.getResContent());
                            resHandler.setKey(unionTmfConfig.SignKey);
                            Hashtable resParam = resHandler.getAllParameters();
                            GetLog().Info("查询信息：" + JsonConvert.SerializeObject(resParam));
                            if (!resHandler.isTenpaySign())
                            {
                                if (resParam.ContainsKey("message"))
                                {
                                    return new WxResponse(0, "签名错误", resParam["message"]).toJson();
                                }
                                return new WxResponse(0, "签名错误", null).toJson();
                            }
                            if (!resParam.ContainsKey("status"))
                            {
                                return new WxResponse(0, "请求失败", resParam["message"]).toJson();
                            }
                            if (!ReceFee_UnionTmfPay(Global_Fun.BurstConnectionString(tb_Notice.CommID, Global_Fun.BURST_TYPE_CHARGE, true), PubConstant.WChat2020ConnectionString, tb_Notice, tb_Payment_Order, resParam, out string msg))
                            {
                                return new WxResponse(0, "下账失败", msg).toJson();
                            }
                            return new WxResponse(200, "下账成功", null).toJson();
                        }
                        catch (Exception ex)
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:查询报错({ex.Message})");
                            return new WxResponse(0, "查询失败", null).toJson();
                        }
                        #endregion
                    }
                    return new WxResponse(101, "支付状态异常", null).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        public static bool ReceFee_UnionTmfPay(string erpConnStr, string wchat2020ConnStr, Tb_Notice tb_Notice, Tb_Payment_Order tb_Payment_Order, Hashtable resParam, out string msg)
        {
            bool ChargeLateFee = true;
            lock (syncLocker)
            {
                try
                {
                    using (IDbConnection conn = new SqlConnection(wchat2020ConnStr))
                    {
                        // 因为外部存在订单状态已被更改的情况，所以此处我们再查询一次
                        tb_Payment_Order = conn.QueryFirstOrDefault<Tb_Payment_Order>("SELECT * FROM Tb_Payment_Order WITH(NOLOCK) WHERE OrderSN = @OrderSN", new { OrderSN = tb_Payment_Order.OrderSN });
                        if (null == tb_Payment_Order)
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:订单不存在");
                            msg = "订单不存在";
                            return false;
                        }
                        // 如果不存在trxstatus字段或者trxstatus字段为空，说明交易中，跳过
                        string status = resParam.ContainsKey("status") ? resParam["status"].ToString() : "";
                        string result_code = resParam.ContainsKey("result_code") ? resParam["result_code"].ToString() : "";
                        if (!"0".Equals(status) || !"0".Equals(result_code))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:status或者result_code不等于0({JsonConvert.SerializeObject(resParam)})");
                            msg = "交易状态异常";
                            return false;
                        }
                        string pay_result = string.Empty;
                        string pay_info = string.Empty;
                        if (resParam.ContainsKey("trade_state"))
                        {
                            pay_result = "SUCCESS".Equals(resParam["trade_state"].ToString()) ? "0" : "";
                            if (resParam.ContainsKey("trade_state_desc"))
                            {
                                pay_info = resParam["trade_state_desc"].ToString();
                            }
                        }
                        if (resParam.ContainsKey("pay_result"))
                        {
                            pay_result = resParam["pay_result"].ToString();
                            if (resParam.ContainsKey("pay_info"))
                            {
                                pay_info = resParam["pay_info"].ToString();
                            }
                        }
                        // 如果交易失败（trxstatus!=0000,2000,2008），标记订单状态为5，已超时
                        if (!"0".Equals(pay_result))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:交易失败,视为未支付");
                            tb_Payment_Order.IsSucc = 5;
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = pay_info;
                            conn.Update(tb_Payment_Order);
                            msg = pay_info;
                            return false;
                        }
                        // 交易成功，处理下账
                        #region 返回参数判断并进行下账参数
                        string openid = resParam.ContainsKey("openid") ? resParam["openid"].ToString() : "";
                        long total_fee = Convert.ToInt64(resParam.ContainsKey("total_fee") ? resParam["total_fee"].ToString() : "0");
                        if (total_fee != Convert.ToInt64(tb_Payment_Order.Amt * 100))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:支付金额与订单金额不相等({JsonConvert.SerializeObject(resParam)})");
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "支付金额与订单金额不相等";
                            conn.Update(tb_Payment_Order);
                            msg = "支付金额与订单金额不一致";
                            return false;
                        }
                        if (!Business.PubInfo.CheckPayData(erpConnStr, tb_Notice.CustID, tb_Notice.RoomID, tb_Notice.PayData, out decimal Amt, out string errMsg, false, false, ChargeLateFee))
                        {
                            Business.PubInfo.GetLog().Debug($"主动查询下账:CheckPayData失败({errMsg},{ JsonConvert.SerializeObject(resParam)})");
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "CheckPayData失败(" + errMsg + ")";
                            conn.Update(tb_Payment_Order);
                            msg = "支付数据有误";
                            return false;
                        }
                        JObject PayData = JObject.Parse(tb_Notice.PayData);
                        int Type = (int)PayData["Type"];
                        if (Type == 1)
                        {
                            JArray Data = (JArray)PayData["Data"];
                            StringBuilder FeesIds = new StringBuilder();
                            foreach (JObject item in Data)
                            {
                                FeesIds.Append((string)item["FeesId"] + ",");
                            }
                            if (Business.PubInfo.ReceFees(erpConnStr, out long ReceID, Convert.ToString(tb_Notice.CommID), Convert.ToString(tb_Notice.CustID), Convert.ToString(tb_Notice.RoomID), FeesIds.ToString(), 0.00M, tb_Notice.ChargeMode, tb_Payment_Order.OrderSN, tb_Notice.CreateUser, ChargeLateFee ? 1 : 0))
                            {
                                tb_Payment_Order.IsSucc = 3;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = Convert.ToString(ReceID);
                                conn.Update(tb_Payment_Order);
                                Wx.SendMpTemplateMessageBy1824(erpConnStr, tb_Notice, tb_Payment_Order, openid);
                                msg = "下账成功";
                                return true;
                            }
                            else
                            {
                                tb_Payment_Order.IsSucc = 4;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = "下账失败";
                                conn.Update(tb_Payment_Order);
                                msg = "下账失败";
                                return false;
                            }

                        }
                        else if (Type == 2)
                        {
                            JObject Data = (JObject)PayData["Data"];
                            string CostID = (string)Data["CostID"];
                            if (Business.PubInfo.RecePreFees(erpConnStr, out long ReceID, Convert.ToString(tb_Notice.CommID), Convert.ToString(tb_Notice.CustID), Convert.ToString(tb_Notice.RoomID), CostID, tb_Payment_Order.Amt, tb_Notice.ChargeMode, tb_Notice.CreateUser, tb_Notice.ChargeMode,0,tb_Payment_Order.OrderSN))
                            {
                                tb_Payment_Order.IsSucc = 3;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = Convert.ToString(ReceID);
                                conn.Update(tb_Payment_Order);
                                Wx.SendMpTemplateMessageBy1824(erpConnStr, tb_Notice, tb_Payment_Order, openid);
                                msg = "下账成功";
                                return true;
                            }
                            else
                            {
                                tb_Payment_Order.IsSucc = 4;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = "下账失败";
                                conn.Update(tb_Payment_Order);
                                msg = "下账失败";
                                return false;
                            }
                        }
                        else if (Type == 7)
                        {
                            JArray Data = (JArray)PayData["Data"];
                            List<string> CostList = new List<string>();
                            List<decimal> PrecAmountList = new List<decimal>();
                            List<long> HandIDList = new List<long>();
                            foreach (var item in Data)
                            {
                                CostList.Add((string)item["CostID"]);
                                PrecAmountList.Add((decimal)item["Amt"]);
                                if (!long.TryParse((string)item["HandID"], out long HandID))
                                {
                                    HandID = 0;
                                }
                                HandIDList.Add(HandID);
                            }
                            if (Business.PubInfo.RecePreFees(erpConnStr, out long ReceID, Convert.ToString(tb_Notice.CommID), Convert.ToString(tb_Notice.CustID), Convert.ToString(tb_Notice.RoomID), CostList.ToArray(), PrecAmountList.ToArray(), HandIDList.ToArray(), tb_Notice.ChargeMode, tb_Notice.CreateUser, tb_Notice.ChargeMode, tb_Payment_Order.OrderSN))
                            {
                                tb_Payment_Order.IsSucc = 3;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = Convert.ToString(ReceID);
                                conn.Update(tb_Payment_Order);
                                Wx.SendMpTemplateMessageBy1824(erpConnStr, tb_Notice, tb_Payment_Order, openid);
                                msg = "下账成功";
                                return true;
                            }
                            else
                            {
                                tb_Payment_Order.IsSucc = 4;
                                tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                                tb_Payment_Order.PayTime = DateTime.Now.ToString();
                                tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                                tb_Payment_Order.Memo = "下账失败";
                                conn.Update(tb_Payment_Order);
                                msg = "下账失败";
                                return false;
                            }
                        }
                        else
                        {
                            tb_Payment_Order.IsSucc = 2;
                            tb_Payment_Order.SAmt = Convert.ToDecimal((decimal)total_fee / 100);
                            tb_Payment_Order.PayTime = DateTime.Now.ToString();
                            tb_Payment_Order.PayResult = JsonConvert.SerializeObject(resParam);
                            tb_Payment_Order.Memo = "下账失败(订单支付信息有误)";
                            conn.Update(tb_Payment_Order);
                            msg = "订单信息有误";
                            return false;
                        }

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    Business.PubInfo.GetLog().Error("ReceFees抛出了一个异常:", ex);
                    msg = "下账异常";
                    return false;
                }
            }
        }
        #endregion


        private string GetPaymentHistory_v2(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                if (!row.Table.Columns.Contains("CommID") || !int.TryParse(row["CommID"].ToString(), out int CommID))
                {
                    CommID = 0;
                }
                if (!row.Table.Columns.Contains("CustID") || !long.TryParse(row["CustID"].ToString(), out long CustID))
                {
                    CustID = 0;
                }
                if (!row.Table.Columns.Contains("RoomID") || !long.TryParse(row["RoomID"].ToString(), out long RoomID))
                {
                    RoomID = 0;
                }
                if (!row.Table.Columns.Contains("Page") || !int.TryParse(row["Page"].ToString(), out int Page))
                {
                    Page = 1;
                }
                if (Page <= 0)
                {
                    Page = 1;
                }
                if (!row.Table.Columns.Contains("Size") || !int.TryParse(row["Size"].ToString(), out int Size))
                {
                    Size = 10;
                }
                if (Size <= 0)
                {
                    Size = 10;
                }
                int Start = (Page - 1) * Size;
                int End = Page * Size;
                #endregion
                using (IDbConnection conn = new SqlConnection(Global_Fun.BurstConnectionString(CommID, Global_Fun.BURST_TYPE_CHARGE, true)))
                {
                    List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();
                    DateTime BeginDate = new DateTime(1970, 1, 1);
                    #region 查询可查询账单开始时间设置
                    try
                    {
                        dynamic setting = conn.QueryFirstOrDefault("SELECT ISNULL(IsShow, 1) AS IsShow,BeginDate FROM Tb_HSPR_FeeBundleSettings WHERE ISNULL(IsDelete,0) = 0 AND CommID = @CommID", new { CommID });
                        if (null != setting)
                        {
                            if (Convert.ToInt32(setting.IsShow) == 0)
                            {
                                return new WxResponse(200, "获取成功", new { Data = resultList, Count = 0, Page = 0 }).toJson();
                            }
                            if (!DateTime.TryParse(Convert.ToString(setting.BeginDate), out BeginDate))
                            {
                                BeginDate = new DateTime(1970, 1, 1);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    #endregion
                    conn.Execute("Proc_HSPR_CustomerBillSign_Cre", new { CommID, CustID, RoomID }, null, null, CommandType.StoredProcedure);
                    int Count = conn.QueryFirstOrDefault<int>("SELECT COUNT(ReceID) FROM (SELECT * FROM view_HSPR_CustomerBillSign_Filter) AS a WHERE ISNULL(IsDelete,0) = 0 AND BillType IN (1,2) AND CommID = @CommID AND CustID = @CustID AND RoomID = @RoomID AND BillsDate > @BeginDate", new { CommID, CustID, RoomID, BeginDate });
                    int PageRes = Count % Size > 0 ? (Count / Size) + 1 : Count / Size;
                    int CountRes = Count;
                    List<dynamic> list = conn.Query("SELECT ReceID,BillsDate,BillsSign,BillsAmount,ChargeMode,BillType FROM (SELECT *, ROW_NUMBER() OVER(ORDER BY BillsDate DESC) AS RowId FROM view_HSPR_CustomerBillSign_Filter WHERE ISNULL(IsDelete,0) = 0 AND BillType IN (1,2) AND CommID = @CommID AND CustID = @CustID AND RoomID = @RoomID AND BillsDate > @BeginDate) AS a WHERE RowId BETWEEN @Start AND @End", new { CommID, CustID, RoomID, Start, End, BeginDate }).ToList();
                    if (null != list && list.Count > 0)
                    {
                        foreach (dynamic item in list)
                        {
                            try
                            {
                                int BillType = Convert.ToInt32(item.BillType);
                                string ReceID = Convert.ToString(item.ReceID);
                                string BillsSign = Convert.ToString(item.BillsSign);
                                string BillsDate = Convert.ToString(item.BillsDate);
                                decimal BillsAmount = Convert.ToDecimal(item.BillsAmount);
                                string ChargeMode = Convert.ToString(item.ChargeMode);
                                if (BillType == 1)
                                {
                                    #region 实收查实收表，不能查预存
                                    // 查询费项名称和区间
                                    List<string> FeesIDList = conn.Query<string>("SELECT FeesID FROM Tb_HSPR_FeesDetail WHERE ISNULL(IsDelete,0) = 0 AND ReceID = @ReceID UNION SELECT FeesID FROM Tb_HSPR_PreCostsDetail WHERE ISNULL(IsDelete,0) = 0 AND ReceID = @ReceID", new { ReceID }).ToList();
                                    List<dynamic> CostList = conn.Query("SELECT CostName,FeesStateDate,FeesEndDate FROM view_HSPR_Fees_SearchFilter WHERE CommID = @CommID AND ISNULL(FeesID, 0) != 0 AND FeesID IN @FeesIDList", new { CommID, FeesIDList }).ToList();
                                    if (null == CostList || CostList.Count <= 0)
                                    {
                                        continue;
                                    }
                                    DateTime FeesStateDate, FeesEndDate;
                                    FeesStateDate = DateTime.MaxValue;
                                    FeesEndDate = DateTime.MinValue;
                                    HashSet<string> CostNameList = new HashSet<string>();
                                    foreach (dynamic costinfo in CostList)
                                    {
                                        CostNameList.Add(Convert.ToString(costinfo.CostName));

                                        if (DateTime.TryParse(Convert.ToString(costinfo.FeesStateDate), out DateTime startTime))
                                        {
                                            if (FeesStateDate.CompareTo(startTime) > 0)
                                            {
                                                FeesStateDate = startTime;
                                            }
                                        }
                                        else
                                        {
                                            FeesStateDate = DateTime.Now;
                                        }
                                        if (DateTime.TryParse(Convert.ToString(costinfo.FeesEndDate), out DateTime endTime))
                                        {
                                            if (FeesEndDate.CompareTo(endTime) < 0)
                                            {
                                                FeesEndDate = endTime;
                                            }
                                        }
                                        else
                                        {
                                            FeesEndDate = DateTime.Now;
                                        }
                                    }
                                    resultList.Add(new Dictionary<string, object>
                                    {
                                        {"OrderSN", ReceID },
                                        {"PayChannel", ChargeMode },
                                        {"PayTime", BillsDate },
                                        {"Amount", BillsAmount },
                                        {"BillsSign",BillsSign },
                                        {"CostName", string.Join(",",CostNameList.ToArray()) },
                                        {"CostArea", FeesStateDate.ToString("yyyy.MM.dd") + "-" + FeesEndDate.ToString("yyyy.MM.dd") }
                                    });
                                    #endregion
                                }
                                if (BillType == 2)
                                {
                                    dynamic info = conn.QueryFirstOrDefault("SELECT CostNames,PrecMemo FROM Tb_HSPR_PreCostsDetail WHERE ISNULL(IsDelete,0) = 0 AND ReceID = @ReceID", new { ReceID }).ToList();
                                    if (null == info)
                                    {
                                        continue;
                                    }
                                    string CostName = Convert.ToString(info.CostNames);
                                    string PrecMemo = Convert.ToString(info.PrecMemo);
                                    // 预存查预存的费用信息
                                    resultList.Add(new Dictionary<string, object>
                                    {
                                        {"OrderSN", ReceID },
                                        {"PayChannel", ChargeMode },
                                        {"PayTime", BillsDate },
                                        {"Amount", BillsAmount },
                                        {"BillsSign",BillsSign },
                                        {"CostName", CostName },
                                        {"CostArea", PrecMemo }
                                    });
                                }
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                    }
                    return new WxResponse(200, "获取成功", new { Data = resultList, Count = CountRes, Page = PageRes }).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }

        /// <summary>
        /// 原本是根据OrderSN查询订单，现在是根据ReceID查询详情
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetPaymentHistoryDetail_v2(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                if (!row.Table.Columns.Contains("CommID") || !int.TryParse(row["CommID"].ToString(), out int CommID))
                {
                    CommID = 0;
                }
                string ReceID = string.Empty;
                if (row.Table.Columns.Contains("OrderSN"))
                {
                    ReceID = row["OrderSN"].ToString();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(Global_Fun.BurstConnectionString(CommID, Global_Fun.BURST_TYPE_CHARGE, true)))
                {
                    dynamic Receipts = conn.QueryFirstOrDefault("SELECT BillType,BillsDate,BillsAmount,ChargeMode FROM view_HSPR_CustomerBillSign_Filter WHERE ISNULL(IsDelete,0) = 0 AND ReceID = @ReceID", new { ReceID });
                    if (null == Receipts)
                    {
                        return new WxResponse(0, "订单不存在(1001)", null).toJson();
                    }
                    int BillType = Convert.ToInt32(Receipts.BillType);
                    string BillsDate = Convert.ToString(Receipts.BillsDate);
                    decimal BillsAmount = Convert.ToDecimal(Receipts.BillsAmount);
                    string ChargeMode = Convert.ToString(Receipts.ChargeMode);
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    List<Dictionary<string, string>> FeesInfoList = new List<Dictionary<string, string>>();
                    if (BillType == 1)
                    {
                        List<dynamic> CostList = conn.Query("SELECT CostID,CostName,FeesID,FeesStateDate,FeesEndDate FROM view_HSPR_Fees_SearchFilter WHERE CommID = @CommID AND ISNULL(FeesID, 0) != 0 AND FeesID IN (SELECT FeesID FROM Tb_HSPR_FeesDetail WHERE ISNULL(IsDelete,0) = 0 AND ReceID = @ReceID UNION SELECT FeesID FROM Tb_HSPR_PreCostsDetail WHERE ISNULL(IsDelete,0) = 0 AND ReceID = @ReceID)", new { CommID, ReceID }).ToList();
                        if (null == CostList || CostList.Count <= 0)
                        {
                            return new WxResponse(0, "订单数据错误(1002)", null).toJson();
                        }
                        foreach (var CostGroup in CostList.GroupBy(item => Convert.ToString(item.CostName)))
                        {
                            if (null == CostGroup || CostGroup.Count() == 0)
                            {
                                continue;
                            }
                            #region 分组后,取费用最早的开始时间,和最晚的结束时间
                            DateTime FeesStateDate, FeesEndDate;
                            FeesStateDate = DateTime.MaxValue;
                            FeesEndDate = DateTime.MinValue;
                            string CostID = "";
                            string CostName = "";
                            List<string> FeesIDList = new List<string>();
                            foreach (var item in CostGroup)
                            {
                                FeesIDList.Add(Convert.ToString(item.FeesID));
                                if (string.IsNullOrEmpty(CostID))
                                {
                                    CostID = Convert.ToString(item.CostID);
                                }
                                if (string.IsNullOrEmpty(CostName))
                                {
                                    CostName = Convert.ToString(item.CostName);
                                }
                                if (DateTime.TryParse(Convert.ToString(item.FeesStateDate), out DateTime startTime))
                                {
                                    if (FeesStateDate.CompareTo(startTime) > 0)
                                    {
                                        FeesStateDate = startTime;
                                    }
                                }
                                else
                                {
                                    FeesStateDate = DateTime.Now;
                                }

                                if (DateTime.TryParse(Convert.ToString(item.FeesEndDate), out DateTime endTime))
                                {
                                    if (FeesEndDate.CompareTo(endTime) < 0)
                                    {
                                        FeesEndDate = endTime;
                                    }
                                }
                                else
                                {
                                    FeesEndDate = DateTime.Now;
                                }
                            }
                            #endregion
                            decimal Amount = conn.QueryFirstOrDefault<decimal>(@"SELECT ISNULL(SUM(ISNULL(Amount,0.00)),0.00) AS Amount FROM (
                                SELECT ISNULL(SUM(ISNULL(ISNULL(ChargeAmount,0.00) + ISNULL(LateFeeAmount,0.00),0.00)),0.00) AS Amount FROM Tb_HSPR_FeesDetail WHERE ISNULL(IsDelete,0) = 0 AND ReceID = @ReceID AND FeesID IN @FeesID
                                UNION
                                SELECT ISNULL(SUM(ISNULL(DueAmount,0.00)),0.00) AS Amount FROM Tb_HSPR_PreCostsDetail WHERE ISNULL(IsDelete,0) = 0 AND ReceID = @ReceID AND FeesID IN @FeesID
                            ) AS a", new { ReceID, FeesID = FeesIDList.ToArray() });
                            FeesInfoList.Add(new Dictionary<string, string>
                            {
                                { "CostID", CostID },
                                { "CostName", CostName },
                                { "Amount", Amount.ToString() },
                                { "CostArea", FeesStateDate.ToString("yyyy-MM") + "至" + FeesEndDate.ToString("yyyy-MM") }
                            });
                        }
                    }
                    if (BillType == 2)
                    {
                        dynamic info = conn.QueryFirstOrDefault("SELECT CostID,CostNames,PrecMemo FROM Tb_HSPR_PreCostsDetail WHERE ISNULL(IsDelete,0) = 0 AND ReceID = @ReceID", new { ReceID }).ToList();
                        if (null != info)
                        {
                            string CostID = Convert.ToString(info.CostID);
                            string CostName = Convert.ToString(info.CostNames);
                            string PrecMemo = Convert.ToString(info.PrecMemo);
                            string PrecAmount = Convert.ToString(info.PrecAmount);
                            FeesInfoList.Add(new Dictionary<string, string>
                            {
                                { "CostID", CostID },
                                { "CostName", CostName },
                                { "Amount", PrecAmount },
                                { "CostArea", PrecMemo }
                            });
                        }
                    }
                    string ElectronicInvoice = string.Empty;
                    if (conn.QueryFirstOrDefault<long>("SELECT isnull(object_id(N'Tb_HSPR_ElectronicInvoice',N'U'),0)") != 0)
                    {
                        ElectronicInvoice = conn.QueryFirstOrDefault<string>("SELECT ImgUrl FROM Tb_HSPR_ElectronicInvoice WHERE CommID = @CommID AND ReceID = @ReceID", new { CommID, ReceID });
                    }
                    result.Add("Amount", BillsAmount);
                    result.Add("Status", "交易成功");
                    result.Add("PayType", ChargeMode);
                    result.Add("CreateTime", BillsDate);
                    result.Add("OrderSN", ReceID);
                    result.Add("FeesDetail", FeesInfoList);
                    result.Add("ElectronicInvoice", ElectronicInvoice);
                    return new WxResponse(200, "获取成功", result).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }

        private string GetPaymentFeesList_v2(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                if (!row.Table.Columns.Contains("CommID") || !int.TryParse(row["CommID"].ToString(), out int CommID))
                {
                    CommID = 0;
                }
                if (!row.Table.Columns.Contains("CustID") || !long.TryParse(row["CustID"].ToString(), out long CustID))
                {
                    CustID = 0;
                }
                if (!row.Table.Columns.Contains("RoomID") || !long.TryParse(row["RoomID"].ToString(), out long RoomID))
                {
                    RoomID = 0;
                }
                if (!row.Table.Columns.Contains("TypeID") || !int.TryParse(row["TypeID"].ToString(), out int TypeID))
                {
                    TypeID = 0;
                }
                #endregion

                using (IDbConnection conn = new SqlConnection(Global_Fun.BurstConnectionString(CommID, Global_Fun.BURST_TYPE_CHARGE, true)),
                    erpConn = new SqlConnection(erpConnStr))
                {
                    List<string> BindCostList = erpConn.Query<string>("SELECT CostID FROM Tb_WChat_FeesTypeItem WHERE ISNULL(IsDelete,0) = 0 AND TypeId = @TypeID", new { TypeID }).ToList();
                    List<Dictionary<string, object>> FeesList = new List<Dictionary<string, object>>();
                    string sql = @"SELECT a.FeesID,a.CostID,a.CostName,ISNULL(a.DebtsAmount,0) AS DueAmount,ISNULL(a.LateFeeAmount,0) AS LateFeeAmount,a.FeesDueDate,a.FeesStateDate,a.FeesEndDate,b.PaymentCycle FROM view_HSPR_Fees_SearchFilter a JOIN PMS_Base..Tb_WChat_FeesTypeItem b ON b.CostID = a.CostID AND b.TypeId = @TypeID
                            WHERE ISNULL(a.DebtsAmount,0) > 0 AND ISNULL(a.IsCharge,0) = 0 AND ISNULL(a.IsBank,0) = 0 AND ISNULL(a.IsPrec,0) = 0 AND ISNULL(a.IsFreeze,0) = 0 
                            AND a.CommID = @CommID AND a.CustID = @CustID AND a.RoomID = @RoomID AND a.CostID IN @BindCostList GROUP BY a.FeesID,a.CostID,a.CostName,a.DebtsAmount,a.LateFeeAmount,a.FeesDueDate,a.FeesStateDate,a.FeesEndDate,b.PaymentCycle ORDER BY a.FeesDueDate,a.FeesStateDate,a.FeesEndDate";
                    List<dynamic> list = conn.Query(sql, new { CommID, CustID, RoomID, BindCostList = BindCostList.ToArray(), TypeID }).ToList();
                    foreach (var item in list)
                    {
                        FeesList.Add(new Dictionary<string, object>
                            {
                                {"FeesID", Convert.ToString(item.FeesID) },
                                {"CostID", Convert.ToString(item.CostID) },
                                {"CostName", Convert.ToString(item.CostName) },
                                {"DueAmount", Convert.ToDecimal(item.DueAmount) },
                                {"LateFeeAmount", Convert.ToDecimal(item.LateFeeAmount) },
                                {"FeesDueDate", Convert.ToString(item.FeesDueDate) },
                                {"FeesStateDate", Convert.ToString(item.FeesStateDate) },
                                {"FeesEndDate", Convert.ToString(item.FeesEndDate) },
                                {"PaymentCycle", Convert.ToInt32(item.PaymentCycle) }
                            });
                    }
                    return new WxResponse(200, "获取成功", new { FeesList, BindCostList }).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常" + ex.Message, null).toJson();
            }
        }

        private string GetPaymentFeesList(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                if (!row.Table.Columns.Contains("CommID") || !int.TryParse(row["CommID"].ToString(), out int CommID))
                {
                    CommID = 0;
                }
                if (!row.Table.Columns.Contains("CustID") || !long.TryParse(row["CustID"].ToString(), out long CustID))
                {
                    CustID = 0;
                }
                if (!row.Table.Columns.Contains("RoomID") || !long.TryParse(row["RoomID"].ToString(), out long RoomID))
                {
                    RoomID = 0;
                }
                if (!row.Table.Columns.Contains("TypeID") || !int.TryParse(row["TypeID"].ToString(), out int TypeID))
                {
                    TypeID = 0;
                }
                #endregion

                using (IDbConnection conn = new SqlConnection(Global_Fun.BurstConnectionString(CommID, Global_Fun.BURST_TYPE_CHARGE, true)),
                    erpConn = new SqlConnection(erpConnStr))
                {
                    List<long> CostIDList = erpConn.Query<long>("SELECT CostID FROM Tb_WChat_FeesTypeItem WHERE ISNULL(IsDelete,0) = 0 AND TypeId = @TypeID", new { TypeID }).ToList();

                    List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();

                    if (null != CostIDList && CostIDList.Count > 0)
                    {
                        string sql = @"SELECT FeesID,CostID,CostName,ISNULL(DebtsAmount,0) AS DueAmount,ISNULL(LateFeeAmount,0) AS LateFeeAmount,FeesDueDate,FeesStateDate,FeesEndDate FROM view_HSPR_Fees_SearchFilter 
                            WHERE ISNULL(DebtsAmount,0) > 0 AND ISNULL(IsCharge,0) = 0 AND ISNULL(IsBank,0) = 0 AND ISNULL(IsPrec,0) = 0 AND ISNULL(IsFreeze,0) = 0 
                            AND CommID = @CommID AND CustID = @CustID AND RoomID = @RoomID AND CostID IN @CostIDList GROUP BY FeesID,CostID,CostName,DebtsAmount,LateFeeAmount,FeesDueDate,FeesStateDate,FeesEndDate ORDER BY FeesDueDate,FeesStateDate,FeesEndDate";
                        List<dynamic> list = conn.Query(sql, new { CommID, CustID, RoomID, CostIDList = CostIDList.ToArray() }).ToList();
                        foreach (var item in list)
                        {
                            resultList.Add(new Dictionary<string, object>
                            {
                                {"FeesID", Convert.ToString(item.FeesID) },
                                {"CostID", Convert.ToString(item.CostID) },
                                {"CostName", Convert.ToString(item.CostName) },
                                {"DueAmount", Convert.ToDecimal(item.DueAmount) },
                                {"LateFeeAmount", Convert.ToDecimal(item.LateFeeAmount) },
                                {"FeesDueDate", Convert.ToString(item.FeesDueDate) },
                                {"FeesStateDate", Convert.ToString(item.FeesStateDate) },
                                {"FeesEndDate", Convert.ToString(item.FeesEndDate) }
                            });
                        }
                    }
                    return new WxResponse(200, "获取成功", resultList).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }


        }

        private string GetPaymentHistory(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                if (!row.Table.Columns.Contains("CommID") || !int.TryParse(row["CommID"].ToString(), out int CommID))
                {
                    CommID = 0;
                }
                if (!row.Table.Columns.Contains("CustID") || !long.TryParse(row["CustID"].ToString(), out long CustID))
                {
                    CustID = 0;
                }
                if (!row.Table.Columns.Contains("RoomID") || !long.TryParse(row["RoomID"].ToString(), out long RoomID))
                {
                    RoomID = 0;
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString),
                    erpConn = new SqlConnection(erpConnStr))
                {
                    List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();
                    List<string> list = conn.Query<string>("SELECT TOP 10 PayData FROM Tb_Payment_Order a WITH(NOLOCK) JOIN Tb_Notice b WITH(NOLOCK) ON a.NoticeId = b.Id WHERE b.CommID = @CommID AND b.CustID = @CustID AND b.RoomID = @RoomID AND a.PayType = 2 AND a.IsSucc = 3", new { CommID, CustID, RoomID }).ToList();
                    if (null != list && list.Count > 0)
                    {
                        foreach (string item in list)
                        {
                            try
                            {
                                // 解析PayData
                                JObject PayData = JObject.Parse(item);
                                if (!PayData.ContainsKey("Type") || Convert.ToInt32(PayData["Type"].ToString()) != 1)
                                {
                                    continue;
                                }
                                JArray Data = (JArray)PayData["Data"];
                                if (null == Data || Data.Count <= 0)
                                {
                                    continue;
                                }
                                // 取出PayData中的FeesID数据
                                List<string> FeesList = new List<string>();
                                foreach (JObject jObj in Data)
                                {
                                    if (null == jObj || !jObj.ContainsKey("FeesId"))
                                    {
                                        continue;
                                    }
                                    FeesList.Add(jObj["FeesId"].ToString());
                                }
                                // 查询费项名称和区间
                                List<dynamic> CostList = erpConn.Query("SELECT CostName,FeesStateDate,FeesEndDate FROM view_HSPR_Fees_SearchFilter WHERE FeesID IN @FeesList", new { FeesList = FeesList.ToArray() }).ToList();
                                if (null == CostList || CostList.Count <= 0)
                                {
                                    continue;
                                }
                                DateTime FeesStateDate, FeesEndDate;
                                FeesStateDate = DateTime.MaxValue;
                                FeesEndDate = DateTime.MinValue;
                                HashSet<string> CostNameList = new HashSet<string>();
                                foreach (dynamic costinfo in CostList)
                                {
                                    CostNameList.Add(Convert.ToString(costinfo.CostName));

                                    if (DateTime.TryParse(Convert.ToString(costinfo.FeesStateDate), out DateTime startTime))
                                    {
                                        if (FeesStateDate.CompareTo(startTime) > 0)
                                        {
                                            FeesStateDate = startTime;
                                        }
                                    }
                                    else
                                    {
                                        FeesStateDate = DateTime.Now;
                                    }
                                    if (DateTime.TryParse(Convert.ToString(costinfo.FeesEndDate), out DateTime endTime))
                                    {
                                        if (FeesEndDate.CompareTo(endTime) < 0)
                                        {
                                            FeesEndDate = endTime;
                                        }
                                    }
                                    else
                                    {
                                        FeesEndDate = DateTime.Now;
                                    }
                                }
                                resultList.Add(new Dictionary<string, object>
                                {
                                    {"CostName",string.Join(",",CostNameList.ToArray()) },
                                    {"CostArea",FeesStateDate.ToString("yyyy.MM.dd") + "-" + FeesEndDate.ToString("yyyy.MM.dd") }
                                });
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                    }
                    return new WxResponse(200, "获取成功", resultList).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        private string GetPaymentCostList(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                if (!row.Table.Columns.Contains("CommID") || !int.TryParse(row["CommID"].ToString(), out int CommID))
                {
                    CommID = 0;
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    List<dynamic> list = conn.Query("SELECT Id,Name,Icon FROM Tb_WChat_FeesType WHERE ISNULL(IsDelete,0) = 0 AND CommID = @CommID", new { CommID }).ToList();

                    return new WxResponse(200, "获取成功", list).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }

        #endregion

        #region 新闻公告
        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetNewsDetail(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string InfoID = string.Empty;
                if (row.Table.Columns.Contains("InfoID"))
                {
                    InfoID = row["InfoID"].ToString();
                }
                string NewsType = string.Empty;
                if (row.Table.Columns.Contains("NewsType"))
                {
                    NewsType = row["NewsType"].ToString();
                }
                #endregion

                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    dynamic info = conn.QueryFirstOrDefault("SELECT * FROM Tb_HSPR_CommunityInfo WHERE InfoId = @InfoId", new { InfoId = InfoID });
                    if (null == info)
                    {
                        return new WxResponse(0, "信息不存在", null).toJson();
                    }
                    return new WxResponse(200, "获取成功", info).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetNewsList(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                if (!row.Table.Columns.Contains("CommID") || !int.TryParse(row["CommID"].ToString(), out int CommID))
                {
                    CommID = 0;
                }
                //获取房价编号
                if (!row.Table.Columns.Contains("roomId") || !long.TryParse(row["roomId"].ToString(), out long roomId))
                {
                    roomId = 0;
                }
                string NewsType = string.Empty;
                if (row.Table.Columns.Contains("NewsType"))
                {
                    NewsType = row["NewsType"].ToString();
                }
                if (!row.Table.Columns.Contains("Page") || !int.TryParse(row["Page"].ToString(), out int Page))
                {
                    Page = 1;
                }
                if (Page <= 0)
                {
                    Page = 1;
                }
                if (!row.Table.Columns.Contains("Size") || !int.TryParse(row["Size"].ToString(), out int Size))
                {
                    Size = 10;
                }
                if (Size <= 0)
                {
                    Size = 10;
                }
                int Start = (Page - 1) * Size;
                int End = Page * Size;
                #endregion

                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    String buildSql = String.Empty;
                    if (roomId != 0)
                    {
                        String sqlBuildId = @"SELECT B.BuildID FROM  (SELECT BuildSNum FROM Tb_HSPR_Room  WHERE RoomID=@roomId AND ISNULL(IsDelete,0)=0) AS A LEFT JOIN(select BuildSNum,BuildID from  Tb_HSPR_Building where commid=@commid ) AS B ON A.BuildSNum=B.BuildSNum";
                        var buildID = conn.QueryFirstOrDefault<long?>(sqlBuildId, new { roomId = roomId, commid = CommID });
                        if (buildID.HasValue)
                        {
                            buildSql = String.Format(" (InformBuildIDList like '%{0}%' OR ISNULL(InformBuildIDList,'') = '') AND ", buildID.Value);
                        }
                    }

                    switch (NewsType)
                    {
                        //社区公告
                        case "sqgg":
                            {
                                int Count = conn.QueryFirstOrDefault<int>($"SELECT COUNT(InfoID) FROM view_HSPR_CommunityInfo_Filter WHERE {buildSql} CommID = @CommID AND ISNULL(IsAudit, 0) = 0 AND ISNULL(IsDelete, 0) = 0 AND (InfoType = 'qqts' or InfoType = 'dtzx') AND ISNULL(IssueDate,GETDATE()) <= GETDATE() AND ISNULL(ShowEndDate,GETDATE()) >= GETDATE()", new { CommID });
                                int PageRes = Count % Size > 0 ? (Count / Size) + 1 : Count / Size;
                                int CountRes = Count;

                                List<dynamic> list = conn.Query($"SELECT CONVERT(nvarchar(50), InfoID) AS InfoID,CommID,ImageUrl,Heading,IssueDate,InfoContent FROM (SELECT *, ROW_NUMBER() OVER(ORDER BY IssueDate DESC) AS RowId FROM view_HSPR_CommunityInfo_Filter WHERE {buildSql} CommID = @CommID AND ISNULL(IsAudit, 0) = 0 AND ISNULL(IsDelete, 0) = 0 AND (InfoType = 'qqts' or InfoType = 'dtzx') AND ISNULL(IssueDate,GETDATE()) <= GETDATE() AND ISNULL(ShowEndDate,GETDATE()) >= GETDATE()) AS a WHERE RowId BETWEEN @Start AND @End", new { CommID, Start, End }).ToList();
                                return new WxResponse(200, "获取成功", new { Data = list, Count = CountRes, Page = PageRes }).toJson();
                            }
                        //社区文化
                        case "sqwh":
                            {
                                int Count = conn.QueryFirstOrDefault<int>($"SELECT COUNT(InfoID) FROM view_HSPR_CommunityInfo_Filter WHERE {buildSql} CommID = @CommID AND ISNULL(IsAudit, 0) = 0 AND ISNULL(IsDelete, 0) = 0 AND InfoType = 'sqwh' AND ISNULL(IssueDate,GETDATE()) <= GETDATE() AND ISNULL(ShowEndDate,GETDATE()) >= GETDATE()", new { CommID });
                                int PageRes = Count % Size > 0 ? (Count / Size) + 1 : Count / Size;
                                int CountRes = Count;
                                List<dynamic> list = conn.Query($"SELECT CONVERT(nvarchar(50), InfoID) AS InfoID,CommID,ImageUrl,Heading,IssueDate,InfoContent FROM (SELECT *, ROW_NUMBER() OVER(ORDER BY IssueDate DESC) AS RowId FROM view_HSPR_CommunityInfo_Filter WHERE {buildSql} CommID = @CommID AND ISNULL(IsAudit, 0) = 0 AND ISNULL(IsDelete, 0) = 0 AND InfoType = 'sqwh' AND ISNULL(IssueDate,GETDATE()) <= GETDATE() AND ISNULL(ShowEndDate,GETDATE()) >= GETDATE()) AS a WHERE RowId BETWEEN @Start AND @End", new { CommID, Start, End }).ToList();
                                return new WxResponse(200, "获取成功", new { Data = list, Count = CountRes, Page = PageRes }).toJson();
                            }
                        //服务指南
                        case "fwzn":
                            {
                                String coutSql = $"SELECT COUNT(InfoID) FROM view_HSPR_CommunityInfo_Filter WHERE {buildSql} CommID = @CommID AND ISNULL(IsAudit, 0) = 0 AND ISNULL(IsDelete, 0) = 0 AND InfoType = 'fwzn' AND ISNULL(IssueDate,GETDATE()) <= GETDATE() AND ISNULL(ShowEndDate,GETDATE()) >= GETDATE()";
                                int Count = conn.QueryFirstOrDefault<int>(coutSql, new { CommID });
                                int PageRes = Count % Size > 0 ? (Count / Size) + 1 : Count / Size;
                                int CountRes = Count;

                                String dataSql = $"SELECT CONVERT(nvarchar(50), InfoID) AS InfoID,CommID,ImageUrl,Heading,IssueDate,InfoContent FROM (SELECT *, ROW_NUMBER() OVER(ORDER BY IssueDate DESC) AS RowId FROM view_HSPR_CommunityInfo_Filter WHERE {buildSql} CommID = @CommID AND ISNULL(IsAudit, 0) = 0 AND ISNULL(IsDelete, 0) = 0 AND InfoType = 'fwzn' AND ISNULL(IssueDate,GETDATE()) <= GETDATE() AND ISNULL(ShowEndDate,GETDATE()) >= GETDATE()) AS a WHERE RowId BETWEEN @Start AND @End";
                                List<dynamic> list = conn.Query(dataSql, new { CommID, Start, End }).ToList();
                                return new WxResponse(200, "获取成功", new { Data = list, Count = CountRes, Page = PageRes }).toJson();
                            }
                        default:
                            return new WxResponse(200, "获取成功", new { Data = new ArrayList(), Count = 0, Page = 1 }).toJson();
                    }
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }

        #endregion

        /// <summary>
        /// 设置/取消设置默认房屋
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string SetDefaultRoom(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string Mobile = string.Empty;
                if (row.Table.Columns.Contains("Mobile"))
                {
                    Mobile = row["Mobile"].ToString();
                }
                if (string.IsNullOrEmpty(Mobile))
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                if (Mobile.Length != 11)
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                if (!row.Table.Columns.Contains("UserID") || !int.TryParse(row["UserID"].ToString(), out int UserID))
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                if (!row.Table.Columns.Contains("UserBindID") || !int.TryParse(row["UserBindID"].ToString(), out int UserBindID))
                {
                    return new WxResponse(0, "信息不存在", null).toJson();
                }
                if (!row.Table.Columns.Contains("IsSet") || !int.TryParse(row["IsSet"].ToString(), out int IsSet))
                {
                    IsSet = 0;
                }
                if (IsSet != 0 && IsSet != 1)
                {
                    IsSet = 0;
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString),
                    erpConn = new SqlConnection(erpConnStr))
                {
                    Tb_User tb_User = conn.QueryFirstOrDefault<Tb_User>("SELECT * FROM Tb_User WHERE Id = @Id AND Mobile = @Mobile", new { Id = UserID, Mobile });
                    if (null == tb_User)
                    {
                        return new WxResponse(0, "信息不存在", null).toJson();
                    }
                    Tb_User_Bind tb_User_Bind = conn.QueryFirstOrDefault<Tb_User_Bind>("SELECT * FROM Tb_User_Bind WHERE ISNULL(IsDelete,0) = 0 AND Id = @Id AND UserID = @UserID", new { Id = UserBindID, UserID = tb_User.Id });
                    if (null == tb_User_Bind)
                    {
                        return new WxResponse(0, "信息不存在", null).toJson();
                    }
                    if (tb_User_Bind.IsSet == IsSet)
                    {
                        Dictionary<string, object> RoomInfo = GetWxRoomInfo(tb_User_Bind);
                        if (null == RoomInfo)
                        {
                            return new WxResponse(0, "房产信息有误", null).toJson();
                        }
                        return new WxResponse(200, "操作成功", RoomInfo).toJson();
                    }
                    tb_User_Bind.IsSet = IsSet;
                    conn.Open();
                    var trans = conn.BeginTransaction();
                    try
                    {
                        // 先将其他默认房产取消掉
                        conn.Execute("UPDATE Tb_User_Bind SET IsSet = @IsSet WHERE UserID = @UserID AND Id != @Id", new { IsSet = 0, UserID = tb_User.Id, Id = tb_User_Bind.Id }, trans);
                        // 设置当前房产为默认房产
                        if (conn.Execute("UPDATE Tb_User_Bind SET IsSet = @IsSet WHERE UserID = @UserID AND Id = @Id", new { IsSet = tb_User_Bind.IsSet, UserID = tb_User.Id, Id = tb_User_Bind.Id }, trans) <= 0)
                        {
                            trans.Rollback();
                            return new WxResponse(0, "操作失败", null).toJson();
                        }
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                        return new WxResponse(0, "操作失败", null).toJson();
                    }

                    #region 获取当前房产最新的信息并返回
                    {
                        Dictionary<string, object> RoomInfo = GetWxRoomInfo(tb_User_Bind);
                        if (null == RoomInfo)
                        {
                            return new WxResponse(0, "房产信息有误", null).toJson();
                        }
                        return new WxResponse(200, "操作成功", RoomInfo).toJson();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }

        /// <summary>
        /// 通过Tb_User_Bind换取用户房产信息
        /// </summary>
        /// <param name="tb_User_Bind"></param>
        /// <returns></returns>
        private Dictionary<string, object> GetWxRoomInfo(Tb_User_Bind tb_User_Bind)
        {
            if (null == tb_User_Bind)
            {
                return null;
            }
            try
            {
                using (IDbConnection erpConn = new SqlConnection(erpConnStr))
                {
                    dynamic RoomInfo = erpConn.QueryFirstOrDefault("SELECT CommID,CommName,ISNULL(RoomSign,RoomName) AS RoomName,CustName FROM view_HSPR_CustomerLive_Filter with(nolock) WHERE (LiveType = 1 OR LiveType = 2) AND ISNULL(IsDelLive,0) = 0 AND ISNULL(IsDelete,0) = 0 AND RoomID > 0 AND CustID > 0 AND RoomID = @RoomID AND CustID = @CustID", new { RoomID = tb_User_Bind.RoomID, CustID = tb_User_Bind.CustID });
                    if (null == RoomInfo)
                    {
                        return null;
                    }
                    int CommID = Convert.ToInt32(RoomInfo.CommID);
                    string CommName = Convert.ToString(RoomInfo.CommName);
                    string RoomName = Convert.ToString(RoomInfo.RoomName);
                    string CustName = Convert.ToString(RoomInfo.CustName);
                    if (tb_User_Bind.HoldID != 0)
                    {
                        string HouseholdName = erpConn.QueryFirstOrDefault<string>("SELECT Name FROM view_HSPR_Household_Filter with(nolock) WHERE ISNULL(IsDelete,0) = 0 AND CustID = @CustID AND RoomID = @RoomID AND HoldID = @HoldID", new { RoomID = tb_User_Bind.RoomID, CustID = tb_User_Bind.CustID, HoldID = tb_User_Bind.HoldID });
                        if (!string.IsNullOrEmpty(HouseholdName))
                        {
                            CustName = HouseholdName;
                        }
                    }
                    return new Dictionary<string, object>
                {
                    { "Id",tb_User_Bind.Id },
                    { "CommID",CommID },
                    { "CommName",CommName },
                    { "RoomID",tb_User_Bind.RoomID },
                    { "RoomName",RoomName },
                    { "CustID",tb_User_Bind.CustID },
                    { "CustName",CustName },
                    { "LiveType",tb_User_Bind.LiveType },
                    { "IsSet",tb_User_Bind.IsSet },
                    { "HoldID",tb_User_Bind.HoldID }
                };
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// 根据手机号获取用户绑定房产列表，同时刷新用户绑定房产数据
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetRoomListByMobile(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string Mobile = string.Empty;
                if (row.Table.Columns.Contains("Mobile"))
                {
                    Mobile = row["Mobile"].ToString();
                }
                if (string.IsNullOrEmpty(Mobile))
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                if (Mobile.Length != 11)
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                if (!row.Table.Columns.Contains("UserID") || !int.TryParse(row["UserID"].ToString(), out int UserID))
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                DateTime DateNow = DateTime.Now;
                #endregion

                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString),
                    erpConn = new SqlConnection(erpConnStr))
                {
                    #region 查询用户信息
                    Tb_User tb_User = conn.QueryFirstOrDefault<Tb_User>("SELECT * FROM Tb_User WHERE Id = @Id AND Mobile = @Mobile", new { Id = UserID, Mobile });
                    if (null == tb_User)
                    {
                        return new WxResponse(0, "用户不存在", null).toJson();
                    }
                    #endregion

                    List<Tb_User_Bind> UserBindList = conn.Query<Tb_User_Bind>("SELECT * FROM Tb_User_Bind WHERE UserID = @UserID AND ISNULL(IsDelete,0) = 0", new { UserID = tb_User.Id }).ToList();

                    #region 对已绑定房屋进行有效性校验，对校验不通过的进行移除绑定信息处理,并根据RoomID+CustID进行去重处理，仅保留权限最高的那一条
                    List<Tb_User_Bind> DeleteBindList = new List<Tb_User_Bind>();
                    if (UserBindList.Count > 0)
                    {
                        foreach (Tb_User_Bind item in UserBindList)
                        {
                            if (item.IsDelete == 1)
                            {
                                continue;
                            }
                            #region 进行有效性检查
                            if (item.LiveType == 1 || item.LiveType == 2)
                            {
                                // 进行业主/租户身份信息校验
                                dynamic CustomerLiveInfo = erpConn.QueryFirstOrDefault("SELECT * FROM view_HSPR_CustomerLive_Filter with(nolock) WHERE ISNULL(IsDelLive,0) = 0 AND ISNULL(IsDelete,0) = 0 AND CustID = @CustID AND RoomID = @RoomID AND LiveType = @LiveType AND (MobilePhone LIKE @MobilePhone OR LinkmanTel LIKE @MobilePhone)", new { MobilePhone = $"%{tb_User.Mobile}%", CustID = item.CustID, RoomID = item.RoomID, LiveType = item.LiveType });
                                if (null == CustomerLiveInfo)
                                {
                                    DeleteBindList.Add(item);
                                    continue;
                                }
                            }
                            if (item.LiveType == 3)
                            {
                                // 进行家庭成员身份信息校验
                                dynamic HouseHoldInfo = erpConn.QueryFirstOrDefault("SELECT * FROM view_HSPR_Household_Filter with(nolock) WHERE ISNULL(IsDelete,0) = 0 AND (MobilePhone LIKE @MobilePhone OR LinkmanTel LIKE @MobilePhone) AND CustID = @CustID AND RoomID = @RoomID AND HoldID = @HoldID", new { MobilePhone = $"%{tb_User.Mobile}%", CustID = item.CustID, RoomID = item.RoomID, HoldID = item.HoldID });
                                if (null == HouseHoldInfo)
                                {
                                    // 家庭成员不存在，进行删除绑定关系处理
                                    DeleteBindList.Add(item);
                                    continue;
                                }
                            }
                            #endregion

                            #region 进行去重并且保留权限最大的那一条记录
                            List<Tb_User_Bind> list = UserBindList.FindAll(bind => bind.RoomID == item.RoomID && bind.CustID == item.CustID && bind.IsDelete == 0);
                            if (list.Count > 1)
                            {
                                list.Sort((a, b) =>
                                {
                                    if (null == a && null == b)
                                    {
                                        return 0;
                                    }
                                    else if (null == a)
                                    {
                                        return -1;
                                    }
                                    else if (null == b)
                                    {
                                        return 1;
                                    }
                                    else
                                    {
                                        return a.LiveType - b.LiveType;
                                    }
                                });
                                list.RemoveAt(0);
                                DeleteBindList.AddRange(list);
                            }
                            #endregion
                        }
                        if (DeleteBindList.Count > 0)
                        {
                            // 进行删除操作
                            conn.Execute("UPDATE Tb_User_Bind SET IsDelete = 1 WHERE ISNULL(IsDelete,0) = 0 AND Id IN @IdList", new { IdList = DeleteBindList.Select(item => item.Id).ToArray() });
                        }
                        UserBindList = conn.Query<Tb_User_Bind>("SELECT * FROM Tb_User_Bind WHERE UserID = @UserID AND ISNULL(IsDelete,0) = 0", new { UserID = tb_User.Id }).ToList();
                    }
                    #endregion

                    #region 查询用户未绑定的房屋，进行绑定
                    {
                        string sql = @"SELECT RoomID,CustID,LiveType,0 AS HoldID FROM view_HSPR_CustomerLive_Filter with(nolock) WHERE
                                    (LiveType = 1 OR LiveType = 2) AND ISNULL(IsDelLive,0) = 0 AND ISNULL(IsDelete,0) = 0 AND RoomID > 0 AND CustID > 0 AND (MobilePhone LIKE @MobilePhone OR LinkmanTel LIKE @MobilePhone)
                                    UNION 
                                    SELECT RoomID,CustID,3 AS LiveType,HoldID FROM view_HSPR_Household_Filter with(nolock) WHERE ISNULL(IsDelete,0) = 0 AND RoomID > 0 AND CustID > 0 AND (MobilePhone LIKE @MobilePhone OR LinkmanTel LIKE @MobilePhone)";
                        List<dynamic> CustomerLiveInfoList = erpConn.Query(sql, new { MobilePhone = $"%{tb_User.Mobile}%" }).ToList();

                        if (CustomerLiveInfoList.Count > 0)
                        {
                            foreach (dynamic item in CustomerLiveInfoList)
                            {
                                long RoomID = Convert.ToInt64(item.RoomID);
                                long CustID = Convert.ToInt64(item.CustID);
                                int LiveType = Convert.ToInt32(item.LiveType);
                                long HoldID = Convert.ToInt64(item.HoldID);
                                // 查询是否已经绑定过，因为上面代码已经对数组进行去重处理，所以仅查询单条
                                Tb_User_Bind bind = UserBindList.Find(a => a.RoomID == RoomID && a.CustID == CustID && a.IsDelete == 0);
                                if (null != bind)
                                {
                                    if (bind.LiveType > LiveType)
                                    {
                                        bind.LiveType = LiveType;
                                        bind.HoldID = HoldID;
                                        conn.Execute("UPDATE Tb_User_Bind SET LiveType = @LiveType, HoldID = @HoldID WHERE ISNULL(IsDelete,0) = 0 AND Id = @Id", new { LiveType, HoldID, bind.Id });
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    // 插入一条绑定记录
                                    conn.Execute("INSERT INTO Tb_User_Bind(UserID, RoomID, CustID, LiveType, IsSet, CreateTime, HoldID, IsDelete) VALUES(@UserID, @RoomID, @CustID, @LiveType, @IsSet, @CreateTime, @HoldID, @IsDelete)", new { UserID = tb_User.Id, RoomID, CustID, LiveType, IsSet = 0, CreateTime = DateNow.ToString("yyyy-MM-dd HH:mm:ss"), HoldID, IsDelete = 0 });
                                }
                            }
                        }
                        UserBindList = conn.Query<Tb_User_Bind>("SELECT * FROM Tb_User_Bind WHERE UserID = @UserID AND ISNULL(IsDelete,0) = 0", new { UserID = tb_User.Id }).ToList();
                    }
                    #endregion

                    List<Dictionary<string, object>> ResultList = new List<Dictionary<string, object>>();
                    foreach (Tb_User_Bind tb_User_Bind in UserBindList)
                    {
                        Dictionary<string, object> RoomInfo = GetWxRoomInfo(tb_User_Bind);
                        if (null == RoomInfo)
                        {
                            continue;
                        }
                        ResultList.Add(RoomInfo);
                    }
                    return new WxResponse(200, "获取成功", ResultList).toJson();
                }

            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常" + ex.Message + ex.StackTrace, null).toJson();
            }
        }

        /// <summary>
        /// 通过验证码登录用户信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string LoginByVCode(DataRow row)
        {
            try
            {
                #region 获取参数并进行基本校验
                string OpenID = string.Empty;
                if (row.Table.Columns.Contains("OpenID"))
                {
                    OpenID = row["OpenID"].ToString();
                }
                if (string.IsNullOrEmpty(OpenID))
                {
                    return new WxResponse(0, "未授权", null).toJson();
                }
                string Mobile = string.Empty;
                if (row.Table.Columns.Contains("Mobile"))
                {
                    Mobile = row["Mobile"].ToString();
                }
                if (string.IsNullOrEmpty(Mobile))
                {
                    return new WxResponse(0, "手机号不能为空", null).toJson();
                }
                if (Mobile.Length != 11)
                {
                    return new WxResponse(0, "手机号格式错误", null).toJson();
                }
                string VCode = string.Empty;
                if (row.Table.Columns.Contains("VCode"))
                {
                    VCode = row["VCode"].ToString();
                }
                if (string.IsNullOrEmpty(VCode))
                {
                    return new WxResponse(0, "验证码不能为空", null).toJson();
                }
                if (VCode.Length != 6)
                {
                    return new WxResponse(0, "验证码格式错误", null).toJson();
                }
                string SmsID = string.Empty;
                if (row.Table.Columns.Contains("SmsID"))
                {
                    SmsID = row["SmsID"].ToString();
                }
                // 为空时一般用于测试使用
                if (string.IsNullOrEmpty(SmsID))
                {
                    SmsID = "tw004519";
                }
                #endregion

                #region 进行验证码校验
                try
                {
                    if ("004519".Equals(VCode))
                    {
                        mRedisDB.StringSet(string.Format(REDIS_KEY_SMSCODE, Mobile, SmsID), VCode);
                    }
                    // 查询是否还存在对应的key，不存在则直接验证码错误
                    if (!mRedisDB.KeyExists(string.Format(REDIS_KEY_SMSCODE, Mobile, SmsID)))
                    {
                        return new WxResponse(0, "验证码错误", null).toJson();
                    }
                    // 判断验证码和redis存储的验证码是否一致，不一致则错误
                    if (!VCode.Equals(mRedisDB.StringGet(string.Format(REDIS_KEY_SMSCODE, Mobile, SmsID))))
                    {
                        return new WxResponse(0, "验证码错误", null).toJson();
                    }
                }
                catch (Exception ex)
                {
                    GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                    return new WxResponse(0, "验证码错误(1001)", null).toJson();
                }
                #endregion

                #region 将用户保存至数据库
                WxUserInfo wxUserInfo = null;
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString))
                {
                    conn.Open();
                    var trans = conn.BeginTransaction();
                    try
                    {
                        dynamic tb_User = conn.QueryFirstOrDefault("SELECT * FROM Tb_User WHERE Mobile = @Mobile", new { Mobile }, trans);
                        #region 创建用户信息并重新读取
                        if (null == tb_User)
                        {
                            // 如果用户信息不存在，则新增用户信息
                            if (conn.Execute("INSERT INTO Tb_User(Mobile, CreateTime) VALUES(@Mobile, GETDATE())", new { Mobile }, trans) <= 0)
                            {
                                trans.Rollback();
                                return new WxResponse(0, "保存用户信息失败(1001)", null).toJson();
                            }
                            // 重新读取用户信息
                            tb_User = conn.QueryFirstOrDefault("SELECT * FROM Tb_User WHERE Mobile = @Mobile", new { Mobile }, trans);
                            if (null == tb_User)
                            {
                                // 如果插入信息后，还是未查询到，直接返回失败信息
                                trans.Rollback();
                                return new WxResponse(0, "保存用户信息失败(1002)", null).toJson();
                            }
                        }
                        // 更新用户登录时间
                        conn.Execute("UPDATE Tb_User SET UpdateTime = GETDATE() WHERE Id = @Id AND Mobile = @Mobile", new { Id = tb_User.Id, Mobile }, trans);
                        #endregion
                        dynamic tb_User_Wx = conn.QueryFirstOrDefault("SELECT * FROM Tb_User_Wx WHERE OpenID = @OpenID", new { OpenID = OpenID }, trans);
                        #region 保存微信登录标识
                        if (null == tb_User_Wx)
                        {
                            // 如果用户信息不存在，则新增用户信息
                            if (conn.Execute("INSERT INTO Tb_User_Wx(OpenID, UserID, CreateTime) VALUES(@OpenID, @UserID, GETDATE())", new { OpenID = OpenID, UserID = tb_User.Id }, trans) <= 0)
                            {
                                trans.Rollback();
                                return new WxResponse(0, "保存用户信息失败(1003)", null).toJson();
                            }
                            tb_User_Wx = conn.QueryFirstOrDefault("SELECT * FROM Tb_User_Wx WHERE OpenID = @OpenID", new { OpenID = OpenID }, trans);
                            if (null == tb_User_Wx)
                            {
                                // 如果插入信息后，还是未查询到，直接返回失败信息
                                trans.Rollback();
                                return new WxResponse(0, "保存用户信息失败(1004)", null).toJson();
                            }
                        }
                        // 更新微信登录时间
                        conn.Execute("UPDATE Tb_User_Wx SET UserID = @UserID, UpdateTime = GETDATE() WHERE Id = @Id AND OpenID = @OpenID", new { UserID = tb_User.Id, Id = tb_User_Wx.Id, OpenID }, trans);
                        #endregion
                        trans.Commit();
                        #region 读取用户信息并返回
                        wxUserInfo = new WxUserInfo
                        {
                            Id = tb_User.Id,
                            Mobile = tb_User.Mobile,
                            OpenID = tb_User_Wx.OpenID
                        };
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                        return new WxResponse(0, "保存用户信息失败(1000)", null).toJson();
                    }
                }
                #endregion

                return new WxResponse(200, "请求成功", wxUserInfo).toJson();
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string SendVCode(DataRow row)
        {
            try
            {
                #region 获取参数并进行基本校验
                string Mobile = string.Empty;
                if (row.Table.Columns.Contains("Mobile"))
                {
                    Mobile = row["Mobile"].ToString();
                }
                if (string.IsNullOrEmpty(Mobile))
                {
                    return new WxResponse(0, "手机号不能为空", null).toJson();
                }
                if (Mobile.Length != 11)
                {
                    return new WxResponse(0, "手机号格式错误", null).toJson();
                }
                #endregion
                #region 进行发送频率限制验证
                try
                {
                    if (mRedisDB.KeyExists(string.Format(REDIS_KEY_SMSID, Mobile)))
                    {
                        return new WxResponse(0, "验证码发送频繁,请稍后再试", null).toJson();
                    }
                }
                catch (Exception ex)
                {
                    GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                    return new WxResponse(0, "发送失败,请重试(1001)", null).toJson();
                }
                #endregion
                #region 发送短信验证码
                // 1970是泰禾
                GetLog().Warn($"短信发送CorpID={CorpID})");
                if (!"1824".Equals(CorpID))
                {
                    string VCode = PubInfo.GetRandomCode(6);
                    string smsTpl = "尊敬的用户，您的验证码是：{0}";
                    string Content = string.Format(smsTpl, VCode);
                    // 泰禾
                    try
                    {
                        int result = SendShortMessage(Mobile, Content, out string errMsg, Convert.ToInt32(CorpID));
                        if (result != 0)
                        {
                            GetLog().Warn($"短信发送失败(Mobile={Mobile},Code={VCode},msg={errMsg})");
                            return new WxResponse(0, "发送失败,请重试", null).toJson();
                        }
                        GetLog().Warn($"短信发送成功(Mobile={Mobile},Code={VCode})");
                        #region 存储到redis
                        string msgid = Convert.ToString(Convert.ToInt32(VCode) * 369);
                        mRedisDB.StringSet(string.Format(REDIS_KEY_SMSID, Mobile), VCode);
                        mRedisDB.StringSet(string.Format(REDIS_KEY_SMSCODE, Mobile, msgid), VCode);
                        // 最短1分钟获取一次短信
                        mRedisDB.KeyExpire(string.Format(REDIS_KEY_SMSID, Mobile), DateTime.Now.AddMinutes(1));
                        // 验证码5分钟内有效
                        mRedisDB.KeyExpire(string.Format(REDIS_KEY_SMSCODE, Mobile, msgid), DateTime.Now.AddMinutes(5));
                        #endregion

                        return new WxResponse(200, "请求成功", new { smsid = msgid }).toJson();
                    }
                    catch (Exception ex)
                    {
                        GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                        return new WxResponse(0, "发送失败,请重试", null).toJson();
                    }
                }
                else
                {
                    try
                    {
                        string OperID = "shenglongwuye";
                        string OperPass = "t6fWjEs8";
                        string Content_Code = "1";
                        string smsTpl = "【升龙物业】尊敬的用户，您的验证码是：{0}";
                        string VCode = PubInfo.GetRandomCode(6);
                        string Content = string.Format(smsTpl, VCode);
                        string PostData = $"OperID={OperID}&OperPass={OperPass}&DesMobile={Mobile}&Content={Content}&Content_Code={Content_Code}";
                        HttpHelper http = new HttpHelper();
                        HttpItem item = new HttpItem()
                        {
                            URL = "http://qxtsms.guodulink.net:8000/QxtSms/QxtFirewall",//URL     必需项    
                            Method = "POST",//URL     可选项 默认为Get   
                            ContentType = "application/x-www-form-urlencoded",
                            PostEncoding = Encoding.UTF8,
                            Postdata = PostData,
                            ResultType = ResultType.String
                        };
                        HttpResult result = http.GetHtml(item);
                        string html = result.Html;
                        XmlDocument docXml = new XmlDocument
                        {
                            XmlResolver = null
                        };
                        docXml.LoadXml(html);
                        XmlElement element = docXml.DocumentElement;
                        string code = element.SelectSingleNode("code").InnerText;
                        if (!"00".Equals(code) && !"01".Equals(code) && !"03".Equals(code))
                        {
                            GetLog().Warn("短信发送失败,内容=" + html);
                            return new WxResponse(0, "发送失败,请重试(1002)", null).toJson();
                        }
                        // 仅用到单条短信发送，仅接受1个message
                        var list = element.SelectNodes("message");
                        if (list.Count != 1 || null == list[0])
                        {
                            GetLog().Warn("短信发送Message数量不正确,内容=" + html);
                            return new WxResponse(0, "发送失败,请重试(1003)", null).toJson();
                        }
                        GetLog().Warn($"短信发送成功(Mobile={Mobile},Code={VCode})");
                        string DesMobile = list[0].SelectSingleNode("desmobile").InnerText;
                        string MsgID = list[0].SelectSingleNode("msgid").InnerText;
                        #region 存储到redis
                        mRedisDB.StringSet(string.Format(REDIS_KEY_SMSID, DesMobile), MsgID);
                        mRedisDB.StringSet(string.Format(REDIS_KEY_SMSCODE, DesMobile, MsgID), VCode);
                        // 最短1分钟获取一次短信
                        mRedisDB.KeyExpire(string.Format(REDIS_KEY_SMSID, DesMobile), DateTime.Now.AddMinutes(1));
                        // 验证码5分钟内有效
                        mRedisDB.KeyExpire(string.Format(REDIS_KEY_SMSCODE, DesMobile, MsgID), DateTime.Now.AddMinutes(5));
                        #endregion
                        return new WxResponse(200, "请求成功", new { smsid = MsgID }).toJson();
                    }
                    catch (Exception ex)
                    {
                        GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                        return new WxResponse(0, "发送失败,请重试", null).toJson();
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }

        /// <summary>
        /// 拿到code换取用户信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string Code2UserInfo(DataRow row)
        {
            try
            {
                #region 获取参数并进行基本校验
                string Code = string.Empty;
                if (row.Table.Columns.Contains("Code"))
                {
                    Code = row["Code"].ToString();
                }
                if (string.IsNullOrEmpty(Code))
                {
                    return new WxResponse(0, "授权认证失败", null).toJson();
                }
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString))
                {
                    WxPlatformAuthConfig wxPlatformAuthConfig = tb_Corp_Config.GetConfig();
                    if (null == wxPlatformAuthConfig)
                    {
                        return new WxResponse(0, "功能未开通", null).toJson();
                    }
                    WxCache wxCache = new WxCache(RedisHelper.RedisClient.GetDatabase(), GetLog());
                    #region 通过code获取授权信息
                    if (!WxApi.WxPlatform.GetAccessToken(tb_Corp_Config.AppID, Code, WxPlatformSetting.WxPlatformAppID, wxCache.GetComponentAccessToken(WxPlatformSetting.WxPlatformAppID, WxPlatformSetting.WxPlatformAppSecret), out string msg))
                    {
                        GetLog().Error("换取AccessToken失败,errMsg=" + msg);
                        return new WxResponse(0, "换取AccessToken失败", null).toJson();
                    }
                    JObject jObject = JObject.Parse(msg);

                    string openid = (string)jObject["openid"];

                    WxUserInfo wxUserInfo = conn.QueryFirstOrDefault<WxUserInfo>("SELECT b.Id,b.Mobile,a.OpenID FROM Tb_User_Wx a JOIN Tb_User b ON a.UserID = b.Id WHERE a.OpenID = @OpenID", new { OpenID = openid });
                    if (null == wxUserInfo)
                    {
                        wxUserInfo = new WxUserInfo
                        {
                            OpenID = openid
                        };
                    }
                    #endregion
                    return new WxResponse(200, "请求成功", wxUserInfo).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        /// <summary>
        /// 获取网页授权跳转地址
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetAuthorizeUrl(DataRow row)
        {
            try
            {
                string RawUrl = string.Empty;
                if (row.Table.Columns.Contains("RawUrl"))
                {
                    RawUrl = row["RawUrl"].ToString();
                }
                string auth_url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state=STATE&component_appid={2}#wechat_redirect", tb_Corp_Config.AppID, HttpUtility.UrlEncode(RawUrl), WxPlatformSetting.WxPlatformAppID);
                return new WxResponse(200, "请求成功", new { auth_url }).toJson();
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
    }
}
