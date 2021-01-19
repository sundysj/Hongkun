using Common;
using Dapper;
using Microsoft.SqlServer.Server;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using static Dapper.SqlMapper;

namespace Business
{
    public partial class PMSHouseInspectionManage : PubInfo
    {
        class TaskRoomModel
        {
            public string Id { get; set; }
            public int IsComplete { get; set; }
            public string CompletePid { get; set; }
            public string CompleteDate { get; set; }
            public int IsQualified { get; set; }
            public List<File> InspectionDrawing { get; set; }
            public List<TaskStandardModel> Standards { get; set; }
        }

        class TaskStandardModel
        {
            public string Id { get; set; }
            public int IsQualified { get; set; }
            public string ProblemType { get; set; }
            public string ProblemContent { get; set; }
            public string AddProblemContent { get; set; }
            public string DutyUnitId { get; set; }
            public string RectificationUnitId { get; set; }
            public int IsIncident { get; set; }
            public string IncidentNo { get; set; }
            public int IsRejection { get; set; }
            public string UnqualifiedType { get; set; }
            public List<File> Files { get; set; }


            // 查验整改才用到
            public string CheckDate { get; set; }
            public string CheckPid { get; set; }
        }

        class File
        {
            public string FileId { get; set; }
            public string PkId { get; set; }
            public string FilePath { get; set; }
            public string AddDate { get; set; }
            public string AddPid { get; set; }
        }

        /// <summary>
        /// 任务相关默认分页页长
        /// </summary
        private static int TASK_PAGE_SIZE = 300;
#pragma warning disable CS0414 // 字段“PMSHouseInspectionManage.TASK_UPLOAD_PAGE_SIZE”已被赋值，但从未使用过它的值
        private static int TASK_UPLOAD_PAGE_SIZE = 10;
#pragma warning restore CS0414 // 字段“PMSHouseInspectionManage.TASK_UPLOAD_PAGE_SIZE”已被赋值，但从未使用过它的值

        public PMSHouseInspectionManage()
        {
            base.Token = "20200520PMSHouseInspectionManage";
        }

        public override void Operate(ref Transfer Trans)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            //验证登录
            if (!new Login().isLogin(ref Trans))
                return;

            //防止未捕获异常出现
            try
            {
                switch (Trans.Command)
                {

                    case "DownloadResponsibleUnit":                     // 下载责任单位
                        Trans.Result = DownloadResponsibleUnit(Row);
                        break;
                    case "DownloadProblemType":                         // 下载问题类型
                        Trans.Result = DownloadProblemType(Row);
                        break;
                    case "DownloadTask":                                // 下载查验计划任务
                        Trans.Result = DownloadTask(Row);
                        break;
                    case "DownloadHouseInspRectifyTask":                // 下载查验整改任务
                        Trans.Result = DownloadHouseInspRectifyTask(Row);
                        break;
                    case "DownloadTaskRoom":                            // 下载查验计划房屋
                        Trans.Result = DownloadTaskRoom(Row);
                        break;
                    case "DownloadHouseInspRectifyRoom":                // 下载查验整改房屋
                        Trans.Result = DownloadHouseInspRectifyRoom(Row);
                        break;
                    case "DownloadTaskStandardWithPager":               // 分页下载查验标准
                        Trans.Result = DownloadTaskStandardWithPager(Row);
                        break;
                    case "DownloadHouseInspRectifyStandardWithPager":   // 分页下载整改标准
                        Trans.Result = DownloadHouseInspRectifyStandardWithPager(Row);
                        break;
                    case "UploadTaskData":                              // 上传任务数据，用于离线任务
                        Trans.Result = UploadTaskData(Row);
                        break;
                    case "UploadTaskRectifyData":                       // 上传整改任务数据，用于离线任务
                        Trans.Result = UploadTaskRectifyData(Row);
                        break;
                    default:
                        Trans.Result = JSONHelper.FromString(false, "未找到命令" + Trans.Command);
                        break;
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace + Environment.NewLine + ex.Source);
                Trans.Result = new ApiResult(false, ex.Message + Environment.CommandLine + ex.StackTrace).toJson();
            }
        }


        /// <summary>
        /// 下载责任单位
        /// </summary>
        private string DownloadResponsibleUnit(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            var commId = row["CommID"].ToString();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = $@"SELECT ResponsibleUnitFile,CommID,Sort,UnitType,UnitName,Contacts,ContactNumber,
                                   Remarks,BuildId,Profession,PublicArea
                            FROM Tb_HI_ResponsibleUnitFile
                            WHERE isnull(IsDelete,0)=0 AND CommID={commId} ORDER BY Sort";

                var data = conn.Query(sql);

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 下载问题类型
        /// </summary>
        private string DownloadProblemType(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            using (IDbConnection conn0 = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = "";

                // 任务列表（筛选具体岗位）
                sql += $@"SELECT a.TaskId FROM Tb_HI_Task a WITH(NOLOCK)
                        WHERE isnull(a.IsDelete,0)=0 AND TaskStatus<>'已完成' AND a.CommID=@CommID
                        AND a.BeginTime<=getdate() AND a.EndTime>=getdate()
                        AND
                        (
                            (SELECT count(t.RoleCode) AS RoleCode FROM (
                                SELECT Value AS RoleCode FROM { Global_Var.ERPDatabaseName }.dbo.SplitString(a.RoleCode,',',1)
                                InterSect
                                SELECT RoleCode FROM { Global_Var.ERPDatabaseName }.dbo.Tb_Sys_UserRole WHERE UserCode=@UserCode
                            ) AS T)>0
                        )";

                sql = $@"SELECT DISTINCT a.ID,a.Sort,a.Content,a.ProblemLevel,a.TimeLimit,a.ObjectId
                        FROM Tb_HI_CheckRoomProblem a WITH(NOLOCK)
                        INNER JOIN Tb_HI_SysStandard b WITH(NOLOCK) ON b.ObjectId=a.ObjectId
                        INNER JOIN Tb_HI_TaskStandard c WITH(NOLOCK) ON b.StandardId=c.StandardId
                        WHERE c.TaskId IN
                        ({sql}) AND isnull(a.IsDelete,0)=0
                        ORDER BY a.Sort";

                try
                {
                    var data = conn0.Query(sql, new
                    {
                        CommID = commId,
                        UserCode = Global_Var.LoginUserCode
                    }).ToList();

                    return new ApiResult(true, data).toJson();
                }
                catch (Exception ex)
                {
                    return new ApiResult(false, ex.Message).toJson();
                }
            }
        }

        /// <summary>
        /// 下载查验计划任务
        /// </summary>
        private string DownloadTask(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            using (IDbConnection conn0 = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = "";

                // 任务列表（筛选具体岗位）
                sql += $@"SELECT a.TaskId FROM Tb_HI_Task a WITH(NOLOCK)
                        WHERE isnull(a.IsDelete,0)=0 AND TaskStatus<>'已完成' AND a.CommID=@CommID
                        AND a.BeginTime<=getdate() AND a.EndTime>=getdate()
                        AND
                        (
                           (SELECT count(t.RoleCode) AS RoleCode FROM (
                                SELECT Value AS RoleCode FROM { Global_Var.ERPDatabaseName }.dbo.SplitString(a.RoleCode,',',1)
                                InterSect
                                SELECT RoleCode FROM { Global_Var.ERPDatabaseName }.dbo.Tb_Sys_UserRole WHERE UserCode=@UserCode
                           ) AS T)>0
                        )";

                sql = $@"SELECT TaskId,TaskNo,a.CommId,BeginTime,EndTime,TaskStatus,RoleCode,Batch,Range,
                                (SELECT CommName FROM Tb_HSPR_Community WHERE CommID=a.CommId) as CommName,
                                (SELECT RoleName FROM { Global_Var.ERPDatabaseName }.dbo.Tb_Sys_Role WHERE RoleCode=a.RoleCode) as RoleName,
                                (SELECT Name FROM Tb_HI_Dictionary WHERE Id=a.Batch) as BatchName,
                                (SELECT count(1) FROM Tb_HI_TaskHouse WITH(NOLOCK) WHERE TaskId=a.TaskId) AS RoomCount,
                                (SELECT count(1) FROM Tb_HI_TaskStandard WITH(NOLOCK) WHERE TaskId=a.TaskId AND isnull(CheckDate,'')='') AS StandardCount,
                                (SELECT count(1) FROM Tb_HI_TaskHouse x WHERE x.TaskId=a.TaskId AND isnull(x.IsDelete,0)=0 AND x.IsComplete=1) AS CompletedRoomCount,
                                (SELECT count(1) FROM Tb_HI_TaskHouse x WHERE x.TaskId=a.TaskId AND isnull(x.IsDelete,0)=0 AND x.IsComplete=1 AND x.IsQualified=0) AS UnqualifiedRoomCount,
                                (SELECT count(1) FROM Tb_HI_TaskStandard x WHERE x.TaskId=a.TaskId AND isnull(x.IsDelete,0)=0 AND x.IsQualified=1) AS QualifiedStandardCount,
                                (SELECT count(1) FROM Tb_HI_TaskStandard x WHERE x.TaskId=a.TaskId AND isnull(x.IsDelete,0)=0 AND x.IsQualified=0) AS UnqualifiedStandardCount,
                                {TASK_PAGE_SIZE} AS StandardPageSize
                        FROM Tb_HI_Task a WITH(NOLOCK)
                        WHERE TaskId IN ({sql})
                        ORDER BY EndTime";

                try
                {
                    var data = conn0.Query(sql, new
                    {
                        CommID = commId,
                        UserCode = Global_Var.LoginUserCode
                    }).ToList();

                    return new ApiResult(true, data).toJson();
                }
                catch (Exception ex)
                {
                    return new ApiResult(false, ex.Message).toJson();
                }
            }
        }

        /// <summary>
        /// 下载查验整改任务
        /// </summary>
        private string DownloadHouseInspRectifyTask(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            using (IDbConnection conn0 = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = "";

                // 任务列表（筛选具体岗位）
                sql += $@"SELECT b.TaskId FROM Tb_HI_TaskStandard b WITH(NOLOCK) 
                        WHERE b.IsQualified=0 AND ISNULL(b.CheckDate,'')='' AND ISNULL(b.CheckPid,'')=''
                        AND
                        (
                           (SELECT count(t.RoleCode) AS RoleCode FROM (
                                SELECT Value AS RoleCode FROM { Global_Var.ERPDatabaseName }.dbo.SplitString(a.RoleCode,',',1)
                                InterSect
                                SELECT RoleCode FROM { Global_Var.ERPDatabaseName }.dbo.Tb_Sys_UserRole WHERE UserCode=@UserCode
                           ) AS T)>0
                        )";

                sql = $@"SELECT TaskId,TaskNo,a.CommId,BeginTime,EndTime,TaskStatus,RoleCode,Batch,Range,
                                (SELECT CommName FROM Tb_HSPR_Community WHERE CommID=a.CommId) as CommName,
                                (SELECT RoleName FROM { Global_Var.ERPDatabaseName }.dbo.Tb_Sys_Role WHERE RoleCode=a.RoleCode) as RoleName,
                                (SELECT Name FROM Tb_HI_Dictionary WHERE Id=a.Batch) as BatchName,
                                (SELECT count(1) FROM Tb_HI_TaskHouse WITH(NOLOCK) WHERE TaskId=a.TaskId 
                                    AND HouseId IN (SELECT HouseId FROM Tb_HI_TaskStandard WITH(NOLOCK) WHERE IsQualified=0 AND isnull(IsDelete,0)=0
                                                    AND isnull(CheckDate,'')='' AND TaskId=a.TaskId GROUP BY HouseId)) AS RoomCount,
                                {TASK_PAGE_SIZE} AS StandardPageSize

                        FROM Tb_HI_Task a WITH(NOLOCK)
                        WHERE a.TaskId IN ({sql}) AND a.CommID = @CommID AND ISNULL(a.IsDelete, 0) = 0
                        ORDER BY EndTime";

                try
                {
                    var data = conn0.Query(sql, new
                    {
                        CommID = commId,
                        UserCode = Global_Var.LoginUserCode
                    }).ToList();

                    return new ApiResult(true, data).toJson();
                }
                catch (Exception ex)
                {
                    return new ApiResult(false, ex.Message).toJson();
                }
            }
        }


        /// <summary>
        /// 下载查验计划房屋数据
        /// </summary>
        private string DownloadTaskRoom(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            if (!row.Table.Columns.Contains("TaskId") || string.IsNullOrEmpty(row["TaskId"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var taskId = row["TaskId"].ToString();

            var pageSize = TASK_PAGE_SIZE;
            var pageIndex = 1;
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }


            using (IDbConnection conn0 = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = $@"SELECT Range FROM Tb_HI_Task WHERE TaskId='{taskId}'";

                var range = conn0.Query<string>(sql).FirstOrDefault();

                // 公区
                if (range == @"OUT")
                {

                    sql = $@"SELECT *FROM 
                        (
                            SELECT ROW_NUMBER() OVER(ORDER BY a.Id) AS RN,
                                a.Id, TaskId, a.HouseId, isnull(a.IsComplete,0) AS IsComplete, a.CompletePid,a.CompleteDate,isnull(a.IsQualified,'') as IsQualified,
                                (SELECT Batch FROM Tb_HI_Task WHERE TaskId=@TaskId) as Batch,
                                b.Sort,b.PublicAreaCode,b.PublicAreaName,b.PublicAreaType,b.Remark,b.Id AS PublicAreaId

                                FROM Tb_HI_TaskHouse a WITH(NOLOCK)
                                LEFT JOIN Tb_HI_PublicAreaSet b WITH(NOLOCK) ON a.HouseId=b.Id AND isnull(b.IsDelete,0)=0
                                WHERE a.TaskId=@TaskId AND isnull(a.IsDelete,0)= 0 AND b.CommID=@CommID
                        ) t
                        WHERE t.RN BETWEEN ({ pageIndex }-1)*{ pageSize }+1 AND { pageIndex }*{ pageSize }";
                }
                else {
                    var condition = "";
                    sql = "SELECT isnull(col_length('Tb_HSPR_Room','UnitName'),0)";
                    var len = conn0.Query<int>(sql).FirstOrDefault();
                    if (len > 0)
                    {
                        condition += @"b.UnitName,";
                    }
                    else
                    {
                        condition += @"b.UnitSNum AS UnitName,";
                    }


                    sql = "SELECT isnull(col_length('Tb_HSPR_Room','FloorName'),0)";
                    len = conn0.Query<int>(sql).FirstOrDefault();
                    if (len > 0)
                    {
                        condition += @"b.FloorName";
                    }
                    else
                    {
                        condition += @"b.FloorsNum AS FloorName";
                    }



                    sql = $@"SELECT *FROM 
                        (
                            SELECT ROW_NUMBER() OVER(ORDER BY a.Id) AS RN,
                                a.Id, TaskId, a.HouseId, isnull(a.IsComplete,0) AS IsComplete, a.CompletePid,a.CompleteDate,isnull(a.IsQualified,'') as IsQualified,
                                (SELECT Batch FROM Tb_HI_Task WHERE TaskId=@TaskId) as Batch,
                                c.BuildID,b.BuildSNum,c.BuildName,b.UnitSNum,b.FloorsNum,{condition},b.RoomSNum,b.RoomSign,b.RoomName

                                FROM Tb_HI_TaskHouse a WITH(NOLOCK)
                                LEFT JOIN Tb_HSPR_Room b WITH(NOLOCK) ON a.HouseId=b.RoomID
                                LEFT JOIN Tb_HSPR_Building c WITH(NOLOCK) ON b.CommID=c.CommID AND b.BuildSNum=c.BuildSNum AND isnull(c.IsDelete,0)=0
                                WHERE a.TaskId=@TaskId AND isnull(a.IsDelete,0)= 0 AND b.CommID=@CommID
                        ) t
                        WHERE t.RN BETWEEN ({ pageIndex }-1)*{ pageSize }+1 AND { pageIndex }*{ pageSize }";
                }

                try
                {
                    var data = conn0.Query(sql, new
                    {
                        CommID = commId,
                        TaskId = taskId
                    }).ToList();

                    // 获取房屋对应的查验图纸
                    sql = @"SELECT x.InspectionDrawingsId,x.Drawing,x.CommID,x.HouseId,
                            (SELECT TOP 1 FilesPath AS FilePath FROM Tb_HI_Files WHERE isnull(IsDelete,0)=0 AND PkId=@Id AND FilesNo=x.InspectionDrawingsId 
	                            ORDER BY AddDate DESC) AS CurrentDrawing 
                            FROM Tb_HI_InspectionDrawings x WITH(NOLOCK)
                            WHERE x.HouseID=@HouseID AND x.Batch=@Batch AND isnull(x.IsDelete,0)=0";

                    foreach (var item in data)
                    {
                        var roomDrawings = conn0.Query(sql, new { Id = item.Id, HouseID = item.HouseId, Batch = item.Batch }).ToList();

                        item.RoomDrawings = roomDrawings;
                    }

                    return new ApiResult(true, data).toJson();
                }
                catch (Exception ex)
                {
                    return new ApiResult(false, ex.Message).toJson();
                }
            }
        }


        /// <summary>
        /// 下载查验整改房屋数据
        /// </summary>
        private string DownloadHouseInspRectifyRoom(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            if (!row.Table.Columns.Contains("TaskId") || string.IsNullOrEmpty(row["TaskId"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var taskId = row["TaskId"].ToString();

            var pageSize = TASK_PAGE_SIZE;
            var pageIndex = 1;
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }


            using (IDbConnection conn0 = new SqlConnection(PubConstant.hmWyglConnectionString))
            {


                var sql = $@"SELECT Range FROM Tb_HI_Task WHERE TaskId='{taskId}'";

                var range = conn0.Query<string>(sql).FirstOrDefault();

                // 公区
                if (range == @"OUT")
                {
                    sql = $@"SELECT *FROM 
                        (
                            SELECT ROW_NUMBER() OVER(ORDER BY a.Id) AS RN,
                                a.Id, TaskId, a.HouseId, isnull(a.IsComplete,0) AS IsComplete,a.CompletePid,a.CompleteDate,isnull(a.IsQualified,'') as IsQualified,
                                (SELECT Batch FROM Tb_HI_Task WHERE TaskId=@TaskId) as Batch,
                                b.PublicAreaCode,b.PublicAreaName,b.PublicAreaType,b.Remark,b.Id AS PublicAreaId,


                                (CASE WHEN (SELECT count(1) FROM Tb_HI_TaskStandard x WHERE x.HouseId=a.HouseId AND x.IsQualified=0 AND isnull(RealCompleteDate,'')='')>0
                                    AND (SELECT count(1) FROM Tb_HI_TaskStandard x WHERE x.HouseId=a.HouseId AND x.IsQualified=0 AND (RealCompleteDate IS NOT NULL OR isnull(ReworkTimes,0)>0))>0
                                    THEN '整改中'
                                WHEN (SELECT count(1) FROM Tb_HI_TaskStandard x WHERE x.HouseId=a.HouseId AND x.IsQualified=0 AND RealCompleteDate IS NOT NULL)=0
                                    THEN '未整改'
                                WHEN (SELECT count(1) FROM Tb_HI_TaskStandard x WHERE x.HouseId=a.HouseId AND x.IsQualified=0 AND isnull(RealCompleteDate,'')='')=0
                                    THEN '待验收'
                                END) AS RoomState,

                               CASE WHEN (SELECT count(1) FROM Tb_HI_TaskStandard x WHERE x.HouseId=a.HouseId AND x.IsQualified=0 AND isnull(RealCompleteDate,'')='' AND PollingCompleteDate<=getdate())>0
                                THEN 1 ELSE 0 END AS IsOverTime


                                FROM Tb_HI_TaskHouse a WITH(NOLOCK)
                                LEFT JOIN Tb_HI_PublicAreaSet b WITH(NOLOCK) ON a.HouseId=b.Id AND isnull(b.IsDelete,0)=0
                                WHERE a.TaskId=@TaskId AND isnull(a.IsDelete,0)=0 AND isnull(a.IsComplete,0)=1 AND a.IsQualified=0 AND b.CommID=@CommID 
                                AND a.HouseId IN(SELECT HouseId FROM Tb_HI_TaskStandard WHERE IsQualified=0 AND isnull(IsDelete,0)=0
                                                 AND ISNULL(CheckDate,'')='' AND TaskId=@TaskId GROUP BY HouseId)
                        ) t
                        WHERE t.RN BETWEEN ({ pageIndex }-1)*{ pageSize }+1 AND { pageIndex }*{ pageSize }";
                }
                else {
                    var condition = "";
                    sql = "SELECT isnull(col_length('Tb_HSPR_Room','UnitName'),0)";
                    var len = conn0.Query<int>(sql).FirstOrDefault();
                    if (len > 0)
                    {
                        condition += @"b.UnitName,";
                    }
                    else
                    {
                        condition += @"b.UnitSNum AS UnitName,";
                    }


                    sql = "SELECT isnull(col_length('Tb_HSPR_Room','FloorName'),0)";
                    len = conn0.Query<int>(sql).FirstOrDefault();
                    if (len > 0)
                    {
                        condition += @"b.FloorName";
                    }
                    else
                    {
                        condition += @"b.FloorsNum AS FloorName";
                    }


                    sql = $@"SELECT *FROM 
                        (
                            SELECT ROW_NUMBER() OVER(ORDER BY a.Id) AS RN,
                                a.Id, TaskId, a.HouseId, isnull(a.IsComplete,0) AS IsComplete,a.CompletePid,a.CompleteDate,isnull(a.IsQualified,'') as IsQualified,
                                (SELECT Batch FROM Tb_HI_Task WHERE TaskId=@TaskId) as Batch,
                                c.BuildID,b.BuildSNum,c.BuildName,b.UnitSNum,b.FloorsNum,{condition},b.RoomSNum,b.RoomSign,b.RoomName,


                                (CASE WHEN (SELECT count(1) FROM Tb_HI_TaskStandard x WHERE x.HouseId=a.HouseId AND x.IsQualified=0 AND isnull(RealCompleteDate,'')='')>0
                                    AND (SELECT count(1) FROM Tb_HI_TaskStandard x WHERE x.HouseId=a.HouseId AND x.IsQualified=0 AND (RealCompleteDate IS NOT NULL OR isnull(ReworkTimes,0)>0))>0
                                    THEN '整改中'
                                WHEN (SELECT count(1) FROM Tb_HI_TaskStandard x WHERE x.HouseId=a.HouseId AND x.IsQualified=0 AND RealCompleteDate IS NOT NULL)=0
                                    THEN '未整改'
                                WHEN (SELECT count(1) FROM Tb_HI_TaskStandard x WHERE x.HouseId=a.HouseId AND x.IsQualified=0 AND isnull(RealCompleteDate,'')='')=0
                                    THEN '待验收'
                                END) AS RoomState,

                               CASE WHEN (SELECT count(1) FROM Tb_HI_TaskStandard x WHERE x.HouseId=a.HouseId AND x.IsQualified=0 AND isnull(RealCompleteDate,'')='' AND PollingCompleteDate<=getdate())>0
                                THEN 1 ELSE 0 END AS IsOverTime


                                FROM Tb_HI_TaskHouse a WITH(NOLOCK)
                                LEFT JOIN Tb_HSPR_Room b WITH(NOLOCK) ON a.HouseId=b.RoomID
                                LEFT JOIN Tb_HSPR_Building c WITH(NOLOCK) ON b.CommID=c.CommID AND b.BuildSNum=c.BuildSNum AND isnull(c.IsDelete,0)=0
                                WHERE a.TaskId=@TaskId AND isnull(a.IsDelete,0)=0 AND isnull(a.IsComplete,0)=1 AND a.IsQualified=0 AND b.CommID=@CommID 
                                AND a.HouseId IN(SELECT HouseId FROM Tb_HI_TaskStandard WHERE IsQualified=0 AND isnull(IsDelete,0)=0
                                                 AND ISNULL(CheckDate,'')='' AND TaskId=@TaskId GROUP BY HouseId)
                        ) t
                        WHERE t.RN BETWEEN ({ pageIndex }-1)*{ pageSize }+1 AND { pageIndex }*{ pageSize }";
                }


                

                try
                {
                    var data = conn0.Query(sql, new
                    {
                        CommID = commId,
                        TaskId = taskId
                    }).ToList();

                    // 获取房屋对应的查验图纸
                    sql = @"SELECT x.InspectionDrawingsId,x.Drawing,x.CommID,x.HouseId,
                            (SELECT TOP 1 FilesPath AS FilePath FROM Tb_HI_Files WHERE isnull(IsDelete,0)=0 AND PkId=@Id AND FilesNo=x.InspectionDrawingsId 
	                            ORDER BY AddDate DESC) AS CurrentDrawing 
                            FROM Tb_HI_InspectionDrawings x WITH(NOLOCK)
                            WHERE x.HouseID=@HouseID AND x.Batch=@Batch AND isnull(x.IsDelete,0)=0";

                    foreach (var item in data)
                    {
                        var roomDrawings = conn0.Query(sql, new { Id = item.Id, HouseID = item.HouseId, Batch = item.Batch }).ToList();
                        item.RoomDrawings = roomDrawings;
                    }

                    return new ApiResult(true, data).toJson();
                }
                catch (Exception ex)
                {
                    return new ApiResult(false, ex.Message).toJson();
                }
            }
        }

        /// <summary>
        /// 下载查验标准数据
        /// </summary>
        private string DownloadTaskStandardWithPager(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }
            if (!row.Table.Columns.Contains("TaskId") || string.IsNullOrEmpty(row["TaskId"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("Range") || string.IsNullOrEmpty(row["Range"].ToString()))
            {
                return JSONHelper.FromString(false, "Range范围不能为空");
            }


            var range = row["Range"].ToString();
            var buildSNum = 0;
            var unitSNum = 0;
            var floorsNum = 0;
            if (range == "IN")
            {
                if (!row.Table.Columns.Contains("BuildSNum") || string.IsNullOrEmpty(row["BuildSNum"].ToString()))
                {
                    return JSONHelper.FromString(false, "楼栋不能为空");
                }
                if (!row.Table.Columns.Contains("UnitSNum") || string.IsNullOrEmpty(row["UnitSNum"].ToString()))
                {
                    return JSONHelper.FromString(false, "单元不能为空");
                }
                if (!row.Table.Columns.Contains("FloorsNum") || string.IsNullOrEmpty(row["FloorsNum"].ToString()))
                {
                    return JSONHelper.FromString(false, "楼层不能为空");
                }


                buildSNum = AppGlobal.StrToInt(row["BuildSNum"].ToString());
                unitSNum = AppGlobal.StrToInt(row["UnitSNum"].ToString());
                floorsNum = AppGlobal.StrToInt(row["FloorsNum"].ToString());
            }

            var pageSize = TASK_PAGE_SIZE;
            var pageIndex = 1;
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var taskId = row["TaskId"].ToString();


            using (IDbConnection conn0 = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = "";
                if (range == "IN")
                {
                    sql = $@"SELECT *FROM 
                            (
                                SELECT ROW_NUMBER() OVER(ORDER BY a.Id) AS RN,a.Id, a.TaskId, a.HouseId, a.IsQualified, a.ProblemType,a.ProblemContent,
                                       a.DutyUnitId,a.RectificationUnitId,a.CheckDate,a.CheckPid,a.IsIncident,a.IncidentNo,a.IsRejection,
                                       b.StandardCode,b.Profession,d.Name AS ProfessionName,e.Name AS ImportanceName,b.Content,b.InspectMethod,b.PermissibleDeviation,
                                       a.CheckRoomDefinitionId,f.HouseName,f.Sort AS CheckRoomSort,
                                       c.ObjectId, c.ObjectName, a.UnqualifiedType, a.ProblemId

                                FROM Tb_HI_TaskStandard a WITH(NOLOCK)
                                LEFT JOIN Tb_HI_SysStandard b WITH(NOLOCK) ON a.StandardId=b.StandardId
                                LEFT JOIN Tb_HI_SysObject c WITH(NOLOCK) ON b.ObjectId=c.ObjectId
                                LEFT JOIN Tb_HI_Dictionary d WITH(NOLOCK) ON b.Profession=d.Id
                                LEFT JOIN Tb_HI_Dictionary e WITH(NOLOCK) ON b.Importance=d.Id
                                LEFT JOIN Tb_HI_CheckRoomDefinition f WITH(NOLOCK) ON a.CheckRoomDefinitionId=f.Id
                                WHERE isnull(a.IsDelete,0)=0 AND isnull(a.IsQualified,'')=''
                                AND a.TaskId=@TaskId
                                AND a.HouseId IN
                                    (SELECT RoomID FROM Tb_HSPR_Room x WITH(NOLOCK) WHERE x.CommID=@CommID AND x.BuildSNum=@BuildSNum AND x.UnitSNum=@UnitSNum
                                    AND x.FloorsNum=@FloorsNum AND isnull(x.IsDelete,0)=0)
                            ) t
                            WHERE t.RN BETWEEN ({ pageIndex }-1)*{ pageSize }+1 AND { pageIndex }*{ pageSize }";
                }
                else
                {

                    sql = $@"SELECT *FROM 
                            (
                                SELECT ROW_NUMBER() OVER(ORDER BY a.Id) AS RN,a.Id, a.TaskId, a.HouseId, a.IsQualified, a.ProblemType,a.ProblemContent,
                                       a.DutyUnitId,a.RectificationUnitId,a.CheckDate,a.CheckPid,a.IsIncident,a.IncidentNo,a.IsRejection,
                                       b.StandardCode,b.Profession,d.Name AS ProfessionName,e.Name AS ImportanceName,b.Content,b.InspectMethod,b.PermissibleDeviation,
                                       a.CheckRoomDefinitionId,f.HouseName,f.Sort AS CheckRoomSort,
                                       c.ObjectId, c.ObjectName, a.UnqualifiedType, a.ProblemId

                                FROM Tb_HI_TaskStandard a WITH(NOLOCK)
                                LEFT JOIN Tb_HI_SysStandard b WITH(NOLOCK) ON a.StandardId=b.StandardId
                                LEFT JOIN Tb_HI_SysObject c WITH(NOLOCK) ON b.ObjectId=c.ObjectId
                                LEFT JOIN Tb_HI_Dictionary d WITH(NOLOCK) ON b.Profession=d.Id
                                LEFT JOIN Tb_HI_Dictionary e WITH(NOLOCK) ON b.Importance=d.Id
                                LEFT JOIN Tb_HI_CheckRoomDefinition f WITH(NOLOCK) ON a.CheckRoomDefinitionId=f.Id
                                WHERE isnull(a.IsDelete,0)=0 AND isnull(a.IsQualified,'')=''
                                AND a.TaskId=@TaskId
                                AND a.HouseId IN
                                    (SELECT Id FROM Tb_HI_PublicAreaSet x WITH (NOLOCK) WHERE x.CommID = @CommID AND isnull(x.IsDelete, 0) = 0)
                            ) t
                            WHERE t.RN BETWEEN ({ pageIndex }-1)*{ pageSize }+1 AND { pageIndex }*{ pageSize }";

                }

                try
                {
                    var data = conn0.Query(sql, new
                    {
                        CommID = commId,
                        TaskId = taskId,
                        BuildSNum = buildSNum,
                        UnitSNum = unitSNum,
                        FloorsNum = floorsNum
                    }).ToList();

                    return new ApiResult(true, data).toJson();
                }
                catch (Exception ex)
                {
                    return new ApiResult(false, ex.Message).toJson();
                }
            }
        }

        /// <summary>
        /// 下载整改标准数据
        /// </summary>
        private string DownloadHouseInspRectifyStandardWithPager(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }
            if (!row.Table.Columns.Contains("TaskId") || string.IsNullOrEmpty(row["TaskId"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("Range") || string.IsNullOrEmpty(row["Range"].ToString()))
            {
                return JSONHelper.FromString(false, "Range范围不能为空");
            }

            var range = row["Range"].ToString();
            var buildSNum = 0;
            var unitSNum = 0;
            var floorsNum = 0;
            if (range == "IN")
            {
                if (!row.Table.Columns.Contains("BuildSNum") || string.IsNullOrEmpty(row["BuildSNum"].ToString()))
                {
                    return JSONHelper.FromString(false, "楼栋不能为空");
                }
                if (!row.Table.Columns.Contains("UnitSNum") || string.IsNullOrEmpty(row["UnitSNum"].ToString()))
                {
                    return JSONHelper.FromString(false, "单元不能为空");
                }
                if (!row.Table.Columns.Contains("FloorsNum") || string.IsNullOrEmpty(row["FloorsNum"].ToString()))
                {
                    return JSONHelper.FromString(false, "楼层不能为空");
                }


                buildSNum = AppGlobal.StrToInt(row["BuildSNum"].ToString());
                unitSNum = AppGlobal.StrToInt(row["UnitSNum"].ToString());
                floorsNum = AppGlobal.StrToInt(row["FloorsNum"].ToString());
            }


            var pageSize = TASK_PAGE_SIZE;
            var pageIndex = 1;
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var taskId = row["TaskId"].ToString();


            using (IDbConnection conn0 = new SqlConnection(PubConstant.hmWyglConnectionString))
            {


                var sql = "";
                if (range == "IN")
                {
                    sql = $@"SELECT *FROM 
                            (
                                SELECT ROW_NUMBER() OVER(ORDER BY a.Id) AS RN,a.Id, a.TaskId, a.HouseId, a.IsQualified, a.ProblemType,a.ProblemContent,
                                       a.DutyUnitId,a.RectificationUnitId,a.CheckDate,a.CheckPid,a.IsIncident,a.IncidentNo,a.IsRejection,
                                       b.StandardCode,b.Profession,d.Name AS ProfessionName,e.Name AS ImportanceName,b.Content,b.InspectMethod,b.PermissibleDeviation,
                                       a.CheckRoomDefinitionId,f.HouseName,f.Sort AS CheckRoomSort, c.ObjectId, c.ObjectName, a.UnqualifiedType, a.ProblemId, 
                                       a.PollingCompleteDate, a.RealCompleteDate, a.ReworkTimes,a.CompleteRemark

                                FROM Tb_HI_TaskStandard a WITH(NOLOCK)
                                LEFT JOIN Tb_HI_SysStandard b WITH(NOLOCK) ON a.StandardId=b.StandardId
                                LEFT JOIN Tb_HI_SysObject c WITH(NOLOCK) ON b.ObjectId=c.ObjectId
                                LEFT JOIN Tb_HI_Dictionary d WITH(NOLOCK) ON b.Profession=d.Id
                                LEFT JOIN Tb_HI_Dictionary e WITH(NOLOCK) ON b.Importance=d.Id
                                LEFT JOIN Tb_HI_CheckRoomDefinition f WITH(NOLOCK) ON a.CheckRoomDefinitionId=f.Id
                                WHERE isnull(a.IsDelete,0)=0 AND a.IsQualified=0 AND isnull(a.CheckDate,'')='' AND isnull(a.CheckPid,'')='' AND a.TaskId=@TaskId
                                AND a.HouseId IN
                                    (SELECT RoomID FROM Tb_HSPR_Room x WITH(NOLOCK) WHERE x.CommID=@CommID AND x.BuildSNum=@BuildSNum AND x.UnitSNum=@UnitSNum
                                    AND x.FloorsNum=@FloorsNum AND isnull(x.IsDelete,0)=0)
                            ) t
                            WHERE t.RN BETWEEN ({ pageIndex }-1)*{ pageSize }+1 AND { pageIndex }*{ pageSize }";
                }
                else {
                    sql = $@"SELECT *FROM 
                            (
                                SELECT ROW_NUMBER() OVER(ORDER BY a.Id) AS RN,a.Id, a.TaskId, a.HouseId, a.IsQualified, a.ProblemType,a.ProblemContent,
                                       a.DutyUnitId,a.RectificationUnitId,a.CheckDate,a.CheckPid,a.IsIncident,a.IncidentNo,a.IsRejection,
                                       b.StandardCode,b.Profession,d.Name AS ProfessionName,e.Name AS ImportanceName,b.Content,b.InspectMethod,b.PermissibleDeviation,
                                       a.CheckRoomDefinitionId,f.HouseName,f.Sort AS CheckRoomSort, c.ObjectId, c.ObjectName, a.UnqualifiedType, a.ProblemId, 
                                       a.PollingCompleteDate, a.RealCompleteDate, a.ReworkTimes,a.CompleteRemark

                                FROM Tb_HI_TaskStandard a WITH(NOLOCK)
                                LEFT JOIN Tb_HI_SysStandard b WITH(NOLOCK) ON a.StandardId=b.StandardId
                                LEFT JOIN Tb_HI_SysObject c WITH(NOLOCK) ON b.ObjectId=c.ObjectId
                                LEFT JOIN Tb_HI_Dictionary d WITH(NOLOCK) ON b.Profession=d.Id
                                LEFT JOIN Tb_HI_Dictionary e WITH(NOLOCK) ON b.Importance=d.Id
                                LEFT JOIN Tb_HI_CheckRoomDefinition f WITH(NOLOCK) ON a.CheckRoomDefinitionId=f.Id
                                WHERE isnull(a.IsDelete,0)=0 AND a.IsQualified=0 AND isnull(a.CheckDate,'')='' AND isnull(a.CheckPid,'')='' AND a.TaskId=@TaskId
                                AND a.HouseId IN
                                    (SELECT Id FROM Tb_HI_PublicAreaSet x WITH (NOLOCK) WHERE x.CommID = @CommID AND isnull(x.IsDelete, 0) = 0)
                            ) t
                            WHERE t.RN BETWEEN ({ pageIndex }-1)*{ pageSize }+1 AND { pageIndex }*{ pageSize }";
                }
                    

                try
                {
                    var data = conn0.Query(sql, new
                    {
                        CommID = commId,
                        TaskId = taskId,
                        BuildSNum = buildSNum,
                        UnitSNum = unitSNum,
                        FloorsNum = floorsNum
                    }).ToList();


                    foreach (var item in data)
                    {
                        sql = @"SELECT b.TaskId,a.FilesId AS FileId,a.PkId,a.FilesPath AS FilePath, a.FilesName AS FileName, a.AddDate, a.AddPid
                                FROM Tb_HI_Files a
                                LEFT JOIN Tb_HI_TaskStandard b ON a.PkId=b.Id
                                WHERE b.TaskId=@TaskId AND a.PkId=@Id AND isnull(a.IsDelete,0)=0";
                        var files = conn0.Query(sql, new { TaskId = item.TaskId, Id = item.Id});
                        item.Files = files;
                    }

                    return new ApiResult(true, data).toJson();
                }
                catch (Exception ex)
                {
                    return new ApiResult(false, ex.Message).toJson();
                }
            }
        }


        /// <summary>
        /// 上传离线任务数据
        /// </summary>
        private string UploadTaskData(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            JObject data = (JObject)JsonConvert.DeserializeObject(row["Data"].ToString());

            if (string.IsNullOrEmpty(data["TaskId"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }

            var taskId = data["TaskId"].ToString();
            var taskState = data["TaskState"].ToString();               // 任务状态
            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            var connStr = PubConstant.hmWyglConnectionString;
            using (var conn = new SqlConnection(connStr))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var trans = conn.BeginTransaction();

                try
                {
                    var sql = $@"SELECT count(1) FROM Tb_HI_Task WITH(NOLOCK) 
                                WHERE TaskId='{ taskId }' AND isnull(IsDelete,0)=0;";

                    if (conn.Query<int>(sql, null, trans).FirstOrDefault() == 0)
                    {
                        trans.Rollback();
                        return JSONHelper.FromString(true, "该任务已关闭或已删除");
                    }

                    // 更新任务房屋
                    var roomTable = JsonConvert.DeserializeObject<List<TaskRoomModel>>(data["Rooms"].ToString());
                    UpdateHouseInspectionTaskRoom(roomTable, commId, taskId, conn, trans);

                    // 执行事务
                    trans.Commit();
                    trans = conn.BeginTransaction();

                    // 更新任务状态
                    UpdateHouseInspectionTask(commId, taskId, taskState, conn, trans);

                    trans.Commit();
                    return JSONHelper.FromString(true, "保存成功");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message);
                }
            }
        }

        /// <summary>
        /// 上传离线整改任务数据
        /// </summary>
        /// 
        private string UploadTaskRectifyData(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            JObject data = (JObject)JsonConvert.DeserializeObject(row["Data"].ToString());

            if (string.IsNullOrEmpty(data["TaskId"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }

            var taskId = data["TaskId"].ToString();
            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            var connStr = PubConstant.hmWyglConnectionString;
            using (var conn = new SqlConnection(connStr))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var trans = conn.BeginTransaction();

                try
                {
                    var sql = $@"SELECT count(1) FROM Tb_HI_Task WITH(NOLOCK) 
                                WHERE TaskId='{ taskId }' AND isnull(IsDelete,0)=0;";

                    if (conn.Query<int>(sql, null, trans).FirstOrDefault() == 0)
                    {
                        trans.Rollback();
                        return JSONHelper.FromString(true, "该任务已关闭或已删除");
                    }

                    // 更新任务房屋
                    var roomTable = JsonConvert.DeserializeObject<List<TaskRoomModel>>(data["Rooms"].ToString());
                    UpdateHouseInspRectifyTaskRoom(roomTable, commId, taskId, conn, trans);

                    // 执行事务
                    trans.Commit();
                    return JSONHelper.FromString(true, "保存成功");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message);
                }
            }
        }


        /// <summary>
        /// 更新检查房屋
        /// </summary>
        private bool UpdateHouseInspectionTaskRoom(List<TaskRoomModel> roomValue, int commId, string taskId, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (roomValue.Count == 0)
                return true;

            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var sql = "";
            foreach (var roomItem in roomValue)
            {
                var Id = roomItem.Id;
                var isComplete = roomItem.IsComplete;
                var completePid = roomItem.CompletePid;
                var completeDate = roomItem.CompleteDate;
                var isQualified = roomItem.IsQualified;

                //
                sql += $@"UPDATE Tb_HI_TaskHouse 
                        SET IsComplete={isComplete},
                            CompletePid='{completePid}',
                            CompleteDate='{completeDate}',
                            IsQualified={isQualified}
                        WHERE Id='{Id}' AND TaskId='{taskId}';";

                // 保存修改过的验房图纸
                var filesTable = roomItem.InspectionDrawing;
                SaveHouseInspectionFiles(filesTable, commId, taskId, conn, trans);

                // 更新检查标准
                var standardTable = roomItem.Standards;
                UpdateHouseInspectionTaskStandard(standardTable, commId, taskId, conn, trans);

            }
            conn.Execute(sql, null, trans);

            if (db == null)
                conn.Dispose();

            return true;
        }

        /// <summary>
        /// 更新整改检查房屋
        /// </summary>
        private bool UpdateHouseInspRectifyTaskRoom(List<TaskRoomModel> roomValue, int commId, string taskId, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (roomValue.Count == 0)
                return true;

            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            foreach (var roomItem in roomValue)
            {
                var Id = roomItem.Id;
                
                // 保存修改过的验房图纸
                var filesTable = roomItem.InspectionDrawing;
                SaveHouseInspectionFiles(filesTable, commId, taskId, conn, trans);

                // 更新检查标准
                var standardTable = roomItem.Standards;
                UpdateHouseInspRectifyTaskStandard(standardTable, commId, taskId, conn, trans);
            }

            if (db == null)
                conn.Dispose();

            return true;
        }

        /// <summary>
        /// 更新检查标准
        /// </summary>
        private bool UpdateHouseInspectionTaskStandard(List<TaskStandardModel> standardValue, int commId, string taskId, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (standardValue.Count == 0)
                return true;

            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var sql = "";
            foreach (var standardItem in standardValue)
            {
                // 更新任务标准数据
                sql += $@"UPDATE Tb_HI_TaskStandard 
                        SET IsQualified={standardItem.IsQualified},
                            ProblemType='{standardItem.ProblemType}',
                            ProblemContent='{standardItem.ProblemContent}',
                            DutyUnitId='{standardItem.DutyUnitId}',
                            RectificationUnitId='{standardItem.RectificationUnitId}',
                            IsIncident={standardItem.IsIncident},
                            IncidentNo='{standardItem.IncidentNo}',
                            UnqualifiedType='{standardItem.UnqualifiedType}',
                            IsRejection={standardItem.IsRejection}
                        WHERE Id='{standardItem.Id}';";


                // 保存检查标准文件
                var filesTable = standardItem.Files;
                SaveHouseInspectionFiles(filesTable, commId, taskId, conn, trans);

            }
            conn.Execute(sql, null, trans);

            if (db == null)
                conn.Dispose();

            return true;
        }

        /// <summary>
        /// 更新整改检查标准
        /// </summary>
        private bool UpdateHouseInspRectifyTaskStandard(List<TaskStandardModel> standardValue, int commId, string taskId, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (standardValue.Count == 0)
                return true;

            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            foreach (var standardItem in standardValue)
            {
                string Id = standardItem.Id;
                int IsQualified = standardItem.IsQualified;
                string ProblemContent = standardItem.ProblemContent;
                string AddProblemContent = standardItem.AddProblemContent;
                string CheckDate = standardItem.CheckDate;
                string CheckPid = standardItem.CheckPid;
                string CheckRemark = "";
                #region 查询对应任务对象标准是否存在
                dynamic Stanard = conn.QueryFirstOrDefault("SELECT * FROM Tb_HI_TaskStandard WHERE TaskId = @TaskId AND Id = @Id", new { TaskId = taskId, Id }, trans);
                if (null == Stanard)
                {
                    GetLog().InfoFormat("任务({0})下的对象标准({1})信息不存在", taskId, Id);

                    trans.Rollback();
                    return false;
                }

                if (string.IsNullOrEmpty(ProblemContent))
                {
                    ProblemContent = Convert.ToString(Stanard.ProblemContent);
                }
                int ReworkTimes = Convert.ToInt32(Stanard.ReworkTimes);
                #endregion

                if (IsQualified == 1)
                {
                    #region 验收合格
                    if (conn.Execute("UPDATE Tb_HI_TaskStandard SET IsQualified=1, ProblemContent = @ProblemContent, CheckPid = @CheckPid, CheckDate = @CheckDate WHERE TaskId = @TaskId AND Id = @Id", new { ProblemContent, CheckPid, CheckDate, TaskId = taskId, Id }, trans) <= 0)
                    {
                        GetLog().InfoFormat("任务({0})下的对象标准({1})在保存标准时保存失败", taskId, Id);
                        trans.Rollback();
                        return false;
                    }
                    #endregion
                }
                else
                {
                    #region 验收返工
                    ReworkTimes += 1;
                    string RealCompleteDate = null;
                    if (conn.Execute("UPDATE Tb_HI_TaskStandard SET  ProblemContent = @ProblemContent, RealCompleteDate = @RealCompleteDate, ReworkTimes = @ReworkTimes WHERE TaskId = @TaskId AND Id = @Id",
                        new { ProblemContent, RealCompleteDate, ReworkTimes, TaskId = taskId, Id }, trans) <= 0)
                    {
                        GetLog().InfoFormat("任务({0})下的对象标准({1})在保存标准时保存失败", taskId, Id);

                        trans.Rollback();
                        return false;
                    }
                    #endregion
                }

                #region 增加一条记录
                if (conn.Execute("INSERT INTO Tb_HI_TaskCheckLog VALUES(NEWID(),@TaskStandardId,@CheckDate,@CheckPid,@CheckRemark,@IsQualified)", new { TaskStandardId = Id, CheckDate, CheckPid, CheckRemark, IsQualified }, trans) <= 0)
                {
                    GetLog().InfoFormat("任务({0})下的对象标准({1})在插入验收记录时保存失败", taskId, Id);

                    trans.Rollback();
                    return false;

                }
                #endregion


                // 保存检查标准文件
                var filesTable = standardItem.Files;
                SaveHouseInspectionFiles(filesTable, commId, taskId, conn, trans);
            }

            if (db == null)
                conn.Dispose();

            return true;
        }

        // 保存图片文件
        private bool SaveHouseInspectionFiles(List<File> files, int commId, string taskId,
            IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var sql = "";

            foreach (var file in files)
            {
                var fileId = file.FileId;
                var pkId = file.PkId;
                var filePath = file.FilePath;
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                var fix = Path.GetExtension(filePath);
                var addDate = file.AddDate;

                var result = conn.Query(@"SELECT * FROM Tb_HI_Files WITH(NOLOCK) WHERE FilesPath=@FilePath;",
                         new { FilePath = filePath }, trans).Count() > 0;

                if (result == true)
                {
                    continue;
                }

                sql = @"INSERT INTO Tb_HI_Files
                        VALUES(@FilesId,@PkId,'',@FilesName,'',@FilesPath,'',@Fix,0,0,@AddPid,@AddDate,NULL,NULL);";
                int count = conn.Execute(sql, new
                {
                    FilesId = fileId,
                    PkId = pkId,
                    FilesName = fileName,
                    FilesPath = filePath,
                    Fix = fix,
                    AddPid = Global_Var.LoginUserCode,
                    AddDate = addDate
                }, trans);

                if (count == 0)
                {
                    throw new Exception($"保存任务点位文件出错，FileId={fileId}");
                }
            }

            if (db == null)
                conn.Dispose();

            return true;
        }


        /// <summary>
        /// 更新任务
        /// </summary>
        private bool UpdateHouseInspectionTask(int commId, string taskId, string taskState,
                                           IDbConnection db = null, IDbTransaction trans = null)
        {

            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            // 读取当前任务下的标准数量
            var sql = @"SELECT count(*) AS AllCount FROM Tb_HI_TaskStandard WHERE TaskId=@TaskId;

                        SELECT count(*) AS CompletedCount FROM Tb_HI_TaskStandard 
                        WHERE TaskId=@TaskId AND IsQualified IS NOT NULL;";

            var reader = conn.QueryMultiple(sql, new { TaskId = taskId }, trans);
            var allCount = reader.Read<int>().First();
            var completedCount = reader.Read<int>().First();
            if (taskState == @"执行中" &&
                allCount == completedCount)
            {
                taskState = @"已完成";
            }

            // 任务完成了
            if (taskState == @"已完成")
            {
                sql = @"UPDATE Tb_HI_Task 
                        SET TaskStatus=@TaskState
                        WHERE TaskId=@TaskId;";
            }
            // 任务执行中
            else
            {
                sql = $@"UPDATE Tb_HI_Task SET TaskStatus=@TaskState WHERE TaskId=@TaskId AND TaskStatus<>'已完成';";

            }

            conn.Execute(sql, new
            {
                TaskId = taskId,
                TaskState = taskState
            }, trans);


            if (db == null)
                conn.Dispose();

            return true;
        }

    }
}
