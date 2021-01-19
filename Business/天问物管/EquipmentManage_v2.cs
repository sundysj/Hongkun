using Dapper;
using DapperExtensions;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Business
{
    public class EquipmentManage_v2 : PubInfo
    {
        private static int TASK_EUIQPMENT_LINE_PAGE_SIZE = 100;
        private static string TASK_SUFFIX_INSPECTION = "";
        private static string TASK_SUFFIX_MAINTENANCE = "Wb";

        public EquipmentManage_v2()
        {
            base.Token = "20190419EquipmentManage_v2";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            try
            {
                Trans.Result = JSONHelper.FromString(false, "未知错误");
                DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];

                //验证登录
                if (!new Login().isLogin(ref Trans))
                    return;

                switch (Trans.Command)
                {
                    case "GetTaskCount":
                        Trans.Result = GetTaskCount(Row);
                        break;
                    case "DownloadTask":                            // 新版本任务下载，
                        Trans.Result = DownloadTask(Row);
                        break;
                    case "DownloadTaskWithPager":                   // 新版本任务下载，分页
                        Trans.Result = DownloadTaskWithPager(Row);
                        break;
                    case "DownloadTaskEquipmentLineWithPager":      // 新版本任务下载，
                        Trans.Result = DownloadTaskEquipmentLineWithPager(Row);
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.Source + Environment.NewLine + Trans.Attribute);
                Trans.Result = ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine;
            }
        }

        // 获取任务数量
        private string GetTaskCount(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("TaskType") || string.IsNullOrEmpty(row["TaskType"].ToString()))
            {
                return JSONHelper.FromString(false, "任务类型不能为空");
            }

            var taskType = row["TaskType"].ToString();
            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 设备巡检 or 设备维保
                taskType = taskType == "1" ? TASK_SUFFIX_INSPECTION : TASK_SUFFIX_MAINTENANCE;

                // 金辉版本设备，多一个doMonth字段
                var sql = $"SELECT count(1) FROM syscolumns WHERE id=object_id('Tb_EQ_{taskType}PollingPlan') AND name='doMonth'";
                var isJinHuiEquipmentSystem = (conn.Query<int>(sql).FirstOrDefault() > 0);

                sql = $@"SELECT count(1) FROM Tb_EQ_{taskType}Task a
                         WHERE a.RoleCode IN (
                            SELECT SysRoleCode FROM Tb_Sys_Role 
                            WHERE RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{Global_Var.LoginUserCode}')
                            UNION
                            SELECT RoleCode AS SysRoleCode FROM Tb_Sys_UserRole WHERE UserCode='{Global_Var.LoginUserCode}'
                         )
                         AND a.CommID={commId} AND a.BeginTime<=getdate() AND a.EndTime>=getdate()
                         AND a.Statue!='已完成' AND a.Statue!='已关闭' AND isnull(a.IsDelete,0)=0";

                if (isJinHuiEquipmentSystem)
                {
                    sql += "";
                }

                var count = conn.Query<int>(sql).FirstOrDefault();
                return new ApiResult(true, new { TaskCount = count, TaskPageSize = 10 }).toJson();
            }
        }

        /// <summary>
        /// 下载任务
        /// </summary>
        private string DownloadTask(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("TaskType") || string.IsNullOrEmpty(row["TaskType"].ToString()))
            {
                return JSONHelper.FromString(false, "任务类型不能为空");
            }

            var taskType = row["TaskType"].ToString();
            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 设备巡检 or 设备维保
                taskType = taskType == "1" ? TASK_SUFFIX_INSPECTION : TASK_SUFFIX_MAINTENANCE;

                // 金辉版本设备，多一个doMonth字段
                var sql = $"SELECT count(1) FROM syscolumns WHERE id=object_id('Tb_EQ_{taskType}PollingPlan') AND name='doMonth'";
                var isJinHuiEquipmentSystem = (conn.Query<int>(sql).FirstOrDefault() > 0);

                sql = $@"SELECT TaskNO,TaskId,PlanName,a.SpaceId,MacRoName,MacRoAddress,a.Content,a.CommId,CommName,a.RoleCode,RoleName,Statue,
                            convert(nvarchar(50),a.BeginTime,121) AS BeginTime,convert(nvarchar(50),a.EndTime,121) AS EndTime,
                            convert(nvarchar(50),PollingDate,121) AS PollingDate,PollingPerson,f.UserName AS PollingPersonName,
                            (SELECT '['+t.RankName+']' FROM Tb_EQ_EqRank t WHERE charindex(t.RankId,a.EqId)>0 FOR XML PATH('')) AS RankName 
                        FROM Tb_EQ_{taskType}Task a
                        LEFT OUTER JOIN Tb_HSPR_Community b ON b.CommId = a.CommID
                        LEFT OUTER JOIN Tb_EQ_{taskType}PollingPlan c ON c.Id = a.PlanId
                        LEFT OUTER JOIN Tb_EQ_Space d ON d.SpaceId = a.SpaceId
                        LEFT OUTER JOIN Tb_Sys_Role e ON e.RoleCode = a.RoleCode
                        LEFT OUTER JOIN Tb_Sys_User f ON f.UserCode = a.PollingPerson
                        WHERE a.RoleCode IN (
                            SELECT SysRoleCode FROM Tb_Sys_Role 
                            WHERE RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{Global_Var.LoginUserCode}')
                            UNION
                            SELECT RoleCode AS SysRoleCode FROM Tb_Sys_UserRole WHERE UserCode='{Global_Var.LoginUserCode}'
                        )
                        AND a.CommID={commId} AND a.BeginTime<=getdate() AND a.EndTime>=getdate()
                        AND a.Statue!='已完成' AND a.Statue!='已关闭' AND isnull(a.IsDelete,0)=0";

                if (isJinHuiEquipmentSystem)
                {
                    sql += "";
                }

                try
                {
                    var taskResult = conn.Query<EquipmentTaskModel>(sql, null, null, false, null, CommandType.Text).ToList();

                    for (int i = 0, j = taskResult.Count(); i < j; i++)
                    {
                        // 默认需要分页
                        var pager = 1;
                        var pageSize = 0;
                        var lineCount = 0;
                        var taskInfo = taskResult[i];

                        var equipments = GetTaskEquipments(commId, taskType, taskInfo.TaskId, out pager, out pageSize, out lineCount);
                        taskInfo.Equipments = new List<EquipmentModel>();
                        taskInfo.Equipments.AddRange(equipments);
                        taskInfo.LinePager = pager;
                        taskInfo.LinePageSize = pageSize;
                        taskInfo.LineCount = lineCount;
                    }

                    return new ApiResult(true, taskResult).toJson();
                }
                catch (Exception ex)
                {
                    return new ApiResult(false, ex.Message).toJson();
                }
            }
        }

        /// <summary>
        /// 下载任务
        /// </summary>
        private string DownloadTaskWithPager(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("TaskType") || string.IsNullOrEmpty(row["TaskType"].ToString()))
            {
                return JSONHelper.FromString(false, "任务类型不能为空");
            }

            var taskType = row["TaskType"].ToString();
            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            var pageSize = 10;
            var pageIndex = 1;
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 设备巡检 or 设备维保
                taskType = taskType == "1" ? TASK_SUFFIX_INSPECTION : TASK_SUFFIX_MAINTENANCE;

                // 金辉版本设备，多一个doMonth字段
                var sql = $"SELECT count(1) FROM syscolumns WHERE id=object_id('Tb_EQ_{taskType}PollingPlan') AND name='doMonth'";
                var isJinHuiEquipmentSystem = (conn.Query<int>(sql).FirstOrDefault() > 0);

                sql = $@"SELECT * FROM 
                        (
	                        SELECT row_number() OVER(ORDER BY a.EndTime DESC) AS RN,
                                TaskNO,TaskId,PlanName,a.SpaceId,MacRoName,MacRoAddress,a.Content,a.CommId,CommName,a.RoleCode,RoleName,Statue,
                                convert(nvarchar(50),a.BeginTime,121) AS BeginTime,convert(nvarchar(50),a.EndTime,121) AS EndTime,
                                convert(nvarchar(50),PollingDate,121) AS PollingDate,PollingPerson,f.UserName AS PollingPersonName,
                                (SELECT '['+t.RankName+']' FROM Tb_EQ_EqRank t WHERE charindex(t.RankId,a.EqId)>0 FOR XML PATH('')) AS RankName 
                            FROM Tb_EQ_{taskType}Task a WITH(NOLOCK)
                            LEFT OUTER JOIN Tb_HSPR_Community b ON b.CommId = a.CommID
                            LEFT OUTER JOIN Tb_EQ_{taskType}PollingPlan c ON c.Id = a.PlanId
                            LEFT OUTER JOIN Tb_EQ_Space d ON d.SpaceId = a.SpaceId
                            LEFT OUTER JOIN Tb_Sys_Role e ON e.RoleCode = a.RoleCode
                            LEFT OUTER JOIN Tb_Sys_User f ON f.UserCode = a.PollingPerson
                            WHERE a.RoleCode IN (
                                SELECT SysRoleCode FROM Tb_Sys_Role 
                                    WHERE RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{Global_Var.LoginUserCode}')
                                UNION
                                SELECT RoleCode AS SysRoleCode FROM Tb_Sys_UserRole WHERE UserCode='{Global_Var.LoginUserCode}'
                            )
                            AND a.CommID={commId} AND a.BeginTime<=getdate() AND a.EndTime>=getdate()
                            AND a.Statue!='已完成' AND a.Statue!='已关闭' AND isnull(a.IsDelete,0)=0
                        ) AS t
                        WHERE RN BETWEEN {pageSize}*({pageIndex}-1)+1 AND {pageSize}*{pageIndex}";

                if (isJinHuiEquipmentSystem)
                {
                    sql += "";
                }

                try
                {
                    var taskResult = conn.Query<EquipmentTaskModel>(sql).ToList();

                    for (int i = 0, j = taskResult.Count(); i < j; i++)
                    {
                        // 默认需要分页
                        var pager = 1;
                        var lineCount = 0;
                        var taskInfo = taskResult[i];

                        var equipments = GetTaskEquipments(commId, taskType, taskInfo.TaskId, out pager, out pageSize, out lineCount);
                        taskInfo.Equipments = new List<EquipmentModel>();
                        taskInfo.Equipments.AddRange(equipments);
                        taskInfo.LinePager = pager;
                        taskInfo.LinePageSize = pageSize;
                        taskInfo.LineCount = lineCount;
                    }

                    return new ApiResult(true, taskResult).toJson();
                }
                catch (Exception ex)
                {
                    return new ApiResult(false, ex.Message).toJson();
                }
            }
        }

        /// <summary>
        /// 分页下载任务下标准
        /// </summary>
        private string DownloadTaskEquipmentLineWithPager(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            if (!row.Table.Columns.Contains("TaskId") || string.IsNullOrEmpty(row["TaskId"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("TaskType") || string.IsNullOrEmpty(row["TaskType"].ToString()))
            {
                return JSONHelper.FromString(false, "任务类型不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var taskId = row["TaskId"].ToString();
            var taskType = row["TaskType"].ToString();
            // 设备巡检 or 设备维保
            taskType = (taskType == "1" ? TASK_SUFFIX_INSPECTION : TASK_SUFFIX_MAINTENANCE);

            var pageSize = TASK_EUIQPMENT_LINE_PAGE_SIZE;
            var pageIndex = 1;
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            var result = GetTaskEquipmentLine(taskType, taskId, pageSize, pageIndex);

            return new ApiResult(true, result).toJson();
        }

        // 获取任务下设备信息，包括报事、文件，可能包括设备下工艺线路信息
        private IEnumerable<EquipmentModel> GetTaskEquipments(int commId, string taskType, string taskId,
            out int pager, out int pageSize, out int lineCount, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            // 工艺路线
            var lineResult = new List<EquipmentLineModel>();

            // 查询设备下工艺线路，根据工艺线路数量，判断是否需要分页
            var sql = $@"SELECT count(1) FROM Tb_Eq_{taskType}TaskEquipmentLine
                            WHERE isnull(IsDelete,0)=0 AND TaskId='{taskId}';";
            lineCount = conn.Query<int>(sql, null, trans, false).FirstOrDefault();

            //if (lineCount <= TASK_EUIQPMENT_LINE_PAGE_SIZE)
            //{
            //    // 设备工艺线路总数小于预定值，此时不需要分页
            //    pager = 0;
            //    pageSize = 0;
            //    sql = $@"SELECT a.TaskId,a.EquiId,a.TaskLineId,a.PollingNote,a.ReferenceValue,b.InputType,b.InputTypeIsControl,
            //            b.CheckType,b.NumType,dbo.EQ_TaskEquipment(a.TextValue,a.NumValue,a.ChooseValue) as Value 
            //            FROM Tb_EQ_{taskType}TaskEquipmentLine a 
            //            LEFT JOIN Tb_EQ_{taskType}PollingPlanDetail b ON a.DetailId=b.DetailId AND a.EquiId=b.EquiId AND a.StanId=b.StanId
            //            WHERE TaskId=@TaskId AND isnull(a.IsDelete,0)=0 ";

            //    // 无需翻页时，直接读取工艺路线
            //    lineResult = conn.Query<EquipmentLineModel>(sql, new { CommID = commId, TaskId = taskId }, trans, false).ToList();
            //}
            //else
            {
                pager = 1;
                pageSize = TASK_EUIQPMENT_LINE_PAGE_SIZE;
            }

            // 任务下设备、任务下设备下工艺路线、任务下设备下报事、文件
            sql = $@"/* 查询任务下设备，此处只查询未检查的设备 */
                        SELECT a.TaskEqId,a.TaskId,a.EquiId,b.EquipmentNO,b.EquipmentName AS EquiIdName,a.PollingNote,
                        a.PollingResult,a.IsHandle,a.HandlePId,c.UserName AS HandlePName,a.IsDelete,a.AddDate
                        FROM Tb_EQ_{taskType}TaskEquipment a 
                        LEFT JOIN Tb_EQ_Equipment b on a.EquiId=b.Id
                        LEFT JOIN Tb_Sys_User c ON a.HandlePId=c.UserCode
                        WHERE a.TaskId='{taskId}' AND isnull(a.IsDelete,0)=0;";

            var equipmentResult = conn.Query<EquipmentModel>(sql, null, trans, false).ToList();
            for (int i = 0, j = equipmentResult.Count(); i < j; i++)
            {
                equipmentResult[i].Lines = new List<EquipmentLineModel>();
            }

            // 工艺路线不分页，筛选任务下设备下工艺路线
            //if (lineResult != null && lineResult.Count() > 0)
            //{
            //    foreach (EquipmentModel equipment in equipmentResult)
            //    {
            //        var tmpLines = lineResult.ToList().FindAll(l => (l.EquiId == equipment.EquiId));
            //        lineResult = lineResult.Except(tmpLines).ToList();

            //        equipment.Lines = tmpLines;
            //    }
            //}

            if (db == null)
                conn.Dispose();

            return equipmentResult;
        }

        // 分页获取任务下设备下工艺路线
        private IEnumerable<EquipmentLineModel> GetTaskEquipmentLine(string taskType, string taskId, int pageSize, int pageIndex,
            IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var sql = $@"SELECT a.TaskId,a.EquiId,a.TaskLineId,a.PollingNote,a.ReferenceValue,b.InputType,b.InputTypeIsControl,
                        b.CheckType,isnull(b.NumType,0) AS NumType,dbo.EQ_TaskEquipment(a.TextValue,a.NumValue,a.ChooseValue) as Value,b.Sort 
                        FROM Tb_EQ_{taskType}TaskEquipmentLine a 
                        LEFT JOIN Tb_EQ_{taskType}PollingPlanDetail b ON a.DetailId=b.DetailId AND a.EquiId=b.EquiId AND a.StanId=b.StanId
                        WHERE TaskId='{taskId}' AND isnull(a.IsDelete,0)=0 ";

            var result = GetListDapper<EquipmentLineModel>(out int pageCount, out int count, sql, pageIndex, pageSize,
                "Sort", 1, "TaskLineId", conn, trans);

            if (db == null)
                conn.Dispose();

            return result;
        }

        class EquipmentTaskModel
        {
            public int TaskType { get; set; }
            public string TaskId { get; set; }
            public string TaskNO { get; set; }
            public string PlanName { get; set; }
            public string CommId { get; set; }
            public string CommName { get; set; }
            public string SpaceId { get; set; }
            public string MacRoName { get; set; }
            public string MacRoAddress { get; set; }
            public string Content { get; set; }
            public string RoleCode { get; set; }
            public string RoleName { get; set; }
            public string Statue { get; set; }
            public string BeginTime { get; set; }
            public string EndTime { get; set; }
            public string PollingDate { get; set; }
            public string PollingPerson { get; set; }
            public string PollingPersonName { get; set; }
            public string RankName { get; set; }
            public int LinePager { get; set; }
            public int LinePageSize { get; set; }
            public int LineCount { get; set; }

            public List<EquipmentModel> Equipments { get; set; }
        }

        class EquipmentModel
        {
            public string TaskEqId { get; set; }
            public string TaskId { get; set; }
            public string EquiId { get; set; }
            public string EquipmentNO { get; set; }
            public string EquiIdName { get; set; }
            public string PollingNote { get; set; }
            public string PollingResult { get; set; }
            public int IsHandle { get; set; }
            public string HandlePId { get; set; }
            public string HandlePName { get; set; }
            public int IsDelete { get; set; }
            public string AddDate { get; set; }

            public List<EquipmentLineModel> Lines { get; set; }
        }

        class EquipmentLineModel
        {
            public string TaskId { get; set; }
            public string EquiId { get; set; }
            public string TaskLineId { get; set; }
            public string PollingNote { get; set; }
            public string ReferenceValue { get; set; }
            public int InputType { get; set; }
            public int InputTypeIsControl { get; set; }
            public int CheckType { get; set; }
            public string NumType { get; set; }
            public string Value { get; set; }

            [JsonIgnore]
            public string Sort { get; set; }
        }
    }
}
