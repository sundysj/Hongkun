using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class HK_CommunityNotification : PubInfo
    {
        public HK_CommunityNotification()
        {
            base.Token = "20190704HK_CommunityNotification";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "GetCommunityNotice":
                    Trans.Result = GetCommunityNotice(Row);
                    break;
                case "GetCommunityActivity":
                    Trans.Result = GetCommunityActivity(Row);
                    break;
                case "ActivityApply":
                    Trans.Result = ActivityApply(Row);
                    break;
                case "MakeScore":
                    Trans.Result = MakeScore(Row);
                    break;
                case "GetApplyInfo":
                    Trans.Result = GetApplyInfo(Row);
                    break;
                case "CancelApply":
                    Trans.Result = CancelApply(Row);
                    break;
                default:
                    break;
            }
        }

        private string GetCommunityNotice(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            string CommID = row["CommID"].ToString();

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

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT TOP 5 * FROM
                            (
                                SELECT convert(NVARCHAR(36),InfoID) AS InfoID,Heading,IssueDate,ImageUrl,1 AS Type,0 AS IsEnd,
                                NULL AS StartDate,NULL AS EndDate 
                                FROM Tb_HSPR_CommunityInfo 
                                WHERE CommID=@CommID AND InfoType IN('qqts','sqwh','dtzx') AND isnull(ShowEndDate,'9999')>getdate() AND isnull(IsDelete,0)=0
                                UNION ALL
                                SELECT convert(NVARCHAR(36),ActivitiesId) AS InfoID,ActivitiesTheme AS Heading,
                                       CreateTime AS IssueDate,ActivitiesImages AS ImageUrl,2 AS Type,
                                       CASE WHEN ActivitiesEndDate>getdate() THEN 0 ELSE 1 END AS IsEnd,
                                        ActivitiesStartDate AS StartDate, ActivitiesEndDate AS EndDate 
                                FROM Tb_HSPR_CommActivities_New
                                WHERE CommID=@CommID AND isnull(IsDelete,0)=0
                            ) AS a ORDER BY a.IssueDate DESC;";

                return new ApiResult(true, conn.Query(sql, new { CommID = CommID })).toJson();
            }
        }

        private string GetCommunityActivity(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            int pageSize = 10;
            int pageIndex = 1;
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            string CommID = row["CommID"].ToString();

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

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = $@"SELECT convert(NVARCHAR(36),ActivitiesId) AS InfoID,ActivitiesTheme AS Heading,
                                    CreateTime AS IssueDate,ActivitiesImages AS ImageUrl,2 AS Type,
                                    CASE WHEN ActivitiesEndDate>getdate() THEN 0 ELSE 1 END AS IsEnd,
                                    ActivitiesStartDate AS StartDate, ActivitiesEndDate AS EndDate 
                            FROM Tb_HSPR_CommActivities_New
                            WHERE CommID={Community.CommID} AND isnull(IsDelete,0)=0 ";

                var result = GetListDapper(out int pageCount, out int count, sql, pageIndex, pageSize, "IssueDate", 1, "InfoID", conn);

                return new ApiResult(true, result).toJson();
            }
        }

        private string ActivityApply(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户id不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }
            if (!row.Table.Columns.Contains("InfoID") || string.IsNullOrEmpty(row["InfoID"].ToString()))
            {
                return JSONHelper.FromString(false, "活动id不能为空");
            }
            if (!row.Table.Columns.Contains("ApplyUser") || string.IsNullOrEmpty(row["ApplyUser"].ToString()))
            {
                return JSONHelper.FromString(false, "报名人不能为空");
            }
            if (!row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(row["Mobile"].ToString()))
            {
                return JSONHelper.FromString(false, "报名人不能为空");
            }
            if (!row.Table.Columns.Contains("ApplyPerson") || string.IsNullOrEmpty(row["ApplyPerson"].ToString()))
            {
                return JSONHelper.FromString(false, "报名人数不能为空");
            }

            var UserID = row["UserID"].ToString();
            var CommID = AppGlobal.StrToInt(row["CommID"].ToString());
            var RoomID = AppGlobal.StrToLong(row["RoomID"].ToString());
            var InfoID = row["InfoID"].ToString();
            var ApplyUser = row["ApplyUser"].ToString();
            var Mobile = row["Mobile"].ToString();
            var ApplyPerson = AppGlobal.StrToInt(row["ApplyPerson"].ToString());

            var IID = "";
            if (row.Table.Columns.Contains("IID") && !string.IsNullOrEmpty(row["IID"].ToString()))
            {
                IID = row["IID"].ToString();
            }

            //查询小区
            Tb_Community Community = GetCommunityByCommID(row["CommID"].ToString());
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            Global_Var.CorpId = Community.CorpID.ToString();
            Global_Var.CorpID = Community.CorpID.ToString();
            Global_Var.LoginCorpID = Community.CorpID.ToString();
            PubConstant.hmWyglConnectionString = GetConnectionStr(Community);

            using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT Mobile FROM Tb_User WHERE Id=@UserId";
                var usermobile = appConn.Query<string>(sql, new { UserId = UserID }).FirstOrDefault();

                using (var erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    erpConn.Open();
                    var trans = erpConn.BeginTransaction();

                    try
                    {
                        // 判断是否是业主
                        sql = @"SELECT CustID FROM Tb_HSPR_Customer
                                WHERE CustID=(SELECT TOP 1 CustID FROM Tb_HSPR_CustomerLive WHERE RoomID=@RoomID AND IsDelLive=0 AND LiveType=1)
                                AND(MobilePhone LIKE @Mobile OR LinkmanTel LIKE @Mobile)
                                UNION ALL
                                SELECT CustID FROM Tb_HSPR_Household WHERE RoomID=@RoomID AND isnull(IsDelete,0)=0";
                        var custId = erpConn.Query<long>(sql, new { RoomID = RoomID, Mobile = $"%{usermobile}%" }, trans).FirstOrDefault();
                        if (custId == 0)
                        {
                            trans.Rollback();
                            return JSONHelper.FromString(false, "抱歉，仅业主或家属才能报名");
                        }

                        // 判断是否超出报名限制
                        sql = @"SELECT EveryLimit FROM Tb_HSPR_CommActivities_New WHERE ActivitiesId=@ActivitieId";
                        var everyLimit = erpConn.Query<int>(sql, new { ActivitieId = InfoID }, trans).FirstOrDefault();
                        if (ApplyPerson > everyLimit && everyLimit != 0)
                        {
                            trans.Rollback();
                            return JSONHelper.FromString(false, $"每户最多 {everyLimit} 人参加哦~");
                        }

                        // 判断是否满员
                        sql = @"SELECT isnull(ActivitiesLimit,0) AS ActivitiesLimit,
                               (SELECT isnull(sum(isnull(SignUpCount,0)),0) FROM Tb_HSPR_CommActivitiesPerson_New 
                                    WHERE ActivitiesId=x.ActivitiesId AND RoomId<>@RoomID AND isnull(IsDelete,0)=0) AS ApplyCount
                            FROM Tb_HSPR_CommActivities_New x WHERE ActivitiesId=@ActivitieId";
                        var tmpResult = erpConn.Query(sql, new { ActivitieId = InfoID, RoomID = RoomID }, trans).FirstOrDefault();
                        var totalLimit = (int)tmpResult.ActivitiesLimit;
                        var applyCount = (int)tmpResult.ApplyCount;

                        if (totalLimit < applyCount + ApplyPerson)
                        {
                            trans.Rollback();
                            if (totalLimit - applyCount == 0)
                            {
                                return JSONHelper.FromString(false, $"已经满员了哦~");
                            }
                            return JSONHelper.FromString(false, $"快满员了，最多还允许 {totalLimit - applyCount} 人参加哦~");
                        }

                        // 判断是否已报名
                        sql = @"SELECT SignUpCount FROM Tb_HSPR_CommActivitiesPerson_New
                                WHERE CustId=@CustID AND RoomId=@RoomID AND ActivitiesId=@ActivitieId AND isnull(IsDelete,0)=0";
                        var signUpCount = erpConn.Query<int>(sql, new { CustID = custId, RoomID = RoomID, ActivitieId = InfoID }, trans).FirstOrDefault();
                        if (signUpCount > 0)
                        {
                            if (string.IsNullOrEmpty(IID))
                            {
                                trans.Rollback();
                                return JSONHelper.FromString(false, "您或您的家属已经报名了~");
                            }
                            else
                            {
                                sql = @"UPDATE Tb_HSPR_CommActivitiesPerson_New SET SignUpCount=@ApplyPerson,JoinCount=@ApplyPerson
                                        WHERE ActivitiesPersonId=@IID";
                                erpConn.Execute(sql, new { ApplyPerson = ApplyPerson, IID = IID }, trans);
                                trans.Commit();
                                return JSONHelper.FromString(true, "修改报名成功");
                            }
                        }

                        // 报名
                        sql = @"INSERT INTO Tb_HSPR_CommActivitiesPerson_New(ActivitiesPersonId,CommId,ActivitiesId,CustId,CustName,RoomId,
                                    RoomSign,LinkPhone,SignUpCount,JoinCount,Score,RegisTime,IsDelete)
                                SELECT newid(),a.CommId,a.ActivitiesId,
                                    @CustId,(SELECT CustName FROM Tb_HSPR_Customer WHERE CustID=@CustId),
                                    @RoomId,(SELECT RoomSign FROM Tb_HSPR_Room WHERE RoomID=@RoomId),
                                    @Mobile,@ApplyPerson,@ApplyPerson,0,getdate(),0
                                FROM Tb_HSPR_CommActivities_New a
                                WHERE a.ActivitiesId=@ActivitieId";
                        int i = erpConn.Execute(sql, new
                        {
                            CustId = custId,
                            RoomId = RoomID,
                            ApplyPerson = ApplyPerson,
                            Mobile = Mobile,
                            ActivitieId = InfoID
                        }, trans);

                        if (i == 1)
                        {
                            trans.Commit();
                            return JSONHelper.FromString(true, "报名成功");
                        }

                        trans.Rollback();
                        return JSONHelper.FromString(false, "报名失败");
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return JSONHelper.FromString(false, "报名失败:" + ex.Message + ex.StackTrace);
                    }
                }
            }


        }

        private string MakeScore(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户id不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }
            if (!row.Table.Columns.Contains("InfoID") || string.IsNullOrEmpty(row["InfoID"].ToString()))
            {
                return JSONHelper.FromString(false, "活动id不能为空");
            }
            if (!row.Table.Columns.Contains("Score") || string.IsNullOrEmpty(row["Score"].ToString()))
            {
                return JSONHelper.FromString(false, "分数不能为空");
            }

            string UserID = row["UserID"].ToString();
            int CommID = AppGlobal.StrToInt(row["CommID"].ToString());
            long RoomID = AppGlobal.StrToLong(row["RoomID"].ToString());
            string InfoID = row["InfoID"].ToString();
            int Score = AppGlobal.StrToInt(row["Score"].ToString());

            //查询小区
            Tb_Community Community = GetCommunityByCommID(row["CommID"].ToString());
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            Global_Var.CorpId = Community.CorpID.ToString();
            Global_Var.CorpID = Community.CorpID.ToString();
            Global_Var.LoginCorpID = Community.CorpID.ToString();
            PubConstant.hmWyglConnectionString = GetConnectionStr(Community);

            using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT Mobile FROM Tb_User WHERE Id=@UserId";
                var usermobile = appConn.Query<string>(sql, new { UserId = UserID }).FirstOrDefault();

                using (var erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    erpConn.Open();
                    var trans = erpConn.BeginTransaction();

                    try
                    {
                        // 判断是否是业主
                        sql = @"SELECT CustID FROM Tb_HSPR_Customer
                                WHERE CustID=(SELECT TOP 1 CustID FROM Tb_HSPR_CustomerLive WHERE RoomID=@RoomID AND IsDelLive=0 AND LiveType=1)
                                AND(MobilePhone LIKE @Mobile OR LinkmanTel LIKE @Mobile)
                                UNION ALL
                                SELECT CustID FROM Tb_HSPR_Household WHERE RoomID=@RoomID AND isnull(IsDelete,0)=0";
                        var custId = erpConn.Query<long>(sql, new { RoomID = RoomID, Mobile = $"%{usermobile}%" }, trans).FirstOrDefault();
                        if (custId == 0)
                        {
                            trans.Rollback();
                            return JSONHelper.FromString(false, "抱歉，仅业主或家属才能评分");
                        }

                        // 判断是否已报名
                        sql = @"SELECT convert(nvarchar(36),ActivitiesPersonId) FROM Tb_HSPR_CommActivitiesPerson_New
                                WHERE RoomId=@RoomID AND ActivitiesId=@ActivitieId";

                        var iid = erpConn.Query<string>(sql, new { RoomID = RoomID, ActivitieId = InfoID }, trans).FirstOrDefault();
                        if (string.IsNullOrEmpty(iid))
                        {
                            trans.Rollback();
                            return JSONHelper.FromString(false, "抱歉，您未参加该活动，无法评分~");
                        }

                        sql = @"SELECT Score FROM Tb_HSPR_CommActivitiesPerson_New WHERE ActivitiesPersonId=@IID";
                        var score = erpConn.Query<int>(sql, new { IID = iid }, trans).FirstOrDefault();
                        if (score > 0)
                        {
                            trans.Rollback();
                            return JSONHelper.FromString(false, "您或您的家属已经评过分啦~");
                        }

                        sql = @"UPDATE Tb_HSPR_CommActivitiesPerson_New SET Score=@Score WHERE ActivitiesPersonId=@IID";
                        var i = erpConn.Execute(sql, new { IID = iid, Score = Score }, trans);
                        if (i == 1)
                        {
                            trans.Commit();
                            return JSONHelper.FromString(true, "评分成功");
                        }

                        trans.Rollback();
                        return JSONHelper.FromString(false, "评分失败");
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return JSONHelper.FromString(false, "评分失败:" + ex.Message + ex.StackTrace);
                    }
                }
            }
        }

        private string GetApplyInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户id不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }
            if (!row.Table.Columns.Contains("InfoID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }

            string UserID = row["UserID"].ToString();
            int CommID = AppGlobal.StrToInt(row["CommID"].ToString());
            long RoomID = AppGlobal.StrToLong(row["RoomID"].ToString());
            string InfoID = row["InfoID"].ToString();

            //查询小区
            Tb_Community Community = GetCommunityByCommID(row["CommID"].ToString());
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            Global_Var.CorpId = Community.CorpID.ToString();
            Global_Var.CorpID = Community.CorpID.ToString();
            Global_Var.LoginCorpID = Community.CorpID.ToString();
            PubConstant.hmWyglConnectionString = GetConnectionStr(Community);

            using (var erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT convert(nvarchar(36),a.ActivitiesPersonId) AS IID,a.ActivitiesId AS InfoID,a.CustName,
                                b.ActivitiesStartDate AS StartDate,b.ActivitiesEndDate AS EndDate,
                                   a.RoomId,a.RoomSign,a.LinkPhone,a.SignUpCount,a.Score
                            FROM Tb_HSPR_CommActivitiesPerson_New a
                            LEFT JOIN Tb_HSPR_CommActivities_New b ON a.ActivitiesId=b.ActivitiesID
                            WHERE a.ActivitiesId=@ActivitiesId AND a.RoomId=@RoomId AND isnull(a.IsDelete,0)=0";

                var applyInfo = erpConn.Query(sql, new { ActivitiesId = InfoID, RoomId = RoomID }).FirstOrDefault();
                return new ApiResult(true, applyInfo).toJson();
            }
        }

        private string CancelApply(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户id不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }
            if (!row.Table.Columns.Contains("InfoID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }

            string UserID = row["UserID"].ToString();
            int CommID = AppGlobal.StrToInt(row["CommID"].ToString());
            long RoomID = AppGlobal.StrToLong(row["RoomID"].ToString());
            string InfoID = row["InfoID"].ToString();

            //查询小区
            Tb_Community Community = GetCommunityByCommID(row["CommID"].ToString());
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            Global_Var.CorpId = Community.CorpID.ToString();
            Global_Var.CorpID = Community.CorpID.ToString();
            Global_Var.LoginCorpID = Community.CorpID.ToString();
            PubConstant.hmWyglConnectionString = GetConnectionStr(Community);

            using (var erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"UPDATE Tb_HSPR_CommActivitiesPerson_New SET IsDelete=1,DelTime=getdate()  
                            WHERE ActivitiesId=@ActivitiesId AND RoomId=@RoomId AND isnull(IsDelete,0)=0";

                var i = erpConn.Execute(sql, new { ActivitiesId = InfoID, RoomId = RoomID });
                return new ApiResult(true, "取消报名成功").toJson();
            }
        }
    }
}
