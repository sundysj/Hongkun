using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business
{
    public partial class PMSIncidentAccept
    {
        /// <summary>
        /// 延期申请
        /// </summary>
        private string IncidentDelayApply(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }
            if (!row.Table.Columns.Contains("DelayHours") || string.IsNullOrEmpty(row["DelayHours"].ToString()))
            {
                return new ApiResult(false, "缺少参数DelayHours").toJson();
            }
            if (!row.Table.Columns.Contains("DelayDate") || string.IsNullOrEmpty(row["DelayDate"].ToString()))
            {
                return new ApiResult(false, "缺少参数DelayDate").toJson();
            }
            if (!row.Table.Columns.Contains("DelayReason") || string.IsNullOrEmpty(row["DelayReason"].ToString()))
            {
                return new ApiResult(false, "缺少参数DelayReason").toJson();
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].ToString());
            var delayHours = AppGlobal.StrToInt(row["DelayHours"].ToString());
            var delayDate = row["DelayDate"].ToString();
            var delayReason = row["DelayReason"].ToString();

            var incidentInfo = GetIncidentInfo(incidentId);

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();

                try
                {
                    var sql = @"SELECT count(1) FROM Tb_HSPR_IncidentDelayApply 
                                WHERE IncidentID=@IncidentID AND (AuditState IS NULL OR AuditState='审核中');";

                    // 是否已经提交申请
                    int i = conn.Query<int>(sql, new { IncidentID = incidentId }, trans).FirstOrDefault();
                    if (i > 0)
                    {
                        trans.Commit();
                        return JSONHelper.FromString(false, "已提交申请，请勿重复申请");
                    }

                    var iid = Guid.NewGuid().ToString();
                    var auditState = "审核中";
                    var title = $"{Global_Var.UserName}提交的报事编号为【{incidentInfo.IncidentNum}】的延期申请";

                    sql = @"INSERT INTO Tb_HSPR_IncidentAuditingWorkFlow(IID, KeyIID, CommID, RoleCode, IsAudit, AuditType,orderID)
                            SELECT newid(), @KeyIID, @CommID, RoleCode, 0, 0,orderID 
                            FROM Tb_HSPR_IncidentAuditingSet 
                            WHERE AuditType=0
                            AND TypeCode=(SELECT BigCorpTypeCode FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID)
                            ORDER BY OrderID;";

                    // 插入有审核权限的岗位
                    i = conn.Execute(sql, new
                    {
                        KeyIID = iid,
                        CommID = incidentInfo.CommID,
                        IncidentID = incidentId
                    }, trans);

                    if (i == 0)
                    {
                        trans.Rollback();
                        return JSONHelper.FromString(false, "未设置审核岗位，不能提交申请");
                    }

                    sql = @"INSERT INTO Tb_HSPR_IncidentDelayApply(IID,CommID,CommName,IncidentID,IncidentNum,DealLimit,
                                RoomSign,BigReasonName,BigReasonCode,SmallReasonName,SmalReasonCode,Title,DelayReason,
                                DelayHours,DealyDate,WorkStartDate,UserCode,UserName,AuditState)

                            SELECT @IID,a.CommID,b.CommName,a.IncidentID,a.IncidentNum,isnull(a.DealLimit,0) AS DealLimit,
                                c.RoomSign,d.TypeName AS BigTypeName,BigCorpTypeCode,e.TypeName AS FineTypeName,FineCorpTypeCode,
                                @Title,@DelayReason,@DelayHours,@DealyDate,getdate(),@UserCode,@UserName,@AuditState
                            FROM Tb_HSPR_IncidentAccept a 
                                LEFT JOIN Tb_HSPR_Community b ON a.CommID=b.CommID
                                LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID
                                LEFT JOIN Tb_HSPR_CorpIncidentType d ON d.TypeCode=a.BigCorpTypeCode
                                LEFT JOIN Tb_HSPR_CorpIncidentType e ON e.TypeCode=a.FineCorpTypeCode
                            WHERE a.IncidentID=@IncidentID;";

                    i = conn.Execute(sql, new
                    {
                        IID = iid,
                        IncidentID = incidentId,
                        Title = title,
                        UserName = Global_Var.LoginUserName,
                        UserCode = Global_Var.LoginUserCode,
                        DelayReason = delayReason,
                        DelayHours = delayHours,
                        DealyDate = delayDate,
                        AuditState = auditState
                    }, trans);

                    trans.Commit();

                    if (i > 0)
                    {
                        // 推送给审核人
                        PMSIncidentPush.SynchPushIncidentDelayApplied(iid);

                        return JSONHelper.FromString(true, "申请成功");
                    }

                    trans.Rollback();
                    return JSONHelper.FromString(false, "申请失败");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 协助申请
        /// </summary>
        private string IncidentAssistApply(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }
            if (!row.Table.Columns.Contains("AssistPeopleCount") || string.IsNullOrEmpty(row["AssistPeopleCount"].ToString()))
            {
                return new ApiResult(false, "缺少参数AssistPeopleCount").toJson();
            }
            if (!row.Table.Columns.Contains("AssistReason") || string.IsNullOrEmpty(row["AssistReason"].ToString()))
            {
                return new ApiResult(false, "缺少参数AssistReason").toJson();
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].ToString());
            var AssistPeopleCount = AppGlobal.StrToInt(row["AssistPeopleCount"].ToString());
            var AssistReason = row["AssistReason"].ToString();

            var incidentInfo = GetIncidentInfo(incidentId);

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();

                try
                {
                    var sql = @"SELECT count(1) FROM Tb_HSPR_IncidentAssistApply 
                                WHERE IncidentID=@IncidentID AND (AuditState IS NULL OR AuditState='审核中');";

                    // 是否已经提交申请
                    int i = conn.Query<int>(sql, new { IncidentID = incidentId }, trans).FirstOrDefault();
                    if (i > 0)
                    {
                        trans.Commit();
                        return JSONHelper.FromString(false, "已提交申请，请勿重复申请");
                    }

                    var iid = Guid.NewGuid().ToString();
                    var auditState = "审核中";
                    var title = $"{Global_Var.UserName}提交的报事编号为【{incidentInfo.IncidentNum}】的协助申请";

                    sql = @"INSERT INTO Tb_HSPR_IncidentAuditingWorkFlow(IID, KeyIID, CommID, RoleCode, IsAudit, AuditType,orderID)
                            SELECT newid(), @KeyIID, @CommID, RoleCode, 0, 1,orderID 
                            FROM Tb_HSPR_IncidentAuditingSet WHERE AuditType=1
                            AND TypeCode=(SELECT BigCorpTypeCode FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID)
                            ORDER BY OrderID;";

                    // 插入有审核权限的岗位
                    i = conn.Execute(sql, new
                    {
                        KeyIID = iid,
                        CommID = incidentInfo.CommID,
                        IncidentID = incidentId
                    }, trans);

                    if (i == 0)
                    {
                        trans.Rollback();
                        return JSONHelper.FromString(false, "未设置审核岗位，不能提交申请");
                    }

                    sql = @"INSERT INTO Tb_HSPR_IncidentAssistApply(IID, CommID, CommName, IncidentID, IncidentNum,
                                RoomSign,BigReasonName,BigReasonCode,SmallReasonName,SmalReasonCode,Title,AssistReason,
                                AssistPeopleCount,WorkStartDate,UserCode,UserName,AuditState)
                            SELECT @IID,a.CommID,b.CommName,IncidentID,IncidentNum,
                                c.RoomSign,d.TypeName AS BigTypeName,BigCorpTypeCode,e.TypeName AS FineTypeName,FineCorpTypeCode,
                                @Title,@AssistReason,@AssistPeopleCount,getdate(),@UserCode,@UserName,@AuditState
                            FROM Tb_HSPR_IncidentAccept a 
                                LEFT JOIN Tb_HSPR_Community b ON a.CommID=b.CommID
                                LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID
                                LEFT JOIN Tb_HSPR_CorpIncidentType d ON d.TypeCode=a.BigCorpTypeCode
                                LEFT JOIN Tb_HSPR_CorpIncidentType e ON e.TypeCode=a.FineCorpTypeCode
                            WHERE a.IncidentID=@IncidentID;";

                    i = conn.Execute(sql, new
                    {
                        IID = iid,
                        IncidentID = incidentId,
                        Title = title,
                        UserName = Global_Var.LoginUserName,
                        UserCode = Global_Var.LoginUserCode,
                        AssistReason = AssistReason,
                        AssistPeopleCount = AssistPeopleCount,
                        AuditState = auditState
                    }, trans);

                    trans.Commit();

                    if (i > 0)
                    {
                        // 推送给审核人
                        PMSIncidentPush.SynchPushIncidentAssistApplied(iid);

                        return JSONHelper.FromString(true, "申请成功");
                    }

                    trans.Rollback();
                    return JSONHelper.FromString(false, "申请失败");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 非正常关闭申请
        /// </summary>
        private string IncidentUnnormalCloseApply(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }
            if (!row.Table.Columns.Contains("CloseReason") || string.IsNullOrEmpty(row["CloseReason"].ToString()))
            {
                return new ApiResult(false, "缺少参数CloseReason").toJson();
            }
            if (!row.Table.Columns.Contains("CloseSituation") || string.IsNullOrEmpty(row["CloseSituation"].ToString()))
            {
                return new ApiResult(false, "缺少参数CloseSituation").toJson();
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].ToString());
            var CloseReason = row["CloseReason"].ToString();
            var CloseSituation = row["CloseSituation"].ToString();
            var imgs = default(string);

            if (row.Table.Columns.Contains("Imgs") && !string.IsNullOrEmpty(row["Imgs"].AsString()))
            {
                imgs = row["Imgs"].AsString();
            }

            var incidentInfo = GetIncidentInfo(incidentId);

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();

                try
                {
                    var sql = @"SELECT count(1) FROM Tb_HSPR_IncidentUnnormalClose 
                                WHERE IncidentID=@IncidentID AND (AuditState IS NULL OR AuditState='审核中');";

                    // 是否已经提交申请
                    int i = conn.Query<int>(sql, new { IncidentID = incidentId }, trans).FirstOrDefault();
                    if (i > 0)
                    {
                        trans.Commit();
                        return JSONHelper.FromString(false, "已提交申请，请勿重复申请");
                    }

                    var iid = Guid.NewGuid().ToString();
                    var auditState = "审核中";
                    var title = $"{Global_Var.UserName}提交的报事编号为【{incidentInfo.IncidentNum}】的非正常关闭申请";

                    sql = @"INSERT INTO Tb_HSPR_IncidentAuditingWorkFlow(IID, KeyIID, CommID, RoleCode, IsAudit, AuditType,orderID)
                            SELECT newid(), @KeyIID, @CommID, RoleCode, 0, 2,orderID 
                            FROM Tb_HSPR_IncidentAuditingSet 
                            WHERE AuditType=2
                            AND TypeCode=(SELECT BigCorpTypeCode FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID)
                            ORDER BY OrderID;";

                    // 插入有审核权限的岗位
                    i = conn.Execute(sql, new
                    {
                        KeyIID = iid,
                        CommID = incidentInfo.CommID,
                        IncidentID = incidentId
                    }, trans);

                    if (i == 0)
                    {
                        trans.Rollback();
                        return JSONHelper.FromString(false, "未设置审核岗位，不能提交申请");

                        // 此处不再更新主表数据
                        //conn.Execute(@"UPDATE Tb_HSPR_IncidentAccept SET IsClose=1,CloseTime=getdate(),CloseType=1,NoNormalCloseReasons=@CloseReason,
                        //            CloseSituation=@CloseSituation,CloseUserCode=@CloseUserCode WHERE IncidentID=@IncidentID;",
                        //            new
                        //            {
                        //                IncidentID = IncidentID,
                        //                CloseUserCode = Global_Var.LoginUserCode,
                        //                CloseSituation = CloseSituation,
                        //                CloseReason = CloseReason
                        //            }, trans);
                    }

                    sql = @"INSERT INTO Tb_HSPR_IncidentUnnormalClose(IID,CommID,CommName,IncidentID,IncidentNum,
                                RoomSign,Title,CloseReason,WorkStartDate,UserCode,UserName,AuditState)

                            SELECT @IID,a.CommID,b.CommName,IncidentID,IncidentNum,c.RoomSign,@Title,
                                @CloseReason,getdate(),@UserCode,@UserName,@AuditState
                            FROM Tb_HSPR_IncidentAccept a
                                LEFT JOIN Tb_HSPR_Community b ON a.CommID=b.CommID
                                LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID
                            WHERE IncidentID=@IncidentID;";

                    i = conn.Execute(sql, new
                    {
                        IID = iid,
                        IncidentID = incidentId,
                        Title = title,
                        UserName = Global_Var.LoginUserName,
                        UserCode = Global_Var.LoginUserCode,
                        CloseReason = CloseReason,
                        AuditState = auditState
                    }, trans);

                    if (!string.IsNullOrEmpty(imgs))
                    {
                        sql = @"INSERT INTO Tb_HSPR_IncidentAdjunct(AdjunctCode,IncidentID, AdjunctName,FilPath,FileExName,FileSize,ImgGUID)
                                VALUES(@AdjunctCode,@IncidentID,'非正常关闭图片',@FilPath,@FileExName,@FileSize,@ImgGUID)";

                        var tmp = imgs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in tmp)
                        {
                            Thread.Sleep(50);
                            var adjunctCode = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random((int)(DateTime.Now.Ticks & 0xffffffffL) | (int)(DateTime.Now.Ticks >> 32)).Next(100, 999).ToString();

                            conn.Execute(sql, new
                            {
                                AdjunctCode = adjunctCode,
                                IncidentID = incidentId,
                                FilPath = item,
                                FileExName = Path.GetExtension(item),
                                FileSize = 0,
                                ImgGUID = iid
                            }, trans);
                        }
                    }

                    trans.Commit();

                    if (i > 0)
                    {
                        // 推送给审核人
                        PMSIncidentPush.SynchPushIncidentUnnormalCloseApplied(iid);

                        return JSONHelper.FromString(true, "申请成功");
                    }

                    trans.Rollback();
                    return JSONHelper.FromString(false, "申请失败");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 免费申请
        /// </summary>
        private string IncidentFreeApply(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))//报事ID
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }
            if (!row.Table.Columns.Contains("FeesFreeReason") || string.IsNullOrEmpty(row["FeesFreeReason"].AsString()))
            {
                return new ApiResult(false, "缺少参数FeesFreeReason").toJson();
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].ToString());
            var feesFreeReason = row["FeesFreeReason"].ToString();
            var incidentInfo = GetIncidentInfo(incidentId);

            using (var conn = new SqlConnection(Global_Fun.BurstConnectionString(incidentInfo.CommID, Global_Fun.BURST_TYPE_CHARGE)))
            {
                // 该报事下至少一笔费用已经收取、冲抵、减免、代扣
                var sql = @"SELECT count(1) FROM Tb_HSPR_Fees 
                            WHERE IncidentID=@IncidentID AND (IsCharge=1 OR PaidAmount>0 OR WaivAmount>0 OR IsBank=1 OR IsPrec=1);";

                var i = conn.Query<int>(sql, new { IncidentID = incidentId }).FirstOrDefault();
                if (i > 0)
                    return new ApiResult(false, "费用已收取，不允许免费").toJson();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();

                try
                {
                    // 是否已经提交申请
                    var sql = @"SELECT count(1) FROM Tb_HSPR_IncidentFreeApply 
                                WHERE IncidentID=@IncidentID AND AuditState='审核中';";
                    int i = conn.Query<int>(sql, new { IncidentID = incidentId }, trans).FirstOrDefault();
                    if (i > 0)
                    {
                        trans.Commit();
                        return new ApiResult(false, "已提交申请，请勿重复申请").toJson();
                    }

                    // 判断该报事本身是否为不收费
                    sql = @"SELECT count(1) FROM Tb_HSPR_IncidentAccept 
                            WHERE IncidentID=@IncidentID AND isnull(IsFee,0)=0";
                    i = conn.Query<int>(sql, new { IncidentID = incidentId }, trans).FirstOrDefault();
                    if (i > 0)
                    {
                        trans.Commit();
                        return new ApiResult(false, "该报事已是免费状态").toJson();
                    }

                    var iid = Guid.NewGuid().ToString();
                    var auditState = "审核中";
                    var auditUser = default(string);
                    var title = $"{Global_Var.UserName}提交的报事编号为【{incidentInfo.IncidentNum}】的免费申请";

                    sql = @"INSERT INTO Tb_HSPR_IncidentAuditingWorkFlow(IID, KeyIID, CommID, RoleCode, IsAudit, AuditType,orderID)
                            SELECT newid(), @KeyIID, @CommID, RoleCode, 0, 0,orderID FROM Tb_HSPR_IncidentAuditingSet WHERE AuditType=3
                            AND TypeCode=(SELECT BigCorpTypeCode FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID)
                            ORDER BY OrderID;";

                    // 插入有审核权限的岗位
                    i = conn.Execute(sql, new
                    {
                        KeyIID = iid,
                        CommID = incidentInfo.CommID,
                        IncidentID = incidentId
                    }, trans);

                    if (i == 0)
                    {
                        trans.Rollback();
                        return JSONHelper.FromString(false, "未设置审核岗位，不能提交申请");
                    }

                    sql = @"INSERT INTO Tb_HSPR_IncidentFreeApply(IID, CommID,CommName,IncidentID,IncidentNum,Title,FreeReason,
                                WorkStartDate,UserCode,UserName,AuditUser,AuditState)

                            SELECT @IID,a.CommID,b.CommName,a.IncidentID,a.IncidentNum,@Title,
                                @FreeReason,getdate(),@UserCode,@UserName,@AuditUser,@AuditState
                            FROM Tb_HSPR_IncidentAccept a 
                            LEFT JOIN Tb_HSPR_Community b ON a.CommID=b.CommID
                            WHERE a.IncidentID=@IncidentID;";

                    i = conn.Execute(sql, new
                    {
                        IID = iid,
                        IncidentID = incidentId,
                        Title = title,
                        UserName = Global_Var.LoginUserName,
                        UserCode = Global_Var.LoginUserCode,
                        FreeReason = feesFreeReason,
                        AuditUser = auditUser,
                        AuditState = auditState
                    }, trans);

                    trans.Commit();

                    if (i > 0)
                    {
                        // 推送给审核人
                        PMSIncidentPush.SynchPushIncidentFreeApplied(iid);

                        return JSONHelper.FromString(true, "申请成功");
                    }

                    trans.Rollback();
                    return JSONHelper.FromString(false, "申请失败");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
        }
    }
}
