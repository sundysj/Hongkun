using Common;
using Common.Enum;
using Dapper;
using MobileSoft.DBUtility;
using MobileSoft.Model.System;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessApp
{
    public class PMSUserAndLogin : PubInfo
    {
        public PMSUserAndLogin()
        {
            base.Token = "202006291722PMSUserAndLogin";
        }

        public override void Operate(ref Transfer Trans)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            //防止未捕获异常出现
            try
            {
                switch (Trans.Command)
                {
                    case "LoginByPwd":
                        Trans.Result = LoginByPwd(Row);
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
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public String LoginByPwd(DataRow row)
        {
            String loginPwd = String.Empty, loginCode = String.Empty;

            if (!row.Table.Columns.Contains("LoginPwd") || string.IsNullOrEmpty(row["LoginPwd"].AsString()))
            {
                return new ApiResult(false, "缺少参数loginPwd").toJson();
            }

            if (!row.Table.Columns.Contains("LoginCode") || string.IsNullOrEmpty(row["LoginCode"].AsString()))
            {
                return new ApiResult(false, "缺少参数loginCode").toJson();
            }
            loginPwd = row["LoginPwd"].ToString();
            loginCode = row["LoginCode"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String Sql = "SELECT * FROM Tb_System_BusinessCorp WHERE LoginCode=@LoginCode AND LoginPassWD=@Pwd AND ISNULL(IsDelete,0)=0 AND isnull(IsClose,'未关闭')='未关闭'";
                    Tb_System_BusinessCorp tb_System_BusinessCorp = conn.QueryFirstOrDefault<Tb_System_BusinessCorp>(Sql, new { LoginCode = loginCode, Pwd = loginPwd });

                    if (tb_System_BusinessCorp != null) return new ResponseEntity<Object>((int)ResoponseEnum.Success, tb_System_BusinessCorp).ToJson();
                    return new ResponseEntity<Object>((int)ResoponseEnum.Failure, "账号或密码错误").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<Object>((int)ResoponseEnum.Error, "登录错误").ToJson();
            }
        }
    }
}
