using Common;
using Dapper;
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
using static Dapper.SqlMapper;

namespace Business
{
    /// <summary>
    /// 拜访管理
    /// </summary>
    public partial class VisitManage_bl : PubInfo
    {
        public VisitManage_bl()
        {
            base.Token = "20190612VisitManage_bl";
        }

        public bool? _isHousekeeper { get; set; }

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
                    case "DownloadTask":                                // 下载任务数据
                        Trans.Result = DownloadTask(Row);
                        break;
                    case "GetTaskRooms":                                // 获取任务下点位信息
                        Trans.Result = GetTaskRooms(Row);
                        break;
                    case "GetTaskExecutionInfo":                        // 获取任务执行信息
                        Trans.Result = GetTaskExecutionInfo(Row);
                        break;
                    case "GetAppointmentsList":                         // 获取已预约列表
                        Trans.Result = GetAppointmentsList(Row);
                        break;
                    case "GetVisitWay":                                 // 获取拜访方式
                        Trans.Result = GetVisitWay();
                        break;
                    case "GetAppointmentsUsers":                        // 获取预约人员列表
                        Trans.Result = GetAppointmentsUsers(Row);
                        break;
                    case "MakeAppointment":                             // 预约登记
                        Trans.Result = MakeAppointment(Row);
                        break;
                    case "MakeAppointment_v2":                          // 预约登记
                        Trans.Result = MakeAppointment_v2(Row);
                        break;
                    case "CancelAppointment":                           // 取消预约
                        Trans.Result = CancelAppointment(Row);
                        break;
                    case "GetRelation":                                 // 获取与业主关系
                        Trans.Result = GetRelation();
                        break;
                    case "GetQuestions":                                // 获取问卷问题列表
                        Trans.Result = GetQuestions(Row);
                        break;
                    case "GetQuestions_v2":                             // 获取问卷问题列表
                        Trans.Result = GetQuestions_v2(Row);
                        break;
                    case "SaveQuestion":                                // 保存问题结果
                        Trans.Result = SaveQuestion(Row);
                        break;
                    case "SaveQuestion_v2":                             // 保存问题结果
                        Trans.Result = SaveQuestion_v2(Row);
                        break;
                    case "AddFile":                                     // 添加文件
                        Trans.Result = AddFile(Row);
                        break;
                    case "AddFile_v2":                                  // 添加文件
                        Trans.Result = AddFile_v2(Row);
                        break;
                    case "GetFileList":                                 // 获取文件列表
                        Trans.Result = GetFileList(Row);
                        break;
                    case "DelFile":                                     // 删除文件
                        Trans.Result = DelFile(Row);
                        break;
                    case "AddIncident":                                 // 添加报事
                        Trans.Result = AddIncident(Row);
                        break;
                    case "AddIncident_v2":                              // 添加报事
                        Trans.Result = AddIncident_v2(Row);
                        break;
                    case "GetIncidentList":                             // 获取报事列表
                        Trans.Result = GetIncidentList(Row);
                        break;
                    case "GetVisitSummary":                             // 获取总结
                        Trans.Result = GetVisitSummary(Row);
                        break;
                    case "MakeVisitSummary":                            // 添加/修改总结
                        Trans.Result = MakeVisitSummary(Row);
                        break;
                    case "CompleteVisit":                               // 完成拜访
                        Trans.Result = CompleteVisit(Row);
                        break;
                    case "TaskComplete":                                // 完成任务
                        Trans.Result = TaskComplete(Row);
                        break;
                    case "GetVisitHistory":                             // 拜访历史
                        Trans.Result = GetVisitHistory(Row);
                        break;


                    default:
                        Trans.Result = JSONHelper.FromString(false, "未找到命令" + Trans.Command);
                        break;

                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace + Environment.NewLine + ex.Source);
                Trans.Result = new ApiResult(false, ex.Message + Environment.CommandLine + ex.StackTrace + Environment.NewLine + ex.Source).toJson();
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
            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 任务列表
                var sql = @"SELECT ID AS TaskId,PlanName,
                            convert(varchar(50),PlanBeginTime,120) AS BeginTime,
                            convert(varchar(50),PlanEndTime,120)  AS EndTime,PlanCoverageRate,
                                RoleName AS TaskRoleName,UserName AS TaskUserName,PlanState,
                                (
                                    SELECT ActualCoverageRate FROM Tb_Visit_VisitingExecuteUser WITH(NOLOCK)
                                    WHERE PlanID=a.ID AND UserCode=@UserCode
                                ) AS RealCoverageRate 
                            FROM Tb_Visit_Plan a WITH(NOLOCK)
                            WHERE ID IN
                            (
                                SELECT ID FROM Tb_Visit_Plan a WITH(NOLOCK)
                                WHERE a.CommID=@CommID AND isnull(UserCode,'')<>'' AND UserCode LIKE @_UserCode
                                AND isnull(IsDelete,0)=0 AND isnull(IsClose,0)=0 
                                AND convert(VARCHAR(10),a.PlanBeginTime,20)<=convert(VARCHAR(10),getdate(),20) 
                                AND convert(VARCHAR(10),a.PlanEndTime,20)>=convert(VARCHAR(10),getdate(),20) 

                                UNION ALL

                                SELECT ID FROM Tb_Visit_Plan a WITH(NOLOCK)
                                WHERE a.CommID=@CommID AND isnull(a.UserCode,'')=''
                                AND isnull(IsDelete,0)=0 AND isnull(IsClose,0)=0 
                                AND convert(VARCHAR(10),a.PlanBeginTime,20)<=convert(VARCHAR(10),getdate(),20) 
                                AND convert(VARCHAR(10),a.PlanEndTime,20)>=convert(VARCHAR(10),getdate(),20) 
                                AND (SELECT count(1) FROM
                                    (
                                        SELECT value AS RoleCode FROM SplitString(a.RoleCode,',',1)
                                        INTERSECT
                                        (
                                            SELECT RoleCode FROM Tb_Sys_UserRole WITH(NOLOCK) WHERE UserCode=@UserCode AND isnull(RoleCode,'')=''
                                            UNION ALL
                                            SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role WITH(NOLOCK)
                                            WHERE RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WITH(NOLOCK) WHERE UserCode=@UserCode) 
                                            AND isnull(SysRoleCode,'')<>''
                                        )
                                    ) AS t)>0
                            )
                            AND 
                            (
                                SELECT count(1) FROM Tb_Visit_VisitingExecuteUser WITH(NOLOCK)
                                WHERE PlanID=a.ID AND UserCode=@UserCode AND IsComplete=1
                            )=0;";

                var taskResult = conn.Query<TaskSimpleModel>(sql, new
                {
                    CommID = commId,
                    UserCode = Global_Var.LoginUserCode,
                    _UserCode = $"%{ Global_Var.LoginUserCode }%"
                });

                return new ApiResult(true, taskResult).toJson();
            }
        }

        /// <summary>
        /// 获取任务执行信息
        /// </summary>
        private string GetTaskExecutionInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }

            var planId = row["TaskId"].ToString();
            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 覆盖率
                var realCoverageRate = 0m;
                var planCoverageRate = 0m;

                var sql = @"SELECT a.PlanCoverageRate,isnull(b.ActualCoverageRate,0.0) AS ActualCoverageRate 
                            FROM Tb_Visit_Plan a WITH(NOLOCK)
                            LEFT JOIN Tb_Visit_VisitingExecuteUser b WITH(NOLOCK) ON a.ID=b.PlanID AND b.UserCode=@UserCode
                            WHERE a.ID=@PlanID;";

                var data = conn.Query(sql, new
                {
                    PlanID = planId,
                    UserCode = Global_Var.LoginUserCode
                }).FirstOrDefault();

                if (data != null)
                {
                    realCoverageRate = data.ActualCoverageRate;
                    planCoverageRate = data.PlanCoverageRate;
                }

                // 角标统计
                GetBadge(commId, planId, out int unvisitCount, out int appointmentCount, out int visitedCount, conn);

                return new ApiResult(true, new
                {
                    PlanCoverageRate = planCoverageRate,
                    RealCoverageRate = realCoverageRate,
                    UnvisitCount = unvisitCount,
                    AppointmentCount = appointmentCount,
                    VisitedCount = visitedCount
                }).toJson();
            }
        }

        /// <summary>
        /// 获取任务下房屋
        /// </summary>
        private string GetTaskRooms(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("State") || string.IsNullOrEmpty(row["State"].ToString()))
            {
                return JSONHelper.FromString(false, "拜访状态不能为空");
            }

            var planId = row["TaskId"].ToString();
            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var state = AppGlobal.StrToInt(row["State"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var condition = "0";
                if (state == 0)
                {
                    condition = GetManageRoomsQuerySql(commId, planId, Convert.ToBoolean(state), conn);
                }
                else
                {
                    condition = @"SELECT RoomID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK) 
                                  WHERE PlanID=@PlanID AND VisitUserCode=@UserCode AND CompleteTime IS NOT NULL AND isnull(IsDelete,0)=0";
                }

                var sql = $@"SELECT a.RoomID,a.RoomSign,a.RoomName,d.CustID,e.CustName,e.MobilePhone,b.BuildName,
                                convert(nvarchar(20),a.BuildSNum) AS BuildSNum,
                                convert(nvarchar(20),a.UnitSNum) AS UnitSNum,
                                convert(nvarchar(20),a.FloorSNum) AS FloorSNum,
                                isnull(a.UnitName,convert(nvarchar(20),a.UnitSNum)) AS UnitName,
                                CASE WHEN (SELECT count(1) FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK)
                                            WHERE RoomID=a.RoomID AND CompleteTime IS NOT NULL
                                            AND convert(varchar(4),CompleteTime,120)=convert(varchar(4),getdate(),120))>0 THEN 1
                                ELSE 0 END AS ThisYearHasVisited  
                            FROM Tb_HSPR_Room a WITH(NOLOCK)
                            LEFT JOIN Tb_HSPR_Building b WITH(NOLOCK) ON a.CommID=b.CommID AND a.BuildSNum=b.BuildSNum AND isnull(b.IsDelete,0)=0 
                            LEFT JOIN Tb_HSPR_CustomerLive d WITH(NOLOCK) ON a.RoomID=d.RoomID AND d.IsDelLive=0 AND d.IsActive=1
                            LEFT JOIN Tb_HSPR_Customer e WITH(NOLOCK) ON d.CustID=e.CustID AND e.CommID=@CommID 
                            WHERE a.CommID=@CommID AND a.RoomID IN
                            (
                                { condition }
                            )";

                // 如有传入房屋名称，这使用房屋名称模糊查询
                if (row.Table.Columns.Contains("RoomName") && !string.IsNullOrEmpty(row["RoomName"].ToString()))
                {
                    sql += $@" AND RoomName LIKE '%{row["RoomName"].ToString()}%'";
                }

                sql += " ORDER BY a.RoomID";

                var data = conn.Query(sql, new { CommID = commId, PlanID = planId, UserCode = Global_Var.LoginUserCode });

                var tree = new List<BuildUnitTreeSimpleModel<dynamic>>();

                foreach (dynamic item in data)
                {
                    var buildInfo = tree.Find(o => o.BuildSNum == item.BuildSNum && o.UnitSNum == item.UnitSNum);
                    if (buildInfo == null)
                    {
                        buildInfo = new BuildUnitTreeSimpleModel<dynamic>()
                        {
                            BuildSNum = item.BuildSNum,
                            BuildName = item.BuildName,
                            UnitSNum = item.UnitSNum,
                            UnitName = item.UnitName
                        };
                        buildInfo.Rooms = new List<dynamic>();

                        tree.Add(buildInfo);
                    }

                    buildInfo.Rooms.Add(item);
                }

                return new ApiResult(true, tree).toJson();
            }
        }

        /// <summary>
        /// 获取已预约信息
        /// </summary>
        private string GetAppointmentsList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var planId = row["TaskId"].ToString();
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

            var sql = $@"SELECT a.ID AS IID,RoomID,RoomSign,RoomName,CustID,CustName,a.ContactTelephone AS Mobile,AppointmentUserCode,
                                b.UserName AS AppointmentUserName,AppointmentObject,AppointmentTime,
                                c.VisitWay,a.AppointmentVisitWayID AS VisitWayID
                            FROM Tb_Visit_VisitingCustomersDetail a WITH(NOLOCK)
                            LEFT JOIN Tb_Sys_User b WITH(NOLOCK) ON a.AppointmentUserCode=b.UserCode
                            LEFT JOIN Tb_Visit_VisitWay c WITH(NOLOCK) ON a.AppointmentVisitWayID=c.ID
                            WHERE a.PlanID='{ planId }' AND a.AppointmentUserCode='{ Global_Var.LoginUserCode }' 
                            AND isnull(a.CompleteTime,'')='' AND isnull(a.IsDelete,0)=0 ";

            //var tmp = GetManageRoomsQuerySql(commId, planId);

            //sql += $" AND RoomID IN({tmp})";

            var dataTable = GetList(out int pageCount, out int count, sql, pageIndex, pageSize, "AppointmentTime", 1, "IID",
                PubConstant.hmWyglConnectionString).Tables[0];

            var json = JSONHelper.FromString(true, dataTable);
            json = json.Insert(json.Length - 1, $",\"PageCount\":{pageCount}");
            return json;
        }

        #region 预约登记
        /// <summary>
        /// 获取预约人员列表
        /// </summary>
        private string GetAppointmentsUsers(DataRow row)
        {
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋id不能为空");
            }
            var planId = row["TaskId"].ToString();
            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT UserCodes AS UserCode FROM
                            (
                              SELECT STUFF((SELECT ','+x.UserCode FROM Tb_Sys_User x
                                INNER JOIN Tb_Sys_UserRole y ON x.UserCode=y.UserCode
                                LEFT JOIN Tb_Sys_Department z ON x.DepCode=z.DepCode
                                WHERE y.RoleCode=a.RoleCode AND z.orgType IN ('421','422') AND x.IsDelete=0 FOR XML PATH('')), 1, 1, '') AS UserCodes
                              FROM Tb_Sys_RoleData a WITH(NOLOCK)
                              LEFT JOIN Tb_Sys_Role b WITH(NOLOCK) ON a.RoleCode=b.RoleCode
                              LEFT JOIN Tb_HSPR_Community c WITH(NOLOCK) ON a.CommID=c.CommID
                              WHERE c.CommID=@CommID AND a.RoleCode IN
                              (
                                SELECT RoleCode FROM Tb_Visit_Plan WITH(NOLOCK) WHERE ID=@PlanID AND isnull(RoleCode,'')<>''
                                UNION
                                SELECT RoleCode FROM Tb_Sys_Role
                                WHERE SysRoleCode IN
                                (
                                  SELECT RoleCode FROM Tb_Visit_Plan WITH(NOLOCK) WHERE ID=@PlanID AND isnull(RoleCode,'')<>''
                                )
                              )
                            ) AS a WHERE UserCodes IS NOT NULL
                            UNION
                            SELECT UserCode FROM Tb_Visit_Plan WITH(NOLOCK) WHERE ID=@PlanID AND isnull(UserCode,'')<>''";

                var result = conn.Query<string>(sql, new { CommID = commId, PlanID = planId });

                var userCodes = new List<string>();
                foreach (var item in result)
                {
                    userCodes.AddRange(item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                }

                sql = $@"SELECT UserCode,UserName,
                            CASE WHEN (SELECT count(1) FROM 
                            (
                                SELECT UserCode FROM Tb_HSPR_BuildHousekeeper WHERE CommID=@CommID 
                                  AND BuildSNum=(SELECT BuildSNum FROM Tb_HSPR_Room WITH(NOLOCK) WHERE RoomID=@RoomID)
                                UNION
                                SELECT UserCode FROM Tb_HSPR_Room WITH(NOLOCK) WHERE RoomID=@RoomID
                                UNION
                                SELECT UserCode FROM Tb_HSPR_RoomHousekeeper WITH(NOLOCK) WHERE RoomID=@RoomID
                            ) AS x WHERE x.UserCode=a.UserCode)>0 THEN '#管家#' ELSE '' END AS Tip
                        FROM Tb_Sys_User a WITH(NOLOCK)
                        WHERE UserCode IN({ string.Join(",", userCodes) })";

                var users = conn.Query(sql, new { CommID = commId, RoomID = roomId });

                return new ApiResult(true, users).toJson();
            }
        }

        /// <summary>
        /// 获取拜访方式
        /// </summary>
        private string GetVisitWay()
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT ID AS IID,VisitWay,IsUploadEnclosure FROM Tb_Visit_VisitWay WHERE isnull(IsDelete,0)=0 ORDER BY Sort;";

                return new ApiResult(true, conn.Query(sql)).toJson();
            }
        }

        /// <summary>
        /// 预约登记
        /// </summary>
        private string MakeAppointment(DataRow row)
        {
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋id不能为空");
            }
            if (!row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(row["Mobile"].ToString()))
            {
                return JSONHelper.FromString(false, "联系电话不能为空");
            }
            if (!row.Table.Columns.Contains("AppointmentUser") || string.IsNullOrEmpty(row["AppointmentUser"].ToString()))
            {
                return JSONHelper.FromString(false, "预约人不能为空");
            }
            if (!row.Table.Columns.Contains("AppointmentObject") || string.IsNullOrEmpty(row["AppointmentObject"].ToString()))
            {
                return JSONHelper.FromString(false, "预约对象不能为空");
            }
            if (!row.Table.Columns.Contains("AppointmentTime") || string.IsNullOrEmpty(row["AppointmentTime"].ToString()))
            {
                return JSONHelper.FromString(false, "预约时间不能为空");
            }
            if (!row.Table.Columns.Contains("VisitWay") || string.IsNullOrEmpty(row["VisitWay"].ToString()))
            {
                return JSONHelper.FromString(false, "拜访方式不能为空");
            }
            var planId = row["TaskId"].ToString();
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());
            var mobile = row["Mobile"].ToString();
            var appointmentUser = row["AppointmentUser"].ToString();
            var appointmentObject = row["AppointmentObject"].ToString();
            var appointmentTime = row["AppointmentTime"].ToString();
            var visitWay = row["VisitWay"].ToString();
            var iid = default(string);

            if (row.Table.Columns.Contains("IID") && !string.IsNullOrEmpty(row["IID"].ToString()))
            {
                iid = row["IID"].ToString();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var trans = conn.BeginTransaction();

                try
                {
                    var sql = @"";

                    // 修改预约
                    if (!string.IsNullOrEmpty(iid))
                    {
                        sql = @"SELECT count(1) FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK)
                                WHERE ID=@IID AND (isnull(IsDelete,0)=1 OR isnull(CompleteTime,'')<>'');";

                        if (conn.Query<int>(sql, new { IID = iid }, trans).FirstOrDefault() != 0)
                        {
                            return JSONHelper.FromString(false, "预约已取消或已完成拜访");
                        }

                        sql = @"UPDATE Tb_Visit_VisitingCustomersDetail SET 
                                    AppointmentUserCode=@AppointmentUserCode,
                                    AppointmentObject=@AppointmentObject,
                                    AppointmentTime=@AppointmentTime,
                                    AppointmentVisitWayID=@VisitWay,
                                    ContactTelephone=@Mobile,
                                    UpdateUserId=@UserCode,
                                    UpdateTime=getdate()
                                WHERE ID=@IID;";
                    }
                    else
                    {
                        // 查询是否已登记
                        sql = @"SELECT ID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK) 
                                WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0";
                        var id = conn.Query<string>(sql, new { PlanID = planId, RoomID = roomId }, trans).FirstOrDefault();
                        if (id != null)
                        {
                            iid = id;
                            sql = @"UPDATE Tb_Visit_VisitingCustomersDetail SET 
                                    AppointmentUserCode=@AppointmentUserCode,AppointmentObject=@AppointmentObject,AppointmentTime=@AppointmentTime,
                                    AppointmentVisitWayID=@VisitWay,ContactTelephone=@Mobile,UpdateUserId=@UserCode,UpdateTime=getdate()
                                    WHERE ID=@IID;";
                        }
                        else
                        {
                            sql = @"INSERT INTO Tb_Visit_VisitingCustomersDetail(ID,CommID,PlanID,RegionSNum,RegionName,BuildSNum,BuildName,
                                        RoomID,RoomSign,RoomName,RoomSNum,CustID,CustName,AppointmentUserCode,AppointmentObject,AppointmentTime,
                                        AppointmentVisitWayID,ContactTelephone,AddUserId, AddTime,IsDelete) 
                                    SELECT newid(),a.CommID,@PlanID,b.RegionSNum,c.RegionName,b.BuildSNum,b.BuildName,a.RoomID,a.RoomSign,
                                        a.RoomName,a.RoomSNum,d.CustID,e.CustName,@AppointmentUserCode,@AppointmentObject,@AppointmentTime,
                                        @VisitWay,@Mobile,@UserCode,getdate(),0 
                                    FROM Tb_HSPR_Room a WITH(NOLOCK)
                                    LEFT JOIN Tb_HSPR_Building b WITH(NOLOCK) ON a.CommID=b.CommID AND a.BuildSNum=b.BuildSNum
                                    LEFT JOIN Tb_HSPR_Region c WITH(NOLOCK) ON b.CommID=c.CommID AND b.RegionSNum=c.RegionSNum
                                    LEFT JOIN Tb_HSPR_CustomerLive d WITH(NOLOCK) ON a.RoomID=d.RoomID AND d.IsDelLive=0 AND d.IsActive=1
                                    LEFT JOIN Tb_HSPR_Customer e WITH(NOLOCK) ON d.CustID=e.CustID
                                    WHERE a.RoomID=@RoomID;";
                        }
                    }

                    conn.Execute(sql, new
                    {
                        PlanID = planId,
                        RoomID = roomId,
                        IID = iid,
                        AppointmentUserCode = appointmentUser,
                        AppointmentObject = appointmentObject,
                        AppointmentTime = appointmentTime,
                        VisitWay = visitWay,
                        Mobile = mobile,
                        UserCode = Global_Var.LoginUserCode
                    }, trans);

                    // 导入问卷问题
                    ImportQuestion(planId, iid, conn, trans);

                    // 更新计划状态
                    UpdatePlanState(planId, conn, trans);

                    trans.Commit();
                    return JSONHelper.FromString(true, string.IsNullOrEmpty(iid) ? "预约成功" : "修改预约信息成功");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message);
                }
            }
        }

        /// <summary>
        /// 预约登记第二版
        /// </summary>
        private string MakeAppointment_v2(DataRow row)
        {
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋id不能为空");
            }
            if (!row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(row["Mobile"].ToString()))
            {
                return JSONHelper.FromString(false, "联系电话不能为空");
            }
            if (!row.Table.Columns.Contains("AppointmentUser") || string.IsNullOrEmpty(row["AppointmentUser"].ToString()))
            {
                return JSONHelper.FromString(false, "预约人不能为空");
            }
            if (!row.Table.Columns.Contains("AppointmentObject") || string.IsNullOrEmpty(row["AppointmentObject"].ToString()))
            {
                return JSONHelper.FromString(false, "预约对象不能为空");
            }
            if (!row.Table.Columns.Contains("AppointmentTime") || string.IsNullOrEmpty(row["AppointmentTime"].ToString()))
            {
                return JSONHelper.FromString(false, "预约时间不能为空");
            }
            if (!row.Table.Columns.Contains("VisitWay") || string.IsNullOrEmpty(row["VisitWay"].ToString()))
            {
                return JSONHelper.FromString(false, "拜访方式不能为空");
            }
            var planId = row["TaskId"].ToString();
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());
            var mobile = row["Mobile"].ToString();
            var appointmentUser = row["AppointmentUser"].ToString();
            var appointmentObject = row["AppointmentObject"].ToString();
            var appointmentTime = row["AppointmentTime"].ToString();
            var visitWay = row["VisitWay"].ToString();
            var iid = default(string);

            if (row.Table.Columns.Contains("IID") && !string.IsNullOrEmpty(row["IID"].ToString()))
            {
                iid = row["IID"].ToString();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var trans = conn.BeginTransaction();

                try
                {
                    var sql = @"";

                    // 修改预约
                    if (!string.IsNullOrEmpty(iid))
                    {
                        sql = @"SELECT count(1) FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK)
                                WHERE ID=@IID AND (isnull(IsDelete,0)=1 OR isnull(CompleteTime,'')<>'');";

                        if (conn.Query<int>(sql, new { IID = iid }, trans).FirstOrDefault() != 0)
                        {
                            return JSONHelper.FromString(false, "预约已取消或已完成拜访");
                        }

                        sql = @"UPDATE Tb_Visit_VisitingCustomersDetail SET 
                                AppointmentUserCode=@AppointmentUserCode,AppointmentObject=@AppointmentObject,AppointmentTime=@AppointmentTime,
                                AppointmentVisitWayID=@VisitWay,ContactTelephone=@Mobile,UpdateUserId=@UserCode,UpdateTime=getdate()
                                WHERE ID=@IID;";
                    }
                    else
                    {
                        // 查询是否已登记
                        sql = @"SELECT ID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK) 
                                WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0";
                        var id = conn.Query<string>(sql, new { PlanID = planId, RoomID = roomId }, trans).FirstOrDefault();
                        if (id != null)
                        {
                            iid = id;
                            sql = @"UPDATE Tb_Visit_VisitingCustomersDetail SET 
                                    AppointmentUserCode=@AppointmentUserCode,AppointmentObject=@AppointmentObject,AppointmentTime=@AppointmentTime,
                                    AppointmentVisitWayID=@VisitWay,ContactTelephone=@Mobile,UpdateUserId=@UserCode,UpdateTime=getdate()
                                    WHERE ID=@IID;";
                        }
                        else
                        {
                            iid = Guid.NewGuid().ToString();
                            sql = @"DECLARE @CommID int,@RegionSNum int,@RegionName nvarchar(50),@BuildSNum int,@BuildName nvarchar(50),@RoomSign nvarchar(50),
                                        @RoomName nvarchar(50),@RoomSNum nvarchar(50),@CustID bigint,@CustName nvarchar(50);

                                    SELECT @CommID=a.CommID,@RegionSNum=b.RegionSNum,@RegionName=c.RegionName,@BuildSNum=b.BuildSNum,@BuildName=b.BuildName,
                                        @RoomSign=a.RoomSign,@RoomName=a.RoomName,@RoomSNum=a.RoomSNum 
                                    FROM Tb_HSPR_Room a WITH(NOLOCK)
                                    LEFT JOIN Tb_HSPR_Building b WITH(NOLOCK) ON a.CommID=b.CommID AND a.BuildSNum=b.BuildSNum AND isnull(b.IsDelete,0)=0
                                    LEFT JOIN Tb_HSPR_Region c WITH(NOLOCK) ON b.CommID=c.CommID AND b.RegionSNum=c.RegionSNum AND isnull(c.IsDelete,0)=0 
                                    WHERE a.RoomID=@RoomID;

                                    SELECT TOP 1 @CustID=a.CustID,@CustName=b.CustName 
                                    FROM Tb_HSPR_CustomerLive a WITH(NOLOCK) 
                                        LEFT JOIN Tb_HSPR_Customer b WITH(NOLOCK) ON a.CustID=b.CustID
                                    WHERE a.RoomID=@RoomID AND a.IsDelLive=0
                                    ORDER BY a.IsActive DESC;

                                    INSERT INTO Tb_Visit_VisitingCustomersDetail(ID,CommID,PlanID,RegionSNum,RegionName,BuildSNum,BuildName,
                                        RoomID,RoomSign,RoomName,RoomSNum,CustID,CustName,AppointmentUserCode,AppointmentObject,AppointmentTime,
                                        AppointmentVisitWayID,ContactTelephone,AddUserId, AddTime,IsDelete)
                                    VALUES(@IID,@CommID,@PlanID,@RegionSNum,@RegionName,@BuildSNum,@BuildName,
                                        @RoomID,@RoomSign,@RoomName,@RoomSNum,@CustID,@CustName,@AppointmentUserCode,@AppointmentObject,@AppointmentTime,
                                        @VisitWay,@Mobile,@UserCode,getdate(),0);";
                        }
                    }

                    conn.Execute(sql, new
                    {
                        IID = iid,
                        PlanID = planId,
                        RoomID = roomId,
                        AppointmentUserCode = appointmentUser,
                        AppointmentObject = appointmentObject,
                        AppointmentTime = appointmentTime,
                        VisitWay = visitWay,
                        Mobile = mobile,
                        UserCode = Global_Var.LoginUserCode
                    }, trans);

                    // 导入问卷问题
                    ImportQuestion_v2(planId, iid, conn, trans);

                    // 更新计划状态
                    UpdatePlanState(planId, conn, trans);

                    trans.Commit();
                    return JSONHelper.FromString(true, string.IsNullOrEmpty(iid) ? "预约成功" : "修改预约信息成功");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message);
                }
            }
        }

        /// <summary>
        /// 取消预约
        /// </summary>
        private string CancelAppointment(DataRow row)
        {
            if (!row.Table.Columns.Contains("IID") || string.IsNullOrEmpty(row["IID"].ToString()))
            {
                return JSONHelper.FromString(false, "预约id不能为空");
            }
            var iid = row["IID"].ToString();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"UPDATE Tb_Visit_VisitingCustomersDetail SET IsDelete=1,DelUserId=@UserCode,DelTime=getdate()
                            WHERE ID=@IID";
                var i = conn.Execute(sql, new { IID = iid, UserCode = Global_Var.LoginUserCode });

                return JSONHelper.FromString(true, "取消成功");
            }
        }

        #endregion

        /// <summary>
        /// 更新计划状态
        /// </summary>
        public void UpdatePlanState(string planId, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var sql = @"UPDATE Tb_Visit_Plan SET PlanState=1 WHERE ID=@PlanId AND PlanState<>2";

            conn.Execute(sql, new { PlanId = planId }, trans);
        }

        #region 拜访登记
        /// <summary>
        /// 获取与业主关系
        /// </summary>
        private string GetRelation()
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT DictionaryName FROM Tb_Dictionary_Relation ORDER BY DictionaryOrderId;";

                return new ApiResult(true, conn.Query(sql)).toJson();
            }
        }

        /// <summary>
        /// 获取问卷问题列表
        /// </summary>
        private string GetQuestions(DataRow row)
        {
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋id不能为空");
            }

            var planId = row["TaskId"].ToString();
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var trans = conn.BeginTransaction();

                try
                {
                    var sql = @"SELECT ID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK)
                                WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0";
                    var detailId = conn.Query<string>(sql, new { PlanID = planId, RoomID = roomId }, trans).FirstOrDefault();
                    if (detailId == null)
                    {
                        detailId = AddVisitRecord(planId, roomId, conn, trans);
                    }

                    sql = @"SELECT count(1) FROM Tb_Visit_VisitingCustomersQuestionnaire WITH(NOLOCK)
                            WHERE VisitingCustomersDetailID=@DetailId AND isnull(IsDelete,0)=0";
                    if (conn.Query<int>(sql, new { PlanID = planId, DetailId = detailId }, trans).FirstOrDefault() == 0)
                    {
                        ImportQuestion(planId, detailId, conn, trans);
                    }

                    trans.Commit();

                    sql = @"SELECT PlanObjective FROM Tb_Visit_Plan WITH(NOLOCK) WHERE ID=@PlanID;

                            SELECT isnull(a.VisitWayID,a.AppointmentVisitWayID) AS VisitWayID,b.VisitWay,
                                IsUploadEnclosure,a.InterviewedObject,a.ContactTelephone,
                                a.RelationsWithOwners AS Relation 
                            FROM Tb_Visit_VisitingCustomersDetail a WITH(NOLOCK)
                            LEFT JOIN Tb_Visit_VisitWay b ON isnull(a.VisitWayID,a.AppointmentVisitWayID)=b.ID
                            WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(a.IsDelete,0)=0;

                            SELECT a.ID AS IID,d.VisitCategory,e.QuestionnaireCategory,a.IsScore,f.Name AS IssueType,a.IssueName,a.IssueResult,
                                    isnull(a.IssueStandardScore,0) AS IssueStandardScore,a.IssueProperty,a.Remark,a.Sort,
                                    ltrim(rtrim(a.Option_1)) AS Option_1,isnull(a.OptionStandardScore_1,0) AS OptionStandardScore_1,
                                    ltrim(rtrim(a.Option_2)) AS Option_2,isnull(a.OptionStandardScore_2,0) AS OptionStandardScore_2,
                                    ltrim(rtrim(a.Option_3)) AS Option_3,isnull(a.OptionStandardScore_3,0) AS OptionStandardScore_3,
                                    ltrim(rtrim(a.Option_4)) AS Option_4,isnull(a.OptionStandardScore_4,0) AS OptionStandardScore_4,
                                    ltrim(rtrim(a.Option_5)) AS Option_5,isnull(a.OptionStandardScore_5,0) AS OptionStandardScore_5,
                                    isnull(a.RealScore,0) AS RealScore,isnull(a.ActualOption,-1) AS ActualOption 
                            FROM Tb_Visit_VisitingCustomersQuestionnaire a WITH(NOLOCK)
                            LEFT JOIN Tb_Visit_VisitingCustomersDetail b WITH(NOLOCK) ON a.VisitingCustomersDetailID=b.ID
                            LEFT JOIN Tb_Visit_Plan c WITH(NOLOCK) ON b.PlanID=c.ID
                            LEFT JOIN Tb_Visit_VisitCategory d ON a.VisitCategoryID=d.ID
                            LEFT JOIN Tb_Visit_QuestionnaireCategory e ON a.QuestionnaireCategoryID=e.ID
                            LEFT JOIN Tb_Visit_Dictionary f ON a.IssueType=f.ID
                            WHERE a.ID IN
                            (
                                SELECT ID FROM Tb_Visit_VisitingCustomersQuestionnaire WITH(NOLOCK)
                                WHERE VisitingCustomersDetailID IN
                                (
                                    SELECT ID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK) 
                                    WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0
                                ) AND isnull(IsDelete,0)=0
                            )";

                    var reader = conn.QueryMultiple(sql, new { PlanID = planId, RoomID = roomId });
                    var planObjective = reader.Read<string>().FirstOrDefault();
                    var visitInfo = reader.Read().FirstOrDefault();
                    var questions = reader.Read();

                    var visitWayId = visitInfo?.VisitWayID?.ToString();
                    var visitWay = visitInfo?.VisitWay?.ToString();
                    var isUploadEnclosure = visitInfo?.IsUploadEnclosure?.ToString();
                    var interviewedObject = visitInfo?.InterviewedObject?.ToString();
                    var interviewedMobile = visitInfo?.ContactTelephone?.ToString();
                    var relationsWithOwnersisit = visitInfo?.Relation?.ToString();

                    return new ApiResult(true, new
                    {
                        DetailId = detailId,
                        PlanObjective = planObjective,
                        VisitWayID = visitWayId,
                        VisitWay = visitWay,
                        IsUploadEnclosure = isUploadEnclosure,
                        InterviewedObject = interviewedObject,
                        InterviewedMobile = interviewedMobile,
                        RelationsWithOwners = relationsWithOwnersisit,
                        Questions = questions
                    }).toJson();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message + ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 获取问卷问题列表第二版，包含多选题
        /// </summary>
        private string GetQuestions_v2(DataRow row)
        {
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋id不能为空");
            }

            var planId = row["TaskId"].ToString();
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();

                try
                {
                    var sql = @"SELECT ID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK)
                                WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0";
                    var detailId = conn.Query<string>(sql, new { PlanID = planId, RoomID = roomId }, trans).FirstOrDefault();
                    if (detailId == null)
                    {
                        detailId = AddVisitRecord_v2(planId, roomId, conn, trans);
                    }

                    sql = @"SELECT count(1) FROM Tb_Visit_VisitingCustomersQuestionnaire WITH(NOLOCK)
                            WHERE VisitingCustomersDetailID=@DetailId AND isnull(IsDelete,0)=0";
                    if (conn.Query<int>(sql, new { PlanID = planId, DetailId = detailId }, trans).FirstOrDefault() == 0)
                    {
                        ImportQuestion_v2(planId, detailId, conn, trans);
                    }
                    //trans.Commit();

                    sql = @"SELECT PlanObjective FROM Tb_Visit_Plan WITH(NOLOCK) WHERE ID=@PlanID;

                            SELECT isnull(a.VisitWayID,a.AppointmentVisitWayID) AS VisitWayID,b.VisitWay,
                                isnull(b.IsUploadEnclosure,0) AS IsUploadEnclosure,
                                a.InterviewedObject,a.ContactTelephone,a.RelationsWithOwners AS Relation 
                            FROM Tb_Visit_VisitingCustomersDetail a WITH(NOLOCK)
                                LEFT JOIN Tb_Visit_VisitWay b WITH(NOLOCK) ON isnull(a.VisitWayID,a.AppointmentVisitWayID)=b.ID
                            WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(a.IsDelete,0)=0;

                            SELECT a.ID AS IID,d.VisitCategory,e.QuestionnaireCategory,a.IsScore,f.Name AS IssueType,a.IssueName,a.IssueResult,
                                isnull(a.IssueStandardScore,0) AS IssueStandardScore,a.IssueProperty,a.Remark,a.Sort,
                                isnull(a.RealScore,0) AS RealScore
                            FROM Tb_Visit_VisitingCustomersQuestionnaire a WITH(NOLOCK) 
                                LEFT JOIN Tb_Visit_VisitingCustomersDetail b WITH(NOLOCK) ON a.VisitingCustomersDetailID=b.ID
                                LEFT JOIN Tb_Visit_Plan c WITH(NOLOCK) ON b.PlanID=c.ID
                                LEFT JOIN Tb_Visit_VisitCategory d ON a.VisitCategoryID=d.ID
                                LEFT JOIN Tb_Visit_QuestionnaireCategory e ON a.QuestionnaireCategoryID=e.ID
                                LEFT JOIN Tb_Visit_Dictionary f ON a.IssueType=f.ID
                            WHERE a.ID IN
                            (
                                SELECT ID FROM Tb_Visit_VisitingCustomersQuestionnaire WITH(NOLOCK)
                                WHERE VisitingCustomersDetailID IN
                                (
                                    SELECT ID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK) 
                                    WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0
                                ) AND isnull(IsDelete,0)=0
                            );";

                    var reader = conn.QueryMultiple(sql, new { PlanID = planId, RoomID = roomId }, trans);
                    var planObjective = reader.Read<string>().FirstOrDefault();
                    var visitInfo = reader.Read().FirstOrDefault();
                    var questions = reader.Read();

                    var tmp = new List<string>();

                    foreach (var question in questions)
                    {
                        if (question.IssueProperty == "单选题" || question.IssueProperty == "多选题" || question.IssueProperty == "选择题")
                        {
                            tmp.Add($@"SELECT a.ID,a.QuestionID AS CustomerQuestionID,a.[Option] AS Content,a.OptionStandardScore AS Score,a.IsChoose,a.Sort
                                        FROM Tb_Visit_VisitingCustomersQuestionnaireOption a WITH(NOLOCK)
                                            INNER JOIN Tb_Visit_VisitingCustomersQuestionnaire b WITH(NOLOCK) ON a.QuestionID=b.ID 
                                       WHERE a.QuestionID='{question.IID}'");
                            //获取实际分数
                            string issueResult = question.IssueResult;
                            if (!string.IsNullOrEmpty(issueResult))
                            {
                                var options = issueResult.Contains(',')
                                    ? string.Join(",", ((String)issueResult).Split(',').Select(A => $"'{A}'"))
                                    : ($"'{issueResult}'");
                                var scoreSql = $@"SELECT TOP 1 OptionStandardScore FROM dbo.Tb_Visit_VisitingCustomersQuestionnaireOption WITH(NOLOCK)
                                                  WHERE ID IN ({options}) ORDER BY  OptionStandardScore DESC ;";
                                var score = conn.Query<double>(scoreSql, new { }, trans).FirstOrDefault();
                                question.RealScore = (object)score;
                            }
                        }
                    }
                    trans.Commit();
                    sql = string.Join(" UNION ALL ", tmp);
                    sql = $"SELECT * FROM ({sql}) AS t ORDER BY Sort";

                    var questionOptions = conn.Query(sql).ToList();

                    var group = new Dictionary<string, List<dynamic>>();
                    foreach (var questionInfo in questions)
                    {
                        var options = questionOptions.FindAll(obj => obj.CustomerQuestionID == questionInfo.IID);

                        questionInfo.Options = options.Select(obj => new { OptionID = obj.ID, Content = obj.Content, Score = obj.Score, IsChoose = obj.IsChoose });

                        foreach (var opt in options)
                        {
                            questionOptions.Remove(opt);
                        }
                    }

                    var visitWayId = visitInfo?.VisitWayID?.ToString();
                    var visitWay = visitInfo?.VisitWay?.ToString();
                    var isUploadEnclosure = visitInfo?.IsUploadEnclosure?.ToString();
                    var interviewedObject = visitInfo?.InterviewedObject?.ToString();
                    var interviewedMobile = visitInfo?.ContactTelephone?.ToString();
                    var relationsWithOwnersisit = visitInfo?.Relation?.ToString();

                    return new ApiResult(true, new
                    {
                        DetailId = detailId,
                        PlanObjective = planObjective,
                        VisitWayID = visitWayId,
                        VisitWay = visitWay,
                        IsUploadEnclosure = isUploadEnclosure,
                        InterviewedObject = interviewedObject,
                        InterviewedMobile = interviewedMobile,
                        RelationsWithOwners = relationsWithOwnersisit,
                        Questions = questions
                    }).toJson();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message + ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 保存问题结果
        /// </summary>
        private string SaveQuestion(DataRow row)
        {
            if (!row.Table.Columns.Contains("DetailID") || string.IsNullOrEmpty(row["DetailID"].ToString()))
            {
                return JSONHelper.FromString(false, "问题id不能为空");
            }
            if (!row.Table.Columns.Contains("VisitWay") || string.IsNullOrEmpty(row["VisitWay"].ToString()))
            {
                return JSONHelper.FromString(false, "拜访方式不能为空");
            }
            if (!row.Table.Columns.Contains("InterviewedObject") || string.IsNullOrEmpty(row["InterviewedObject"].ToString()))
            {
                return JSONHelper.FromString(false, "受访对象不能为空");
            }
            if (!row.Table.Columns.Contains("Relation") || string.IsNullOrEmpty(row["Relation"].ToString()))
            {
                return JSONHelper.FromString(false, "与业主关系不能为空");
            }
            if (!row.Table.Columns.Contains("Data") || string.IsNullOrEmpty(row["Data"].ToString()))
            {
                return JSONHelper.FromString(false, "问题结果不能为空");
            }

            var detailId = row["DetailID"].ToString();
            var signatureImg = default(string);
            var interviewedObject = row["InterviewedObject"].ToString();
            var interviewedMobile = row["InterviewedMobile"].ToString();
            var visitWayId = row["VisitWay"].ToString();
            var relation = row["Relation"].ToString();

            if (row.Table.Columns.Contains("SignatureImg") && !string.IsNullOrEmpty(row["SignatureImg"].ToString()))
            {
                signatureImg = row["SignatureImg"].ToString();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();

                try
                {
                    var sql = @"UPDATE Tb_Visit_VisitingCustomersDetail 
                                SET VisitUserCode=@VisitUserCode,InterviewedObject=@InterviewedObject,ContactTelephone=@InterviewedMobile,
                                VisitWayID=@VisitWayID,RelationsWithOwners=@Relation,SignatureImg=@SignatureImg  
                                WHERE ID=@DetailID";

                    conn.Execute(sql, new
                    {
                        VisitUserCode = Global_Var.LoginUserCode,
                        SignatureImg = signatureImg,
                        InterviewedObject = interviewedObject,
                        InterviewedMobile = interviewedMobile,
                        VisitWayID = visitWayId,
                        Relation = relation,
                        DetailID = detailId
                    }, trans);

                    sql = @"SELECT PlanID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK) WHERE ID=@DetailID";
                    var planId = conn.Query<string>(sql, new { DetailID = detailId }, trans).FirstOrDefault();

                    // 问卷问题表
                    var questionArray = (JArray)JsonConvert.DeserializeObject(row["Data"].ToString());

                    foreach (var question in questionArray)
                    {
                        var iid = question["IID"].ToString();
                        var result = question["Result"].ToString();


                        sql = @"SELECT count(1) FROM Tb_Visit_VisitingCustomersQuestionnaire WITH(NOLOCK) WHERE ID=@IID AND IssueProperty='选择题'";
                        if (conn.Query<int>(sql, new { IID = iid }, trans).FirstOrDefault() > 0)
                        {
                            if (result == "-1")
                            {
                                continue;
                            }

                            conn.Execute($@"UPDATE Tb_Visit_VisitingCustomersQuestionnaire 
                                            SET ActualOption=@Result,RealScore=OptionStandardScore_{result} 
                                            WHERE ID=@IID",
                                            new { Result = AppGlobal.StrToInt(result), IID = iid }, trans);
                        }
                        else
                        {
                            conn.Execute(@"UPDATE Tb_Visit_VisitingCustomersQuestionnaire SET IssueResult=@Result WHERE ID=@IID;",
                                            new { Result = result, IID = iid }, trans);
                        }
                    }

                    // 更新计划状态
                    UpdatePlanState(planId, conn, trans);

                    trans.Commit();
                    return JSONHelper.FromString(true, "保存成功");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message + ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 保存问题结果
        /// </summary>
        private string SaveQuestion_v2(DataRow row)
        {
            if (!row.Table.Columns.Contains("DetailID") || string.IsNullOrEmpty(row["DetailID"].ToString()))
            {
                return JSONHelper.FromString(false, "问题id不能为空");
            }
            if (!row.Table.Columns.Contains("VisitWay") || string.IsNullOrEmpty(row["VisitWay"].ToString()))
            {
                return JSONHelper.FromString(false, "拜访方式不能为空");
            }
            if (!row.Table.Columns.Contains("InterviewedObject") || string.IsNullOrEmpty(row["InterviewedObject"].ToString()))
            {
                return JSONHelper.FromString(false, "受访对象不能为空");
            }
            if (!row.Table.Columns.Contains("Relation") || string.IsNullOrEmpty(row["Relation"].ToString()))
            {
                return JSONHelper.FromString(false, "与业主关系不能为空");
            }
            if (!row.Table.Columns.Contains("Data") || string.IsNullOrEmpty(row["Data"].ToString()))
            {
                return JSONHelper.FromString(false, "问题结果不能为空");
            }

            var detailId = row["DetailID"].ToString();
            var signatureImg = default(string);
            var interviewedObject = row["InterviewedObject"].ToString();
            var interviewedMobile = row["InterviewedMobile"].ToString();
            var visitWayId = row["VisitWay"].ToString();
            var relation = row["Relation"].ToString();

            if (row.Table.Columns.Contains("SignatureImg") && !string.IsNullOrEmpty(row["SignatureImg"].ToString()))
            {
                signatureImg = row["SignatureImg"].ToString();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();

                try
                {
                    var sql = @"UPDATE Tb_Visit_VisitingCustomersDetail 
                                SET VisitUserCode=@VisitUserCode,InterviewedObject=@InterviewedObject,ContactTelephone=@InterviewedMobile,
                                VisitWayID=@VisitWayID,RelationsWithOwners=@Relation,SignatureImg=@SignatureImg  
                                WHERE ID=@DetailID";

                    conn.Execute(sql, new
                    {
                        VisitUserCode = Global_Var.LoginUserCode,
                        SignatureImg = signatureImg,
                        InterviewedObject = interviewedObject,
                        InterviewedMobile = interviewedMobile,
                        VisitWayID = visitWayId,
                        Relation = relation,
                        DetailID = detailId
                    }, trans);

                    sql = @"SELECT PlanID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK) WHERE ID=@DetailID";
                    var planId = conn.Query<string>(sql, new { DetailID = detailId }, trans).FirstOrDefault();

                    var questionArray = (JArray)JsonConvert.DeserializeObject(row["Data"].ToString());

                    sql = @"DECLARE @RealScore decimal(10,2);
                            DECLARE @OptionIDs varchar(max);";

                    for (int i = 0; i < questionArray.Count; i++)
                    {
                        var questionInfo = questionArray[i];

                        var iid = questionInfo["IID"].ToString();
                        var result = questionInfo["Result"].ToString();
                        var options = result.Split(',').Select(obj => $"'{obj}'");
                        //根据标准传入的Result 拿到实际的分数并刷新，之前只刷新了IssueResult 没有刷新 RealScore
                        //var scoreSql = $@"select  OptionStandardScore from dbo.Tb_Visit_VisitingCustomersQuestionnaireOption where ID='{result}';";
                        //var score = conn.Query<double>(scoreSql, new { DetailID = detailId }, trans).FirstOrDefault();

                        sql += $@"IF (SELECT count(1) FROM Tb_Visit_VisitingCustomersQuestionnaire WITH(NOLOCK)
                                        WHERE ID='{iid}' AND IssueProperty IN('单选题','多选题','选择题'))>0
                                    BEGIN
                                        UPDATE Tb_Visit_VisitingCustomersQuestionnaireOption SET IsChoose=0
                                        WHERE ID IN(SELECT ID FROM Tb_Visit_VisitingCustomersQuestionnaireOption WHERE QuestionID='{iid}')

                                        UPDATE Tb_Visit_VisitingCustomersQuestionnaireOption SET IsChoose=1 WHERE ID IN({string.Join(",", options)});

                                        SELECT @RealScore=max(OptionStandardScore) FROM Tb_Visit_VisitingCustomersQuestionnaireOption WITH(NOLOCK) WHERE QuestionID='{iid}';
                                        UPDATE Tb_Visit_VisitingCustomersQuestionnaire SET RealScore=@RealScore WHERE ID='{iid}';
                                    END
                                  UPDATE Tb_Visit_VisitingCustomersQuestionnaire SET IssueResult='{result}'  WHERE ID='{iid}';";
                    }

                    conn.Execute(sql, null, trans);

                    // 更新计划状态
                    UpdatePlanState(planId, conn, trans);

                    trans.Commit();
                    return JSONHelper.FromString(true, "保存成功");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message + ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 添加文件
        /// </summary>
        private string AddFile(DataRow row)
        {
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋id不能为空");
            }
            if (!row.Table.Columns.Contains("Image") || string.IsNullOrEmpty(row["Image"].ToString()))
            {
                return JSONHelper.FromString(false, "报事id不能为空");
            }

            var planId = row["TaskId"].ToString();
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());
            var image = row["Image"].ToString();
            var detailId = default(string);

            if (row.Table.Columns.Contains("DetailID") && !string.IsNullOrEmpty(row["DetailID"].ToString()))
            {
                detailId = row["DetailID"].ToString();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                if (detailId == null)
                {
                    detailId = AddVisitRecord(planId, roomId, conn);
                }

                var fileName = Path.GetFileName(image);
                var fix = Path.GetExtension(fileName);

                var sql = @"INSERT INTO Tb_Visit_VisitingCustomersDetailFile(ID,CommID,PlanID,VisitingCustomersDetailID,
                                FileName,Fix,FilePath,PhoneName,PhotoTime,PhotoUserCode,IsDelete) 
                            SELECT newid(),CommID,PlanID,ID,@FileName,@Fix,@FilePath,@PhoneName,getdate(),@PhotoUserCode,0
                            FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK) 
                            WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0";

                var i = conn.Execute(sql, new
                {
                    PlanID = planId,
                    RoomID = roomId,
                    FileName = fileName,
                    Fix = fix,
                    FilePath = image,
                    PhoneName = fileName,
                    PhotoUserCode = Global_Var.LoginUserCode
                });

                if (i == 1)
                {
                    // 更新计划状态
                    UpdatePlanState(planId, conn);

                    return JSONHelper.FromString(true, "保存成功");
                }


                return JSONHelper.FromString(false, "保存失败");
            }
        }


        /// <summary>
        /// 添加文件
        /// </summary>
        private string AddFile_v2(DataRow row)
        {
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋id不能为空");
            }
            if (!row.Table.Columns.Contains("Image") || string.IsNullOrEmpty(row["Image"].ToString()))
            {
                return JSONHelper.FromString(false, "报事id不能为空");
            }

            var planId = row["TaskId"].ToString();
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());
            var image = row["Image"].ToString();
            var detailId = default(string);

            if (row.Table.Columns.Contains("DetailID") && !string.IsNullOrEmpty(row["DetailID"].ToString()))
            {
                detailId = row["DetailID"].ToString();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                if (detailId == null)
                {
                    detailId = AddVisitRecord_v2(planId, roomId, conn);
                }

                var fileName = Path.GetFileName(image);
                var fix = Path.GetExtension(fileName);

                var sql = @"INSERT INTO Tb_Visit_VisitingCustomersDetailFile(ID,CommID,PlanID,VisitingCustomersDetailID,
                                FileName,Fix,FilePath,PhoneName,PhotoTime,PhotoUserCode,IsDelete) 
                            SELECT newid(),CommID,PlanID,ID,@FileName,@Fix,@FilePath,@PhoneName,getdate(),@PhotoUserCode,0
                            FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK) 
                            WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0";

                var i = conn.Execute(sql, new
                {
                    PlanID = planId,
                    RoomID = roomId,
                    FileName = fileName,
                    Fix = fix,
                    FilePath = image,
                    PhoneName = fileName,
                    PhotoUserCode = Global_Var.LoginUserCode
                });

                if (i == 1)
                {
                    // 更新计划状态
                    UpdatePlanState(planId, conn);

                    return JSONHelper.FromString(true, "保存成功");
                }


                return JSONHelper.FromString(false, "保存失败");
            }
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        private string GetFileList(DataRow row)
        {
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }
            var planId = row["TaskId"].ToString();
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT a.ID AS FileId,@PlanID AS TaskId,b.RoomID AS PointId,FileName,Fix,FilePath,PhoneName,
                                   PhotoTime,PhotoUserCode,c.UserName AS PhotoUserName,c.MobileTel AS PhotoUserMobileTel 
                            FROM Tb_Visit_VisitingCustomersDetailFile a WITH(NOLOCK)
                            LEFT JOIN Tb_Visit_VisitingCustomersDetail b WITH(NOLOCK) ON a.VisitingCustomersDetailID=b.ID
                            LEFT JOIN Tb_Sys_User c WITH(NOLOCK) ON a.PhotoUserCode=c.UserCode 
                            WHERE a.PlanID=@PlanID AND VisitingCustomersDetailID IN
                            (
                                SELECT ID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK)
                                WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0
                            ) AND isnull(a.IsDelete,0)=0";

                var result = conn.Query(sql, new { PlanID = planId, RoomID = roomId });

                return new ApiResult(true, result).toJson();
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        private string DelFile(DataRow row)
        {
            if (!row.Table.Columns.Contains("FileID") || string.IsNullOrEmpty(row["FileID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            var fileId = row["FileID"].ToString();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"UPDATE Tb_Visit_VisitingCustomersDetailFile SET IsDelete=1 WHERE ID=@FileID";

                var result = conn.Query(sql, new { FileID = fileId });

                return JSONHelper.FromString(true, "删除成功");
            }
        }

        /// <summary>
        /// 添加报事
        /// </summary>
        private string AddIncident(DataRow row)
        {
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋id不能为空");
            }
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事id不能为空");
            }

            var planId = row["TaskId"].ToString();
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());
            var incidentId = AppGlobal.StrToLong(row["IncidentID"].ToString());
            var detailId = default(string);

            if (row.Table.Columns.Contains("DetailID") && !string.IsNullOrEmpty(row["DetailID"].ToString()))
            {
                detailId = row["DetailID"].ToString();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                if (detailId == null)
                {
                    detailId = AddVisitRecord(planId, roomId, conn);
                }

                var sql = @"INSERT INTO Tb_Visit_VisitingCustomersDetailIncident(ID,CommID,PlanID,VisitingCustomersDetailID,IncidentId) 
                            SELECT newid(),CommID,@PlanID,@DetailID,IncidentID 
                            FROM Tb_HSPR_IncidentAccept WITH(NOLOCK) WHERE IncidentID=@IncidentID";

                var i = conn.Execute(sql, new { PlanID = planId, DetailID = detailId, IncidentID = incidentId });
                if (i == 1)
                {
                    // 更新计划状态
                    UpdatePlanState(planId, conn);
                    return JSONHelper.FromString(true, "保存成功");
                }

                return JSONHelper.FromString(false, "保存失败");
            }
        }

        /// <summary>
        /// 添加报事
        /// </summary>
        private string AddIncident_v2(DataRow row)
        {
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋id不能为空");
            }
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事id不能为空");
            }

            var planId = row["TaskId"].ToString();
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());
            var incidentId = AppGlobal.StrToLong(row["IncidentID"].ToString());
            var detailId = default(string);

            if (row.Table.Columns.Contains("DetailID") && !string.IsNullOrEmpty(row["DetailID"].ToString()))
            {
                detailId = row["DetailID"].ToString();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                if (detailId == null)
                {
                    detailId = AddVisitRecord_v2(planId, roomId, conn);
                }

                var sql = @"INSERT INTO Tb_Visit_VisitingCustomersDetailIncident(ID,CommID,PlanID,VisitingCustomersDetailID,IncidentId) 
                            SELECT newid(),CommID,@PlanID,@DetailID,IncidentID 
                            FROM Tb_HSPR_IncidentAccept WITH(NOLOCK) WHERE IncidentID=@IncidentID";

                var i = conn.Execute(sql, new { PlanID = planId, DetailID = detailId, IncidentID = incidentId });
                if (i == 1)
                {
                    // 更新计划状态
                    UpdatePlanState(planId, conn);
                    return JSONHelper.FromString(true, "保存成功");
                }

                return JSONHelper.FromString(false, "保存失败");
            }
        }

        /// <summary>
        /// 获取报事列表
        /// </summary>
        private string GetIncidentList(DataRow row)
        {
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }
            var planId = row["TaskId"].ToString();
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT a.ID AS IID,a.IncidentID,b.IncidentNum,b.IncidentContent,b.EmergencyDegree,
                                   c.TypeName AS BigCorpTypeName,d.TypeName AS SmallCorpTypeName 
                            FROM Tb_Visit_VisitingCustomersDetailIncident a WITH(NOLOCK)
                            LEFT JOIN Tb_HSPR_IncidentAccept b WITH(NOLOCK) ON a.IncidentId=b.IncidentID
                            LEFT JOIN Tb_HSPR_CorpIncidentType c WITH(NOLOCK) ON b.BigCorpTypeID=c.CorpTypeID
                            LEFT JOIN Tb_HSPR_CorpIncidentType d WITH(NOLOCK) ON b.FineCorpTypeID=d.CorpTypeID
                            WHERE a.PlanID=@PlanID AND b.RoomID=@RoomID";

                var result = conn.Query(sql, new { PlanID = planId, RoomID = roomId });

                return new ApiResult(true, result).toJson();
            }
        }

        /// <summary>
        /// 获取总结
        /// </summary>
        private string GetVisitSummary(DataRow row)
        {
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }
            var planId = row["TaskId"].ToString();
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT VisitSummary FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK) 
                            WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0";

                var summary = conn.Query<string>(sql, new { PlanID = planId, RoomID = roomId }).FirstOrDefault();

                return JSONHelper.FromString(true, summary);
            }
        }

        /// <summary>
        /// 添加或修改总结
        /// </summary>
        private string MakeVisitSummary(DataRow row)
        {
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            var planId = row["TaskId"].ToString();
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());
            var content = default(string);

            if (row.Table.Columns.Contains("Content"))
            {
                content = row["Content"].ToString();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"UPDATE Tb_Visit_VisitingCustomersDetail SET VisitSummary=@VisitSummary 
                            WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0";

                var i = conn.Execute(sql, new { PlanID = planId, RoomID = roomId, VisitSummary = content });
                if (i == 1)
                {
                    // 更新计划状态
                    UpdatePlanState(planId, conn);

                    return JSONHelper.FromString(true, "保存成功");
                }

                return JSONHelper.FromString(false, "保存失败");
            }
        }

        /// <summary>
        /// 完成拜访
        /// </summary>
        private string CompleteVisit(DataRow row)
        {
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }
            var planId = row["TaskId"].ToString();
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var trans = conn.BeginTransaction();

                // 判断是否已完成所有问题
                var sql = @"SELECT * FROM Tb_Visit_VisitingCustomersQuestionnaire WITH(NOLOCK)
                            WHERE VisitingCustomersDetailID IN
                            (
                                SELECT ID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK) 
                                WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0
                            ) AND IssueProperty='问答题' AND IssueResult IS NULL

                            UNION ALL

                            SELECT * FROM Tb_Visit_VisitingCustomersQuestionnaire a WITH(NOLOCK)
                            WHERE VisitingCustomersDetailID IN
                            (
                                SELECT ID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK)
                                WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0
                            ) 
                            AND IssueProperty IN ('单选题','选择题')
                            AND (SELECT count(1) FROM Tb_Visit_VisitingCustomersQuestionnaireOption b WITH(NOLOCK) WHERE a.ID=b.QuestionID AND IsChoose=1)=0";

                var count = conn.Query(sql, new { PlanID = planId, RoomID = roomId }, trans).Count();
                if (count > 0)
                {
                    return JSONHelper.FromString(false, "请先完成所有问卷调查");
                }

                // 判断是否需要附件
                sql = @"SELECT IsUploadEnclosure FROM Tb_Visit_VisitWay 
                        WHERE ID IN
                        (
                            SELECT VisitWayID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK)
                            WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0
                        )";


                var isUploadEnclosure = conn.Query<int>(sql, new { PlanID = planId, RoomID = roomId }, trans).FirstOrDefault();
                if (isUploadEnclosure == 1)
                {
                    sql = @"SELECT count(1) FROM Tb_Visit_VisitingCustomersDetailFile WITH(NOLOCK)
                            WHERE PlanID=@PlanID AND VisitingCustomersDetailID IN
                            (
                                SELECT ID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK)
                                WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0
                            )";
                    if (conn.Query<int>(sql, new { PlanID = planId, RoomID = roomId }, trans).FirstOrDefault() == 0)
                    {
                        return JSONHelper.FromString(false, "该拜访要求上传附件，请先上传");
                    }
                }

                // 判断是否已填写总结
                sql = @"SELECT VisitSummary FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK)
                        WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0";
                var visitSummary = conn.Query<string>(sql, new { PlanID = planId, RoomID = roomId }, trans).FirstOrDefault();
                if (string.IsNullOrEmpty(visitSummary))
                {
                    return JSONHelper.FromString(false, "请先填写拜访总结");
                }

                // 记录执行信息
                sql = $"SELECT CommID FROM Tb_Visit_Plan WITH(NOLOCK) WHERE Id='{ planId }'";
                var commId = conn.Query<int>(sql, null, trans).FirstOrDefault();
                var isHousekeeper = IsHousekeeper(commId, conn, trans);
                var planRooms = GetManageRooms(commId, planId, null, conn, trans);

                sql = @"UPDATE Tb_Visit_VisitingCustomersDetail 
                            SET CompleteTime=getdate(),VisitUserCode=@UserCode
                        WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0;

                        UPDATE Tb_Visit_Plan SET VisitHouseholds=isnull(VisitHouseholds,0)+1 WHERE ID=@PlanID;

                        /* 记录自己的执行信息 */
                        DECLARE @VisitedRoomCount int,
                                @PlanCoverageRate decimal(18,2),
                                @ActualCoverageRate decimal(18,2),
                                @TmpActualCoverageRate decimal(18,4);

                        SELECT @VisitedRoomCount=count(1) FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK)
                        WHERE PlanID=@PlanID AND VisitUserCode=@UserCode AND CompleteTime IS NOT NULL AND isnull(IsDelete,0)=0;

                        SELECT @PlanCoverageRate=PlanCoverageRate FROM Tb_Visit_Plan WITH(NOLOCK) WHERE ID=@PlanID;

                        -- 实际覆盖率
                        SET @TmpActualCoverageRate=convert(decimal(18,4),@VisitedRoomCount)/@PlanRoomCount*100;
                        SELECT @ActualCoverageRate=(left(@TmpActualCoverageRate, charindex('.',@TmpActualCoverageRate)+2));

                        IF exists(SELECT * FROM Tb_Visit_VisitingExecuteUser WITH(NOLOCK) WHERE PlanID=@PlanID AND UserCode=@UserCode)
	                        BEGIN
		                        UPDATE Tb_Visit_VisitingExecuteUser 
		                        SET VisitedRoomCount=@VisitedRoomCount,ActualCoverageRate=@ActualCoverageRate
	                            WHERE PlanID=@PlanID AND UserCode=@UserCode;
	                        END
                        ELSE
	                        BEGIN
		                        INSERT INTO Tb_Visit_VisitingExecuteUser(IID,PlanID,UserCode,UserName,PlanRoomCount,VisitedRoomCount,
                                    ActualCoverageRate,IsHousekeeper,IsComplete,FirstVisitingTime,PlanCoverageRate)
                                VALUES(newid(),@PlanID,@UserCode,@UserName,@PlanRoomCount,@VisitedRoomCount,@ActualCoverageRate,
                                    @IsHousekeeper,0,getdate(),@PlanCoverageRate);
	                        END

                        /* 任务层级拜访信息 */
                        SELECT @VisitedRoomCount=count(1) FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK)
                        WHERE PlanID=@PlanID AND CompleteTime IS NOT NULL AND isnull(IsDelete,0)=0;

                        UPDATE Tb_Visit_Plan SET VisitHouseholds=@VisitedRoomCount WHERE Id=@PlanID;";

                var i = conn.Execute(sql, new
                {
                    PlanID = planId,
                    RoomID = roomId,
                    UserCode = Global_Var.LoginUserCode,
                    UserName = Global_Var.LoginUserName,
                    IsHousekeeper = isHousekeeper,
                    PlanRoomCount = planRooms.Count()
                }, trans);

                trans.Commit();
                return JSONHelper.FromString(true, "操作成功");
            }
        }

        /// <summary>
        /// 完成拜访计划
        /// </summary>
        private string TaskComplete(DataRow row)
        {
            if (!row.Table.Columns.Contains("TaskID") || string.IsNullOrEmpty(row["TaskID"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            var planId = row["TaskId"].ToString();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 判断是否存在已预约未拜访
                var sql = @"SELECT count(1) FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK)
                            WHERE PlanID=@PlanId AND AppointmentUserCode=@UserCode AND isnull(CompleteTime,'')='' AND isnull(IsDelete,0)=0";
                var count = conn.Query<int>(sql, new { PlanId = planId, UserCode = Global_Var.LoginUserCode }).FirstOrDefault();
                if (count > 0)
                {
                    return JSONHelper.FromString(false, "还有已预约未拜访的客户，无法完成");
                }

                sql = @"SELECT IID FROM Tb_Visit_VisitingExecuteUser WITH(NOLOCK)
                        WHERE PlanID=@PlanID AND UserCode=@UserCode AND ActualCoverageRate>=PlanCoverageRate";

                var iid = conn.Query<string>(sql, new { PlanId = planId, UserCode = Global_Var.LoginUserCode }).FirstOrDefault();
                if (iid == null)
                {
                    return JSONHelper.FromString(false, "覆盖率未达标，无法完成");
                }

                sql = @"UPDATE Tb_Visit_VisitingExecuteUser SET IsComplete=1,CompleteTime=getdate() 
                        WHERE PlanID=@PlanID AND UserCode=@UserCode";
                conn.Execute(sql, new { PlanId = planId, UserCode = Global_Var.LoginUserCode });

                // 任务层级
                sql = @"DECLARE @VisitedRoomCount int,
                                @PlanRoomCount decimal(18,2),
                                @PlanCoverageRate decimal(18,2),
                                @ActualCoverageRate decimal(18,2),
                                @TmpActualCoverageRate decimal(18,4);

                        SELECT @VisitedRoomCount=count(1) FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK)
                        WHERE PlanID=@PlanID AND CompleteTime IS NOT NULL AND isnull(IsDelete,0)=0;

                        SELECT @PlanRoomCount=TotalHouseholds,@PlanCoverageRate=PlanCoverageRate
                        FROM Tb_Visit_Plan WITH(NOLOCK) WHERE ID=@PlanID;

                        -- 实际覆盖率
                        SET @TmpActualCoverageRate=convert(decimal(18,4),@VisitedRoomCount)/@PlanRoomCount*100;
                        SELECT @ActualCoverageRate=(left(@TmpActualCoverageRate, charindex('.',@TmpActualCoverageRate)+2));

                        IF @ActualCoverageRate>=@PlanCoverageRate
                            BEGIN
                                UPDATE Tb_Visit_Plan SET PlanState=2,CompleteTime=getdate(),CompleteUserCode=@UserCode
                                WHERE ID=@PlanID;
                            END";

                conn.Execute(sql, new { PlanId = planId, UserCode = Global_Var.LoginUserCode });

                return JSONHelper.FromString(true, "任务完成成功");
            }
        }

        /// <summary>
        /// 获取拜访历史
        /// </summary>
        private string GetVisitHistory(DataRow row)
        {
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "拜访房屋不能为空");
            }
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT c.VisitCategory,d.QuestionnaireCategory,a.CustName,a.RoomSign,a.RoomName,VisitWay,a.CompleteTime,
                                   a.InterviewedObject,f.UserName AS AppointmentUserName,g.UserName AS VisitUserName 
                                   FROM Tb_Visit_VisitingCustomersDetail a WITH(NOLOCK)
                            LEFT JOIN Tb_Visit_Plan b WITH(NOLOCK) ON a.PlanID=b.ID
                            LEFT JOIN Tb_Visit_VisitCategory c ON b.VisitCategoryID=c.ID
                            LEFT JOIN Tb_Visit_QuestionnaireCategory d ON b.QuestionnaireCategoryID=d.ID
                            LEFT JOIN Tb_Visit_VisitWay e ON a.VisitWayID=e.ID
                            LEFT JOIN Tb_Sys_User f WITH(NOLOCK) ON a.AppointmentUserCode=f.UserCode
                            LEFT JOIN Tb_Sys_User g WITH(NOLOCK) ON a.VisitUserCode=g.UserCode
                            WHERE a.RoomID=@RoomID AND a.CompleteTime IS NOT NULL
                            ORDER BY a.CompleteTime DESC ;";
                var resultSet = conn.Query(sql, new { RoomID = roomId });
                return new ApiResult(true, resultSet).toJson();
            }
        }

        #endregion

        #region 辅助方法
        /// <summary>
        /// 判断用户是否是该项目管家
        /// </summary>
        private bool IsHousekeeper(int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            if (_isHousekeeper.HasValue)
            {
                return _isHousekeeper.Value;
            }

            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            /*
             * 取消楼栋管家
             * SELECT UserCode FROM Tb_HSPR_BuildHousekeeper WITH(NOLOCK) WHERE CommID=@CommID AND UserCode=@UserCode 
            */

            var sql = @"SELECT UserCode FROM Tb_HSPR_Room WITH(NOLOCK) WHERE CommID=@CommID AND UserCode=@UserCode  
                        UNION
                        SELECT UserCode FROM Tb_HSPR_RoomHousekeeper WITH(NOLOCK) 
                        WHERE UserCode=@UserCode AND RoomID IN 
                        (
                            SELECT RoomID FROM Tb_HSPR_Room WITH(NOLOCK) WHERE CommID=@CommID AND isnull(IsDelete,0)=0
                        );";

            if (conn.Query(sql, new { UserCode = Global_Var.LoginUserCode, CommID = commId }, trans).Count() > 0)
                _isHousekeeper = true;
            else
                _isHousekeeper = false;

            return _isHousekeeper.Value;
        }

        /// <summary>
        /// 拼接查询有权拜访的房屋的sql
        /// </summary>
        private string GetManageRoomsQuerySql(int commId, string planId, bool? visitState = null, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            // 房屋状态，使用性质
            var sql = @"SELECT RoomState FROM Tb_Visit_Plan WITH(NOLOCK) WHERE ID=@PlanID;
                        SELECT PropertyUses FROM Tb_Visit_Plan WITH(NOLOCK) WHERE ID=@PlanID;";

            var reader = conn.QueryMultiple(sql, new { PlanID = planId }, trans);
            var roomState = reader.Read<string>().FirstOrDefault();
            var propertyUses = reader.Read<string>().FirstOrDefault();

            // 是否是管家
            var isHousekeeper = IsHousekeeper(commId, conn, trans);

            sql = $@"SELECT RoomID FROM Tb_HSPR_Room WITH(NOLOCK) 
                     WHERE CommID={commId} AND PropertyRights<>'虚拟房产' AND IsSplitUnite<>1 AND IsSplitUnite<>3 AND isnull(IsDelete,0)=0";

            if (!string.IsNullOrEmpty(roomState))
            {
                var tmp = roomState.Trim(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (tmp.Length > 0)
                {
                    sql += $" AND RoomState IN({ string.Join(",", tmp)}) ";
                }
            }

            if (!string.IsNullOrEmpty(propertyUses))
            {
                var tmp = propertyUses.Trim(',')
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(obj => $"'{obj}'").ToArray();
                if (tmp.Length > 0)
                {
                    sql += $@" AND PropertyUses IN
                               (
                                    SELECT value FROM SplitString
                                    (
                                        (
                                            SELECT stuff(
                                                (
                                                    SELECT ','+DictionaryName FROM Tb_Dictionary_PropertyUses 
                                                    WHERE DictionaryCode IN({ string.Join(",", tmp) }) FOR XML PATH('')
                                                ),1,1,'')
                                        ),
                                    ',',1)
                                ) ";
                }
            }

            if (isHousekeeper)
            {
                /*
                 * 取消楼栋管家
                 * SELECT RoomID FROM Tb_HSPR_Room WITH(NOLOCK) WHERE CommID={commId} AND isnull(IsDelete,0)=0
                    AND BuildSNum IN(SELECT BuildSNum FROM Tb_HSPR_BuildHousekeeper WITH(NOLOCK) WHERE CommID={commId} AND UserCode='{Global_Var.LoginUserCode}')
                    UNION ALL 
                 */
                sql += $@" AND RoomID IN
                           (
                                SELECT RoomID FROM Tb_HSPR_Room WITH(NOLOCK) 
                                WHERE CommID={commId} AND UserCode='{Global_Var.LoginUserCode}' AND isnull(IsDelete,0)=0
                                UNION ALL 
                                SELECT RoomID FROM Tb_HSPR_RoomHousekeeper WITH(NOLOCK) WHERE UserCode='{Global_Var.LoginUserCode}' 
                           ) ";
            }

            // 拜访状态
            if (visitState != null)
            {
                if (visitState.HasValue && visitState.Value)
                {
                    // 已拜访
                    return $@"SELECT RoomID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK)
                              WHERE PlanID='{ planId }' AND VisitUserCode='{ Global_Var.LoginUserCode }' 
                              AND isnull(CompleteTime,'')<>'' AND isnull(IsDelete,0)=0";
                }
                else
                {
                    // 未拜访
                    sql += $@" AND RoomID NOT IN
                                (
                                    SELECT RoomID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK)
                                    WHERE PlanID='{ planId }' AND VisitUserCode='{ Global_Var.LoginUserCode }' 
                                    AND isnull(CompleteTime,'')<>'' AND isnull(IsDelete,0)=0
                                )";
                }
            }

            return sql;
        }

        /// <summary>
        /// 获取有权拜访的房屋
        /// </summary>
        private IEnumerable<long> GetManageRooms(int commId, string planId, bool? visitState, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var sql = GetManageRoomsQuerySql(commId, planId, visitState, conn, trans);

            return conn.Query<long>(sql, null, trans);
        }

        /// <summary>
        /// 获取计划覆盖率
        /// </summary>
        private decimal GetPlanCoverageRate(string planId, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var rate = conn.Query<decimal>(@"SELECT PlanCoverageRate FROM Tb_Visit_Plan WITH(NOLOCK) WHERE ID=@PlanID",
                new { PlanID = planId }, trans).FirstOrDefault();

            return Math.Round(rate, 2);
        }

        /// <summary>
        /// 计算实际覆盖率
        /// </summary>
        private decimal CalcRealCoverageRate(int commId, string planId, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var manageRooms = GetManageRooms(commId, planId, null, conn, trans);
            if (manageRooms.Count() == 0)
                return 0.0m;

            var tmp = GetManageRoomsQuerySql(commId, planId, true, conn, trans);

            var sql = $@"SELECT RoomID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK) 
                         WHERE PlanID='{ planId }' AND isnull(CompleteTime,'')<>'' AND isnull(IsDelete,0)=0
                         AND RoomID IN({ tmp })";

            var visitedRooms = conn.Query<long>(sql, null, trans);

            return Math.Round(visitedRooms.Count() / (decimal)manageRooms.Count() * 100, 2);
        }

        /// <summary>
        /// 获取角标
        /// </summary>
        private void GetBadge(int commId, string planId, out int unvisitCount, out int appointmentCount, out int visitedCount,
            IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            unvisitCount = GetManageRooms(commId, planId, false, conn).Count();

            var sql = $@"SELECT count(1) FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK) 
                         WHERE PlanID='{ planId }' AND AppointmentUserCode='{Global_Var.LoginUserCode}' 
                         AND isnull(CompleteTime,'')='' AND isnull(IsDelete,0)=0;

                         SELECT count(1) FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK) 
                         WHERE PlanID='{ planId }' AND VisitUserCode='{Global_Var.LoginUserCode}' 
                         AND isnull(CompleteTime,'')<>'' AND isnull(IsDelete,0)=0;";

            var reader = conn.QueryMultiple(sql, null, trans);
            appointmentCount = reader.Read<int>().FirstOrDefault();
            visitedCount = reader.Read<int>().FirstOrDefault();
        }

        /// <summary>
        /// 添加拜访记录
        /// </summary>
        private string AddVisitRecord(string planId, long roomId, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var sql = @"SELECT ID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK) 
                        WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0;";

            var iid = conn.Query<string>(sql, new { PlanID = planId, RoomID = roomId }, trans).FirstOrDefault();
            if (iid == null)
            {
                iid = Guid.NewGuid().ToString();
                sql = @"INSERT INTO Tb_Visit_VisitingCustomersDetail(ID,CommID,PlanID,RegionSNum,RegionName,BuildSNum,BuildName,
                            RoomID,RoomSign,RoomName,RoomSNum,CustID,CustName,AddUserId,AddTime,IsDelete)
                        SELECT @IID,a.CommID,@PlanID,b.RegionSNum,c.RegionName,b.BuildSNum,b.BuildName,a.RoomID,a.RoomSign,
                            a.RoomName,a.RoomSNum,d.CustID,e.CustName,@UserCode,getdate(),0
                        FROM Tb_HSPR_Room a WITH(NOLOCK)
                        LEFT JOIN Tb_HSPR_Building b WITH(NOLOCK) ON a.CommID=b.CommID AND a.BuildSNum=b.BuildSNum AND isnull(b.IsDelete,0)=0
                        LEFT JOIN Tb_HSPR_Region c WITH(NOLOCK) ON b.CommID=c.CommID AND b.RegionSNum=c.RegionSNum AND isnull(c.IsDelete,0)=0 
                        LEFT JOIN Tb_HSPR_CustomerLive d WITH(NOLOCK) ON a.RoomID=d.RoomID AND d.IsDelLive=0 AND d.IsActive=1
                        LEFT JOIN Tb_HSPR_Customer e WITH(NOLOCK) ON d.CustID=e.CustID
                        WHERE a.RoomID=@RoomID;";

                int i = conn.Execute(sql, new { IID = iid, PlanID = planId, RoomID = roomId, UserCode = Global_Var.LoginUserCode }, trans);
                if (i == 1)
                {
                    ImportQuestion(planId, iid, conn, trans);
                    return iid;
                }
                return null;
            }
            return iid;
        }

        public string AddVisitRecord_v2(string planId, long roomId, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var sql = @"SELECT ID FROM Tb_Visit_VisitingCustomersDetail WITH(NOLOCK) 
                        WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0;";

            var iid = conn.Query<string>(sql, new { PlanID = planId, RoomID = roomId }, trans).FirstOrDefault();
            if (iid == null)
            {
                iid = Guid.NewGuid().ToString();
                sql = @"INSERT INTO Tb_Visit_VisitingCustomersDetail(ID,CommID,PlanID,RegionSNum,RegionName,BuildSNum,BuildName,
                            RoomID,RoomSign,RoomName,RoomSNum,CustID,CustName,AddUserId,AddTime,IsDelete)
                        SELECT IID=@IID,a.CommID,PlanID=@PlanID,b.RegionSNum,c.RegionName,b.BuildSNum,b.BuildName,a.RoomID,a.RoomSign,
                            a.RoomName,a.RoomSNum,d.CustID,e.CustName,UserCode=@UserCode,getdate(),0
                        FROM Tb_HSPR_Room a WITH(NOLOCK)
                        LEFT JOIN Tb_HSPR_Building b WITH(NOLOCK) ON a.CommID=b.CommID AND a.BuildSNum=b.BuildSNum AND isnull(b.IsDelete,0)=0
                        LEFT JOIN Tb_HSPR_Region c WITH(NOLOCK) ON b.CommID=c.CommID AND b.RegionSNum=c.RegionSNum AND isnull(c.IsDelete,0)=0 
                        LEFT JOIN Tb_HSPR_CustomerLive d WITH(NOLOCK) ON a.RoomID=d.RoomID AND d.IsDelLive=0 AND d.IsActive=1 AND d.IsSale=0
                        LEFT JOIN Tb_HSPR_Customer e WITH(NOLOCK) ON d.CustID=e.CustID
                        WHERE a.RoomID=@RoomID;";

                int i = conn.Execute(sql, new { IID = iid, PlanID = planId, RoomID = roomId, UserCode = Global_Var.LoginUserCode }, trans);
                if (i == 1)
                {
                    ImportQuestion_v2(planId, iid, conn, trans);
                    return iid;
                }
                return null;
            }
            return iid;
        }

        /// <summary>
        /// 导入问卷问题
        /// </summary>
        private bool ImportQuestion(string planId, string detailId, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            // 判断是否已经导入问卷问题
            var sql = @"DECLARE @count int;
                        SELECT @count=count(1) FROM Tb_Visit_VisitingCustomersQuestionnaire WITH(NOLOCK) 
                        WHERE VisitingCustomersDetailID=@IID AND isnull(IsDelete,0)=0;
                        IF @count=0
                          BEGIN 
                            INSERT INTO Tb_Visit_VisitingCustomersQuestionnaire(ID,VisitingCustomersDetailID,VisitCategoryID,QuestionnaireCategoryID,
                                IsScore,IssueType,IssueName,IssueStandardScore,IssueProperty,Remark,Sort,Option_1,OptionStandardScore_1,
                                Option_2,OptionStandardScore_2,Option_3,OptionStandardScore_3,Option_4,OptionStandardScore_4,
                                Option_5,OptionStandardScore_5,IsProjectRequired, AddUserId, AddTime, PlanQuestionID)
                            SELECT convert(varchar(36),cast(cast(newid() AS binary(10))+cast(getdate() AS binary(6)) AS uniqueidentifier)),
                                @IID,VisitCategoryID,QuestionnaireCategoryID,IsScore,IssueType,IssueName,IssueStandardScore,IssueProperty,
                                Remark,Sort,Option_1,OptionStandardScore_1,Option_2,OptionStandardScore_2,Option_3,OptionStandardScore_3,
                                Option_4,OptionStandardScore_4,Option_5,OptionStandardScore_5,IsProjectRequired,@UserCode,getdate(),ID
                            FROM Tb_Visit_PlanQuestionnaire WITH(NOLOCK)
                            WHERE PlanID=@PlanID
                          END";
            // BUG-22521，根据ImportQuestion_v2方法，上面的sql增加了插入值PlanQuestionID，插入值为Tb_Visit_PlanQuestionnaire表的ID字段
            var i = conn.Execute(sql, new { IID = detailId, PlanID = planId, UserCode = Global_Var.LoginUserCode }, trans);
            if (i == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 导入问卷问题第二版，包括多选题
        /// </summary>
        public bool ImportQuestion_v2(string planId, string detailId, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            // 判断是否已经导入问卷问题
            var sql = @"SELECT count(1) FROM Tb_Visit_VisitingCustomersQuestionnaire WITH(NOLOCK)
                        WHERE VisitingCustomersDetailID=@IID AND isnull(IsDelete,0)=0;";

            if (conn.Query<int>(sql, new { IID = detailId }, trans).FirstOrDefault() == 0)
            {
                // 导入问卷问题
                sql = @"SELECT ID,VisitCategoryID,QuestionnaireCategoryID,IsScore,IssueType,IssueName,IssueStandardScore,
                            IssueProperty,Remark,Sort,IsProjectRequired
                        FROM Tb_Visit_PlanQuestionnaire WITH(NOLOCK)
                        WHERE PlanID=@PlanID;";

                var data = conn.Query(sql, new { PlanID = planId }, trans).ToList();

                var parameter = new DynamicParameters();
                sql = "";

                int i = 0;
                for (i = 0; i < data.Count(); i++)
                {
                    var item = data[i];
                    sql += $@"INSERT INTO Tb_Visit_VisitingCustomersQuestionnaire(ID,PlanQuestionID,VisitingCustomersDetailID,VisitCategoryID,
                                QuestionnaireCategoryID,IsScore,IssueType,IssueName,IssueStandardScore,IssueProperty,
                                Remark,Sort,IsProjectRequired,AddUserId,AddTime)
                              VALUES(@IID_{i},@PlanQuestionID_{i},'{detailId}',@VisitCategoryID_{i},@QuestionnaireCategoryID_{i},
                                @IsScore_{i},@IssueType_{i},@IssueName_{i},@IssueStandardScore_{i},@IssueProperty_{i},@Remark_{i},@Sort_{i},
                                @IsProjectRequired_{i},'{Global_Var.LoginUserCode}',getdate());";

                    parameter.Add($"@IID_{i}", SequentialGuid.NewGuid().ToString());
                    parameter.Add($"@PlanQuestionID_{i}", item.ID);
                    parameter.Add($"@VisitCategoryID_{i}", item.VisitCategoryID);
                    parameter.Add($"@QuestionnaireCategoryID_{i}", item.QuestionnaireCategoryID);
                    parameter.Add($"@IsScore_{i}", item.IsScore);
                    parameter.Add($"@IssueType_{i}", item.IssueType);
                    parameter.Add($"@IssueName_{i}", item.IssueName);
                    parameter.Add($"@IssueStandardScore_{i}", item.IssueStandardScore);
                    parameter.Add($"@IssueProperty_{i}", item.IssueProperty);
                    parameter.Add($"@Remark_{i}", item.Remark);
                    parameter.Add($"@Sort_{i}", item.Sort);
                    parameter.Add($"@IsProjectRequired_{i}", item.IsProjectRequired);
                }

                if (sql.Length == 0)
                    return false;

                i = conn.Execute(sql, parameter, trans);

                // 导入问题答案
                sql = @"SELECT a.ID,d.[Option] AS OptionContent,d.OptionStandardScore,isnull(d.Sort,0) AS Sort,d.Remarks
                        FROM Tb_Visit_VisitingCustomersQuestionnaire a WITH(NOLOCK INDEX(IX_Tb_Visit_VisitingCustomersQuestionnaire_1)) 
                            LEFT JOIN Tb_Visit_VisitingCustomersDetail b WITH(NOLOCK) On a.VisitingCustomersDetailID=b.ID
                            LEFT JOIN Tb_Visit_PlanQuestionnaire c WITH(NOLOCK) ON b.PlanID=c.PlanID AND a.PlanQuestionID=c.ID 
                            LEFT JOIN Tb_Visit_PlanQuestionnaireOption d WITH(NOLOCK) ON c.ID=d.QuestionID
                        WHERE a.VisitingCustomersDetailID=@IID AND d.ID IS NOT NULL AND isnull(a.IsDelete,0)=0;";

                // 导入问题答案
                // 暂时没有搬迁 索引  先直接使用
                sql = @"SELECT a.ID,d.[Option] AS OptionContent,d.OptionStandardScore,isnull(d.Sort,0) AS Sort,d.Remarks
                        FROM Tb_Visit_VisitingCustomersQuestionnaire a WITH(NOLOCK) 
                            LEFT JOIN Tb_Visit_VisitingCustomersDetail b WITH(NOLOCK) On a.VisitingCustomersDetailID=b.ID
                            LEFT JOIN Tb_Visit_PlanQuestionnaire c WITH(NOLOCK) ON b.PlanID=c.PlanID AND a.PlanQuestionID=c.ID 
                            LEFT JOIN Tb_Visit_PlanQuestionnaireOption d WITH(NOLOCK) ON c.ID=d.QuestionID
                        WHERE a.VisitingCustomersDetailID=@IID AND d.ID IS NOT NULL AND isnull(a.IsDelete,0)=0;";
                data = conn.Query(sql, new { IID = detailId }, trans).ToList();

                parameter = new DynamicParameters();

                sql = "";

                for (i = 0; i < data.Count(); i++)
                {
                    var item = data[i];
                    sql += $@"INSERT INTO Tb_Visit_VisitingCustomersQuestionnaireOption(ID,QuestionID,[Option],OptionStandardScore,Remarks,Sort) 
                              VALUES (@IID_{i},@QuestionID_{i},@Option_{i},@OptionStandardScore_{i},@Remark_{i},@Sort_{i});";

                    parameter.Add($"@IID_{i}", SequentialGuid.NewGuid().ToString());
                    parameter.Add($"@QuestionID_{i}", item.ID);
                    parameter.Add($"@Option_{i}", item.OptionContent);
                    parameter.Add($"@OptionStandardScore_{i}", item.OptionStandardScore);
                    parameter.Add($"@Remark_{i}", item.Remarks);
                    parameter.Add($"@Sort_{i}", item.Sort);
                }
                if (!String.IsNullOrEmpty(sql))
                {
                    i = conn.Execute(sql, parameter, trans);
                }

                if (i == 0)
                    return false;
            }

            return true;
        }
        #endregion

        class TaskSimpleModel
        {
            public string TaskId { get; set; }
            public string PlanName { get; set; }
            public string BeginTime { get; set; }
            public string EndTime { get; set; }
            public decimal PlanCoverageRate { get; set; }
            public decimal RealCoverageRate { get; set; }
            public string TaskRoleName { get; set; }
            public string TaskUserName { get; set; }
            public int PlanState { get; set; }
        }
    }
}
