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
    public class ComprehensiveInspectionManage_pro : PubInfo
    {
        public ComprehensiveInspectionManage_pro()
        {
            Token = @"200190425ComprehensiveInspectionManage_pro";
            log = GetLog();
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
      
                    case "DownloadTaskWithPager_v2":
                        Trans.Result = DownloadTaskWithPager_v2(Row);
                        break;
                    case "DownloadTaskObjectStandardWithPager":
                        Trans.Result = DownloadTaskObjectStandardWithPager(Row);
                        break;
                    case "UploadTask":
                        Trans.Result = UploadTask(Row, Trans.Attribute);
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
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                //GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace + Environment.NewLine + ex.Source + Environment.NewLine + Trans.Attribute);
                Trans.Result = new ApiResult(false, ex.Message).toJson();
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


                        List<dynamic> Incidents = new List<dynamic>();

                        // 是否存在报事
                        sql = "SELECT a.TaskId,a.PointId,a.IncidentID FROM Tb_CP_TaskPointIncident a WHERE a.TaskId=@TaskId;";
                        var taskIncidents = conn.Query(sql, new { TaskId = dataRow["TaskId"] });
                        if (taskIncidents.Count() > 0)
                        {
                            //sql = @"SELECT @TaskId AS TaskId,@PointId AS PointId,b.IncidentID,b.IncidentNum,b.IncidentContent,b.ClassName,
                            //        isnull(b.EmergencyDegree,0) AS EmergencyDegree,b.RegionalName,b.TypeName,b.Phone
                            //        FROM view_HSPR_IncidentSeach_Filter b
                            //        WHERE IncidentID=@IncidentID";

                            sql = @"SELECT @TaskId AS TaskId,
                                           @PointId AS PointId,
                                           @IncidentID AS IncidentID,
	                                       TaskCode,
										   TaskNum,
                                           TaskMemo AS IncidentContent,
                                           TaskLevelName,
                                           BigReasonName,
                                           SmallReasonName,
                                           LiableUserMobile,
                                           LiableUserName
                                    FROM View_Incident_Task_Filter A
                                    WHERE TaskCode=@IncidentID";  

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
                                        TaskCode = incidentInfo.TaskCode,
                                        TaskNum = incidentInfo.TaskNum,
                                        IncidentContent = incidentInfo.IncidentContent,
                                        TaskLevelName = incidentInfo.TaskLevelName,
                                        BigReasonName = incidentInfo.BigReasonName,
                                        SmallReasonName = incidentInfo.SmallReasonName,
                                        LiableUserMobile = incidentInfo.LiableUserMobile,
                                        LiableUserName = incidentInfo.LiableUserName
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
                string endTime = null;

                if (row.Table.Columns.Contains("EndTime") && !string.IsNullOrEmpty(row["EndTime"].ToString()))
                {
                    endTime = row["EndTime"].ToString();
                }

                // 读取点位
                JArray array = (JArray)JsonConvert.DeserializeObject(row["Data"].ToString());

                int incidentCount = 0;
                string sql = "";

                if (array.Count > 0)
                {
                    using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                    {
                        foreach (JObject item in array)
                        {
                            int PointState = 1;

                            // 点位下对象对应标准
                            string IID = item["IID"].ToString();
                            string ScanTime = item["ScanTime"].ToString();
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

                            //if (!string.IsNullOrEmpty(sql))
                            //{
                            //    conn.Execute(sql);
                            //}

                            // 报事信息
                            if (IncidentID.Count > 0)
                            {
                                foreach (JValue _incidentId in IncidentID)
                                {
                                    sql += $@"INSERT INTO Tb_CP_TaskPointIncident(TaskIncidentId, CommId, TaskId, TaskPointId, PointId, IncidentId)
                                        SELECT newid() AS TaskIncidentId, CommId, TaskId, TaskPointId, PointId, '{_incidentId.ToString()}' AS IncidentId
                                        FROM Tb_CP_TaskPoint WHERE TaskPointId = '{IID}';";

                                    //conn.Execute(sql, new
                                    //{
                                    //    TaskPointId = IID,
                                    //    IncidentId = _incidentId.ToString(),
                                    //    DidUserCode = Global_Var.LoginUserCode
                                    //});
                                }
                            }

                            if (string.IsNullOrEmpty(ScanTime))
                            {
                                ScanTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            // 更新点位信息
                            sql += $@"UPDATE Tb_CP_TaskPoint SET DidTime='{ScanTime}',DidUserCode='{Global_Var.LoginUserCode}',
                                RectificationNum=(isnull(RectificationNum,0)+{IncidentID.Count}),PointState='{PointState}' 
                                WHERE TaskPointId='{IID}';";
                            //conn.Execute(sql, new
                            //{
                            //    DidTime = ScanTime,
                            //    DidUserCode = Global_Var.LoginUserCode,
                            //    RectificationNum = IncidentID.Count,
                            //    PointState = PointState,
                            //    TaskPointId = IID
                            //});
                        }

                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        IDbTransaction trans = conn.BeginTransaction();

                        try
                        {
                            conn.Execute(sql, null, trans);
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw ex;
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
                    sql = @"UPDATE Tb_CP_Task SET DidBeginTime=@DidBeginTime,DidEndTime=@DidEndTime,PlanState=@PlanState,
                                RectificationNum=(isnull(RectificationNum,0)+@RectificationNum),DidAllPointNum=(@Count1+@Count2),
                                DidMustCheckPointNum=@Count1, DidOtherPointPercentage=cast(round(@OtherPercent,2) AS NUMERIC(5,2))*100   
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

                return JSONHelper.FromString(false, ex.Message + ex.StackTrace);
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
                                                    LEFT JOIN Tb_Incident_Task d ON a.IncidentId=d.TaskCode
                                                    LEFT JOIN Tb_HSPR_IncidentRegional e ON d.RegionalID=e.RegionalID
                                                    LEFT JOIN Tb_CP_Point f ON a.PointId=f.PointId
                                                WHERE a.CheckTime IS NULL) y ON x.TaskId=y.TaskId
                                            WHERE TaskRoleCode IN(SELECT a.RoleCode FROM Tb_Sys_UserRole a LEFT JOIN Tb_Sys_Role b ON a.RoleCode=b.RoleCode
                                                                    WHERE a.UserCode='{0}' AND b.SysRoleCode IS NOT NULL AND b.SysRoleCode<>'')",
                                                                        Global_Var.LoginUserCode);

                    sql += @" AND (SELECT count(1) FROM Tb_Incident_Task WHERE TaskCode=y.IncidentID AND isnull(IsDelete,0)=0)>0";

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
                string sql = string.Format(@"SELECT DISTINCT 
                                                y.IncidentID, 
                                                y.PointId,
                                                y.TaskId,
                                                y.PlanName,
                                                y.PointName,
                                                y.BeginTime,
                                                y.EndTime,
                                                y.IncidentNum,
                                                y.IncidentDate,
                                                y.TypeName,
                                                y.DealLimit,
                                                y.EmergencyDegree,
                                                y.IncidentContent,
                                                y.DealMan,
                                                y.TaskState,
                                                y.DealState,
                                                y.DealSituation,
                                                y.MainEndDate FROM Tb_CP_Task x 
                                                RIGHT JOIN (
                                                SELECT a.PointId,b.TaskId,c.PlanName,f.PointName,b.BeginTime,b.EndTime,d.TaskCode AS IncidentID,
                                                d.TaskNum AS IncidentNum,
                                                d.OrderDate AS IncidentDate,
                                                ISNULL(d.SmallReasonName,d.BigReasonName) as TypeName,
                                                d.DealHours AS DealLimit,
                                                d.TaskLevelName AS EmergencyDegree,
                                                d.TaskMemo AS IncidentContent,
                                                d.OprUser AS DealMan,
                                                d.TaskState AS TaskState,
                                                d.DealSituation,
                                                d.TaskCompleteDate AS MainEndDate,
                                                CASE WHEN d.TaskState='已完结'OR d.TaskState ='已完成未完结' THEN 0 ELSE 1 END AS DealState
                                            FROM Tb_CP_TaskPointIncident a LEFT JOIN Tb_CP_Task b ON a.TaskId=b.TaskId
                                            LEFT JOIN Tb_CP_Plan c ON b.PlanId=c.PlanId
                                            LEFT JOIN view_Incident_Task_Filter d ON a.IncidentId=d.TaskCode
                                            LEFT JOIN Tb_CP_Point f ON a.PointId=f.PointId
                                                WHERE a.CheckTime IS {0} NULL) y ON x.TaskId=y.TaskId
                                            WHERE TaskRoleCode IN(SELECT a.RoleCode FROM Tb_Sys_UserRole a LEFT JOIN Tb_Sys_Role b ON a.RoleCode=b.RoleCode
                                                                    WHERE a.UserCode='{1}' AND b.SysRoleCode IS NOT NULL AND b.SysRoleCode<>'')",
                                                                    state == "1" ? "NOT" : "", Global_Var.LoginUserCode);


                IEnumerable<dynamic> rectificationSet = GetListDapper(out int pageCount, out int count, sql, pageIndex, pageSize, "IncidentDate", 0, "TaskId", conn);

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
                                WHERE b.CommID={commID} AND a.CheckTime IS NULL AND a.CheckUserCode IS NULL AND
                                    b.TaskRoleCode IN(SELECT a.RoleCode FROM Tb_Sys_UserRole a 
                                    LEFT JOIN Tb_Sys_Role b ON a.RoleCode=b.RoleCode
                                    WHERE a.UserCode='{Global_Var.LoginUserCode}' AND b.SysRoleCode IS NOT NULL AND b.SysRoleCode<>'')
                                GROUP BY a.TaskId,c.PlanName,b.BeginTime,b.EndTime";


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

            #region 基础数据校验
            // 任务Code
            if (!row.Table.Columns.Contains("TaskCode") || string.IsNullOrEmpty(row["TaskCode"].ToString()))
            {
                return new ApiResult(false, "缺少参数TaskCode").toJson();
            }
            string TaskCode = row["TaskCode"].ToString();

            // 当前任务状态
            string TaskState;

            // 任务所属人员
            string OprUser;

            //任务分派人员
            string AssignUserName;
            //任务分派人员Code
            string AssignUserCode;

            //任务责任人
            string LiableUserName;
            //任务责任人Code
            string LiableUserCode;

            // CommID
            string CommID;
            // 查询任务是否存在
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dynamic task = conn.QueryFirstOrDefault("SELECT TaskCode,CommID,TaskState,OprUser,AssignUserName,AssignUser,LiableUser,LiableUserName FROM View_Incident_Task_Filter WHERE TaskCode = @TaskCode", new { TaskCode = TaskCode }, null, null, CommandType.Text);
                if (null == task)
                {
                    return new ApiResult(false, "该任务不存在").toJson();
                }
                TaskState = task.TaskState;
                OprUser = task.OprUser;
                AssignUserName = task.AssignUserName;
                AssignUserCode = Convert.ToString(task.AssignUser);
                LiableUserName = task.LiableUserName;
                LiableUserCode = Convert.ToString(task.LiableUser);
                CommID = Convert.ToString(task.CommID);
            }
            // 退回描述
            if (!row.Table.Columns.Contains("ProcessContent") || string.IsNullOrEmpty(row["ProcessContent"].ToString()))
            {
                return new ApiResult(false, "缺少参数ProcessContent").toJson();
            }
            string ProcessContent = row["ProcessContent"].ToString();

            #endregion

            #region 任务进程数据准备
            DateTime DateNow = DateTime.Now;
            string ProcessCode = Guid.NewGuid().ToString();
            string BussType;
            string InfoType;

            // 上级责任人手机号
            string LiableMoble = "";

            // 提交到ERP无需推送
            if ("已提交未分派".Equals(TaskState))
            {
                BussType = "分派人退回接待人";
                InfoType = "【" + Global_Var.LoginUserName + "】退回任务到【" + OprUser + "】";
                TaskState = "已登记未提交";
            }
            else if ("已分派未完成".Equals(TaskState))
            {
                BussType = "责任人退回分派人";
                InfoType = "【" + Global_Var.LoginUserName + "】退回任务到【" + AssignUserName + "】";
                TaskState = "已提交未分派";
                using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    LiableMoble = conn.QueryFirstOrDefault<string>("SELECT MobileTel FROM Tb_Sys_User WHERE UserCode = @UserCode", new { UserCode = AssignUserCode });
                }
            }
            else if ("已完成未完结".Equals(TaskState))
            {
                BussType = "分派人退回责任人";
                InfoType = "【" + Global_Var.LoginUserName + "】退回任务到【" + LiableUserName + "】";
                TaskState = "已分派未完成";
                using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    LiableMoble = conn.QueryFirstOrDefault<string>("SELECT MobileTel FROM Tb_Sys_User WHERE UserCode = @UserCode", new { UserCode = LiableUserCode });
                }
            }
            // 中南置地状态为已完成未关闭
            else if ("已完成未关闭".Equals(TaskState))
            {
                BussType = "回访人退回责任人";
                InfoType = "【" + Global_Var.LoginUserName + "】退回任务到【" + LiableUserName + "】";
                TaskState = "已分派未完成";
                using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    LiableMoble = conn.QueryFirstOrDefault<string>("SELECT MobileTel FROM Tb_Sys_User WHERE UserCode = @UserCode", new { UserCode = LiableUserCode });
                }
            }
            else
            {
                BussType = "";
                InfoType = "";
            }

            ProcessContent += "【APP】";
            string ProcessUser = Global_Var.LoginUserName;
            string ProcessDate = DateNow.ToString();
            string RemindersContent = ProcessContent;
            string RemindersUser = Global_Var.LoginUserName;
            string RemindersDate = DateNow.ToString();

            #endregion

            #region 进行任务进程添加
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {

                #region 准备DynamicParameters
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ProcessCode", ProcessCode);
                parameters.Add("TaskCode", TaskCode);
                parameters.Add("BussType", BussType);
                parameters.Add("InfoType", InfoType);
                parameters.Add("ProcessContent", ProcessContent);
                parameters.Add("ProcessDocs", null);
                parameters.Add("ProcessUser", null);
                parameters.Add("ProcessDate", ProcessDate);
                parameters.Add("RepeatIncidnetDate", null);
                parameters.Add("RemindersContent", RemindersContent);
                parameters.Add("RemindersDocs", null);
                parameters.Add("RemindersNoticeMethod", null);
                parameters.Add("RemindersNoticeUser", null);
                parameters.Add("RemindersUser", RemindersUser);
                parameters.Add("RemindersDate", RemindersDate);
                parameters.Add("LiableCompanyCode", null);
                parameters.Add("ThreeCompanyCode", null);
                parameters.Add("UpgradeType", null);
                parameters.Add("TaskLevelCode", null);
                parameters.Add("Negotiation", null);
                parameters.Add("CC", null);
                parameters.Add("NegotiationContent", null);
                parameters.Add("NegotiationDocs", null);
                parameters.Add("NegotiationNoticeSys", null);
                parameters.Add("NegotiationNoticeMsg", null);
                parameters.Add("NegotiationScope", null);
                parameters.Add("NegotiationNoticeUser", null);
                parameters.Add("NegotiationStartUser", null);
                parameters.Add("NegotiationStartDate", null);
                parameters.Add("LitigationMemo", null);
                parameters.Add("LitigationDocs", null);
                parameters.Add("LitigationNoticeUser", null);
                parameters.Add("LitigationNotice", null);
                parameters.Add("LitigationStartDate", null);
                parameters.Add("LitigationStartUser", null);
                parameters.Add("VisitUser", null);
                parameters.Add("VisitDate", null);
                parameters.Add("VisitedUser", null);
                parameters.Add("VisiteMemo", null);
                parameters.Add("VisiteIsTimely", null);
                parameters.Add("VisiteIsSolve", null);
                parameters.Add("VisiteIsCharge", null);
                parameters.Add("VisiteEvaluation", null);
                parameters.Add("VisiteSuggest", null);
                parameters.Add("TimeOutRemark", null);
                parameters.Add("NoticeRange", null);
                parameters.Add("ReporterMan", null);
                parameters.Add("ReporterDate", null);
                parameters.Add("NegotiationCode", null);
                parameters.Add("CCCode", null);
                parameters.Add("NegotiationNoticeUserCode", null);
                parameters.Add("NetAddr", null);
                parameters.Add("LitigationNoticeUserCode", null);
                parameters.Add("ReplayContent", null);
                parameters.Add("ReplayCount", null);
                parameters.Add("VisiteExitType", null);
                #endregion

                if (conn.Execute("Proc_Incident_TaskProcess_Insert", parameters, null, null, CommandType.StoredProcedure) <= 0)
                {
                    return new ApiResult(false, "添加任务进程失败,请重试").toJson();
                }
            }
            #endregion

            #region 准备任务退回数据
            string AssignExitReason = ProcessContent;
            #endregion

            #region 进行任务退回,更新任务信息
            try
            {
                using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    if (conn.Execute("Proc_Incident_Task_UpdateState", new { TaskCode = TaskCode, TaskState = TaskState, AssignExitReason = AssignExitReason }, null, null, CommandType.StoredProcedure) <= 0)
                    {
                        Proc_Incident_TaskProcess_Delete(ProcessCode);
                        return new ApiResult(false, "退回失败,请重试").toJson();
                    }

                    // 删除实签人图片
                    conn.Execute(@"DELETE FROM Tb_Incident_Task_File WHERE FileType='报事处理签名图片' AND TaskCode=@TaskCode", new { TaskCode = TaskCode });

                    return new ApiResult(true, "退回成功").toJson();
                }
            }
            catch (Exception ex)
            {
                log.Error("Proc_Incident_Task_UpdateState时出现了一个异常," + ex.Message);
                Proc_Incident_TaskProcess_Delete(ProcessCode);
                return new ApiResult(false, "退回失败,请重试").toJson();
            }
            #endregion
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

                return JSONHelper.FromString(true, "验收成功");
            }
        }

        /// <summary>
        /// 删除任务进程
        /// </summary>
        /// <param name="ProcessCode"></param>
        /// <returns></returns>
        public static bool Proc_Incident_TaskProcess_Delete(string ProcessCode)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    return conn.Execute("Proc_Incident_TaskProcess_Delete", new { ProcessCode = ProcessCode }, null, null, CommandType.StoredProcedure) > 0;
                }
            }
            catch (Exception ex)
            {
                log.Error("Proc_Incident_TaskProcess_Delete(" + ProcessCode + ")时出现了一个异常," + ex.Message);
                return false;
            }

        }

    }
}
