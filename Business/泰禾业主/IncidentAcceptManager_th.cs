using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.HSPR;
using MobileSoft.Model.Unified;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Business
{
    public class IncidentAcceptManager_th : PubInfo
    {
        public IncidentAcceptManager_th() //获取小区、项目信息
        {
            base.Token = "20171024IncidentAcceptManager";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "GetIncidentHistoryList":      // 获取报事历史
                    Trans.Result = GetIncidentHistoryList(Row);
                    break;
                case "GetComplaintList":      // 获取报事历史
                    Trans.Result = GetComplaintList(Row);
                    break;
                case "GetIncidentDetail":           // 获取报事详情
                    Trans.Result = GetIncidentDetail(Row);
                    break;
                case "IncidentAccept":              // 报事提交
                    Trans.Result = IncidentAccept(Row);
                    break;
                case "IncidentAccept_20181211":              // 报事提交
                    Trans.Result = IncidentAccept_20181211(Row);
                    break;
                case "IncidentCancel":              // 取消报事
                    Trans.Result = IncidentCancel(Row);
                    break;
                case "GetIncidentType":      // 获取报事类别(俊发)
                    Trans.Result = GetIncidentType(Row);
                    break;
                case "IncidentAccept_jf":              // 报事提交(俊发)
                    Trans.Result = IncidentAccept_jf(Row);
                    break;
                case "GetIncidentRegional":              // 获取公区对象(俊发)
                    Trans.Result = GetIncidentRegional(Row);
                    break;
                case "GetIncidentHistoryList_jf":      // 获取报事历史(俊发)
                    Trans.Result = GetIncidentHistoryList_jf(Row);
                    break;
                case "GetIncidentDetail_jf":           // 获取报事详情(俊发)
                    Trans.Result = GetIncidentDetail_jf(Row);
                    break;
                case "GetIncidentBigType_Property":
                    Trans.Result = GetIncidentBigType_Property(Row);
                    break;
                case "IncidentAccept_Property":
                    Trans.Result = IncidentAccept_Property(Row);
                    break;
                case "IncidentAccept_Property_20181211":
                    Trans.Result = IncidentAccept_Property_20181211(Row);
                    break;
                case "GetIncidentRegional_Property":
                    Trans.Result = GetIncidentRegional_Property(Row);
                    break;
                case "GetIncidentState":
                    Trans.Result = GetIncidentState(Row);
                    break;
                case "EditIncidentState":
                    Trans.Result = EditIncidentState(Row);
                    break;
                default:
                    break;
            }
        }

        private string EditIncidentState(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string CommID = row["CommID"].ToString();

            string IncidentNum = string.Empty;
            if (row.Table.Columns.Contains("IncidentNum"))
            {
                IncidentNum = row["IncidentNum"].ToString();
            }
            //构建链接字符串
            string ContionString = PubConstant.GetConnectionString("SQLConnection_TH").ToString();
            using (IDbConnection conn = new SqlConnection(ContionString))
            {

                string UpdateSql = "update Tb_HSPR_IncidentAccept set MainEndDate=GETDATE(),DealSituation='',DealState=1  where 1=1 and  DispType=2 and CommID=" + CommID + " and IncidentNum='" + IncidentNum + "'";

                string QuerySql = "select * from Tb_HSPR_IncidentAccept where DispType=2 and CommID=" + CommID + " and IncidentNum='" + IncidentNum + "'";

                dynamic Incident = conn.QueryFirstOrDefault(QuerySql);
                if (null == Incident)
                {
                    return new ApiResult(false, "未查询到对应的报事").toJson();
                }
                conn.Execute(UpdateSql);

                return JSONHelper.FromString(true, "推送成功");
            }
        }

        private string GetIncidentRegional_Property(DataRow row)
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                return new ApiResult(true, conn.Query(@"SELECT CommID,RegionalID,RegionalPlace FROM Tb_HSPR_IncidentRegional;")).toJson();
            }
        }

        private string GetIncidentType(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "小区编号不能为空").toJson();
            }

            if (!row.Table.Columns.Contains("Duty") || string.IsNullOrEmpty(row["Duty"].ToString()))
            {
                return new ApiResult(false, "请选择报事分类").toJson();
            }

            string CommunityId = row["CommunityId"].AsString();
            string Duty = row["Duty"].AsString();
            if (!"物业类".Equals(Duty) && !"地产类".Equals(Duty))
            {
                return new ApiResult(false, "报事分类选择错误").toJson();
            }
            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommunityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = new CostInfo().GetConnectionStringStr(Community);
            //报事类型(书面报事=1,口头报事=2),默认为书面报事
            int ClassID = 1;
            DataTable dt;
            using (IDbConnection conn = new SqlConnection(strcon))
            {
                dt = conn.ExecuteReader("Proc_HSPR_IncidentType_GetAllNodes_NewComm_Big", new { CommID = Community.CommID, ClassID = ClassID, CostName = "", IsEnabled = "1", IncidentPlace = "", Duty = Duty }, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
            }
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            Dictionary<string, object> dic;
            if (null != dt && dt.Rows.Count > 0)
            {
                if (Community.CorpID == 2045 || Community.CorpID == 2046)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        dic = new Dictionary<string, object>
                    {
                        { "CorpTypeID", item["CorpTypeID"] }
                        ,{ "TypeCode", item["TypeCode"] }
                        ,{ "IsAPPUse", item["IsAPPUse"] }
                        ,{ "TypeID", item["TypeID"] }
                        ,{ "TypeName", item["TypeName"] }
                        ,{ "RatedWorkHour", item["RatedWorkHour"] }
                        ,{ "KPIRatio", item["KPIRatio"] }
                        ,{ "IsTousu", item["IsTousu"] }
                        ,{ "DealLimit", item["DealLimit"] }
                        ,{ "DealLimit2", item["DealLimit2"] }
                    };
                        if (AppGlobal.StrToInt(item["IsAPPUse"].ToString()) == 1)
                        {
                            list.Add(dic);
                        }

                    }

                }
                else
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        dic = new Dictionary<string, object>
                        {
                            { "CorpTypeID", item["CorpTypeID"] }
                            ,{ "TypeCode", item["TypeCode"] }
                            ,{ "TypeID", item["TypeID"] }
                            ,{ "TypeName", item["TypeName"] }
                            ,{ "RatedWorkHour", item["RatedWorkHour"] }
                            ,{ "KPIRatio", item["KPIRatio"] }
                            ,{ "IsTousu", item["IsTousu"] }
                            ,{ "DealLimit", item["DealLimit"] }
                            ,{ "DealLimit2", item["DealLimit2"] }
                        };
                        list.Add(dic);
                    }

                    // 丽创需求 6431
                    if (Community.CorpID == 2087)
                    {
                        var removes = list.FindAll(obj => obj["TypeCode"].ToString().StartsWith("0002"));
                        foreach (var tmp in removes)
                        {
                            list.Remove(tmp);
                        }
                    }
                }

            }
            return new ApiResult(true, list).toJson();
        }

        /// <summary>
        /// 报事历史列表
        /// </summary>
        private string GetIncidentHistoryList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "小区编号不能为空").toJson();
            }

            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return new ApiResult(false, "用户ID不能为空").toJson();
            }

            string communityId = row["CommunityId"].AsString();
            string userID = row["UserID"].AsString();

            int page = 1;
            int size = 10;

            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                page = 1;
            }
            else
            {
                if (!int.TryParse(row["PageIndex"].AsString(), out page))
                {
                    page = 1;
                }
            }

            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                size = 5;
            }
            else
            {
                if (!int.TryParse(row["PageSize"].AsString(), out size))
                {
                    size = 5;
                }
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = GetConnectionStringStr(Community);
            Community.DBName = "tw2_bs";
            PubConstant.hmWyglConnectionString = strcon;
            PubConstant.tw2bsConnectionString = GetConnectionStringStr(Community);
            Global_Var.CorpId = Community.CorpID.ToString();
            Global_Var.CorpID = Community.CorpID.ToString();

            string sql = string.Format(@"SELECT(SELECT cast(RoomID AS VARCHAR)+',' FROM Tb_User_Relation 
                                            WHERE UserId='{0}' FOR XML PATH('')) AS RoomIDs; ", userID);
            using (IDbConnection conn = new SqlConnection(DbHelperSQL.ConnectionString))
            {
                dynamic value = conn.Query(sql).FirstOrDefault().RoomIDs.ToString().Trim(',');

                string result;

                if (value == null)
                {
                    result = new ApiResult(true, new DataTable()).toJson();
                    return result.Insert(result.Length - 1, ",\"PageCount\":0");
                }

                int pageCount;
                int count;
                sql = string.Format(@"SELECT a.IncidentID, isnull(convert(VARCHAR(30), a.IncidentDate, 120), '') AS IncidentDate,a.IncidentContent,
                                      case
                                        WHEN a.IsDelete=1 THEN '取消'
                                        WHEN (a.DealState=1 AND isnull(a.IsDelete,0)=0) THEN '完成'
                                        WHEN (a.DispType=1 AND isnull(a.IsDelete,0)=0 AND isnull(a.DealState,0)=0) THEN '进行中'
                                        ELSE '已受理'
                                        END AS State,
                                       (SELECT count(1) FROM Tb_HSPR_IncidentReply WHERE isnull(IsDelete,0)=0 AND IncidentID=a.IncidentID AND ReplyWay='客户线上评价') AS HasEvaluate 
                                      FROM Tb_HSPR_IncidentAccept a WHERE a.IsStatistics=1 AND a.CommID={0}
                                        AND a.RoomID IN(SELECT convert(NVARCHAR(30), colName) FROM dbo.funSplitTabel('{1}',','))",
                                        Community.CommID, value.ToString());
                if (Global_Var.CorpID == "1953")
                {
                    IDbConnection conn1 = new SqlConnection(PubConstant.hmWyglConnectionString);
                    sql = @"SELECT TypeID FROM Tb_HSPR_IncidentType WHERE CommID =@CommID AND  CorpTypeID = (
                        SELECT CorpTypeID FROM Tb_HSPR_CorpIncidentType WHERE TypeName LIKE '%园区商家投诉%'AND ISNULL(IsDelete,0)=0)";
                    string typeID = conn1.Query<string>(sql, new { CommID = Community.CommID }).FirstOrDefault();

                    sql = string.Format(@"SELECT a.IncidentID, isnull(convert(VARCHAR(30), a.IncidentDate, 120), '') AS IncidentDate,a.IncidentContent,
                                      case
                                        WHEN a.IsDelete=1 THEN '取消'
                                        WHEN (a.DealState=1 AND isnull(a.IsDelete,0)=0) THEN '完成'
                                        WHEN (a.DispType=1 AND isnull(a.IsDelete,0)=0 AND isnull(a.DealState,0)=0) THEN '进行中'
                                        ELSE '已受理'
                                        END AS State,
                                       (SELECT count(1) FROM Tb_HSPR_IncidentReply WHERE isnull(IsDelete,0)=0 
                                        AND IncidentID=a.IncidentID AND ReplyWay='客户线上评价') AS HasEvaluate 
                                      FROM Tb_HSPR_IncidentAccept a WHERE a.IsStatistics=1 AND a.CommID={0} AND a.TypeID <>'{2}'
                                        AND a.RoomID IN(SELECT convert(NVARCHAR(30), colName) FROM dbo.funSplitTabel('{1}',','))",
                                            Community.CommID, value.ToString(), typeID);
                }

                DataSet ds = GetList(out pageCount, out count, sql, page, size, "IncidentDate", 1, "IncidentID", strcon);
                result = new ApiResult(true, ds.Tables[0]).toJson();
                return result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            }
        }

        /// <summary>
        /// 中集投诉列表
        /// </summary>
        private string GetComplaintList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "小区编号不能为空").toJson();
            }

            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return new ApiResult(false, "用户ID不能为空").toJson();
            }

            string communityId = row["CommunityId"].AsString();
            string userID = row["UserID"].AsString();

            int page = 1;
            int size = 10;

            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                page = 1;
            }
            else
            {
                if (!int.TryParse(row["PageIndex"].AsString(), out page))
                {
                    page = 1;
                }
            }

            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                size = 5;
            }
            else
            {
                if (!int.TryParse(row["PageSize"].AsString(), out size))
                {
                    size = 5;
                }
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = GetConnectionStringStr(Community);
            Community.DBName = "tw2_bs";
            PubConstant.hmWyglConnectionString = strcon;
            PubConstant.tw2bsConnectionString = GetConnectionStringStr(Community);
            Global_Var.CorpId = Community.CorpID.ToString();
            Global_Var.CorpID = Community.CorpID.ToString();
            string typeID = "";
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {

                string a = @"SELECT TypeID FROM Tb_HSPR_IncidentType WHERE CommID =@CommID AND  CorpTypeID = (
                        SELECT CorpTypeID FROM Tb_HSPR_CorpIncidentType WHERE TypeName LIKE '%园区商家投诉%'AND ISNULL(IsDelete,0)=0)";
                typeID = conn.Query<string>(a, new { CommID = Community.CommID }).FirstOrDefault();

            }
            string sql = string.Format(@"SELECT(SELECT cast(RoomID AS VARCHAR)+',' FROM Tb_User_Relation 
                                            WHERE UserId='{0}' FOR XML PATH('')) AS RoomIDs; ", userID);



            using (IDbConnection conn = new SqlConnection(DbHelperSQL.ConnectionString))
            {


                dynamic value = conn.Query(sql).FirstOrDefault().RoomIDs.ToString().Trim(',');

                string result;

                if (value == null)
                {
                    result = new ApiResult(true, new DataTable()).toJson();
                    return result.Insert(result.Length - 1, ",\"PageCount\":0");
                }

                int pageCount;
                int count;
                sql = string.Format(@"SELECT a.IncidentID, isnull(convert(VARCHAR(30), a.IncidentDate, 120), '') AS IncidentDate,a.IncidentContent,
                                      case
                                        WHEN a.IsDelete=1 THEN '取消'
                                        WHEN (a.DealState=1 AND isnull(a.IsDelete,0)=0) THEN '完成'
                                        WHEN (a.DispType=1 AND isnull(a.IsDelete,0)=0 AND isnull(a.DealState,0)=0) THEN '进行中'
                                        ELSE '已受理'
                                        END AS State,
                                       (SELECT count(1) FROM Tb_HSPR_IncidentReply WHERE isnull(IsDelete,0)=0 AND IncidentID=a.IncidentID AND ReplyWay='客户线上评价') AS HasEvaluate 
                                      FROM Tb_HSPR_IncidentAccept a WHERE a.IsStatistics=1 AND a.CommID={0} AND a.TypeID ='{2}'
                                        AND a.RoomID IN(SELECT convert(NVARCHAR(30), colName) FROM dbo.funSplitTabel('{1}',','))",
                                        Community.CommID, value.ToString(), typeID);

                DataSet ds = GetList(out pageCount, out count, sql, page, size, "IncidentDate", 1, "IncidentID", strcon);
                result = new ApiResult(true, ds.Tables[0]).toJson();
                return result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            }
        }

        /// <summary>
        /// 报事详情
        /// </summary>
        private string GetIncidentDetail(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "小区编号不能为空").toJson();
            }

            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return new ApiResult(false, "客户编号不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return new ApiResult(false, "报事编号不能为空").toJson();
            }

            string communityId = row["CommunityId"].AsString();
            string custId = row["CustID"].AsString();
            string incidentID = row["IncidentID"].AsString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = new CostInfo().GetConnectionStringStr(Community);

            using (IDbConnection conn = new SqlConnection(strcon))
            {
                DataTable dt = conn.ExecuteReader(@"SELECT x.IncidentID,x.IncidentNum,x.CommName,x.RoomID,x.RoomName,
                x.IncidentMode,x.IncidentDate,x.DispMan,x.DispDate,x.ReceivingDate,x.ArriveData,x.DealMan,
                x.MainEndDate,x.IncidentContent,x.IncidentImgs,x.ProcessIncidentImgs,x.Phone,x.FinishUser,
                (SELECT TOP 1 isnull(MobileTel,'') FROM Tb_Sys_User b WHERE b.UserName=x.FinishUser) AS FinishUserMobileTel,
                (SELECT TOP 1 isnull(MobileTel,'') FROM Tb_Sys_User WHERE UserCode=x.DispUserCode) AS DispManMobileTel,
				(SELECT TOP 1 isnull(MobileTel,'') FROM Tb_Sys_User b WHERE b.UserName=x.DealMan) AS DealManMobileTel,
                (SELECT TOP 1 ServiceQuality FROM Tb_HSPR_IncidentReply WHERE IsDelete=0 AND IncidentID=@IncidentID AND ReplyWay='客户线上评价') AS ServiceQuality, 
                (SELECT TOP 1 ReplyContent FROM Tb_HSPR_IncidentReply WHERE IsDelete=0 AND IncidentID=@IncidentID AND ReplyWay='客户线上评价') AS ReplyContent, 
                case 
					WHEN isnull((SELECT TOP 1 CustName FROM Tb_HSPR_Customer WHERE CustID=x.CustID), RTRIM(LTRIM(isnull(x.AdmiMan,''))))='' THEN x.IncidentMan
					ELSE (SELECT TOP 1 CustName FROM Tb_HSPR_Customer WHERE CustID=x.CustID)
					END AS CustName,
                case
                    WHEN x.IsDelete=1 THEN '取消'
                    WHEN (x.DealState=1 AND isnull(x.IsDelete,0)=0) THEN '完成'
                    WHEN (x.DispType=1 AND isnull(x.IsDelete,0)=0 AND isnull(x.DealState,0)=0) THEN '进行中'
                    ELSE '已受理'
                    END AS State
                FROM view_HSPR_IncidentAccept_Filter x
                WHERE x.IncidentID=@IncidentID",
                new { IncidentID = incidentID }).ToDataSet().Tables[0];
                return JSONHelper.FromString(dt);
            }
        }

        private string IncidentAccept(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
            {
                return JSONHelper.FromString(false, "报事内容不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房间编号不能为空");
            }
            if (!row.Table.Columns.Contains("Phone") || string.IsNullOrEmpty(row["Phone"].ToString()))
            {
                return JSONHelper.FromString(false, "联系方式不能为空");
            }


            string communityId = row["CommunityId"].ToString();

            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = GetConnectionStringStr(Community);
            Community.DBName = "tw2_bs";
            PubConstant.hmWyglConnectionString = strcon;
            PubConstant.tw2bsConnectionString = GetConnectionStringStr(Community);
            Global_Var.CorpId = Community.CorpID.ToString();
            Global_Var.CorpID = Community.CorpID.ToString();


            string Content = row["Content"].ToString();
            string CustID = row["CustID"].ToString();
            string RoomID = row["RoomID"].ToString();
            string Phone = row["Phone"].ToString();
            string IncidentImgs = "";
            string ReserveDate = DateTime.Now.ToString();
            string IncidentMan = row["IncidentMan"].ToString();
            int isTousu = 0;
            if (row.Table.Columns.Contains("IncidentImgs") && !string.IsNullOrEmpty(row["IncidentImgs"].ToString()))
            {
                IncidentImgs = row["IncidentImgs"].ToString();
            }
            if (row.Table.Columns.Contains("ReserveDate") && !string.IsNullOrEmpty(row["ReserveDate"].ToString()))
            {
                ReserveDate = row["ReserveDate"].ToString();
            }
            if (row.Table.Columns.Contains("IsTousu") && !string.IsNullOrEmpty(row["IsTousu"].ToString()))
            {
                isTousu = AppGlobal.StrToInt(row["IsTousu"].ToString());
            }

            using (IDbConnection conn = new SqlConnection(strcon))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CommID", Community.CommID);
                parameters.Add("@CustID", CustID);
                parameters.Add("@RoomID", RoomID);
                parameters.Add("@IncidentDate", DateTime.Now);
                parameters.Add("@IncidentMan", IncidentMan);
                parameters.Add("@IncidentContent", Content);

                parameters.Add("@ReserveDate", ReserveDate);
                parameters.Add("@Phone", Phone);
                parameters.Add("@IncidentImgs", IncidentImgs);

                parameters.Add("@DealLimit", null);
                parameters.Add("@EmergencyDegree", 0);
                if (Global_Var.CorpID == "1953")
                {
                    if (isTousu == 1)
                    {
                        var sql = @"SELECT TypeID FROM Tb_HSPR_IncidentType WHERE CommID =@CommID AND  CorpTypeID = (
                        SELECT CorpTypeID FROM Tb_HSPR_CorpIncidentType WHERE TypeName LIKE '%园区商家投诉%'AND ISNULL(IsDelete,0)=0)";
                        string typeID = conn.Query<string>(sql, new { CommID = Community.CommID }).FirstOrDefault();

                        parameters.Add("@TypeID", typeID);
                    }
                    else
                    {
                        parameters.Add("@TypeID", null);
                    }

                }
                else
                {
                    parameters.Add("@TypeID", null);
                }

                conn.Execute("Proc_HSPR_IncidentAccept_PhoneInsert", parameters, null, null, CommandType.StoredProcedure);

                //获取当前报事
                string str = @"select TOP 1 * from Tb_HSPR_IncidentAccept where CommID = @CommID and RoomID = @RoomID and Phone = @Phone and IncidentImgs = @IncidentImgs and IncidentContent = @IncidentContent ORDER BY IncidentDate DESC";
                Tb_HSPR_IncidentAccept model = conn.Query<Tb_HSPR_IncidentAccept>(str, new { CommID = Community.CommID, RoomID = RoomID, Phone = Phone, IncidentImgs = IncidentImgs, IncidentContent = Content }).LastOrDefault();

                IncidentAcceptPush.SynchPushIndoorIncidentWithoutIncidentType(model);

                return JSONHelper.FromString(true, "报事成功!稍后会有人员与您联系!");
            }
        }


        private string IncidentAccept_20181211(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string CommID = row["CommID"].ToString();

            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
            {
                return JSONHelper.FromString(false, "报事内容不能为空");
            }
            string Content = row["Content"].ToString();
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }

            string CustID = row["CustID"].ToString();
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房间编号不能为空");
            }
            string RoomID = row["RoomID"].ToString();
            if (!row.Table.Columns.Contains("TypeID") || string.IsNullOrEmpty(row["TypeID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事类别不能为空");
            }
            string TypeID = row["TypeID"].ToString();
            if (!row.Table.Columns.Contains("ClassID") || string.IsNullOrEmpty(row["ClassID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事类型不能为空");
            }
            string ClassID = row["ClassID"].ToString();
            if (!row.Table.Columns.Contains("IncidentMan") || string.IsNullOrEmpty(row["IncidentMan"].ToString()))
            {
                return JSONHelper.FromString(false, "报事人不能为空");
            }
            string IncidentMan = row["IncidentMan"].ToString();
            if (!row.Table.Columns.Contains("Phone") || string.IsNullOrEmpty(row["Phone"].ToString()))
            {
                return JSONHelper.FromString(false, "联系人电话不能为空");
            }
            string Phone = row["Phone"].ToString();
            if (!row.Table.Columns.Contains("DCIncidentNum") || string.IsNullOrEmpty(row["DCIncidentNum"].ToString()))
            {
                return JSONHelper.FromString(false, "地产报事编号不能为空");
            }
            string DCIncidentNum = row["DCIncidentNum"].ToString();


            Tb_Community Community = GetCommunityByCommID(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = GetConnectionStringStr(Community);
            Community.DBName = "tw2_bs";
            PubConstant.hmWyglConnectionString = strcon;
            PubConstant.tw2bsConnectionString = GetConnectionStringStr(Community);
            Global_Var.CorpId = Community.CorpID.ToString();
            Global_Var.CorpID = Community.CorpID.ToString();

            string ReserveDate = DateTime.Now.ToString();

            using (IDbConnection conn = new SqlConnection(strcon))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CommID", Community.CommID);
                parameters.Add("@CustID", CustID);
                parameters.Add("@RoomID", RoomID);
                parameters.Add("@IncidentDate", DateTime.Now);
                parameters.Add("@IncidentMan", IncidentMan);
                parameters.Add("@IncidentContent", Content);
                parameters.Add("@ReserveDate", ReserveDate);
                parameters.Add("@Phone", Phone);
                parameters.Add("@IncidentImgs", "");
                parameters.Add("@TypeID", TypeID);
                parameters.Add("@DealLimit", null);
                parameters.Add("@EmergencyDegree", 0);
                parameters.Add("@DCIncidentNum", DCIncidentNum);

                conn.Execute("Proc_HSPR_IncidentAccept_PhoneInsert_DC", parameters, null, null, CommandType.StoredProcedure);

                //获取当前报事
                string str = @"select TOP 1 * from Tb_HSPR_IncidentAccept where CommID = @CommID and RoomID = @RoomID and Phone = @Phone and IncidentImgs = @IncidentImgs and IncidentContent = @IncidentContent ORDER BY IncidentDate DESC";
                Tb_HSPR_IncidentAccept model = conn.Query<Tb_HSPR_IncidentAccept>(str, new { CommID = Community.CommID, RoomID = RoomID, Phone = Phone, IncidentImgs = "", IncidentContent = Content }).LastOrDefault();

                IncidentAcceptPush.SynchPushIndoorIncidentWithoutIncidentType(model);

                return new ApiResult(true, new { IncidentNum = model.IncidentNum }).toJson();
            }
        }

        /// <summary>
        /// 业主取消报事
        /// </summary>
        private string IncidentCancel(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编号不能为空");
            }


            string communityId = row["CommunityId"].ToString();

            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = new Business.CostInfo().GetConnectionStringStr(Community);

            using (IDbConnection conn = new SqlConnection(strcon))
            {
                conn.Execute(@"UPDATE Tb_HSPR_IncidentAccept SET IsDelete=1 WHERE IncidentID=@IncidentID", new { IncidentID = row["IncidentID"].ToString() });

                return JSONHelper.FromString(true, "操作成功");
            }
        }

        /// <summary>
        /// 俊发版本报事
        /// </summary>
        private string IncidentAccept_jf(DataRow row)
        {
            #region 基础数据校验
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("IncidentContent") || string.IsNullOrEmpty(row["IncidentContent"].AsString()))
            {
                return new ApiResult(false, "报事内容不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("Phone") || string.IsNullOrEmpty(row["Phone"].ToString()))
            {
                return JSONHelper.FromString(false, "联系方式不能为空");
            }
            if (!row.Table.Columns.Contains("IncidentMan") || string.IsNullOrEmpty(row["IncidentMan"].AsString()))
            {
                return new ApiResult(false, "报事人不能为空").toJson();
            }
            //户内/公区
            if (!row.Table.Columns.Contains("IncidentPlace") || string.IsNullOrEmpty(row["IncidentPlace"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentPlace").toJson();
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房间编号不能为空");
            }


            string communityId = row["CommunityId"].ToString();
            string mBigCorpTypeID = "";
            string duty = "";
            string mIsTouSu = "";

            if (row.Table.Columns.Contains("BigCorpTypeID") && !string.IsNullOrEmpty(row["BigCorpTypeID"].ToString()))
            {
                mBigCorpTypeID = row["BigCorpTypeID"].ToString();
            }

            if (row.Table.Columns.Contains("Duty") && !string.IsNullOrEmpty(row["Duty"].ToString()))
            {
                duty = row["Duty"].ToString();
            }
            else
            {
                duty = "物业类";
            }

            if (row.Table.Columns.Contains("IsTouSu") && !string.IsNullOrEmpty(row["IsTouSu"].ToString()))
            {
                mIsTouSu = row["IsTouSu"].ToString();
            }

            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string incidentPlace = row["IncidentPlace"].AsString(); // 报事区域
            string custID = "0";                                    // 户内报事业主id
            string roomID = "0";                                    // 户内报事房屋编号
            string regionalID = "0";                                // 公区报事公区id
            string EqId = null;                                     // 公区报事设备id
            string taskEqId = null;                                 // 公区报事设备编码，只有设备报事的时候才有用

            if ("户内".Equals(incidentPlace))
            {
                custID = row["CustID"].AsString();
                roomID = row["RoomID"].AsString();
            }
            else
            {
                incidentPlace = "公区";
                if (!row.Table.Columns.Contains("RegionalID") || string.IsNullOrEmpty(row["RegionalID"].AsString()))
                {
                    return new ApiResult(false, "缺少参数RegionalID").toJson();
                }
                regionalID = row["RegionalID"].AsString();
                EqId = row.Table.Columns.Contains("EqId") ? row["EqId"].AsString() : null;
                taskEqId = row.Table.Columns.Contains("TaskEqId") ? row["TaskEqId"].AsString() : null;
            }

            string drClass = "1";                                           // 报事类型
            string bigCorpTypeID = mBigCorpTypeID;         // 报事大类
            string smallTypeID = null;                                      // 报事细类，口头派工时必传

            string commID = Community.CommID;                               // 小区id
            string incidentSource = "客户报事";                             // 报事来源
            string isTousu = mIsTouSu;                     // 是否投诉
            string incidentMan = row["IncidentMan"].AsString();             // 报事人
            string phone = row["Phone"].AsString();                         // 报事电话
            string incidentContent = row["IncidentContent"].AsString();     // 报事内容
            string reserveDate = null;                                      // 要求处理时间
            string incidentImgs = null;                                     // 报事图片

            if (row.Table.Columns.Contains("ReserveDate") && !string.IsNullOrEmpty(row["ReserveDate"].ToString()))
            {
                reserveDate = row["ReserveDate"].AsString();
            }
            else
            {
                reserveDate = DateTime.Now.ToString();
            }

            if (row.Table.Columns.Contains("IncidentImgs") && !string.IsNullOrEmpty(row["IncidentImgs"].AsString()))
            {
                incidentImgs = row["IncidentImgs"].AsString();
            }


            #endregion

            #region 获取数据库链接字符串
            string strcon = GetConnectionStringStr(Community);
            Community.DBName = "tw2_bs";
            PubConstant.hmWyglConnectionString = strcon;
            PubConstant.tw2bsConnectionString = GetConnectionStringStr(Community);
            Global_Var.CorpId = Community.CorpID.ToString();
            Global_Var.CorpID = Community.CorpID.ToString();
            #endregion

            #region 获取incidentID
            string incidentID;
            {
                IDbConnection conn = new SqlConnection(strcon);
                DynamicParameters param = new DynamicParameters();
                param.Add("@CommID", commID);
                param.Add("@SQLEx", "");
                incidentID = conn.ExecuteScalar("Proc_HSPR_IncidentAccept_GetMaxNum", param, null, null, CommandType.StoredProcedure).AsString();
                conn.Dispose();
            }

            #endregion

            #region 获取incidentNum
            string incidentNum;
            {
                IDbConnection conn = new SqlConnection(strcon);
                DynamicParameters param = new DynamicParameters();
                param.Add("@CommID", commID);
                incidentNum = conn.ExecuteScalar("Proc_HSPR_IncidentAccept_GetMaxIncidentNum", param, null, null, CommandType.StoredProcedure).AsString();
                conn.Dispose();
            }

            #endregion

            #region 插入报事记录
            {
                IDbConnection conn = new SqlConnection(strcon);
                DynamicParameters param = new DynamicParameters();
                param.Add("@AdmiMan", Global_Var.LoginUserName);
                param.Add("@CommID", commID);
                param.Add("@IncidentPlace", incidentPlace);
                param.Add("@IncidentID", incidentID);
                param.Add("@IncidentNum", incidentNum);
                param.Add("@IncidentSource", incidentSource);
                param.Add("@DrClass", drClass);
                param.Add("@IsTouSu", isTousu);
                param.Add("@CustID", custID);
                param.Add("@RoomID", roomID);
                param.Add("@RegionalID", regionalID);
                param.Add("@EqId", EqId);
                param.Add("@TaskEqId", taskEqId);
                param.Add("@IncidentMan", incidentMan);
                param.Add("@IncidentContent", incidentContent);
                param.Add("@IncidentImgs", incidentImgs);
                param.Add("@Phone", phone);
                param.Add("@ReserveDate", reserveDate);
                param.Add("@Duty", duty);
                param.Add("@BigCorpTypeID", bigCorpTypeID);
                param.Add("@FineCorpTypeID", smallTypeID);

                conn.Execute("Proc_HSPR_IncidentAccept_Insert_Phone_Cust", param, null, null, CommandType.StoredProcedure);

                // 推送
                var incidentInfo = conn.Query<Tb_HSPR_IncidentAccept_jh>(@"SELECT * FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID", new { IncidentID = incidentID }).FirstOrDefault();

                conn.Dispose();
            }
            #endregion

            if (!string.IsNullOrEmpty(taskEqId))
            {
                return new ApiResult(true, incidentID).toJson();
            }

            return new ApiResult(true, "受理成功").toJson();
        }

        /// <summary>
        /// 俊发获取公区对象
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetIncidentRegional(DataRow row)
        {

            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string communityId = row["CommunityId"].ToString();

            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = new Business.CostInfo().GetConnectionStringStr(Community);
            DataTable dt;
            using (IDbConnection conn = new SqlConnection(strcon))
            {
                dt = conn.ExecuteReader("Proc_HSPR_IncidentRegional_Filter", new { SQLEx = "AND ISNULL(IsDelete,0)=0 AND CommID='" + Community.CommID + "'" }, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
            }
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (null != dt && dt.Rows.Count > 0)
            {
                Dictionary<string, object> dic;
                foreach (DataRow item in dt.Rows)
                {
                    dic = new Dictionary<string, object>()
                    {
                        {"RegionalID",item["RegionalID"] }
                        ,{"RegionalNum",item["RegionalNum"] }
                        ,{"RegionalPlace",item["RegionalPlace"] }
                        ,{"RegionalName",item["RegionalName"] }
                        ,{"RegionalMemo",item["RegionalMemo"] }
                    };
                    list.Add(dic);
                }
            }
            return new ApiResult(true, list).toJson();
        }


        /// <summary>
        /// 报事历史列表(俊发)
        /// </summary>
        private string GetIncidentHistoryList_jf(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "小区编号不能为空").toJson();
            }

            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return new ApiResult(false, "用户ID不能为空").toJson();
            }

            string communityId = row["CommunityId"].AsString();
            string userID = row["UserID"].AsString();

            int page = 1;
            int size = 10;

            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                page = 1;
            }
            else
            {
                if (!int.TryParse(row["PageIndex"].AsString(), out page))
                {
                    page = 1;
                }
            }

            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                size = 5;
            }
            else
            {
                if (!int.TryParse(row["PageSize"].AsString(), out size))
                {
                    size = 5;
                }
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = new CostInfo().GetConnectionStringStr(Community);

            // 查询CustID
            string sql = string.Format(@"SELECT(SELECT cast(CustID AS VARCHAR)+',' FROM Tb_User_Relation 
                                            WHERE UserId='{0}' FOR XML PATH('')) AS CustID; ", userID);
            using (IDbConnection conn = new SqlConnection(DbHelperSQL.ConnectionString))
            {
                dynamic value = conn.Query(sql).FirstOrDefault().CustID;
                value = value.ToString().Trim(',');

                sql = @"SELECT Mobile FROM Tb_User WHERE Id=@Id";
                string userMobile = conn.Query<string>(sql, new { Id = userID }).FirstOrDefault();

                string result;
                if (value == null && userMobile == null)
                {
                    result = new ApiResult(true, new DataTable()).toJson();
                    return result.Insert(result.Length - 1, ",\"PageCount\":0");
                }

                int pageCount;
                int count;
                sql = string.Format(@"SELECT a.IncidentID, isnull(convert(VARCHAR(30), a.IncidentDate, 120), '') AS IncidentDate,
                                    a.IncidentContent,
                                      case
                                        WHEN a.IsDelete=1 THEN '取消'
                                        WHEN (a.DealState=1 AND isnull(a.IsDelete,0)=0) THEN '完成'
                                        WHEN (a.DispType=1 AND isnull(a.IsDelete,0)=0 AND isnull(a.DealState,0)=0) THEN '进行中'
                                        ELSE '已受理'
                                        END AS State,
                                       (SELECT count(1) FROM Tb_HSPR_IncidentReply WHERE IncidentID=a.IncidentID AND ReplyWay='客户线上评价') AS HasEvaluate 
                                      FROM Tb_HSPR_IncidentAccept a WHERE a.IsStatistics=1 AND a.CommID={0} 
                                        AND (a.Phone LIKE '%{1}%' OR a.CustID IN(SELECT convert(NVARCHAR(30), colName) FROM dbo.funSplitTabel('{2}',',')))",
                                        Community.CommID, userMobile, value.ToString());

                if (Community.CorpID == 2021)
                {
                    sql = string.Format(@"SELECT a.IncidentID, isnull(convert(VARCHAR(30), a.IncidentDate, 120), '') AS IncidentDate,
                                    a.IncidentContent,
                                      case
                                        WHEN a.IsDelete=1 THEN '取消'
                                        WHEN (a.DealState=1 AND isnull(a.IsDelete,0)=0) THEN '完成'
                                        WHEN (a.DispType=1 AND isnull(a.IsDelete,0)=0 AND isnull(a.DealState,0)=0) THEN '进行中'
                                        ELSE '已受理'
                                        END AS State,
                                       (SELECT count(1) FROM Tb_HSPR_IncidentReply WHERE IncidentID=a.IncidentID AND ReplyType='App业主自评') AS HasEvaluate 
                                      FROM Tb_HSPR_IncidentAccept a WHERE a.IsStatistics=1 AND a.CommID={0}
                                        AND (a.Phone LIKE '%{1}%' OR a.CustID IN(SELECT convert(NVARCHAR(30), colName) FROM dbo.funSplitTabel('{2}',',')))",
                                        Community.CommID, userMobile, value.ToString());
                }
                if (Community.CorpID == 2045 || Community.CorpID == 2046)
                {
                    sql = string.Format(@"SELECT a.IncidentID, isnull(convert(VARCHAR(30), a.IncidentDate, 120), '') AS IncidentDate,
                                    a.IncidentContent,
                                      case
                                        WHEN a.IsDelete=1 THEN '取消'
                                        WHEN (a.DealState=1 AND isnull(a.IsDelete,0)=0) THEN '完成'
                                        WHEN (a.DispType=1 AND isnull(a.IsDelete,0)=0 AND isnull(a.DealState,0)=0) THEN '进行中'
                                        ELSE '已受理'
                                        END AS State,
                                       (SELECT count(1) FROM Tb_HSPR_IncidentReply WHERE IncidentID=a.IncidentID AND ReplyWay='客户线上评价') AS HasEvaluate 
                                      FROM Tb_HSPR_IncidentAccept a WHERE a.IsStatistics=1 AND a.CommID={0} AND IncidentMode='业主APP'
                                        AND (a.Phone LIKE '%{1}%' OR a.CustID IN(SELECT convert(NVARCHAR(30), colName) FROM dbo.funSplitTabel('{2}',',')))",
                        Community.CommID, userMobile, value.ToString());
                }
                // 丽创仅显示业主APP发起的报事
                if (Community.CorpID == 2087)
                {
                    sql = string.Format(@"SELECT a.IncidentID, isnull(convert(VARCHAR(30), a.IncidentDate, 120), '') AS IncidentDate,
                                    a.IncidentContent,
                                      case
                                        WHEN a.IsDelete=1 THEN '取消'
                                        WHEN (a.DealState=1 AND isnull(a.IsDelete,0)=0) THEN '完成'
                                        WHEN (a.DispType=1 AND isnull(a.IsDelete,0)=0 AND isnull(a.DealState,0)=0) THEN '进行中'
                                        ELSE '已受理'
                                        END AS State,
                                       (SELECT count(1) FROM Tb_HSPR_IncidentReply WHERE IncidentID=a.IncidentID) AS HasEvaluate 
                                      FROM Tb_HSPR_IncidentAccept a WHERE a.IsStatistics=1 AND a.CommID={0} AND IncidentMode = '业主APP'
                                        AND (a.Phone LIKE '%{1}%' OR a.CustID IN(SELECT convert(NVARCHAR(30), colName) FROM dbo.funSplitTabel('{2}',',')))",
                                        Community.CommID, userMobile, value.ToString());
                }
                DataSet ds = GetList(out pageCount, out count, sql, page, size, "IncidentDate", 1, "IncidentID", strcon);
                result = new ApiResult(true, ds.Tables[0]).toJson();
                return result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            }
        }

        /// <summary>
        /// 报事详情(俊发)
        /// </summary>
        private string GetIncidentDetail_jf(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "小区编号不能为空").toJson();
            }

            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return new ApiResult(false, "客户编号不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return new ApiResult(false, "报事编号不能为空").toJson();
            }

            string communityId = row["CommunityId"].AsString();
            string custId = row["CustID"].AsString();
            string incidentID = row["IncidentID"].AsString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = GetConnectionStringStr(Community);
            Community.DBName = "tw2_bs";
            PubConstant.hmWyglConnectionString = strcon;
            PubConstant.tw2bsConnectionString = GetConnectionStringStr(Community);
            Global_Var.CorpId = Community.CorpID.ToString();
            Global_Var.CorpID = Community.CorpID.ToString();

            using (IDbConnection conn = new SqlConnection(strcon))
            {
                DataTable dt = null;
                if (Community.CorpID == 2021)
                {
                    dt = conn.ExecuteReader(@"SELECT x.IncidentID,x.IncidentNum,x.CommName,x.RoomID,x.RoomName,x.IncidentMode,x.IncidentDate,x.DispMan,x.DispDate,x.ReceivingDate,
                    x.ArriveData,x.DealMan,x.MainEndDate,x.IncidentContent,x.IncidentImgs,x.ProcessIncidentImgs,x.Phone,x.FinishUser,
                    (SELECT TOP 1 isnull(MobileTel,'') FROM Tb_HSPR_IncidentAcceptDeal a LEFT JOIN Tb_Sys_User b ON a.UserCode=b.UserCode
                        WHERE b.UserName=x.FinishUser AND a.IncidentID=x.IncidentID ) AS FinishUserMobileTel,
                    (SELECT TOP 1 isnull(MobileTel,'') FROM Tb_Sys_User WHERE UserCode=x.DispUserCode) AS DispManMobileTel,
                    (SELECT STUFF((SELECT ','+ISNULL(MobileTel,'') FROM Tb_Sys_User WHERE UserCode IN(
                          SELECT UserCode FROM Tb_HSPR_IncidentAcceptDeal WHERE IncidentID=x.IncidentID) for xml path('')),1,1,''))
                       AS DealManMobileTel,
                    (SELECT TOP 1 ServiceQuality FROM Tb_HSPR_IncidentReply WHERE IncidentID=@IncidentID AND ReplyType='App业主自评') AS ServiceQuality, 
                    (SELECT TOP 1 ReplyContent FROM Tb_HSPR_IncidentReply WHERE IncidentID=@IncidentID AND ReplyType='App业主自评') AS ReplyContent, 
                    x.IncidentMan AS CustName,
                    case
                        WHEN x.IsDelete=1 THEN '取消'
                        WHEN (x.DealState=1 AND isnull(x.IsDelete,0)=0) THEN '完成'
                        WHEN (x.DispType=1 AND isnull(x.IsDelete,0)=0 AND isnull(x.DealState,0)=0) THEN '进行中'
                        ELSE '已受理'
                        END AS State
                    FROM view_HSPR_IncidentAccept_Filter x
                    WHERE x.IncidentID=@IncidentID",
                    new { IncidentID = incidentID }).ToDataSet().Tables[0];
                }
                else
                {//嘉和 新增HeadImg
                    dt = conn.ExecuteReader(@"SELECT x.IncidentID,x.IncidentNum,x.CommName,x.RoomID,x.RoomName,x.IncidentMode,x.IncidentDate,x.DispMan,x.DispDate,x.ReceivingDate,
                    x.ArriveData,x.DealMan,x.MainEndDate,x.IncidentContent,x.IncidentImgs,x.ProcessIncidentImgs,x.Phone,x.FinishUser,
                    (SELECT TOP 1 isnull(MobileTel,'') FROM Tb_HSPR_IncidentAcceptDeal a LEFT JOIN Tb_Sys_User b ON a.UserCode=b.UserCode
                        WHERE b.UserName=x.FinishUser AND a.IncidentID=x.IncidentID ) AS FinishUserMobileTel,
                    (SELECT TOP 1 isnull(MobileTel,'') FROM Tb_Sys_User WHERE UserCode=x.DispUserCode) AS DispManMobileTel,
                    (SELECT HeadImg FROM Tb_Sys_User WHERE UserName=x.DealMan) AS HeadImg,
                    (SELECT STUFF((SELECT ','+ISNULL(MobileTel,'') FROM Tb_Sys_User WHERE UserCode IN(
                          SELECT UserCode FROM Tb_HSPR_IncidentAcceptDeal WHERE IncidentID=x.IncidentID) for xml path('')),1,1,''))
                       AS DealManMobileTel,
                    (SELECT TOP 1 ServiceQuality FROM Tb_HSPR_IncidentReply WHERE IncidentID=@IncidentID AND ReplyWay='客户线上评价') AS ServiceQuality, 
                    (SELECT TOP 1 ReplyContent FROM Tb_HSPR_IncidentReply WHERE IncidentID=@IncidentID AND ReplyWay='客户线上评价') AS ReplyContent, 
                    x.IncidentMan AS CustName,
                    case
                        WHEN x.IsDelete=1 THEN '取消'
                        WHEN (x.DealState=1 AND isnull(x.IsDelete,0)=0) THEN '完成'
                        WHEN (x.DispType=1 AND isnull(x.IsDelete,0)=0 AND isnull(x.DealState,0)=0) THEN '进行中'
                        ELSE '已受理'
                        END AS State
                    FROM view_HSPR_IncidentAccept_Filter x
                    WHERE x.IncidentID=@IncidentID",
                    new { IncidentID = incidentID }).ToDataSet().Tables[0];
                }

                return JSONHelper.FromString(dt);
            }
        }


        private string GetIncidentBigType_Property(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("IncidentPlace") || string.IsNullOrEmpty(row["IncidentPlace"].ToString()))
            {
                return JSONHelper.FromString(false, "报事区域不能为空");
            }
            if (!row.Table.Columns.Contains("ClassID") || string.IsNullOrEmpty(row["ClassID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事类型不能为空");
            }


            string strcon = "";
            bool bl = GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            using (IDbConnection conn = new SqlConnection(strcon))
            {
                var resultSet = conn.Query(@"SELECT TypeID, TypeName, DealLimit2 FROM Tb_HSPR_IncidentType
                        WHERE len(TypeCode) = 4 AND CommID=@CommID; ", new { CommID = row["CommID"] });

                return new ApiResult(true, resultSet).toJson();
            }
        }

        private string IncidentAccept_Property(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
            {
                return JSONHelper.FromString(false, "报事内容不能为空");
            }
            if (!row.Table.Columns.Contains("RegionalID") || string.IsNullOrEmpty(row["RegionalID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事区域不能为空");
            }
            if (!row.Table.Columns.Contains("TypeID") || string.IsNullOrEmpty(row["TypeID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事项目不能为空");
            }
            if (!row.Table.Columns.Contains("ClassID") || string.IsNullOrEmpty(row["ClassID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事类型不能为空");
            }
            if (!row.Table.Columns.Contains("EmergencyDegree") || string.IsNullOrEmpty(row["EmergencyDegree"].ToString()))
            {
                return JSONHelper.FromString(false, "紧急程度不能为空");
            }

            int returnType = 1;
            if (row.Table.Columns.Contains("ReturnType"))
            {
                returnType = AppGlobal.StrToInt(row["ReturnType"].ToString());
            }

            string IncidentDate = DateTime.Now.ToString();
            if (row.Table.Columns.Contains("IncidentDate") && !string.IsNullOrEmpty(row["IncidentDate"].ToString()))
            {
                IncidentDate = row["IncidentDate"].ToString();
            }

            int ClassID = AppGlobal.StrToInt(row["ClassID"].ToString());
            if (ClassID == 2)
            {
                if (!row.Table.Columns.Contains("DealMan") || string.IsNullOrEmpty(row["DealMan"].ToString()))
                {
                    return JSONHelper.FromString(false, "处理人不能为空");
                }
                if (!row.Table.Columns.Contains("DealUser") || string.IsNullOrEmpty(row["DealUser"].ToString()))
                {
                    return JSONHelper.FromString(false, "处理人编号不能为空");
                }
            }
            string locationId = null;
            string objectId = null;
            if (row.Table.Columns.Contains("LocationID") && !string.IsNullOrEmpty(row["LocationID"].ToString()))
            {
                locationId = row["LocationID"].ToString();
            }
            if (row.Table.Columns.Contains("ObjectID") && !string.IsNullOrEmpty(row["ObjectID"].ToString()))
            {
                objectId = row["ObjectID"].ToString();
            }


            string Content = row["Content"].ToString();
            string CommID = row["CommID"].ToString();
            string RegionalID = row["RegionalID"].ToString();
            string TypeID = row["TypeID"].ToString();
            string Phone = Global_Var.LoginMobile;
            string images = "";
            string ObjectIDList = "";
            int EmergencyDegree = AppGlobal.StrToInt(row["EmergencyDegree"].ToString());
            int DealLimit = 0;

            if (row.Table.Columns.Contains("Phone"))
            {
                Phone = row["Phone"].ToString();
            }
            if (row.Table.Columns.Contains("images"))
            {
                images = row["images"].ToString();
            }
            if (row.Table.Columns.Contains("ObjectIDList"))
            {
                ObjectIDList = row["ObjectIDList"].ToString();
            }
            if (row.Table.Columns.Contains("DealLimit"))
            {
                DealLimit = AppGlobal.StrToInt(row["DealLimit"].ToString());
            }

            // 设备报事相关，数据字段为这两个
            string deviceId = null;
            string taskEqId = null;

            if (row.Table.Columns.Contains("DeviceID") && row["DeviceID"].ToString() != "(null)")
            {
                deviceId = row["DeviceID"].ToString();
            }

            if (row.Table.Columns.Contains("TaskEqId") && row["TaskEqId"].ToString() != "(null)")
            {
                taskEqId = row["TaskEqId"].ToString();
            }

            string strcon = "";
            string backStr = "";
            string incidentId = "";
            string incidentNum = "";
            try
            {
                bool bl = GetDBServerPath(CommID, out strcon);
                if (bl == false)
                {
                    return JSONHelper.FromString(false, strcon);
                }

                IDbConnection con = new SqlConnection(strcon);

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CommID", CommID);
                parameters.Add("@AdmiMan", Global_Var.LoginUserName);
                parameters.Add("@IncidentContent", Content);

                parameters.Add("@Phone", Phone);
                parameters.Add("@IncidentImgs", images);
                parameters.Add("@RegionalID", RegionalID);

                parameters.Add("@TypeID", TypeID);
                parameters.Add("@DealLimit", DealLimit);
                parameters.Add("@EmergencyDegree", EmergencyDegree);
                if (!string.IsNullOrEmpty(locationId))
                {
                    parameters.Add("@LocationID", locationId);
                }
                if (!string.IsNullOrEmpty(objectId))
                {
                    parameters.Add("@ObjectID", objectId);
                }

                //口头派工时
                if (ClassID == 2)
                {
                    parameters.Add("@DispType", 3);
                    parameters.Add("@DispMan", Global_Var.UserName);
                    parameters.Add("@DealMan", row["DealMan"]);
                    parameters.Add("@DealUser", row["DealUser"]);
                    parameters.Add("@CoordinateNum",
                        new IncidentAcceptManage().HSPR_IncidentAssigned_GetCoordinateNum(row["CommID"].ToString(), 3, "K"));
                }
                else
                {
                    parameters.Add("@DispType", 0);
                    parameters.Add("@DispMan", "");
                    parameters.Add("@DealMan", "");
                    parameters.Add("@DealUser", "");
                    parameters.Add("@CoordinateNum", "");
                }

                if (string.IsNullOrEmpty(deviceId) && string.IsNullOrEmpty(taskEqId))
                {
                    con.ExecuteScalar("Proc_HSPR_IncidentAccept_PhoneInsert_Region_WG", parameters, null, null, CommandType.StoredProcedure);
                }
                else
                {
                    parameters.Add("@DeviceID", deviceId);
                    parameters.Add("@TaskEqId", taskEqId);
                    con.ExecuteScalar("Proc_HSPR_IncidentAccept_PhoneInsert_Region_WG_Device", parameters, null, null, CommandType.StoredProcedure);
                }

                con.Dispose();
                //获取当前报事
                con = new SqlConnection(strcon);
                string str = "select top 1 * from Tb_HSPR_IncidentAccept where CommID=@CommID  and Phone=@Phone  and IncidentImgs=@IncidentImgs and IncidentContent=@IncidentContent order by incidentdate desc";
                Tb_HSPR_IncidentAccept model = con.Query<Tb_HSPR_IncidentAccept>(str, new { CommID = CommID, Phone = Phone, IncidentImgs = images, IncidentContent = Content }).LastOrDefault();
                incidentId = (model != null ? model.IncidentID.ToString() : "");
                incidentNum = (model != null ? model.IncidentNum : "");
                //2018-01-17,敬志强
                //解决公区报事无预约时间的问题
                //省时省力的解决办法
                if (!string.IsNullOrEmpty(incidentNum))
                {
                    con.Execute("UPDATE Tb_HSPR_IncidentAccept SET ReserveDate = @ReserveDate WHERE IncidentNum = @IncidentNum", new { ReserveDate = IncidentDate, IncidentNum = incidentNum }, null, null, CommandType.Text);
                }
                if (ObjectIDList != "")
                {
                    if (model != null)
                    {
                        //删除所有子表数据
                        string IncidentID = model.IncidentID.ToString();
                        SqlParameter[] dbParams = new SqlParameter[] {
                        new SqlParameter("@IncidentID",SqlDbType.BigInt)
                    };
                        dbParams[0].Value = IncidentID;
                        int rowNum = con.Execute("Proc_HSPR_IncidentAcceptObject_DeleteALL", dbParams, null, null, CommandType.StoredProcedure);
                        con.Dispose();
                        //插入子表数据
                        string[] idListPar = ObjectIDList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < idListPar.Length; i++)
                        {
                            con = new SqlConnection(strcon);
                            SqlParameter[] sqlpar = new SqlParameter[] {
                                new SqlParameter("@IncidentAcceptObjectID",SqlDbType.BigInt),
                                new SqlParameter("@ObjectID",SqlDbType.BigInt),
                                new SqlParameter("@IncidentID",SqlDbType.BigInt),
                                new SqlParameter("@IsDelete",SqlDbType.Int),
                                new SqlParameter("@CommID",SqlDbType.Int)
                            };
                            sqlpar[0].Value = 0;
                            sqlpar[1].Value = AppGlobal.StrToLong(idListPar[i]);
                            sqlpar[2].Value = IncidentID;
                            sqlpar[3].Value = 0;
                            sqlpar[4].Value = CommID;
                            dbParams[0].Value = IncidentID;
                            int rowNum_inser = con.Execute("Proc_HSPR_IncidentAcceptObject_Insert", sqlpar, null, null, CommandType.StoredProcedure);
                            con.Dispose();
                        }
                    }
                }
                // 推送
                IncidentAcceptPush.SynchPushPublicIncident(model);

                #region  鸿坤报事需要把报事信息推送给第三方400 
                try
                {
                    //鸿坤单独
                    if (Global_Var.LoginCorpID == "1973")
                    {
                        #region 同步新增报事   
                        Dictionary<string, string> dir = new Dictionary<string, string>();
                        dir.Add("incidentID", model.IncidentID.ToString());
                        dir.Add("commID", model.CommID.ToString());
                        dir.Add("custID", model.CustID.ToString());
                        dir.Add("roomID", model.RoomID.ToString());
                        dir.Add("corpTypeID", model.TypeID);
                        dir.Add("incidentPlace", model.IncidentPlace);
                        dir.Add("incidentMan", model.IncidentMan);
                        dir.Add("incidentDate", model.IncidentDate.ToString());
                        dir.Add("incidentMode", model.IncidentMode);
                        dir.Add("dealLimit", model.DealLimit.ToString());
                        dir.Add("replyLimit", model.ReplyLimit);
                        dir.Add("incidentContent", model.IncidentContent);
                        dir.Add("reserveDate", model.ReserveDate.ToString());
                        dir.Add("phone", model.Phone);
                        dir.Add("admiMan", model.AdmiMan);
                        dir.Add("admiDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        dir.Add("dispType", model.DispType.ToString());
                        dir.Add("dispMan", string.IsNullOrEmpty(model.DispMan) ? "" : model.DispMan.ToString());
                        dir.Add("dispDate", model.DispDate.ToString());
                        dir.Add("dispLimit", model.DispLimit);
                        dir.Add("dealMan", model.DealMan);
                        dir.Add("coordinateNum", string.IsNullOrEmpty(model.CoordinateNum) ? "" : model.CoordinateNum.ToString());
                        dir.Add("mainEndDate", ClassID == 2 ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : "");
                        dir.Add("isDeal", ClassID == 2 ? "1" : "0");//0:未完结1：完结    我们的口派直接完结 
                        dir.Add("incidentnum", model.IncidentNum.ToString());
                        dir.Add("operateType", "0");//0：新增，1：修改，2：删除 
                        SynchronizeIncidentData.SynchronizeData_WorkOrderInfo(dir, Connection.GetConnection("8"));

                        #endregion
                    }

                }
                catch
                {


                }



                #endregion

            }
            catch (Exception ex)
            {
                backStr = ex.Message;
            }

            if (backStr != "")
            {
                return JSONHelper.FromString(false, backStr);
            }
            else
            {
                if (returnType == 0)
                {
                    return JSONHelper.FromString(true, incidentId);
                }
                else if (returnType == 1)
                {
                    return JSONHelper.FromString(true, incidentNum);
                }
                else
                {
                    return JSONHelper.FromString(true, incidentId + "|" + incidentNum);
                }
            }
        }

        private string IncidentAccept_Property_20181211(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
            {
                return JSONHelper.FromString(false, "报事内容不能为空");
            }

            if (!row.Table.Columns.Contains("TypeID") || string.IsNullOrEmpty(row["TypeID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事项目不能为空");
            }
            if (!row.Table.Columns.Contains("ClassID") || string.IsNullOrEmpty(row["ClassID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事类型不能为空");
            }
            if (!row.Table.Columns.Contains("Phone") || string.IsNullOrEmpty(row["Phone"].ToString()))
            {
                return JSONHelper.FromString(false, "报事手机不能为空");
            }
            string IncidentDate = DateTime.Now.ToString();
            if (row.Table.Columns.Contains("Time") && !string.IsNullOrEmpty(row["Time"].ToString()))
            {
                IncidentDate = row["Time"].ToString();
            }

            int ClassID = AppGlobal.StrToInt(row["ClassID"].ToString());

            string Content = row["Content"].ToString();
            string CommID = row["CommID"].ToString();
            string TypeID = row["TypeID"].ToString();
            string Phone = row["Phone"].ToString();
            string dcNum = row["DCIncidentNum"].ToString();

            Tb_Community Community = GetCommunityByCommID(CommID);
            if (null == Community)
            {
                return new ApiResult(false, "项目不存在").toJson();
            }

            string strcon = GetConnectionStringStr(Community);
            Community.DBName = "tw2_bs";
            PubConstant.hmWyglConnectionString = strcon;
            PubConstant.tw2bsConnectionString = GetConnectionStringStr(Community);
            Global_Var.CorpId = Community.CorpID.ToString();
            Global_Var.CorpID = Community.CorpID.ToString();

            string incidentId = "";
            string incidentNum = "";
            try
            {
                IDbConnection con = new SqlConnection(strcon);

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CommID", CommID);
                parameters.Add("@AdmiMan", Global_Var.LoginUserName);
                parameters.Add("@IncidentContent", Content);

                parameters.Add("@Phone", Phone);
                parameters.Add("@IncidentImgs", "");
                parameters.Add("@RegionalID", "");

                parameters.Add("@TypeID", TypeID);
                parameters.Add("@DealLimit", "");
                parameters.Add("@EmergencyDegree", "");
                //口头派工时
                if (ClassID == 2)
                {
                    parameters.Add("@DispType", 3);
                    parameters.Add("@DispMan", Global_Var.UserName);
                    parameters.Add("@DealMan", "");
                    parameters.Add("@DealUser", "");
                    parameters.Add("@CoordinateNum",
                        new IncidentAcceptManage().HSPR_IncidentAssigned_GetCoordinateNum(row["CommID"].ToString(), 3, "K"));
                }
                else
                {
                    parameters.Add("@DispType", 0);
                    parameters.Add("@DispMan", "");
                    parameters.Add("@DealMan", "");
                    parameters.Add("@DealUser", "");
                    parameters.Add("@CoordinateNum", "");
                }

                if (Global_Var.CorpID == "1970")
                {
                    parameters.Add("@DCIncidentNum", dcNum);
                }

                con.ExecuteScalar("Proc_HSPR_IncidentAccept_PhoneInsert_Region_WG", parameters, null, null, CommandType.StoredProcedure);
                con.Dispose();
                //获取当前报事
                con = new SqlConnection(strcon);
                string str = "select top 1 * from Tb_HSPR_IncidentAccept where CommID=@CommID  and Phone=@Phone  and IncidentImgs=@IncidentImgs and IncidentContent=@IncidentContent order by incidentdate desc";
                Tb_HSPR_IncidentAccept model = con.Query<Tb_HSPR_IncidentAccept>(str, new { CommID = CommID, Phone = Phone, IncidentImgs = "", IncidentContent = Content }).LastOrDefault();
                incidentId = (model != null ? model.IncidentID.ToString() : "");
                incidentNum = (model != null ? model.IncidentNum : "");
                //2018-01-17,敬志强
                //解决公区报事无预约时间的问题
                //省时省力的解决办法
                if (!string.IsNullOrEmpty(incidentNum))
                {
                    con.Execute("UPDATE Tb_HSPR_IncidentAccept SET ReserveDate = IncidentDate WHERE IncidentNum = @IncidentNum", new { IncidentNum = incidentNum }, null, null, CommandType.Text);
                }
                // 推送
                IncidentAcceptPush.SynchPushPublicIncident(model);


                return new ApiResult(true, new { IncidentNum = incidentNum }).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex).toJson();
            }
        }

        private string GetIncidentState(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string CommID = row["CommID"].ToString();
            Tb_Community Community = GetCommunityByCommID(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string IncidentID = string.Empty;
            if (row.Table.Columns.Contains("IncidentID"))
            {
                IncidentID = row["IncidentID"].ToString();
            }
            string IncidentNum = string.Empty;
            if (row.Table.Columns.Contains("IncidentNum"))
            {
                IncidentNum = row["IncidentNum"].ToString();
            }
            string strcon = GetConnectionStringStr(Community);
            using (IDbConnection conn = new SqlConnection(strcon))
            {
                dynamic Incident = null;
                if (string.IsNullOrEmpty(IncidentID))
                {
                    IncidentID = conn.QueryFirstOrDefault<string>("SELECT IncidentID FROM Tb_HSPR_IncidentAccept WHERE IncidentNum = @IncidentNum", new { IncidentNum });
                }
                Incident = conn.QueryFirstOrDefault("SELECT ISNULL(DispType,0),ISNULL(DealState,0),ISNULL(IsRework,0) FROM Tb_HSPR_IncidentAccept WHERE IncidentID = @IncidentID", new { IncidentID });
                if (null == Incident)
                {
                    return new ApiResult(false, "未查询到对应的报事").toJson();
                }
                int IsReply = conn.Query("SELECT * FROM Tb_HSPR_IncidentReply WHERE IncidentID = @IncidentID", new { IncidentID }).Count() > 0 ? 1 : 0;
                Incident.IsReply = IsReply;
                string IncidentState;
                if (Convert.ToInt32(Incident.DispType) == 0)
                {
                    IncidentState = "已受理未分派";
                }
                else if (Convert.ToInt32(Incident.DispType) == 1 && Convert.ToInt32(Incident.DealState) == 0 && Convert.ToInt32(Incident.IsRework) == 0)
                {
                    IncidentState = "已分派未处理";
                }
                else if (Convert.ToInt32(Incident.DispType) == 1 && Convert.ToInt32(Incident.DealState) == 0 && Convert.ToInt32(Incident.IsRework) == 1)
                {
                    IncidentState = "已返工未处理";
                }
                else if (Convert.ToInt32(Incident.DispType) == 1 && Convert.ToInt32(Incident.DealState) == 1 && Convert.ToInt32(Incident.IsReply) == 0)
                {
                    IncidentState = "已处理未回访";
                }
                else if (Convert.ToInt32(Incident.DispType) == 1 && Convert.ToInt32(Incident.DealState) == 1 && Convert.ToInt32(Incident.IsReply) == 1)
                {
                    IncidentState = "已回访";
                }
                else
                {
                    IncidentState = "报事状态异常";
                }
                return new ApiResult(true, IncidentState).toJson();
            }
        }
    }
}
