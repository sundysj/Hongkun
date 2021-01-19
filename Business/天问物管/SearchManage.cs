using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Business
{
    class SearchManage : PubInfo
    {
        public SearchManage()
        {
            base.Token = "20160523SearchManage";
        }
        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = new ApiResult(false, "未知错误").toJson();
            try
            {
                DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];
                //验证登录
                if (!new Login().isLogin(ref Trans))
                    return;

                //项目，责任人，时间
                switch (Trans.Command)
                {
                    //获取客户信息
                    case "GetCustomerDetail":
                        Trans.Result = GetCustomerDetail(Row);
                        break;
                    //获取房屋号码
                    case "GetRoomSign":
                        Trans.Result = GetRoomSign(Row);
                        break;
                    //获取通讯录号码
                    case "GetTelSearch":
                        Trans.Result = GetTelSearch(Row);
                        break;
                    //获取报事信息
                    case "GetReportSearch":
                        Trans.Result = GetReportSearch(Row);
                        break;
                    case "GetReportCommList":
                        Trans.Result = GetReportCommList(Row);
                        break;
                    case "GetMaterialSearch":
                        Trans.Result = GetMaterialSearch(Row);
                        break;
                    //获取车位列表
                    case "GetParkNameList":
                        Trans.Result = GetParkNameList(Row);
                        break;
                    //获取车牌列表
                    case "GetCarNumList":
                        Trans.Result = GetCarNumList(Row);
                        break;
                    //获取车位信息
                    case "GetParkSearch":
                        Trans.Result = GetParkSearch(Row);
                        break;
                    //获取设备档案，大事记，维保记录，维修记录
                    case "GetEquipmentSearch":
                        Trans.Result = GetEquipmentSearch(Row);
                        break;
                    
                    //获取设备名称
                    case "GetEquipmentNameSearch":
                        Trans.Result = GetEquipmentNameSearch(Row);
                        break;
                    case "GetEquipmentNumSearch":
                        Trans.Result = GetEquipmentNumSearch(Row);
                        break;
                        #region 设备查询(新拆分成单独接口)
                    //获取设备档案
                    case "GetEquipmentSearch_Archive":
                        Trans.Result = GetEquipmentSearch_Archive(Row);
                        break;
                    //获取设备状态
                    case "GetEquipmentSearch_Status":
                        Trans.Result = GetEquipmentSearch_Status(Row);
                        break;
                    //获取设备大事记
                    case "GetEquipmentSearch_Event":
                        Trans.Result = GetEquipmentSearch_Event(Row);
                        break;
                    //获取设备巡检历史
                    case "GetEquipmentSearch_PatrolList":
                        Trans.Result = GetEquipmentSearch_PatrolList(Row);
                        break;
                    //获取设备维保历史
                    case "GetEquipmentSearch_MaintenanceList":
                        Trans.Result = GetEquipmentSearch_MaintenanceList(Row);
                        break;
                    //获取设备维修历史
                    case "GetEquipmentSearch_IncidentList":
                        Trans.Result = GetEquipmentSearch_IncidentList(Row);
                        break;
                        #endregion
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex);
                Trans.Result = new ApiResult(false, "接口抛出了一个异常").toJson();
            }
            
        }

        #region 设备查询(新拆分成单独接口)

        private string GetEquipmentSearch_IncidentList(DataRow row)
        {

            #region 获取基本参数

            string EquiId = string.Empty;
            if (row.Table.Columns.Contains("EquiId"))
            {
                EquiId = row["EquiId"].ToString();
            }
            if (string.IsNullOrEmpty(EquiId))
            {
                return new ApiResult(false, "EquiId不能为空").toJson();
            }
            int page = 1;
            int size = 10;
            if (row.Table.Columns.Contains("Page"))
            {
                if (!int.TryParse(row["Page"].ToString(), out page))
                {
                    page = 1;
                }
                if (page <= 0)
                {
                    page = 1;
                }
            }
            if (row.Table.Columns.Contains("Size"))
            {
                if (!int.TryParse(row["Size"].ToString(), out size))
                {
                    size = 10;
                }
                if (page <= 0)
                {
                    size = 10;
                }
            }

            // 规则page*size +1 , (page + 1)*size，page 从0开始
            int Start = (page - 1) * size + 1;
            int End = page * size;
            #endregion
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                List<dynamic> list = conn.Query("SELECT IncidentID,IncidentNum,ReserveDate,IncidentContent,DealSituation,FinishDate,FinishUser FROM (SELECT *, ROW_NUMBER() OVER(ORDER BY a.IncidentDate DESC) AS RowId FROM Tb_HSPR_IncidentAccept AS a WHERE ISNULL(a.IsDelete,0) = 0 AND a.EqId = @EquiId) AS a WHERE RowId BETWEEN @Start AND @End", new { EquiId, Start, End }).ToList();
                if (null == list)
                {
                    list = new List<dynamic>();
                }
                return new ApiResult(true, list).toJson();
            }
        }
        /// <summary>
        /// 获取设备维保历史
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetEquipmentSearch_MaintenanceList(DataRow row)
        {

            #region 获取基本参数

            string EquiId = string.Empty;
            if (row.Table.Columns.Contains("EquiId"))
            {
                EquiId = row["EquiId"].ToString();
            }
            if (string.IsNullOrEmpty(EquiId))
            {
                return new ApiResult(false, "EquiId不能为空").toJson();
            }
            int page = 1;
            int size = 10;
            if (row.Table.Columns.Contains("Page"))
            {
                if (!int.TryParse(row["Page"].ToString(), out page))
                {
                    page = 1;
                }
                if (page <= 0)
                {
                    page = 1;
                }
            }
            if (row.Table.Columns.Contains("Size"))
            {
                if (!int.TryParse(row["Size"].ToString(), out size))
                {
                    size = 10;
                }
                if (page <= 0)
                {
                    size = 10;
                }
            }

            // 规则page*size +1 , (page + 1)*size，page 从0开始
            int Start = (page - 1) * size + 1;
            int End = page * size;
            #endregion
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                try
                {
                    List<dynamic> list = conn.Query("SELECT PollingResult,BeginTime,EndTime,Statue,HandlePIdName,DoTime,PollingDate FROM (SELECT a.PollingResult,b.BeginTime,b.EndTime,b.Statue,b.PollingPersonName AS HandlePIdName,b.DoTime,b.PollingDate, ROW_NUMBER() OVER(ORDER BY a.AddDate DESC) AS RowId FROM View_EQ_WbPatrolEquipmentList AS a JOIN View_EQ_WbPatrolTaskExecList b ON b.TaskId = a.TaskId WHERE ISNULL(a.IsDelete,0) = 0 AND ISNULL(b.IsDelete,0) = 0 AND (b.Statue = '已完成' OR b.Statue = '已过期') AND a.EquiId = @EquiId) AS a WHERE RowId BETWEEN @Start AND @End", new { EquiId, Start, End }).ToList();
                    if (null == list)
                    {
                        list = new List<dynamic>();
                    }
                    return new ApiResult(true, list).toJson();
                }
                catch (Exception)
                {
                    List<dynamic> list = conn.Query("SELECT PollingResult,BeginTime,EndTime,Statue,HandlePIdName,PollingDate FROM (SELECT a.PollingResult,b.BeginTime,b.EndTime,b.Statue,b.PollingPersonName AS HandlePIdName,b.DoTime,b.PollingDate, ROW_NUMBER() OVER(ORDER BY a.AddDate DESC) AS RowId FROM View_EQ_WbPatrolEquipmentList AS a JOIN View_EQ_WbPatrolTaskExecList b ON b.TaskId = a.TaskId WHERE ISNULL(a.IsDelete,0) = 0 AND ISNULL(b.IsDelete,0) = 0 AND (b.Statue = '已完成' OR b.Statue = '已过期') AND a.EquiId = @EquiId) AS a WHERE RowId BETWEEN @Start AND @End", new { EquiId, Start, End }).ToList();
                    if (null == list)
                    {
                        list = new List<dynamic>();
                    }
                    return new ApiResult(true, list).toJson();
                }
             
            }
        }

        /// <summary>
        /// 查询设备巡检内容
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>

        private string GetEquipmentSearch_PatrolList(DataRow row)
        {
            #region 获取基本参数

            string EquiId = string.Empty;
            if (row.Table.Columns.Contains("EquiId"))
            {
                EquiId = row["EquiId"].ToString();
            }
            if (string.IsNullOrEmpty(EquiId))
            {
                return new ApiResult(false, "EquiId不能为空").toJson();
            }
            int page = 1;
            int size = 10;
            if (row.Table.Columns.Contains("Page"))
            {
                if (!int.TryParse(row["Page"].ToString(), out page))
                {
                    page = 1;
                }
                if (page <= 0)
                {
                    page = 1;
                }
            }
            if (row.Table.Columns.Contains("Size"))
            {
                if (!int.TryParse(row["Size"].ToString(), out size))
                {
                    size = 10;
                }
                if (page <= 0)
                {
                    size = 10;
                }
            }

            // 规则page*size +1 , (page + 1)*size，page 从0开始
            int Start = (page - 1) * size + 1;
            int End = page * size;
            #endregion

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                try
                {
                    // 只显示已完成数据
                    List<dynamic> list = conn.Query("SELECT PollingResult,BeginTime,EndTime,Statue,PollingPersonName,DoTime,PollingDate " +
                        "FROM (SELECT a.PollingResult,b.BeginTime,b.EndTime,b.Statue,b.PollingPersonName,b.DoTime,b.PollingDate, ROW_NUMBER() OVER(ORDER BY a.AddDate DESC) AS RowId " +
                        "FROM View_EQ_PatrolEquipmentList AS a JOIN View_EQ_PatrolTaskExecList b ON b.TaskId = a.TaskId WHERE ISNULL(a.IsDelete,0) = 0 AND ISNULL(b.IsDelete,0) = 0 AND (b.Statue = '已完成') AND a.EquiId = @EquiId) AS a WHERE RowId BETWEEN @Start AND @End", new { EquiId, Start, End }).ToList();
                    if (null == list)
                    {
                        list = new List<dynamic>();
                    }
                    return new ApiResult(true, list).toJson();
                }
                catch (Exception)
                {
                    // 只显示已完成数据
                    List<dynamic> list = conn.Query("SELECT PollingResult,BeginTime,EndTime,Statue,PollingPersonName,PollingDate " +
                        "FROM (SELECT a.PollingResult,b.BeginTime,b.EndTime,b.Statue,b.PollingPersonName,b.DoTime,b.PollingDate, ROW_NUMBER() OVER(ORDER BY a.AddDate DESC) AS RowId " +
                        "FROM View_EQ_PatrolEquipmentList AS a JOIN View_EQ_PatrolTaskExecList b ON b.TaskId = a.TaskId WHERE ISNULL(a.IsDelete,0) = 0 AND ISNULL(b.IsDelete,0) = 0 AND (b.Statue = '已完成') AND a.EquiId = @EquiId) AS a WHERE RowId BETWEEN @Start AND @End", new { EquiId, Start, End }).ToList();
                    if (null == list)
                    {
                        list = new List<dynamic>();
                    }
                    return new ApiResult(true, list).toJson();
                }

            }
        }

        /// <summary>
        /// 获取设备的大事记信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetEquipmentSearch_Event(DataRow row) {
            #region 获取基本参数

            string EquiId = string.Empty;
            if (row.Table.Columns.Contains("EquiId"))
            {
                EquiId = row["EquiId"].ToString();
            }
            if (string.IsNullOrEmpty(EquiId))
            {
                return new ApiResult(false, "EquiId不能为空").toJson();
            }
            int page = 1;
            int size = 10;
            if (row.Table.Columns.Contains("Page"))
            {
                if (!int.TryParse(row["Page"].ToString(), out page))
                {
                    page = 1;
                }
                if (page <= 0)
                {
                    page = 1;
                }
            }
            if (row.Table.Columns.Contains("Size"))
            {
                if (!int.TryParse(row["Size"].ToString(), out size))
                {
                    size = 10;
                }
                if (page <= 0)
                {
                    size = 10;
                }
            }

            // 规则page*size +1 , (page + 1)*size，page 从0开始
            int Start = (page - 1) * size + 1;
            int End = page * size;
            #endregion

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                List<dynamic> list = conn.Query("SELECT HappenDate,EventContent,ResponsibilityPersonName,EventTypeName AS EventType FROM (SELECT *, ROW_NUMBER() OVER(ORDER BY Sort DESC) AS RowId FROM VIEW_EQ_GetEventChronicle AS a WHERE ISNULL(IsDelete,0) = 0 AND EquiId = @EquiId) AS a WHERE RowId BETWEEN @Start AND @End", new { EquiId, Start, End }).ToList();
                return new ApiResult(true, list).toJson();
            }
        }
        /// <summary>
        /// 获取设备的状态信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetEquipmentSearch_Status(DataRow row)
        {
            #region 获取基本参数

            string EquiId = string.Empty;
            if (row.Table.Columns.Contains("EquiId"))
            {
                EquiId = row["EquiId"].ToString();
            }
            if (string.IsNullOrEmpty(EquiId))
            {
                return new ApiResult(false, "EquiId不能为空").toJson();
            }
            int page = 1;
            int size = 10;
            if (row.Table.Columns.Contains("Page"))
            {
                if (!int.TryParse(row["Page"].ToString(), out page))
                {
                    page = 1;
                }
                if (page <= 0)
                {
                    page = 1;
                }
            }
            if (row.Table.Columns.Contains("Size"))
            {
                if (!int.TryParse(row["Size"].ToString(), out size))
                {
                    size = 10;
                }
                if (page <= 0)
                {
                    size = 10;
                }
            }

            // 规则page*size +1 , (page + 1)*size，page 从0开始
            int Start = (page - 1) * size + 1;
            int End = page * size;
            #endregion

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                List<dynamic> list = conn.Query("SELECT BeginTime,EndTime,EquipmentStatus,Remark,AddTime FROM (SELECT *, ROW_NUMBER() OVER(ORDER BY AddTime DESC) AS RowId FROM View_Tb_Eq_EquipmentStatus_Filter AS a WHERE ISNULL(IsDelete,0) = 0 AND EquipmentId = @EquiId) AS a WHERE RowId BETWEEN @Start AND @End", new { EquiId, Start, End }).ToList();
                return new ApiResult(true, list).toJson();
            }

        }
        /// <summary>
        /// 获取设备档案信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetEquipmentSearch_Archive(DataRow row)
        {
            #region 获取基本参数

            string EquiId = string.Empty;
            if (row.Table.Columns.Contains("EquiId"))
            {
                EquiId = row["EquiId"].ToString();
            }
            if (string.IsNullOrEmpty(EquiId))
            {
                return new ApiResult(false, "EquiId不能为空").toJson();
            }
            #endregion

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dynamic info = conn.QueryFirstOrDefault("SELECT * FROM VIEW_QM_SelEquipmentInformation WHERE Id = @EquiId", new { EquiId });
                return new ApiResult(true, info).toJson();
            }
        }
        #endregion
        #region 获取车牌列表
        private string GetCarNumList(DataRow Row)
        {
            string result = JSONHelper.FromString(false, "未知错误!");
            try
            {
                if (Row.Table.Columns.Contains("CommID") && Row["CommID"].ToString() != "" && Row.Table.Columns.Contains("Key") && Row["Key"] != null)
                {
                    MobileSoft.BLL.HSPR.Bll_Tb_HSPR_Parking B_Parking = new MobileSoft.BLL.HSPR.Bll_Tb_HSPR_Parking();
                    DataTable dTable = B_Parking.GetList("CommID = '" + Row["CommID"].ToString() + "'  AND ISNULL(CarSign,'') LIKE '%" + DataSecurity.FilteSQLStr(Row["Key"].ToString()) + "%'", "view_HSPR_ParkingSel_Filter").Tables[0];
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Value", typeof(string));
                    foreach (DataRow row in dTable.Rows)
                    {
                        string value = row["CarSign"].ToString();
                        DataRow dr = dt.NewRow();
                        dr["Value"] = value;
                        dt.Rows.Add(dr);
                    }
                    result = JSONHelper.FromString(dt);
                }
                else {
                    result = JSONHelper.FromString(false, "缺少参数");
                }
            }
            catch (Exception e)
            {
                result = JSONHelper.FromString(false, e.Message);
            }
            return result;
        }
        #endregion 获取车位列表
        #region 获取车位列表
        private string GetParkNameList(DataRow Row)
        {
            string result = JSONHelper.FromString(false, "未知错误!");
            try
            {
                if (Row.Table.Columns.Contains("CommID") && Row["CommID"].ToString() != "" && Row.Table.Columns.Contains("Key") && Row["Key"] != null)
                {
                    MobileSoft.BLL.HSPR.Bll_Tb_HSPR_Parking B_Parking = new MobileSoft.BLL.HSPR.Bll_Tb_HSPR_Parking();
                    DataTable dTable = B_Parking.GetList("CommID = '" + Row["CommID"].ToString() + "'  AND ISNULL(ParkName,'') LIKE '%" + DataSecurity.FilteSQLStr(Row["Key"].ToString()) + "%'", "view_HSPR_ParkingSel_Filter").Tables[0];
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Value", typeof(string));
                    foreach (DataRow row in dTable.Rows)
                    {
                        string value = row["ParkName"].ToString();
                        DataRow dr = dt.NewRow();
                        dr["Value"] = value;
                        dt.Rows.Add(dr);
                    }
                    result = JSONHelper.FromString(dt);
                }
                else {
                    result = JSONHelper.FromString(false, "缺少参数");
                }
            }
            catch (Exception e)
            {
                result = JSONHelper.FromString(false, e.Message);
            }
            return result;
        }
        #endregion 获取车位列表
        #region 设备查询
        /// <summary>
        /// 设备查询 GetEquipmentSearch
        /// </summary>
        /// <param name="Row"></param>
        /// EquiId              ID
        /// <returns></returns>
        private string GetEquipmentSearch(DataRow Row)
        {
            string result = JSONHelper.FromString(false, "未知错误!");           
            try
            {
                //Row["EquiId"] = "07bfb30f-88cc-474d-839e-cfae05e8fe92";
                //EquiId 设备ID
                if (Row.Table.Columns.Contains("EquiId") && Row["EquiId"].ToString() != "")
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Equipment", Type.GetType("System.String"));
                    dt.Columns.Add("EventList", Type.GetType("System.String"));
                    dt.Columns.Add("WbEquipmentList", Type.GetType("System.String"));
                    dt.Columns.Add("WxEquipmentList", Type.GetType("System.String"));
                    DataRow dr = dt.NewRow();
                    dr["Equipment"] = "";
                    dr["EventList"] = "";
                    dr["WbEquipmentList"] = "";
                    dr["WxEquipmentList"] = "";
                    dt.Rows.Add(dr);
                    //档案信息
                    string strSqlEquipment = " SELECT * FROM VIEW_QM_SelEquipmentInformation WHERE Id='" + Row["EquiId"].ToString().Trim() + "'";
                    DataTable dtTask = Query(strSqlEquipment).Tables[0];
                    dt.Rows[0]["Equipment"] = JSONHelper.FromString(dtTask, true, false);

                    //大事记
                    string strSqlEvent = " SELECT * FROM VIEW_EQ_GetEventChronicle WHERE EquiId='" + Row["EquiId"].ToString().Trim() + "'";
                    DataTable dtEvent = Query(strSqlEvent).Tables[0];
                    dt.Rows[0]["EventList"] = JSONHelper.FromString(dtEvent, true, false);

                    //维保记录
                    string strSqlTaskWb = " SELECT * FROM View_EQ_WbEquipmentList WHERE EquiId='" + Row["EquiId"].ToString().Trim() + "' AND ISNULL(PollingResult,'') != '' ";
                    DataTable dtTaskWb = Query(strSqlTaskWb).Tables[0];
                    dt.Rows[0]["WbEquipmentList"] = JSONHelper.FromString(dtTaskWb, true, false);

                    //维修记录暂时没有功能
                    dt.Rows[0]["WxEquipmentList"] = JSONHelper.FromString(new DataTable(), true, false);

                    result = JSONHelper.FromString(dt);
                }
                else {
                    result = JSONHelper.FromString(false, "缺少参数/EquiId");
                }
            }
            catch (Exception EX)
            {
                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }


        /// <summary>
        /// 设备名称 模糊查询GetEquipmentNameSearch
        /// </summary>
        /// <param name="Row"></param>
        /// CommID              小区编号
        /// EquiName         设备名称
        /// <returns></returns>
        private string GetEquipmentNameSearch(DataRow Row)
        {
            string CommID = string.Empty;
            if (Row.Table.Columns.Contains("CommID"))
            {
                CommID = Row["CommID"].ToString();
            }
            string EquiName = string.Empty;
            if (Row.Table.Columns.Contains("EquiName"))
            {
                EquiName = Row["EquiName"].ToString();
            }
            //档案信息
            string strSqlEquipment = " SELECT id,EquipmentName as Value FROM dbo.Tb_EQ_Equipment WHERE ISNULL(IsDelete,'0') ='0' AND  EquipmentName LIKE '%" + AppGlobal.StrToStr(Row["EquiName"].ToString().Trim()) + "%' and CommId='" + Row["CommID"].ToString().Trim() + "'";
            DataTable dtTask = Query(strSqlEquipment).Tables[0];

            return JSONHelper.FromString(dtTask);
        }

        /// <summary>
        /// 设备编号 模糊查询GetEquipmentNumSearch
        /// </summary>
        /// <param name="Row"></param>
        /// CommID              小区编号
        /// EquipmentNO         设备编号
        /// <returns></returns>
        private string GetEquipmentNumSearch(DataRow Row)
        {
            string result = JSONHelper.FromString(false, "未知错误!");
            try
            {
                //Row["EquiId"] = "07bfb30f-88cc-474d-839e-cfae05e8fe92";
                //EquiId 设备ID

                if (Row.Table.Columns.Contains("CommID") && Row["CommID"].ToString() != "" && Row.Table.Columns.Contains("EquipmentNO"))
                {
                    //档案信息
                    string strSqlEquipment = " SELECT id,EquipmentNO as Value FROM dbo.Tb_EQ_Equipment WHERE ISNULL(IsDelete,'0') ='0'  and  CommId='" + Row["CommID"].ToString().Trim() + "'";
                    if (Row["EquipmentNO"].ToString() != "")
                    {
                        strSqlEquipment = " SELECT id,EquipmentNO as Value FROM dbo.Tb_EQ_Equipment WHERE ISNULL(IsDelete,'0') ='0' AND  EquipmentNO LIKE '%" + AppGlobal.StrToStr(Row["EquipmentNO"].ToString().Trim()) + "%' and CommId='" + Row["CommID"].ToString().Trim() + "'";
                    }

                    DataTable dtTask = Query(strSqlEquipment).Tables[0];

                    result = JSONHelper.FromString(dtTask);
                }
                else
                {
                    result = JSONHelper.FromString(false, "缺少参数/EquipmentNO");
                }
            }
            catch (Exception EX)
            {
                result = JSONHelper.FromString(false, EX.ToString());
            }
            return result;
        }


        #endregion 设备查询
        #region 得到车位资料
        private string GetParkSearch(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommID").toJson();
            }
            var commId = AppGlobal.StrToInt(row["CommID"].AsString());

            var parkName = default(string);
            var carSign = default(string);
            var roomSign = default(string);

            if (row.Table.Columns.Contains("ParkName") && !string.IsNullOrEmpty(row["ParkName"].ToString()))
            {
                parkName = row["ParkName"].ToString();
            }
            if (row.Table.Columns.Contains("CarSign") && !string.IsNullOrEmpty(row["CarSign"].ToString()))
            {
                carSign = row["CarSign"].ToString();
            }
            if (row.Table.Columns.Contains("RoomSign") && !string.IsNullOrEmpty(row["RoomSign"].ToString()))
            {
                roomSign = row["RoomSign"].ToString();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT CarparkName,ParkType,ParkName,isnull(RoomSign,'') AS RoomSign,PropertyRight,CustName,Phone,
                            isnull(ParkingCarSign,'') AS ParkingCarSign,CarSign,isnull(CarType,'') AS CarType,
                            isnull(FacBrands,'') AS FacBrands,isnull(CarColor,'') AS CarColor,ParkEndDate,
                            (SELECT max(FeesEndDate) FROM Tb_HSPR_Fees x WHERE x.ParkID=a.ParkID AND (x.IsCharge=1 OR x.IsPrec=1)) AS FeesEndDate
                            FROM view_HSPR_ParkingSel_Filter a
                            WHERE isnull(IsDelete,0)=0 ";

                if (!string.IsNullOrEmpty(parkName))
                    sql += $" AND ParkName=@ParkName";
                if (!string.IsNullOrEmpty(carSign))
                    sql += $" AND CarSign=@CarSign";
                if (!string.IsNullOrEmpty(roomSign))
                    sql += $" AND RoomSign=@RoomSign";

                var parkInfo = conn.Query(sql, new { ParkName = parkName, CarSign = carSign, RoomSign = roomSign }).FirstOrDefault();

                return new ApiResult(true, parkInfo).toJson();
            }
        }

        #endregion

        #region 物资查询
        /// <summary>
        /// GetMaterialSearch
        /// </summary>
        /// <param name="Row"></param>
        /// CommID              小区编码
        /// WareHouseName       仓库名称 
        /// MaterialName        物资名称 
        /// Num                 物资编码
        /// rows
        /// page
        /// <returns></returns>
        public string GetMaterialSearch(DataRow Row)
        {
            try
            {
                int rows = 5, page = 1;
                if (!Row.Table.Columns.Contains("CommID"))
                {
                    return new ApiResult(false, "缺少参数CommID").toJson();
                }
                if (!Row.Table.Columns.Contains("WareHouseName"))
                {
                    return new ApiResult(false, "缺少参数WareHouseName").toJson();
                }
                //根据申购仓库的权限进行查询出来
                string strSQL = "";
                //
                ///   SELECT RoleCode FROM Tb_Sys_UserRole
                int PageCount = 0, Counts = 0;
                if (Row.Table.Columns.Contains("MaterialName") && Row["MaterialName"] != null && Row["MaterialName"].ToString().Trim() != "")
                {
                    strSQL = strSQL + " AND MaterialName  LIKE '%" + AppGlobal.ChkStr(Row["MaterialName"].ToString()) + "%'";
                }
                if (Row.Table.Columns.Contains("rows") && Row["rows"] != null && Row["rows"].ToString().Trim() != "")
                {
                    rows = Convert.ToInt32(Row["rows"]);
                }
                if (Row.Table.Columns.Contains("page") && Row["page"] != null && Row["page"].ToString().Trim() != "")
                {
                    page = Convert.ToInt32(Row["page"]);
                }
                if (Row.Table.Columns.Contains("Num") && Row["Num"] != null && Row["Num"].ToString().Trim() != "")
                {
                    strSQL = strSQL + " AND Num  LIKE '%" + AppGlobal.ChkStr(Row["Num"].ToString()) + "%'";
                }

                if (Row.Table.Columns.Contains("WareHouseName") && Row["WareHouseName"] != null && Row["WareHouseName"].ToString().Trim() != "")
                {
                    strSQL = strSQL + " AND WareHouseName LIKE '%" + AppGlobal.ChkStr(Row["WareHouseName"].ToString()) + "%'";
                }
                DataTable dt = wzGetList(out PageCount, out Counts, strSQL.ToString(), page, rows, "View_Tb_Mt_Store_Filter", "Id", 1).Tables[0];
                List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();
                if (null != dt)
                {
                    Dictionary<string, object> dictionary;
                    foreach (DataRow item in dt.Rows)
                    {
                        dictionary = new Dictionary<string, object>();
                        if (item.Table.Columns.Contains("WareHouseName"))
                        {
                            dictionary.Add("WareHouseName", item["WareHouseName"]);
                        }
                        else
                        {
                            dictionary.Add("WareHouseName", "字段不存在");
                        }
                        if (item.Table.Columns.Contains("MaterialTypeName"))
                        {
                            dictionary.Add("MaterialTypeName", item["MaterialTypeName"]);
                        }
                        else
                        {
                            dictionary.Add("MaterialTypeName", "字段不存在");
                        }
                        if (item.Table.Columns.Contains("Num"))
                        {
                            dictionary.Add("Num", item["Num"]);
                        }
                        else
                        {
                            dictionary.Add("Num", "字段不存在");
                        }
                        if (item.Table.Columns.Contains("Specification"))
                        {
                            dictionary.Add("Specification", item["MaterialName"]);
                        }
                        else
                        {
                            dictionary.Add("Specification", "字段不存在");
                        }
                        if (item.Table.Columns.Contains("Spell"))
                        {
                            dictionary.Add("Spell", item["Spell"]);
                        }
                        else
                        {
                            dictionary.Add("Spell", "字段不存在");
                        }
                        if (item.Table.Columns.Contains("UnitName"))
                        {
                            dictionary.Add("UnitName", item["UnitName"]);
                        }
                        else
                        {
                            dictionary.Add("UnitName", "字段不存在");
                        }
                        if (item.Table.Columns.Contains("ColorName"))
                        {
                            dictionary.Add("ColorName", item["ColorName"]);
                        }
                        else
                        {
                            dictionary.Add("ColorName", "字段不存在");
                        }
                        if (item.Table.Columns.Contains("Brand"))
                        {
                            dictionary.Add("Brand", item["Brand"]);
                        }
                        else
                        {
                            dictionary.Add("Brand", "字段不存在");
                        }
                        if (item.Table.Columns.Contains("Quantity"))
                        {
                            dictionary.Add("Quantity", item["Quantity"]);
                        }
                        else
                        {
                            dictionary.Add("Quantity", "字段不存在");
                        }

                        #region 华南城1975增加销售单价
                        if (Global_Var.CorpId == "1975")
                        {
                            string WareHouseId = "";
                            if (item.Table.Columns.Contains("WareHouseId"))
                            {
                                WareHouseId = item["WareHouseId"].ToString();
                            }
                            string MaterialId = "";
                            if (item.Table.Columns.Contains("MaterialId"))
                            {
                                MaterialId = item["MaterialId"].ToString();
                            }
                            decimal Price = 0.00M;
                            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                            {
                                Price = conn.QueryFirstOrDefault<decimal>("SELECT SellPrice FROM Tb_Mt_WareHouseMaterial WHERE WareHouseId = @WareHouseId AND MaterialId = @MaterialId", new { WareHouseId, MaterialId });
                            }
                            dictionary.Add("Price", Price);
                        }
                        #endregion
                        resultList.Add(dictionary);
                    }
                }
                return new ApiResult(true, resultList).toJson();
            }
            catch (Exception e)
            {
                return new ApiResult(false, e.Message).toJson();
            }
        }
        public DataSet wzGetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string ViewName, string SortField, int Sort)
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
            parameters[5].Value = "SELECT * FROM " + ViewName + " WHERE 1=1 " + StrCondition;
            parameters[6].Value = "Id";
            DataSet Ds = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }
        public static DataSet GetList(string Fileld, out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, string strView, int Sort)
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
            parameters[0].Value = Fileld;
            parameters[1].Value = PageSize;
            parameters[2].Value = PageIndex;
            parameters[3].Value = SortField;
            parameters[4].Value = Sort;
            parameters[5].Value = "SELECT * FROM " + strView + " WHERE 1=1 " + StrCondition;
            parameters[6].Value = "id";
            DataSet Ds = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }
        #endregion 物资查询

        #region 得到查询结果
        private DataSet Query(string SQLString)
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
        #endregion 得到查询结果
        #region 获取报事信息
        private string GetReportCommList(DataRow row)
        {
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                IEnumerable<dynamic> resultSet = conn.Query("Proc_Sys_Organ_GetEntryNodes", 
                    new { UserCode = Global_Var.LoginUserCode }, null, false, null, CommandType.StoredProcedure);

                List<Dictionary<string, string>> resultList = new List<Dictionary<string, string>>();
                if (null != resultSet) {
                    foreach (var item in resultSet)
                    {
                        if ("1".Equals(Convert.ToString(item.IsComp)))
                        {
                            continue;
                        }
                        if ("0".Equals(Convert.ToString(item.InPopedom)))
                        {
                            continue;
                        }
                        resultList.Add(new Dictionary<string, string>
                        {
                            { "CommName",Convert.ToString(item.OrganName)},
                            { "CommID",Convert.ToString(item.InPopedom)}
                        });
                    }
                }
                return new ApiResult(true, resultList).toJson();
            }
        }
        private string GetReportSearch(DataRow Row)
        {
            string result = JSONHelper.FromString(false, "未知错误!");
            try
            {
                if (Row.Table.Columns.Contains("CommID") && Row["CommID"].ToString() != "" && 
                    Row.Table.Columns.Contains("BeginTime") && Row["BeginTime"].ToString() != "" && 
                    Row.Table.Columns.Contains("EndTime") && Row["EndTime"].ToString() != "" && 
                    Row.Table.Columns.Contains("Page") && Row["Page"].ToString() != "")
                {
                    string strSQL = "";

                    string CommID = Row["CommID"].ToString();

                    // 如果CommID包含逗号，代表是多个项目
                    if (CommID.Contains(","))
                    {
                        string CommIDs = "";
                        foreach (var item in CommID.Split(','))
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                CommIDs += DataSecurity.StrToInt(item) + ",";
                            }
                        }
                        if (!string.IsNullOrEmpty(CommIDs))
                        {
                            strSQL = " and CommID IN (" + CommIDs.Trim(',') + ") ";
                        }
                    }
                    else
                    {
                        if (DataSecurity.StrToInt(CommID) > 0)
                        {
                            strSQL = " and CommID = " + DataSecurity.StrToInt(Row["CommID"].ToString()) + " ";
                        }
                    }
                    strSQL = strSQL + " and IncidentDate >= '" + DataSecurity.StrToDateTime(Row["BeginTime"].ToString()) + "'";
                    strSQL = strSQL + " and IncidentDate <= '" + DataSecurity.StrToDateTime(Row["EndTime"].ToString()) + "'";

                    if (Row.Table.Columns.Contains("TypeID") && Row["TypeID"].ToString() != "")
                    {
                        //报事类别
                        strSQL = strSQL + " and TypeID LIKE  '%" + DataSecurity.FilteSQLStr(Row["TypeID"].ToString()) + "%'";
                    }
                    if (Row.Table.Columns.Contains("ProcesPerson") && Row["ProcesPerson"].ToString() != "")
                    {
                        //处理人
                        strSQL = strSQL + " and DealMan LIKE  '%" + DataSecurity.FilteSQLStr(Row["ProcesPerson"].ToString()) + "%'";
                    }
                    if (Row.Table.Columns.Contains("RoomSign") && Row["RoomSign"].ToString() != "")
                    {
                        if (Global_Var.LoginCorpID=="1971")
                        {
                            //房号
                            strSQL = strSQL + " and NC_NewRoomSign like '%" + DataSecurity.FilteSQLStr(Row["RoomSign"].ToString()) + "%' ";
                        }
                        else
                        {
                            //房号
                            strSQL = strSQL + " and RoomSign like '%" + DataSecurity.FilteSQLStr(Row["RoomSign"].ToString()) + "%' ";
                        }
                    }
                    if (Row.Table.Columns.Contains("ProcesState") && Row["ProcesState"].ToString() != "")
                    {
                        //处理状态
                        strSQL = strSQL + " and State = '" + DataSecurity.FilteSQLStr(Row["ProcesState"].ToString()) + "' ";
                    }
                    if (Row.Table.Columns.Contains("IncidentPlace") && Row["IncidentPlace"].ToString() != "")
                    {
                        //报事区域
                        strSQL = strSQL + " and IncidentPlace = '" + DataSecurity.FilteSQLStr(Row["IncidentPlace"].ToString()) + "' ";
                    }

                    ////单号
                    //strSQL = strSQL + "and DispType = " + DataSecurity.StrToInt(Row["DispType"].ToString()) + " and CoordinateNum like '%" + DataSecurity.FilteSQLStr(Row["OrderNum"].ToString()) + "%'";

                    //是否派工
                    if (Row.Table.Columns.Contains("DispatchState") && Row["DispatchState"].ToString() != "")
                    {
                        if (Row["DispatchState"].ToString() == "0")//未分派
                        {
                            strSQL = strSQL + " and ISNULL(DispType,0)=0 ";
                        }
                        else//已分派
                        {
                            // strSQL = strSQL + " and DispType is  null ";

                            if (Row.Table.Columns.Contains("DispType") && Row["DispType"].ToString() != "")//派工单or 协调单
                            {
                                strSQL = strSQL + " and DispType="+Row["DispType"].ToString();
                            }
                            if (Row.Table.Columns.Contains("OrderNum")&& Row["OrderNum"].ToString()!="")//单号
                            {
                                strSQL = strSQL + " and  CoordinateNum like '%" + DataSecurity.FilteSQLStr(Row["OrderNum"].ToString()) + "%'";
                            }
                        }
                    }

                    //if (Row.Table.Columns.Contains("IsTouSu") && Row["IsTouSu"].ToString()!="")//投诉
                    //{
                    //    //strSQL= strSQL+" and dbo.funCheckIncidentClassID(TypeID,CommID)>0 ";
                    //    strSQL += " and IsTousu="+ Row["IsTouSu"].ToString();
                    //}
                    strSQL = strSQL + "and ISNULL(IsDelete,0)=0";


                    int pageCount, counts;
                    int CurrPage = DataSecurity.StrToInt(Row["Page"].ToString());
                    DataTable dTable = IncidentSearch("*", out pageCount, out counts, strSQL, CurrPage, 5, "IncidentDate", 1).Tables[0];
                    result = JSONHelper.FromString(dTable);
                }
                else {
                    result = JSONHelper.FromString(false, "缺少参数!");
                }
            }
            catch (Exception e)
            {
                result = JSONHelper.FromString(false, e.Message);
            }
            return result;
        }
        #endregion 获取报事信息
        #region 获取通讯录
        private string GetTelSearch(DataRow Row)
        {
            string result = JSONHelper.FromString(false, "未知错误!");
            try
            {
                if (Row.Table.Columns.Contains("Key") && Row.Table.Columns.Contains("Page") && Row["Page"].ToString() != "")
                {
                    string key = Row["Key"].ToString();
                    string page = Row["Page"].ToString();
                    int pageCount, counts;
                    string strSQL = " AND ISNULL(OprName,'') LIKE '%" + DataSecurity.FilteSQLStr(key) + "%' OR ISNULL(Duty,'') LIKE '%" + DataSecurity.FilteSQLStr(key) + "%' OR ISNULL(Phone,'') LIKE '%" + DataSecurity.FilteSQLStr(key) + "%' OR ISNULL(Mark,'') LIKE '%" + DataSecurity.FilteSQLStr(key) + "%'  OR ISNULL(TelNum,'') LIKE '%" + DataSecurity.FilteSQLStr(key) + "%'  OR ISNULL(Mail,'') LIKE '%" + DataSecurity.FilteSQLStr(key) + "%'  OR ISNULL(CommName,'') LIKE '%" + DataSecurity.FilteSQLStr(key) + "%'";
                    int CurrPage = DataSecurity.StrToInt(page);
                    DataTable dTable = GetTelList("*", out pageCount, out counts, strSQL, CurrPage, 5, "CompanyNameSort ASC,CommNameSort ASC,Sort ASC,DutySort", 0).Tables[0];

                    DataTable dt = new DataTable();
                    dt.Columns.Add("OprName", typeof(string));
                    dt.Columns.Add("CommName", typeof(string));
                    dt.Columns.Add("Mark", typeof(string));
                    dt.Columns.Add("TelNum", typeof(string));
                    dt.Columns.Add("Mail", typeof(string));
                    foreach (DataRow row in dTable.Rows)
                    {
                        DataRow dr = dt.NewRow();

                        if (row["CompanyName"] != null)
                        {
                            dr["OprName"] = string.Format("【{0}】{1}", row["CompanyName"].ToString(), row["OprName"].ToString());
                        }
                        else
                        {
                            dr["OprName"] = row["OprName"].ToString();
                        }
                        
                        dr["CommName"] = row["CommName"].ToString();
                        dr["Mark"] = row["Mark"].ToString();
                        dr["TelNum"] = row["TelNum"].ToString();
                        dr["Mail"] = row["Mail"].ToString();
                        dt.Rows.Add(dr);
                    }
                    result = JSONHelper.FromString(dt);
                }
                else {
                    result = JSONHelper.FromString(false, "缺少参数!");
                }

            }
            catch (Exception e)
            {
                result = JSONHelper.FromString(false, e.Message);
            }
            return result;
        }
        #endregion 获取通讯录
        #region 获取房号信息
        private string GetRoomSign(DataRow Row)
        {
            string result = JSONHelper.FromString(false, "未知错误!");
            try
            {
                if (Row.Table.Columns.Contains("CommID") && Row.Table.Columns.Contains("RoomSign")
                   && Row["CommID"].ToString() != "" && Row["RoomSign"].ToString() != "")
                {
                    MobileSoft.BLL.HSPR.Bll_Tb_HSPR_Room Bll = new MobileSoft.BLL.HSPR.Bll_Tb_HSPR_Room();
                    string strSQL = "  ISNULL(IsDelete,0)=0 And CommID = " + Row["CommID"].ToString() + " AND RoomSign LIKE '%" + Row["RoomSign"].ToString() + "%'";
                    DataTable dTable = Bll.GetList(10, strSQL, "RoomID").Tables[0];
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Value", typeof(string));
                    foreach (DataRow row in dTable.Rows)
                    {
                        var A = row["BuildArea"].ToString();
                        if (A == null) A = "0.00";
                        var roomSign = row["RoomSign"] + "(" + A + ")(" + row["CustName"] + ")";
                        DataRow dr = dt.NewRow();
                        dr["Value"] = roomSign;
                        dt.Rows.Add(dr);
                    }
                    result = JSONHelper.FromString(dt);
                }
                else {
                    result = JSONHelper.FromString(false, "缺少参数!");
                }

            }
            catch (Exception e)
            {
                result = JSONHelper.FromString(false, e.Message);
            }
            return result;
        }
        #endregion 获取房号信息
        #region 得到客户资料
        private string GetCustomerDetail(DataRow Row)
        {
            string result = JSONHelper.FromString(false, "未知错误!");
            try
            {
                if (Row.Table.Columns.Contains("CommID") && Row.Table.Columns.Contains("RoomSign")
                   && Row["CommID"].ToString() != "" && Row["RoomSign"].ToString() != "")
                {
                    DataSet Ds = new DataSet();
                    long iRoomID = 0;
                    long iCustID = 0;
                    string strRoomSign = "";
                    int iCommID = Convert.ToInt32(Row["CommId"].ToString());
                    string[] ArrRoomSign = Row["RoomSign"].ToString().Split('(');
                    if (ArrRoomSign.Length > 0)
                    {
                        strRoomSign = ArrRoomSign[0].ToString();
                    }
                    MobileSoft.BLL.HSPR.Bll_Tb_HSPR_Room Bll = new MobileSoft.BLL.HSPR.Bll_Tb_HSPR_Room();

                    string strSQL = "  ISNULL(IsDelete,0)=0 And CommID = " + iCommID + " AND RoomSign='" + strRoomSign.ToString() + "'";

                    DataTable dTable = Bll.GetList(strSQL).Tables[0];

                    DataTable MeterLlistTable = new DataTable();
                    MeterLlistTable.TableName = "TableA";
                    DataTable CutTable = new DataTable();

                    DataTable TableA = new DataTable();
                    if (dTable.Rows.Count > 0)
                    {
                        iRoomID = DataSecurity.StrToLong(dTable.Rows[0]["RoomID"].ToString());
                        MobileSoft.BLL.HSPR.Bll_Tb_HSPR_CustomerLive B_CustomerLive = new MobileSoft.BLL.HSPR.Bll_Tb_HSPR_CustomerLive();
                        DataTable CustLive = B_CustomerLive.GetList("RoomID='" + iRoomID.ToString() + "'").Tables[0];
                        iCustID = DataSecurity.StrToLong(CustLive.Rows[0]["CustID"].ToString());
                        MobileSoft.BLL.HSPR.Bll_Tb_HSPR_Customer B_Customer = new MobileSoft.BLL.HSPR.Bll_Tb_HSPR_Customer();
                        CutTable = B_Customer.GetList("CustID='" + iCustID.ToString() + "'").Tables[0];

                        if (CutTable.Rows.Count > 0)
                        {
                            TableA = CutTable.Copy();

                        }
                    }
                    TableA.TableName = "TableA";
                    Ds.Tables.Add(TableA);
                    DataTable DebtTable = HSPR_Customer_FilterDebt(iCommID, iCustID, iRoomID);//DebtsAmount PrecAmount

                    DataTable TableB = new DataTable();
                    TableB = DebtTable.Copy();
                    TableB.TableName = "TableB";

                    Ds.Tables.Add(TableB);


                    DataTable TableC = new DataTable();

                    strSQL = " and CommID = '" + iCommID + "' and CustID = '" + iCustID.ToString() + "' ";

                    strSQL = strSQL + " and DebtsAmount > 0 and isnull(IsCharge,0) = 0 and isnull(IsBank,0) = 0 ";

                    strSQL = strSQL + " and Isnull(IsPrec,0) = 0 and isnull(IsFreeze,0) = 0 ";

                    DataTable dTableFees = GetFeesNoPayList(strSQL, 1, 9999, "FeesID", 1);

                    TableC = dTableFees.Copy();
                    TableC.TableName = "TableC";

                    Ds.Tables.Add(TableC);

                    result = JSONHelper.FromString(Ds, true, true);
                }
                else {
                    result = JSONHelper.FromString(false, "缺少参数!");
                }

            }
            catch (Exception e)
            {
                result = JSONHelper.FromString(false, "错误:" + e.Message);
            }
            return result;
        }

        #endregion 得到客户资料
        #region 查询业主欠费明细
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        private DataTable GetFeesNoPayList(string StrCondition, int PageIndex, int PageSize, string SortField, int Sort)
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
            parameters[5].Value = "SELECT * FROM view_HSPR_Fees_SearchFilter WHERE 1=1 " + StrCondition;
            parameters[6].Value = "FeesId";
            DataTable Ds = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet").Tables[0];
            return Ds;
        }
        #endregion
        #region 查询业主欠费

        private DataTable HSPR_Customer_FilterDebt(int CommID, long CustID, long RoomID)
        {

            SqlParameter[] parameters = {
                    new SqlParameter("@CommID", SqlDbType.BigInt),
                    new SqlParameter("@CustID", SqlDbType.BigInt),
                    new SqlParameter("@RoomID", SqlDbType.BigInt)
            };
            parameters[0].Value = CommID;
            parameters[1].Value = CustID;
            parameters[2].Value = RoomID;
            DataTable dTable = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_HSPR_Customer_FilterDebt", parameters, "RetDataSet").Tables[0];

            return dTable;

        }
        #endregion
        #region 得到通讯录
        public static DataSet GetTelList(string Fileld, out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort)
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
            parameters[0].Value = Fileld;
            parameters[1].Value = PageSize;
            parameters[2].Value = PageIndex;
            parameters[3].Value = SortField;
            parameters[4].Value = Sort;
            parameters[5].Value = "SELECT * FROM view_Common_TelList_Filter WHERE 1=1 " + StrCondition;
            parameters[6].Value = "iid";
            DataSet Ds = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }
        #endregion
        #region 得到报事信息
        public static DataSet IncidentSearch(string Fileld, out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort)
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
            parameters[0].Value = Fileld;
            parameters[1].Value = PageSize;
            parameters[2].Value = PageIndex;
            parameters[3].Value = SortField;
            parameters[4].Value = Sort;
            parameters[5].Value = "SELECT * FROM view_HSPR_IncidentSeach_Filter WHERE 1=1 " + StrCondition;
            parameters[6].Value = "IncidentID";
            DataSet Ds = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }
        #endregion
    }
}