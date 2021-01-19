using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Resources
{
    /// <summary>
    /// 数据访问类Dal_Tb_Resources_Type。
    /// </summary>
    public class Dal_Tb_Resources_Type
    {
        public Dal_Tb_Resources_Type()
        {
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
        }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ResourcesTypeID)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@ResourcesTypeID", SqlDbType.BigInt)};
            parameters[0].Value = ResourcesTypeID;

            int result = DbHelperSQL.RunProcedure("Proc_Tb_Resources_Type_Exists", parameters, out rowsAffected);
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
        public void Add(MobileSoft.Model.Resources.Tb_Resources_Type model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@ResourcesTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@ResourcesTypeName", SqlDbType.NVarChar,80),
					new SqlParameter("@ResourcesTypeImgUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ResourcesTypeIndex", SqlDbType.SmallInt,2),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.BussId;
            parameters[2].Value = model.ResourcesTypeName;
            parameters[3].Value = model.ResourcesTypeImgUrl;
            parameters[4].Value = model.ResourcesTypeIndex;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.IsDelete;

            DbHelperSQL.RunProcedure("Proc_Tb_Resources_Type_ADD", parameters, out rowsAffected);
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public void Update(MobileSoft.Model.Resources.Tb_Resources_Type model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@ResourcesTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@ResourcesTypeName", SqlDbType.NVarChar,80),
					new SqlParameter("@ResourcesTypeImgUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ResourcesTypeIndex", SqlDbType.SmallInt,2),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
            parameters[0].Value = model.ResourcesTypeID;
            parameters[1].Value = model.BussId;
            parameters[2].Value = model.ResourcesTypeName;
            parameters[3].Value = model.ResourcesTypeImgUrl;
            parameters[4].Value = model.ResourcesTypeIndex;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.IsDelete;

            DbHelperSQL.RunProcedure("Proc_Tb_Resources_Type_Update", parameters, out rowsAffected);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(long ResourcesTypeID)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@ResourcesTypeID", SqlDbType.BigInt)};
            parameters[0].Value = ResourcesTypeID;

            DbHelperSQL.RunProcedure("Proc_Tb_Resources_Type_Delete", parameters, out rowsAffected);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MobileSoft.Model.Resources.Tb_Resources_Type GetModel(long ResourcesTypeID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ResourcesTypeID", SqlDbType.BigInt)};
            parameters[0].Value = ResourcesTypeID;
            MobileSoft.Model.Resources.Tb_Resources_Type model = new MobileSoft.Model.Resources.Tb_Resources_Type();
            DataSet ds = DbHelperSQL.RunProcedure("Proc_Tb_Resources_Type_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ResourcesTypeID"].ToString() != "")
                {
                    model.ResourcesTypeID = long.Parse(ds.Tables[0].Rows[0]["ResourcesTypeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BussId"].ToString() != "")
                {
                    model.BussId = long.Parse(ds.Tables[0].Rows[0]["BussId"].ToString());
                }
                model.ResourcesTypeName = ds.Tables[0].Rows[0]["ResourcesTypeName"].ToString();
                model.ResourcesTypeImgUrl = ds.Tables[0].Rows[0]["ResourcesTypeImgUrl"].ToString();
                if (ds.Tables[0].Rows[0]["ResourcesTypeIndex"].ToString() != "")
                {
                    model.ResourcesTypeIndex = int.Parse(ds.Tables[0].Rows[0]["ResourcesTypeIndex"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
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
            strSql.Append("select * ");
            strSql.Append(" FROM view_Resources_Type_Filter ");
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
            strSql.Append(" ResourcesTypeID,BussId,ResourcesTypeName,ResourcesTypeImgUrl,ResourcesTypeIndex,Remark,IsDelete ");
            strSql.Append(" FROM Tb_Resources_Type ");
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
            parameters[5].Value = "SELECT * FROM Tb_Resources_Type WHERE 1=1 " + StrCondition;
            parameters[6].Value = "ResourcesTypeID";
            DataSet Ds = DbHelperSQL.RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        #endregion  成员方法
    }
}

