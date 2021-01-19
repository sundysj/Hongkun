using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using Business.PMS10.物管App.报事.Models;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Newtonsoft.Json;
using static Dapper.SqlMapper;

namespace Business
{
    public partial class PMSIncidentAccept
    {
        /// <summary>
        /// 受理
        /// </summary>
        private string IncidentAccept(DataRow row)
        {
            #region 基础数据校验

            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return new ApiResult(false, "缺少参数CommID").toJson();
            }
            if (!row.Table.Columns.Contains("IncidentPlace") || string.IsNullOrEmpty(row["IncidentPlace"].ToString()))
            {
                return new ApiResult(false, "缺少参数IncidentPlace").toJson();
            }
            if (!row.Table.Columns.Contains("BigCorpTypeID") || string.IsNullOrEmpty(row["BigCorpTypeID"].ToString()))
            {
                return new ApiResult(false, "缺少参数BigCorpTypeID").toJson();
            }
            if (!row.Table.Columns.Contains("IncidentMan") || string.IsNullOrEmpty(row["IncidentMan"].ToString()))
            {
                return new ApiResult(false, "报事人不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("IncidentContent") || string.IsNullOrEmpty(row["IncidentContent"].ToString()))
            {
                return new ApiResult(false, "报事内容不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("Phone") || string.IsNullOrEmpty(row["Phone"].ToString()))
            {
                return new ApiResult(false, "报事手机不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("IncidentSource") || string.IsNullOrEmpty(row["IncidentSource"].ToString()))
            {
                return new ApiResult(false, "报事来源不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("DrClass") || string.IsNullOrEmpty(row["DrClass"].ToString()))
            {
                return new ApiResult(false, "报事类型不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("Duty") || string.IsNullOrEmpty(row["Duty"].ToString()))
            {
                return new ApiResult(false, "报事责任不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("IsTouSu") || string.IsNullOrEmpty(row["IsTouSu"].ToString()))
            {
                return new ApiResult(false, "是否投诉不能为空").toJson();
            }

            var incidentPlace = row["IncidentPlace"].ToString();        // 报事区域
            var commID = AppGlobal.StrToInt(row["CommID"].ToString());  // 小区id
            var custID = 0L;                                            // 户内报事业主id
            var roomID = 0L;                                            // 户内报事房屋编号
            var isFee = 0;                                              // 是否收费

            var regionalID = 0L;                                        // 公区报事公区id
            var localePosition = default(string);                       // 公区报事公区方位
            var localeFunction = default(string);                       // 公区报事公区方位

            var EqId = default(string);                                 // 公区报事设备id
            var taskEqId = default(string);                             // 公区报事设备任务编码，只有设备报事的时候才有用

            if (row.Table.Columns.Contains("CustID") && !string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                custID = AppGlobal.StrToLong(row["CustID"].ToString());
            }
            if (row.Table.Columns.Contains("RoomID") && !string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                roomID = AppGlobal.StrToLong(row["RoomID"].ToString());
            }
            if (row.Table.Columns.Contains("LocalePosition") && !string.IsNullOrEmpty(row["LocalePosition"].ToString()))
            {
                localePosition = row["LocalePosition"].ToString();
            }
            if (row.Table.Columns.Contains("LocaleFunction") && !string.IsNullOrEmpty(row["LocaleFunction"].ToString()))
            {
                localeFunction = row["LocaleFunction"].ToString();
            }
            if (row.Table.Columns.Contains("EqId") && !string.IsNullOrEmpty(row["EqId"].ToString()))
            {
                EqId = row["EqId"].ToString();
            }
            if (row.Table.Columns.Contains("taskEqId") && !string.IsNullOrEmpty(row["taskEqId"].ToString()))
            {
                taskEqId = row["taskEqId"].ToString();
            }
            if (row.Table.Columns.Contains("IsFee") && !string.IsNullOrEmpty(row["IsFee"].ToString()))
            {
                isFee = AppGlobal.StrToInt(row["IsFee"].ToString());
            }

            if (incidentPlace == "户内" || incidentPlace == "业主权属")
            {
                if (roomID == 0)
                {
                    return new ApiResult(false, "缺少参数RoomID").toJson();
                }
            }
            else
            {
                if (!row.Table.Columns.Contains("RegionalID") || string.IsNullOrEmpty(row["RegionalID"].ToString()))
                {
                    return new ApiResult(false, "缺少参数RegionalID").toJson();
                }
                regionalID = AppGlobal.StrToLong(row["RegionalID"].ToString());
            }

            var drClass = row["DrClass"].ToString();                                    // 报事类型
            var bigCorpTypeID = AppGlobal.StrToLong(row["BigCorpTypeID"].ToString());   // 报事大类
            var smallTypeID = 0L;                                                       // 报事细类，口头派工时必传

            // 口头派工
            if (drClass == "2")
            {
                if (!row.Table.Columns.Contains("SmallCorpTypeID") || string.IsNullOrEmpty(row["SmallCorpTypeID"].ToString()))
                {
                    return new ApiResult(false, "缺少参数SmallCorpTypeID").toJson();
                }

                smallTypeID = AppGlobal.StrToLong(row["SmallCorpTypeID"].ToString());
            }


            var incidentSource = row["IncidentSource"].ToString();      // 报事来源
            var isTousu = row["IsTouSu"].ToString();                    // 是否投诉
            var incidentMan = row["IncidentMan"].ToString();            // 报事人
            var phone = row["Phone"].ToString();                        // 报事电话
            var incidentContent = row["IncidentContent"].ToString();    // 报事内容
            var duty = row["Duty"].ToString();                          // 报事责任
            var reserveDate = default(string);                          // 要求处理时间
            var incidentImgs = default(string);                         // 报事图片

            incidentContent = incidentContent.Replace("0x2B", "+");
            incidentContent = incidentContent.Replace("0x3C", "<");
            if (row.Table.Columns.Contains("ReserveDate") && !string.IsNullOrEmpty(row["ReserveDate"].ToString()))
            {
                reserveDate = row["ReserveDate"].ToString();
            }
            else
            {
                reserveDate = DateTime.Now.ToString();
            }

            if (row.Table.Columns.Contains("IncidentImgs") && !string.IsNullOrEmpty(row["IncidentImgs"].ToString()))
            {
                incidentImgs = row["IncidentImgs"].ToString();
                incidentImgs = string.Join(",", incidentImgs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct());
            }
            if (incidentSource == "0")
            {
                incidentSource = "客户报事";
            }
            if (incidentSource == "1")
            {
                incidentSource = "自查报事";
            }

            #endregion

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();

                var incidentId = 0L;
                var incidentNum = "";

                try
                {
                    // 检查是否重复报事
                    var sql = @"SELECT IncidentID,IncidentNum FROM Tb_HSPR_IncidentAccept
                                WHERE CommID=@CommID AND RoomID=@RoomID AND RegionalID=@RegionalID AND IncidentContent=@IncidentContent
                                AND isnull(IsDelete,0)=0 AND isnull(IsClose,0)=0 AND convert(varchar(13),IncidentDate)=convert(varchar(13),getdate())";

                    var incidentInfo = conn.Query(sql, new
                    {
                        CommID = commID,
                        RoomID = roomID,
                        RegionalID = regionalID,
                        IncidentContent = incidentContent
                    }, trans).FirstOrDefault();

                    if (incidentInfo != null)
                    {
                        trans.Commit();

                        // 兼容报事调整之前的10.0报事系统
                        var oldPMS = ConfigurationManager.AppSettings["OldPMS"];
                        if (oldPMS != null && oldPMS == "1")
                        {
                            return new ApiResult(true, $"{incidentInfo.IncidentID}|{incidentInfo.IncidentNum}").toJson();
                        }

                        return new ApiResult(true, new
                        {
                            IncidentID = incidentInfo.IncidentID,
                            IncidentNum = incidentInfo.IncidentNum,
                            Message = "该报事已提交，请勿重复报事"
                        }).toJson();
                    }

                    // 报事id
                    incidentId = conn.Query<long>("Proc_HSPR_IncidentAccept_GetMaxNum",
                        new { CommID = commID, SQLEx = "" }, trans, false, null, CommandType.StoredProcedure).FirstOrDefault();

                    // 报事编号
                    incidentNum = conn.Query<string>("Proc_HSPR_IncidentAccept_GetMaxIncidentNum",
                        new { CommID = commID, }, trans, false, null, CommandType.StoredProcedure).FirstOrDefault();

                    var parameters = new DynamicParameters();
                    parameters.Add("@AdmiMan", Global_Var.LoginUserName);
                    parameters.Add("@CommID", commID);
                    parameters.Add("@IncidentPlace", incidentPlace);
                    parameters.Add("@IncidentID", incidentId);
                    parameters.Add("@IncidentNum", incidentNum);
                    parameters.Add("@IncidentSource", incidentSource);
                    parameters.Add("@DrClass", drClass);
                    parameters.Add("@IsTouSu", isTousu);
                    parameters.Add("@CustID", custID);
                    parameters.Add("@RoomID", roomID);
                    parameters.Add("@RegionalID", regionalID);
                    parameters.Add("@LocalePosition", localePosition);
                    parameters.Add("@LocaleFunction", localeFunction);
                    parameters.Add("@EqId", EqId);
                    parameters.Add("@TaskEqId", taskEqId);
                    parameters.Add("@IncidentMan", incidentMan);
                    parameters.Add("@IncidentContent", incidentContent);
                    parameters.Add("@IncidentImgs", incidentImgs);
                    parameters.Add("@Phone", phone);
                    parameters.Add("@ReserveDate", reserveDate);
                    parameters.Add("@IsFee", isFee);
                    parameters.Add("@Duty", duty);
                    parameters.Add("@BigCorpTypeID", bigCorpTypeID);
                    parameters.Add("@FineCorpTypeID", smallTypeID);

                    conn.Execute("Proc_HSPR_IncidentAccept_Insert_Phone_WJ", parameters, trans, null, CommandType.StoredProcedure);

                    sql = @"UPDATE Tb_HSPR_IncidentAccept SET AdmiManUserCode=@UserCode WHERE IncidentID=@IncidentID;";
                    conn.Execute(sql, new { UserCode = Global_Var.LoginUserCode, IncidentID = incidentId }, trans);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message);
                }

                try
                {
                    if (Global_Var.CorpName.Contains("星河"))
                    {
                        // 星河推送产业
                        XingHeIncidentPush(commID, roomID, incidentId.ToString(), incidentContent, phone, incidentMan, incidentImgs);
                    }

                    var message = "";

                    // 如果选择了责任人，直接调用分派接口
                    if (row.Table.Columns.Contains("DealManUserCode") && !string.IsNullOrEmpty(row["DealManUserCode"].AsString()))
                    {
                        row.Table.Columns.Add("IncidentID", typeof(string));
                        row["IncidentID"] = incidentId;

                        IncidentAssign(row, true);
                    }
                    else
                    {
                        PMSIncidentPush.SynchPushIncidentAccepted(incidentId);
                    }

                    // 禅道需求-9514
                    if (Global_Var.CorpName.Contains("彰泰"))
                    {
                        var crm = new IncidentAcceptCRM_ZT();
                        crm.IncidentCRM(incidentId.ToString(), CRMZTType.受理);
                    }

                    // 兼容报事调整之前的10.0报事系统
                    var oldPMS = ConfigurationManager.AppSettings["OldPMS"];
                    if (oldPMS != null && oldPMS == "1")
                    {
                        return new ApiResult(true, $"{incidentId}|{incidentNum}").toJson();
                    }

                    return new ApiResult(true, new
                    {
                        IncidentID = incidentId,
                        IncidentNum = incidentNum,
                        Message = "报事成功" + message
                    }).toJson();
                }
                catch (Exception ex)
                {
                    return new ApiResult(false, ex.Message + ex.StackTrace).toJson();
                }
            }
        }

        /// <summary>
        /// 派单 & 抢单
        /// </summary>
        private string IncidentAssign(DataRow row, bool acceptAssign = false)
        {
            #region 基础数据校验
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))
            {
                return new ApiResult(false, "缺少参数报事ID").toJson();
            }
            if (!row.Table.Columns.Contains("BigCorpTypeID") || string.IsNullOrEmpty(row["BigCorpTypeID"].AsString()))
            {
                return new ApiResult(false, "缺少参数BigCorpTypeID").toJson();
            }
            if (!row.Table.Columns.Contains("Duty") || string.IsNullOrEmpty(row["Duty"].AsString()))
            {
                return new ApiResult(false, "缺少参数Duty").toJson();
            }
            if (!row.Table.Columns.Contains("IncidentContent") || string.IsNullOrEmpty(row["IncidentContent"].AsString()))
            {
                return new ApiResult(false, "报事内容不能为空").toJson();
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].AsString());
            var bigCorpTypeID = AppGlobal.StrToLong(row["BigCorpTypeID"].AsString());
            var regionalId = 0L;                                                        // 公区id
            var isSnatch = 0;                                                           // 是否是抢单
            var incidentContent = row["IncidentContent"].AsString();
            var duty = row["Duty"].AsString();                                          // 报事责任归属
            var dispRemarks = default(string);                                          // 分派备注信息，一般由管家或分派岗位填写

            var assistant = default(string);                                            // 协助人
            var dealMan = default(string);
            var dealManUserCode = default(string);

            incidentContent = incidentContent.Replace("0x2B", "+");
            incidentContent = incidentContent.Replace("0x3C", "<");
            if (row.Table.Columns.Contains("IsSnatch") && !string.IsNullOrEmpty(row["IsSnatch"].AsString()))
            {
                isSnatch = AppGlobal.StrToInt(row["IsSnatch"].AsString());
            }
            if (row.Table.Columns.Contains("RegionalID") && !string.IsNullOrEmpty(row["RegionalID"].AsString()))
            {
                regionalId = AppGlobal.StrToLong(row["RegionalID"].AsString());
            }
            if (row.Table.Columns.Contains("DispRemarks") && !string.IsNullOrEmpty(row["DispRemarks"].AsString()))
            {
                dispRemarks = row["DispRemarks"].AsString();
            }
            if (row.Table.Columns.Contains("Assistant") && !string.IsNullOrEmpty(row["Assistant"].AsString()))
            {
                assistant = row["Assistant"].AsString();
            }

            if (isSnatch == 0)  // 派单时，处理人不能为空
            {
                if (!row.Table.Columns.Contains("DealMan") || string.IsNullOrEmpty(row["DealMan"].AsString()))
                {
                    return new ApiResult(false, "处理人不能为空").toJson();
                }
                if (!row.Table.Columns.Contains("DealManUserCode") || string.IsNullOrEmpty(row["DealManUserCode"].AsString()))
                {
                    return new ApiResult(false, "处理人不能为空").toJson();
                }
                dealMan = row["DealMan"].AsString();
                dealManUserCode = row["DealManUserCode"].AsString();
            }
            else
            {
                dealMan = Global_Var.LoginUserName;
                dealManUserCode = Global_Var.UserCode;
            }

            #endregion 基础数据校验

            #region 执行抢单/派单操作

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = "";
                if (isSnatch == 1 || dealManUserCode == Global_Var.LoginUserCode)
                {
                    isSnatch = 1;

                    // 受理时派单给自己，不受此限制
                    if (acceptAssign == false)
                    {
                        // 抢单时，在办工单限制
                        sql = $@"SELECT count(*) FROM Tb_HSPR_IncidentAccept 
                                WHERE DealManCode ='{dealManUserCode}' AND isnull(DealState,0)=0 AND isnull(IsDelete,0)=0";

                        // 判断抢单或分派员工是否超过最大工单数
                        var nowCount = conn.Query<int>(sql).FirstOrDefault();
                        var maxCount = GetIncidentControlSet(conn).DealManMaxOrderLimit;
                        if (nowCount >= maxCount)
                            return new ApiResult(false, "在办工单超过限制，请先处理在办工单").toJson();
                    }
                }

                var parameters = new DynamicParameters();
                parameters.Add("@IncidentID", incidentId);
                parameters.Add("@IncidentContent", incidentContent);
                parameters.Add("@DispType", 1);
                parameters.Add("@DispDate", DateTime.Now);
                parameters.Add("@DispMan", Global_Var.LoginUserName);
                parameters.Add("@DispUserCode", Global_Var.UserCode);
                parameters.Add("@Duty", duty);
                parameters.Add("@BigCorpTypeID", bigCorpTypeID);
                parameters.Add("@DealMan", dealMan);
                parameters.Add("@DealManUserCode", dealManUserCode);
                parameters.Add("@CoordinateNum", "");
                parameters.Add("@RegionalID", regionalId);
                parameters.Add("@IsSnatch", isSnatch);
                parameters.Add("@DispRemarks", dispRemarks);
                parameters.Add("@IsSucceed", 1, DbType.Int32, ParameterDirection.InputOutput);

                conn.Execute("Proc_HSPR_IncidentAccept_Update_Assigned_Phone", parameters, null, null, CommandType.StoredProcedure);

                // 删除预警信息，更新抢单标识、派单方式
                sql = @"DELETE FROM Tb_HSPR_IncidentWarningPush WHERE IncidentID=@IncidentID;
                        UPDATE Tb_HSPR_IncidentAccept SET IsSnatch=@IsSnatch,DispWay='员工APP' WHERE IncidentID=@IncidentID;";

                conn.Execute(sql, new { IsSnatch = isSnatch, IncidentID = incidentId });

                var succeed = parameters.Get<int>("@IsSucceed");
                if (succeed == 1)
                {
                    // 存在协助人
                    if (!string.IsNullOrEmpty(assistant))
                    {
                        var iid = Guid.NewGuid().ToString();
                        var usercodes = assistant.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        sql = @"INSERT INTO Tb_HSPR_IncidentAssistApply(IID, CommID, CommName, IncidentID, IncidentNum,
                                    RoomSign,BigReasonName, BigReasonCode, SmallReasonName, SmalReasonCode, Title, AssistReason,
                                    AssistPeopleCount, WorkStartDate, UserCode, UserName, AuditState, AuditUser)
                                SELECT @IID,a.CommID,b.CommName,IncidentID,IncidentNum,
                                    c.RoomSign,d.TypeName AS BigTypeName,BigCorpTypeCode, e.TypeName AS FineTypeName,FineCorpTypeCode,
                                    @UserName+'提交并审核通过的报事编号为'+a.IncidentNum+'的协助申请',@AssistReason,
                                    @AssistPeopleCount,getdate(), @UserCode, @UserName, '已审核',@UserCode 
                                FROM Tb_HSPR_IncidentAccept a 
                                    LEFT JOIN Tb_HSPR_Community b ON a.CommID=b.CommID
                                    LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID
                                    LEFT JOIN Tb_HSPR_CorpIncidentType d ON d.TypeCode=a.BigCorpTypeCode
                                    LEFT JOIN Tb_HSPR_CorpIncidentType e ON e.TypeCode=a.FineCorpTypeCode
                                WHERE a.IncidentID=@IncidentID;";

                        // 插入协助信息
                        int j = conn.Execute(sql, new
                        {
                            IID = iid,
                            IncidentID = incidentId,
                            UserName = Global_Var.LoginUserName,
                            UserCode = Global_Var.LoginUserCode,
                            AssistReason = "分派岗位或管家派单时选择协助人",
                            AssistPeopleCount = usercodes.Count(),
                        });

                        // 记录协助人
                        foreach (var usercode in usercodes)
                        {
                            conn.Execute("INSERT INTO Tb_HSPR_IncidentAssistDetail(IID,AssistID,UserCode) VALUES(newid(),@IID,@UserCode)",
                                new { IID = iid, UserCode = usercode });
                        }
                    }

                    // 推送通知
                    PMSIncidentPush.SynchPushIncidentAssigned(incidentId);

                    // 禅道需求-9514，暂时都为分派
                    if (Global_Var.CorpName.Contains("彰泰"))
                    {
                        var crm = new IncidentAcceptCRM_ZT();
                        crm.IncidentCRM(incidentId.ToString(), CRMZTType.分派);
                    }

                    if (isSnatch == 1) 
                    {
                        return new ApiResult(true, "抢单成功").toJson();
                    }
                        return new ApiResult(true, "派单成功").toJson();
                }
                else
                {
                    sql = $@"SELECT * FROM Tb_HSPR_IncidentAccept WHERE IncidentID={ incidentId }";

                    var incidentInfo = conn.Query<PMSIncidentAcceptModel>(sql).FirstOrDefault();
                    if (incidentInfo.IsDelete == 0)
                        return new ApiResult(false, "派单失败，此单已被废弃").toJson();
                    else if (incidentInfo.DispType != 0)
                        return new ApiResult(false, $"派单失败，此单已被{incidentInfo.DispMan}分派").toJson();

                    return new ApiResult(false, "派单失败").toJson();
                }
            }
            #endregion 执行抢单/派单操作
        }

        /// <summary>
        /// 接单
        /// </summary>
        private string IncidentReceive(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].AsString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var incidentInfo = GetIncidentInfo(incidentId);

                var date = DateTime.Now;

                var sql = "UPDATE Tb_HSPR_IncidentAccept SET ReceivingDate=@ReceivingDate WHERE IncidentID=@IncidentID;";

                // 口派
                if (incidentInfo.DrClass == 2)
                {
                    sql += @"UPDATE Tb_HSPR_IncidentAccept SET DispType=1,DispDate=@ReceivingDate,DispUserCode=@DispUserCode,
                                DispMan=@DispMan,DealManCode=@DealManCode,DealMan=@DealMan
                             WHERE IncidentID=@IncidentID;";
                }

                conn.Execute(sql, new
                {
                    ReceivingDate = date,
                    IncidentID = incidentId,
                    DispUserCode = Global_Var.LoginUserCode,
                    DispMan = Global_Var.LoginUserName,
                    DealManCode = Global_Var.LoginUserCode,
                    DealMan = Global_Var.LoginUserName
                });

                // 删除预警信息
                conn.Execute("DELETE FROM Tb_HSPR_IncidentWarningPush WHERE IncidentID=@IncidentID", new { IncidentID = incidentId });

                // 推送信息
                PMSIncidentPush.SynchPushIncidentReceived(incidentId);

                // 禅道需求-9514
                if (Global_Var.CorpName.Contains("彰泰"))
                {
                    var crm = new IncidentAcceptCRM_ZT();
                    crm.IncidentCRM(incidentId.ToString(), CRMZTType.接单);
                }

                return JSONHelper.FromString(true, date.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        /// <summary>
        /// 到场
        /// </summary>
        private string IncidentArrive(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].AsString());
            var imgs = default(string);

            if (row.Table.Columns.Contains("Imgs") && !string.IsNullOrEmpty(row["Imgs"].AsString()))
            {
                imgs = row["Imgs"].AsString();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var date = DateTime.Now;

                var sql = @"UPDATE Tb_HSPR_IncidentAccept SET ArriveData=@ArriveData WHERE IncidentID=@IncidentID";

                conn.Execute(sql, new { ArriveData = date, IncidentID = incidentId });

                if (!string.IsNullOrEmpty(imgs))
                {
                    sql = @"INSERT INTO Tb_HSPR_IncidentAdjunct(AdjunctCode,IncidentID, AdjunctName,FilPath,FileExName,FileSize)
                            VALUES(@AdjunctCode,@IncidentID,'到场确认图片',@FilPath,@FileExName,@FileSize)";

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
                            FileSize = 0
                        });
                    }
                }

                // 推送信息
                PMSIncidentPush.SynchPushIncidentArrived(incidentId);

                // 禅道需求-9514
                if (Global_Var.CorpName.Contains("彰泰"))
                {
                    var crm = new IncidentAcceptCRM_ZT();
                    crm.IncidentCRM(incidentId.ToString(), CRMZTType.到场);
                }

                return JSONHelper.FromString(true, date.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        /// <summary>
        /// 报事处理
        /// </summary>
        private string IncidentDeal(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }
            if (!row.Table.Columns.Contains("IncidentContent") || string.IsNullOrEmpty(row["IncidentContent"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentContent").toJson();
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].AsString());
            var incidentContent = row["IncidentContent"].ToString();
            var smallCorpTypeID = 0L;
            var dealSituation = default(string);
            var overdueReason = default(string);
            var signatoryImg = default(string);
            var processingImg = default(string);
            int isFinish = 0;
            var eqId = default(string);

            incidentContent = incidentContent.Replace("0x2B", "+");
            incidentContent = incidentContent.Replace("0x3C", "<");

            if (row.Table.Columns.Contains("SmallCorpTypeID") && !string.IsNullOrEmpty(row["SmallCorpTypeID"].AsString()))
            {
                smallCorpTypeID = AppGlobal.StrToLong(row["SmallCorpTypeID"].ToString());
            }
            if (row.Table.Columns.Contains("DealSituation") && !string.IsNullOrEmpty(row["DealSituation"].AsString()))
            {
                dealSituation = row["DealSituation"].ToString();
                dealSituation = dealSituation.Replace("0x2B", "+");
                dealSituation = dealSituation.Replace("0x3C", "<");
            }
            if (row.Table.Columns.Contains("OverdueReason") && !string.IsNullOrEmpty(row["OverdueReason"].AsString()))
            {
                overdueReason = row["OverdueReason"].ToString();
            }
            if (row.Table.Columns.Contains("SignatoryImg") && !string.IsNullOrEmpty(row["SignatoryImg"].AsString()))
            {
                signatoryImg = row["SignatoryImg"].ToString();
            }
            if (row.Table.Columns.Contains("ProcessingImg") && !string.IsNullOrEmpty(row["ProcessingImg"].AsString()))
            {
                processingImg = row["ProcessingImg"].ToString();
                processingImg = string.Join(",", processingImg.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct());
            }
            if (row.Table.Columns.Contains("IsFinish") && !string.IsNullOrEmpty(row["IsFinish"].AsString()))
            {
                isFinish = AppGlobal.StrToInt(row["IsFinish"].ToString());
            }
            if (row.Table.Columns.Contains("EqId") && !string.IsNullOrEmpty(row["EqId"].AsString()))
            {
                eqId = row["EqId"].ToString();
            }

            // 报事处理离线
            var receivingDate = default(string);
            var arriveDate = default(string);

            var finishDate = DateTime.Now;

            if (row.Table.Columns.Contains("ReceivingDate") && !string.IsNullOrEmpty(row["ReceivingDate"].AsString()))
            {
                receivingDate = row["ReceivingDate"].ToString();
            }
            if (row.Table.Columns.Contains("ArriveDate") && !string.IsNullOrEmpty(row["ArriveDate"].AsString()))
            {
                arriveDate = row["ArriveDate"].ToString();
            }

            var incidentInfo = GetIncidentInfo(incidentId);

            using (var conn = new SqlConnection(Global_Fun.BurstConnectionString(incidentInfo.CommID, Global_Fun.BURST_TYPE_CHARGE)))
            {
                // 如果数据库内是否收费为是，则先读取费用主表里面该报事的费用，若费用主表无值，则提示“收费工单，请输入费用”
                var sql = @"SELECT count(*) FROM Tb_HSPR_Fees WHERE IncidentID=@IncidentID;";

                var i = conn.Query<int>(sql, new { IncidentID = incidentId }).FirstOrDefault();
                if (i == 0 && incidentInfo.IsFee == 1)
                    return new ApiResult(false, "收费工单，请输入费用").toJson();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();

                // 接单到场离线
                var sql = @"UPDATE Tb_HSPR_IncidentAccept SET ReceivingDate=@ReceivingDate 
                            WHERE IncidentID=@IncidentID AND isnull(ReceivingDate,'')='';

                            UPDATE Tb_HSPR_IncidentAccept SET ArriveData=@ArriveDate 
                            WHERE IncidentID=@IncidentID AND isnull(ArriveData,'')='';";

                if (!string.IsNullOrEmpty(receivingDate) && !string.IsNullOrEmpty(arriveDate))
                {
                    conn.Execute(sql, new { IncidentID = incidentId, ReceivingDate = receivingDate, ArriveDate = arriveDate }, trans);
                }

                conn.Execute("Proc_HSPR_IncidentAccept_IncidentDeal_Phone", new
                {
                    CommID = incidentInfo.CommID,
                    IncidentID = incidentId,
                    IncidentContent = incidentContent,
                    SmallCorpTypeID = smallCorpTypeID,
                    DealSituation = dealSituation,
                    OverdueReason = overdueReason,
                    DealState = isFinish,
                    FinishUserCode = (isFinish > 0) ? Global_Var.LoginUserCode : null,
                    IncidentProcessingImg = processingImg,
                    SignatoryImg = signatoryImg,
                    EqId = eqId,
                    FinishDate = finishDate
                }, trans, null, CommandType.StoredProcedure);

                // 刷新处理时限
                sql = @"DECLARE @DealLimit decimal(10,2)=0.0, @DelayHours decimal(10,2)=0.0;

                        SELECT @DealLimit=(CASE WHEN a.IncidentPlace='公区' THEN b.DealLimit2 ELSE b.DealLimit END)
                        FROM Tb_HSPR_IncidentAccept a
                        LEFT JOIN Tb_HSPR_CorpIncidentType b ON a.BigCorpTypeID=b.CorpTypeID
                        WHERE a.IncidentID=@IncidentID;

                        SELECT @DelayHours=isnull(sum(DelayHours),0.0) FROM Tb_HSPR_IncidentDelayApply 
                        WHERE IncidentID=@IncidentID AND AuditState='已审核';

                        UPDATE Tb_HSPR_IncidentAccept SET DealLimit=@DealLimit+@DelayHours WHERE IncidentID=@IncidentID;

                        /* 删除预警 */
                        DELETE FROM Tb_HSPR_IncidentWarningPush WHERE IncidentID=@IncidentID;";

                conn.Execute(sql, new { IncidentID = incidentId }, trans);

                trans.Commit();

                if (isFinish == 1)
                {
                    incidentInfo = GetIncidentInfo(incidentId);

                    sql = @"/* 工时审核 */
                            DECLARE @HandlePeopleNum int=1;

                            SELECT @HandlePeopleNum=count(1)+1 FROM Tb_HSPR_IncidentAssistDetail
                            WHERE AssistID IN
                            (
                                SELECT IID FROM Tb_HSPR_IncidentAssistApply WHERE IncidentID=@IncidentID AND AuditState='已审核'
                            );

                            INSERT INTO Tb_HSPR_IncidentWorkingHoursApply(IID,IncidentID,CommID,RatedWorkHour,KPIRatio,ActualWorkHour,HandlePeopleNum)
                            VALUES(@IID,@IncidentID,@CommID,@RatedWorkHour,@KPIRatio,@ActualWorkHour,@HandlePeopleNum);

                            INSERT INTO Tb_HSPR_IncidentWorkingHoursApplyDetail(IID,WHIID,PeopleType,PeopleName,ActualWorkHour)
                            VALUES(newid(),@IID,'责任人',@DealMan,@ActualWorkHour);

                            INSERT INTO Tb_HSPR_IncidentWorkingHoursApplyDetail(IID,WHIID,PeopleType,PeopleName,ActualWorkHour)
                            SELECT newid(),@IID,'协助人',b.UserName,convert(decimal(18,2),@ActualWorkHour/@HandlePeopleNum) 
                            FROM Tb_HSPR_IncidentAssistDetail a
                            LEFT JOIN Tb_Sys_User b ON a.UserCode=b.UserCode
                            WHERE a.AssistID IN
                            (
                                SELECT IID FROM Tb_HSPR_IncidentAssistApply WHERE IncidentID=@IncidentID AND AuditState='已审核'
                            );

                            /* 处理方式 */
                            UPDATE Tb_HSPR_IncidentAccept SET DealWay='员工APP' WHERE IncidentID=@IncidentID;";

                    conn.Execute(sql, new
                    {
                        IID = Guid.NewGuid().ToString(),
                        IncidentID = incidentInfo.IncidentID,
                        CommID = incidentInfo.CommID,
                        RatedWorkHour = incidentInfo.RatedWorkHour,
                        KPIRatio = incidentInfo.KPIRatio,
                        DealMan = Global_Var.LoginUserName,
                        ActualWorkHour = ((TimeSpan)(finishDate - incidentInfo.ArriveData)).TotalMinutes / 60
                    });

                    // 推送信息
                    PMSIncidentPush.SynchPushIncidentDealt(incidentId);
                }

                // 禅道需求-9514
                if (Global_Var.CorpName.Contains("彰泰"))
                {
                    var crm = new IncidentAcceptCRM_ZT();
                    crm.IncidentCRM(incidentId.ToString(), CRMZTType.处理);
                }

                return new ApiResult(true, "操作成功").toJson();
            }
        }

        /// <summary>
        /// 正常关闭
        /// </summary>
        private string IncidentClose(DataRow row)
        {
            #region 基础数据校验
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }
            if (!row.Table.Columns.Contains("CloseSituation") || string.IsNullOrEmpty(row["CloseSituation"].AsString()))//关闭情况
            {
                return new ApiResult(false, "缺少参数CloseSituation").toJson();
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].AsString());
            var closeSituation = row["CloseSituation"].AsString().Replace("0x2B", "+").Replace("0x3C", "<");
            var imgs = default(string);

            if (row.Table.Columns.Contains("Imgs") && !string.IsNullOrEmpty(row["Imgs"].AsString()))
            {
                imgs = row["Imgs"].AsString();
            }

            #endregion

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 查询是否已提交非正常关闭且非正常关闭正在审核中
                var sql = @"SELECT count(1) FROM Tb_HSPR_IncidentUnnormalClose 
                            WHERE IncidentID=@IncidentID AND AuditState='审核中';";

                if (conn.Query<int>(sql, new { IncidentID = incidentId }).FirstOrDefault() > 0)
                {
                    return JSONHelper.FromString(false, "此报事正在申请非正常关闭");
                }

                sql = @"SELECT *,b.UserName AS CloseUserName 
                        FROM Tb_HSPR_IncidentAccept a 
                        LEFT JOIN Tb_Sys_User b ON a.CloseUserCode=b.UserCode
                        WHERE IncidentID=@IncidentID;";

                var result = conn.Query(sql, new { IncidentID = incidentId }).FirstOrDefault();
                if (result == null)
                {
                    return JSONHelper.FromString(false, "关闭失败，此单已被删除");
                }

                if (result.IsClose == 1)
                {
                    return JSONHelper.FromString(false, $"关闭失败，此单已被{result.CloseUserName}关闭");
                }

                sql = @"UPDATE Tb_HSPR_IncidentAccept  
                        SET CloseUserCode=@CloseUserCode,CloseUser=@CloseUser,CloseSituation=@CloseSituation,
                            CloseType=0,IsClose=1,NoNormalCloseReasons=null,CloseTime=getdate()
                        WHERE IncidentID=@IncidentID;

                        /* 删除预警信息 */
                        DELETE FROM Tb_HSPR_IncidentWarningPush WHERE IncidentID=@IncidentID";

                conn.Execute(sql, new
                {
                    IncidentID = incidentId,
                    CloseUserCode = Global_Var.LoginUserCode,
                    CloseUser = Global_Var.LoginUserName,
                    CloseSituation = closeSituation
                });

                if (!string.IsNullOrEmpty(imgs))
                {
                    sql = @"INSERT INTO Tb_HSPR_IncidentAdjunct(AdjunctCode,IncidentID, AdjunctName,FilPath,FileExName,FileSize)
                            VALUES(@AdjunctCode,@IncidentID,'关闭图片',@FilPath,@FileExName,@FileSize)";

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
                            FileSize = 0
                        });
                    }
                }

                // 禅道需求-9514
                if (Global_Var.CorpName.Contains("彰泰"))
                {
                    var crm = new IncidentAcceptCRM_ZT();
                    crm.IncidentCRM(incidentId.ToString(), CRMZTType.关闭);
                }

                return new ApiResult(true, "关闭成功").toJson();
            }
        }

        /// <summary>
        /// 报事关闭退回
        /// </summary>
        private string IncidentCloseReturn(DataRow row)
        {
            #region 基础数据校验
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }
            if (!row.Table.Columns.Contains("ReturnReason") || string.IsNullOrEmpty(row["ReturnReason"].AsString()))
            {
                return new ApiResult(false, "缺少参数ReturnReason").toJson();
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].AsString());
            var returnReason = row["ReturnReason"].AsString();
            #endregion

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"DECLARE @CommID int;
                            DECLARE @LastFinishTime datetime;
                            DECLARE @LastFinishUserCode nvarchar(10);
                            DECLARE @LastFinishUser nvarchar(20);

                            SELECT @CommID=CommID,@LastFinishTime=FinishDate,@LastFinishUserCode=DealManCode,@LastFinishUser=DealMan 
                            FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID;
                            
                            INSERT INTO Tb_HSPR_IncidentCloseGoBack(GoBackIID,CommID,IncidentID,LastFinishTime,LastFinishUserCode,
                                LastFinishUser,CloseGoBackReasons,CloseGoBackTime,CloseGoBackUserCode,CloseGoBackUser)
                            VALUES(newid(),@CommID,@IncidentID,@LastFinishTime,@LastFinishUserCode,@LastFinishUser,
                                @FinishGoBackReasons,getdate(),@FinishGoBackUserCode,@FinishGoBackUser);

                            UPDATE Tb_HSPR_IncidentAccept SET DealState=0,MainEndDate=null,FinishDate=null,IsClose=0,
                                CloseSituation=null,CloseTime=null,CloseType=null,CloseUser=null,CloseUserCode=null
                            WHERE IncidentID = @IncidentID;";

                conn.Execute(sql, new
                {
                    IncidentID = incidentId,
                    FinishGoBackReasons = returnReason,
                    FinishGoBackUserCode = Global_Var.LoginUserCode,
                    FinishGoBackUser = Global_Var.LoginUserName
                });
            }

            // 推送消息
            PMSIncidentPush.SynchPushIncidentCloseReturn(incidentId);

            // 禅道需求-9514
            if (Global_Var.CorpName.Contains("彰泰"))
            {
                var crm = new IncidentAcceptCRM_ZT();
                crm.IncidentCRM(incidentId.ToString(), CRMZTType.退回);
            }

            return new ApiResult(true, "退回成功").toJson();
        }

        /// <summary>
        /// 跟进
        /// </summary>
        private string IncidentFollow(DataRow row)
        {
            #region 基础数据校验
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }
            if (!row.Table.Columns.Contains("FollowReasons") || string.IsNullOrEmpty(row["FollowReasons"].AsString()))
            {
                return new ApiResult(false, "缺少参数FollowReasons").toJson();
            }
            if (!row.Table.Columns.Contains("FollowType") || string.IsNullOrEmpty(row["FollowType"].AsString()))
            {
                return new ApiResult(false, "缺少参数FollowType").toJson();
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].AsString());
            var forwardReasons = row["FollowReasons"].AsString();   // 跟进原因
            var forwardType = row["FollowType"].AsString();         // 跟进类型
            var overdueReason = default(string);
            var imgs = default(string);

            forwardReasons = forwardReasons.Replace("0x2B", "+");
            forwardReasons = forwardReasons.Replace("0x3C", "<");

            if (row.Table.Columns.Contains("OverdueReason") && !string.IsNullOrEmpty(row["OverdueReason"].AsString()))
            {
                overdueReason = row["OverdueReason"].AsString();
            }
            if (row.Table.Columns.Contains("Imgs") && !string.IsNullOrEmpty(row["Imgs"].AsString()))
            {
                imgs = row["Imgs"].AsString();
            }
            #endregion

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var iid = Guid.NewGuid().ToString();

                var sql = @"DECLARE @CommID int;
                            SELECT @CommID=CommID FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID;

                            INSERT INTO Tb_HSPR_IncidentForward(IID,CommID,IncidentID,ForwardReasons,ForwardType,
                                OverdueReason,ForwardDateTime ,ForwardUserCode,ForwardUser)  
                            VALUES (@IID,@CommID,@IncidentID,@ForwardReasons,@ForwardType,@OverdueReason,getdate(),@ForwardUserCode,@ForwardUser)";

                conn.Execute(sql, new
                {
                    IID = iid,
                    IncidentID = incidentId,
                    ForwardReasons = forwardReasons,
                    ForwardType = forwardType,
                    OverdueReason = overdueReason,
                    ForwardUserCode = Global_Var.LoginUserCode,
                    ForwardUser = Global_Var.LoginUserName
                });

                if (!string.IsNullOrEmpty(imgs))
                {
                    sql = @"INSERT INTO Tb_HSPR_IncidentAdjunct(AdjunctCode,IncidentID, AdjunctName,FilPath,FileExName,FileSize,ImgGUID)
                            VALUES(@AdjunctCode,@IncidentID,'跟进图片',@FilPath,@FileExName,@FileSize,@ImgGUID)";

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
                        });
                    }
                }

                // 禅道需求-9514
                if (Global_Var.CorpName.Contains("彰泰"))
                {
                    var crm = new IncidentAcceptCRM_ZT();
                    crm.IncidentCRM(incidentId.ToString(), CRMZTType.跟进);
                }

                return new ApiResult(true, "跟进成功").toJson();
            }
        }

        /// <summary>
        /// 转发
        /// </summary>
        private string IncidentTranspond(DataRow row)
        {
            #region 基础数据校验
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }
            if (!row.Table.Columns.Contains("BigCorpTypeID") || string.IsNullOrEmpty(row["BigCorpTypeID"].AsString()))
            {
                return new ApiResult(false, "缺少参数BigCorpTypeID").toJson();
            }
            if (!row.Table.Columns.Contains("Duty") || string.IsNullOrEmpty(row["Duty"].AsString()))
            {
                return new ApiResult(false, "缺少参数Duty").toJson();
            }
            if (!row.Table.Columns.Contains("DealMan") || string.IsNullOrEmpty(row["DealMan"].AsString()))
            {
                return new ApiResult(false, "缺少参数DealMan").toJson();
            }
            if (!row.Table.Columns.Contains("DealManUserCode") || string.IsNullOrEmpty(row["DealManUserCode"].AsString()))
            {
                return new ApiResult(false, "缺少参数DealManUserCode").toJson();
            }

            #endregion

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].AsString());
            var duty = row["Duty"].AsString();
            var bigCorpTypeID = AppGlobal.StrToLong(row["BigCorpTypeID"].AsString());
            var dealMan = row["DealMan"].AsString();
            var dealManUserCode = row["DealManUserCode"].AsString();
            var transpondReason = "";
            var assistant = "";

            if (row.Table.Columns.Contains("TransmitReason") && !string.IsNullOrEmpty(row["TransmitReason"].AsString()))
            {
                transpondReason = row["TransmitReason"].AsString();
            }
            if (row.Table.Columns.Contains("TranspondReason") && !string.IsNullOrEmpty(row["TranspondReason"].AsString()))
            {
                transpondReason = row["TranspondReason"].AsString();
            }
            if (row.Table.Columns.Contains("Assistant") && !string.IsNullOrEmpty(row["Assistant"].AsString()))
            {
                assistant = row["Assistant"].AsString();
            }

            transpondReason = transpondReason.Replace("0x2B", "+").Replace("0x3C", "<");

            // 确认转发
            bool confirmTransmit = true;
            if (row.Table.Columns.Contains("ConfirmTransmit") && !string.IsNullOrEmpty(row["ConfirmTransmit"].AsString()))
            {
                confirmTransmit = row["ConfirmTransmit"].AsString() != "0";
            }

            var incidentInfo = GetIncidentInfo(incidentId);

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                if (incidentInfo.DealState == 1)
                    return JSONHelper.FromString(false, "报事已完成，无法转发");
                if (incidentInfo.IsClose == 1)
                    return JSONHelper.FromString(false, "报事已关闭，无法转发");
                if (incidentInfo.IsDelete == 1)
                    return JSONHelper.FromString(false, "报事已删除，无法转发");

                // 转发，查看是否具有转发历史
                if (confirmTransmit == false)
                {
                    var sql = @"SELECT a.PersonLiable as NewLiableUserName, b.UserName AS TransmitUserName,TransmitDateTime 
                                FROM Tb_HSPR_IncidentTransmit a
                                LEFT JOIN Tb_Sys_User b on a.TransmitUserCode=b.UserCode
                                WHERE IncidentID=@IncidentID ORDER BY TransmitDateTime DESC ";

                    var lastTransmit = conn.Query(sql, new { IncidentID = incidentId }).FirstOrDefault();
                    if (lastTransmit != null)
                    {
                        return new ApiResult(false, lastTransmit).toJson();
                    }
                }

                conn.Execute(@"Proc_HSPR_IncidentAccept_Transmit_Phone", new
                {
                    CommID = incidentInfo.CommID,
                    IncidentID = incidentId,
                    DispMan = Global_Var.LoginUserName,
                    DispUserCode = Global_Var.LoginUserCode,
                    Duty = duty,
                    BigCorpTypeID = bigCorpTypeID,
                    DealMan = dealMan,
                    DealManUserCode = dealManUserCode,
                    TransmitReason = transpondReason
                }, null, null, CommandType.StoredProcedure);

                if (!string.IsNullOrEmpty(assistant))
                {
                    var iid = Guid.NewGuid().ToString();
                    var usercodes = assistant.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    var sql = @"INSERT INTO Tb_HSPR_IncidentAssistApply(IID, CommID, CommName, IncidentID, IncidentNum,
                                    RoomSign,BigReasonName, BigReasonCode, SmallReasonName, SmalReasonCode, Title, AssistReason,
                                    AssistPeopleCount, WorkStartDate, UserCode, UserName, AuditState, AuditUser)
                                SELECT @IID,a.CommID,b.CommName,IncidentID,IncidentNum,
                                    c.RoomSign,d.TypeName AS BigTypeName,BigCorpTypeCode, e.TypeName AS FineTypeName,FineCorpTypeCode,
                                    @UserName+'提交并审核通过的报事编号为'+a.IncidentNum+'的协助申请',@AssistReason,
                                    @AssistPeopleCount,getdate(), @UserCode, @UserName, '已审核',@UserCode 
                                FROM Tb_HSPR_IncidentAccept a 
                                LEFT JOIN Tb_HSPR_Community b ON a.CommID=b.CommID
                                LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID
                                LEFT JOIN Tb_HSPR_CorpIncidentType d ON d.TypeCode=a.BigCorpTypeCode
                                LEFT JOIN Tb_HSPR_CorpIncidentType e ON e.TypeCode=a.FineCorpTypeCode
                                WHERE a.IncidentID=@IncidentID;";

                    // 插入协助信息
                    int j = conn.Execute(sql, new
                    {
                        IID = iid,
                        IncidentID = incidentId,
                        UserName = Global_Var.LoginUserName,
                        UserCode = Global_Var.LoginUserCode,
                        AssistReason = "报事转发时选择协助人",
                        AssistPeopleCount = usercodes.Count(),
                    });

                    // 记录协助人
                    foreach (var usercode in usercodes)
                    {
                        conn.Execute("INSERT INTO Tb_HSPR_IncidentAssistDetail(IID,AssistID,UserCode) VALUES(newid(),@IID,@UserCode)",
                            new { IID = iid, UserCode = usercode });
                    }
                }

                // 推送信息
                PMSIncidentPush.SynchPushIncidentTranspond(incidentId);

                // 禅道需求-9514
                if (Global_Var.CorpName.Contains("彰泰"))
                {
                    var crm = new IncidentAcceptCRM_ZT();
                    crm.IncidentCRM(incidentId.ToString(), CRMZTType.转派);
                }

                return JSONHelper.FromString(true, "转发成功");
            }
        }

        /// <summary>
        /// 废弃
        /// </summary>
        private string IncidentDiscard(DataRow row)
        {
            #region 基础数据校验
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))//报事ID
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }
            if (!row.Table.Columns.Contains("CloseSituation") || string.IsNullOrEmpty(row["CloseSituation"].AsString())) //废弃情况
            {
                return new ApiResult(false, "缺少参数CloseSituation").toJson();
            }

            string closeSituation = row["CloseSituation"].AsString();
            string incidentID = row["IncidentID"].AsString();
            #endregion

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var trans = conn.BeginTransaction();

                string deleteMan = conn.Query<string>(@"SELECT DeleteMan FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID AND IsDelete=1",
                    new { IncidentID = incidentID }, trans).FirstOrDefault();

                string result = null;
                if (!string.IsNullOrEmpty(deleteMan))
                {
                    trans.Commit();
                    result = new ApiResult(false, $"废弃失败，此单已被{deleteMan}废弃").toJson();
                    result = result.Insert(result.Length - 1, ",\"GoBack\":1");
                    return result;
                }

                DynamicParameters param = new DynamicParameters();
                param.Add("@IncidentID", incidentID);
                param.Add("@DeleteManCode", Global_Var.LoginUserCode);
                param.Add("@DeleteReasons", closeSituation);
                param.Add("@DeleteDate", DateTime.Now);
                param.Add("@DeleteMan", Global_Var.LoginUserName);
                conn.Execute("Proc_HSPR_IncidentAccept_CloseReason", param, trans, null, CommandType.StoredProcedure);

                trans.Commit();

                // 禅道需求-9514
                if (Global_Var.CorpName.Contains("彰泰"))
                {
                    var crm = new IncidentAcceptCRM_ZT();
                    crm.IncidentCRM(incidentID, CRMZTType.作废);
                }

                result = new ApiResult(true, "废弃成功").toJson();
                return result;
            }
        }

    }
}
