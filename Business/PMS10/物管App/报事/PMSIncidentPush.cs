using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using Business.PMS10.物管App.报事.Enum;
using Business.PMS10.物管App.报事.Models;
using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using TWTools.Push;

namespace Business
{
    public partial class PMSIncidentPush
    {
        private static char[] SplitChars = new char[] { ',', '，', ' ', '、', '|', '/', '\\', ';', '；' };

        public static PMSIncidentAcceptModel GetIncidentInfo(long incidentId)
        {
            try
            {
                using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    var sql = @"SELECT * FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID";

                    var incidentInfo = conn.Query<PMSIncidentAcceptModel>(sql, new { IncidentID = incidentId }).FirstOrDefault();
                    if (incidentInfo != null)
                    {
                        sql = @"SELECT CommName FROM Tb_HSPR_Community WHERE CommID=@CommID;
                                SELECT RoomName FROM Tb_HSPR_Room WHERE RoomID=@RoomID;
                                SELECT RegionalPlace FROM Tb_HSPR_IncidentRegional WHERE RegionalID=@RegionalID;";

                        var reader = conn.QueryMultiple(sql, new
                        {
                            CommID = incidentInfo.CommID,
                            RoomID = incidentInfo.RoomID,
                            RegionalID = incidentInfo.RegionalID
                        });

                        incidentInfo.CommName = reader.Read<string>().FirstOrDefault();
                        incidentInfo.RoomName = reader.Read<string>().FirstOrDefault();
                        incidentInfo.RegionalPlace = reader.Read<string>().FirstOrDefault();
                    }

                    return incidentInfo;
                }
            }
            catch (Exception e)
            {
                new Logger().WriteLog("接口推送日志", incidentId + "获取报事报错" + e.ToString());
                return null;
            }
        }

        private static void SynchPushIncidentAction(long incidentId, Action<PMSIncidentAcceptModel, PushModel, Tb_System_CorpAppPushSet> action)
        {
            SynchPushIncident(GetIncidentInfo(incidentId), action);
        }

        private static void SynchPushIncident(PMSIncidentAcceptModel incidentInfo, Action<PMSIncidentAcceptModel, PushModel, Tb_System_CorpAppPushSet> action)
        {
            if (incidentInfo == null || incidentInfo.IsDelete == 1)
                return;

            try
            {
                var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
                var factory = new TaskFactory(taskScheduler);
                factory.StartNew((httpContext) =>
                {
                    HttpContext.Current = (HttpContext)httpContext;

                    if (GetAppPushSetting(AppGlobal.StrToInt(Global_Var.CorpId), out Tb_System_CorpAppPushSet setting))
                    {
                        // 推送App消息
                        PushModel pushModel = new PushModel()
                        {
                            AppIdentifier = Global_Var.CorpId,
                            AppKey = setting.PropertyAppKey,
                            AppSecret = setting.PropertyAppSecret,

                            Badge = 1,
                            Sound = "push_alert_property.wav",
                            LowerExtraKey = false,
                            KeyInfomation = incidentInfo.IncidentID.ToString(),
                        };
                        pushModel.Audience.Category = PushAudienceCategory.Alias;
                        pushModel.Extras.Add("CommID", incidentInfo.CommID.ToString());
                        pushModel.Extras.Add("IncidentID", incidentInfo.IncidentID.ToString());
                        pushModel.Extras.Add("IncidentPlace", incidentInfo.IncidentPlace);
                        pushModel.Extras.Add("Class", incidentInfo.DrClass.ToString());

                        action?.Invoke(incidentInfo, pushModel, setting);
                    }
                }, HttpContext.Current);
            }
            catch (Exception)
            {

            }
        }

        #region 报事动作

        /// <summary>
        /// 报事受理后推送
        /// </summary>
        public static void SynchPushIncidentAccepted(long incidentId)
        {
            SynchPushIncidentAction(incidentId, (incidentInfo, pushModel, setting) =>
            {
                using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    var list = GetPushModel(PMSIncidentAction.Accepted, conn).OrderBy(obj => obj.AudienceType);

                    pushModel.PMSIncidentAction = (int)PMSIncidentAction.Accepted;
                    foreach (var item in list)
                    {
                        pushModel.Title = $"【{incidentInfo.CommName}】有新的报事信息";
                        pushModel.Audience.Objects.Clear();
                        if (incidentInfo.IncidentPlace == "户内")
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RoomName}】{item.PushContent}";
                        else
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RegionalPlace}】{item.PushContent}";
                        switch (item.AudienceType)
                        {
                            // 分派岗位，通知派单
                            case PMSIncidentPushAudienceType.AssignRole:
                                pushModel.Audience.Objects.AddRange(GetCanAssignUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.INCIDENT_ASSIGN;
                                break;

                            // 处理岗位，通知抢单
                            case PMSIncidentPushAudienceType.ProcessRole:
                                pushModel.Audience.Objects.AddRange(GetCanSnatchUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.INCIDENT_SNATCH;
                                break;

                            // 楼栋管家或房屋管家，通知派单
                            case PMSIncidentPushAudienceType.RoomHousekeeper:
                                if (incidentInfo.IncidentPlace != "户内")
                                    continue;

                                pushModel.Audience.Objects.AddRange(GetHousekeeperMobiles(incidentInfo.CommID, incidentInfo.RoomID, conn));
                                pushModel.Command = PushCommand.INCIDENT_ASSIGN;
                                break;

                            // 公区管家，通知派单
                            case PMSIncidentPushAudienceType.RegionHousekeeper:
                                if (incidentInfo.IncidentPlace != "公区")
                                    continue;

                                pushModel.Audience.Objects.AddRange(GetRegionkeeperMobiles(incidentInfo.CommID, incidentInfo.RegionalID, conn));
                                pushModel.Command = PushCommand.INCIDENT_ASSIGN;
                                break;

                            // 报事员工，通知已受理
                            case PMSIncidentPushAudienceType.IncidentEmployee:
                                if (incidentInfo.IncidentSource != "自查报事")
                                    continue;

                                pushModel.Audience.Objects.AddRange(GetIncidentManMobiles(incidentInfo.IncidentID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;

                            // 报事客户，通知已受理
                            case PMSIncidentPushAudienceType.Customer:
                                if (incidentInfo.IncidentSource != "客户报事")
                                    continue;

                                if (setting.CustAppKey == null || setting.CustAppSecret == null)
                                    continue;

                                pushModel.Audience.Objects.AddRange(GetCustomerMobiles(incidentInfo.CustID, conn));
                                pushModel.Command = PushCommand.NORMAL;

                                pushModel.AppKey = setting.CustAppKey;
                                pushModel.AppSecret = setting.CustAppSecret;
                                pushModel.Sound = "default";
                                break;
                            default: continue;
                        }

                        // 推送App消息
                        SynchPushNotification(item, pushModel);

                        // 发送手机短信
                        SynchSendShortMessage(item, pushModel);
                    }
                }
            });
        }

        /// <summary>
        /// 报事分派后推送
        /// </summary>
        public static void SynchPushIncidentAssigned(long incidentId)
        {
            SynchPushIncidentAction(incidentId, (incidentInfo, pushModel, setting) =>
            {
                using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    var list = GetPushModel(PMSIncidentAction.Assigned, conn);

                    pushModel.PMSIncidentShortMessage = AliyunSMS.incidentAssignParam(incidentInfo.DispMan, incidentInfo.IncidentPlace, incidentInfo.IncidentMan);
                    pushModel.PMSIncidentAction = (int)PMSIncidentAction.Assigned;
                    foreach (var item in list)
                    {
                        pushModel.Title = $"【{incidentInfo.CommName}】有新的报事{(incidentInfo.IsSnatch ? "抢单" : "派单")}信息";
                        pushModel.Audience.Objects.Clear();

                        if (incidentInfo.IncidentPlace == "户内")
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RoomName}】{item.PushContent}";
                        else
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RegionalPlace}】{item.PushContent}";

                        switch (item.AudienceType)
                        {
                            // 抢单后，通知分派岗位
                            case PMSIncidentPushAudienceType.AssignRole:
                                if (incidentInfo.IsSnatch == false)
                                    continue;

                                pushModel.Audience.Objects.AddRange(GetCanAssignUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;

                            // 抢单后，通知楼栋管家或房屋管家
                            case PMSIncidentPushAudienceType.RoomHousekeeper:
                                if (incidentInfo.IsSnatch == false || incidentInfo.IncidentSource != "客户报事")
                                    continue;

                                pushModel.Audience.Objects.AddRange(GetHousekeeperMobiles(incidentInfo.CommID, incidentInfo.RoomID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;

                            // 抢单后，通知公区管家
                            case PMSIncidentPushAudienceType.RegionHousekeeper:
                                if (incidentInfo.IsSnatch == false || incidentInfo.IncidentPlace != "公区")
                                    continue;

                                pushModel.Audience.Objects.AddRange(GetRegionkeeperMobiles(incidentInfo.CommID, incidentInfo.RegionalID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;

                            // 派单后，通知受理员工
                            case PMSIncidentPushAudienceType.IncidentEmployee:
                                if (incidentInfo.IsSnatch)
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetIncidentManMobiles(incidentInfo.IncidentID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;

                            // 派单后，通知处理人处理
                            case PMSIncidentPushAudienceType.DealMan:
                                if (incidentInfo.IsSnatch)
                                    continue;

                                pushModel.Audience.Objects.AddRange(GetCanSnatchUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.INCIDENT_PROCESSING;
                                break;

                            // 派单后，通知协助人处理
                            case PMSIncidentPushAudienceType.AssistantMan:
                                if (incidentInfo.IsSnatch)
                                    continue;

                                pushModel.Audience.Objects.AddRange(GetCanSnatchUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.INCIDENT_PROCESSING;
                                break;

                            // 抢单或派单后，通知报事客户
                            case PMSIncidentPushAudienceType.Customer:
                                if (incidentInfo.IncidentSource != "客户报事")
                                    continue;

                                pushModel.Audience.Objects.AddRange(GetCustomerMobiles(incidentInfo.CustID, conn));
                                pushModel.Command = PushCommand.NORMAL;

                                pushModel.AppKey = setting.CustAppKey;
                                pushModel.AppSecret = setting.CustAppSecret;
                                pushModel.Sound = "default";
                                break;
                            default: continue;
                        }

                        // 推送App消息
                        SynchPushNotification(item, pushModel);

                        // 发送手机短信
                        SynchSendShortMessage(item, pushModel);
                    }
                }
            });
        }

        /// <summary>
        /// 报事接单后推送
        /// </summary>
        public static void SynchPushIncidentReceived(long incidentId)
        {
            SynchPushIncidentAction(incidentId, (incidentInfo, pushModel, setting) =>
            {
                using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    var list = GetPushModel(PMSIncidentAction.Received, conn).OrderBy(obj => obj.AudienceType);

                    foreach (var item in list)
                    {
                        pushModel.PMSIncidentAction = (int)PMSIncidentAction.Received;
                        pushModel.Title = $"【{incidentInfo.CommName}】有新的报事接单信息";
                        pushModel.Audience.Objects.Clear();

                        if (incidentInfo.IncidentPlace == "户内")
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RoomName}】{item.PushContent}";
                        else
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RegionalPlace}】{item.PushContent}";

                        switch (item.AudienceType)
                        {
                            // 分派岗位，通知派单
                            case PMSIncidentPushAudienceType.AssignRole:
                                pushModel.Audience.Objects.AddRange(GetCanAssignUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;

                            // 楼栋管家、房屋管家
                            case PMSIncidentPushAudienceType.RoomHousekeeper:
                                if (incidentInfo.IncidentPlace != "户内")
                                    continue;

                                pushModel.Audience.Objects.AddRange(GetHousekeeperMobiles(incidentInfo.CommID, incidentInfo.RoomID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;

                            // 公区管家
                            case PMSIncidentPushAudienceType.RegionHousekeeper:
                                if (incidentInfo.IncidentPlace != "公区")
                                    continue;

                                pushModel.Audience.Objects.AddRange(GetRegionkeeperMobiles(incidentInfo.CommID, incidentInfo.RegionalID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;

                            // 报事客户
                            case PMSIncidentPushAudienceType.Customer:
                                if (incidentInfo.IncidentSource != "客户报事")
                                    continue;

                                pushModel.Audience.Objects.AddRange(GetCustomerMobiles(incidentInfo.CustID, conn));
                                pushModel.Command = PushCommand.NORMAL;

                                pushModel.AppKey = setting.CustAppKey;
                                pushModel.AppSecret = setting.CustAppSecret;
                                pushModel.Sound = "default";

                                break;
                            //  报事员工
                            case PMSIncidentPushAudienceType.IncidentEmployee:
                                if (incidentInfo.IncidentSource != "自查报事")
                                    continue;

                                pushModel.Audience.Objects.AddRange(GetIncidentManMobiles(incidentInfo.IncidentID, conn));
                                pushModel.Command = PushCommand.NORMAL;

                                break;
                            default: continue;
                        }

                        // 推送App消息
                        SynchPushNotification(item, pushModel);

                        // 发送手机短信
                        SynchSendShortMessage(item, pushModel);
                    }
                }
            });
        }

        /// <summary>
        /// 报事到场后推送
        /// </summary>
        public static void SynchPushIncidentArrived(long incidentId)
        {
            SynchPushIncidentAction(incidentId, (incidentInfo, pushModel, setting) =>
            {
                using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    var list = GetPushModel(PMSIncidentAction.Arrived, conn).OrderBy(obj => obj.AudienceType);

                    foreach (var item in list)
                    {
                        pushModel.PMSIncidentAction = (int)PMSIncidentAction.Arrived;
                        pushModel.Title = $"【{incidentInfo.CommName}】有新的报事到场信息";
                        pushModel.Audience.Objects.Clear();

                        if (incidentInfo.IncidentPlace == "户内")
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RoomName}】{item.PushContent}";
                        else
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RegionalPlace}】{item.PushContent}";

                        switch (item.AudienceType)
                        {
                            // 分派岗位，通知派单
                            case PMSIncidentPushAudienceType.AssignRole:
                                pushModel.Audience.Objects.AddRange(GetCanAssignUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            // 楼栋管家、房屋管家
                            case PMSIncidentPushAudienceType.RoomHousekeeper:
                                if (incidentInfo.IncidentPlace != "客户报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetHousekeeperMobiles(incidentInfo.CommID, incidentInfo.RoomID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            // 公区管家
                            case PMSIncidentPushAudienceType.RegionHousekeeper:
                                if (incidentInfo.IncidentPlace != "公区报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetRegionkeeperMobiles(incidentInfo.CommID, incidentInfo.RegionalID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            // 报事客户
                            case PMSIncidentPushAudienceType.Customer:
                                if (incidentInfo.IncidentPlace != "客户报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetCustomerMobiles(incidentInfo.CustID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            //  报事员工
                            case PMSIncidentPushAudienceType.IncidentEmployee:
                                if (incidentInfo.IncidentPlace != "自查报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetIncidentManMobiles(incidentInfo.IncidentID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            default: continue;
                        }

                        // 推送App消息
                        SynchPushNotification(item, pushModel);

                        // 发送手机短信
                        SynchSendShortMessage(item, pushModel);
                    }
                }
            });
        }

        /// <summary>
        /// 口派转书面后推送
        /// </summary>
        public static void SynchPushIncidentVerbalToWritten(long incidentId)
        {
            SynchPushIncidentAction(incidentId, (incidentInfo, pushModel, setting) =>
            {
                using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    var list = GetPushModel(PMSIncidentAction.VerbalToWritten, conn).OrderBy(obj => obj.AudienceType);

                    foreach (var item in list)
                    {
                        pushModel.PMSIncidentAction = (int)PMSIncidentAction.VerbalToWritten;
                        pushModel.Title = $"【{incidentInfo.CommName}】有新的报事转单信息";
                        pushModel.Audience.Objects.Clear();

                        if (incidentInfo.IncidentPlace == "户内")
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RoomName}】{item.PushContent}";
                        else
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RegionalPlace}】{item.PushContent}";

                        switch (item.AudienceType)
                        {
                            // 分派岗位，通知派单
                            case PMSIncidentPushAudienceType.AssignRole:
                                pushModel.Audience.Objects.AddRange(GetCanAssignUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.INCIDENT_ASSIGN;
                                break;

                            // 楼栋管家、房屋管家
                            case PMSIncidentPushAudienceType.DealMan:
                                pushModel.Audience.Objects.AddRange(GetDealManMobiles(incidentInfo.IncidentID, conn));
                                pushModel.Command = PushCommand.INCIDENT_PROCESSING;
                                break;
                            // 协助员工
                            case PMSIncidentPushAudienceType.AssistantMan:
                                pushModel.Audience.Objects.AddRange(GetAssistantManMobiles(incidentInfo.IncidentID, conn));
                                pushModel.Command = PushCommand.INCIDENT_PROCESSING;
                                break;
                            // 客户管家
                            case PMSIncidentPushAudienceType.RoomHousekeeper:
                                if (incidentInfo.IncidentSource != "客户报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetHousekeeperMobiles(incidentInfo.CommID, incidentInfo.RoomID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            // 公区管家
                            case PMSIncidentPushAudienceType.RegionHousekeeper:
                                if (incidentInfo.IncidentSource != "公区报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetRegionkeeperMobiles(incidentInfo.CommID, incidentInfo.RegionalID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            // 报事客户
                            case PMSIncidentPushAudienceType.Customer:
                                if (incidentInfo.IncidentSource != "客户报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetCustomerMobiles(incidentInfo.CustID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            // 报事员工
                            case PMSIncidentPushAudienceType.IncidentEmployee:
                                if (incidentInfo.IncidentSource != "自查报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetIncidentManMobiles(incidentInfo.IncidentID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            default: continue;
                        }

                        // 推送App消息
                        SynchPushNotification(item, pushModel);

                        // 发送手机短信
                        SynchSendShortMessage(item, pushModel);
                    }
                }
            });
        }

        /// <summary>
        /// 报事转发后推送
        /// </summary>
        public static void SynchPushIncidentTranspond(long incidentId)
        {
            SynchPushIncidentAction(incidentId, (incidentInfo, pushModel, setting) =>
            {
                using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    var list = GetPushModel(PMSIncidentAction.Transpond, conn).OrderBy(obj => obj.AudienceType);

                    pushModel.PMSIncidentShortMessage = AliyunSMS.incidentAssignParam(incidentInfo.DispMan, incidentInfo.IncidentPlace, incidentInfo.IncidentMan);
                    pushModel.PMSIncidentAction = (int)PMSIncidentAction.Transpond;
                    foreach (var item in list)
                    {
                        pushModel.Title = $"【{incidentInfo.CommName}】有新的报事转派信息";
                        pushModel.Audience.Objects.Clear();

                        if (incidentInfo.IncidentPlace == "户内")
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RoomName}】{item.PushContent}";
                        else
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RegionalPlace}】{item.PushContent}";

                        switch (item.AudienceType)
                        {
                            //分派岗位
                            case PMSIncidentPushAudienceType.AssignRole:
                                pushModel.Audience.Objects.AddRange(GetCanAssignUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            //责任员工
                            case PMSIncidentPushAudienceType.DealMan:
                                pushModel.Audience.Objects.AddRange(GetDealManMobiles(incidentInfo.IncidentID, conn));
                                pushModel.Command = PushCommand.INCIDENT_PROCESSING;
                                break;
                            //协助员工
                            case PMSIncidentPushAudienceType.AssistantMan:
                                pushModel.Audience.Objects.AddRange(GetAssistantManMobiles(incidentInfo.IncidentID, conn));
                                pushModel.Command = PushCommand.INCIDENT_PROCESSING;
                                break;
                            // 客户管家
                            case PMSIncidentPushAudienceType.RoomHousekeeper:
                                if (incidentInfo.IncidentSource != "客户报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetHousekeeperMobiles(incidentInfo.CommID, incidentInfo.RoomID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            // 公区管家
                            case PMSIncidentPushAudienceType.RegionHousekeeper:
                                if (incidentInfo.IncidentSource != "公区报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetRegionkeeperMobiles(incidentInfo.CommID, incidentInfo.RegionalID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            // 报事客户
                            case PMSIncidentPushAudienceType.Customer:
                                if (incidentInfo.IncidentSource != "客户报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetCustomerMobiles(incidentInfo.CustID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            // 报事员工
                            case PMSIncidentPushAudienceType.IncidentEmployee:
                                if (incidentInfo.IncidentSource != "自查报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetIncidentManMobiles(incidentInfo.IncidentID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            default: continue;
                        }

                        // 推送App消息
                        SynchPushNotification(item, pushModel);

                        // 发送手机短信
                        SynchSendShortMessage(item, pushModel);
                    }
                }

            });
        }

        /// <summary>
        /// 报事处理完毕后推送
        /// </summary>
        public static void SynchPushIncidentDealt(long incidentId)
        {
            SynchPushIncidentAction(incidentId, (incidentInfo, pushModel, setting) =>
            {
                using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    var list = GetPushModel(PMSIncidentAction.Dealt, conn).OrderBy(obj => obj.AudienceType);

                    foreach (var item in list)
                    {
                        pushModel.PMSIncidentAction = (int)PMSIncidentAction.Dealt;
                        pushModel.Title = $"【{incidentInfo.CommName}】有新的报事处理信息";
                        pushModel.Audience.Objects.Clear();

                        if (incidentInfo.IncidentPlace == "户内")
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RoomName}】{item.PushContent}";
                        else
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RegionalPlace}】{item.PushContent}";

                        switch (item.AudienceType)
                        {
                            //分派岗位
                            case PMSIncidentPushAudienceType.AssignRole:
                                pushModel.Audience.Objects.AddRange(GetCanAssignUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            // 客户管家
                            case PMSIncidentPushAudienceType.RoomHousekeeper:
                                if (incidentInfo.IncidentSource != "客户报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetHousekeeperMobiles(incidentInfo.CommID, incidentInfo.RoomID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            // 公区管家
                            case PMSIncidentPushAudienceType.RegionHousekeeper:
                                if (incidentInfo.IncidentSource != "公区报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetRegionkeeperMobiles(incidentInfo.CommID, incidentInfo.RegionalID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            // 报事客户
                            case PMSIncidentPushAudienceType.Customer:
                                if (incidentInfo.IncidentSource != "客户报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetCustomerMobiles(incidentInfo.CustID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            // 报事员工
                            case PMSIncidentPushAudienceType.IncidentEmployee:
                                if (incidentInfo.IncidentSource != "自查报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetIncidentManMobiles(incidentInfo.IncidentID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            default: continue;
                        }

                        // 推送App消息
                        SynchPushNotification(item, pushModel);

                        // 发送手机短信
                        SynchSendShortMessage(item, pushModel);
                    }
                }
            });
        }

        /// <summary>
        /// 报事关闭退回后推送
        /// </summary>
        public static void SynchPushIncidentCloseReturn(long incidentId)
        {
            SynchPushIncidentAction(incidentId, (incidentInfo, pushModel, setting) =>
            {
                using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    var list = GetPushModel(PMSIncidentAction.CloseReturn, conn).OrderBy(obj => obj.AudienceType);

                    foreach (var item in list)
                    {
                        pushModel.PMSIncidentAction = (int)PMSIncidentAction.CloseReturn;
                        pushModel.Title = $"【{incidentInfo.CommName}】有新的报事退单信息";
                        pushModel.Audience.Objects.Clear();

                        if (incidentInfo.IncidentPlace == "户内")
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RoomName}】{item.PushContent}";
                        else
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RegionalPlace}】{item.PushContent}";

                        switch (item.AudienceType)
                        {
                            //分派岗位
                            case PMSIncidentPushAudienceType.AssignRole:
                                pushModel.Audience.Objects.AddRange(GetCanAssignUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            //责任员工
                            case PMSIncidentPushAudienceType.DealMan:
                                pushModel.Audience.Objects.AddRange(GetDealManMobiles(incidentInfo.IncidentID, conn));
                                pushModel.Command = PushCommand.INCIDENT_PROCESSING;
                                break;
                            //协助员工
                            case PMSIncidentPushAudienceType.AssistantMan:
                                pushModel.Audience.Objects.AddRange(GetAssistantManMobiles(incidentInfo.IncidentID, conn));
                                pushModel.Command = PushCommand.INCIDENT_PROCESSING;
                                break;
                            // 客户管家
                            case PMSIncidentPushAudienceType.RoomHousekeeper:
                                if (incidentInfo.IncidentSource != "客户报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetHousekeeperMobiles(incidentInfo.CommID, incidentInfo.RoomID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            // 公区管家
                            case PMSIncidentPushAudienceType.RegionHousekeeper:
                                if (incidentInfo.IncidentSource != "公区报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetRegionkeeperMobiles(incidentInfo.CommID, incidentInfo.RegionalID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            // 报事客户
                            case PMSIncidentPushAudienceType.Customer:
                                if (incidentInfo.IncidentSource != "客户报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetCustomerMobiles(incidentInfo.CustID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            // 报事员工
                            case PMSIncidentPushAudienceType.IncidentEmployee:
                                if (incidentInfo.IncidentSource != "自查报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetIncidentManMobiles(incidentInfo.IncidentID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            default: continue;
                        }

                        // 推送App消息
                        SynchPushNotification(item, pushModel);

                        // 发送手机短信
                        SynchSendShortMessage(item, pushModel);
                    }
                }
            });
        }

        /// <summary>
        /// 报事回访退回后推送
        /// </summary>
        public static void SynchPushIncidentReplyReturn(long incidentId)
        {
            SynchPushIncidentAction(incidentId, (incidentInfo, pushModel, setting) =>
            {
                using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    var list = GetPushModel(PMSIncidentAction.ReplyReturn, conn).OrderBy(obj => obj.AudienceType);

                    foreach (var item in list)
                    {
                        pushModel.PMSIncidentAction = (int)PMSIncidentAction.ReplyReturn;
                        pushModel.Title = $"【{incidentInfo.CommName}】有新的报事退单信息";
                        pushModel.Audience.Objects.Clear();

                        if (incidentInfo.IncidentPlace == "户内")
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RoomName}】{item.PushContent}";
                        else
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RegionalPlace}】{item.PushContent}";

                        switch (item.AudienceType)
                        {
                            //分派岗位
                            case PMSIncidentPushAudienceType.AssignRole:
                                pushModel.Audience.Objects.AddRange(GetCanAssignUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.INCIDENT_ASSIGN;
                                break;
                            //责任员工
                            case PMSIncidentPushAudienceType.DealMan:
                                pushModel.Audience.Objects.AddRange(GetCanSnatchUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.INCIDENT_PROCESSING;
                                break;
                            //协助员工
                            case PMSIncidentPushAudienceType.AssistantMan:
                                pushModel.Audience.Objects.AddRange(GetCanSnatchUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.INCIDENT_PROCESSING;
                                break;
                            //关闭员工
                            case PMSIncidentPushAudienceType.CloseEmployee:
                                pushModel.Audience.Objects.AddRange(GetCanSnatchUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.INCIDENT_PROCESSING;
                                break;
                            // 客户管家
                            case PMSIncidentPushAudienceType.RoomHousekeeper:
                                if (incidentInfo.IncidentSource != "客户报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetHousekeeperMobiles(incidentInfo.CommID, incidentInfo.RoomID, conn));
                                pushModel.Command = PushCommand.INCIDENT_PROCESSING;
                                break;
                            // 公区管家
                            case PMSIncidentPushAudienceType.RegionHousekeeper:
                                if (incidentInfo.IncidentSource != "公区报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetRegionkeeperMobiles(incidentInfo.CommID, incidentInfo.RegionalID, conn));
                                pushModel.Command = PushCommand.INCIDENT_PROCESSING;
                                break;
                            // 报事客户
                            case PMSIncidentPushAudienceType.Customer:
                                if (incidentInfo.IncidentSource != "客户报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetCustomerMobiles(incidentInfo.CustID, conn));
                                pushModel.Command = PushCommand.INCIDENT_PROCESSING;
                                break;
                            // 报事员工
                            case PMSIncidentPushAudienceType.IncidentEmployee:
                                if (incidentInfo.IncidentSource != "自查报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetIncidentManMobiles(incidentInfo.IncidentID, conn));
                                pushModel.Command = PushCommand.INCIDENT_PROCESSING;
                                break;
                            default: continue;
                        }

                        // 推送App消息
                        SynchPushNotification(item, pushModel);

                        // 发送手机短信
                        SynchSendShortMessage(item, pushModel);
                    }
                }
            });
        }

        /// <summary>
        /// 报事分派催办后推送
        /// </summary>
        public static void SynchPushIncidentAssignUrged(long incidentId)
        {
            SynchPushIncidentAction(incidentId, (incidentInfo, pushModel, setting) =>
            {
                using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    var list = GetPushModel(PMSIncidentAction.AssignUrged, conn).OrderBy(obj => obj.AudienceType);

                    foreach (var item in list)
                    {
                        pushModel.PMSIncidentAction = (int)PMSIncidentAction.AssignUrged;
                        pushModel.Title = $"【{incidentInfo.CommName}】有新的报事催办信息";
                        pushModel.Audience.Objects.Clear();

                        if (incidentInfo.IncidentPlace == "户内")
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RoomName}】{item.PushContent}";
                        else
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RegionalPlace}】{item.PushContent}";

                        switch (item.AudienceType)
                        {
                            // 分派岗位，通知派单
                            case PMSIncidentPushAudienceType.AssignRole:
                                pushModel.Audience.Objects.AddRange(GetCanAssignUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.INCIDENT_ASSIGN;
                                break;

                            // 处理岗位，通知抢单
                            case PMSIncidentPushAudienceType.ProcessRole:
                                pushModel.Audience.Objects.AddRange(GetCanSnatchUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.INCIDENT_SNATCH;
                                break;
                            // 楼栋管家或房屋管家，通知派单
                            case PMSIncidentPushAudienceType.RoomHousekeeper:
                                if (incidentInfo.IncidentPlace != "客户报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetHousekeeperMobiles(incidentInfo.CommID, incidentInfo.RoomID, conn));
                                pushModel.Command = PushCommand.INCIDENT_ASSIGN;
                                break;

                            // 公区管家，通知派单
                            case PMSIncidentPushAudienceType.RegionHousekeeper:
                                if (incidentInfo.IncidentPlace != "公区报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetRegionkeeperMobiles(incidentInfo.CommID, incidentInfo.RegionalID, conn));
                                pushModel.Command = PushCommand.INCIDENT_ASSIGN;
                                break;
                            default: continue;
                        }

                        // 推送App消息
                        SynchPushNotification(item, pushModel);

                        // 发送手机短信
                        SynchSendShortMessage(item, pushModel);
                    }
                }
            });
        }

        /// <summary>
        /// 报事处理催办后推送
        /// </summary>
        public static void SynchPushIncidentDealUrged(long incidentId)
        {
            SynchPushIncidentAction(incidentId, (incidentInfo, pushModel, setting) =>
            {
                using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    var list = GetPushModel(PMSIncidentAction.DealUrged, conn).OrderBy(obj => obj.AudienceType);

                    foreach (var item in list)
                    {
                        pushModel.PMSIncidentAction = (int)PMSIncidentAction.DealUrged;
                        pushModel.Title = $"【{incidentInfo.CommName}】有新的报事催办信息";
                        pushModel.Audience.Objects.Clear();

                        if (incidentInfo.IncidentPlace == "户内")
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RoomName}】{item.PushContent}";
                        else
                            pushModel.Message = $"【{incidentInfo.IncidentNum}】【{incidentInfo.RegionalPlace}】{item.PushContent}";

                        switch (item.AudienceType)
                        {
                            // 分派岗位，通知派单
                            case PMSIncidentPushAudienceType.AssignRole:
                                pushModel.Audience.Objects.AddRange(GetCanAssignUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;

                            // 处理岗位，通知抢单
                            case PMSIncidentPushAudienceType.ProcessRole:
                                if (incidentInfo.IncidentPlace != "口派报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetCanSnatchUserMobiles(incidentInfo.CommID, incidentInfo.BigCorpTypeID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;

                            //责任员工
                            case PMSIncidentPushAudienceType.DealMan:
                                if (incidentInfo.IncidentPlace != "书面报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetDealManMobiles(incidentInfo.IncidentID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;

                            // 协助人
                            case PMSIncidentPushAudienceType.AssistantMan:
                                if (incidentInfo.IncidentPlace != "书面报事")
                                    continue;
                                pushModel.Audience.Objects.AddRange(GetAssistantManMobiles(incidentInfo.IncidentID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;

                            // 公区管家，通知派单
                            case PMSIncidentPushAudienceType.RegionHousekeeper:
                                if (incidentInfo.IncidentPlace != "公区报事")
                                    continue;

                                pushModel.Audience.Objects.AddRange(GetRegionkeeperMobiles(incidentInfo.CommID, incidentInfo.RegionalID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;

                            // 客户管家
                            case PMSIncidentPushAudienceType.RoomHousekeeper:
                                if (incidentInfo.IncidentSource != "客户报事")
                                    continue;

                                pushModel.Audience.Objects.AddRange(GetIncidentManMobiles(incidentInfo.IncidentID, conn));
                                pushModel.Command = PushCommand.NORMAL;
                                break;
                            default: continue;
                        }

                        // 推送App消息
                        SynchPushNotification(item, pushModel);

                        // 发送手机短信
                        SynchSendShortMessage(item, pushModel);
                    }
                }
            });
        }

        #endregion

        /// <summary>
        /// 发起报事协助申请后推送
        /// </summary>
        public static void SynchPushIncidentAssistApplied(string iid)
        {
            var tmp = default(PMSIncidentAcceptModel);

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT * FROM Tb_HSPR_IncidentAccept WHERE IncidentID=
                                (SELECT IncidentID FROM Tb_HSPR_IncidentAssistApply WHERE IID=@IID)";

                tmp = conn.Query<PMSIncidentAcceptModel>(sql, new { IID = iid }).FirstOrDefault();
            }

            SynchPushIncident(tmp, (incidentInfo, pushModel, setting) =>
            {
                pushModel.PMSIncidentAction = (int)PMSIncidentAction.DealUrged;
                using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    // 申请信息
                    var sql = @"SELECT AuditUser,AuditState,b.UserName AS AuditUserName 
                                FROM Tb_HSPR_IncidentAssistApply a 
                                LEFT JOIN Tb_Sys_User b ON a.AuditUser=b.UserCode 
                                WHERE a.IID=@IID";
                    var applyInfo = conn.Query(sql, new { IID = iid }).FirstOrDefault();
                    if (applyInfo == null)
                        return;

                    // 审核人信息
                    sql = @"SELECT MobileTels FROM view_Sys_RoleData_Filter 
                            WHERE CommID=@CommID AND RoleCode IN
                            (
                                SELECT RoleCode FROM Tb_Sys_Role WHERE SysRoleCode IS NULL AND RoleCode IN
	                            (
		                            SELECT TOP 1 RoleCode FROM Tb_HSPR_IncidentAuditingWorkFlow WHERE KeyIID=@IID AND AuditType=1 ORDER BY orderID
	                            )
	                            UNION ALL
	                            SELECT RoleCode FROM Tb_Sys_Role WHERE SysRoleCode IN
	                            (
		                            SELECT TOP 1 RoleCode FROM Tb_HSPR_IncidentAuditingWorkFlow WHERE KeyIID=@IID AND AuditType=1 ORDER BY orderID
	                            )
                            )";

                    var mobiles = conn.Query<string>(sql, new { CommID = incidentInfo.CommID, IID = iid }).ToList();
                    if (mobiles.Count() == 0)
                        return;

                    mobiles = UserMobilesHandle(mobiles);

                    var item = new Tb_HSPR_IncidentPushSetting()
                    {
                        PushContent = $"【报事协助审核】有新的报事协助申请需要您审核，请及时处理，申请人：{incidentInfo.DealMan}；"
                    };

                    // 推送App消息
                    SynchPushNotification(item, pushModel);

                    // 发送手机短信
                    SynchSendShortMessage(item, pushModel);
                }
            });
        }

        /// <summary>
        /// 报事协助审核后推送
        /// </summary>
        public static void SynchPushIncidentAssistAudited(long incidentId)
        {

        }

        /// <summary>
        /// 发起报事延期申请后推送
        /// </summary>
        public static void SynchPushIncidentDelayApplied(string iid)
        {

        }

        /// <summary>
        /// 报事延期审核后推送
        /// </summary>
        public static void SynchPushIncidentDelayAudited(long incidentId)
        {

        }

        /// <summary>
        /// 发起报事非正常关闭申请后推送
        /// </summary>
        public static void SynchPushIncidentUnnormalCloseApplied(string iid)
        {

        }

        /// <summary>
        /// 报事非正常关闭审核后推送
        /// </summary>
        public static void SynchPushIncidentUnnormalCloseAudited(long incidentId)
        {

        }

        /// <summary>
        /// 发起报事免费申请后推送
        /// </summary>
        public static void SynchPushIncidentFreeApplied(string iid)
        {

        }

        /// <summary>
        /// 报事免费审核后推送
        /// </summary>
        public static void SynchPushIncidentFreeAudited(long incidentId)
        {

        }

        /// <summary>
        /// 推送App信息
        /// </summary>
        private static void SynchPushNotification(Tb_HSPR_IncidentPushSetting incidentPushSetting, PushModel pushModel)
        {
            if (incidentPushSetting == null || pushModel == null)
                return;

            if (incidentPushSetting.PushNotification && pushModel.Audience.Objects.Count > 0)
            {
                pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];

                TWTools.Push.Push.SendAsync(pushModel);
            }
        }

        /// <summary>
        /// 推送手机短信
        /// </summary>
        private static void SynchSendShortMessage(Tb_HSPR_IncidentPushSetting incidentPushSetting, PushModel pushModel)
        {
            if (incidentPushSetting == null || pushModel == null)
                return;

            if (!incidentPushSetting.SendShortMessage || pushModel.Audience.Objects.Count <= 0)
            {
                return;
            }
            #region 以下代码暂时仅用于隆泰阿里云短信推送，后续做兼容
            if (AppGlobal.StrToInt(Global_Fun.AppWebSettings("SMS_RUN")) != 1)
            {
                return;
            }
            switch (pushModel.PMSIncidentAction)
            {
                case (int)PMSIncidentAction.Assigned:
                    if (AppGlobal.StrToInt(Global_Fun.AppWebSettings("SMS_ASSIGN")) == 1)
                    {
                        AliyunSMS.sendBatchSms(AliyunSMS.TEMPLATE_INCIDENT_ASSIGN, pushModel.Audience.Objects, pushModel.PMSIncidentShortMessage);
                    }
                    break;
                case (int)PMSIncidentAction.Transpond:
                    if (AppGlobal.StrToInt(Global_Fun.AppWebSettings("SMS_TRANSMIT")) == 1)
                    {
                        AliyunSMS.sendBatchSms(AliyunSMS.TEMPLATE_INCIDENT_TRANSMIT, pushModel.Audience.Objects, pushModel.PMSIncidentShortMessage);
                    }
                    break;
                case (int)PMSIncidentAction.AssistApply:
                    if (AppGlobal.StrToInt(Global_Fun.AppWebSettings("SMS_AUDIT")) == 1)
                    {
                        AliyunSMS.sendBatchSms(AliyunSMS.TEMPLATE_INCIDENT_AUDIT, pushModel.Audience.Objects, pushModel.PMSIncidentShortMessage);
                    }
                    break;
                case (int)PMSIncidentAction.DelayApply:
                    if (AppGlobal.StrToInt(Global_Fun.AppWebSettings("SMS_AUDIT")) == 1)
                    {
                        AliyunSMS.sendBatchSms(AliyunSMS.TEMPLATE_INCIDENT_AUDIT, pushModel.Audience.Objects, pushModel.PMSIncidentShortMessage);
                    }
                    break;
                case (int)PMSIncidentAction.UnnormalCloseApply:
                    if (AppGlobal.StrToInt(Global_Fun.AppWebSettings("SMS_AUDIT")) == 1)
                    {
                        AliyunSMS.sendBatchSms(AliyunSMS.TEMPLATE_INCIDENT_AUDIT, pushModel.Audience.Objects, pushModel.PMSIncidentShortMessage);
                    }
                    break;
                default: return;
            }

            #endregion
        }
    }


}
