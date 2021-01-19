using Common;
using Dapper;
using MobileSoft.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Business
{
    public class WorkDiary : PubInfo
    {
        public WorkDiary()
        {
            base.Token = "20180822WorkDiary";
        }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            //验证登录
            if (!new Login().isLogin(ref Trans))
                return;

            switch (Trans.Command)
            {
                case "GetWorkDiary":
                    Trans.Result = GetWorkDiary(Row);
                    break;
                case "WorkDiaryInsUpdate":
                    Trans.Result = WorkDiaryInsUpdate(Row);
                    break;
                case "GetSubordinateList":
                    Trans.Result = GetSubordinateList(Row);
                    break;
                case "GetReportUserList":
                    Trans.Result = GetReportUserList(Row);
                    break;
                case "AddReportUser":
                    Trans.Result = AddReportUser(Row);
                    break;
                case "DeleteReportUser":
                    Trans.Result = DeleteReportUser(Row);
                    break;
            }
        }

        /// <summary>
        /// 获取工作日志
        /// </summary>
        private string GetWorkDiary(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserCode") || string.IsNullOrEmpty(row["UserCode"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编号不能为空");
            }

            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }

            if (!row.Table.Columns.Contains("Date") || string.IsNullOrEmpty(row["Date"].ToString()))
            {
                return JSONHelper.FromString(false, "日志日期不能为空");
            }

            string corpId = row["CorpID"].ToString();

            //构建链接字符串
            string strcon = "";
            bool bl = GetDBServerPathWithCorpID(corpId, out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            using (IDbConnection con = new SqlConnection(strcon))
            {
                DataSet ds = con.ExecuteReader(@"SELECT * FROM view_OAPublicWork_WorkRecord_Filter WHERE WriteDate=@Date", new { Date = row["Date"].ToString() }).ToDataSet();
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"Result\":\"true\",");
                sb.Append("\"data\":");
                if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                {
                    sb.Append("[]");
                }
                else
                {
                    int i = 0;
                    sb.Append("[");
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        if (i > 0)
                        {
                            sb.Append(",");
                        }

                        string infoId = item["InfoId"].ToString();

                        string sstr = JSONHelper.FromDataRow(item);
                        string ss = sstr.ToString().Substring(0, sstr.ToString().Length - 1);
                        sb.Append(ss);
                        sb.Append(",");
                        sb.Append("\"Idea\":");
                        sb.Append("[");
                        int j = 0;
                        IDbConnection con2 = new SqlConnection(strcon);
                        DataSet ds_Property = con2.ExecuteReader(@"SELECT * FROM Tb_OAPublicWork_WorkRecordIdea WHERE WorkRecord_InfoId=@InfoId", new { InfoId = infoId }).ToDataSet();
                        foreach (DataRow dr in ds_Property.Tables[0].Rows)
                        {
                            if (j == 0)
                            {
                                sb.Append(JSONHelper.FromDataRow(dr));
                            }
                            else
                            {
                                sb.Append(",");
                                sb.Append(JSONHelper.FromDataRow(dr));
                            }
                            j++;
                        }

                        sb.Append("]");
                        sb.Append("}");

                        i++;
                    }
                    sb.Append("]");
                }

                sb.Append("}");

                return sb.ToString();
            } 
        }

        /// <summary>
        /// 新增或修改工作日志
        /// </summary>
        private string WorkDiaryInsUpdate(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserCode") || string.IsNullOrEmpty(row["UserCode"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编号不能为空");
            }

            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }

            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
            {
                return JSONHelper.FromString(false, "日志内容不能为空");
            }

            if (!row.Table.Columns.Contains("Ex2") || string.IsNullOrEmpty(row["Ex2"].ToString()))
            {
                return JSONHelper.FromString(false, "完成工时不能为空");
            }

            if (!row.Table.Columns.Contains("Date") || string.IsNullOrEmpty(row["Date"].ToString()))
            {
                return JSONHelper.FromString(false, "日期不能为空");
            }

            string corpId = row["CorpID"].ToString();

            //构建链接字符串
            string strcon = "";
            bool bl = GetDBServerPathWithCorpID(corpId, out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            // 日志id，如果有，则更新
            string InfoId = null;
            if (row.Table.Columns.Contains("InfoId") && !string.IsNullOrEmpty(row["InfoId"].ToString()))
            {
                InfoId = row["InfoId"].ToString();
            }

            string Ex1 = null;
            if (row.Table.Columns.Contains("Ex1") && !string.IsNullOrEmpty(row["Ex1"].ToString()))
            {
                Ex1 = row["Ex1"].ToString();
            }

            using (IDbConnection con = new SqlConnection(strcon))
            {
                if (InfoId != null)
                {
                    con.Execute(@"UPDATE Tb_OAPublicWork_WorkRecord SET NowRecord=@NowRecord, Ex1=@Ex1, Ex2=@Ex2 WHERE InfoId=@InfoId", 
                        new
                        {
                            NowRecord = row["Content"].ToString(),
                            Ex1 = Ex1,
                            Ex2 = row["Ex2"].ToString(),
                            InfoId = InfoId
                        });
                }
                else
                {

                    con.Execute("Proc_OAPublicWork_WorkRecord_Insert",
                        new
                        {
                            NowRecord = row["Content"].ToString(),
                            AfterRecord = "",
                            WriteDate = row["Date"].ToString(),
                            Ex1 = Ex1,
                            Ex2 = row["Ex2"].ToString(),
                            Ex3 = "",
                            Ex4 = "",
                            Ex5 = "",
                            Ex6 = "",
                            UserCode = row["UserCode"].ToString()
                        }, null, null, CommandType.StoredProcedure);
                }
                return JSONHelper.FromString(true, "操作成功");
            }
        }

        /// <summary>
        /// 获取下属列表
        /// </summary>
        private string GetSubordinateList(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserCode") || string.IsNullOrEmpty(row["UserCode"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编号不能为空");
            }

            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }

            string corpId = row["CorpID"].ToString();

            //构建链接字符串
            string strcon = "";
            bool bl = GetDBServerPathWithCorpID(corpId, out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            using (IDbConnection con = new SqlConnection(strcon))
            {
               DataTable dt = con.ExecuteReader(@"SELECT b.UserCode,b.UserName,b.DepCode, 
                            DepName=(SELECT top 1 DepName FROM Tb_Sys_Department WHERE DepCode=b.DepCode)
                            FROM Tb_OAPublicWork_WorkRecordNoticeUser a LEFT JOIN Tb_Sys_User b ON a.MyUserCode=b.UserCode
                            WHERE ToUserCode=@ToUserCode", new { ToUserCode = row["UserCode"].ToString() }).ToDataSet().Tables[0];
                return JSONHelper.FromString(dt);
            }
        }

        /// <summary>
        /// 获取汇报对象
        /// </summary>
        private string GetReportUserList(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserCode") || string.IsNullOrEmpty(row["UserCode"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编号不能为空");
            }

            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }

            string corpId = row["CorpID"].ToString();

            //构建链接字符串
            string strcon = "";
            bool bl = GetDBServerPathWithCorpID(corpId, out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            using (IDbConnection con = new SqlConnection(strcon))
            {
                DataTable dt = con.ExecuteReader(@"SELECT UserCode,UserName FROM Tb_Sys_User WHERE UserCode IN (SELECT DISTINCT ToUserCode FROM Tb_OAPublicWork_WorkRecordNoticeUser WHERE MyUserCode=@UserCode)", new { UserCode = row["UserCode"].ToString() }).ToDataSet().Tables[0];
                return JSONHelper.FromString(dt);
            }
        }

        /// <summary>
        /// 添加汇报对象
        /// </summary>
        private string AddReportUser(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserCode") || string.IsNullOrEmpty(row["UserCode"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编号不能为空");
            }

            if (!row.Table.Columns.Contains("ToUserCode") || string.IsNullOrEmpty(row["ToUserCode"].ToString()))
            {
                return JSONHelper.FromString(false, "汇报对象不能为空");
            }

            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }

            string corpId = row["CorpID"].ToString();

            //构建链接字符串
            string strcon = "";
            bool bl = GetDBServerPathWithCorpID(corpId, out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            using (IDbConnection con = new SqlConnection(strcon))
            {
                con.Execute("Proc_OAPublicWork_WorkRecordNoticeUser_Insert", 
                    new
                    {
                        MyUserCode = row["UserCode"].ToString(),
                        ToUserCode = row["ToUserCode"].ToString()
                    }, null, null, CommandType.StoredProcedure);
                return JSONHelper.FromString(true, "操作成功");
            }
        }

        /// <summary>
        /// 删除汇报对象
        /// </summary>
        private string DeleteReportUser(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserCode") || string.IsNullOrEmpty(row["UserCode"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编号不能为空");
            }

            if (!row.Table.Columns.Contains("ToUserCode") || string.IsNullOrEmpty(row["ToUserCode"].ToString()))
            {
                return JSONHelper.FromString(false, "汇报对象不能为空");
            }

            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }

            string corpId = row["CorpID"].ToString();

            //构建链接字符串
            string strcon = "";
            bool bl = GetDBServerPathWithCorpID(corpId, out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            using (IDbConnection con = new SqlConnection(strcon))
            {
                con.Execute(@"DELETE FROM Tb_OAPublicWork_WorkRecordNoticeUser WHERE MyUserCode=@MyUserCode AND ToUserCode=@ToUserCode",
                    new
                    {
                        MyUserCode = row["UserCode"].ToString(),
                        ToUserCode = row["ToUserCode"].ToString()
                    });
                return JSONHelper.FromString(true, "操作成功");
            }
        }
    }
}
