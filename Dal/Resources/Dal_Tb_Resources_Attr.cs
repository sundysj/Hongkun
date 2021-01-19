using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Resources
{
    /// <summary>
    /// 数据访问类Dal_Tb_Resources_Attr。
    /// </summary>
    public class Dal_Tb_Resources_Attr
    {
        public Dal_Tb_Resources_Attr()
        {
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
        }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long AttrID)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@AttrID", SqlDbType.BigInt)};
            parameters[0].Value = AttrID;

            int result = DbHelperSQL.RunProcedure("Proc_Tb_Resources_Attr_Exists", parameters, out rowsAffected);
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
        public void Add(MobileSoft.Model.Resources.Tb_Resources_Attr model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@AttrID", SqlDbType.BigInt,8),
					new SqlParameter("@AttrName", SqlDbType.NVarChar,100),
					new SqlParameter("@AttrIndex", SqlDbType.SmallInt,2),
					new SqlParameter("@AttrType", SqlDbType.NVarChar,50),
					new SqlParameter("@AttrColor", SqlDbType.NVarChar,50),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.AttrName;
            parameters[2].Value = model.AttrIndex;
            parameters[3].Value = model.AttrType;
            parameters[4].Value = model.AttrColor;
            parameters[5].Value = model.BussId;
            parameters[6].Value = model.IsDelete;

            DbHelperSQL.RunProcedure("Proc_Tb_Resources_Attr_ADD", parameters, out rowsAffected);
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public void Update(MobileSoft.Model.Resources.Tb_Resources_Attr model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@AttrID", SqlDbType.BigInt,8),
					new SqlParameter("@AttrName", SqlDbType.NVarChar,100),
					new SqlParameter("@AttrIndex", SqlDbType.SmallInt,2),
					new SqlParameter("@AttrType", SqlDbType.NVarChar,50),
					new SqlParameter("@AttrColor", SqlDbType.NVarChar,50),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
            parameters[0].Value = model.AttrID;
            parameters[1].Value = model.AttrName;
            parameters[2].Value = model.AttrIndex;
            parameters[3].Value = model.AttrType;
            parameters[4].Value = model.AttrColor;
            parameters[5].Value = model.BussId;
            parameters[6].Value = model.IsDelete;

            DbHelperSQL.RunProcedure("Proc_Tb_Resources_Attr_Update", parameters, out rowsAffected);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(long AttrID)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@AttrID", SqlDbType.BigInt)};
            parameters[0].Value = AttrID;

            DbHelperSQL.RunProcedure("Proc_Tb_Resources_Attr_Delete", parameters, out rowsAffected);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MobileSoft.Model.Resources.Tb_Resources_Attr GetModel(long AttrID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@AttrID", SqlDbType.BigInt)};
            parameters[0].Value = AttrID;

            MobileSoft.Model.Resources.Tb_Resources_Attr model = new MobileSoft.Model.Resources.Tb_Resources_Attr();
            DataSet ds = DbHelperSQL.RunProcedure("Proc_Tb_Resources_Attr_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["AttrID"].ToString() != "")
                {
                    model.AttrID = long.Parse(ds.Tables[0].Rows[0]["AttrID"].ToString());
                }
                model.AttrName = ds.Tables[0].Rows[0]["AttrName"].ToString();
                if (ds.Tables[0].Rows[0]["AttrIndex"].ToString() != "")
                {
                    model.AttrIndex = int.Parse(ds.Tables[0].Rows[0]["AttrIndex"].ToString());
                }
                model.AttrType = ds.Tables[0].Rows[0]["AttrType"].ToString();
                model.AttrColor = ds.Tables[0].Rows[0]["AttrColor"].ToString();
                if (ds.Tables[0].Rows[0]["BussId"].ToString() != "")
                {
                    model.BussId = long.Parse(ds.Tables[0].Rows[0]["BussId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDelete"].ToString() != "")
                {
                    model.IsDelete = int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
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
            strSql.Append("select AttrID,AttrName,AttrIndex,AttrType,AttrColor,BussId,IsDelete ");
            strSql.Append(" FROM Tb_Resources_Attr ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string fieldOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" AttrID,AttrName,AttrIndex,AttrType,AttrColor,BussId,IsDelete ");
            strSql.Append(" FROM Tb_Resources_Attr ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + fieldOrder);
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
            parameters[5].Value = "SELECT * FROM Tb_Resources_Attr WHERE 1=1 " + StrCondition;
            parameters[6].Value = "AttrID";
            DataSet Ds = DbHelperSQL.RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        #endregion  成员方法
    }
}

