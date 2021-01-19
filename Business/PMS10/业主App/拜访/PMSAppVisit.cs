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
using System.Linq;
using static Dapper.SqlMapper;

namespace Business
{
    public class PMSAppVisit : PubInfo
    {
        public PMSAppVisit()
        {
            base.Token = "20191209PMSAppVisit";
        }

        public override void Operate(ref Transfer Trans)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            //防止未捕获异常出现
            try
            {
                switch (Trans.Command)
                {
                    case "GetQuestionnaires":
                        Trans.Result = GetQuestionnaires(Row);
                        break;
                    case "GetQuestionnaireDetail":
                        Trans.Result = GetQuestionnaireDetail(Row);
                        break;
                    case "SaveQuestion_v2":
                        Trans.Result = SaveQuestion_v2(Row);
                        break;
                    case "GetRelation":
                        Trans.Result = GetRelation(Row);
                        break;
                    default:
                        Trans.Result = new ApiResult(false, "未知错误").toJson();
                        break;
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace + Environment.NewLine + ex.Source);
                Trans.Result = new ApiResult(false, ex.Message + ex.StackTrace).toJson();
            }
        }

        private string GetQuestionnaires(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].AsString()))
            {
                return new ApiResult(false, "缺少参数UserId").toJson();
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }

            var userId = row["UserId"].AsString();
            var communityId = row["CommunityId"].AsString();

            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }

            using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT RoomID FROM Tb_User_Relation 
                            WHERE UserId=@UserId AND CommunityId=@CommunityId AND isnull(Locked,0)=0";

                var rooms = appConn.Query<long>(sql, new { UserId = userId, CommunityId = community.Id });
                if (rooms.Count() > 0)
                {
                    using (var erpConn = new SqlConnection(GetConnectionStr(community)))
                    {
                        /* 2020年4月22日16:05:54，谭阳说取消时间限制：
                        AND convert(varchar(10),b.PlanBeginTime,120)<=convert(varchar(10),getdate(),120)
                        AND convert(varchar(10),b.PlanEndTime,120)>= convert(varchar(10), getdate(), 120)
                        */
                        sql = $@"SELECT b.ID AS TaskID,a.RoomID,b.PlanName,b.PlanBeginTime AS BeginTime,b.PlanEndTime AS EndTime,
                                        CASE WHEN c.ID IS NOT NULL AND c.CompleteTime IS NOT NULL THEN '已登记' ELSE '' END AS State,
                                        d.VisitCategory,e.QuestionnaireCategory,a.PropertyUses,b.PropertyUses,a.RoomState,b.RoomState 
                                FROM Tb_HSPR_Room a
                                LEFT JOIN Tb_Visit_Plan b ON b.CommID=@CommID
                                LEFT JOIN Tb_Visit_VisitingCustomersDetail c ON b.ID=c.PlanID AND c.RoomID=a.RoomID
                                LEFT JOIN Tb_Visit_VisitCategory d ON b.VisitCategoryID=d.ID
                                LEFT JOIN Tb_Visit_QuestionnaireCategory e ON b.QuestionnaireCategoryID=e.ID
                                LEFT JOIN Tb_Dictionary_PropertyUses f ON a.PropertyUses=f.DictionaryName
                                WHERE a.RoomID IN
                                (
                                    { string.Join(",", rooms) }
                                )
                                AND isnull(b.IsDelete,0)=0 AND isnull(b.IsClose,0)=0
                                AND
                                (
	                                (charindex(convert(varchar(5),a.RoomState),isnull(b.RoomState,''))>0)
	                                OR
	                                ((SELECT isnull(stuff((SELECT ','+value FROM SplitString(isnull(b.RoomState,''),',',1) FOR XML PATH('')),1,1,''),''))='')
                                )
                                AND
                                (
	                                (charindex(isnull(f.DictionaryCode,''),convert(varchar(5),b.PropertyUses))>0)
	                                OR
	                                ((SELECT isnull(stuff((SELECT ','+value FROM SplitString(isnull(f.DictionaryCode,''),',',1) FOR XML PATH('')),1,1,''),''))='')
                                ) ORDER BY BeginTime DESC";


                        var data = erpConn.Query(sql, new { CommID = community.CommID });

                        return new ApiResult(true, data).toJson();
                    }
                }

                return JSONHelper.FromString(false, "暂无问卷调查");
            }
        }

        /// <summary>
        /// 获取问卷问题列表第二版，包含多选题
        /// </summary>
        private string GetQuestionnaireDetail(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }
            if (!row.Table.Columns.Contains("TaskId") || string.IsNullOrEmpty(row["TaskId"].ToString()))
            {
                return JSONHelper.FromString(false, "任务id不能为空");
            }
            if (!row.Table.Columns.Contains("RoomId") || string.IsNullOrEmpty(row["RoomId"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋id不能为空");
            }

            var communityId = row["CommunityId"].ToString();
            var planId = row["TaskId"].ToString();
            var roomId = AppGlobal.StrToLong(row["RoomId"].ToString());

            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }

            using (var conn = new SqlConnection(GetConnectionStr(community)))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var trans = conn.BeginTransaction();

                try
                {
                    var visitManage = new VisitManage_bl();

                    var sql = @"SELECT ID FROM Tb_Visit_VisitingCustomersDetail 
                                WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0";
                    var detailId = conn.Query<string>(sql, new { PlanID = planId, RoomID = roomId }, trans).FirstOrDefault();
                    if (detailId == null)
                    {
                        detailId = visitManage.AddVisitRecord_v2(planId, roomId, conn, trans);
                    }

                    sql = @"SELECT count(1) FROM Tb_Visit_VisitingCustomersQuestionnaire 
                            WHERE VisitingCustomersDetailID=@DetailId AND isnull(IsDelete,0)=0";
                    if (conn.Query<int>(sql, new { PlanID = planId, DetailId = detailId }, trans).FirstOrDefault() == 0)
                    {
                        visitManage.ImportQuestion_v2(planId, detailId, conn, trans);
                    }

                    trans.Commit();

                    sql = @"SELECT PlanObjective FROM Tb_Visit_Plan WHERE ID=@PlanID;

                            SELECT isnull(a.VisitWayID,a.AppointmentVisitWayID) AS VisitWayID,b.VisitWay,
                                IsUploadEnclosure,a.InterviewedObject,a.ContactTelephone,a.SignatureImg,
                                a.RelationsWithOwners AS Relation 
                            FROM Tb_Visit_VisitingCustomersDetail a
                            LEFT JOIN Tb_Visit_VisitWay b ON isnull(a.VisitWayID,a.AppointmentVisitWayID)=b.ID
                            WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(a.IsDelete,0)=0;

                            SELECT a.ID AS IID,d.VisitCategory,e.QuestionnaireCategory,a.IsScore,f.Name AS IssueType,a.IssueName,a.IssueResult,
                                    isnull(a.IssueStandardScore,0) AS IssueStandardScore,a.IssueProperty,a.Remark,a.Sort,
                                    isnull(a.RealScore,0) AS RealScore
                            FROM Tb_Visit_VisitingCustomersQuestionnaire a 
                            LEFT JOIN Tb_Visit_VisitingCustomersDetail b ON a.VisitingCustomersDetailID=b.ID
                            LEFT JOIN Tb_Visit_Plan c ON b.PlanID=c.ID
                            LEFT JOIN Tb_Visit_VisitCategory d ON a.VisitCategoryID=d.ID
                            LEFT JOIN Tb_Visit_QuestionnaireCategory e ON a.QuestionnaireCategoryID=e.ID
                            LEFT JOIN Tb_Visit_Dictionary f ON a.IssueType=f.ID
                            WHERE a.ID IN
                            (
                                SELECT ID FROM Tb_Visit_VisitingCustomersQuestionnaire 
                                WHERE VisitingCustomersDetailID IN
                                (
                                    SELECT ID FROM Tb_Visit_VisitingCustomersDetail WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0
                                ) AND isnull(IsDelete,0)=0
                            );

                            SELECT a.ID,a.QuestionID AS CustomerQuestionID,a.[Option] AS Content,a.OptionStandardScore AS Score,IsChoose 
                            FROM Tb_Visit_VisitingCustomersQuestionnaireOption a
                            LEFT JOIN Tb_Visit_VisitingCustomersQuestionnaire b ON a.QuestionID=b.ID
                            WHERE QuestionID IN
                            (
                                SELECT ID FROM Tb_Visit_VisitingCustomersQuestionnaire 
                                WHERE VisitingCustomersDetailID IN
                                (
                                    SELECT ID FROM Tb_Visit_VisitingCustomersDetail WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0
                                ) AND isnull(IsDelete,0)=0
                            ) ORDER BY b.Sort,a.Sort;";

                    var reader = conn.QueryMultiple(sql, new { PlanID = planId, RoomID = roomId });
                    var planObjective = reader.Read<string>().FirstOrDefault();
                    var visitInfo = reader.Read().FirstOrDefault();
                    var questions = reader.Read();
                    var questionOptions = reader.Read().ToList();

                    var group = new Dictionary<string, List<dynamic>>();
                    foreach (var questionInfo in questions)
                    {
                        var options = questionOptions.FindAll(obj => obj.CustomerQuestionID == questionInfo.IID);

                        questionInfo.Options = options.Select(obj => new { OptionID = obj.ID, Content = obj.Content, Score = obj.Score, IsChoose = obj.IsChoose });

                        foreach (var tmp in options)
                        {
                            questionOptions.Remove(tmp);
                        }
                    }

                    var visitWayId = visitInfo?.VisitWayID?.ToString();
                    var visitWay = visitInfo?.VisitWay?.ToString();
                    var isUploadEnclosure = visitInfo?.IsUploadEnclosure?.ToString();
                    var interviewedObject = visitInfo?.InterviewedObject?.ToString();
                    var interviewedMobile = visitInfo?.ContactTelephone?.ToString();
                    var relationsWithOwnersisit = visitInfo?.Relation?.ToString();
                    var signatureImg = visitInfo?.SignatureImg?.ToString();

                    return new ApiResult(true, new
                    {
                        DetailId = detailId,
                        PlanObjective = planObjective,
                        VisitWayID = visitWayId,
                        VisitWay = visitWay,
                        SignatureImg = signatureImg,
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
        private string SaveQuestion_v2(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }
            if (!row.Table.Columns.Contains("DetailID") || string.IsNullOrEmpty(row["DetailID"].ToString()))
            {
                return JSONHelper.FromString(false, "问题id不能为空");
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

            var communityId = row["CommunityId"].ToString();
            var detailId = row["DetailID"].ToString();
            var signatureImg = default(string);
            var interviewedObject = row["InterviewedObject"].ToString();
            var interviewedMobile = row["InterviewedMobile"].ToString();
            var relation = row["Relation"].ToString();

            if (row.Table.Columns.Contains("SignatureImg") && !string.IsNullOrEmpty(row["SignatureImg"].ToString()))
            {
                signatureImg = row["SignatureImg"].ToString();
            }

            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }

            using (var conn = new SqlConnection(GetConnectionStr(community)))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var trans = conn.BeginTransaction();

                try
                {
                    var sql = @"SELECT CompleteTime FROM Tb_Visit_VisitingCustomersDetail WHERE ID=@ID;";
                    var completeTime = conn.Query<string>(sql, new { ID = detailId }, trans).FirstOrDefault();
                    if (completeTime != null)
                    {
                        return new ApiResult(false, "此问卷已完成，不能再修改哦").toJson();
                    }

                    sql = @"DECLARE @VisitWayID varchar(36);
                            SELECT @VisitWayID=ID FROM Tb_Visit_VisitWay WHERE VisitWay LIKE '%业主App%';
                            UPDATE Tb_Visit_VisitingCustomersDetail 
                            SET InterviewedObject=@InterviewedObject,ContactTelephone=@InterviewedMobile,
                            VisitWayID=@VisitWayID,RelationsWithOwners=@Relation,SignatureImg=@SignatureImg  
                            WHERE ID=@DetailID";

                    conn.Execute(sql, new
                    {
                        SignatureImg = signatureImg,
                        InterviewedObject = interviewedObject,
                        InterviewedMobile = interviewedMobile,
                        Relation = relation,
                        DetailID = detailId
                    }, trans);

                    sql = @"SELECT PlanID FROM Tb_Visit_VisitingCustomersDetail WHERE ID=@DetailID";
                    var planId = conn.Query<string>(sql, new { DetailID = detailId }, trans).FirstOrDefault();

                    JArray questionArray = (JArray)JsonConvert.DeserializeObject(row["Data"].ToString());
                    // 问卷问题表
                    foreach (var question in questionArray)
                    {
                        var iid = question["IID"].ToString();
                        var result = question["Result"].ToString();

                        sql = @"SELECT count(1) FROM Tb_Visit_VisitingCustomersQuestionnaire 
                                WHERE ID=@IID AND IssueProperty IN('单选题','多选题','选择题')";
                        if (conn.Query<int>(sql, new { IID = iid }, trans).FirstOrDefault() > 0)
                        {
                            var options = result.Split(',').Select(obj => $"'{obj}'");

                            sql = $@"UPDATE Tb_Visit_VisitingCustomersQuestionnaireOption SET IsChoose=0 WHERE QuestionID=@IID;
                                     UPDATE Tb_Visit_VisitingCustomersQuestionnaireOption SET IsChoose=1 WHERE ID IN({string.Join(",", options)});
                                     DECLARE @RealScore decimal(10,2);
                                     SELECT @RealScore=max(OptionStandardScore) FROM Tb_Visit_VisitingCustomersQuestionnaireOption WHERE QuestionID=@IID;
                                     UPDATE Tb_Visit_VisitingCustomersQuestionnaire SET RealScore=@RealScore WHERE ID=@IID;";

                            conn.Execute(sql, new { IID = iid }, trans);
                        }

                        conn.Execute(@"UPDATE Tb_Visit_VisitingCustomersQuestionnaire SET IssueResult=@Result WHERE ID=@IID",
                                            new { Result = result, IID = iid }, trans);
                    }

                    // 更新计划状态
                    new VisitManage_bl().UpdatePlanState(planId, conn, trans);

                    sql = @"DECLARE @RoomID bigint;
                            SELECT @RoomID=RoomID FROM Tb_Visit_VisitingCustomersDetail WHERE Id=@DetailId;

                            UPDATE Tb_Visit_VisitingCustomersDetail SET CompleteTime=getdate()
                            WHERE PlanID=@PlanID AND RoomID=@RoomID AND isnull(IsDelete,0)=0;

                            UPDATE Tb_Visit_Plan SET VisitHouseholds=isnull(VisitHouseholds,0)+1 WHERE ID=@PlanID;";

                    conn.Execute(sql, new { DetailId = detailId, PlanID = planId }, trans);

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
        /// 获取与业主关系
        /// </summary>
        private string GetRelation(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            var communityId = row["CommunityId"].ToString();
            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }

            using (var conn = new SqlConnection(GetConnectionStr(community)))
            {
                var sql = @"SELECT DictionaryName FROM Tb_Dictionary_Relation ORDER BY DictionaryOrderId;";

                return new ApiResult(true, conn.Query(sql)).toJson();
            }
        }
    }
}
