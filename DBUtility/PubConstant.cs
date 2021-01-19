using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
namespace MobileSoft.DBUtility
{
    public class PubConstant
    {
        /// <summary>
        /// 得到公共业务数据库连接字符串
        /// </summary>
        public static string tw2bsConnectionString
        {           
            get 
            {
                  string _ConnectionString = "";

                if (System.Web.HttpContext.Current.Session["PublicConnectionString"] != null)
                {
                    _ConnectionString = System.Web.HttpContext.Current.Session["PublicConnectionString"].ToString();

                    if (string.IsNullOrEmpty(_ConnectionString))
                    {
                        _ConnectionString = GetConnectionString("tw2bsConnectionString").ToString(); 
                    }
                }

                if (string.IsNullOrEmpty(_ConnectionString))
                {
                    _ConnectionString = GetConnectionString("tw2bsConnectionString").ToString();
                }

                return _ConnectionString;
            }
              set
              {
                    System.Web.HttpContext.Current.Session["PublicConnectionString"] = value;
              }
        }

        /// <summary>
        /// 得到短信发送数据库
        /// </summary>
        public static string SMSConnectionString
        {
            get
            {
                string _ConnectionString = "";

                _ConnectionString = GetConnectionString("SMSConnectionString").ToString();

                return _ConnectionString;
            }
        }
        /// <summary>
        /// 错误日志记录
        /// </summary>
        public static string ErrorConnectionString
        {
            get
            {
                string _ErrorConnectionString = "";

                _ErrorConnectionString = GetConnectionString("ErrorConnectionString").ToString();

                return _ErrorConnectionString;
            }
        }

        /// <summary>
        /// 得到引擎业务数据库连接字符串
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string hmWyglConnectionString
        {
            get
            {
                string _ConnectionString = "";

                if (System.Web.HttpContext.Current.Session["PrivateConnectionString"] != null)
                {
                    _ConnectionString = System.Web.HttpContext.Current.Session["PrivateConnectionString"].ToString();
                }

                return _ConnectionString;
            }
            set
            {
                System.Web.HttpContext.Current.Session["PrivateConnectionString"] = value;
            }
        }

        /// <summary>
        /// 积分系统数据库
        /// </summary>
        public static string TWAppConnectionString {
            get
            {
                return GetConnectionString("TWAppConnection");
            }
        }

        /// <summary>
        /// 得到引擎业务数据库连接字符串
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string SQLConnection
        {
            get
            {
                string _ConnectionString = "";

                if (System.Web.HttpContext.Current.Session["SQLConnection"] != null)
                {
                    _ConnectionString = System.Web.HttpContext.Current.Session["SQLConnection"].ToString();
                }

                return _ConnectionString;
            }
            set
            {
                System.Web.HttpContext.Current.Session["SQLConnection"] = value;
            }
        }

        /// <summary>
        /// 得到引擎业务数据库连接字符串
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string SQIBSContionString
        {
            get
            {
                string _ConnectionString = "";

                if (System.Web.HttpContext.Current.Session["SQIBSContionString"] != null)
                {
                    _ConnectionString = System.Web.HttpContext.Current.Session["SQIBSContionString"].ToString();
                }
                else
                {
                    _ConnectionString = GetConnectionString("SQIBSContionString").ToString();
                    System.Web.HttpContext.Current.Session["SQIBSContionString"] = _ConnectionString;
                }

                return _ConnectionString;
            }
            set
            {
                System.Web.HttpContext.Current.Session["SQIBSContionString"] = value;
            }
        }

        /// <summary>
        /// 得到商家BS数据库连接字符串
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string SQMBSContionString
        {
              get
              {
                    string _ConnectionString = "";

                    if (System.Web.HttpContext.Current.Session["SQMBSContionString"] != null)
                    {
                          _ConnectionString = System.Web.HttpContext.Current.Session["SQMBSContionString"].ToString();
                    }
                    else
                    {
                          _ConnectionString = GetConnectionString("SQMBSContionString").ToString();
                          System.Web.HttpContext.Current.Session["SQMBSContionString"] = _ConnectionString;
                    }

                    return _ConnectionString;
              }
              set
              {
                    System.Web.HttpContext.Current.Session["SQMBSContionString"] = value;
              }
        }

        public static string SQTWBSContionString
        {
              get
              {
                    string _ConnectionString = "";

                    if (System.Web.HttpContext.Current.Session["SQTWBSContionString"] != null)
                    {
                          _ConnectionString = System.Web.HttpContext.Current.Session["SQTWBSContionString"].ToString();
                    }
                    else
                    {
                          _ConnectionString = GetConnectionString("SQTWBSContionString").ToString();
                          System.Web.HttpContext.Current.Session["SQTWBSContionString"] = _ConnectionString;
                    }

                    return _ConnectionString;
              }
              set
              {
                    System.Web.HttpContext.Current.Session["SQTWBSContionString"] = value;
              }
        }

        /// <summary>
        /// 得到引擎业务数据库连接字符串
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string JSYHContionString
        {
            get
            {
                string _ConnectionString = "";

                if (System.Web.HttpContext.Current.Session["JSYHContionString"] != null)
                {
                    _ConnectionString = System.Web.HttpContext.Current.Session["JSYHContionString"].ToString();
                }
                else
                {
                    _ConnectionString = GetConnectionString("JSYHContionString").ToString();
                    System.Web.HttpContext.Current.Session["JSYHContionString"] = _ConnectionString;
                }

                return _ConnectionString;
            }
            set
            {
                System.Web.HttpContext.Current.Session["JSYHContionString"] = value;
            }
        }

        /// <summary>
        /// 得到引擎业务数据库连接字符串
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string NCYHContionString
        {
            get
            {
                string _ConnectionString = "";

                if (System.Web.HttpContext.Current.Session["NCYHContionString"] != null)
                {
                    _ConnectionString = System.Web.HttpContext.Current.Session["NCYHContionString"].ToString();
                }
                else
                {
                    _ConnectionString = GetConnectionString("NCYHContionString").ToString();
                    System.Web.HttpContext.Current.Session["NCYHContionString"] = _ConnectionString;
                }

                return _ConnectionString;
            }
            set
            {
                System.Web.HttpContext.Current.Session["NCYHContionString"] = value;
            }
        }

        /// <summary>
        /// 得到引擎业务数据库连接字符串
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string ECOContionString
        {
            get
            {
                string _ConnectionString = "";

                if (System.Web.HttpContext.Current.Session["ECOContionString"] != null)
                {
                    _ConnectionString = System.Web.HttpContext.Current.Session["ECOContionString"].ToString();
                }
                else
                {
                    _ConnectionString = GetConnectionString("ECOContionString").ToString();
                    System.Web.HttpContext.Current.Session["ECOContionString"] = _ConnectionString;
                }

                return _ConnectionString;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ECOContionString"] = value;
            }
        }

        /// <summary>
        /// 得到引擎业务数据库连接字符串
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string UnionContionString
        {
            get
            {
                string _ConnectionString = "";

                if (System.Web.HttpContext.Current.Session["UnionContionString"] != null)
                {
                    _ConnectionString = System.Web.HttpContext.Current.Session["UnionContionString"].ToString();
                }
                else
                {
                    _ConnectionString = GetConnectionString("UnionContionString").ToString();
                    System.Web.HttpContext.Current.Session["UnionContionString"] = _ConnectionString;
                }

                return _ConnectionString;
            }
            set
            {
                System.Web.HttpContext.Current.Session["UnionContionString"] = value;
            }
        }


        /// <summary>
        /// 得到引擎业务数据库连接字符串
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string UnifiedContionString
        {
            get
            {
                string _ConnectionString = "";

                _ConnectionString = GetConnectionString("UnifiedConnectionString").ToString();

                return _ConnectionString;
            }
            set
            {
                System.Web.HttpContext.Current.Session["UnifiedConnectionString"] = value;
            }
        }

        /// <summary>
        /// 得到商家数据库连接字符串
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string BusinessContionString
        {
            get
            {
                string _ConnectionString = "";

                _ConnectionString = GetConnectionString("BusinessContionString").ToString();

                return _ConnectionString;
            }
            set
            {
                System.Web.HttpContext.Current.Session["BusinessContionString"] = value;
            }
        }

        /// <summary>
        /// 得到web.config里配置项的数据库连接字符串。
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string ConfigName)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings[ConfigName].ToString();

            return connectionString;
        }

        /// <summary>
        /// 得到web.config里配置项。
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetAppSetString(string ConfigName)
        {
            
            string connectionString = WebConfigurationManager.AppSettings[ConfigName].ToString();

            return connectionString;
        }


        /// <summary>
        /// 彰泰物业数据库连接字符串
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string ZTWYConnectionString
        {
            get
            {
                string _ConnectionString = "";
                _ConnectionString = GetConnectionString("ZTWYConnectionString").ToString();
                return _ConnectionString;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ZTWYConnectionString"] = value;
            }
        }

        /// <summary>
        /// 彰泰物业报事操作默认人
        /// </summary>
        public static string ZTWYIncidentUserCode
        {
            get
            {
                string _ConnectionString = "";
                _ConnectionString = GetConnectionString("ZTWYIncidentUserCode").ToString();
                return _ConnectionString;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ZTWYIncidentUserCode"] = value;
            }
        }


        #region 碧桂园连接字符串
        /// <summary>
        /// 获取连接字符串(碧桂园多租户，同样适用用于平台版)
        /// </summary>
        /// <param name="CorpID">公司ID</param>
        /// <param name="DbType">数据库类型：rts报表库，write主库，ReadServer1从库【暂支持一台读库】</param>
        /// <returns></returns>
        public static string GetSQLConnection(string CorpID,string DbType)
        {
            //获取TW2_bs数据库
            string conn = GetConnectionString("SQLTW2BSConnection");            
            DataTable dt = Query("  select * from Tb_System_Corp with(nolock) where CorpId='" + CorpID + "' ", conn).Tables[0];
            string strDBServer = "";
            string strDBName = "";
            string strDBUser = "";
            string strDBPwd = "";
            if (dt.Rows.Count > 0)
            {
                DataRow DRow = dt.Rows[0];
                if (DbType.ToLower() == "rts")//报表
                {
                    strDBServer= DRow["RtsServer"].ToString();
                    strDBName = ClearLogStr(DRow["RtsDBName"].ToString());
                    strDBUser = DRow["RtsUser"].ToString();
                    strDBPwd = DRow["RtsPwd"].ToString();
                }
                else if (DbType.ToLower() == "write")//主库，写
                {                    
                    strDBServer = DRow["DBServer"].ToString();
                    strDBName = ClearLogStr(DRow["DbName"].ToString());
                    strDBUser = DRow["DBUser"].ToString();
                    strDBPwd = DRow["DBPwd"].ToString();
                }
                else if (DbType.ToLower() == "readserver1")//从库1  ，读
                {
                    strDBServer = DRow["ReadServer1"].ToString();
                    strDBName = ClearLogStr(DRow["DbName"].ToString());
                    strDBUser = DRow["ReadServer1User"].ToString();
                    strDBPwd = DRow["ReadServer1Pwd"].ToString();
                }    
                //从库2
                //.
                //.
                //N
            }
             
            return string.Format("Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2048;Min Pool Size=0;Pooling=true;data source={0};initial catalog={1};PWD={3};persist security info=False;user id={2};packet size=4096", strDBServer, strDBName, strDBUser, strDBPwd);            

        }

        /// <summary>
        /// 获取数据库名称（支持平台版）        
        /// </summary>
        /// <param name="CorpID">公司编号</param>
        /// <param name="DbType">数据库类型：rts报表库，write主库，ReadServer1写库</param>
        /// <returns></returns>
        public static String GetDBServerName(string CorpID, string DbType)
        {
            //获取TW2_bs数据库
            string conn = tw2bsConnectionString;
            DataTable dt = Query("  select * from Tb_System_Corp with(nolock) where CorpId='" + CorpID + "' ", conn).Tables[0];
            string strDBName = "";
            if (dt.Rows.Count > 0)
            {
                DataRow DRow = dt.Rows[0];
                if (DbType.ToLower() == "rts")//报表
                {                    
                    strDBName = ClearLogStr(DRow["RtsDBName"].ToString());
                    
                }
                else if (DbType.ToLower() == "write")//主库，写
                {                    
                    strDBName = ClearLogStr(DRow["DbName"].ToString());
                  
                }
                else if (DbType.ToLower() == "ReadServer1")//从库1  ，读
                {                    
                    strDBName = ClearLogStr(DRow["DbName"].ToString());                   
                }
            }
            return strDBName;

        }



        private static DataSet Query(string SQLString, string conn)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }


        public static string ClearLogStr(string str)
        {
            if (str.StartsWith("[") && str.EndsWith("]"))
            {
                string ss = str.Trim();
                ss = ss.Substring(0, ss.Length - 1);//去掉最后一个逗号
                ss = ss.Substring(1);//去掉第一个字
                return ss;
            }
            else
            {
                return str;
            }
        }


        #endregion

        #region 金辉微信链接字符串
        public static string WeChat_JinHuiConnectionString {
            get {

                string connectionString = WebConfigurationManager.ConnectionStrings["WeChat_JinHui"].ConnectionString;

                return connectionString;
            }
        }
        #endregion
    }
}
