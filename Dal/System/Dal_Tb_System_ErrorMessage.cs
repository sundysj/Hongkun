using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace Erp.DAL.System
{
    /// <summary>
    /// 数据访问类Dal_Tb_System_ErrorMessage。
    /// </summary>
    public class Dal_Tb_System_ErrorMessage
    {
        public Dal_Tb_System_ErrorMessage()
        {
            DbHelperSQL.ConnectionString = PubConstant.ErrorConnectionString.ToString();
        }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int fdi_ErrorId)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@fdi_ErrorId", SqlDbType.Int,4)};
            parameters[0].Value = fdi_ErrorId;

            int result = DbHelperSQL.RunProcedure("Proc_Tb_System_ErrorMessage_Exists", parameters, out rowsAffected);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(Erp.Model.System.Tb_System_ErrorMessage model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@fdi_ErrorId", SqlDbType.Int,4),
					new SqlParameter("@fdi_CorpId", SqlDbType.Int,4),
					new SqlParameter("@fdv_OprUserName", SqlDbType.VarChar,50),
					new SqlParameter("@fdv_ErrorPage", SqlDbType.VarChar,3999),
					new SqlParameter("@fdv_ErrorMessage", SqlDbType.VarChar,8000),
					new SqlParameter("@fdd_ErrorDate", SqlDbType.DateTime)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.fdi_CorpId;
            parameters[2].Value = model.fdv_OprUserName;
            parameters[3].Value = model.fdv_ErrorPage;
            parameters[4].Value = model.fdv_ErrorMessage;
            parameters[5].Value = model.fdd_ErrorDate;

            DbHelperSQL.RunProcedure("Proc_Tb_System_ErrorMessage_ADD", parameters, out rowsAffected);
            return (int)parameters[0].Value;
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public void Update(Erp.Model.System.Tb_System_ErrorMessage model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@fdi_ErrorId", SqlDbType.Int,4),
					new SqlParameter("@fdi_CorpId", SqlDbType.Int,4),
					new SqlParameter("@fdv_OprUserName", SqlDbType.VarChar,50),
					new SqlParameter("@fdv_ErrorPage", SqlDbType.VarChar,3999),
					new SqlParameter("@fdv_ErrorMessage", SqlDbType.VarChar,8000),
					new SqlParameter("@fdd_ErrorDate", SqlDbType.DateTime)};
            parameters[0].Value = model.fdi_ErrorId;
            parameters[1].Value = model.fdi_CorpId;
            parameters[2].Value = model.fdv_OprUserName;
            parameters[3].Value = model.fdv_ErrorPage;
            parameters[4].Value = model.fdv_ErrorMessage;
            parameters[5].Value = model.fdd_ErrorDate;

            DbHelperSQL.RunProcedure("Proc_Tb_System_ErrorMessage_Update", parameters, out rowsAffected);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int fdi_ErrorId)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@fdi_ErrorId", SqlDbType.Int,4)};
            parameters[0].Value = fdi_ErrorId;

            DbHelperSQL.RunProcedure("Proc_Tb_System_ErrorMessage_Delete", parameters, out rowsAffected);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Erp.Model.System.Tb_System_ErrorMessage GetModel(int fdi_ErrorId)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@fdi_ErrorId", SqlDbType.Int,4)};
            parameters[0].Value = fdi_ErrorId;

            Erp.Model.System.Tb_System_ErrorMessage model = new Erp.Model.System.Tb_System_ErrorMessage();
            DataSet ds = DbHelperSQL.RunProcedure("Proc_Tb_System_ErrorMessage_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["fdi_ErrorId"].ToString() != "")
                {
                    model.fdi_ErrorId = int.Parse(ds.Tables[0].Rows[0]["fdi_ErrorId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["fdi_CorpId"].ToString() != "")
                {
                    model.fdi_CorpId = int.Parse(ds.Tables[0].Rows[0]["fdi_CorpId"].ToString());
                }
                model.fdv_OprUserName = ds.Tables[0].Rows[0]["fdv_OprUserName"].ToString();
                model.fdv_ErrorPage = ds.Tables[0].Rows[0]["fdv_ErrorPage"].ToString();
                model.fdv_ErrorMessage = ds.Tables[0].Rows[0]["fdv_ErrorMessage"].ToString();
                if (ds.Tables[0].Rows[0]["fdd_ErrorDate"].ToString() != "")
                {
                    model.fdd_ErrorDate = DateTime.Parse(ds.Tables[0].Rows[0]["fdd_ErrorDate"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select fdi_ErrorId,fdi_CorpId,fdv_OprUserName,fdv_ErrorPage,fdv_ErrorMessage,fdd_ErrorDate ");
            strSql.Append(" FROM Tb_System_ErrorMessage ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" fdi_ErrorId,fdi_CorpId,fdv_OprUserName,fdv_ErrorPage,fdv_ErrorMessage,fdd_ErrorDate ");
            strSql.Append(" FROM Tb_System_ErrorMessage ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort)
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
            parameters[5].Value = "SELECT * FROM Tb_System_ErrorMessage WHERE 1=1 " + StrCondition;
            parameters[6].Value = "fdi_ErrorId";
            DataSet Ds = DbHelperSQL.RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        #endregion  成员方法
    }
}

