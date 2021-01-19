using log4net;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using TWTools.Logger;

namespace Common
{
    public sealed class Push
    {
        /// <summary>
        /// 获取AppKey & AppSecret
        /// </summary>
        /// <param name="tw2bsConnectionString">tw2_bs数据库连接字符串</param>
        /// <param name="corpId">公司编号</param>
        /// <param name="appKey">AppKey</param>
        /// <param name="appSecret">AppSecret</param>
        /// <param name="isCust">是否是业主App</param>
        public static bool GetAppKeyAndAppSecret(string tw2bsConnectionString, string corpId,
            out string appIdentifier, out string appKey, out string appSecret, bool isCust = false)
        {
            appKey = null;
            appSecret = null;
            appIdentifier = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(tw2bsConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        SqlParameter parameter = new SqlParameter()
                        {
                            ParameterName = "CorpID",
                            Value = corpId
                        };

                        cmd.Connection = conn;
                        cmd.CommandText = @"SELECT * FROM Tb_System_CorpAppPushSet WHERE isnull(IsAppPushMsg,0)=1 AND CorpID=@CorpID";
                        cmd.Parameters.Add(parameter);

                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }

                        SqlDataReader dataReader = cmd.ExecuteReader();

                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                if (isCust)
                                {
                                    if (dataReader["CustAppKey"] != null)
                                        appKey = dataReader["CustAppKey"].ToString();
                                    if (dataReader["CustAppSecret"] != null)
                                        appSecret = dataReader["CustAppSecret"].ToString();
                                }
                                else
                                {
                                    if (dataReader["PropertyAppKey"] != null)
                                        appKey = dataReader["PropertyAppKey"].ToString();
                                    if (dataReader["PropertyAppSecret"] != null)
                                        appSecret = dataReader["PropertyAppSecret"].ToString();
                                }

                                if (dataReader["AppPushPackage"] != null)
                                    appIdentifier = dataReader["AppPushPackage"].ToString();
                                else
                                    appIdentifier = "unknown";

                                dataReader.Close();
                                break;
                            }
                        }

                        // 记录错误日志
                        if (string.IsNullOrEmpty(appKey) || string.IsNullOrEmpty(appSecret))
                        {
                            ILog logger = TWRollingFileLogger.GetCustomLogger(TWTools.Common.DefaultLoggerName, TWTools.Common.DefaultLoggerSource, "Error");
                            logger.Error(string.Format("App推送未配置或配置错误，IsCust={0}，CorpID={1}", isCust ? "是" : "否", corpId));
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists("TWInterface"))
                {
                    EventLog.CreateEventSource("TWInterface", "Push");
                }
                EventLog.WriteEntry("TWInterface", ex.Message, EventLogEntryType.Error);
                return false;
            }
        }




        /// <summary>
        /// 获取AppKey & AppSecret
        /// </summary>
        /// <param name="tw2bsConnectionString">tw2_bs数据库连接字符串</param>
        /// <param name="corpId">公司编号</param>
        /// <param name="appKey">AppKey</param>
        /// <param name="appSecret">AppSecret</param>
        /// <param name="isCust">是否是业主App</param>
        public static bool GetBusinessAppKeyAndAppSecret(string tw2bsConnectionString, string corpId,
            out string appIdentifier, out string appKey, out string appSecret)
        {
            appKey = null;
            appSecret = null;
            appIdentifier = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(tw2bsConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        SqlParameter parameter = new SqlParameter()
                        {
                            ParameterName = "CorpID",
                            Value = corpId
                        };

                        cmd.Connection = conn;
                        cmd.CommandText = @"SELECT * FROM Tb_System_CorpAppPushSet WHERE isnull(IsAppPushMsg,0)=1 AND CorpID=@CorpID";
                        cmd.Parameters.Add(parameter);

                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }

                        SqlDataReader dataReader = cmd.ExecuteReader();

                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                if (dataReader["BusinessAppKey"] != null)
                                    appKey = dataReader["BusinessAppKey"].ToString();
                                if (dataReader["BusinessAppSecret"] != null)
                                    appSecret = dataReader["BusinessAppSecret"].ToString();


                                if (dataReader["AppPushPackage"] != null)
                                    appIdentifier = dataReader["AppPushPackage"].ToString();
                                else
                                    appIdentifier = "unknown";

                                dataReader.Close();
                                break;
                            }
                        }
                        // 记录错误日志
                        if (string.IsNullOrEmpty(appKey) || string.IsNullOrEmpty(appSecret))
                        {
                            ILog logger = TWRollingFileLogger.GetCustomLogger(TWTools.Common.DefaultLoggerName, TWTools.Common.DefaultLoggerSource, "Error");
                            logger.Error(string.Format("App推送未配置或配置错误，CorpID={0}", corpId));
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists("TWInterface"))
                {
                    EventLog.CreateEventSource("TWInterface", "Push");
                }
                EventLog.WriteEntry("TWInterface", ex.Message, EventLogEntryType.Error);
                return false;
            }
        }
    }
}
