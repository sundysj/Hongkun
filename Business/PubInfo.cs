using Dapper;
using log4net;
using MobileSoft.BLL.Common;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Common;
using MobileSoft.Model.Unified;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using TaiHeSmsSdk;
using allinpay.utils;
using Business.WChat2020.Model;

namespace Business
{
    public abstract class PubInfo
    {
        protected static ILog log;
        public PubInfo()
        {

        }

        #region 抽象公共属性

        #endregion

        #region 抽象公共接口
        //通用令牌号
        public string Token = "8E41C3916E85";
        public abstract void Operate(ref Common.Transfer Trans);
        #endregion

        #region XML转换成DataTable对象
        public DataSet XmlToDataSet(string xmlStr)
        {
            if (!string.IsNullOrEmpty(xmlStr))
            {
                StringReader StrStream = null;
                XmlTextReader Xmlrdr = null;
                try
                {
                    DataSet ds = new DataSet();
                    //读取字符串中的信息  
                    StrStream = new StringReader(xmlStr);
                    //获取StrStream中的数据  
                    Xmlrdr = new XmlTextReader(StrStream);
                    //ds获取Xmlrdr中的数据                  
                    ds.ReadXml(Xmlrdr);
                    return ds;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    //释放资源  
                    if (Xmlrdr != null)
                    {
                        Xmlrdr.Close();
                        StrStream.Close();
                        StrStream.Dispose();
                    }
                }
            }
            else
            {
                return null;
            }
        }
        public DataTable XmlToDatatTable(string xmlStr)
        {
            if (!string.IsNullOrEmpty(xmlStr))
            {
                return XmlToDataSet(xmlStr).Tables[0];
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region DataTable对象转换成XML字符串
        public string DataToXml(DataTable dt, string TableName, string RolwName)
        {
            StringBuilder xmlStr = new StringBuilder();

            int nCount = 0;

            nCount = dt.Columns.Count;

            //添加列记录
            xmlStr.Append("<" + TableName + ">");

            foreach (DataRow Row in dt.Rows)
            {
                xmlStr.Append("<Row ");

                for (int i = 0; i < nCount; i++)
                {
                    xmlStr.Append(dt.Columns[i].ColumnName.ToString() + "=\"" + Row[i].ToString() + "\" ");
                }
                xmlStr.Append("/>");
            }
            xmlStr.Append("</" + TableName + ">");
            return xmlStr.ToString();
        }

        #endregion
        
        public bool GetDBServerPath(string CommID, out string DBPath)
        {
            if (string.IsNullOrEmpty(Global_Var.LoginCorpID) == false)
            {
                return GetDBServerPathWithCorpID(Global_Var.LoginCorpID, out DBPath);
            }

            bool bl = true;
            DBPath = "";
            try
            {
                IDbConnection Connectionstr = new SqlConnection(PubConstant.tw2bsConnectionString);
                string strSql = "select CorpID from Tb_HSPR_community where IsNull( Isdelete,0)=0 and commid='" + CommID + "'";
                List<string> list = Connectionstr.Query<string>(strSql).ToList<string>();
                //string corpId = Connectionstr.ExecuteScalar(strSql, null, null, null, CommandType.Text).ToString();
                if (list.Count <= 0)
                {
                    bl = false;
                    DBPath = "公司不存在";
                    return bl;
                }
                //Connectionstr.Dispose();
                //Connectionstr = new SqlConnection(PubConstant.PublicConnectionString);
                DataTable dt = Connectionstr.ExecuteReader("select DBServer,DBName,DBPwd,DBUser from Tb_System_Corp where IsNull( Isdelete,0)=0 and CorpID='" + list[0] + "'", null, null, null, CommandType.Text).ToDataSet().Tables[0];
                Connectionstr.Dispose();
                if (dt.Rows.Count <= 0)
                {
                    bl = false;
                    DBPath = "公司不存在";
                    return bl;
                }
                DataRow dr = dt.Rows[0];
                StringBuilder ConStr = new StringBuilder();
                ConStr.Append("Connect Timeout=60;Connection Lifetime=60;Max Pool Size=1000;Min Pool Size=0;");
                ConStr.Append("Pooling = true;");
                ConStr.AppendFormat(" data source = {0};", dr["DBServer"]);
                ConStr.AppendFormat(" initial catalog = {0};", dr["DBName"]);
                ConStr.AppendFormat(" PWD={0};", dr["DBPwd"]);
                ConStr.Append("persist security info=False;");
                ConStr.AppendFormat(" user id = {0};packet size=4096", dr["DBUser"]);
                DBPath = ConStr.ToString();
            }
            catch (Exception ex)
            {
                bl = false;
                DBPath = ex.Message;
            }

            return bl;
        }

        public bool GetDBServerPathWithCorpID(string CorpID, out string DBPath)
        {
            DBPath = "公司不存在";
            try
            {
                using (IDbConnection Connectionstr = new SqlConnection(PubConstant.tw2bsConnectionString))
                {
                    string strSql = "SELECT DBServer,DBName,DBPwd,DBUser FROM Tb_System_Corp where IsNull(Isdelete,0)=0 and CorpID=" + CorpID;
                    DataTable dt = Connectionstr.ExecuteReader(strSql, null, null, null, CommandType.Text).ToDataSet().Tables[0];

                    if (dt.Rows.Count == 0)
                    {
                        return false;
                    }

                    DataRow row = dt.Rows[0];

                    StringBuilder ConStr = new StringBuilder();
                    ConStr.Append("Connect Timeout=60;Connection Lifetime=60;Max Pool Size=1000;Min Pool Size=50;");
                    ConStr.Append("Pooling = true;");
                    ConStr.AppendFormat(" data source = {0};", row["DBServer"]);
                    ConStr.AppendFormat(" initial catalog = {0};", row["DBName"]);
                    ConStr.AppendFormat(" PWD={0};", row["DBPwd"]);
                    ConStr.Append("persist security info=False;");
                    ConStr.AppendFormat(" user id = {0};packet size=4096", row["DBUser"]);
                    DBPath = ConStr.ToString();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool GetDBServerPath(out string DBPath)
        {
            try
            {
                StringBuilder ConStr = new StringBuilder();
                ConStr.Append("Connect Timeout=60;Connection Lifetime=60;Max Pool Size=1000;Min Pool Size=50;");
                ConStr.Append("Pooling = true;");
                ConStr.AppendFormat(" data source = {0};", GetConfigValByKey("DBServer"));
                ConStr.AppendFormat(" initial catalog = {0};", GetConfigValByKey("DBName"));
                ConStr.AppendFormat(" PWD={0};", GetConfigValByKey("DBPwd"));
                ConStr.Append("persist security info=False;");
                ConStr.AppendFormat(" user id = {0};packet size=4096", GetConfigValByKey("DBUser"));
                DBPath = ConStr.ToString();
                return true;
            }
            catch (Exception)
            {
                DBPath = "flase";
                return false;
            }
        }
        private string GetConfigValByKey(string key)
        {
            string val = System.Configuration.ConfigurationManager.AppSettings[key];
            return val;
        }

        public static string GetDBServerPath(string CommID)
        {
            try
            {
                string tw2bsConnStr = AppGlobal.GetConnectionString("SQLConnection");
                using (IDbConnection conn = new SqlConnection(tw2bsConnStr))
                {
                    string CorpID = conn.QueryFirstOrDefault<string>("SELECT CorpID FROM Tb_HSPR_community WHERE ISNULL(IsDelete,0) = 0 AND CommID = @CommID", new { CommID });
                    if (string.IsNullOrEmpty(CorpID))
                    {
                        return "";
                    }
                    dynamic CorpInfo = conn.QueryFirstOrDefault<dynamic>("SELECT  DBServer,DBName,DBPwd,DBUser FROM Tb_System_Corp WHERE ISNULL(IsDelete,0) = 0 AND CorpID = @CorpID", new { CorpID });
                    if(null == CorpInfo)
                    {
                        return "";
                    }
                    return $"Max Pool Size=2048;Min Pool Size=0;Pooling=true;Data Source={Convert.ToString(CorpInfo.DBServer)};Initial Catalog={Convert.ToString(CorpInfo.DBName)};User ID={Convert.ToString(CorpInfo.DBUser)};Password={Convert.ToString(CorpInfo.DBPwd)}";
                }
            }
            catch (Exception ex)
            {
                PubInfo.GetLog().Debug("GetDBServerPath_err=" + ex.Message);
                return "";
            }
        }

        public static string GetDBServerPath(string Net, string CommID)
        {
            try
            {
                string tw2bsConnStr = Global_Fun.Tw2bsConnectionString(Net);
                using (IDbConnection conn = new SqlConnection(tw2bsConnStr))
                {
                    string CorpID = conn.QueryFirstOrDefault<string>("SELECT CorpID FROM Tb_HSPR_community WHERE ISNULL(IsDelete,0) = 0 AND CommID = @CommID", new { CommID });
                    if (string.IsNullOrEmpty(CorpID))
                    {
                        return "";
                    }
                    dynamic CorpInfo = conn.QueryFirstOrDefault<dynamic>("SELECT  DBServer,DBName,DBPwd,DBUser FROM Tb_System_Corp WHERE ISNULL(IsDelete,0) = 0 AND CorpID = @CorpID", new { CorpID });
                    if (null == CorpInfo)
                    {
                        return "";
                    }
                    return $"Max Pool Size=2048;Min Pool Size=0;Pooling=true;Data Source={Convert.ToString(CorpInfo.DBServer)};Initial Catalog={Convert.ToString(CorpInfo.DBName)};User ID={Convert.ToString(CorpInfo.DBUser)};Password={Convert.ToString(CorpInfo.DBPwd)}";
                }
            }
            catch (Exception ex)
            {
                PubInfo.GetLog().Debug("GetDBServerPath_err=" + ex.Message);
                return "";
            }
        }

        /// <summary>
        /// 动态构建链接字符串
        /// </summary>
        /// <param name="Community"></param>
        public void GetConnectionString(Tb_Community Community)
        {
            StringBuilder ConStr = new StringBuilder();
            ConStr.Append("Connect Timeout=60;Connection Lifetime=60;Max Pool Size=1000;Min Pool Size=50;");
            ConStr.Append("Pooling = true;");
            ConStr.AppendFormat(" data source = {0};", Community.DBServer);
            ConStr.AppendFormat(" initial catalog = {0};", Community.DBName);
            ConStr.AppendFormat(" Password={0};", Community.DBPwd);
            ConStr.Append("persist security info=False;");
            ConStr.AppendFormat(" user id = {0};packet size=4096", Community.DBUser);

            Global_Var.CorpSQLConnstr = ConStr.ToString();
            Global_Var.LoginSQLConnStr = ConStr.ToString();
        }

        public string GetConnectionStringStr(Tb_Community Community)
        {
            StringBuilder ConStr = new StringBuilder();
            ConStr.Append("Connect Timeout=60;Connection Lifetime=60;Max Pool Size=1000;Min Pool Size=50;");
            ConStr.Append("Pooling=true;");
            ConStr.AppendFormat("Data Source={0};", Community.DBServer);
            ConStr.AppendFormat("Initial Catalog={0};", Community.DBName);
            ConStr.AppendFormat("Password={0};", Community.DBPwd);
            ConStr.Append("persist security info=False;");
            ConStr.AppendFormat("User ID={0};packet size=4096;MultipleActiveResultSets=true;", Community.DBUser);

            return ConStr.ToString();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="PageCount">总页数</param>
        /// <param name="Counts">总条数</param>
        /// <param name="StrCondition">执行语句</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">每页多少条</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">升序/降序</param>
        /// <param name="ID">主键</param>
        /// <returns></returns>
        public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort, string ID, string constr)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@FldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@FldSort", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Sort", SqlDbType.Int),
                    new SqlParameter("@StrCondition", SqlDbType.VarChar, 8000),
                    new SqlParameter("@Id", SqlDbType.VarChar, 50),
                    new SqlParameter("@PageCount", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    new SqlParameter("@Counts", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    };
            parameters[0].Value = "*";
            parameters[1].Value = PageSize;
            parameters[2].Value = PageIndex;
            parameters[3].Value = SortField;
            parameters[4].Value = Sort;
            parameters[5].Value = StrCondition;
            parameters[6].Value = ID;
            DataSet Ds = new DbHelperSQLP(constr).RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = AppGlobal.StrToInt(parameters[7].Value.ToString());
            Counts = AppGlobal.StrToInt(parameters[8].Value.ToString());
            return Ds;
        }


        public IEnumerable<T> GetListDapper<T>(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort, string ID, IDbConnection conn, IDbTransaction trans = null) 
        {
            PageCount = 0;
            Counts = 0;

            if (conn == null)
            {
                return null;
            }

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FldName", "*");
            parameters.Add("@PageSize", PageSize);
            parameters.Add("@PageIndex", PageIndex);
            parameters.Add("@FldSort", SortField);
            parameters.Add("@Sort", Sort);
            parameters.Add("@StrCondition", StrCondition);
            parameters.Add("@Id", ID);
            parameters.Add("@PageCount", 0, DbType.Int32, ParameterDirection.Output);
            parameters.Add("@Counts", 0, DbType.Int32, ParameterDirection.Output);
            
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            var reader = conn.QueryMultiple("Proc_System_TurnPage", parameters, trans, null, CommandType.StoredProcedure);
            var resultSet = reader.Read<T>();
            Counts = reader.Read<int>().FirstOrDefault();

            return resultSet;
        }

        public IEnumerable<dynamic> GetListDapper(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort, string ID, IDbConnection conn, IDbTransaction trans = null)
        {
            PageCount = 0;
            Counts = 0;

            if (conn == null)
            {
                return null;
            }

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FldName", "*");
            parameters.Add("@PageSize", PageSize);
            parameters.Add("@PageIndex", PageIndex);
            parameters.Add("@FldSort", SortField);
            parameters.Add("@Sort", Sort);
            parameters.Add("@StrCondition", StrCondition);
            parameters.Add("@Id", ID);
            parameters.Add("@PageCount", 0, DbType.Int32, ParameterDirection.Output);
            parameters.Add("@Counts", 0, DbType.Int32, ParameterDirection.Output);

            //if (conn.State == ConnectionState.Closed)
            //{
            //    conn.Open();
            //}

            var reader = conn.QueryMultiple("Proc_System_TurnPage", parameters, trans, null, CommandType.StoredProcedure);
            var resultSet = reader.Read();
            Counts = reader.Read<int>().FirstOrDefault();

            return resultSet;
        }

        private bool CheckValidationResult(
            Object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors
        )
        {
            if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNotAvailable) != SslPolicyErrors.RemoteCertificateNotAvailable)
                return true;
            throw new Exception("SSL验证失败");
        }

        #region 泰禾
        protected int SendShortMessage(string mobile, string message, out string errorMessage, int corpid = 1000)
        {
            int Result = 0;

            if (corpid == 1970)
            {
                TaiHeSmsSdk.ITaiHeSmsSdk taiHeSmsSdk = new ITaiHeSmsSdk();
                message = "【泰禾家】" + message;

                if (taiHeSmsSdk.sendSms("taihejia", "taihe@123", mobile, message, out string msg))
                {
                    Result = 0;
                }
                else
                {
                    Result = 99;
                }
            }
            else
            {
                Tb_Sms_Account smsModel = SmsInfo.GetSms_Account();
                message = message + smsModel.Sign;
                //Result = Common.Sms.Send(smsModel.SmsAccount, smsModel.SmsPwd, mobile, message, "", "");
                Result = Common.Sms.Send_v2(smsModel.SmsUserId, smsModel.SmsAccount, smsModel.SmsPwd, mobile, message, out string strErrMsg);
            }

            Tb_SendMessageRecord m = new Tb_SendMessageRecord();

            switch (Result)
            {
                case 0:
                    errorMessage = "发送成功";
                    try
                    {
                        //记录短信
                        m = new Bll_Tb_SendMessageRecord().Add(mobile, message, Guid.NewGuid().ToString(), "业主App", "");
                    }
                    catch (Exception ex)
                    {
                    }
                    break;
                case -4:
                    errorMessage = "手机号码格式不正确";
                    break;
                default:
                    errorMessage = "发送失败：" + Result;
                    break;
            }
            //修改状态
            m.SendState = Result.ToString();
            //重写短信记录状态
            new Bll_Tb_SendMessageRecord().Update(m);

            return Result;
        }

        #endregion
        #region 发送http
        public string SendHttp(string url, string method, string content)
        {
            #region 发送
            HttpWebRequest request = null;
            byte[] postData;
            Uri uri = new Uri(url);
            if (uri.Scheme == "https")
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);
            }
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);
            HttpWebRequest.DefaultCachePolicy = policy;

            request = (HttpWebRequest)WebRequest.Create(uri);
            request.AllowAutoRedirect = false;
            request.AllowWriteStreamBuffering = false;
            request.Method = method;

            postData = Encoding.GetEncoding("utf-8").GetBytes(content);

            //request.ContentType = "application/x-www-form-urlencoded";
            request.ContentType = "text/plain;charset = utf-8"; //request.ContentType = "text/plain";
            request.ContentLength = postData.Length;
            request.KeepAlive = false;

            Stream reqStream = request.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();
            #endregion

            #region 响应
            string respText = "";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();

            MemoryStream ms = new MemoryStream();
            byte[] buf = new byte[4096];
            int count;
            while ((count = resStream.Read(buf, 0, buf.Length)) > 0)
            {
                ms.Write(buf, 0, count);
            }
            resStream.Close();
            respText = Encoding.GetEncoding("utf-8").GetString(ms.ToArray());
            #endregion

            return respText;
        }
        #endregion



        #region "碧桂园"

        public string CheckParam(DataRow dr, string[,] arrayParam)
        {
            string msg = string.Empty, value = string.Empty;
            for (int i = 0; i < arrayParam.GetLength(0); i++)
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    break;
                }
                if (dr.Table.Columns.Contains(arrayParam[i, 0]))
                {
                    if (arrayParam[i, 2] == "NotNull")
                    {
                        value = dr[arrayParam[i, 0]].ToString();
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            msg = $"{arrayParam[i, 1]} 参数不能为空";
                        }
                        else
                        {
                            if (SqlFilter(value))
                            {
                                msg = $"{arrayParam[i, 1]} 参数含有非法字符";
                            }
                        }
                    }
                }
                else
                {
                    msg = $"{arrayParam[i, 1]} 参数不存在";
                }
            }
            return msg;

        }

        #region 检查危险字符

        /// <summary>
        /// 检查Sql危险字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns>存在返回true,不存在返回false</returns>
        private static bool SqlFilter(string str)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(str))
            {
                string pattern = @"net user|net localgroup|xp_cmdshell|and|or|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(";
                if (Regex.Match(str.ToLower(), Regex.Escape(pattern), RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
                {
                    result = true;
                }
            }
            return result;
        }


        #endregion
        /// <summary>
        /// 拼接多住户连接字符串
        /// </summary>
        /// <param name="corpID"></param>
        /// <returns></returns>
        public string GetConnection(string corpID)
        {
            string connection = string.Empty;
            try
            {
                string strSql = $"SELECT TOP 1 DBServer,DBName,DBUser,DBPwd FROM dbo.Tb_System_Corp WHERE CorpID = '{corpID}'";
                DataTable dt = new DbHelperSQLP(PubConstant.GetConnectionString("BGY_TW").ToString()).Query(strSql.ToString()).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    connection = $"Pooling=false;Data Source={dr["DBServer"]};Initial Catalog={dr["DBName"]};User ID={dr["DBUser"]};Password={dr["DBPwd"]}";
                }
            }
            catch (Exception e)
            {

            }
            return connection;

        }

        public static string QuerySplitSql(DataRow dr, string[,] list)
        {
            string strSql = "", name = "", value = "";
            for (int i = 0; i < list.GetLength(0); i++)
            {
                name = list[i, 0].ToString().Trim();
                if (dr.Table.Columns.Contains(name) == true && !string.IsNullOrWhiteSpace(dr[name].ToString()))
                {
                    value = AppGlobal.ChkStr(dr[name].ToString().Trim());
                    switch (list[i, 1].ToString().Trim())
                    {
                        case "=":
                            strSql += $" AND [{name}] = '{value}'";
                            break;
                        case "like":
                            strSql += $" AND ISNULL([{name}],'') LIKE '%{value}%'";
                            break;
                        case "int=":
                            strSql += $" AND ISNULL([{name}],0) ='{value}'";
                            break;
                        case ">=":
                            strSql += $" AND [{ name.Replace("Start", "")}] >='{value}'";
                            break;
                        case "<=":
                            strSql += $" AND [{name.Replace("End", "") }] <= '{value}' ";
                            break;
                        case "int>=":
                            strSql += $" AND ISNULL([{ name.Replace("Start", "")}],0) >='{value}'";
                            break;
                        case "int<=":
                            strSql += $" AND ISNULL([{ name.Replace("End", "")}],0) <='{value}'";
                            break;
                        case "date=":
                            strSql += $" AND CONVERT(VARCHAR(10),[{ name}],23)  >='{value}'";
                            break;
                        case "date>=":
                            strSql += $" AND CONVERT(VARCHAR(10),[{ name.Replace("Start", "")}],23)  >='{value}'";
                            break;
                        case "date<=":
                            strSql += $" AND CONVERT(VARCHAR(10),[{ name.Replace("End", "")}],23)  <='{value}'";
                            break;
                        case "StringIn":
                            string arrayList = "";
                            foreach (string item in value.Split(','))
                            {
                                if (!string.IsNullOrEmpty(item))
                                    arrayList += ",'" + item + "'";
                            }
                            if (arrayList.Length > 0)
                            {
                                arrayList = arrayList.Substring(1, arrayList.Length - 1);
                                strSql += $" AND ISNULL([{name}],'')  IN (" + arrayList + ")";
                            }
                            break;
                        case "IntIn":
                            strSql += $" AND ISNULL([{name}],0)  IN (" + value + ")";
                            break;
                        default:
                            break;
                    }
                }
            }
            return strSql;
        }

        #endregion


        public static ILog GetLog()
        {
            if (null == log)
            {
                log = LogManager.GetLogger(typeof(PubInfo));
            }
            return log;
        }

        #region 支付部分公共方法
        /// <summary>
        /// 获取物业数据库链接字符串
        /// </summary>
        /// <param name="Community"></param>
        /// <returns></returns>
        public static string GetConnectionStr(Tb_Community Community)
        {
            StringBuilder ConStr = new StringBuilder();
            ConStr.Append("Connect Timeout=60;Connection Lifetime=60;Max Pool Size=1000;Min Pool Size=50;");
            ConStr.Append("Pooling = true;");
            ConStr.AppendFormat(" data source = {0};", Community.DBServer);
            ConStr.AppendFormat(" initial catalog = {0};", Community.DBName);
            ConStr.AppendFormat(" PWD={0};", Community.DBPwd);
            ConStr.Append("persist security info=False;");
            ConStr.AppendFormat(" user id = {0};packet size=4096", Community.DBUser);
            return ConStr.ToString();
        }
        /// <summary>
        /// 获取小区配置
        /// </summary>
        /// <param name="CommID"></param>
        /// <returns></returns>
        public static Tb_Community GetCommunityByCommID(string CommID)
        {
            if (string.IsNullOrEmpty(CommID))
            {
                return null;
            }
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                return conn.QueryFirstOrDefault<Tb_Community>("SELECT * FROM Tb_Community WHERE CommID = @CommID", new { CommID = CommID });
            }
        }
        /// <summary>
        /// 获取小区配置
        /// </summary>
        /// <param name="CommunityId"></param>
        /// <returns></returns>
        public static Tb_Community GetCommunity(string CommunityId)
        {
            if (string.IsNullOrEmpty(CommunityId))
            {
                return null;
            }
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                return conn.QueryFirstOrDefault<Tb_Community>("SELECT * FROM Tb_Community WHERE Id = @Id OR CommID=@Id", new { Id = CommunityId });
            }
        }


        /// <summary>
        /// 插入车位预存记录
        /// </summary>
        /// <param name="erpConnStr"></param>
        /// <param name="OrderSN"></param>
        /// <param name="HandID"></param>
        /// <returns></returns>
        public static bool Tb_OL_ParkCar_Insert(string erpConnStr, string OrderSN, long HandID)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    return conn.Execute("INSERT INTO Tb_OL_ParkCar(OrderSN, HandID) VALUES(@OrderSN, @HandID)", new { OrderSN, HandID }) > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 通过商户订单号查询车位的HandID
        /// </summary>
        /// <param name="erpConnStr"></param>
        /// <param name="OrderSN"></param>
        /// <returns></returns>
        public static long GetHandID(string erpConnStr, string OrderSN)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    return conn.QueryFirstOrDefault<long>("SELECT HandID FROM Tb_OL_ParkCar WHERE OrderSN = @OrderSN", new { OrderSN });
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 验证PayData数据,并取出金额
        /// </summary>
        /// <param name="erpConnStr"></param>
        /// <param name="PayData"></param>
        /// <param name="Amt"></param>
        /// <param name="errMsg"></param>
        /// <param name="RoomID">房屋ID</param>
        /// <param name="CustID">业主ID</param>
        /// <param name="IsOwe">是否欠费</param>
        /// <returns></returns>
        public static bool CheckPayData(string erpConnStr, long CustID, long RoomID, string PayData, out decimal Amt, out string errMsg, bool IsOwe = true, bool IsPaid = false, bool ChargeLateFee = true)
        {
            try
            {
                JObject PayDataObj = JObject.Parse(PayData);
                int Type = (int)PayDataObj["Type"];
                switch (Type)
                {
                    // Type = 1为实收
                    case 1:
                        {
                            JArray Data = (JArray)PayDataObj["Data"];
                            if (null == Data || Data.Count == 0)
                            {
                                Amt = 0.00M;
                                errMsg = "支付金额必须大于0";
                                return false;
                            }
                            StringBuilder FeesIds = new StringBuilder();
                            foreach (JObject item in Data)
                            {
                                FeesIds.Append((string)item["FeesId"] + ",");
                            }
                            using (IDbConnection conn = new SqlConnection(erpConnStr))
                            {

                                DynamicParameters parameters = new DynamicParameters();
                                parameters.Add("FeesIds", FeesIds.ToString());
                                parameters.Add("IsOwe", IsOwe ? 1 : 0);
                                parameters.Add("IsPaid", IsPaid ? 1 : 0);
                                if (!ChargeLateFee)
                                {
                                    parameters.Add("ChargeLateFee", ChargeLateFee ? 1 : 0);
                                }

                                Amt = conn.QueryFirstOrDefault<decimal>("Proc_CalcAmount", parameters, null, null, CommandType.StoredProcedure);
                                errMsg = "验证成功";
                                return true;
                            }
                        }
                    // Type = 2为单项预存
                    case 2:
                        {
                            JObject Data = (JObject)PayDataObj["Data"];
                            if (null == Data)
                            {
                                Amt = 0.00M;
                                errMsg = "支付金额必须大于0";
                                return false;
                            }
                            string CostID = (string)Data["CostID"];

                            using (IDbConnection conn = new SqlConnection(erpConnStr))
                            {
                                dynamic costInfo = conn.QueryFirstOrDefault<dynamic>("SELECT CostID, CostName FROM view_HSPR_CostStanSetting_Filter WHERE CustID = @CustID " + (0 == RoomID ? "" : " AND RoomID = @RoomID ") + " AND CostID= @CostID GROUP BY CostID, CostName", new { CustID = CustID, RoomID = RoomID, CostID = CostID });
                                if (null == costInfo) 
                                {
                                    Amt = 0.00M;
                                    errMsg = "该费项不存在";
                                    return false;
                                }
                            }
                            Amt = (decimal)Data["Amt"];
                            errMsg = "验证成功";
                            return true;
                        }
                    // Type = 3为单次预存多个费项
                    case 3:
                        {
                            JArray Data = (JArray)PayDataObj["Data"];
                            if (null == Data)
                            {
                                Amt = 0.00M;
                                errMsg = "没有需要预存的费用";
                                return false;
                            }
                            Amt = 0.00M;
                            using (IDbConnection conn = new SqlConnection(erpConnStr))
                            {
                                foreach (JObject item in Data)
                                {
                                    string CostID = (string)item["CostID"];
                                    //华宇存在佣金情况，不对费用做判断
                                    //dynamic costInfo = conn.QueryFirstOrDefault<dynamic>("SELECT CostID, CostName FROM view_HSPR_CostStanSetting_Filter WHERE CustID = @CustID " + (0 == RoomID ? "" : " AND RoomID = @RoomID ") + " AND CostID= @CostID GROUP BY CostID, CostName", new { CustID = CustID, RoomID = RoomID, CostID = CostID });
                                    //if (null == costInfo)
                                    //{
                                    //    Amt = 0.00M;
                                    //    errMsg = "费项" + CostID + "不存在";
                                    //    return false;
                                    //}
                                    Amt += (decimal)item["Amt"];
                                }
                            }
                            errMsg = "验证成功";
                            return true;
                        }
                    // Type = 4为单次无费项综合预存
                    case 4:
                        {
                            JObject Data = (JObject)PayDataObj["Data"];
                            if (null == Data)
                            {
                                Amt = 0.00M;
                                errMsg = "支付金额必须大于0";
                                return false;
                            }
                            string CostID = (string)Data["CostID"];
                            Amt = (decimal)Data["Amt"];
                            errMsg = "验证成功";
                            return true;
                        }
                    // Type = 5为带车位的单项预存（相当于Type=2的扩展）
                    case 5:
                        {
                            JObject Data = (JObject)PayDataObj["Data"];
                            if (null == Data)
                            {
                                Amt = 0.00M;
                                errMsg = "支付金额必须大于0";
                                return false;
                            }
                            string CostID = (string)Data["CostID"];
                            string HandID = (string)Data["HandID"];
                            using (IDbConnection conn = new SqlConnection(erpConnStr))
                            {
                                //华宇存在佣金情况，不对费用做判断
                                //dynamic costInfo = conn.QueryFirstOrDefault<dynamic>("SELECT CostID, CostName FROM view_HSPR_CostStanSetting_Filter WHERE CustID = @CustID " + (0 == RoomID ? "" : " AND RoomID = @RoomID ") + " AND CostID= @CostID GROUP BY CostID, CostName", new { CustID = CustID, RoomID = RoomID, CostID = CostID });
                                //if (null == costInfo)
                                //{
                                //    Amt = 0.00M;
                                //    errMsg = "该费项不存在";
                                //    return false;
                                //}
                                // 查询ERP是否存在对应车位
                                dynamic ParkCarInfo = conn.QueryFirstOrDefault("SELECT * FROM view_HSPR_ParkingHand_Filter WHERE ISNULL(IsDelete,0) = 0 AND CustID = @CustID " + (0 == RoomID ? "" : " AND RoomID = @RoomID ") + " AND HandID = @HandID", new { CustID = CustID, RoomID = RoomID, HandID = HandID });
                                if (null == ParkCarInfo)
                                {
                                    Amt = 0.00M;
                                    errMsg = "未查询到对应车位信息";
                                    return false;
                                }
                            }
                            Amt = (decimal)Data["Amt"];
                            errMsg = "验证成功";
                            return true;
                        }
                    // Type = 6为带车位带佣金的单项预存（相当于Type=235的扩展）
                    case 6:
                        {
                            Amt = 0.00M;
                            JObject Data = (JObject)PayDataObj["Data"];
                            string HandID = (string)Data["HandID"];
                            JArray CostList = (JArray)Data["CostList"];
                            using (IDbConnection conn = new SqlConnection(erpConnStr))
                            {
                                // 查询ERP是否存在对应车位
                                dynamic ParkCarInfo = conn.QueryFirstOrDefault("SELECT * FROM view_HSPR_ParkingHand_Filter WHERE ISNULL(IsDelete,0) = 0 AND CustID = @CustID " + (0 == RoomID ? "" : " AND RoomID = @RoomID ") + " AND HandID = @HandID", new { CustID = CustID, RoomID = RoomID, HandID = HandID });
                                if (null == ParkCarInfo)
                                {
                                    Amt = 0.00M;
                                    errMsg = "未查询到对应车位信息";
                                    return false;
                                }
                                foreach (JObject item in CostList)
                                {
                                    string CostID = (string)item["CostID"];
                                    //华宇存在佣金情况，不对费用做判断
                                    //dynamic costInfo = conn.QueryFirstOrDefault<dynamic>("SELECT CostID, CostName FROM view_HSPR_CostStanSetting_Filter WHERE CustID = @CustID " + (0 == RoomID ? "" : " AND RoomID = @RoomID ") + " AND CostID= @CostID GROUP BY CostID, CostName", new { CustID = CustID, RoomID = RoomID, CostID = CostID });
                                    //if (null == costInfo)
                                    //{
                                    //    Amt = 0.00M;
                                    //    errMsg = "费项" + CostID + "不存在";
                                    //    return false;
                                    //}
                                    Amt += (decimal)item["Amt"];
                                }
                            }
                            errMsg = "验证成功";
                            return true;
                        }
                        // Type = 7为POS机专用，多费项预存时需要计算佣金，需要拿到数据，在后台获取到佣金后，重新组装PayData,适用于多费项预存，多费项带佣金预存
                    case 7:
                        {
                            Amt = 0.00M;
                            JArray Data = (JArray)PayDataObj["Data"];
                            using (IDbConnection conn = new SqlConnection(erpConnStr))
                            {
                                foreach (JObject item in Data)
                                {
                                    #region 预存信息校验
                                    string CostName = (string)item["CostName"];
                                    string CostID = (string)item["CostID"];
                                    // 华宇POS，带佣金情况下，可能查询不到，取消关联
                                    //dynamic costInfo = conn.QueryFirstOrDefault<dynamic>("SELECT CommID, CostID, CostName, StanID FROM view_HSPR_CostStanSetting_Filter WHERE CustID = @CustID " + (0 == RoomID ? "" : " AND RoomID = @RoomID ") + " AND CostID = @CostID GROUP BY CommID, CostID, CostName, StanID", new { CustID = CustID, RoomID = RoomID, CostID = CostID });
                                    //if (null == costInfo)
                                    //{
                                    //    Amt = 0.00M;
                                    //    errMsg = "费项[" + (string.IsNullOrEmpty(CostName) ? CostID : CostName) + "]不存在";
                                    //    return false;
                                    //}
                                    //string CommID = Convert.ToString(costInfo.CommID);
                                    //string StanID = Convert.ToString(costInfo.StanID);
                                    string HandID = (string)item["HandID"];
                                    if (!string.IsNullOrEmpty(HandID))
                                    {
                                        // 查询ERP是否存在对应车位
                                        dynamic ParkCarInfo = conn.QueryFirstOrDefault("SELECT * FROM view_HSPR_ParkingHand_Filter WHERE ISNULL(IsDelete,0) = 0 AND CustID = @CustID " + (0 == RoomID ? "" : " AND RoomID = @RoomID ") + " AND HandID = @HandID", new { CustID = CustID, RoomID = RoomID, HandID = HandID });
                                        if (null == ParkCarInfo)
                                        {
                                            Amt = 0.00M;
                                            errMsg = "费项[" + (string.IsNullOrEmpty(CostName) ? CostID : CostName) + "]不存在所选车位";
                                            return false;
                                        }
                                    }
                                    decimal DueAmount = (decimal)item["Amt"];
                                    #endregion
                                    Amt += DueAmount;
                                }
                                errMsg = "验证成功";
                                return true;
                            }
                        }
                    default:
                        {
                            Amt = 0.00M;
                            errMsg = "PayData格式有误";
                            return false;
                        }
                }
            }
            catch (Exception ex)
            {
                Amt = 0.00M;
                errMsg = ex.Message;
                return false;
            }
        }


        public static DataTable HSPR_PreCostsDetail_Filter(string erpConnStr, string SQLEx)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    return conn.ExecuteReader("Proc_HSPR_PreCostsDetail_Filter", new { SQLEx = SQLEx }, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return null;
            }

        }
        public static DataTable HSPR_BillUse_GetBillsSignUseRange(string erpConnStr, string CommID, string UserCode = "", string UseRange = "POS机")
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    return conn.ExecuteReader("Proc_HSPR_BillUse_GetBillsSignUseRange", new { CommID = CommID, UserCode = UserCode, UseRange = UseRange }, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return null;
            }
        }
        public static long HSPR_PreCostsReceipts_InsertAuto(string erpConnStr, string CommID, string CustID, string RoomID, string BillsSign, int PrintTimes
           , string BillsDate, string UserCode, string ChargeMode, int AccountWay, string ReceMemo, decimal PrecAmount, int IsDelete
           , string InvoiceBill, string InvoiceUnit, string RemitterUnit, string BankName, string BankAccount, string ChequeBill, long BillTypeID
           , string SQLEx1, string SQLEx2, string SQLEx3, string SQLEx4, string SQLEx5, string SQLEx6, string SQLEx7, string SQLEx8, string SQLEx9, string SQLEx10)
        {
            long ReceID = 0;
            try
            {
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    #region 参数
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("CommID", CommID);
                    parameters.Add("CustID", CustID);
                    parameters.Add("RoomID", RoomID);
                    parameters.Add("BillsSign", BillsSign);
                    parameters.Add("PrintTimes", PrintTimes);
                    parameters.Add("BillsDate", BillsDate);
                    parameters.Add("UserCode", UserCode);
                    parameters.Add("ChargeMode", ChargeMode);
                    parameters.Add("AccountWay", AccountWay);
                    parameters.Add("ReceMemo", ReceMemo);
                    parameters.Add("PrecAmount", PrecAmount);
                    parameters.Add("IsDelete", IsDelete);
                    parameters.Add("InvoiceBill", InvoiceBill);
                    parameters.Add("InvoiceUnit", InvoiceUnit);
                    parameters.Add("RemitterUnit", RemitterUnit);
                    parameters.Add("BankName", BankName);
                    parameters.Add("BankAccount", BankAccount);
                    parameters.Add("ChequeBill", ChequeBill);
                    parameters.Add("BillTypeID", BillTypeID);
                    parameters.Add("SQLEx1", SQLEx1);
                    parameters.Add("SQLEx2", SQLEx2);
                    parameters.Add("SQLEx3", SQLEx3);
                    parameters.Add("SQLEx4", SQLEx4);
                    parameters.Add("SQLEx5", SQLEx5);
                    parameters.Add("SQLEx6", SQLEx6);
                    parameters.Add("SQLEx7", SQLEx7);
                    parameters.Add("SQLEx8", SQLEx8);
                    parameters.Add("SQLEx9", SQLEx9);
                    parameters.Add("SQLEx10", SQLEx10);
                    parameters.Add("ReceID", ReceID, DbType.Int64, ParameterDirection.Output);
                    #endregion
                    DataSet ds = conn.ExecuteReader("Proc_HSPR_PreCostsReceipts_InsertAuto", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
                    ReceID = parameters.Get<long>("ReceID");
                    return ReceID;
                }

            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return ReceID;
            }

        }

        public static void HSPR_BillUseInstead_InsUpdate(string erpConnStr, string CommID, long BillTypeID, string BillsSign, string UserCode, long SourceID, int SourceType, int BillUseCase)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    DataSet ds = conn.ExecuteReader("Proc_HSPR_BillUseInstead_InsUpdate", new { CommID, BillTypeID, BillsSign, UserCode, SourceID, SourceType, BillUseCase }, null, null, CommandType.StoredProcedure).ToDataSet();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
            }
        }

        public static void HSPR_BillUse_UpdateData(string erpConnStr, string CommID, long BillTypeID, string BillsSign)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(erpConnStr))
                {
                    DataSet ds = conn.ExecuteReader("Proc_HSPR_BillUse_UpdateData", new { CommID, BillTypeID, BillsSign }, null, null, CommandType.StoredProcedure).ToDataSet();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
            }
        }
        private static readonly object syncLocker = new object();
        /// <summary>
        /// 实收、预收下帐
        /// </summary>
        /// <param name="CommunityId"></param>
        /// <param name="OrderId"></param>
        public static bool ReceFees(string erpConnStr, out long ReceID, string CommID, string CustID, string RoomID, string FeesIds, decimal PrecAmount = 0.00M, string ChargeMode = "", string ReceMemo = "", string UserCode = null, int ChargeLateFee = 1)
        {
            lock (syncLocker)
            {
                ReceID = 0;
                try
                {
                    using (IDbConnection conn = new SqlConnection(erpConnStr))
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("CustIDs", CustID);
                        parameters.Add("CommID", CommID);
                        parameters.Add("RoomIDs", RoomID);
                        parameters.Add("ChargeMode", ChargeMode);
                        parameters.Add("ReceMemo", ReceMemo);
                        parameters.Add("FeesIds", FeesIds);
                        parameters.Add("PrecAmount", PrecAmount);
                        parameters.Add("UserCode", UserCode);
                        if (ChargeLateFee != 1)
                        {
                            parameters.Add("ChargeLateFee", ChargeLateFee);
                        }
                        dynamic info = conn.QueryFirstOrDefault("Proc_HSPR_ReceFees", parameters, null, null, CommandType.StoredProcedure);
                        if (null == info)
                        {
                            return false;
                        }
                        try
                        {
                            ReceID = Convert.ToInt64(info.ReceID);
                        }
                        catch (Exception)
                        {
                        }
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    GetLog().Error("ReceFees:ERROR:" + ex.Message + Environment.CommandLine + ex.StackTrace);
                    return false;
                }
            }
        }

        /// <summary>
        /// 预存下账
        /// </summary>
        /// <param name="erpConnStr">ERP链接字符串</param>
        /// <param name="ReceID">输出的票据ID</param>
        /// <param name="CommID">项目ID</param>
        /// <param name="CustID">业主ID</param>
        /// <param name="RoomID">房屋ID</param>
        /// <param name="CostID">费用ID</param>
        /// <param name="PrecAmount">预存金额</param>
        /// <param name="ChargeMode">收费方式</param>
        /// <param name="useUserCode">票据收款人</param>
        /// <param name="useRange">票据收款方式</param>
        /// <param name="HandID">车位预存HandID</param>
        /// <returns></returns>
        public static bool RecePreFees(string erpConnStr, out long ReceID, string CommID, string CustID, string RoomID, string CostID, decimal PrecAmount, string ChargeMode = "", string useUserCode = "", string useRange = "", long HandID = 0)
        {
            return RecePreFees(erpConnStr, out ReceID, CommID, CustID, RoomID, new string[] { CostID }, new decimal[] { PrecAmount }, new long[] { HandID }, ChargeMode, useUserCode, useRange);
        }
        public static bool RecePreFees(string erpConnStr, out long ReceID, string CommID, string CustID, string RoomID, string[] CostID, decimal[] PrecAmount, long[] HandID, string ChargeMode = "", string useUserCode = "", string useRange = "")
        {
            ReceID = 0;
            if (null == CostID || null == PrecAmount || CostID.Length != PrecAmount.Length || CostID.Length != HandID.Length)
            {
                return false;
            }
            string strBillsSign = "";
            int iPrintTimes = 0;
            DateTime dBillsDate = DateTime.Now;
            string strBillsDate = dBillsDate.ToString();

            int AccountWay = 3;

            string UserCode;
            if (string.IsNullOrEmpty(useUserCode))
            {
                UserCode = "_Sys_";
            }
            else
            {
                UserCode = useUserCode;
                useUserCode = "";
            }

            string strReceMemo = "";
            int iIsDelete = 0;

            string strInvoiceBill = "";
            string strInvoiceUnit = "";
            string strRemitterUnit = "";

            string strBankName = "";
            string strBankAccount = "";

            string strChequeBill = "";
            long iBillTypeID = 0;

            try
            {
                #region 保存明细
                string strSQL = "  and CommID = " + CommID.ToString() + "  ";
                strSQL = strSQL + " and CustID = -1 and RoomID = -1 ";

                DataTable dTableDetail = HSPR_PreCostsDetail_Filter(erpConnStr, strSQL);
                //DataTable dTableDetail = new DataTable();
                if (dTableDetail != null)
                {
                    for (int i = 0; i < CostID.Length; i++)
                    {
                        string ItemCostID = CostID[i];
                        decimal ItemPrecAmount = PrecAmount[i];
                        long ItemHandID = HandID[i];
                        DataRow DRow = dTableDetail.NewRow();
                        DRow["RecdID"] = 0;
                        DRow["PrecID"] = 0;
                        DRow["CommID"] = CommID;
                        DRow["CostID"] = ItemCostID;
                        DRow["CustID"] = CustID;
                        DRow["RoomID"] = RoomID;
                        if (ItemPrecAmount <= 0)
                        {
                            DRow["PrecAmount"] = 0;
                        }
                        else
                        {
                            DRow["PrecAmount"] = ItemPrecAmount;
                        }
                        string strPrecMemo = "";
                        if (strPrecMemo == "请点击费用备注生成费用期间")
                        {
                            strPrecMemo = "";
                        }
                        DRow["PrecMemo"] = strPrecMemo;
                        DRow["BillsSign"] = strBillsSign;
                        DRow["PrecDate"] = DateTime.Now.ToString();
                        DRow["UserCode"] = UserCode;


                        DRow["CostIDs"] = ItemCostID;
                        DRow["RoomSign"] = "";
                        DRow["CostNames"] = "";
                        DRow["UserName"] = "";
                        DRow["ChargeMode"] = ChargeMode;
                        DRow["AccountWay"] = AccountWay;

                        DRow["HandID"] = ItemHandID;

                        DRow["HandIDs"] = "";

                        DRow["ParkNames"] = "";

                        DRow["IsDelete"] = 0;

                        dTableDetail.Rows.Add(DRow);
                    }
                    dTableDetail.AcceptChanges();
                }
                #endregion

                decimal iTotalAmount = 0;
                int iFeesLimitCount = 0;

                string strProcName = "Proc_HSPR_PreCosts_InsertAuto";

                string ChildSQLEx = "";

                string[] arrSQLEx = new string[10];

                int iSelCount = 0;
                int iCut = 0;

                //每段笔数
                int iExcCount = 10;

                if (null == dTableDetail || dTableDetail.Rows.Count == 0)
                {
                    GetLog().Error("RecePreFees:FAIL:请添加需要预交的费用");
                    return false;
                }

                foreach (DataRow DRow in dTableDetail.Rows)
                {

                    #region 取值
                    int iiCommID = Convert.ToInt32(DRow["CommID"].ToString());
                    long iiCustID = Convert.ToInt64(DRow["CustID"].ToString());
                    long iiRoomID = Convert.ToInt64(DRow["RoomID"].ToString());
                    long iiCostID = Convert.ToInt64(DRow["CostID"].ToString());
                    decimal iiPrecAmount = Convert.ToDecimal(DRow["PrecAmount"].ToString());
                    string strPrecMemo = DRow["PrecMemo"].ToString();
                    long iiHandID = Convert.ToInt64(DRow["HandID"].ToString());

                    string strTmpCostIDs = DRow["CostIDs"].ToString();
                    string strTmpHandIDs = DRow["HandIDs"].ToString();

                    #endregion

                    #region 合计
                    iTotalAmount = iTotalAmount + iiPrecAmount;

                    if (iiPrecAmount > 0)
                    {
                        iFeesLimitCount++;
                    }
                    #endregion

                    //有需要预交的费用
                    if (iiPrecAmount > 0)
                    {
                        #region 明细生成

                        ChildSQLEx = " exec " + strProcName + " " + iiCommID.ToString() + ","
                            + iiCustID.ToString() + ","
                            + iiRoomID.ToString() + ","
                            + iiCostID.ToString() + ","
                            + iiPrecAmount.ToString() + ","
                            + "$ReceID$" + ","
                            + "N'" + strPrecMemo + "',"
                            + iiHandID.ToString() + ","
                            + "N'" + strTmpCostIDs + "',"
                            + "N'" + strTmpHandIDs + "'"
                            + "\r\n";

                        #region 分成多段
                        if ((iSelCount < (iExcCount * (iCut + 1))) && (iSelCount >= iExcCount * iCut))
                        {
                            arrSQLEx[iCut] = arrSQLEx[iCut] + ChildSQLEx;
                        }

                        iSelCount++;

                        if (iSelCount >= (iExcCount * (iCut + 1)))
                        {
                            iCut++;
                        }

                        #endregion

                        #endregion
                    }
                }
                //没有需要预交的费用,直接返回true
                if (iFeesLimitCount <= 0)
                {
                    GetLog().Info("RecePreFees:SUCCESS:iFeesLimitCount=" + iFeesLimitCount);
                    return true;
                }
                #region 票据
                DataTable dTable = HSPR_BillUse_GetBillsSignUseRange(erpConnStr, CommID, useUserCode, useRange);

                if (dTable.Rows.Count > 0)
                {
                    DataRow DRow = dTable.Rows[0];

                    iBillTypeID = Convert.ToInt64(DRow["BillTypeID"].ToString());
                    strBillsSign = DRow["BillsSign"].ToString();
                }
                dTable.Dispose();
                #endregion

                //预交收据
                ReceID = HSPR_PreCostsReceipts_InsertAuto(erpConnStr, CommID, CustID, RoomID, strBillsSign
                    , iPrintTimes, strBillsDate, UserCode, ChargeMode, AccountWay, strReceMemo, iTotalAmount, iIsDelete
                    , strInvoiceBill, strInvoiceUnit, strRemitterUnit, strBankName, strBankAccount, strChequeBill, iBillTypeID
                    , arrSQLEx[0], arrSQLEx[1], arrSQLEx[2], arrSQLEx[3], arrSQLEx[4], arrSQLEx[5], arrSQLEx[6], arrSQLEx[7], arrSQLEx[8], arrSQLEx[9]);

                #region 收据记录
                if (strBillsSign != "")
                {
                    if (ReceID != 0)
                    {
                        int iSourceType = 2;//预交收据
                        HSPR_BillUseInstead_InsUpdate(erpConnStr, CommID, iBillTypeID, strBillsSign, UserCode, ReceID, iSourceType, iSourceType);

                    }

                    //更新号段数量和金额
                    if ((iBillTypeID != 0) && (strBillsSign != ""))
                    {
                        HSPR_BillUse_UpdateData(erpConnStr, CommID, iBillTypeID, strBillsSign);
                    }
                }
                #endregion

                if (ReceID != 0)
                {
                    GetLog().Info("RecePreFees:SUCCESS:iReceID=" + ReceID);
                    return true;
                }
                else
                {
                    GetLog().Info("RecePreFees:FAIL:iReceID=" + ReceID);
                    return false;
                }
            }
            catch (Exception ex)
            {
                GetLog().Error("RecePreFees:ERROR:" + ex.Message);
                return false;
            }
        }

        public static string GetRandomCode(int length)
        {
            Random random = new Random(GetRandSeed());
            string str = "";
            for (int i = 0; i < length; i++)
            {
                str += random.Next(0, 10).ToString();
            }
            return str;
        }
        public static int GetRandSeed()
        {
            byte[] bytes = new byte[8];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        #endregion

        public static void Log(string message, string directory)
        {
            if (string.IsNullOrEmpty(directory))
            {
                directory = "Logs\\";
            }
            if (!directory.EndsWith("\\"))
            {
                directory += "\\";
            }
            allinpay.utils.AppUtil.WriteLog(message, directory, "");
        }

        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static string HttpPost(string url, string data)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);

            //字符串转换为字节码
            byte[] bs = Encoding.UTF8.GetBytes(data);
            //参数类型，这里是json类型
            //还有别的类型如"application/x-www-form-urlencoded"，不过我没用过(逃
            httpWebRequest.ContentType = "application/json";
            //参数数据长度
            httpWebRequest.ContentLength = bs.Length;
            //设置请求类型
            httpWebRequest.Method = "POST";
            //设置超时时间
            httpWebRequest.Timeout = 10000;
            //将参数写入请求地址中


            try
            {
                httpWebRequest.GetRequestStream().Write(bs, 0, bs.Length);
                //发送请求
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                //读取返回数据
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
                string responseContent = streamReader.ReadToEnd();

                streamReader.Close();
                httpWebResponse.Close();
                httpWebRequest.Abort();

                streamReader.Dispose();
                httpWebResponse.Dispose();

                return responseContent;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 获取分业参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        protected PageInfoModel GetParamEntity(DataRow row)
        {
            PageInfoModel result = new PageInfoModel();
            int type = 0;
            int pageSize = 10, pageIndex = 1;
            if (row.Table.Columns.Contains("PageType"))
            {
                int.TryParse(row["PageType"].ToString(), out type);
            }
            if (row.Table.Columns.Contains("descending"))
            {
                result.Descending = Convert.ToBoolean(row["descending"]);
            }
            if (row.Table.Columns.Contains("sort"))
            {
                result.Sort = row["sort"].ToString();
            }

            type = type <= 0 ? 0 : type;

            switch (type)
            {
                //根据参数获取分页
                case 0:
                    if (row.Table.Columns.Contains("PageSize"))
                    {
                        int.TryParse(row["PageSize"].ToString(), out pageSize);
                        result.PageSize = pageSize;
                    }
                    if (row.Table.Columns.Contains("PageIndex"))
                    {
                        int.TryParse(row["PageIndex"].ToString(), out pageIndex);
                        result.PageIndex = pageIndex;
                    }
                    return result; ;
                //特殊分页 仅仅取第一页 条数根据参数获取
                case 1:
                    if (row.Table.Columns.Contains("PageSize"))
                    {
                        int.TryParse(row["PageSize"].ToString(), out pageSize);
                        result.PageSize = pageSize;
                    }
                    return result;
                //默认分页即 第一页 每页十条
                default:
                    return result;
            }
        }
    }
}