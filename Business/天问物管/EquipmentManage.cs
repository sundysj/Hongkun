using Dapper;
using DapperExtensions;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TWTools.Push;

namespace Business
{
    public class EquipmentManage : PubInfo
    {
        public EquipmentManage()
        {
            base.Token = "20160503Equipment";
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

                //项目，责任人，时间
                switch (Trans.Command)
                {
                    //登记设备状态
                    case "SaveEquipmentState":
                        Trans.Result = SaveEquipmentState(Row);
                        break;
                    //获取设备状态
                    case "GetEquipmentStateList":
                        Trans.Result = GetEquipmentStateList(Row);
                        break;
                    //获取当前设备状态
                    case "GetEquipmentState":
                        Trans.Result = GetEquipmentState(Row);
                        break;
                    //获取当前巡检
                    case "GetEquipmentTaskList":
                        Trans.Result = GetEquipmentTaskList(Row);
                        break;
                    case "SubmitEquipmentTaskList":
                        Trans.Result = SubmitEquipmentTaskList(Row);
                        break;
                    case "SubmitEquipmentTaskListNew":
                        Trans.Result = SubmitEquipmentTaskListNew(Row);
                        break;
                    //维保任务列表
                    case "GetMaintenanceTaskList":
                        Trans.Result = GetMaintenanceTaskList(Row);
                        break;
                    //设备维保任务上传
                    case "SubmitMaintenanceTaskList":
                        Trans.Result = SubmitMaintenanceTaskList(Row);
                        break;
                    case "SubmitMaintenanceTaskListNew":
                        Trans.Result = SubmitMaintenanceTaskListNew(Row);
                        break;
                    //设备维保验收
                    case "SubmitMaintenanceTaskAcceptance":
                        Trans.Result = SubmitMaintenanceTaskAcceptance(Row);
                        break;
                    //设备图片上传附件
                    case "SubmitMaintenanceTaskFile":
                        Trans.Result = SubmitMaintenanceTaskFile(Row);
                        break;
                    case "SendMsgAll":
                        Trans.Result = SendMsgAll(Row);
                        break;
                    case "SendMsgRole":
                        Trans.Result = SendMsgRole(Row);
                        break;
                    case "SaveEventChronicle":
                        Trans.Result = SaveEventChronicle(Row);
                        break;
                    case "GetEventChronicleList":
                        Trans.Result = GetEventChronicleList(Row);
                        break;
                    case "GetEventChronicleType":
                        Trans.Result = GetEventChronicleType(Row);
                        break;
                    case "GetEquipmentStatusList":
                        Trans.Result = GetEquipmentStatusList(Row);
                        break;
                    case "SaveEquipmentStatus":
                        Trans.Result = SaveEquipmentStatus(Row);
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

        /// <summary>
        /// 
        /// </summary>
        public string SaveEquipmentStatus(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("EquipmentId") || string.IsNullOrEmpty(Row["EquipmentId"].ToString()))
            {
                return new ApiResult(false, "EquipmentId不能为空").toJson();
            }

            if (!Row.Table.Columns.Contains("EquipmentStatus") || string.IsNullOrEmpty(Row["EquipmentStatus"].ToString()))
            {
                return new ApiResult(false, "EquipmentStatus不能为空").toJson();
            }
            if (!Row.Table.Columns.Contains("BeginTime") || string.IsNullOrEmpty(Row["BeginTime"].ToString()))
            {
                return new ApiResult(false, "开始时间不能为空").toJson();
            }
            if (!Row.Table.Columns.Contains("EndTime") || string.IsNullOrEmpty(Row["EndTime"].ToString()))
            {
                return new ApiResult(false, "结束时间不能为空").toJson();
            }
            string equipmentId = Row["EquipmentId"].ToString();
            string equipmentStatus = Row["EquipmentStatus"].ToString();//正常中，维修中，维保中，故障中，正常停机，异常停机
            string beginTime = Row["BeginTime"].ToString();
            string endTime = Row["EndTime"].ToString();
            string remark = "";
            if (Row.Table.Columns.Contains("Remark"))
            {
                remark = Row["Remark"].ToString();
            }
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"INSERT INTO Tb_Eq_EquipmentStatus
                            (Id,BeginTime,EndTime,EquipmentStatus,Remark,EquipmentId,AddPid,AddTime,IsDelete)
                            VALUES(NEWID(),@BeginTime,@EndTime,@EquipmentStatus,@Remark,@EquipmentId,@AddPid,GETDATE(),0)";

                int count = conn.Execute(sql, new
                {
                    BeginTime = beginTime,
                    EndTime = endTime,
                    EquipmentStatus = equipmentStatus,
                    Remark = remark,
                    EquipmentId = equipmentId,
                    AddPid = Global_Var.LoginUserCode
                });

                if (count > 0)
                {
                    return JSONHelper.FromString(true, "保存成功");
                }
                else
                {
                    return JSONHelper.FromString(false, "保存失败");
                }

            }
        }

        public string GetEquipmentStatusList(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("EquipmentId") || string.IsNullOrEmpty(Row["EquipmentId"].ToString()))
            {
                return new ApiResult(false, "EquipmentId不能为空").toJson();
            }
            string equipmentId = Row["EquipmentId"].ToString();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                int PageIndex = 1;
                int PageSize = 5;

                if (Row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(Row["PageIndex"].ToString()))
                {
                    PageIndex = AppGlobal.StrToInt(Row["PageIndex"].ToString());
                }
                if (Row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(Row["PageSize"].ToString()))
                {
                    PageSize = AppGlobal.StrToInt(Row["PageSize"].ToString());
                }


                var sql = $@"SELECT * FROM View_Tb_Eq_EquipmentStatus_Filter WHERE ISNULL(IsDelete,0)=0 AND EquipmentId={equipmentId}";

                int PageCount = 0, Counts = 0;
                DataTable dtTask = GetList(out PageCount, out Counts, sql, PageIndex, PageSize, "AddTime", 1, "Id", PubConstant.hmWyglConnectionString).Tables[0];

                return JSONHelper.FromString(dtTask);
            }
        }

        public string GetEventChronicleType(DataRow Row)
        {
                var sql = $@"SELECT Id,Name FROM Tb_Eq_Dictionary WHERE DType='大事件类别' AND ISNULL(IsDelete,0)=0";

                DataTable dt = Query(sql).Tables[0];

                return JSONHelper.FromString(dt);
            
        }
        /// <summary>
        /// 获取大事记列表
        /// </summary>
        public string GetEventChronicleList(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("EquiId") || string.IsNullOrEmpty(Row["EquiId"].ToString()))
            {
                return new ApiResult(false, "EquiId不能为空").toJson();
            }
            string equiId = Row["EquiId"].ToString();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                int PageIndex = 1;
                int PageSize = 5;

                if (Row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(Row["PageIndex"].ToString()))
                {
                    PageIndex = AppGlobal.StrToInt(Row["PageIndex"].ToString());
                }
                if (Row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(Row["PageSize"].ToString()))
                {
                    PageSize = AppGlobal.StrToInt(Row["PageSize"].ToString());
                }


                var sql = $@"SELECT * FROM VIEW_EQ_GetEventChronicle WHERE ISNULL(IsDelete,0)=0 AND EquiId = {equiId}";

                int PageCount = 0, Counts = 0;
                DataTable dtTask = GetList(out PageCount, out Counts, sql, PageIndex, PageSize, "AddDate", 1, "EventId", PubConstant.hmWyglConnectionString).Tables[0];

                return JSONHelper.FromString(dtTask);
            }
        }

        public string SaveEventChronicle(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("EquiId") || string.IsNullOrEmpty(Row["EquiId"].ToString()))
            {
                return new ApiResult(false, "EquiId不能为空").toJson();
            }

            if (!Row.Table.Columns.Contains("EventType") || string.IsNullOrEmpty(Row["EventType"].ToString()))
            {
                return new ApiResult(false, "EventType不能为空").toJson();
            }
            if (!Row.Table.Columns.Contains("EventContent") || string.IsNullOrEmpty(Row["EventContent"].ToString()))
            {
                return new ApiResult(false, "大事记内容不能为空").toJson();
            }
            string equiId = Row["EquiId"].ToString();
            string eventType = Row["EventType"].ToString();
            string eventContent = Row["EventContent"].ToString();
            string remark = "";
            if (Row.Table.Columns.Contains("Remark"))
            {
                remark = Row["Remark"].ToString();
            }
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"INSERT INTO Tb_EQ_EventChronicle
                                (EventId,EquiId,Sort,EventType,HappenDate,EventContent,ResponsibilityPerson,Remark,AddPId,AddDate)
                            VALUES(NEWID(),@EquiId,(SELECT MAX(Sort)+1 AS Sort FROM Tb_EQ_EventChronicle),@EventType,GETDATE(),
                                   @EventContent,@ResponsibilityPerson,@Remark,@AddPId,GETDATE());";

                int count = conn.Execute(sql, new
                {
                    EquiId = equiId,
                    EventType = eventType,
                    EventContent = eventContent,
                    ResponsibilityPerson = Global_Var.LoginUserCode,
                    Remark = remark,
                    AddPId = Global_Var.LoginUserCode
                });

                if (count > 0)
                {
                    return JSONHelper.FromString(true, "保存成功");
                }
                else
                {
                    return JSONHelper.FromString(false, "保存失败");
                }

            }
        }

        #region

        public string SendMsgAll(DataRow Row)
        {
            string result = "";
            IDbConnection Conn = new SqlConnection(PubConstant.tw2bsConnectionString);
            string strSQL = "SELECT MobileTel FROM Tb_Sys_User WHERE isnull(IsDelete,0)=0 AND isnull(MobileTel,'')<>'' AND isnull(IsMobile,0)=1";
            Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
            DataTable dtUser = Conn.ExecuteReader(strSQL, null, null, null, CommandType.Text).ToDataSet().Tables[0];
            List<string> telMo = new List<string>();
            for (int i = 0; i < dtUser.Rows.Count; i++)
            {
                telMo.Add(dtUser.Rows[i][0].ToString());
            }
            string tw2bsConnectionString = PubConstant.tw2bsConnectionString;
            string hmWyglConnectionString = PubConstant.hmWyglConnectionString;
            string corpId = Global_Var.CorpId;
            System.Threading.Tasks.Task.Run(() =>
            {
                string appIdentifier, appKey, appSecret;

                if (Common.Push.GetAppKeyAndAppSecret(tw2bsConnectionString, corpId, out appIdentifier, out appKey, out appSecret))
                {

                    // 推送
                    PushModel pushModel = new PushModel(appKey, appSecret)
                    {
                        AppIdentifier = appIdentifier,
                        Badge = 1
                    };
                    pushModel.Audience.Category = PushAudienceCategory.Alias;
                    if (telMo.Count > 0)
                    {
                        pushModel.Title = Row["Title"].ToString();
                        pushModel.Message = Row["Title"].ToString();
                        pushModel.Command = PushCommand.QUALITY_EQUIPMENT_GENERATE;
                        pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                        pushModel.Audience.Objects.AddRange(telMo);
                        Push.SendAsync(pushModel);
                        return;
                    }
                }
            });
            return result;
        }
        public string SendMsgRole(DataRow Row)
        {
            string result = "";
            IDbConnection Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
            #region 激光推送消息
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", Row["Data"].ToString().Trim());
            DataTable dtUser = Conn.ExecuteReader("Proc_Get_AppPush_Role", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
            List<string> telMo = new List<string>();
            for (int i = 0; i < dtUser.Rows.Count; i++)
            {
                telMo.Add(dtUser.Rows[i][0].ToString());
            }
            string tw2bsConnectionString = PubConstant.tw2bsConnectionString;
            string hmWyglConnectionString = PubConstant.hmWyglConnectionString;
            string corpId = Global_Var.CorpId;
            System.Threading.Tasks.Task.Run(() =>
            {
                if (Common.Push.GetAppKeyAndAppSecret(tw2bsConnectionString, corpId, out string appIdentifier, out string appKey, out string appSecret))
                {
                    // 推送
                    PushModel pushModel = new PushModel(appKey, appSecret)
                    {
                        AppIdentifier = appIdentifier,
                        Badge = 1
                    };
                    pushModel.Audience.Category = PushAudienceCategory.Alias;
                    if (telMo.Count > 0)
                    {
                        pushModel.Title = Row["Title"].ToString();
                        pushModel.Message = Row["MsgContent"].ToString();
                        pushModel.Command = PushCommand.QUALITY_EQUIPMENT_GENERATE;
                        pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                        pushModel.Audience.Objects.AddRange(telMo);
                        Push.SendAsync(pushModel);
                        return;
                    }
                }
            });
            DynamicParameters parametersQ = new DynamicParameters();
            parametersQ.Add("@Id", Row["Data"].ToString().Trim());
            Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
            Conn.ExecuteReader("Proc_Get_AppPush_Role_Delete", parametersQ, null, null, CommandType.StoredProcedure);
            #endregion
            return result;
        }
        #endregion
        //SaveEquipmentState
        #region 设备状态上传
        public string SaveEquipmentState(DataRow Row)
        {
            string Result = JSONHelper.FromString(false, "未知错误");
            try
            {
                HM.Model.Eq.Tb_EQ_Task M_Task = new HM.Model.Eq.Tb_EQ_Task();
                HM.Model.Eq.Tb_Eq_EquipmentStatus M_EquipmentStatus = new HM.Model.Eq.Tb_Eq_EquipmentStatus();
                AppGlobal.FillModel(Row, M_EquipmentStatus);
                M_EquipmentStatus.EndTime = Convert.ToDateTime("2099-12-31 23:59:59");
                M_EquipmentStatus.Id = Guid.NewGuid().ToString();
                new HM.BLL.Eq.Bll_Tb_Eq_EquipmentStatus().Add(M_EquipmentStatus);
            }
            catch (Exception e)
            {
                return JSONHelper.FromString(false, e.Message);
            }
            return JSONHelper.FromString(true, "上传成功!");
        }
        #endregion
        #region 设备巡查任务上传
        public string SubmitEquipmentTaskListNew(DataRow Row)
        {
            string Result = JSONHelper.FromString(false, "未知错误");
            try
            {
                if (Row["Data"] == null || Row["Data"].ToString().Trim() == "")
                {
                    return JSONHelper.FromString(false, "没有数据");
                }
                HM.Model.Eq.Tb_EQ_Task M_Task = new HM.Model.Eq.Tb_EQ_Task();
                HM.BLL.Eq.Bll_Tb_EQ_Task B_Task = new HM.BLL.Eq.Bll_Tb_EQ_Task();
                HM.Model.Eq.Tb_EQ_TaskEquipment M_TaskEquipment = new HM.Model.Eq.Tb_EQ_TaskEquipment();
                HM.BLL.Eq.Bll_Tb_EQ_TaskEquipment B_TaskEquipment = new HM.BLL.Eq.Bll_Tb_EQ_TaskEquipment();
                HM.Model.Eq.Tb_EQ_TaskEquipmentLine M_TaskEquipmentLine = new HM.Model.Eq.Tb_EQ_TaskEquipmentLine();
                HM.BLL.Eq.Bll_Tb_EQ_TaskEquipmentLine B_TaskEquipmentLine = new HM.BLL.Eq.Bll_Tb_EQ_TaskEquipmentLine();
                ArrayList rows = (ArrayList)JSON.Decode(Row["Data"].ToString().Trim());
                IDbConnection Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                bool CheckBool = false;
                DynamicParameters parametersIng = new DynamicParameters();
                parametersIng.Add("@CommID", "CheckAll");
                parametersIng.Add("@Type", "RoutingInspection");
                DataTable dTable = Conn.ExecuteReader("Proc_EqTask_PhoneCheckUp", parametersIng, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
                if (dTable.Rows.Count > 0)
                {
                    if (dTable.Rows[0][0].ToString().Trim() == "TRUE")
                    {
                        CheckBool = true;
                    }
                }
                foreach (Hashtable row in rows)
                {
                    var doTime = default(string);
                    string UpdateSQLTask = "";

                    try
                    {
                        if (row.Contains("doTime") && !string.IsNullOrEmpty(row["doTime"].ToString()))
                            doTime = row["doTime"].ToString();
                        else
                            doTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.000");

                        UpdateSQLTask = "Update Tb_EQ_Task set PollingPerson='" + row["PollingPerson"] + "',Statue = '已完成',PollingDate='" + row["PollingDate"] + "',doTime='" + row["doTime"] + "' where TaskId='" + row["TaskId"] + "'";

                        new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).ExecuteSql(UpdateSQLTask);
                    }
                    catch (Exception)
                    {
                        UpdateSQLTask = "Update Tb_EQ_Task set PollingPerson='" + row["PollingPerson"] + "',Statue = '已完成',PollingDate='" + row["PollingDate"] + "' where TaskId='" + row["TaskId"] + "'";
                        new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).ExecuteSql(UpdateSQLTask);
                    }

                    if (row["EquipentList"] == null)
                    {
                        return JSONHelper.FromString(false, "无巡检设备");
                    }
                    ArrayList EquipentRows = (ArrayList)row["EquipentList"];
                    //更新任务设备信息
                    foreach (Hashtable Equipent in EquipentRows)
                    {
                        //执行更新任务设备语句
                        string UpdateSQLEquipent = "Update Tb_EQ_TaskEquipment set PollingNote='" + Equipent["PollingNote"] + "',PollingResult='" + Equipent["PollingResult"] + "' ";
                        if (Equipent.Contains("BSBH"))
                        {
                            UpdateSQLEquipent += ",BSBH='" + Equipent["BSBH"] + "',IsMend='" + Equipent["IsMend"] + "' ";
                        }
                        UpdateSQLEquipent += " where TaskEqId='" + Equipent["TaskEquiId"] + "'";
                        //执行更新操作
                        //string SqlStringTaskEquipent = "SELECT * FROM Tb_EQ_TaskEquipment where TaskEqId = '" + Equipent["TaskEquiId"] + "'";
                        //Update( SqlStringTaskEquipent, UpdateSQLEquipent);
                        int equipentCount = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).ExecuteSql(UpdateSQLEquipent);
                        if (equipentCount <= 0)
                        {
                            Result = JSONHelper.FromString(false, "无巡检设备");
                            return Result;
                        }
                        if (Equipent["LineList"] == null)
                        {
                            return JSONHelper.FromString(false, "无设备工艺路线信息");
                        }
                        //更新任务设备状态信息
                        ArrayList EquipmentState = (ArrayList)Equipent["EquipmentState"];
                        foreach (Hashtable Equipment in EquipmentState)
                        {
                            if (Equipment["BeginTime"].ToString().Trim() != "")
                            {
                                HM.Model.Eq.Tb_Eq_EquipmentStatus M_EquipmentStatus = new HM.Model.Eq.Tb_Eq_EquipmentStatus();
                                M_EquipmentStatus.AddTime = Convert.ToDateTime(Equipment["AddTime"]);
                                M_EquipmentStatus.AddPid = Equipment["AddPid"].ToString();
                                M_EquipmentStatus.EquipmentId = Equipment["EquipmentId"].ToString();
                                M_EquipmentStatus.Remark = Equipment["Remark"].ToString();
                                M_EquipmentStatus.EquipmentStatus = Equipment["EquipmentStatus"].ToString();
                                M_EquipmentStatus.BeginTime = Convert.ToDateTime(Equipment["BeginTime"]);
                                M_EquipmentStatus.EndTime = Convert.ToDateTime("2099-12-31 23:59:59");
                                M_EquipmentStatus.Id = Guid.NewGuid().ToString();
                                new HM.BLL.Eq.Bll_Tb_Eq_EquipmentStatus().Add(M_EquipmentStatus);
                            }
                        }
                        //更新任务设备工艺路线信息
                        ArrayList EquipentLineRows = (ArrayList)Equipent["LineList"];
                        foreach (Hashtable EquipentLine in EquipentLineRows)
                        {
                            //查询
                            string sqlEquipentLine = @"SELECT * FROM dbo.Tb_EQ_TaskEquipmentLine A 
                                                            LEFT OUTER JOIN dbo.Tb_EQ_PollingPlanDetail B ON A.DetailId = B.DetailId
                                                            WHERE TaskLineId = '" + EquipentLine["TaskLineId"] + "'";
                            DataTable dtEquipentLine = Query(sqlEquipentLine).Tables[0];
                            int boolPaging = 0;
                            for (int i = 0; i < dtEquipentLine.Rows.Count; i++)
                            {
                                string UpdateSQLEquipentLine = "";
                                boolPaging = 0;
                                //string SqlStringTaskEquipentLine = "SELECT * FROM Tb_EQ_TaskEquipmentLine where TaskLineId='" + EquipentLine["TaskLineId"] + "'";
                                //1选项   2数字   3文本
                                if (dtEquipentLine.Rows[i]["InputType"].ToString() == "1")
                                {
                                    if (EquipentLine["Value"].ToString().Trim() == "")
                                        boolPaging = 1;
                                    //执行更新任务设备工艺路线语句
                                    UpdateSQLEquipentLine = "Update Tb_EQ_TaskEquipmentLine set ChooseValue='" + EquipentLine["Value"] + "' where TaskLineId='" + EquipentLine["TaskLineId"] + "'";
                                }
                                if (dtEquipentLine.Rows[i]["InputType"].ToString() == "2")
                                {
                                    if (EquipentLine["Value"].ToString().Trim() == "")
                                        boolPaging = 1;
                                    //执行更新任务设备工艺路线语句
                                    UpdateSQLEquipentLine = "Update Tb_EQ_TaskEquipmentLine set NumValue='" + EquipentLine["Value"] + "' where TaskLineId='" + EquipentLine["TaskLineId"] + "'";
                                }
                                if (dtEquipentLine.Rows[i]["InputType"].ToString() == "3")
                                {
                                    //执行更新任务设备工艺路线语句
                                    UpdateSQLEquipentLine = "Update Tb_EQ_TaskEquipmentLine set TextValue='" + EquipentLine["Value"] + "' where TaskLineId='" + EquipentLine["TaskLineId"] + "'";
                                }
                                if (boolPaging < 1)
                                {
                                    //执行
                                    //Update(SqlStringTaskEquipentLine, UpdateSQLEquipentLine);
                                    int equipentLinCount = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).ExecuteSql(UpdateSQLEquipentLine);
                                    if (equipentLinCount <= 0)
                                    {
                                        return JSONHelper.FromString(false, "无设备工艺路线信息");
                                    }
                                }
                            }
                        }
                    }
                    if (CheckBool == true)
                    {
                        Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@Type", "Eq");
                        parameters.Add("@TaskId", row["TaskId"].ToString());
                        Conn.ExecuteReader("Proc_Eq_Submit_Phone", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
                        Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                    }
                }
            }
            catch (Exception e)
            {
                return JSONHelper.FromString(false, e.Message);
            }
            return JSONHelper.FromString(true, "上传成功!");
        }

        public string SubmitEquipmentTaskList(DataRow Row)
        {
            string Result = JSONHelper.FromString(false, "未知错误");
            try
            {
                if (Row["Data"] == null || Row["Data"].ToString().Trim() == "")
                {
                    return JSONHelper.FromString(false, "没有数据");
                }
                HM.Model.Eq.Tb_EQ_Task M_Task = new HM.Model.Eq.Tb_EQ_Task();
                HM.BLL.Eq.Bll_Tb_EQ_Task B_Task = new HM.BLL.Eq.Bll_Tb_EQ_Task();
                HM.Model.Eq.Tb_EQ_TaskEquipment M_TaskEquipment = new HM.Model.Eq.Tb_EQ_TaskEquipment();
                HM.BLL.Eq.Bll_Tb_EQ_TaskEquipment B_TaskEquipment = new HM.BLL.Eq.Bll_Tb_EQ_TaskEquipment();
                HM.Model.Eq.Tb_EQ_TaskEquipmentLine M_TaskEquipmentLine = new HM.Model.Eq.Tb_EQ_TaskEquipmentLine();
                HM.BLL.Eq.Bll_Tb_EQ_TaskEquipmentLine B_TaskEquipmentLine = new HM.BLL.Eq.Bll_Tb_EQ_TaskEquipmentLine();
                ArrayList rows = (ArrayList)JSON.Decode(Row["Data"].ToString().Trim());

                foreach (Hashtable row in rows)
                {
                    var doTime = default(string);
                    var PollingDate = default(string);
                  
                    string UpdateSQLTask = "";

                    try
                    {
                        if (row.Contains("doTime") && !string.IsNullOrEmpty(row["doTime"].ToString()))
                        {
                            doTime = row["doTime"].ToString();
                        }
                        else
                        {
                            doTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.000");
                        }

                        if (row.Contains("PollingDate") && !string.IsNullOrEmpty(row["PollingDate"].ToString()))
                        {
                            PollingDate = row["PollingDate"].ToString();
                        }
                        else
                        {
                            PollingDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.000");
                        }

                        if (DateTime.Compare(Convert.ToDateTime(doTime), Convert.ToDateTime(PollingDate))>0)
                        {
                            doTime =  Convert.ToDateTime(doTime).AddHours(-1).ToString();
                        }
                        
                          

                        UpdateSQLTask = "Update Tb_EQ_Task set PollingPerson='" + row["PollingPerson"] + "',Statue = '已完成',PollingDate='" + PollingDate + "',doTime='" + doTime + "' where TaskId='" + row["TaskId"] + "'";

                        new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).ExecuteSql(UpdateSQLTask);
                    }
                    catch (Exception)
                    {
                        UpdateSQLTask = "Update Tb_EQ_Task set PollingPerson='" + row["PollingPerson"] + "',Statue = '已完成',PollingDate='" + PollingDate + "' where TaskId='" + row["TaskId"] + "'";
                        new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).ExecuteSql(UpdateSQLTask);
                    }

                    if (row["EquipentList"] == null)
                    {
                        return JSONHelper.FromString(false, "无巡检设备");
                    }
                    ArrayList EquipentRows = (ArrayList)row["EquipentList"];
                    bool CheckBool = false;

                    SqlDataReader dataReader = new DbHelperSQLP(PubConstant.hmWyglConnectionString).RunProcedure("Proc_EqTask_PhoneCheckUp",
                        new SqlParameter[] {
                                    new SqlParameter(){ ParameterName = "CommID", Value = "CheckAll" },
                                    new SqlParameter(){ ParameterName = "Type", Value = "RoutingInspection" }
                        });
                    if (dataReader.HasRows)
                    {
                        if (dataReader.Read() && dataReader.GetString(0) == "TRUE")
                        {
                            CheckBool = true;
                        }
                    }
                    dataReader.Close();

                    //更新任务设备信息
                    foreach (Hashtable Equipent in EquipentRows)
                    {
                        // 如果上传了设备状态
                        if (Equipent.Contains("EquipmentStatus"))
                        {
                            if (row.Contains("doTime"))
                            {
                                doTime = row["doTime"].ToString();
                            }
                            string BeginTime = (doTime ?? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.000"));
                            string updateEqStatue = $@"
                                        DECLARE @ID NVARCHAR(36);
                                        DECLARE @EquipmentStatus NVARCHAR(10);
                                        IF exists(SELECT * FROM Tb_Eq_EquipmentStatus WHERE ISNULL(IsDelete,0)=0 AND EquipmentId='{Equipent["EquiId"]}')
                                          BEGIN
                                            SELECT TOP 1 @ID=ID,@EquipmentStatus=EquipmentStatus 
                                            FROM Tb_Eq_EquipmentStatus
                                            WHERE ISNULL(IsDelete,0)=0 AND EquipmentId='{Equipent["EquiId"]}' ORDER BY BeginTime DESC;

                                            IF @EquipmentStatus IS NOT NULL AND '{Equipent["EquipmentStatus"]}'<>@EquipmentStatus
                                              BEGIN
                                                UPDATE Tb_Eq_EquipmentStatus SET EndTime='{BeginTime}' WHERE ID=@ID;

                                                INSERT INTO Tb_Eq_EquipmentStatus(Id,BeginTime,EndTime,EquipmentStatus,EquipmentId,AddPid,AddTime)
                                                VALUES(newid(),'{BeginTime}','2099-12-31 23:59:59.000','{Equipent["EquipmentStatus"]}',
                                                        '{Equipent["EquiId"]}','{Global_Var.LoginUserCode}',getdate());
                                              END
                                          END
                                        ELSE
                                          BEGIN
                                            INSERT INTO Tb_Eq_EquipmentStatus(Id,BeginTime,EndTime,EquipmentStatus,EquipmentId,AddPid,AddTime)
                                            VALUES(newid(),'{BeginTime}','2099-12-31 23:59:59.000','{Equipent["EquipmentStatus"]}',
                                                        '{Equipent["EquiId"]}','{Global_Var.LoginUserCode}',getdate());
                                          END
                                        ";

                            new DbHelperSQLP(PubConstant.hmWyglConnectionString).ExecuteSql(updateEqStatue);
                        }

                        //执行更新任务设备语句
                        string UpdateSQLEquipent = $@"UPDATE Tb_EQ_TaskEquipment SET 
                                                                PollingNote='{Equipent["PollingNote"]}',
                                                                PollingResult='{Equipent["PollingResult"]}' ";
                        if (Equipent.Contains("bsbh") || Equipent.Contains("BSBH"))
                        {
                            string isMend = "否";
                            if (Equipent.Contains("isMend") && Equipent["isMend"].ToString().Trim() == "1")
                            {
                                isMend = "是";
                            }

                            if (Equipent.Contains("IsMend") && Equipent["IsMend"].ToString().Trim() == "1")
                            {
                                isMend = "是";
                            }

                            string bsbh = null;
                            if (isMend == "是")
                            {
                                if (Equipent.Contains("bsbh"))
                                {
                                    bsbh = Equipent["bsbh"].ToString().Trim();
                                }
                                if (Equipent.Contains("BSBH"))
                                {
                                    bsbh = Equipent["BSBH"].ToString().Trim();
                                }
                            }

                            UpdateSQLEquipent += ",BSBH='" + bsbh + "',IsMend='" + isMend + "' ";
                        }
                        UpdateSQLEquipent += " WHERE TaskEqId='" + Equipent["TaskEquiId"] + "'";
                        //执行更新操作
                        //string SqlStringTaskEquipent = "SELECT * FROM Tb_EQ_TaskEquipment where TaskEqId = '" + Equipent["TaskEquiId"] + "'";
                        //Update( SqlStringTaskEquipent, UpdateSQLEquipent);
                        int equipentCount = new DbHelperSQLP(PubConstant.hmWyglConnectionString).ExecuteSql(UpdateSQLEquipent);
                        if (equipentCount <= 0)
                        {
                            Result = JSONHelper.FromString(false, "无巡检设备");
                            return Result;
                        }
                        if (Equipent["LineList"] == null)
                        {
                            return JSONHelper.FromString(false, "无设备工艺路线信息");
                        }
                        int boolPaging = 0;
                        //更新任务设备工艺路线信息
                        ArrayList EquipentLineRows = (ArrayList)Equipent["LineList"];
                        foreach (Hashtable EquipentLine in EquipentLineRows)
                        {
                            //查询
                            string sqlEquipentLine = @"SELECT * FROM dbo.Tb_EQ_TaskEquipmentLine A 
                                                            LEFT OUTER JOIN dbo.Tb_EQ_PollingPlanDetail B ON A.DetailId = B.DetailId
                                                            WHERE TaskLineId = '" + EquipentLine["TaskLineId"] + "'";
                            //DataTable dtEquipentLine = Query(sqlEquipentLine).Tables[0];

                            DataTable dtEquipentLine = new DbHelperSQLP(PubConstant.hmWyglConnectionString).Query(sqlEquipentLine).Tables[0];

                            for (int i = 0; i < dtEquipentLine.Rows.Count; i++)
                            {
                                string UpdateSQLEquipentLine = "";
                                boolPaging = 0;
                                //string SqlStringTaskEquipentLine = "SELECT * FROM Tb_EQ_TaskEquipmentLine where TaskLineId='" + EquipentLine["TaskLineId"] + "'";
                                //1选项   2数字   3文本
                                if (dtEquipentLine.Rows[i]["InputType"].ToString() == "1")
                                {
                                    if (EquipentLine["Value"].ToString().Trim() == "")
                                        boolPaging = 1;
                                    //执行更新任务设备工艺路线语句
                                    UpdateSQLEquipentLine = $"UPDATE Tb_EQ_TaskEquipmentLine SET ChooseValue='{EquipentLine["Value"]}' WHERE TaskLineId='{EquipentLine["TaskLineId"]}'";
                                }
                                if (dtEquipentLine.Rows[i]["InputType"].ToString() == "2")
                                {
                                    if (EquipentLine["Value"].ToString().Trim().Replace(" ", "") == "")
                                        boolPaging = 1;
                                    //执行更新任务设备工艺路线语句
                                    UpdateSQLEquipentLine = $"UPDATE Tb_EQ_TaskEquipmentLine SET NumValue={EquipentLine["Value"].ToString().Trim().Replace(" ", "")} WHERE TaskLineId='{EquipentLine["TaskLineId"]}'";
                                }
                                if (dtEquipentLine.Rows[i]["InputType"].ToString() == "3")
                                {
                                    //执行更新任务设备工艺路线语句
                                    UpdateSQLEquipentLine = $"UPDATE Tb_EQ_TaskEquipmentLine SET TextValue='{EquipentLine["Value"]}' WHERE TaskLineId='{EquipentLine["TaskLineId"]}'";
                                }
                                if (boolPaging < 1)
                                {
                                    //执行
                                    //Update(SqlStringTaskEquipentLine, UpdateSQLEquipentLine);
                                    int equipentLinCount = new DbHelperSQLP(PubConstant.hmWyglConnectionString).ExecuteSql(UpdateSQLEquipentLine);
                                    if (equipentLinCount <= 0)
                                    {
                                        return JSONHelper.FromString(false, "无设备工艺路线信息");
                                    }
                                }
                            }
                        }
                    }
                    if (CheckBool == true)
                    {
                        new DbHelperSQLP(PubConstant.hmWyglConnectionString).RunProcedure("Proc_Eq_Submit_Phone",
                            new SqlParameter[] {
                                    new SqlParameter(){ ParameterName = "Type", Value = "Eq" },
                                    new SqlParameter(){ ParameterName = "TaskId", Value = row["TaskId"].ToString() }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return JSONHelper.FromString(true, "上传成功");
        }
        #endregion
        //GetEquipmentState
        #region 获取当前设备状态
        public string GetEquipmentState(DataRow row)
        {
            if (!row.Table.Columns.Contains("EqId") || string.IsNullOrEmpty(row["EqId"].ToString()))
            {
                return new ApiResult(false, "设备id不能为空").toJson();
            }
            string eqId = row["EqId"].ToString();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT TOP 1 EquipmentStatus FROM View_Phone_TB_EQ_EquipmentStatusList 
                            WHERE  EQUIPMENTID=@EqId ORDER BY BeginTime DESC ,EndTime DESC";

                var status = conn.Query<string>(sql, new { EqId = eqId }).FirstOrDefault();

                return JSONHelper.FromString(true, status ?? "正常中");
            }
        }
        #endregion
        #region 获取设备状态列表
        public string GetEquipmentStateList(DataRow Row)
        {
            string result = "";
            try
            {
                //ItemCode 项目编号，用户编码Usercode， Timer任务执行时间  yyyy-MM-dd
                if (Row.Table.Columns.Contains("UserCode") && Row["UserCode"].ToString() != "")
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(" SELECT *FROM View_Phone_TB_EQ_EquipmentStatusList WHERE 1=1 ");
                    if (Row.Table.Columns.Contains("CommID") && Row["CommID"].ToString().Trim() != "")
                    {
                        strSql.Append(" AND  CommID='" + Row["CommID"].ToString().Trim() + "'");
                    }
                    if (Row.Table.Columns.Contains("EqId") && Row["EqId"].ToString().Trim() != "")
                    {
                        strSql.Append(" AND  EQUIPMENTID='" + Row["EqId"].ToString().Trim() + "'");
                    }
                    if (Row.Table.Columns.Contains("EquipmentName") && Row["EquipmentName"].ToString().Trim() != "")
                    {
                        strSql.Append(" AND  EquipmentName like '%" + Row["EquipmentName"].ToString().Trim() + "%'");
                    }
                    if (Row.Table.Columns.Contains("EquipmentNO") && Row["EquipmentNO"].ToString().Trim() != "")
                    {
                        strSql.Append(" AND  EquipmentNO like '%" + Row["EquipmentNO"].ToString().Trim() + "%'");
                    }

                    int PageIndex = 1;
                    int PageSize = 5;
                    if (Row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(Row["PageIndex"].ToString()) > 0)
                    {
                        PageIndex = AppGlobal.StrToInt(Row["PageIndex"].ToString());
                    }
                    if (Row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(Row["PageSize"].ToString()) > 0)
                    {
                        PageSize = AppGlobal.StrToInt(Row["PageSize"].ToString());
                    }
                    int PageCount = 0, Counts = 0;
                    DataTable dtTask = GetList(out PageCount, out Counts, strSql.ToString(), PageIndex, PageSize, "EndTime", 1, "Id", PubConstant.hmWyglConnectionString).Tables[0];

                    result = JSONHelper.FromString(dtTask);
                }
                else
                {
                    result = JSONHelper.FromString(false, "缺少参数UserCode");
                }
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, EX.ToString());
            }

            return result;
        }
        #endregion
        #region 获取巡检任务
        public string GetEquipmentTaskList(DataRow Row)
        {
            string result = "";
            try
            {
                //ItemCode 项目编号，用户编码Usercode， Timer任务执行时间  yyyy-MM-dd
                if (Row.Table.Columns.Contains("CommID") &&
                    Row.Table.Columns.Contains("UserCode") &&
                    Row.Table.Columns.Contains("Timer") &&
                    Row["CommID"].ToString() != "" &&
                    Row["UserCode"].ToString() != "" &&
                    Row["Timer"].ToString() != "")
                {
                    using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                    {
                        // 判断是否是金辉系统,需要进行doMonth判断
                        bool isJinHui = false;
                        var sql = @"select count(1) from syscolumns where id=object_id('Tb_EQ_PollingPlan') and name='doMonth'";
                        if (conn.Query<int>(sql).FirstOrDefault() > 0)
                        {
                            isJinHui = true;
                        }

                        string Timer = Convert.ToDateTime(Row["Timer"]).ToString("yyyy-MM-dd");
                        StringBuilder strSql = new StringBuilder();
                        bool CheckUp = false;
                        //DynamicParameters parameters = new DynamicParameters();
                        //parameters.Add("@CommID", Row["CommID"].ToString().Trim());
                        //parameters.Add("@Type", "RoutingInspection");
                        //DataTable dTable = con.ExecuteReader("Proc_EqTask_PhoneCheckUp", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
                        //if (dTable.Rows.Count > 0)
                        {
                            //if (dTable.Rows[0][0].ToString().Trim() == "TRUE")
                            //{
                            //    CheckUp = true;
                            //    strSql.Append(" SELECT TaskNO,Content,TaskId,PlanId,EqId,SpaceId,CONVERT(NVARCHAR(50),BeginTime,121) as BeginTime,CONVERT(NVARCHAR(50),EndTime,121) as EndTime,RoleCode,Statue,ClosePerson,CONVERT(NVARCHAR(50),CloseTime,121) as CloseTime,CloseReason,PollingPerson,CONVERT(NVARCHAR(50),PollingDate,121) as PollingDate,Remark,IsDelete,AddPId,CONVERT(NVARCHAR(50),AddDate,121) as AddDate,CommId,CommName,PlanName,RankName,MacRoName,RoleName,ClosePersonName,PollingPersonName,CloseReasonName,MacRoAddress FROM TB_EQ_Task_"+ Row["CommID"].ToString().Trim() + "_Phone WHERE ISNULL(ISDELETE,0)=0 AND CommID='" + Row["CommID"].ToString().Trim() + "' ");
                            //}
                            //else
                            {
                                //CheckUp = false;
                                strSql.Append(@"SELECT TaskNO,Content,TaskId,PlanId,EqId,SpaceId,
                                CONVERT(NVARCHAR(50),BeginTime,121) as BeginTime,CONVERT(NVARCHAR(50),EndTime,121) as EndTime,
                                RoleCode,Statue,ClosePerson,CONVERT(NVARCHAR(50),CloseTime,121) as CloseTime,CloseReason,
                                PollingPerson,CONVERT(NVARCHAR(50),PollingDate,121) as PollingDate,Remark,IsDelete,AddPId,
                                CONVERT(NVARCHAR(50),AddDate,121) as AddDate,CommId,CommName,PlanName,RankName,MacRoName,
                                RoleName,ClosePersonName,PollingPersonName,CloseReasonName,MacRoAddress 
                                FROM View_EQ_PatrolTaskExec_Phone_List 
                                WHERE ISNULL(ISDELETE,0)=0 AND CommID='" + Row["CommID"].ToString().Trim() + "' ");
                            }
                        }
                        strSql.Append(" AND RoleCode IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode ");
                        strSql.Append(" IN( SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode = '" + Global_Var.UserCode + "'))  ");
                        strSql.Append(" AND Statue != '已完成' AND Statue != '已关闭' ");
                        strSql.Append(" AND CONVERT(nvarchar(100), BeginTime, 23) <='" + Timer + "' AND CONVERT(nvarchar(100), EndTime, 23) >= '" + Timer + "'");
                        strSql.Append(" AND  EndTime >= '" + Timer + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "'");
                        if (isJinHui && Global_Var.CorpId == "1329")
                        {
                            strSql.Append(" AND datediff(m, BeginTime, EndTime)<=1 UNION " + strSql.ToString());
                            int month = DateTime.Now.Month;
                            string doMonthSql = string.Format(" AND (doMonth IS NULL OR doMonth LIKE '%{0}%' OR LTRIM(RTRIM(doMonth))='')", month);
                            strSql.Append(doMonthSql);
                            strSql.Append(" AND datediff(m, BeginTime, EndTime)>1");
                        }
                        int PageIndex = 1;
                        int PageSize = 5;
                        if (Row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(Row["PageIndex"].ToString()) > 0)
                        {
                            PageIndex = AppGlobal.StrToInt(Row["PageIndex"].ToString());
                        }
                        if (Row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(Row["PageSize"].ToString()) > 0)
                        {
                            PageSize = AppGlobal.StrToInt(Row["PageSize"].ToString());
                        }
                        int PageCount = 0, Counts = 0;
                        DataTable dtTask = GetList(out PageCount, out Counts, strSql.ToString(), PageIndex, PageSize, "TaskId ", 1, "TaskId", PubConstant.hmWyglConnectionString).Tables[0];
                        //任务列表
                        //DataTable dtTask = Query(strSqlTask).Tables[0];
                        //dtTask.Columns["BeginTime"].DataType = Type.GetType("System.String");
                        dtTask.Columns.Add("EquipentList", Type.GetType("System.String"));
                        //获取任务设备列表　 
                        StringBuilder sbTaskEquipment = new StringBuilder();
                        string strTaskEquipmentSql = "";
                        DataRow[] dtTaskEquipmentLineRows;
                        HM.Model.Qm.Tb_Interface_Record Interface_Record = new HM.Model.Qm.Tb_Interface_Record();
                        Interface_Record.Type = "EquipmentTask";
                        Interface_Record.GetDate = DateTime.Now;
                        Interface_Record.ItemCode = Row["CommID"].ToString().Trim();
                        Interface_Record.UserCode = Row["UserCode"].ToString().Trim();
                        for (int i = 0; i < dtTask.Rows.Count; i++)
                        {
                            if (CheckUp == false)
                                strTaskEquipmentSql = " SELECT * FROM View_EQ_PatrolEquipmentList_And_Line_PollingPlan where TaskId = '" + dtTask.Rows[i]["TaskId"].ToString().Trim() + "' and ISNULL(IsDelete,'')='' and ISNULL(LineIsDelete,'')= '' ";
                            else
                                strTaskEquipmentSql = " SELECT * FROM TB_EQ_Task_" + Row["CommID"].ToString().Trim() + "_Line_Phone where TaskId = '" + dtTask.Rows[i]["TaskId"].ToString().Trim() + "' and ISNULL(IsDelete,'')='' and ISNULL(LineIsDelete,'')= '' ";

                            var con = new SqlConnection(PubConstant.hmWyglConnectionString);
                            //string strSqlTaskEquipment = " SELECT TaskEqId,TaskId,EquiId,PollingNote,PollingResult,IsMend,BSBH,IsHandle,HandlePId,IsDelete,AddDate,EquiIdName,CommID,HandlePIdName FROM View_EQ_PatrolEquipmentList WHERE TaskId = '" + dtTask.Rows[i]["TaskId"].ToString().Trim() + "' ";
                            DataTable dtTaskEquipmentAll = con.ExecuteReader(strTaskEquipmentSql, null, null, null, CommandType.Text).ToDataSet().Tables[0];

                            dtTaskEquipmentAll.Columns.Remove("IsDelete");
                            dtTaskEquipmentAll.Columns.Remove("LineIsDelete");
                            //DataTable dtTaskEquipment = Query(strSqlTaskEquipment).Tables[0];
                            DataTable dtTaskEquipment = dtTaskEquipmentAll.Copy();
                            #region 移除列
                            dtTaskEquipment.Columns.Remove("DetailId");
                            dtTaskEquipment.Columns.Remove("TaskLineId");
                            dtTaskEquipment.Columns.Remove("StanId");
                            dtTaskEquipment.Columns.Remove("PollingNote");
                            dtTaskEquipment.Columns.Remove("ReferenceValue");
                            //dtTaskEquipment.Columns.Remove("EquiId");
                            dtTaskEquipment.Columns.Remove("InputType");
                            dtTaskEquipment.Columns.Remove("CheckType");
                            dtTaskEquipment.Columns.Remove("NumType");
                            dtTaskEquipment.Columns.Remove("InputTypeIsControl");
                            dtTaskEquipment.Columns.Remove("IsControl");
                            dtTaskEquipment.Columns.Remove("Value");
                            dtTaskEquipment.Columns.Add("LineList", Type.GetType("System.String"));
                            #endregion
                            DataView dv = new DataView(dtTaskEquipment);
                            DataTable newdtTaskEquipmentBy = dv.ToTable(true, "TaskId", "EquiId");
                            DataTable newDtTaskEquipment = dtTaskEquipment.Clone();
                            newDtTaskEquipment.Clear();
                            DataRow rowsNew = newDtTaskEquipment.NewRow();
                            DataRow dtTaskEquipmentRowsNew = dtTaskEquipment.NewRow();
                            for (int l = 0; l < newdtTaskEquipmentBy.Rows.Count; l++)
                            {

                                dtTaskEquipmentRowsNew = dtTaskEquipment.Select("TaskId = '" + newdtTaskEquipmentBy.Rows[l]["TaskId"].ToString() + "' AND EquiId = '" + newdtTaskEquipmentBy.Rows[l]["EquiId"].ToString() + "' ")[0];
                                //rowsNew.ItemArray = dtTaskEquipmentRowsNew.ItemArray;
                                newDtTaskEquipment.ImportRow(dtTaskEquipmentRowsNew);
                                //newDtTaskEquipment.Rows.Add(rowsNew);
                                // rowsNew = newDtTaskEquipment.NewRow();
                            }
                            //dtTaskEquipment = dv.ToTable(true);
                            // dtTaskEquipment = dv.ToTable(true, "TaskId,EquiId");
                            //遍历获取该任务下设备的工艺路线
                            DataTable dtTaskEquipmentLine = new DataTable();
                            #region 移除列
                            dtTaskEquipmentAll.Columns.Remove("TaskEqId");
                            dtTaskEquipmentAll.Columns.Remove("PollingResult");
                            dtTaskEquipmentAll.Columns.Remove("PollingNote");
                            dtTaskEquipmentAll.Columns.Remove("IsMend");
                            dtTaskEquipmentAll.Columns.Remove("IsHandle");
                            dtTaskEquipmentAll.Columns.Remove("BSBH");
                            dtTaskEquipmentAll.Columns.Remove("HandlePId");
                            dtTaskEquipmentAll.Columns.Remove("HandlePIdName");
                            dtTaskEquipmentAll.Columns.Remove("CommID");
                            dtTaskEquipmentAll.Columns.Remove("EquiIdName");
                            dtTaskEquipmentAll.Columns.Remove("AddDate");
                            //TaskEqId, TaskId, EquiId,PollingNote,PollingResult, IsMend,BSBH,IsHandle,HandlePId,IsDelete
                            //    ,AddDate,EquipmentName as EquiIdName ,CommID,UserName AS HandlePIdName
                            #endregion
                            if (dtTaskEquipmentAll.Columns.Contains("LinePollingNote"))
                            {
                                dtTaskEquipmentAll.Columns["LinePollingNote"].ColumnName = "PollingNote";
                            }
                            for (int j = 0; j < newDtTaskEquipment.Rows.Count; j++)
                            {
                                dtTaskEquipmentLineRows = dtTaskEquipmentAll.Select("TaskId = '" + newDtTaskEquipment.Rows[j]["TaskId"].ToString() + "' AND EquiId = '" + newDtTaskEquipment.Rows[j]["EquiId"].ToString() + "' ");
                                newDtTaskEquipment.Rows[j]["LineList"] = JSONHelper.FromString(dtTaskEquipmentLineRows.CopyToDataTable(), true, false);
                            }
                            if (newDtTaskEquipment.Columns.Contains("LinePollingNote"))
                            {
                                newDtTaskEquipment.Columns["LinePollingNote"].ColumnName = "PollingNote";
                            }
                            //dtTaskEquipment.Columns["LinePollingNote"].ColumnName = "PollingNote";
                            dtTask.Rows[i]["EquipentList"] = JSONHelper.FromString(newDtTaskEquipment, true, false);
                            con = new SqlConnection(PubConstant.hmWyglConnectionString);
                            Interface_Record.Id = Guid.NewGuid().ToString();
                            Interface_Record.TaskId = dtTask.Rows[i]["TaskId"].ToString().Trim();
                            //任务记录
                            con.Insert<HM.Model.Qm.Tb_Interface_Record>(Interface_Record);
                        }
                        result = JSONHelper.FromString(dtTask);
                    }
                }
                else
                {
                    result = JSONHelper.FromString(false, "缺少参数CommID/UserCode/Timer");
                }
            }
            catch (Exception EX)
            {
                result = JSONHelper.FromString(false, EX.ToString());
            }

            return result;
        }
        #endregion

        #region 维保任务列表
        public string GetMaintenanceTaskList(DataRow Row)
        {
            string result = "";
            try
            {
                //ItemCode 项目编号，用户编码Usercode， Timer任务执行时间  yyyy-MM-dd
                if (Row.Table.Columns.Contains("CommID") && Row.Table.Columns.Contains("UserCode") && Row.Table.Columns.Contains("Timer")
                    && Row["CommID"].ToString() != "" && Row["UserCode"].ToString() != "" && Row["Timer"].ToString() != "")
                {
                    string Timer = Convert.ToDateTime(Row["Timer"]).ToString("yyyy-MM-dd");

                    using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                    {
                        // 判断是否是金辉系统,需要进行doMonth判断
                        bool isJinHui = false;
                        var sql = @"select count(1) from syscolumns where id=object_id('Tb_EQ_WbPollingPlan') and name='doMonth'";
                        if (conn.Query<int>(sql).FirstOrDefault() > 0)
                        {
                            isJinHui = true;
                        }
                        StringBuilder strSqlTask = new StringBuilder();
                        bool CheckUp = false;
                        //DynamicParameters parameters = new DynamicParameters();
                        //parameters.Add("@CommID", Row["CommID"].ToString().Trim());
                        //parameters.Add("@Type", "Maintenance");
                        //DataTable dTable = con.ExecuteReader("Proc_EqTask_PhoneCheckUp", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
                        //if (dTable.Rows.Count > 0)
                        {
                            //if (dTable.Rows[0][0].ToString().Trim() == "TRUE")
                            //{
                            //    CheckUp = true;
                            //    strSqlTask.Append(" SELECT TaskNO, TaskId,PlanId,EqId,SpaceId,Content,CONVERT(NVARCHAR(50),BeginTime,121) as BeginTime,CONVERT(NVARCHAR(50),EndTime,121) as EndTime,RoleCode,Statue,ClosePerson,CONVERT(NVARCHAR(50),CloseTime,121) as CloseTime,CloseReason,CONVERT(NVARCHAR(50),PollingDate,121) as PollingDate,Remark,IsDelete,AddPId,PollingPerson,CONVERT(NVARCHAR(50),AddDate,121) as AddDate,CommId,CommName,PlanName,RankName,MacRoName,RoleName,ClosePersonName,PollingPersonName,CloseReasonName,CheckRoleCode,IsEntrust,EntrustCompany,CheckRoleName,MacRoAddress FROM TB_EQ_WbTask_" + Row["CommID"].ToString().Trim() + "_Phone WHERE ISNULL(ISDELETE,0)=0 AND CommID='" + Row["CommID"].ToString().Trim() + "' AND RoleCode IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + Row["UserCode"].ToString().Trim() + "'))  ");

                            //}
                            //else
                            {
                                //CheckUp = false;
                                strSqlTask.Append(@"SELECT TaskNO, TaskId,PlanId,EqId,SpaceId,Content,
                            CONVERT(NVARCHAR(50),BeginTime,121) as BeginTime,CONVERT(NVARCHAR(50),EndTime,121) as EndTime,
                            RoleCode,Statue,ClosePerson,CONVERT(NVARCHAR(50),CloseTime,121) as CloseTime,CloseReason,
                            CONVERT(NVARCHAR(50),PollingDate,121) as PollingDate,Remark,IsDelete,AddPId,PollingPerson,
                            CONVERT(NVARCHAR(50),AddDate,121) as AddDate,CommId,CommName,PlanName,RankName,MacRoName,RoleName,
                            ClosePersonName,PollingPersonName,CloseReasonName,CheckRoleCode,IsEntrust,EntrustCompany,
                            CheckRoleName,MacRoAddress 
                            FROM View_EQ_WbPatrolTaskExec_Phone_List 
                            WHERE ISNULL(ISDELETE,0)=0 AND CommID='" + Row["CommID"].ToString().Trim() + "' AND RoleCode IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + Row["UserCode"].ToString().Trim() + "'))  ");

                            }
                        }
                        strSqlTask.Append(" AND Statue != '已完成' AND Statue != '已关闭' ");
                        strSqlTask.Append(" AND CONVERT(nvarchar(100), BeginTime, 23) <='" + Timer + "' AND CONVERT(nvarchar(100), EndTime, 23) >= '" + Timer + "'");
                        strSqlTask.Append(" AND  EndTime >= '" + Timer + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "'");
                        strSqlTask.Append(" AND ISNULL(CheckMan,'') = '' AND  ISNULL(CheckNoto,'') = '' AND  ISNULL(CheckRusult,'') = ''");
                        //任务列表
                        //DataTable dtTask = Query(strSqlTask).Tables[0];
                        if (isJinHui && Global_Var.CorpId == "1329")
                        {
                            strSqlTask.Append(" AND datediff(m, BeginTime, EndTime)<=1 UNION " + strSqlTask.ToString());
                            int month = DateTime.Now.Month;
                            string doMonthSql = string.Format(" AND (doMonth IS NULL OR doMonth LIKE '%{0}%' OR LTRIM(RTRIM(doMonth))='')", month);
                            strSqlTask.Append(doMonthSql);
                            strSqlTask.Append(" AND datediff(m, BeginTime, EndTime)>1");
                        }
                        int PageCount = 0, Counts = 0;
                        int PageIndex = 1;
                        int PageSize = 100;
                        if (Row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(Row["PageIndex"].ToString()) > 0)
                        {
                            PageIndex = AppGlobal.StrToInt(Row["PageIndex"].ToString());
                        }
                        if (Row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(Row["PageSize"].ToString()) > 0)
                        {
                            PageSize = AppGlobal.StrToInt(Row["PageSize"].ToString());
                        }
                        DataTable dtTask = GetList(out PageCount, out Counts, strSqlTask.ToString(), PageIndex, PageSize, "TaskId ", 1, "TaskId", PubConstant.hmWyglConnectionString).Tables[0];
                        dtTask.Columns.Add("EquipentList", Type.GetType("System.String"));

                        //获取任务设备列表　 
                        string strWbTaskEquipmentSql = "";
                        DataRow[] dtTaskEquipmentLineRows;
                        HM.Model.Qm.Tb_Interface_Record Interface_Record = new HM.Model.Qm.Tb_Interface_Record();
                        Interface_Record.Type = "WbEquipmentTask";
                        Interface_Record.GetDate = DateTime.Now;
                        Interface_Record.ItemCode = Row["CommID"].ToString().Trim();
                        Interface_Record.UserCode = Row["UserCode"].ToString().Trim();
                        for (int i = 0; i < dtTask.Rows.Count; i++)
                        {
                            #region
                            if (CheckUp == false)
                                strWbTaskEquipmentSql = " SELECT * FROM View_EQ_WbPatrolEquipmentList_And_Line_PollingPlan where TaskId = '" + dtTask.Rows[i]["TaskId"].ToString().Trim() + "' and ISNULL(IsDelete,'')='' and ISNULL(LineIsDelete,'')= '' ";
                            else
                                strWbTaskEquipmentSql = " SELECT * FROM TB_EQ_WbTask_" + Row["CommID"].ToString().Trim() + "_Line_Phone where TaskId = '" + dtTask.Rows[i]["TaskId"].ToString().Trim() + "' and ISNULL(IsDelete,'')='' and ISNULL(LineIsDelete,'')= '' ";

                            var con = new SqlConnection(PubConstant.hmWyglConnectionString);
                            //string strSqlTaskEquipment = " SELECT TaskEqId,TaskId,EquiId,PollingNote,PollingResult,IsMend,BSBH,IsHandle,HandlePId,IsDelete,AddDate,EquiIdName,CommID,HandlePIdName FROM View_EQ_PatrolEquipmentList WHERE TaskId = '" + dtTask.Rows[i]["TaskId"].ToString().Trim() + "' ";
                            DataTable dtTaskEquipmentAll = con.ExecuteReader(strWbTaskEquipmentSql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
                            dtTaskEquipmentAll.Columns.Remove("IsDelete");
                            dtTaskEquipmentAll.Columns.Remove("LineIsDelete");
                            //DataTable dtTaskEquipment = Query(strSqlTaskEquipment).Tables[0];
                            DataTable dtTaskEquipment = dtTaskEquipmentAll.Copy();
                            #region 移除列
                            dtTaskEquipment.Columns.Remove("DetailId");
                            dtTaskEquipment.Columns.Remove("TaskLineId");
                            dtTaskEquipment.Columns.Remove("StanId");
                            dtTaskEquipment.Columns.Remove("PollingNote");
                            dtTaskEquipment.Columns.Remove("ReferenceValue");
                            //dtTaskEquipment.Columns.Remove("EquiId");
                            dtTaskEquipment.Columns.Remove("InputType");
                            dtTaskEquipment.Columns.Remove("CheckType");
                            dtTaskEquipment.Columns.Remove("NumType");
                            dtTaskEquipment.Columns.Remove("InputTypeIsControl");
                            dtTaskEquipment.Columns.Remove("IsControl");
                            dtTaskEquipment.Columns.Remove("Value");
                            dtTaskEquipment.Columns.Add("LineList", Type.GetType("System.String"));
                            #endregion
                            //DataView dv = new DataView(dtTaskEquipment);
                            //dtTaskEquipment = dv.ToTable(true);
                            ////遍历获取该任务下设备的工艺路线
                            //DataTable dtTaskEquipmentLine = new DataTable();
                            DataView dv = new DataView(dtTaskEquipment);
                            DataTable newdtTaskEquipmentBy = dv.ToTable(true, "TaskId", "EquiId");
                            DataTable newDtTaskEquipment = dtTaskEquipment.Clone();
                            newDtTaskEquipment.Clear();
                            DataRow dtTaskEquipmentRowsNew = dtTaskEquipment.NewRow();
                            DataRow rowsNew = newDtTaskEquipment.NewRow();
                            for (int l = 0; l < newdtTaskEquipmentBy.Rows.Count; l++)
                            {
                                dtTaskEquipmentRowsNew = dtTaskEquipment.Select("TaskId = '" + newdtTaskEquipmentBy.Rows[l]["TaskId"].ToString() + "' AND EquiId = '" + newdtTaskEquipmentBy.Rows[l]["EquiId"].ToString() + "' ")[0];
                                //rowsNew.ItemArray = dtTaskEquipmentRowsNew.ItemArray;
                                newDtTaskEquipment.ImportRow(dtTaskEquipmentRowsNew);
                                //newDtTaskEquipment.Rows.Add(rowsNew);
                            }
                            #region 移除列
                            dtTaskEquipmentAll.Columns.Remove("TaskEqId");
                            dtTaskEquipmentAll.Columns.Remove("PollingResult");
                            dtTaskEquipmentAll.Columns.Remove("PollingNote");
                            dtTaskEquipmentAll.Columns.Remove("IsMend");
                            dtTaskEquipmentAll.Columns.Remove("IsHandle");
                            dtTaskEquipmentAll.Columns.Remove("BSBH");
                            dtTaskEquipmentAll.Columns.Remove("HandlePId");
                            dtTaskEquipmentAll.Columns.Remove("HandlePIdName");
                            dtTaskEquipmentAll.Columns.Remove("CommID");
                            dtTaskEquipmentAll.Columns.Remove("EquiIdName");
                            dtTaskEquipmentAll.Columns.Remove("AddDate");
                            //TaskEqId, TaskId, EquiId,PollingNote,PollingResult, IsMend,BSBH,IsHandle,HandlePId,IsDelete
                            //    ,AddDate,EquipmentName as EquiIdName ,CommID,UserName AS HandlePIdName
                            #endregion
                            if (dtTaskEquipmentAll.Columns.Contains("LinePollingNote"))
                            {
                                dtTaskEquipmentAll.Columns["LinePollingNote"].ColumnName = "PollingNote";
                            }
                            for (int j = 0; j < newDtTaskEquipment.Rows.Count; j++)
                            {
                                dtTaskEquipmentLineRows = dtTaskEquipmentAll.Select("TaskId = '" + newDtTaskEquipment.Rows[j]["TaskId"].ToString() + "' AND EquiId = '" + newDtTaskEquipment.Rows[j]["EquiId"].ToString() + "' ");
                                newDtTaskEquipment.Rows[j]["LineList"] = JSONHelper.FromString(dtTaskEquipmentLineRows.CopyToDataTable(), true, false);
                            }
                            if (newDtTaskEquipment.Columns.Contains("LinePollingNote"))
                            {
                                newDtTaskEquipment.Columns["LinePollingNote"].ColumnName = "PollingNote";
                            }
                            dtTask.Rows[i]["EquipentList"] = JSONHelper.FromString(newDtTaskEquipment, true, false);
                            con = new SqlConnection(PubConstant.hmWyglConnectionString);
                            Interface_Record.Id = Guid.NewGuid().ToString();
                            Interface_Record.TaskId = dtTask.Rows[i]["TaskId"].ToString().Trim();
                            //任务记录
                            con.Insert<HM.Model.Qm.Tb_Interface_Record>(Interface_Record);
                            #endregion
                        }
                        result = JSONHelper.FromString(dtTask);
                    }
                }
                else
                {
                    result = JSONHelper.FromString(false, "缺少参数CommID/UserCode/Timer");

                }
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, EX.ToString());
            }

            return result;
        }
        #endregion

        #region 设备维保任务上传
        public string SubmitMaintenanceTaskListNew(DataRow Row)
        {
            string Result = JSONHelper.FromString(false, "未知错误");
            if (Row["Data"] != null && Row["Data"].ToString().Trim() != "")
            {
                //string jsonData = "[{\"TaskId\": \"a1b74fb7-2988-4361-ae1d-ec2d50f0302c\",\"PollingPerson\": \"000096\",\"PollingDate\": \"2016-05-01\",\"EquipentList\": [{\"TaskEquiId\": \"1a49d029-efe5-499c-8fa0-a52a2392948d\",\"PollingNote\": \"完成1\",\"PollingResult\": \"正常\",\"LineList\": [{\"TaskLineId\": \"2f0c3ab7-b774-4805-9143-a9e0aa10cd9e\",\"Value\": \"完成1\"}]}]}]";
                try
                {
                    ArrayList rows = (ArrayList)JSON.Decode(Row["Data"].ToString().Trim());
                    IDbConnection Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                    bool CheckBool = false;
                    DynamicParameters parametersIng = new DynamicParameters();
                    parametersIng.Add("@CommID", "CheckAll");
                    parametersIng.Add("@Type", "Maintenance");
                    DataTable dTable = Conn.ExecuteReader("Proc_EqTask_PhoneCheckUp", parametersIng, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
                    if (dTable.Rows.Count > 0)
                    {
                        if (dTable.Rows[0][0].ToString().Trim() == "TRUE")
                        {
                            CheckBool = true;
                        }
                    }
                    foreach (Hashtable row in rows)
                    {
                        string UpdateSQLTask = "";
                        var doTime = default(string);
                        var pollingDate = default(string); 

                        try
                        {
                            if (row.Contains("doTime") && !string.IsNullOrEmpty(row["doTime"].ToString()))
                            {
                                doTime = row["doTime"].ToString();
                            }
                            else
                            {
                                doTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.000");
                            }


                            if (row.Contains("PollingDate") && !string.IsNullOrEmpty(row["PollingDate"].ToString()))
                            {
                                pollingDate = row["PollingDate"].ToString();
                            }
                            else
                            {
                                pollingDate = DateTime.Now.AddHours(1).ToString("yyyy-MM-dd HH:mm:ss.000");
                            }


                            UpdateSQLTask = "Update Tb_EQ_WbTask set PollingPerson='" + row["PollingPerson"] + "',PollingDate='" + pollingDate + "',CheckNoto='" + row["CheckNoto"] + "',CheckRusult='" + row["CheckRusult"] + "',Statue = '已完成',doTime='" + doTime + "' where TaskId='" + row["TaskId"] + "'";

                            new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).ExecuteSql(UpdateSQLTask);
                        }
                        catch (Exception)
                        {
                            UpdateSQLTask = "Update Tb_EQ_WbTask set PollingPerson='" + row["PollingPerson"] + "',PollingDate='" + row["PollingDate"] + "',CheckNoto='" + row["CheckNoto"] + "',CheckRusult='" + row["CheckRusult"] + "',Statue = '已完成' where TaskId='" + row["TaskId"] + "'";
                            new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).ExecuteSql(UpdateSQLTask);
                        }

                        if (row["EquipentList"] != null)
                        {
                            ArrayList EquipentRows = (ArrayList)row["EquipentList"];
                            string UpdateSQLEquipent = "", strUpdate = "";
                            //更新任务设备信息
                            foreach (Hashtable Equipent in EquipentRows)
                            {
                                if (Equipent.Contains("bsbh"))
                                    strUpdate += ",BSBH='" + Equipent["bsbh"] + "',isMend='" + Equipent["isMend"] + "'";

                                //执行更新任务设备语句
                                UpdateSQLEquipent = "Update Tb_EQ_WbTaskEquipment set PollingNote='" + Equipent["PollingNote"] + "',PollingResult='" + Equipent["PollingResult"] + "'" + strUpdate + " where TaskEqId='" + Equipent["TaskEquiId"] + "'";
                                //执行更新操作
                                //string SqlStringTaskEquipent = "SELECT * FROM Tb_EQ_TaskEquipment where TaskEqId = '" + Equipent["TaskEquiId"] + "'";
                                //Update( SqlStringTaskEquipent, UpdateSQLEquipent);
                                int equipentCount = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).ExecuteSql(UpdateSQLEquipent);
                                if (equipentCount <= 0)
                                {
                                    Result = JSONHelper.FromString(false, "无设备任务");
                                    return Result;
                                }
                                //更新任务设备状态信息
                                ArrayList EquipmentState = (ArrayList)Equipent["EquipmentState"];
                                foreach (Hashtable Equipment in EquipmentState)
                                {
                                    if (Equipment["BeginTime"].ToString().Trim() != "")
                                    {
                                        HM.Model.Eq.Tb_Eq_EquipmentStatus M_EquipmentStatus = new HM.Model.Eq.Tb_Eq_EquipmentStatus();
                                        M_EquipmentStatus.AddTime = Convert.ToDateTime(Equipment["AddTime"]);
                                        M_EquipmentStatus.AddPid = Equipment["AddPid"].ToString();
                                        M_EquipmentStatus.EquipmentId = Equipment["EquipmentId"].ToString();
                                        M_EquipmentStatus.Remark = Equipment["Remark"].ToString();
                                        M_EquipmentStatus.EquipmentStatus = Equipment["EquipmentStatus"].ToString();
                                        M_EquipmentStatus.BeginTime = Convert.ToDateTime(Equipment["BeginTime"]);
                                        M_EquipmentStatus.EndTime = Convert.ToDateTime("2099-12-31 23:59:59");
                                        M_EquipmentStatus.Id = Guid.NewGuid().ToString();
                                        new HM.BLL.Eq.Bll_Tb_Eq_EquipmentStatus().Add(M_EquipmentStatus);
                                    }
                                }
                                if (Equipent["LineList"] != null)
                                {
                                    int boolPaging = 0;
                                    //更新任务设备工艺路线信息
                                    ArrayList EquipentLineRows = (ArrayList)Equipent["LineList"];
                                    foreach (Hashtable EquipentLine in EquipentLineRows)
                                    {
                                        //查询
                                        string sqlEquipentLine = @"SELECT * FROM dbo.Tb_EQ_WbTaskEquipmentLine A 
                                                            LEFT OUTER JOIN dbo.Tb_EQ_WbPollingPlanDetail B ON A.DetailId = B.DetailId
                                                            WHERE TaskLineId = '" + EquipentLine["TaskLineId"] + "'";
                                        DataTable dtEquipentLine = Query(sqlEquipentLine).Tables[0];
                                        for (int i = 0; i < dtEquipentLine.Rows.Count; i++)
                                        {
                                            string UpdateSQLEquipentLine = "";
                                            boolPaging = 0;
                                            //string SqlStringTaskEquipentLine = "SELECT * FROM Tb_EQ_TaskEquipmentLine where TaskLineId='" + EquipentLine["TaskLineId"] + "'";
                                            //1选项   2数字   3文本
                                            if (dtEquipentLine.Rows[i]["InputType"].ToString() == "1")
                                            {
                                                if (EquipentLine["Value"].ToString().Trim() == "")
                                                    boolPaging = 1;
                                                //执行更新任务设备工艺路线语句
                                                UpdateSQLEquipentLine = "Update Tb_EQ_WbTaskEquipmentLine set ChooseValue='" + EquipentLine["Value"] + "' where TaskLineId='" + EquipentLine["TaskLineId"] + "'";
                                            }
                                            if (dtEquipentLine.Rows[i]["InputType"].ToString() == "2")
                                            {
                                                if (EquipentLine["Value"].ToString().Trim() == "")
                                                    boolPaging = 1;
                                                //执行更新任务设备工艺路线语句
                                                UpdateSQLEquipentLine = "Update Tb_EQ_WbTaskEquipmentLine set NumValue=" + EquipentLine["Value"] + " where TaskLineId='" + EquipentLine["TaskLineId"] + "'";
                                            }
                                            if (dtEquipentLine.Rows[i]["InputType"].ToString() == "3")
                                            {
                                                //执行更新任务设备工艺路线语句
                                                UpdateSQLEquipentLine = "Update Tb_EQ_WbTaskEquipmentLine set TextValue='" + EquipentLine["Value"] + "' where TaskLineId='" + EquipentLine["TaskLineId"] + "'";
                                            }
                                            if (boolPaging < 1)
                                            {
                                                //执行
                                                //Update(SqlStringTaskEquipentLine, UpdateSQLEquipentLine);
                                                int equipentLineCount = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).ExecuteSql(UpdateSQLEquipentLine);
                                                if (equipentLineCount <= 0)
                                                {
                                                    Result = JSONHelper.FromString(false, "无设备工艺路线信息");
                                                    return Result;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Result = JSONHelper.FromString(false, "无设备工艺路线信息");
                                    return Result;
                                }
                            }
                        }
                        else
                        {
                            Result = JSONHelper.FromString(false, "无维保设备");
                            return Result;
                        }
                        if (CheckBool == true)
                        {
                            Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                            DynamicParameters parameters = new DynamicParameters();
                            parameters.Add("@Type", "Wb");
                            parameters.Add("@TaskId", row["TaskId"].ToString());
                            Conn.ExecuteReader("Proc_Eq_Submit_Phone", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
                            Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                        }
                    }
                }
                catch (Exception e)
                {
                    Result = JSONHelper.FromString(false, "异常信息:" + e.Message);
                }
            }
            else
            {
                Result = JSONHelper.FromString(false, "无数据");
                return Result;
            }
            return JSONHelper.FromString(true, "上传完成");
        }
        public string SubmitMaintenanceTaskList(DataRow Row)
        {
            string Result = JSONHelper.FromString(false, "未知错误");
            if (Row["Data"] != null && Row["Data"].ToString().Trim() != "")
            {
                //string jsonData = "[{\"TaskId\": \"a1b74fb7-2988-4361-ae1d-ec2d50f0302c\",\"PollingPerson\": \"000096\",\"PollingDate\": \"2016-05-01\",\"EquipentList\": [{\"TaskEquiId\": \"1a49d029-efe5-499c-8fa0-a52a2392948d\",\"PollingNote\": \"完成1\",\"PollingResult\": \"正常\",\"LineList\": [{\"TaskLineId\": \"2f0c3ab7-b774-4805-9143-a9e0aa10cd9e\",\"Value\": \"完成1\"}]}]}]";
                try
                {

                    ArrayList rows = (ArrayList)JSON.Decode(Row["Data"].ToString().Trim());
                    IDbConnection Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                    bool CheckBool = false;
                    DynamicParameters parametersIng = new DynamicParameters();
                    parametersIng.Add("@CommID", "CheckAll");
                    parametersIng.Add("@Type", "Maintenance");
                    DataTable dTable = Conn.ExecuteReader("Proc_EqTask_PhoneCheckUp", parametersIng, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
                    if (dTable.Rows.Count > 0)
                    {
                        if (dTable.Rows[0][0].ToString().Trim() == "TRUE")
                        {
                            CheckBool = true;
                        }
                    }

                    using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                    {
                        foreach (Hashtable row in rows)
                        {
                            var doTime = default(string);
                            var PollingDate = default(string);
                            string UpdateSQLTask = "";

                            try
                            {
                                if (row.Contains("doTime") && !string.IsNullOrEmpty(row["doTime"].ToString()))
                                {
                                    doTime = row["doTime"].ToString();
                                }
                                else
                                {
                                    doTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.000");
                                }
                                if (row.Contains("PollingDate") && !string.IsNullOrEmpty(row["PollingDate"].ToString()))
                                {
                                    PollingDate = row["PollingDate"].ToString();
                                }
                                else
                                {
                                    PollingDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.000");
                                }

                                if (DateTime.Compare(Convert.ToDateTime(doTime), Convert.ToDateTime(PollingDate)) > 0)
                                {
                                    doTime = Convert.ToDateTime(doTime).AddHours(-1).ToString();
                                }

                                UpdateSQLTask = "Update Tb_EQ_WbTask set PollingPerson='" + row["PollingPerson"] + "',PollingDate='" + PollingDate + "',CheckNoto='" + row["CheckNoto"] + "',CheckRusult='" + row["CheckRusult"] + "',Statue = '已完成',doTime='" + doTime + "' where TaskId='" + row["TaskId"] + "'";

                                new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).ExecuteSql(UpdateSQLTask);
                            }
                            catch (Exception)
                            {
                                UpdateSQLTask = "Update Tb_EQ_WbTask set PollingPerson='" + row["PollingPerson"] + "',PollingDate='" + PollingDate + "',CheckNoto='" + row["CheckNoto"] + "',CheckRusult='" + row["CheckRusult"] + "',Statue = '已完成' where TaskId='" + row["TaskId"] + "'";
                                new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).ExecuteSql(UpdateSQLTask);
                            }

                            if (row["EquipentList"] != null)
                            {
                                ArrayList EquipentRows = (ArrayList)row["EquipentList"];
                                string UpdateSQLEquipent = "", strUpdate = "";
                                //更新任务设备信息
                                foreach (Hashtable Equipent in EquipentRows)
                                {
                                    // 如果上传了设备状态
                                    if (Equipent.Contains("EquipmentStatus"))
                                    {
                                        string updateEqStatue = $@"
                                        DECLARE @IID NVARCHAR(36);
                                        DECLARE @EquipmentStatus NVARCHAR(10);
                                        IF exists(SELECT TOP 1 @IID=IID,@EquipmentStatus=EquipmentStatus 
                                                    FROM Tb_Eq_EquipmentStatus
                                                    WHERE ISNULL(IsDelete,0)=0 AND EquipmentId=@EquipmentId ORDER BY BeginTime DESC)
                                          BEGIN
                                            IF @EquipmentStatus IS NOT NULL AND @CurrentEquipmentStatus<>@EquipmentStatus
                                              BEGIN
                                                UPDATE Tb_Eq_EquipmentStatus SET EndTime= @BeginTime WHERE IID=@IID;

                                                INSERT INTO Tb_Eq_EquipmentStatus(Id,BeginTime,EndTime,EquipmentStatus,
                                                                                    EquipmentId,AddPid,AddTime)
                                                VALUES(newid(),@BeginTime,'2099-12-31 23:59:59.000',@CurrentEquipmentStatus,
                                                        @EquipmentId,@AddPid,getdate());
                                              END
                                          END
                                        ELSE
                                          BEGIN
                                            INSERT INTO Tb_Eq_EquipmentStatus(Id,BeginTime,EndTime,EquipmentStatus,
                                                                                EquipmentId,AddPid,AddTime)
                                                VALUES(newid(),@BeginTime,'2099-12-31 23:59:59.000',@CurrentEquipmentStatus,
                                                        @EquipmentId,@AddPid,getdate());
                                          END
                                        ";

                                        conn.Execute(updateEqStatue, new
                                        {
                                            EquiId = Equipent["EquiId"],
                                            CurrentEquipmentStatus = Equipent["EquipmentStatus"],
                                            BeginTime = doTime,
                                            AddPid = Global_Var.LoginUserCode
                                        });
                                    }

                                    if (Equipent.Contains("bsbh") || Equipent.Contains("BSBH"))
                                    {
                                        string isMend = "否";
                                        if (Equipent.Contains("isMend") && Equipent["isMend"].ToString().Trim() == "1")
                                        {
                                            isMend = "是";
                                        }

                                        if (Equipent.Contains("IsMend") && Equipent["IsMend"].ToString().Trim() == "1")
                                        {
                                            isMend = "是";
                                        }

                                        string bsbh = null;
                                        if (isMend == "是")
                                        {
                                            if (Equipent.Contains("bsbh"))
                                            {
                                                bsbh = Equipent["bsbh"].ToString().Trim();
                                            }
                                            if (Equipent.Contains("BSBH"))
                                            {
                                                bsbh = Equipent["BSBH"].ToString().Trim();
                                            }
                                        }

                                        UpdateSQLEquipent += ",BSBH='" + bsbh + "',IsMend='" + isMend + "' ";
                                    }
                                    //执行更新任务设备语句
                                    UpdateSQLEquipent = "Update Tb_EQ_WbTaskEquipment set PollingNote='" + Equipent["PollingNote"] + "',PollingResult='" + Equipent["PollingResult"] + "'" + strUpdate + " where TaskEqId='" + Equipent["TaskEquiId"] + "'";
                                    //执行更新操作
                                    //string SqlStringTaskEquipent = "SELECT * FROM Tb_EQ_TaskEquipment where TaskEqId = '" + Equipent["TaskEquiId"] + "'";
                                    //Update( SqlStringTaskEquipent, UpdateSQLEquipent);
                                    int equipentCount = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).ExecuteSql(UpdateSQLEquipent);
                                    if (equipentCount <= 0)
                                    {
                                        Result = JSONHelper.FromString(false, "无设备任务");
                                        return Result;
                                    }
                                    if (Equipent["LineList"] != null)
                                    {
                                        int boolPaging = 0;
                                        //更新任务设备工艺路线信息
                                        ArrayList EquipentLineRows = (ArrayList)Equipent["LineList"];
                                        foreach (Hashtable EquipentLine in EquipentLineRows)
                                        {
                                            //查询
                                            string sqlEquipentLine = @"SELECT * FROM dbo.Tb_EQ_WbTaskEquipmentLine A 
                                                            LEFT OUTER JOIN dbo.Tb_EQ_WbPollingPlanDetail B ON A.DetailId = B.DetailId
                                                            WHERE TaskLineId = '" + EquipentLine["TaskLineId"] + "'";
                                            DataTable dtEquipentLine = Query(sqlEquipentLine).Tables[0];
                                            for (int i = 0; i < dtEquipentLine.Rows.Count; i++)
                                            {
                                                string UpdateSQLEquipentLine = "";
                                                boolPaging = 0;
                                                //string SqlStringTaskEquipentLine = "SELECT * FROM Tb_EQ_TaskEquipmentLine where TaskLineId='" + EquipentLine["TaskLineId"] + "'";
                                                //1选项   2数字   3文本
                                                if (dtEquipentLine.Rows[i]["InputType"].ToString() == "1")
                                                {
                                                    if (EquipentLine["Value"].ToString().Trim() == "")
                                                        boolPaging = 1;
                                                    //执行更新任务设备工艺路线语句
                                                    UpdateSQLEquipentLine = "Update Tb_EQ_WbTaskEquipmentLine set ChooseValue='" + EquipentLine["Value"] + "' where TaskLineId='" + EquipentLine["TaskLineId"] + "'";
                                                }
                                                if (dtEquipentLine.Rows[i]["InputType"].ToString() == "2")
                                                {
                                                    if (EquipentLine["Value"].ToString().Trim() == "")
                                                        boolPaging = 1;
                                                    //执行更新任务设备工艺路线语句
                                                    UpdateSQLEquipentLine = "Update Tb_EQ_WbTaskEquipmentLine set NumValue=" + EquipentLine["Value"] + " where TaskLineId='" + EquipentLine["TaskLineId"] + "'";
                                                }
                                                if (dtEquipentLine.Rows[i]["InputType"].ToString() == "3")
                                                {
                                                    //执行更新任务设备工艺路线语句
                                                    UpdateSQLEquipentLine = "Update Tb_EQ_WbTaskEquipmentLine set TextValue='" + EquipentLine["Value"] + "' where TaskLineId='" + EquipentLine["TaskLineId"] + "'";
                                                }
                                                if (boolPaging < 1)
                                                {
                                                    //执行
                                                    //Update(SqlStringTaskEquipentLine, UpdateSQLEquipentLine);
                                                    int equipentLineCount = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).ExecuteSql(UpdateSQLEquipentLine);
                                                    if (equipentLineCount <= 0)
                                                    {
                                                        Result = JSONHelper.FromString(false, "无设备工艺路线信息");
                                                        return Result;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Result = JSONHelper.FromString(false, "无设备工艺路线信息");
                                        return Result;
                                    }
                                }
                            }
                            else
                            {
                                Result = JSONHelper.FromString(false, "无维保设备");
                                return Result;
                            }
                            if (CheckBool == true)
                            {
                                Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                DynamicParameters parameters = new DynamicParameters();
                                parameters.Add("@Type", "Wb");
                                parameters.Add("@TaskId", row["TaskId"].ToString());
                                Conn.ExecuteReader("Proc_Eq_Submit_Phone", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
                                Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Result = JSONHelper.FromString(false, "异常信息:" + e.Message + Environment.NewLine + e.StackTrace + Environment.NewLine + e.Source);
                }
            }
            else
            {
                Result = JSONHelper.FromString(false, "无数据");
                return Result;
            }
            return JSONHelper.FromString(true, "上传完成");
        }
        #endregion

        #region 设备维保验收

        public string SubmitMaintenanceTaskAcceptance(DataRow Row)
        {
            string result = "";
            try
            {
                if (Row["Data"] != null && Row["Data"].ToString().Trim() != "")
                {
                    return JSONHelper.FromString(false, "无数据");
                }
                HM.Model.Eq.Tb_EQ_WbTask M_WbTask = new HM.Model.Eq.Tb_EQ_WbTask();
                HM.BLL.Eq.Bll_Tb_EQ_WbTask B_WbTask = new HM.BLL.Eq.Bll_Tb_EQ_WbTask();
                ArrayList rows = (ArrayList)JSON.Decode(Row["Data"].ToString().Trim());
                foreach (Hashtable item in rows)
                {
                    M_WbTask = B_WbTask.GetModel(item["TaskId"].ToString());
                    M_WbTask.PollingPerson = item["PollingPerson"].ToString();
                    M_WbTask.PollingDate = Convert.ToDateTime(item["PollingDate"].ToString());
                    M_WbTask.CheckNoto = item["CheckNoto"].ToString();
                    M_WbTask.CheckRusult = item["CheckRusult"].ToString();
                    M_WbTask.CheckTime = Convert.ToDateTime(item["CheckTime"].ToString());
                    B_WbTask.Update(M_WbTask);
                }
                result = JSONHelper.FromString(true, "上传成功");
            }
            catch (Exception e)
            {
                result = JSONHelper.FromString(false, e.Message);
            }
            return result;
        }
        #endregion

        #region 任务设备上传附件


        public void AddQuanlityFiles(string taskId, string phototime, string fix, string addpid, string fileName, string Path)
        {

            AppGlobal.GetHmWyglConnection();
            string mp3 = ",wav,amr,m4a,aac";
            string img = ",jpeg,peg,jpg,bmp";
            string mp4 = ",mp4,avi,3gp";


            HM.Model.Eq.Tb_EQ_EquipmentFile model = new HM.Model.Eq.Tb_EQ_EquipmentFile();
            HM.BLL.Eq.Bll_Tb_EQ_EquipmentFile bll = new HM.BLL.Eq.Bll_Tb_EQ_EquipmentFile();
            model.Id = Guid.NewGuid().ToString();
            if (mp3.Contains(fix.ToLower()))
                model.Fix = "mp3";
            else if (mp4.Contains(fix.ToLower()))
                model.Fix = "video";
            else
                model.Fix = "img";

            model.PhotoTime = Convert.ToDateTime(phototime);
            model.PhotoPId = addpid;
            model.EquiId = taskId;
            model.FilePath = Path + fileName;
            model.FileName = fileName;
            //IDbConnection Connectionstr = new SqlConnection(Connection.GetConnection("2"));
            //Connectionstr.Insert(model);
            bll.Add(model);
        }
        public string SubmitMaintenanceTaskFile(DataRow Row)
        {
            HM.Model.Eq.Tb_EQ_EquipmentFile M_EquipmentFile = new HM.Model.Eq.Tb_EQ_EquipmentFile();
            HM.BLL.Eq.Bll_Tb_EQ_EquipmentFile B_EquipmentFile = new HM.BLL.Eq.Bll_Tb_EQ_EquipmentFile();
            string jsonData = "[{\"TaskId\": \"a1b74fb7-2988-4361-ae1d-ec2d50f0302c\",\"PollingPerson\": \"000096\",\"PollingDate\": \"2016-05-01\",\"CheckNoto\":\"没有意见，看心情\",\"CheckRusult\":\"不合格\",\"CheckTime\":\"2016-05-05\"},{\"TaskId\": \"a1b74fb7-2988-4361-ae1d-ec2d50f0302c\",\"PollingPerson\": \"000096\",\"PollingDate\": \"2016-05-01\",\"CheckNoto\":\"没有意见，看心情\",\"CheckRusult\":\"不合格\",\"CheckTime\":\"2016-05-05\"}]";
            string subStrStart = jsonData.Substring(0, 1);
            if (subStrStart != "[")
            {
                jsonData = "[" + jsonData;
            }
            string subStrEnd = jsonData.Substring(jsonData.Length - 1, jsonData.Length);
            if (subStrEnd != "]")
            {
                jsonData = "]" + jsonData;
            }
            ArrayList rows = (ArrayList)JSON.Decode(jsonData);
            foreach (Hashtable item in rows)
            {
                M_EquipmentFile.Id = Guid.NewGuid().ToString();
                M_EquipmentFile.EquiId = item["EquiId"].ToString();
                M_EquipmentFile.FileName = item["FileName"].ToString();
                M_EquipmentFile.Fix = item["Fix"].ToString();
                M_EquipmentFile.IsDelete = 0;
                M_EquipmentFile.FilePath = item["FilePath"].ToString();
                M_EquipmentFile.PhotoTime = Convert.ToDateTime(item["Timer"].ToString());
                M_EquipmentFile.PhotoPId = item["UserCode"].ToString();
                B_EquipmentFile.Add(M_EquipmentFile);
            }
            return "";
        }
        #endregion

        #region 公共方法
        public static DataSet Query(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }
        public static bool Update(string SQLString, string UpdateSql)
        {
            using (SqlConnection connection = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQLString, connection);
                DataSet ds = new DataSet();
                da.Fill(ds, "DataTable");

                SqlCommand updateCmd = new SqlCommand(UpdateSql, connection);
                da.UpdateCommand = updateCmd;
                int count = da.Update(ds.Tables[0]);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion
    }
}