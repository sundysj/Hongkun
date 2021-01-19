using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Order
{
    /// <summary>
    /// 数据访问类Dal_Tb_Order_BusCar。
    /// </summary>
    public class Dal_Tb_Order_BusCar
    {
        public Dal_Tb_Order_BusCar()
        {
            DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
        }
        #region  成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string BusCarID)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@BusCarID", SqlDbType.NVarChar,50)};
            parameters[0].Value = BusCarID;

            int result = DbHelperSQL.RunProcedure("Proc_Tb_Order_BusCar_Exists", parameters, out rowsAffected);
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
        public void Add(MobileSoft.Model.Order.Tb_Order_BusCar model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@BusCarID", SqlDbType.NVarChar,50),
					new SqlParameter("@BusCustID", SqlDbType.BigInt,8),
					new SqlParameter("@BusReleaseID", SqlDbType.BigInt,8),
					new SqlParameter("@BusBussId", SqlDbType.BigInt,8),
					new SqlParameter("@Created", SqlDbType.DateTime)};
            parameters[0].Value = model.BusCarID;
            parameters[1].Value = model.BusCustID;
            parameters[2].Value = model.BusReleaseID;
            parameters[3].Value = model.BusBussId;
            parameters[4].Value = model.Created;

            DbHelperSQL.RunProcedure("Proc_Tb_Order_BusCar_ADD", parameters, out rowsAffected);
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public void Update(MobileSoft.Model.Order.Tb_Order_BusCar model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@BusCarID", SqlDbType.NVarChar,50),
					new SqlParameter("@BusCustID", SqlDbType.BigInt,8),
					new SqlParameter("@BusReleaseID", SqlDbType.BigInt,8),
					new SqlParameter("@BusBussId", SqlDbType.BigInt,8),
					new SqlParameter("@Created", SqlDbType.DateTime)};
            parameters[0].Value = model.BusCarID;
            parameters[1].Value = model.BusCustID;
            parameters[2].Value = model.BusReleaseID;
            parameters[3].Value = model.BusBussId;
            parameters[4].Value = model.Created;

            DbHelperSQL.RunProcedure("Proc_Tb_Order_BusCar_Update", parameters, out rowsAffected);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(string BusCarID)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@BusCarID", SqlDbType.NVarChar,50)};
            parameters[0].Value = BusCarID;

            DbHelperSQL.RunProcedure("Proc_Tb_Order_BusCar_Delete", parameters, out rowsAffected);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MobileSoft.Model.Order.Tb_Order_BusCar GetModel(string BusCarID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@BusCarID", SqlDbType.NVarChar,50)};
            parameters[0].Value = BusCarID;

            MobileSoft.Model.Order.Tb_Order_BusCar model = new MobileSoft.Model.Order.Tb_Order_BusCar();
            DataSet ds = DbHelperSQL.RunProcedure("Proc_Tb_Order_BusCar_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.BusCarID = ds.Tables[0].Rows[0]["BusCarID"].ToString();
                if (ds.Tables[0].Rows[0]["BusCustID"].ToString() != "")
                {
                    model.BusCustID = long.Parse(ds.Tables[0].Rows[0]["BusCustID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BusReleaseID"].ToString() != "")
                {
                    model.BusReleaseID = long.Parse(ds.Tables[0].Rows[0]["BusReleaseID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BusBussId"].ToString() != "")
                {
                    model.BusBussId = long.Parse(ds.Tables[0].Rows[0]["BusBussId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Created"].ToString() != "")
                {
                    model.Created = DateTime.Parse(ds.Tables[0].Rows[0]["Created"].ToString());
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
            strSql.Append("select BusCarID,BusCustID,BusReleaseID,BusBussId,Created ");
            strSql.Append(" FROM Tb_Order_BusCar ");
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
            strSql.Append(" BusCarID,BusCustID,BusReleaseID,BusBussId,Created ");
            strSql.Append(" FROM Tb_Order_BusCar ");
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
            parameters[5].Value = "SELECT * FROM Tb_Order_BusCar WHERE 1=1 " + StrCondition;
            parameters[6].Value = "BusCarID";
            DataSet Ds = DbHelperSQL.RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        #endregion  成员方法
    }
}

