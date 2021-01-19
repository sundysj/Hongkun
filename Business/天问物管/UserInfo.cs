using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Business
{
    public class UserInfo : PubInfo
    {
        public UserInfo()
        {
            base.Token = "20160607UserInfo";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误!");
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            //验证登录
            if (!new Login().isLogin(ref Trans)) return;

            switch (Trans.Command.ToString())
            {
                //切换机构列表
                case "ChangePassword":
                    Trans.Result = ChangePassword(Row);
                    break;
                case "GetUserRole":
                    Trans.Result = GetUserRole(Row);
                    break;
                case "UploadPhoneInfo":
                    Trans.Result = AddPhoneInfo(Row);
                    break;
                case "UploadPhoneToken":
                    Trans.Result = AddPhoneToken(Row);
                    break;
            }
        }

        private string AddPhoneToken(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("UserCode") || string.IsNullOrEmpty(Row["UserCode"].ToString()))
            {
                return new ApiResult(false, "缺少参数UserCode").toJson();
            }
            string UserCode = Row["UserCode"].ToString();
            string PhoneToken = Row["PhoneToken"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();
                DataTable dt = conn.ExecuteReader("select UserCode,PhoneToken from Tb_Sys_User_Phone where UserCode= @UserCode", new { UserCode = UserCode }, null, null, CommandType.Text).ToDataSet().Tables[0];
                if (null != dt && dt.Rows.Count > 0)
                {
                    conn.Execute(@"update Tb_Sys_User_Phone set PhoneToken=@PhoneToken where UserCode= @UserCode", new { UserCode = UserCode, PhoneToken = PhoneToken }, trans);
                }
                else
                {
                    conn.Execute(@"INSERT INTO Tb_Sys_User_Phone(UserCode, PhoneToken) VALUES(@UserCode, @PhoneToken)", new { UserCode = UserCode, PhoneToken = PhoneToken }, trans);
                }

                trans.Commit();
                conn.Close();
            }

            return new ApiResult(true, "成功").toJson();
        }

        private string AddPhoneInfo(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("UserCode") || string.IsNullOrEmpty(Row["UserCode"].ToString()))
            {
                return new ApiResult(false, "缺少参数UserCode").toJson();
            }
            string UserCode = Row["UserCode"].ToString();
            string PhoneBrand = Row["PhoneBrand"].ToString();
            string PhoneModel = Row["PhoneModel"].ToString();
            string AppVersion = Row["AppVersion"].ToString();
            string PhoneToken = Row["PhoneToken"].ToString();

            if (!string.IsNullOrEmpty(PhoneBrand))
            {
                PhoneBrand = PhoneBrand.ToLower();
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string querySql = "select UserCode from Tb_Sys_User_Phone where UserCode= @UserCode";
                var varIsExist = conn.Query<string>(querySql, new { UserCode = UserCode }).FirstOrDefault();
                if (varIsExist != null)
                {
                    conn.Execute(@"update Tb_Sys_User_Phone set PhoneBrand=@PhoneBrand, PhoneModel=@PhoneModel, AppVersion=@AppVersion,PhoneToken=@PhoneToken where UserCode= @UserCode", new { UserCode = UserCode, PhoneBrand = PhoneBrand, PhoneModel= PhoneModel, AppVersion= AppVersion, PhoneToken = PhoneToken });
                }
                else
                {
                    conn.Execute(@"INSERT INTO Tb_Sys_User_Phone(UserCode, PhoneBrand, PhoneModel, AppVersion,PhoneToken) VALUES(@UserCode, @PhoneBrand, @PhoneModel, @AppVersion,@PhoneToken)", new { UserCode = UserCode, PhoneBrand = PhoneBrand, PhoneModel = PhoneModel, AppVersion = AppVersion, PhoneToken= PhoneToken });
                }
            }

            return new ApiResult(true, "成功").toJson();
        }

        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetUserRole(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("UserCode")||string.IsNullOrEmpty(Row["UserCode"].ToString()))
            {
                return new ApiResult(false, "缺少参数UserCode").toJson();
            }
            string UserCode = Row["UserCode"].ToString();
            DataTable dt = null;
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dt = conn.ExecuteReader("SELECT RoleCode,DepName,RoleName,SysRoleName,UserRoleCode,UserCode FROM view_Sys_UserRole_Filter WHERE UserCode = @UserCode", new { UserCode = UserCode }, null, null, CommandType.Text).ToDataSet().Tables[0];
            }
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (null != dt && dt.Rows.Count > 0)
            {
                Dictionary<string, object> dic;
                foreach (DataRow item in dt.Rows)
                {
                    dic = new Dictionary<string, object>();
                    dic.Add("RoleCode", item["RoleCode"]);
                    dic.Add("DepName", item["DepName"]);
                    dic.Add("RoleName", item["RoleName"]);
                    dic.Add("SysRoleName", item["SysRoleName"]);
                    dic.Add("UserRoleCode", item["UserRoleCode"]);
                    dic.Add("UserCode", item["UserCode"]);
                    list.Add(dic);
                }
            }
            return new ApiResult(true, list).toJson();
        }

        private string ChangePassword(DataRow Row)
        {            string Result = "";
            string strPwdTemp = Row["OldPWD"].ToString();
            string strNewPwdTemp = Row["NewPWD"].ToString();
            string UserCode = Row["UserCode"].ToString();

            MobileSoft.BLL.Sys.Bll_Tb_Sys_User B_User = new MobileSoft.BLL.Sys.Bll_Tb_Sys_User();
            DataTable dTable = B_User.GetList("UserCode='" + UserCode + "' and PassWord='" + strPwdTemp + "' and IsDelete = 0").Tables[0];

            if (dTable.Rows.Count > 0)
            {
                ChangePassword(UserCode, strNewPwdTemp);
                Result = JSONHelper.FromString(true, "密码修改成功");
            }
            else
            {
                Result = JSONHelper.FromString(false,"旧密码输入错误");
            }
            return Result;
        }

        public void ChangePassword(string UserCode,string Password)
        {

            SqlParameter[] parameters = {
                    new SqlParameter("@UserCode", SqlDbType.VarChar),
                    new SqlParameter("@PassWord", SqlDbType.VarChar)
            };
            parameters[0].Value = UserCode;
            parameters[1].Value = Password;
            new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_Sys_User_ChanagePWD_Mobile", parameters);
        }
    }
}