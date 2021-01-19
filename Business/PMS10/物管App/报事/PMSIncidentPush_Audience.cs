using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MobileSoft.DBUtility;

namespace Business
{
    public partial class PMSIncidentPush
    {
        /// <summary>
        /// 获取楼栋管家手机号
        /// </summary>
        private static List<string> GetHousekeeperMobiles(int commId, long roomId, IDbConnection db = null)
        {
            if (roomId == 0)
                return new List<string>();

            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);
            {
                // 管家设置到房屋
                var sql = @"SELECT UserCode FROM Tb_HSPR_Room WHERE RoomID=@RoomID
                            UNION ALL
                            SELECT UserCode FROM Tb_HSPR_RoomHousekeeper WHERE RoomID=@RoomID";

                var users = conn.Query<string>(sql, new { RoomID = roomId });
                if (users.Count() == 0)
                {
                    // 管家设置到楼宇
                    sql = @"SELECT UserCode FROM Tb_HSPR_BuildHousekeeper 
                            WHERE CommID=@CommID AND BuildSNum=(SELECT BuildSNum FROM Tb_HSPR_Room WHERE RoomID=@RoomID)";

                    users = conn.Query<string>(sql, new { CommID = commId, RoomID = roomId });
                }

                if (users.Count() == 0)
                    return new List<string>();

                users = users.Select(obj => $"'{ obj }'");

                sql = $@"SELECT ltrim(rtrim(MobileTel)) FROM Tb_Sys_User 
                         WHERE isnull(MobileTel,'')<>'' AND UserCode IN({ string.Join(",", users) })";

                var mobiles = conn.Query<string>(sql);

                if (db == null)
                    conn.Dispose();

                return UserMobilesHandle(mobiles);
            }
        }

        /// <summary>
        /// 获取公区管家手机号
        /// </summary>
        private static List<string> GetRegionkeeperMobiles(int commId, long regionalId, IDbConnection db = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);
            {
                var sql = @"SELECT ltrim(rtrim(MobileTel)) FROM Tb_Sys_User 
                            WHERE isnull(MobileTel,'')<>'' AND UserCode IN
                            (
                                SELECT UserCode FROM Tb_HSPR_IncidentRegional WHERE CommID=@CommID AND RegionalID=@RegionalID
                                UNION ALL
                                SELECT UserCode FROM Tb_HSPR_RegionHousekeeper WHERE CommID=@CommID AND RegionalID=@RegionalID
                            )";

                var mobiles = conn.Query<string>(sql, new { CommID = commId, RegionalID = regionalId });

                if (db == null)
                    conn.Dispose();

                return UserMobilesHandle(mobiles);
            }
        }

        /// <summary>
        /// 获取具有派单权限的人的手机号
        /// </summary>
        private static List<string> GetCanAssignUserMobiles(int commId, long typeId, IDbConnection db = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);
            {
                var condition = "";

                var sql = "SELECT DepCode FROM Tb_Sys_Department WHERE DepType='项目级' AND isnull(IsDelete,0)=0";
                if (conn.Query(sql).Count() > 0)
                {
                    // 禅道BUG-22325，经沟通暂时先取消这个判断，2020年8月13日16:12:31
                    //condition = " AND DepCode IN (SELECT DepCode FROM Tb_Sys_Department WHERE DepType='项目级') ";
                }

                sql = $@"SELECT ltrim(rtrim(MobileTel)) FROM Tb_Sys_User 
                         WHERE UserCode IN
                         (
                            SELECT UserCode FROM Tb_Sys_UserRole WHERE RoleCode IN
                            (
                                SELECT RoleCode FROM Tb_Sys_Role WHERE RoleCode IN
                                (
                                    SELECT RoleCode FROM Tb_Sys_RoleData WHERE CommID=@CommID
                                ) 
	                            { condition }
                                AND SysRoleCode IN(SELECT RoleCode FROM Tb_HSPR_IncidentTypeAssignedPost WHERE CorpTypeID=@CorpTypeID
                            )
                         ))";

                var mobiles = conn.Query<string>(sql, new { CommID = commId, CorpTypeID = typeId });

                if (db == null)
                    conn.Dispose();

                return UserMobilesHandle(mobiles);
            }
        }

        /// <summary>
        /// 获取具有抢单权限的人的手机号
        /// </summary>
        private static List<string> GetCanSnatchUserMobiles(int commId, long typeId, IDbConnection db = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);
            {
                var condition = "";

                var sql = "SELECT DepCode FROM Tb_Sys_Department WHERE DepType='项目级' AND isnull(IsDelete,0)=0";
                if (conn.Query(sql).Count() > 0)
                {
                    // 禅道BUG-22325，经沟通暂时先取消这个判断，2020年8月13日16:12:31
                    //condition = " AND DepCode IN (SELECT DepCode FROM Tb_Sys_Department WHERE DepType='项目级') ";
                }

                sql = $@"SELECT ltrim(rtrim(MobileTel)) FROM Tb_Sys_User 
                         WHERE UserCode IN
                         (
                            SELECT UserCode FROM Tb_Sys_UserRole WHERE RoleCode IN
                            (
                                SELECT RoleCode FROM Tb_Sys_Role WHERE RoleCode IN
                                (
                                    SELECT RoleCode FROM Tb_Sys_RoleData WHERE CommID=@CommID
                                ) 
	                            { condition }
                                AND SysRoleCode IN(SELECT RoleCode FROM Tb_HSPR_IncidentTypeProcessPost WHERE CorpTypeID=@CorpTypeID
                            )
                         ))";

                var mobiles = conn.Query<string>(sql, new { CommID = commId, CorpTypeID = typeId });

                if (db == null)
                    conn.Dispose();

                return UserMobilesHandle(mobiles);
            }
        }

        /// <summary>
        /// 获取具有关闭权限的人的手机号
        /// </summary>
        private static List<string> GetCanCloseUserMobiles(int commId, long typeId, IDbConnection db = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);
            {
                var condition = "";

                var sql = "SELECT DepCode FROM Tb_Sys_Department WHERE DepType='项目级' AND isnull(IsDelete,0)=0";
                if (conn.Query(sql).Count() > 0)
                {
                    // 禅道BUG-22325，经沟通暂时先取消这个判断，2020年8月13日16:12:31
                    // condition = " AND DepCode IN (SELECT DepCode FROM Tb_Sys_Department WHERE DepType='项目级') ";
                }

                sql = $@"SELECT ltrim(rtrim(MobileTel)) FROM Tb_Sys_User 
                         WHERE UserCode IN
                         (
                            SELECT UserCode FROM Tb_Sys_UserRole WHERE RoleCode IN
                            (
                                SELECT RoleCode FROM Tb_Sys_Role WHERE RoleCode IN
                                (
                                    SELECT RoleCode FROM Tb_Sys_RoleData WHERE CommID=@CommID
                                ) 
	                            { condition }
                                AND SysRoleCode IN(SELECT RoleCode FROM Tb_HSPR_IncidentTypeClosePost WHERE CorpTypeID=@CorpTypeID
                            )
                         ))";

                var mobiles = conn.Query<string>(sql, new { CommID = commId, CorpTypeID = typeId });

                if (db == null)
                    conn.Dispose();

                return UserMobilesHandle(mobiles);
            }
        }

        /// <summary>
        /// 获取报事客户手机号
        /// </summary>
        private static List<string> GetCustomerMobiles(long custId, IDbConnection db = null)
        {
            if (custId == 0)
                return new List<string>();

            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);
            {
                var sql = @"SELECT ltrim(rtrim(isnull(MobilePhone,isnull(LinkmanTel,'')))) 
                            FROM Tb_HSPR_Customer WHERE CustID=@CustID";

                var mobiles = conn.Query<string>(sql, new { CustID = custId });

                if (db == null)
                    conn.Dispose();

                return UserMobilesHandle(mobiles);
            }
        }

        /// <summary>
        /// 获取受理人手机号
        /// </summary>
        private static List<string> GetIncidentManMobiles(long incidentId, IDbConnection db = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);
            {
                var sql = @"SELECT ltrim(rtrim(MobileTel)) FROM Tb_Sys_User 
                            WHERE UserCode=(SELECT AdmiManUserCode FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID)";

                var mobiles = conn.Query<string>(sql, new { IncidentID = incidentId });

                if (db == null)
                    conn.Dispose();

                return UserMobilesHandle(mobiles);
            }
        }

        /// <summary>
        /// 获取处理人手机号
        /// </summary>
        private static List<string> GetDealManMobiles(long incidentId, IDbConnection db = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);
            {
                var sql = @"SELECT ltrim(rtrim(MobileTel)) FROM Tb_Sys_User 
                            WHERE UserCode=(SELECT DealManCode FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID)";

                var mobiles = conn.Query<string>(sql, new { IncidentID = incidentId });

                if (db == null)
                    conn.Dispose();

                return UserMobilesHandle(mobiles);
            }
        }

        /// <summary>
        /// 获取协助人手机号
        /// </summary>
        private static List<string> GetAssistantManMobiles(long incidentId, IDbConnection db = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);
            {
                var sql = @"SELECT ltrim(rtrim(MobileTel)) FROM Tb_Sys_User 
                            WHERE UserCode IN 
                            (
                                SELECT UserCode FROM Tb_HSPR_IncidentAssistDetail x
                                WHERE AssistID IN
                                (
                                    SELECT IID FROM Tb_HSPR_IncidentAssistApply x WHERE x.IncidentID=@IncidentID AND x.AuditState='审核通过'
                                )
                            )";

                var mobiles = conn.Query<string>(sql, new { IncidentID = incidentId });

                if (db == null)
                    conn.Dispose();

                return UserMobilesHandle(mobiles);
            }
        }

        /// <summary>
        /// 获取报事审核人手机号
        /// </summary>
        private static List<string> GetWorkflowApplyUserMobiles(string appleId, IDbConnection db = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);
            {
                var sql = @"SELECT ltrim(rtrim(MobileTel)) FROM Tb_Sys_User 
                            WHERE UserCode IN 
                            (
                                SELECT UserCode FROM Tb_HSPR_IncidentAssistDetail x
                                WHERE AssistID IN
                                (
                                    SELECT IID FROM Tb_HSPR_IncidentAssistApply x WHERE x.IncidentID=@IncidentID AND x.AuditState='审核通过'
                                )
                            )";

                var mobiles = conn.Query<string>(sql, new { IncidentID = appleId });

                if (db == null)
                    conn.Dispose();

                return UserMobilesHandle(mobiles);
            }
        }

        /// <summary>
        /// 用户手机号码处理
        /// </summary>
        private static List<string> UserMobilesHandle(IEnumerable<string> mobiles)
        {
            var objs = new List<string>();

            if (mobiles.Count() > 0)
            {
                foreach (dynamic item in mobiles)
                {
                    if (item != null && !string.IsNullOrEmpty(item))
                    {
                        objs.AddRange(item.Split(SplitChars, StringSplitOptions.RemoveEmptyEntries));
                    }
                }

                objs = objs.Distinct().ToList();
            }

            return objs;
        }
    }
}
