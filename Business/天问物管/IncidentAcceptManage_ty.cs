using Dapper;
using DapperExtensions;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.HSPR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static Dapper.SqlMapper;

namespace Business
{
    public partial class IncidentAcceptManage
    {
        public struct structFees
        {
            public string FeesDueDate;
            public string FeesStateDate;
            public string FeesEndDate;
            public decimal DueAmount;
        }

        /// <summary>
        /// 是否具有分派权限
        /// </summary>
        private string CanAssign(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            string commid = row["CommID"].ToString();
            string incidentID = row["IncidentID"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = "select * from Tb_HSPR_IncidentAccept where IncidentID=@IncidentID";
                dynamic incidentInfo = conn.Query(sql, new { IncidentID = incidentID }).FirstOrDefault();

                if (incidentInfo != null)
                {
                    sql = @"SELECT count(1) AS Count FROM Tb_Sys_RolePope a LEFT JOIN Tb_Sys_PowerNode b ON a.PNodeCode = b.PNodeCode
                            WHERE b.PNodeName = '报事分派'  AND a.RoleCode IN
                            (
                                    SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode = @UserCode
                                    UNION
                                    SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                                        WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode= @UserCode)
							)";

                    // 敏捷
                    if (Global_Var.LoginCorpID == "1971")
                    {
                        if (incidentInfo.BigCorpTypeID == null)
                        {
                            // 如果该条报事没有设置报事类别，查询当前用户是否具有小区报事分派模块权限
                            if (conn.Query(sql, new { CommID = commid, UserCode = Global_Var.LoginUserCode }).Count() > 0)
                            {
                                return JSONHelper.FromString(true, "1");
                            }
                            return JSONHelper.FromString(true, "0");
                        }
                    }
                    else
                    {
                        // 没有报事类别，默认就没有权限
                        if (incidentInfo.TypeID == null || incidentInfo.TypeID.Trim(',') == "")
                        {
                            if (conn.Query<int>(sql, new { CommID = commid, UserCode = Global_Var.LoginUserCode }).FirstOrDefault() > 0)
                            {
                                return JSONHelper.FromString(true, "1");
                            }
                            return JSONHelper.FromString(true, "0");
                        }
                    }
                }
                else
                {
                    return JSONHelper.FromString(true, "0");
                }

                // 1、如果是户内报事，是否设置了楼栋管家，如果设置了楼栋管家
                if (incidentInfo.IncidentPlace == "业主权属")
                {
                    // 判断报事房屋所在楼栋是否设置了楼栋管家
                    sql = string.Format(@"SELECT UserCode FROM Tb_HSPR_BuildHousekeeper WHERE CommID={0} AND BuildSNum IN (SELECT BuildSNum FROM Tb_HSPR_Room WHERE CommID={0} AND RoomID={1} AND isnull(IsDelete,0)=0)", incidentInfo.CommID, incidentInfo.RoomID);

                    IEnumerable<dynamic> houseKeeperResult = conn.Query(sql);

                    // 设置了楼栋管家
                    if (houseKeeperResult.Count() > 0)
                    {
                        foreach (dynamic item in houseKeeperResult)
                        {
                            // 该用户是楼栋管家，判断是否具有分派权限
                            if (item.UserCode == Global_Var.LoginUserCode)
                            {
                                //// 判断楼栋管家是否具有分派权限，这里的分派岗位，是针对通用岗位
                                //sql = @"SELECT SysRoleCode FROM
                                //    (SELECT SysRoleCode FROM Tb_Sys_Role WHERE SysRoleCode IS NOT NULL AND
                                //        (RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode) OR
                                //        SysRoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode))) AS a 
                                //    WHERE a.SysRoleCode IN
                                //    (SELECT DISTINCT RoleCode FROM Tb_HSPR_IncidentTypeAssignedPost WHERE CorpTypeID IN
                                //    (SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE TypeID IN (SELECT Convert(nvarchar(50), ColName) FROM dbo.funSplitTabel(@TypeID, ','))))";

                                //IEnumerable<dynamic> sysRoleCodeResult = conn.Query(sql, new { UserCode = item.UserCode, TypeID = incidentInfo.TypeID });

                                //// 有分派权限
                                //if (sysRoleCodeResult.Count() > 0)
                                //{
                                //    return JSONHelper.FromString(true, "1");
                                //}
                                return JSONHelper.FromString(true, "1");
                            }
                        }

                        //// 不是楼栋管家或者楼栋管家不具有分派权限
                        //return JSONHelper.FromString(true, "0");
                    }

                    // 未设置楼栋管家，判断该用户是否具有分派权限
                    return CanAssignIfNoHouseKeeper(PubConstant.hmWyglConnectionString, incidentInfo);
                }
                else
                {
                    // 如果是公区报事
                    return CanAssignIfNoHouseKeeper(PubConstant.hmWyglConnectionString, incidentInfo);
                }
            }
        }

        /// <summary>
        /// 没有楼栋管家时，是否可以分派
        /// </summary>
        private string CanAssignIfNoHouseKeeper(string strcon, dynamic incidentInfo)
        {
            using (IDbConnection conn = new SqlConnection(strcon))
            {
                // 没有报事类别，默认就没有权限
                if (incidentInfo.CommID.ToString().StartsWith("1971"))
                {
                    if (incidentInfo.BigCorpTypeID == null)
                    {
                        return JSONHelper.FromString(true, "0");
                    }
                }
                else
                {
                    if (incidentInfo.TypeID == null || incidentInfo.TypeID.Trim(',') == "")
                    {
                        return JSONHelper.FromString(true, "0");
                    }
                }

                // 设置了报事类别，判断是否具有该报事类别分派权限，这里的分派岗位，是针对通用岗位
                string sql = $@"SELECT RoleCode FROM Tb_HSPR_IncidentTypeAssignedPost 
                                WHERE CorpTypeID IN (SELECT CorpTypeID FROM Tb_HSPR_IncidentType 
                                    WHERE TypeID IN (SELECT Convert(nvarchar(50), ColName) 
                                        FROM dbo.funSplitTabel('{incidentInfo.TypeID}', ',')))";

                // 敏捷
                if (Global_Var.LoginCorpID == "1971")
                {
                    sql = $@"SELECT RoleCode FROM Tb_HSPR_IncidentTypeAssignedPost 
                                WHERE CorpTypeID IN (SELECT CorpTypeID FROM Tb_HSPR_IncidentType 
                                    WHERE CorpTypeID IN (SELECT Convert(nvarchar(50), ColName) 
                                        FROM dbo.funSplitTabel('{incidentInfo.BigCorpTypeID}', ',')))";
                }

                IEnumerable<dynamic> assignedPostResult = conn.Query(sql);

                // 设置了分派岗位，判断用户是否具有相关分派岗位权限
                if (assignedPostResult.Count() > 0)
                {
                    sql = $@"SELECT * FROM (SELECT DISTINCT SysRoleCode FROM Tb_Sys_Role
                            WHERE RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{Global_Var.LoginUserCode}')) AS a 
                            WHERE a.SysRoleCode IN(SELECT RoleCode FROM Tb_HSPR_IncidentTypeAssignedPost 
                                WHERE CorpTypeID IN (SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE TypeID IN (
                                    SELECT Convert(nvarchar(50), ColName) FROM dbo.funSplitTabel('{incidentInfo.TypeID}', ','))))";

                    // 敏捷
                    if (Global_Var.LoginCorpID == "1971")
                    {
                        sql = $@"SELECT * FROM (SELECT DISTINCT SysRoleCode FROM Tb_Sys_Role
                            WHERE RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{Global_Var.LoginUserCode}')) AS a 
                            WHERE a.SysRoleCode IN(SELECT RoleCode FROM Tb_HSPR_IncidentTypeAssignedPost 
                                WHERE CorpTypeID IN (SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE CorpTypeID IN (
                                    SELECT Convert(nvarchar(50), ColName) FROM dbo.funSplitTabel('{incidentInfo.BigCorpTypeID}', ','))))";
                    }

                    IEnumerable<dynamic> result = conn.Query(sql);

                    if (result.Count() > 0)
                    {
                        return JSONHelper.FromString(true, "1");
                    }

                    return JSONHelper.FromString(true, "0");
                }
                else
                {
                    // 敏捷
                    if (Global_Var.LoginCorpID == "1971")
                    {
                        return JSONHelper.FromString(true, "0");
                    }

                    // 未设置分派岗位，并且未设置处理岗位，那么，判断是否具有报事分派模块权限

                    // 判断是否设置了处理岗位
                    sql = $@"SELECT * FROM Tb_HSPR_IncidentTypeProcessPost 
                             WHERE CorpTypeID IN ((SELECT Convert(nvarchar(50), ColName) 
                                FROM dbo.funSplitTabel('{incidentInfo.TypeID}', ',')))";



                    IEnumerable<dynamic> result = conn.Query(sql);

                    // 设置了处理岗位，那么没有权限
                    if (result.Count() > 0)
                    {
                        return JSONHelper.FromString(true, "0");
                    }

                    // 判断是否具报事分派模块权限
                    //sql = string.Format(@"SELECT RoleCode FROM Tb_Sys_RolePope WHERE PNodeCode='0109060303'
                    //                  AND RoleCode IN (SELECT RoleCode FROM view_Sys_User_RoleData_Filter2 WHERE CommID={0} AND UserCode='{1}')
                    //                UNION
                    //                SELECT RoleCode FROM Tb_Sys_RolePope WHERE PNodeCode='0109060303'
                    //                  AND RoleCode IN (SELECT RoleCode FROM Tb_Sys_Role WHERE SysRoleCode IN(
                    //                    SELECT RoleCode FROM view_Sys_User_RoleData_Filter2 WHERE CommID={0} AND UserCode='{1}'))",
                    //      incidentInfo.CommID, Global_Var.LoginUserCode);
                    sql = string.Format(@"SELECT RoleCode FROM Tb_Sys_RolePope WHERE PNodeCode='0109060303' AND RoleCode IN
                                            (
	                                            SELECT RoleCode FROM view_Sys_User_RoleData_Filter2 WHERE CommID={0} AND UserCode='{1}'
	                                            UNION
	                                            SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role WHERE SysRoleCode IS NOT NULL AND 
                                                   RoleCode IN(SELECT RoleCode FROM view_Sys_User_RoleData_Filter2 WHERE CommID={0} AND UserCode='{1}')
                                            )", incidentInfo.CommID, Global_Var.LoginUserCode);

                    result = conn.Query(sql);

                    if (result.Count() > 0)
                    {
                        return JSONHelper.FromString(true, "1");
                    }
                    return JSONHelper.FromString(true, "0");
                }
            }

        }

        /// <summary>
        /// 是否具有抢单权限
        /// </summary>
        private string CanSnatch(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            string commid = row["CommID"].ToString();
            string incidentID = row["IncidentID"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = "select * from Tb_HSPR_IncidentAccept where IncidentID=@IncidentID";
                if (Global_Var.LoginCorpID == "1970")
                {
                    sql = "select * from Tb_HSPR_IncidentAccept where IncidentID=@IncidentID AND ISNULL(IsSnatchBack,0)= 0";
                }
                dynamic incidentInfo = conn.Query(sql, new { IncidentID = incidentID }).FirstOrDefault();

                if (incidentInfo != null)
                {
                    // 敏捷
                    if (Global_Var.LoginCorpID == "1971")
                    {
                        if (incidentInfo.BigCorpTypeID == null)
                        {
                            return JSONHelper.FromString(true, "0");
                        }
                    }
                    else
                    {
                        // 没有报事类别，默认就没有权限
                        if (incidentInfo.TypeID == null || incidentInfo.TypeID.ToString().Replace(",", "") == "")
                        {
                            return JSONHelper.FromString(true, "0");
                        }
                    }
                }
                else
                {
                    return JSONHelper.FromString(true, "0");
                }

                // 设置了报事类别，判断是否具有该报事类别处理权限，这里的处理岗位，是针对通用岗位
                sql = $@"SELECT RoleCode FROM Tb_HSPR_IncidentTypeProcessPost WHERE CorpTypeID IN 
                        (SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE TypeID IN (SELECT Convert(nvarchar(50), ColName) 
                            FROM dbo.funSplitTabel('{incidentInfo.TypeID}', ',')))";

                // 敏捷
                if (Global_Var.LoginCorpID == "1971")
                {
                    sql = $@"SELECT RoleCode FROM Tb_HSPR_IncidentTypeSnatchPost WHERE CorpTypeID IN 
                        (SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE CorpTypeID IN (SELECT Convert(nvarchar(50), ColName) 
                            FROM dbo.funSplitTabel('{incidentInfo.BigCorpTypeID}', ',')))";
                }

                IEnumerable<dynamic> snatchPostResult = conn.Query(sql);

                // 设置了处理/抢单岗位，判断用户是否具有相关处理/抢单岗位权限
                if (snatchPostResult.Count() > 0)
                {
                    sql = $@"SELECT * FROM (SELECT DISTINCT SysRoleCode FROM Tb_Sys_Role
                             WHERE RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{Global_Var.LoginUserCode}')) AS a 
                             WHERE a.SysRoleCode IN(SELECT RoleCode FROM Tb_HSPR_IncidentTypeProcessPost 
                                WHERE CorpTypeID IN (SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE TypeID IN (
                                    SELECT Convert(nvarchar(50), ColName) FROM dbo.funSplitTabel('{incidentInfo.TypeID}', ','))))";

                    // 敏捷
                    if (Global_Var.LoginCorpID == "1971")
                    {
                        sql = $@"SELECT * FROM (SELECT DISTINCT SysRoleCode FROM Tb_Sys_Role
                             WHERE RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{Global_Var.LoginUserCode}')) AS a 
                             WHERE a.SysRoleCode IN(SELECT RoleCode FROM Tb_HSPR_IncidentTypeSnatchPost 
                                WHERE CorpTypeID IN (SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE CorpTypeID IN (
                                    SELECT Convert(nvarchar(50), ColName) FROM dbo.funSplitTabel('{incidentInfo.BigCorpTypeID}', ','))))";
                    }

                    IEnumerable<dynamic> result = conn.Query(sql);

                    if (result.Count() > 0)
                    {
                        return JSONHelper.FromString(true, "1");
                    }
                }

                return JSONHelper.FromString(true, "0");
            }
        }

        /// <summary>
        /// 获取本人可【派】的未分派报事列表
        /// </summary>
        private string GetCanAssignIncidentList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            int PageIndex = 1;
            int PageSize = 20;
            if (row.Table.Columns.Contains("PageIndex"))
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            if (row.Table.Columns.Contains("PageSize"))
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            string commId = row["CommID"].ToString();
            string resultStr = null;

            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 获取具有分派权限的报事类别
                string sql = @"SELECT(SELECT cast(TypeID AS VARCHAR)+','
                                FROM Tb_HSPR_IncidentType WHERE CommID=@CommID AND CorpTypeID IN (
                                  SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeAssignedPost WHERE RoleCode IN(
                                    SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN(
                                      SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode))) FOR XML PATH ('')) AS TypeID";

                // 敏捷
                if (Global_Var.CorpId == "1971")
                {
                    sql = @"SELECT(SELECT cast(TypeCode AS VARCHAR)+','
                                FROM Tb_HSPR_IncidentType WHERE CommID=@CommID AND CorpTypeID IN (
                                  SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeAssignedPost WHERE RoleCode IN(
                                    SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN(
                                      SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode))) FOR XML PATH ('')) AS TypeID";
                }

                dynamic canAssignTypeIDs = con.Query(sql, new { CommID = commId, UserCode = Global_Var.UserCode }).FirstOrDefault();

                sql = "";
                if (canAssignTypeIDs != null && canAssignTypeIDs.TypeID != null && canAssignTypeIDs.TypeID.ToString().Trim(',') != "")
                {
                    // 未分派的报事
                    sql = string.Format(@"SELECT distinct IncidentID,IncidentContent,IncidentPlace,IncidentMode,RoomSign,RoomName,TypeName,
                                            ReserveDate,Phone,IncidentNum,RegionalPlace
                                            FROM view_HSPR_IncidentAccept_Filter v
                                            WHERE CommID={0} AND isnull(DispType,0)=0 AND isnull(IsDelete,0)=0 AND IsStatistics=1 
                                                  AND (SELECT count(1) as Count FROM (SELECT VALUE FROM dbo.SplitString(v.TypeID,',',1) 
                                            InterSect 
                                            SELECT VALUE FROM dbo.SplitString('{1}',',',1)) as A)>0", commId, canAssignTypeIDs.TypeID);

                    // 敏捷排除责任归属为“地产”的
                    if (Global_Var.CorpId == "1971")
                    {
                        sql = string.Format(@"SELECT distinct IncidentID,IncidentContent,IncidentPlace,IncidentMode,RoomSign
                                            ,RoomName,TypeName,NC_NewRoomSign,
                                            ReserveDate,Phone,IncidentNum,RegionalPlace
                                            FROM view_HSPR_IncidentAccept_Filter v
                                            WHERE CommID={0} AND isnull(DispType,0)=0 AND isnull(IsDelete,0)=0 AND IsStatistics=1 
                                                  AND (SELECT count(1) as Count FROM (SELECT VALUE FROM dbo.SplitString(v.BigCorpTypeCode,',',1) 
                                            InterSect 
                                            SELECT VALUE FROM dbo.SplitString('{1}',',',1)) as A)>0", commId, canAssignTypeIDs.TypeID);
                        sql += " AND v.Attribution<>'地产' AND datediff(minute, v.IncidentDate, getdate())>2";
                    }

                    sql += " UNION ";
                }

                // 敏捷排除责任归属为“地产”的
                if (Global_Var.CorpId == "1971")
                {
                    sql += string.Format(@"SELECT distinct IncidentID,IncidentContent,IncidentPlace,IncidentMode,a.RoomSign,a.RoomName
                                            ,TypeName,a.NC_NewRoomSign,
                                            ReserveDate,Phone,IncidentNum,RegionalPlace
                                            FROM view_HSPR_IncidentAccept_Filter a  
                                            INNER JOIN Tb_HSPR_Room b ON a.RoomID=b.RoomID AND isnull(b.IsDelete,0)=0
                                            INNER JOIN Tb_HSPR_Building c ON b.BuildSNum=c.BuildSNum AND c.CommID={0} AND isnull(c.IsDelete,0)=0
                                            WHERE a.CommID={0} AND isnull(a.DispType,0)=0 AND isnull(a.IsDelete,0)=0 AND IsStatistics=1 AND 
                                            c.BuildSNum IN(SELECT BuildSNum FROM Tb_HSPR_BuildHousekeeper WHERE CommID={0} AND UserCode='{1}')",
                              commId, Global_Var.LoginUserCode);

                    sql += " AND a.Attribution<>'地产' AND datediff(minute, a.IncidentDate, getdate())>2";
                }
                else
                {
                    // 楼栋管家可分派的 IsStatistics 表示统计报表时是否统计该条报事 一般用于派工转协调
                    sql += string.Format(@"SELECT distinct IncidentID,IncidentContent,IncidentPlace,IncidentMode,a.RoomSign,a.RoomName,TypeName,
                                            ReserveDate,Phone,IncidentNum,RegionalPlace
                                            FROM view_HSPR_IncidentAccept_Filter a  
                                            INNER JOIN Tb_HSPR_Room b ON a.RoomID=b.RoomID AND isnull(b.IsDelete,0)=0
                                            INNER JOIN Tb_HSPR_Building c ON b.BuildSNum=c.BuildSNum AND c.CommID={0} AND isnull(c.IsDelete,0)=0
                                            WHERE a.CommID={0} AND isnull(a.DispType,0)=0 AND isnull(a.IsDelete,0)=0 AND IsStatistics=1 AND 
                                            c.BuildSNum IN(SELECT BuildSNum FROM Tb_HSPR_BuildHousekeeper WHERE CommID={0} AND UserCode='{1}')",
                                            commId, Global_Var.LoginUserCode);
                }

                int PageCount;
                int Counts;
                DataTable dt = GetList(out PageCount, out Counts, sql, PageIndex, PageSize, "IncidentNum", 1, "IncidentID",
                    PubConstant.hmWyglConnectionString).Tables[0];

                resultStr = JSONHelper.FromString(dt);
                return resultStr.Insert(resultStr.Length - 1, ",\"PageCount\":" + PageCount);
            }
        }

        /// <summary>
        /// 获取本人可【派】的未分派报事数量
        /// </summary>
        private string GetCanAssignIncidentCount(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            string commId = row["CommID"].ToString();

            if (GetDBServerPath(commId, out string strcon) == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            using (IDbConnection con = new SqlConnection(strcon))
            {
                // 获取具有分派权限的报事类别
                string sql = @"SELECT(SELECT cast(TypeID AS VARCHAR)+','
                                FROM Tb_HSPR_IncidentType WHERE CommID=@CommID AND CorpTypeID IN (
                                  SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeAssignedPost WHERE RoleCode IN(
                                    SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN(
                                      SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode))) FOR XML PATH ('')) AS TypeID";

                // 敏捷
                if (Global_Var.CorpId == "1971")
                {
                    sql = @"SELECT(SELECT cast(TypeCode AS VARCHAR)+','
                                FROM Tb_HSPR_IncidentType WHERE CommID=@CommID AND CorpTypeID IN (
                                  SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeAssignedPost WHERE RoleCode IN(
                                    SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN(
                                      SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode))) FOR XML PATH ('')) AS TypeID";
                }

                dynamic canAssignTypeIDs = con.Query(sql, new { CommID = commId, UserCode = Global_Var.UserCode }).FirstOrDefault();

                sql = "";
                int count1 = 0, count2 = 0;
                if (canAssignTypeIDs != null && canAssignTypeIDs.TypeID != null && canAssignTypeIDs.TypeID.ToString().Trim(',') != "")
                {
                    // 未分派的报事
                    sql = string.Format(@"SELECT count(distinct IncidentID) AS Count FROM Tb_HSPR_IncidentAccept v
                                            WHERE CommID={0} AND isnull(DispType,0)=0 AND isnull(IsDelete,0)=0 AND IsStatistics=1 
                                                  AND (SELECT count(1) as Count FROM (SELECT VALUE FROM dbo.SplitString(v.TypeID,',',1) 
                                            InterSect SELECT VALUE FROM dbo.SplitString('{1}',',',1)) as A)>0",
                                                commId, canAssignTypeIDs.TypeID);

                    // 敏捷排除责任归属为“地产”的
                    if (Global_Var.CorpId == "1971")
                    {
                        // 未分派的报事
                        sql = string.Format(@"SELECT count(distinct IncidentID) AS Count FROM Tb_HSPR_IncidentAccept v
                                            WHERE CommID={0} AND isnull(DispType,0)=0 AND isnull(IsDelete,0)=0 AND IsStatistics=1 
                                                  AND (SELECT count(1) as Count FROM (SELECT VALUE FROM dbo.SplitString(v.BigCorpTypeCode,',',1) 
                                            InterSect SELECT VALUE FROM dbo.SplitString('{1}',',',1)) as A)>0",
                                                    commId, canAssignTypeIDs.TypeID);

                        sql += " AND v.Attribution<>'地产' AND datediff(minute, v.IncidentDate, getdate())>2";
                    }

                    count1 = con.Query<int>(sql).FirstOrDefault();
                }

                // 楼栋管家
                sql = string.Format(@"SELECT count(distinct IncidentID) AS Count FROM Tb_HSPR_IncidentAccept a  
                                        INNER JOIN Tb_HSPR_Room b ON a.RoomID=b.RoomID AND isnull(b.IsDelete,0)=0
                                        INNER JOIN Tb_HSPR_Building c ON b.BuildSNum=c.BuildSNum AND c.CommID=@CommID AND isnull(c.IsDelete,0)=0
                                        WHERE a.CommID=@CommID AND isnull(a.DispType,0)=0 AND isnull(a.IsDelete,0)=0 AND IsStatistics=1 AND 
                                        c.BuildSNum IN(SELECT BuildSNum FROM Tb_HSPR_BuildHousekeeper WHERE CommID=@CommID AND UserCode=@UserCode)");

                // 敏捷排除责任归属为“地产”的
                if (Global_Var.CorpId == "1971")
                {
                    sql += " AND a.Attribution<>'地产' AND datediff(minute, a.IncidentDate, getdate())>2";
                }

                count2 = con.Query<int>(sql, new { CommID = commId, UserCode = Global_Var.UserCode }).FirstOrDefault();

                return JSONHelper.FromString(true, (count1 + count2).ToString());
            }
        }

        /// <summary>
        /// 获取本人可【抢】的未分派报事列表
        /// </summary>
        private string GetCanSnatchIncidentList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            int PageIndex = 1;
            int PageSize = 20;
            if (row.Table.Columns.Contains("PageIndex"))
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            if (row.Table.Columns.Contains("PageSize"))
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            string commId = row["CommID"].ToString();

            if (GetDBServerPath(commId, out string strcon) == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            string resultStr = null;

            using (IDbConnection con = new SqlConnection(strcon))
            {
                // 获取具有抢单权限的报事类别
                string sql = @"SELECT(SELECT cast(TypeID AS VARCHAR)+','
                                FROM Tb_HSPR_IncidentType  WHERE CommID=@CommID AND CorpTypeID IN (
                                  SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeProcessPost WHERE RoleCode IN(
                                    SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN(
                                      SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode))) FOR XML PATH ('')) AS TypeID";

                // 敏捷
                if (Global_Var.CorpId == "1971")
                {
                    sql = @"SELECT(SELECT cast(TypeCode AS VARCHAR)+','
                                FROM Tb_HSPR_IncidentType  WHERE CommID=@CommID AND CorpTypeID IN (
                                  SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeSnatchPost WHERE RoleCode IN(
                                    SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN(
                                      SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode))) FOR XML PATH ('')) AS TypeID";
                }

                dynamic canSnatchTypeIDs = con.Query(sql, new { CommID = commId, UserCode = Global_Var.UserCode }).FirstOrDefault();

                if (canSnatchTypeIDs != null && canSnatchTypeIDs.TypeID != null && canSnatchTypeIDs.TypeID.ToString().Trim(',') != "")
                {
                    // 未分派的报事
                    sql = string.Format(@"SELECT distinct IncidentID,IncidentContent,IncidentPlace,IncidentMode,RoomSign,TypeID,TypeName,
                                            ReserveDate,Phone,IncidentNum,RegionalPlace
                                            FROM view_HSPR_IncidentAccept_Filter v
                                            WHERE CommID={0} AND isnull(DispType,0)=0 AND isnull(IsDelete,0)=0  AND IsStatistics=1
                                                  AND (SELECT count(1) as Count FROM (SELECT VALUE FROM dbo.SplitString(v.TypeID,',',1) 
                                        InterSect SELECT VALUE FROM dbo.SplitString('{1}',',',1)) as A)>0", commId, canSnatchTypeIDs.TypeID);

                    // 敏捷排除责任归属为“地产”的
                    if (Global_Var.CorpId == "1971")
                    {
                        sql = string.Format(@"SELECT distinct IncidentID,IncidentContent,IncidentPlace,IncidentMode,RoomSign,TypeID,TypeName,
                                            ReserveDate,Phone,IncidentNum,RegionalPlace
                                            FROM view_HSPR_IncidentAccept_Filter v
                                            WHERE CommID={0} AND isnull(DispType,0)=0 AND isnull(IsDelete,0)=0  AND IsStatistics=1
                                                  AND (SELECT count(1) as Count FROM (SELECT VALUE FROM dbo.SplitString(v.BigCorpTypeCode,',',1) 
                                        InterSect SELECT VALUE FROM dbo.SplitString('{1}',',',1)) as A)>0", commId, canSnatchTypeIDs.TypeID);

                        sql += " AND v.Attribution<>'地产'";
                    }

                    // 泰禾 排除退单过的
                    if (Global_Var.CorpId == "1970")
                    {
                        sql = string.Format(@"SELECT distinct IncidentID,IncidentContent,IncidentPlace,IncidentMode,RoomSign,TypeID,TypeName,
                                            ReserveDate,Phone,IncidentNum,RegionalPlace
                                            FROM view_HSPR_IncidentAccept_Filter v
                                            WHERE CommID={0} AND isnull(DispType,0)=0 AND isnull(IsDelete,0)=0  AND IsStatistics=1
                                            AND isnull(v.IsSnatchBack,0)=0 
                                                  AND (SELECT count(1) as Count FROM (SELECT VALUE FROM dbo.SplitString(v.TypeID,',',1) 
                                        InterSect SELECT VALUE FROM dbo.SplitString('{1}',',',1)) as A)>0", commId, canSnatchTypeIDs.TypeID);
                    }

                    int PageCount;
                    int Counts;
                    DataTable dt = GetList(out PageCount, out Counts, sql, PageIndex, PageSize, "IncidentNum", 1, "IncidentNum", strcon).Tables[0];

                    dt.Columns.Remove("TypeID");

                    resultStr = JSONHelper.FromString(dt);
                    return resultStr.Insert(resultStr.Length - 1, ",\"PageCount\":" + PageCount);
                }
                resultStr = JSONHelper.FromString(new DataTable());

                return resultStr.Insert(resultStr.Length - 1, ",\"PageCount\":0");
            }
        }

        /// <summary>
        /// 获取本人可【抢】的未分派报事数量
        /// </summary>
        private string GetCanSnatchIncidentCount(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            string commId = row["CommID"].ToString();

            if (GetDBServerPath(commId, out string strcon) == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            using (IDbConnection con = new SqlConnection(strcon))
            {
                string sql;

                // 敏捷
                if (Global_Var.CorpId == "1971")
                {
                    sql = @"SELECT(SELECT cast(TypeCode AS VARCHAR)+','
                                FROM Tb_HSPR_IncidentType  WHERE CommID=@CommID AND CorpTypeID IN (
                                  SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeSnatchPost WHERE RoleCode IN(
                                    SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN(
                                      SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode))) FOR XML PATH ('')) AS TypeID";
                }
                else
                {
                    // 获取具有分派权限的报事类别
                    sql = @"SELECT(SELECT cast(TypeID AS VARCHAR)+','
                                FROM Tb_HSPR_IncidentType  WHERE CommID=@CommID AND CorpTypeID IN (
                                  SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeProcessPost WHERE RoleCode IN(
                                    SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN(
                                      SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode))) FOR XML PATH ('')) AS TypeID";
                }


                dynamic canSnatchTypeIDs = con.Query(sql, new { CommID = commId, UserCode = Global_Var.UserCode }).FirstOrDefault();

                if (canSnatchTypeIDs != null && canSnatchTypeIDs.TypeID != null && canSnatchTypeIDs.TypeID.ToString().Trim(',') != "")
                {
                    // 未分派的报事
                    sql = string.Format(@"SELECT count(distinct IncidentID) AS Count FROM view_HSPR_IncidentAccept_Filter v
                                            WHERE CommID={0} AND isnull(DispType,0)=0 AND isnull(IsDelete,0)=0  AND IsStatistics=1
                                                  AND (SELECT count(1) as Count FROM (SELECT VALUE FROM dbo.SplitString(v.TypeID,',',1) 
                                        InterSect SELECT VALUE FROM dbo.SplitString('{1}',',',1)) as A)>0", commId, canSnatchTypeIDs.TypeID);

                    // 敏捷排除责任归属为“地产”的
                    if (Global_Var.CorpId == "1971")
                    {
                        sql = string.Format(@"SELECT count(distinct IncidentID) AS Count FROM view_HSPR_IncidentAccept_Filter v
                                            WHERE CommID={0} AND isnull(DispType,0)=0 AND isnull(IsDelete,0)=0  AND IsStatistics=1
                                                  AND (SELECT count(1) as Count FROM (SELECT VALUE FROM dbo.SplitString(v.BigCorpTypeCode,',',1) 
                                        InterSect SELECT VALUE FROM dbo.SplitString('{1}',',',1)) as A)>0", commId, canSnatchTypeIDs.TypeID);

                        sql += " AND v.Attribution<>'地产'";
                    }

                    if (Global_Var.CorpId == "1970")
                    {
                        sql = string.Format(@"SELECT count(distinct IncidentID) AS Count FROM view_HSPR_IncidentAccept_Filter v
                                            WHERE CommID={0} AND isnull(DispType,0)=0 AND isnull(IsDelete,0)=0  AND IsStatistics=1
                                            AND isnull(v.IsSnatchBack,0)=0 
                                                  AND (SELECT count(1) as Count FROM (SELECT VALUE FROM dbo.SplitString(v.TypeID,',',1) 
                                        InterSect SELECT VALUE FROM dbo.SplitString('{1}',',',1)) as A)>0", commId, canSnatchTypeIDs.TypeID);
                    }

                    DataTable dt = con.ExecuteReader(sql).ToDataSet().Tables[0];

                    return JSONHelper.FromString(true, dt.Rows[0]["Count"].ToString());
                }
                return JSONHelper.FromString(true, "0");
            }
        }

        /// <summary>
        /// 获取报事预警数量
        /// </summary>
        private string GetIncidentWarningCount(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            string commId = row["CommID"].ToString();

            if (GetDBServerPath(commId, out string strcon) == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            using (IDbConnection con = new SqlConnection(strcon))
            {
                // 获取具有分派权限的报事类别
                string sql = @"SELECT Count(1) AS Count1 FROM Tb_HSPR_IncidentWarningPush WHERE PushUsers LIKE @PushUsers AND IncidentStep=1;
                               SELECT Count(1) AS Count2 FROM Tb_HSPR_IncidentWarningPush WHERE PushUsers LIKE @PushUsers AND IncidentStep=2;
                               SELECT Count(1) AS Count3 FROM Tb_HSPR_IncidentWarningPush WHERE PushUsers LIKE @PushUsers AND IncidentStep=3";

                GridReader gridReader = con.QueryMultiple(sql, new { CommID = commId, PushUsers = string.Format("%{0}%", Global_Var.UserCode) });

                int count1 = gridReader.Read<int>().First();
                int count2 = gridReader.Read<int>().First();
                int count3 = gridReader.Read<int>().First();

                Dictionary<string, int> dict = new Dictionary<string, int>();
                dict.Add("Count1", count1);
                dict.Add("Count2", count2);
                dict.Add("Count3", count3);

                return new ApiResult(true, dict).toJson();
            }
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
            int PageIndex = 1;
            int PageSize = 10;
            if (row.Table.Columns.Contains("PageIndex"))
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            if (row.Table.Columns.Contains("PageSize"))
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            string commId = row["CommID"].ToString();
            int step = AppGlobal.StrToInt(row["Step"].ToString());

            if (GetDBServerPath(commId, out string strcon) == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            using (IDbConnection con = new SqlConnection(strcon))
            {
                string sql = string.Format(@"SELECT DISTINCT a.IncidentID,b.IncidentMode,b.CommName,b.IncidentNum,b.IncidentContent,b.IncidentDate,b.DealLimit 
                                FROM Tb_HSPR_IncidentWarningPush a LEFT JOIN view_HSPR_IncidentAccept_SearchFilter b ON a.IncidentID=b.IncidentID
                                WHERE a.PushUsers LIKE '%{0}%' AND a.IncidentStep={1}", Global_Var.UserCode, step);

                int PageCount;
                int Counts;
                DataTable dt = GetList(out PageCount, out Counts, sql, PageIndex, PageSize, "IncidentDate", 1, "IncidentID", strcon).Tables[0];

                string resultStr = JSONHelper.FromString(dt);
                return resultStr.Insert(resultStr.Length - 1, ",\"PageCount\":" + PageCount);
            }
        }

        /// <summary>
        /// 获取报事服务费用列表
        /// </summary>
        private string GetIncidentServiceList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事id不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var incidentId = AppGlobal.StrToLong(row["IncidentID"].ToString());

            using (IDbConnection conn = new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CHARGE)))
            {
                var list = conn.Query(@"SELECT FeesID,StanName,StanName AS CostName,AccountsDueDate,
                                        DueAmount,isnull(PaidAmount,0) AS PaidAmount,DebtsAmount,isnull(IsCharge,0) AS IsCharge
                                        FROM view_HSPR_Fees_Filter WHERE IncidentID = @IncidentID 
                                        ORDER BY FeesChargeDate DESC", new { IncidentID = incidentId }).ToList();

                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 获取报事特约服务费用项目列表
        /// </summary>
        private string GetIncidentServiceCostItemList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CommID", commId);
                parameters.Add("@CostName", "");
                parameters.Add("@SysCostSign", "B0004");
                parameters.Add("@CostGeneType", 0);
                parameters.Add("@IsHis", 0);
                parameters.Add("@CostType", 0);

                DataSet ds = con.ExecuteReader("Proc_HSPR_CorpCostItem_GetCommNodes", parameters, null, null, CommandType.StoredProcedure).ToDataSet();

                return JSONHelper.FromString(ds.Tables[0]);
            }
        }

        /// <summary>
        /// 该房屋报事特约服务费用项目是否绑定了收费标准
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetIncidentServiceCostStandardSetting(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "业主编号不能为空");
            }

            if (!row.Table.Columns.Contains("CostID") || string.IsNullOrEmpty(row["CostID"].ToString()))
            {
                return JSONHelper.FromString(false, "费项编号不能为空");
            }

            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var custId = AppGlobal.StrToLong(row["CustID"].ToString());
            var costId = AppGlobal.StrToLong(row["CostID"].ToString());
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());

            using (IDbConnection con = new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CHARGE)))
            {
                // 是否为此客户该房间单独设置了费用标准
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@SQLEx", string.Format(" and CommID={0} and CustID={1} and CostID={2} and RoomID={3}", commId, custId, costId, roomId));

                DataSet ds = con.ExecuteReader("Proc_HSPR_CostStanSetting_Filter", parameters, null, null, CommandType.StoredProcedure).ToDataSet();

                return JSONHelper.FromString(ds.Tables[0]);
            }
        }

        /// <summary>
        /// 该费用项目是否仅仅只有一种标准
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetIncidentServiceCostItemOnlyOneStandard(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "业主编号不能为空");
            }

            if (!row.Table.Columns.Contains("CostID") || string.IsNullOrEmpty(row["CostID"].ToString()))
            {
                return JSONHelper.FromString(false, "费项编号不能为空");
            }

            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var custId = AppGlobal.StrToLong(row["CustID"].ToString());
            var costId = AppGlobal.StrToLong(row["CostID"].ToString());
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());

            using (IDbConnection con = new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CHARGE)))
            {
                // 是否为此客户该房间单独设置了费用标准
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@SQLEx", string.Format(" and CommID={0} and CustID={1} and CostID={2} and RoomID={3}", commId, custId, costId, roomId));

                DataSet ds = con.ExecuteReader("Proc_HSPR_CostStandard_Filter", parameters, null, null, CommandType.StoredProcedure).ToDataSet();

                return JSONHelper.FromString(ds.Tables[0]);
            }
        }

        /// <summary>
        /// 获取报事特约服务费用项目收费标准列表
        /// </summary>
        private string GetIncidentServiceCostItemStandardList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            if (!row.Table.Columns.Contains("CostID") || string.IsNullOrEmpty(row["CostID"].ToString()))
            {
                return JSONHelper.FromString(false, "费项编号不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var costId = AppGlobal.StrToLong(row["CostID"].ToString());

            string sql = string.Format("SELECT * FROM view_HSPR_CostStandard_Filter WHERE CommID={0} and CostID={1} AND isnull(IsDelete,0) = 0  AND (datediff(day, isnull(StanStartDate, '1900-1-1'), getdate()) >= 0 AND datediff(day, isnull(StanEndDate, '2900-12-31'), getdate()) < 0) ORDER BY StanSign ASC ", commId, costId);

            using (IDbConnection con = new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CHARGE)))
            {
                DataSet ds = con.ExecuteReader(sql).ToDataSet();

                return JSONHelper.FromString(ds.Tables[0]);
            }
        }

        /// <summary>
        /// 获取报事服务费用开始日期和费用结束日期
        /// </summary>
        private string CalcIncidentServiceCostBeginDateAndEndDate(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "业主编号不能为空");
            }

            if (!row.Table.Columns.Contains("CostID") || string.IsNullOrEmpty(row["CostID"].ToString()))
            {
                return JSONHelper.FromString(false, "费项编号不能为空");
            }

            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }

            if (!row.Table.Columns.Contains("ChargeCycle"))
            {
                return JSONHelper.FromString(false, "缺少ChargeCycle参数");
            }

            if (!row.Table.Columns.Contains("StanFormula"))
            {
                return JSONHelper.FromString(false, "缺少StanFormula参数");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var custId = AppGlobal.StrToLong(row["CustID"].ToString());
            var costId = AppGlobal.StrToLong(row["CostID"].ToString());
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());

            int chargeCycle = string.IsNullOrEmpty(row["ChargeCycle"].ToString()) ? 0 : AppGlobal.StrToInt(row["ChargeCycle"].ToString());
            string stanFormula = row["StanFormula"].ToString();

            // 计算费用开始日期、结束日期
            DateTime? feesBeginDate = null;
            DateTime? feesEndDate = null;

            // 非实际发生额
            if (stanFormula != "8")
            {
                using (IDbConnection con = new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CHARGE)))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@CommID", commId);
                    parameters.Add("@CustID", custId);
                    parameters.Add("@RoomID", roomId);
                    parameters.Add("@CostID", costId);
                    parameters.Add("@HandID", 0);

                    DataSet ds = con.ExecuteReader("Proc_HSPR_Fees_CalcBeginDateFilter", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
                    DataTable table = ds.Tables[0];

                    if (table.Rows.Count > 0)
                    {
                        DataRow DRow = table.Rows[0];
                        string calcBeginDate = AppGlobal.StrDateToDate(DRow["CalcBeginDate"].ToString());

                        if (calcBeginDate != "")
                        {
                            if (chargeCycle == 0)
                            {
                                chargeCycle = 1;
                            }

                            feesBeginDate = AppGlobal.StrToDate(calcBeginDate);
                            feesEndDate = feesBeginDate?.AddMonths(chargeCycle).AddDays(-1);
                        }
                    }
                }
            }
            else
            {
                // 其他计算方式时默认为当月1号
                DateTime now = DateTime.Now;
                feesBeginDate = AppGlobal.CheckDateTime(now.Year, now.Month, 1, now.Hour, now.Minute, now.Second, now.Millisecond);
                feesEndDate = AppGlobal.CheckDateTime(now.Year, now.Month, 1, now.Hour, now.Minute, now.Second, now.Millisecond);
            }

            DataTable dt = new DataTable();
            DataColumn column1 = new DataColumn("FeesBeginDate", typeof(string));
            DataColumn column2 = new DataColumn("FeesEndDate", typeof(string));
            dt.Columns.Add(column1);
            dt.Columns.Add(column2);
            DataRow dr = dt.NewRow();
            dr["FeesBeginDate"] = (feesBeginDate == null ? "" : feesBeginDate?.ToString("yyyy-MM-dd"));
            dr["FeesEndDate"] = (feesEndDate == null ? "" : feesEndDate?.ToString("yyyy-MM-dd"));

            return JSONHelper.FromString(dr);
        }

        /// <summary>
        /// 报事服务计算收费金额
        /// </summary>
        private string IncidentServiceCalcAmount(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "业主编号不能为空");
            }

            if (!row.Table.Columns.Contains("CostID") || string.IsNullOrEmpty(row["CostID"].ToString()))
            {
                return JSONHelper.FromString(false, "费项编号不能为空");
            }

            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }

            if (!row.Table.Columns.Contains("StanID") || string.IsNullOrEmpty(row["StanID"].ToString()))
            {
                return JSONHelper.FromString(false, "收费标准编码不能为空");
            }

            int commId = AppGlobal.StrToInt(row["CommID"].ToString());

            long custId = AppGlobal.StrToLong(row["CustID"].ToString());
            long costId = AppGlobal.StrToLong(row["CostID"].ToString());
            long roomId = AppGlobal.StrToLong(row["RoomID"].ToString());
            long stanId = AppGlobal.StrToLong(row["StanID"].ToString());

            // 费用开始、结束日期
            string feesBeginDate = "";
            string feesEndDate = "";
            if (row.Table.Columns.Contains("FeesBeginDate"))
            {
                feesBeginDate = row["FeesBeginDate"].ToString();
            }

            if (row.Table.Columns.Contains("FeesEndDate"))
            {
                feesEndDate = row["FeesEndDate"].ToString();
            }

            // 数量
            int num1 = 0, num2 = 0;
            if (row.Table.Columns.Contains("Num1"))
            {
                num1 = AppGlobal.StrToInt(row["Num1"].ToString());
            }

            if (row.Table.Columns.Contains("Num2"))
            {
                num2 = AppGlobal.StrToInt(row["Num2"].ToString());
            }

            // 按自然月分解
            //int isSplit = 0;
            //if (row.Table.Columns.Contains("IsSplit"))
            //{
            //    isSplit = AppGlobal.StrToInt(row["IsSplit"].ToString());
            //}

            if ((commId != 0) && (costId != 0) && (custId != 0) && (stanId != 0))
            {
                using (IDbConnection con = new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CHARGE)))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@CommID", commId);
                    parameters.Add("@CustID", custId);
                    parameters.Add("@RoomID", roomId);
                    parameters.Add("@CostID", costId);
                    parameters.Add("@HandID", 0);
                    parameters.Add("@StanID", stanId);
                    parameters.Add("@FeesStateDate", feesBeginDate);
                    parameters.Add("@FeesEndDate", feesEndDate);
                    parameters.Add("@Amount", num1);
                    parameters.Add("@Amount2", num2);

                    DataSet ds = con.ExecuteReader("Proc_HSPR_Fees_CalcAmount", parameters, null, null, CommandType.StoredProcedure).ToDataSet();

                    DataTable dataTable = ds.Tables[0];

                    if (dataTable.Rows.Count > 0)
                    {
                        if ((dataTable.Rows[0]["ErrorMsg"] != null && dataTable.Rows[0]["ErrorMsg"].ToString().Trim(' ') != "") ||
                            dataTable.Rows[0]["DueAmount"] == null)
                        {
                            return JSONHelper.FromString(false, dataTable.Rows[0]["ErrorMsg"].ToString());
                        }
                        return JSONHelper.FromString(ds.Tables[0]);
                    }

                    return JSONHelper.FromString(false, "费用标准不存在");
                }
            }
            return JSONHelper.FromString(false, "参数错误");
        }

        /// <summary>
        /// 保存报事服务
        /// </summary>
        private string IncidentServiceSave(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "业主编号不能为空");
            }

            if (!row.Table.Columns.Contains("CostID") || string.IsNullOrEmpty(row["CostID"].ToString()))
            {
                return JSONHelper.FromString(false, "费项编号不能为空");
            }

            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }

            if (!row.Table.Columns.Contains("StanID") || string.IsNullOrEmpty(row["StanID"].ToString()))
            {
                return JSONHelper.FromString(false, "收费标准编码不能为空");
            }

            if (!row.Table.Columns.Contains("CorpStanID") || string.IsNullOrEmpty(row["CorpStanID"].ToString()))
            {
                return JSONHelper.FromString(false, "CorpStanID不能为空");
            }

            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编号不能为空");
            }

            if (!row.Table.Columns.Contains("FeesDueDate") || string.IsNullOrEmpty(row["FeesDueDate"].ToString()))
            {
                return JSONHelper.FromString(false, "费用日期不能为空");
            }

            if (!row.Table.Columns.Contains("AccountsDueDate") || string.IsNullOrEmpty(row["AccountsDueDate"].ToString()))
            {
                return JSONHelper.FromString(false, "应收日期不能为空");
            }

            if (!row.Table.Columns.Contains("FeesBeginDate") || string.IsNullOrEmpty(row["FeesBeginDate"].ToString()))
            {
                return JSONHelper.FromString(false, "费用开始日期不能为空");
            }

            if (!row.Table.Columns.Contains("FeesEndDate") || string.IsNullOrEmpty(row["FeesEndDate"].ToString()))
            {
                return JSONHelper.FromString(false, "费用结束日期不能为空");
            }

            if (row.Table.Columns.Contains("FeesBeginDate").ToString().CompareTo(row.Table.Columns.Contains("FeesEndDate").ToString()) > 0)
            {
                return JSONHelper.FromString(false, "费用结束日期不能小于开始日期");
            }

            int commId = AppGlobal.StrToInt(row["CommID"].ToString());

            long custId = AppGlobal.StrToLong(row["CustID"].ToString());
            long costId = AppGlobal.StrToLong(row["CostID"].ToString());
            long roomId = AppGlobal.StrToLong(row["RoomID"].ToString());
            long stanId = AppGlobal.StrToLong(row["StanID"].ToString());
            long corpStanId = AppGlobal.StrToLong(row["CorpStanID"].ToString());

            // 报事编号
            long incidentId = AppGlobal.StrToLong(row["IncidentID"].ToString());

            // 费用日期、应收日期、费用开始日期、结束日期日期
            string feesDueDate = row["FeesDueDate"].ToString();
            string accountsDueDate = row["AccountsDueDate"].ToString();
            string feesBeginDate = row["FeesBeginDate"].ToString(); ;
            string feesEndDate = row["FeesEndDate"].ToString();

            // 费用金额
            decimal dueAmount = 0.0m;
            if (row.Table.Columns.Contains("DueAmount"))
            {
                dueAmount = AppGlobal.StrToDec(row["DueAmount"].ToString());
            }

            // 费用备注
            string feesMemo = "";
            if (row.Table.Columns.Contains("FeesMemo"))
            {
                feesMemo = row["FeesMemo"].ToString();
            }

            // 数量
            int num1 = 0, num2 = 0;
            if (row.Table.Columns.Contains("Num1"))
            {
                num1 = AppGlobal.StrToInt(row["Num1"].ToString());
            }

            if (row.Table.Columns.Contains("Num2"))
            {
                num2 = AppGlobal.StrToInt(row["Num2"].ToString());
            }

            // 按自然月分解
            int isSplit = 0;
            if (row.Table.Columns.Contains("IsSplit"))
            {
                isSplit = AppGlobal.StrToInt(row["IsSplit"].ToString());
            }

            string result = "";
            if (isSplit == 1 && feesBeginDate != "" && feesEndDate != "")
            {
                //计算插入费用
                result = IncidentServiceSaveWithSplit(commId, custId, roomId, 0, costId, stanId, corpStanId, feesDueDate, accountsDueDate, feesBeginDate, feesEndDate, num1, num2, dueAmount, incidentId, feesMemo, Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CHARGE));
            }
            else
            {
                result = InsertFee(commId, custId, roomId, 0, costId, stanId, corpStanId, feesDueDate, accountsDueDate, feesBeginDate, feesEndDate, num1, num2, dueAmount, incidentId, feesMemo, Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CHARGE));
            }
            return result;
        }

        // 按自然月分解时插入费用
        private string IncidentServiceSaveWithSplit(int commId, long custId, long roomId, long handId, long costId, long stanId, long corpStanId, string feesDueDate, string accountsDueDate, string feesBeginDate, string feesEndDate, int num1, int num2, decimal dueAmount, long incidentId, string feesMemo, string strcon)
        {
            int RoundingNum = 2;

            int StanFormula = 0;
            decimal PeriodNum = 1.0M;

            string strErrorMsg = "";

            decimal iCostAmount = 0;
            decimal iPerMonthAmount = 0;

            DateTime CaluDueDate = new DateTime(1900, 1, 1);
            DateTime CaluBeginDate = new DateTime(1900, 1, 1);
            DateTime CaluFirstBeginDate = new DateTime(1900, 1, 1);
            DateTime CaluEndDate = new DateTime(1900, 1, 1);
            DateTime CaluDivDate = new DateTime(1900, 1, 1);
            DateTime CaluNextDate = new DateTime(1900, 1, 1);
            DateTime CaluPeriodFinalDate = new DateTime(1900, 1, 1);
            DateTime CaluFeesDueDate = new DateTime(1900, 1, 1);
            DateTime CaluTmpDate = new DateTime(1900, 1, 1);
            DateTime CaluTmpEndDate = new DateTime(1900, 1, 1);

            if ((custId == 0) || (costId == 0) || (stanId == 0))
            {
                return JSONHelper.FromString(false, "未设收费标准");
            }

            //using (IDbConnection con = new SqlConnection(strcon))
            {
                // 是否为此客户该房间单独设置了费用标准
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@SQLEx", string.Format(" and CommID={0} and CostID={1} and StanID={2} and IsDelete = 0", commId, costId, stanId));

                IDbConnection con = new SqlConnection(strcon);
                DataSet ds1 = con.ExecuteReader("Proc_HSPR_CostStandard_Filter", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
                DataTable dt1 = ds1.Tables[0];
                con.Close();

                if (dt1.Rows.Count > 0)
                {
                    StanFormula = AppGlobal.StrToInt(dt1.Rows[0]["StanFormula"].ToString());
                    RoundingNum = AppGlobal.StrToInt(dt1.Rows[0]["RoundingNum"].ToString());
                }

                #region 按每月、季计算
                //按每月、季计算
                if ((StanFormula == 1) || (StanFormula == 2) || (StanFormula == 3) || (StanFormula == 5) || (@StanFormula == 9) || (StanFormula == 10))
                {
                    PeriodNum = 1.0M;
                }
                else
                {
                    if ((StanFormula == 20) || (StanFormula == 21) || (StanFormula == 22) || (StanFormula == 23) || (StanFormula == 24))
                    {
                        PeriodNum = 3.0M;
                    }
                }
                #endregion

                decimal PerMonthDat = 0;
                int TheDay = 0;

                int CaluFeesCount = 0;
                decimal iDueAmount = 0;
                decimal AllDueAmount = (decimal)dueAmount;
                decimal iHasDueAmount = 0;
                decimal AllDays = Convert.ToDecimal(AppGlobal.CompDateDay(feesBeginDate, feesEndDate));
                int AllMonths = AppGlobal.CompDateMonth(feesBeginDate, feesEndDate);

                int iMonNum = 0;
                structFees[] FeesIns = new structFees[AllMonths + 1];

                if (AllDays < 0)
                {
                    return JSONHelper.FromString(false, "计费时间间隔错误");
                }

                if ((feesBeginDate != "") && (feesEndDate != "") && (feesDueDate != ""))
                {
                    #region 初始化
                    CaluDueDate = AppGlobal.StrToDate(feesDueDate);

                    CaluFeesDueDate = AppGlobal.StrToDate(feesBeginDate);

                    //初始化计算开始日期和结束日期
                    CaluBeginDate = AppGlobal.StrToDate(feesBeginDate);
                    CaluEndDate = AppGlobal.StrToDate(feesEndDate).AddDays(1);

                    //下一个对月
                    CaluPeriodFinalDate = CaluBeginDate.AddMonths(1);

                    //下一个开始时间
                    CaluNextDate = CaluBeginDate;
                    #endregion

                    //下一个缴费日期没有超过结束日期
                    if (AppGlobal.CompDateDay(CaluNextDate, CaluEndDate) > 0)
                    {
                        #region 前部
                        //自然月底+1
                        CaluDivDate = GetDivDate(CaluBeginDate, 1);

                        //月前部
                        //开始日期
                        CaluTmpDate = CaluBeginDate;

                        //结束日期
                        if (AppGlobal.CompDateDay(CaluDivDate, CaluEndDate) > 0)
                        {
                            //下一个缴费日期
                            CaluNextDate = CaluDivDate;
                            CaluNextDate = CaluNextDate.AddDays(1);
                        }
                        else
                        {
                            //下一个缴费日期
                            CaluNextDate = CaluEndDate;
                        }

                        //本月天数
                        PerMonthDat = Convert.ToDecimal(AppGlobal.GetMaxDay(CaluFeesDueDate));

                        //每月分别计算标准
                        CaluTmpEndDate = CaluNextDate.AddDays(-1);

                        parameters = new DynamicParameters();
                        parameters.Add("@CommID", commId);
                        parameters.Add("@CustID", custId);
                        parameters.Add("@RoomID", roomId);
                        parameters.Add("@CostID", costId);
                        parameters.Add("@HandID", 0);
                        parameters.Add("@StanID", stanId);
                        parameters.Add("@FeesStateDate", feesBeginDate);
                        parameters.Add("@FeesEndDate", feesEndDate);
                        parameters.Add("@Amount", num1);
                        parameters.Add("@Amount2", num2);
                        parameters.Add("@DueAmount", iCostAmount, DbType.Decimal, ParameterDirection.Output);
                        parameters.Add("@ErrorMsg", strErrorMsg, DbType.String, ParameterDirection.Output);

                        con = new SqlConnection(strcon);
                        con.Execute("Proc_HSPR_Fees_CalcStanAmount", parameters, null, null, CommandType.StoredProcedure);
                        con.Close();

                        iCostAmount = parameters.Get<decimal>("DueAmount");
                        strErrorMsg = parameters.Get<String>("ErrorMsg");

                        iPerMonthAmount = (iCostAmount / PeriodNum);

                        if (AppGlobal.CompDateDay(CaluTmpDate, CaluNextDate) > 0)
                        {
                            TheDay = AppGlobal.CompDateDay(CaluTmpDate, CaluNextDate);
                            decimal iThisAmount = AppGlobal.Round((TheDay / PerMonthDat) * iPerMonthAmount, RoundingNum);//C#采用的是“四舍六入五成双 Math.Round(2.5,0) = 2;2.5应该等于3才对！ 

                            //不能超过
                            if (iThisAmount > AllDueAmount)
                            {
                                iThisAmount = AllDueAmount;
                            }
                            iDueAmount = iDueAmount + iThisAmount;
                        }

                        #region 插入费用
                        feesDueDate = CaluDueDate.ToString();
                        feesBeginDate = CaluTmpDate.ToString();
                        feesEndDate = CaluNextDate.AddDays(-1).ToString();

                        iHasDueAmount = iHasDueAmount + iDueAmount;
                        //插入费用
                        feesDueDate = CalcFeesDueDate(feesDueDate, feesBeginDate).ToString();
                        feesDueDate = AppGlobal.StrToDate(feesDueDate).AddSeconds(CaluFeesCount).ToString();

                        FeesIns[iMonNum].FeesDueDate = feesDueDate;
                        FeesIns[iMonNum].FeesStateDate = feesBeginDate;
                        FeesIns[iMonNum].FeesEndDate = feesEndDate;
                        FeesIns[iMonNum].DueAmount = AppGlobal.Round(iDueAmount, RoundingNum);
                        iMonNum++;

                        //InsertFee(feesDueDate,feesBeginDate,feesEndDate,iDueAmount);
                        iDueAmount = 0;

                        CaluFeesCount++;
                        #endregion

                        #endregion
                    }

                    if (AppGlobal.CompDateDay(CaluNextDate, CaluEndDate) > 0)
                    {
                        #region 中部
                        for (int k = 0; k < (AllMonths - 1); k++)
                        {
                            //月后部
                            //开始日期
                            CaluTmpDate = CaluNextDate;

                            //结束日期
                            CaluNextDate = CaluNextDate.AddMonths(1);

                            if (AppGlobal.CompDateDay(CaluNextDate, CaluEndDate) <= 0)
                            {
                                CaluNextDate = CaluNextDate.AddMonths(-1);
                                break;
                            }

                            //每月分别计算标准
                            CaluTmpEndDate = CaluNextDate.AddDays(-1);

                            con = new SqlConnection(strcon);
                            con.ExecuteReader("Proc_HSPR_Fees_CalcStanAmount", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
                            con.Close();

                            iCostAmount = parameters.Get<decimal>("DueAmount");
                            strErrorMsg = parameters.Get<String>("ErrorMsg");

                            iPerMonthAmount = (iCostAmount / PeriodNum);

                            iDueAmount = iDueAmount + iPerMonthAmount;

                            #region 插入费用
                            feesDueDate = CaluDueDate.ToString();
                            feesBeginDate = CaluTmpDate.ToString();
                            feesEndDate = CaluNextDate.AddDays(-1).ToString();

                            iHasDueAmount = iHasDueAmount + iDueAmount;
                            //插入费用
                            feesDueDate = CalcFeesDueDate(feesDueDate, feesBeginDate).ToString();
                            feesDueDate = AppGlobal.StrToDate(feesDueDate).AddSeconds(CaluFeesCount).ToString();

                            FeesIns[iMonNum].FeesDueDate = feesDueDate;
                            FeesIns[iMonNum].FeesStateDate = feesBeginDate;
                            FeesIns[iMonNum].FeesEndDate = feesEndDate;
                            FeesIns[iMonNum].DueAmount = AppGlobal.Round(iDueAmount, RoundingNum);
                            iMonNum++;

                            //InsertFee(feesDueDate,feesBeginDate,feesEndDate,iDueAmount);
                            iDueAmount = 0;

                            CaluFeesCount++;
                            #endregion
                        }

                        #endregion
                    }

                    if (AppGlobal.CompDateDay(CaluNextDate, CaluEndDate) > 0)
                    {
                        #region 尾部
                        CaluTmpDate = CaluNextDate;
                        CaluNextDate = CaluEndDate;

                        //每月分别计算标准
                        CaluTmpEndDate = CaluNextDate.AddDays(-1);

                        con = new SqlConnection(strcon);
                        con.ExecuteReader("Proc_HSPR_Fees_CalcStanAmount", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
                        con.Close();

                        iCostAmount = parameters.Get<decimal>("DueAmount");
                        strErrorMsg = parameters.Get<String>("ErrorMsg");

                        iPerMonthAmount = (iCostAmount / PeriodNum);

                        //整月
                        if (AppGlobal.CompDateDay(CaluTmpDate.AddMonths(1), CaluNextDate) == 0)
                        {
                            iDueAmount = iDueAmount + iPerMonthAmount;

                            iHasDueAmount = iHasDueAmount - FeesIns[0].DueAmount;
                            iHasDueAmount = iHasDueAmount + iDueAmount;

                            if (iMonNum > 0)
                            {
                                FeesIns[0].DueAmount = AllDueAmount - iHasDueAmount;
                            }

                        }
                        else
                        {
                            iDueAmount = AllDueAmount - iHasDueAmount;
                        }

                        #region 插入费用
                        feesDueDate = CaluDueDate.ToString();
                        feesBeginDate = CaluTmpDate.ToString();
                        feesEndDate = CaluNextDate.AddDays(-1).ToString();

                        iHasDueAmount = iHasDueAmount + iDueAmount;
                        //插入费用
                        feesDueDate = CalcFeesDueDate(feesDueDate, feesBeginDate).ToString();
                        feesDueDate = AppGlobal.StrToDate(feesDueDate).AddSeconds(CaluFeesCount).ToString();

                        FeesIns[iMonNum].FeesDueDate = feesDueDate;
                        FeesIns[iMonNum].FeesStateDate = feesBeginDate;
                        FeesIns[iMonNum].FeesEndDate = feesEndDate;
                        FeesIns[iMonNum].DueAmount = AppGlobal.Round(iDueAmount, RoundingNum);
                        iMonNum++;

                        //InsertFee(feesDueDate,feesBeginDate,feesEndDate,iDueAmount);
                        iDueAmount = 0;

                        CaluFeesCount++;
                        #endregion

                        #endregion
                    }

                    #region 批量插入费用

                    for (int i = 0; i < FeesIns.Length; i++)
                    {
                        if (FeesIns[i].DueAmount > 0)
                        {
                            InsertFee(commId, custId, roomId, handId, costId, stanId, corpStanId, feesDueDate, accountsDueDate, feesBeginDate, feesEndDate, num1, num2, dueAmount, incidentId, feesMemo, strcon);
                        }
                    }
                    #endregion

                    //				//更新计费开始日期
                    //				UpdateCalcBeginDate(CaluEndDate.AddDays(-1).ToString());

                }
            }

            return JSONHelper.FromString(true, "插入成功");
        }

        private string InsertFee(int commId, long custId, long roomId, long handId, long costId, long stanId, long corpStanId, string feesDueDate, string accountsDueDate, string feesBeginDate, string feesEndDate, int num1, int num2, decimal dueAmount, long incidentId, string feesMemo, string strcon)
        {
            if (dueAmount <= 0)
            {
                return JSONHelper.FromString(false, "费用金额不能小于0"); ;
            }
            //**填充数据实例对象
            Tb_HSPR_Fees Item = new Tb_HSPR_Fees();
            Item.FeesID = 0;
            Item.CommID = commId;

            Item.CostID = costId;
            Item.CustID = custId;
            Item.RoomID = roomId;

            Item.FeesDueDate = AppGlobal.StrToDate(feesDueDate);
            Item.FeesStateDate = AppGlobal.StrToDate(feesBeginDate);
            Item.FeesEndDate = AppGlobal.StrToDate(feesEndDate);

            Item.DueAmount = (decimal)dueAmount;

            Item.AccountFlag = TypeRule.TWAccountType.Sporadic;
            Item.FeesMemo = feesMemo;

            Item.CorpStanID = corpStanId;
            Item.StanID = stanId;

            Item.CalcAmount = num1;
            Item.CalcAmount2 = num2;
            Item.HandID = 0;
            Item.IncidentID = incidentId;
            Item.LeaseContID = 0;
            Item.ContID = 0;

            Item.UserCode = Global_Var.UserCode;
            Item.AccountsDueDate = AppGlobal.StrToDate(accountsDueDate);
            Item.OrderCode = "";

            //using (IDbConnection con = new SqlConnection(strcon))
            {
                // 是否为此客户该房间单独设置了费用标准
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CommID", commId);
                parameters.Add("@SQLEx", "");

                IDbConnection con = new SqlConnection(strcon);
                DataSet ds1 = con.ExecuteReader("Proc_HSPR_Fees_GetMaxNum", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
                long nextId = Convert.ToInt64(ds1.Tables[0].Rows[0][0].ToString());
                con.Close();

                Item.FeesID = nextId;

                parameters = new DynamicParameters();
                parameters.Add("@FeesID", Item.FeesID);
                parameters.Add("@CommID", Item.CommID);
                parameters.Add("@CostID", Item.CostID);
                parameters.Add("@CustID", Item.CustID);
                parameters.Add("@RoomID", Item.RoomID);
                parameters.Add("@FeesDueDate", Item.FeesDueDate);
                parameters.Add("@FeesStateDate", Item.FeesStateDate);
                parameters.Add("@FeesEndDate", Item.FeesEndDate);
                parameters.Add("@DueAmount", Item.DueAmount);
                parameters.Add("@DebtsAmount", Item.DebtsAmount);
                parameters.Add("@WaivAmount", Item.WaivAmount);
                parameters.Add("@PrecAmount", Item.PrecAmount);
                parameters.Add("@PaidAmount", Item.PaidAmount);
                parameters.Add("@RefundAmount", Item.RefundAmount);
                parameters.Add("@IsAudit", Item.IsAudit);
                parameters.Add("@FeesMemo", Item.FeesMemo);
                parameters.Add("@AccountFlag", Item.AccountFlag);
                parameters.Add("@IsBank", Item.IsBank);
                parameters.Add("@IsCharge", Item.IsCharge);
                parameters.Add("@CorpStanID", Item.CorpStanID);
                parameters.Add("@StanID", Item.StanID);
                parameters.Add("@AccountsDueDate", Item.AccountsDueDate);
                parameters.Add("@HandID", Item.HandID);
                parameters.Add("@MeterSign", Item.MeterSign);
                parameters.Add("@CalcAmount", Item.CalcAmount);
                parameters.Add("@CalcAmount2", Item.CalcAmount2);
                parameters.Add("@IncidentID", Item.IncidentID);
                parameters.Add("@LeaseContID", Item.LeaseContID);
                parameters.Add("@ContID", Item.ContID);
                parameters.Add("@PMeterID", Item.PMeterID);
                parameters.Add("@StanMemo", Item.StanMemo);
                parameters.Add("@UserCode", Item.UserCode);
                parameters.Add("@OrderCode", Item.OrderCode);
                parameters.Add("@IsPast", 0);
                parameters.Add("@AssumeCustID", 0);
                parameters.Add("@ContSetID", 0);

                con = new SqlConnection(strcon);
                int i = con.Execute("Proc_HSPR_Fees_Insert", parameters, null, null, CommandType.StoredProcedure);
                con.Close();


                return JSONHelper.FromString(i > 0, i > 0 ? "插入成功" : "插入失败");
            }
        }

        // 按一个周期的自然月底
        private DateTime GetDivDate(DateTime Date1, int PayPeriod)
        {
            int PerYearMonth = 12; //一年12月			
            int i = 0;
            int iYear = 0;
            int iMonth = 0;
            string strDivDate = "";

            while (i < PerYearMonth / PayPeriod)
            {
                iMonth = iMonth + PayPeriod;
                iYear = Date1.Year;

                strDivDate = AppGlobal.CheckDate(iYear.ToString(), iMonth.ToString(), "31");

                if (AppGlobal.CompDateDay(Date1.ToString(), strDivDate) >= 0)
                {
                    break;
                }

                i++;
            }

            return AppGlobal.StrToDate(strDivDate);
        }

        // 计算费用日期
        private DateTime CalcFeesDueDate(string feesDueDate, string feesBeginDate)
        {
            DateTime dFeesDueDate = AppGlobal.StrToDate(feesDueDate);
            DateTime dFeesStateDate = AppGlobal.StrToDate(feesBeginDate);
            DateTime dNow = DateTime.Now;


            //为本月1日					
            //DateTime dThisMonthFirst = AppGlobal.CheckDateTime(dNow.Year,dNow.Month,1,dNow.Hour,dNow.Minute,dNow.Second,dNow.Millisecond);
            //如果开始时间和费用时间相等
            DateTime dThisMonthSecond = AppGlobal.CheckDateTime(dNow.Year, dNow.Month, dFeesDueDate.Day, dNow.Hour, dNow.Minute, dNow.Second, dNow.Millisecond);

            if ((dFeesStateDate.Year * 100 + dFeesStateDate.Month) > (dNow.Year * 100 + dNow.Month))
            {
                dFeesDueDate = dFeesStateDate;
            }
            else
            {
                dFeesDueDate = dThisMonthSecond;
            }


            //if ((dFeesStateDate.Year * 100 + dFeesStateDate.Month) < (dNow.Year * 100 + dNow.Month))
            //{
            //    dFeesDueDate = dThisMonthFirst;
            //}
            //else
            //{
            //    dFeesDueDate = dThisMonthSecond;
            //}

            return dFeesDueDate;
        }

        /// <summary>
        /// 删除报事费用
        /// </summary>
        private string DeleteIncidentFees(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编码不能为空");
            }
            if (!row.Table.Columns.Contains("FeesID") || string.IsNullOrEmpty(row["FeesID"].ToString()))
            {
                return JSONHelper.FromString(false, "费用id不能为空");
            }

            string feesID = row["FeesID"].ToString();
            string incidentID = row["IncidentID"].ToString();
            var commId = AppGlobal.StrToInt(row["IncidentID"].ToString().Substring(0, 6));

            using (IDbConnection conn = new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CHARGE)))
            {
                #region 检查费用是否已收
                int IsCharge = conn.QueryFirstOrDefault<int>("SELECT IsCharge FROM Tb_HSPR_Fees WHERE FeesID = @FeesID AND IncidentID = @IncidentID", new { FeesID = feesID, IncidentID = incidentID });
                if (IsCharge == 1)
                {
                    return JSONHelper.FromString(false, "操作失败,该费用已收取");
                }
                #endregion
                try
                {
                    conn.Execute(@"UPDATE Tb_HSPR_IncidentAccept SET dueamount=(dueamount-(SELECT isnull((SELECT isnull(dueamount,0) AS amount FROM Tb_HSPR_Fees WHERE FeesID=@FeesID AND incidentid=@IncidentID),0))) WHERE IncidentID=@IncidentID;",
                    new { FeesID = feesID, IncidentID = incidentID });
                }
                catch (Exception)
                {

                }

                conn.Execute(@"DELETE FROM Tb_HSPR_Fees WHERE FeesID=@FeesID AND IncidentID=@IncidentID",
                    new { FeesID = feesID, IncidentID = incidentID });
            }
            return JSONHelper.FromString(true, "操作成功");
        }

        /// <summary>
        /// 保存报事实签人图片
        /// </summary>
        private string SaveSignatoryImg(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编码不能为空");
            }
            if (!row.Table.Columns.Contains("Img") || string.IsNullOrEmpty(row["Img"].ToString()))
            {
                return JSONHelper.FromString(false, "图片路径能为空");
            }

            string img = row["Img"].ToString();
            string incidentID = row["IncidentID"].ToString();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"UPDATE Tb_HSPR_IncidentAccept SET SignatoryImg=@Img WHERE IncidentID=@IncidentID";

                int i = conn.Execute(sql, new { IncidentID = incidentID, Img = img });
                if (i == 1)
                    return JSONHelper.FromString(true, "操作成功");
                return JSONHelper.FromString(false, "操作失败");
            }
        }

    }
}
