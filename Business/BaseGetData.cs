using Common;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Business
{
    /// <summary>
    /// 获取数据集类
    /// </summary>
    public class BaseGetData
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="LoginSQLConnStr">数据库连接</param>
        /// <param name="PageCount">总页数（输出）</param>
        /// <param name="Counts">总条数（输出）</param>
        /// <param name="StrCondition">查询条件</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">条数</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">排序方式（1倒序、0正序）</param>
        /// <param name="SQLView">查询的视图</param>
        /// <param name="SQLParaKey">主键</param>
        /// <returns></returns>
        /// 
        public static DataSet GetList(string LoginSQLConnStr, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort, string SQLView, string SQLParaKey)
        {

            SqlParameter[] parameters = {
                    new SqlParameter("@FldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@FldSort", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Sort", SqlDbType.Int),
                    new SqlParameter("@StrCondition", SqlDbType.VarChar, 8000),
                    new SqlParameter("@Id", SqlDbType.VarChar, 50)
                    };
            parameters[0].Value = "*";
            parameters[1].Value = PageSize;
            parameters[2].Value = PageIndex;
            parameters[3].Value = SortField;
            parameters[4].Value = Sort;
            parameters[5].Value = "SELECT * FROM " + SQLView + " WHERE 1=1 " + StrCondition;
            parameters[6].Value = SQLParaKey;
            DataSet Ds = new DbHelperSQLP(LoginSQLConnStr.ToString()).RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            return Ds;
        }

        public static DataSet GetList(string LoginSQLConnStr, out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort, string SQLView, string SQLParaKey)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@FldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@FldSort", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Sort", SqlDbType.Int),
                    new SqlParameter("@StrCondition", SqlDbType.VarChar, 8000),
                    new SqlParameter("@Id", SqlDbType.VarChar, 50),
                    new SqlParameter("@PageCount", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    new SqlParameter("@Counts", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    };
            parameters[0].Value = "*";
            parameters[1].Value = PageSize;
            parameters[2].Value = PageIndex;
            parameters[3].Value = SortField;
            parameters[4].Value = Sort;
            parameters[5].Value = "SELECT * FROM " + SQLView + " WHERE 1=1 " + StrCondition;
            parameters[6].Value = SQLParaKey;
            DataSet Ds = new DbHelperSQLP(LoginSQLConnStr).RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }
        public static DataSet GetList(string LoginSQLConnStr, out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort, string SQLView, string SQLParaKey, string Fields)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@FldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@FldSort", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Sort", SqlDbType.Int),
                    new SqlParameter("@StrCondition", SqlDbType.VarChar, 8000),
                    new SqlParameter("@Id", SqlDbType.VarChar, 50),
                    new SqlParameter("@PageCount", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    new SqlParameter("@Counts", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    };
            parameters[0].Value = "*";
            parameters[1].Value = PageSize;
            parameters[2].Value = PageIndex;
            parameters[3].Value = SortField;
            parameters[4].Value = Sort;
            parameters[5].Value = "SELECT " + Fields + " FROM " + SQLView + " WHERE 1=1 " + StrCondition;
            parameters[6].Value = SQLParaKey;
            DataSet Ds = new DbHelperSQLP(LoginSQLConnStr).RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }
        /// <summary>
        /// 获取数据列表(自定义语句分页)
        /// </summary>
        /// <param name="LoginSQLConnStr">数据库连接</param>
        /// <param name="PageCount">总页数（输出）</param>
        /// <param name="Counts">总条数（输出）</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">条数</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">排序方式（1倒序、0正序）</param>
        /// <param name="SQL">查询语句</param>
        /// <param name="SQLParaKey">主键</param>
        /// <returns></returns>
        public static DataSet GetDefinitionList(string LoginSQLConnStr, out int PageCount, out int Counts, int PageIndex, int PageSize, string SortField, int Sort, string SQL, string SQLParaKey)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@FldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@FldSort", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Sort", SqlDbType.Int),
                    new SqlParameter("@StrCondition", SqlDbType.VarChar, 8000),
                    new SqlParameter("@Id", SqlDbType.VarChar, 50),
                    new SqlParameter("@PageCount", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    new SqlParameter("@Counts", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    };
            parameters[0].Value = "*";
            parameters[1].Value = PageSize;
            parameters[2].Value = PageIndex;
            parameters[3].Value = SortField;
            parameters[4].Value = Sort;
            parameters[5].Value = SQL;
            parameters[6].Value = SQLParaKey;
            DataSet Ds = new DbHelperSQLP(LoginSQLConnStr).RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="LoginSQLConnStr">数据库连接</param>
        /// <param name="FldName">需要查询的字段</param>
        /// <param name="PageCount">总页数（输出）</param>
        /// <param name="Counts">总条数（输出）</param>
        /// <param name="StrCondition">查询条件</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">条数</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">排序方式（1倒序、0正序）</param>
        /// <param name="SQLView">查询的视图</param>
        /// <param name="SQLParaKey">主键</param>
        /// <returns></returns>
        public static DataSet GetList(string LoginSQLConnStr, string FldName, out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort, string SQLView, string SQLParaKey)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@FldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@FldSort", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Sort", SqlDbType.Int),
                    new SqlParameter("@StrCondition", SqlDbType.VarChar, 8000),
                    new SqlParameter("@Id", SqlDbType.VarChar, 50),
                    new SqlParameter("@PageCount", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    new SqlParameter("@Counts", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    };
            parameters[0].Value = FldName;
            parameters[1].Value = PageSize;
            parameters[2].Value = PageIndex;
            parameters[3].Value = SortField;
            parameters[4].Value = Sort;
            parameters[5].Value = "SELECT * FROM " + SQLView + " WHERE 1=1 " + StrCondition;
            parameters[6].Value = SQLParaKey;
            DataSet Ds = new DbHelperSQLP(LoginSQLConnStr).RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }
    }
}