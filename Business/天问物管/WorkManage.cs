using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using MobileSoft.Common;
using System.Data;
using MobileSoft.DBUtility;
using System.Data.SqlClient;
using Dapper;
using Newtonsoft.Json;
using static Dapper.SqlMapper;

namespace Business
{
    public class WorkManage : PubInfo
    {
        private readonly string hrefString1 = "href=\"/ueditor/net/upload/";
        private readonly string hrefString2 = "href=\"/hm/m_main/oauploadfile/";
        private readonly string hrefString3 = "src=\"/ueditor/net/upload/";
        private readonly string hrefString4 = "href=\"/hm/m_main/jscript-ui/ueditor/net/upload/";

        public WorkManage()
        {
            base.Token = "20160503WorkManage";
        }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            //验证登录
            if (!new Login().isLogin(ref Trans))
                return;

            //项目，责任人，时间
            switch (Trans.Command)
            {
                case "GetAll"://获取全部工作
                    Trans.Result = GetAll(Row);
                    break;
                case "OaWork"://Tw研发中心工作计划
                    Trans.Result = OaWork(Row);
                    break;
                case "GetInternalNews":             // 获取内部信息
                    Trans.Result = GetInternalNews(Row);
                    break;
                case "GetUnreadNewsCount":          // 获取未读信息总数
                    Trans.Result = GetUnreadNewsCount(Row);
                    break;
                case "GetInternalNewsContent":      // 获取信息详情，并设置为已读
                    Trans.Result = GetInternalNewsContent(Row);
                    break;
            }
        }
        private string OaWork(DataRow row)
        {
            try
            {
                StringBuilder ConStr = new StringBuilder();
                ConStr.Append("Connect Timeout=60;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;");
                ConStr.Append("Pooling = true;");
                ConStr.AppendFormat(" data source = {0};", "125.64.16.10,8433");
                ConStr.AppendFormat(" initial catalog = {0};", "TwTask");
                ConStr.AppendFormat(" PWD={0};", "LF123SPoss");
                ConStr.Append("persist security info=False;");
                ConStr.AppendFormat(" user id = {0};packet size=4096", "LFUser");
                string connStr = ConStr.ToString();
                IDbConnection conn = new SqlConnection(connStr);
                List<OaWorkModel> list = conn.Query<OaWorkModel>("select * from Tb_TwTask order by Id desc").ToList();
                return new ApiResult(true, list).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.ToString()).toJson();
            }

        }

        private string GetAll(DataRow row)
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = GetZBGZ(row);
                dtable.Merge(GetGSGZ(row));
                dtable.Merge(GetXMGZ(row));
                DataTable newtable = dtable.DefaultView.ToTable(true);
                return JSONHelper.FromString(newtable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 读取内部信息
        /// </summary>
        private string GetInternalNews(DataRow row)
        {
            if (!row.Table.Columns.Contains("OrganCode") || string.IsNullOrEmpty(row["OrganCode"].AsString()))
            {
                return new ApiResult(false, "缺少参数OrganCode").toJson();
            }
            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].AsString()))
            {
                return new ApiResult(false, "缺少参数PageSize").toJson();
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].AsString()))
            {
                return new ApiResult(false, "缺少参数PageIndex").toJson();
            }
            if (!row.Table.Columns.Contains("Level") || string.IsNullOrEmpty(row["Level"].AsString()))
            {
                return new ApiResult(false, "缺少参数Level").toJson();
            }

            string organCode = row["OrganCode"].ToString();
            int pageSize = AppGlobal.StrToInt(row["PageSize"].AsString());
            int pageIndex = AppGlobal.StrToInt(row["PageIndex"].AsString());
            int level = AppGlobal.StrToInt(row["Level"].AsString());

            if (string.IsNullOrEmpty(organCode) || organCode.Length < 2)
            {
                return JSONHelper.FromString(false, "组织机构代码错误");
            }

            string key = "";
            if (row.Table.Columns.Contains("Key") && !string.IsNullOrEmpty(row["Key"].AsString()))
            {
                key = $" AND Title LIKE '%{row["Key"].ToString()}%'";
            }


            if (level == 0)
            {
                return GetGroupNews(pageSize, pageIndex, key);
            }
            else if (level == 1)
            {
                return GetCompanyNews(organCode, pageSize, pageIndex, key);
            }
            else
            {
                return GetCommunityNews(organCode, pageSize, pageIndex, key);
            }
        }

        // 读取总部内部信息
        private string GetGroupNews(int pageSize, int pageIndex, string key)
        {
            string sql = $@"SELECT IID,TypeName,replace(replace(Title,CHAR(10),''),CHAR(13),'') AS Title,IssueDate,
                            CASE PATINDEX('%{Global_Var.LoginUserCode}%', isnull(HaveReadUserCode,'')) 
                                WHEN 0 THEN 0 ELSE 1 END AS IsRead
                            FROM view_Common_CommonInfo_Filter
                            WHERE IsDelete=0 {key} AND OrganCode='01' AND  
                            ((ReadDepartCode IS NULL AND ReadUserCode IS NULL) 
                                OR patindex('%{Global_Var.LoginSortDepCode}%',ReadDepartCode)>0 
                                OR patindex('%{Global_Var.LoginUserCode}%',ReadUserCode)>0)";

            DataTable table = GetList(out int pageCount, out int counts, sql, pageIndex, pageSize, "IssueDate", 1, "IID",
                    PubConstant.hmWyglConnectionString).Tables[0];

            string result = JSONHelper.FromString(true, table);
            result = result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            return result;
        }

        // 读取公司内部信息
        private string GetCompanyNews(string organCode, int pageSize, int pageIndex, string key)
        {
            string where = "1=1";
            if (organCode.Length == 2)
            {
                where = "OrganCode like '01__'";
            }
            if (organCode.Length == 4)
            {
                where = string.Format("OrganCode='{0}'", organCode);
            }
            if (organCode.Length == 6)
            {
                where = string.Format(@"OrganCode=(SELECT TOP 1 substring(OrganCode,0,5) FROM Tb_HSPR_Community 
                                            WHERE isnull(IsDelete,0)=0 AND CommID={0})", organCode);
            }

            string sql = $@"SELECT IID,TypeName,replace(replace(Title,CHAR(10),''),CHAR(13),'') AS Title,IssueDate,
                                            CASE PATINDEX('%{Global_Var.LoginUserCode}%', isnull(HaveReadUserCode,'')) 
                                                WHEN 0 THEN 0 ELSE 1 END AS IsRead
                                        FROM view_Common_CommonInfo_Filter 
                                        WHERE IsDelete=0 {key} AND {where} AND 
                                        ((ReadDepartCode IS NULL AND ReadUserCode IS NULL) 
                                            OR patindex('%{Global_Var.LoginSortDepCode}%',ReadDepartCode)>0 
                                            OR patindex('%{Global_Var.LoginUserCode}%',ReadUserCode)>0)";

            DataTable table = GetList(out int pageCount, out int counts, sql, pageIndex, pageSize, "IssueDate", 1, "IID",
                    PubConstant.hmWyglConnectionString).Tables[0];

            string result = JSONHelper.FromString(true, table);
            result = result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            return result;
        }

        // 读取项目内部信息
        private string GetCommunityNews(string organCode, int pageSize, int pageIndex, string key)
        {
            string where = "1=1";
            if (organCode.Length == 2 || organCode.Length == 4)
            {
                where = string.Format("OrganCode like '{0}%'", organCode);
            }

            if (organCode.Length == 6)
            {
                where = string.Format("DepCode={0}", organCode);
            }

            string sql = $@"SELECT a.InfoId AS IID,a.InfoTypeName AS TypeName,
                            replace(replace(a.Title,CHAR(10),''),CHAR(13),'') AS Title,a.IssueDate,
                                (SELECT CASE count(0) WHEN 0 THEN 0 ELSE 1 END 
                                    FROM Tb_Common_CommunityService_ReadRecord b 
                                    WHERE b.InfoId=a.InfoId AND b.UserCode='{Global_Var.LoginUserCode}') AS IsRead
                            FROM Tb_Common_CommunityService a WHERE a.IsDelete=0 {key} AND {where}";

            DataTable table = GetList(out int pageCount, out int counts, sql, pageIndex, pageSize, "IssueDate", 1, "IID",
                    PubConstant.hmWyglConnectionString).Tables[0];

            string result = JSONHelper.FromString(true, table);
            result = result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            return result;
        }

        // 读取未读内部信息数量
        private string GetUnreadNewsCount(DataRow row)
        {
            if (!row.Table.Columns.Contains("OrganCode") || string.IsNullOrEmpty(row["OrganCode"].AsString()))
            {
                return new ApiResult(false, "缺少参数OrganCode").toJson();
            }

            string organCode = row["OrganCode"].ToString();
            GetUnreadNewsCount(organCode, out int count1, out int count2, out int count3);
            return new ApiResult(true, new { Group = count1, Company = count2, Community = count3 }).toJson();
        }

        public void GetUnreadNewsCount(string organCode, out int count1, out int count2, out int count3)
        {
            // 总公司
            string sql = @"SELECT COUNT(0) AS Count1 FROM Tb_Common_CommonInfo 
                            WHERE isnull(IsDelete,0)=0 AND OrganCode='01' AND patindex('%{0}%',HaveReadUserCode)= 0 AND
                            ((ReadDepartCode IS NULL AND ReadUserCode IS NULL) 
                                OR patindex('%{1}%',ReadDepartCode)>0
                                OR patindex('%{0}%',ReadUserCode)>0) ";

            // 分公司，项目查询条件
            string where1 = "1=1", where2 = "1=1";

            if (organCode.Length == 2)          // 定位到总公司
            {
                where1 = "OrganCode like '01__'";
            }
            else if (organCode.Length == 4)     // 定位到分公司
            {
                where1 = string.Format("OrganCode='{0}'", organCode);
                where2 = where1;
            }
            else
            {
                where1 = string.Format(@"OrganCode=(SELECT TOP 1 substring(OrganCode,0,5) FROM Tb_HSPR_Community 
                                            WHERE isnull(IsDelete,0)=0 AND CommID={0})", organCode);
                where2 = string.Format("DepCode='{0}'", organCode);
            }

            sql += @"SELECT COUNT(0) AS Count2 FROM Tb_Common_CommonInfo 
                            WHERE isnull(IsDelete,0)=0 AND {2} AND patindex('%{0}%',HaveReadUserCode)= 0 AND
                            ((ReadDepartCode IS NULL AND ReadUserCode IS NULL) 
                                OR patindex('%{1}%',ReadDepartCode)>0
                                OR patindex('%{0}%',ReadUserCode)>0);";
            sql += @"SELECT COUNT(0) AS Count3 FROM Tb_Common_CommunityService 
                        WHERE isnull(IsDelete,0)=0 AND InfoId NOT IN (SELECT InfoId FROM Tb_Common_CommunityService_ReadRecord b WHERE UserCode='{0}') AND {3};";

            sql = string.Format(sql, Global_Var.UserCode, Global_Var.LoginSortDepCode, where1, where2);

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                GridReader reader = conn.QueryMultiple(sql);
                count1 = reader.Read<int>().FirstOrDefault();
                count2 = reader.Read<int>().FirstOrDefault();
                count3 = reader.Read<int>().FirstOrDefault();
            }
        }

        // 获取内容详情，同时设置为已读
        private string GetInternalNewsContent(DataRow row)
        {
            if (!row.Table.Columns.Contains("IID") || string.IsNullOrEmpty(row["IID"].AsString()))
            {
                return new ApiResult(false, "缺少参数IID").toJson();
            }
            if (!row.Table.Columns.Contains("Level") || string.IsNullOrEmpty(row["Level"].AsString()))
            {
                return new ApiResult(false, "缺少参数Level").toJson();
            }

            string iid = row["IID"].ToString();
            int level = AppGlobal.StrToInt(row["Level"].AsString());

            string sql1 = null, sql2 = null;

            if (level == 0 || level == 1)
            {
                sql1 = @"SELECT Title,Content,UserName,IssueDate FROM Tb_Common_CommonInfo WHERE IID=@IID";
                sql2 = string.Format(@"IF NOT exists(SELECT * FROM Tb_Common_CommonInfo WHERE IID='{1}' AND patindex('%{0}%',HaveReadUserCode)>0)
                                        BEGIN
                                          UPDATE Tb_Common_CommonInfo SET HaveReadUserCode=isnull(HaveReadUserCode,'')+'[{0}]',
                                            HaveReadUserName=(isnull(HaveReadUserName,'')+(SELECT '['+UserName+']' FROM Tb_Sys_User WHERE UserCode='{0}'))
                                          WHERE IID='{1}'
                                        END", Global_Var.UserCode, iid);
            }
            else
            {
                sql1 = @"SELECT Title,Content,UserName,IssueDate FROM Tb_Common_CommunityService a LEFT JOIN Tb_Sys_User b ON a.UserCode=b.UserCode 
                            WHERE InfoId=@IID";
                sql2 = string.Format(@"IF NOT exists(SELECT * FROM Tb_Common_CommunityService_ReadRecord WHERE UserCode='{0}' AND InfoId='{1}')
                                        BEGIN
                                            INSERT INTO Tb_Common_CommunityService_ReadRecord(IID, InfoId, UserCode, ReadCount, LastReadDate)
                                              VALUES(newid(),'{1}','{0}',1,getdate());
                                        END
                                      ELSE 
                                        BEGIN 
                                            UPDATE Tb_Common_CommunityService_ReadRecord SET ReadCount=ReadCount+1,LastReadDate=getdate()
                                                WHERE UserCode='{0}' AND InfoId='{1}' 
                                        END", Global_Var.UserCode, iid);
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dynamic info = conn.Query(sql1, new { IID = iid }).FirstOrDefault();

                if (info != null)
                {
                    // 设置信息为已读
                    conn.Execute(sql2);

                    return new ApiResult(true, ConvertContent(info.Content, info.Title, info.UserName, info.IssueDate.ToString("yyyy-MM-dd HH:mm:ss"))).toJson();
                }
            }

            return new ApiResult(false, "未查询到该信息").toJson();
        }

        /// <summary>
        /// 获取公司工作
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private DataTable GetGSGZ(DataRow row)
        {
            string strSQL = " AND isnull(IsDelete,0)=0";

            // 2017年6月12日16:41:01，谭洋
            string organCode = row["OrganCode"].ToString();
            if (organCode.Length == 2)
            {
                strSQL = strSQL + " and OrganCode LIKE  '" + organCode + "__'";
            }

            if (organCode.Length == 4)
            {
                strSQL = strSQL + " and OrganCode='" + organCode + "'";
            }

            if (organCode.Length == 6)
            {
                strSQL = strSQL + " and OrganCode='" + organCode.Substring(0, 4) + "'";
            }

            strSQL = strSQL + " and (Type='0001' OR Type='0002')";

            strSQL = strSQL + " and ISNULL(ReadDepartCode,'" + Global_Var.LoginSortDepCode + "') LIKE '%" + Global_Var.LoginSortDepCode.ToString() + "%'";

            strSQL = strSQL + " and ISNULL(ReadUserCode,'" + Global_Var.LoginUserCode + "') LIKE '%" + Global_Var.LoginUserCode.ToString() + "%'";

            MobileSoft.BLL.Common.Bll_Tb_Common_CommonInfo B = new MobileSoft.BLL.Common.Bll_Tb_Common_CommonInfo();

            DataTable dTable = B.Common_CommonInfo_TopNum(10, strSQL);
            dTable.Columns.Add("BeanType", typeof(Int32));
            foreach (DataRow dr in dTable.Rows)
            {
                dr["BeanType"] = 2;

                string content = (dr["Content"] == null ? "" : dr["Content"].ToString());
                string title = (dr["Title"] == null ? "" : dr["Title"].ToString());
                string userName = (dr["UserName"] == null ? "" : dr["UserName"].ToString());
                string issueDate = (dr["IssueDate"] == null ? "" : dr["IssueDate"].ToString());

                dr["Content"] = ConvertContent(content, title, userName, issueDate);
            }
            return dTable;
        }

        /// <summary>
        /// 获取项目工作
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private DataTable GetXMGZ(DataRow row)
        {
            string strSQL = " AND isnull(IsDelete,0)=0";

            // 2017年6月12日16:41:01，谭洋
            string organCode = row["OrganCode"].ToString();
            if (organCode.Length == 2)
            {
                strSQL = strSQL + " and OrganCode LIKE  '" + organCode + "__'";
            }

            if (organCode.Length == 4)
            {
                strSQL = strSQL + " and OrganCode='" + organCode + "'";
            }

            if (organCode.Length == 6)
            {
                strSQL = strSQL + " and OrganCode='" + organCode.Substring(0, 4) + "'";
            }

            //strSQL = strSQL + " and (OrganCode LIKE '" + row["OrganCode"].ToString() + "%')";

            MobileSoft.BLL.Common.Bll_Tb_Common_CommunityService C = new MobileSoft.BLL.Common.Bll_Tb_Common_CommunityService();

            DataTable dTable = C.Common_CommunityService_TopFilter(10, strSQL);
            dTable.Columns.Add("BeanType", typeof(Int32));
            foreach (DataRow dr in dTable.Rows)
            {
                dr["BeanType"] = 3;

                string content = (dr["Content"] == null ? "" : dr["Content"].ToString());
                string title = (dr["Title"] == null ? "" : dr["Title"].ToString());
                string userName = (dr["UserName"] == null ? "" : dr["UserName"].ToString());
                string issueDate = (dr["IssueDate"] == null ? "" : dr["IssueDate"].ToString());

                dr["Content"] = ConvertContent(content, title, userName, issueDate);
            }

            return dTable;
        }

        /// <summary>
        /// 获取总部工作`
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private DataTable GetZBGZ(DataRow row)
        {
            string strSQL = " and OrganCode ='01' AND isnull(IsDelete,0)=0";

            strSQL = strSQL + " and (Type='0001' OR Type='0002')";

            strSQL = strSQL + " and ISNULL(ReadDepartCode,'" + Global_Var.LoginSortDepCode + "') LIKE '%" + Global_Var.LoginSortDepCode.ToString() + "%'";

            strSQL = strSQL + " and ISNULL(ReadUserCode,'" + Global_Var.LoginUserCode + "') LIKE '%" + Global_Var.LoginUserCode.ToString() + "%'";

            MobileSoft.BLL.Common.Bll_Tb_Common_CommonInfo A = new MobileSoft.BLL.Common.Bll_Tb_Common_CommonInfo();

            DataTable dTable = A.Common_CommonInfo_TopNum(10, strSQL);
            dTable.Columns.Add("BeanType", typeof(Int32));
            foreach (DataRow dr in dTable.Rows)
            {
                dr["BeanType"] = 1;

                string content = (dr["Content"] == null ? "" : dr["Content"].ToString());
                string title = (dr["Title"] == null ? "" : dr["Title"].ToString());
                string userName = (dr["UserName"] == null ? "" : dr["UserName"].ToString());
                string issueDate = (dr["IssueDate"] == null ? "" : dr["IssueDate"].ToString());

                dr["Content"] = ConvertContent(content, title, userName, issueDate);
            }

            return dTable;
        }

        private string ConvertContent(string content, string title, string author, string date)
        {
            content = (content ?? "").ToLower();

            if (content.Contains(hrefString1))
            {
                content = content.Replace(hrefString1, " href=\"#\" onclick=\"openFile(this);return false;\" x=\"/HM/M_Main/Jscript-Ui/UEditor/net/upload/");
            }

            if (content.Contains(hrefString2))
            {
                content = content.Replace(hrefString2, " href=\"#\" onclick=\"openFile(this);return false;\" x=\"/HM/M_Main/oauploadfile/");
            }

            if (content.Contains(hrefString3))
            {
                content = content.Replace(hrefString3, "src=\"/HM/M_Main/Jscript-Ui/UEditor/net/upload/");
            }

            if (content.Contains(hrefString4))
            {
                content = content.Replace(hrefString4, " href=\"#\" onclick=\"openFile(this);return false;\" x=\"/hm/m_main/jscript-ui/ueditor/net/upload/");
            }

            // 拼接网页
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\">");
            sb.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no\"/>");
            sb.AppendLine("<script type='text/javascript'>");
            sb.AppendLine(@"    function openFile(xxx) { 
                                    var file = xxx.getAttribute('x');
                                    var browser = browserType();
                                    if(browser == 'iphone') {
                                        window.location.href = ('objc:OpenFile:' + file);
                                    } else if(browser == 'android') {
                                        window.MobileSoft.showFile(file);
                                    }
                                }

                                function browserType() {
                                    var sUserAgent = navigator.userAgent.toLowerCase();
                                    var isIphone = sUserAgent.match(/iphone/i) == 'iphone';
                                    var isAndroid = sUserAgent.match(/android/i) == 'android';
                                    if (isIphone) return 'iphone';
                                    if (isAndroid) return 'android';
                                    return 'other';
                                }");
            sb.AppendLine("</script>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<div style='text-align:center;font-size:20;'>");
            sb.AppendLine("     <strong>" + title + "</strong>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div style='text-align:center;font-size:15;height:30px;line-height:30px;color:#767676;'>");
            sb.AppendLine(string.Format("{0}&nbsp;&nbsp;&nbsp;&nbsp;{1}", author, date));
            sb.AppendLine("</div>");
            sb.AppendLine("<br />");
            sb.AppendLine("<div>");
            sb.AppendLine(content);
            sb.AppendLine("</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
            return sb.ToString();
        }
    }
}
