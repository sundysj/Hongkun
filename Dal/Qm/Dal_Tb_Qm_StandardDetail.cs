using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace HM.DAL.Qm
{
    /// <summary>
    /// 数据访问类Dal_Tb_Qm_StandardDetail。
    /// </summary>
    public class Dal_Tb_Qm_StandardDetail
    {
        public Dal_Tb_Qm_StandardDetail()
        {
            DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
        }
        #region  成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Id)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,50)};
            parameters[0].Value = Id;

            int result = DbHelperSQL.RunProcedure("Proc_Tb_Qm_StandardDetail_Exists", parameters, out rowsAffected);
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
        public void Add(HM.Model.Qm.Tb_Qm_StandardDetail model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,36),
                    new SqlParameter("@StanId", SqlDbType.VarChar,36),
                    new SqlParameter("@TaskLevelId", SqlDbType.VarChar,36),
                    new SqlParameter("@TaskRoleId", SqlDbType.VarChar,36),
                    new SqlParameter("@CheckFrequency", SqlDbType.VarChar,36),
                    new SqlParameter("@IsControlDate", SqlDbType.VarChar,36),
                    new SqlParameter("@PointCoverage", SqlDbType.Decimal,9),
                    new SqlParameter("@IsDelete", SqlDbType.Int,4),
                    new SqlParameter("@Sort", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.StanId;
            parameters[2].Value = model.TaskLevelId;
            parameters[3].Value = model.TaskRoleId;
            parameters[4].Value = model.CheckFrequency;
            parameters[5].Value = model.IsControlDate;
            parameters[6].Value = model.PointCoverage;
            parameters[7].Value = model.IsDelete;
            parameters[8].Value = model.Sort;

            DbHelperSQL.RunProcedure("Proc_Tb_Qm_StandardDetail_ADD", parameters, out rowsAffected);
        }
        public DataSet GetMaxList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(Sort) ");
            strSql.Append(" FROM Tb_Qm_StandardDetail ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        ///  更新一条数据
        /// </summary>
        public void Update(HM.Model.Qm.Tb_Qm_StandardDetail model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,36),
                    new SqlParameter("@StanId", SqlDbType.VarChar,36),
                    new SqlParameter("@TaskLevelId", SqlDbType.VarChar,36),
                    new SqlParameter("@TaskRoleId", SqlDbType.VarChar,36),
                    new SqlParameter("@CheckFrequency", SqlDbType.VarChar,36),
                    new SqlParameter("@IsControlDate", SqlDbType.VarChar,36),
                    new SqlParameter("@PointCoverage", SqlDbType.Decimal,9),
                    new SqlParameter("@IsDelete", SqlDbType.Int,4),
                    new SqlParameter("@Sort", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.StanId;
            parameters[2].Value = model.TaskLevelId;
            parameters[3].Value = model.TaskRoleId;
            parameters[4].Value = model.CheckFrequency;
            parameters[5].Value = model.IsControlDate;
            parameters[6].Value = model.PointCoverage;
            parameters[7].Value = model.IsDelete;
            parameters[8].Value = model.Sort;

            DbHelperSQL.RunProcedure("Proc_Tb_Qm_StandardDetail_Update", parameters, out rowsAffected);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(string Id)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,50)};
            parameters[0].Value = Id;

            DbHelperSQL.RunProcedure("Proc_Tb_Qm_StandardDetail_Delete", parameters, out rowsAffected);
        }
        /// <summary>
        /// 删除副表数据
        /// </summary>
        public void DeleteStanId(string Id)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,50)};
            parameters[0].Value = Id;

            DbHelperSQL.RunProcedure("Proc_Tb_Qm_StandardDetail_DeleteStanId", parameters, out rowsAffected);
        }
       
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HM.Model.Qm.Tb_Qm_StandardDetail GetModel(string Id)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,50)};
            parameters[0].Value = Id;

            HM.Model.Qm.Tb_Qm_StandardDetail model = new HM.Model.Qm.Tb_Qm_StandardDetail();
            DataSet ds = DbHelperSQL.RunProcedure("Proc_Tb_Qm_StandardDetail_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.Id = ds.Tables[0].Rows[0]["Id"].ToString();
                model.StanId = ds.Tables[0].Rows[0]["StanId"].ToString();
                model.TaskLevelId = ds.Tables[0].Rows[0]["TaskLevelId"].ToString();
                model.TaskRoleId = ds.Tables[0].Rows[0]["TaskRoleId"].ToString();
                model.CheckFrequency = ds.Tables[0].Rows[0]["CheckFrequency"].ToString();
                model.IsControlDate = ds.Tables[0].Rows[0]["IsControlDate"].ToString();
                if (ds.Tables[0].Rows[0]["PointCoverage"].ToString() != "")
                {
                    model.PointCoverage = decimal.Parse(ds.Tables[0].Rows[0]["PointCoverage"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDelete"].ToString() != "")
                {
                    model.IsDelete = int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sort"].ToString() != "")
                {
                    model.Sort = int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
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
            strSql.Append("select Id,StanId,TaskLevelId,TaskRoleId,CheckFrequency,IsControlDate,PointCoverage,IsDelete,Sort ");
            strSql.Append(" FROM Tb_Qm_StandardDetail ");
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
            strSql.Append(" Id,StanId,TaskLevelId,TaskRoleId,CheckFrequency,IsControlDate,PointCoverage,IsDelete,Sort ");
            strSql.Append(" FROM Tb_Qm_StandardDetail ");
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
            parameters[5].Value = "SELECT * FROM Tb_Qm_StandardDetail WHERE 1=1 " + StrCondition;
            parameters[6].Value = "Id";
            DataSet Ds = DbHelperSQL.RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        #endregion  成员方法
    }
}

