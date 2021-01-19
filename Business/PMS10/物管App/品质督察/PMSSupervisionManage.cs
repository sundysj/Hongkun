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
    public partial class PMSSupervisionManage : PubInfo
    {
        class TaskStandardScoreModel
        {
            public string TaskStandardId { get; set; }
            public float DeductionScore { get; set; }
            public string CheckDate { get; set; }
            public List<TaskStandardDeductionsModel> Deductions { get; set; }
        }

        class TaskStandardDeductionsModel
        {
            public string TaskDeductionItemId { get; set; }
            public string CheckPlace { get; set; }
            public string Content { get; set; }
            public float DeductionScore { get; set; }
            public string ProblemId { get; set; }
            public string ProblemName { get; set; }
            public string IsNeedRectification { get; set; }
            public string RectificationExplain { get; set; }
            public string RectificationTimeLimit { get; set; }
            public string AddDate { get; set; }
            public string EditState { get; set; } //(0未做任何修改，1新增，2删除，3修改)
            public List<File> Files { get; set; }
        }

        class File
        {
            public string FileId { get; set; }
            public string FilePath { get; set; }
            public string AddDate { get; set; }
            public string EditState { get; set; } //(0未做任何修改，1新增，2删除)
        }


        /// <summary>
        /// 任务相关默认分页页长
        /// </summary
        private static int TASK_POINT_STANDARD_PAGE_SIZE = 300;
#pragma warning disable CS0414 // 字段“PMSSupervisionManage.TASK_POINT_UPLOAD_PAGE_SIZE”已被赋值，但从未使用过它的值
        private static int TASK_POINT_UPLOAD_PAGE_SIZE = 10;
#pragma warning restore CS0414 // 字段“PMSSupervisionManage.TASK_POINT_UPLOAD_PAGE_SIZE”已被赋值，但从未使用过它的值

        public PMSSupervisionManage()
        {
            base.Token = "20200507PMSSupervisionManage";
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

                    case "DownloadQuestionType":                        // 下载问题类型
                        Trans.Result = DownloadQuestionType(Row);
                        break;
                    case "DownloadTask":                                // 下载任务数据
                        Trans.Result = DownloadTask(Row);
                        break;
                    case "DownloadTaskStandardWithPager":               // 分页下载任务标准
                        Trans.Result = DownloadTaskStandardWithPager(Row);
                        break;
                    case "DownloadTaskDeductionWithPager":               // 分页下载任务标准加扣分项
                        Trans.Result = DownloadTaskDeductionWithPager(Row);
                        break;
                    case "UploadTaskData":                              // 上传任务数据，用于离线任务
                        Trans.Result = UploadTaskData(Row);
                        break;
                    case "SearchTask":                                  // 品质督查跟进
                        Trans.Result = SearchTask(Row);
                        break;
                    case "CloseTask":                                  // 品质督查跟进关闭
                        Trans.Result = CloseTask(Row);
                        break;
                    case "HandoverTask":                                // 品质督查变更责任岗位/责任人
                        Trans.Result = HandoverTask(Row);
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
        /// 下载问题类型
        /// </summary>
        private string DownloadQuestionType(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT Id,Name FROM Tb_Supervision_Dictionary WHERE DType='问题类型' AND ISNULL(IsDelete,0)=0";

                var data = conn.Query(sql);

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 下载任务
        /// </summary>
        private string DownloadTask(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            using (IDbConnection conn0 = new SqlConnection(PubConstant.hmWyglConnectionString),
                                 conn1 = new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_SUPERVISION)))
            {
                var sql = "";

                // 任务列表
                // 1、明确指定责任人，只查询责任人
                // 2、未指定责任人，筛选具体岗位
                // 3、未指定责任人，筛选通用岗位
                sql += $@"SELECT a.TaskId FROM Tb_Supervision_Task a WITH(NOLOCK)
                        WHERE isnull(a.IsClose,0)=0 AND TaskState<>2 AND a.CommID=@CommID
                        AND a.TaskBeginTime<=getdate() AND a.TaskEndTime>=getdate()
                        AND
                        (
                            charindex(@UserCode,a.TaskUserCode)>0
                            OR
                            (isnull(TaskUserCode,'')=''
                            AND (SELECT count(t.RoleCode) AS RoleCode FROM (
                                    SELECT Value AS RoleCode FROM { Global_Var.ERPDatabaseName }.dbo.SplitString(a.TaskRoleCode,',',1)
                                    InterSect
                                    SELECT RoleCode FROM { Global_Var.ERPDatabaseName }.dbo.Tb_Sys_UserRole WHERE UserCode=@UserCode
                                ) AS T)>0)
                            OR
                            (isnull(a.TaskUserCode,'')=''
                            AND (SELECT count(U.RoleCode) AS RoleCode
                                 FROM (SELECT RoleCode FROM  { Global_Var.ERPDatabaseName }.dbo.Tb_Sys_RoleData WHERE CommId=@CommID
                                     AND RoleCode IN (SELECT RoleCode FROM  { Global_Var.ERPDatabaseName }.dbo.Tb_Sys_Role
                                                      WHERE SysRoleCode IN (SELECT Value AS RoleCode
                                                                            FROM  { Global_Var.ERPDatabaseName }.dbo.SplitString(a.TaskRoleCode,',',1)))
                                    InterSect
                                    SELECT RoleCode FROM  { Global_Var.ERPDatabaseName }.dbo.Tb_Sys_UserRole WHERE UserCode=@UserCode)
                                AS U)>0
                            )
                        )";

                sql = $@"SELECT TaskId,TaskNO,CommName,a.CommId,isnull(b.IsScore,0) AS IsScore,
                            CheckTypeName,CheckTypeId,PlanName,
                            TaskSource,TaskBeginTime,TaskEndTime,DidBeginTime,DidEndTime,DidEndUserCode,DidEndUserName,
                            (SELECT count(1) FROM Tb_Supervision_TaskStandard WITH(NOLOCK) WHERE TaskId=a.TaskId) AS StandardNum,
                            isnull(CompositeScore,0) AS CompositeScore,isnull(TaskState,0) AS TaskState,
                            TaskRoleCode,TaskRoleName,TaskUserCode,TaskUserName,b.ScoreType,
                            1 AS StandardPager,
                            { TASK_POINT_STANDARD_PAGE_SIZE } AS StandardPageSize,
                            (SELECT count(1) 
                                FROM Tb_Supervision_TaskStandard WITH(NOLOCK)
                                WHERE TaskId=a.TaskId AND CheckDate IS NULL
                            ) AS StandardCount,
                            { TASK_POINT_STANDARD_PAGE_SIZE } AS DeductionPageSize,
                            (SELECT count(1) 
                                FROM Tb_Supervision_TaskDeductionItem WITH(NOLOCK)
                                WHERE TaskId=a.TaskId AND isnull(IsDelete,0)=0
                            ) AS DeductionNum

                        FROM Tb_Supervision_Task a WITH(NOLOCK)
                        LEFT JOIN { Global_Var.ERPDatabaseName }.dbo.Tb_Supervision_CheckType b ON a.CheckTypeId=b.Id
                        WHERE TaskId IN ({sql}) ORDER BY TaskEndTime";

                try
                {
                    var data = conn1.Query(sql, new
                    {
                        CommID = commId,
                        UserCode = Global_Var.LoginUserCode
                    }).ToList();


                    foreach (dynamic taskInfo in data)
                    {
                        // 获取任务下的所有检查项目
                        sql = $@"SELECT DISTINCT
                                    CASE t.CheckMethod
                                    WHEN 1 THEN t.ProfessionalLineScoreItem
                                    WHEN 2 THEN t.CategoryDimensionScoreItem
                                    WHEN 3 THEN t.DepartmentScoreItem
                                    ELSE '' END AS ScoreItemName,

                                    CASE t.CheckMethod
                                    WHEN 1 THEN t.ProfessionalLine
                                    WHEN 2 THEN t.CategoryDimension
                                    WHEN 3 THEN t.Department
                                    ELSE '' END AS ScoreItemID,

                                    t.TaskId,
                                    t.CheckItemName,
                                    t.CheckItemID,
                                    t.Sort
                                FROM
                                (
                                    SELECT a.TaskId, b.CheckMethod, c.ProfessionalLine, c.CategoryDimension, c.Department,
                                           d.ScoreItem AS ProfessionalLineScoreItem,
                                           e.ScoreItem AS CategoryDimensionScoreItem,
                                           f.ScoreItem AS DepartmentScoreItem,
                                           c.ItemNameAll AS CheckItemName,
                                           c.ItemId AS CheckItemID,g.Sort
                                    FROM Tb_Supervision_Task a WITH(NOLOCK)
                                    LEFT JOIN { Global_Var.ERPDatabaseName }.dbo.Tb_Supervision_Plan b ON a.PlanId = b.Id
                                    LEFT JOIN Tb_Supervision_TaskStandard c WITH(NOLOCK) ON a.TaskId = c.TaskId
                                    LEFT JOIN { Global_Var.ERPDatabaseName }.dbo.Tb_Supervision_CheckStandard_ScoreItem d ON d.Id = c.ProfessionalLine
                                    LEFT JOIN { Global_Var.ERPDatabaseName }.dbo.Tb_Supervision_CheckStandard_ScoreItem e ON e.Id = c.CategoryDimension
                                    LEFT JOIN { Global_Var.ERPDatabaseName }.dbo.Tb_Supervision_CheckStandard_ScoreItem f ON f.Id = c.Department
                                    LEFT JOIN { Global_Var.ERPDatabaseName }.dbo.Tb_Supervision_CheckStandard_CheckItem g ON c.ItemId = g.Id
                                    WHERE a.CommId=@CommId AND a.TaskBeginTime<=getdate() AND a.TaskEndTime>=getdate()
                                    AND a.TaskId=@TaskId
                                    ORDER BY g.Sort ASC 
                                ) AS t";
                        //需求9900 隆基泰和 根据Sort升序 ORDER BY g.Sort ASC

                        var checkItems = conn1.Query(sql, new { TaskId = taskInfo.TaskId, CommId = taskInfo.CommId });

                        var scoreItems = new List<dynamic>();

                        foreach (dynamic checkItem in checkItems)
                        {
                            var scoreTmp = scoreItems.Find(obj => obj.ScoreItemID == checkItem.ScoreItemID);

                            if (scoreTmp == null)
                            {
                                scoreTmp = new
                                {
                                    TaskId = taskInfo.TaskId,
                                    ScoreItemID = checkItem.ScoreItemID,
                                    ScoreItemName = checkItem.ScoreItemName,
                                    CheckItems = new List<dynamic>()
                                };

                                scoreItems.Add(scoreTmp);
                            }

                            scoreTmp.CheckItems.Add(checkItem);
                        }

                        taskInfo.ScoreItems = scoreItems;
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
        /// 分页下载任务标准
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

            var taskId = row["TaskId"].ToString();
            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            var pageSize = TASK_POINT_STANDARD_PAGE_SIZE;
            var pageIndex = 1;
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            var connStr = Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_SUPERVISION);
            using (var conn = new SqlConnection(connStr))
            {
                var sql = $@"SELECT * FROM (
                                SELECT ROW_NUMBER() OVER(ORDER BY a.TaskStandardId) AS RN,
                                CASE c.CheckMethod
                                    WHEN 1 THEN a.ProfessionalLine
                                    WHEN 2 THEN a.CategoryDimension
                                    WHEN 3 THEN a.Department
                                    ELSE '' END AS ScoreItemID,
                                    a.TaskStandardId,a.TaskId,a.ItemId,a.ItemNameAll,a.StandardId,a.Content,a.CheckScenario,a.CheckMethod,
                                    a.SamplingStandard,a.MarkStandard,a.StandardScore,a.IsAllowFloat,a.MaximumFloatingScore,a.DeductionNature,
                                    a.ProfessionalLine,a.CategoryDimension,a.Department,a.DeductionScore,a.CheckUserCode,a.CheckUserName,
                                    a.CheckDate,a.IsDiscard,'' AS Remark, '' AS Img,
                                    g.Sort
                                FROM Tb_Supervision_TaskStandard a WITH(NOLOCK)
                                LEFT JOIN Tb_Supervision_Task b ON a.TaskId=b.TaskId
                                LEFT JOIN { Global_Var.ERPDatabaseName }.dbo.Tb_Supervision_Plan c ON b.PlanId=c.Id
                                LEFT JOIN { Global_Var.ERPDatabaseName }.dbo.Tb_Supervision_CheckStandard_ScoreItem d ON d.Id = a.ProfessionalLine
                                LEFT JOIN { Global_Var.ERPDatabaseName }.dbo.Tb_Supervision_CheckStandard_ScoreItem e ON e.Id = a.CategoryDimension
                                LEFT JOIN { Global_Var.ERPDatabaseName }.dbo.Tb_Supervision_CheckStandard_ScoreItem f ON f.Id = a.Department
                                LEFT JOIN { Global_Var.ERPDatabaseName }.dbo.Tb_Supervision_CheckStandard_CheckItem g ON a.ItemId=g.Id
                                WHERE a.TaskId='{ taskId }'
                                ORDER BY g.Sort ASC
                            ) t
							WHERE t.RN BETWEEN ({ pageIndex }-1)*{ pageSize }+1 AND { pageIndex }*{ pageSize }";

                var data = conn.Query(sql).ToList();

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 分页下载标准的加扣分项
        /// </summary>
        private string DownloadTaskDeductionWithPager(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }
            if (!row.Table.Columns.Contains("TaskId") || string.IsNullOrEmpty(row["TaskId"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }

            var taskId = row["TaskId"].ToString();
            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            var pageSize = TASK_POINT_STANDARD_PAGE_SIZE;
            var pageIndex = 1;
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            var connStr = Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_SUPERVISION);
            using (var conn = new SqlConnection(connStr))
            {
                var sql = $@"SELECT * FROM (
                                SELECT ROW_NUMBER() OVER(ORDER BY a.TaskDeductionItemId) AS RN,
                                a.*
                                FROM Tb_Supervision_TaskDeductionItem a WITH(NOLOCK)
                                WHERE a.TaskId='{ taskId }'
                            ) t
							WHERE t.RN BETWEEN ({ pageIndex }-1)*{ pageSize }+1 AND { pageIndex }*{ pageSize }";

                var data = conn.Query(sql).ToList();


                foreach (var deductionItem in data)
                {
                    var keyId = deductionItem.TaskDeductionItemId;
                    sql = $@"SELECT a.* FROM Tb_Supervision_TaskFiles a WITH(NOLOCK) WHERE TaskId='{ taskId }' AND KeyId='{ keyId }'";

                    var files = conn.Query(sql).ToList();
                    deductionItem.Files = files;
                }

                return new ApiResult(true, data).toJson();
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
            var taskDidBeginTime = data["DidBeginTime"].ToString();     // 任务开始执行时间
            var taskDidEndTime = data["DidEndTime"].ToString();         // 任务完成时间
            var didEndUserCode = data["DidEndUserCode"].ToString();     // 任务完成人Code
            var didEndUserName = data["DidEndUserName"].ToString();     // 任务完成人名称
            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            var connStr = Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_SUPERVISION);
            using (var conn = new SqlConnection(connStr))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var trans = conn.BeginTransaction();

                try
                {
                    var sql = $@"SELECT count(1) FROM Tb_Supervision_Task WITH(NOLOCK) 
                                WHERE TaskId='{ taskId }' AND isnull(IsClose,0)=0;";

                    if (conn.Query<int>(sql, null, trans).FirstOrDefault() == 0)
                    {
                        trans.Rollback();
                        return JSONHelper.FromString(true, "该任务已关闭或已删除");
                    }

                    // 更新任务标准
                    var standardTable = JsonConvert.DeserializeObject<List<TaskStandardScoreModel>>(data["Standards"].ToString());
                    UpdateSupervisionTaskStandard(standardTable, commId, taskId, conn, trans);

                    // 执行事务
                    trans.Commit();
                    trans = conn.BeginTransaction();

                    // 更新任务状态
                    UpdateSupervisionTask(commId, taskId, taskState, taskDidBeginTime, taskDidEndTime, didEndUserCode, didEndUserName, conn, trans);

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
        /// 更新检查标准
        /// </summary>
        private bool UpdateSupervisionTaskStandard(List<TaskStandardScoreModel> standardValue, int commId, string taskId, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (standardValue.Count == 0)
                return true;

            var conn = db ?? new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_SUPERVISION));

            var sql = "";
            foreach (var standardItem in standardValue)
            {
                var taskStandardId = standardItem.TaskStandardId;
                var deductionScore = standardItem.DeductionScore;
                var checkDate = standardItem.CheckDate;

                // 更新任务标准数据
                sql += $@"UPDATE Tb_Supervision_TaskStandard 
                            SET DeductionScore='{deductionScore}',
                                CheckUserCode='{Global_Var.LoginUserCode}',
                                CheckUserName='{Global_Var.LoginUserName}',
                                CheckDate='{checkDate}'
                            WHERE TaskId='{taskId}' AND TaskStandardId='{taskStandardId}';";

                for (int i = 0; i < standardItem.Deductions.Count; i++)
                {
                    var deductionItem = standardItem.Deductions[i];
                    var taskDeductionItemId = deductionItem.TaskDeductionItemId;
                    var editState = deductionItem.EditState;
                    bool taskDeductionIsDelete = false;
                    var condition = $@"SELECT count(1) as Count FROM Tb_Supervision_TaskDeductionItem WHERE TaskDeductionItemId='{deductionItem.TaskDeductionItemId}';";

                    // (0未做任何修改，1新增，2删除，3修改)
                    switch (editState)
                    {
                        case "0":
                            sql += "";
                            break;
                        case "1":

                            /* 注释了IF NOT exists(SELECT count(1) FROM Tb_Supervision_TaskDeductionItem 
                            WHERE TaskDeductionItemId = '{deductionItem.TaskDeductionItemId}')*/
                            // 判断数据库是否有值
                            if (conn.Query<int>(condition, null, trans).FirstOrDefault() == 0)
                            {
                                sql += $@"INSERT INTO Tb_Supervision_TaskDeductionItem
                                      VALUES ('{deductionItem.TaskDeductionItemId}',
                                            '{taskId}',
                                            '{taskStandardId}',{i},
                                            '{deductionItem.CheckPlace}',
                                            '{deductionItem.Content}',
                                            {deductionItem.DeductionScore},
                                            '{deductionItem.ProblemId}',
                                            '{deductionItem.ProblemName}',
                                            {deductionItem.IsNeedRectification},
                                            '{deductionItem.RectificationExplain}',
                                            '{deductionItem.RectificationTimeLimit}',
                                            '{Global_Var.LoginUserCode}',
                                            '{Global_Var.LoginUserName}',
                                            '{deductionItem.AddDate}',
                                            0,NULL,NULL);";
                            }
                            break;
                        case "2":
                            sql += $@"DELETE FROM Tb_Supervision_TaskDeductionItem WHERE TaskDeductionItemId='{deductionItem.TaskDeductionItemId}';";
                            taskDeductionIsDelete = true;
                            break;
                        case "3":
                            // 判断数据库是否有值
                            if (conn.Query<int>(condition, null, trans).FirstOrDefault() > 0)
                            {
                                sql += $@"UPDATE Tb_Supervision_TaskDeductionItem 
                                      SET
                                        CheckPlace = '{deductionItem.CheckPlace}',
                                        Content = '{deductionItem.Content}',
                                        DeductionScore = {deductionItem.DeductionScore},
                                        ProblemId = '{deductionItem.ProblemId}',
                                        ProblemName = '{deductionItem.ProblemName}',
                                        IsNeedRectification = {deductionItem.IsNeedRectification},
                                        RectificationExplain = '{deductionItem.RectificationExplain}',
                                        RectificationTimeLimit = '{deductionItem.RectificationTimeLimit}',
                                        AddUserCode = '{Global_Var.LoginUserCode}',
                                        AddUserName = '{Global_Var.LoginUserName}',
                                        AddDate = '{deductionItem.AddDate}'
                                      WHERE TaskDeductionItemId = '{deductionItem.TaskDeductionItemId}';";
                            }
                            else
                            {
                                sql += $@"INSERT INTO Tb_Supervision_TaskDeductionItem
                                          VALUES ('{deductionItem.TaskDeductionItemId}',
                                                '{taskId}',
                                                '{taskStandardId}',{i},
                                                '{deductionItem.CheckPlace}',
                                                '{deductionItem.Content}',
                                                {deductionItem.DeductionScore},
                                                '{deductionItem.ProblemId}',
                                                '{deductionItem.ProblemName}',
                                                {deductionItem.IsNeedRectification},
                                                '{deductionItem.RectificationExplain}',
                                                '{deductionItem.RectificationTimeLimit}',
                                                '{Global_Var.LoginUserCode}',
                                                '{Global_Var.LoginUserName}',
                                                '{deductionItem.AddDate}',
                                                0,NULL,NULL);";
                            }
                            break;
                    }

                    // 保存加扣分项图片
                    SaveTaskDeductionsFiles(deductionItem.Files, commId, taskId, taskDeductionItemId, taskDeductionIsDelete, conn, trans);
                }
            }
            conn.Execute(sql, null, trans);

            if (db == null)
                conn.Dispose();

            return true;
        }

        // 保存加扣分项文件
        private bool SaveTaskDeductionsFiles(List<File> standardFiles, int commId, string taskId, string taskDeductionItemId, bool taskDeductionIsDelete,
            IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_SUPERVISION));

            var sql = "";

            if (taskDeductionIsDelete)
            {
                sql = $@"DELETE FROM Tb_Supervision_TaskFiles WHERE KeyId='{taskDeductionItemId}';";
                conn.Execute(sql, null, trans);
            }
            else
            {
                foreach (var file in standardFiles)
                {
                    var fileId = file.FileId;
                    var filePath = file.FilePath;
                    var fileName = Path.GetFileNameWithoutExtension(filePath);
                    var fix = Path.GetExtension(filePath);
                    var addDate = file.AddDate;

                    // (0未做任何修改，1新增，2删除)
                    switch (file.EditState)
                    {
                        case "0":
                            break;
                        case "1":
                            var result = conn.Query(@"SELECT * FROM Tb_Supervision_TaskFiles WITH(NOLOCK) WHERE FilePath=@FilePath;",
                            new { FilePath = filePath }, trans).Count() > 0;

                            if (result == true)
                            {
                                continue;
                            }

                            sql = @"INSERT INTO Tb_Supervision_TaskFiles
                                    VALUES(@FileId,@TaskId,@TaskDeductionItemId,'加扣分项',@FileName,@Fix,@FilePath,@AddUserCode,@AddUserName,@AddDate,0,NULL,NULL);";
                            int count = conn.Execute(sql, new
                            {
                                FileId = fileId,
                                TaskId = taskId,
                                TaskDeductionItemId = taskDeductionItemId,
                                FileName = fileName,
                                Fix = fix,
                                FilePath = filePath,
                                AddUserCode = Global_Var.LoginUserCode,
                                AddUserName = Global_Var.LoginUserName,
                                AddDate = addDate
                            }, trans);

                            if (count == 0)
                            {
                                throw new Exception($"保存任务点位文件出错，TaskDeductionItemId={taskDeductionItemId}");
                            }
                            break;
                        case "2":
                            sql = $@"DELETE FROM Tb_Supervision_TaskFiles WHERE FileId='{fileId}';";
                            conn.Execute(sql, null, trans);
                            break;
                    }
                }
            }

            if (db == null)
                conn.Dispose();

            return true;
        }

        /// <summary>
        /// 更新任务
        /// </summary>
        private bool UpdateSupervisionTask(int commId, string taskId, string taskState, string taskDidBeginTime,
                                           string taskDidEndTime, string didEndUserCode, string didEndUserName,
                                           IDbConnection db = null, IDbTransaction trans = null)
        {

            var conn = db ?? new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_SUPERVISION));


            // 读取当前任务下的标准数量
            var sql = @"SELECT count(*) AS AllCount FROM Tb_Supervision_TaskStandard WHERE TaskId=@TaskId;

                        SELECT count(*) AS CompletedCount FROM Tb_Supervision_TaskStandard WHERE CheckDate IS NOT NULL AND TaskId=@TaskId;";

            var reader = conn.QueryMultiple(sql, new { TaskId = taskId }, trans);
            var allCount = reader.Read<int>().First();
            var completedCount = reader.Read<int>().First();
            if (taskState == @"1" &&
                allCount == completedCount)
            {
                taskState = @"2";
            }

            // 任务完成了
            if (taskState == @"2")
            {
                if (string.IsNullOrEmpty(taskDidEndTime))
                {
                    taskDidEndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (string.IsNullOrEmpty(didEndUserCode))
                {
                    didEndUserCode = Global_Var.LoginUserCode;
                }

                if (string.IsNullOrEmpty(didEndUserName))
                {
                    didEndUserName = Global_Var.LoginUserName;
                }

                sql = @"UPDATE Tb_Supervision_Task 
                        SET TaskState=@TaskState,DidBeginTime=@DidBeginTime,DidEndTime=@DidEndTime,
                            DidEndUserCode=@DidEndUserCode,DidEndUserName=@DidEndUserName
                        WHERE TaskId=@TaskId;";
            }
            // 任务执行中
            else
            {
                sql = $@"UPDATE Tb_Supervision_Task SET TaskState=@TaskState,DidBeginTime=@DidBeginTime WHERE TaskId=@TaskId AND TaskState<>2;";

            }

            conn.Execute(sql, new
            {
                TaskId = taskId,
                TaskState = taskState,
                DidBeginTime = taskDidBeginTime,
                DidEndTime = taskDidEndTime,
                DidEndUserCode = didEndUserCode,
                DidEndUserName = didEndUserName
            }, trans);


            if (db == null)
                conn.Dispose();

            return true;
        }


        /// <summary>
        /// 品质督查跟进
        /// </summary>
        private static string[] TaskStatesDefine = new string[] { "已逾期", "未开始", "执行中", "已完成" };

        private string SearchTask(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            if (!row.Table.Columns.Contains("TaskState") || string.IsNullOrEmpty(row["TaskState"].ToString()))
            {
                return JSONHelper.FromString(false, "任务状态不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var taskState = row["TaskState"].ToString();

            var taskNO = default(string);
            var taskUserCode = default(string);
            var taskRoleCode = default(string);
            var taskBeginTime = default(string);
            var taskEndTime = default(string);

            var pageSize = 10;
            var pageIndex = 1;

            if (row.Table.Columns.Contains("TaskNO") && !string.IsNullOrEmpty(row["TaskNO"].ToString()))
            {
                taskNO = row["TaskNO"].ToString();
            }
            if (row.Table.Columns.Contains("TaskUserCode") && !string.IsNullOrEmpty(row["TaskUserCode"].ToString()))
            {
                taskUserCode = row["TaskUserCode"].ToString();
            }
            if (row.Table.Columns.Contains("TaskRoleCode") && !string.IsNullOrEmpty(row["TaskRoleCode"].ToString()))
            {
                taskRoleCode = row["TaskRoleCode"].ToString();
            }
            if (row.Table.Columns.Contains("TaskBeginTime") && !string.IsNullOrEmpty(row["TaskBeginTime"].ToString()))
            {
                taskBeginTime = row["TaskBeginTime"].ToString();
            }
            if (row.Table.Columns.Contains("TaskEndTime") && !string.IsNullOrEmpty(row["TaskEndTime"].ToString()))
            {
                taskEndTime = row["TaskEndTime"].ToString();
            }
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            using (var conn = new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_SUPERVISION)))
            {
                var condition = $" a.CommID={commId} AND isnull(a.IsClose,0)=0";

                if (!string.IsNullOrEmpty(taskNO))
                    condition += $" AND a.TaskNO LIKE '%{taskNO}%' ";

                if (!string.IsNullOrEmpty(taskUserCode))
                    condition += $" AND a.TaskUserCode LIKE '%{taskUserCode}%' ";

                if (!string.IsNullOrEmpty(taskRoleCode))
                    condition += $" AND a.TaskRoleCode LIKE '%{taskRoleCode}%' ";

                if (!string.IsNullOrEmpty(taskBeginTime) && string.IsNullOrEmpty(taskEndTime))
                    condition += $" AND a.TaskBeginTime>=convert(datetime, '{taskBeginTime}')";
                else if (string.IsNullOrEmpty(taskBeginTime) && !string.IsNullOrEmpty(taskEndTime))
                    condition += $" AND a.TaskEndTime<=convert(datetime, convert(varchar(10), '{taskEndTime}')+' 23:59:59')";
                else if (!string.IsNullOrEmpty(taskBeginTime) && !string.IsNullOrEmpty(taskEndTime))
                    condition += $@" AND (
                        (convert(varchar(10), '{taskBeginTime}')+' 23:59:59'>=a.TaskBeginTime AND
                         convert(varchar(10), '{taskEndTime}')+' 23:59:59'<=a.TaskEndTime) OR
                        (convert(varchar(10), '{taskBeginTime}')+' 00:00:00'<=isnull(a.TaskBeginTime,convert(varchar(10), '{taskBeginTime}')+' 00:00:00') AND
                         convert(varchar(10), '{taskEndTime}')+' 00:00:00'>=isnull(a.TaskBeginTime,convert(varchar(10), '{taskBeginTime}')+' 00:00:00')) OR
                        (convert(varchar(10), '{taskEndTime}')+' 00:00:00'>=isnull(a.TaskEndTime,convert(varchar(10), '{taskEndTime}')+' 00:00:00') AND
                         convert(varchar(10), '{taskBeginTime}')+' 00:00:00'<=isnull(a.TaskEndTime,convert(varchar(10), '{taskEndTime}')+' 00:00:00'))
                    ) ";

                var planState = GetPlanState(taskState);

                var sql = $@"SELECT TaskId,TaskNO,CommName,a.CommId,PlanName,TaskSource,
                            TaskBeginTime, TaskEndTime, isnull(CompositeScore,0) AS CompositeScore, TaskRoleName,TaskUserName,
                            CASE
							WHEN a.TaskState<>0 THEN 0 
							WHEN a.AddUserCode='{ Global_Var.LoginUserCode }' THEN 1 
                            WHEN (SELECT count(1) FROM (SELECT value FROM { Global_Var.ERPDatabaseName }.dbo.SplitString(b.AuditingRoleCode, ',',1)
                            INTERSECT
                            (
                                SELECT RoleCode FROM { Global_Var.ERPDatabaseName }.dbo.Tb_Sys_UserRole 
                                WHERE UserCode='{ Global_Var.LoginUserCode }'
                            )) as x)>0 THEN 1
                            ELSE 0 END AS CanClose,
							CASE 
								WHEN a.AddUserCode='{ Global_Var.LoginUserCode }' THEN 1 
								WHEN b.AddUserId='{ Global_Var.LoginUserCode }' THEN 1
                                ELSE 0 END AS CanHandover 
                            FROM Tb_Supervision_Task a WITH(NOLOCK)
                            WHERE {condition} ";

                var taskStateIndex = TaskStatesDefine.ToList().IndexOf(taskState);
                if (taskStateIndex == 0)
                    sql += " AND a.TaskState<>2 AND a.TaskEndTime<=getdate()";
                else
                    sql += $" AND a.TaskState={taskStateIndex - 1}";

                try
                {
                    var resultSet = GetListDapper(out int pageCount, out int count, sql, pageIndex, pageSize, "TaskEndTime", 0, "TaskId", conn);

                    var json = new ApiResult(true, resultSet).toJson();
                    return json.Insert(json.Length - 1, ",\"PageCount\":" + pageCount);
                }
                catch (Exception ex)
                {
                    return new ApiResult(false, ex.Message).toJson();
                }
            }
        }

        private int GetPlanState(string taskState)
        {
            switch (taskState)
            {
                case "已逾期": return -1;
                case "未开始": return 0;
                case "执行中": return 1;
                case "已完成": return 2;
                default: return 0;
            }
        }


        /// <summary>
        /// 关闭任务
        /// </summary>
        private string CloseTask(DataRow row)
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

            if (!row.Table.Columns.Contains("CloseReason") || string.IsNullOrEmpty(row["CloseReason"].ToString()))
            {
                return JSONHelper.FromString(false, "请选择任务关闭原因");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var taskId = row["TaskId"].ToString();
            var taskType = row["TaskType"].ToString();

            var closeReason = row["CloseReason"].ToString();
            var closeRemark = default(string);
            if (row.Table.Columns.Contains("CloseRemark") && !string.IsNullOrEmpty(row["CloseRemark"].ToString()))
            {
                closeRemark = row["CloseRemark"].ToString();
            }

            using (var conn = new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_SUPERVISION)))
            {
                var sql = $@"UPDATE Tb_Supervision_Task SET IsClose=1,CloseDate=getdate(),CloseUserCode=@CloseUserCode,
                            CloseReason=@CloseReason,CloseRemark=@CloseRemark WHERE TaskId=@TaskId";

                int i = conn.Execute(sql, new { CloseUserCode = Global_Var.LoginUserCode, CloseReason = closeReason, CloseRemark = closeRemark, TaskId = taskId });
                if (i == 1)
                    return JSONHelper.FromString(true, "关闭成功");
                return JSONHelper.FromString(false, "关闭失败");
            }
        }


        /// <summary>
        /// 变更责任岗位责任人
        /// </summary>
        private string HandoverTask(DataRow row)
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

            var newUser = default(string);
            var newRole = default(string);
            if (row.Table.Columns.Contains("NewUser") && !string.IsNullOrEmpty(row["NewUser"].ToString()))
            {
                newUser = row["NewUser"].ToString();
            }
            if (row.Table.Columns.Contains("NewRole") && !string.IsNullOrEmpty(row["NewRole"].ToString()))
            {
                newRole = row["NewRole"].ToString();
            }

            if (string.IsNullOrEmpty(newUser) && string.IsNullOrEmpty(newRole))
            {
                return JSONHelper.FromString(false, "责任人和责任岗位不能同时为空");
            }

            using (var conn = new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_SUPERVISION)))
            {
                var sql = $@"DECLARE @OldTaskRoleCode nvarchar(max);
                             DECLARE @OldTaskRoleName nvarchar(max);
                             DECLARE @OldTaskUserCode nvarchar(max);
                             DECLARE @OldTaskUserName nvarchar(max);

                             DECLARE @NewTaskRoleName nvarchar(max);
                             DECLARE @NewTaskUserName nvarchar(max);

                            SELECT @OldTaskRoleCode=TaskRoleCode,@OldTaskRoleName=TaskRoleName,
                                @OldTaskUserCode=TaskUserCode,@OldTaskUserName=TaskUserName  
                            FROM Tb_Supervision_Task WHERE TaskId=@TaskId;

                            UPDATE Tb_Supervision_Task SET TaskUserCode=@NewUser,TaskRoleCode=@NewRole WHERE TaskId=@TaskId;

                            SELECT @NewTaskRoleName=STUFF((SELECT ','+RoleName FROM { Global_Var.ERPDatabaseName }.dbo.Tb_Sys_Role
                            WHERE RoleCode IN(SELECT Value FROM { Global_Var.ERPDatabaseName }.dbo.SplitString(
                                (SELECT TaskRoleCode FROM Tb_Supervision_Task WHERE TaskId=@TaskId),',',1)) FOR XML PATH('')), 1, 1, '');

                            SELECT @NewTaskUserName=STUFF((SELECT ','+UserName FROM { Global_Var.ERPDatabaseName }.dbo.Tb_Sys_User
                            WHERE UserCode IN(SELECT Value FROM { Global_Var.ERPDatabaseName }.dbo.SplitString(
                                (SELECT TaskUserCode FROM Tb_Supervision_Task WHERE TaskId=@TaskId),',',1)) FOR XML PATH('')), 1, 1, '');

                            UPDATE Tb_Supervision_Task SET TaskUserName=@NewTaskUserName,TaskRoleName=@NewTaskRoleName WHERE TaskId=@TaskId;

                            INSERT INTO Tb_Supervision_TaskHandover(IID,TaskId,OldTaskUserCode,OldTaskUserName,OldTaskRoleCode,
                                OldTaskRoleName,NewTaskUserCode,NewTaskUserName,NewTaskRoleCode,NewTaskRoleName,HandoverTime,OperateUser)
                            VALUES(newid(),@TaskId,@OldTaskUserCode,@OldTaskUserName,@OldTaskRoleCode,@OldTaskRoleName,
                                @NewUser,@NewTaskUserName,@NewRole,@NewTaskRoleName,getdate(),@UserCode);";

                int i = conn.Execute(sql, new { NewUser = newUser, NewRole = newRole, TaskId = taskId, UserCode = Global_Var.LoginUserCode });
                if (i == 3)
                    return JSONHelper.FromString(true, "变更成功");

                return JSONHelper.FromString(false, "变更失败");
            }
        }
    }
}


