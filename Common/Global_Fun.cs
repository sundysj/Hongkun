using Dapper;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MobileSoft.Common
{
    public class Global_Fun : System.Web.UI.Page
    {
        public static string AppWebSettings(string Key)
        {
            try
            {
                AppSettingsReader Rd = new AppSettingsReader();
                string v = "";
                return Convert.ToString(Rd.GetValue(Key, v.GetType()));
            }
            catch (Exception)
            {
                return "";
            }

        }

        public static string Tw2bsConnectionString(string entry)
        {
            if (entry == "1")
            {
                // 125.64.16.10
                return Global_Fun.GetConnectionString("10Connection");
            }
            else if (entry == "2")
            {
                // 125.64.16.14
                return Global_Fun.GetConnectionString("14Connection");
            }
            else if (entry == "3")
            {
                return Global_Fun.GetConnectionString("37Connection");
            }
            else if (entry == "4")
            {
                return Global_Fun.GetConnectionString("19Connection");
            }
            else if (entry == "6")
            {
                return Global_Fun.GetConnectionString("36Connection");
            }
            else if (entry == "7")
            {
                return Global_Fun.GetConnectionString("ConnectionDX4");
            }
            else if (entry == "99")
            {
                // 125.64.16.12
                return Global_Fun.GetConnectionString("12Connection");
            }
            return null;
        }

        public static readonly object lockObj = new object { };
        public const string BURST_TYPE_CP = "CP";
        public const string BURST_TYPE_EQ = "EQ";
        public const string BURST_TYPE_SAFE = "SAFE";
        public const string BURST_TYPE_AMBIENT = "AMBIENT";   
        public const string BURST_TYPE_PATROL = "PATROL";
        public const string BURST_TYPE_CHARGE = "CHARGE";
        public const string BURST_TYPE_INCIDENT = "INCIDENT";
        public const string BURST_TYPE_RTS = "RTS";
        public const string BURST_TYPE_SUPERVISION = "SUPERVISION";

        public static string BurstConnectionString(int commId, string burstType, bool @readonly = false, 
            string tw2bsConnectionString = null, string hmWyglConnectionString = null)
        {
            var model = default(Global_Var.BurstConnectionModel);

            try
            {
                model = Global_Var.BurstConnectionPool?.Find(m => m.CommID == commId && m.Type == burstType);

                if (string.IsNullOrEmpty(tw2bsConnectionString))
                    tw2bsConnectionString = PubConstant.tw2bsConnectionString;

                if (string.IsNullOrEmpty(hmWyglConnectionString))
                    hmWyglConnectionString = PubConstant.hmWyglConnectionString;
            }
            catch (Exception)
            {

            }
            finally
            {
                if (model == null)
                    model = new Global_Var.BurstConnectionModel() { CommID = commId, Type = burstType };
            }

            if (string.IsNullOrEmpty(model.ConnectionString))
            {
                using (var conn = new SqlConnection(tw2bsConnectionString))
                {
                    var sql = @"SELECT DataBaseIp,DataBasePort,DataBaseName,DataBaseUser,DataBasePwd
                                FROM Tb_System_Burst WHERE CommID=@CommID AND BurstType=@BurstType;";

                    if (@readonly)
                    {
                        sql = @"SELECT DataBaseIp_Read AS DataBaseIp,DataBasePort_Read AS DataBasePort,DataBaseName_Read AS DataBaseName,
                                DataBaseUser_Read AS DataBaseUser,DataBasePwd_Read AS DataBasePwd 
                                FROM Tb_System_Burst WHERE CommID=@CommID AND BurstType=@BurstType;";
                    }

                    if (burstType == BURST_TYPE_RTS)
                    {
                        sql = @"SELECT RtsServer AS DataBaseIp,80 AS DataBasePort,RtsDbName AS DataBaseName,RtsUser AS DataBaseUser,RtsPwd AS DataBasePwd
                                FROM Tb_System_Corp WHERE isnull(IsDelete,0)=0 AND CorpID=@CorpID";
                    }

                    try
                    {
                        dynamic databaseInfo = conn.Query(sql, new { CommID = commId, BurstType = burstType, CorpID = Global_Var.LoginCorpID }).FirstOrDefault();
                        if (databaseInfo != null)
                        {
                            model.ConnectionString = $@"Max Pool Size=2048;Min Pool Size=0;Pooling=true;Connect Timeout=100;Connection Lifetime=60;
                                Data Source={databaseInfo.DataBaseIp}{(databaseInfo.DataBasePort == 80 ? "" : "," + databaseInfo.DataBasePort)};
                                Initial Catalog={databaseInfo.DataBaseName};
                                User ID={databaseInfo.DataBaseUser};
                                Password={databaseInfo.DataBasePwd}";
                        }
                        else
                        {
                            model.ConnectionString = hmWyglConnectionString;
                        }
                    }
                    catch (Exception)
                    {
                        model.ConnectionString = hmWyglConnectionString;
                    }

                    try
                    {
                        Global_Var.BurstConnectionPool.Add(model);
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            return model.ConnectionString;
        }

        #region 布尔型变量返回1或0

        public static string GetCheckState(Boolean State)
        {
            if (State == true) return "1";
            return "0";
        }

        #endregion

        #region 创建一个日期序列字符串

        public static string CreateKey(string KeyCode)
        {
            return KeyCode + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        }

        #endregion

        #region 绑定DropDownList控件
        public static void BindDropDownList(DropDownList Ddl, DataTable Dt, string Value, string Text)
        {
            Ddl.DataValueField = Value;
            Ddl.DataTextField = Text;
            Ddl.DataSource = Dt.DefaultView;
            Ddl.DataBind();

        }
        public static void BindDropDownList(DropDownList Ddl, DataTable Dt, string Value, string Text, string DefaultValue, string DefaultText)
        {
            Ddl.DataValueField = Value;
            Ddl.DataTextField = Text;
            Ddl.DataSource = Dt.DefaultView;
            Ddl.DataBind();
            Ddl.Items.Insert(0, new ListItem("", ""));

        }
        public static void BindDropDownList(HtmlSelect Ddl, DataTable Dt, string Value, string Text)
        {
            Ddl.DataValueField = Value;
            Ddl.DataTextField = Text;
            Ddl.DataSource = Dt.DefaultView;
            Ddl.DataBind();
        }
        public static void BindDropDownList(HtmlSelect Ddl, DataTable Dt, string Value, string Text, string DefaultValue, string DefaultText)
        {
            Ddl.DataValueField = Value;
            Ddl.DataTextField = Text;
            Ddl.DataSource = Dt.DefaultView;
            Ddl.DataBind();
            Ddl.Items.Insert(0, new ListItem("", ""));

        }
        #endregion

        #region 生成随机码

        public static string CreateRandomCode(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(36);
                if (temp != -1 && temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }

        #endregion

        #region JScript信息输出

        public void MsgScript(string Info, string Title, string Url)
        {
            if (Url.ToString() == "")
            {
                (HttpContext.Current.Handler as Page).ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=\"javascript\">ymPrompt.alert(\'" + Info + "\', null, null, \'" + Title + "\', handler); function handler(tp) {}</script>");
            }
            else
            {
                (HttpContext.Current.Handler as Page).ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=\"javascript\">ymPrompt.alert(\'" + Info + "\', null, null, \'" + Title + "\', handler); function handler(tp) {if(tp=='ok'){window.location.href='" + Url + "'} }</script>");
            }
        }

        public void WriteScript(string Info)
        {
            (HttpContext.Current.Handler as Page).ClientScript.RegisterStartupScript(this.GetType(), "", "<script type=\"text/javascript\" language=\"javascript\">" + Info.ToString() + "</script>");
        }

        public static void AjaxWriteScript(System.Web.UI.Page Page, string Info)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), Info, true);
        }

        public static void AjaxMsgScript(System.Web.UI.Page Page, string Info, string Title, string Url)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "ymPrompt.alert(\'" + Info + "\', null, null, \'" + Title + "\', handler); function handler(tp) {}", true);
        }
        #endregion

        #region 生成连接字符串

        public static string BuildLoginConnectSql(string DbServer, string DataBase, string DbUser, string DbPwd)
        {
            string strConnectSql = "Connect Timeout=100;Connection Lifetime=60;min pool size=0;max pool size=1024;connect timeout = 20; Pooling=true;Data Source=" + DbServer + ";Initial Catalog=" + DataBase + ";User ID=" + DbUser + ";Password=" + DbPwd;

            return strConnectSql;
        }

        #endregion

        #region 数据类型转换

        public static int StrToInt(string Str)
        {
            try
            {
                return Convert.ToInt32(Str);
            }
            catch
            {
                return 0;
            }
        }

        public static long StrToLong(string Str)
        {
            try
            {
                return Convert.ToInt64(Str);
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region 去除最后一个字符

        public static string CutLastChar(string Str)
        {
            return Str.Substring(0, Str.Length - 1);
        }

        #endregion

        #region 截取字符串
        /// <summary>
        /// 判断输入的字符串只包含汉字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsChineseCh(string input)
        {
            Regex regex = new Regex("^[\u4e00-\u9fa5]+$");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断输入的字符串包含全角字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEnglisAllCh(string input)
        {
            Regex regex = new Regex("^[Ａ-Ｚａ-ｚ，“”／、］［｜｛｝：；‘？〈〉《》（）＊………％￥＃＠！～＋＿＝－０-９]+$");
            return regex.IsMatch(input);
        }

        /// <summary>
		/// 获取包括汉字和全码的字符串
		/// </summary>
		/// <param name="strContent"></param>
		/// <param name="ilength"></param>
		/// <returns></returns>
		public static string GetSubString(string strContent, int ilength)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            char[] stringChar = strContent.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int nLength = 0;
            bool isCut = false;
            for (int i = 0; i < stringChar.Length; i++)
            {
                if (IsChineseCh(stringChar[i].ToString()) || IsEnglisAllCh(stringChar[i].ToString()))
                {
                    sb.Append(stringChar[i]);
                    nLength += 2;
                }
                else
                {
                    sb.Append(stringChar[i]);
                    nLength = nLength + 1;
                }

                if (nLength > ilength)
                {
                    isCut = true;
                    break;
                }
            }
            if (isCut)
                return sb.ToString() + "...";
            else
                return sb.ToString();
        }

        #endregion

        #region 获得web.config Connection字符串
        public static string GetConnectionString(string ConfigName)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings[ConfigName].ToString();

            return connectionString;
        }
        #endregion

        #region 将序列化对象转换成DataTable

        public static DataTable SerializeGetToDataTable(System.Web.HttpRequest Request, System.Web.HttpServerUtility Server)
        {
            DataTable dTable = new DataTable();

            int iCount = Request.QueryString.Count;

            for (int i = 0; i < iCount; i++)
            {
                DataColumn Col = new DataColumn();
                Col.DataType = System.Type.GetType("System.String");
                Col.ColumnName = Request.QueryString.GetKey(i).ToString();
                dTable.Columns.Add(Col);
            }

            DataRow Row = dTable.NewRow();
            foreach (DataColumn Col in dTable.Columns)
            {
                Row[Col.ColumnName] = Server.UrlDecode(Request[Col.ColumnName].ToString());
            }
            dTable.Rows.Add(Row);
            return dTable;
        }

        public static DataTable SerializePostToDataTable(System.Web.HttpRequest Request, System.Web.HttpServerUtility Server)
        {
            DataTable dTable = new DataTable();

            int iCount = Request.Form.Count;

            for (int i = 0; i < iCount; i++)
            {
                DataColumn Col = new DataColumn();
                Col.DataType = System.Type.GetType("System.String");
                Col.ColumnName = Request.Form.GetKey(i).ToString();
                dTable.Columns.Add(Col);
            }

            DataRow Row = dTable.NewRow();
            foreach (DataColumn Col in dTable.Columns)
            {
                Row[Col.ColumnName] = Server.UrlDecode(Request[Col.ColumnName].ToString());
            }
            dTable.Rows.Add(Row);
            return dTable;
        }

        #endregion

        public static DataSet AppendDataSet(DataTable A, int PageCount)
        {
            DataSet Ds = new DataSet();

            DataTable TableA = new DataTable();
            TableA = A.Copy();
            TableA.TableName = "TableA";

            Ds.Tables.Add(TableA);

            DataTable TableB = new DataTable();
            TableB.TableName = "TableB";

            DataColumn NewColumn = new DataColumn();
            NewColumn.ColumnName = "PageCount";
            NewColumn.DataType = System.Type.GetType("System.String");
            TableB.Columns.Add(NewColumn);

            DataRow Row = TableB.NewRow();
            Row["PageCount"] = PageCount;
            TableB.Rows.Add(Row);

            Ds.Tables.Add(TableB);

            return Ds;
        }

        #region 绑定服务器控件的值
        public static void BindControl(System.Web.UI.Page P, DataRow dRow)
        {
            foreach (Control c in P.Form.Controls)
            {
                if (c.GetType() == typeof(Label))
                {
                    Label tLabLabel = (Label)c;

                    if (dRow.Table.Columns.Contains(tLabLabel.ID))
                    {
                        tLabLabel.Text = dRow[tLabLabel.ID].ToString();
                    }
                }
                else if (c.GetType() == typeof(HiddenField))
                {
                    HiddenField tHidden = (HiddenField)c;

                    if (dRow.Table.Columns.Contains(tHidden.ID))
                    {
                        tHidden.Value = dRow[tHidden.ID].ToString();
                    }
                }
                else if (c.GetType() == typeof(TextBox))
                {
                    TextBox textBox = (TextBox)c;

                    if (dRow.Table.Columns.Contains(textBox.ID))
                    {
                        textBox.Text = dRow[textBox.ID].ToString();
                    }
                }
                else if (c.GetType() == typeof(DropDownList))
                {
                    DropDownList tdrop = (DropDownList)c;

                    if (dRow.Table.Columns.Contains(tdrop.ID))
                    {
                        tdrop.SelectedValue = dRow[tdrop.ID].ToString();
                    }
                }
                else if (c.GetType() == typeof(HtmlInputText))
                {
                    HtmlInputText tinput = (HtmlInputText)c;

                    if (dRow.Table.Columns.Contains(tinput.ID))
                    {
                        tinput.Value = dRow[tinput.ID].ToString();
                    }
                }
            }

        }
        #endregion

        #region GetXML
        public static string GetXML(int IsSucc, string ErrMsg, int PageCount, int Counts, string ListContent)
        {
            if (ErrMsg != "")
            {
                IsSucc = 0;
            }
            StringBuilder ContXmlS = new StringBuilder("");
            ContXmlS.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + "\r\n");
            ContXmlS.Append("<Message>");
            ContXmlS.Append("<Head>");
            ContXmlS.Append("<IsSucc>" + IsSucc.ToString() + "</IsSucc>");
            ContXmlS.Append("<ErrMsg>" + ErrMsg + "</ErrMsg>");
            ContXmlS.Append("<PageCount>" + PageCount.ToString() + "</PageCount>");
            ContXmlS.Append("<Counts>" + Counts.ToString() + "</Counts>");

            ContXmlS.Append("</Head>");
            ContXmlS.Append("<Lists>");
            ContXmlS.Append(ListContent);
            ContXmlS.Append("</Lists>");

            ContXmlS.Append("</Message>");

            return ContXmlS.ToString();
        }
        #endregion

        #region 发送http
        private string SendHttp(string Url, string Contents)
        {
            //#region 发送
            //HttpWebRequest request = null;

            //byte[] postData;

            //Uri uri = new Uri(Url);
            //if (uri.Scheme == "https")
            //{
            //    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);
            //}
            //HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);
            //HttpWebRequest.DefaultCachePolicy = policy;

            //request = (HttpWebRequest)WebRequest.Create(uri);
            //request.AllowAutoRedirect = false;
            //request.AllowWriteStreamBuffering = false;
            //request.Method = "POST";

            //postData = Encoding.GetEncoding("utf-8").GetBytes(Contents);

            //request.ContentType = "application/x-www-form-urlencoded";
            ////request.ContentType = "text/plain;charset = utf-8"; //request.ContentType = "text/plain";
            //request.ContentLength = postData.Length;
            //request.KeepAlive = false;

            //Stream reqStream = request.GetRequestStream();
            //reqStream.Write(postData, 0, postData.Length);
            //reqStream.Close();
            //#endregion

            //#region 响应
            //string respText = "";

            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Stream resStream = response.GetResponseStream();

            //MemoryStream ms = new MemoryStream();
            //byte[] buf = new byte[4096];
            //int count;
            //while ((count = resStream.Read(buf, 0, buf.Length)) > 0)
            //{
            //    ms.Write(buf, 0, count);
            //}
            //resStream.Close();
            //respText = Encoding.GetEncoding("utf-8").GetString(ms.ToArray());
            //#endregion

            return "";
        }

        #endregion


        [DllImport("kernel32.dll", EntryPoint = "GetSystemDefaultLCID")]
        public static extern int GetSystemDefaultLCID();
        [DllImport("kernel32.dll", EntryPoint = "SetLocaleInfoA")]
        public static extern int SetLocaleInfo(int Locale, int LCType, string lpLCData);
        public const int LOCALE_SLONGDATE = 0x20;
        public const int LOCALE_SSHORTDATE = 0x1F;
        public const int LOCALE_STIME = 0x1003;

        public static void SetDateTimeFormat()
        {
            try
            {
                int x = GetSystemDefaultLCID();
                SetLocaleInfo(x, LOCALE_STIME, "HH:mm:ss");        //时间格式  
                SetLocaleInfo(x, LOCALE_SSHORTDATE, "yyyy-MM-dd");   //短日期格式    
                SetLocaleInfo(x, LOCALE_SLONGDATE, "yyyy-MM-dd");   //长日期格式   
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static string GetNetType(string server)
        {
            var netType = "1";
            if (server.StartsWith("125.64.16.10"))
            {
                netType = "1";
            }
            else if (server.StartsWith("125.64.16.14"))
            {
                netType = "2";
            }
            else if (server.StartsWith("222.211.83.164"))
            {
                netType = "3";
            }
            else if (server.StartsWith("222.211.83.167"))
            {
                netType = "7";
            }

            return netType;
        }
    }
}
