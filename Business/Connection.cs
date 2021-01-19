using MobileSoft.Common;
using MobileSoft.DBUtility;
using System.Data;

namespace Business
{
    public class Connection
    {
        public static string GetConnection(string NetType)
        {
            string ConnectionString = "";
            //获得所在服务器的tw2_bs数据库连接字符串
            switch (NetType)
            {
                case "1":
                    ConnectionString = Global_Fun.GetConnectionString("10Connection");
                    break;
                case "2":
                    ConnectionString = Global_Fun.GetConnectionString("14Connection");
                    break;
                case "3":
                    ConnectionString = Global_Fun.GetConnectionString("37Connection");
                    break;
                case "4":
                    ConnectionString = Global_Fun.GetConnectionString("UnifiedConnectionString");
                    break;
                case "5":
                    ConnectionString = Global_Fun.GetConnectionString("ConnectionDX3");
                    break;
                case "6":
                    ConnectionString = Global_Fun.GetConnectionString("DYConnection");
                    break;
                case "7":
                    ConnectionString = Global_Fun.GetConnectionString("TestConnection");
                    break;

                case "8":
                    ConnectionString = Global_Fun.GetConnectionString("HKConnection");
                    break;

                case "9":
                    ConnectionString = Global_Fun.GetConnectionString("SDConnection");
                    break;
                case "10":
                    ConnectionString = Global_Fun.GetConnectionString("MJConnection");
                    break;

                case "11":
                    ConnectionString = Global_Fun.GetConnectionString("HJDataVConnection");
                    break;

                case "12":
                    ConnectionString = Global_Fun.GetConnectionString("SQLConnectionInterface");
                    break;
                case "13":
                    ConnectionString = Global_Fun.GetConnectionString("PolyDataVString");
                    break;

                default:
                    ConnectionString = Global_Fun.GetConnectionString("SQLConnection");
                    break;
            }

            return ConnectionString;
        }


        #region 获取连接字符串
        public static string GetCorpSQLContion(int CorpID)
        {
            string ConnStr = "";
            string strDBServer = "";
            string strDBName = "";
            string strDBUser = "";
            string strDBPwd = "";

            try
            {
                string strSQL = " and RegType = 1 and isnull(IsDelete,0) = 0 and CorpID = " + CorpID.ToString() + " ";

                DataTable dTableCorp = (new Business.TWBusinRule(PubConstant.tw2bsConnectionString)).System_Corp_Filter(strSQL);

                if (dTableCorp.Rows.Count > 0)
                {
                    DataRow DRow = dTableCorp.Rows[0];
                    strDBServer = DRow["DBServer"].ToString();
                    strDBName = DRow["DBName"].ToString();
                    strDBUser = DRow["DBUser"].ToString();
                    strDBPwd = DRow["DBPwd"].ToString();

                    //*连结字符串
                    ConnStr = string.Format("Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source={0};initial catalog={1};PWD={3};persist security info=False;user id={2};packet size=4096", strDBServer, strDBName, strDBUser, strDBPwd);
                }

                dTableCorp.Dispose();
            }
            catch
            {

            }

            return ConnStr;
        }

        #endregion


        # region 重构GetCorpSQLContion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Constr">配置文件KEY</param>
        /// <param name="CorpName"></param>
        /// <param name="CorpID"></param>
        /// <returns></returns>
        public static string GetCorpSQLContion_new(string Constr,string CorpName,int CorpID)
        {
            string ConnStr = "";
            string strDBServer = "";
            string strDBName = "";
            string strDBUser = "";
            string strDBPwd = "";

            try
            {
                string strSQL = "";
                if (CorpID>0)
                {
                    strSQL = " and RegType = 1 and isnull(IsDelete,0) = 0 and CorpID = " + CorpID;
                }
                if (CorpName!="")
                {
                    strSQL = " and RegType = 1 and isnull(IsDelete,0) = 0 and CorpName = '" + CorpName + "'";
                }

                DataTable dTableCorp = (new Business.TWBusinRule(PubConstant.GetConnectionString(Constr))).System_Corp_Filter(strSQL);

                if (dTableCorp.Rows.Count > 0)
                {
                    DataRow DRow = dTableCorp.Rows[0];
                    strDBServer = DRow["DBServer"].ToString();
                    strDBName = DRow["DBName"].ToString();
                    strDBUser = DRow["DBUser"].ToString();
                    strDBPwd = DRow["DBPwd"].ToString();

                    //*连结字符串
                    ConnStr = string.Format("Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source={0};initial catalog={1};PWD={3};persist security info=False;user id={2};packet size=4096", strDBServer, strDBName, strDBUser, strDBPwd);
                }
                else
                {
                    ConnStr = "";
                }

                dTableCorp.Dispose();
            }
            catch (System.Exception ex)
            {
                ConnStr = ex.Message;
            }

            return ConnStr;
        }

        #endregion



        public static string GetCorpCRMSQLContion(string strCon,int CorpID)
        {
            string ConnStr = "";
            string strDBServer = "";
            string strDBName = "";
            string strDBUser = "";
            string strDBPwd = "";

            try
            {
                string strSQL = " and RegType = 1 and isnull(IsDelete,0) = 0 and CorpID = " + CorpID.ToString() + " ";

                DataTable dTableCorp = (new Business.TWBusinRule(strCon)).System_Corp_Filter(strSQL);

                if (dTableCorp.Rows.Count > 0)
                {
                    DataRow DRow = dTableCorp.Rows[0];
                    strDBServer = DRow["DBServer"].ToString();
                    strDBName = DRow["DBName"].ToString();
                    strDBUser = DRow["DBUser"].ToString();
                    strDBPwd = DRow["DBPwd"].ToString();

                    //*连结字符串
                    ConnStr = string.Format("Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source={0};initial catalog={1};PWD={3};persist security info=False;user id={2};packet size=4096", strDBServer, strDBName, strDBUser, strDBPwd);
                }

                dTableCorp.Dispose();
            }
            catch
            {

            }

            return ConnStr;
        }

        public static string GetRZSQLContion(string strCon, string CorpID)
        {
            string ConnStr = "";
            string strDBServer = "";
            string strDBName = "";
            string strDBUser = "";
            string strDBPwd = "";

            try
            {
                string strSQL = " and RegType = 1 and isnull(IsDelete,0) = 0 and CorpID = " + CorpID + " ";

                DataTable dTableCorp = (new Business.TWBusinRule(strCon)).System_Corp_Filter(strSQL);

                if (dTableCorp.Rows.Count > 0)
                {
                    DataRow DRow = dTableCorp.Rows[0];
                    strDBServer = DRow["DBServer"].ToString();
                    strDBName = DRow["DBName"].ToString();
                    strDBUser = DRow["DBUser"].ToString();
                    strDBPwd = DRow["DBPwd"].ToString();

                    //*连结字符串
                    ConnStr = string.Format("Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source={0};initial catalog={1};PWD={3};persist security info=False;user id={2};packet size=4096", strDBServer, strDBName, strDBUser, strDBPwd);
                }

                dTableCorp.Dispose();
            }
            catch
            {

            }

            return ConnStr;
        }
    }
}
