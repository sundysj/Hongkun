using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.HSPR;
using MobileSoft.Model.Unified;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace Business
{
    public class HK_IncidentAccept : PubInfo
    {
        public HK_IncidentAccept()
        {
            base.Token = "20181129HK_IncidentAccept";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "SaveSuggestion":      // 保存投诉建议
                    Trans.Result = SaveSuggestion(Row);
                    break;
                case "GetSuggestionSimpleList":   // 投诉建议列表
                    Trans.Result = GetSuggestionSimpleList(Row);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 投诉建议列表
        /// </summary>
        private string SaveSuggestion(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
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

            string Content = row["Content"].ToString();
            string CommID = row["CommID"].ToString();
            string CustID = row["CustID"].ToString();
            string RoomID = row["RoomID"].ToString();
            string Phone = row["Phone"].ToString();
            string Imgs = "";

            if (row.Table.Columns.Contains("Imgs") && !string.IsNullOrEmpty(row["Imgs"].ToString()))
            {
                Imgs = row["Imgs"].ToString();
            }

            //查询小区
            Tb_Community Community = GetCommunityByCommID(CommID);
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            Global_Var.CorpId = Community.CorpID.ToString();
            Global_Var.CorpID = Community.CorpID.ToString();
            Global_Var.LoginCorpID = Community.CorpID.ToString();
            PubConstant.hmWyglConnectionString = GetConnectionStr(Community);

            Regex regex = new Regex(@"(initial catalog = [^;]+);");
            if (regex.IsMatch(PubConstant.hmWyglConnectionString))
            {
                PubConstant.tw2bsConnectionString = regex.Replace(PubConstant.hmWyglConnectionString, @"initial catalog = tw2_bs;");
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var custName = conn.Query<string>(@"SELECT CustName FROM Tb_HSPR_Customer WHERE CustID=@CustID",
                    new { CustID = CustID }).FirstOrDefault();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CommID", Community.CommID);
                parameters.Add("@CustID", CustID);
                parameters.Add("@RoomID", RoomID);
                parameters.Add("@IncidentDate", DateTime.Now);
                parameters.Add("@IncidentMan", custName);
                parameters.Add("@IncidentContent", Content);
                parameters.Add("@ReserveDate", null);
                parameters.Add("@Phone", Phone);
                parameters.Add("@IncidentImgs", Imgs);
                parameters.Add("@TypeID", null);
                parameters.Add("@DealLimit", null);
                parameters.Add("@EmergencyDegree", 0);
                parameters.Add("@IncidentMode", "App爱我家园");
                parameters.Add("@IncidentID", 0, DbType.Int64, ParameterDirection.Output);
                //需求4067 业主APP填报爱我家园信息时，自动转为工单（incidentaccept表数据），默认将报事类别设定为：投诉-物业管理-综合服务类
                conn.Execute("Proc_HSPR_IncidentAccept_PhoneInsert", parameters, null, null, CommandType.StoredProcedure);

                //获取当前报事
                string str = "SELECT * FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID";
                dynamic model = conn.Query(str, new
                {
                    IncidentID = parameters.Get<long>(@"IncidentID")
                }).LastOrDefault();

                //zw增加model非空验证
                if (model != null)
                {
                    IncidentAcceptPush.SynchPushIndoorIncidentWithoutIncidentType(new Tb_HSPR_IncidentAccept()
                    {
                        TypeID = model.TypeID,
                        CommID = model.CommID,
                        RoomID = model.RoomID,
                        IncidentID = model.IncidentID,
                        IncidentPlace = model.IncidentPlace,
                        IncidentMan = model.IncidentMan,
                    });
                }

                return JSONHelper.FromString(true, "提交成功");
            }
        }

        /// <summary>
        /// 投诉建议列表
        /// </summary>
        private string GetSuggestionSimpleList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return new ApiResult(false, "小区编号不能为空").toJson();
            }

            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return new ApiResult(false, "用户ID不能为空").toJson();
            }

            string commID = row["CommID"].AsString();
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
            Tb_Community Community = GetCommunityByCommID(commID);
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            PubConstant.hmWyglConnectionString = GetConnectionStr(Community);

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                dynamic value = conn.Query(@"SELECT(SELECT cast(RoomID AS VARCHAR)+',' FROM Tb_User_Relation WHERE UserId=@UserId FOR XML PATH('')) AS RoomIDs",
                                                new { UserId = userID }).FirstOrDefault().RoomIDs.ToString().Trim(',');

                string result;

                if (value == null)
                {
                    result = new ApiResult(true, new DataTable()).toJson();
                    return result.Insert(result.Length - 1, ",\"PageCount\":0");
                }

                int pageCount;
                int count;
       
      
                string sql = string.Format(@"SELECT a.IncidentID, isnull(convert(VARCHAR(30), a.IncidentDate, 120), '') AS IncidentDate,a.IncidentContent,
                                              CASE
                                                WHEN a.IsDelete=1 THEN '删除'
                                                WHEN (a.DealState=1 AND isnull(a.IsDelete,0)=0) THEN '已回复'
                                                WHEN (a.DispType=1 AND isnull(a.IsDelete,0)=0 AND isnull(a.DealState,0)=0) THEN '已查看'
                                                ELSE '已提交'
                                                END AS State,
                                               (SELECT count(1) FROM Tb_HSPR_IncidentReply WHERE isnull(IsDelete,0)=0 AND IncidentID=a.IncidentID AND ReplyWay='客户线上评价') AS HasEvaluate 
                                              FROM Tb_HSPR_IncidentAccept a WHERE a.IsStatistics=1 AND a.IncidentMode='App投诉建议' AND a.CommID={0}
                                                AND a.RoomID IN(SELECT convert(NVARCHAR(30), colName) FROM dbo.funSplitTabel('{1}',','))  AND ISNULL(a.IsDelete,0) = 0",
                                                Community.CommID, value.ToString());

                if (Global_Var.CorpId == "1973")
                {
                     sql = string.Format(@"SELECT a.IncidentID, isnull(convert(VARCHAR(30), a.IncidentDate, 120), '') AS IncidentDate,a.IncidentContent,
                                              CASE
                                                WHEN a.IsDelete=1 THEN '删除'
                                                WHEN (a.DealState=1 AND isnull(a.IsDelete,0)=0) THEN '已回复'
                                                WHEN (a.DispType=1 AND isnull(a.IsDelete,0)=0 AND isnull(a.DealState,0)=0) THEN '已查看'
                                                ELSE '已提交'
                                                END AS State,
                                               (SELECT count(1) FROM Tb_HSPR_IncidentReply WHERE isnull(IsDelete,0)=0 AND IncidentID=a.IncidentID AND ReplyWay='客户线上评价') AS HasEvaluate 
                                              FROM Tb_HSPR_IncidentAccept a WHERE a.IsStatistics=1 AND a.IncidentMode='App爱我家园' AND a.CommID={0}
                                                AND a.RoomID IN(SELECT convert(NVARCHAR(30), colName) FROM dbo.funSplitTabel('{1}',','))  AND ISNULL(a.IsDelete,0) = 0",
                                      Community.CommID, value.ToString());
                }

                DataSet ds = GetList(out pageCount, out count, sql, page, size, "IncidentDate", 1, "IncidentID", PubConstant.hmWyglConnectionString);
                result = new ApiResult(true, ds.Tables[0]).toJson();
                return result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            }
        }


    }
}
