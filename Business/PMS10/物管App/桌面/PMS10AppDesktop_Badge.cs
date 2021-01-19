using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Dapper.SqlMapper;

namespace Business
{
    /// <summary>
    /// PMS10模块角标
    /// </summary>
    public partial class PMS10AppDesktop
    {
        /// <summary>
        /// 获取App桌面角标
        /// </summary>
        private string GetAppDesktopBadge(DataRow row)
        {
            if (!row.Table.Columns.Contains("Codes") || string.IsNullOrEmpty(row["Codes"].ToString()))
            {
                return JSONHelper.FromString(false, "Codes不能为空");
            }

            var codes = row["Codes"].ToString();

            var commId = 0;
            var organCode = "";

            _usercode = Global_Var.LoginUserCode;

            if (row.Table.Columns.Contains("CommID") && !string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                commId = AppGlobal.StrToInt(row["CommID"].ToString());
                _commId = commId;
            }
            if (row.Table.Columns.Contains("OrganCode") && !string.IsNullOrEmpty(row["OrganCode"].ToString()))
            {
                organCode = row["OrganCode"].ToString();
                if (organCode.Length > 2)
                    organCode = organCode.Substring(0, organCode.Length - 2);
                _organCode = organCode;
            }
            using (var conn0 = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var codeArr = codes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var result = new Dictionary<string, int>();

                var usercode = Global_Var.LoginUserCode;
                
                StringBuilder stringBuilder = new StringBuilder("");
                stringBuilder.AppendLine($"获取code：{string.Join(",", codeArr)}");
                DateTime DateNow = DateTime.Now;
                stringBuilder.AppendLine($"Start:{DateNow:yyyy-MM-dd HH:mm:ss:fff}");
                foreach (var code in codeArr)
                {
                    DateNow = DateTime.Now;
                    stringBuilder.AppendLine($"角标：{code}：Start:{DateNow:yyyy-MM-dd HH:mm:ss:fff}");
                    if (Global_Var.LoginCorpID == "2096" && HttpContext.Current.Request.Url.Host.Contains("183.230.204.117")) // 相关置地，暂时禁用角标
                    {
                        result.Add(code, 0);
                        continue;
                    }
                    switch (code)
                    {
                        case "990101":  // 办公审批
                            result.Add(code, 办公审批角标(usercode, organCode, commId, conn0));
                            break;
                        case "990102":  // 业务审批
                            //result.Add(code, 业务审批角标(usercode, organCode, commId, conn0));
                            break;
                        case "990103":  // 信息公告
                            result.Add(code, 信息公告角标(usercode, organCode, commId, conn0));
                            break;
                        case "990104":  // 请假申请
                            result.Add(code, 请假申请角标(usercode, organCode, commId, conn0));
                            break;
                        case "990105":  // 领料申请
                            result.Add(code, 领料申请角标(usercode, organCode, commId, conn0));
                            break;
                        case "990202":  // 拜访管理
                            result.Add(code, 拜访管理角标(usercode, commId, conn0));
                            break;
                        case "990203":  // 装修审核
                            result.Add(code, 装修审核角标(usercode, organCode, commId, conn0));
                            break;
                        case "990303":  // 工单池
                            result.Add(code, 工单池角标(usercode, commId, conn0));
                            break;
                        case "990304":  // 报事处理
                            result.Add(code, 报事处理角标(usercode, commId, conn0));
                            break;
                        case "990305":  // 报事关闭
                            result.Add(code, 报事关闭角标(usercode, commId, conn0));
                            break;
                        case "990306":  // 报事审核
                            result.Add(code, 报事审核角标(usercode, organCode, commId, conn0));
                            break;
                        case "990308":  // 报事预警
                            result.Add(code, 报事预警角标(usercode, conn0));
                            break;
                        case "990401":  // 设备巡检
                            result.Add(code, 设备巡检角标(usercode, commId));
                            break;
                        case "990402":  // 设备维保
                            result.Add(code, 设备维保角标(usercode, commId));
                            break;
                        case "990501":  // 安全巡更
                            result.Add(code, 安全巡更角标(usercode, commId));
                            break;
                        case "990502":  // 消防巡检
                            result.Add(code, 消防巡检角标(usercode, commId));
                            break;
                        case "990601":  // 环境保洁
                            result.Add(code, 环境保洁角标(usercode, commId));
                            break;
                        case "990602":  // 绿化养护
                            result.Add(code, 绿化养护角标(usercode, commId));
                            break;
                        case "990603":  // 四害消杀
                            result.Add(code, 四害消杀角标(usercode, commId));
                            break;
                        case "990604":  // 垃圾清运
                            result.Add(code, 垃圾清运角标(usercode, commId));
                            break;
                        case "990701":  // 公区巡查
                            result.Add(code, 公区巡查角标(usercode, commId));
                            break;
                        case "990702":  // 空房巡查
                            result.Add(code, 空房巡查角标(usercode, commId));
                            break;
                        case "990703":  // 装修巡查
                            result.Add(code, 装修巡查角标(usercode, commId));
                            break;
                        case "990801":  // 验房计划
                            result.Add(code, 验房计划角标(usercode, commId));
                            break;
                        case "990802":  // 验房整改
                            result.Add(code, 验房整改角标(usercode, commId));
                            break;
                        case "990901":  // 品质核查
                            result.Add(code, 品质核查角标(usercode, commId));
                            break;
                        case "990903":  // 品质督查
                            result.Add(code, 品质督查角标(usercode, commId));
                            break;
                        case "991001":  // 交班信息
                            result.Add(code, 交班信息角标(usercode, commId));
                            break;
                        case "991002":  // 值班信息
                            result.Add(code, 值班信息角标(usercode, commId));
                            break;
                    }
                    stringBuilder.AppendLine($"角标:{code}:{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff},Speed:{(DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000 - (DateNow.ToUniversalTime().Ticks - 621355968000000000) / 10000000}ms");
                }
                stringBuilder.AppendLine($"End:{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}");
                Log(stringBuilder.ToString(), "AppDesktop_Logs\\");

                return new ApiResult(true, result).toJson();
            }
        }

        #region App模块角标

        private int GetBadge(string code, string usercode, string organCode = null, int commId = 0, IDbConnection conn = null, IDbTransaction trans = null)
        {
            switch (code)
            {
                case "990101":
                    return 办公审批角标(usercode, organCode, commId, conn, trans);
                case "990102":
                    return 业务审批角标(usercode, organCode, commId, conn, trans);
                case "990103":
                    return 信息公告角标(usercode, organCode, commId, conn, trans);
                case "990104":
                    return 请假申请角标(usercode, organCode, commId, conn, trans);
                case "990105":
                    return 领料申请角标(usercode, organCode, commId, conn, trans);
                case "990107":
                    return 工作日志角标(usercode, organCode, commId, conn, trans);
                case "990202":
                    return 拜访管理角标(usercode, commId, conn, trans);
                case "990203":
                    return 装修审核角标(usercode, organCode, commId, conn, trans);
                case "990303":
                    return 工单池角标(usercode, commId, conn, trans);
                case "990304":
                    return 报事处理角标(usercode, commId, conn, trans);
                case "990305":
                    return 报事关闭角标(usercode, commId, conn, trans);
                case "990306":
                    return 报事审核角标(usercode, organCode, commId, conn, trans);
                case "990308":
                    return 报事预警角标(usercode, conn, trans);
                case "990401":
                    return 设备巡检角标(usercode, commId, conn, trans);
                case "990402":
                    return 设备维保角标(usercode, commId, conn, trans);
                case "990501":
                    return 安全巡更角标(usercode, commId, conn, trans);
                case "990502":
                    return 消防巡检角标(usercode, commId, conn, trans);
                case "990601":
                    return 环境保洁角标(usercode, commId, conn, trans);
                case "990602":
                    return 绿化养护角标(usercode, commId, conn, trans);
                case "990603":
                    return 四害消杀角标(usercode, commId, conn, trans);
                case "990604":
                    return 垃圾清运角标(usercode, commId, conn, trans);
                case "990701":
                    return 公区巡查角标(usercode, commId, conn, trans);
                case "990702":
                    return 空房巡查角标(usercode, commId, conn, trans);
                case "990703":
                    return 装修巡查角标(usercode, commId, conn, trans);
                case "990801":
                    return 验房计划角标(usercode, commId, conn, trans);
                case "990802":
                    return 验房整改角标(usercode, commId, conn, trans);
                case "990901":
                    return 品质核查角标(usercode, commId, conn, trans);
                case "991001":
                    return 交班信息角标(usercode, commId, conn, trans);
                case "991002":
                    return 值班信息角标(usercode, commId, conn, trans);
                default:
                    return 0;
            }
        }

        private int 办公审批角标(string usercode, string organCode = null, int commId = 0, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CP));



            if (db == null)
                conn.Dispose();

            return 0;
        }

        private int 业务审批角标(string usercode, string organCode = null, int commId = 0, IDbConnection db = null, IDbTransaction trans = null)
        {
            var host = AppGlobal.GetAppSetting("ERPHost");
            if (host == null || host.Length == 0)
            {
                host = HttpContext.Current.Request.Url.Host.Split(':')[0];
            }

            if (host.ToLower().StartsWith("http") == false)
            {
                host = $"{ HttpContext.Current.Request.Url.Scheme }://{ host }"; 
            }

            try
            {
                return GetBusinessApprovalBadge(host, Global_Var.LoginCode, AppGlobal.StrToInt(Global_Var.CorpId), commId, db, trans);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private int 信息公告角标(string usercode, string organCode = null, int commId = 0, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CP));



            if (db == null)
                conn.Dispose();

            return 0;
        }

        private int 请假申请角标(string usercode, string organCode = null, int commId = 0, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CP));



            if (db == null)
                conn.Dispose();

            return 0;
        }

        private int 领料申请角标(string usercode, string organCode = null, int commId = 0, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CP));


            if (db == null)
                conn.Dispose();

            return 0;
        }

        private int 工作日志角标(string usercode, string organCode = null, int commId = 0, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CP));



            if (db == null)
                conn.Dispose();

            return 0;
        }

        private int 拜访管理角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            // 任务列表
            var sql = @"SELECT ID FROM Tb_Visit_Plan a
                        WHERE ID IN
                        (
                            SELECT ID FROM Tb_Visit_Plan a
                            WHERE a.CommID=@CommID AND isnull(UserCode,'')<>'' AND UserCode LIKE @_UserCode
                            AND isnull(IsDelete,0)=0 AND isnull(IsClose,0)=0 
                            AND convert(VARCHAR(10),a.PlanBeginTime,20)<=convert(VARCHAR(10),getdate(),20) 
                            AND convert(VARCHAR(10),a.PlanEndTime,20)>=convert(VARCHAR(10),getdate(),20)  

                            UNION ALL

                            SELECT ID FROM Tb_Visit_Plan a
                            WHERE a.CommID=@CommID AND isnull(a.UserCode,'')=''
                            AND isnull(IsDelete,0)=0 AND isnull(IsClose,0)=0 
                            AND convert(VARCHAR(10),a.PlanBeginTime,20)<=convert(VARCHAR(10),getdate(),20)  
                            AND convert(VARCHAR(10),a.PlanEndTime,20)>=convert(VARCHAR(10),getdate(),20)  
                            AND (SELECT count(1) FROM
                                (
                                    SELECT value AS RoleCode FROM SplitString(a.RoleCode,',',1)
                                    INTERSECT
                                    (
                                        SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode AND isnull(RoleCode,'')=''
                                        UNION ALL
                                        SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                                        WHERE RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode) 
                                        AND isnull(SysRoleCode,'')<>''
                                    )
                                ) AS t)>0
                        )
                        AND 
                        (
                            SELECT count(1) FROM Tb_Visit_VisitingExecuteUser 
                            WHERE PlanID=a.ID AND UserCode=@UserCode AND IsComplete=1
                        )=0;";

            try
            {
                var badge = conn.Query<string>(sql, new
                {
                    CommID = commId,
                    UserCode = Global_Var.LoginUserCode,
                    _UserCode = $"%{ Global_Var.LoginUserCode }%"
                }).Count();

                return badge;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        private int 装修审核角标(string usercode, string organCode = null, int commId = 0, IDbConnection db = null, IDbTransaction trans = null)
        {
            var host = AppGlobal.GetAppSetting("ERPHost");
            if (host == null || host.Length == 0)
            {
                host = HttpContext.Current.Request.Url.Host.Split(':')[0];
            }

            if (host.ToLower().StartsWith("http") == false)
            {
                host = $"{ HttpContext.Current.Request.Url.Scheme }://{ host }";
            }


            try
            {
                return GetRenovationAuditBadge(host, organCode, AppGlobal.StrToInt(Global_Var.CorpId), commId, db, trans);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private int 工单池角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (commId == 0)
                return 0;

            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var sql = "";
            var condition = $"{commId}";

            var control = PMSIncidentAccept.GetIncidentControlSet(conn);
            if (control.AllowCrossRegionWorkOrder)
            {
                // 允许区域工单
                sql = @"SELECT CommID FROM Tb_Sys_RoleData 
                        WHERE CommID<>0 AND RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode)";
                var comms = conn.Query<int>(sql, new { UserCode = usercode }).ToList();
                comms.Insert(0, commId);

                condition = $"{string.Join(",", comms)}";
            }

            sql = $@"/*分派岗位，处理岗位*/
                    SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                    WHERE DrClass=1 AND isnull(IsDelete,0)=0 AND isnull(DispType,0)=0 AND CommID IN({condition})
                    AND BigCorpTypeID IN
                    (
                        SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeAssignedPost 
                        WHERE RoleCode IN
                        (
                            /* 直接是通用岗位 */
                            SELECT RoleCode FROM Tb_Sys_Role
                            WHERE RoleCode IN
                            (
                                SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ usercode }'
                            ) AND SysRoleCode IS NULL

                            UNION ALL
                            /* 从具体岗位读通用岗位 */
                            SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                            WHERE RoleCode IN
                            (
                                SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ usercode }'
                            ) AND SysRoleCode IS NOT NULL
                        )
                        UNION ALL
                        SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeProcessPost 
                        WHERE RoleCode IN
                        (
                            /* 直接是通用岗位 */
                            SELECT RoleCode FROM Tb_Sys_Role
                            WHERE RoleCode IN
                            (
                                SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ usercode }'
                            ) AND SysRoleCode IS NULL

                            UNION ALL
                            /* 从具体岗位读通用岗位 */
                            SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                            WHERE RoleCode IN
                            (
                                SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ usercode }'
                            ) AND SysRoleCode IS NOT NULL
                        )
                    )

                    UNION ALL
                    /*管家，设置到楼栋*/
                    SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                    WHERE DrClass=1 AND isnull(IsDelete,0)=0 AND isnull(DispType,0)=0 AND CommID IN({condition})
                    AND (IncidentPlace='户内' OR (IncidentPlace='公区' AND RoomID<>0))
                    AND RoomID IN
                    (
                        SELECT RoomID FROM Tb_HSPR_Room y 
                        WHERE y.CommID=x.CommID 
                        AND BuildSNum IN
                        (
                            SELECT BuildSNum FROM Tb_HSPR_BuildHousekeeper WHERE CommID=y.CommID AND UserCode='{usercode}'
                        )
                    )

                    UNION ALL
                    /*管家，设置到房屋*/
                    SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                    WHERE DrClass=1 AND isnull(IsDelete,0)=0 AND isnull(DispType,0)=0 AND CommID IN({condition})
                    AND (IncidentPlace='户内' OR (IncidentPlace='公区' AND RoomID<>0))
                    AND RoomID IN
                    (
                        SELECT RoomID FROM Tb_HSPR_Room WHERE CommID IN({condition}) AND UserCode='{usercode}'
                        UNION ALL
                        SELECT RoomID FROM Tb_HSPR_RoomHousekeeper WHERE RoomID=x.RoomID AND UserCode='{usercode}'
                    )

                    UNION ALL
                    /*公区管家*/
                    SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                    WHERE DrClass=1 AND isnull(IsDelete,0)=0 AND isnull(DispType,0)=0 AND CommID IN({condition})
                    AND IncidentPlace='公区'
                    AND RegionalID IN
                    (
                        SELECT RegionalID FROM Tb_HSPR_IncidentRegional
                        WHERE CommID IN({condition}) AND UserCode='{usercode}'
                        UNION ALL
                        SELECT RegionalID FROM Tb_HSPR_RegionHousekeeper
                        WHERE CommID IN({condition}) AND UserCode='{usercode}'
                    )

                    UNION ALL
                    SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                    WHERE DrClass=1 AND isnull(IsDelete,0)=0 AND isnull(DispType,0)=0 AND CommID IN({condition}) AND isnull(BigCorpTypeID,0)=0;";

            var badge = conn.Query<long>(sql, null, trans).Distinct().Count();

            if (db == null)
                conn.Dispose();

            return badge;
        }

        private int 报事处理角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (commId == 0)
                return 0;

            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var sql = "";
            var condition = $"{commId}";

            var control = PMSIncidentAccept.GetIncidentControlSet(conn);
            if (control.AllowCrossRegionWorkOrder)
            {
                // 允许区域工单
                sql = @"SELECT CommID FROM Tb_Sys_RoleData 
                        WHERE CommID<>0 AND RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode)";
                var comms = conn.Query<int>(sql, new { UserCode = usercode }).ToList();
                comms.Insert(0, commId);

                condition = $"{string.Join(",", comms)}";
            }

            sql = $@"/* 责任人 */
                    SELECT IncidentID FROM Tb_HSPR_IncidentAccept
                    WHERE isnull(IsDelete,0)=0 AND isnull(DispType,0)=1 AND isnull(DealState,0)=0 AND CommID IN({condition})
                    AND DealManCode='{usercode}'

                    UNION ALL
                    /* 协助人 */
                    SELECT IncidentID FROM Tb_HSPR_IncidentAccept 
                    WHERE isnull(IsDelete,0)=0 AND isnull(DispType,0)=1 AND isnull(DealState,0)=0 AND CommID IN({condition})
                    AND IncidentID IN 
                    (
                        SELECT IncidentID FROM Tb_HSPR_IncidentAssistApply 
                        WHERE AuditState='已审核'
                        AND IID IN
                        (
                            SELECT AssistID FROM Tb_HSPR_IncidentAssistDetail WHERE UserCode='{usercode}'
                        )
                    )";

            var badge = conn.Query<long>(sql, null, trans).Distinct().Count();

            if (db == null)
                conn.Dispose();

            return badge;
        }

        private int 报事关闭角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (commId == 0)
                return 0;

            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var sql = "";
            var condition = $"{commId}";

            var control = PMSIncidentAccept.GetIncidentControlSet(conn);
            if (control.AllowCrossRegionWorkOrder)
            {
                // 允许区域工单
                sql = @"SELECT CommID FROM Tb_Sys_RoleData 
                        WHERE CommID<>0 AND RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode)";
                var comms = conn.Query<int>(sql, new { UserCode = usercode }).ToList();
                comms.Insert(0, commId);

                condition = $"{string.Join(",", comms)}";
            }

            sql = $@"SELECT count(1) FROM Tb_HSPR_IncidentAccept a 
                    WHERE isnull(IsDelete,0)=0 AND isnull(DealState,0)=1 AND isnull(IsClose,0)=0 AND CommID IN({condition})
                    AND (
                        BigCorpTypeID IN
                        (
                            SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeClosePost
                            WHERE RoleCode IN
                            (
                                /* 直接是通用岗位 */
                                SELECT RoleCode FROM Tb_Sys_Role
                                WHERE RoleCode IN
                                (
                                    SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ usercode }'
                                ) AND SysRoleCode IS NULL

                                UNION ALL
                                /* 从具体岗位读通用岗位 */
                                SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                                WHERE RoleCode IN
                                (
                                    SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ usercode }'
                                ) AND SysRoleCode IS NOT NULL
                            )
                        )
                        OR
                        IncidentID IN
                        (
                            /*管家，设置到楼栋*/
                            SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                            WHERE DrClass=1 AND isnull(IsDelete,0)=0 AND isnull(DealState,0)=1 AND isnull(IsClose,0)=0 AND CommID IN({condition})
                            AND (IncidentPlace='户内' OR (IncidentPlace='公区' AND RoomID<>0))
                            AND RoomID IN
                            (
                                SELECT RoomID FROM Tb_HSPR_Room y 
                                WHERE y.CommID=x.CommID 
                                AND BuildSNum IN
                                (
                                    SELECT BuildSNum FROM Tb_HSPR_BuildHousekeeper
                                        WHERE CommID=y.CommID AND UserCode='{ usercode }'
                                )
                            )

                            UNION ALL
                            /*管家，设置到房屋*/
                            SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                            WHERE DrClass=1 AND isnull(IsDelete,0)=0 AND isnull(DealState,0)=1 AND isnull(IsClose,0)=0 AND CommID IN({condition})
                            AND (IncidentPlace='户内' OR (IncidentPlace='公区' AND RoomID<>0))
                            AND RoomID IN
                            (
                                SELECT RoomID FROM Tb_HSPR_Room WHERE CommID IN({condition}) AND UserCode='{ usercode }'
                                UNION ALL
                                SELECT RoomID FROM Tb_HSPR_RoomHousekeeper WHERE RoomID=x.RoomID AND UserCode='{ usercode }'
                            )

                            UNION ALL
                            /*公区管家*/
                            SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                            WHERE DrClass=1 AND isnull(IsDelete,0)=0 AND isnull(DealState,0)=1 AND isnull(IsClose,0)=0 AND CommID IN({condition})
                            AND IncidentPlace='公区'
                            AND RegionalID IN
                            (
                                SELECT RegionalID FROM Tb_HSPR_IncidentRegional
                                WHERE CommID IN({condition}) AND UserCode='{ usercode }'
                                UNION ALL
                                SELECT RegionalID FROM Tb_HSPR_RegionHousekeeper
                                WHERE CommID IN({condition}) AND UserCode='{ usercode }'
                            )
                        )
                    )";

            var badge = conn.Query<int>(sql, null, trans).FirstOrDefault();

            if (db == null)
                conn.Dispose();

            return badge;
        }

        public int 报事审核角标(string usercode, string organCode = null, int commId = 0, IDbConnection db = null, IDbTransaction trans = null)
        {
            var host = AppGlobal.GetAppSetting("ERPHost");
            if (host == null || host.Length == 0)
            {
                host = HttpContext.Current.Request.Url.Host.Split(':')[0];
            }

            if (host.ToLower().StartsWith("http") == false)
            {
                host = $"{ HttpContext.Current.Request.Url.Scheme }://{ host }";
            }

            try
            {
                return GetIncidentAuditBadge(host, Global_Var.LoginCode, AppGlobal.StrToInt(Global_Var.CorpId), commId, db, trans);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private int 报事预警角标(string usercode, IDbConnection db = null, IDbTransaction trans = null)
        {
            return 报事预警角标(usercode, out int _1, out int _2, out int _3, out int _4, out int _5, db, trans);
        }

        private int 报事预警角标(string usercode, out int _1, out int _2, out int _3, out int _4, out int _5, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            // 1：已受理未分派，分派超时
            // 2：已分派未接单，接单超时
            // 3：已接单未处理，处理超时
            // 4：已处理未关闭，关闭超时
            // 5：已关闭未回访，回访超时
            var sql = @"SELECT Count(1) AS Count1 FROM Tb_HSPR_IncidentWarningPush WHERE PushUsers LIKE @PushUsers AND IncidentStep=1;
                        SELECT Count(1) AS Count2 FROM Tb_HSPR_IncidentWarningPush WHERE PushUsers LIKE @PushUsers AND IncidentStep=2;
                        SELECT Count(1) AS Count3 FROM Tb_HSPR_IncidentWarningPush WHERE PushUsers LIKE @PushUsers AND IncidentStep=3;
                        SELECT Count(1) AS Count4 FROM Tb_HSPR_IncidentWarningPush WHERE PushUsers LIKE @PushUsers AND IncidentStep=4;
                        SELECT Count(1) AS Count4 FROM Tb_HSPR_IncidentWarningPush WHERE PushUsers LIKE @PushUsers AND IncidentStep=5";

            GridReader gridReader = conn.QueryMultiple(sql, new { PushUsers = $"%{Global_Var.LoginUserCode}%" }, trans);

            _1 = gridReader.Read<int>().First();
            _2 = gridReader.Read<int>().First();
            _3 = gridReader.Read<int>().First();
            _4 = gridReader.Read<int>().First();
            _5 = gridReader.Read<int>().First();

            if (db == null)
                conn.Dispose();

            return (_1 + _2 + _3 + _4 + _5);
        }

        private int 设备巡检角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            return 设备角标(usercode, commId, 1, db, trans);
        }

        private int 设备维保角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            return 设备角标(usercode, commId, 2, db, trans);
        }

        private int 设备角标(string usercode, int commId, int type, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (commId == 0)
                return 0;

            var taskType = "Inspection";

            if (type == 2)
                taskType = "Maintenance";

            var conn = db ?? new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_EQ));

            var sql = $@"SELECT count(1) FROM Tb_Eq_Task_{ taskType } a
                        WHERE a.TaskBeginTime<=getdate() AND a.TaskEndTime>=getdate() AND a.CommID=@CommID
                        AND isnull(a.IsDelete,0)=0 AND isnull(a.IsClose,0)=0 AND isnull(a.IsOverDue,0)=0 AND a.TaskStatue<>2 
                        AND
                        (
                            charindex(@UserCode,a.TaskUserCode)>0
                            OR
                            (isnull(a.TaskUserCode,'')='' AND
                            (SELECT count(t.RoleCode) AS RoleCode FROM (
                                SELECT Value AS RoleCode FROM {Global_Var.ERPDatabaseName}.dbo.SplitString(a.TaskRoleCode,',',1)
                                InterSect
                                SELECT RoleCode FROM {Global_Var.ERPDatabaseName}.dbo.Tb_Sys_UserRole WHERE UserCode=@UserCode
                            ) AS T)>0)
                        )";

            if (type == 1 || type == 3)
            {
                sql += $" AND a.IsFireControl={ (type == 3 ? "1" : "0")};";
            }

            try
            {
                var badge = conn.Query<int>(sql, new
                {
                    CommID = commId,
                    UserCode = Global_Var.LoginUserCode
                }, trans).FirstOrDefault();

                return badge;
            }
#pragma warning disable CS0168 // 声明了变量“ex”，但从未使用过
            catch (Exception ex)
#pragma warning restore CS0168 // 声明了变量“ex”，但从未使用过
            {
                return 0;
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        private int 安全巡更角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (commId == 0)
                return 0;

            var conn = db ?? new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_SAFE));

            var sql = $@"SELECT count(1) FROM Tb_CP_Task_Safe WHERE TaskId IN
                         (
                            SELECT TaskId FROM Tb_CP_Task_Safe a 
                            WHERE isnull(a.IsDelete,0)=0 AND isnull(a.IsClose,0)=0 AND PlanState<>2 AND a.CommID=@CommID 
                                AND a.TaskTypeName='安全巡更' AND charindex(@UserCode,a.TaskUserCode)>0
                                AND a.BeginTime<=getdate() AND a.EndTime>=getdate() 
                            UNION
                            SELECT TaskId FROM Tb_CP_Task_Safe a 
                            WHERE isnull(a.IsDelete,0)=0 AND isnull(a.IsClose,0)=0 AND PlanState<>2 AND a.CommID=@CommID 
                                AND a.TaskTypeName='安全巡更' AND isnull(TaskUserCode,'')=''
                                AND a.BeginTime<=getdate() AND a.EndTime>=getdate() 
                                AND (SELECT count(t.RoleCode) AS RoleCode FROM (
                                        SELECT Value AS RoleCode FROM {Global_Var.ERPDatabaseName}.dbo.SplitString(a.TaskRoleCode,',',1)
                                        InterSect
                                        SELECT RoleCode FROM {Global_Var.ERPDatabaseName}.dbo.Tb_Sys_UserRole WHERE UserCode=@UserCode
                                    ) AS T)>0
                         )";

            try
            {
                var badge = conn.Query<int>(sql, new
                {
                    CommID = commId,
                    UserCode = Global_Var.LoginUserCode
                }, trans).FirstOrDefault();

                return badge;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        private int 消防巡检角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            return 设备角标(usercode, commId, 3, db, trans);
        }

        private int 环境保洁角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            return 环境角标(usercode, commId, "环境保洁", db, trans);
        }

        private int 绿化养护角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            return 环境角标(usercode, commId, "绿化养护", db, trans);
        }

        private int 四害消杀角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            return 环境角标(usercode, commId, "四害消杀", db, trans);
        }

        private int 垃圾清运角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            return 环境角标(usercode, commId, "垃圾清运", db, trans);
        }

        private int 环境角标(string usercode, int commId, string taskTypeName, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (commId == 0)
                return 0;

            var conn = db ?? new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_AMBIENT));

            var sql = $@"SELECT TaskId FROM Tb_CP_Task_Ambient a 
                            WHERE isnull(a.IsDelete,0)=0 AND isnull(a.IsClose,0)=0 AND PlanState<>2 AND a.CommID=@CommID 
                                AND a.TaskTypeName=@TaskTypeName AND charindex(@UserCode,a.TaskUserCode)>0
                                AND a.BeginTime<=getdate() AND a.EndTime>=getdate() 
                            UNION
                            SELECT TaskId FROM Tb_CP_Task_Ambient a 
                            WHERE isnull(a.IsDelete,0)=0 AND isnull(a.IsClose,0)=0 AND PlanState<>2 AND a.CommID=@CommID 
                                AND a.TaskTypeName=@TaskTypeName AND isnull(TaskUserCode,'')=''
                                AND a.BeginTime<=getdate() AND a.EndTime>=getdate() 
                                AND (SELECT count(t.RoleCode) AS RoleCode FROM (
                                        SELECT Value AS RoleCode FROM {Global_Var.ERPDatabaseName}.dbo.SplitString(a.TaskRoleCode,',',1)
                                        InterSect
                                        SELECT RoleCode FROM {Global_Var.ERPDatabaseName}.dbo.Tb_Sys_UserRole WHERE UserCode=@UserCode
                                    ) AS T)>0";

            try
            {
                var badge = conn.Query<string>(sql, new
                {
                    CommID = commId,
                    UserCode = Global_Var.LoginUserCode,
                    TaskTypeName = taskTypeName
                }, trans).Count();

                return badge;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        private int 公区巡查角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            return 巡查角标(usercode, commId, "公区巡查", db, trans);
        }

        private int 空房巡查角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            return 巡查角标(usercode, commId, "空房巡查", db, trans);
        }

        private int 装修巡查角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            return 巡查角标(usercode, commId, "装修巡查", db, trans);
        }

        private int 巡查角标(string usercode, int commId, string taskTypeName, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (commId == 0)
                return 0;

            var conn = db ?? new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_PATROL));

            var sql = $@"SELECT TaskId FROM Tb_CP_Task_Patrol a 
                            WHERE isnull(a.IsDelete,0)=0 AND isnull(a.IsClose,0)=0 AND PlanState<>2 AND a.CommID=@CommID 
                                AND a.TaskTypeName=@TaskTypeName AND charindex(@UserCode,a.TaskUserCode)>0
                                AND a.BeginTime<=getdate() AND a.EndTime>=getdate() 
                        UNION
                        SELECT TaskId FROM Tb_CP_Task_Patrol a 
                        WHERE isnull(a.IsDelete,0)=0 AND isnull(a.IsClose,0)=0 AND PlanState<>2 AND a.CommID=@CommID 
                            AND a.TaskTypeName=@TaskTypeName AND isnull(TaskUserCode,'')=''
                            AND a.BeginTime<=getdate() AND a.EndTime>=getdate() 
                            AND (SELECT count(t.RoleCode) AS RoleCode FROM (
                                    SELECT Value AS RoleCode FROM {Global_Var.ERPDatabaseName}.dbo.SplitString(a.TaskRoleCode,',',1)
                                    InterSect
                                    SELECT RoleCode FROM {Global_Var.ERPDatabaseName}.dbo.Tb_Sys_UserRole WHERE UserCode=@UserCode
                                ) AS T)>0";

            try
            {
                var badge = conn.Query<string>(sql, new
                {
                    CommID = commId,
                    UserCode = Global_Var.LoginUserCode,
                    TaskTypeName = taskTypeName
                }, trans).Count();

                return badge;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        private int 验房计划角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (commId == 0)
                return 0;

            var conn = db ?? new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CP));



            if (db == null)
                conn.Dispose();

            return 0;
        }

        private int 验房整改角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (commId == 0)
                return 0;

            var conn = db ?? new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CP));



            if (db == null)
                conn.Dispose();

            return 0;
        }

        private int 品质核查角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (commId == 0)
                return 0;

            var conn = db ?? new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CP));

            var sql = $@"SELECT a.TaskId FROM Tb_CP_Task a 
                            LEFT JOIN {Global_Var.ERPDatabaseName}.dbo.Tb_CP_TaskLevel b ON a.TaskLevelId=b.LevelId
                            WHERE a.CommID=@CommID AND isnull(a.IsDelete,0)=0 AND isnull(a.IsClose,0)=0 AND PlanState<>2   
                            AND a.TaskSource='临时任务' AND a.TaskTypeName='品质核查' AND a.AddUserCode=@UserCode
                        UNION 
                        SELECT a.TaskId FROM Tb_CP_Task a 
                            LEFT JOIN {Global_Var.ERPDatabaseName}.dbo.Tb_CP_TaskLevel b ON a.TaskLevelId=b.LevelId
                            WHERE a.BeginTime<=getdate() AND a.EndTime>=getdate() AND a.CommID=@CommID  
                            AND isnull(a.IsDelete,0)=0 AND isnull(a.IsClose,0)=0 AND PlanState<>2  
                            AND a.TaskTypeName='品质核查' AND a.TaskSource<>'临时任务' 
                        AND 
                        (
                            charindex(@UserCode,a.TaskUserCode)>0
                            OR
                            (isnull(a.TaskUserCode,'')='' AND
                            (SELECT count(t.RoleCode) AS RoleCode FROM (
                                SELECT Value AS RoleCode FROM {Global_Var.ERPDatabaseName}.dbo.SplitString(a.TaskRoleCode,',',1)
                                InterSect
                                SELECT RoleCode FROM {Global_Var.ERPDatabaseName}.dbo.Tb_Sys_UserRole WHERE UserCode=@UserCode
                            ) AS T)>0)
                        )";

            try
            {
                var badge = conn.Query<string>(sql, new
                {
                    CommID = commId,
                    UserCode = Global_Var.LoginUserCode
                }, trans).Count();

                return badge;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        private int 品质督查角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (commId == 0)
                return 0;

            var conn = db ?? new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_SUPERVISION));

            var sql = @"SELECT count(1) FROM Tb_Supervision_Task 
                        WHERE CommID=@CommID AND IsDelete=0 AND EndTime>=GETDATE() AND BeginTime<=GETDATE()
                        AND (TaskState='未开始' OR TaskState='进行中')
                        AND CHARINDEX(@UserCode,InspectPersonID)>0;";

            try
            {
                var badge = conn.Query<int>(sql, new
                {
                    CommID = commId,
                    UserCode = Global_Var.LoginUserCode
                }, trans).FirstOrDefault();

                return badge;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        private int 交班信息角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (commId == 0)
                return 0;

            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var sql = @"SELECT ID FROM Tb_Duty_Record 
                        WHERE CommID=@CommID AND isnull(IsDelete,0)=0 AND isnull(IsClose,0)=0 AND EndTime IS NOT NULL 
                        AND IsHandover=1 AND isnull(IsTakeover,0)=0 
                        AND 
                        (((
                            SELECT count(1) FROM
                            (
                                SELECT Value AS RoleCode FROM SplitString(RoleCode,',',1)
                                INTERSECT
                                (
                                    SELECT RoleCode FROM Tb_Sys_Role 
                                    WHERE RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode) AND SysRoleCode IS NOT NULL
                                    UNION ALL
                                    SELECT RoleCode FROM Tb_Sys_Role 
                                    WHERE SysRoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode)
                                )
                            ) AS T
                        )>0 AND isnull(UpChoiceUserCode,'')='')
                        OR UpChoiceUserCode=@UserCode)";

            try
            {
                var badge = conn.Query<string>(sql, new
                {
                    CommID = commId,
                    UserCode = Global_Var.LoginUserCode
                }, trans).Count();

                return badge;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        private int 值班信息角标(string usercode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (commId == 0)
                return 0;

            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var sql = $@"SELECT ID FROM Tb_Duty_Record  
                        WHERE CommID=@CommID AND isnull(IsClose,0)=0 AND isnull(IsDelete,0)=0 AND EndTime IS NULL 
                        AND DutyUserCode LIKE '%{ Global_Var.LoginUserCode }%'";

            try
            {
                var badge = conn.Query<string>(sql, new
                {
                    CommID = commId,
                    UserCode = Global_Var.LoginUserCode
                }, trans).Count();

                return badge;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        #endregion

        private static string[] BusinessApprovalCommands = new string[]
        {
            "CorpID={0}&CommID={1}&LoginCode={2}&rows=99&page=1&Method=DataPost&EditBeginDate={3}&EditEndDate={4}&Class=AuditManage&Command=HousingStatusAuditList&IsOption=0",
            "CorpID={0}&CommID={1}&LoginCode={2}&rows=99&page=1&Method=DataPost&EditBeginDate={3}&EditEndDate={4}&Class=AuditManage&Command=AuditingFeeList&IsAuditing=0",
            "CorpID={0}&CommID={1}&LoginCode={2}&rows=99&page=1&Method=DataPost&EditBeginDate={3}&EditEndDate={4}&Class=AuditManage&Command=AuditingInputFeesList&IsAuditing=0",
            "CorpID={0}&CommID={1}&LoginCode={2}&rows=99&page=1&Method=DataPost&EditBeginDate={3}&EditEndDate={4}&Class=AuditManage&Command=AuditingInputPreCostsList&IsAuditing=0",
            "CorpID={0}&CommID={1}&LoginCode={2}&rows=99&page=1&Method=DataPost&EditBeginDate={3}&EditEndDate={4}&Class=AuditManage&Command=AuditingPreList&IsAuditing=0",

            "CorpID={0}&CommID={1}&LoginCode={2}&rows=99&page=1&Method=DataPost&EditBeginDate={3}&EditEndDate={4}&Class=AuditManage&Command=WaiversFeeAuditList&IsAudit=0&SelAudit=0",
            "CorpID={0}&CommID={1}&LoginCode={2}&rows=99&page=1&Method=DataPost&EditBeginDate={3}&EditEndDate={4}&Class=AuditManage&Command=WaiversFeeList&IsAudit=",
            "CorpID={0}&CommID={1}&LoginCode={2}&rows=99&page=1&Method=DataPost&EditBeginDate={3}&EditEndDate={4}&Class=AuditManage&Command=RepealReceiptsList&IsAudit=&DrReceive=0&SelIsAudit=0",
            "CorpID={0}&CommID={1}&LoginCode={2}&rows=99&page=1&Method=DataPost&EditBeginDate={3}&EditEndDate={4}&Class=AuditManage&Command=RefundMultiAuditList&IsAudit=0&BusinessType=0",
            "CorpID={0}&CommID={1}&LoginCode={2}&rows=99&page=1&Method=DataPost&EditBeginDate={3}&EditEndDate={4}&Class=WriteOffAuditing&Command=GetList&IsAudit=0&ListType=WriteoffFeesReceiptsAudit&AuditUser=&AuditBeginDate=&AuditEndDate=",

            "CorpID={0}&CommID={1}&LoginCode={2}&rows=99&page=1&Method=DataPost&EditBeginDate={3}&EditEndDate={4}&Class=WriteOffAuditing&Command=GetList&IsAudit=0&ListType=WriteoffPreCostsOffsetAudit&AuditUser=&AuditBeginDate=&AuditEndDate=",
            "CorpID={0}&CommID={1}&LoginCode={2}&rows=99&page=1&Method=DataPost&EditBeginDate={3}&EditEndDate={4}&Class=WriteOffAuditing&Command=GetList&IsAudit=0&ListType=WriteoffWaiversFeeOffsetAudit&AuditUser=&AuditBeginDate=&AuditEndDate=",
            "CorpID={0}&CommID={1}&LoginCode={2}&rows=99&page=1&Method=DataPost&EditBeginDate={3}&EditEndDate={4}&Class=BankSurrTLAudit&Command=search&IsAudit=0"
        };

        // 报事审核
        private static string[] IncidentAuditCommands = new string[]
        {
            "CorpID={0}&CommID={1}&LoginCode={2}&rows=99&page=1&Method=DataPost&EditBeginDate={3}&EditEndDate={4}&Class=IncidentProcessingNew&Command=GetIncidentUnnormalCloseList&IsAudit=0&AuditState=审核中",
            "CorpID={0}&CommID={1}&LoginCode={2}&rows=99&page=1&Method=DataPost&EditBeginDate={3}&EditEndDate={4}&Class=IncidentProcessingNew&Command=GetIncidentDelayList&IsAudit=0&AuditState=审核中",
            "CorpID={0}&CommID={1}&LoginCode={2}&rows=99&page=1&Method=DataPost&EditBeginDate={3}&EditEndDate={4}&Class=IncidentProcessingNew&Command=GetIncidentAssistList&IsAudit=0&AuditState=审核中",
            "CorpID={0}&CommID={1}&LoginCode={2}&rows=99&page=1&Method=DataPost&EditBeginDate={3}&EditEndDate={4}&Class=IncidentProcessingNew&Command=GetIncidentFreeList&IsAudit=0&AuditState=审核中"
        };

        private static object lockObj = new object();

        /// <summary>
        /// 业务审批角标
        /// </summary>
        private static int GetBusinessApprovalBadge(string host, string logincode, int corpId, int commId = 0, IDbConnection db = null, IDbTransaction trans = null)
        {
            var corpPort = 80;
            using (var tw2bs = new SqlConnection(PubConstant.tw2bsConnectionString))
            {
                var sql = @"SELECT CorpPort FROM Tb_System_Corp WHERE CorpID=@CorpID AND isnull(IsDelete,0)=0";

                var port = tw2bs.Query<int>(sql, new { CorpID = corpId }).FirstOrDefault();
                if (port == 0)
                    port = 80;

                if (port == 443 && host.ToLower().StartsWith("http://"))
                {
                    host = host.ToLower().Replace("http://", "https://");
                }

                corpPort = port;
            }

            var dataUrl = $"{host}:{corpPort}/HM/M_Main/HC/Service.ashx";

            var d1 = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 00:00:00");
            var d2 = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
            var len = BusinessApprovalCommands.Length;

            var regex = new Regex("^{\"total\":([0-9]+)");
            var badge = 0;

            var parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = 10;

            var parallel = Parallel.For<int>(0, len, parallelOptions, () => 0, (index, state, _badge) =>
            {
                var url = $"{dataUrl}?{string.Format(BusinessApprovalCommands[index], corpId, commId, logincode, d1, d2) }";
                if (url.Contains("WaiversFeeAuditList") || url.Contains("WaiversFeeList"))
                {
                    url = url.Replace(" 00:00:00", "").Replace(" 23:59:59", "");
                }

                var json = HttpPost(url, "");
                var match = regex.Match(json);
                if (match.Success)
                {
                    if (int.TryParse(match.Groups[1].Value, out int count))
                    {
                        _badge += count;
                    }
                }
                return _badge;
            }, (x) =>
            {
                Interlocked.Add(ref badge, x);
            });

            return badge;
        }

        /// <summary>
        /// 报事审核角标
        /// </summary>
        /// <returns></returns>
        private static int GetIncidentAuditBadge(string host, string logincode, int corpId, int commId = 0, IDbConnection db = null, IDbTransaction trans = null)
        {
            var corpPort = 80;
            using (var tw2bs = new SqlConnection(PubConstant.tw2bsConnectionString))
            {
                var sql = @"SELECT CorpPort FROM Tb_System_Corp WHERE CorpID=@CorpID AND isnull(IsDelete,0)=0";

                var port = tw2bs.Query<int>(sql, new { CorpID = corpId }).FirstOrDefault();
                if (port == 0)
                    port = 80;

                if (port == 443 && host.ToLower().StartsWith("http://"))
                {
                    host = host.ToLower().Replace("http://", "https://");
                }

                corpPort = port;
            }

            var dataUrl = $"{host}:{corpPort}/HM/M_Main/HC/Service.ashx";

            var d1 = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 00:00:00");
            var d2 = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
            var len = IncidentAuditCommands.Length;

            var regex = new Regex("^{\"total\":([0-9]+)");
            var badge = 0;

            var parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = 10;

            var parallel = Parallel.For<int>(0, len, parallelOptions, () => 0, (index, state, _badge) =>
            {
                var url = $"{dataUrl}?{string.Format(IncidentAuditCommands[index], corpId, commId, logincode, d1, d2) }";

                var json = HttpPost(url, "");
                var match = regex.Match(json);
                if (match.Success)
                {
                    if (int.TryParse(match.Groups[1].Value, out int count))
                    {
                        _badge += count;
                    }
                }
                return _badge;
            }, (x) =>
            {
                Interlocked.Add(ref badge, x);
            });

            return badge;
        }

        /// <summary>
        /// 装修审核角标
        /// </summary>
        /// <returns></returns>
        private static int GetRenovationAuditBadge(string host, string logincode, int corpId, int commId = 0, IDbConnection db = null, IDbTransaction trans = null)
        {
            var corpPort = 80;
            using (var tw2bs = new SqlConnection(PubConstant.tw2bsConnectionString))
            {
                var sql = @"SELECT CorpPort FROM Tb_System_Corp WHERE CorpID=@CorpID AND isnull(IsDelete,0)=0";

                var port = tw2bs.Query<int>(sql, new { CorpID = corpId }).FirstOrDefault();
                if (port == 0)
                    port = 80;

                if (port == 443 && host.ToLower().StartsWith("http://"))
                {
                    host = host.ToLower().Replace("http://", "https://");
                }

                corpPort = port;
            }

            var dataUrl = $"{host}:{corpPort}/HM/M_Main/HC/Service.ashx";

            var d1 = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 00:00:00");
            var d2 = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");

            var url = $"{dataUrl}?{string.Format("CorpID={0}&CommID={1}&LoginCode={2}&rows=99&page=1&Method=DataPost&EditBeginDate={3}&EditEndDate={4}&Class=Renovation&Command=GetListRenoAuditAllHTML5", corpId, commId, logincode, d1, d2) }";

            var json = HttpPost(url, "");
            var jObj = (JObject)JsonConvert.DeserializeObject(json);
            if (jObj != null && jObj.TryGetValue("result", out JToken result))
            {
                if (result.ToString().ToLower() == "true")
                {
                    if (jObj.TryGetValue("data", out JToken data))
                    {
                        var _data = (JValue)data;
                        return JArray.Parse(_data.Value.ToString()).Count;
                    }
                }
            }
            return 0;
        }

    }
}
