using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Business.PMS10.物管App.报事.Enum;
using Business.PMS10.物管App.报事.Models;
using Dapper;
using MobileSoft.DBUtility;

namespace Business
{
    public partial class PMSIncidentPush
    {
        /// <summary>
        /// 获取报事生命周期动作推送模型
        /// </summary>
        private static List<Tb_HSPR_IncidentPushSetting> GetPushModel(PMSIncidentAction action, IDbConnection db = null)
        {
            var list = default(List<Tb_HSPR_IncidentPushSetting>);

            switch (action)
            {
                case PMSIncidentAction.Accepted: 
                    list = GetIncidentActionPushModel("Tb_HSPR_IncidentPushSettingAccep", db); break;

                case PMSIncidentAction.AssignUrged:
                case PMSIncidentAction.Assigned:
                    list = GetIncidentActionPushModel("Tb_HSPR_IncidentPushSettingWorkOrder", db); break;

                case PMSIncidentAction.VerbalToWritten:
                case PMSIncidentAction.DealUrged:
                case PMSIncidentAction.Transpond:
                case PMSIncidentAction.Received:
                case PMSIncidentAction.Arrived:
                case PMSIncidentAction.Dealt:
                    list = GetIncidentActionPushModel("Tb_HSPR_IncidentPushSettingHandle", db); break;

                case PMSIncidentAction.CloseReturn:
                    list = GetIncidentActionPushModel("Tb_HSPR_IncidentPushSettingClose", db); break; 

                case PMSIncidentAction.ReplyReturn:
                    list = GetIncidentActionPushModel("Tb_HSPR_IncidentPushSettingReply", db); break;

                case PMSIncidentAction.Deleted:
                    list = GetIncidentActionPushModel("", db); break;

                case PMSIncidentAction.AssistApply:
                    list = GetIncidentAuditPushModel("Tb_HSPR_IncidentPushSettingAudit", "协助申请提交完毕", db); break;
                case PMSIncidentAction.AssistApplyResult:
                    list = GetIncidentAuditPushModel("Tb_HSPR_IncidentPushSettingAudit", "协助申请审核完毕", db); break;
                case PMSIncidentAction.DelayApply:
                    list = GetIncidentAuditPushModel("Tb_HSPR_IncidentPushSettingAudit", "延期申请提交完毕", db); break;
                case PMSIncidentAction.DelayApplyResult:
                    list = GetIncidentAuditPushModel("Tb_HSPR_IncidentPushSettingAudit", "延期申请审核完毕", db); break;
                case PMSIncidentAction.UnnormalCloseApply:
                    list = GetIncidentAuditPushModel("Tb_HSPR_IncidentPushSettingAudit", "非正常关闭提交完毕", db); break;
                case PMSIncidentAction.UnnormalCloseApplyResult:
                    list = GetIncidentAuditPushModel("Tb_HSPR_IncidentPushSettingAudit", "非正常关闭审核完毕", db); break;
                case PMSIncidentAction.FreeApply:
                    list = GetIncidentAuditPushModel("Tb_HSPR_IncidentPushSettingAudit", "免费申请提交完毕", db); break;
                case PMSIncidentAction.FreeApplyResult:
                    list = GetIncidentAuditPushModel("Tb_HSPR_IncidentPushSettingAudit", "免费申请审核完毕", db); break;

                default:
                    break;
            }

            return list;
        }

        /// <summary>
        /// 获取报事生命周期动作推送模型
        /// </summary>
        private static List<Tb_HSPR_IncidentPushSetting> GetIncidentActionPushModel(string tableName, IDbConnection db = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);
            {
                var sql = $@"SELECT PushObject,PushContent,App AS PushNotification,Phone AS SendShortMessage 
                             FROM {tableName} WHERE CheckPushObject=1;";

                var data = conn.Query<Tb_HSPR_IncidentPushSetting>(sql).ToList();

                if (db == null)
                {
                    conn.Dispose();
                }

                return data;
            }
        }

        /// <summary>
        /// 获取报事审核推送模型
        /// </summary>
        private static List<Tb_HSPR_IncidentPushSetting> GetIncidentAuditPushModel(string tableName, string business, IDbConnection db = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);
            {
                var sql = $@"SELECT PushObject,PushContent,App AS PushNotification,Phone AS SendShortMessage 
                             FROM {tableName} WHERE Business='{business}' AND CheckPushObject=1;";

                var data = conn.Query<Tb_HSPR_IncidentPushSetting>(sql).ToList();

                if (db == null)
                {
                    conn.Dispose();
                }

                return data;
            }
        }

        public static bool GetAppPushSetting(int corpId, out Tb_System_CorpAppPushSet setting)
        {
            using (var conn = new SqlConnection(PubConstant.tw2bsConnectionString))
            {
                var sql = "SELECT * FROM Tb_System_CorpAppPushSet WHERE CorpID=@CorpID;";

                setting = conn.Query<Tb_System_CorpAppPushSet>(sql, new { CorpID = corpId }).FirstOrDefault();

                if (setting != null)
                    return true;

                return false;
            }
        }
    }
}
