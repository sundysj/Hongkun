using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static Dapper.SqlMapper;

namespace Business
{
    public class BBSManager : PubInfo
    {
        public BBSManager() //获取小区、项目信息
        {
            base.Token = "20180316BBSManager";
        }
        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = new ApiResult(false, "接口不存在").toJson();
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            switch (Trans.Command)
            {
                case "GetList_th":                      // 泰禾，获取跳蚤信息
                    Trans.Result = GetList_th(Row);
                    break;
                case "SubmitSecondHandInfo_th":         // 泰禾，发布跳蚤信息
                    Trans.Result = SubmitSecondHandInfo_th(Row);
                    break;
                case "DeleteInfo":                      // 删除已发布的跳蚤信息
                    Trans.Result = DeleteInfo(Row);
                    break;
                case "SubmitComment":                   // 提交评论
                    Trans.Result = SubmitComment(Row);
                    break;
                case "GetCommentList":                  // 获取评论列表
                    Trans.Result = GetCommentList(Row);
                    break;
                case "DeleteComment":                   // 删除评论
                    Trans.Result = DeleteComment(Row);
                    break;
                case "AgreeComment":
                    Trans.Result = AgreeComment(Row);
                    break;
            }
        }

        /// <summary>
        /// 获取跳蚤信息
        /// </summary>
        private string GetList_th(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId"))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }

            if (!row.Table.Columns.Contains("PageSize"))
            {
                return new ApiResult(false, "缺少参数PageSize").toJson();
            }

            if (!row.Table.Columns.Contains("PageIndex"))
            {
                return new ApiResult(false, "缺少参数PageIndex").toJson();
            }

            string Mobile = null;
            if (row.Table.Columns.Contains("Mobile") && !string.IsNullOrEmpty(row["Mobile"].ToString()))
            {
                Mobile = row["Mobile"].ToString();
            }
            string communityId = row["CommunityId"].ToString();

            int pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            int pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());

            // 获取数据库连接字符串
            Tb_Community tb_Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "该小区不存在").toJson();
            }
            string connStr = GetConnectionStringStr(tb_Community);

            string sql = @"SELECT ActivitiesID,isnull(ActivitiesTypeName,ActivitiesType) AS ActivitiesTypeName,ActivitiesContent,
                                  ActivitiesTheme,ActivitiesImages,isnull(IssueDate, ActivitiesStartDate) AS IssueDate,LinkPhone
                                FROM VIEW_HSPR_CommActivities_Filter WHERE ISNULL(isRun,0)=1 AND isnull(IsDelete,0)=0 AND ActivitiesType='0001'";
            if (!string.IsNullOrEmpty(Mobile))
            {
                sql += " AND LinkPhone='" + Mobile + "'";
            }
            DataTable dataTable = GetList(out int pageCount, out int counts, sql, pageIndex, pageSize, "IssueDate", 1, "ActivitiesID", connStr).Tables[0];

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                dataTable.Columns.Add(new DataColumn() { ColumnName = "EvaCount", DataType = typeof(int) });
                dataTable.Columns.Add(new DataColumn() { ColumnName = "UserName", DataType = typeof(string) });
                dataTable.Columns.Add(new DataColumn() { ColumnName = "UserPic", DataType = typeof(string) });

                foreach (DataRow item in dataTable.Rows)
                {
                    GridReader reader = conn.QueryMultiple(@"SELECT Count(*) FROM Tb_CommunityEvaluation WHERE InfoId=@InfoId AND isnull(Shielding,'否')='否';
                                    SELECT isnull(NickName,isnull(Name, '匿名')) AS UserName, UserPic FROM Tb_User WHERE Mobile=@Mobile;",
                                        new { InfoId = item["ActivitiesID"], Mobile = item["LinkPhone"] });

                    item["EvaCount"] = reader.Read<int>().FirstOrDefault();
                    dynamic userInfo = reader.Read<dynamic>().FirstOrDefault();
                    item["UserName"] = userInfo.UserName;
                    item["UserPic"] = userInfo.UserPic;
                }
            }

            string result = JSONHelper.FromString(dataTable);
            result = result.Replace(Environment.NewLine, "\r\n").Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            return result;
        }

        /// <summary>
        /// 发布跳蚤信息
        /// </summary>
        private string SubmitSecondHandInfo_th(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId"))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }
            if (!row.Table.Columns.Contains("Title") || string.IsNullOrEmpty(row["Title"].ToString()))
            {
                return new ApiResult(false, "缺少参数Title").toJson();
            }
            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
            {
                return new ApiResult(false, "缺少参数Content").toJson();
            }
            if (!row.Table.Columns.Contains("Images"))
            {
                return new ApiResult(false, "缺少参数Images").toJson();
            }
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return new ApiResult(false, "缺少参数UserID").toJson();
            }
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return new ApiResult(false, "缺少参数UserID").toJson();
            }

            string communityId = row["CommunityId"].ToString();
            string Title = row["Title"].ToString().Replace(Environment.NewLine, "\r\n");
            string Content = row["Content"].ToString().Replace(Environment.NewLine, "\r\n");
            string Images = row["Images"].ToString();
            string UserID = row["UserID"].ToString();
            string InfoID = null;

            if (row.Table.Columns.Contains("InfoID") && !string.IsNullOrEmpty(row["InfoID"].ToString()))
            {
                InfoID = row["InfoID"].ToString();
            }

            // 获取数据库连接字符串
            Tb_Community tb_Community = GetCommunity(communityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "该小区不存在").toJson();
            }
            string connStr = GetConnectionStringStr(tb_Community);

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                string sql = @"SELECT isnull(NickName,isnull(Name,'匿名')) AS Name,Mobile FROM Tb_User WHERE Id=@UserId";

                dynamic userInfo = conn.Query(sql, new { UserId = UserID }).FirstOrDefault();
                if (userInfo == null || string.IsNullOrEmpty(userInfo.Mobile.ToString()))
                {
                    return new ApiResult(false, "用户不存在").toJson();
                }

                string userName = userInfo.Name.ToString();
                string mobile = userInfo.Mobile.ToString();

                using (IDbConnection conn2 = new SqlConnection(connStr))
                {
                    if (string.IsNullOrEmpty(InfoID))
                    {
                        conn2.Execute(@"INSERT INTO Tb_HSPR_CommActivities(ActivitiesID,CommID,ActivitiesType,ActivitiesTheme,ActivitiesContent,
                                        CustName,LinkPhone,ActivitiesImages,ActivitiesStartDate,ActivitiesEndDate,IssueDate,IsDelete,isRun)
                                   VALUES(newid(),@CommID,'0001',@Title, @Content, @CustName, @LinkPhone,@Images,getdate(),
                                    convert(DATETIME,'2099-01-01 23:59:59'),getdate(),0,0)",
                                    new
                                    {
                                        CommID = tb_Community.CommID,
                                        Title = Title,
                                        Content = Content,
                                        CustName = userName,
                                        LinkPhone = mobile,
                                        Images = Images
                                    });
                        return JSONHelper.FromString(true, "发布成功");
                    }
                    else
                    {
                        conn2.Execute(@"UPDATE Tb_HSPR_CommActivities SET ActivitiesTheme=@Title,ActivitiesContent=@Content,
                                            ActivitiesImages=@Images,IssueDate=getdate()
                                        WHERE ActivitiesID=@InfoID",
                                    new
                                    {
                                        Title = Title,
                                        Content = Content,
                                        Images = Images,
                                        InfoID = InfoID
                                    });
                        return JSONHelper.FromString(true, "修改成功");
                    }
                }
            }
        }

        private string DeleteInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId"))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }
            if (!row.Table.Columns.Contains("InfoId"))
            {
                return new ApiResult(false, "缺少参数InfoId").toJson();
            }
            if (!row.Table.Columns.Contains("UserId"))
            {
                return new ApiResult(false, "缺少参数UserId").toJson();
            }


            string communityId = row["CommunityId"].ToString();
            string InfoId = row["InfoId"].ToString();
            string UserId = row["UserId"].ToString();

            // 获取数据库连接字符串
            Tb_Community tb_Community = GetCommunity(communityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "该小区不存在").toJson();
            }
            string connStr = GetConnectionStringStr(tb_Community);

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                // 需要验证手机号是否一致
                string mobile = conn.Query<string>(@"SELECT Mobile FROM Tb_User WHERE Id=@UserID", new { UserID = UserId }).FirstOrDefault();

                if (!string.IsNullOrEmpty(mobile))
                {
                    using (IDbConnection conn2 = new SqlConnection(connStr))
                    {
                        if (conn2.Execute(@"UPDATE Tb_HSPR_CommActivities SET IsDelete=1 WHERE ActivitiesID=@ID AND LinkPhone=@Mobile",
                            new { ID = InfoId, Mobile = mobile }) > 0)
                        {
                            return JSONHelper.FromString(true, "删除成功");
                        }
                    }
                }
                return JSONHelper.FromString(false, "删除失败");
            }
        }

        private string AgreeComment(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId"))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }
            if (!row.Table.Columns.Contains("EvaluationId"))
            {
                return new ApiResult(false, "缺少参数EvaluationId").toJson();
            }
            if (!row.Table.Columns.Contains("UserId"))
            {
                return new ApiResult(false, "缺少参数UserId").toJson();
            }
            if (!row.Table.Columns.Contains("Agree"))
            {
                return new ApiResult(false, "缺少参数Agree").toJson();
            }

            string communityId = row["CommunityId"].ToString();
            string EvaluationId = row["EvaluationId"].ToString();
            string UserId = row["UserId"].ToString();
            int Agree = AppGlobal.StrToInt(row["Agree"].ToString());
            Tb_Community tb_Community = GetCommunity(communityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "该小区不存在").toJson();
            }
            string connStr = GetConnectionStringStr(tb_Community);

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                conn.Execute("Proc_CommuityEvaluation_AgreeOppose_Insert",
                    new { ID = "", UserId = UserId, EvaluationId = EvaluationId, IsAgree = Agree }, null, null, CommandType.StoredProcedure);

                return JSONHelper.FromString(true, "操作成功");
            }
        }

        /// <summary>
        /// 屏蔽/删除评论
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string DeleteComment(DataRow row)
        {
            if (!row.Table.Columns.Contains("EvaluationId"))
            {
                return new ApiResult(false, "缺少参数EvaluationId").toJson();
            }
            string EvaluationId = row["EvaluationId"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                conn.Execute("UPDATE Tb_CommunityEvaluation SET Shielding='是' WHERE ID=@ID", new { ID = EvaluationId });

                return JSONHelper.FromString(true, "删除成功");
            }
        }

        /// <summary>
        /// 通过InfoId获取评论列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetCommentList(DataRow row)
        {
            #region 基础数据校验
            if (!row.Table.Columns.Contains("CommunityId"))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }

            if (!row.Table.Columns.Contains("InfoId"))
            {
                return new ApiResult(false, "缺少参数InfoId").toJson();
            }

            if (!row.Table.Columns.Contains("UserId"))
            {
                return new ApiResult(false, "缺少参数UserId").toJson();
            }

            if (!row.Table.Columns.Contains("PageSize"))
            {
                return new ApiResult(false, "缺少参数PageSize").toJson();
            }

            if (!row.Table.Columns.Contains("PageIndex"))
            {
                return new ApiResult(false, "缺少参数PageIndex").toJson();
            }

            string communityId = row["CommunityId"].ToString();
            string InfoId = row["InfoId"].ToString();
            string UserId = row["UserId"].ToString();
            int pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            int pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            #endregion


            #region 检验小区是否存在并获取连接字符串
            Tb_Community tb_Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "该小区不存在").toJson();
            }
            #endregion

            string sql = @"SELECT a.ID AS EvaluationID,a.ReviewContent AS EvaluationContent,a.EvaluationTime,
                            isnull(b.NickName,'匿名') AS UserName,b.UserPic,a.UserId,
                            isnull((SELECT TOP 1 IsAgree FROM Tb_CommunityEvaluation_AgreeOppose x WHERE x.UserId='" + UserId +
                                @"' AND x.EvaluationId=a.ID), 0) AS IsAgree,
                                  (SELECT COUNT(1) FROM Tb_CommunityEvaluation_AgreeOppose x WHERE x.EvaluationId=a.ID) AS AgreeCount
                                FROM Tb_CommunityEvaluation a LEFT JOIN Tb_User b ON a.UserId=b.Id
                                WHERE isnull(a.Shielding,'否')='否' AND a.InfoId='" + InfoId + "'";

            DataTable dataTable = GetList(out int pageCount, out int counts, sql, pageIndex, pageSize, "EvaluationTime", 0, "EvaluationID", PubConstant.UnifiedContionString).Tables[0];

            string result = JSONHelper.FromString(dataTable);
            result = result.Replace(Environment.NewLine, "\r\n").Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            return result;
        }
        /// <summary>
        /// 提交评论
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string SubmitComment(DataRow row)
        {
            #region 基础数据校验
            if (!row.Table.Columns.Contains("CommunityId"))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }
            if (!row.Table.Columns.Contains("UserId"))
            {
                return new ApiResult(false, "缺少参数UserId").toJson();
            }
            if (!row.Table.Columns.Contains("InfoId"))
            {
                return new ApiResult(false, "缺少参数InfoId").toJson();
            }
            if (!row.Table.Columns.Contains("Identification"))
            {
                return new ApiResult(false, "缺少参数Identification").toJson();
            }
            int Identification;
            if (!int.TryParse(row["Identification"].ToString(), out Identification))
            {
                return new ApiResult(false, "参数Identification有误").toJson();
            }
            if (!row.Table.Columns.Contains("ReviewContent") || string.IsNullOrEmpty(row["ReviewContent"].ToString()))
            {
                return new ApiResult(false, "内容不能为空").toJson();
            }

            string communityId = row["CommunityId"].ToString();
            string UserId = row["UserId"].ToString();
            string InfoId = row["InfoId"].ToString();
            string ReviewContent = row["ReviewContent"].ToString();
            #endregion

            #region 检验用户是否存在
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                DataSet ds = conn.ExecuteReader("SELECT * FROM Tb_User WHERE Id = @Id", new { Id = UserId }, null, null, CommandType.Text).ToDataSet();
                if (null == ds || ds.Tables.Count == 0)
                {
                    return new ApiResult(false, "该用户不存在").toJson();
                }
                DataTable dt = ds.Tables[0];
                if (null == dt || dt.Rows.Count == 0)
                {
                    return new ApiResult(false, "该用户不存在").toJson();
                }
                DataRow dr = dt.Rows[0];
                UserId = dr["Id"].ToString();
            }
            #endregion

            #region 检验小区是否存在并获取连接字符串
            Tb_Community tb_Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            if (null == tb_Community)

            {
                return new ApiResult(false, "该小区不存在").toJson();
            }
            string connStr = GetConnectionStringStr(tb_Community);
            #endregion

            //数据库获取到的新闻标题
            string ReviewTheme = "";

            #region 检验新闻信息在ERP是否存在
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                string sqlStr;
                if (Identification == 1)    // 1、社区活动，2、社区资讯
                {
                    sqlStr = "SELECT ActivitiesID AS InfoID,ActivitiesTheme AS Title FROM Tb_HSPR_CommActivities WHERE ActivitiesID=@InfoId";
                }
                else
                {
                    sqlStr = "SELECT InfoID,Heading AS Title FROM Tb_HSPR_CommunityInfo WHERE InfoID = @InfoId ";
                }
                DataSet ds = conn.ExecuteReader(sqlStr, new { InfoId = InfoId }, null, null, CommandType.Text).ToDataSet();

                if (null == ds || ds.Tables.Count == 0)
                {
                    return new ApiResult(false, "该新闻不存在").toJson();
                }
                DataTable dt = ds.Tables[0];
                if (null == dt || dt.Rows.Count == 0)
                {
                    return new ApiResult(false, "该新闻不存在").toJson();
                }
                DataRow dr = dt.Rows[0];

                InfoId = dr["InfoID"].ToString();
                ReviewTheme = dr["Title"].ToString();
            }
            #endregion

            int result = 0;
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ID", Guid.NewGuid().ToString());
                parameters.Add("UserId", UserId);
                parameters.Add("CommunityId", communityId);
                parameters.Add("InfoId", InfoId);
                parameters.Add("ReviewTheme", ReviewTheme);
                parameters.Add("ReviewContent", ReviewContent);
                parameters.Add("EvaluationTime", DateTime.Now.ToString());
                parameters.Add("Shielding", "否");
                parameters.Add("Identification", Identification);
                result = conn.Execute("INSERT INTO Tb_CommunityEvaluation(ID, UserId, CommunityId, InfoId, ReviewTheme, ReviewContent, EvaluationTime, Shielding, Identification) VALUES (@ID, @UserId, @CommunityId, @InfoId, @ReviewTheme, @ReviewContent, @EvaluationTime, @Shielding, @Identification)", parameters);
            }
            if (result == 0)
            {
                return new ApiResult(false, "提交失败,请重试").toJson();
            }
            return new ApiResult(true, "提交成功").toJson();
        }
    }
}
