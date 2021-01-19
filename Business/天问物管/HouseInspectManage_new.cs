using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Business
{
    public class HouseInspectManage_new : PubInfo
    {
        public HouseInspectManage_new()
        {
            base.Token = "20181030HouseInspectManage_new";
        }
        public override void Operate(ref Transfer Trans)
        {

            try
            {
                DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];

                //验证登录
                if (!new Login().isLogin(ref Trans))
                {
                    return;
                }

                if (Global_Var.LoginCorpID == "1973")
                {
                    HK_OperateLog.WriteLog(Trans.Class, Trans.Command);
                }
                switch (Trans.Command)
                {
                    case "SaveHouseInspectPolling":
                        Trans.Result = SaveHouseInspectPolling(Row);
                        break;
                    case "GetHouseInspectPollingList":
                        Trans.Result = GetHouseInspectPollingList(Row);
                        break;
                    case "SaveHouseInspect":
                        Trans.Result = SaveHouseInspect(Row);
                        break;
                    case "GetHouseInspectList":
                        Trans.Result = GetHouseInspectList(Row);
                        break;
                    case "GetRoomObjectUnitList":
                        Trans.Result = GetRoomObjectUnitList(Row);
                        break;
                    case "GetIncidentTypeList":
                        Trans.Result = GetIncidentTypeList(Row);
                        break;
                    case "GetUnitFilingList":
                        Trans.Result = GetUnitFilingList(Row);
                        break;
                    case "GetRoomList":
                        Trans.Result = GetRoomList(Row);
                        break;
                    case "GetUnqualifiedTypeList":
                        Trans.Result = GetUnqualifiedTypeList(Row);
                        break;
                    default:
                        Trans.Result = new ApiResult(false, "接口不存在").toJson();
                        break;
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                Trans.Result = new ApiResult(false, "接口抛出了一个异常").toJson();
            }
        }
        /// <summary>
        /// 获取不合格原因列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetUnqualifiedTypeList(DataRow row)
        {
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                List<dynamic> list = conn.Query("SELECT Id,Name,Code,Sort FROM Tb_HI_Dictionary WHERE DType = '不合格原因' AND ISNULL(IsDelete,0) = 0").ToList();
                if (null == list)
                {
                    list = new List<dynamic>();
                }
                return new ApiResult(true, list).toJson();
            }
        }
        /// <summary>
        /// 上传验房整改任务
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string SaveHouseInspectPolling(DataRow row)
        {
            // 是否严格验证模式
            bool Mode = true;
            JArray jArray = null;
            DateTime DateNow = DateTime.Now;
            string UserCode = Global_Var.LoginUserCode;
            string UserName = Global_Var.LoginUserName;
            var Data = default(string);

            if (!row.Table.Columns.Contains("Data"))
            {
                return new ApiResult(false, "缺少参数Data").toJson();
            }

            if (Global_Var.LoginCorpID != "2009" && Global_Var.LoginCorpID != "2022")
            {
                if (!Base64Helper.Decode(Encoding.UTF8, row["Data"].ToString(), out Data))
                {
                    return new ApiResult(false, "Data格式有误").toJson();
                }
            }
            else
            {
                Data = row["Data"].ToString();
            }
            try
            {
                jArray = JArray.Parse(System.Web.HttpUtility.UrlDecode(Data));
            }
            catch (Exception)
            {
                return new ApiResult(false, "Data格式有误,不是有效的JSON数组格式").toJson();
            }
            if (null == jArray || jArray.Count == 0)
            {
                return new ApiResult(true, "没有需要保存的数据").toJson();
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();
                try
                {
                    foreach (JObject item in jArray)
                    {
                        string TaskId = (string)item["TaskId"];

                        #region 遍历房屋数据列表,进行保存

                        JArray RoomList = (JArray)item["RoomList"];
                        foreach (JObject RoomItem in RoomList)
                        {
                            string RoomID = (string)RoomItem["RoomID"];

                            #region 遍历房屋对象标准数据列表,进行保存

                            JArray StanardList = (JArray)RoomItem["StanardList"];
                            foreach (JObject StanardItem in StanardList)
                            {
                                string StandardId = (string)StanardItem["StandardId"];
                                int IsQualified = (int)StanardItem["IsQualified"];
                                string ProblemContent = (string)StanardItem["ProblemContent"];
                                string addProblemContent = (string)StanardItem["AddProblemContent"];
                                string CheckDate = (string)StanardItem["CheckDate"];
                                string CheckPid = Global_Var.LoginUserCode;
                                string CheckRemark = "";
                                #region 查询对应任务对象标准是否存在
                                dynamic Stanard = conn.QueryFirstOrDefault("SELECT * FROM Tb_HI_TaskStandard WHERE TaskId = @TaskId AND HouseId = @RoomID AND StandardId = @StandardId", new { TaskId, RoomID, StandardId }, trans);
                                if (null == Stanard)
                                {
                                    GetLog().InfoFormat("任务({0})房屋({1})下的对象标准({2})信息不存在,TaskData={3}", TaskId, RoomID, StandardId, JsonConvert.SerializeObject(item));
                                    if (Mode)
                                    {
                                        trans.Rollback();
                                        return new ApiResult(false, "任务房屋下的对象标准不存在,保存失败(TaskId=" + TaskId + ",RoomID=" + RoomID + ",StandardId=" + StandardId + ")").toJson();
                                    }
                                    continue;
                                }
                                string TaskStandardId = Convert.ToString(Stanard.Id);
                                if (string.IsNullOrEmpty(ProblemContent))
                                {
                                    ProblemContent = Convert.ToString(Stanard.ProblemContent);
                                }
                                int ReworkTimes = Convert.ToInt32(Stanard.ReworkTimes);
                                #endregion

                                if (IsQualified == 1)
                                {
                                    #region 验收合格
                                    if (conn.Execute("UPDATE Tb_HI_TaskStandard SET IsQualified=1, ProblemContent = @ProblemContent, CheckPid = @CheckPid, CheckDate = @CheckDate WHERE TaskId = @TaskId AND HouseId = @RoomID AND StandardId = @StandardId", new { ProblemContent, CheckPid, CheckDate, TaskId, RoomID, StandardId }, trans) <= 0)
                                    {
                                        GetLog().InfoFormat("任务({0})房屋({1})下的对象标准({2})在保存标准时保存失败,TaskData={3}", TaskId, RoomID, StandardId, JsonConvert.SerializeObject(item));
                                        if (Mode)
                                        {
                                            trans.Rollback();
                                            return new ApiResult(false, "在保存标准时,保存失败(TaskId=" + TaskId + ",RoomID=" + RoomID + ",StandardId=" + StandardId + ")").toJson();
                                        }
                                        continue;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region 验收返工
                                    ReworkTimes += 1;
                                    string RealCompleteDate = null;
                                    if (conn.Execute("UPDATE Tb_HI_TaskStandard SET  ProblemContent = @ProblemContent, RealCompleteDate = @RealCompleteDate, ReworkTimes = @ReworkTimes WHERE TaskId = @TaskId AND HouseId = @RoomID AND StandardId = @StandardId",
                                        new { ProblemContent, RealCompleteDate, ReworkTimes, TaskId, RoomID, StandardId }, trans) <= 0)
                                    {
                                        GetLog().InfoFormat("任务({0})房屋({1})下的对象标准({2})在保存标准时保存失败,TaskData={3}", TaskId, RoomID, StandardId, JsonConvert.SerializeObject(item));
                                        if (Mode)
                                        {
                                            trans.Rollback();
                                            return new ApiResult(false, "在保存标准时,保存失败(TaskId=" + TaskId + ",RoomID=" + RoomID + ",StandardId=" + StandardId + ")").toJson();
                                        }
                                        continue;
                                    }

                                    // 中南添加相关记录
                                    if (Global_Var.LoginCorpID == "2009" || Global_Var.LoginCorpID == "2022")
                                    {
                                        var taskCode = conn.Query<string>(@"SELECT TaskCode FROM Tb_HI_TaskStandard WHERE TaskId = @TaskId AND HouseId = @RoomID AND StandardId = @StandardId", new { TaskId, RoomID, StandardId }, trans).FirstOrDefault();

                                        if (!string.IsNullOrEmpty(taskCode))
                                        {
                                            conn.Execute($@"INSERT INTO Tb_Incident_TaskProcess(ProcessCode,TaskCode,BussType,InfoType,
                                                        ProcessUser,ProcessContent,ProcessDate)
                                                        VALUES(newid(), '{taskCode}', '报事返工', '【{Global_Var.LoginUserName}】进行了报事返工',
                                                        '{Global_Var.LoginUserName}',
                                                        '【{Global_Var.LoginUserName}】验房整改验收不合格返工，验收不合格内容：【{addProblemContent}】', 
                                                        getdate())", null, trans);
                                        }

                                        // 中南，添加返工记录信息
                                        conn.Execute(@"INSERT INTO Tb_HI_TaskStandardReworkRecord(TsID,ProblemContent,AddUserCode)
                                                        VALUES(@TsID, @ProblemContent, @UserCode)",
                                        new
                                        {
                                            TsID = TaskStandardId,
                                            ProblemContent = addProblemContent,
                                            UserCode = Global_Var.LoginUserCode
                                        }, trans);
                                    }
                                    #endregion
                                }

                                #region 增加一条记录
                                if (conn.Execute("INSERT INTO Tb_HI_TaskCheckLog VALUES(NEWID(),@TaskStandardId,@CheckDate,@CheckPid,@CheckRemark,@IsQualified)", new { TaskStandardId, CheckDate, CheckPid, CheckRemark, IsQualified }, trans) <= 0)
                                {
                                    GetLog().InfoFormat("任务({0})房屋({1})下的对象标准({2})在插入验收记录时保存失败,TaskData={3}", TaskId, RoomID, StandardId, JsonConvert.SerializeObject(item));
                                    if (Mode)
                                    {
                                        trans.Rollback();
                                        return new ApiResult(false, "在插入验收记录时,保存失败(TaskId=" + TaskId + ",RoomID=" + RoomID + ",StandardId=" + StandardId + ")").toJson();
                                    }
                                    continue;
                                }
                                #endregion
                            }
                            #endregion
                        }

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                    return new ApiResult(false, "在保存内容时抛出了一个异常").toJson();
                }
                trans.Commit();
                return new ApiResult(true, "上传成功").toJson();
            }
        }
        /// <summary>
        /// 获取验房整改任务
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetHouseInspectPollingList(DataRow row)
        {
            int page = 1;
            int size = 10;
            if (row.Table.Columns.Contains("Page"))
            {
                if (int.TryParse(row["Page"].ToString(), out page))
                {
                    if (page <= 0)
                    {
                        page = 1;
                    }
                }
            }
            if (row.Table.Columns.Contains("Size"))
            {
                if (int.TryParse(row["Size"].ToString(), out size))
                {
                    if (size <= 0)
                    {
                        size = 10;
                    }
                }
            }
            // 规则page*size +1 , (page + 1)*size，page 从0开始
            int Start = (page - 1) * size + 1;
            int End = page * size;
            int CommID = 0;
            if (row.Table.Columns.Contains("CommID"))
            {
                if (!int.TryParse(row["CommID"].ToString(), out CommID))
                {
                    CommID = 0;
                }
            }
            string UserCode = Global_Var.LoginUserCode;

            string ToDay = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = @"SELECT TaskId,TaskNo,CommID,CommName,Batch,BatchName,RoleCode,RoleName,BeginTime,EndTime,ProfessionName
                                FROM (SELECT *, ROW_NUMBER() OVER(ORDER BY TaskNo ASC) AS RowId FROM View_Tb_HI_Task AS a 
                                        WHERE CommID = @CommID AND ISNULL(IsDelete, 0) = 0 
                                        AND RoleCode IN( SELECT RoleCode FROM view_Sys_UserRole_Filter WHERE UserCode = @UserCode) 
                                        AND TaskId IN(SELECT TaskId FROM Tb_HI_TaskStandard WHERE IsQualified = 0 AND ISNULL(CheckDate,'') = '' 
                                                        AND ISNULL(CheckPid,'') = '')) AS a WHERE RowId BETWEEN @Start AND @End";
                List<dynamic> list = conn.Query<dynamic>(sql, new { CommID, UserCode, ToDay, Start, End }).ToList();
                if (null == list)
                {
                    list = new List<dynamic>();
                    return new ApiResult(true, list).toJson();
                }
                foreach (dynamic item in list)
                {
                    string TaskId = Convert.ToString(item.TaskId);
                    //#region 获取房屋RoomID和对应的查验图纸

                    sql = @"SELECT Id,HouseId FROM Tb_HI_TaskHouse WHERE TaskId=@TaskId  
                            AND HouseId IN(SELECT HouseId FROM Tb_HI_TaskStandard WHERE IsQualified = 0 AND ISNULL(CheckDate,'') = '' 
                                                    AND TaskId = @TaskId GROUP BY HouseId)";
                    var roomList = conn.Query(sql, new { TaskId = TaskId });

                    var resultSet = new List<dynamic>();

                    foreach (dynamic taskHouse in roomList)
                    {
                        sql = @"SELECT x.InspectionDrawingsId,x.Drawing,
                            (SELECT TOP 1 FilesPath FROM Tb_HI_Files WHERE isnull(IsDelete,0)=0 AND PkId=@Id AND FilesNo=x.InspectionDrawingsId 
	                            ORDER BY AddDate DESC) AS CurrentDrawing 
                            FROM Tb_HI_InspectionDrawings x WHERE x.HouseID=@HouseID AND x.Batch=@Batch AND x.IsDelete=0";

                        var files = conn.Query(sql, new { Id = taskHouse.Id, HouseID = taskHouse.HouseId, Batch = item.Batch });

                        string RoomID = Convert.ToString(taskHouse.HouseId);
                        sql = @"SELECT Id,ObjectId,StandardId,StandardCode,Content,DutyUnitId,RectificationUnitId,ProblemType,ProblemContent,
                                    PollingCompleteDate,RealCompleteDate,CompleteRemark,ReworkTimes FROM View_Tb_HI_TaskStandard 
                                WHERE TaskId = @TaskId AND HouseId = @RoomID AND IsQualified = 0 AND ISNULL(CheckDate,'') = '' ";
                        List<dynamic> ObjectStandardList = conn.Query<dynamic>(sql, new { TaskId, RoomID }).ToList();
                        foreach (dynamic ObjectStandardItem in ObjectStandardList)
                        {
                            string TaskStandardId = Convert.ToString(ObjectStandardItem.Id);
                            string ObjectId = Convert.ToString(ObjectStandardItem.ObjectId);
                            ObjectStandardItem.ObjectName = GetObjectName(PubConstant.hmWyglConnectionString, ObjectId);

                            string DutyUnitId = Convert.ToString(ObjectStandardItem.DutyUnitId);
                            string RectificationUnitId = Convert.ToString(ObjectStandardItem.RectificationUnitId);
                            string ProblemType = Convert.ToString(ObjectStandardItem.ProblemType);

                            //#region 获取问题类型名称，如果不存在，显示空
                            if (Global_Var.LoginCorpID == "2009" || Global_Var.LoginCorpID == "2022")
                            {
                                ObjectStandardItem.ProblemTypeName = conn.QueryFirstOrDefault<string>("SELECT InfoContent AS TypeName FROM Tb_Incident_Reason WHERE ReasonCode=@ProblemType", new { ProblemType });
                            }
                            else
                            {
                                ObjectStandardItem.ProblemTypeName = conn.QueryFirstOrDefault<string>("SELECT x.TypeName FROM Tb_HSPR_IncidentType x INNER JOIN Tb_HSPR_CorpIncidentType y ON x.TypeCode = y.TypeCode AND ISNULL(y.IsDelete,0) = 0 WHERE ISNULL(x.IsEnabled, 0) = 0 AND x.TypeID = @ProblemType", new { ProblemType });
                            }

                            //#endregion

                            //#region 获取责任单位和整改单位
                            ObjectStandardItem.DutyUnitName = conn.QueryFirstOrDefault<string>("SELECT UnitName FROM View_HI_GetUnitFilingList WHERE (UnitType = 1 OR UnitType = 3) AND ResponsibleUnitFile = @ResponsibleUnitID", new { ResponsibleUnitID = DutyUnitId });
                            ObjectStandardItem.RectificationUnitName = conn.QueryFirstOrDefault<string>("SELECT UnitName FROM View_HI_GetUnitFilingList WHERE (UnitType = 2 OR UnitType = 3) AND ResponsibleUnitFile = @ResponsibleUnitID", new { ResponsibleUnitID = RectificationUnitId });
                            //#endregion

                            string FilePath = "";
                            //#region 获取问题图片
                            List<string> FileList = conn.Query<string>("SELECT FilesPath FROM Tb_HI_Files WHERE PkId = @PkId",
                                new { PkId = TaskStandardId }).ToList();
                            if (null != FileList)
                            {
                                FilePath = string.Join(",", FileList.ToArray());
                            }
                            //#endregion
                            ObjectStandardItem.FilePath = FilePath;
                        }

                        resultSet.Add(new
                        {
                            RoomID = taskHouse.HouseId,
                            Files = files,
                            ObjectStandardList = ObjectStandardList
                        });
                    }

                    item.RoomList = resultSet;

                    //sql = @"SELECT a.HouseId as RoomID,b.InspectionDrawingsId,b.Drawing,
                    //        (SELECT TOP 1 FilesPath FROM Tb_HI_Files WHERE isnull(IsDelete,0)=0 AND PkId=a.Id AND FilesNo=b.InspectionDrawingsId
                    //            ORDER BY AddDate DESC) AS CurrentDrawing
                    //        FROM View_Tb_HI_TaskHouse a LEFT JOIN Tb_HI_InspectionDrawings b ON a.HouseId = b.HouseId 
                    //        WHERE TaskId = @TaskId AND a.HouseId IN(SELECT HouseId FROM Tb_HI_TaskStandard 
                    //            WHERE IsQualified = 0 AND ISNULL(CheckDate,'') = '' AND TaskID = @TaskID GROUP BY HouseId)";
                    //List<dynamic> RoomList = conn.Query(sql, new { TaskId }).ToList();
                    //if (null == RoomList)
                    //{
                    //    RoomList = new List<dynamic>();
                    //}

                    //#region 获取问题列表
                    //foreach (dynamic RoomItem in RoomList)
                    //{
                    //    string RoomID = Convert.ToString(RoomItem.RoomID);
                    //    sql = @"SELECT Id,ObjectId,StandardId,StandardCode,Content,DutyUnitId,RectificationUnitId,ProblemType,ProblemContent,
                    //                PollingCompleteDate,RealCompleteDate,CompleteRemark,ReworkTimes FROM View_Tb_HI_TaskStandard 
                    //            WHERE TaskId = @TaskId AND HouseId = @RoomID AND IsQualified = 0 AND ISNULL(CheckDate,'') = '' ";
                    //    List<dynamic> ObjectStandardList = conn.Query<dynamic>(sql, new { TaskId, RoomID }).ToList();
                    //    foreach (dynamic ObjectStandardItem in ObjectStandardList)
                    //    {
                    //        string TaskStandardId = Convert.ToString(ObjectStandardItem.Id);
                    //        string ObjectId = Convert.ToString(ObjectStandardItem.ObjectId);
                    //        ObjectStandardItem.ObjectName = GetObjectName(PubConstant.hmWyglConnectionString, ObjectId);

                    //        string DutyUnitId = Convert.ToString(ObjectStandardItem.DutyUnitId);
                    //        string RectificationUnitId = Convert.ToString(ObjectStandardItem.RectificationUnitId);
                    //        string ProblemType = Convert.ToString(ObjectStandardItem.ProblemType);

                    //        #region 获取问题类型名称，如果不存在，显示空
                    //        ObjectStandardItem.ProblemTypeName = conn.QueryFirstOrDefault<string>("SELECT x.TypeName FROM Tb_HSPR_IncidentType x INNER JOIN Tb_HSPR_CorpIncidentType y ON x.TypeCode = y.TypeCode AND ISNULL(y.IsDelete,0) = 0 WHERE ISNULL(x.IsEnabled, 0) = 0 AND x.TypeID = @ProblemType", new { ProblemType });
                    //        #endregion

                    //        #region 获取责任单位和整改单位
                    //        ObjectStandardItem.DutyUnitName = conn.QueryFirstOrDefault<string>("SELECT UnitName FROM View_HI_GetUnitFilingList WHERE (UnitType = 1 OR UnitType = 3) AND ResponsibleUnitFile = @ResponsibleUnitID", new { ResponsibleUnitID = DutyUnitId });
                    //        ObjectStandardItem.RectificationUnitName = conn.QueryFirstOrDefault<string>("SELECT UnitName FROM View_HI_GetUnitFilingList WHERE (UnitType = 2 OR UnitType = 3) AND ResponsibleUnitFile = @ResponsibleUnitID", new { ResponsibleUnitID = RectificationUnitId });
                    //        #endregion

                    //        string FilePath = "";
                    //        #region 获取问题图片
                    //        List<string> FileList = conn.Query<string>("SELECT FilesPath FROM Tb_HI_Files WHERE PkId = @PkId", 
                    //            new { PkId = TaskStandardId }).ToList();
                    //        if (null != FileList)
                    //        {
                    //            FilePath = string.Join(",", FileList.ToArray());
                    //        }
                    //        #endregion
                    //        ObjectStandardItem.FilePath = FilePath;
                    //    }
                    //    RoomItem.ObjectStandardList = ObjectStandardList;
                    //}
                    //#endregion
                    //item.RoomList = RoomList;
                    //#endregion

                }
                return new ApiResult(true, list).toJson();
            }
        }
        /// <summary>
        /// 上传验房任务
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string SaveHouseInspect(DataRow row)
        {
            // 是否严格验证模式
            bool Mode = false;
            JArray jArray = null;
            DateTime DateNow = DateTime.Now;
            string UserCode = Global_Var.LoginUserCode;
            string UserName = Global_Var.LoginUserName;
            var Data = default(string);

            if (!row.Table.Columns.Contains("Data"))
            {
                return new ApiResult(false, "缺少参数Data").toJson();
            }

            if (Global_Var.LoginCorpID != "2009" && Global_Var.LoginCorpID != "2022")
            {
                if (!Base64Helper.Decode(Encoding.UTF8, row["Data"].ToString(), out Data))
                {
                    return new ApiResult(false, "Data格式有误").toJson();
                }
            }
            else
            {
                Data = row["Data"].ToString();
            }
            try
            {
                jArray = JArray.Parse(System.Web.HttpUtility.UrlDecode(Data));
            }
            catch (Exception)
            {
                return new ApiResult(false, "Data格式有误,不是有效的JSON数组格式").toJson();
            }
            if (null == jArray || jArray.Count == 0)
            {
                return new ApiResult(true, "没有需要保存的数据").toJson();
            }
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var trans = conn.BeginTransaction();

                try
                {
                    foreach (JObject item in jArray)
                    {
                        string TaskId = (string)item["TaskId"];
                        string TaskStatus = (string)item["TaskStatus"];
                        #region 检查任务是否存在
                        dynamic Task = conn.QueryFirstOrDefault("SELECT * FROM Tb_HI_Task WHERE TaskId = @TaskId", new { TaskId }, trans);
                        if (null == Task)
                        {
                            GetLog().InfoFormat("任务({0})不存在,TaskData={1}", TaskId, JsonConvert.SerializeObject(item));
                            if (Mode)
                            {
                                trans.Rollback();
                                return new ApiResult(false, "任务不存在,保存失败(TaskId=" + TaskId + ")").toJson();
                            }
                            continue;
                        }
                        // 不考虑任务已完成的状态，当任务已完成时，覆盖数据
                        #endregion

                        JArray RoomList = (JArray)item["RoomList"];

                        foreach (JObject RoomItem in RoomList)
                        {
                            string RoomID = (string)RoomItem["RoomID"];
                            string CompleteDate = (string)RoomItem["CompleteDate"];

                            if (RoomItem.TryGetValue("ReceCode", out JToken receCode))
                            {
                                if (!string.IsNullOrEmpty(receCode.ToString()))
                                {
                                    conn.Execute("UPDATE Tb_HI_TaskHouse SET ReceCode=@ReceCode WHERE TaskId=@TaskId AND HouseId=@RoomID",
                                        new { ReceCode = receCode.ToString(), TaskId = TaskId, RoomID = RoomID }, trans);
                                }
                            }

                            // 默认上传房屋为完成(现在仅已完成房屋可以上传)
                            int IsComplete = 1;
                            string CompletePid = UserCode;
                            // 默认合格
                            int RoomIsQualified = 1;

                            JArray StanardList = (JArray)RoomItem["StanardList"];

                            foreach (JObject StanardItem in StanardList)
                            {
                                string StandardId = (string)StanardItem["StandardId"];
                                string IsQualified = (string)StanardItem["IsQualified"];
                                string ProblemType = (string)StanardItem["ProblemType"];
                                string ProblemContent = (string)StanardItem["ProblemContent"];
                                string ResponsibleUnitID = (string)StanardItem["ResponsibleUnitID"];
                                string RectificationUnitID = (string)StanardItem["RectificationUnitID"];
                                string UnqualifiedType = (string)StanardItem["UnqualifiedType"];

                                if (StanardItem.TryGetValue("TaskNum", out JToken taskNum) &&
                                    StanardItem.TryGetValue("TaskCode", out JToken taskCode))
                                {
                                    if (!string.IsNullOrEmpty(taskNum.ToString()) && !string.IsNullOrEmpty(taskCode.ToString()))
                                    {
                                        conn.Execute("UPDATE Tb_HI_TaskStandard SET IncidentNO=@TaskNum,TaskCode=@TaskCode WHERE TaskId=@TaskId AND HouseId=@RoomID AND StandardId=@StandardId",
                                        new { TaskNum = taskNum, TaskCode = taskCode.ToString(), TaskId = TaskId, RoomID = RoomID, StandardId = StandardId }, trans);
                                    }
                                    else
                                    {
                                        conn.Execute("UPDATE Tb_HI_TaskStandard SET IncidentNO=NULL,TaskCode=NULL WHERE TaskId=@TaskId AND HouseId=@RoomID AND StandardId=@StandardId",
                                        new { TaskNum = taskNum, TaskCode = taskCode.ToString(), TaskId = TaskId, RoomID = RoomID, StandardId = StandardId }, trans);
                                    }
                                }

                                if ("0".Equals(IsQualified))
                                {
                                    // 房屋下标准有一条不合格，视为房屋不合格
                                    RoomIsQualified = 0;
                                }

                                #region 当标准不合格时验证相关数据
                                if (!"1".Equals(IsQualified))
                                {
                                    #region 验证问题描述,如果内容为空，默认查验不合格
                                    if (string.IsNullOrEmpty(ProblemContent))
                                    {
                                        ProblemContent = string.Format("查验不合格({0},{1})", Global_Var.LoginUserName, DateNow.ToString("yyyy/MM/dd HH:mm:ss"));
                                    }
                                    #endregion

                                    #region 不合格情况下,验证不合格原因
                                    if (!string.IsNullOrEmpty(UnqualifiedType))
                                    {
                                        dynamic Unqualified = conn.QueryFirstOrDefault("SELECT * FROM Tb_HI_Dictionary WHERE DType = '不合格原因' AND Id = @Id ", new { Id = UnqualifiedType }, trans);
                                        if (null == Unqualified)
                                        {
                                            // 如果问题类型不存在,那么值为空
                                            UnqualifiedType = string.Empty;
                                        }
                                    }

                                    #endregion
                                }
                                #endregion


                                #region 修改对象标准信息
                                if (conn.Execute($@"UPDATE Tb_HI_TaskStandard SET IsQualified = {IsQualified}, ProblemType = '{ProblemType}', 
                                                    ProblemContent = '{ProblemContent}', DutyUnitId = '{ResponsibleUnitID}', RectificationUnitId = '{RectificationUnitID}', 
                                                    UnqualifiedType = '{UnqualifiedType}' WHERE TaskId = '{TaskId}' AND HouseId = {RoomID} AND StandardId = '{StandardId}'",
                                                    null, trans) <= 0)
                                {
                                    GetLog().InfoFormat("任务({0})房屋({1})下的对象标准({2})在保存标准时保存失败,TaskData={3}", TaskId, RoomID, StandardId, JsonConvert.SerializeObject(item));
                                    if (Mode)
                                    {
                                        trans.Rollback();
                                        return new ApiResult(false, "在保存标准时,保存失败(TaskId=" + TaskId + ",RoomID=" + RoomID + ",StandardId=" + StandardId + ")").toJson();
                                    }
                                    continue;
                                }
                                #endregion
                            }

                            #region 修改房屋信息

                            if (conn.Execute("UPDATE Tb_HI_TaskHouse SET IsComplete = @IsComplete, CompletePid = @CompletePid, CompleteDate = @CompleteDate, IsQualified = @RoomIsQualified WHERE TaskId = @TaskId AND HouseId = @RoomID", new { IsComplete, CompletePid, CompleteDate, RoomIsQualified, TaskId, RoomID }, trans) <= 0)
                            {
                                GetLog().InfoFormat("任务({0})房屋({1})在保存时保存失败,TaskData={2}", TaskId, RoomID, JsonConvert.SerializeObject(item));
                                if (Mode)
                                {
                                    trans.Rollback();
                                    return new ApiResult(false, "在保存房屋时,保存失败(TaskId=" + TaskId + ",RoomID=" + RoomID + ")").toJson();
                                }
                                continue;
                            }
                            #endregion
                        }

                        #region 进行任务状态修改
                        // 不判断保存结果，因为状态可能一致
                        conn.Execute("UPDATE Tb_HI_Task SET TaskStatus = @TaskStatus WHERE TaskId = @TaskId AND TaskStatus != @TaskStatus", new { TaskId, TaskStatus }, trans);
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                    return new ApiResult(false, "在保存内容时抛出了一个异常").toJson();
                }

                trans.Commit();
                return new ApiResult(true, "上传成功").toJson();
            }
        }

        /// <summary>
        /// 获取验房任务列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetHouseInspectList(DataRow row)
        {
            int page = 1;
            int size = 10;
            if (row.Table.Columns.Contains("Page"))
            {
                if (int.TryParse(row["Page"].ToString(), out page))
                {
                    if (page <= 0)
                    {
                        page = 1;
                    }
                }
            }
            if (row.Table.Columns.Contains("Size"))
            {
                if (int.TryParse(row["Size"].ToString(), out size))
                {
                    if (size <= 0)
                    {
                        size = 10;
                    }
                }
            }
            // 规则page*size +1 , (page + 1)*size，page 从0开始
            int Start = (page - 1) * size + 1;
            int End = page * size;
            int CommID = 0;
            if (row.Table.Columns.Contains("CommID"))
            {
                if (!int.TryParse(row["CommID"].ToString(), out CommID))
                {
                    CommID = 0;
                }
            }
            string UserCode = Global_Var.LoginUserCode;

            string ToDay = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = $@"SELECT TaskId,TaskNo,CommID,CommName,Batch,BatchName,RoleCode,RoleName,BeginTime,EndTime,ProfessionName,
                                (SELECT count(1) FROM Tb_HI_TaskHouse x WHERE x.TaskId=a.TaskId AND isnull(x.IsDelete,0)=0) AS RoomCount,
                                (SELECT count(1) FROM Tb_HI_TaskHouse x WHERE x.TaskId=a.TaskId AND isnull(x.IsDelete,0)=0 AND x.IsComplete=1) AS CompletedRoomCount,
                                (SELECT count(1) FROM Tb_HI_TaskStandard x WHERE x.TaskId=a.TaskId AND isnull(x.IsDelete,0)=0 AND x.IsQualified=1) AS QualifiedStandardCount,
                                (SELECT count(1) FROM Tb_HI_TaskStandard x WHERE x.TaskId=a.TaskId AND isnull(x.IsDelete,0)=0 AND x.IsQualified=0) AS UnqualifiedStandardCount,
                                (SELECT count(1) FROM Tb_HI_TaskStandard x WHERE x.TaskId=a.TaskId AND isnull(x.IsDelete,0)=0 AND x.IsQualified=-1) AS IgnoreStandardCount
                                { ((Global_Var.LoginCorpID == "2009" || Global_Var.LoginCorpID == "2022") ? ",IsIncident" : "") }
                                FROM (SELECT *, ROW_NUMBER() OVER(ORDER BY TaskNo ASC) AS RowId FROM View_Tb_HI_Task AS a 
                                        WHERE CommID = @CommID AND ISNULL(IsDelete, 0) = 0 
                                        AND RoleCode IN( SELECT RoleCode FROM  view_Sys_UserRole_Filter WHERE UserCode = @UserCode) 
                                        AND TaskStatus != '已完成' AND BeginTime <= @ToDay) AS a WHERE RowId BETWEEN @Start AND @End";
                List<dynamic> list = conn.Query<dynamic>(sql, new { CommID, UserCode, ToDay, Start, End }).ToList();
                if (null == list)
                {
                    return new ApiResult(true, list).toJson();
                }
                foreach (dynamic item in list)
                {
                    string TaskId = Convert.ToString(item.TaskId);
                    string Batch = Convert.ToString(item.Batch);
                    #region 获取房屋RoomID和对应的查验图纸
                    // 
                    sql = @"SELECT Id,HouseId,isnull(IsComplete,0) AS IsComplete FROM Tb_HI_TaskHouse WHERE TaskId=@TaskId AND isnull(IsDelete,0)=0";
                    var roomList = conn.Query(sql, new { TaskId = TaskId });

                    var resultSet = new List<dynamic>();

                    sql = @"SELECT x.InspectionDrawingsId,x.Drawing,
                            (SELECT TOP 1 FilesPath FROM Tb_HI_Files WHERE isnull(IsDelete,0)=0 AND PkId=@Id AND FilesNo=x.InspectionDrawingsId 
	                            ORDER BY AddDate DESC) AS CurrentDrawing 
                            FROM Tb_HI_InspectionDrawings x WHERE x.HouseID=@HouseID AND x.Batch=@Batch AND x.IsDelete=0";

                    foreach (dynamic taskHouse in roomList)
                    {
                        resultSet.Add(new
                        {
                            RoomID = taskHouse.HouseId,
                            IsComplete = taskHouse.IsComplete,
                            Files = conn.Query(sql, new { Id = taskHouse.Id, HouseID = taskHouse.HouseId, Batch = Batch })
                        });
                    }

                    //sql = @"SELECT a.HouseId as RoomID,b.InspectionDrawingsId,b.Drawing,
                    //        (SELECT TOP 1 FilesPath FROM Tb_HI_Files WHERE isnull(IsDelete,0)=0 AND PkId=a.Id AND FilesNo=b.InspectionDrawingsId 
                    //            ORDER BY AddDate DESC) AS CurrentDrawing 
                    //        FROM View_Tb_HI_TaskHouse a 
                    //        LEFT JOIN Tb_HI_InspectionDrawings b ON a.HouseId = b.HouseId AND b.Batch = @Batch 
                    //        WHERE TaskId = @TaskId AND ISNULL(IsComplete,0) = 0";
                    //List<dynamic> RoomList = conn.Query(sql, new { TaskId, Batch }).ToList();
                    //if (null == RoomList)
                    //{
                    //    RoomList = new List<dynamic>();
                    //}
                    item.RoomList = resultSet;
                    #endregion
                    #region 获取查验对象
                    // 先查询任务对象列表
                    sql = "SELECT ObjectId FROM View_Tb_HI_TaskStandard WHERE TaskId = @TaskId GROUP BY ObjectId";
                    List<string> TaskObjectList = conn.Query<string>(sql, new { TaskId }).ToList();
                    if (null == TaskObjectList)
                    {
                        TaskObjectList = new List<string>();
                    }
                    sql = "SELECT StandardId,ObjectId,StandardCode,Content,InspectMethod,PermissibleDeviation,Profession,ProfessionName,Importance,ImportanceName,Form,FormName,Batch,BatchName,Remark FROM View_Tb_HI_SysStandardList WHERE StandardId IN(SELECT StandardId FROM Tb_HI_TaskStandard WHERE TaskId = @TaskId GROUP BY StandardId) AND ObjectId = @ObjectId";
                    List<Dictionary<string, object>> ObjectStandardList = new List<Dictionary<string, object>>();

                    Dictionary<string, object> dictionary;
                    foreach (string ObjectId in TaskObjectList)
                    {
                        // 根据对象Id查询对象所有的标准
                        List<dynamic> StandardList = conn.Query(sql, new { TaskId, ObjectId }).ToList();
                        if (null == StandardList)
                        {
                            StandardList = new List<dynamic>();
                        }
                        //
                        dictionary = new Dictionary<string, object>
                        {
                            { "ObjectId", ObjectId },
                            { "ObjectName", GetObjectName(PubConstant.hmWyglConnectionString, ObjectId) },
                            { "StandardList", StandardList }
                        };
                        ObjectStandardList.Add(dictionary);
                    }
                    item.ObjectStandardList = ObjectStandardList;
                    #endregion

                }
                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 循环拼接对象名称，直到没有父级为止
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="ObjectId"></param>
        /// <returns></returns>
        private string GetObjectName(string connStr, string ObjectId)
        {
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string ObjectName = "";
                dynamic obj = null;
                do
                {
                    // 查询该标准
                    obj = conn.QueryFirstOrDefault("SELECT ParentId,ObjectName FROM Tb_HI_SysObject WHERE ObjectId = @ObjectId", new { ObjectId });
                    if (null != obj)
                    {
                        // 如果标准不为空，取出该标准的名称
                        string name = Convert.ToString(obj.ObjectName);
                        if (string.IsNullOrEmpty(name))
                        {
                            // 如果标准名称为空，默认名字为空
                            name = "空";
                        }
                        // 进行对象名字格式拼接
                        ObjectName = name + (string.IsNullOrEmpty(ObjectName) ? "" : "-" + ObjectName);
                        //重新赋值新的标准对象Id
                        ObjectId = Convert.ToString(obj.ParentId);
                    }
                    // 判断如果标准对象不为空的话,继续循环查询父级
                } while (obj != null);
                return ObjectName;
            }
        }

        /// <summary>
        /// 获取房屋关联的对象整改单位
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetRoomObjectUnitList(DataRow row)
        {
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT 0 AS RoomID,ObjectID,ResponsibleUnitID,RectificationUnitID FROM Tb_HI_ResponsibleUnitSet 
                            WHERE isnull(IsDelete,0)=0
                            GROUP BY ObjectID,ResponsibleUnitID,RectificationUnitID;";

                var list = conn.Query(sql);
                return new ApiResult(true, list).toJson();
            }
        }


        /// <summary>
        /// 获取项目所有报事类别
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetIncidentTypeList(DataRow row)
        {
            int CommID = 0;
            if (row.Table.Columns.Contains("CommID"))
            {
                if (!int.TryParse(row["CommID"].ToString(), out CommID))
                {
                    CommID = 0;
                }
            }
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                if (Global_Var.LoginCorpID == "2022" || Global_Var.LoginCorpID == "2009")
                {
                    var tmp = conn.Query(@"SELECT ReasonCode AS TypeID,ReasonSortCode AS TypeCode,InfoContent AS TypeName
                                            FROM Tb_Incident_Reason WHERE ISNULL(IsDelete, 0) = 0 ;");

                    return new ApiResult(true, tmp).toJson();
                }

                List<dynamic> list = conn.Query("SELECT x.TypeID,x.TypeCode,x.TypeName FROM Tb_HSPR_IncidentType x INNER JOIN Tb_HSPR_CorpIncidentType y ON x.TypeCode = y.TypeCode AND ISNULL(y.IsDelete,0) = 0 " +
                    "WHERE x.CommID = @CommID AND ISNULL(x.IsEnabled, 0) = 0 ", new { CommID }).ToList();

                return new ApiResult(true, list).toJson();
            }

        }
        /// <summary>
        /// 获取该项目所有的责任单位/整改单位列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetUnitFilingList(DataRow row)
        {
            int CommID = 0;
            if (row.Table.Columns.Contains("CommID"))
            {
                if (!int.TryParse(row["CommID"].ToString(), out CommID))
                {
                    CommID = 0;
                }
            }
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                List<dynamic> list = conn.Query("SELECT ResponsibleUnitFile,UnitType,UnitName FROM View_HI_GetUnitFilingList WHERE CommID = @CommID AND ISNULL(IsDelete,0) = 0", new { CommID }).ToList();
                if (null == list)
                {
                    list = new List<dynamic>();
                }
                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 获取任务用到的房屋信息列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetRoomList(DataRow row)
        {
            int CommID = 0;
            if (row.Table.Columns.Contains("CommID"))
            {
                if (!int.TryParse(row["CommID"].ToString(), out CommID))
                {
                    CommID = 0;
                }
            }
            string UserCode = Global_Var.LoginUserCode;

            string ToDay = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT RoomID,RoomName,RoomSign,RegionSNum,RegionName,BuildSNum,BuildName,UnitSNum,FloorSNum,RoomSNum FROM view_HSPR_Room_Filter WHERE ISNULL(IsDelete,0) = 0 AND RoomID IN " +
                    "( " +
                        "SELECT HouseId FROM View_Tb_HI_TaskHouse WHERE TaskId IN " +
                        "( " +
                            "SELECT TaskId FROM View_Tb_HI_Task WHERE CommID = @CommID AND ISNULL(IsDelete, 0) = 0 AND CommID IN " +
                            "( " +
                                "SELECT CommID FROM view_Sys_User_RoleData_Filter WHERE UserCode = @UserCode " +
                            ") AND BeginTime <= @ToDay AND TaskStatus != '已完成' " +
                            "UNION " +
                            "SELECT TaskId FROM Tb_HI_TaskStandard WHERE IsQualified = 0 AND ISNULL(CheckDate,'') = '' AND ISNULL(CheckPid,'') = '' " +
                        ") AND ISNULL(IsDelete,0) = 0 GROUP BY HouseId " +
                    ")";
                var list = conn.Query(sql, new { CommID, UserCode, ToDay }).ToList();
                return new ApiResult(true, list).toJson();
            }
        }
    }
}
