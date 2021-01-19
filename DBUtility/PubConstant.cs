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
        /// �õ�����ҵ�����ݿ������ַ���
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
        /// �õ����ŷ������ݿ�
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
        /// ������־��¼
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
        /// �õ�����ҵ�����ݿ������ַ���
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
        /// ����ϵͳ���ݿ�
        /// </summary>
        public static string TWAppConnectionString {
            get
            {
                return GetConnectionString("TWAppConnection");
            }
        }

        /// <summary>
        /// �õ�����ҵ�����ݿ������ַ���
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
        /// �õ�����ҵ�����ݿ������ַ���
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
        /// �õ��̼�BS���ݿ������ַ���
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
        /// �õ�����ҵ�����ݿ������ַ���
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
        /// �õ�����ҵ�����ݿ������ַ���
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
        /// �õ�����ҵ�����ݿ������ַ���
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
        /// �õ�����ҵ�����ݿ������ַ���
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
        /// �õ�����ҵ�����ݿ������ַ���
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
        /// �õ��̼����ݿ������ַ���
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
        /// �õ�web.config������������ݿ������ַ�����
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string ConfigName)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings[ConfigName].ToString();

            return connectionString;
        }

        /// <summary>
        /// �õ�web.config�������
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetAppSetString(string ConfigName)
        {
            
            string connectionString = WebConfigurationManager.AppSettings[ConfigName].ToString();

            return connectionString;
        }


        /// <summary>
        /// ��̩��ҵ���ݿ������ַ���
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
        /// ��̩��ҵ���²���Ĭ����
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


        #region �̹�԰�����ַ���
        /// <summary>
        /// ��ȡ�����ַ���(�̹�԰���⻧��ͬ����������ƽ̨��)
        /// </summary>
        /// <param name="CorpID">��˾ID</param>
        /// <param name="DbType">���ݿ����ͣ�rts����⣬write���⣬ReadServer1�ӿ⡾��֧��һ̨���⡿</param>
        /// <returns></returns>
        public static string GetSQLConnection(string CorpID,string DbType)
        {
            //��ȡTW2_bs���ݿ�
            string conn = GetConnectionString("SQLTW2BSConnection");            
            DataTable dt = Query("  select * from Tb_System_Corp with(nolock) where CorpId='" + CorpID + "' ", conn).Tables[0];
            string strDBServer = "";
            string strDBName = "";
            string strDBUser = "";
            string strDBPwd = "";
            if (dt.Rows.Count > 0)
            {
                DataRow DRow = dt.Rows[0];
                if (DbType.ToLower() == "rts")//����
                {
                    strDBServer= DRow["RtsServer"].ToString();
                    strDBName = ClearLogStr(DRow["RtsDBName"].ToString());
                    strDBUser = DRow["RtsUser"].ToString();
                    strDBPwd = DRow["RtsPwd"].ToString();
                }
                else if (DbType.ToLower() == "write")//���⣬д
                {                    
                    strDBServer = DRow["DBServer"].ToString();
                    strDBName = ClearLogStr(DRow["DbName"].ToString());
                    strDBUser = DRow["DBUser"].ToString();
                    strDBPwd = DRow["DBPwd"].ToString();
                }
                else if (DbType.ToLower() == "readserver1")//�ӿ�1  ����
                {
                    strDBServer = DRow["ReadServer1"].ToString();
                    strDBName = ClearLogStr(DRow["DbName"].ToString());
                    strDBUser = DRow["ReadServer1User"].ToString();
                    strDBPwd = DRow["ReadServer1Pwd"].ToString();
                }    
                //�ӿ�2
                //.
                //.
                //N
            }
             
            return string.Format("Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2048;Min Pool Size=0;Pooling=true;data source={0};initial catalog={1};PWD={3};persist security info=False;user id={2};packet size=4096", strDBServer, strDBName, strDBUser, strDBPwd);            

        }

        /// <summary>
        /// ��ȡ���ݿ����ƣ�֧��ƽ̨�棩        
        /// </summary>
        /// <param name="CorpID">��˾���</param>
        /// <param name="DbType">���ݿ����ͣ�rts����⣬write���⣬ReadServer1д��</param>
        /// <returns></returns>
        public static String GetDBServerName(string CorpID, string DbType)
        {
            //��ȡTW2_bs���ݿ�
            string conn = tw2bsConnectionString;
            DataTable dt = Query("  select * from Tb_System_Corp with(nolock) where CorpId='" + CorpID + "' ", conn).Tables[0];
            string strDBName = "";
            if (dt.Rows.Count > 0)
            {
                DataRow DRow = dt.Rows[0];
                if (DbType.ToLower() == "rts")//����
                {                    
                    strDBName = ClearLogStr(DRow["RtsDBName"].ToString());
                    
                }
                else if (DbType.ToLower() == "write")//���⣬д
                {                    
                    strDBName = ClearLogStr(DRow["DbName"].ToString());
                  
                }
                else if (DbType.ToLower() == "ReadServer1")//�ӿ�1  ����
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
                ss = ss.Substring(0, ss.Length - 1);//ȥ�����һ������
                ss = ss.Substring(1);//ȥ����һ����
                return ss;
            }
            else
            {
                return str;
            }
        }


        #endregion

        #region ���΢�������ַ���
        public static string WeChat_JinHuiConnectionString {
            get {

                string connectionString = WebConfigurationManager.ConnectionStrings["WeChat_JinHui"].ConnectionString;

                return connectionString;
            }
        }
        #endregion
    }
}
