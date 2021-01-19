using Common;
using Dapper;
using DapperExtensions;
using EIAC.SSO.PSO;
using MobileSoft.BLL.Sys;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Web.Configuration;

namespace Business
{
    public class Login : PubInfo
    {
        public Login()
        {
            base.Token = "20160321LOGIN";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误!");

            string strEntryID = "";
            string strOrganCode = "";
            bool bCanEntry = false;
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            string NetType = Row["Net"].ToString();
            string Account = Row["Account"].ToString();
            string LoginPwd = Row["LoginPwd"].ToString();

            Global_Var.SystemType = "property";

            string[] arrUser = Account.Split('-');
            if (arrUser.Length > 1)
            {
                Global_Var.CorpId = arrUser[0].ToString();
                Global_Var.CorpID = Global_Var.CorpId;
                Account = arrUser[1].ToString();
                Global_Var.LoginCode = Account;
            }
            else
            {
                Trans.Result = JSONHelper.FromString(false, "用户名格式错误!");
                return;
            }

            PubConstant.tw2bsConnectionString = Global_Fun.Tw2bsConnectionString(NetType);
            DbHelperSQL.ConnectionString = PubConstant.tw2bsConnectionString;
            //获得所在公司的数据库连接字符串
            AppGlobal.GetHmWyglConnection();

            // 登出，记录登出日志
            if (Trans.Command == "2")
            {
                WriteLogout(Account);
                return;
            }

            // 鸿坤EAC系统访问令牌
            string eacToken = null;

            string IsAd = "true";
            try
            {
                IsAd = Global_Fun.AppWebSettings("IsAd");
            }
            catch (Exception)
            {
                IsAd = "false";
            }

            string strSQLUser = "";

            if (IsAd == "true")
            {
                //AD域进行验证
                string CheckResult = IsAuthenticated(Account, LoginPwd);
                if (CheckResult == "true")
                {
                    strSQLUser = " LoginCode='" + Account + "' and IsDelete = 0 ";
                }
                else
                {
                    strSQLUser = " LoginCode='-1' and IsDelete=-1";
                }
            }
            else
            {
                // 中南、实地
                // 保利、隆泰、丽创
                if (LoginPwd.Length == 32 || Global_Var.LoginCorpID == "1329" || Global_Var.LoginCorpID == "2009" || Global_Var.LoginCorpID == "2022")
                {
                    strSQLUser = string.Format(" LoginCode='{0}' AND (PassWord='{1}' OR dbo.GetMD5(PassWord)='{2}') AND IsDelete = 0",
                    Account, LoginPwd, LoginPwd.ToUpper());
                }
                else if (Global_Var.CorpId == "1971")
                {
                    if (NetType == "99")
                    {
                        strSQLUser = string.Format(" LoginCode='{0}' AND PassWord='{1}' AND IsDelete = 0 ", Account, LoginPwd);
                    }
                    else
                    {
                        //2018-1-21 新增敏捷AD域登录验证
                        string urlmj = @"http://172.27.1.240/Api/ADLogin/Login?userName=" + Account + "&userPwd=" + LoginPwd;
                        string mjsuss = TWRequest.HttpGet(urlmj);
                        if (mjsuss == "\"success\"")
                        {
                            strSQLUser = " LoginCode='" + Account + "' and IsDelete = 0 ";
                        }
                        else
                        {
                            strSQLUser = " LoginCode='" + Account + "'  AND PassWord='" + LoginPwd.ToString() + "' AND IsDelete = 0 ";
                        }
                    }
                }
                else
                {
                    strSQLUser = string.Format(" LoginCode='{0}' AND PassWord='{1}' AND IsDelete = 0 ", Account, LoginPwd);
                }
            }

            #region 实地单点登录判断
            if (SDLogin(Account, LoginPwd))
            {
                // 如果实地单点登录成功，无需密码
                strSQLUser = string.Format(" LoginCode='{0}' AND IsDelete = 0 ", Account);
            }
            #endregion
            Bll_Tb_Sys_User Bll = new Bll_Tb_Sys_User();

            DataTable dTable = Bll.GetList(strSQLUser).Tables[0];

            if (dTable.Rows.Count > 0)
            {
                DataRow DRow = dTable.Rows[0];

                if (DRow["IsMobile"].ToString() != "1")
                {
                    Trans.Result = JSONHelper.FromString(false, "不允许手机端登录!");
                    return;
                }
                else
                {
                    //存在此用户,登陆成功
                    Global_Var.UserCode = DRow["UserCode"].ToString();
                    Global_Var.LoginUserCode = DRow["UserCode"].ToString();
                    Global_Var.UserName = DRow["UserName"].ToString();
                    Global_Var.LoginUserName = DRow["UserName"].ToString();
                    Global_Var.LoginDepCode = DRow["DepCode"].ToString();
                    Global_Var.LoginMobile = DRow["MobileTel"].ToString();

                    WriteLog(DRow, "登陆系统");

                    #region 可进入的系统

                    string strSQL = " AND UserCode  = '" + Global_Var.LoginUserCode.ToString() + "' ";
                    //DataTable dTableEntry = (new BusinessRule.TWBusinRule(LoginSQLConnStr)).Sys_User_RoleData_Filter(strSQL);
                    MobileSoft.BLL.Sys.Bll_Tb_Sys_RoleData A = new Bll_Tb_Sys_RoleData();
                    DataTable dTableEntry = A.Sys_User_RoleData_Filter(strSQL);
                    if (dTableEntry.Rows.Count > 0)
                    {
                        //查询默认项目
                        DataRow[] DSelRows = dTableEntry.Select(" EntryType = 1 ");
                        if (DSelRows.Length > 0)
                        {
                            strEntryID = DSelRows[0]["CommID"].ToString();
                            strOrganCode = DSelRows[0]["OrganCode"].ToString();
                        }
                        else
                        {
                            strEntryID = dTableEntry.Rows[0]["CommID"].ToString();
                            strOrganCode = dTableEntry.Rows[0]["OrganCode"].ToString();
                        }
                        bCanEntry = true;
                    }
                    else
                    {
                        bCanEntry = false;
                    }

                    dTableEntry.Dispose();

                    #endregion

                    if (bCanEntry)
                    {
                        int iCommID = AppGlobal.StrToInt(strEntryID);

                        if (iCommID != 0)
                        {
                            #region 加载管理处信息
                            string strSQLComm = " IsDelete = 0 and CorpID = " + Global_Var.CorpId.ToString() + " and CommID = " + iCommID.ToString() + " ";
                            MobileSoft.BLL.HSPR.Bll_Tb_HSPR_Community B = new MobileSoft.BLL.HSPR.Bll_Tb_HSPR_Community();
                            DataTable dTableComm = B.GetList(strSQLComm).Tables[0];

                            if (dTableComm.Rows.Count > 0)
                            {
                                DataRow DRowComm = dTableComm.Rows[0];

                                dTable.Rows[0]["CommID"] = DRowComm["CommID"];
                                Global_Var.LoginCommID = DRowComm["CommID"].ToString();
                                Global_Var.LoginCommName = DRowComm["CommName"].ToString();
                                Global_Var.LoginCorpID = DRowComm["CorpID"].ToString();
                                Global_Var.LoginBranchID = DRowComm["BranchID"].ToString();
                                Global_Var.LoginOrganCode = DRowComm["OrganCode"].ToString();
                                Global_Var.LoginCorpRegionCode = DRowComm["CorpRegionCode"].ToString();
                                Global_Var.LoginCommType = DRowComm["CommType"].ToString();

                            }
                            dTableComm.Dispose();
                            #endregion

                            //查询人员可进入小区的岗位
                            MobileSoft.BLL.Sys.Bll_Tb_Sys_User C = new Bll_Tb_Sys_User();
                            Global_Var.LoginRoles = C.Sys_User_FilterRoles(Global_Var.LoginUserCode, Global_Var.LoginOrganCode, AppGlobal.StrToInt(Global_Var.LoginCommID));
                            Global_Var.LoginSysTitle = Global_Var.LoginCommName;
                            Global_Var.LoginFunType = "5";//进入管理处系统
                        }
                        else
                        {
                            if (strOrganCode != "")
                            {
                                #region 查询区域
                                string strSQLOrgan = "";
                                if (strOrganCode == "" || strOrganCode == "01")
                                {
                                    strSQLOrgan = " IsDelete = 0 and OrganCode = '01'";
                                    Global_Var.LoginOrganCode = "01";
                                }
                                else
                                {
                                    strSQLOrgan = " IsDelete = 0 and OrganCode = '" + strOrganCode + "' and IsComp = 1 ";
                                    Global_Var.LoginOrganCode = strOrganCode;
                                }

                                MobileSoft.BLL.Sys.Bll_Tb_Sys_Organ D = new Bll_Tb_Sys_Organ();
                                DataTable dTableOrgan = D.GetList(strSQLOrgan).Tables[0];
                                if (dTableOrgan.Rows.Count > 0)
                                {
                                    DataRow DRowOrgan = dTableOrgan.Rows[0];
                                    Global_Var.LoginOrganName = DRowOrgan["OrganName"].ToString();
                                    Global_Var.LoginCorpID = Global_Var.CorpId.ToString();
                                    Global_Var.LoginCommID = "0";
                                    Global_Var.LoginOrganCode = DRowOrgan["OrganCode"].ToString();
                                    Global_Var.LoginCommType = "";
                                }
                                dTableOrgan.Dispose();
                                #endregion

                                //查询人员可进入小区的岗位

                                MobileSoft.BLL.Sys.Bll_Tb_Sys_User C = new Bll_Tb_Sys_User();
                                Global_Var.LoginRoles = C.Sys_User_FilterRoles(Global_Var.LoginUserCode, Global_Var.LoginOrganCode, AppGlobal.StrToInt(Global_Var.LoginCommID));
                                Global_Var.LoginSysTitle = Global_Var.LoginOrganName.ToString();
                                Global_Var.LoginFunType = "1";//进入公司系统
                            }
                        }
                    }
                    else
                    {
                        Global_Var.LoginFunType = "1";
                        string strSQLOrgan = "";
                        if (strOrganCode == "" || strOrganCode == "01")
                        {
                            strSQLOrgan = " IsDelete = 0 and OrganCode = '01'";
                            Global_Var.LoginOrganCode = "01";
                        }
                        else
                        {
                            strSQLOrgan = " IsDelete = 0 and OrganCode = '" + strOrganCode + "' and IsComp = 1 ";
                            Global_Var.LoginOrganCode = strOrganCode;
                        }

                        MobileSoft.BLL.Sys.Bll_Tb_Sys_Organ D = new MobileSoft.BLL.Sys.Bll_Tb_Sys_Organ();
                        DataTable dTableOrgan = D.GetList(strSQLOrgan).Tables[0];
                        if (dTableOrgan.Rows.Count > 0)
                        {
                            DataRow DRowOrgan = dTableOrgan.Rows[0];
                            Global_Var.LoginOrganName = DRowOrgan["OrganName"].ToString();
                            Global_Var.LoginCorpID = Global_Var.CorpId.ToString();
                            Global_Var.LoginCommID = "0";
                            Global_Var.LoginOrganCode = DRowOrgan["OrganCode"].ToString();
                            Global_Var.LoginCommType = "";
                        }
                        dTableOrgan.Dispose();

                        MobileSoft.BLL.Sys.Bll_Tb_Sys_User E = new MobileSoft.BLL.Sys.Bll_Tb_Sys_User();
                        Global_Var.LoginRoles = E.Sys_User_FilterRoles(Global_Var.LoginUserCode, Global_Var.LoginOrganCode, DataSecurity.StrToInt(Global_Var.LoginCommID));

                        Global_Var.LoginSysTitle = Global_Var.LoginOrganName.ToString();
                    }

                    MobileSoft.BLL.Sys.Bll_Tb_Sys_Organ F = new MobileSoft.BLL.Sys.Bll_Tb_Sys_Organ();
                    Global_Var.LoginOrganCorp = F.Sys_Organ_GetComp(Global_Var.LoginOrganCode);
                    if (Global_Var.SysVersion.ToString().ToLower() != "group")
                    {
                        Global_Var.LoginOrganCorp = Global_Var.LoginOrganCode.Substring(0, 2);
                    }

                    Trans.Result = JSONHelper.FromString(dTable);
                }
            }
            else
            {
                Trans.Result = JSONHelper.FromString(false, "账号或密码错误!");
                return;
            }
        }

        /// <summary>
        /// 提供给其他接口判断是否登录
        /// </summary>
        /// <param name="Trans"></param>
        /// <returns></returns>
        public bool isLogin(ref Common.Transfer Trans, bool verifyPassword = true)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            string NetType = Row["Net"].ToString();
            string Account = Row["Account"].ToString();
            string LoginPwd = Row["LoginPwd"].ToString();

            Global_Var.SystemType = "property";

            string[] arrUser = Account.Split('-');
            if (arrUser.Length > 1)
            {
                Global_Var.CorpId = arrUser[0].ToString();
                Global_Var.CorpID = Global_Var.CorpId;
                Account = arrUser[1].ToString();
                Global_Var.LoginCode = Account;
            }
            else
            {
                Trans.Result = JSONHelper.FromString(false, "用户名格式错误!");
                return false;
            }

            PubConstant.tw2bsConnectionString = Global_Fun.Tw2bsConnectionString(NetType);
            DbHelperSQL.ConnectionString = PubConstant.tw2bsConnectionString;
            //获得所在公司的数据库连接字符串
            AppGlobal.GetHmWyglConnection();

            string IsAd = "true";
            try
            {
                IsAd = Global_Fun.AppWebSettings("IsAd");
            }
            catch (Exception)
            {
                IsAd = "false";
            }

            string strSQLUser = "";

            if (IsAd == "true")
            {
                //AD域进行验证
                string CheckResult = IsAuthenticated(Account, LoginPwd);
                if (CheckResult == "true")
                {
                    strSQLUser = " LoginCode='" + Account + "' and IsDelete = 0 ";
                }
                else
                {
                    strSQLUser = " LoginCode='-1' and IsDelete=-1";
                }
            }
            else
            {
                // 中南版本、金辉版本及其分支、保利版本及其分支
                if (LoginPwd.Length == 32)
                {
                    strSQLUser = string.Format(" LoginCode='{0}' AND (PassWord='{1}' OR dbo.GetMD5(PassWord)='{1}') AND IsDelete = 0 ",
                    Account, LoginPwd);
                }
                else if (Global_Var.CorpId == "1971")
                {
                    //2018-1-21 新增敏捷AD域登录验证
                    if (NetType == "99")
                    {
                        strSQLUser = " LoginCode='" + Account + "'  AND PassWord='" + LoginPwd.ToString() + "' AND IsDelete = 0 ";
                    }
                    else
                    {
                        string urlmj = @"http://172.27.1.240/Api/ADLogin/Login?userName=" + Account + "&userPwd=" + LoginPwd;
                        string mjsuss = TWRequest.HttpGet(urlmj);
                        if (mjsuss == "\"success\"")
                        {
                            strSQLUser = " LoginCode='" + Account + "' and IsDelete = 0 ";
                        }
                        else
                        {
                            strSQLUser = " LoginCode='" + Account + "'  AND PassWord='" + LoginPwd.ToString() + "' AND IsDelete = 0 ";
                        }
                    }
                }
                else
                {
                    strSQLUser = string.Format(" LoginCode='{0}' AND IsDelete = 0 ", Account);
                    // 强制要求验证密码
                    if (verifyPassword == true)
                    {
                        strSQLUser += $" AND PassWord='{LoginPwd}'";
                    }
                }

            }
            #region 实地单点登录判断
            if (SDLogin(Account, LoginPwd))
            {
                // 如果实地单点登录成功，无需密码
                strSQLUser = string.Format(" LoginCode='{0}' AND IsDelete = 0", Account);
            }
            #endregion
            DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;

            Bll_Tb_Sys_User Bll = new Bll_Tb_Sys_User();
            DataTable dTable = Bll.GetList(strSQLUser).Tables[0];

            if (dTable.Rows.Count > 0)
            {
                DataRow DRow = dTable.Rows[0];

                if (DRow["IsMobile"].ToString() != "1")
                {
                    Trans.Result = JSONHelper.FromString(false, "不允许手机端登录!");
                    return false;
                }
                else
                {
                    //存在此用户,登陆成功
                    Global_Var.UserCode = DRow["UserCode"].ToString();
                    Global_Var.LoginUserCode = DRow["UserCode"].ToString();
                    Global_Var.UserName = DRow["UserName"].ToString();
                    Global_Var.LoginUserName = DRow["UserName"].ToString();
                    Global_Var.LoginDepCode = DRow["DepCode"].ToString();
                    Global_Var.LoginMobile = DRow["MobileTel"].ToString();

                    // 2017年6月12日16:16:01，谭洋，新增LoginSortDepCode
                    Global_Var.LoginSortDepCode = DRow["SortDepCode"].ToString();

                    //WriteLog(DRow, "登陆系统");

                    return true;
                }
            }
            else
            {
                Trans.Result = JSONHelper.FromString(false, "帐号或密码错误!");
                return false;
            }
        }

        /// <summary>
        /// 实地单点登录接口
        /// </summary>
        /// <returns></returns>
        public static bool SDLogin(string loginCode, string loginPwd)
        {
            try
            {
                // 是否启用单点登录
                if (!"1".Equals(WebConfigurationManager.AppSettings.Get("SD_SSO_Enabled")))
                {
                    return false;
                }
                string Api = WebConfigurationManager.AppSettings.Get("SD_SSO_API");
                string SystemCode = WebConfigurationManager.AppSettings.Get("SD_SSO_SystemCode");
                string Key = WebConfigurationManager.AppSettings.Get("SD_SSO_Key");

                string data = string.Format("systemCode={0}&userCode={1}&password={2}&sign={3}", SystemCode, loginCode, loginPwd, AppGlobal.getMd5Hash(loginCode + loginPwd + Key).ToUpper());
                DotNet4.Utilities.HttpHelper http = new DotNet4.Utilities.HttpHelper();
                DotNet4.Utilities.HttpItem httpItem = new DotNet4.Utilities.HttpItem()
                {
                    URL = Api,//URL     必需项  
                    Method = "POST",//URL     可选项 默认为Get  
                    Timeout = 3000,//连接超时时间     可选项默认为100000  
                    ReadWriteTimeout = 3000,//写入Post数据超时时间     可选项默认为30000  
                    IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写  
                    ContentType = "application/x-www-form-urlencoded",
                    PostEncoding = Encoding.UTF8,
                    Postdata = data,
                    ResultType = DotNet4.Utilities.ResultType.String,//返回数据类型，是Byte还是String  
                };
                DotNet4.Utilities.HttpResult result = http.GetHtml(httpItem);
                JObject jObj = JsonConvert.DeserializeObject<JObject>(result.Html);
                if (!(bool)jObj["success"])
                {
                    GetLog().Error(JsonConvert.SerializeObject(jObj));
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return false;
            }
        }
        //活动目录判断
        public static string IsAuthenticated(String username, String pwd)
        {
            string IsAd = "true";

            try
            {
                IsAd = Global_Fun.AppWebSettings("IsAd");
            }
            catch (Exception E)
            {
                return "true";
            }

            string ADDomain = Global_Fun.AppWebSettings("ADDomain");

            if (IsAd == "true")
            {
                try
                {
                    string domain = ADDomain;
                    String domainAndUsername = domain + @"\" + username;
                    DirectoryEntry entry = new DirectoryEntry("LDAP://" + domain, username, pwd);
                    Object obj = entry.NativeObject;
                    DirectorySearcher search = new DirectorySearcher(entry);
                    search.Filter = "(SAMAccountName=" + username + ")";
                    search.PropertiesToLoad.Add("cn");
                    SearchResult result = search.FindOne();
                    if (null == result)
                    {
                        return "活动目录验证该账号失败";
                    }
                }
                catch (Exception E)
                {
                    return "活动目录:" + E.Message.ToString();
                }
            }

            return "true";
        }

        #region 写操作日志
        public void WriteLog(DataRow Rows, string OperateName)
        {
            try
            {
                IDbConnection con = new System.Data.SqlClient.SqlConnection(PubConstant.hmWyglConnectionString);
                int iCorpID = AppGlobal.StrToInt(Global_Var.LoginCorpID);
                int iBranchID = AppGlobal.StrToInt(Global_Var.LoginBranchID);
                int iCommID = AppGlobal.StrToInt(Global_Var.LoginCommID);
                MobileSoft.Model.Sys.Tb_Sys_Log Item = new MobileSoft.Model.Sys.Tb_Sys_Log();
                Item.CorpID = iCorpID;
                Item.BranchID = iBranchID;
                Item.CommID = iCommID;
                Item.LocationIP = "";
                Item.ManagerCode = Rows["UserCode"].ToString();
                Item.LogTime = DateTime.Now;
                Item.OperateName = OperateName;
                Item.Memo = "物管App登录";
                Item.OperateURL = "";
                con.Insert<MobileSoft.Model.Sys.Tb_Sys_Log>(Item);
                //(new BusinessRule.TWBusinRule(LoginSQLConnStr)).Sys_Log_Base_Insert(ref Item);
            }
            catch (Exception ex)
            { }
        }

        public void WriteLogout(string loginCode)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    var userCode = conn.Query<string>("SELECT UserCode FROM Tb_Sys_User WHERE LoginCode=@LoginCode AND isnull(IsDelete,0)=0",
                        new { LoginCode = loginCode }).FirstOrDefault();

                    if (!string.IsNullOrEmpty(userCode))
                    {
                        int iCorpID = AppGlobal.StrToInt(Global_Var.LoginCorpID);
                        int iBranchID = AppGlobal.StrToInt(Global_Var.LoginBranchID);
                        int iCommID = AppGlobal.StrToInt(Global_Var.LoginCommID);
                        MobileSoft.Model.Sys.Tb_Sys_Log Item = new MobileSoft.Model.Sys.Tb_Sys_Log();
                        Item.CorpID = iCorpID;
                        Item.BranchID = iBranchID;
                        Item.CommID = iCommID;
                        Item.LocationIP = "";
                        Item.ManagerCode = userCode;
                        Item.LogTime = DateTime.Now;
                        Item.OperateName = "登出系统";
                        Item.Memo = "物管App登出";
                        Item.OperateURL = "";
                        conn.Insert<MobileSoft.Model.Sys.Tb_Sys_Log>(Item);
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion

    }
}
