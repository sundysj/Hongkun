using Business.PMS10.物管App.报事.Models;
using Common;
using Common.Extenions;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static Dapper.SqlMapper;

namespace Business
{
    public class PMSAppIncidentAcceptManage : PubInfo
    {
        public PMSAppIncidentAcceptManage()
        {
            base.Token = "20200320PMSAppIncidentAcceptManage";
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
                    case "GetIncidentRegional":
                        Trans.Result = GetIncidentRegional(Row);
                        break;
                    case "IncidentAccept":
                        Trans.Result = IncidentAccept(Row);
                        break;
                    case "IncidentEvaluate":
                        Trans.Result = IncidentEvaluate(Row);
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

        /// <summary>
        /// 获取公区区域
        /// </summary>
        private string GetIncidentRegional(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }

            var communityId = row["CommunityId"].AsString();

            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }

            using (var conn = new SqlConnection(GetConnectionStr(community)))
            {
                var sql = @"SELECT RegionalID,RegionalPlace 
                            FROM Tb_HSPR_IncidentRegional WHERE CommID=@CommID AND isnull(IsDelete,0)=0;";

                var data = conn.Query(sql, new { CommID = community.CommID });

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 报事受理
        /// </summary>
        private string IncidentAccept(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("IncidentPlace") || string.IsNullOrEmpty(row["IncidentPlace"].ToString()))
            {
                return JSONHelper.FromString(false, "报事区域不能为空");
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

            var communityId = row["CommunityId"].AsString();

            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }

            var incidentPlace = row["IncidentPlace"].ToString();
            var content = row["Content"].ToString();
            var custId = AppGlobal.StrToLong(row["CustID"].ToString());
            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());
            var regionalId = 0L;
            var phone = row["Phone"].ToString();
            var incidentImgs = "";
            var reserveDate = DateTime.Now.ToString();
            var incidentMan = row["IncidentMan"].ToString();
            // 1=户内报事，2=公区报事，3=通知开水，4=通知开电，5=人员放行，6=车辆放行
            var type = 0;
            var isTousu = 0;
            var appName = "业主App";

            if (row.Table.Columns.Contains("RegionalID") && !string.IsNullOrEmpty(row["RegionalID"].ToString()))
            {
                regionalId = AppGlobal.StrToLong(row["RegionalID"].ToString());
            }
            if (row.Table.Columns.Contains("IncidentImgs") && !string.IsNullOrEmpty(row["IncidentImgs"].ToString()))
            {
                incidentImgs = row["IncidentImgs"].ToString();
            }
            if (row.Table.Columns.Contains("ReserveDate") && !string.IsNullOrEmpty(row["ReserveDate"].ToString()))
            {
                reserveDate = row["ReserveDate"].ToString();
            }
            if (row.Table.Columns.Contains("IsTousu") && !string.IsNullOrEmpty(row["IsTousu"].ToString()))
            {
                isTousu = AppGlobal.StrToInt(row["IsTousu"].ToString());
            }
            if (row.Table.Columns.Contains("Type") && !string.IsNullOrEmpty(row["Type"].ToString()))
            {
                type = AppGlobal.StrToInt(row["Type"].ToString());
            }
            if (row.Table.Columns.Contains("AppName") && !string.IsNullOrEmpty(row["AppName"].ToString()))
            {
                appName = row["AppName"].ToString();
            }

            var bigCorpTypeID = 0L;
            var bigCorpTypeCode = default(string);
            var fineCorpTypeID = 0L;
            var fineCorpTypeCode = default(string);

            var incidentSource = "客户报事";
            var drClass = 1;

            PubConstant.hmWyglConnectionString = GetConnectionStr(community);

            // 新版报事控制
            var control = PMSIncidentAccept.GetIncidentControlSet();
            if (control.DefaultIndoorIncidentAcceptTypeID != 0 &&
                (incidentPlace == "户内" || incidentPlace == "户内报事" || incidentPlace == "业主权属"))
            {
                bigCorpTypeID = control.DefaultIndoorIncidentAcceptTypeID;
            }
            else if (control.DefaultPublicIncidentAcceptTypeID != 0 &&
                (incidentPlace == "公区" || incidentPlace == "公区报事" || incidentPlace == "公共区域"))
            {
                bigCorpTypeID = control.DefaultPublicIncidentAcceptTypeID;
            }

            // 1 = 户内报事，2 = 公区报事，3 = 通知开水，4 = 通知开电，5 = 人员放行，6 = 车辆放行
            switch (type)
            {
                case 3: drClass = 2; fineCorpTypeID = control.DefaultNotifyOpenWaterIncidentAcceptTypeID; break; // 通知开水
                case 4: drClass = 2; fineCorpTypeID = control.DefaultNotifyOpenPowerIncidentAcceptTypeID; break; // 通知开电
                case 5: drClass = 2; fineCorpTypeID = control.DefaultLetPersonPassIncidentAcceptTypeID; break;   // 人员放行
                case 6: drClass = 2; fineCorpTypeID = control.DefaultLetCarPassIncidentAcceptTypeID; break;      // 车辆放行
            }

            if (bigCorpTypeID == 0 && fineCorpTypeID == 0)
            {
                using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
                {
                    var sql = @"SELECT * FROM Tb_Control_AppIncidentAccept 
                                WHERE CommunityID IS NOT NULL AND CommunityID=@CommunityID AND IsEnable=1 AND IsDelete=0
                                UNION ALL
                                SELECT * FROM Tb_Control_AppIncidentAccept 
                                WHERE CommunityID IS NULL AND CorpID=@CorpID AND IsEnable=1 AND IsDelete=0";

                    try
                    {
                        var data = conn.Query(sql, new { CorpID = community.CorpID, CommunityID = community.Id }).FirstOrDefault();
                        if (data != null)
                        {
                            if (incidentPlace == "户内" || incidentPlace == "户内报事" || incidentPlace == "业主权属")
                                bigCorpTypeID = data.DefaultIndoorIncidentTypeID;
                            else
                                bigCorpTypeID = data.DefaultPublicIncidentTypeID;

                            incidentSource = data.DefaultIncidentSource;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();

                try
                {
                    var sql = @"SELECT IncidentID,IncidentNum FROM Tb_HSPR_IncidentAccept
                                WHERE CommID=@CommID AND RoomID=@RoomID AND RegionalID=@RegionalID AND IncidentContent=@IncidentContent
                                AND isnull(IsDelete,0)=0 AND isnull(IsClose,0)=0 AND convert(varchar(13),IncidentDate)=convert(varchar(13),getdate())";

                    var incidentInfo = conn.Query(sql, new
                    {
                        CommID = community.CommID,
                        RoomID = roomId,
                        RegionalID = regionalId,
                        IncidentContent = content
                    }, trans).FirstOrDefault();

                    if (incidentInfo != null)
                    {
                        trans.Commit();
                        return new ApiResult(false, "此报事已受理，请勿重复提交").toJson();
                    }

                    var dispLimit = 0.0m;
                    var dealLimit = 0.0m;

                    if (bigCorpTypeID != 0)
                    {
                        sql = @"SELECT TypeCode FROM Tb_HSPR_CorpIncidentType WHERE CorpTypeID=@BigCorpTypeID;
                                SELECT isnull(DispLimit,0) FROM Tb_HSPR_CorpIncidentType WHERE CorpTypeID=@BigCorpTypeID;
                                SELECT CASE WHEN @IncidentPlace='户内' THEN isnull(DealLimit,0) ELSE isnull(DealLimit2,0) END 
                                FROM Tb_HSPR_CorpIncidentType WHERE CorpTypeID=@BigCorpTypeID;";
                        var reader = conn.QueryMultiple(sql, new { BigCorpTypeID = bigCorpTypeID, IncidentPlace = incidentPlace }, trans);
                        bigCorpTypeCode = reader.Read<string>().FirstOrDefault();
                        dispLimit = reader.Read<decimal>().FirstOrDefault();
                        dealLimit = reader.Read<decimal>().FirstOrDefault();
                    }

                    if (fineCorpTypeID != 0)
                    {
                        sql = @"SELECT TypeCode FROM Tb_HSPR_CorpIncidentType WHERE CorpTypeID=@CorpTypeID;";
                        fineCorpTypeCode = conn.Query<string>(sql, new { CorpTypeID = fineCorpTypeID }, trans).FirstOrDefault();
                    }

                    // 报事id
                    var incidentId = conn.Query<long>("Proc_HSPR_IncidentAccept_GetMaxNum",
                        new { CommID = community.CommID, SQLEx = "" }, trans, false, null, CommandType.StoredProcedure).FirstOrDefault();

                    // 报事编号
                    var incidentNum = conn.Query<long>("Proc_HSPR_IncidentAccept_GetMaxIncidentNum",
                        new { CommID = community.CommID, }, trans, false, null, CommandType.StoredProcedure).FirstOrDefault();

                    // 更新报事编号
                    conn.Execute("Proc_HSPR_IncidentAssigned_GetCoordinateNum_UpdateSNum",
                        new { CommID = community.CommID, IncidentType = 3, IncidentHead = "" }, trans, null, CommandType.StoredProcedure);

                    sql = @"INSERT INTO Tb_HSPR_IncidentAccept(CommID,IncidentID,IncidentNum,IncidentPlace,IncidentSource,IncidentMode,
                                 DrClass,IsTouSu,CustID,RoomID,RegionalID,IncidentMan,IncidentDate,IncidentContent,IncidentImgs,Phone,AdmiMan,AdmiDate,
                                 DispType,DispLimit,DealLimit,ReserveDate,IsFee,Duty,BigCorpTypeID,TypeID,BigCorpTypeCode,IsStatistics,IsDelete,
                                 FineCorpTypeID,FineCorpTypeCode)
                            VALUES(@CommID,@IncidentID,@IncidentNum,@IncidentPlace,@IncidentSource,@IncidentMode,
                                 @DrClass,0,@CustID,@RoomID,@RegionalID,@IncidentMan,getdate(),@IncidentContent,@IncidentImgs,@Phone,@AdmiMan,getdate(),
                                 0,@DispLimit,@DealLimit,@ReserveDate,0,'物业类',@BigCorpTypeID,@TypeID,@BigCorpTypeCode,1,0,
                                 @FineCorpTypeID,@FineCorpTypeCode);";

                    conn.Execute(sql, new
                    {
                        CommID = community.CommID,
                        IncidentID = incidentId,
                        IncidentNum = incidentNum,
                        IncidentPlace = incidentPlace,
                        IncidentSource = incidentSource,
                        IncidentMode = "业主APP",
                        CustID = custId,
                        RoomID = roomId,
                        RegionalID = regionalId,
                        IncidentMan = incidentMan,
                        IncidentContent = content,
                        IncidentImgs = incidentImgs,
                        Phone = phone,
                        DispLimit = dispLimit,
                        DealLimit = dealLimit,
                        ReserveDate = reserveDate,
                        BigCorpTypeID = bigCorpTypeID,
                        BigCorpTypeCode = bigCorpTypeCode,
                        FineCorpTypeID = fineCorpTypeID,
                        FineCorpTypeCode = fineCorpTypeCode,
                        DrClass = drClass,
                        AdmiMan = appName,
                        TypeID = $",{bigCorpTypeID},"
                    }, trans);

                    trans.Commit();

                    // 推送信息
                    PMSIncidentPush.SynchPushIncidentAccepted(incidentId);

                    return JSONHelper.FromString(true, "报事成功!稍后会有人员与您联系!");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message + ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 报事评价
        /// </summary>
        private string IncidentEvaluate(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编号不能为空");
            }
            if (!row.Table.Columns.Contains("ServiceQuality") || string.IsNullOrEmpty(row["ServiceQuality"].ToString()))
            {
                return JSONHelper.FromString(false, "评价星级不能为空");
            }
            if (!row.Table.Columns.Contains("CommentContent") || string.IsNullOrEmpty(row["CommentContent"].ToString()))
            {
                return JSONHelper.FromString(false, "评价内容不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var incidentId = AppGlobal.StrToLong(row["IncidentID"].ToString());
            var serviceQuality = row["ServiceQuality"].ToString();
            var commentContent = row["CommentContent"].ToString();

            //查询小区
            var community = GetCommunity(commId.ToString());
            if (community == null)
                return JSONHelper.FromString(false, "该小区不存在");

            PubConstant.hmWyglConnectionString = GetConnectionStr(community);

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT * FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID;";

                var incidentInfo = conn.Query<PMSIncidentAcceptModel>(sql, new { IncidentID = incidentId }).FirstOrDefault();
                if (incidentInfo == null)
                    return JSONHelper.FromString(false, "不存在此报事信息");

                var control = PMSIncidentAccept.GetIncidentControlSet(conn);
                if (control.IncidentComprehensiveEvaluationOptional.Contains("一般") == false && serviceQuality == "一般")
                {
                    serviceQuality = "满意";
                }

                // 业主自己从业主App评价，受访人默认为客户本人、没有回访人，此条件可以用于ERP端判断用
                sql = @"INSERT INTO Tb_HSPR_IncidentReply(CommID,IncidentID,ReplyType,
                            ReplyDate,ReplyContent,ServiceQuality,ReplyWay,ReplyResult,IsDelete) 
                        VALUES(@CommID,@IncidentID,'App业主自评',getdate(),@CommentContent,@ServiceQuality,@ReplyWay,@ReplyResult,0);

                        UPDATE Tb_HSPR_IncidentAccept SET ServiceQuality=@ServiceQuality,CustComments=@CommentContent,
                            IsClose=1,CloseTime=getdate()
                        WHERE IncidentID=@IncidentID;";

                conn.Execute(sql, new
                {
                    CommID = community.CommID,
                    IncidentID = incidentId,
                    CommentContent = commentContent,
                    ServiceQuality = serviceQuality,
                    ReplyWay = "客户线上评价",
                    ReplyResult = "1"
                });

                return JSONHelper.FromString(true, "报事评价成功!");
            }
        }
    }
}
