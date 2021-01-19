using Common.Extenions;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Business
{
    public partial class GWAppHomeManage
    {

        class TaskInfo
        {
            public string TaskType { get; set; }
            public string LastExecuteTime { get; set; }
        }

        class TaskDetailInfo
        {
            public TaskDetailInfo(string taskType, List<TaskRecentlyExecuteInfo> detailList)
            {
                this.TaskType = taskType;
                this.DetailList = detailList;
            }

            public string TaskType { get; set; }
            public List<TaskRecentlyExecuteInfo> DetailList { get; set; }
        }


        class TaskRecentlyExecuteInfo
        {
            public string TaskTypeName { get; set; }
            public string PlanState { get; set; }
            public string DidBeginTime { get; set; }
            public string DidEndTime { get; set; }
            public string TaskBeginTime { get; set; }
        }



        private string GetSunPropertyTask(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }

            var communityId = row["CommunityId"].ToString();
            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }
            var commID = AppGlobal.StrToInt(community.CommID);

            PubConstant.tw2bsConnectionString = Global_Fun.Tw2bsConnectionString("1");
            PubConstant.hmWyglConnectionString = GetConnectionStr(community);

            var personCount = 0;
            var safeDate = "";
            var fireDate = "";
            var list = new List<TaskInfo>();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT COUNT(*) AS COUNT FROM Tb_Sys_RoleData a
                            LEFT JOIN Tb_Sys_Role b ON a.RoleCode = b.RoleCode
                            LEFT JOIN Tb_Sys_Department c ON b.DepCode = c.DepCode
                            LEFT JOIN tb_sys_userrole d ON b.RoleCode = d.RoleCode
                            LEFT JOIN tb_sys_user e ON d.UserCode = e.UserCode
                            WHERE a.CommID = @CommID AND e.IsDelete=0
                            AND DepType = '项目级'";
                // 长城要求首页显示
                sql = "SELECT COUNT(1) FROM Tb_Sys_User WHERE CommID = @CommID";

                sql = @"SELECT  COUNT(DISTINCT(e.USERCODE)  ) AS Number FROM Tb_Sys_RoleData a
                            LEFT JOIN Tb_Sys_Role b ON a.RoleCode = b.RoleCode
                            LEFT JOIN tb_sys_userrole d ON b.RoleCode = d.RoleCode
                            LEFT JOIN tb_sys_user e ON d.UserCode = e.UserCode
                            WHERE a.CommID = @CommID AND e.IsDelete = 0  ";

                personCount = conn.Query<int>(sql, new { CommID = commID }).FirstOrDefault();
            }

            // 安全
            using (var safeConn = new SqlConnection(Global_Fun.BurstConnectionString(commID, Global_Fun.BURST_TYPE_SAFE)))
            {
                // 安全巡更
                var sql = $@"SELECT TOP 1 convert(varchar(20),LastExecuteTime,120) FROM
                            (
                                SELECT BeginTime AS LastExecuteTime FROM Tb_CP_Task_Safe
                                WHERE IsClose=0 AND IsDelete=0 AND BeginTime<=getdate() AND TaskLevelName='安全巡更' AND CommId={commID}
                            ) AS t
                            ORDER BY LastExecuteTime DESC;";

                safeDate = safeConn.Query<string>(sql).FirstOrDefault();
            }

            // 设备
            using (var eqConn = new SqlConnection(Global_Fun.BurstConnectionString(commID, Global_Fun.BURST_TYPE_EQ)))
            {
                var sql = $@"SELECT TOP 1 convert(varchar(20),LastExecuteTime,120) FROM
                            (
                                SELECT TaskBeginTime AS LastExecuteTime FROM Tb_Eq_Task_Inspection
                                WHERE IsClose=0 AND IsDelete=0 AND TaskBeginTime<=getdate() AND IsFireControl=0 AND CommId={commID}
                            ) AS t
                            ORDER BY LastExecuteTime DESC;


                            SELECT TOP 1 convert(varchar(20),LastExecuteTime,120) FROM     
                            (
                                SELECT TaskBeginTime AS LastExecuteTime FROM Tb_Eq_Task_Inspection
                                WHERE IsClose=0 AND IsDelete=0 AND TaskBeginTime<=getdate() AND IsFireControl=1 AND CommId={commID}
                            ) AS t
                            ORDER BY LastExecuteTime DESC;";

                var reader = eqConn.QueryMultiple(sql);
                var eqDate = reader.Read<string>().FirstOrDefault();
                fireDate = reader.Read<string>().FirstOrDefault();

                // 比较安全最大值
                var maxDate = AppGlobal.StrToDate(safeDate) > AppGlobal.StrToDate(fireDate) ? safeDate : fireDate;

                list.Add(new TaskInfo
                {
                    TaskType = "安全巡检",
                    LastExecuteTime = maxDate ?? "暂无巡检记录"
                });

                list.Add(GetEnvironment(commID));

                list.Add(new TaskInfo
                {
                    TaskType = "设备巡检",
                    LastExecuteTime = eqDate ?? "暂无巡检记录"
                });


            }
            return new ApiResult(true, new { PeoCount = personCount, List = list }).toJson();
        }

        /// <summary>
        /// 环境巡检 记录
        /// </summary>
        /// <param name="commId"></param>
        /// <returns></returns>
        private TaskInfo GetEnvironment(int commId)
        {
            // 环境
            using (var ambientConn = new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_AMBIENT)))
            {
                var sql = $@"SELECT TOP 1 convert(varchar(20),LastExecuteTime,120) FROM
                            (
                                SELECT BeginTime AS LastExecuteTime FROM Tb_CP_Task_Ambient
                                WHERE IsClose=0 AND IsDelete=0 AND BeginTime<=getdate() AND CommId=@CommID
                            ) AS t
                            ORDER BY LastExecuteTime DESC";

                var ambientDate = ambientConn.Query<string>(sql, new { CommID = commId }).FirstOrDefault();
                return new TaskInfo
                {
                    TaskType = "环境巡检",
                    LastExecuteTime = ambientDate ?? "暂无巡检记录"
                };
            }
        }

        /// <summary>
        /// 获取单个模块详情
        /// </summary>
        /// <returns></returns>
        /// 
        private string GetSunPropertyTaskDetail(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }

            if (!row.Table.Columns.Contains("TaskType") || string.IsNullOrEmpty(row["TaskType"].AsString()))
            {
                return new ApiResult(false, "缺少参数TaskType").toJson();
            }

            var communityId = row["CommunityId"].ToString();
            var taskType = row["TaskType"].ToString();
            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }
            var commId = AppGlobal.StrToInt(community.CommID);

            PubConstant.tw2bsConnectionString = Global_Fun.Tw2bsConnectionString("1");
            PubConstant.hmWyglConnectionString = GetConnectionStr(community);
            try {
            
            var list = new List<TaskDetailInfo>();
            switch (taskType)
            {
                case "环境巡检":
                    list.AddRange(GetEnviromentTaskRecentlyExecuteInfo(commId));
                    break;
                case "设备巡检":
                    list.AddRange(GetEquipmentExecuteInfo(commId));
                    break;
                case "安全巡检":
                    list.AddRange(GetSecureExecuteInfo(commId));
                    break;
            }
            return new ApiResult(true, list).toJson();
            }
            catch (Exception o)
            {
                return new ApiResult(false, o.Message).toJson();
            }

        }


        /// <summary>
        /// 获取环境任务最近执行信息
        /// </summary>
        private IEnumerable<TaskDetailInfo> GetEnviromentTaskRecentlyExecuteInfo(int commId)
        {
            using (var conn = new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_AMBIENT)))
            {
                var sql = @"SELECT TOP 5 TaskTypeName,
                                CASE PlanState WHEN 2 THEN '已完成' ELSE '进行中' END AS PlanState,
                                CASE WHEN DidEndTime IS NOT NULL THEN DidEndTime ELSE EndTime END AS DidEndTime,
                                convert(varchar(19),DidBeginTime,120) AS DidBeginTime,
                                convert(varchar(20),BeginTime,120) AS TaskBeginTime
                            FROM Tb_CP_Task_Ambient
                            WHERE IsClose=0 AND IsDelete=0 AND BeginTime<=getdate() AND CommId=@CommID AND TaskTypeName='环境保洁'
                            ORDER BY BeginTime DESC;

                            SELECT TOP 5 TaskTypeName,
                                CASE PlanState WHEN 2 THEN '已完成' ELSE '进行中' END AS PlanState,
                                CASE WHEN DidEndTime IS NOT NULL THEN DidEndTime ELSE EndTime END AS DidEndTime,
                                convert(varchar(19),DidBeginTime,120) AS DidBeginTime,
                                convert(varchar(20),BeginTime,120) AS TaskBeginTime
                            FROM Tb_CP_Task_Ambient
                            WHERE IsClose=0 AND IsDelete=0 AND BeginTime<=getdate() AND CommId=@CommID AND TaskTypeName='绿化养护'
                            ORDER BY BeginTime DESC;

                            SELECT TOP 5 TaskTypeName,
                                CASE PlanState WHEN 2 THEN '已完成' ELSE '进行中' END AS PlanState,
                                CASE WHEN DidEndTime IS NOT NULL THEN DidEndTime ELSE EndTime END AS DidEndTime,
                                convert(varchar(19),DidBeginTime,120) AS DidBeginTime,
                                convert(varchar(20),BeginTime,120) AS TaskBeginTime
                            FROM Tb_CP_Task_Ambient
                            WHERE IsClose=0 AND IsDelete=0 AND BeginTime<=getdate() AND CommId=@CommID AND TaskTypeName='四害消杀'
                            ORDER BY BeginTime DESC;

                            SELECT TOP 5 TaskTypeName,
                                CASE PlanState WHEN 2 THEN '已完成' ELSE '进行中' END AS PlanState,
                                CASE WHEN DidEndTime IS NOT NULL THEN DidEndTime ELSE EndTime END AS DidEndTime,
                                convert(varchar(19),DidBeginTime,120) AS DidBeginTime,
                                convert(varchar(20),BeginTime,120) AS TaskBeginTime
                            FROM Tb_CP_Task_Ambient
                            WHERE IsClose=0 AND IsDelete=0 AND BeginTime<=getdate() AND CommId=@CommID AND TaskTypeName='垃圾清运'
                            ORDER BY BeginTime DESC;";

                var reader = conn.QueryMultiple(sql, new { CommID = commId });
                List<TaskRecentlyExecuteInfo> task1 = reader.Read<TaskRecentlyExecuteInfo>().ToList();
                List<TaskRecentlyExecuteInfo> task2 = reader.Read<TaskRecentlyExecuteInfo>().ToList();
                List<TaskRecentlyExecuteInfo> task3 = reader.Read<TaskRecentlyExecuteInfo>().ToList();
                List<TaskRecentlyExecuteInfo> task4 = reader.Read<TaskRecentlyExecuteInfo>().ToList();
               
                var list = new List<TaskDetailInfo>();

                if (task1.Count>0)
                {
                    list.Add(new TaskDetailInfo("环境保洁", task1));
                }
                if (task2.Count>0)
                {
                    list.Add(new TaskDetailInfo("绿化养护", task2));
                }
                if (task3.Count>0)
                {
                    list.Add(new TaskDetailInfo("四害消杀", task3));
                }
                if (task4.Count>0)
                {
                    list.Add(new TaskDetailInfo("垃圾清运", task4));
                }
                return list;
            }
        }

        /// <summary>
        /// 设备任务最近执行信息
        /// </summary>
        private IEnumerable<TaskDetailInfo> GetEquipmentExecuteInfo(int commId)
        {
            using (var conn = new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_EQ)))
            {

                var sql = $@"SELECT TOP 5 TaskId ,PlanName,
                                TaskStatue AS PlanState,ExecuteBeginTime AS DidBeginTime,
                                (case when ExecuteEndTime IS NOT NULL THEN ExecuteEndTime ELSE TaskEndTime end) AS DidEndTime,
                                convert(varchar(20),TaskBeginTime,120) AS TaskBeginTime, TaskTypeName='设备巡检'
                            FROM Tb_Eq_Task_Inspection
                            WHERE IsClose=0 AND IsDelete=0 AND TaskBeginTime<=getdate() AND IsFireControl=0
                            AND CommId=@CommID
                            ORDER BY TaskBeginTime DESC";

                var reader = conn.QueryMultiple(sql, new { CommID = commId });
                List<TaskRecentlyExecuteInfo> task1 = reader.Read<TaskRecentlyExecuteInfo>().ToList();

                var list = new List<TaskDetailInfo>();
                if (task1.Count>0)
                {
                    list.Add(new TaskDetailInfo("设备巡检", task1));
                }
                return list;
            }
        }

        /// <summary>
        /// 安全任务最近执行信息
        /// </summary>
        private IEnumerable<TaskDetailInfo> GetSecureExecuteInfo(int commId)
        {
            using (IDbConnection safeConn = new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_SAFE)),
                eqConn = new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_EQ)))
            {
                var sql = @"SELECT TOP 5 TaskId,PlanName,PlanState,DidBeginTime,
                                (case when DidEndTime IS NOT NULL THEN DidEndTime ELSE EndTime end) AS DidEndTime,
                                convert(varchar(20),BeginTime,120) AS TaskBeginTime, TaskTypeName='安全巡更'
                            FROM  Tb_CP_Task_Safe
                            WHERE IsClose=0 AND IsDelete=0 AND BeginTime<=getdate() AND CommId=@CommID AND  TaskLevelName='安全巡更'
                            ORDER BY BeginTime DESC;";

                List<TaskRecentlyExecuteInfo> task1 = safeConn.Query<TaskRecentlyExecuteInfo>(sql, new { CommID = commId }).ToList();

                sql = @"SELECT TOP 5 TaskId,PlanName,TaskStatue AS PlanState,ExecuteBeginTime AS DidBeginTime,
                            (case when ExecuteEndTime IS NOT NULL THEN ExecuteEndTime ELSE TaskEndTime end) AS DidEndTime,
                            convert(varchar(20),TaskBeginTime,120) AS TaskBeginTime, TaskTypeName='消防巡检'
                        FROM  Tb_Eq_Task_Inspection
                        WHERE IsClose=0 AND IsDelete=0 AND TaskBeginTime<=getdate() AND IsFireControl=1 AND CommId=@CommID
                        ORDER BY TaskBeginTime DESC";

                List<TaskRecentlyExecuteInfo> task2 = eqConn.Query<TaskRecentlyExecuteInfo>(sql, new { CommID = commId }).ToList();

                var list = new List<TaskDetailInfo>();

                if (task1.Count>0)
                {
                    list.Add(new TaskDetailInfo("安全巡更", task1));
                }
                if (task2.Count>0)
                {
                    list.Add(new TaskDetailInfo("消防巡检", task2));
                }
                return list;
            }
        }
    }
}
