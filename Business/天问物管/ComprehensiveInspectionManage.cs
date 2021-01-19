using Dapper;
using DapperExtensions;
using log4net;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.HSPR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Business
{
    /// <summary>
    /// 综合巡查
    /// </summary>
    public class ComprehensiveInspectionManage : PubInfo
    {
        public ComprehensiveInspectionManage()
        {
            Token = @"20180126ComprehensiveInspectionManage";
            log = GetLog();
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            try
            {
                Trans.Result = JSONHelper.FromString(false, "未知错误");
                DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];

                bool verifyPassword = true;

                // 荣盛不验证账号密码
                if (Row["Account"].ToString().StartsWith("2036-"))
                {
                    verifyPassword = false;
                }

                //验证登录
                if (!new Login().isLogin(ref Trans, verifyPassword))
                    return;

                switch (Trans.Command)
                {
                    case "DownloadTask":    // 下载任务
                        Trans.Result = DownloadTask(Row);
                        break;
                    case "DownloadTaskOnFile":    // 通过文件方式下载任务(先查询数据,生成文件,返回文件url进行下载)
                        Trans.Result = DownloadTaskOnFile(Row);
                        break;
                    case "DownloadTaskWithPager":
                        Trans.Result = DownloadTaskWithPager(Row);
                        break;
                    case "DownloadTaskWithPager_v2":
                        Trans.Result = DownloadTaskWithPager_v2(Row);
                        break;
                    case "DownloadTaskObjectStandardWithPager":
                        Trans.Result = DownloadTaskObjectStandardWithPager(Row);
                        break;
                    case "UploadTask":
                        Trans.Result = UploadTask(Row, Trans.Attribute);
                        break;
                    case "PointScanHistory":
                        Trans.Result = PointScanHistory(Row);
                        break;
                    case "RectificationList":
                        Trans.Result = RectificationList(Row);
                        break;
                    case "RectificationTaskList":
                        Trans.Result = RectificationTaskList(Row);
                        break;
                    case "RectificationTaskIncidentList":
                        Trans.Result = RectificationTaskIncidentList(Row);
                        break;
                    case "RectificationUrge":   // 催办
                        Trans.Result = RectificationUrge(Row);
                        break;
                    case "RectificationReturn":
                        Trans.Result = RectificationReturn(Row);
                        break;
                    case "RectificationAcceptance":
                        Trans.Result = RectificationAcceptance(Row);
                        break;
                    case "RectificationCount":
                        Trans.Result = RectificationCount(Row);
                        break;
                    case "GetStandardSimpleGuide":
                        Trans.Result = GetStandardSimpleGuide(Row);
                        break;
                    case "GetStandardInfo":
                        Trans.Result = GetStandardInfo(Row);
                        break;
                    case "GetPointInfo":
                        Trans.Result = GetPointInfo(Row);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace + Environment.NewLine + ex.Source + Environment.NewLine + Trans.Attribute);
                Trans.Result = new ApiResult(false, ex.Message).toJson();
            }
        }

        private string GetStandardInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("StandardIds") || string.IsNullOrEmpty(row["StandardIds"].ToString()))
            {
                return JSONHelper.FromString(false, "标准ID不能为空");
            }
            string StandardIds = row["StandardIds"].ToString();

            StandardIds = string.Format("'{0}'", StandardIds.Replace(",", "','"));

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                IEnumerable<string> resultSet = conn.Query<string>(@"SELECT CheckStandard FROM Tb_CP_SysStandard WHERE SysStandId IN (" + StandardIds + ")");

                return new ApiResult(true, resultSet).toJson();
            }
        }

        /// <summary>
        /// 通过标准ID获取操作指引信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns> 
        private string GetStandardSimpleGuide(DataRow row)
        {
            if (!row.Table.Columns.Contains("StandardId") || string.IsNullOrEmpty(row["StandardId"].ToString()))
            {
                return JSONHelper.FromString(false, "标准ID不能为空");
            }
            string StandardId = row["StandardId"].ToString();

            DataTable dt = null;
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sqlStr = " SELECT a.Code AS StandardCode, b.Id AS ProfessionalId, b.Name AS ProfessionalName, c.Id AS TypeId, c.Name AS TypeName, a.CheckStandard, a.CheckWay, Files, FilesName, SamplePic, SamplePicName ";
                sqlStr += " FROM Tb_CP_SysStandard AS a JOIN Tb_CP_Dictionary AS b ON a.Professional = b.Id JOIN Tb_CP_Dictionary AS c ON a.DType = c.Id ";
                sqlStr += " WHERE a.SysStandId = @StandardId ";
                dt = conn.ExecuteReader(sqlStr, new { StandardId = StandardId }, null, null, CommandType.Text).ToDataSet().Tables[0];
            }
            if (null == dt || dt.Rows.Count == 0)
            {
                return new ApiResult(false, "没有查询到对应的标准信息").toJson();
            }
            DataRow dr = dt.Rows[0];
            if (null == dr)
            {
                return new ApiResult(false, "没有查询到对应的标准信息").toJson();
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            //遍历查询出来的列,并添加
            foreach (DataColumn item in dr.Table.Columns)
            {
                dictionary.Add(item.ColumnName, dr[item].ToString());
            }
            return new ApiResult(true, dictionary).toJson();
        }
        /// <summary>
        /// 下载任务数据
        /// </summary>
        private string DownloadTask(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            string commID = row["CommID"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 读取巡查计划
                // 任务id、计划名称、计划开始时间、计划结束时间、点位总数、必查点位总数、非必查点位覆盖率、计划状态
                string sql = @"SELECT TaskId,PlanName,BeginTime,EndTime,PointNum AS PointCount,DidBeginTime,
                                    MustCheckPointNum AS MustCheckPointCount,OtherPointPercentage,isnull(PlanState,0) AS PlanState
                                FROM View_Tb_CP_TaskPlanMaintenance
                                WHERE isnull(IsDelete,0)=0 AND isnull(IsClose,0)=0 AND isnull(PlanState,0)<>2 AND CommID=@CommID
                                    AND convert(nvarchar(30), BeginTime, 20)<=@DateTime
                                    AND convert(nvarchar(30), EndTime, 20)>=@DateTime
                                    AND TaskRoleCode IN(SELECT a.RoleCode FROM Tb_Sys_UserRole a LEFT JOIN Tb_Sys_Role b ON a.RoleCode=b.RoleCode
                                                        WHERE a.UserCode=@UserCode AND b.SysRoleCode IS NOT NULL AND b.SysRoleCode<>'')
                                ORDER BY BeginTime";

                // 巡查计划列表
                IEnumerable<dynamic> taskSet = conn.Query(sql, new
                {
                    CommID = commID,
                    UserCode = Global_Var.LoginUserCode,
                    DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });
                if (null == taskSet || taskSet.Count() == 0)
                {
                    return new ApiResult(false, "无任务").toJson();
                }
                List<dynamic> taskList = new List<dynamic>();
                foreach (dynamic task in taskSet)
                {
                    // 读取巡查计划下巡查点位，对象，规则

                    // 点位表：任务点位关系id、任务id、点位id、点位名称、点位简称、点位状态、点位地址、扫码时间、扫码人、是否必查、点位排序号、公区、设备
                    sql = @"SELECT DISTINCT x.TaskPointId AS IID,x.TaskId,x.PointId,x.PointName,x.PointCode as PointSimpleName,isnull(x.PointState,0) AS PointState,
                                    x.Addr as PointAddress,x.DidTime as ScanTime,x.DidUserCode AS ScanUserCode,x.DidUserName AS ScanUserName,
                                    isnull(x.IsMustCheck,0) AS IsMustCheck,x.Sort,y.PublicArea AS RegionalID,z.RegionalPlace, y.EqSpaceId  
                                FROM View_Tb_CP_TaskPointList x LEFT JOIN Tb_CP_Point y ON x.PointId=y.PointId
                                    LEFT JOIN Tb_HSPR_IncidentRegional z ON y.PublicArea=z.RegionalID WHERE TaskId=@TaskId ;";

                    task.Point = conn.Query(sql, new
                    {
                        TaskId = task.TaskId
                    });

                    // 对象表：任务对象标准关系id、任务id、点位id、对象id、对象名称、标准id、标准代码、巡查标准内容、标准分值、巡查结果、巡检得分
                    sql = @"SELECT DISTINCT TsId AS IID, TaskId, PointId, ObjId, ObjName, SysStandId, Code AS SysStandCode, 
                                    CheckStandard, StandardScore, isnull(TaskResult,'') AS CheckResult,Score 
                                FROM View_Tb_CP_TaskStandardList WHERE TaskId=@TaskId AND TaskRoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode);";
                    task.ObjectStandard = conn.Query(sql, new
                    {
                        TaskId = task.TaskId,
                        UserCode = Global_Var.LoginUserCode
                    });

                    // 附件表：任务id、点位id、文件路径
                    sql = @"SELECT TaskId,PointId,FilePath FROM Tb_CP_TaskFiles WHERE isnull(IsDelete,0)=0 AND TaskId=@TaskId;";
                    task.Files = conn.Query(sql, new
                    {
                        TaskId = task.TaskId
                    });

                    // 报事表：任务id、点位id、报事id、报事编号、报事内容、报事类型、紧急程度、公区名称、报事类别、联系电话
                    sql = @"SELECT a.TaskId,a.PointId,a.IncidentID,b.IncidentNum,b.IncidentContent,b.ClassName,
                                    isnull(b.EmergencyDegree,0) AS EmergencyDegree,b.RegionalName,b.TypeName,b.Phone
                                FROM Tb_CP_TaskPointIncident a LEFT JOIN view_HSPR_IncidentSeach_Filter b ON a.IncidentId=b.IncidentID
                                WHERE a.TaskId=@TaskId;";
                    task.Incidents = conn.Query(sql, new
                    {
                        TaskId = task.TaskId
                    });

                    taskList.Add(task);
                }
                return new ApiResult(true, taskList).toJson();
            }
        }

        /// <summary>
        /// 通过文件方式下载数据
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string DownloadTaskOnFile(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return new ApiResult(false, "小区编号不能为空").toJson();
            }
            string CommID = row["CommID"].ToString();

            DateTime dateNow = DateTime.Now;

            #region 清理过期文件
            if ("1".Equals(AppGlobal.GetAppSetting("DelOverdueComprehensiveInspectionTaskFile")))
            {
                Task.Run(() =>
                {
                    try
                    {
                        string date = dateNow.ToString("yyyyMMdd");
                        // 执行删除过期任务文件(昨天以前的文件)
                        string taskFilePath = AppGlobal.GetAppSetting("ComprehensiveInspectionTaskFilesPath");
                        string[] dirList = Directory.GetDirectories(taskFilePath);
                        if (null != dirList)
                        {
                            DirectoryInfo directoryInfo;
                            foreach (var item in dirList)
                            {
                                directoryInfo = new DirectoryInfo(item);
                                if (date.Equals(directoryInfo.Name))
                                {
                                    continue;
                                }
                                Directory.Delete(directoryInfo.FullName, true);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                    }

                });
            }
            #endregion

            List<dynamic> taskList = new List<dynamic>();

            #region 查询巡查计划数据
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 读取巡查计划
                // 任务id、计划名称、计划开始时间、计划结束时间、点位总数、必查点位总数、非必查点位覆盖率、计划状态
                string sql = @"SELECT TaskId,PlanName,BeginTime,EndTime,PointNum AS PointCount,DidBeginTime,
                                    MustCheckPointNum AS MustCheckPointCount,OtherPointPercentage,isnull(PlanState,0) AS PlanState
                                FROM View_Tb_CP_TaskPlanMaintenance
                                WHERE isnull(IsDelete,0)=0 AND isnull(IsClose,0)=0 AND isnull(PlanState,0)<>2 AND CommID=@CommID
                                    AND convert(nvarchar(30), BeginTime, 20)<=@DateTime
                                    AND convert(nvarchar(30), EndTime, 20)>=@DateTime
                                    AND TaskRoleCode IN(SELECT a.RoleCode FROM Tb_Sys_UserRole a LEFT JOIN Tb_Sys_Role b ON a.RoleCode=b.RoleCode
                                                        WHERE a.UserCode=@UserCode AND b.SysRoleCode IS NOT NULL AND b.SysRoleCode<>'')
                                ORDER BY BeginTime";

                // 巡查计划列表
                IEnumerable<dynamic> taskSet = conn.Query(sql, new
                {
                    CommID = CommID,
                    UserCode = Global_Var.LoginUserCode,
                    DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });
                if (null == taskSet || taskSet.Count() == 0)
                {
                    return new ApiResult(false, "无任务").toJson();
                }
                foreach (dynamic task in taskSet)
                {
                    // 读取巡查计划下巡查点位，对象，规则
                    // 点位表：任务点位关系id、任务id、点位id、点位名称、点位简称、点位状态、点位地址、扫码时间、扫码人、是否必查、点位排序号、公区、设备
                    // 对象表：任务对象标准关系id、任务id、点位id、对象id、对象名称、标准id、标准代码、巡查标准内容、标准分值、巡查结果、巡检得分
                    // 附件表：任务id、点位id、文件路径
                    // 报事表：任务id、点位id、报事id、报事编号、报事内容、报事类型、紧急程度、公区名称、报事类别、联系电话
                    // 历史表：
                    sql = @"SELECT DISTINCT x.TaskPointId AS IID,x.TaskId,x.PointId,x.PointName,x.PointCode as PointSimpleName,isnull(x.PointState,0) AS PointState,
                                    x.Addr as PointAddress,x.DidTime as ScanTime,x.DidUserCode AS ScanUserCode,x.DidUserName AS ScanUserName,
                                    isnull(x.IsMustCheck,0) AS IsMustCheck,x.Sort,y.PublicArea AS RegionalID,z.RegionalPlace, y.EqSpaceId  
                                FROM View_Tb_CP_TaskPointList x LEFT JOIN Tb_CP_Point y ON x.PointId=y.PointId
                                    LEFT JOIN Tb_HSPR_IncidentRegional z ON y.PublicArea=z.RegionalID WHERE TaskId=@TaskId ;

                                SELECT DISTINCT TsId AS IID, TaskId, PointId, ObjId, ObjName, SysStandId, Code AS SysStandCode, 
                                    CheckStandard, StandardScore, isnull(TaskResult,'') AS CheckResult,Score 
                                FROM View_Tb_CP_TaskStandardList WHERE TaskId=@TaskId AND TaskRoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode);

                                SELECT TaskId,PointId,FilePath FROM Tb_CP_TaskFiles WHERE isnull(IsDelete,0)=0 AND TaskId=@TaskId;

                                SELECT a.TaskId,a.PointId,a.IncidentID,b.IncidentNum,b.IncidentContent,b.ClassName,
                                    isnull(b.EmergencyDegree,0) AS EmergencyDegree,b.RegionalName,b.TypeName,b.Phone
                                FROM Tb_CP_TaskPointIncident a LEFT JOIN view_HSPR_IncidentSeach_Filter b ON a.IncidentId=b.IncidentID
                                WHERE a.TaskId=@TaskId;";

                    GridReader gridReader = conn.QueryMultiple(sql, new { TaskId = task.TaskId });
                    task.Point = gridReader.Read();
                    task.ObjectStandard = gridReader.Read();
                    task.Files = gridReader.Read();
                    task.Incidents = gridReader.Read();
                    taskList.Add(task);
                }
            }
            #endregion

            string content = JsonConvert.SerializeObject(taskList);

            // 日期(年月日)/CommID/用户名/时间戳为文件名.txt, 每次请求清理前一天的数据
            string path = dateNow.ToString("yyyyMMdd") + "/" + CommID + "/" + Global_Var.LoginUserCode + "/";
            string filePath = AppGlobal.GetAppSetting("ComprehensiveInspectionTaskFilesPath") + path;
            string fileUrl = AppGlobal.GetAppSetting("ComprehensiveInspectionFilesTaskPathRela") + path;
            string fileName = dateNow.ToString("HHmmssfff") + ".txt";
            try
            {
                WriteTaskFile(filePath, fileName, content);
                return new ApiResult(true, fileUrl).toJson();
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new ApiResult(false, "生成任务文件失败,请重试").toJson();
            }

        }

        /// <summary>
        /// 分页下载任务数据
        /// </summary>
        private string DownloadTaskWithPager(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                return JSONHelper.FromString(false, "PageSize不能为空");
            }

            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                return JSONHelper.FromString(false, "PageIndex不能为空");
            }

            string commID = row["CommID"].ToString();
            int pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            int pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 读取巡查计划
                // 任务id、计划名称、计划开始时间、计划结束时间、点位总数、必查点位总数、非必查点位覆盖率、计划状态
                string sql = string.Format(@"SELECT TaskId,PlanName,BeginTime,EndTime,PointNum AS PointCount,
                    DidBeginTime,MustCheckPointNum AS MustCheckPointCount,OtherPointPercentage,
                    isnull(PlanState,0) AS PlanState
                    FROM View_Tb_CP_TaskPlanMaintenance
                    WHERE isnull(IsDelete,0)=0 AND isnull(IsClose,0)=0 AND isnull(PlanState,0)<>2 AND CommID={0}
                        AND convert(nvarchar(30), BeginTime, 20)<='{1}'
                        AND convert(nvarchar(30), EndTime, 20)>='{1}'
                        AND TaskRoleCode IN(SELECT a.RoleCode FROM Tb_Sys_UserRole a LEFT JOIN Tb_Sys_Role b ON a.RoleCode=b.RoleCode 
                        WHERE a.UserCode='{2}' AND b.SysRoleCode IS NOT NULL AND b.SysRoleCode<>'')",
                        commID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Global_Var.LoginUserCode);

                // 巡查计划列表
                var resultTable = GetList(out int pageCount, out int counts, sql, pageIndex, pageSize, "BeginTime", 0, "TaskId",
                    PubConstant.hmWyglConnectionString).Tables[0];

                if (resultTable.Rows.Count == 0)
                {
                    return new ApiResult(false, "无任务").toJson();
                }
                List<dynamic> taskList = new List<dynamic>();
                foreach (DataRow dataRow in resultTable.Rows)
                {
                    try
                    {
                        // 读取巡查计划下巡查点位，对象，规则
                        // 点位表：任务点位关系id、任务id、点位id、点位名称、点位简称、点位状态、点位地址、扫码时间、扫码人、是否必查、点位排序号、公区、设备
                        sql = @"SELECT DISTINCT x.TaskPointId AS IID,x.TaskId,x.PointId,x.PointName,x.PointCode as PointSimpleName,isnull(x.PointState,0) AS PointState,
                                    x.Addr as PointAddress,x.DidTime as ScanTime,x.DidUserCode AS ScanUserCode,x.DidUserName AS ScanUserName,
                                    isnull(x.IsMustCheck,0) AS IsMustCheck,x.Sort,y.PublicArea AS RegionalID,z.RegionalPlace, y.EqSpaceId  
                                FROM View_Tb_CP_TaskPointList x LEFT JOIN Tb_CP_Point y ON x.PointId=y.PointId
                                    LEFT JOIN Tb_HSPR_IncidentRegional z ON y.PublicArea=z.RegionalID WHERE TaskId=@TaskId ;";

                        IEnumerable<dynamic> Point = conn.Query(sql, new
                        {
                            TaskId = dataRow["TaskId"]
                        });

                        // 对象表：任务对象标准关系id、任务id、点位id、对象id、对象名称、标准id、标准代码、巡查标准内容、标准分值、巡查结果、巡检得分
                        sql = $@"SELECT DISTINCT TsId AS IID, TaskId, PointId, ObjId, ObjName, SysStandId,
                                Code AS SysStandCode, CheckStandard, StandardScore, 
                                isnull(TaskResult,'') AS CheckResult,Score 
                                FROM View_Tb_CP_TaskStandardList WHERE TaskId='{dataRow["TaskId"]}' AND TaskRoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{Global_Var.LoginUserCode}');";
                        DataTable dataTable = new DbHelperSQLP(PubConstant.hmWyglConnectionString).ExecuteReader(sql).ToDataSet().Tables[0];
                        List<dynamic> tempList = new List<dynamic>();
                        foreach (DataRow tempRow in dataTable.Rows)
                        {
                            dynamic point = new
                            {
                                IID = tempRow["IID"],
                                TaskId = tempRow["TaskId"],
                                PointId = tempRow["PointId"],
                                ObjId = tempRow["ObjId"],
                                ObjName = tempRow["ObjName"],
                                SysStandId = tempRow["SysStandId"],
                                SysStandCode = tempRow["SysStandCode"],
                                CheckStandard = tempRow["CheckStandard"],
                                StandardScore = tempRow["StandardScore"],
                                CheckResult = tempRow["CheckResult"],
                                Score = tempRow["Score"]
                            };
                            tempList.Add(point);
                        }


                        // 附件表：任务id、点位id、文件路径
                        sql = @"SELECT TaskId,PointId,FilePath FROM Tb_CP_TaskFiles WHERE isnull(IsDelete,0)=0 AND TaskId=@TaskId;";
                        IEnumerable<dynamic> Files = conn.Query(sql, new
                        {
                            TaskId = dataRow["TaskId"]
                        });

                        // 报事表：任务id、点位id、报事id、报事编号、报事内容、报事类型、紧急程度、公区名称、报事类别、联系电话
                        sql = @"SELECT a.TaskId,a.PointId,a.IncidentID,b.IncidentNum,b.IncidentContent,b.ClassName,
                                    isnull(b.EmergencyDegree,0) AS EmergencyDegree,b.RegionalName,b.TypeName,b.Phone
                                FROM Tb_CP_TaskPointIncident a LEFT JOIN view_HSPR_IncidentSeach_Filter b ON a.IncidentId=b.IncidentID
                                WHERE a.TaskId=@TaskId;";
                        IEnumerable<dynamic> Incidents = conn.Query(sql, new
                        {
                            TaskId = dataRow["TaskId"]
                        });

                        dynamic task = new
                        {
                            TaskId = dataRow["TaskId"],
                            PlanName = dataRow["PlanName"],
                            BeginTime = dataRow["BeginTime"],
                            DidBeginTime = dataRow["DidBeginTime"],
                            EndTime = dataRow["EndTime"],
                            PointCount = dataRow["PointCount"],
                            MustCheckPointCount = dataRow["MustCheckPointCount"],
                            OtherPointPercentage = dataRow["OtherPointPercentage"],
                            PlanState = dataRow["PlanState"],
                            Point = Point,
                            ObjectStandard = tempList,
                            Files = Files,
                            Incidents = Incidents,
                        };

                        taskList.Add(task);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message + ",SQL=" + sql);
                    }
                }
                return new ApiResult(true, taskList).toJson();
            }
        }

        private string DownloadTaskWithPager_v2(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                return JSONHelper.FromString(false, "PageSize不能为空");
            }

            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                return JSONHelper.FromString(false, "PageIndex不能为空");
            }

            string commID = row["CommID"].ToString();
            int pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            int pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 读取巡查计划
                // 任务id、计划名称、计划开始时间、计划结束时间、点位总数、必查点位总数、非必查点位覆盖率、计划状态
                string sql = $@"SELECT TaskId,PlanName,BeginTime,EndTime,PointNum AS PointCount,DidBeginTime,MustCheckPointNum AS MustCheckPointCount,
                                OtherPointPercentage, isnull(PlanState,0) AS PlanState FROM View_Tb_CP_TaskPlanMaintenance
                            WHERE isnull(IsDelete,0)=0 AND isnull(IsClose,0)=0 AND isnull(PlanState,0)<>2 AND CommID={commID}
                            AND BeginTime<=getdate() AND EndTime>=getdate()
                            AND TaskRoleCode IN(SELECT a.RoleCode FROM Tb_Sys_UserRole a LEFT JOIN Tb_Sys_Role b ON a.RoleCode=b.RoleCode 
                            WHERE a.UserCode='{Global_Var.LoginUserCode}' AND b.SysRoleCode IS NOT NULL AND b.SysRoleCode<>'')";

                // 巡查计划列表
                var resultTable = GetList(out int pageCount, out int counts, sql, pageIndex, pageSize, "BeginTime", 0, "TaskId",
                    PubConstant.hmWyglConnectionString).Tables[0];

                if (resultTable.Rows.Count == 0)
                {
                    return new ApiResult(false, "无任务").toJson();
                }
                List<dynamic> taskList = new List<dynamic>();
                foreach (DataRow dataRow in resultTable.Rows)
                {
                    try
                    {
                        // 读取巡查计划下巡查点位，对象，规则
                        // 点位表：任务点位关系id、任务id、点位id、点位名称、点位简称、点位状态、点位地址、扫码时间、扫码人、是否必查、点位排序号、公区、设备
                        sql = @"SELECT DISTINCT x.TaskPointId AS IID,x.TaskId,x.PointId,x.PointName,x.PointCode as PointSimpleName,isnull(x.PointState,0) AS PointState,
                                    x.Addr as PointAddress,x.DidTime as ScanTime,x.DidUserCode AS ScanUserCode,x.DidUserName AS ScanUserName,
                                    isnull(x.IsMustCheck,0) AS IsMustCheck,x.Sort,y.PublicArea AS RegionalID,z.RegionalPlace, y.EqSpaceId  
                                FROM View_Tb_CP_TaskPointList x LEFT JOIN Tb_CP_Point y ON x.PointId=y.PointId
                                    LEFT JOIN Tb_HSPR_IncidentRegional z ON y.PublicArea=z.RegionalID WHERE TaskId=@TaskId ;";

                        IEnumerable<dynamic> Point = conn.Query(sql, new
                        {
                            TaskId = dataRow["TaskId"]
                        });


                        int max = 1000;
                        List<dynamic> ObjectStandard = new List<dynamic>();
                        // 获取对象标准数量，当对象标准数量小于1000条的时候，不需要分页下载
                        sql = $@"SELECT count(TsId) FROM View_Tb_CP_TaskStandardList WHERE TaskId='{dataRow["TaskId"]}' AND TaskRoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{Global_Var.LoginUserCode}');";

                        int objectCount = conn.Query<int>(sql).FirstOrDefault();
                        if (objectCount <= max)
                        {
                            // 对象表：任务对象标准关系id、任务id、点位id、对象id、对象名称、标准id、标准代码、巡查标准内容、标准分值、巡查结果、巡检得分
                            sql = $@"SELECT DISTINCT TsId AS IID, TaskId, PointId, ObjId, ObjName, SysStandId,
                                Code AS SysStandCode, CheckStandard, StandardScore, 
                                isnull(TaskResult,'') AS CheckResult,Score 
                                FROM View_Tb_CP_TaskStandardList WHERE TaskId='{dataRow["TaskId"]}' AND TaskRoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{Global_Var.LoginUserCode}');";
                            DataTable dataTable = new DbHelperSQLP(PubConstant.hmWyglConnectionString).ExecuteReader(sql).ToDataSet().Tables[0];
                            foreach (DataRow tempRow in dataTable.Rows)
                            {
                                dynamic point = new
                                {
                                    IID = tempRow["IID"],
                                    TaskId = tempRow["TaskId"],
                                    PointId = tempRow["PointId"],
                                    ObjId = tempRow["ObjId"],
                                    ObjName = tempRow["ObjName"],
                                    SysStandId = tempRow["SysStandId"],
                                    SysStandCode = tempRow["SysStandCode"],
                                    CheckStandard = tempRow["CheckStandard"],
                                    StandardScore = tempRow["StandardScore"],
                                    CheckResult = tempRow["CheckResult"],
                                    Score = tempRow["Score"]
                                };
                                ObjectStandard.Add(point);
                            }
                        }


                        // 附件表：任务id、点位id、文件路径
                        sql = @"SELECT TaskId,PointId,FilePath FROM Tb_CP_TaskFiles WHERE isnull(IsDelete,0)=0 AND TaskId=@TaskId;";
                        IEnumerable<dynamic> Files = conn.Query(sql, new
                        {
                            TaskId = dataRow["TaskId"]
                        });


                        // 报事表：任务id、点位id、报事id、报事编号、报事内容、报事类型、紧急程度、公区名称、报事类别、联系电话
                        // 此处查询超时
                        //sql = @"SELECT a.TaskId,a.PointId,a.IncidentID,b.IncidentNum,b.IncidentContent,b.ClassName,
                        //            isnull(b.EmergencyDegree,0) AS EmergencyDegree,b.RegionalName,b.TypeName,b.Phone
                        //        FROM Tb_CP_TaskPointIncident a LEFT JOIN view_HSPR_IncidentSeach_Filter b ON a.IncidentId=b.IncidentID
                        //        WHERE a.TaskId=@TaskId;";
                        //IEnumerable<dynamic> Incidents = conn.Query(sql, new
                        //{
                        //    TaskId = dataRow["TaskId"]
                        //});

                        List<dynamic> Incidents = new List<dynamic>();

                        // 是否存在报事
                        sql = "SELECT a.TaskId,a.PointId,a.IncidentID FROM Tb_CP_TaskPointIncident a WHERE a.TaskId=@TaskId;";
                        var taskIncidents = conn.Query(sql, new { TaskId = dataRow["TaskId"] });
                        if (taskIncidents.Count() > 0)
                        {
                            sql = @"SELECT @TaskId AS TaskId,@PointId AS PointId,b.IncidentID,b.IncidentNum,b.IncidentContent,b.ClassName,
                                    isnull(b.EmergencyDegree,0) AS EmergencyDegree,b.RegionalName,b.TypeName,b.Phone
                                    FROM view_HSPR_IncidentSeach_Filter b
                                    WHERE IncidentID=@IncidentID";

                            foreach (var item in taskIncidents)
                            {
                                dynamic incidentInfo = conn.Query(sql, new
                                {
                                    TaskId = item.TaskId,
                                    PointId = item.PointId,
                                    IncidentID = item.IncidentID
                                }).FirstOrDefault();

                                if (incidentInfo != null)
                                {
                                    Incidents.Add(new
                                    {
                                        TaskId = incidentInfo.TaskId,
                                        PointId = incidentInfo.PointId,
                                        IncidentID = incidentInfo.IncidentID,
                                        IncidentNum = incidentInfo.IncidentNum,
                                        IncidentContent = incidentInfo.IncidentContent,
                                        ClassName = incidentInfo.ClassName,
                                        EmergencyDegree = incidentInfo.EmergencyDegree,
                                        RegionalName = incidentInfo.RegionalName,
                                        TypeName = incidentInfo.TypeName,
                                        Phone = incidentInfo.Phone
                                    });
                                }
                            }
                        }


                        dynamic task = new
                        {
                            TaskId = dataRow["TaskId"],
                            PlanName = dataRow["PlanName"],
                            BeginTime = dataRow["BeginTime"],
                            DidBeginTime = dataRow["DidBeginTime"],
                            EndTime = dataRow["EndTime"],
                            PointCount = dataRow["PointCount"],
                            MustCheckPointCount = dataRow["MustCheckPointCount"],
                            OtherPointPercentage = dataRow["OtherPointPercentage"],
                            PlanState = dataRow["PlanState"],
                            Point = Point,
                            ObjectStandard = ObjectStandard,
                            ObjectStandardPageSize = (objectCount > max ? max : 0),
                            ObjectStandardCount = objectCount,
                            Files = Files,
                            Incidents = Incidents,
                        };

                        taskList.Add(task);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message + ",SQL=" + sql);
                    }
                }
                return new ApiResult(true, taskList).toJson();
            }
        }

        /// <summary>
        /// 下载任务下包含的对象标准
        /// </summary>
        private string DownloadTaskObjectStandardWithPager(DataRow row)
        {
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务编号不能为空");
            }

            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                return JSONHelper.FromString(false, "PageIndex不能为空");
            }

            string TaskID = row["TaskID"].ToString();
            int pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());

            // 对象表：任务对象标准关系id、任务id、点位id、对象id、对象名称、标准id、标准代码、巡查标准内容、标准分值、巡查结果、巡检得分
            string sql = $@"SELECT DISTINCT TsId AS IID, TaskId, PointId, ObjId, ObjName, SysStandId,
                Code AS SysStandCode, CheckStandard, StandardScore, 
                isnull(TaskResult,'') AS CheckResult,Score 
                FROM View_Tb_CP_TaskStandardList WHERE TaskId='{TaskID}' AND TaskRoleCode 
                    IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='{Global_Var.LoginUserCode}')";

            int PageCount;
            int Counts;
            DataTable dt = GetList(out PageCount, out Counts, sql, pageIndex, 1000, "IID", 1, "IID", PubConstant.hmWyglConnectionString).Tables[0];

            return JSONHelper.FromString(true, dt);
        }

        /// <summary>
        /// 写出任务文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool WriteTaskFile(string filePath, string fileName, string content)
        {
            try
            {
                // 如果文件夹不存在,则创建文件夹
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                // 如果文件存在,则删除文件
                if (File.Exists(filePath + fileName))
                {
                    File.Delete(filePath);
                }
                File.WriteAllText(filePath + fileName, content);
                return true;
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return false;
            }

        }
        /// <summary>
        /// 上传任务数据
        /// </summary>
        private string UploadTask(DataRow row, string AttributeData)
        {
            try
            {
                if (!row.Table.Columns.Contains("TaskId") || string.IsNullOrEmpty(row["TaskId"].ToString()))
                {
                    return JSONHelper.FromString(false, "任务id不能为空");
                }
                if (!row.Table.Columns.Contains("PlanState") || string.IsNullOrEmpty(row["PlanState"].ToString()))
                {
                    return JSONHelper.FromString(false, "任务状态不能为空");
                }
                if (!row.Table.Columns.Contains("StartTime") || string.IsNullOrEmpty(row["StartTime"].ToString()))
                {
                    return JSONHelper.FromString(false, "任务开始时间不能为空");
                }

                string taskId = row["TaskId"].ToString();
                string planState = row["PlanState"].ToString();
                string startTime = row["StartTime"].ToString();
                DateTime? endTime = null;

                if (row.Table.Columns.Contains("EndTime") && !string.IsNullOrEmpty(row["EndTime"].ToString()))
                {
                    endTime = Convert.ToDateTime(row["EndTime"].ToString().Substring(0, 19));
                }

                // 读取点位
                JArray array = (JArray)JsonConvert.DeserializeObject(row["Data"].ToString());

                int incidentCount = 0;
                string sql = "";

                if (array.Count > 0)
                {
                    using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                    {
                        try
                        {
                            foreach (JObject item in array)
                            {
                                sql = "";
                                int PointState = 1;

                                // 点位下对象对应标准
                                var IID = item["IID"].ToString();
                                var ScanTime = item["ScanTime"].ToString();
                                if (string.IsNullOrEmpty(ScanTime))
                                {
                                    ScanTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                }

                                JArray ObjectStandard = (JArray)item["ObjectStandard"];
                                JArray IncidentID = (JArray)item["IncidentID"];
                                incidentCount += IncidentID.Count;

                                // 更新对象标准巡查信息
                                foreach (var objStan in ObjectStandard)
                                {
                                    string objStanIID = objStan["IID"].ToString();
                                    string Qualified = objStan["Qualified"].ToString();

                                    if (Qualified == "1")
                                    {
                                        sql += $@"UPDATE Tb_CP_TaskStandard SET TaskResult='合格', Score=(SELECT x.Score FROM Tb_CP_SysStandard x
                                        WHERE x.SysStandId=(SELECT TOP 1 y.SysStandId FROM Tb_CP_TaskStandard y WHERE y.TsId='{objStanIID}')) 
                                        WHERE TsId='{objStanIID}';";
                                    }
                                    else
                                    {
                                        sql += $@"UPDATE Tb_CP_TaskStandard SET TaskResult='不合格', Score=0 WHERE TsId='{objStanIID}';";
                                    }
                                }

                                // 报事信息
                                if (IncidentID.Count > 0)
                                {
                                    foreach (JValue _incidentId in IncidentID)
                                    {
                                        sql += $@"INSERT INTO Tb_CP_TaskPointIncident(TaskIncidentId, CommId, TaskId, TaskPointId, PointId, IncidentId)
                                        SELECT newid() AS TaskIncidentId, CommId, TaskId, TaskPointId, PointId, '{_incidentId.ToString()}' AS IncidentId
                                        FROM Tb_CP_TaskPoint WHERE TaskPointId = '{IID}';";
                                    }
                                }

                                // 更新点位信息
                                sql += $@"UPDATE Tb_CP_TaskPoint SET DidTime='{ScanTime}',DidUserCode='{Global_Var.LoginUserCode}',
                                RectificationNum=(isnull(RectificationNum,0)+{IncidentID.Count}),PointState='{PointState}' 
                                WHERE TaskPointId='{IID}';";

                                conn.Execute(sql, null, null, 30);
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
                

                using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    // 任务数据统计
                    sql = @"SELECT count(1) AS Count1 FROM Tb_CP_TaskPoint WHERE TaskId=@TaskId AND PointState=1 AND isnull(IsMustCheck,0)=1;";
                    int count1 = conn.Query<int>(sql, new { TaskId = taskId }, null, false, 10).FirstOrDefault();

                    sql = @"SELECT count(1) AS Count2 FROM Tb_CP_TaskPoint WHERE TaskId=@TaskId AND PointState=1 AND isnull(IsMustCheck,0)=0;";
                    float count2 = conn.Query<float>(sql, new { TaskId = taskId }, null, false, 10).FirstOrDefault();

                    sql = @"SELECT count(1) AS Count3 FROM Tb_CP_TaskPoint WHERE TaskId=@TaskId AND isnull(IsMustCheck,0)=0;";
                    float count3 = conn.Query<float>(sql, new { TaskId = taskId }, null, false, 10).FirstOrDefault();

                    float otherPercent = 0f;
                    if (count3 != 0)
                    {
                        otherPercent = (float)((count2 * 1.0) / count3);
                    }

                    // 更新任务数据
                    sql = @"UPDATE Tb_CP_Task SET DidBeginTime=isnull(DidBeginTime,@DidBeginTime),
                                DidEndTime=@DidEndTime,PlanState=@PlanState,
                                RectificationNum=(isnull(RectificationNum,0)+@RectificationNum),
                                DidAllPointNum=(@Count1+@Count2),
                                DidMustCheckPointNum=@Count1, 
                                DidOtherPointPercentage=cast(round(@OtherPercent,2) AS NUMERIC(5,2))*100   
                            WHERE TaskId=@TaskId";
                    conn.Execute(sql, new
                    {
                        DidBeginTime = startTime,
                        DidEndTime = endTime,
                        PlanState = planState,
                        RectificationNum = incidentCount,
                        Count1 = count1,
                        Count2 = count2,
                        OtherPercent = otherPercent,
                        TaskId = taskId
                    });
                }

                return JSONHelper.FromString(true, "上传成功");
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace + Environment.NewLine + ex.Source + Environment.NewLine + AttributeData);

                return JSONHelper.FromString(false, ex.Message);
            }
        }

        /// <summary>
        /// 点位巡查历史
        /// </summary>
        private string PointScanHistory(DataRow row)
        {
            if (!row.Table.Columns.Contains("PointID") || string.IsNullOrEmpty(row["PointID"].ToString()))
            {
                return JSONHelper.FromString(false, "点位ID不能为空");
            }

            int pageIndex = 1;
            int pageSize = 5;

            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = string.Format(@"SELECT b.TaskId,a.PointId,c.PlanName,b.BeginTime,b.EndTime,a.DidTime as ScanTime,d.UserName AS ScanUserName
                                                FROM Tb_CP_TaskPoint a LEFT JOIN Tb_CP_Task b ON a.TaskId=b.TaskId
                                                    LEFT JOIN Tb_CP_Plan c ON b.PlanId=c.PlanId
                                                    LEFT JOIN Tb_Sys_User d ON a.DidUserCode=d.UserCode
                                                WHERE a.PointId='{0}' AND a.PointState=1", row["PointID"].ToString());
                IEnumerable<dynamic> historySet = GetListDapper(out int pageCount, out int count, sql, pageIndex, pageSize, "ScanTime", 1, "TaskId",
                    new SqlConnection(PubConstant.hmWyglConnectionString));

                List<dynamic> historyList = new List<dynamic>();

                if (historySet.Count() > 0)
                {
                    sql = @"SELECT IncidentID,IncidentNum,IncidentContent,IncidentDate,DispType,DealState,FinishUser
                            FROM Tb_HSPR_IncidentAccept WHERE IncidentID IN
                              (SELECT IncidentId FROM Tb_CP_TaskPointIncident WHERE TaskId=@TaskId AND PointId=@PointId)";

                    foreach (dynamic item in historySet)
                    {
                        item.Incidents = conn.Query(sql, new { TaskId = item.TaskId, PointId = item.PointId });
                        historyList.Add(item);
                    }
                    string returnString = new ApiResult(true, historyList).toJson();
                    return returnString.Insert(returnString.Length - 1, ",\"PageCount\":" + pageCount);
                }
                return JSONHelper.FromString(false, "该点位无巡查历史");
            }
        }

        /// <summary>
        /// 巡查整改数量
        /// </summary>
        private string RectificationCount(DataRow row)
        {
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                try
                {
                    string sql = string.Format(@"SELECT count(1) AS Count FROM Tb_CP_Task x RIGHT JOIN (
                                                SELECT b.TaskId,c.PlanName,f.PointName,b.BeginTime,b.EndTime,d.IncidentID,d.IncidentNum,d.IncidentDate,
                                                    dbo.funGetIncidentTypeNames(d.TypeID,d.CommID) as TypeName,d.DealLimit,d.EmergencyDegree,
                                                    d.IncidentContent,e.RegionalPlace,d.DealMan,d.DealState,d.DealSituation,d.MainEndDate
                                                FROM Tb_CP_TaskPointIncident a LEFT JOIN Tb_CP_Task b ON a.TaskId=b.TaskId
                                                    LEFT JOIN Tb_CP_Plan c ON b.PlanId=c.PlanId
                                                    LEFT JOIN Tb_HSPR_IncidentAccept d ON a.IncidentId=d.IncidentID
                                                    LEFT JOIN Tb_HSPR_IncidentRegional e ON d.RegionalID=e.RegionalID
                                                    LEFT JOIN Tb_CP_Point f ON a.PointId=f.PointId
                                                WHERE a.CheckTime IS NULL) y ON x.TaskId=y.TaskId
                                            WHERE TaskRoleCode IN(SELECT a.RoleCode FROM Tb_Sys_UserRole a LEFT JOIN Tb_Sys_Role b ON a.RoleCode=b.RoleCode
                                                                    WHERE a.UserCode='{0}' AND b.SysRoleCode IS NOT NULL AND b.SysRoleCode<>'')",
                                                                        Global_Var.LoginUserCode);

                    sql += @" AND (SELECT count(1) FROM Tb_HSPR_IncidentAccept WHERE IncidentID=y.IncidentID AND isnull(IsDelete,0)=0)>0";

                    return JSONHelper.FromString(true, conn.Query(sql).First().Count.ToString());
                }
                catch (Exception)
                {
                    return JSONHelper.FromString(true, "0");
                }

            }
        }

        /// <summary>
        /// 巡查整改列表
        /// </summary>
        private string RectificationList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("State") || string.IsNullOrEmpty(row["State"].ToString()))
            {
                return JSONHelper.FromString(false, "State不能为空");
            }

            string commID = row["CommID"].ToString();
            string state = row["State"].ToString();

            int pageIndex = 1;
            int pageSize = 5;

            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = string.Format(@"SELECT DISTINCT y.IncidentID, y.PointId,y.TaskId,y.PlanName,y.PointName,y.BeginTime,y.EndTime,y.IncidentNum,
                                                y.IncidentDate,y.ClassName,y.TypeName,y.DealLimit,y.EmergencyDegree,y.IncidentContent,y.RegionalPlace,
                                                y.DealMan,y.DealState,y.DealSituation,y.MainEndDate FROM Tb_CP_Task x RIGHT JOIN (
                                                SELECT a.PointId,b.TaskId,c.PlanName,f.PointName,b.BeginTime,b.EndTime,d.IncidentID,d.IncidentNum,d.ClassName,
                                                d.IncidentDate,dbo.funGetIncidentTypeNames(d.TypeID,d.CommID) as TypeName,d.DealLimit,d.EmergencyDegree,
                                                    d.IncidentContent,e.RegionalPlace,d.DealMan,d.DealState,d.DealSituation,d.MainEndDate
                                                FROM Tb_CP_TaskPointIncident a LEFT JOIN Tb_CP_Task b ON a.TaskId=b.TaskId
                                                    LEFT JOIN Tb_CP_Plan c ON b.PlanId=c.PlanId
                                                    LEFT JOIN view_HSPR_IncidentSeach_Filter d ON a.IncidentId=d.IncidentID
                                                    LEFT JOIN Tb_HSPR_IncidentRegional e ON d.RegionalID=e.RegionalID
                                                    LEFT JOIN Tb_CP_Point f ON a.PointId=f.PointId
                                                WHERE a.CheckTime IS {0} NULL) y ON x.TaskId=y.TaskId
                                            WHERE TaskRoleCode IN(SELECT a.RoleCode FROM Tb_Sys_UserRole a LEFT JOIN Tb_Sys_Role b ON a.RoleCode=b.RoleCode
                                                                    WHERE a.UserCode='{1}' AND b.SysRoleCode IS NOT NULL AND b.SysRoleCode<>'')",
                                                                    state == "1" ? "NOT" : "", Global_Var.LoginUserCode);

                // 金辉、实地、海亮、华宇、俊发，大发，都是用的金辉版本报事
                if (Global_Var.LoginCorpID == "1329" || 
                    Global_Var.LoginCorpID == "2021" || 
                    Global_Var.LoginCorpID == "1940" || 
                    Global_Var.LoginCorpID == "1985" ||
                    Global_Var.LoginCorpID == "2046")
                {
                    sql = string.Format(@"SELECT DISTINCT y.IncidentID, y.PointId,y.TaskId,y.PlanName,y.PointName,y.BeginTime,y.EndTime,y.IncidentNum,
                                                y.IncidentDate,y.ClassName,y.TypeName,y.DealLimit,y.EmergencyDegree,y.IncidentContent,y.RegionalPlace,
                                                y.DealMan,y.DealState,y.DealSituation,y.MainEndDate FROM Tb_CP_Task x RIGHT JOIN (
                                                SELECT a.PointId,b.TaskId,c.PlanName,f.PointName,b.BeginTime,b.EndTime,d.IncidentID,d.IncidentNum,d.ClassName,d.IncidentDate,
                                                    isnull(d.FineTypeName,d.BigTypeName) as TypeName,d.DealLimit,d.EmergencyDegree,
                                                    d.IncidentContent,e.RegionalPlace,d.DealMan,d.DealState,d.DealSituation,d.MainEndDate
                                                FROM Tb_CP_TaskPointIncident a LEFT JOIN Tb_CP_Task b ON a.TaskId=b.TaskId
                                                    LEFT JOIN Tb_CP_Plan c ON b.PlanId=c.PlanId
                                                    LEFT JOIN view_HSPR_IncidentNewJH_Search_Filter_SJ d ON a.IncidentId=d.IncidentID
                                                    LEFT JOIN Tb_HSPR_IncidentRegional e ON d.RegionalID=e.RegionalID
                                                    LEFT JOIN Tb_CP_Point f ON a.PointId=f.PointId
                                                WHERE a.CheckTime IS {0} NULL) y ON x.TaskId=y.TaskId
                                            WHERE TaskRoleCode IN(SELECT a.RoleCode FROM Tb_Sys_UserRole a LEFT JOIN Tb_Sys_Role b ON a.RoleCode=b.RoleCode
                                                                    WHERE a.UserCode='{1}' AND b.SysRoleCode IS NOT NULL AND b.SysRoleCode<>'')",
                                                                    state == "1" ? "NOT" : "", Global_Var.LoginUserCode);
                }

                sql += @" AND (SELECT count(1) FROM Tb_HSPR_IncidentAccept WHERE IncidentID=y.IncidentID AND isnull(IsDelete,0)=0)>0";

                IEnumerable<dynamic> rectificationSet = GetListDapper(out int pageCount, out int count, sql, pageIndex, pageSize, "IncidentDate", (Global_Var.LoginCorpID=="1824")?1:0, "TaskId", conn);

                string returnString = new ApiResult(true, rectificationSet).toJson();
                return returnString.Insert(returnString.Length - 1, ",\"PageCount\":" + pageCount);
            }
        }

        private string RectificationTaskList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            string commID = row["CommID"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = $@"SELECT a.TaskId,c.PlanName,b.BeginTime,b.EndTime FROM Tb_CP_TaskPointIncident a
                                  LEFT JOIN Tb_CP_Task b ON a.TaskId=b.TaskId
                                  LEFT JOIN Tb_CP_Plan c ON b.PlanId=c.PlanId
                                WHERE (SELECT count(1) FROM Tb_HSPR_IncidentAccept WHERE IncidentID=a.IncidentID AND isnull(IsDelete,0)=0)>0 
                                    AND b.CommID={commID} AND a.CheckTime IS NULL AND a.CheckUserCode IS NULL AND
                                    b.TaskRoleCode IN(SELECT a.RoleCode FROM Tb_Sys_UserRole a 
                                    LEFT JOIN Tb_Sys_Role b ON a.RoleCode=b.RoleCode
                                    WHERE a.UserCode='{Global_Var.LoginUserCode}' AND b.SysRoleCode IS NOT NULL AND b.SysRoleCode<>'')
                                GROUP BY a.TaskId,c.PlanName,b.BeginTime,b.EndTime";

                sql += @" ";

                IEnumerable<dynamic> resultSet = conn.Query(sql);
                return new ApiResult(true, resultSet).toJson();
            }
        }

        private string RectificationTaskIncidentList(DataRow row)
        {
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }

            string TaskID = row["TaskID"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = @"SELECT TaskIncidentId AS IID,TaskId,CommId,PointId,IncidentId FROM Tb_CP_TaskPointIncident 
                                WHERE TaskID=@TaskID AND CheckTime IS NULL AND CheckUserCode IS NULL";

                IEnumerable<dynamic> resultSet = conn.Query(sql, new { TaskID = TaskID });
                return new ApiResult(true, resultSet).toJson();
            }
        }

        /// <summary>
        /// 巡查催办
        /// </summary>
        private string RectificationUrge(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编号不能为空");
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = @"SELECT * FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID";

                dynamic incidentInfo = conn.Query(sql, new { IncidentID = row["IncidentID"].ToString() }).FirstOrDefault();

                if (incidentInfo.MainEndDate != null && !string.IsNullOrEmpty(incidentInfo.MainEndDate))
                {
                    return JSONHelper.FromString(false, "该报事已经处理完毕");
                }

                Tb_HSPR_IncidentAccept incidentAccept = new Tb_HSPR_IncidentAccept()
                {
                    Phone = incidentInfo.Phone,
                    CommID = incidentInfo.CommID,
                    TypeID = incidentInfo.TypeID,
                    IncidentID = incidentInfo.IncidentID,
                    IncidentPlace = incidentInfo.IncidentPlace,
                    IncidentMan = incidentInfo.IncidentMan,
                    DispMan = incidentInfo.DispMan,
                    CoordinateNum = incidentInfo.CoordinateNum
                };

                // 已分派，推送给处理人
                if (incidentInfo.DispUserCode != null)
                {
                    IncidentAcceptPush.SynchPushAfterAssign(incidentAccept);
                }
                else
                {
                    // 推送分派和抢单
                    IncidentAcceptPush.SynchPushPublicIncident(incidentAccept, "综合巡查");
                }

                // 金辉、实地、海亮、华宇、俊发，都是用的金辉版本报事
                if (Global_Var.LoginCorpID == "1329" || Global_Var.LoginCorpID == "2021" || Global_Var.LoginCorpID == "1940" || Global_Var.LoginCorpID == "1985")
                {
                    conn.Execute(@"INSERT INTO Tb_HSPR_IncidentRemindersInfo(IncidentID,CommID,RemindersDate,UserID,UserName,IsDelete) 
                                    VALUES(@IncidentID,@CommID,getdate(),@UserID,@UserName,0)",
                                new
                                {
                                    IncidentID = incidentInfo.IncidentID,
                                    CommID = incidentInfo.CommID,
                                    UserID = Global_Var.LoginUserCode,
                                    UserName = Global_Var.LoginUserName
                                });
                }

                return JSONHelper.FromString(true, "催办成功");
            }
        }

        /// <summary>
        /// 巡查报事退回
        /// </summary>
        private string RectificationReturn(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编号不能为空");
            }
            if (!row.Table.Columns.Contains("TaskId") || string.IsNullOrEmpty(row["TaskId"].ToString()))
            {
                return JSONHelper.FromString(false, "任务编号不能为空");
            }
            if (!row.Table.Columns.Contains("PointId") || string.IsNullOrEmpty(row["PointId"].ToString()))
            {
                return JSONHelper.FromString(false, "点位编号不能为空");
            }
            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
            {
                return JSONHelper.FromString(false, "退回原因不能为空");
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = @"UPDATE Tb_HSPR_IncidentAccept SET ReWorkCreater=@UserName,ReWorkContent=@Content,ReWorkDate=getdate(),IsReWork=1 
                                    WHERE IncidentID=@IncidentID;
                                DECLARE @Count INT;
                                SELECT @Count=count(1) FROM Tb_CP_TaskPointIncident WHERE TaskId=@TaskId AND PointId=@PointId 
                                    AND CheckTime IS NOT NULL AND CheckUserCode IS NOT NULL;
                                UPDATE Tb_CP_Task SET RectificationAccepNum=(isnull(RectificationAccepNum,0)-@Count) WHERE TaskId=@TaskId;
                                UPDATE Tb_CP_TaskPointIncident SET CheckTime=NULL,CheckUserCode=NULL WHERE TaskId=@TaskId AND PointId=@PointId;";

                conn.Execute(sql, new
                {
                    UserName = Global_Var.LoginUserName,
                    Content = row["Content"].ToString(),
                    IncidentID = row["IncidentID"].ToString(),
                    TaskId = row["TaskId"].ToString(),
                    PointId = row["PointId"].ToString()
                });
                //修复bug 11611 返工在ERP上无纪录
                sql = @"INSERT INTO Tb_HSPR_IncidentReworkRecord(ID,IncidentID,CommID,ReworkMan,ReworkDate,ReworkReason,CreateDate,CreateMan)  
                                                         VALUES (NEWID(),@IncidentID,@CommID,@UserName,GETDATE(),@Content,GETDATE(),@UserName)";

                conn.Execute(sql, new
                {
                    UserName = Global_Var.LoginUserName,
                    Content = row["Content"].ToString(),
                    IncidentID = row["IncidentID"].ToString(),
                    CommID = Global_Var.LoginCommID
                });
                string str = "SELECT TOP 1 * FROM Tb_HSPR_IncidentAccept where IncidentID=@IncidentID";
                Tb_HSPR_IncidentAccept model = conn.Query<Tb_HSPR_IncidentAccept>(str, new { IncidentID = row["IncidentID"].ToString() }).FirstOrDefault();

                // 通知处理人
                if (model != null)
                {
                    IncidentAcceptPush.SynchPushWhenReturn(model, Global_Var.LoginUserName);

                    // 金辉、实地、海亮、华宇、俊发，都是用的金辉版本报事
                    if (Global_Var.LoginCorpID == "1329" || Global_Var.LoginCorpID == "2021" || Global_Var.LoginCorpID == "1940" || Global_Var.LoginCorpID == "1985")
                    {
                        DynamicParameters param = new DynamicParameters();
                        param.Add("@CommID", model.CommID);
                        param.Add("@IncidentID", model.IncidentID);
                        param.Add("@FinishGoBackReasons", "巡查整改不合格");
                        param.Add("@FinishGoBackUserCode", Global_Var.LoginUserCode);
                        param.Add("@FinishGoBackUser", Global_Var.LoginUserName);
                        conn.Execute("Proc_HSPR_Incident_CancleFinish_Phone", param, null, null, CommandType.StoredProcedure);
                    }
                }

                return JSONHelper.FromString(true, "退回成功");
            }
        }

        /// <summary>
        /// 巡查整改验收
        /// </summary>
        private string RectificationAcceptance(DataRow row)
        {
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "计划编号不能为空");
            }

            if (!row.Table.Columns.Contains("PointID") || string.IsNullOrEmpty(row["PointID"].ToString()))
            {
                return JSONHelper.FromString(false, "点位编号不能为空");
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = @"UPDATE Tb_CP_TaskPointIncident SET CheckTime=getdate(),CheckUserCode=@UserCode 
                                WHERE TaskId=@TaskId AND PointId=@PointId;
                               UPDATE Tb_CP_Task SET RectificationAccepNum=(isnull(RectificationAccepNum,0)+1) 
                                WHERE TaskId=@TaskId;";

                conn.Execute(sql, new
                {
                    UserCode = Global_Var.LoginUserCode,
                    TaskId = row["TaskId"].ToString(),
                    PointId = row["PointId"].ToString()
                });

                // 实地综合巡查整改工单验收时，应该直接跳过报事关闭
                if (Global_Var.CorpName.Contains("实地"))
                {
                    sql = @"UPDATE Tb_HSPR_IncidentAccept SET IsClose=1,CloseUser=@CloseUser,CloseUserCode=CloseUserCode,
                        CloseTime=getdate(),CloseSituation='综合巡查整改验收合格',CloseType=0
                        WHERE IncidentID IN(SELECT IncidentID FROM Tb_CP_TaskPointIncident 
                            WHERE TaskId=@TaskId AND PointId=@PointId)";

                    conn.Execute(sql, new
                    {
                        CloseUser = Global_Var.LoginUserName,
                        CloseUserCode = Global_Var.LoginUserCode,
                        TaskId = row["TaskId"].ToString(),
                        PointId = row["PointId"].ToString()
                    });
                }

                return JSONHelper.FromString(true, "验收成功");
            }
        }

        /// <summary>
        /// 获取点位信息
        /// </summary>
        private string GetPointInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("PointID") || string.IsNullOrEmpty(row["PointID"].ToString()))
            {
                return JSONHelper.FromString(false, "点位id不能为空");
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var info = conn.Query(@"SELECT FilesName,Files,Remark FROM Tb_CP_Point WHERE PointId=@PointId", 
                    new { PointId = row["PointID"].ToString() }).FirstOrDefault();

                return new ApiResult(true, info).toJson();
            }
        }
    }
}
