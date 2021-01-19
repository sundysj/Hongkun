using System;
using MobileSoft.DBUtility;
using MobileSoft.Common;
using System.Data;
using System.Linq;
using Common;
using MobileSoft.BLL.Common;
using MobileSoft.Model.Common;
using MobileSoft.Model.Unified;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;

namespace Business
{
    /// <summary>
    /// 修改用户资料时，
    /// 接口中未对外提供注册日期
    /// 接口中未对外提供注册功能
    /// </summary>
    public class BindUserInfo : PubInfo
    {
        public BindUserInfo() { base.Token = "20160906BindUserInfo"; }
        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = "false:";

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "Save":
                    Trans.Result = Save(Row, Trans.Mac);
                    break;
                case "GetUserInfo":
                    Trans.Result = GetUserInfo(Row, Trans.Mac);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="row"></param>
        /// <param name="mac"></param>
        /// 手机号：Mobile  必填
        /// 返回信息：用户所有信息
        /// <returns></returns>
        private string GetUserInfo(DataRow row, string mac)
        {
            if (!row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(row["Mobile"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "手机号不能为空");
            }
            IDbConnection Connectionstr = new SqlConnection(Connection.GetConnection("4"));
            string Sql = "SELECT Id,Name,Mobile,Email,QQ,QQToken,WeChatNum,WeChatToken,NickName,UserPic,Sex,Birthday  FROM Tb_User where Mobile=@Mobile";
            Tb_User user = Connectionstr.Query<Tb_User>(Sql, new { Mobile = row["Mobile"].ToString() }).SingleOrDefault();
            if (user==null)
            {
                user = new Tb_User();
                return JSONHelper.FromString(false,"该用户不存在");
            }
            
            return JSONHelper.FromString(user);
        }


        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="row"></param>
        /// <param name="mac"></param>
        /// 手机号：Mobile 必填
        /// 无返回
        /// <returns></returns>
        private string Save(DataRow row, string mac)
        {
            if (!row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty( row["Mobile"].ToString()))
            {
                return JSONHelper.FromJsonString(false,"手机号不能为空");
            }
            Save(row);
            return JSONHelper.FromString(true,"保存成功!");
        }




        public void Save(DataRow dr)
        {
            IDbConnection Connectionstr = new SqlConnection(Connection.GetConnection("4"));
            string Sql = "SELECT TOP 1 * FROM Tb_User where Mobile=@Mobile AND isnull(IsDelete,0)=0";
            Tb_User user = Connectionstr.Query<Tb_User>(Sql,new { Mobile = dr["Mobile"].ToString()}).SingleOrDefault();
            if (user==null)
            {
               user.Id = Guid.NewGuid().ToString();
               user= GetUser(dr, user);
               Connectionstr.Insert(user);
            }
            else
            {
                user = GetUser(dr, user);
                Connectionstr.Update(user);
            }
            
        }

        private static Tb_User GetUser(DataRow dr, Tb_User user)
        {
            if (dr.Table.Columns.Contains("Name"))
            {
                user.Name = dr["Name"].ToString();
            }
            if (dr.Table.Columns.Contains("Birthday"))
            {
                user.Birthday = dr["Birthday"].ToString();
            }

            if (dr.Table.Columns.Contains("Mobile"))
            {
                user.Mobile = dr["Mobile"].ToString();
            }
            if (dr.Table.Columns.Contains("Email"))
            {
                user.Email = dr["Email"].ToString();
            }
            if (dr.Table.Columns.Contains("QQ"))
            {
                user.QQ = dr["QQ"].ToString();
            }
            if (dr.Table.Columns.Contains("QQToken"))
            {
                user.QQToken = dr["QQToken"].ToString();
            }
            if (dr.Table.Columns.Contains("WeChatNum"))
            {
                user.WeChatNum = dr["WeChatNum"].ToString();
            }
            if (dr.Table.Columns.Contains("WeChatToken"))
            {
                user.WeChatToken = dr["WeChatToken"].ToString();
            }
            if (dr.Table.Columns.Contains("NickName"))
            {
                user.NickName = dr["NickName"].ToString();
            }
            if (dr.Table.Columns.Contains("Pwd"))
            {
                user.Pwd = dr["Pwd"].ToString();
            }
            if (dr.Table.Columns.Contains("UserPic"))
            {
                user.UserPic = dr["UserPic"].ToString();
            }
            if (dr.Table.Columns.Contains("Sex"))
            {
                user.Sex = AppGlobal.StrToInt(dr["Sex"].ToString());
            }
            if (dr.Table.Columns.Contains("RegDate"))
            {
                user.RegDate = AppGlobal.StrToDate(dr["RegDate"].ToString());
            }
            return user;
        }


    }
}
