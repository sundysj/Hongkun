using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace HM.DAL.Qm
{
    /// <summary>
    /// 数据访问类Dal_Tb_Qm_TaskLevel。
    /// </summary>
    public class Dal_Tb_Qm_TaskLevel
    {
        public Dal_Tb_Qm_TaskLevel()
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

            int result = DbHelperSQL.RunProcedure("Proc_Tb_Qm_TaskLevel_Exists", parameters, out rowsAffected);
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
        public void Add(HM.Model.Qm.Tb_Qm_TaskLevel model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,36),
                    new SqlParameter("@TaskLevelName", SqlDbType.VarChar,36),
                    new SqlParameter("@TaskRoleId", SqlDbType.VarChar,36),
                    new SqlParameter("@CheckRoleId", SqlDbType.VarChar,36),
                    new SqlParameter("@IsDelete", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.VarChar),
                    new SqlParameter("@Sort", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.TaskLevelName;
            parameters[2].Value = model.TaskRoleId;
            parameters[3].Value = model.CheckRoleId;
            parameters[4].Value = model.IsDelete;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.Sort;

            DbHelperSQL.RunProcedure("Proc_Tb_Qm_TaskLevel_ADD", parameters, out rowsAffected);
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public void Update(HM.Model.Qm.Tb_Qm_TaskLevel model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,36),
                    new SqlParameter("@TaskLevelName", SqlDbType.VarChar,36),
                    new SqlParameter("@TaskRoleId", SqlDbType.VarChar,36),
                    new SqlParameter("@CheckRoleId", SqlDbType.VarChar,36),
                    new SqlParameter("@IsDelete", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.VarChar),
                    new SqlParameter("@Sort", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.TaskLevelName;
            parameters[2].Value = model.TaskRoleId;
            parameters[3].Value = model.CheckRoleId;
            parameters[4].Value = model.IsDelete;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.Sort;

            DbHelperSQL.RunProcedure("Proc_Tb_Qm_TaskLevel_Update", parameters, out rowsAffected);
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

            DbHelperSQL.RunProcedure("Proc_Tb_Qm_TaskLevel_Delete", parameters, out rowsAffected);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HM.Model.Qm.Tb_Qm_TaskLevel GetModel(string Id)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,50)};
            parameters[0].Value = Id;

            HM.Model.Qm.Tb_Qm_TaskLevel model = new HM.Model.Qm.Tb_Qm_TaskLevel();
            DataSet ds = DbHelperSQL.RunProcedure("Proc_Tb_Qm_TaskLevel_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.Id = ds.Tables[0].Rows[0]["Id"].ToString();
                model.TaskLevelName = ds.Tables[0].Rows[0]["TaskLevelName"].ToString();
                model.TaskRoleId = ds.Tables[0].Rows[0]["TaskRoleId"].ToString();
                model.CheckRoleId = ds.Tables[0].Rows[0]["CheckRoleId"].ToString();
                if (ds.Tables[0].Rows[0]["IsDelete"].ToString() != "")
                {
                    model.IsDelete = int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
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
            strSql.Append("select Id,TaskLevelName,TaskRoleId,CheckRoleId,IsDelete,Remark,Sort ");
            strSql.Append(" FROM Tb_Qm_TaskLevel ");
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
            strSql.Append(" Id,TaskLevelName,TaskRoleId,CheckRoleId,IsDelete,Remark,Sort ");
            strSql.Append(" FROM Tb_Qm_TaskLevel ");
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
            parameters[5].Value = "SELECT * FROM Tb_Qm_TaskLevel WHERE 1=1 " + StrCondition;
            parameters[6].Value = "Id";
            DataSet Ds = DbHelperSQL.RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        #endregion  成员方法
    }
}

