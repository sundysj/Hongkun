using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Common;
using Dapper;
using DapperExtensions;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Sys;

namespace Business
{
    /// <summary>
    /// PMS用户鉴权
    /// </summary>
    public class PMSUserAuthentication : PubInfo
    {
        public PMSUserAuthentication()
        {
            Token = "20200810PMSUserAuthentication";
        }

        public override void Operate(ref Transfer Trans)
        {
            try
            {
                switch (Trans.Command)
                {
                    case "Login":
                        Login(ref Trans);
                        break;
                    case "Logout":
                        Logout(ref Trans);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Trans.Result = ex.Message + Environment.NewLine + ex.StackTrace;
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        public bool Login(ref Transfer Trans)
        {
            var table = XmlToDatatTable(Trans.Attribute);
            var row = table.Rows[0];

            if (!row.Table.Columns.Contains("Net") || string.IsNullOrEmpty(row["Net"].ToString()))
                Trans.Result = new ApiResult(false, "请选择登录服务器").toJson();
            if (!row.Table.Columns.Contains("Account") || string.IsNullOrEmpty(row["Account"].ToString()))
                Trans.Result = new ApiResult(false, "账号错误").toJson();
            if (!row.Table.Columns.Contains("LoginPwd") || string.IsNullOrEmpty(row["LoginPwd"].ToString()))
                Trans.Result = new ApiResult(false, "密码错误").toJson();

            var server = AppGlobal.StrToInt(row["Net"].ToString());
            var account = row["Account"].ToString();
            var password = row["LoginPwd"].ToString();

            var accountSplited = account.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (accountSplited.Length <= 1)
                Trans.Result = new ApiResult(false, "账号格式错误").toJson();

            account = account.Remove(0, accountSplited[0].Length + 1);

            // 企业id
            var corpId = AppGlobal.StrToInt(accountSplited[0]);
            if (corpId == 0)
                Trans.Result = new ApiResult(false, "账号格式错误").toJson();

            var commId = 0;
            if (row.Table.Columns.Contains("CommID") && !string.IsNullOrEmpty(row["CommID"].ToString()))
                commId = AppGlobal.StrToInt(row["CommID"].ToString());

            // 内网环境不验证密码，用于调试方便
            var verifyPassword = true;
            if (HttpContext.Current.Request.Url.Host.ToLowerInvariant() == "localhost")
                verifyPassword = false;
            if (verifyPassword == false && row.Table.Columns.Contains("VerifyPassword") && !string.IsNullOrEmpty(row["VerifyPassword"].ToString()))
                verifyPassword = AppGlobal.StrToInt(row["VerifyPassword"].ToString()) > 0;

            // 手机号登录
            var mobileLogin = false;
            if (row.Table.Columns.Contains("MobileLogin") && !string.IsNullOrEmpty(row["MobileLogin"].ToString()))
                mobileLogin = AppGlobal.StrToInt(row["MobileLogin"].ToString()) > 0;

            Trans.Result = TWERPAccountLogin(server, corpId, commId, account, password, out bool loginResult, verifyPassword, mobileLogin);
            return loginResult;
        }

        /// <summary>
        /// 注销
        /// </summary>
        public void Logout(ref Transfer Trans)
        {
            var table = XmlToDatatTable(Trans.Attribute);
            var row = table.Rows[0];

            if (!row.Table.Columns.Contains("Net") || string.IsNullOrEmpty(row["Net"].ToString()))
                Trans.Result = new ApiResult(false, "请选择登录服务器").toJson();
            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
                Trans.Result = new ApiResult(false, "企业编号不能为空").toJson();
            if (!row.Table.Columns.Contains("UserCode") || string.IsNullOrEmpty(row["UserCode"].ToString()))
                Trans.Result = new ApiResult(false, "用户编号不能为空").toJson();

            var server = AppGlobal.StrToInt(row["Net"].ToString());
            var corpId = AppGlobal.StrToInt(row["CorpID"].ToString());
            var usercode = row["UserCode"].ToString();

            Trans.Result = TWERPAccountLogout(server, corpId, usercode);
        }

        /// <summary>
        /// 物业ERP账号登录
        /// </summary>
        private string TWERPAccountLogin(int server, int corpId, int commId, string account, string password,
            out bool loginResult, bool verifyPassword = true, bool mobileLogin = false)
        {
            loginResult = false;

            PubConstant.tw2bsConnectionString = Global_Fun.Tw2bsConnectionString(server.ToString());
            DbHelperSQL.ConnectionString = PubConstant.tw2bsConnectionString;

            using (var conn = new SqlConnection(PubConstant.tw2bsConnectionString))
            {
                var sql = @"SELECT * FROM Tb_System_Corp WHERE CorpID=@CorpID AND isnull(IsDelete,0)=0;";

                var corpInfo = conn.Query(sql, new { CorpID = corpId }).FirstOrDefault();
                if (corpInfo == null)
                    return new ApiResult(false, "未找到CorpID配置信息").toJson();

                PubConstant.hmWyglConnectionString = $"Max Pool Size=2048;Min Pool Size=0;Pooling=true;Data Source={corpInfo.DBServer};Initial Catalog={corpInfo.DBName};User ID={corpInfo.DBUser};Password={corpInfo.DBPwd};";
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT UserCode,UserName,DepCode,MobileTel,isnull(IsMobile,0) AS IsMobile,HeadImg 
                            FROM Tb_Sys_User
                            WHERE isnull(IsDelete,0)=0";

                if (mobileLogin)
                    sql += " AND MobileTel=@LoginCode ";
                else
                    sql += " AND LoginCode=@LoginCode ";

                if (verifyPassword)
                    sql += " AND (Password=@Password OR upper(@Password)=upper(substring(sys.fn_sqlvarbasetostr(hashbytes('MD5',convert(varchar(max),PassWord))),3,32)))";
                

                var userInfo = conn.Query(sql, new { LoginCode = account, Password = password }).FirstOrDefault();
                if (userInfo == null)
                    return new ApiResult(false, "账号或密码错误").toJson();

                if (userInfo.IsMobile == 0)
                    return new ApiResult(false, "未开通员工App使用权限").toJson();

                loginResult = true;

                // session
                Global_Var.CorpId = corpId.ToString();
                Global_Var.CorpID = Global_Var.CorpId;
                Global_Var.LoginCorpID = Global_Var.CorpId;
                Global_Var.LoginCommID = commId.ToString();
                Global_Var.SystemType = "property";
                Global_Var.UserHostAddress = HttpContext.Current.Request.UserHostAddress;

                Global_Var.UserCode = userInfo.UserCode;
                Global_Var.UserName = userInfo.UserName;
                Global_Var.LoginCode = account;
                Global_Var.LoginUserCode = userInfo.UserCode;
                Global_Var.LoginUserName = userInfo.UserName;
                Global_Var.LoginMobile = userInfo.MobileTel;
                Global_Var.LoginDepCode = userInfo.DepCode;

                if (commId != 0)
                {
                    sql = "SELECT * FROM Tb_HSPR_Community WHERE CommID=@CommID AND isnull(IsDelete,0)=0;";

                    var commInfo = conn.Query(sql, new { CommID = commId }).FirstOrDefault();
                    if (commInfo == null)
                        return new ApiResult(false, "未找到项目信息").toJson();

                    Global_Var.LoginCommName = commInfo.CommName;
                    Global_Var.LoginCommType = commInfo.CommType;
                    Global_Var.LoginBranchID = commInfo.BranchID;
                    Global_Var.LoginOrganCode = commInfo.OrganCode;
                    Global_Var.LoginCorpRegionCode = commInfo.CorpRegionCode;
                }

                // 记录登录日志
                WriteLog(userInfo.UserCode, "登入系统", "物管App登录", conn);

                return new ApiResult(true, userInfo).toJson();
            }
        }

        /// <summary>
        /// 物业ERP账号注销
        /// </summary>
        private string TWERPAccountLogout(int server, int corpId, string usercode)
        {
            PubConstant.tw2bsConnectionString = Global_Fun.Tw2bsConnectionString(server.ToString());
            DbHelperSQL.ConnectionString = PubConstant.tw2bsConnectionString;

            using (var conn = new SqlConnection(PubConstant.tw2bsConnectionString))
            {
                var sql = @"SELECT * FROM Tb_System_Corp WHERE CorpID=@CorpID AND isnull(IsDelete,0)=0;";

                var corpInfo = conn.Query(sql, new { CorpID = corpId }).FirstOrDefault();
                if (corpInfo == null)
                    return new ApiResult(false, "未找到CorpID配置信息").toJson();

                PubConstant.hmWyglConnectionString = $"Max Pool Size=2048;Min Pool Size=0;Pooling=true;Data Source={corpInfo.DBServer};Initial Catalog={corpInfo.DBName};User ID={corpInfo.DBUser};Password={corpInfo.DBPwd};";
            }

            // 记录注销日志
            WriteLog(usercode, "登出系统", "物管App登出");

            return new ApiResult(true, "注销成功").toJson();
        }

        private void WriteLog(string usercode, string operateName, string memo, IDbConnection db = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            try
            {
                var model = new Tb_Sys_Log
                {
                    CorpID = AppGlobal.StrToInt(Global_Var.LoginCorpID),
                    CommID = AppGlobal.StrToInt(Global_Var.LoginCommID),
                    BranchID = AppGlobal.StrToInt(Global_Var.LoginBranchID),
                    ManagerCode = usercode,
                    LocationIP = Global_Var.UserHostAddress,
                    LogTime = DateTime.Now,
                    OperateName = operateName,
                    Memo = memo,
                };

                conn.Insert(model);
            }
            catch (Exception)
            {

            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }
    }
}
