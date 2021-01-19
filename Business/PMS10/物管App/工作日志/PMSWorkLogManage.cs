using Business.PMS10.报事.Models;
using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using static Dapper.SqlMapper;

namespace Business
{
    public class PMSWorkLogManage : PubInfo
    {
        public PMSWorkLogManage()
        {
            base.Token = "20191120PMSWorkLogManage";
        }

        public override void Operate(ref Transfer Trans)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            //验证登录
            if (!new Login().isLogin(ref Trans))
                return;

            //防止未捕获异常出现
            try
            {
                switch (Trans.Command)
                {
                    case "GetWorkLogStateOfYearMonth":          // 获取某月工作日志状态
                        Trans.Result = GetWorkLogStateOfYearMonth(Row);
                        break;
                    case "GetWorkLog":                          // 获取某日工作日志内容
                        Trans.Result = GetWorkLog(Row);
                        break;
                    case "GenerateDailyWorkLog":                // 生成日常工作
                        Trans.Result = GenerateDailyWorkLog(Row);
                        break;
                    case "SaveWorkLog":                         // 保存工作日志
                        Trans.Result = SaveWorkLog(Row);
                        break;
                    case "SubmitWorkLog":                       // 提交工作日志
                        Trans.Result = SubmitWorkLog(Row);
                        break;
                    case "GetMyReportObjects":                  // 获取我的汇报对象
                        Trans.Result = GetMyReportObjects();
                        break;
                    case "AddReportObjects":                    // 设置我的汇报对象
                        Trans.Result = AddReportObjects(Row);
                        break;
                    case "RemoveReportObjects":                 // 删除我的汇报对象
                        Trans.Result = RemoveReportObjects(Row);
                        break;
                    case "GetMyUnderlingObjects":               // 获取我的下属对象
                        Trans.Result = GetMyUnderlingObjects(Row);
                        break;
                    case "GetMyUnderlingObjectsWorkLog":        // 获取我的下属对象工作日志
                        Trans.Result = GetMyUnderlingObjectsWorkLog(Row);
                        break;
                    case "SaveIdea":                            // 添加批阅信息
                        Trans.Result = SaveIdea(Row);
                        break;
                    case "RemoveIdea":                          // 添加批阅信息
                        Trans.Result = RemoveIdea(Row);
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

        internal class ReportObjectModel
        {
            public string Id { get; set; }
            public string UserCode { get; set; }
            public string UserName { get; set; }
            public List<string> RoleInfo { get; set; }
        }

        /// <summary>
        /// 获取某月工作日志状态
        /// </summary>
        private string GetWorkLogStateOfYearMonth(DataRow row)
        {
            var date = DateTime.Now.ToString("yyyy-MM-01");
            var usercode = Global_Var.LoginUserCode;
            if (row.Table.Columns.Contains("YearMonth") && !string.IsNullOrEmpty(row["YearMonth"].AsString()))
            {
                date = row["YearMonth"].AsString().Substring(0, 7) + "-01";
            }
            if (row.Table.Columns.Contains("UserCode") && !string.IsNullOrEmpty(row["UserCode"].AsString()))
            {
                usercode = row["UserCode"].AsString();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var list = new Dictionary<string, int>();

                var sql = @"SELECT convert(varchar(10),RecordTime,120) AS RecordTime,IsSubmit FROM Tb_OAPublicWork_WorkRecord
                            WHERE UserCode=@UserCode AND convert(varchar(7),RecordTime,120)=convert(varchar(7),convert(datetime,@Date),120) 
                            ORDER BY RecordTime;";

                var data = conn.Query(sql, new { UserCode = usercode, Date = date }).ToList();
                if (data != null)
                {
                    var filterDate = Convert.ToDateTime(date);
                    var nextDate = Convert.ToDateTime(filterDate.AddMonths(1).ToString("yyyy-MM-01"));

                    var daysDiffer = (nextDate - filterDate).Days;
                    var currentDay = DateTime.Now.ToString("yyyy-MM-dd");

                    for (int i = 0; i < daysDiffer; i++)
                    {
                        var key = filterDate.ToString($"yyyy-MM-{(i + 1).ToString().PadLeft(2, '0')}");
                        var value = -1;

                        if (string.Compare(key, currentDay) > 0)
                        {
                            break;
                        }

                        var tmp = data.Find(obj => obj.RecordTime == key);
                        if (tmp != null)
                        {
                            value = (int)tmp.IsSubmit;
                        }

                        list.Add(key, value);
                    }
                }

                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 获取某日工作日志内容
        /// </summary>
        private string GetWorkLog(DataRow row)
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var usercode = Global_Var.LoginUserCode;
            if (row.Table.Columns.Contains("Date") && !string.IsNullOrEmpty(row["Date"].AsString()))
            {
                date = row["Date"].AsString();
            }
            if (row.Table.Columns.Contains("UserCode") && !string.IsNullOrEmpty(row["UserCode"].AsString()))
            {
                usercode = row["UserCode"].AsString();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT Id,DaysRecord,OtherRecord,IsSubmit,convert(varchar(10),AddTime,120) AS AddTime 
                            FROM Tb_OAPublicWork_WorkRecord
                            WHERE UserCode=@UserCode AND convert(varchar(10),RecordTime,120)=convert(varchar(10),convert(datetime,@Date),120)
                            ORDER BY RecordTime DESC;";

                var data = conn.Query(sql, new { UserCode = usercode, Date = date }).FirstOrDefault();
                if (data != null)
                {
                    sql = @"SELECT FileId AS Id,FilePath FROM Tb_OAPublicWork_WorkRecordFile 
                            WHERE WorkRecordId=@WorkRecordId;

                            SELECT a.Id,a.IdeaContent AS Content,a.AddTime,a.UserCode,b.UserName,
                                STUFF((SELECT '，'+RoleName FROM Tb_Sys_Role 
                                        WHERE RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole x WHERE x.UserCode=a.UserCode)
                                        FOR XML PATH('')),1,1,'') AS RoleName 
                            FROM Tb_OAPublicWork_WorkRecordIdea a
                            LEFT JOIN Tb_Sys_User b ON a.UserCode=b.UserCode
                            WHERE a.WorkRecordId=@WorkRecordId ORDER BY a.AddTime;";

                    var reader = conn.QueryMultiple(sql, new { WorkRecordId = data.Id });
                    data.Files = reader.Read();
                    data.Ideas = reader.Read();

                    return new ApiResult(true, data).toJson();
                }

                var dayOfYear = Convert.ToDateTime(date).DayOfYear;
                var currentDayOfYear = DateTime.Now.DayOfYear;
                if (dayOfYear > currentDayOfYear)
                {
                    return JSONHelper.FromString(false, "还没到日子呢");
                }
                else if (dayOfYear == currentDayOfYear)
                {
                    return JSONHelper.FromString(false, "今日还未记录工作日志，请尽快记录");
                }

                return JSONHelper.FromString(false, "当日未记录工作日志");
            }
        }

        /// <summary>
        /// 生成日常工作
        /// </summary>
        private string GenerateDailyWorkLog(DataRow row)
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            if (row.Table.Columns.Contains("Date") && !string.IsNullOrEmpty(row["Date"].AsString()))
            {
                date = row["Date"].AsString();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"DECLARE @Id varchar(36);
                            SELECT TOP 1 @Id=Id FROM Tb_OAPublicWork_WorkRecord
                            WHERE UserCode=@UserCode AND convert(varchar(10),RecordTime,120)=convert(varchar(10),convert(datetime,@Date),120)
                            ORDER BY AddTime DESC;

                            IF @Id IS NULL
                                BEGIN 
                                    INSERT INTO Tb_OAPublicWork_WorkRecord(Id,UserCode,DaysRecord,RecordTime,IsSubmit,AddTime,OperationTime)
                                    VALUES(newid(),@UserCode,@Content,convert(datetime,@Date),0,getdate(),getdate())
                                END 
                            ELSE 
                                BEGIN 
                                    UPDATE Tb_OAPublicWork_WorkRecord 
                                    SET DaysRecord=@Content,RecordTime=convert(datetime,@Date),OperationTime=getdate()
                                    WHERE Id=@Id                            
                                END;";

                conn.Open();
                var trans = conn.BeginTransaction();

                try
                {
                    var i = conn.Execute(sql, new { UserCode = Global_Var.LoginUserCode, Date = date, Content = "这是一键生成的日常工作" }, trans);
                    trans.Commit();

                    return JSONHelper.FromString(true, "保存成功");
                }
                catch (Exception)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, "保存失败");
                }
            }
        }

        /// <summary>
        /// 保存工作日志
        /// </summary>
        private string SaveWorkLog(DataRow row)
        {
            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].AsString()))
            {
                return JSONHelper.FromString(false, "缺少参数Content");
            }

            var content = row["Content"].AsString();

            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var files = new List<string>();

            if (row.Table.Columns.Contains("Date") && !string.IsNullOrEmpty(row["Date"].AsString()))
            {
                date = row["Date"].AsString();
            }
            if (row.Table.Columns.Contains("Files") && !string.IsNullOrEmpty(row["Files"].AsString()))
            {
                files = row["Files"].AsString().Split(',').ToList();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"DECLARE @Id varchar(36);
                            SELECT TOP 1 @Id=Id FROM Tb_OAPublicWork_WorkRecord
                            WHERE UserCode=@UserCode AND convert(varchar(10),RecordTime,120)=convert(varchar(10),convert(datetime,@Date),120)
                            ORDER BY AddTime DESC;

                            IF @Id IS NULL
                                BEGIN 
                                    INSERT INTO Tb_OAPublicWork_WorkRecord(Id,UserCode,OtherRecord,RecordTime,IsSubmit,AddTime,OperationTime)
                                    VALUES(newid(),@UserCode,@Content,convert(datetime,@Date),0,getdate(),getdate())
                                END 
                            ELSE 
                                BEGIN 
                                    UPDATE Tb_OAPublicWork_WorkRecord 
                                    SET OtherRecord=@Content,RecordTime=convert(datetime,@Date),OperationTime=getdate()
                                    WHERE Id=@Id                            
                                END;";

                foreach (var item in files)
                {
                    var ext = Path.GetExtension(item);
                    var filename = Path.GetFileName(item);

                    sql += $@"IF not exists(SELECT * FROM Tb_OAPublicWork_WorkRecordFile WHERE WorkRecordId=@Id AND FilePath='{item}')
                                BEGIN
                                    INSERT INTO Tb_OAPublicWork_WorkRecordFile(FileId,WorkRecordId,FilePath,FileExName,FileName,Soure,AddTime) 
                                    VALUES(newid(),@Id,'{item}','{ext}','{filename}','App',getdate());
                                END;";
                }

                conn.Open();
                var trans = conn.BeginTransaction();

                try
                {
                    var i = conn.Execute(sql, new { UserCode = Global_Var.LoginUserCode, Date = date, Content = content }, trans);
                    trans.Commit();

                    return JSONHelper.FromString(true, "保存成功");
                }
                catch (Exception)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, "保存失败");
                }
            }
        }

        /// <summary>
        /// 提交工作日志
        /// </summary>
        private string SubmitWorkLog(DataRow row)
        {
            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].AsString()))
            {
                return JSONHelper.FromString(false, "缺少参数Id");
            }

            var id = row["Id"].AsString();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"UPDATE Tb_OAPublicWork_WorkRecord SET IsSubmit=1 WHERE Id=@Id";
                var i = conn.Execute(sql, new { Id = id });
                if (i == 1)
                {
                    // 推送

                    return JSONHelper.FromString(true, "提交成功");
                }

                return JSONHelper.FromString(false, "提交失败");
            }
        }

        /// <summary>
        /// 获取我的汇报对象
        /// </summary>
        private string GetMyReportObjects()
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var reportObject = new List<ReportObjectModel>();

                var sql = @"SELECT a.Id,a.ToUserCode AS UserCode,b.UserName AS UserName,b.RoleName,
                                (SELECT STUFF(
                                    (SELECT '--' + Value FROM
                                        (
                                            SELECT TOP 2 * FROM SplitString(b.DepName,'--',1) ORDER BY Id DESC
                                        ) AS t ORDER BY Id
                                    FOR XML PATH('')), 1, 2, '')) AS DepName
                            FROM Tb_OAPublicWork_WorkRecordNoticeUser a
                            LEFT JOIN view_Sys_UserRole_Filter b ON a.ToUserCode=b.UserCode
                            WHERE MyUserCode=@UserCode";

                var data = conn.Query(sql, new { UserCode = Global_Var.LoginUserCode });
                foreach (var item in data)
                {
                    var tmp = reportObject.Find(obj => obj.UserCode == item.UserCode);
                    if (tmp == null)
                    {
                        tmp = new ReportObjectModel()
                        {
                            Id = item.Id,
                            UserCode = item.UserCode,
                            UserName = item.UserName,
                            RoleInfo = new List<string>()
                        };

                        reportObject.Add(tmp);
                    }

                    tmp.RoleInfo.Add(((item.DepName ?? "") + "，" + item.RoleName).ToString().Trim('，'));
                }

                return new ApiResult(true, reportObject).toJson();
            }
        }

        /// <summary>
        /// 设置我的汇报对象
        /// </summary>
        private string AddReportObjects(DataRow row)
        {
            if (!row.Table.Columns.Contains("ReportUsers") || string.IsNullOrEmpty(row["ReportUsers"].AsString()))
            {
                return new ApiResult(false, "缺少参数ReportUsers").toJson();
            }

            var reportUsers = row["ReportUsers"].AsString();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"INSERT INTO Tb_OAPublicWork_WorkRecordNoticeUser(MyUserCode,ToUserCode)
                            SELECT @UserCode,UserCode 
                            FROM Tb_Sys_User
                            WHERE UserCode IN(SELECT Value FROM SplitString(@ToUserCode,',',1))
                            AND UserCode NOT IN(SELECT ToUserCode FROM Tb_OAPublicWork_WorkRecordNoticeUser 
                            WHERE MyUserCode=@UserCode AND ToUserCode IN(SELECT Value FROM SplitString(@ToUserCode,',',1)))";

                var i = conn.Execute(sql, new { ToUserCode = reportUsers.Trim(','), UserCode = Global_Var.LoginUserCode });

                if (i > 0)
                    return JSONHelper.FromString(true, "添加成功");

                return JSONHelper.FromString(true, "添加失败");
            }
        }

        /// <summary>
        /// 删除我的汇报对象
        /// </summary>
        private string RemoveReportObjects(DataRow row)
        {
            if (!row.Table.Columns.Contains("Ids") || string.IsNullOrEmpty(row["Ids"].AsString()))
            {
                return new ApiResult(false, "缺少参数Ids").toJson();
            }

            var ids = row["Ids"].AsString();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"DELETE FROM Tb_OAPublicWork_WorkRecordNoticeUser 
                            WHERE Id IN(SELECT Value FROM SplitString(@Ids,',',1));";

                conn.Execute(sql, new { Ids = ids });

                return JSONHelper.FromString(true, "删除成功");
            }
        }

        /// <summary>
        /// 获取我的下属对象
        /// </summary>
        private string GetMyUnderlingObjects(DataRow row)
        {
            var key = default(string);
            if (row.Table.Columns.Contains("Key") && !string.IsNullOrEmpty(row["Key"].AsString()))
            {
                key = row["Key"].AsString();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var reportObject = new List<ReportObjectModel>();

                var sql = @"SELECT a.Id,a.MyUserCode AS UserCode,b.UserName AS UserName,b.RoleName,
                                (SELECT STUFF(
                                    (SELECT '--' + Value FROM
                                        (
                                            SELECT TOP 2 * FROM SplitString(b.DepName,'--',1) ORDER BY Id DESC
                                        ) AS t ORDER BY Id
                                    FOR XML PATH('')), 1, 2, '')) AS DepName
                            FROM Tb_OAPublicWork_WorkRecordNoticeUser a
                            LEFT JOIN view_Sys_UserRole_Filter b ON a.ToUserCode=b.UserCode
                            WHERE ToUserCode=@UserCode";

                if (!string.IsNullOrEmpty(key))
                {
                    sql += " AND (UserName LIKE @Key OR RoleName LIKE @Key OR DepName LIKE @Key)";
                }

                var data = conn.Query(sql, new { UserCode = Global_Var.LoginUserCode, Key = $"%{key}%" });
                foreach (var item in data)
                {
                    var tmp = reportObject.Find(obj => obj.UserCode == item.UserCode);
                    if (tmp == null)
                    {
                        tmp = new ReportObjectModel()
                        {
                            Id = item.Id,
                            UserCode = item.UserCode,
                            UserName = item.UserName,
                            RoleInfo = new List<string>()
                        };

                        reportObject.Add(tmp);
                    }

                    tmp.RoleInfo.Add(((item.DepName ?? "") + "，" + item.RoleName).ToString().Trim('，'));
                }

                return new ApiResult(true, reportObject).toJson();
            }
        }

        /// <summary>
        /// 获取我的下属对象工作日志
        /// </summary>
        private string GetMyUnderlingObjectsWorkLog(DataRow row)
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var usercode = default(string);
            var isIdea = -1;
            if (row.Table.Columns.Contains("Date") && !string.IsNullOrEmpty(row["Date"].AsString()))
            {
                date = row["Date"].AsString();
            }
            if (row.Table.Columns.Contains("UserCode") && !string.IsNullOrEmpty(row["UserCode"].AsString()))
            {
                usercode = row["UserCode"].AsString();
            }
            if (row.Table.Columns.Contains("IsIdea") && !string.IsNullOrEmpty(row["IsIdea"].AsString()))
            {
                isIdea = AppGlobal.StrToInt(row["IsIdea"].AsString());
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT * FROM
                            (
                                SELECT a.Id,a.DaysRecord,a.OtherRecord,a.RecordTime,a.UserCode,b.UserName,
                                    isnull(stuff((SELECT '、'+y.UserName+'('+convert(varchar(10),x.AddTime,120)+')'
                                        FROM Tb_OAPublicWork_WorkRecordIdea x
                                        LEFT JOIN Tb_Sys_User y ON x.UserCode=y.UserCode
                                        WHERE x.WorkRecordId=a.Id FOR XML PATH('')),1,1,''),'暂无批阅信息') AS IdeaInfo
                                FROM Tb_OAPublicWork_WorkRecord a
                                LEFT JOIN Tb_Sys_User b ON a.UserCode=b.UserCode 
                                WHERE a.UserCode IN
                                (
                                    SELECT MyUserCode FROM Tb_OAPublicWork_WorkRecordNoticeUser WHERE ToUserCode=@UserCode
                                )
                                AND convert(varchar(10),a.RecordTime,120)=convert(varchar(10),convert(datetime,@Date),120)
                                AND a.IsSubmit=1 
                            ) as t WHERE 1=1";

                if (isIdea != -1)
                {
                    if (isIdea == 0)
                        sql += " AND t.IdeaInfo='暂无批阅信息'";
                    else
                        sql += " AND t.IdeaInfo<>'暂无批阅信息'";
                }

                if (!string.IsNullOrEmpty(usercode))
                {
                    sql += " AND t.UserCode=@UnderlingUserCode";
                }

                sql += " ORDER BY t.RecordTime DESC";

                var data = conn.Query(sql, new { UserCode = Global_Var.LoginUserCode, Date = date, UnderlingUserCode = usercode });

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 添加批阅信息
        /// </summary>
        private string SaveIdea(DataRow row)
        {
            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].AsString()))
            {
                return new ApiResult(false, "缺少参数Id").toJson();
            }
            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].AsString()))
            {
                return new ApiResult(false, "缺少参数Content").toJson();
            }

            var id = row["Id"].AsString();
            var content = row["Content"].AsString();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"IF not exists(SELECT * FROM Tb_OAPublicWork_WorkRecordNoticeUser 
                                WHERE MyUserCode=(SELECT UserCode FROM Tb_OAPublicWork_WorkRecord WHERE Id=@WorkRecordId)
                                AND ToUserCode=@UserCode)
                                BEGIN
                                    RETURN;
                                END

                            DECLARE @Id varchar(36);
                            SELECT TOP 1 @Id=Id FROM Tb_OAPublicWork_WorkRecordIdea
                            WHERE UserCode=@UserCode AND WorkRecordId=@WorkRecordId
                            ORDER BY AddTime DESC;

                            IF @Id IS NULL
                                BEGIN
                                    INSERT INTO Tb_OAPublicWork_WorkRecordIdea(Id,WorkRecordId,UserCode,IdeaContent,AddTime)
                                    VALUES(newid(),@WorkRecordId,@UserCode,@Content,getdate())
                                END
                            ELSE
                                BEGIN
                                    UPDATE Tb_OAPublicWork_WorkRecordIdea
                                    SET IdeaContent=@Content,AddTime=getdate()
                                    WHERE Id=@Id
                                END;";

                conn.Open();
                var trans = conn.BeginTransaction();

                try
                {
                    var i = conn.Execute(sql, new { UserCode = Global_Var.LoginUserCode, WorkRecordId = id, Content = content }, trans);
                    if (i > 0)
                    {
                        trans.Commit();
                        return JSONHelper.FromString(true, "保存成功");
                    }

                    trans.Rollback();
                    return JSONHelper.FromString(false, "抱歉，您还不是当前员工设置的日志汇报人");
                }
                catch (Exception)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, "保存失败");
                }
            }
        }

        /// <summary>
        /// 删除批阅信息
        /// </summary>
        private string RemoveIdea(DataRow row)
        {
            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].AsString()))
            {
                return new ApiResult(false, "缺少参数Id").toJson();
            }

            var id = row["Id"].AsString();
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"DELETE FROM Tb_OAPublicWork_WorkRecordIdea WHERE Id=@Id AND UserCode=@UserCode;";

                if (conn.Execute(sql, new { Id = id, UserCode = Global_Var.LoginUserCode }) == 1)
                {
                    return JSONHelper.FromString(true, "删除成功");
                }

                return JSONHelper.FromString(false, "删除失败");
            }
        }
    }
}
