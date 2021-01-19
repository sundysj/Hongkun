using Business.PMS10.物管App.员工;
using Common.Extenions;
using Dapper;
using DapperExtensions;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using static Dapper.SqlMapper;

namespace Business
{
    public partial class PMSIncidentAccept
    {
        /// <summary>
        /// 获取工单池列表
        /// </summary>
        private string GetIncidentPoolList(DataRow row)
        {
            var oldPMS = ConfigurationManager.AppSettings["OldPMS"];
            if (oldPMS != null && oldPMS == "1")
            {
                //return new IncidentAcceptManage_bl().GetIncidentPoolList(row);
            }

            #region 基础数据校验
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var pageSize = 10;
            var pageIndex = 1;

            /*
             * 1、默认按项目：本项目置顶，他项目依次，一个项目显示完后再显示下一项目；项目相同，按预约处理时间排序，最新的在上面；
             * 2、支持按时间：按预约处理时间排序，最新的在上面；
             * 3、支持按位置：先显示户内，后显示公区；位置相同，按预约处理时间
             */
            var type = 1;

            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("Type") && !string.IsNullOrEmpty(row["Type"].ToString()))
            {
                type = AppGlobal.StrToInt(row["Type"].ToString());
            }
            #endregion

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {

                var sql = "";
                var condition = $"{commId}";
                var orderField = "ReserveDate DESC,CommID";
                var isReserveAssigned = false;
                var sort = 1;

                if (type == 2)
                    orderField = "ReserveDate";// 按预约处理时间排序，最新的在上面；
                else if (type == 3)
                    orderField = "IncidentPlace DESC,ReserveDate";// 先显示户内，后显示公区；位置相同，按预约处理时间

                // 需求-9496
                if (Global_Var.CorpName.Contains("彰泰"))
                {
                    sql = @"SELECT isnull(col_length('Tb_HSPR_IncidentControlModelSet', 'IsReserve_Assigned'),0)";
                    var count = conn.Query<int>(sql).FirstOrDefault();

                    if (count > 0)
                    {
                        //
                        sql = @"SELECT count(1) AS Count FROM Tb_HSPR_IncidentControlModelSet WHERE isnull(IsReserve_Assigned,'否')='是' OR isnull(IsReserve_Assigned,'0')='1'";
                        isReserveAssigned = conn.Query<int>(sql).FirstOrDefault() > 0;
                        sort = 0;

                        if (type == 1)
                            orderField = "IncidentPlace DESC, CommID DESC, ReserveDate";
                        else if (type == 2)
                            orderField = "IncidentPlace DESC, ReserveDate";// 按预约处理时间排序；
                        else if (type == 3)
                            orderField = "IncidentPlace DESC, ReserveDate";// 先显示户内，后显示公区；位置相同，按预约处理时间
                    }
                }

                

                var control = GetIncidentControlSet(conn);
                if (control.AllowCrossRegionWorkOrder)
                {
                    // 允许区域工单
                    sql = @"SELECT CommID FROM Tb_Sys_RoleData 
                            WHERE CommID<>0 AND RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode)";
                    var comms = conn.Query<int>(sql, new { UserCode = Global_Var.LoginUserCode }).ToList();
                    comms.Insert(0, commId);

                    condition = $"{string.Join(",", comms)}";
                }

                sql = $@"SELECT CASE WHEN a.CommID={commId} THEN a.CommID*10 ELSE a.CommID END AS CommID,
                            f.CommName,IncidentID,IncidentNum,IncidentContent,a.IncidentPlace,IncidentDate,
                            ReserveDate,Phone,isnull(a.BigCorpTypeID,0) AS BigCorpTypeID,e.TypeName AS BigTypeName,
                            a.RoomID,c.BuildSNum,RoomSign,RoomName,a.RegionalID,d.RegionalPlace,
                            (
                                SELECT count(1) FROM Tb_HSPR_IncidentRemindersInfo x 
                                WHERE a.IncidentID=x.IncidentID AND isnull(x.IsDelete,0)=0
                            ) AS UrgeCount,
                            0 AS isRob,0 AS isDispatch 
                        FROM Tb_HSPR_IncidentAccept a 
                            LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID
                            LEFT JOIN Tb_HSPR_IncidentRegional d ON a.RegionalID=d.RegionalID
                            LEFT JOIN Tb_HSPR_CorpIncidentType e ON a.BigCorpTypeID=e.CorpTypeID
                            LEFT JOIN Tb_HSPR_Community f ON a.CommID=f.CommID
                        WHERE IncidentID IN
                        (
                            /*分派岗位，处理岗位*/
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
                                        SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ Global_Var.LoginUserCode }'
                                    ) AND SysRoleCode IS NULL

                                    UNION ALL
                                    /* 从具体岗位读通用岗位 */
                                    SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                                    WHERE RoleCode IN
                                    (
                                        SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ Global_Var.LoginUserCode }'
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
                                        SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ Global_Var.LoginUserCode }'
                                    ) AND SysRoleCode IS NULL

                                    UNION ALL
                                    /* 从具体岗位读通用岗位 */
                                    SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                                    WHERE RoleCode IN
                                    (
                                        SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ Global_Var.LoginUserCode }'
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
                                    SELECT BuildSNum FROM Tb_HSPR_BuildHousekeeper
                                        WHERE CommID=y.CommID AND UserCode='{Global_Var.LoginUserCode}'
                                )
                            )

                            UNION ALL
                            /*管家，设置到房屋*/
                            SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                            WHERE DrClass=1 AND isnull(IsDelete,0)=0 AND isnull(DispType,0)=0 AND CommID IN({condition})
                            AND (IncidentPlace='户内' OR (IncidentPlace='公区' AND RoomID<>0))
                            AND RoomID IN
                            (
                                SELECT RoomID FROM Tb_HSPR_Room WHERE CommID IN({condition}) AND UserCode='{Global_Var.LoginUserCode}'
                                UNION ALL
                                SELECT RoomID FROM Tb_HSPR_RoomHousekeeper WHERE RoomID=x.RoomID AND UserCode='{Global_Var.LoginUserCode}'
                            )

                            UNION ALL
                            /*公区管家*/
                            SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                            WHERE DrClass=1 AND isnull(IsDelete,0)=0 AND isnull(DispType,0)=0 AND CommID IN({condition})
                            AND IncidentPlace='公区'
                            AND RegionalID IN
                            (
                                SELECT RegionalID FROM Tb_HSPR_IncidentRegional
                                WHERE CommID IN({condition}) AND UserCode='{Global_Var.LoginUserCode}'
                                UNION ALL
                                SELECT RegionalID FROM Tb_HSPR_RegionHousekeeper
                                WHERE CommID IN({condition}) AND UserCode='{Global_Var.LoginUserCode}'
                            )

                            UNION ALL
                            SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                            WHERE DrClass=1 AND isnull(IsDelete,0)=0 AND isnull(DispType,0)=0 AND CommID IN({condition}) AND isnull(BigCorpTypeID,0)=0 
                        )";

                var dataTable = GetList(out int PageCount, out int Counts, sql, pageIndex, pageSize, orderField, sort, "IncidentID",
                    PubConstant.hmWyglConnectionString).Tables[0];

                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow item in dataTable.Rows)
                    {
                        if (item["CommID"].ToString() == (commId * 10).ToString())
                        {
                            item["CommID"] = commId;
                        }
                        var roomId = AppGlobal.StrToLong(item["RoomID"]?.ToString());
                        var regionalId = AppGlobal.StrToLong(item["RegionalID"]?.ToString());
                        var bigCorpTypeId = AppGlobal.StrToLong(item["BigCorpTypeID"]?.ToString());

                        var isHousekeeper = false;

                        if (roomId > 0)
                            isHousekeeper = PMSUserIdentity.IsRoomHousekeeper(roomId, Global_Var.LoginUserCode, conn) ||
                                            PMSUserIdentity.IsBuildHousekeeper(roomId, Global_Var.LoginUserCode, conn);

                        if (regionalId > 0)
                            isHousekeeper = PMSUserIdentity.IsRegionHousekeeper(regionalId, Global_Var.LoginUserCode, conn);

                        if (isHousekeeper)
                        {
                            item["isDispatch"] = 1;
                        }
                        else
                        {
                            if (bigCorpTypeId > 0)
                            {
                                sql = @"SELECT * FROM Tb_HSPR_IncidentTypeAssignedPost 
                                    WHERE CorpTypeID=@CorpTypeID 
                                    AND RoleCode IN
                                    (
                                        /* 直接是通用岗位 */
                                        SELECT RoleCode FROM Tb_Sys_Role
                                        WHERE RoleCode IN
                                        (
                                            SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode
                                        ) AND SysRoleCode IS NULL

                                        UNION ALL
                                        /* 从具体岗位读通用岗位 */
                                        SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                                        WHERE RoleCode IN
                                        (
                                            SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode
                                        ) AND SysRoleCode IS NOT NULL
                                    )";
                                // 分派岗位，派单权限
                                if (conn.Query(sql, new { CorpTypeID = bigCorpTypeId, UserCode = Global_Var.LoginUserCode }).Count() > 0)
                                {
                                    item["isDispatch"] = 1;
                                }
                            }
                        }

                        // 处理岗位，抢单权限
                        sql = @"SELECT * FROM Tb_HSPR_IncidentTypeProcessPost 
                                WHERE CorpTypeID=@CorpTypeID 
                                AND RoleCode IN
                                (
                                    /* 直接是通用岗位 */
                                    SELECT RoleCode FROM Tb_Sys_Role
                                    WHERE RoleCode IN
                                    (
                                        SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode
                                    ) AND SysRoleCode IS NULL

                                    UNION ALL
                                    /* 从具体岗位读通用岗位 */
                                    SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                                    WHERE RoleCode IN
                                    (
                                        SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode
                                    ) AND SysRoleCode IS NOT NULL
                                )";

                        if (conn.Query(sql, new { CorpTypeID = bigCorpTypeId, UserCode = Global_Var.LoginUserCode }).Count() > 0)
                        {
                            item["isRob"] = 1;
                        }
                    }
                }

                var json = JSONHelper.FromString(dataTable);
                return json.Insert(json.Length - 1, ",\"PageCount\":" + PageCount + ",\"IsReserveAssigned\":" + isReserveAssigned.ToString().ToLower());
            }
        }

        /// <summary>
        /// 获取报事处理列表
        /// </summary>
        private string GetIncidentDealList(DataRow row)
        {
            var oldPMS = ConfigurationManager.AppSettings["OldPMS"];
            if (oldPMS != null && oldPMS == "1")
            {
                //return new IncidentAcceptManage_bl().GetIncidentDealList(row);
            }

            #region 基础数据校验
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return new ApiResult(false, "缺少参数CommID").toJson();
            }
            if (!row.Table.Columns.Contains("DrClass") || string.IsNullOrEmpty(row["DrClass"].ToString()))
            {
                return new ApiResult(false, "缺少参数DrClass").toJson();
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var drClass = AppGlobal.StrToInt(row["DrClass"].ToString());
            var pageIndex = 1;
            var pageSize = 10;
            var isAll = 0;

            /*
             * 1、默认按项目：本项目置顶，他项目依次，一个项目显示完后再显示下一项目；项目相同，按预约处理时间排序，最新的在上面；
             * 2、支持按时间：按预约处理时间排序，最新的在上面；
             * 3、支持按位置：先显示户内，后显示公区；位置相同，按预约处理时间
             */
            var type = 1;

            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = Convert.ToInt32(row["PageIndex"]);
            }
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = Convert.ToInt32(row["PageSize"]);
            }
            if (row.Table.Columns.Contains("IsAll") && !string.IsNullOrEmpty(row["IsAll"].ToString()))
            {
                isAll = Convert.ToInt32(row["IsAll"]);
            }

            #endregion 基础数据校验

            var sql = "";
            var condition = $"{commId}";
            var orderField = "ReserveDate DESC,CommID";

            if (type == 2)
                orderField = "ReserveDate";// 按预约处理时间排序，最新的在上面；
            else if (type == 3)
                orderField = "IncidentPlace DESC,ReserveDate";// 先显示户内，后显示公区；位置相同，按预约处理时间

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var control = GetIncidentControlSet(conn);
                if (control.AllowCrossRegionWorkOrder)
                {
                    // 允许区域工单
                    sql = @"SELECT CommID FROM Tb_Sys_RoleData 
                            WHERE CommID<>0 AND RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode)";
                    var comms = conn.Query<int>(sql, new { UserCode = Global_Var.LoginUserCode }).ToList();
                    comms.Insert(0, commId);

                    condition = $"{string.Join(",", comms)}";
                }
            }

            sql = $@"SELECT CASE WHEN a.CommID={commId} THEN a.CommID*10 ELSE a.CommID END AS CommID,
                        IncidentID,IncidentNum,IncidentContent,a.IncidentPlace,IncidentDate,Phone,
                        g.CommName,a.RoomID,RoomSign,RoomName,a.RegionalID,RegionalPlace,
                        e.TypeName AS BigTypeName,f.TypeName AS SmallTypeName,
	                    ReserveDate,ReserveLimit,DispDate,CoordinateNum,a.DealLimit,
                        DealManCode,a.Duty,ReceivingDate,ArriveData,a.IsTouSu,BigCorpTypeID,
                        0 AS CanDeal 
                    FROM Tb_HSPR_IncidentAccept a 
                    LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID
                    LEFT JOIN Tb_HSPR_IncidentRegional d ON a.RegionalID=d.RegionalID
                    LEFT JOIN Tb_HSPR_CorpIncidentType e ON a.BigCorpTypeID=e.CorpTypeID
                    LEFT JOIN Tb_HSPR_CorpIncidentType f ON a.FineCorpTypeID=f.CorpTypeID
                    LEFT JOIN Tb_HSPR_Community g ON a.CommID=g.CommID 
                    WHERE DrClass={drClass} ";

            // 书面
            if (drClass == 1)
            {
                if (isAll >= 1)
                {
                    sql += $@" AND IncidentID IN
                                (
                                    /*分派岗位，处理岗位*/
                                    SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                                    WHERE isnull(IsDelete,0)=0 AND isnull(DispType,0)=1 AND isnull(DealState,0)=0 AND CommID IN({condition})
                                    AND BigCorpTypeID IN
                                    (
                                        SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeAssignedPost 
                                        WHERE RoleCode IN
                                        (
                                            /* 直接是通用岗位 */
                                            SELECT RoleCode FROM Tb_Sys_Role
                                            WHERE RoleCode IN
                                            (
                                                SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ Global_Var.LoginUserCode }'
                                            ) AND SysRoleCode IS NULL

                                            UNION ALL
                                            /* 从具体岗位读通用岗位 */
                                            SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                                            WHERE RoleCode IN
                                            (
                                                SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ Global_Var.LoginUserCode }'
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
                                                SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ Global_Var.LoginUserCode }'
                                            ) AND SysRoleCode IS NULL

                                            UNION ALL
                                            /* 从具体岗位读通用岗位 */
                                            SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                                            WHERE RoleCode IN
                                            (
                                                SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ Global_Var.LoginUserCode }'
                                            ) AND SysRoleCode IS NOT NULL
                                        )
                                    )

                                    UNION ALL
                                    /*管家，设置到楼栋*/
                                    SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                                    WHERE isnull(IsDelete,0)=0 AND isnull(DispType,0)=1 AND isnull(DealState,0)=0 AND CommID IN({condition})
                                    AND (IncidentPlace='户内' OR (IncidentPlace='公区' AND RoomID<>0))
                                    AND RoomID IN
                                    (
                                        SELECT RoomID FROM Tb_HSPR_Room y 
                                        WHERE y.CommID=a.CommID 
                                        AND BuildSNum IN
                                        (
                                            SELECT BuildSNum FROM Tb_HSPR_BuildHousekeeper
                                             WHERE CommID=y.CommID AND UserCode='{Global_Var.LoginUserCode}'
                                        )
                                    )

                                    UNION ALL
                                    /*管家，设置到房屋*/
                                    SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                                    WHERE isnull(IsDelete,0)=0 AND isnull(DispType,0)=1 AND isnull(DealState,0)=0 AND CommID IN({condition})
                                    AND (IncidentPlace='户内' OR (IncidentPlace='公区' AND RoomID<>0))
                                    AND RoomID IN
                                    (
                                        SELECT RoomID FROM Tb_HSPR_Room WHERE CommID IN({condition}) AND UserCode='{Global_Var.LoginUserCode}'
                                        UNION ALL
                                        SELECT RoomID FROM Tb_HSPR_RoomHousekeeper WHERE RoomID=x.RoomID AND UserCode='{Global_Var.LoginUserCode}'
                                    )

                                    UNION ALL
                                    /*公区管家*/
                                    SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                                    WHERE isnull(IsDelete,0)=0 AND isnull(DispType,0)=1 AND isnull(DealState,0)=0 AND CommID IN({condition})
                                    AND IncidentPlace='公区'
                                    AND RegionalID IN
                                    (
                                        SELECT RegionalID FROM Tb_HSPR_IncidentRegional
                                        WHERE CommID IN({condition}) AND UserCode='{Global_Var.LoginUserCode}'
                                        UNION ALL
                                        SELECT RegionalID FROM Tb_HSPR_RegionHousekeeper
                                        WHERE CommID IN({condition}) AND UserCode='{Global_Var.LoginUserCode}'
                                    )
                                )";
                }
                else
                {
                    sql += $@" AND IncidentID IN
                                (
                                    /* 责任人 */
                                    SELECT IncidentID FROM Tb_HSPR_IncidentAccept
                                    WHERE isnull(IsDelete,0)=0 AND isnull(DispType,0)=1 AND isnull(DealState,0)=0 AND CommID IN({condition})
                                    AND DealManCode='{Global_Var.LoginUserCode}'

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
                                            SELECT AssistID FROM Tb_HSPR_IncidentAssistDetail WHERE UserCode='{Global_Var.LoginUserCode}'
                                        )
                                    )
                                )";
                }
            }
            // 口派
            else
            {
                sql += $@" AND IncidentID IN
                            (
                                /*处理人*/
                                SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                                WHERE isnull(IsDelete,0)=0 AND isnull(DrClass,1)=2 AND isnull(DealState,0)=0 AND CommID IN({condition})
                                AND isnull(DealManCode,'')='{Global_Var.LoginUserCode}'
        
                                UNION ALL
                                /*处理岗位*/
                                SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                                WHERE isnull(IsDelete,0)=0 AND isnull(DrClass,1)=2 AND isnull(DealState,0)=0 AND CommID IN({condition})
                                AND isnull(DealManCode,'')=''  
                                AND BigCorpTypeID IN
                                (
                                    SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeProcessPost 
                                    WHERE RoleCode IN
                                    (
                                        /* 直接是通用岗位 */
                                        SELECT RoleCode FROM Tb_Sys_Role
                                        WHERE RoleCode IN
                                        (
                                            SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ Global_Var.LoginUserCode }'
                                        ) AND SysRoleCode IS NULL

                                        UNION ALL
                                        /* 从具体岗位读通用岗位 */
                                        SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                                        WHERE RoleCode IN
                                        (
                                            SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ Global_Var.LoginUserCode }'
                                        ) AND SysRoleCode IS NOT NULL
                                    )
                                )
        
                                UNION ALL
                                /*分派岗位*/
                                SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                                WHERE isnull(IsDelete,0)=0 AND isnull(DrClass,1)=2 AND isnull(DealState,0)=0 AND CommID IN({condition})
                                AND BigCorpTypeID IN
                                (
                                    SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeAssignedPost 
                                    WHERE RoleCode IN
                                    (
                                        /* 直接是通用岗位 */
                                        SELECT RoleCode FROM Tb_Sys_Role
                                        WHERE RoleCode IN
                                        (
                                            SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ Global_Var.LoginUserCode }'
                                        ) AND SysRoleCode IS NULL

                                        UNION ALL
                                        /* 从具体岗位读通用岗位 */
                                        SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                                        WHERE RoleCode IN
                                        (
                                            SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ Global_Var.LoginUserCode }'
                                        ) AND SysRoleCode IS NOT NULL
                                    )
                                )

                                UNION ALL
                                /*楼栋管家*/
                                SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                                WHERE isnull(IsDelete,0)=0 AND isnull(DrClass,1)=2 AND isnull(DealState,0)=0 AND CommID IN({condition})
                                AND (IncidentPlace='户内' OR (IncidentPlace='公区' AND RoomID<>0))
                                AND RoomID IN
                                (
                                    SELECT RoomID FROM Tb_HSPR_Room y 
                                    WHERE y.CommID=a.CommID 
                                    AND BuildSNum IN
                                    (
                                        SELECT BuildSNum FROM Tb_HSPR_BuildHousekeeper
                                            WHERE CommID=y.CommID AND UserCode='{Global_Var.LoginUserCode}'
                                    )
                                )

                                UNION ALL
                                /*房屋管家*/
                                SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                                WHERE isnull(IsDelete,0)=0 AND isnull(DrClass,1)=2 AND isnull(DealState,0)=0 AND CommID IN({condition})
                                AND (IncidentPlace='户内' OR (IncidentPlace='公区' AND RoomID<>0))
                                AND RoomID IN
                                (
                                    SELECT RoomID FROM Tb_HSPR_Room WHERE CommID IN({condition}) AND UserCode='{Global_Var.LoginUserCode}'
                                    UNION ALL
                                    SELECT RoomID FROM Tb_HSPR_RoomHousekeeper WHERE RoomID=x.RoomID AND UserCode='{Global_Var.LoginUserCode}'
                                )

                                UNION ALL
                                /*公区管家*/
                                SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                                WHERE isnull(IsDelete,0)=0 AND isnull(DrClass,1)=2 AND isnull(DealState,0)=0 AND CommID IN({condition})
                                AND IncidentPlace='公区'
                                AND RegionalID IN
                                (
                                    SELECT RegionalID FROM Tb_HSPR_IncidentRegional
                                    WHERE CommID IN({condition}) AND UserCode='{Global_Var.LoginUserCode}'
                                    UNION ALL
                                    SELECT RegionalID FROM Tb_HSPR_RegionHousekeeper
                                    WHERE CommID IN({condition}) AND UserCode='{Global_Var.LoginUserCode}'
                                )
                            )";
            }

            var dataTable = GetList(out int PageCount, out int Counts, sql, pageIndex, pageSize, orderField, 1, "IncidentID", PubConstant.hmWyglConnectionString).Tables[0];
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                return new ApiResult(true, new string[] { }).toJson();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                foreach (DataRow item in dataTable.Rows)
                {
                    if (item["CommID"].ToString() == (commId * 10).ToString())
                    {
                        item["CommID"] = commId;
                    }

                    if (drClass == 1 && isAll < 1)
                    {
                        item["CanDeal"] = 1;
                    }
                    else
                    {
                        item["CanDeal"] = Convert.ToInt32(CanDeal((long)item["IncidentID"], Global_Var.LoginUserCode, conn));
                    }
                }

                var json = JSONHelper.FromString(dataTable);

                // 口派废弃权限
                int CanAbandon = 0;

                var funcode = "990311"; // 报事处理-书面报事废弃
                if (drClass == 2)
                    funcode = "990310"; // 报事处理-口派报事废弃

                sql = @"SELECT count(1) FROM Tb_Sys_FunctionPope
                        WHERE RoleCode IN
                        (
                            /* 直接是通用岗位 */
                            SELECT RoleCode FROM Tb_Sys_Role
                            WHERE RoleCode IN
                            (
                                SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode
                            ) AND SysRoleCode IS NULL

                            UNION ALL
                            /* 从具体岗位读通用岗位 */
                            SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                            WHERE RoleCode IN
                            (
                                SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode
                            ) AND SysRoleCode IS NOT NULL
                        )
                        AND FunCode=@FunCode;";

                if (conn.Query<int>(sql, new { UserCode = Global_Var.LoginUserCode, FunCode = funcode }).FirstOrDefault() > 0)
                {
                    CanAbandon = 1;
                }

                return json.Insert(json.Length - 1, string.Format(",\"PageCount\":{0},\"Counts\":{1},\"CanAbandon\":{2}", PageCount, Counts, CanAbandon));
            }
        }

        /// <summary>
        /// 获取报事关闭列表
        /// </summary>
        private string GetIncidentCloseList(DataRow row)
        {
            var oldPMS = ConfigurationManager.AppSettings["OldPMS"];
            if (oldPMS != null && oldPMS == "1")
            {
                //return new IncidentAcceptManage_bl().GetIncidentCloseList(row);
            }

            #region 基础数据校验

            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return new ApiResult(false, "缺少参数CommID").toJson();
            }
            if (!row.Table.Columns.Contains("OpType") || string.IsNullOrEmpty(row["OpType"].ToString()))//1我的工单 2全部工单
            {
                return new ApiResult(false, "缺少参数OpType").toJson();
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var opType = row["OpType"].ToString();

            int pageIndex = 1;
            int pageSize = 10;
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = Convert.ToInt32(row["PageIndex"]);
            }
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = Convert.ToInt32(row["PageSize"]);
            }

            #endregion 基础数据校验

            var sql = "";
            var condition = $"{commId}";

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var control = GetIncidentControlSet(conn);
                if (control.AllowCrossRegionWorkOrder)
                {
                    // 允许区域工单
                    sql = @"SELECT CommID FROM Tb_Sys_RoleData 
                            WHERE CommID<>0 AND RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode)";
                    var comms = conn.Query<int>(sql, new { UserCode = Global_Var.LoginUserCode }).ToList();
                    comms.Insert(0, commId);

                    condition = $"{string.Join(",", comms)}";
                }
            }

            var canClose = ",1 AS CanClose";
            var canFollow = ",1 AS CanFollow";

            //  全部工单，判断是否有关闭、跟进权限
            if (opType != "1")
            {
                canClose = $@",CASE WHEN (SELECT count(1) FROM
                                (
                                    /* 直接是通用岗位 */
                                    SELECT RoleCode FROM Tb_Sys_Role
                                    WHERE RoleCode IN
                                    (
                                        SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ Global_Var.LoginUserCode }'
                                    ) AND SysRoleCode IS NULL

                                    UNION ALL
                                    /* 从具体岗位读通用岗位 */
                                    SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                                    WHERE RoleCode IN
                                    (
                                        SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ Global_Var.LoginUserCode }'
                                    ) AND SysRoleCode IS NOT NULL

                                    INTERSECT
                                    SELECT RoleCode FROM Tb_HSPR_IncidentTypeClosePost WHERE CorpTypeID=a.BigCorpTypeID
                                ) AS t)>0 THEN 1 
                                WHEN a.IncidentPlace='户内' AND
                                    (SELECT count(1) FROM (
                                        SELECT UserCode FROM Tb_HSPR_Room WHERE RoomID=a.RoomID
                                        UNION ALL
                                        SELECT UserCode FROM Tb_HSPR_RoomHousekeeper WHERE RoomID=a.RoomID
                                        UNION ALL
                                        SELECT UserCode FROM Tb_HSPR_BuildHousekeeper 
                                        WHERE CommID=a.CommID AND BuildSNum=(SELECT BuildSNum FROM Tb_HSPR_Room WHERE RoomID=a.RoomID)
                                    ) AS t)>0 THEN 1 
                                WHEN a.IncidentPlace='公区' AND
                                    (SELECT count(1) FROM (
                                        SELECT UserCode FROM Tb_HSPR_IncidentRegional WHERE CommID=a.CommID AND RegionalID=a.RegionalID
                                        UNION ALL
                                        SELECT UserCode FROM Tb_HSPR_RegionHousekeeper WHERE CommID=a.CommID AND RegionID=a.RegionalID
                                    ) AS t)>0 THEN 1 
                                ELSE 0 END ";

                canFollow = canClose;
                canClose += " AS CanClose";
                canFollow += " AS CanFollow";
            }

            sql = $@"SELECT a.CommID,f.CommName,a.IncidentID,a.IncidentNum,a.IncidentContent,a.IncidentPlace,a.IncidentDate,
                        a.ReserveDate,a.Phone,a.DealMan,e.TypeName AS BigTypeName,g.TypeName AS FineTypeName,
                        a.RoomID,c.RoomSign,c.RoomName,a.RegionalID,d.RegionalPlace,a.FinishDate { canClose }{ canFollow } 
                    FROM Tb_HSPR_IncidentAccept a 
                        LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID
                        LEFT JOIN Tb_HSPR_IncidentRegional d ON a.RegionalID=d.RegionalID
                        LEFT JOIN Tb_HSPR_CorpIncidentType e ON a.BigCorpTypeID=e.CorpTypeID 
                        LEFT JOIN Tb_HSPR_Community f ON a.CommID=f.CommID
                        LEFT JOIN Tb_HSPR_CorpIncidentType g ON a.FineCorpTypeID=g.CorpTypeID 
                    WHERE isnull(a.IsDelete,0)=0 AND isnull(a.DealState,0)=1 AND isnull(a.IsClose,0)=0 ";

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var control = GetIncidentControlSet(conn);
                if (control.AllowCrossRegionWorkOrder)
                {
                    // 允许区域工单
                    var tmp = @"SELECT CommID FROM Tb_Sys_RoleData 
                                WHERE CommID!=0 AND RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode)";
                    var comms = conn.Query<int>(tmp, new { UserCode = Global_Var.LoginUserCode }).ToList();
                    comms.Insert(0, commId);

                    sql += $" AND a.CommID IN({string.Join(",", comms)})";
                }
                else
                {
                    sql += $" AND a.CommID={commId}";
                }
            }

            // 我的
            if (opType == "1")
            {
                sql += $@" AND 
                        (
                            a.BigCorpTypeID IN
                            (
                                /* 关闭岗位 */
                                SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeClosePost
                                WHERE RoleCode IN
                                (
                                    /* 直接是通用岗位 */
                                    SELECT RoleCode FROM Tb_Sys_Role
                                    WHERE RoleCode IN
                                    (
                                        SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ Global_Var.LoginUserCode }'
                                    ) AND SysRoleCode IS NULL

                                    UNION ALL
                                    /* 从具体岗位读通用岗位 */
                                    SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                                    WHERE RoleCode IN
                                    (
                                        SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{ Global_Var.LoginUserCode }'
                                    ) AND SysRoleCode IS NOT NULL
                                )
                            ) 
                            OR
                            a.IncidentID IN
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
                                            WHERE CommID=y.CommID AND UserCode='{Global_Var.LoginUserCode}'
                                    )
                                )

                                UNION ALL
                                /*管家，设置到房屋*/
                                SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                                WHERE DrClass=1 AND isnull(IsDelete,0)=0 AND isnull(DealState,0)=1 AND isnull(IsClose,0)=0 AND CommID IN({condition})
                                AND (IncidentPlace='户内' OR (IncidentPlace='公区' AND RoomID<>0))
                                AND RoomID IN
                                (
                                    SELECT RoomID FROM Tb_HSPR_Room WHERE CommID IN({condition}) AND UserCode='{Global_Var.LoginUserCode}'
                                    UNION ALL
                                    SELECT RoomID FROM Tb_HSPR_RoomHousekeeper WHERE RoomID=x.RoomID AND UserCode='{Global_Var.LoginUserCode}'
                                )

                                UNION ALL
                                /*公区管家*/
                                SELECT IncidentID FROM Tb_HSPR_IncidentAccept x
                                WHERE DrClass=1 AND isnull(IsDelete,0)=0 AND isnull(DealState,0)=1 AND isnull(IsClose,0)=0 AND CommID IN({condition})
                                AND IncidentPlace='公区'
                                AND RegionalID IN
                                (
                                    SELECT RegionalID FROM Tb_HSPR_IncidentRegional
                                    WHERE CommID IN({condition}) AND UserCode='{Global_Var.LoginUserCode}'
                                    UNION ALL
                                    SELECT RegionalID FROM Tb_HSPR_RegionHousekeeper
                                    WHERE CommID IN({condition}) AND UserCode='{Global_Var.LoginUserCode}'
                                )
                            )
                        )";
            }

            var ds = GetList(out int PageCount, out int Counts, sql, pageIndex, pageSize, "FinishDate", 1, "IncidentID", PubConstant.hmWyglConnectionString);

            var json = JSONHelper.FromString(ds.Tables[0]);
            return json.Insert(json.Length - 1, ",\"PageCount\":" + PageCount);
        }

        /// <summary>
        /// 获取报事审核列表
        /// </summary>
        private string GetIncidentAuditList(DataRow row)
        {
            return null;
        }

        /// <summary>
        /// 获取报事预警列表
        /// </summary>
        private string GetIncidentWarningList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("Step") || string.IsNullOrEmpty(row["Step"].ToString()))
            {
                return JSONHelper.FromString(false, "报事阶段不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            int pageIndex = 1;
            int pageSize = 10;
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = Convert.ToInt32(row["PageIndex"]);
            }
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = Convert.ToInt32(row["PageSize"]);
            }

            int step = AppGlobal.StrToInt(row["Step"].ToString());


            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = $@"SELECT a.IncidentID,b.IncidentNum,b.IncidentSource,b.CommID,f.CommName,
                                b.RoomID,c.RoomName,c.RoomSign,b.RegionalID,d.RegionalPlace,
                                b.DealSituation,b.DispLimit,b.DealLimit,b.ReplyLimit,b.Duty,b.IsTousu,
                                b.IncidentMan,b.IncidentMode,b.IncidentDate,b.IncidentPlace,b.IncidentContent,b.Phone,
                                b.DispDate,b.ReserveDate,b.ReceivingDate,b.ArriveData,b.FinishDate,b.CloseTime,
                                b.BigCorpTypeID,e.TypeName AS BigTypeName,isnull(g.TypeName,e.TypeName) AS TypeName,
                                b.DispMan,(SELECT isnull(MobileTel,'') FROM Tb_Sys_User WHERE UserCode=b.DispUserCode) AS DispManMobile,
                                b.DealMan,(SELECT isnull(MobileTel,'') FROM Tb_Sys_User WHERE UserCode=b.DealManCode) AS DealManMobile,
                                b.FinishUser,(SELECT isnull(MobileTel,'') FROM Tb_Sys_User WHERE UserCode=b.FinishUserCode) AS FinishManMobile,
                                b.CloseUser,(SELECT isnull(MobileTel,'') FROM Tb_Sys_User WHERE UserCode=b.CloseUserCode) AS CloseManMobile,
                                i.ReplyMan,(SELECT isnull(MobileTel,'') FROM Tb_Sys_User WHERE UserCode=i.ReplyManCode) AS ReplyManMobile,
                                i.ReplyContent,i.ReplyDate,
                                dbo.funIncidentDealState(dbo.funGetIncidentReplyInfoDate(i.IID, 2),b.CloseTime,b.FinishDate,b.DispDate,b.IncidentDate) as IncidentDealStateName
                            FROM Tb_HSPR_IncidentWarningPush a
                            LEFT JOIN Tb_HSPR_IncidentAccept b ON a.IncidentID=b.IncidentID
                            LEFT JOIN Tb_HSPR_Room c ON b.RoomID=c.RoomID
                            LEFT JOIN Tb_HSPR_IncidentRegional d ON b.RegionalID=d.RegionalID
                            LEFT JOIN Tb_HSPR_CorpIncidentType e ON b.BigCorpTypeID=e.CorpTypeID
                            LEFT JOIN Tb_HSPR_Community f ON a.CommID=f.CommID
                            LEFT JOIN Tb_HSPR_CorpIncidentType g ON b.FineCorpTypeID=g.CorpTypeID
                            LEFT JOIN Tb_HSPR_IncidentReply i ON i.IID=
                                (
                                    SELECT max(IID) AS IID FROM Tb_HSPR_IncidentReply x
                                    WHERE x.IsDelete=0 AND x.IncidentID=a.IncidentID
                                )
                            WHERE a.PushUsers LIKE '%{Global_Var.UserCode}%' AND a.IncidentStep={step}";

                var dt = GetList(out int PageCount, out int Counts, sql, pageIndex, pageSize, "IncidentDate", 1, "IncidentID", PubConstant.hmWyglConnectionString).Tables[0];

                var json = JSONHelper.FromString(dt);
                return json.Insert(json.Length - 1, ",\"PageCount\":" + PageCount);
            }
        }

        /// <summary>
        /// 报事查询
        /// </summary>
        private string IncidentSearch(DataRow row)
        {
            int commId = 0;
            int pageIndex = 1;
            int pageSize = 30;

            var roomId = 0L;
            var regionalId = 0L;
            var bigCorpTypeID = 0;

            var incidentNum = default(string);
            var incidentSource = default(string);
            var incidentPlace = default(string);
            var taskCategory = default(string);

            var startTime = default(string);
            var endTime = default(string);
            var dealStartTime = default(string);
            var dealEndTime = default(string);

            var drClass = 1;
            var isTousu = 0;
            var duty = default(string);

            var dealState = default(string);
            var admiMan = default(string);
            var dealMan = default(string);
            var houseKeeper = default(string);

            var incidentState = default(string);

            #region 
            var condition = new StringBuilder();

            if (row.Table.Columns.Contains("CommID") && !string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                commId = AppGlobal.StrToInt(row["CommID"].ToString());
                condition.AppendLine($" AND a.CommID={commId}");
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("RoomID") && !string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                roomId = AppGlobal.StrToLong(row["RoomID"].ToString());
                condition.AppendLine($" AND a.RoomID={roomId}");
            }
            if (row.Table.Columns.Contains("RegionalID") && !string.IsNullOrEmpty(row["RegionalID"].ToString()))
            {
                regionalId = AppGlobal.StrToLong(row["RegionalID"].ToString());
                condition.AppendLine($" AND a.RegionalID={regionalId}");
            }
            if (row.Table.Columns.Contains("BigCorpTypeID") && !string.IsNullOrEmpty(row["BigCorpTypeID"].ToString()))
            {
                bigCorpTypeID = AppGlobal.StrToInt(row["BigCorpTypeID"].ToString());
                condition.AppendLine($" AND a.BigCorpTypeID={bigCorpTypeID}");
            }
            if (row.Table.Columns.Contains("IncidentNum") && !string.IsNullOrEmpty(row["IncidentNum"].ToString()))
            {
                incidentNum = row["IncidentNum"].ToString();
                condition.AppendLine($" AND a.IncidentNum='{incidentNum}'");
            }
            if (row.Table.Columns.Contains("IncidentSource") && !string.IsNullOrEmpty(row["IncidentSource"].ToString()))
            {
                incidentSource = row["IncidentSource"].ToString();
                condition.AppendLine($" AND a.IncidentSource='{incidentSource}'");
            }
            if (row.Table.Columns.Contains("IncidentPlace") && !string.IsNullOrEmpty(row["IncidentPlace"].ToString()))
            {
                incidentPlace = row["IncidentPlace"].ToString();
                condition.AppendLine($" AND a.IncidentPlace='{incidentPlace}'");
            }
            if (row.Table.Columns.Contains("TaskCategory") && !string.IsNullOrEmpty(row["TaskCategory"].ToString()))
            {
                taskCategory = row["TaskCategory"].ToString();
                condition.AppendLine($" AND m.TaskCategory='{taskCategory}'");
            }
            if (row.Table.Columns.Contains("StartTime") && !string.IsNullOrEmpty(row["StartTime"].ToString()))
            {
                startTime = row["StartTime"].ToString();
                if (DateTime.TryParse(startTime, out DateTime datetime))
                {
                    condition.AppendLine($" AND a.IncidentDate>=convert(datetime,'{datetime.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }
            }
            if (row.Table.Columns.Contains("EndTime") && !string.IsNullOrEmpty(row["EndTime"].ToString()))
            {
                endTime = row["EndTime"].ToString();
                if (DateTime.TryParse(endTime, out DateTime datetime))
                {
                    condition.AppendLine($" AND a.IncidentDate<=convert(datetime,'{datetime.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }
            }
            if (row.Table.Columns.Contains("DealStartTime") && !string.IsNullOrEmpty(row["DealStartTime"].ToString()))
            {
                dealStartTime = row["DealStartTime"].ToString();
                if (DateTime.TryParse(dealStartTime, out DateTime datetime))
                {
                    condition.AppendLine($" AND a.ArriveData>=convert(datetime,'{datetime.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }
            }
            if (row.Table.Columns.Contains("DealEndTime") && !string.IsNullOrEmpty(row["DealEndTime"].ToString()))
            {
                dealEndTime = row["DealEndTime"].ToString();
                if (DateTime.TryParse(dealEndTime, out DateTime datetime))
                {
                    condition.AppendLine($" AND a.FinishDate<=convert(datetime,'{datetime.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }
            }
            if (row.Table.Columns.Contains("DrClass") && !string.IsNullOrEmpty(row["DrClass"].ToString()))
            {
                drClass = AppGlobal.StrToInt(row["DrClass"].ToString());

                if (drClass != 0)
                {
                    drClass = (drClass != 1 && drClass != 2) ? 1 : drClass;
                    condition.AppendLine($" AND a.DrClass={drClass}");
                }
            }
            if (row.Table.Columns.Contains("IsTousu") && !string.IsNullOrEmpty(row["IsTousu"].ToString()))
            {
                isTousu = AppGlobal.StrToInt(row["IsTousu"].ToString());
                isTousu = (isTousu != 0 && isTousu != 1) ? 0 : isTousu;
                condition.AppendLine($" AND a.IsTousu={isTousu}");
            }
            if (row.Table.Columns.Contains("Duty") && !string.IsNullOrEmpty(row["Duty"].ToString()))
            {
                duty = row["Duty"].ToString();
                condition.AppendLine($" AND a.Duty={duty}");
            }
            if (row.Table.Columns.Contains("DealState") && !string.IsNullOrEmpty(row["DealState"].ToString()))
            {
                dealState = row["DealState"].ToString();

                var getSql = @"SELECT isnull(object_id('funIncidentDealState_zt'),0);";

                var conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                var getCount = conn.Query<int>(getSql).FirstOrDefault();

                // 判断是否有函数funIncidentDealState_zt
                if (getCount == 0)
                {
                    condition.AppendLine($@" AND dbo.funIncidentDealState(
                                dbo.funGetIncidentReplyInfoDate(xx.IID, 2),
                                a.CloseTime,
                                a.MainEndDate,
                                a.DispDate,
                                a.IncidentDate
                            ) IN (SELECT Value FROM SplitString('{dealState}',',',1))");
                }
                else {
                    condition.AppendLine($@" AND dbo.funIncidentDealState_zt(
                                dbo.funGetIncidentReplyInfoDate(xx.IID, 2),
                                a.CloseTime,
                                a.MainEndDate,
                                a.ArriveData,
                                a.ReceivingDate,
                                a.DispDate,
                                a.IncidentDate
                            ) IN (SELECT Value FROM SplitString('{dealState}',',',1))");
                }
            }
            if (row.Table.Columns.Contains("AdmiMan") && !string.IsNullOrEmpty(row["AdmiMan"].ToString()))
            {
                admiMan = row["AdmiMan"].ToString();
                var tmp = admiMan.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(obj => $"'{obj}'").ToArray();
                condition.AppendLine($" AND a.AdmiMan IN({string.Join(",", tmp)})");
            }
            if (row.Table.Columns.Contains("DealMan") && !string.IsNullOrEmpty(row["DealMan"].ToString()))
            {
                dealMan = row["DealMan"].ToString();
                var tmp = dealMan.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(obj => $"'{obj}'").ToArray();
                condition.AppendLine($" AND a.DealManCode IN({string.Join(",", tmp)})");
            }
            if (row.Table.Columns.Contains("HouseKeeper") && !string.IsNullOrEmpty(row["HouseKeeper"].ToString()))
            {
                houseKeeper = row["HouseKeeper"].ToString();
                var tmp = houseKeeper.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(obj => $"'{obj}'").ToArray();
                condition.AppendLine($@" AND a.RoomID IN
                                        (
                                            SELECT a.RoomID FROM Tb_HSPR_Room x 
                                            JOIN Tb_HSPR_BuildHousekeeper y ON x.CommID=y.CommID AND x.BuildSNum=y.BuildSNum 
                                            WHERE y.UserCode IN ({string.Join(",", tmp)})
                                            UNION
                                            SELECT RoomID FROM Tb_HSPR_Room WHERE UserCode IN({string.Join(",", tmp)})
                                        )");
            }
            if (row.Table.Columns.Contains("IncidentState") && !string.IsNullOrEmpty(row["IncidentState"].ToString()))
            {
                incidentState = row["IncidentState"].ToString();
                if (incidentState == "有效")
                {
                    condition.AppendLine($" AND isnull(a.IsDelete,0)=0");
                }
                else if (incidentState == "无效" || incidentState == "废弃")
                {
                    condition.AppendLine($" AND isnull(a.IsDelete,0)=1");
                }
            }
            #endregion

            var sql = $@"SELECT a.IncidentID,a.IncidentNum,b.CommName,c.CustName,a.RoomID,d.RoomSign,d.RoomName,
                            a.RegionalID,f.RegionalPlace,g.DictionaryName AS LocalePosition,h.DictionaryName AS LocaleFunction,
                            a.IncidentPlace,a.IncidentSource,a.IncidentMan,a.IncidentDate,a.IncidentContent,m.TaskType,
                            k.TypeName AS BigTypeName,a.Duty,a.ReserveDate,a.DealMan,a.Phone,a.NoNormalCloseReasons,
                            a.CloseType,
                            CASE WHEN isnull(a.IsDelete,0)=1 THEN '已废弃'
                                 WHEN (SELECT Count(1) FROM Tb_HSPR_IncidentReply z WHERE z.IncidentID=a.IncidentID AND isnull(z.IsDelete,0)=0)>0 THEN '已回访'
                                 WHEN isnull(a.IsClose,0)=1 THEN '已关闭'
                                 WHEN isnull(a.FinishDate,'')<>'' THEN '已处理'
                                 WHEN isnull(a.ArriveData,'')<>'' THEN '已到场处理'
                                 WHEN isnull(a.ReceivingDate,'')<>'' THEN '已接单'
                                 WHEN isnull(a.DispType,'')=1 THEN '已分派'
                                 ELSE '已受理'
                            END AS IncidentDealStateName
                        FROM Tb_HSPR_IncidentAccept a
                        LEFT JOIN Tb_HSPR_Community b ON a.CommID=b.CommID
                        LEFT JOIN Tb_HSPR_Customer c ON a.CustID=c.CustID
                        LEFT JOIN Tb_HSPR_Room d ON a.RoomID=d.RoomID
                        LEFT JOIN Tb_HSPR_IncidentRegional f ON a.RegionalID=f.RegionalID
                        LEFT JOIN Tb_HSPR_IncidentPublicLocale g ON a.LocalePosition=g.IID
                        LEFT JOIN Tb_HSPR_IncidentPublicLocale h ON a.LocaleFunction=h.IID
                        LEFT JOIN Tb_HSPR_CorpIncidentType k ON a.BigCorpTypeID=k.CorpTypeID
                        LEFT JOIN Tb_HSPR_CorpIncidentType l ON a.FineCorpTypeID=l.CorpTypeID
                        LEFT JOIN (SELECT max(IID) AS IID,IncidentID FROM Tb_HSPR_IncidentReply x 
                                    WHERE isnull(x.IsDelete,0)=0 GROUP BY IncidentID) AS xx
                            ON a.IncidentID=xx.IncidentID
                        LEFT JOIN Tb_HSPR_IncidentAssociatedTask m ON a.IncidentID=m.IncidentID
                        WHERE 1=1 {condition}";

            var data = GetListDapper(out int pageCount, out int count, sql, pageIndex, pageSize, "IncidentDate", 1, "IncidentID", new SqlConnection(PubConstant.hmWyglConnectionString));

            if (incidentState == "无效" || incidentState == "废弃")
            {
                //data.Foreach(obj => obj.IncidentDealStateName = "已废弃");
            }

            var json = new ApiResult(true, data).toJson(); ;
            json = json.Insert(json.Length - 1, ",\"PageCount\":" + pageCount + ",\"Count\":" + count);
            return json;
        }

        /// <summary>
        /// 报事查询我的工单-禅道需求-7838
        /// </summary>
        private string IncidentSearchMyList(DataRow row)
        {

            int commId = 0;
            if (row.Table.Columns.Contains("CommID") && !string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                commId = AppGlobal.StrToInt(row["CommID"].ToString());
            }
            if (!row.Table.Columns.Contains("IncidentMan") || string.IsNullOrEmpty(row["IncidentMan"].ToString()))
            {
                return new ApiResult(false, "缺少参数IncidentMan").toJson();
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].AsString()))
            {
                return new ApiResult(false, "缺少参数PageIndex").toJson();
            }

            int pageIndex = AppGlobal.StrToInt(row[@"PageIndex"].ToString());
            int pageSize = 10;
            var incidentMan = row[@"IncidentMan"].ToString();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT *FROM (
	                            SELECT 
	                            row_number() OVER (ORDER BY a.IncidentDate) AS RID,
		                            CONVERT(varchar(20),a.IncidentID) AS IncidentID,a.IncidentNum,b.CommName,c.CustName,d.RoomSign,d.RoomName,
		                            f.RegionalPlace,g.DictionaryName AS LocalePosition,h.DictionaryName AS LocaleFunction,
		                            a.IncidentPlace,a.IncidentSource,a.IncidentMan,a.IncidentDate,a.IncidentContent,m.TaskType,
		                            k.TypeName AS BigTypeName,a.Duty,a.ReserveDate,a.DealMan,a.Phone,a.NoNormalCloseReasons,
		                            dbo.funIncidentDealState(
			                            dbo.funGetIncidentReplyInfoDate(xx.IID, 2),
			                            a.CloseTime,
			                            a.MainEndDate,
			                            a.DispDate,
			                            a.IncidentDate
		                            ) AS IncidentDealStateName
	                            FROM Tb_HSPR_IncidentAccept a 
	                            LEFT JOIN Tb_HSPR_Community b ON a.CommID=b.CommID
	                            LEFT JOIN Tb_HSPR_Customer c ON a.CustID=c.CustID
	                            LEFT JOIN Tb_HSPR_Room d ON a.RoomID=d.RoomID
	                            LEFT JOIN Tb_HSPR_IncidentRegional f ON a.RegionalID=f.RegionalID
	                            LEFT JOIN Tb_HSPR_IncidentPublicLocale g ON a.LocalePosition=g.IID
	                            LEFT JOIN Tb_HSPR_IncidentPublicLocale h ON a.LocaleFunction=h.IID
	                            LEFT JOIN Tb_HSPR_CorpIncidentType k ON a.BigCorpTypeID=k.CorpTypeID
	                            LEFT JOIN Tb_HSPR_CorpIncidentType l ON a.FineCorpTypeID=l.CorpTypeID
	                            LEFT JOIN (SELECT max(IID) AS IID,IncidentID FROM Tb_HSPR_IncidentReply x 
				                            WHERE isnull(x.IsDelete,0)=0 GROUP BY IncidentID) AS xx
		                            ON a.IncidentID=xx.IncidentID
	                            LEFT JOIN Tb_HSPR_IncidentAssociatedTask m ON a.IncidentID=m.IncidentID
	                            WHERE a.IncidentMan=@IncidentMan AND isnull(a.IsDelete,0)=0 AND a.CommID=@CommID
                            ) AS t WHERE  t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize)";


                var data = conn.Query(sql, new
                {
                    CommId = commId,
                    IncidentMan = incidentMan,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                });

                return new ApiResult(true, data).toJson();
            }




        }
    }
}
