using Business.PMS10.物管App.桌面.Models.AppModules;
using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static Dapper.SqlMapper;

namespace Business
{
    public partial class PMS10AppDesktop : PubInfo
    {
        public PMS10AppDesktop()
        {
            base.Token = "20200102PMS10AppDesktop";
        }

        public override void Operate(ref Transfer Trans)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            //验证登录
            if (!new Login().isLogin(ref Trans))
                return;

            //防止未捕获异常出现
            try
            {
                switch (Trans.Command)
                {
                    case "GetAppPermissions":                               // App权限
                        Trans.Result = GetAppPermissions(Row);
                        break;
                    case "GetAppDesktopBadge":
                        Trans.Result = GetAppDesktopBadge(Row);
                        break;
                    case "GetAppDesktopStatisticsDetails":
                        Trans.Result = GetAppDesktopStatisticsDetails(Row);
                        break;
                    case "RefreshAppDesktopStatistics":
                        Trans.Result = RefreshAppDesktopStatistics(Row);
                        break;

                    default:
                        Trans.Result = new ApiResult(false, "未知错误").toJson();
                        break;
                } 
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace + Environment.NewLine + ex.Source);
                Trans.Result = new ApiResult(false, ex.Message + ex.StackTrace).toJson();
            }
        }

        /// <summary>
        /// 获取App信息桌面权限
        /// </summary>
        private string GetAppPermissions(DataRow row)
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var commId = 0;
                var organCode = "";

                if (row.Table.Columns.Contains("CommID"))
                {
                    commId = AppGlobal.StrToInt(row["CommID"].ToString());
                }

                if (row.Table.Columns.Contains("OrganCode"))
                {
                    organCode = row["OrganCode"].ToString();
                    if (string.IsNullOrEmpty(organCode))
                    {
                        organCode = "01";
                    }
                }

                //信息桌面
                var _1 = new AppModule<AppWebModule>() { Name = "信息桌面", Code = "9999", Submodules = new List<AppWebModule>() };

                // 桌面模块
                var sql = @"SELECT Code,FunName AS Name FROM Tb_Sys_Function 
                        WHERE isnull(IsDelete,0)=0 AND Code<>'9999' AND Code LIKE '99__' ORDER BY Code;";
                var _2 = conn.Query<AppModule<AppSubmodule>>(sql).ToList();

                // 桌面子模块
                sql = @"SELECT Code,FunName AS Name,isnull(Memo,'') AS Memo 
                        FROM Tb_Sys_Function WHERE isnull(IsDelete,0)=0 AND isnull(Memo,'')<>'App操作权限' 
                        AND Code IN
                        (
                            SELECT FunCode FROM Tb_Sys_FunctionPope WHERE FunCode LIKE '99____' 
                                AND RoleCode IN(SELECT RoleCode FROM Tb_Sys_Role 
                                    WHERE RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode))
                            UNION
                            SELECT FunCode FROM Tb_Sys_FunctionPope WHERE FunCode LIKE '99____' 
                                AND RoleCode IN(SELECT SysRoleCode FROM Tb_Sys_Role 
                                        WHERE RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode))
                        ) ORDER BY Code;";
                var _3 = conn.Query<AppSubmodule>(sql, new { UserCode = Global_Var.LoginUserCode }).ToList();

                // 总部、片区、区域、公司级：只显示决策支撑信息桌面
                // 项目级：没有权限时，默认显示项目员工信息桌面

                if (commId == 0)
                {
                    _1.Submodules.Add(new AppWebModule() { Code = "999908", Name = "决策支撑信息桌面" });
                }
                else
                {
                    var tmp = _3.FindAll(obj => obj. Code.StartsWith("9999"))
                                .Select(obj => { return new AppWebModule() { Code = obj.Code, Name = obj.Name }; });

                    _1.Submodules.AddRange(tmp);

                    if (_1.Submodules.Count() == 0)
                    {
                        _1.Submodules.Add(new AppWebModule() { Code = "999901", Name = "项目员工信息桌面" });
                    }
                }

                _1.Submodules.ForEach(obj =>
                {
                    obj.Url = $"frame/PolyPropertyAPP/H5KanBan_{obj.Code.Last()}.aspx";
                });

                foreach (var module in _2)
                {
                    module.Submodules = new List<AppSubmodule>();

                    var tmp = _3.FindAll(obj => obj.Code.StartsWith(module.Code));
                    if (tmp.Count != 0)
                    {
                        module.Submodules.AddRange(tmp);

                        foreach (var item in tmp)
                        {
                            _3.Remove(item);
                        }
                    }
                }

                _2.RemoveAll(m => m.Submodules == null || m.Submodules.Count == 0);

                return new ApiResult(true, new
                {
                    AppDesktops = _1.Submodules,
                    AppModules = _2,
                    ERPMessage = new
                    {
                        Notifies = GetERPNotifies(Global_Var.LoginUserCode, organCode, commId, conn),
                        UnreadMessageCount = GetERPUnreadMessageCount(Global_Var.LoginUserCode, conn)
                    }
                }).toJson();
            }
        }

        #region ERP通知&短信

        // 7天内未读短信条数
        private int GetERPUnreadMessageCount(string usercode, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var lastWeekTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            var sql = @"SELECT count(1) FROM Tb_Sys_Message WHERE SendTime>=@LastWeekTime AND MsgState=1 AND UserCode=@UserCode";

            int badge = conn.Query<int>(sql, new { LastWeekTime = lastWeekTime, UserCode = usercode }, trans).FirstOrDefault();

            if (db == null)
                conn.Dispose();

            return badge;
        }

        private IEnumerable<ERPMessageModel> GetERPNotifies(string usercode, string organCode, int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);
            var result = default(IEnumerable<ERPMessageModel>);

            if (commId != 0)
            {
                var sql = @"SELECT TOP 5 InfoId AS IID,Title,IssueDate,2 AS Level 
                            FROM Tb_Common_CommunityService 
                            WHERE isnull(IsDelete,0)=0 AND DepCode=@CommID ORDER BY IssueDate DESC;";

                result = conn.Query<ERPMessageModel>(sql, new { CommID = commId.ToString() }, trans);
            }
            else
            {
                var sql = @"SELECT TOP 5 IID,Title,IssueDate,CASE WHEN len(OrganCode)=2 THEN 0 ELSE 1 END AS Level
                            FROM Tb_Common_CommonInfo
                            WHERE isnull(IsDelete,0)=0 AND OrganCode=@OrganCode 
                            AND (isnull(ReadDepartCode,@ReadDepartCode) LIKE @_ReadDepartCode OR isnull(ReadUserCode,@UserCode) LIKE @_UserCode)
                            ORDER BY IssueDate DESC;";

                result = conn.Query<ERPMessageModel>(sql, new
                {
                    OrganCode = organCode,
                    ReadDepartCode = Global_Var.LoginSortDepCode,
                    _ReadDepartCode = $"%{Global_Var.LoginSortDepCode}%",
                    UserCode = usercode,
                    _UserCode = $"%{usercode}%"
                }, trans);
            }

            if (db == null)
                conn.Dispose();

            return result;
        }


        #endregion
    }
}
