using Dapper;
using DapperExtensions;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWTools.Push;

namespace Business
{
    public class QualityManage : PubInfo
    {
        public QualityManage()
        {
            base.Token = "20160324QualityManage";
        }
        public override void Operate(ref Common.Transfer Trans)
        {
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

            //项目，责任人，时间
            try
            {
                switch (Trans.Command)
                {



                    case "SearchPoint":
                        Trans.Result = SearchPoint(Row);//需求4920
                        break;
                    #region 任务页操作
                    case "OptionTaskOver":
                        Trans.Result = OptionTaskOver(Row);//任务完成操作
                        break;
                    case "OptionTaskRegister":
                        Trans.Result = OptionTaskRegister(Row);//任务登记操作(更改任务状态)
                        break;
                    #endregion
                    //已对接
                    case "GetQualityConfig"://品质配置
                        Trans.Result = GetQualityConfig(Row);
                        break;//Equipment
                    case "GetQmAndEqAndConfigInspect"://核查任务，整改，设备维保，巡检 数量品质配置数据
                        Trans.Result = GetQualityInspectListCount(Row);
                        break;//GetQualityInspectListCount
                    case "GetQualityInspectListCount"://核查任务数量(旧)
                        Trans.Result = GetQualityInspectListCount(Row);
                        break;//GetQualityInspectListCount
                    case "GetQualityInspectList"://核查任务列表(使用)(旧)
                        Trans.Result = GetQualityInspectList(Row);
                        break;
                    //(已使用)
                    case "GetQualityInspectSave"://核查任务登记（安卓,）
                        Trans.Result = QualityInspectSave(Row);
                        break;
                    case "SaveQmTaskPoint"://核查点位上传
                        Trans.Result = SaveQmTaskPoint(Row);
                        break;
                    //已对接(使用)
                    case "GetTaskAbarbeitungList"://核查整改列表
                        Trans.Result = GetTaskAbarbeitungList(Row);
                        break;
                    //已对接(使用)
                    case "QualityIsOkSave"://品质整改登记,验收登记（使用中）
                                           //调用这个接口用于整改登记必须调用SaveQmTaskPoint上传已查点位
                        Trans.Result = QualityIsOkSave(Row);
                        break;
                    //已对接
                    case "QualityAbarbeitungSave"://品质整改登记
                        Trans.Result = QualityAbarbeitungSave(Row);
                        break;
                    //已对接
                    case "QualityOkSave"://品质验收（批量）
                        Trans.Result = QualityOkSave(Row);
                        break;
                    //已对接
                    case "GetUserByCommId"://获取责任人
                        Trans.Result = GetUserByCommId(Row);
                        break;
                    case "GetProblem"://返回问题类型列表
                        Trans.Result = GetProblem();
                        break;
                    //已对接
                    case "GetPointStandardList":
                        Trans.Result = GetPointStandardList(Row);
                        break;
                    case "GetStandardList":
                        Trans.Result = GetStandardList(Row);
                        break;
                    case "GetProfessionalList":
                        Trans.Result = GetProfessionalList(Row);
                        break;
                    case "GetTypeList":
                        Trans.Result = GetTypeList(Row);
                        break;

                    #region (新接口)
                    case "GetQmAndEqAndConfigInspectNew"://品质,设备任务数量(新)
                        Trans.Result = JSONHelper.FromString(false, "未知！错误");
                        Trans.Result = GetQualityInspectListCountNew(Row);
                        break;//GetQualityInspectListCount
                    case "GetQualityInspectListNew"://核查任务列表(使用)(新)(任务中的整改APP端没和整改列表关联)
                        Trans.Result = GetQualityInspectListNew(Row);
                        break;
                    case "GetQualityInspectSaveNew"://核查任务登记(新)
                        Trans.Result = QualityInspectSaveNew(Row);
                        break;
                    //case "GetQualityInspectListApprovedNew"://核查任务列表(新+批转)
                    //    Trans.Result = GetQualityInspectListApprovedNew(Row);
                    //    break;
                    //case "GetQualityInspectSaveApprovedNew"://核查任务登记(新+批转)
                    //    Trans.Result = QualityInspectSaveApprovedNew(Row);
                    //    break;
                    ////////APP逻辑-////////
                    case "GetTaskAbarbeitungListApprovedNew"://核查整改列表(新+批转)
                        Trans.Result = GetTaskAbarbeitungListApprovedNew(Row);
                        break;
                    case "GetTaskAbarbeitungByIdApprovedNew"://核查整改列表(新+批转)
                        Trans.Result = GetTaskAbarbeitungByIdApprovedNew(Row);
                        break;
                    case "GetTaskCount":
                        Trans.Result = GetTaskCount(Row);
                        break;
                    case "GetTaskCount_hnc":
                        Trans.Result = GetTaskCount_hnc(Row);
                        break;
                    case "GetQmAndEqAndConfigInspectApprovedNew"://核查任务，整改，设备维保，巡检 数量品质配置数据(新+批转)
                        Trans.Result = GetQualityInspectListApprovedCount(Row);
                        break;
                    case "GetQmAndEqAndConfigInspectApprovedNew_hnc"://核查任务，整改，设备维保，巡检 数量品质配置数据(新+批转)+华南城新增装修巡查
                        Trans.Result = GetQualityInspectListApprovedCount_hnc(Row);
                        break;
                    case "GetDecorateDealList"://装修巡查列表(华南城)
                        Trans.Result = GetDecorateDealList(Row);
                        break;
                    case "OnDecorateDealSave"://装修巡查登记(华南城)
                        Trans.Result = OnDecorateDealSave(Row);
                        break;
                    case "QualityIsOkSaveApprovedNew"://品质整改登记,验收登记（新+批转）
                                                      //调用这个接口用于整改登记必须调用SaveQmTaskPoint上传已查点位
                        Trans.Result = QualityIsOkSaveApprovedNew(Row);
                        break;
                    #endregion
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Trans.Result = ex.ToString();
            }
        }
        public string SaveQmTaskPoint(DataRow Row)
        {
            string result = JSONHelper.FromString(false, "错误");
            IDbConnection Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
            DataTable dtPoints = JSONHelper.JsonToDataTable(Row["Data"].ToString().Trim());
            HM.Model.Qm.Tb_Qm_TaskPoint TaskPointModel = new HM.Model.Qm.Tb_Qm_TaskPoint();
            for (int i = 0; i < dtPoints.Rows.Count; i++)
            {
                TaskPointModel = new HM.Model.Qm.Tb_Qm_TaskPoint();
                AppGlobal.FillModel(dtPoints.Rows[i], TaskPointModel);
                if (TaskPointModel.AddPId != null && TaskPointModel.AddPId != "")
                {
                    TaskPointModel.PointIds = dtPoints.Rows[i]["PointId"].ToString();
                    TaskPointModel.Id = Guid.NewGuid().ToString();
                    TaskPointModel.TaskId = dtPoints.Rows[i]["TaskId"].ToString();
                    //TaskPointBll.Add(TaskPointModel);
                    Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                    Conn.Insert<HM.Model.Qm.Tb_Qm_TaskPoint>(TaskPointModel);
                }
            }
            result = JSONHelper.FromString(false, "成功");
            return result;
        }
        /// <summary>
        /// 整改时限取值位置( 1: 标准中取值,0:任务中取值)
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string GetQualityConfig(DataRow Row)
        {
            string result = "";
            DataTable dt = Query(" SELECT isnull(TimeLimit, 0) as TimeLimit FROM Tb_Qm_TaskParameterSetting where ISNULL(IsDelete,0)=0 ").Tables[0];
            result = JSONHelper.FromString(dt);
            return result;
        }
        /// <summary>
        /// 针对基层员工，可以通过扫描点位，查看该点位对应的巡查标准，而不能对巡查任务进行修改；同时支持查询所有的巡查标准。
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string GetPointStandardList(DataRow Row)
        {
            int PageCount, Counts;
            string result = "";
            DataTable dtPointStandard = null;
            string StrSql = "";
            try
            {
                if (Row.Table.Columns.Contains("PointId") && Row["PointId"].ToString() == "")
                {
                    StrSql = "";
                }
                //ItemCode 项目编号，用户编码Usercode， Timer任务执行时间  yyyy-MM-dd
                if (Row.Table.Columns.Contains("CommID") && Row.Table.Columns.Contains("UserCode") && Row.Table.Columns.Contains("PointId") && Row.Table.Columns.Contains("Page")
                    && Row["CommID"].ToString() != "" && Row["UserCode"].ToString() != "" && Row["PointId"].ToString() != "" && Row["Rows"].ToString() != ""
                    )
                {
                    StrSql = " AND ISNULL(ISDELETE,0)=0 AND ProjectCode = '" + Row["CommID"].ToString() + "' ";
                    StrSql += " AND PointId = '" + Row["PointId"].ToString() + "' ";
                    dtPointStandard = GetList(PubConstant.hmWyglConnectionString.ToString(), out PageCount, out Counts, StrSql, Convert.ToInt32(Row["Page"]),
                        Convert.ToInt32(Row["Rows"].ToString()), "Sort", 0, "View_Tb_Qm_PointStandardList", "Id").Tables[0];
                    result = JSONHelper.FromStringPaging(dtPointStandard, true, false, PageCount, Counts);
                }
                else
                {
                    result = JSONHelper.FromString(false, "缺少参数CommID/UserCode/PointId/Page/Rows");
                }
            }
            catch (Exception EX)
            {
                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }

        /// <summary>
        /// 单独增加核查标准查询模块，支持按所属专业、所属类型、扫描点位、输入点位查询响应的核查标准
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string GetStandardList(DataRow Row)
        {
            int PageCount, Counts;
            string result = "";
            DataTable dtPointStandard = null;
            string StrSql = "";
            try
            {
                //ItemCode 项目编号，用户编码Usercode， Timer任务执行时间  yyyy-MM-dd
                if (
                        Row.Table.Columns.Contains("CommID") && Row.Table.Columns.Contains("UserCode") && Row.Table.Columns.Contains("Page") && Row.Table.Columns.Contains("Rows")
                        && Row["CommID"].ToString() != "" && Row["UserCode"].ToString() != "" && Row["Page"].ToString() != "" && Row["Rows"].ToString() != ""
                    )
                {
                    StrSql = " AND ISNULL(ISDELETE,0)=0 AND ProjectCode = '" + Row["CommID"].ToString() + "' ";

                    if (Row.Table.Columns.Contains("PointId") && Row["PointId"].ToString() != "")
                    {
                        StrSql += " AND PointId = '" + Row["PointId"].ToString() + "' ";
                    }
                    if (Row.Table.Columns.Contains("Professional") && Row["Professional"].ToString() != "")
                    {
                        StrSql += " AND ProfessionalId = '" + Row["Professional"].ToString() + "' ";
                    }
                    if (Row.Table.Columns.Contains("Type") && Row["Type"].ToString() != "")
                    {
                        StrSql += " AND STypeId = '" + Row["Type"].ToString() + "' ";
                    }

                    dtPointStandard = GetList(PubConstant.hmWyglConnectionString.ToString(), out PageCount, out Counts, StrSql, Convert.ToInt32(Row["Page"]),
                        Convert.ToInt32(Row["Rows"].ToString()), "Sort", 0, "View_Tb_Qm_PointStandardList", "Id").Tables[0];
                    result = JSONHelper.FromStringPaging(dtPointStandard, true, true, PageCount, Counts);
                }
                else
                {
                    result = JSONHelper.FromString(false, "缺少参数CommID/UserCode/Page/Rows");
                }
            }
            catch (Exception EX)
            {
                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }

        /// <summary>
        /// 需求4920 大发 二维码或者点位名模糊查询
        /// </summary>
        public string SearchPoint(DataRow Row)
        {

            string pointId = null;
            string pointName = null;
            if (Row.Table.Columns.Contains("PointId") && !string.IsNullOrEmpty(Row["PointId"].ToString()))
            {
                pointId = Row["PointId"].ToString();
            }
            if (Row.Table.Columns.Contains("PointName") && !string.IsNullOrEmpty(Row["PointName"].ToString()))
            {
                pointName = Row["PointName"].ToString();
            }

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


  
              string sql = $@"SELECT PointId,PointCode,PointName,Addr,Remark,Sort FROM Tb_CP_Point WHERE ISNULL(IsDelete,0)=0 
                                            {(pointId==null?"":"AND PointId ="+pointId)} 
                                            {(pointName == null ? "" : "AND PointName LIKE" +"'%"+ pointName+"%'")}";
           

            int PageCount = 0, Counts = 0;
            DataTable dtTask = GetList(out PageCount, out Counts, sql, PageIndex, PageSize, "Sort", 1, "PointId", PubConstant.hmWyglConnectionString).Tables[0];

            return JSONHelper.FromString(dtTask);

        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="LoginSQLConnStr">数据库连接</param>
        /// <param name="PageCount">总页数（输出）</param>
        /// <param name="Counts">总条数（输出）</param>
        /// <param name="StrCondition">查询条件</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">条数</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">排序方式（1倒序、0正序）</param>
        /// <param name="SQLView">查询的视图</param>
        /// <param name="SQLParaKey">主键</param>
        /// <returns></returns>
        public DataSet GetList(string LoginSQLConnStr, out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort, string SQLView, string SQLParaKey)
        {

            SqlParameter[] parameters = {
                    new SqlParameter("@FldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@FldSort", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Sort", SqlDbType.Int),
                    new SqlParameter("@StrCondition", SqlDbType.VarChar, 8000),
                    new SqlParameter("@Id", SqlDbType.VarChar, 50),
                    new SqlParameter("@PageCount", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    new SqlParameter("@Counts", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    };
            parameters[0].Value = "*";
            parameters[1].Value = PageSize;
            parameters[2].Value = PageIndex;
            parameters[3].Value = SortField;
            parameters[4].Value = Sort;
            parameters[5].Value = "SELECT * FROM " + SQLView + " WHERE 1=1 " + StrCondition;
            parameters[6].Value = SQLParaKey;
            DataSet Ds = new DbHelperSQLP(LoginSQLConnStr).RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        /// <summary>
        /// 所属专业
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string GetProfessionalList(DataRow Row)
        {
            string result = "";
            try
            {
                //TaskId 任务ID

                HM.BLL.Qm.Bll_Tb_QM_Dictionary B_Dictionary = new HM.BLL.Qm.Bll_Tb_QM_Dictionary();
                HM.Model.Qm.Tb_QM_Dictionary M_Dictionary = new HM.Model.Qm.Tb_QM_Dictionary();
                DataTable dt = B_Dictionary.GetList("DType='所属专业' AND ISNULL(IsDelete,0)=0 ORDER BY CONVERT(INT,Sort) ").Tables[0];

                result = JSONHelper.FromString(dt);

            }
            catch (Exception EX)
            {
                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }

        /// <summary>
        /// 所属类别
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string GetTypeList(DataRow Row)
        {
            string result = "";
            try
            {
                //TaskId 任务ID

                HM.BLL.Qm.Bll_Tb_QM_Dictionary B_Dictionary = new HM.BLL.Qm.Bll_Tb_QM_Dictionary();
                HM.Model.Qm.Tb_QM_Dictionary M_Dictionary = new HM.Model.Qm.Tb_QM_Dictionary();
                DataTable dt = B_Dictionary.GetList("DType='所属类别' AND ISNULL(IsDelete,0)=0 ORDER BY CONVERT(INT,Sort) ").Tables[0];

                result = JSONHelper.FromString(dt);

            }
            catch (Exception EX)
            {
                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }


        public string GetProblem()
        {

            string result = "";
            try
            {
                //TaskId 任务ID

                DataSet ds = new HM.BLL.Qm.Bll_Tb_Qm_Problem().GetList(" ISNULL(IsDelete,0)=0 ");
                foreach (DataTable table in ds.Tables)
                {
                    table.Columns["Id"].ColumnName = "ProblemId";

                }
                result = JSONHelper.FromString(ds.Tables[0]);

            }
            catch (Exception EX)
            {
                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }

        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string GetUserByCommId(DataRow Row)
        {
            string result = "";
            try
            {
                //TaskId 任务ID
                if (Row.Table.Columns.Contains("CommID"))
                {
                    //string strSql = " SELECT  UserCode,UserName  FROM dbo.Tb_Sys_User WHERE UserCode IN (SELECT UserCode FROM dbo.Tb_Sys_UserRole WHERE RoleCode IN(SELECT RoleCode FROM dbo.Tb_Sys_RoleData WHERE CommID = '" + Row["CommID"].ToString().Trim() + "'))  AND ISNULL(IsDelete,0)=0 ";
                    IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@CommID", Row["CommID"].ToString().Trim());
                    DataTable dTable = con.ExecuteReader("Proc_Qm_Phone_UserList", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
                    result = JSONHelper.FromString(dTable);

                    //string strSql = " SELECT  UserCode,UserName  FROM dbo.Tb_Sys_User WHERE UserCode IN (SELECT UserCode FROM dbo.Tb_Sys_UserRole WHERE RoleCode IN(SELECT RoleCode FROM dbo.Tb_Sys_RoleData WHERE CommID = '" + Row["CommID"].ToString().Trim() + "'))  AND ISNULL(IsDelete,0)=0 ";
                    //DataSet ds = Query(strSql);
                    //result = JSONHelper.FromString(ds.Tables[0]);

                }
                else
                {
                    result = JSONHelper.FromString(false, "缺少参数CommID");
                }
            }
            catch (Exception EX)
            {
                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }
        //
        public string QualityIsOkSaveApprovedNew(DataRow Row)
        {
            string result = JSONHelper.FromString(false, "错误");
            try
            {
                if (Row["Data"] != null && Row["Data"].ToString().Trim() != "")
                {
                    string strYs = "整改";
                    IDbConnection Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                    HM.Model.Qm.Tb_Qm_TaskAbarbeitung GetModel = new HM.Model.Qm.Tb_Qm_TaskAbarbeitung();
                    HM.Model.Qm.Tb_Qm_TaskAbarbeitung Model = new HM.Model.Qm.Tb_Qm_TaskAbarbeitung();
                    HM.BLL.Qm.Bll_Tb_Qm_TaskAbarbeitung Bll = new HM.BLL.Qm.Bll_Tb_Qm_TaskAbarbeitung();
                    HM.BLL.Qm.Bll_Tb_Qm_Task bllTask = new HM.BLL.Qm.Bll_Tb_Qm_Task();
                    HM.Model.Qm.Tb_Qm_Task Task = new HM.Model.Qm.Tb_Qm_Task();
                    DataTable dtTable = new DataTable();
                    dtTable = JSONHelper.JsonToDataTable(Row["Data"].ToString().Trim());
                    int typeOne = 0;
                    int typeTwo = 0;
                    double doubleBool = 0;
                    for (int dtI = 0; dtI < dtTable.Rows.Count; dtI++)
                    {
                        AppGlobal.FillModel(dtTable.Rows[dtI], GetModel);
                        if (dtTable.Rows.Count > 0 & dtTable.Columns.Contains("AbarbeitungId"))
                        {
                            Model = Bll.GetModel(dtTable.Rows[dtI]["AbarbeitungId"].ToString());
                        }
                        //1是整改 2是验收
                        if (dtTable.Rows[dtI]["Type"].ToString() == "2")
                        {
                            #region 验收
                            Model.ReduceCheckResult = GetModel.ReduceCheckResult;
                            Model.IsOk = GetModel.IsOk;
                            if (Model.IsOk != 1)
                            {
                                Model.ExtendStr = "";
                            }
                            Model.ReduceCheckTime = GetModel.ReduceCheckTime;
                            Model.CheckTime = GetModel.CheckTime;
                            Model.IsInTime = GetModel.IsInTime;
                            Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                            Conn.Update<HM.Model.Qm.Tb_Qm_TaskAbarbeitung>(Model);
                            if (Model.IsOk == 1)//当验收合格后获取全部验收情况如果全部验收成功更改任务状态为已完成
                            {
                                Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                StringBuilder strSql = new StringBuilder();
                                strSql.Append("select Id,TaskId,PointIds,AddPId,Pictures,AddTime,CheckTime,CheckNote,CheckRemark,CheckResult,CheckResult1,ProblemType,RectificationPeriod,RectificationNote,AbarPId,ReducePoint,ReducePId,AbarCheck,AbarCheckPId,AbarCheckTime,Major,ReduceResult,ReduceTime,ReduceCheckResult,ReduceCheckTime,IsInTime,IsOk,CheckStatus,Files,Coordinate,IsDelete ");
                                strSql.Append(" FROM Tb_Qm_TaskAbarbeitung ");
                                strSql.Append(" where TaskId='" + Model.TaskId + "' ");
                                DataTable dt = Conn.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                                //DataTable dt = Bll.GetList("TaskId='" + Model.TaskId + "'").Tables[0];
                                int boolInt = 0;
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    if (dt.Rows[j]["IsOk"].ToString() == "1")
                                        boolInt++;
                                }
                                if (boolInt == dt.Rows.Count)
                                {
                                    Task = bllTask.GetModel(Model.TaskId);
                                    if (Task != null)
                                    {
                                        Task.TaskStatus = "已完成";
                                        Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                        Conn.Update<HM.Model.Qm.Tb_Qm_Task>(Task);
                                        //bllTask.Update(Task);
                                    }
                                }
                            }
                            #endregion
                            typeTwo++;
                            // result = JSONHelper.FromString(true, "品质验收成功");
                        }
                        else
                        {
                            #region 整改
                            string AbarPId = "";
                            if (dtTable.Columns.Contains("AppovedState") && dtTable.Rows[dtI]["AppovedState"].ToString().Trim() == "1")
                            {
                                DynamicParameters parametersAppoved = new DynamicParameters();
                                parametersAppoved.Add("@OriginatorId", Global_Var.UserCode);
                                parametersAppoved.Add("@UndertakingId", Model.AbarPId);
                                parametersAppoved.Add("@AddTime", DateTime.Now);
                                parametersAppoved.Add("@TaskId", Model.TaskId);
                                parametersAppoved.Add("@TaskAbarbeitungId", Model.Id);
                                parametersAppoved.Add("@Type", "Insert");
                                parametersAppoved.Add("@ID", "");
                                Conn.ExecuteReader("PROC_TB_QM_APPROVED_COMMAND", parametersAppoved, null, null, CommandType.StoredProcedure).ToDataSet();
                                Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                            }
                            if (Model != null && Model.Id != null && Model.Id != "")
                            {
                                #region
                                Model.ExtendStr = "1";
                                Model.ReduceResult = GetModel.ReduceResult;
                                Model.ReduceTime = GetModel.ReduceTime;
                                if (Model.ReduceTime != null && Model.UploadTime != null && Model.RectificationPeriod != null)
                                {
                                    if (Double.TryParse(Model.RectificationPeriod, System.Globalization.NumberStyles.Float,
        System.Globalization.NumberFormatInfo.InvariantInfo, out doubleBool))
                                        if (Convert.ToDouble(Model.RectificationPeriod) >= ExecDateDiff(Convert.ToDateTime(Model.ReduceTime), Convert.ToDateTime(Model.UploadTime)))
                                        {
                                            Model.IsInTime = 1;
                                        }
                                        else
                                            Model.IsInTime = 0;
                                }
                                //Model.UploadTime = GetModel.UploadTime;
                                //Bll.Update(Model);
                                //ExecDateDiff(endTime, Convert.ToDateTime(dtPerson.Rows[i]["WorkStartDate"]));
                                Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                Conn.Update<HM.Model.Qm.Tb_Qm_TaskAbarbeitung>(Model);
                                if (Model.CheckResult.Trim() == "合格" || Model.CheckResult.Trim() == "不涉及")
                                {
                                    Model.ReducePoint = 0;
                                    StringBuilder strSql = new StringBuilder();
                                    strSql.Append("select count(*)");
                                    strSql.Append(" FROM Tb_Qm_TaskAbarbeitung ");
                                    strSql.Append(" where TaskId='" + Model.TaskId + "' and CheckResult ='整改' ");
                                    DataTable dt = Conn.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                                    if (dt.Rows.Count > 0)
                                    {
                                        if (Convert.ToInt32(dt.Rows[0][0]) == 0)
                                        {
                                            Task = bllTask.GetModel(Model.TaskId);
                                            if (Task != null)
                                            {
                                                Task.TaskStatus = "已完成";
                                                Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                                Conn.Update<HM.Model.Qm.Tb_Qm_Task>(Task);
                                                //bllTask.Update(Task);
                                            }
                                        }
                                    }
                                    strYs = "验收";
                                    if (Model.AbarCheckPId == "")
                                    {
                                        Model = Bll.GetModel(Model.Id);
                                    }
                                    AbarPId = "'" + Model.AbarCheckPId + "'";
                                }
                                else
                                {
                                    strYs = "验收";
                                    if (Model.AbarCheckPId == "")
                                    {
                                        Model = Bll.GetModel(Model.Id);
                                    }
                                    AbarPId = "'" + Model.AbarCheckPId + "'";
                                }
                                /////////////

                                /////////////
                                #endregion
                            }
                            else
                            {
                                #region
                                Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                if (Model.Id != null && GetModel.Id != "")
                                {
                                    Conn.Update<HM.Model.Qm.Tb_Qm_TaskAbarbeitung>(GetModel);
                                    if (GetModel.CheckResult.Trim() == "合格" || GetModel.CheckResult.Trim() == "不涉及")
                                    {
                                        AbarPId = "";
                                    }
                                }
                                else
                                {
                                    AbarPId = "'" + GetModel.AbarPId + "'";
                                    if (GetModel.AddTime != null && GetModel.AddTime.ToString().Trim() != "")
                                        GetModel.AddTime = DateTime.Now;
                                    GetModel.AddPId = Global_Var.UserCode;
                                    GetModel.UploadTime = DateTime.Now;
                                    GetModel.ExtendStr = "1";
                                    GetModel.Id = Guid.NewGuid().ToString();
                                    Task = bllTask.GetModel(GetModel.TaskId);
                                    if (Task != null && Task.Id != null && Task.Id != "")
                                    {
                                        if (GetModel.CheckResult.Trim() == "合格" || GetModel.CheckResult.Trim() == "不涉及")
                                        {
                                            StringBuilder strSql = new StringBuilder();
                                            strSql.Append("select count(*)");
                                            strSql.Append(" FROM Tb_Qm_TaskAbarbeitung ");
                                            strSql.Append(" where TaskId='" + GetModel.TaskId + "' and CheckResult !='合格' ");
                                            DataTable dt = Conn.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                                            if (dt.Rows.Count > 0)
                                            {
                                                if (Convert.ToInt32(dt.Rows[0][0]) == 0)
                                                {
                                                    Task.TaskStatus = "已完成";
                                                    Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                                    Conn.Update<HM.Model.Qm.Tb_Qm_Task>(Task);
                                                }
                                            }
                                            AbarPId = "";
                                        }
                                        else
                                        {
                                            Task.TaskStatus = "执行中";
                                            Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                            Conn.Update<HM.Model.Qm.Tb_Qm_Task>(Task);
                                        }
                                    }
                                    Conn.Insert<HM.Model.Qm.Tb_Qm_TaskAbarbeitung>(GetModel);
                                }
                                #endregion
                            }
                            Conn = new SqlConnection(PubConstant.tw2bsConnectionString);

                            #region 激光推送消息
                            if (AbarPId != "")//给整改人推送消息
                            {
                                List<string> telMo = new List<string>();
                                string strSQL = "";
                                strSQL = "SELECT MobileTel FROM Tb_Sys_User WHERE 1=1 AND isnull(IsDelete,0)=0 AND isnull(MobileTel,'')<>'' AND isnull(IsMobile,0)=1 AND USERCODE in (" + AbarPId + ")";
                                Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                DataTable dtUser = Conn.ExecuteReader(strSQL, null, null, null, CommandType.Text).ToDataSet().Tables[0];
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
                                            pushModel.Message = "请注意：你有一条新的品质" + strYs + "任务请及时获取";
                                            pushModel.Command = PushCommand.QUALITY_ABARBEITUNG;
                                            pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                                            pushModel.Audience.Objects.AddRange(telMo);
                                            Push.SendAsync(pushModel);
                                            return;
                                        }
                                    }
                                });
                            }
                            #endregion
                            typeOne++;
                            // result = JSONHelper.FromString(true, "品质整改成功");
                            #endregion
                        }

                    }
                    if (typeOne == 0 && typeTwo > 0)
                    {
                        result = JSONHelper.FromString(true, "品质验收成功");
                    }
                    else if (typeTwo == 0 && typeOne > 0)
                    {
                        result = JSONHelper.FromString(true, "品质整改成功");
                    }
                    else if (typeTwo > 0 && typeOne > 0)
                    {
                        result = JSONHelper.FromString(true, "品质整改验收成功");
                    }
                }
                else
                {
                    result = JSONHelper.FromString(false, "缺少参数");
                }
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, AppGlobal.ChkStr(EX.ToString()));
            }
            return result;
        }
        public string QualityIsOkSave(DataRow Row)
        {
            string result = JSONHelper.FromString(false, "错误");
            try
            {
                if (Row["Data"] != null && Row["Data"].ToString().Trim() != "")
                {
                    IDbConnection Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                    HM.Model.Qm.Tb_Qm_TaskAbarbeitung GetModel = new HM.Model.Qm.Tb_Qm_TaskAbarbeitung();
                    HM.Model.Qm.Tb_Qm_TaskAbarbeitung Model = new HM.Model.Qm.Tb_Qm_TaskAbarbeitung();
                    HM.BLL.Qm.Bll_Tb_Qm_TaskAbarbeitung Bll = new HM.BLL.Qm.Bll_Tb_Qm_TaskAbarbeitung();
                    HM.BLL.Qm.Bll_Tb_Qm_Task bllTask = new HM.BLL.Qm.Bll_Tb_Qm_Task();
                    HM.Model.Qm.Tb_Qm_Task Task = new HM.Model.Qm.Tb_Qm_Task();
                    DataTable dtTable = new DataTable();
                    dtTable = JSONHelper.JsonToDataTable(Row["Data"].ToString().Trim());
                    int typeOne = 0;
                    int typeTwo = 0;
                    double doubleBool = 0;
                    for (int dtI = 0; dtI < dtTable.Rows.Count; dtI++)
                    {
                        AppGlobal.FillModel(dtTable.Rows[dtI], GetModel);
                        if (dtTable.Rows.Count > 0 & dtTable.Columns.Contains("AbarbeitungId"))
                        {
                            Model = Bll.GetModel(dtTable.Rows[dtI]["AbarbeitungId"].ToString());
                        }
                        //1是整改 2是验收
                        if (dtTable.Rows[dtI]["Type"].ToString() == "2")
                        {
                            #region 验收
                            Model.ReduceCheckResult = GetModel.ReduceCheckResult;
                            Model.IsOk = GetModel.IsOk;
                            if (Model.IsOk != 1)
                            {
                                Model.ExtendStr = "";
                            }
                            Model.ReduceCheckTime = GetModel.ReduceCheckTime;
                            Model.CheckTime = GetModel.CheckTime;
                            Model.IsInTime = GetModel.IsInTime;
                            Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                            Conn.Update<HM.Model.Qm.Tb_Qm_TaskAbarbeitung>(Model);
                            if (Model.IsOk == 1)//当验收合格后获取全部验收情况如果全部验收成功更改任务状态为已完成
                            {
                                Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                StringBuilder strSql = new StringBuilder();
                                strSql.Append("select Id,TaskId,PointIds,AddPId,Pictures,AddTime,CheckTime,CheckNote,CheckRemark,CheckResult,CheckResult1,ProblemType,RectificationPeriod,RectificationNote,AbarPId,ReducePoint,ReducePId,AbarCheck,AbarCheckPId,AbarCheckTime,Major,ReduceResult,ReduceTime,ReduceCheckResult,ReduceCheckTime,IsInTime,IsOk,CheckStatus,Files,Coordinate,IsDelete ");
                                strSql.Append(" FROM Tb_Qm_TaskAbarbeitung ");
                                strSql.Append(" where TaskId='" + Model.TaskId + "' ");
                                DataTable dt = Conn.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                                //DataTable dt = Bll.GetList("TaskId='" + Model.TaskId + "'").Tables[0];
                                int boolInt = 0;
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    if (dt.Rows[j]["IsOk"].ToString() == "1")
                                        boolInt++;
                                }
                                if (boolInt == dt.Rows.Count)
                                {
                                    Task = bllTask.GetModel(Model.TaskId);
                                    if (Task != null)
                                    {
                                        Task.TaskStatus = "已完成";
                                        Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                        Conn.Update<HM.Model.Qm.Tb_Qm_Task>(Task);
                                        //bllTask.Update(Task);
                                    }
                                }
                            }
                            #endregion
                            typeTwo++;
                            // result = JSONHelper.FromString(true, "品质验收成功");
                        }
                        else
                        {
                            #region 整改
                            string AbarPId = "";
                            if (Model != null && Model.Id != null && Model.Id != "")
                            {
                                #region
                                Model.ReduceResult = GetModel.ReduceResult;
                                Model.ReduceTime = GetModel.ReduceTime;
                                Model.ExtendStr = "1";
                                if (Model.ReduceTime != null && Model.UploadTime != null && Model.RectificationPeriod != null)
                                {
                                    if (Double.TryParse(Model.RectificationPeriod, System.Globalization.NumberStyles.Float,
        System.Globalization.NumberFormatInfo.InvariantInfo, out doubleBool))
                                        if (Convert.ToDouble(Model.RectificationPeriod) >= ExecDateDiff(Convert.ToDateTime(Model.ReduceTime), Convert.ToDateTime(Model.UploadTime)))
                                        {
                                            Model.IsInTime = 1;
                                        }
                                        else
                                            Model.IsInTime = 0;
                                }
                                //Model.UploadTime = GetModel.UploadTime;
                                //Bll.Update(Model);
                                //ExecDateDiff(endTime, Convert.ToDateTime(dtPerson.Rows[i]["WorkStartDate"]));
                                Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                Conn.Update<HM.Model.Qm.Tb_Qm_TaskAbarbeitung>(Model);
                                if (Model.CheckResult.Trim() == "合格" || Model.CheckResult.Trim() == "不涉及")
                                {
                                    Model.ReducePoint = 0;
                                    StringBuilder strSql = new StringBuilder();
                                    strSql.Append("select count(*)");
                                    strSql.Append(" FROM Tb_Qm_TaskAbarbeitung ");
                                    strSql.Append(" where TaskId='" + Model.TaskId + "' and CheckResult ='整改' ");
                                    DataTable dt = Conn.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                                    if (dt.Rows.Count > 0)
                                    {
                                        if (Convert.ToInt32(dt.Rows[0][0]) == 0)
                                        {
                                            Task = bllTask.GetModel(Model.TaskId);
                                            if (Task != null)
                                            {
                                                Task.TaskStatus = "已完成";
                                                Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                                Conn.Update<HM.Model.Qm.Tb_Qm_Task>(Task);
                                                //bllTask.Update(Task);
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    AbarPId = "'" + GetModel.AbarPId + "'";
                                }
                                #endregion
                            }
                            else
                            {
                                Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                if (Model.Id != null && GetModel.Id != "")
                                {
                                    Conn.Update<HM.Model.Qm.Tb_Qm_TaskAbarbeitung>(GetModel);
                                }
                                else
                                {
                                    AbarPId = "'" + GetModel.AbarPId + "'";
                                    if (GetModel.AddTime != null && GetModel.AddTime.ToString().Trim() != "")
                                        GetModel.AddTime = DateTime.Now;
                                    GetModel.AddPId = Global_Var.UserCode;
                                    GetModel.UploadTime = DateTime.Now;
                                    GetModel.ExtendStr = "1";
                                    GetModel.Id = Guid.NewGuid().ToString();
                                    Task = bllTask.GetModel(GetModel.TaskId);
                                    if (Task != null && Task.Id != null && Task.Id != "")
                                    {
                                        if (GetModel.CheckResult.Trim() == "合格" || GetModel.CheckResult.Trim() == "不涉及")
                                        {
                                            StringBuilder strSql = new StringBuilder();
                                            strSql.Append("select count(*)");
                                            strSql.Append(" FROM Tb_Qm_TaskAbarbeitung ");
                                            strSql.Append(" where TaskId='" + GetModel.TaskId + "' and CheckResult !='合格' ");
                                            DataTable dt = Conn.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                                            if (dt.Rows.Count > 0)
                                            {
                                                if (Convert.ToInt32(dt.Rows[0][0]) == 0)
                                                {
                                                    Task.TaskStatus = "已完成";
                                                    Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                                    Conn.Update<HM.Model.Qm.Tb_Qm_Task>(Task);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Task.TaskStatus = "执行中";
                                            Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                            Conn.Update<HM.Model.Qm.Tb_Qm_Task>(Task);
                                        }
                                    }
                                    Conn.Insert<HM.Model.Qm.Tb_Qm_TaskAbarbeitung>(GetModel);
                                }
                            }
                            Conn = new SqlConnection(PubConstant.tw2bsConnectionString);

                            #region 激光推送消息
                            if (AbarPId != "")//给整改人推送消息
                            {
                                List<string> telMo = new List<string>();
                                string strSQL = "";
                                strSQL = "SELECT MobileTel FROM Tb_Sys_User WHERE 1=1 AND isnull(IsDelete,0)=0 AND isnull(MobileTel,'')<>'' AND isnull(IsMobile,0)=1 AND USERCODE in (" + AbarPId + ")";
                                Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                DataTable dtUser = Conn.ExecuteReader(strSQL, null, null, null, CommandType.Text).ToDataSet().Tables[0];
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
                                            pushModel.Message = "请注意：你有一条新的品质整改任务请及时获取";
                                            pushModel.Command = PushCommand.QUALITY_ABARBEITUNG;
                                            pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                                            pushModel.Audience.Objects.AddRange(telMo);
                                            Push.SendAsync(pushModel);
                                            return;
                                        }
                                    }
                                });
                            }
                            #endregion

                            typeOne++;
                            // result = JSONHelper.FromString(true, "品质整改成功");
                            #endregion
                        }

                    }
                    if (typeOne == 0 && typeTwo > 0)
                    {
                        result = JSONHelper.FromString(true, "品质验收成功");
                    }
                    else if (typeTwo == 0 && typeOne > 0)
                    {
                        result = JSONHelper.FromString(true, "品质整改成功");
                    }
                    else if (typeTwo > 0 && typeOne > 0)
                    {
                        result = JSONHelper.FromString(true, "品质整改验收成功");
                    }
                }
                else
                {
                    result = JSONHelper.FromString(false, "缺少参数");
                }
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, AppGlobal.ChkStr(EX.ToString()));
            }
            return result;
        }
        // "<attributes><Package>" + Package.Text.ToString() + "</Package><Tag>" + Tag.Text.ToString() + "</Tag><Title>" + TITLE.Text.ToString() + "</Title><MsgContent>" + MSG_CONTENT.Text.ToString() + "</MsgContent></attributes>";

        //////////////
        //string strDoUrl = PushUrlSure.Text;
        //string strDate = DateTime.Now.ToString("yyyyMMdd");
        //string strToken = "20160907AppPush";
        //string Attribute = "<attributes><Package>" + Package.Text.ToString() + "</Package><Tag>" + Tag.Text.ToString() + "</Tag><Title>" + TITLE.Text.ToString() + "</Title><MsgContent>" + MSG_CONTENT.Text.ToString() + "</MsgContent></attributes>";
        //string Mac = CreMAC(Attribute, strDate, strToken);
        //string strContent = "Class=AppPush&Command=PushAlias&Attribute=" + Attribute + "&Mac=" + Mac;
        //string strNewUrl = strDoUrl + "?" + strContent;
        //PushUrlSure.Text = strNewUrl;
        //        repsTxt = SendHttp(strNewUrl, "");
        ///////////////////////
        public static double ExecDateDiff(DateTime dateBegin, DateTime dateEnd)
        {
            TimeSpan ts1 = new TimeSpan(dateBegin.Ticks);
            TimeSpan ts2 = new TimeSpan(dateEnd.Ticks);
            TimeSpan ts3 = ts1.Subtract(ts2);
            //你想转的格式
            return ts3.TotalHours;
        }
        /// <summary>
        /// 品质整改
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string QualityAbarbeitungSave(DataRow Row)
        {
            string result = JSONHelper.FromString(false, "未知错误!");
            try
            {
                if (Row["Data"] != null && Row["Data"].ToString().Trim() != "")
                {

                    HM.Model.Qm.Tb_Qm_TaskAbarbeitung GetModel = new HM.Model.Qm.Tb_Qm_TaskAbarbeitung();
                    HM.Model.Qm.Tb_Qm_TaskAbarbeitung Model = new HM.Model.Qm.Tb_Qm_TaskAbarbeitung();
                    HM.BLL.Qm.Bll_Tb_Qm_TaskAbarbeitung Bll = new HM.BLL.Qm.Bll_Tb_Qm_TaskAbarbeitung();
                    DataTable dtTable = new DataTable();
                    dtTable = JSONHelper.JsonToDataTable(Row["Data"].ToString().Trim());

                    AppGlobal.FillModel(dtTable.Rows[0], GetModel);
                    if (dtTable.Rows.Count > 0 & dtTable.Columns.Contains("AbarbeitungId"))
                    {
                        Model = Bll.GetModel(dtTable.Rows[0]["AbarbeitungId"].ToString());
                    }
                    if (Model != null && GetModel.ReduceResult != null && GetModel.ReduceResult != "")
                    {
                        Model.ReduceResult = GetModel.ReduceResult;
                        Model.ReduceTime = GetModel.ReduceTime;
                        Bll.Update(Model);
                        result = JSONHelper.FromString(true, "品质整改完成");
                    }
                    else
                    {
                        result = JSONHelper.FromString(false, "参数错误");
                    }
                }
                else
                {
                    result = JSONHelper.FromString(false, "缺少参数");
                }
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }
        /// <summary>
        /// 品质验收
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string QualityOkSave(DataRow Row)
        {
            string result = JSONHelper.FromString(false, "未知错误!");
            try
            {
                if (Row["Data"] != null && Row["Data"].ToString().Trim() != "")
                {
                    HM.Model.Qm.Tb_Qm_TaskAbarbeitung GetModel = new HM.Model.Qm.Tb_Qm_TaskAbarbeitung();
                    HM.Model.Qm.Tb_Qm_TaskAbarbeitung Model = new HM.Model.Qm.Tb_Qm_TaskAbarbeitung();
                    HM.BLL.Qm.Bll_Tb_Qm_TaskAbarbeitung Bll = new HM.BLL.Qm.Bll_Tb_Qm_TaskAbarbeitung();
                    HM.BLL.Qm.Bll_Tb_Qm_Task bllTask = new HM.BLL.Qm.Bll_Tb_Qm_Task();
                    HM.Model.Qm.Tb_Qm_Task Task = new HM.Model.Qm.Tb_Qm_Task();
                    DataTable dtTable = new DataTable();
                    dtTable = JSONHelper.JsonToDataTable(Row["Data"].ToString().Trim());
                    int k = 0;
                    for (int i = 0; i < dtTable.Rows.Count; i++)
                    {
                        AppGlobal.FillModel(dtTable.Rows[i], GetModel);
                        Model = Bll.GetModel(GetModel.Id);
                        Model.ReduceCheckResult = GetModel.ReduceCheckResult;
                        Model.IsOk = GetModel.IsOk;
                        Model.ReduceCheckTime = GetModel.ReduceCheckTime;
                        Model.CheckTime = GetModel.CheckTime;
                        Model.IsInTime = GetModel.IsInTime;
                        if (Model.IsOk == 1)//当验收合格后获取全部验收情况如果全部验收成功更改任务状态为已完成
                        {
                            DataTable dt = Bll.GetList("TaskId='" + Model.TaskId + "'").Tables[0];
                            int boolInt = 0;
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                if (dt.Rows[j]["IsOk"].ToString() == "1")
                                    boolInt++;
                            }
                            if (boolInt == dt.Rows.Count)
                            {
                                Task = bllTask.GetModel(Model.TaskId);
                                if (Task != null)
                                {
                                    Task.TaskStatus = "已完成";
                                    bllTask.Update(Task);
                                }
                            }
                        }
                        Bll.Update(Model);
                        k++;
                    }
                    if (dtTable.Rows.Count == k)
                        result = JSONHelper.FromString(true, "品质验收完成");
                    else
                    {
                        result = JSONHelper.FromString(false, "品质验收过程中出现错误未能全部登记");
                    }
                    //}
                    //else
                    //{
                    //    result = JSONHelper.FromString(false, "缺少Data参数！");
                    //}

                }
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }

        /// <summary>
        /// 核查任务登记（旧）
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        /// 
        public string QualityInspectSave(DataRow Row)
        {
            string result = "";
            try
            {
                if (Row["Data"] != null && Row["Data"].ToString().Trim() != "")
                {
                    IDbConnection Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                    HM.Model.Qm.Tb_Qm_Task TaskModel = new HM.Model.Qm.Tb_Qm_Task();
                    HM.BLL.Qm.Bll_Tb_Qm_Task TaskBll = new HM.BLL.Qm.Bll_Tb_Qm_Task();
                    HM.Model.Qm.Tb_Qm_TaskAbarbeitung Model = new HM.Model.Qm.Tb_Qm_TaskAbarbeitung();
                    HM.BLL.Qm.Bll_Tb_Qm_TaskAbarbeitung Bll = new HM.BLL.Qm.Bll_Tb_Qm_TaskAbarbeitung();
                    HM.Model.Qm.Tb_Qm_TaskPoint TaskPointModel = new HM.Model.Qm.Tb_Qm_TaskPoint();
                    HM.BLL.Qm.Bll_Tb_Qm_TaskPoint TaskPointBll = new HM.BLL.Qm.Bll_Tb_Qm_TaskPoint();
                    Newtonsoft.Json.Linq.JObject obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(Row["Data"].ToString().Trim());//获得JsonObject对象
                    DataTable dtTable = new DataTable();
                    string TaskId = obj["TaskId"].ToString();
                    TaskModel = TaskBll.GetModel(TaskId);
                    ///点位
                    DataTable dtPoints = JsonConvert.DeserializeObject<DataTable>(obj["Points"].ToString());
                    for (int i = 0; i < dtPoints.Rows.Count; i++)
                    {
                        TaskPointModel = new HM.Model.Qm.Tb_Qm_TaskPoint();
                        AppGlobal.FillModel(dtPoints.Rows[i], TaskPointModel);
                        if (TaskPointModel.AddPId != null && TaskPointModel.AddPId != "")
                        {
                            TaskPointModel.PointIds = dtPoints.Rows[i]["PointId"].ToString();
                            TaskPointModel.Id = Guid.NewGuid().ToString();
                            TaskPointModel.TaskId = TaskId;
                            TaskPointBll.Add(TaskPointModel);
                        }
                    }
                    ///记录
                    dtTable = JsonConvert.DeserializeObject<DataTable>(obj["Record"].ToString());
                    string AbarPId = "";
                    string AbarbeitungId = "";
                    int k = 0;
                    for (int i = 0; i < dtTable.Rows.Count; i++)
                    {
                        Model = new HM.Model.Qm.Tb_Qm_TaskAbarbeitung();
                        AppGlobal.FillModel(dtTable.Rows[i], Model);
                        if (Model.AddTime == null || Model.AddTime.ToString().Trim() == "")
                            Model.AddTime = DateTime.Now;
                        Model.UploadTime = DateTime.Now;
                        Model.AddPId = Global_Var.UserCode;
                        AbarPId += Model.AbarPId + ",";
                        Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                        if (Model.CheckResult.Trim() == "合格" || Model.CheckResult.Trim() == "不涉及")
                        {
                            Model.ReducePoint = 0;
                        }
                        if (Model.Id != null && Model.Id != "")
                        {
                            Conn.Update<HM.Model.Qm.Tb_Qm_TaskAbarbeitung>(Model);
                        }
                        else
                        {
                            Model.Id = Guid.NewGuid().ToString();
                            Conn.Insert<HM.Model.Qm.Tb_Qm_TaskAbarbeitung>(Model);
                        }
                        AbarbeitungId += Model.Id + ",";
                        k++;
                        if (TaskModel != null)
                        {
                            #region 以前的任务逻辑
                            //if (Model.CheckResult.Trim() == "合格" || Model.CheckResult.Trim() == "不涉及")
                            //{
                            //    StringBuilder strSql = new StringBuilder();
                            //    strSql.Append("select count(*)");
                            //    strSql.Append(" FROM Tb_Qm_TaskAbarbeitung ");
                            //    strSql.Append(" where TaskId='" + Model.TaskId + "' and CheckResult ='整改' ");
                            //    DataTable dt = Conn.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                            //    if (dt.Rows.Count > 0)
                            //    {
                            //        if (Convert.ToInt32(dt.Rows[0][0]) == 0)
                            //        {
                            //            TaskModel.TaskStatus = "已完成";
                            //            Conn = new SqlConnection(PubConstant.PrivateConnectionString);
                            //            Conn.Update<HM.Model.Qm.Tb_Qm_Task>(TaskModel);
                            //        }
                            //    }
                            //}
                            //else if (TaskModel.TaskStatus != "执行中")
                            //{
                            //    TaskModel.TaskStatus = "执行中";
                            //    Conn = new SqlConnection(PubConstant.PrivateConnectionString);
                            //    Conn.Update<HM.Model.Qm.Tb_Qm_Task>(TaskModel);
                            //}
                            #endregion
                            #region
                            if (Model.CheckResult.Trim() == "合格" || Model.CheckResult.Trim() == "不涉及")
                            {
                                TaskModel.TaskStatus = "已完成";
                                Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                Conn.Update<HM.Model.Qm.Tb_Qm_Task>(TaskModel);
                            }
                            else if (TaskModel.TaskStatus == "执行中")
                            {
                                TaskModel.TaskStatus = "执行中";
                                Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                Conn.Update<HM.Model.Qm.Tb_Qm_Task>(TaskModel);
                            }
                            else
                            {
                                if (TaskModel.TaskStatus.Trim() == "")
                                    TaskModel.TaskStatus = "已完成";
                                Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                Conn.Update<HM.Model.Qm.Tb_Qm_Task>(TaskModel);
                            }
                            #endregion
                        }
                    }
                    Conn = new SqlConnection(PubConstant.tw2bsConnectionString);
                    //if (AbarPId != "")//给整改人推送消息
                    //{
                    //string strSQL = "";
                    //strSQL = "SELECT MobileTel FROM Tb_Sys_User WHERE USERCODE='" + AbarPId + "'";
                    //Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                    //DataTable dtUser = Conn.ExecuteReader(strSQL, null, null, null, CommandType.Text).ToDataSet().Tables[0];
                    //if (dtUser != null && dtUser.Rows.Count > 0 && dtUser.Rows[0][0].ToString().Trim() != "")
                    //{
                    //    new AppPush().SendAppPushMsg("你有一条品质整改任务请下载", "你有一条品质整改任务请下载", dtUser.Rows[0][0].ToString().Trim(), AppPush.JPushMsgType.DEFAULT, "");
                    //}

                    ////
                    #region 激光推送消息
                    if (AbarPId != "")//给整改人推送消息
                    {
                        AbarPId = AbarPId.Substring(0, AbarPId.Length - 1);
                        List<string> telMo = new List<string>();
                        string strSQL = "";
                        strSQL = "SELECT MobileTel FROM Tb_Sys_User WHERE 1=1 AND isnull(IsDelete,0)=0 AND isnull(MobileTel,'')<>'' AND isnull(IsMobile,0)=1 AND USERCODE in (" + AbarPId + ")";
                        Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                        DataTable dtUser = Conn.ExecuteReader(strSQL, null, null, null, CommandType.Text).ToDataSet().Tables[0];
                        for (int i = 0; i < dtUser.Rows.Count; i++)
                        {
                            telMo.Add(dtUser.Rows[i][0].ToString());
                        }
                        string tw2bsConnectionString = PubConstant.tw2bsConnectionString;
                        string hmWyglConnectionString = PubConstant.hmWyglConnectionString;
                        string corpId = Global_Var.CorpId;

                        Task.Run(() =>
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
                                    pushModel.Title = "你有一条新的品质整改任务";
                                    pushModel.Message = "请注意：你有一条新的品质整改任务请及时获取";
                                    pushModel.Command = PushCommand.QUALITY_ABARBEITUNG;
                                    pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                                    pushModel.Audience.Objects.AddRange(telMo);
                                    Push.SendAsync(pushModel);
                                    return;
                                }
                            }
                        });
                    }
                    #endregion
                    // }
                    if (dtTable.Rows.Count == k)
                        result = JSONHelper.FromString(true, "登记完成");
                    else
                    {
                        result = JSONHelper.FromString(false, "登记过程中出现错误未能全部登记");
                    }
                }
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }
        /// <summary>
        /// 任务登记(更改任务状态)
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string OptionTaskRegister(DataRow Row)
        {
            string result = "";
            try
            {
                //TaskId 任务ID
                if (Row.Table.Columns.Contains("TaskId"))
                {
                    string strSql = " Update Tb_Qm_Task Set TaskStatus='执行中' WHERE Id ='" + Row["TaskId"].ToString().Trim() + "' ";
                    DataSet ds = Query(strSql);
                    result = JSONHelper.FromString(ds.Tables[0]);
                }
            }
            catch (Exception EX)
            {
                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }
        /// <summary>
        /// 任务完成操作
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string OptionTaskOver(DataRow Row)
        {
            string result = "";
            try
            {
                //TaskId 任务ID
                if (Row.Table.Columns.Contains("TaskId"))
                {
                    string strSql = " Update Tb_Qm_Task Set TaskStatus='已完成' WHERE Id ='" + Row["TaskId"].ToString().Trim() + "' ";
                    DataSet ds = Query(strSql);
                    result = JSONHelper.FromString(ds.Tables[0]);
                }
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }
        /// <summary>
        /// 核查整改详细
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetTaskAbarbeitungSel(string AbarbeitungId)
        {
            string result = "";
            try
            {
                string strSql = " SELECT * FROM View_Phone_Tb_Qm_TaskAbarbeitung WHERE ISNULL(ISDELETE,0)=0 AND AbarbeitungId = '" + AbarbeitungId + "' ";
                DataTable dt = Query(strSql).Tables[0];
                result = JSONHelper.FromString(dt, false, false);
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }
        /// <summary>
        /// 核查整改列表（新+加批转）
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string GetTaskAbarbeitungByIdApprovedNew(DataRow Row)
        {
            string result = "";
            try
            {
                //整改人员 整改人员Id
                if (Row.Table.Columns.Contains("AbarbeitungId"))
                {
                    IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    StringBuilder strSql = new StringBuilder();
                    //View_Qm_Phone_AbarbeitungApprovedList

                    strSql.Append(" SELECT 1 as OutAbarbeitung,* FROM View_Qm_Phone_AbarbeitungApprovedList  WHERE ISNULL(ISDELETE,0)=0 AND ");

                    strSql.Append(" AbarbeitungId='" + Row["AbarbeitungId"].ToString().Trim() + "' ");
                    int PageCount = 0, Counts = 0;
                    int PageIndex = 1;
                    int PageSize = 20;

                    DataTable dt = GetList(out PageCount, out Counts, strSql.ToString(), PageIndex, PageSize, "BeginDate", 1, "AbarbeitungId", PubConstant.hmWyglConnectionString, "*").Tables[0];
                    //DataTable dt = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                    dt.Columns.Add("DataDetail", typeof(string));
                    dt.Columns.Add("DataFiles", typeof(string));
                    dt.Columns.Add("Approved", typeof(string));

                    ArrayList arr = new ArrayList();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        #region
                        dt.Rows[i]["DataDetail"] = GetTaskAbarbeitungSel(dt.Rows[i]["AbarbeitungId"].ToString().Trim());
                        #endregion
                        #region 批转列表

                        strSql.Length = 0;
                        strSql.Append(" SELECT * FROM View_TB_QM_APPROVED_File where TaskId = '" + dt.Rows[i]["TaskId"] + "' and TaskAbarbeitungId='" + dt.Rows[i]["AbarbeitungId"] + "' ");
                        con = new SqlConnection(PubConstant.hmWyglConnectionString);
                        dt.Rows[i]["Approved"] = JSONHelper.FromString(con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0], false, false);

                        #endregion
                        dt.Rows[i]["DataFiles"] = "[]";
                        #region 附件列表
                        if (!arr.Contains(dt.Rows[i]["TaskId"].ToString()))
                        {
                            strSql.Length = 0;
                            strSql.Append(" SELECT FileName,Fix,FilePath,TaskId FROM Tb_Qm_TaskFiles  WHERE  ISNULL(ISDELETE,0)=0 and TaskId = '" + dt.Rows[i]["TaskId"] + "' ");
                            con = new SqlConnection(PubConstant.hmWyglConnectionString);
                            dt.Rows[i]["DataFiles"] = JSONHelper.FromString(con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0], false, false);

                        }
                        #endregion
                        arr.Add(dt.Rows[i]["TaskId"].ToString());
                    }


                    if (dt.Rows.Count > 0)
                    {
                        con = new SqlConnection(PubConstant.hmWyglConnectionString);
                        HM.Model.Qm.Tb_Interface_Record Interface_Record = new HM.Model.Qm.Tb_Interface_Record();
                        Interface_Record.Id = Guid.NewGuid().ToString();
                        Interface_Record.Type = "品质核查整改";
                        Interface_Record.GetDate = DateTime.Now;
                        Interface_Record.ItemCode = dt.Rows[0]["ItemCode"].ToString().Trim();
                        Interface_Record.UserCode = dt.Rows[0]["AbarPId"].ToString().Trim();
                        //任务获取记录
                        con.Insert<HM.Model.Qm.Tb_Interface_Record>(Interface_Record);
                    }
                    result = JSONHelper.FromString(dt);
                }
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }
        /// <summary>
        /// 核查整改列表（新+加批转）
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string GetTaskAbarbeitungListApprovedNew(DataRow Row)
        {
            string result = "";
            try
            {
                //整改人员 整改人员Id
                if (Row.Table.Columns.Contains("AbarPId"))
                {
                    IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    StringBuilder strSql = new StringBuilder();
                    //View_Qm_Phone_AbarbeitungApprovedList

                    strSql.Append(" SELECT 1 as OutAbarbeitung,* FROM View_Qm_Phone_AbarbeitungApprovedList  WHERE ISNULL(ISDELETE,0)=0 and CheckResult='整改' AND ");
                    strSql.Append(" ISNULL(ExtendStr,'')='' and (AbarPId = '" + Row["AbarPId"].ToString().Trim() + "' or UndertakingId='" + Row["AbarPId"].ToString().Trim() + "' ) ");
                    strSql.Append(" AND ISNULL(IsOk,'') !='1' ");
                    //strSql.Append(" AND getCheckDate>='" + DateTime.Now + "' AND ISNULL(IsOk,'') !='1' ");
                    strSql.Append(" AND ItemCode='" + Row["ItemCode"].ToString().Trim() + "' ");
                    int PageCount = 0, Counts = 0;
                    int PageIndex = 1;
                    int PageSize = 60;

                    if (Row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(Row["PageIndex"].ToString()) > 0)
                    {
                        PageIndex = AppGlobal.StrToInt(Row["PageIndex"].ToString());
                    }
                    if (Row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(Row["PageSize"].ToString()) > 0)
                    {
                        PageSize = AppGlobal.StrToInt(Row["PageSize"].ToString());
                    }
                    DataTable dt = GetList(out PageCount, out Counts, strSql.ToString(), PageIndex, PageSize, "BeginDate", 1, "AbarbeitungId", PubConstant.hmWyglConnectionString, "*").Tables[0];
                    //DataTable dt = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                    dt.Columns.Add("DataDetail", typeof(string));
                    dt.Columns.Add("DataFiles", typeof(string));
                    dt.Columns.Add("Approved", typeof(string));

                    ArrayList arr = new ArrayList();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        #region
                        dt.Rows[i]["DataDetail"] = GetTaskAbarbeitungSel(dt.Rows[i]["AbarbeitungId"].ToString().Trim());
                        #endregion
                        #region 批转列表

                        strSql.Length = 0;
                        strSql.Append(" SELECT * FROM View_TB_QM_APPROVED_File where TaskId = '" + dt.Rows[i]["TaskId"] + "' and TaskAbarbeitungId='" + dt.Rows[i]["AbarbeitungId"] + "' ");
                        con = new SqlConnection(PubConstant.hmWyglConnectionString);
                        dt.Rows[i]["Approved"] = JSONHelper.FromString(con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0], false, false);

                        #endregion
                        dt.Rows[i]["DataFiles"] = "[]";
                        #region 附件列表
                        if (!arr.Contains(dt.Rows[i]["TaskId"].ToString()))
                        {
                            strSql.Length = 0;
                            strSql.Append(" SELECT FileName,Fix,FilePath,TaskId FROM Tb_Qm_TaskFiles  WHERE  ISNULL(ISDELETE,0)=0 and TaskId = '" + dt.Rows[i]["TaskId"] + "' ");
                            con = new SqlConnection(PubConstant.hmWyglConnectionString);
                            dt.Rows[i]["DataFiles"] = JSONHelper.FromString(con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0], false, false);

                        }
                        #endregion
                        arr.Add(dt.Rows[i]["TaskId"].ToString());
                    }
                    strSql.Length = 0;
                    //WriteLog



                    strSql.Append(" SELECT 2 as OutAbarbeitung,* FROM View_Qm_Phone_AbarbeitungList  WHERE ISNULL(ISDELETE,0)=0 AND CheckResult='整改' AND ");

                    if (Row.Table.Columns.Contains("Account") && Row["Account"].ToString() != "")//当只传一个值时
                    {
                        string[] accounts = Row["Account"].ToString().Split('-');
                        if (accounts[0] == "1971")//如果是敏捷项目将不会反悔整改还未开始的验收任务
                        {
                            strSql.Append(" (addPId = '" + Row["AbarPId"].ToString().Trim() + "' AND ReduceTime!='')  ");
                        }
                        else
                            strSql.Append(" addPId = '" + Row["AbarPId"].ToString().Trim() + "' ");
                    }
                    else
                        strSql.Append(" addPId = '" + Row["AbarPId"].ToString().Trim() + "' ");
                    strSql.Append(" AND ISNULL(IsOk,'') !='1'  ");
                    //strSql.Append(" AND getCheckDate>='" + DateTime.Now + "' AND ISNULL(IsOk,'') !='1'  ");
                    strSql.Append(" AND ItemCode='" + Row["ItemCode"].ToString().Trim() + "' ");

                    if (Global_Var.CorpID != "" && Global_Var.CorpID == "1971")
                    {
                        strSql.Append(" AND ISNULL(ReduceTime,'')!='' ");
                    }
                    con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    //DataTable dt2 = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];

                    DataTable dt2 = GetList(out PageCount, out Counts, strSql.ToString(), PageIndex, PageSize, "BeginDate", 1, "AbarbeitungId", PubConstant.hmWyglConnectionString, "*").Tables[0];


                    dt2.Columns.Add("DataDetail", typeof(string));
                    dt2.Columns.Add("DataFiles", typeof(string));
                    dt2.Columns.Add("Approved", typeof(string));
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        #region
                        dt2.Rows[i]["DataDetail"] = GetTaskAbarbeitungSel(dt2.Rows[i]["AbarbeitungId"].ToString().Trim());
                        dt2.Rows[i]["DataFiles"] = "[]";
                        #region 批转列表
                        strSql.Length = 0;
                        strSql.Append(" SELECT * FROM View_TB_QM_APPROVED_File where TaskId = '" + dt2.Rows[i]["TaskId"] + "' and TaskAbarbeitungId='" + dt2.Rows[i]["AbarbeitungId"] + "' ");
                        con = new SqlConnection(PubConstant.hmWyglConnectionString);
                        dt2.Rows[i]["Approved"] = JSONHelper.FromString(con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0], false, false);
                        #endregion
                        #endregion
                        #region 附件列表
                        if (!arr.Contains(dt2.Rows[i]["TaskId"].ToString()))
                        {
                            strSql.Length = 0;
                            strSql.Append(" SELECT FileName,Fix,FilePath,TaskId FROM Tb_Qm_TaskFiles  WHERE ISNULL(ISDELETE,0)=0 AND TaskId = '" + dt2.Rows[i]["TaskId"] + "' ");
                            con = new SqlConnection(PubConstant.hmWyglConnectionString);
                            dt2.Rows[i]["DataFiles"] = JSONHelper.FromString(con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0], false, false);
                        }
                        #endregion
                        arr.Add(dt2.Rows[i]["TaskId"].ToString());
                    }
                    dt.Merge(dt2);

                    con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    HM.Model.Qm.Tb_Interface_Record Interface_Record = new HM.Model.Qm.Tb_Interface_Record();
                    Interface_Record.Id = Guid.NewGuid().ToString();
                    Interface_Record.Type = "品质核查整改";
                    Interface_Record.GetDate = DateTime.Now;
                    Interface_Record.ItemCode = Row["ItemCode"].ToString().Trim();
                    Interface_Record.UserCode = Row["AbarPId"].ToString().Trim();
                    //任务获取记录
                    con.Insert<HM.Model.Qm.Tb_Interface_Record>(Interface_Record);
                    result = JSONHelper.FromString(dt);
                }
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }
        /// <summary>
        /// 核查整改列表
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string GetTaskAbarbeitungList(DataRow Row)
        {
            string result = "";
            try
            {
                //整改人员 整改人员Id
                if (Row.Table.Columns.Contains("AbarPId"))
                {
                    IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(" SELECT 1 as OutAbarbeitung,* FROM View_Qm_Phone_AbarbeitungList  WHERE ISNULL(ISDELETE,0)=0 and CheckResult='整改' AND AbarPId = '" + Row["AbarPId"].ToString().Trim() + "' AND getCheckDate>='" + DateTime.Now + "' AND ISNULL(IsOk,'') !='1' ");
                    strSql.Append(" AND ItemCode='" + Row["ItemCode"].ToString().Trim() + "' ");
                    int PageCount = 0, Counts = 0;
                    int PageIndex = 1;
                    int PageSize = 60;

                    if (Row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(Row["PageIndex"].ToString()) > 0)
                    {
                        PageIndex = AppGlobal.StrToInt(Row["PageIndex"].ToString());
                    }
                    if (Row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(Row["PageSize"].ToString()) > 0)
                    {
                        PageSize = AppGlobal.StrToInt(Row["PageSize"].ToString());
                    }
                    DataTable dt = GetList(out PageCount, out Counts, strSql.ToString(), PageIndex, PageSize, "BeginDate", 1, "AbarbeitungId", PubConstant.hmWyglConnectionString, "*").Tables[0];
                    //DataTable dt = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                    dt.Columns.Add("DataDetail", typeof(string));
                    dt.Columns.Add("DataFiles", typeof(string));

                    ArrayList arr = new ArrayList();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        #region
                        //if (!Convert.IsDBNull(dt.Rows[i]["ReduceTime"]))
                        //{
                        //    dt.Rows[i]["ReduceTime"] = Convert.ToDateTime(dt.Rows[i]["ReduceTime"]).ToString("yyyy/MM/dd HH:mm:ss");
                        //}
                        //if (!Convert.IsDBNull(dt.Rows[i]["BeginDate"]))
                        //{
                        //    dt.Rows[i]["BeginDate"] = Convert.ToDateTime(dt.Rows[i]["BeginDate"]).ToString("yyyy/MM/dd HH:mm:ss");
                        //}
                        //if (!Convert.IsDBNull(dt.Rows[i]["endDate"]))
                        //{
                        //    dt.Rows[i]["endDate"] = Convert.ToDateTime(dt.Rows[i]["endDate"]).ToString("yyyy/MM/dd HH:mm:ss");
                        //}
                        //if (!Convert.IsDBNull(dt.Rows[i]["AddTime"]))
                        //{
                        //    dt.Rows[i]["AddTime"] = Convert.ToDateTime(dt.Rows[i]["AddTime"]).ToString("yyyy/MM/dd HH:mm:ss");
                        //}
                        dt.Rows[i]["DataDetail"] = GetTaskAbarbeitungSel(dt.Rows[i]["AbarbeitungId"].ToString().Trim());

                        #endregion
                        dt.Rows[i]["DataFiles"] = "[]";
                        #region 附件列表
                        if (!arr.Contains(dt.Rows[i]["TaskId"].ToString()))
                        {
                            strSql.Length = 0;
                            strSql.Append(" SELECT FileName,Fix,FilePath,TaskId FROM Tb_Qm_TaskFiles  WHERE  ISNULL(ISDELETE,0)=0 and TaskId = '" + dt.Rows[i]["TaskId"] + "' ");
                            con = new SqlConnection(PubConstant.hmWyglConnectionString);
                            dt.Rows[i]["DataFiles"] = JSONHelper.FromString(con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0], false, false);

                        }
                        #endregion
                        arr.Add(dt.Rows[i]["TaskId"].ToString());
                    }
                    strSql.Length = 0;
                    //WriteLog

                    strSql.Append(" SELECT 2 as OutAbarbeitung,* FROM View_Qm_Phone_AbarbeitungList  WHERE ISNULL(ISDELETE,0)=0 AND CheckResult='整改' AND addPId = '" + Row["AbarPId"].ToString().Trim() + "' AND getCheckDate>='" + DateTime.Now + "' AND ISNULL(IsOk,'') !='1'  ");
                    strSql.Append(" AND ItemCode='" + Row["ItemCode"].ToString().Trim() + "' ");
                    con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    //DataTable dt2 = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];

                    DataTable dt2 = GetList(out PageCount, out Counts, strSql.ToString(), PageIndex, PageSize, "BeginDate", 1, "AbarbeitungId", PubConstant.hmWyglConnectionString, "*").Tables[0];


                    dt2.Columns.Add("DataDetail", typeof(string));
                    dt2.Columns.Add("DataFiles", typeof(string));
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        #region
                        //if (!Convert.IsDBNull(dt2.Rows[i]["ReduceTime"]))
                        //{
                        //    dt2.Rows[i]["ReduceTime"] = Convert.ToDateTime(dt2.Rows[i]["ReduceTime"]).ToString("yyyy/MM/dd HH:mm:ss");
                        //}
                        //if (!Convert.IsDBNull(dt2.Rows[i]["BeginDate"]))
                        //{
                        //    dt2.Rows[i]["BeginDate"] = Convert.ToDateTime(dt2.Rows[i]["BeginDate"]).ToString("yyyy/MM/dd HH:mm:ss");
                        //}
                        //if (!Convert.IsDBNull(dt2.Rows[i]["endDate"]))
                        //{
                        //    dt2.Rows[i]["endDate"] = Convert.ToDateTime(dt2.Rows[i]["endDate"]).ToString("yyyy/MM/dd HH:mm:ss");
                        //}
                        //if (!Convert.IsDBNull(dt2.Rows[i]["AddTime"]))
                        //{
                        //    dt2.Rows[i]["AddTime"] = Convert.ToDateTime(dt2.Rows[i]["AddTime"]).ToString("yyyy/MM/dd HH:mm:ss");
                        //}
                        dt2.Rows[i]["DataDetail"] = GetTaskAbarbeitungSel(dt2.Rows[i]["AbarbeitungId"].ToString().Trim());
                        dt2.Rows[i]["DataFiles"] = "[]";
                        #endregion
                        #region 附件列表
                        if (!arr.Contains(dt2.Rows[i]["TaskId"].ToString()))
                        {
                            strSql.Length = 0;
                            strSql.Append(" SELECT FileName,Fix,FilePath,TaskId FROM Tb_Qm_TaskFiles  WHERE ISNULL(ISDELETE,0)=0 AND TaskId = '" + dt2.Rows[i]["TaskId"] + "' ");
                            con = new SqlConnection(PubConstant.hmWyglConnectionString);
                            dt2.Rows[i]["DataFiles"] = JSONHelper.FromString(con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0], false, false);
                        }
                        #endregion
                        arr.Add(dt2.Rows[i]["TaskId"].ToString());
                    }
                    dt.Merge(dt2);

                    con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    HM.Model.Qm.Tb_Interface_Record Interface_Record = new HM.Model.Qm.Tb_Interface_Record();
                    Interface_Record.Id = Guid.NewGuid().ToString();
                    Interface_Record.Type = "品质核查整改";
                    Interface_Record.GetDate = DateTime.Now;
                    Interface_Record.ItemCode = Row["ItemCode"].ToString().Trim();
                    Interface_Record.UserCode = Row["AbarPId"].ToString().Trim();
                    //任务获取记录
                    con.Insert<HM.Model.Qm.Tb_Interface_Record>(Interface_Record);
                    result = JSONHelper.FromString(dt);
                }
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }
        public string GetTaskInspectRecordSel(DataRow Row)
        {
            string result = "";
            try
            {
                //ID
                if (Row.Table.Columns.Contains("AbarbeitungId"))
                {
                    string strSql = " SELECT * FROM Tb_Qm_TaskAbarbeitung WHERE ISNULL(ISDELETE,0)=0 AND Id = '" + Row["AbarbeitungId"].ToString().Trim() + "' ";
                    DataSet ds = Query(strSql);
                    result = JSONHelper.FromString(ds.Tables[0]);
                }
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }

        /// <summary>
        /// 核查任务详细
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string GetQualityInspectSelDetail(string TaskId, string ProjectCode)
        {
            #region 
            //PubConstant.PrivateConnectionString
            //IDbConnection con = new SqlConnection(PubConstant.PrivateConnectionString);
            //Tb_System_BusinessCorp m = con.Query<Tb_System_BusinessCorp>("select BussId, BussName, BussAddress, BussLinkMan, BussMobileTel, BussWorkedTel, BussWeiXin, LogoImgUrl from dbo.Tb_System_BusinessCorp where BussId=@BussId ", new { BussId = BussId }).SingleOrDefault<Tb_System_BusinessCorp>();

            //DataSet ds = con.ExecuteReader("select BussId, BussName, BussAddress, BussLinkMan, BussMobileTel, BussWorkedTel, BussWeiXin, LogoImgUrl from dbo.Tb_System_BusinessCorp where BussId='" + BussId + "'").ToDataSet();
            //return ds;
            #endregion
            string result = "";
            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
            try
            {
                //TaskId 任务ID
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TaskId", TaskId);
                parameters.Add("@ProjectCode", ProjectCode);
                DataSet ds = new DataSet();
                #region 核查点位（旧）

                #endregion
                #region 核查点位
                int dwCount = 0;
                DataTable dTable = con.ExecuteReader("Proc_Qm_Phone_InspectPoint", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];

                dwCount = dTable.Rows.Count;
                dTable.Columns.Add("TaskId", typeof(string));
                if (!dTable.Columns.Contains("AddTime") && !dTable.Columns.Contains("AddPId"))
                {
                    dTable.Columns.Add("AddTime", typeof(string));//点位查询时间 添加时间2016.5.23
                    dTable.Columns.Add("AddPId", typeof(string));//点位查询人 添加时间2016.5.23
                }
                con = new SqlConnection(PubConstant.hmWyglConnectionString);
                DataTable dtTaskPoint = con.ExecuteReader("SELECT * FROM Tb_Qm_TaskPoint WHERE taskId='" + TaskId + "' AND ISNULL(ISDELETE,0)=0 ", null, null, null, CommandType.Text).ToDataSet().Tables[0];//拼接以查点位
                DataRow[] dtTaskPointRows;
                if (!dTable.Columns.Contains("Address"))
                {
                    dTable.Columns.Add("Address", typeof(string));//点位地址
                }
                for (int i = 0; i < dTable.Rows.Count; i++)
                {
                    dTable.Rows[i]["TaskId"] = TaskId;
                    if (dtTaskPoint.Rows.Count > 0)
                    {
                        dtTaskPointRows = dtTaskPoint.Select("PointIds='" + dTable.Rows[i]["PointId"] + "'");
                        if (dtTaskPointRows.Length > 0)
                        {
                            // dTable.Rows[i]["AddTime"] = Convert.ToDateTime(dtTaskPointRows[0]["AddTime"]).ToString("yyyy/MM/dd HH:mm:ss");
                            dTable.Rows[i]["AddPId"] = dtTaskPointRows[0]["AddPId"];
                        }
                        else
                        {
                            dTable.Rows[i]["AddTime"] = "";
                            dTable.Rows[i]["AddPId"] = "";
                        }
                    }
                    else
                    {
                        dTable.Rows[i]["AddTime"] = "";
                        dTable.Rows[i]["AddPId"] = "";
                    }
                }
                dTable.TableName = "Position";
                DataView dv = new DataView(dTable);
                dTable = dv.ToTable(true, GetColumnsByDataTable(dTable));
                ds.Tables.Add(dTable);
                dTable.Dispose();
                #endregion
                #region 核查任务(旧)

                #endregion
                #region 核查任务
                con = new SqlConnection(PubConstant.hmWyglConnectionString);
                DataSet dSet = con.ExecuteReader("Proc_Qm_Phone_InspectTask", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
                dTable = dSet.Tables[0].Copy();
                dTable.Rows[0]["ZZDWCOUNT"] = dwCount;
                dTable.TableName = "Task";
                ds.Tables.Add(dTable);
                dTable.Dispose();
                dSet.Dispose();
                #endregion
                #region 核查记录(旧)
                //string strSql = " SELECT * FROM View_Tb_Qm_TaskAbarbeitung_Phone  WHERE TaskId = '" + TaskId + "' ";
                //dSet = Query(strSql);
                //dTable = dSet.Tables[0].Copy();
                //dTable.TableName = "Record";
                //ds.Tables.Add(dTable);
                //dSet.Dispose();
                //dTable.Dispose();
                #endregion
                #region 核查记录
                con = new SqlConnection(PubConstant.hmWyglConnectionString);
                dSet = con.ExecuteReader("SELECT * FROM View_Tb_Qm_TaskAbarbeitung_Phone WHERE ISNULL(ISDELETE,0)=0 AND taskId='" + TaskId + "'", null, null, null, CommandType.Text).ToDataSet();
                dTable = dSet.Tables[0].Copy();
                dTable.TableName = "Record";
                ds.Tables.Add(dTable);
                dSet.Dispose();
                dTable.Dispose();
                #endregion
                #region 附件列表
                //strSql = " SELECT FileName,Fix,FilePath,TaskId FROM Tb_Qm_TaskFiles  WHERE TaskId = '" + TaskId + "' ";
                //dSet = Query(strSql);
                dTable = new DataTable();
                dTable.TableName = "Files";
                ds.Tables.Add(dTable);
                dSet.Dispose();
                dTable.Dispose();
                #endregion
                result = JSONHelper.FromString(ds, true, false);

            }
            catch (Exception EX)
            {
                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }
        //GetQmAndEqAndConfigInspectApprovedNew
        public string GetQmAndEqAndConfigInspectApprovedNew(DataRow Row)
        {
            string result = "";
            try
            {
                string Timer = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ItemCode = "", UserCode = "";//时间段
                if (Row.Table.Columns.Contains("Timer") && Row["Timer"].ToString() != "")//当只传一个值时
                {
                    Timer = Convert.ToDateTime(Row["Timer"]).ToString("yyyy-MM-dd HH:mm:ss");
                }
                //ItemCode 项目编号，TaskPId 责任人，BeginDate任务开始时间，endDate任务结束时间  yyyy/MM/dd  TaskStatus='未开始' AND
                if (Row.Table.Columns.Contains("ItemCode") && Row.Table.Columns.Contains("UserCode") && Row.Table.Columns.Contains("Timer"))
                {
                    //&&Row.Table.Columns.Contains("AbarPId")
                    ItemCode = Row["ItemCode"].ToString().Trim();
                    UserCode = Row["UserCode"].ToString().Trim();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(" SELECT count(TaskNo) FROM View_Qm_Phone_QualityInspectList WHERE ISNULL(ISDELETE,0)=0 AND TaskStatus='未开始' AND ItemCode='" + ItemCode + "' AND TaskRoleId IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + UserCode + "'))  ");
                    if (Timer != "")
                    {
                        Timer = Convert.ToDateTime(Timer).ToString("yyyy-MM-dd");
                        strSql.Append(" AND  BeginDateD <='" + Timer + " 00:00:00.000'  AND EndDateD >='" + Timer + " 23:59:59.000' ");
                    }
                    IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    DataTable dt = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                    strSql.Length = 0;
                    strSql.Append(" SELECT COUNT (AbarbeitungId) FROM View_Qm_Phone_AbarbeitungList  WHERE ISNULL(ISDELETE,0)=0 AND CheckResult='整改' AND (addPId = '" + UserCode + "' or  AbarPId ='" + UserCode + "' ) AND ISNULL(IsOk,'0')='0'  ");

                    con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    DataTable dt2 = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.Append("{\"Result\":\"true\",\"data\": {");
                        //任务数量
                        sb.Append("\"TaskQualityCount\":\"");
                        sb.Append(dt.Rows[0][0].ToString());
                        sb.Append("\",");
                        //品质整改数量
                        sb.Append("\"RectificationCount\":\"");
                        if (dt2 != null && dt2.Rows.Count > 0)
                            sb.Append(dt2.Rows[0][0].ToString());
                        else
                            sb.Append("0");
                        sb.Append("\",");
                        //设备维保数量
                        sb.Append("\"MaintenanceTaskCount\":\"");

                        if (Row["UserCode"].ToString() != "" && Row["Timer"].ToString() != "")
                        {
                            Timer = Convert.ToDateTime(Row["Timer"]).ToString("yyyy-MM-dd");
                            strSql.Length = 0;
                            strSql.Append(" SELECT  COUNT (TaskId) FROM View_EQ_WbPatrolTaskExecList WHERE ISNULL(ISDELETE,0)=0 AND CommID='" + ItemCode + "' AND RoleCode IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + UserCode + "'))  ");

                            strSql.Append(" AND CONVERT(nvarchar(100), BeginTime, 23) <='" + Timer + "' AND CONVERT(nvarchar(100), EndTime, 23) >= '" + Timer + "'");
                            strSql.Append(" AND  EndTime >= '" + Timer + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "'");
                            strSql.Append(" AND ISNULL(CheckMan,'') = '' AND  ISNULL(CheckNoto,'') = '' AND  ISNULL(CheckRusult,'') = ''");
                            con = new SqlConnection(PubConstant.hmWyglConnectionString);
                            DataTable dtTask = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                            if (dtTask != null && dtTask.Rows.Count > 0)
                                sb.Append(dtTask.Rows[0][0].ToString());
                        }
                        else
                        {
                            sb.Append("0");
                        }
                        sb.Append("\",");
                        sb.Append("\"PatrolTaskExec\":\"");
                        if (ItemCode != "" && UserCode != "" && Row["Timer"].ToString() != "")
                        {
                            strSql.Length = 0;
                            strSql.Append(" SELECT COUNT(TaskId) FROM View_EQ_PatrolTaskExecList WHERE ISNULL(ISDELETE,0)=0 AND CommID='" + ItemCode + "' AND RoleCode IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + UserCode + "'))  ");
                            strSql.Append(" AND CONVERT(nvarchar(100), BeginTime, 23) <='" + Timer + "' AND CONVERT(nvarchar(100), EndTime, 23) >= '" + Timer + "'");
                            strSql.Append(" AND  EndTime >= '" + Timer + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "'");
                            strSql.Append(" AND Statue = '未完成' ");
                            con = new SqlConnection(PubConstant.hmWyglConnectionString);
                            DataTable dtTask = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                            if (dtTask != null && dtTask.Rows.Count > 0)
                                sb.Append(dtTask.Rows[0][0].ToString());
                        }
                        else
                        {
                            sb.Append("0");
                        }
                        sb.Append("\",");
                        sb.Append("\"ConfigState\":\"");
                        con = new SqlConnection(PubConstant.hmWyglConnectionString);
                        dt = con.ExecuteReader(" SELECT isnull(TimeLimit, 0) as TimeLimit FROM Tb_Qm_TaskParameterSetting where ISNULL(IsDelete,0)=0 ", null, null, null, CommandType.Text).ToDataSet().Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            sb.Append(dt.Rows[0][0].ToString());
                        }
                        else
                        {
                            sb.Append("0");
                        }
                        //result = JSONHelper.FromString(dt);
                        //
                        sb.Append("\"}}");
                        result = sb.ToString();
                    }

                    else
                        result = JSONHelper.FromString(true, "0");

                }
                else
                {
                    result = JSONHelper.FromString(false, "缺少参数ItemCode/UserCode/Timer/AbarPId");
                }
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, EX.ToString());
            }

            return result;
        }

        /// <summary>
        /// 获取任务数量
        /// </summary>
        private string GetTaskCount_hnc(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(Row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            string commId = Row["CommID"].ToString().Trim();
            string timer = DateTime.Now.ToString("yyyy-MM-dd");

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                int TaskQualityCount = 0;
                int RectificationCount = 0;
                int MaintenanceTaskCount = 0;
                int PatrolTaskExec = 0;
                // 品质核查任务数量
                string sql = @"SELECT count(DISTINCT TaskId) FROM View_Qm_Phone_QualityInspectList WHERE ISNULL(ISDELETE,0)=0 AND (TaskStatus='未开始' or TaskStatus='执行中') AND ItemCode=@ItemCode AND TaskRoleId IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode)) AND BeginDateD <= @Timer+' 00:00:00.000' AND EndDateD >= @Timer+' 23:59:59.000'";
                try
                {
                    TaskQualityCount = conn.Query<int>(sql,
                    new { ItemCode = commId, UserCode = Global_Var.UserCode, Timer = timer }).FirstOrDefault();
                }
                catch (Exception)
                {
                    TaskQualityCount = 0;
                }

                // 品质整改任务数量
                sql = @"SELECT COUNT (AbarbeitungId) FROM View_Qm_Phone_AbarbeitungApprovedList  WHERE ISNULL(ISDELETE,0)=0 AND CheckResult='整改' AND ISNULL(ExtendStr,'')='' and (addPId=@UserCode or AbarPId=@UserCode or UndertakingId=@UserCode) AND ISNULL(IsOk,'') !='1' AND ItemCode=@ItemCode ";
                try
                {
                    RectificationCount = conn.Query<int>(sql, new { UserCode = Global_Var.UserCode, ItemCode = commId }).FirstOrDefault();
                }
                catch (Exception)
                {
                    RectificationCount = 0;
                }

                // 设备维保任务数量
                sql = @"SELECT  COUNT (TaskId) FROM View_EQ_WbPatrolTaskExecList WHERE ISNULL(ISDELETE,0)=0 AND CommID=@CommID AND RoleCode IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode))  AND Statue!='已完成' AND Statue!='已关闭' AND CONVERT(nvarchar(100), BeginTime, 23)<=@Timer AND CONVERT(nvarchar(100), EndTime, 23) >=@Timer AND EndTime >= @Timer+@HHmmss AND ISNULL(CheckMan,'')='' AND  ISNULL(CheckNoto,'')='' AND ISNULL(CheckRusult,'')=''";
                try
                {
                    MaintenanceTaskCount = conn.Query<int>(sql,
                    new
                    {
                        UserCode = Global_Var.UserCode,
                        CommID = commId,
                        Timer = timer,
                        HHmmss = DateTime.Now.ToString(" HH:mm:ss")
                    }).FirstOrDefault();
                }
                catch (Exception)
                {
                    MaintenanceTaskCount = 0;
                }

                // 设备巡检任务数量
                sql = @"SELECT COUNT(TaskId) FROM View_EQ_PatrolTaskExecList WHERE ISNULL(ISDELETE,0)= 0 AND CommID=@CommID AND RoleCode IN(SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode)) AND CONVERT(nvarchar(100), BeginTime, 23) <=@Timer AND CONVERT(nvarchar(100), EndTime, 23) >=@Timer AND EndTime >=@Timer+@HHmmss AND Statue != '已完成' AND Statue != '已关闭'";
                try
                {
                    PatrolTaskExec = conn.Query<int>(sql,
                    new
                    {
                        UserCode = Global_Var.UserCode,
                        CommID = commId,
                        Timer = timer,
                        HHmmss = DateTime.Now.ToString(" HH:mm:ss")
                    }).FirstOrDefault();
                }
                catch (Exception)
                {
                    PatrolTaskExec = 0;
                }

                // 装修巡检任务数量
                sql = @"SELECT count(*) FROM view_HSPR_RenoCust_Filter WHERE CommID=@CommID AND RenoStatus <> '取消'";
                int RenoCustCount = conn.Query<int>(sql, new { CommID = commId }).FirstOrDefault();

                // 入伙待办数量
                sql = @"SELECT count(1) FROM View_Tb_Occupation_OccupationBacklog
                            WHERE OccupationState='待入伙' AND CommID=@CommID AND ISNULL(IsDelete,0)=0";
                int OccupationCount = conn.Query<int>(sql, new { CommID = commId }).FirstOrDefault();

                // 进场待办数量
                sql = @"SELECT count(1) FROM view_HSPR_GetReadyList_Filter x 
                            WHERE ISNULL(ApproachState, '')='' AND x.CommID=@CommID AND ISNULL(x.IsDelete,0)=0";
                int ContCount = conn.Query<int>(sql, new { CommID = commId }).FirstOrDefault();
                // 退场待办数量
                sql = "SELECT COUNT(1) FROM VIEW_Tb_Customer_Withdrawal WHERE State = @State AND CommID = @CommID";
                int WithdrawalCount = conn.QueryFirstOrDefault<int>(sql, new { State = "待退场", CommID = commId });
                // 复验待办数量
                sql = @"SELECT count(*) FROM Tb_HSPR_IncidentAccept a
                            LEFT JOIN Tb_HSPR_HousingInspDetail b ON b.IID=a.HousingInspDetailID
                            LEFT JOIN Tb_HSPR_HousingProj c ON b.ProjID=c.ProjID
                            WHERE isnull(a.DealState,0)=1 AND a.CommID=@CommID
                                AND a.HousingInspDetailID IS NOT NULL 
                                AND (isnull(b.Review,0)=0 OR b.Review=2)";
                int ReviewCount = conn.Query<int>(sql, new { CommID = commId }).FirstOrDefault();

                int CPCount = 0;
                try
                {
                    // 综合巡查数量
                    sql = @"SELECT count(*) FROM View_Tb_CP_TaskPlanMaintenance
                            WHERE isnull(IsDelete,0)=0 AND isnull(IsClose,0)=0 AND isnull(PlanState,0)<>2 AND CommID=@CommID
                                AND convert(nvarchar(30), BeginTime, 20)<=@DateTime
                                AND convert(nvarchar(30), EndTime, 20)>=@DateTime
                                AND TaskRoleCode IN(SELECT a.RoleCode FROM Tb_Sys_UserRole a LEFT JOIN Tb_Sys_Role b ON a.RoleCode=b.RoleCode
                                                    WHERE a.UserCode=@UserCode AND b.SysRoleCode IS NOT NULL AND b.SysRoleCode<>'')";
                    CPCount = conn.Query<int>(sql, new
                    {
                        UserCode = Global_Var.LoginUserCode,
                        DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        CommID = commId,
                    }).FirstOrDefault();
                }
                catch (Exception)
                {
                    CPCount = 0;
                }

                sql = @"SELECT isnull(TimeLimit, 0) as TimeLimit FROM Tb_Qm_TaskParameterSetting where ISNULL(IsDelete,0)=0";
                int ConfigState = conn.Query<int>(sql).FirstOrDefault();

                Dictionary<string, int> dic = new Dictionary<string, int>();
                dic.Add("TaskQualityCount", TaskQualityCount);
                dic.Add("RectificationCount", RectificationCount);
                dic.Add("MaintenanceTaskCount", MaintenanceTaskCount);
                dic.Add("PatrolTaskExec", PatrolTaskExec);
                dic.Add("RenoCustCount", RenoCustCount);
                dic.Add("OccupationCount", OccupationCount);
                dic.Add("ContCount", ContCount);
                dic.Add("WithdrawalCount", WithdrawalCount);
                dic.Add("ReviewCount", ReviewCount);
                dic.Add("CPCount", CPCount);
                dic.Add("ConfigState", ConfigState);

                return new ApiResult(true, dic).toJson();
            }
        }

        /// <summary>
        /// 获取任务数量
        /// </summary>
        private string GetTaskCount(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(Row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (Global_Var.LoginCorpID == "1975")
            {
                return GetTaskCount_hnc(Row);
            }


            string commId = Row["CommID"].ToString().Trim();
            string timer = DateTime.Now.ToString("yyyy-MM-dd");
            int month = DateTime.Now.Month;
            string doMonthSql = string.Format(" AND (doMonth IS NULL OR doMonth LIKE '%{0}%' OR LTRIM(RTRIM(doMonth))='')", month);

            // 判断是否是金辉系统,需要进行doMonth判断
            bool isJinHui = false;
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"select count(1) from syscolumns where id=object_id('Tb_EQ_WbPollingPlan') and name='doMonth'";
                if (conn.Query<int>(sql).FirstOrDefault() > 0)
                {
                    isJinHui = true;
                }

                // 品质核查任务数量
                sql = @"SELECT count(DISTINCT TaskId) FROM View_Qm_Phone_QualityInspectList WHERE ISNULL(ISDELETE,0)=0 
                        AND (TaskStatus='未开始' or TaskStatus='执行中') AND ItemCode=@ItemCode 
                        AND TaskRoleId IN (SELECT SysRoleCode FROM Tb_Sys_Role 
                        WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode)) 
                        AND BeginDateD <= @Timer+' 00:00:00.000' AND EndDateD >= @Timer+' 23:59:59.000'";

                if (Global_Var.LoginCorpID == "1971")
                {
                    sql += " AND isnull(IsShanchu,0)=0";
                }

                int TaskQualityCount = conn.Query<int>(sql,
                    new { ItemCode = commId, UserCode = Global_Var.UserCode, Timer = timer }).FirstOrDefault();

                // 品质整改任务数量
                sql = @"SELECT COUNT (AbarbeitungId) FROM View_Qm_Phone_AbarbeitungApprovedList  
                        WHERE ISNULL(ISDELETE,0)=0 AND CheckResult='整改' AND ISNULL(ExtendStr,'')='' 
                        and (addPId=@UserCode or AbarPId=@UserCode or UndertakingId=@UserCode) 
                        AND ISNULL(IsOk,'') !='1' AND ItemCode=@ItemCode ";
                int RectificationCount = conn.Query<int>(sql, new { UserCode = Global_Var.UserCode, ItemCode = commId }).FirstOrDefault();

                // 设备维保任务数量
                sql = @"SELECT SUM(count1) FROM( 
                SELECT COUNT(TaskId) AS count1 FROM View_EQ_WbPatrolTaskExec_Phone_List WHERE ISNULL(ISDELETE,0)=0 AND CommID=@CommID AND RoleCode IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode))  AND Statue!='已完成' AND Statue!='已关闭' AND CONVERT(nvarchar(100), BeginTime, 23)<=@Timer AND CONVERT(nvarchar(100), EndTime, 23) >=@Timer AND EndTime >= @Timer+@HHmmss AND ISNULL(CheckMan,'')='' AND  ISNULL(CheckNoto,'')='' AND ISNULL(CheckRusult,'')='' 
                    AND datediff(m, BeginTime, EndTime)<=1
                  Union
                SELECT COUNT(TaskId) AS count1 FROM View_EQ_WbPatrolTaskExec_Phone_List WHERE ISNULL(ISDELETE,0)=0 AND CommID=@CommID AND RoleCode IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode))  AND Statue!='已完成' AND Statue!='已关闭' AND CONVERT(nvarchar(100), BeginTime, 23)<=@Timer AND CONVERT(nvarchar(100), EndTime, 23) >=@Timer AND EndTime >= @Timer+@HHmmss AND ISNULL(CheckMan,'')='' AND  ISNULL(CheckNoto,'')='' AND ISNULL(CheckRusult,'')=''
                    AND datediff(m, BeginTime, EndTime)>1 ";
                if (isJinHui && Global_Var.CorpID == "1329")
                {
                    // 如果是金辉,需要加上doMonth判断(执行月份)
                    sql += doMonthSql;
                }
                sql += ") AS t";
                int MaintenanceTaskCount = conn.Query<int>(sql,
                    new
                    {
                        UserCode = Global_Var.UserCode,
                        CommID = commId,
                        Timer = timer,
                        HHmmss = DateTime.Now.ToString(" HH:mm:ss"),
                        doMonth = ""
                    }).FirstOrDefault();

                // 合景，使用新接口
                //if (Global_Var.LoginCorpID == "1862")
                //{
                //    MaintenanceTaskCount = 0;
                //}

                // 设备巡检任务数量
                sql = @"
                SELECT SUM(count1) FROM( 
                SELECT COUNT(TaskId) AS count1 FROM View_EQ_PatrolTaskExec_Phone_List WHERE ISNULL(ISDELETE,0)= 0 AND CommID=@CommID AND RoleCode IN(SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode)) AND CONVERT(nvarchar(100), BeginTime, 23) <=@Timer AND CONVERT(nvarchar(100), EndTime, 23) >=@Timer AND EndTime >=@Timer+@HHmmss AND Statue != '已完成' AND Statue != '已关闭' 
                    AND datediff(m, BeginTime, EndTime)<=1
                  Union
                SELECT COUNT(TaskId) AS count1 FROM View_EQ_PatrolTaskExec_Phone_List WHERE ISNULL(ISDELETE,0)= 0 AND CommID=@CommID AND RoleCode IN(SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode)) AND CONVERT(nvarchar(100), BeginTime, 23) <=@Timer AND CONVERT(nvarchar(100), EndTime, 23) >=@Timer AND EndTime >=@Timer+@HHmmss AND Statue != '已完成' AND Statue != '已关闭' 
                AND datediff(m, BeginTime, EndTime)>1 ";
                if (isJinHui && Global_Var.CorpID == "1329")
                {
                    // 如果是金辉,需要加上doMonth判断(执行月份)
                    sql += doMonthSql;
                }
                sql += ") AS t";
                int PatrolTaskExec = conn.Query<int>(sql,
                    new
                    {
                        UserCode = Global_Var.UserCode,
                        CommID = commId,
                        Timer = timer,
                        HHmmss = DateTime.Now.ToString(" HH:mm:ss")
                    }).FirstOrDefault();
                
                // 合景，使用新接口
                //if (Global_Var.LoginCorpID == "1862")
                //{
                //    PatrolTaskExec = 0;
                //}

                int CPCount = 0;
                try
                {
                    // 综合巡查数量
                    sql = @"SELECT count(*) FROM View_Tb_CP_TaskPlanMaintenance
                            WHERE isnull(IsDelete,0)=0 AND isnull(IsClose,0)=0 AND isnull(PlanState,0)<>2 AND CommID=@CommID
                                AND convert(nvarchar(30), BeginTime, 20)<=@DateTime
                                AND convert(nvarchar(30), EndTime, 20)>=@DateTime
                                AND TaskRoleCode IN(SELECT a.RoleCode FROM Tb_Sys_UserRole a LEFT JOIN Tb_Sys_Role b ON a.RoleCode=b.RoleCode
                                                    WHERE a.UserCode=@UserCode AND b.SysRoleCode IS NOT NULL AND b.SysRoleCode<>'')";
                    CPCount = conn.Query<int>(sql, new
                    {
                        UserCode = Global_Var.LoginUserCode,
                        DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        CommID = commId,
                    }).FirstOrDefault();
                }
                catch (Exception)
                {
                    CPCount = 0;
                }

                #region 分户查验数量
                int HouseInspectCount = 0;
                try
                {
                    // 获取规则,获取开始时间在今天并且未完成的任务，不考虑过期任务，过期任务需要下载作为已逾期任务继续执行
                    sql = @"SELECT COUNT(*) FROM View_Tb_HI_Task WHERE CommID = @CommID AND ISNULL(IsDelete,0) = 0 
                                AND RoleCode IN( SELECT RoleCode FROM view_Sys_UserRole_Filter WHERE UserCode=@UserCode) 
                                AND TaskStatus != '已完成' AND BeginTime <= getdate()";
                    HouseInspectCount = conn.QueryFirstOrDefault<int>(sql, new { CommID = commId, UserCode = Global_Var.LoginUserCode });
                }
                catch (Exception ex)
                {
                    
                }
                #endregion

                #region 分户查验整改数量
                int HouseInspectRectificationCount = 0;
                try
                {
                    // 获取规则,获取开始时间在今天并且未完成的任务，不考虑过期任务，过期任务需要下载作为已逾期任务继续执行
                    sql = @"SELECT COUNT(*) FROM View_Tb_HI_Task AS a WHERE CommID=@CommID AND ISNULL(IsDelete, 0) = 0 
                                AND TaskId IN(SELECT TaskId FROM Tb_HI_TaskStandard WHERE IsQualified=0 AND ISNULL(CheckDate,'')='' 
                                    AND ISNULL(CheckPid,'')='' AND RoleCode IN(SELECT RoleCode FROM view_Sys_UserRole_Filter 
                                        WHERE UserCode=@UserCode))";
                    HouseInspectRectificationCount = conn.QueryFirstOrDefault<int>(sql, new { CommID = commId, UserCode = Global_Var.LoginUserCode });

                }
                catch (Exception ex)
                {
                    
                }
                #endregion

                sql = @"SELECT isnull(TimeLimit, 0) as TimeLimit FROM Tb_Qm_TaskParameterSetting where ISNULL(IsDelete,0)=0";
                int ConfigState = conn.Query<int>(sql).FirstOrDefault();

                Dictionary<string, int> dic = new Dictionary<string, int>();
                dic.Add("TaskQualityCount", TaskQualityCount);
                dic.Add("RectificationCount", RectificationCount);
                dic.Add("MaintenanceTaskCount", MaintenanceTaskCount);
                dic.Add("PatrolTaskExec", PatrolTaskExec);
                dic.Add("CPCount", CPCount);
                dic.Add("HouseInspectCount", HouseInspectCount);
                dic.Add("HouseInspectRectificationCount", HouseInspectRectificationCount);
                dic.Add("ConfigState", ConfigState);

                return new ApiResult(true, dic).toJson();
            }
        }

        public string GetQualityInspectListApprovedCount(DataRow Row)
        {
            string result = "";
            try
            {
                using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    string Timer = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ItemCode = "", UserCode = "";//时间段
                    if (Row.Table.Columns.Contains("Timer") && Row["Timer"].ToString() != "")//当只传一个值时
                    {
                        Timer = Convert.ToDateTime(Row["Timer"]).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    //ItemCode 项目编号，TaskPId 责任人，BeginDate任务开始时间，endDate任务结束时间  yyyy/MM/dd  TaskStatus='未开始' AND
                    if (Row.Table.Columns.Contains("ItemCode") && Row.Table.Columns.Contains("UserCode") && Row.Table.Columns.Contains("Timer"))
                    {
                        //&&Row.Table.Columns.Contains("AbarPId")
                        ItemCode = Row["ItemCode"].ToString().Trim();
                        UserCode = Row["UserCode"].ToString().Trim();
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append(" SELECT count(TaskNo) FROM View_Qm_Phone_QualityInspectList WHERE ISNULL(ISDELETE,0)=0 AND (TaskStatus='未开始' or TaskStatus='执行中') AND ItemCode='" + ItemCode + "' AND TaskRoleId IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + UserCode + "'))  ");
                        if (Timer != "")
                        {
                            Timer = Convert.ToDateTime(Timer).ToString("yyyy-MM-dd");
                            strSql.Append(" AND  BeginDateD <='" + Timer + " 00:00:00.000'  AND EndDateD >='" + Timer + " 23:59:59.000' ");
                        }
                        //IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
                        //DataTable dt = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                        var count1 = conn.Query<int>(strSql.ToString()).FirstOrDefault();
                        strSql.Length = 0;
                        //1971
                        string[] accounts = null;
                        if (Row.Table.Columns.Contains("Account") && Row["Account"].ToString() != "")//当只传一个值时
                        {
                            accounts = Row["Account"].ToString().Split('-');
                            if (accounts[0] == "1971")//如果是敏捷项目将不会反悔整改还未开始的验收任务
                            {
                                strSql.Append(" SELECT COUNT (AbarbeitungId) FROM View_Qm_Phone_AbarbeitungApprovedList  WHERE ISNULL(ISDELETE,0)=0 AND CheckResult='整改' AND ISNULL(ExtendStr,'')='' and ((addPId = '" + UserCode + "' AND ReduceTime!='') or  AbarPId ='" + UserCode + "' or UndertakingId='" + UserCode + "' )  ");
                            }
                            else
                                strSql.Append(" SELECT COUNT (AbarbeitungId) FROM View_Qm_Phone_AbarbeitungApprovedList  WHERE ISNULL(ISDELETE,0)=0 AND CheckResult='整改' AND ISNULL(ExtendStr,'')='' and (addPId = '" + UserCode + "' or  AbarPId ='" + UserCode + "' or UndertakingId='" + UserCode + "' )  ");

                        }
                        else
                            strSql.Append(" SELECT COUNT (AbarbeitungId) FROM View_Qm_Phone_AbarbeitungApprovedList  WHERE ISNULL(ISDELETE,0)=0 AND CheckResult='整改' AND ISNULL(ExtendStr,'')='' and (addPId = '" + UserCode + "' or  AbarPId ='" + UserCode + "' or UndertakingId='" + UserCode + "' )  ");

                        strSql.Append(" AND ISNULL(IsOk,'') !='1' ");
                        strSql.Append(" AND ItemCode='" + Row["ItemCode"].ToString().Trim() + "' ");
                        //con = new SqlConnection(PubConstant.hmWyglConnectionString);
                        //DataTable dt2 = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                        var count2 = conn.Query<int>(strSql.ToString()).FirstOrDefault();

                        //if (dt != null && dt.Rows.Count > 0)
                        {
                            StringBuilder sb = new StringBuilder();

                            sb.Append("{\"Result\":\"true\",\"data\": {");
                            //任务数量
                            sb.Append("\"TaskQualityCount\":\"");
                            //sb.Append(dt.Rows[0][0].ToString());
                            sb.Append(count1.ToString());
                            sb.Append("\",");
                            //品质整改数量
                            sb.Append("\"RectificationCount\":\"");
                            //if (dt2 != null && dt2.Rows.Count > 0)
                            //    sb.Append(dt2.Rows[0][0].ToString());
                            //else
                            //    sb.Append("0");
                            sb.Append(count2.ToString());
                            sb.Append("\",");
                            //设备维保数量
                            sb.Append("\"MaintenanceTaskCount\":\"");

                            if (Row["UserCode"].ToString() != "" && Row["Timer"].ToString() != "")
                            {
                                Timer = Convert.ToDateTime(Row["Timer"]).ToString("yyyy-MM-dd");
                                strSql.Length = 0;
                                strSql.Append(" SELECT  COUNT (TaskId) FROM ");
                                if (accounts.Length > 0 && accounts[0] == "1329")
                                {
                                    strSql.Append(" View_EQ_WbPatrolTaskExecListNew");
                                }
                                else
                                {
                                    strSql.Append(" View_EQ_WbPatrolTaskExecList");
                                }
                                strSql.Append(" WHERE ISNULL(ISDELETE,0)=0 AND CommID='" + ItemCode + "' AND RoleCode IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + UserCode + "'))  ");
                                strSql.Append(" AND Statue != '已完成' AND Statue != '已关闭' ");
                                strSql.Append(" AND CONVERT(nvarchar(100), BeginTime, 23) <='" + Timer + "' AND CONVERT(nvarchar(100), EndTime, 23) >= '" + Timer + "'");
                                strSql.Append(" AND  EndTime >= '" + Timer + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "'");
                                strSql.Append(" AND ISNULL(CheckMan,'') = '' AND  ISNULL(CheckNoto,'') = '' AND  ISNULL(CheckRusult,'') = ''");
                                //con = new SqlConnection(PubConstant.hmWyglConnectionString);
                                //DataTable dtTask = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                                //if (dtTask != null && dtTask.Rows.Count > 0)
                                //    sb.Append(dtTask.Rows[0][0].ToString());
                                var count3 = conn.Query<int>(strSql.ToString()).FirstOrDefault();
                                sb.Append(count3.ToString());
                            }
                            else
                            {
                                sb.Append("0");
                            }
                            sb.Append("\",");
                            sb.Append("\"PatrolTaskExec\":\"");
                            if (ItemCode != "" && UserCode != "" && Row["Timer"].ToString() != "")
                            {
                                strSql.Length = 0;
                                strSql.Append(" SELECT  COUNT (TaskId) FROM ");
                                if (accounts.Length > 0 && accounts[0] == "1329")
                                {
                                    strSql.Append(" View_EQ_PatrolTaskExecListNew");
                                }
                                else
                                {
                                    strSql.Append(" View_EQ_PatrolTaskExecList");
                                }
                                strSql.Append(" WHERE ISNULL(ISDELETE,0)=0 AND CommID='" + ItemCode + "' AND RoleCode IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + UserCode + "'))  ");
                                strSql.Append(" AND CONVERT(nvarchar(100), BeginTime, 23) <='" + Timer + "' AND CONVERT(nvarchar(100), EndTime, 23) >= '" + Timer + "'");
                                strSql.Append(" AND  EndTime >= '" + Timer + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "'");
                                strSql.Append(" AND Statue != '已完成' AND Statue != '已关闭' ");
                                //con = new SqlConnection(PubConstant.hmWyglConnectionString);
                                //DataTable dtTask = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                                //if (dtTask != null && dtTask.Rows.Count > 0)
                                //    sb.Append(dtTask.Rows[0][0].ToString());
                                var count4 = conn.Query<int>(strSql.ToString()).FirstOrDefault();
                                sb.Append(count4.ToString());
                            }
                            else
                            {
                                sb.Append("0");
                            }
                            sb.Append("\",");
                            sb.Append("\"ConfigState\":\"");
                            //con = new SqlConnection(PubConstant.hmWyglConnectionString);
                            //dt = con.ExecuteReader(" SELECT isnull(TimeLimit, 0) as TimeLimit FROM Tb_Qm_TaskParameterSetting where ISNULL(IsDelete,0)=0 ", null, null, null, CommandType.Text).ToDataSet().Tables[0];
                            //if (dt != null && dt.Rows.Count > 0)
                            //{
                            //    sb.Append(dt.Rows[0][0].ToString());
                            //}
                            //else
                            //{
                            //    sb.Append("0");
                            //}
                            //result = JSONHelper.FromString(dt);
                            //
                            var count5 = conn.Query<int>(strSql.ToString()).FirstOrDefault();
                            sb.Append(count5.ToString());
                            sb.Append("\"}}");
                            result = sb.ToString();
                        }
                    }
                    else
                    {
                        result = JSONHelper.FromString(false, "缺少参数ItemCode/UserCode/Timer/AbarPId");
                    }
                }
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, EX.ToString());
            }

            return result;
        }


        public string GetQualityInspectListApprovedCount_hnc(DataRow Row)
        {
            try
            {
                string Timer = DateTime.Now.ToString("yyyy-MM-dd"), ItemCode = "", UserCode = "";//时间段
                                                                                                 //ItemCode 项目编号，TaskPId 责任人，BeginDate任务开始时间，endDate任务结束时间  yyyy/MM/dd  TaskStatus='未开始' AND
                if (!Row.Table.Columns.Contains("ItemCode") || !Row.Table.Columns.Contains("UserCode") || !Row.Table.Columns.Contains("Timer"))
                {
                    return new ApiResult(false, "缺少参数ItemCode/UserCode/Timer/AbarPId").toJson();
                }
                if (Row["Timer"].ToString() != "")//当只传一个值时
                {
                    Timer = Convert.ToDateTime(Row["Timer"]).ToString("yyyy-MM-dd");
                }
                //&&Row.Table.Columns.Contains("AbarPId")
                ItemCode = Row["ItemCode"].ToString().Trim();
                UserCode = Row["UserCode"].ToString().Trim();

                Dictionary<string, object> dic = new Dictionary<string, object>();


                using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    if (ItemCode != "" && UserCode != "0")
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@CommID", ItemCode, DbType.Int32);
                        dic.Add("RenoCustCount", con.QuerySingle<int>("SELECT count(*) FROM view_HSPR_RenoCust_Filter WHERE CommID = @CommID", parameters, null, null, CommandType.Text));
                    }
                    else
                    {
                        dic.Add("RenoCustCount", 0);
                    }
                }
                using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    if (ItemCode != "" && UserCode != "0")
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("OccupationState", "待入伙", DbType.String, ParameterDirection.Input, 10);
                        parameters.Add("CommID", ItemCode);
                        dic.Add("InspectionHomeCount", con.QuerySingle<int>("SELECT COUNT(*) FROM (SELECT RoomSign, CustName, RoomID, CustID, MobilePhone, PaperCode FROM View_Tb_Occupation_OccupationBacklog WHERE 1 = 1 AND OccupationState = @OccupationState AND CommID = @CommID AND ISNULL(IsDelete, 0) = 0 UNION SELECT RoomSign, CustName, RoomID, CustID, '' AS MobilePhone, '' AS PaperCode FROM view_HSPR_GetReadyList_Filter WHERE 1 = 1 AND ISNULL( ApproachState, '' ) = '' AND CommID = @CommID AND ISNULL(IsDelete, 0) = 0 UNION SELECT RoomSign, CustName, RoomID, CustID, MobilePhone, PaperCode  FROM View_HSPR_HousingInsp_Review WHERE DealState=1 and review = 0 AND CommID =  @CommID) AS b", parameters, null, null, CommandType.Text));
                    }
                    else
                    {
                        dic.Add("InspectionHomeCount", 0);
                    }
                }
                using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    if (ItemCode != "" && UserCode != "0")
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("CommID", ItemCode);
                        DataTable dt = con.ExecuteReader("SELECT ProjID, HS_Projects, HS_Standards, Memo FROM Tb_HSPR_HousingProj WHERE CommID = @CommID", parameters, null, null, CommandType.Text).ToDataSet().Tables[0];
                        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                        if (null != dt && dt.Rows.Count > 0)
                        {
                            Dictionary<string, object> tempDic;
                            foreach (DataRow item in dt.Rows)
                            {
                                tempDic = new Dictionary<string, object>();
                                tempDic.Add("ProjID", item["ProjID"]);
                                tempDic.Add("HS_Projects", item["HS_Projects"]);
                                tempDic.Add("HS_Standards", item["HS_Standards"]);
                                tempDic.Add("Memo", item["Memo"]);
                                list.Add(tempDic);
                            }
                        }
                        dic.Add("InspectionHomeStandardList", list);
                    }
                    else
                    {
                        dic.Add("InspectionHomeCount", 0);
                    }
                }

                return new ApiResult(true, dic).toJson();

            }
            catch (Exception EX)
            {
                return new ApiResult(false, EX.Message).toJson();
            }

        }
        public string GetQualityInspectListCount(DataRow Row)
        {
            string result = "";
            try
            {
                string Timer = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ItemCode = "", UserCode = "";//时间段
                if (Row.Table.Columns.Contains("Timer") && Row["Timer"].ToString() != "")//当只传一个值时
                {
                    Timer = Convert.ToDateTime(Row["Timer"]).ToString("yyyy-MM-dd HH:mm:ss");
                }
                //ItemCode 项目编号，TaskPId 责任人，BeginDate任务开始时间，endDate任务结束时间  yyyy/MM/dd  TaskStatus='未开始' AND
                if (Row.Table.Columns.Contains("ItemCode") && Row.Table.Columns.Contains("UserCode") && Row.Table.Columns.Contains("Timer"))
                {
                    //&&Row.Table.Columns.Contains("AbarPId")
                    ItemCode = Row["ItemCode"].ToString().Trim();
                    UserCode = Row["UserCode"].ToString().Trim();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(" SELECT count(TaskNo) FROM View_Qm_Phone_QualityInspectList WHERE ISNULL(ISDELETE,0)=0 AND TaskStatus='未开始' AND ItemCode='" + ItemCode + "' AND TaskRoleId IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + UserCode + "'))  ");
                    if (Timer != "")
                    {
                        Timer = Convert.ToDateTime(Timer).ToString("yyyy-MM-dd");
                        strSql.Append(" AND  BeginDateD <='" + Timer + " 00:00:00.000'  AND EndDateD >='" + Timer + " 23:59:59.000' ");
                    }
                    IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    DataTable dt = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                    strSql.Length = 0;
                    strSql.Append(" SELECT COUNT (AbarbeitungId) FROM View_Qm_Phone_AbarbeitungList  WHERE ISNULL(ISDELETE,0)=0 AND CheckResult='整改' AND (addPId = '" + UserCode + "' or  AbarPId ='" + UserCode + "' ) AND ISNULL(IsOk,'0')='0'  ");

                    con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    DataTable dt2 = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.Append("{\"Result\":\"true\",\"data\": {");
                        //任务数量
                        sb.Append("\"TaskQualityCount\":\"");
                        sb.Append(dt.Rows[0][0].ToString());
                        sb.Append("\",");
                        //品质整改数量
                        sb.Append("\"RectificationCount\":\"");
                        if (dt2 != null && dt2.Rows.Count > 0)
                            sb.Append(dt2.Rows[0][0].ToString());
                        else
                            sb.Append("0");
                        sb.Append("\",");
                        //设备维保数量
                        sb.Append("\"MaintenanceTaskCount\":\"");

                        if (Row["UserCode"].ToString() != "" && Row["Timer"].ToString() != "")
                        {
                            Timer = Convert.ToDateTime(Row["Timer"]).ToString("yyyy-MM-dd");
                            strSql.Length = 0;
                            strSql.Append(" SELECT  COUNT (TaskId) FROM View_EQ_WbPatrolTaskExecList WHERE ISNULL(ISDELETE,0)=0 AND CommID='" + ItemCode + "' AND RoleCode IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + UserCode + "'))  ");
                            strSql.Append(" AND Statue != '已完成' AND Statue != '已关闭' ");
                            strSql.Append(" AND CONVERT(nvarchar(100), BeginTime, 23) <='" + Timer + "' AND CONVERT(nvarchar(100), EndTime, 23) >= '" + Timer + "'");
                            strSql.Append(" AND  EndTime >= '" + Timer + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "'");
                            strSql.Append(" AND ISNULL(CheckMan,'') = '' AND  ISNULL(CheckNoto,'') = '' AND  ISNULL(CheckRusult,'') = ''");
                            con = new SqlConnection(PubConstant.hmWyglConnectionString);
                            DataTable dtTask = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                            if (dtTask != null && dtTask.Rows.Count > 0)
                                sb.Append(dtTask.Rows[0][0].ToString());
                        }
                        else
                        {
                            sb.Append("0");
                        }
                        sb.Append("\",");
                        sb.Append("\"PatrolTaskExec\":\"");
                        if (ItemCode != "" && UserCode != "" && Row["Timer"].ToString() != "")
                        {
                            strSql.Length = 0;
                            strSql.Append(" SELECT COUNT(TaskId) FROM View_EQ_PatrolTaskExecList WHERE ISNULL(ISDELETE,0)=0 AND CommID='" + ItemCode + "' AND RoleCode IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + UserCode + "'))  ");
                            strSql.Append(" AND CONVERT(nvarchar(100), BeginTime, 23) <='" + Timer + "' AND CONVERT(nvarchar(100), EndTime, 23) >= '" + Timer + "'");
                            strSql.Append(" AND  EndTime >= '" + Timer + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "'");
                            strSql.Append(" AND Statue != '已完成' AND Statue != '已关闭' ");
                            con = new SqlConnection(PubConstant.hmWyglConnectionString);
                            DataTable dtTask = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                            if (dtTask != null && dtTask.Rows.Count > 0)
                                sb.Append(dtTask.Rows[0][0].ToString());
                        }
                        else
                        {
                            sb.Append("0");
                        }
                        sb.Append("\",");
                        sb.Append("\"ConfigState\":\"");
                        con = new SqlConnection(PubConstant.hmWyglConnectionString);
                        dt = con.ExecuteReader(" SELECT isnull(TimeLimit, 0) as TimeLimit FROM Tb_Qm_TaskParameterSetting where ISNULL(IsDelete,0)=0 ", null, null, null, CommandType.Text).ToDataSet().Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            sb.Append(dt.Rows[0][0].ToString());
                        }
                        else
                        {
                            sb.Append("0");
                        }
                        //result = JSONHelper.FromString(dt);
                        //
                        sb.Append("\"}}");
                        result = sb.ToString();
                    }

                    else
                        result = JSONHelper.FromString(true, "0");

                }
                else
                {
                    result = JSONHelper.FromString(false, "缺少参数ItemCode/UserCode/Timer/AbarPId");
                }
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, EX.ToString());
            }

            return result;
        }
        /// <summary>
        /// 任务列表
        /// </summary>
        public string GetQualityInspectList(DataRow Row)
        {
            string result = "";
            try
            {
                string Timer = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");///时间段
                if (Row.Table.Columns.Contains("Timer") && Row["Timer"].ToString() != "")//当只传一个值时
                {
                    Timer = Convert.ToDateTime(Row["Timer"]).ToString();
                }
                //ItemCode 项目编号，TaskPId 责任人，BeginDate任务开始时间，endDate任务结束时间  yyyy/MM/dd  TaskStatus='未开始' AND
                if (Row.Table.Columns.Contains("ItemCode") && Row.Table.Columns.Contains("UserCode"))
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(" SELECT * FROM View_Qm_Phone_QualityInspectList WHERE ISNULL(ISDELETE,0)=0 and TaskStatus='未开始' AND ItemCode='" + Row["ItemCode"].ToString().Trim() + "' AND TaskRoleId IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + Row["UserCode"].ToString().Trim() + "'))  ");
                    if (Timer != "")
                    {
                        Timer = Convert.ToDateTime(Timer).ToString("yyyy-MM-dd");
                        strSql.Append(" AND  BeginDateD <='" + Timer + " 00:00:00.000'  AND EndDateD >='" + Timer + " 23:59:59.000' ");
                    }
                    int PageIndex = 1;
                    int PageSize = 60;

                    if (Row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(Row["PageIndex"].ToString()) > 0)
                    {
                        PageIndex = AppGlobal.StrToInt(Row["PageIndex"].ToString());
                    }
                    if (Row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(Row["PageSize"].ToString()) > 0)
                    {
                        PageSize = AppGlobal.StrToInt(Row["PageSize"].ToString());
                    }
                    //strSql.Append(" ORDER BY BeginDate ASC,TaskNo ASC ");
                    //IDbConnection con = new SqlConnection(PubConstant.PrivateConnectionString);
                    int PageCount = 0, Counts = 0;
                    DataTable dt = GetList(out PageCount, out Counts, strSql.ToString(), PageIndex, PageSize, "BeginDate ASC,TaskNo ", 1, "TaskId", PubConstant.hmWyglConnectionString, "*").Tables[0];
                    // DataTable dt = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                    dt.Columns.Add("DataDetail", typeof(string));
                    HM.Model.Qm.Tb_Interface_Record Interface_Record = new HM.Model.Qm.Tb_Interface_Record();
                    Interface_Record.Type = "QualityTask";
                    Interface_Record.GetDate = DateTime.Now;
                    Interface_Record.ItemCode = Row["ItemCode"].ToString().Trim();
                    Interface_Record.UserCode = Row["UserCode"].ToString().Trim();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["DataDetail"] = GetQualityInspectSelDetail(dt.Rows[i]["TaskId"].ToString().Trim(), dt.Rows[i]["ItemCode"].ToString().Trim());
                        IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
                        Interface_Record.Id = Guid.NewGuid().ToString();
                        Interface_Record.TaskId = dt.Rows[i]["TaskId"].ToString().Trim();
                        //任务记录
                        con.Insert<HM.Model.Qm.Tb_Interface_Record>(Interface_Record);
                    }
                    result = JSONHelper.FromString(dt);
                }
                else
                {
                    result = JSONHelper.FromString(false, "缺少参数ItemCode/UserCode/Timer");
                }
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, EX.ToString());
            }

            return result;
        }
        #region 
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="PageCount">总页数</param>
        /// <param name="Counts">总条数</param>
        /// <param name="StrCondition">执行语句</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">每页多少条</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">升序/降序</param>
        /// <param name="ID">主键</param>
        /// <param name="ConStr">链接字符串</param>
        /// <param name="FldName">显示字段</param>
        /// <returns></returns>
        private static DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort, string ID, string ConStr, string FldName)
        {
            PageCount = 0;
            Counts = 0;
            IDbConnection con = new SqlConnection(ConStr);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FldName", FldName);
            parameters.Add("@PageSize", PageSize);
            parameters.Add("@PageIndex", PageIndex);
            parameters.Add("@FldSort", SortField);
            parameters.Add("@Sort", Sort);

            parameters.Add("@StrCondition", StrCondition);
            parameters.Add("@Id", ID);
            parameters.Add("@PageCount", PageCount);
            parameters.Add("@Counts", Counts);


            DataSet Ds = con.ExecuteReader("Proc_System_TurnPage", parameters, null, null, CommandType.StoredProcedure).ToDataSet();

            return Ds;
        }
        #endregion
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

        public void AddQuanlityFiles(string taskId, string phototime, string fix, string addpid, string fileName, string Path)
        {
            //public void AddQuanlityFiles(string taskId, string phototime, string fix, string addpid, string fileName, string Path, Common.Transfer Trans)
            //if (!IsLogin(ref Trans))
            //    return; 

            AppGlobal.GetHmWyglConnection();
            string mp3 = ",wav,amr,m4a,aac";
            string img = ",jpeg,peg,jpg,bmp";
            string mp4 = ",mp4,avi,3gp";


            HM.BLL.Qm.Bll_Tb_Qm_TaskFiles bll = new HM.BLL.Qm.Bll_Tb_Qm_TaskFiles();
            HM.Model.Qm.Tb_Qm_TaskFiles model = new HM.Model.Qm.Tb_Qm_TaskFiles();
            model.Id = Guid.NewGuid().ToString();
            if (mp3.Contains(fix.ToLower()))
                model.Fix = "mp3";
            else if (mp4.Contains(fix.ToLower()))
                model.Fix = "video";
            else
                model.Fix = "img";

            model.PhotoTime = Convert.ToDateTime(phototime);
            model.PhotoPId = addpid;
            model.TaskId = taskId;
            model.FilePath = Path + fileName;
            model.FileName = fileName;
            //IDbConnection Connectionstr = new SqlConnection(Connection.GetConnection("2"));
            //Connectionstr.Insert(model);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TaskId", taskId);
            parameters.Add("@FileName", fileName);
            new SqlConnection(PubConstant.hmWyglConnectionString).ExecuteReader("Proc_Tb_Qm_TaskFiles_UploadCommand", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
            //bll.Add(model);
            new SqlConnection(PubConstant.hmWyglConnectionString).Insert<HM.Model.Qm.Tb_Qm_TaskFiles>(model);
        }

        // 装修巡查图片
        public void AddHousingFiles(string commId, string inspId, string projId, string filePath, string roomId)
        {
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = @"INSERT INTO Tb_HSPR_HousingFiles(ID, CommID, InspID, ProjID, FilePath, Sort, 
                                FileName, RoomID, AddTime, AddUser, Fix, Classification)
                                VALUES (newid(), @CommID, @InspID, @ProjID, @FilePath, 0, @FileName, @RoomID, getdate(), @AddUser, @Fix, @Classification)";
                conn.Execute(sql, new
                {
                    CommID = commId,
                    InspID = inspId,
                    ProjID = projId,
                    FilePath = filePath,
                    FileName = filePath.Substring(filePath.LastIndexOf('/') + 1),
                    RoomID = roomId,
                    AddUser = Global_Var.LoginUserCode,
                    Fix = filePath.Substring(filePath.LastIndexOf('.')),
                    Classification = string.IsNullOrEmpty(inspId) ? "装修巡查附件" : "验房项目附件"
                });
            }
        }

        public static string[] GetColumnsByDataTable(DataTable dt)
        {
            string[] strColumns = null;
            if (dt.Columns.Count > 0)
            {
                int columnNum = 0;
                columnNum = dt.Columns.Count;
                strColumns = new string[columnNum];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    strColumns[i] = dt.Columns[i].ColumnName;
                }
            }
            return strColumns;
        }

        #region 新版本接口

        /// <summary>
        /// 核查任务登记（新）
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        /// 
        public string QualityInspectSaveNew(DataRow Row)
        {
            string result = "";
            try
            {
                if (Row["Data"] != null && Row["Data"].ToString().Trim() != "")
                {
                    IDbConnection Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                    HM.Model.Qm.Tb_Qm_Task TaskModel = new HM.Model.Qm.Tb_Qm_Task();
                    HM.BLL.Qm.Bll_Tb_Qm_Task TaskBll = new HM.BLL.Qm.Bll_Tb_Qm_Task();
                    HM.Model.Qm.Tb_Qm_TaskAbarbeitung Model = new HM.Model.Qm.Tb_Qm_TaskAbarbeitung();
                    HM.BLL.Qm.Bll_Tb_Qm_TaskAbarbeitung Bll = new HM.BLL.Qm.Bll_Tb_Qm_TaskAbarbeitung();
                    HM.Model.Qm.Tb_Qm_TaskPoint TaskPointModel = new HM.Model.Qm.Tb_Qm_TaskPoint();
                    HM.BLL.Qm.Bll_Tb_Qm_TaskPoint TaskPointBll = new HM.BLL.Qm.Bll_Tb_Qm_TaskPoint();
                    Newtonsoft.Json.Linq.JObject obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(Row["Data"].ToString().Trim());//获得JsonObject对象
                    DataTable dtTable = new DataTable();
                    string TaskId = obj["TaskId"].ToString();
                    string TaskStatus = obj["TaskStatus"].ToString();
                    TaskModel = TaskBll.GetModel(TaskId);
                    DataTable dtBack = new DataTable();
                    ///点位
                    DataTable dtPoints = JsonConvert.DeserializeObject<DataTable>(obj["Points"].ToString());
                    int pointCount = dtPoints.Rows.Count;
                    for (int i = 0; i < dtPoints.Rows.Count; i++)
                    {
                        #region 点位
                        TaskPointModel = new HM.Model.Qm.Tb_Qm_TaskPoint();
                        AppGlobal.FillModel(dtPoints.Rows[i], TaskPointModel);

                        if (TaskPointModel.AddPId != null && TaskPointModel.AddPId != "")
                        {
                            DynamicParameters parameters = new DynamicParameters();
                            parameters.Add("@TaskId", TaskId);
                            parameters.Add("@PointId", dtPoints.Rows[i]["PointId"].ToString());
                            Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                            dtBack = Conn.ExecuteReader("Proc_Tb_Qm_TaskPoint_Select_Phone", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
                            if (dtBack != null && dtBack.Rows.Count > 0)
                            {
                                TaskPointModel.PointIds = dtPoints.Rows[i]["PointId"].ToString();
                                TaskPointModel.Id = dtBack.Rows[0][0].ToString().Trim();
                                TaskPointModel.TaskId = TaskId;

                                DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
                                TaskPointBll.Update(TaskPointModel);
                            }
                            else
                            {
                                TaskPointModel.PointIds = dtPoints.Rows[i]["PointId"].ToString();
                                TaskPointModel.Id = Guid.NewGuid().ToString();
                                TaskPointModel.TaskId = TaskId;

                                DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
                                TaskPointBll.Add(TaskPointModel);
                            }
                        }
                        #endregion
                    }
                    ///记录
                    dtTable = JsonConvert.DeserializeObject<DataTable>(obj["Record"].ToString());
                    if (dtTable != null && dtTable.Rows.Count > 0)//判断记录是否存在
                    {
                        #region 
                        string AbarPId = "";
                        int k = 0;
                        int AbarbeitungState = 0;//判断任务上传时任务为合格但是数据库中有整改记录时使用
                        int AbarbeitungStateInner = 0;//判断任务上传时任务为合格但是数据库中有整改记录时使用
                        for (int i = 0; i < dtTable.Rows.Count; i++)
                        {
                            #region  整改,任务
                            Model = new HM.Model.Qm.Tb_Qm_TaskAbarbeitung();
                            AppGlobal.FillModel(dtTable.Rows[i], Model);
                            if (Model.AddTime == null || Model.AddTime.ToString().Trim() == "")
                                Model.AddTime = DateTime.Now;
                            Model.UploadTime = DateTime.Now;
                            Model.AddPId = Global_Var.UserCode;
                            AbarPId += "'" + Model.AbarPId + "',";
                            Conn = new SqlConnection(PubConstant.hmWyglConnectionString);

                            if (Model.CheckResult.Trim() == "合格" || Model.CheckResult.Trim() == "不涉及")
                            {
                                AbarbeitungStateInner = 0;
                                Model.ReducePoint = 0;
                                if (Model.CheckResult.Trim() == "合格")
                                {
                                    DynamicParameters parameters = new DynamicParameters();
                                    parameters.Add("@taskId", TaskId);
                                    ///判断数据库中记录是否有整改记录
                                    dtBack = Conn.ExecuteReader("Proc_Tb_Qm_TaskAbarbeitung_SelectOut_Phone", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];

                                    if (dtBack != null && dtBack.Rows.Count > 0 && dtBack.Rows[0][0].ToString() != "0")
                                    {
                                        AbarbeitungStateInner++;
                                    }
                                }
                            }
                            else
                            {
                                AbarbeitungStateInner = 0;
                                //TaskId 任务ID
                                DynamicParameters parameters = new DynamicParameters();
                                parameters.Add("@taskId", TaskId);
                                Conn.ExecuteReader("Proc_Tb_Qm_TaskAbarbeitung_DeleteOut_Phone", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
                            }
                            Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                            if (AbarbeitungStateInner < 1)//判断任务上传时任务为合格但是数据库中有整改记录时不做添加操作
                            {
                                if (Model.Id != null && Model.Id != "")
                                {
                                    HM.Model.Qm.Tb_Qm_TaskAbarbeitung ModelCheck = Bll.GetModel(Model.Id);
                                    if (ModelCheck != null)
                                        Conn.Update<HM.Model.Qm.Tb_Qm_TaskAbarbeitung>(Model);
                                    else
                                        Conn.Insert<HM.Model.Qm.Tb_Qm_TaskAbarbeitung>(Model);
                                }
                                else
                                {
                                    if (Model.CheckResult.Trim() == "合格")
                                    {
                                        DynamicParameters parameters = new DynamicParameters();
                                        parameters.Add("@taskId", Model.TaskId);
                                        Conn.ExecuteReader("Proc_Tb_Qm_TaskAbarbeitung_DeleteOk_Phone", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
                                        Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                    }
                                    Model.Id = Guid.NewGuid().ToString();
                                    Conn.Insert<HM.Model.Qm.Tb_Qm_TaskAbarbeitung>(Model);
                                }
                            }
                            else
                            {
                                AbarbeitungState++;
                            }
                            k++;
                            if (TaskModel != null)
                            {
                                #region
                                if (TaskStatus == "已完成")
                                {
                                    #region ///查询点位数量并判断覆盖率是否正确后如不正确更正任务状态
                                    //PointCoverage

                                    //[Proc_Qm_Phone_Save_InspectPoint]
                                    DynamicParameters parameters = new DynamicParameters();
                                    parameters.Add("@TaskId", TaskModel.Id);
                                    parameters.Add("@ProjectCode", TaskModel.ItemCode);
                                    Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                    DataTable dtCon = Conn.ExecuteReader("Proc_Qm_Phone_Save_InspectPoint", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
                                    if (dtCon != null && dtCon.Rows.Count > 0 && pointCount > 0)
                                    {
                                        TaskModel.PointCoverageDone = decimal.Round((Convert.ToInt32(dtCon.Rows[0][0]) / pointCount), 2) * 100;
                                    }
                                    #endregion
                                    TaskModel.TaskStatus = TaskStatus;
                                    Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                    Conn.Update<HM.Model.Qm.Tb_Qm_Task>(TaskModel);
                                }
                                else
                                {
                                    DynamicParameters parameters = new DynamicParameters();
                                    parameters.Add("@TaskId", TaskModel.Id);
                                    parameters.Add("@ProjectCode", TaskModel.ItemCode);
                                    Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                    DataTable dtCon = Conn.ExecuteReader("Proc_Qm_Phone_Save_InspectPoint", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
                                    if (dtCon != null && dtCon.Rows.Count > 0 && pointCount > 0)
                                    {
                                        TaskModel.PointCoverageDone = decimal.Round((Convert.ToInt32(dtCon.Rows[0][0]) / pointCount), 2) * 100;
                                    }
                                    TaskModel.TaskStatus = "执行中";
                                    Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                                    Conn.Update<HM.Model.Qm.Tb_Qm_Task>(TaskModel);
                                }
                                #endregion
                            }
                            #endregion
                        }

                        #region 激光推送消息
                        if (AbarPId != "")//给整改人推送消息
                        {
                            AbarPId = AbarPId.Substring(0, AbarPId.Length - 1);
                            List<string> telMo = new List<string>();
                            string strSQL = "";
                            strSQL = "SELECT MobileTel FROM Tb_Sys_User WHERE 1=1 AND isnull(IsDelete,0)=0 AND isnull(MobileTel,'')<>'' AND isnull(IsMobile,0)=1 AND USERCODE in (" + AbarPId + ")";
                            Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                            DataTable dtUser = Conn.ExecuteReader(strSQL, null, null, null, CommandType.Text).ToDataSet().Tables[0];
                            for (int i = 0; i < dtUser.Rows.Count; i++)
                            {
                                telMo.Add(dtUser.Rows[i][0].ToString());
                            }
                            string tw2bsConnectionString = PubConstant.tw2bsConnectionString;
                            string hmWyglConnectionString = PubConstant.hmWyglConnectionString;
                            string corpId = Global_Var.CorpId;

                            Task.Run(() =>
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
                                        pushModel.Title = "你有一条新的品质整改任务";
                                        pushModel.Message = "请注意：你有一条新的品质整改任务请及时获取";
                                        pushModel.Command = PushCommand.QUALITY_ABARBEITUNG;
                                        pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                                        pushModel.Audience.Objects.AddRange(telMo);
                                        Push.SendAsync(pushModel);
                                        return;
                                    }
                                }
                            });
                        }
                        #endregion


                        if (dtTable.Rows.Count == k)
                        {
                            if (AbarbeitungState < 1)
                                result = JSONHelper.FromString(true, "登记完成");
                            else
                                result = JSONHelper.FromString(true, "已存在整改记录的任务合格信息未能上传");
                        }

                        else
                        {
                            result = JSONHelper.FromString(false, "登记过程中出现错误未能全部登记");
                        }
                        #endregion
                    }
                    else
                    {
                        TaskModel.TaskStatus = "执行中";
                        Conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                        Conn.Update<HM.Model.Qm.Tb_Qm_Task>(TaskModel);
                        result = JSONHelper.FromString(true, "无核查记录!");
                    }
                }
            }
            catch (Exception EX)
            {
                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }

        ///// <summary>
        ///// 核查任务登记（新+批转）
        ///// </summary>
        ///// <param name="Row"></param>
        ///// <returns></returns>
        ///// 
        //public string QualityInspectSaveApprovedNew(DataRow Row)
        //{
        //    string result = "";
        //    try
        //    {
        //        if (Row["Data"] != null && Row["Data"].ToString().Trim() != "")
        //        {
        //            IDbConnection Conn = new SqlConnection(PubConstant.PrivateConnectionString);
        //            HM.Model.Qm.Tb_Qm_Task TaskModel = new HM.Model.Qm.Tb_Qm_Task();
        //            HM.BLL.Qm.Bll_Tb_Qm_Task TaskBll = new HM.BLL.Qm.Bll_Tb_Qm_Task();
        //            HM.Model.Qm.Tb_Qm_TaskAbarbeitung Model = new HM.Model.Qm.Tb_Qm_TaskAbarbeitung();
        //            HM.BLL.Qm.Bll_Tb_Qm_TaskAbarbeitung Bll = new HM.BLL.Qm.Bll_Tb_Qm_TaskAbarbeitung();
        //            HM.Model.Qm.Tb_Qm_TaskPoint TaskPointModel = new HM.Model.Qm.Tb_Qm_TaskPoint();
        //            HM.BLL.Qm.Bll_Tb_Qm_TaskPoint TaskPointBll = new HM.BLL.Qm.Bll_Tb_Qm_TaskPoint();
        //            Newtonsoft.Json.Linq.JObject obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(Row["Data"].ToString().Trim());//获得JsonObject对象
        //            DataTable dtTable = new DataTable();
        //            string TaskId = obj["TaskId"].ToString();
        //            string TaskStatus = obj["TaskStatus"].ToString();
        //            TaskModel = TaskBll.GetModel(TaskId);
        //            DataTable dtBack = new DataTable();
        //            ///点位
        //            DataTable dtPoints = JsonConvert.DeserializeObject<DataTable>(obj["Points"].ToString());
        //            string TaskPointModelId = "";
        //            int pointCount = dtPoints.Rows.Count;
        //            for (int i = 0; i < dtPoints.Rows.Count; i++)
        //            {
        //                #region 点位
        //                TaskPointModel = new HM.Model.Qm.Tb_Qm_TaskPoint();
        //                AppGlobal.FillModel(dtPoints.Rows[i], TaskPointModel);

        //                if (TaskPointModel.AddPId != null && TaskPointModel.AddPId != "")
        //                {
        //                    DynamicParameters parameters = new DynamicParameters();
        //                    parameters.Add("@TaskId", TaskId);
        //                    parameters.Add("@PointId", dtPoints.Rows[i]["PointId"].ToString());
        //                    Conn = new SqlConnection(PubConstant.PrivateConnectionString);
        //                    dtBack = Conn.ExecuteReader("Proc_Tb_Qm_TaskPoint_Select_Phone", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
        //                    if (dtBack != null && dtBack.Rows.Count > 0)
        //                    {
        //                        TaskPointModel.PointIds = dtPoints.Rows[i]["PointId"].ToString();
        //                        TaskPointModel.Id = dtBack.Rows[0][0].ToString().Trim();
        //                        TaskPointModel.TaskId = TaskId;
        //                        TaskPointBll.Update(TaskPointModel);
        //                    }
        //                    else
        //                    {
        //                        TaskPointModel.PointIds = dtPoints.Rows[i]["PointId"].ToString();
        //                        TaskPointModel.Id = Guid.NewGuid().ToString();
        //                        TaskPointModel.TaskId = TaskId;
        //                        TaskPointBll.Add(TaskPointModel);
        //                    }
        //                }
        //                #endregion
        //            }
        //            ///记录
        //            dtTable = JsonConvert.DeserializeObject<DataTable>(obj["Record"].ToString());
        //            if (dtTable != null && dtTable.Rows.Count > 0)//判断记录是否存在
        //            {
        //                #region 
        //                string AbarPId = "";
        //                int k = 0;
        //                int AbarbeitungState = 0;//判断任务上传时任务为合格但是数据库中有整改记录时使用
        //                int AbarbeitungStateInner = 0;//判断任务上传时任务为合格但是数据库中有整改记录时使用

        //                for (int i = 0; i < dtTable.Rows.Count; i++)
        //                {
        //                    #region  整改,任务
        //                    Model = new HM.Model.Qm.Tb_Qm_TaskAbarbeitung();
        //                    AppGlobal.FillModel(dtTable.Rows[i], Model);
        //                    if (Model.AddTime == null || Model.AddTime.ToString().Trim() == "")
        //                        Model.AddTime = DateTime.Now;
        //                    Model.UploadTime = DateTime.Now;
        //                    Model.AddPId = Global_Var.UserCode;
        //                    AbarPId += Model.AbarPId + ",";
        //                    Conn = new SqlConnection(PubConstant.PrivateConnectionString);

        //                    if (Model.CheckResult.Trim() == "合格" || Model.CheckResult.Trim() == "不涉及")
        //                    {
        //                        AbarbeitungStateInner = 0;
        //                        Model.ReducePoint = 0;
        //                        if (Model.CheckResult.Trim() == "合格")
        //                        {
        //                            DynamicParameters parameters = new DynamicParameters();
        //                            parameters.Add("@taskId", TaskId);
        //                            ///判断数据库中记录是否有整改记录
        //                            dtBack = Conn.ExecuteReader("Proc_Tb_Qm_TaskAbarbeitung_SelectOut_Phone", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];

        //                            if (dtBack != null && dtBack.Rows.Count > 0 && dtBack.Rows[0][0].ToString() != "0")
        //                            {
        //                                AbarbeitungStateInner++;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        AbarbeitungStateInner = 0;
        //                        //TaskId 任务ID
        //                        DynamicParameters parameters = new DynamicParameters();
        //                        parameters.Add("@taskId", TaskId);
        //                        Conn.ExecuteReader("Proc_Tb_Qm_TaskAbarbeitung_DeleteOut_Phone", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
        //                    }
        //                    Conn = new SqlConnection(PubConstant.PrivateConnectionString);
        //                    if (AbarbeitungStateInner < 1)//判断任务上传时任务为合格但是数据库中有整改记录时不做添加操作
        //                    {
        //                        #region
        //                        if (Model.Id != null && Model.Id != "")
        //                        {
        //                            if (dtTable.Columns.Contains("AppovedState") && dtTable.Rows[i]["AppovedState"].ToString().Trim() == "1")
        //                            {
        //                                DynamicParameters parametersAppoved = new DynamicParameters();
        //                                parametersAppoved.Add("@OriginatorId", Global_Var.UserCode);
        //                                parametersAppoved.Add("@UndertakingId", Model.AbarPId);
        //                                parametersAppoved.Add("@AddTime", DateTime.Now);
        //                                parametersAppoved.Add("@TaskId", Model.TaskId);
        //                                parametersAppoved.Add("@TaskAbarbeitungId", Model.Id); 
        //                                parametersAppoved.Add("@Type", "Insert");
        //                                parametersAppoved.Add("@ID", "");

        //                                Conn.ExecuteReader("PROC_TB_QM_APPROVED_COMMAND", parametersAppoved, null, null, CommandType.StoredProcedure).ToDataSet();

        //                                Conn = new SqlConnection(PubConstant.PrivateConnectionString);
        //                            }
        //                            Conn = new SqlConnection(PubConstant.PrivateConnectionString);
        //                            Conn.Update<HM.Model.Qm.Tb_Qm_TaskAbarbeitung>(Model);
        //                        }
        //                        else
        //                        {
        //                            if (Model.CheckResult.Trim() == "合格")
        //                            {
        //                                DynamicParameters parameters = new DynamicParameters();
        //                                parameters.Add("@taskId", Model.TaskId);
        //                                Conn.ExecuteReader("Proc_Tb_Qm_TaskAbarbeitung_DeleteOk_Phone", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
        //                                Conn = new SqlConnection(PubConstant.PrivateConnectionString);
        //                            }
        //                            if (dtTable.Columns.Contains("AppovedState") && dtTable.Rows[i]["AppovedState"].ToString().Trim() == "1")
        //                            {
        //                                DynamicParameters parametersAppoved = new DynamicParameters();
        //                                parametersAppoved.Add("@OriginatorId", Global_Var.UserCode);
        //                                parametersAppoved.Add("@UndertakingId", Model.AbarPId);
        //                                parametersAppoved.Add("@AddTime", DateTime.Now);
        //                                parametersAppoved.Add("@TaskId", Model.TaskId);
        //                                parametersAppoved.Add("@TaskAbarbeitungId", Model.Id);
        //                                parametersAppoved.Add("@Type", "Insert");
        //                                parametersAppoved.Add("@ID", "");

        //                                Conn.ExecuteReader("PROC_TB_QM_APPROVED_COMMAND", parametersAppoved, null, null, CommandType.StoredProcedure).ToDataSet();
        //                                Conn = new SqlConnection(PubConstant.PrivateConnectionString);
        //                            }
        //                            Model.Id = Guid.NewGuid().ToString();
        //                            Conn.Insert<HM.Model.Qm.Tb_Qm_TaskAbarbeitung>(Model);
        //                        }
        //                        #endregion
        //                    }
        //                    else
        //                    {
        //                        AbarbeitungState++;
        //                    }
        //                    k++;
        //                    if (TaskModel != null)
        //                    {
        //                        #region
        //                        if (TaskStatus == "已完成")
        //                        {
        //                            #region ///查询点位数量并判断覆盖率是否正确后如不正确更正任务状态
        //                            //PointCoverage

        //                            //[Proc_Qm_Phone_Save_InspectPoint]
        //                            DynamicParameters parameters = new DynamicParameters();
        //                            parameters.Add("@TaskId", TaskModel.Id);
        //                            parameters.Add("@ProjectCode", TaskModel.ItemCode);
        //                            Conn = new SqlConnection(PubConstant.PrivateConnectionString);
        //                            DataTable dtCon = Conn.ExecuteReader("Proc_Qm_Phone_Save_InspectPoint", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
        //                            if (dtCon != null && dtCon.Rows.Count > 0 && pointCount > 0)
        //                            {
        //                                TaskModel.PointCoverageDone = decimal.Round((Convert.ToInt32(dtCon.Rows[0][0]) / pointCount), 2) * 100;
        //                            }
        //                            #endregion
        //                            TaskModel.TaskStatus = TaskStatus;
        //                            Conn = new SqlConnection(PubConstant.PrivateConnectionString);
        //                            Conn.Update<HM.Model.Qm.Tb_Qm_Task>(TaskModel);
        //                        }
        //                        else
        //                        {
        //                            DynamicParameters parameters = new DynamicParameters();
        //                            parameters.Add("@TaskId", TaskModel.Id);
        //                            parameters.Add("@ProjectCode", TaskModel.ItemCode);
        //                            Conn = new SqlConnection(PubConstant.PrivateConnectionString);
        //                            DataTable dtCon = Conn.ExecuteReader("Proc_Qm_Phone_Save_InspectPoint", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
        //                            if (dtCon != null && dtCon.Rows.Count > 0 && pointCount > 0)
        //                            {
        //                                TaskModel.PointCoverageDone = decimal.Round((Convert.ToInt32(dtCon.Rows[0][0]) / pointCount), 2) * 100;
        //                            }
        //                            TaskModel.TaskStatus = "执行中";
        //                            Conn = new SqlConnection(PubConstant.PrivateConnectionString);
        //                            Conn.Update<HM.Model.Qm.Tb_Qm_Task>(TaskModel);
        //                        }
        //                        #endregion
        //                    }
        //                    #endregion
        //                }
        //                Conn = new SqlConnection(PubConstant.PublicConnectionString);
        //                List<string> Ls = Conn.Query<string>("select AppPushPackage from Tb_System_CorpAppPushSet where CorpID = " + Global_Var.LoginCorpID + " and IsAppPushMsg = 1 ").AsList<string>();
        //                if (Ls.Count > 0)
        //                {
        //                    #region 激光推送消息
        //                    if (AbarPId != "")//给整改人推送消息
        //                    {
        //                        AbarPId = AbarPId.Substring(0, AbarPId.Length - 1);
        //                        string[] AbarPIds = AbarPId.Split(',');
        //                        for (int i = 0; i < AbarPIds.Length; i++)
        //                        {
        //                            if (AbarPIds[i] != "")
        //                            {
        //                                DataTable dtRows = new DataTable();
        //                                dtRows.Columns.Add(new DataColumn("Package"));
        //                                dtRows.Columns.Add(new DataColumn("Tag"));
        //                                dtRows.Columns.Add(new DataColumn("Title"));
        //                                dtRows.Columns.Add(new DataColumn("MsgContent"));
        //                                DataRow rows = dtRows.NewRow();
        //                                string strSQL = "";
        //                                strSQL = "SELECT MobileTel FROM Tb_Sys_User WHERE USERCODE='" + AbarPIds[i] + "'";
        //                                Conn = new SqlConnection(PubConstant.PrivateConnectionString);
        //                                DataTable dtUser = Conn.ExecuteReader(strSQL, null, null, null, CommandType.Text).ToDataSet().Tables[0];
        //                                if (dtUser != null && dtUser.Rows.Count > 0 && dtUser.Rows[0][0].ToString().Trim() != "")
        //                                {
        //                                    rows["Tag"] = dtUser.Rows[0][0].ToString().Trim();
        //                                    rows["Package"] = Ls[0].ToString();
        //                                    rows["Title"] = "你有一条品质整改任务";
        //                                    rows["MsgContent"] = "你有一条整改任务";
        //                                    new AppPush().Operate("PushMsgObjectForTag", rows);
        //                                }
        //                            }
        //                        }
        //                    }
        //                    #endregion
        //                }

        //                if (dtTable.Rows.Count == k)
        //                {
        //                    if (AbarbeitungState < 1)
        //                        result = JSONHelper.FromString(true, "登记完成");
        //                    else
        //                        result = JSONHelper.FromString(true, "已存在整改记录的任务合格信息未能上传");
        //                }

        //                else
        //                {
        //                    result = JSONHelper.FromString(false, "登记过程中出现错误未能全部登记");
        //                }
        //                #endregion
        //            }
        //            else
        //            {
        //                TaskModel.TaskStatus = "执行中";
        //                Conn = new SqlConnection(PubConstant.PrivateConnectionString);
        //                Conn.Update<HM.Model.Qm.Tb_Qm_Task>(TaskModel);
        //                result = JSONHelper.FromString(true, "无核查记录!");
        //            }
        //        }
        //    }
        //    catch (Exception EX)
        //    {

        //        result = JSONHelper.FromString(false, EX.ToString());
        //    }
        //    return result;
        //}
        /// <summary>
        /// 品质设备任务数量(新)
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string GetQualityInspectListCountNew(DataRow Row)
        {
            string result = "";
            try
            {
                string Timer = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ItemCode = "", UserCode = "";//时间段
                if (Row.Table.Columns.Contains("Timer") && Row["Timer"].ToString() != "")//当只传一个值时
                {
                    Timer = Convert.ToDateTime(Row["Timer"]).ToString("yyyy-MM-dd HH:mm:ss");
                }
                //ItemCode 项目编号，TaskPId 责任人，BeginDate任务开始时间，endDate任务结束时间  yyyy/MM/dd  TaskStatus='未开始' AND
                if (Row.Table.Columns.Contains("ItemCode") && Row.Table.Columns.Contains("UserCode") && Row.Table.Columns.Contains("Timer"))
                {
                    //&&Row.Table.Columns.Contains("AbarPId")
                    ItemCode = Row["ItemCode"].ToString().Trim();
                    UserCode = Row["UserCode"].ToString().Trim();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(" SELECT count(TaskNo) FROM View_Qm_Phone_QualityInspectList WHERE ISNULL(ISDELETE,0)=0 AND ( TaskStatus='未开始' or TaskStatus='执行中' )AND ItemCode='" + ItemCode + "' AND TaskRoleId IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + UserCode + "'))  ");
                    if (Global_Var.LoginCorpID=="1971")
                    {
                        strSql = new StringBuilder();
                        strSql.Append(" SELECT count(TaskNo) FROM View_Qm_Phone_QualityInspectList WHERE ISNULL(ISDELETE,0)=0 AND  ISNULL(IsShanchu, 0) = 0 AND( TaskStatus='未开始' or TaskStatus='执行中' )AND ItemCode='" + ItemCode + "' AND TaskRoleId IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + UserCode + "'))  ");
                    }

                    if (Timer != "")
                    {
                        Timer = Convert.ToDateTime(Timer).ToString("yyyy-MM-dd");
                        strSql.Append(" AND  BeginDateD <='" + Timer + " 00:00:00.000'  AND EndDateD >='" + Timer + " 23:59:59.000' ");
                    }
                    IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    DataTable dt = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                    strSql.Length = 0;
                    strSql.Append(" SELECT COUNT (AbarbeitungId) FROM View_Qm_Phone_AbarbeitungList  WHERE ISNULL(ISDELETE,0)=0 AND CheckResult='整改' AND (addPId = '" + UserCode + "' or  AbarPId ='" + UserCode + "' ) AND ISNULL(IsOk,'') !='1' AND getCheckDate>='" + DateTime.Now + "'   ");
                    strSql.Append(" AND ItemCode='" + ItemCode + "' ");
                    con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    DataTable dt2 = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.Append("{\"Result\":\"true\",\"data\": {");
                        //任务数量
                        sb.Append("\"TaskQualityCount\":\"");
                        sb.Append(dt.Rows[0][0].ToString());
                        sb.Append("\",");
                        //品质整改数量
                        sb.Append("\"RectificationCount\":\"");
                        if (dt2 != null && dt2.Rows.Count > 0)
                            sb.Append(dt2.Rows[0][0].ToString());
                        else
                            sb.Append("0");
                        sb.Append("\",");
                        //设备维保数量
                        sb.Append("\"MaintenanceTaskCount\":\"");

                        if (Row["UserCode"].ToString() != "" && Row["Timer"].ToString() != "")
                        {
                            Timer = Convert.ToDateTime(Row["Timer"]).ToString("yyyy-MM-dd");
                            strSql.Length = 0;
                            strSql.Append(" SELECT  COUNT (TaskId) FROM View_EQ_WbPatrolTaskExecList WHERE ISNULL(ISDELETE,0)=0 AND CommID='" + ItemCode + "' AND RoleCode IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + UserCode + "'))  ");
                            strSql.Append(" AND Statue != '已完成' AND Statue != '已关闭' ");
                            strSql.Append(" AND CONVERT(nvarchar(100), BeginTime, 23) <='" + Timer + "' AND CONVERT(nvarchar(100), EndTime, 23) >= '" + Timer + "'");
                            strSql.Append(" AND  EndTime >= '" + Timer + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "'");
                            strSql.Append(" AND ISNULL(CheckMan,'') = '' AND  ISNULL(CheckNoto,'') = '' AND  ISNULL(CheckRusult,'') = ''");
                            con = new SqlConnection(PubConstant.hmWyglConnectionString);
                            DataTable dtTask = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                            if (dtTask != null && dtTask.Rows.Count > 0)
                                sb.Append(dtTask.Rows[0][0].ToString());
                        }
                        else
                        {
                            sb.Append("0");
                        }
                        sb.Append("\",");
                        sb.Append("\"PatrolTaskExec\":\"");
                        if (ItemCode != "" && UserCode != "" && Row["Timer"].ToString() != "")
                        {
                            strSql.Length = 0;
                            strSql.Append(" SELECT COUNT(TaskId) FROM View_EQ_PatrolTaskExecList WHERE ISNULL(ISDELETE,0)=0 AND CommID='" + ItemCode + "' AND RoleCode IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + UserCode + "'))  ");
                            strSql.Append(" AND CONVERT(nvarchar(100), BeginTime, 23) <='" + Timer + "' AND CONVERT(nvarchar(100), EndTime, 23) >= '" + Timer + "'");
                            strSql.Append(" AND  EndTime >= '" + Timer + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "'");
                            strSql.Append(" AND Statue != '已完成' AND Statue != '已关闭' ");
                            con = new SqlConnection(PubConstant.hmWyglConnectionString);
                            DataTable dtTask = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                            if (dtTask != null && dtTask.Rows.Count > 0)
                                sb.Append(dtTask.Rows[0][0].ToString());
                        }
                        else
                        {
                            sb.Append("0");
                        }
                        sb.Append("\",");
                        sb.Append("\"ConfigState\":\"");
                        con = new SqlConnection(PubConstant.hmWyglConnectionString);
                        dt = con.ExecuteReader(" SELECT isnull(TimeLimit, 0) as TimeLimit FROM Tb_Qm_TaskParameterSetting where ISNULL(IsDelete,0)=0 ", null, null, null, CommandType.Text).ToDataSet().Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            sb.Append(dt.Rows[0][0].ToString());
                        }
                        else
                        {
                            sb.Append("0");
                        }
                        //result = JSONHelper.FromString(dt);
                        //
                        sb.Append("\"}}");
                        result = sb.ToString();
                    }

                    else
                        result = JSONHelper.FromString(true, "0");

                }
                else
                {
                    result = JSONHelper.FromString(false, "缺少参数ItemCode/UserCode/Timer/AbarPId");
                }
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, EX.ToString());
            }

            return result;
        }

        /// <summary>
        /// 装修巡查列表(华南城)
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string GetDecorateDealList(DataRow Row)
        {
            try
            {
                if (!Row.Table.Columns.Contains("CommID") || !Row.Table.Columns.Contains("OrganCode"))
                {
                    return new ApiResult(false, "缺少参数CommID/OrganCode").toJson();
                }
                int PageIndex = 1;
                int PageSize = 10;
                if (Row.Table.Columns.Contains("PageIndex"))
                {
                    PageIndex = Global_Fun.StrToInt(Row["PageIndex"].ToString());
                }
                if (Row.Table.Columns.Contains("PageSize"))
                {
                    PageSize = Global_Fun.StrToInt(Row["PageSize"].ToString());
                }
                int CommID = Global_Fun.StrToInt(Row["CommID"].ToString());
                string OrganCode = Row["OrganCode"].ToString();

                DataTable dt = GetList(out int PageCount, out int Counts, "SELECT * FROM view_HSPR_RenoCust_Filter WHERE (RenoStatus IS NULL OR RenoStatus<>'完工') AND (RenoStatus IS NULL OR RenoStatus<>'取消')AND CommID = " + CommID, PageIndex, PageSize, "RenoID", 1, "RenoID", PubConstant.hmWyglConnectionString, "*").Tables[0];

                Dictionary<string, object> result = new Dictionary<string, object>();

                if (null != dt && dt.Rows.Count > 0)
                {
                    #region 巡检任务列表构建
                    List<Dictionary<string, object>> renoList = new List<Dictionary<string, object>>();
                    {
                        using(IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                        {

                            foreach (DataRow dr in dt.Rows)
                            {
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("RenoID", dr["RenoID"]);
                                dictionary.Add("CommID", dr["CommID"]);
                                dictionary.Add("RoomID", dr["RoomID"]);
                                dictionary.Add("ApplyDate", DBNull.Value == dr["ApplyDate"] ? "" : ((DateTime)dr["ApplyDate"]).ToString("yyyy-MM-dd HH:mm:ss"));
                                dictionary.Add("MobilePhone", dr["MobilePhone"]);
                                dictionary.Add("AdmiMan", dr["AdmiMan"]);
                                dictionary.Add("AdmiDate", DBNull.Value == dr["AdmiDate"] ? "" : ((DateTime)dr["AdmiDate"]).ToString("yyyy-MM-dd HH:mm:ss"));
                                dictionary.Add("FormerReno", dr["FormerReno"]);
                                dictionary.Add("ConsContent", dr["ConsContent"]);
                                dictionary.Add("RenoStartDate", DBNull.Value == dr["RenoStartDate"] ? "" : ((DateTime)dr["RenoStartDate"]).ToString("yyyy-MM-dd HH:mm:ss"));
                                dictionary.Add("ExpRenoEndDate", DBNull.Value == dr["ExpRenoEndDate"] ? "" : ((DateTime)dr["ExpRenoEndDate"]).ToString("yyyy-MM-dd HH:mm:ss"));
                                dictionary.Add("ActRenoEndDate", DBNull.Value == dr["ActRenoEndDate"] ? "" : ((DateTime)dr["ActRenoEndDate"]).ToString("yyyy-MM-dd HH:mm:ss"));
                                dictionary.Add("ConsUnit", dr["ConsUnit"]);
                                dictionary.Add("QuaGrades", dr["QuaGrades"]);
                                dictionary.Add("OfficeAddr", dr["OfficeAddr"]);
                                dictionary.Add("LiceNumber", dr["LiceNumber"]);
                                dictionary.Add("UnitRespMan", dr["UnitRespMan"]);
                                dictionary.Add("UnitRespManTel", dr["UnitRespManTel"]);
                                dictionary.Add("SceneRespMan", dr["SceneRespMan"]);
                                dictionary.Add("SceneRespManTel", dr["SceneRespManTel"]);
                                dictionary.Add("AdmissionNum", dr["AdmissionNum"]);
                                dictionary.Add("Islive", dr["Islive"]);
                                dictionary.Add("RenoPermitSign", dr["RenoPermitSign"]);
                                dictionary.Add("Handling", dr["Handling"]);
                                dictionary.Add("DrawingProg", dr["DrawingProg"]);
                                dictionary.Add("InfoSource", dr["InfoSource"]);
                                dictionary.Add("Memo", dr["Memo"]);
                                dictionary.Add("IsSubmit", dr["IsSubmit"]);
                                dictionary.Add("Engineering", dr["Engineering"]);
                                dictionary.Add("IsOrderly", dr["IsOrderly"]);
                                dictionary.Add("Orderly", dr["Orderly"]);
                                dictionary.Add("ExpRenoDays", dr["ExpRenoDays"]);
                                dictionary.Add("ExtensionDate", dr["ExtensionDate"]);
                                dictionary.Add("DownTimeRecord", dr["DownTimeRecord"]);
                                dictionary.Add("ActRenoDays", dr["ActRenoDays"]);
                                dictionary.Add("RenoStatus", dr["RenoStatus"]);
                                dictionary.Add("RenoChangeDate", dr["RenoChangeDate"]);
                                dictionary.Add("RenoChangeReason", dr["RenoChangeReason"]);
                                dictionary.Add("HistoryRecord", dr["HistoryRecord"]);
                                dictionary.Add("cunt", dr["cunt"]);
                                dictionary.Add("PaperSign", dr["PaperSign"]);
                                dictionary.Add("RuleCount", dr["RuleCount"]);
                                dictionary.Add("RoomSign", dr["RoomSign"]);
                                dictionary.Add("RoomName", dr["RoomName"]);
                                dictionary.Add("CustID", dr["CustID"]);
                                dictionary.Add("CustName", dr["CustName"]);
                                dictionary.Add("IsAct", dr["IsAct"]);
                                dictionary.Add("IsPass", dr["IsPass"]);
                                dictionary.Add("IsRule", dr["IsRule"]);
                                dictionary.Add("RenoCustCharge", dr["RenoCustCharge"]);
                                dictionary.Add("RenoPorgram", dr["RenoPorgram"]);
                                dictionary.Add("RenoProgramName", dr["RenoProgramName"]);
                                dictionary.Add("IsDecoration", dr["IsDecoration"]);

                                #region 装修人员信息和办证数量
                                List<string> RenoPeople = conn.Query<string>("SELECT PassMan FROM view_HSPR_Pass_Filter WHERE Statue = '正常' AND RenoID = @RenoID", new { RenoID = dr["RenoID"].ToString() }).ToList();
                                dictionary.Add("RenoPeople", string.Join(",", RenoPeople.ToArray()));
                                dictionary.Add("RenoPassCount", RenoPeople.Count());
                                #endregion
                                renoList.Add(dictionary);
                            }
                        }
                    }
                    #endregion

                    #region 违章情况列表获取
                    {
                        //查询所有的违章标准,然后根据ID来添加到下面的违章情况中
                        string renoIds = string.Join("','", renoList.ConvertAll(dic => dic["RenoID"].ToString()));
                        Dictionary<string, List<Dictionary<string, object>>> custRuleStandardList = new Dictionary<string, List<Dictionary<string, object>>>();
                        {
                            IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                            DataTable dt2 = conn.ExecuteReader("select * from Tb_HSPR_RenoCustRuleStandard WHERE RenoID in ('" + renoIds + "')").ToDataSet().Tables[0];
                            if (null != dt2 && dt2.Rows.Count > 0)
                            {
                                List<Dictionary<string, object>> tempList = new List<Dictionary<string, object>>();

                                foreach (DataRow row in dt2.Rows)
                                {
                                    Dictionary<string, object> tempDic2 = new Dictionary<string, object>();
                                    tempDic2.Add("IID", row["IID"]);
                                    tempDic2.Add("RenoCustRuleID", row["RenoCustRuleID"]);
                                    tempDic2.Add("RenoID", row["RenoID"]);
                                    tempDic2.Add("PatrolID", row["PatrolID"]);
                                    tempDic2.Add("StandardState", row["StandardState"]);
                                    tempDic2.Add("StandardExplain", row["StandardExplain"]);
                                    //如果没有这个ID键,RenoCustRuleID是登记记录的IID
                                    //就新创建
                                    //否则直接添加
                                    if (!custRuleStandardList.ContainsKey(row["RenoCustRuleID"].ToString()))
                                    {
                                        custRuleStandardList.Add(row["RenoCustRuleID"].ToString(), new List<Dictionary<string, object>>());
                                    }
                                    custRuleStandardList[row["RenoCustRuleID"].ToString()].Add(tempDic2);
                                }
                            }
                            conn.Dispose();
                        }

                        {
                            //先查询出违章情况所有列表,然后根据列表ID来添加到list,这样只花销了服务器运算,而不重复创建sql链接
                            IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                            DataTable dt2 = conn.ExecuteReader("select * from view_HSPR_RenoCustRule_filter WHERE RenoID in ('" + renoIds + "')").ToDataSet().Tables[0];

                            IDbConnection conn2 = new SqlConnection(PubConstant.hmWyglConnectionString);
                            DataTable dt3 = conn2.ExecuteReader("select * from view_HSPR_Pass_Filter WHERE RenoID in ('" + renoIds + "')").ToDataSet().Tables[0];
                            foreach (var dic in renoList)
                            {
                                List<Dictionary<string, object>> custRuleList = new List<Dictionary<string, object>>();
                                List<Dictionary<string, object>> custCertList = new List<Dictionary<string, object>>();
                                if (null != dt2 && dt2.Rows.Count > 0)
                                {
                                    foreach (DataRow row in dt2.Rows)
                                    {
                                        if (dic["RenoID"].ToString().Equals(row["RenoID"].ToString()))
                                        {
                                            Dictionary<string, object> tempDic = new Dictionary<string, object>();
                                            tempDic.Add("IID", row["IID"]);
                                            tempDic.Add("RenoID", row["RenoID"]);
                                            tempDic.Add("RuleDate", row["RuleDate"]);
                                            tempDic.Add("RuleSituation", row["RuleSituation"]);
                                            tempDic.Add("QueryMan", row["QueryMan"]);
                                            //如果该违章情况有登记情况,就添加进去
                                            List<Dictionary<string, object>> ruleStandardList = new List<Dictionary<string, object>>();
                                            if (custRuleStandardList.ContainsKey(row["IID"].ToString()))
                                            {
                                                ruleStandardList = custRuleStandardList[row["IID"].ToString()];
                                            }
                                            tempDic.Add("RuleStandardList", ruleStandardList);
                                            custRuleList.Add(tempDic);
                                        }
                                    }
                                }
                                dic.Add("RenoPatrolList", custRuleList);

                                if (null != dt3 && dt3.Rows.Count > 0)
                                {
                                    foreach (DataRow row in dt3.Rows)
                                    {
                                        if (dic["RenoID"].ToString().Equals(row["RenoID"].ToString()))
                                        {
                                            Dictionary<string, object> tempDic = new Dictionary<string, object>();
                                            tempDic.Add("RenoID", row["RenoID"]);
                                            tempDic.Add("PassSign", row["PassSign"]);
                                            tempDic.Add("PassName", row["PassName"]);
                                            tempDic.Add("PassMan", row["PassMan"]);
                                            tempDic.Add("Statue", row["Statue"]);
                                            tempDic.Add("PassDate", row["PassDate"]);
                                            custCertList.Add(tempDic);
                                        }
                                    }
                                }
                                dic.Add("RenoCertList", custCertList);
                            }

                            conn.Dispose();
                            conn2.Dispose();
                        }
                    }

                    #endregion
                    {
                        IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@OrganCode", OrganCode);
                        DataTable dt2 = conn.ExecuteReader("SELECT * FROM View_HSPR_RenoPatrolStandardList WHERE ISNULL(IsDelete, 0) = 0 AND OrganCode = @OrganCode", parameters).ToDataSet().Tables[0];

                        #region 查询巡检标准
                        List<Dictionary<string, object>> standardList = new List<Dictionary<string, object>>();
                        if (null != dt2 && dt2.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt2.Rows)
                            {
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("PatrolID", dr["PatrolID"]);
                                dictionary.Add("PatrolStandard", dr["PatrolStandard"]);
                                dictionary.Add("Remark", dr["Remark"]);
                                standardList.Add(dictionary);
                            }
                            result.Add("RenoList", renoList);
                            result.Add("StandardList", standardList);
                        }
                        else
                        {
                            result.Add("RenoList", new ArrayList());
                            result.Add("StandardList", new ArrayList());
                        }
                    }
                    #endregion

                }
                else
                {
                    result.Add("RenoList", new ArrayList());
                    result.Add("StandardList", new ArrayList());

                }
                return new ApiResult(true, result).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message + ex.StackTrace).toJson();
            }
        }

        /// <summary>
        /// 装修巡查上传(华南城)
        /// </summary>
        public string OnDecorateDealSave(DataRow Row)
        {
            try
            {
                if (!Row.Table.Columns.Contains("CommID") || !Row.Table.Columns.Contains("OrganCode") ||
                string.IsNullOrEmpty(Row["CommID"].ToString()) || string.IsNullOrEmpty(Row["OrganCode"].ToString()))
                {
                    return new ApiResult(false, "缺少参数CommID/OrganCode").toJson();
                }
                if (!Row.Table.Columns.Contains("Data") || string.IsNullOrEmpty(Row["Data"].ToString()))
                {
                    return new ApiResult(false, "缺少参数Data").toJson();
                }
                int CommID = Global_Fun.StrToInt(Row["CommID"].ToString());
                string OrganCode = Row["OrganCode"].ToString();
                JObject obj = (JObject)JsonConvert.DeserializeObject(Row["Data"].ToString().Trim());//获得JsonObject对象

                #region 保存违章情况
                //违章情况列表
                JArray renoCustRuleList = (JArray)obj["RenoCustRuleList"];
                if (null == renoCustRuleList)
                {
                    return new ApiResult(false, "RenoCustRuleList为空").toJson();
                }
                foreach (JObject item in renoCustRuleList)
                {

                    string IID = item["IID"].ToString();
                    string RenoID = item["RenoID"].ToString();
                    string RuleDate = item["RuleDate"].ToString();
                    string RuleSituation = item["RuleSituation"].ToString();
                    string QueryMan = item["QueryMan"].ToString();
                    //function == 1.新增/2.删除/3.修改
                    int function = Global_Fun.StrToInt(item["Function"].ToString());
                    JArray ruleStandardList = (JArray)item["RuleStandardList"];

                    //如果IID为空,一定为新增
                    if (function == 1)
                    {
                        if (string.IsNullOrEmpty(IID))
                        {
                            IID = Guid.NewGuid().ToString();
                        }
                        Proc_HSPR_RenoCustRule_Insert(IID, CommID, RenoID, RuleDate, RuleSituation, QueryMan);
                        OnRenoCustRuleStandardlSave(ruleStandardList, IID, RenoID, OrganCode, CommID);
                        continue;
                    }
                    //如果为更新,先查询是否还存在该记录,存在即更新,不存在就添加
                    if (function == 3)
                    {
                        IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString);
                        int count = conn.Query<int>("SELECT COUNT(*) FROM Tb_HSPR_RenoCustRule WHERE IID = @IID", new { IID = IID }, null, true, null, CommandType.Text).FirstOrDefault();
                        conn.Dispose();
                        if (count <= 0)
                        {
                            Proc_HSPR_RenoCustRule_Insert(Guid.NewGuid().ToString(), CommID, RenoID, RuleDate, RuleSituation, QueryMan);
                        }
                        else
                        {
                            Proc_HSPR_RenoCustRule_Update(IID, CommID, RenoID, RuleDate, RuleSituation, QueryMan);
                        }
                        OnRenoCustRuleStandardlSave(ruleStandardList, IID, RenoID, OrganCode, CommID);
                        continue;
                    }
                    //其他情况均视为删除
                    //删除
                    Proc_HSPR_RenoCustRule_Delete(IID);
                    OnRenoCustRuleStandardlSave(null, IID, RenoID, OrganCode, CommID);
                }
                #endregion

                return new ApiResult(true, "登记完成").toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }

        /// <summary>
        /// 进行装修登记标准的保存
        /// </summary>
        /// <param name="ruleStandardList"></param>
        /// <param name="RenoCustRuleID"></param>
        /// <param name="RenoID"></param>
        /// <param name="OrganCode"></param>
        /// <param name="CommID"></param>
        /// <returns></returns>
        private bool OnRenoCustRuleStandardlSave(JArray ruleStandardList, string RenoCustRuleID, string RenoID, string OrganCode, int CommID)
        {
            #region 进行装修登记标准的保存
            //删除该登记记录下的所有标准
            IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString);
            conn.Execute("DELETE Tb_HSPR_RenoCustRuleStandard WHERE RenoCustRuleID = @RenoCustRuleID", new { RenoCustRuleID = RenoCustRuleID }, null, null, CommandType.Text);
            conn.Dispose();
            //再遍历进行保存
            if (null == ruleStandardList)
            {
                return true;
            }
            foreach (JObject item2 in ruleStandardList)
            {
                string PatrolID = item2["PatrolID"].ToString();
                string StandardName = item2["StandardName"].ToString();
                int StandardState = Global_Fun.StrToInt(item2["StandardState"].ToString());
                string StandardExplain = item2["StandardExplain"]?.ToString() ?? "";
                //进行新增
                try
                {
                    Tb_HSPR_RenoCustRuleStandard_Insert(Guid.NewGuid().ToString(), RenoCustRuleID, RenoID, OrganCode, CommID, PatrolID, StandardName, StandardState, StandardExplain);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return true;
            #endregion
        }

        /// <summary>
        /// 删除一条装修登记
        /// </summary>
        /// <param name="IID"></param>
        /// <returns></returns>
        private bool Proc_HSPR_RenoCustRule_Delete(string IID)
        {
            IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("IID", IID);
            int result = conn.Execute("Proc_HSPR_RenoCustRule_Delete", parameters, null, null, CommandType.StoredProcedure);
            conn.Dispose();
            return result > 0;
        }

        /// <summary>
        /// 修改一条装修登记
        /// </summary>
        /// <param name="IID"></param>
        /// <param name="CommID"></param>
        /// <param name="RenoID"></param>
        /// <param name="RuleDate"></param>
        /// <param name="RuleSituation"></param>
        /// <param name="QueryMan"></param>
        /// <returns></returns>
        private bool Proc_HSPR_RenoCustRule_Update(string IID, int CommID, string RenoID, string RuleDate, string RuleSituation, string QueryMan)
        {
            IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("IID", IID);
            parameters.Add("CommID", CommID);
            parameters.Add("RenoID", RenoID);
            parameters.Add("RuleDate", RuleDate);
            parameters.Add("RuleSituation", RuleSituation);
            parameters.Add("QueryMan", QueryMan);
            int result = conn.Execute("Proc_HSPR_RenoCustRule_Update", parameters, null, null, CommandType.StoredProcedure);
            conn.Dispose();
            return result > 0;
        }

        /// <summary>
        /// 新增一条装修登记
        /// </summary>
        /// <param name="IID"></param>
        /// <param name="CommID"></param>
        /// <param name="RenoID"></param>
        /// <param name="RuleDate"></param>
        /// <param name="RuleSituation"></param>
        /// <param name="QueryMan"></param>
        /// <returns></returns>
        private bool Proc_HSPR_RenoCustRule_Insert(string IID, int CommID, string RenoID, string RuleDate, string RuleSituation, string QueryMan)
        {
            IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("IID", IID);
            parameters.Add("CommID", CommID);
            parameters.Add("RenoID", RenoID);
            parameters.Add("RuleDate", RuleDate);
            parameters.Add("RuleSituation", RuleSituation);
            parameters.Add("QueryMan", QueryMan);
            int result = conn.Execute("Proc_HSPR_RenoCustRule_Insert", parameters, null, null, CommandType.StoredProcedure);
            conn.Dispose();
            return result > 0;
        }

        /// <summary>
        /// 新增一条违章标准记录
        /// </summary>
        /// <param name="IID"></param>
        /// <param name="RenoCustRuleID"></param>
        /// <param name="RenoID"></param>
        /// <param name="OrganCode"></param>
        /// <param name="CommID"></param>
        /// <param name="PatrolID"></param>
        /// <param name="StandardName"></param>
        /// <param name="StandardState"></param>
        /// <param name="StandardExplain"></param>
        /// <returns></returns>
        private bool Tb_HSPR_RenoCustRuleStandard_Insert(string IID, string RenoCustRuleID, string RenoID, string OrganCode, int CommID, string PatrolID, string StandardName, int StandardState, string StandardExplain)
        {
            string sqlStr = "insert into Tb_HSPR_RenoCustRuleStandard(IID,RenoCustRuleID,RenoID,OrganCode,CommID,PatrolID,StandardName,StandardState,StandardExplain) values(@IID,@RenoCustRuleID,@RenoID,@OrganCode,@CommID,@PatrolID,@StandardName,@StandardState,@StandardExplain)";
            IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("IID", IID);
            parameters.Add("RenoCustRuleID", RenoCustRuleID);
            parameters.Add("RenoID", RenoID);
            parameters.Add("OrganCode", OrganCode);
            parameters.Add("CommID", CommID);
            parameters.Add("PatrolID", PatrolID);
            parameters.Add("StandardName", StandardName);
            parameters.Add("StandardState", StandardState);
            parameters.Add("StandardExplain", StandardExplain);
            try
            {
                int result = conn.Execute(sqlStr, parameters, null, null, CommandType.Text);
                conn.Dispose();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// 核查任务列表（新）
        /// </summary>
        public string GetQualityInspectListNew(DataRow Row)
        {
            string result = "";
            try
            {
                string Timer = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");///时间段
                if (Row.Table.Columns.Contains("Timer") && Row["Timer"].ToString() != "")//当只传一个值时
                {
                    Timer = Convert.ToDateTime(Row["Timer"]).ToString();
                }
                //ItemCode 项目编号，TaskPId 责任人，BeginDate任务开始时间，endDate任务结束时间  yyyy/MM/dd  TaskStatus='未开始' AND
                if (Row.Table.Columns.Contains("ItemCode") && Row.Table.Columns.Contains("UserCode"))
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(" SELECT * FROM View_Qm_Phone_QualityInspectList WHERE ISNULL(ISDELETE,0)=0 and ( TaskStatus='未开始' or TaskStatus='执行中') AND ItemCode='" + Row["ItemCode"].ToString().Trim() + "' AND TaskRoleId IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + Row["UserCode"].ToString().Trim() + "'))  ");
                    if (Global_Var.LoginCorpID=="1971")//bug 10369 排除IsShanchu为1的项
                    { 
                        strSql = new StringBuilder();
                        strSql.Append(" SELECT * FROM View_Qm_Phone_QualityInspectList WHERE ISNULL(ISDELETE,0)=0 and ISNULL(IsShanchu, 0) = 0 and( TaskStatus='未开始' or TaskStatus='执行中') AND ItemCode='" + Row["ItemCode"].ToString().Trim() + "' AND TaskRoleId IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + Row["UserCode"].ToString().Trim() + "'))  ");
                    }
                    if (Timer != "")
                    {
                        Timer = Convert.ToDateTime(Timer).ToString("yyyy-MM-dd");
                        strSql.Append(" AND  BeginDateD <='" + Timer + " 00:00:00.000'  AND EndDateD >='" + Timer + " 23:59:59.000' ");
                    }
                    if (Global_Var.LoginCorpID == "1971")
                    {
                        strSql.Append(" AND isnull(IsShanchu,0)=0");
                    }

                    int PageIndex = 1;
                    int PageSize = 60;

                    if (Row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(Row["PageIndex"].ToString()) > 0)
                    {
                        PageIndex = AppGlobal.StrToInt(Row["PageIndex"].ToString());
                    }
                    if (Row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(Row["PageSize"].ToString()) > 0)
                    {
                        PageSize = AppGlobal.StrToInt(Row["PageSize"].ToString());
                    }
                    //strSql.Append(" ORDER BY BeginDate ASC,TaskNo ASC ");
                    //IDbConnection con = new SqlConnection(PubConstant.PrivateConnectionString);
                    int PageCount = 0, Counts = 0;
                    DataTable dt = GetList(out PageCount, out Counts, strSql.ToString(), PageIndex, PageSize, "BeginDate ASC,TaskNo ", 1, "TaskId", PubConstant.hmWyglConnectionString, "*").Tables[0];
                    // DataTable dt = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                    dt.Columns.Add("DataDetail", typeof(string));
                    HM.Model.Qm.Tb_Interface_Record Interface_Record = new HM.Model.Qm.Tb_Interface_Record();
                    Interface_Record.Type = "QualityTask";
                    Interface_Record.GetDate = DateTime.Now;
                    Interface_Record.ItemCode = Row["ItemCode"].ToString().Trim();
                    Interface_Record.UserCode = Row["UserCode"].ToString().Trim();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["DataDetail"] = GetQualityInspectSelDetailNew(dt.Rows[i]["TaskId"].ToString().Trim(), dt.Rows[i]["ItemCode"].ToString().Trim());
                        IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
                        Interface_Record.Id = Guid.NewGuid().ToString();
                        Interface_Record.TaskId = dt.Rows[i]["TaskId"].ToString().Trim();
                        //任务记录
                        con.Insert<HM.Model.Qm.Tb_Interface_Record>(Interface_Record);
                    }
                    result = JSONHelper.FromString(dt);
                }
                else
                {
                    result = JSONHelper.FromString(false, "缺少参数ItemCode/UserCode/Timer");
                }
            }
            catch (Exception EX)
            {

                result = JSONHelper.FromString(false, EX.ToString());
            }

            return result;
        }
        ///// <summary>
        ///// 核查任务列表（新）
        ///// </summary>
        //public string GetQualityInspectListNew(DataRow Row)
        //{
        //    string result = "";
        //    try
        //    {
        //        string Timer = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");///时间段
        //        if (Row.Table.Columns.Contains("Timer") && Row["Timer"].ToString() != "")//当只传一个值时
        //        {
        //            Timer = Convert.ToDateTime(Row["Timer"]).ToString();
        //        }
        //        //ItemCode 项目编号，TaskPId 责任人，BeginDate任务开始时间，endDate任务结束时间  yyyy/MM/dd  TaskStatus='未开始' AND
        //        if (Row.Table.Columns.Contains("ItemCode") && Row.Table.Columns.Contains("UserCode"))
        //        {
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append(" SELECT *,'' as DataDetail FROM View_Qm_Phone_QualityInspectList WHERE ISNULL(ISDELETE,0)=0 and ( TaskStatus='未开始' or TaskStatus='执行中') AND ItemCode='" + Row["ItemCode"].ToString().Trim() + "' AND TaskRoleId IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + Row["UserCode"].ToString().Trim() + "'))  ");
        //            if (Timer != "")
        //            {
        //                Timer = Convert.ToDateTime(Timer).ToString("yyyy-MM-dd");
        //                strSql.Append(" AND  BeginDateD <='" + Timer + " 00:00:00.000'  AND EndDateD >='" + Timer + " 23:59:59.000' ");
        //            }
        //            int PageIndex = 1;
        //            int PageSize = 60;

        //            if (Row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(Row["PageIndex"].ToString()) > 0)
        //            {
        //                PageIndex = AppGlobal.StrToInt(Row["PageIndex"].ToString());
        //            }
        //            if (Row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(Row["PageSize"].ToString()) > 0)
        //            {
        //                PageSize = AppGlobal.StrToInt(Row["PageSize"].ToString());
        //            }
        //            //strSql.Append(" ORDER BY BeginDate ASC,TaskNo ASC ");
        //            //IDbConnection con = new SqlConnection(PubConstant.PrivateConnectionString);
        //            int PageCount = 0, Counts = 0;

        //            /////////

        //            PageCount = 0;
        //            Counts = 0;
        //            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
        //            DynamicParameters parameters = new DynamicParameters();

        //            parameters.Add("@FldName", "*");
        //            parameters.Add("@PageSize", PageSize);
        //            parameters.Add("@PageIndex", PageIndex);
        //            parameters.Add("@FldSort", "BeginDate ASC,TaskNo ");
        //            parameters.Add("@Sort", 1);

        //            parameters.Add("@StrCondition", strSql.ToString());
        //            parameters.Add("@Id", "TaskId");
        //            parameters.Add("@PageCount", PageCount);
        //            parameters.Add("@Counts", Counts);
        //            IEnumerable<dynamic> taskSet = 

        //              //con.Query(strSql.ToString());


        //                con.ExecuteReader("Proc_System_TurnPage", parameters, null, null, CommandType.StoredProcedure);

        //            /////////Ds.Tables[0]
        //            DataTable dt = new DataTable();

        //            HM.Model.Qm.Tb_Interface_Record Interface_Record = new HM.Model.Qm.Tb_Interface_Record();
        //            Interface_Record.Type = "QualityTask";
        //            Interface_Record.GetDate = DateTime.Now;
        //            Interface_Record.ItemCode = Row["ItemCode"].ToString().Trim();
        //            Interface_Record.UserCode = Row["UserCode"].ToString().Trim();
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                dt.Rows[i]["DataDetail"] = GetQualityInspectSelDetailNew(dt.Rows[i]["TaskId"].ToString().Trim(), dt.Rows[i]["ItemCode"].ToString().Trim());
        //                IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
        //                Interface_Record.Id = Guid.NewGuid().ToString();
        //                Interface_Record.TaskId = dt.Rows[i]["TaskId"].ToString().Trim();
        //                //任务记录
        //                con.Insert<HM.Model.Qm.Tb_Interface_Record>(Interface_Record);
        //            }
        //            result = JSONHelper.FromString(dt);
        //        }
        //        else
        //        {
        //            result = JSONHelper.FromString(false, "缺少参数ItemCode/UserCode/Timer");
        //        }
        //    }
        //    catch (Exception EX)
        //    {

        //        result = JSONHelper.FromString(false, EX.ToString());
        //    }

        //    return result;
        //}
        ///// <summary>
        ///// 核查任务列表+批转（新）
        ///// </summary>
        //public string GetQualityInspectListApprovedNew(DataRow Row)
        //{
        //    string result = "";
        //    try
        //    {
        //        string Timer = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");///时间段
        //        if (Row.Table.Columns.Contains("Timer") && Row["Timer"].ToString() != "")//当只传一个值时
        //        {
        //            Timer = Convert.ToDateTime(Row["Timer"]).ToString();
        //        }
        //        //ItemCode 项目编号，TaskPId 责任人，BeginDate任务开始时间，endDate任务结束时间  yyyy/MM/dd  TaskStatus='未开始' AND
        //        if (Row.Table.Columns.Contains("ItemCode") && Row.Table.Columns.Contains("UserCode"))
        //        {
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append(" SELECT * FROM View_Qm_Phone_QualityInspectList WHERE ISNULL(ISDELETE,0)=0 and ( TaskStatus='未开始' or TaskStatus='执行中') AND ItemCode='" + Row["ItemCode"].ToString().Trim() + "' AND TaskRoleId IN (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + Row["UserCode"].ToString().Trim() + "'))  ");
        //            if (Timer != "")
        //            {
        //                Timer = Convert.ToDateTime(Timer).ToString("yyyy-MM-dd");
        //                strSql.Append(" AND  BeginDateD <='" + Timer + " 00:00:00.000'  AND EndDateD >='" + Timer + " 23:59:59.000' ");
        //            }
        //            int PageIndex = 1;
        //            int PageSize = 60;

        //            if (Row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(Row["PageIndex"].ToString()) > 0)
        //            {
        //                PageIndex = AppGlobal.StrToInt(Row["PageIndex"].ToString());
        //            }
        //            if (Row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(Row["PageSize"].ToString()) > 0)
        //            {
        //                PageSize = AppGlobal.StrToInt(Row["PageSize"].ToString());
        //            }
        //            //strSql.Append(" ORDER BY BeginDate ASC,TaskNo ASC ");
        //            //IDbConnection con = new SqlConnection(PubConstant.PrivateConnectionString);
        //            int PageCount = 0, Counts = 0;
        //            DataTable dt = GetList(out PageCount, out Counts, strSql.ToString(), PageIndex, PageSize, "BeginDate ASC,TaskNo ", 1, "TaskId", PubConstant.PrivateConnectionString, "*").Tables[0];
        //            // DataTable dt = con.ExecuteReader(strSql.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
        //            dt.Columns.Add("DataDetail", typeof(string));
        //            HM.Model.Qm.Tb_Interface_Record Interface_Record = new HM.Model.Qm.Tb_Interface_Record();
        //            Interface_Record.Type = "QualityTask";
        //            Interface_Record.GetDate = DateTime.Now;
        //            Interface_Record.ItemCode = Row["ItemCode"].ToString().Trim();
        //            Interface_Record.UserCode = Row["UserCode"].ToString().Trim();
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                dt.Rows[i]["DataDetail"] = GetQualityInspectSelDetailApprovedNew(dt.Rows[i]["TaskId"].ToString().Trim(), dt.Rows[i]["ItemCode"].ToString().Trim());
        //                IDbConnection con = new SqlConnection(PubConstant.PrivateConnectionString);
        //                Interface_Record.Id = Guid.NewGuid().ToString();
        //                Interface_Record.TaskId = dt.Rows[i]["TaskId"].ToString().Trim();
        //                //任务记录
        //                con.Insert<HM.Model.Qm.Tb_Interface_Record>(Interface_Record);
        //            }
        //            result = JSONHelper.FromString(dt);
        //        }
        //        else {
        //            result = JSONHelper.FromString(false, "缺少参数ItemCode/UserCode/Timer");
        //        }
        //    }
        //    catch (Exception EX)
        //    {

        //        result = JSONHelper.FromString(false, EX.ToString());
        //    }

        //    return result;
        //}

        public string GetQualityInspectSelDetailApprovedNew(string TaskId, string ProjectCode)
        {
            #region 
            //PubConstant.PrivateConnectionString
            //IDbConnection con = new SqlConnection(PubConstant.PrivateConnectionString);
            //Tb_System_BusinessCorp m = con.Query<Tb_System_BusinessCorp>("select BussId, BussName, BussAddress, BussLinkMan, BussMobileTel, BussWorkedTel, BussWeiXin, LogoImgUrl from dbo.Tb_System_BusinessCorp where BussId=@BussId ", new { BussId = BussId }).SingleOrDefault<Tb_System_BusinessCorp>();

            //DataSet ds = con.ExecuteReader("select BussId, BussName, BussAddress, BussLinkMan, BussMobileTel, BussWorkedTel, BussWeiXin, LogoImgUrl from dbo.Tb_System_BusinessCorp where BussId='" + BussId + "'").ToDataSet();
            //return ds;
            #endregion
            string result = "";
            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
            try
            {
                //TaskId 任务ID
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TaskId", TaskId);
                parameters.Add("@ProjectCode", ProjectCode);
                DataSet ds = new DataSet();
                #region 核查点位

                #region Old
                int dwCount = 0;
                DataTable dTable = con.ExecuteReader("Proc_Qm_Phone_InspectPoint", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
                dwCount = dTable.Rows.Count;
                dTable.Columns.Add("TaskId", typeof(string));
                if (!dTable.Columns.Contains("AddTime") && !dTable.Columns.Contains("AddPId"))
                {
                    dTable.Columns.Add("AddTime", typeof(string));//点位查询时间 添加时间2016.5.23
                    dTable.Columns.Add("AddPId", typeof(string));//点位查询人 添加时间2016.5.23
                }
                con = new SqlConnection(PubConstant.hmWyglConnectionString);
                DataTable dtTaskPoint = con.ExecuteReader("SELECT * FROM Tb_Qm_TaskPoint WHERE taskId='" + TaskId + "' AND ISNULL(ISDELETE,0)=0 ", null, null, null, CommandType.Text).ToDataSet().Tables[0];//拼接以查点位
                DataRow[] dtTaskPointRows;
                if (!dTable.Columns.Contains("Address"))
                {
                    dTable.Columns.Add("Address", typeof(string));//点位地址
                }
                for (int i = 0; i < dTable.Rows.Count; i++)
                {
                    dTable.Rows[i]["TaskId"] = TaskId;
                    if (dtTaskPoint.Rows.Count > 0)
                    {
                        dtTaskPointRows = dtTaskPoint.Select("PointIds='" + dTable.Rows[i]["PointId"] + "'");
                        if (dtTaskPointRows.Length > 0)
                        {
                            if (!Convert.IsDBNull(dtTaskPointRows[0]["AddTime"]))
                                dTable.Rows[i]["AddTime"] = Convert.ToDateTime(dtTaskPointRows[0]["AddTime"]).ToString("yyyy/MM/dd HH:mm:ss");
                            dTable.Rows[i]["AddPId"] = dtTaskPointRows[0]["AddPId"];
                        }
                        else
                        {
                            dTable.Rows[i]["AddTime"] = "";
                            dTable.Rows[i]["AddPId"] = "";
                        }
                    }
                    else
                    {
                        dTable.Rows[i]["AddTime"] = "";
                        dTable.Rows[i]["AddPId"] = "";
                    }
                }
                dTable.TableName = "Position";
                DataView dv = new DataView(dTable);
                dTable = dv.ToTable(true, GetColumnsByDataTable(dTable));
                ds.Tables.Add(dTable);
                dTable.Dispose();
                #endregion

                #region New
                //int dwCount = 0;
                //con = new SqlConnection(PubConstant.PrivateConnectionString);
                //DataTable dTable = con.ExecuteReader("SELECT * FROM View_Tb_Qm_TaskPoint_Message_Phone_Filter WHERE TaskId='" + TaskId + "' ", null, null, null, CommandType.Text).ToDataSet().Tables[0];
                //dwCount = dTable.Rows.Count;
                //dTable.TableName = "Position";
                //DataView dv = new DataView(dTable);
                //dTable = dv.ToTable(true, GetColumnsByDataTable(dTable));
                //ds.Tables.Add(dTable);
                //dTable.Dispose();
                #endregion

                #endregion
                #region 核查任务
                //con = new SqlConnection(PubConstant.PrivateConnectionString);
                //DataSet dSet = con.ExecuteReader("SELECT * FROM Tb_QM_TaskExtend WHERE TaskId='" + TaskId + "' ", null, null, null, CommandType.Text).ToDataSet();
                //dTable = dSet.Tables[0].Copy();
                //dTable.Rows[0]["ZZDWCOUNT"] = dwCount;
                //dTable.TableName = "Task";
                //ds.Tables.Add(dTable);
                //dTable.Dispose();
                //dSet.Dispose();
                con = new SqlConnection(PubConstant.hmWyglConnectionString);
                DataSet dSet = con.ExecuteReader("Proc_Qm_Phone_InspectTask", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
                dTable = dSet.Tables[0].Copy();
                dTable.Rows[0]["ZZDWCOUNT"] = dwCount;
                dTable.TableName = "Task";
                ds.Tables.Add(dTable);
                dTable.Dispose();
                dSet.Dispose();
                #endregion
                #region 核查记录
                //con = new SqlConnection(PubConstant.PrivateConnectionString);
                //dSet = con.ExecuteReader("SELECT * FROM View_Tb_Qm_TaskAbarbeitung_Phone WHERE ISNULL(ISDELETE,0)=0 AND taskId='" + TaskId + "'", null, null, null, CommandType.Text).ToDataSet();
                //dTable = dSet.Tables[0].Copy();
                //dTable.TableName = "Record";
                //ds.Tables.Add(dTable);
                //dSet.Dispose();
                //dTable.Dispose();
                con = new SqlConnection(PubConstant.hmWyglConnectionString);
                dSet = con.ExecuteReader("SELECT * FROM View_Tb_Qm_TaskAbarbeitung_Phone WHERE ISNULL(ISDELETE,0)=0 AND taskId='" + TaskId + "'", null, null, null, CommandType.Text).ToDataSet();
                dTable = dSet.Tables[0].Copy();
                dTable.TableName = "Record";
                ds.Tables.Add(dTable);
                dSet.Dispose();
                dTable.Dispose();
                #endregion
                #region 批转记录
                con = new SqlConnection(PubConstant.hmWyglConnectionString);
                dSet = con.ExecuteReader("SELECT * FROM View_TB_QM_APPROVED_File WHERE taskId='" + TaskId + "'", null, null, null, CommandType.Text).ToDataSet();
                dTable = dSet.Tables[0].Copy();
                dTable.TableName = "Approved";
                ds.Tables.Add(dTable);
                dSet.Dispose();
                dTable.Dispose();
                #endregion
                #region 附件列表

                con = new SqlConnection(PubConstant.hmWyglConnectionString);
                dSet = con.ExecuteReader("SELECT FileName,Fix,FilePath,TaskId FROM Tb_Qm_TaskFiles  WHERE ISNULL(ISDELETE,0)=0 AND  TaskId = '" + TaskId + "'", null, null, null, CommandType.Text).ToDataSet();
                dTable = dSet.Tables[0].Copy();
                dTable.TableName = "Files";
                ds.Tables.Add(dTable);
                dSet.Dispose();
                dTable.Dispose();
                #endregion
                result = JSONHelper.FromString(ds, true, false);

            }
            catch (Exception EX)
            {
                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }
        /// <summary>
        /// 核查任务详细（新）
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string GetQualityInspectSelDetailNew(string TaskId, string ProjectCode)
        {
            #region 
            //PubConstant.PrivateConnectionString
            //IDbConnection con = new SqlConnection(PubConstant.PrivateConnectionString);
            //Tb_System_BusinessCorp m = con.Query<Tb_System_BusinessCorp>("select BussId, BussName, BussAddress, BussLinkMan, BussMobileTel, BussWorkedTel, BussWeiXin, LogoImgUrl from dbo.Tb_System_BusinessCorp where BussId=@BussId ", new { BussId = BussId }).SingleOrDefault<Tb_System_BusinessCorp>();

            //DataSet ds = con.ExecuteReader("select BussId, BussName, BussAddress, BussLinkMan, BussMobileTel, BussWorkedTel, BussWeiXin, LogoImgUrl from dbo.Tb_System_BusinessCorp where BussId='" + BussId + "'").ToDataSet();
            //return ds;
            #endregion
            string result = "";
            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
            try
            {
                //TaskId 任务ID
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TaskId", TaskId);
                parameters.Add("@ProjectCode", ProjectCode);
                DataSet ds = new DataSet();
                #region 核查点位

                #region Old
                int boolInterface = 0;
                int dwCount = 0;
                DataTable dTable = new DataTable();
                // 谭洋2018年9月5日15:15:38
                //dTable = con.ExecuteReader("Proc_Tb_Qm_Task_PointInterface", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
                //if (dTable != null && dTable.Rows.Count > 0)
                //{
                //    dwCount = dTable.Rows.Count;
                //}
                //else
                {
                    boolInterface = 1;
                    con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    dTable = con.ExecuteReader("Proc_Qm_Phone_InspectPoint", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
                    dwCount = dTable.Rows.Count;
                    dTable.Columns.Add("TaskId", typeof(string));
                    if (!dTable.Columns.Contains("AddTime") && !dTable.Columns.Contains("AddPId"))
                    {
                        dTable.Columns.Add("AddTime", typeof(string));//点位查询时间 添加时间2016.5.23
                        dTable.Columns.Add("AddPId", typeof(string));//点位查询人 添加时间2016.5.23
                    }
                    con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    DataTable dtTaskPoint = con.ExecuteReader("SELECT * FROM Tb_Qm_TaskPoint WHERE taskId='" + TaskId + "' AND ISNULL(ISDELETE,0)=0 ", null, null, null, CommandType.Text).ToDataSet().Tables[0];//拼接以查点位
                    DataRow[] dtTaskPointRows;
                    if (!dTable.Columns.Contains("Address"))
                    {
                        dTable.Columns.Add("Address", typeof(string));//点位地址
                    }
                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        dTable.Rows[i]["TaskId"] = TaskId;
                        if (dtTaskPoint.Rows.Count > 0)
                        {
                            dtTaskPointRows = dtTaskPoint.Select("PointIds='" + dTable.Rows[i]["PointId"] + "'");
                            if (dtTaskPointRows.Length > 0)
                            {
                                if (!Convert.IsDBNull(dtTaskPointRows[0]["AddTime"]))
                                    dTable.Rows[i]["AddTime"] = Convert.ToDateTime(dtTaskPointRows[0]["AddTime"]).ToString("yyyy/MM/dd HH:mm:ss");
                                dTable.Rows[i]["AddPId"] = dtTaskPointRows[0]["AddPId"];
                            }
                            else
                            {
                                dTable.Rows[i]["AddTime"] = "";
                                dTable.Rows[i]["AddPId"] = "";
                            }
                        }
                        else
                        {
                            dTable.Rows[i]["AddTime"] = "";
                            dTable.Rows[i]["AddPId"] = "";
                        }
                    }
                }
                dTable.TableName = "Position";
                //if(boolInterface==1)
                //{
                DataView dv = new DataView(dTable);
                dTable = dv.ToTable(true, GetColumnsByDataTable(dTable));
                // }
                ds.Tables.Add(dTable);
                dTable.Dispose();
                #endregion

                #region New
                //int dwCount = 0;
                //con = new SqlConnection(PubConstant.PrivateConnectionString);
                //DataTable dTable = con.ExecuteReader("SELECT * FROM View_Tb_Qm_TaskPoint_Message_Phone_Filter WHERE TaskId='" + TaskId + "' ", null, null, null, CommandType.Text).ToDataSet().Tables[0];
                //dwCount = dTable.Rows.Count;
                //dTable.TableName = "Position";
                //DataView dv = new DataView(dTable);
                //dTable = dv.ToTable(true, GetColumnsByDataTable(dTable));
                //ds.Tables.Add(dTable);
                //dTable.Dispose();
                #endregion

                #endregion
                #region 核查任务
                //con = new SqlConnection(PubConstant.PrivateConnectionString);
                //DataSet dSet = con.ExecuteReader("SELECT * FROM Tb_QM_TaskExtend WHERE TaskId='" + TaskId + "' ", null, null, null, CommandType.Text).ToDataSet();
                //dTable = dSet.Tables[0].Copy();
                //dTable.Rows[0]["ZZDWCOUNT"] = dwCount;
                //dTable.TableName = "Task";
                //ds.Tables.Add(dTable);
                //dTable.Dispose();
                //dSet.Dispose();
                con = new SqlConnection(PubConstant.hmWyglConnectionString);
                DataSet dSet = con.ExecuteReader("Proc_Qm_Phone_InspectTask", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
                dTable = dSet.Tables[0].Copy();
                dTable.Rows[0]["ZZDWCOUNT"] = dwCount;
                dTable.TableName = "Task";
                ds.Tables.Add(dTable);
                dTable.Dispose();
                dSet.Dispose();
                #endregion
                #region 核查记录
                //con = new SqlConnection(PubConstant.PrivateConnectionString);
                //dSet = con.ExecuteReader("SELECT * FROM View_Tb_Qm_TaskAbarbeitung_Phone WHERE ISNULL(ISDELETE,0)=0 AND taskId='" + TaskId + "'", null, null, null, CommandType.Text).ToDataSet();
                //dTable = dSet.Tables[0].Copy();
                //dTable.TableName = "Record";
                //ds.Tables.Add(dTable);
                //dSet.Dispose();
                //dTable.Dispose();
                con = new SqlConnection(PubConstant.hmWyglConnectionString);
                dSet = con.ExecuteReader("SELECT * FROM View_Tb_Qm_TaskAbarbeitung_Phone WHERE ISNULL(ISDELETE,0)=0 AND taskId='" + TaskId + "'", null, null, null, CommandType.Text).ToDataSet();
                dTable = dSet.Tables[0].Copy();
                dTable.TableName = "Record";
                ds.Tables.Add(dTable);
                dSet.Dispose();
                dTable.Dispose();
                #endregion
                #region 附件列表

                con = new SqlConnection(PubConstant.hmWyglConnectionString);
                dSet = con.ExecuteReader("SELECT FileName,Fix,FilePath,TaskId FROM Tb_Qm_TaskFiles  WHERE ISNULL(ISDELETE,0)=0 AND  TaskId = '" + TaskId + "'", null, null, null, CommandType.Text).ToDataSet();
                dTable = dSet.Tables[0].Copy();
                dTable.TableName = "Files";
                ds.Tables.Add(dTable);
                dSet.Dispose();
                dTable.Dispose();
                #endregion
                result = JSONHelper.FromString(ds, true, false);

            }
            catch (Exception EX)
            {
                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }
        #endregion


    }
}