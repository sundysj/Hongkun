using Business.PMS10.报事.Models;
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
    public class PMSDepartmentManage : PubInfo
    {
        public PMSDepartmentManage()
        {
            base.Token = "20191212PMSDepartmentManage";
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
                    case "GetAllDepartment":               // 获取所有部门
                        Trans.Result = GetAllDepartment();
                        break;
                    case "GetUserDep":                              // 获取当前人员部门
                        Trans.Result = GetUserDep(Row);
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

        private string GetAllDepartment()
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT DepCode,SortDepCode,DepName FROM Tb_Sys_Department
                            WHERE SortDepCode<>'0001' AND isnull(IsDelete,0)=0 ORDER BY SortDepCode;";

                var data = conn.Query(sql);

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取当前人员部门
        /// </summary>
        private string GetUserDep(DataRow row)
        {

            if (!row.Table.Columns.Contains("UserCode") || string.IsNullOrEmpty(row["UserCode"].ToString()))
            {
                return new ApiResult(false, "缺少参数UserCode").toJson();
            }

            var userCode = row[@"UserCode"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = $@"SELECT DepCode,DepName FROM Tb_Sys_Department WHERE DepCode IN 
                            (
                                SELECT DepCode FROM Tb_Sys_User WHERE UserCode={userCode}
                            ) AND isnull(IsDelete,0)=0";

                var data = conn.Query(sql).FirstOrDefault();
                return new ApiResult(true, data).toJson();
            }

        }
    }
}
