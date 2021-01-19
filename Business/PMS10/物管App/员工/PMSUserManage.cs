using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using static Dapper.SqlMapper;

namespace Business
{
    public class PMSUserManage : PubInfo
    {
        public PMSUserManage()
        {
            base.Token = "20191121PMSUserManage";
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
                    case "FuzzySearchUserSimpleInfo":                  
                        Trans.Result = FuzzySearchUserSimpleInfo(Row);
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

        private string GetUserSimpleInfo(DataRow row)
        {
            return null;
        }

        /// <summary>
        /// 模糊查询用户信息
        /// </summary>
        private string FuzzySearchUserSimpleInfo(DataRow row)
        {
            var key = default(string);
            if (row.Table.Columns.Contains("Key") && !string.IsNullOrEmpty(row["Key"].AsString()))
            {
                key = row["Key"].AsString();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT TOP 20 * FROM 
                            (
                                SELECT a.UserCode,a.UserName,isnull(a.MobileTel,a.LinkmanTel) AS MobileTel,
                                    c.DepName+'--'+b.DepName AS DepName,a.Email,a.EmployeeCode,
                                    stuff((SELECT ','+y.RoleName FROM Tb_Sys_UserRole x 
                                            LEFT JOIN Tb_Sys_Role y ON x.RoleCode=y.RoleCode
                                            WHERE UserCode=a.UserCode FOR XML PATH('')),1,1,'') AS RoleName 
                                FROM Tb_Sys_User a
                                LEFT JOIN Tb_Sys_Department b ON a.DepCode=b.DepCode
                                LEFT JOIN Tb_Sys_Department c ON left(b.SortDepCode,len(b.SortDepCode)-4)=c.SortDepCode
                                WHERE isnull(a.IsDelete,0)=0 
                            ) as t ";

                if (!string.IsNullOrEmpty(key))
                {
                    sql += $" WHERE t.UserName LIKE '%{key}%' OR t.MobileTel LIKE '%{key}%' OR DepName LIKE '%{key}%' OR RoleName LIKE '%{key}%'";
                }

                sql += " ORDER BY t.UserCode";

                var data = conn.Query(sql);

                return new ApiResult(true, data).toJson();
            }
        }
    }
}
