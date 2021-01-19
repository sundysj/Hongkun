using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Eq
{
    /// <summary>
    /// 数据访问类Dal_Tb_Eq_EquipmentStatus。
    /// </summary>
    public class Dal_Tb_Eq_EquipmentStatus
    {
        public Dal_Tb_Eq_EquipmentStatus()
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

            int result = DbHelperSQL.RunProcedure("Proc_Tb_Eq_EquipmentStatus_Exists", parameters, out rowsAffected);
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
        public void Add(HM.Model.Eq.Tb_Eq_EquipmentStatus model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,36),
                    new SqlParameter("@BeginTime", SqlDbType.DateTime),
                    new SqlParameter("@EndTime", SqlDbType.DateTime),
                    new SqlParameter("@EquipmentStatus", SqlDbType.VarChar,36),
                    new SqlParameter("@Remark", SqlDbType.NVarChar),
                    new SqlParameter("@EquipmentId", SqlDbType.VarChar,36),
                    new SqlParameter("@AddPid", SqlDbType.VarChar,36),
                    new SqlParameter("@AddTime", SqlDbType.DateTime),
                    new SqlParameter("@OperationPid", SqlDbType.VarChar,36),
                    new SqlParameter("@OperationTime", SqlDbType.DateTime),
                    new SqlParameter("@IsDelete", SqlDbType.Int,4),
                    new SqlParameter("@Express", SqlDbType.NVarChar,3000)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.BeginTime;
            parameters[2].Value = model.EndTime;
            parameters[3].Value = model.EquipmentStatus;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.EquipmentId;
            parameters[6].Value = model.AddPid;
            parameters[7].Value = model.AddTime;
            parameters[8].Value = model.OperationPid;
            parameters[9].Value = model.OperationTime;
            parameters[10].Value = model.IsDelete;
            parameters[11].Value = model.Express;

            DbHelperSQL.RunProcedure("Proc_Tb_Eq_EquipmentStatus_ADD", parameters, out rowsAffected);
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public void Update(HM.Model.Eq.Tb_Eq_EquipmentStatus model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,36),
                    new SqlParameter("@BeginTime", SqlDbType.DateTime),
                    new SqlParameter("@EndTime", SqlDbType.DateTime),
                    new SqlParameter("@EquipmentStatus", SqlDbType.VarChar,36),
                    new SqlParameter("@Remark", SqlDbType.NVarChar),
                    new SqlParameter("@EquipmentId", SqlDbType.VarChar,36),
                    new SqlParameter("@AddPid", SqlDbType.VarChar,36),
                    new SqlParameter("@AddTime", SqlDbType.DateTime),
                    new SqlParameter("@OperationPid", SqlDbType.VarChar,36),
                    new SqlParameter("@OperationTime", SqlDbType.DateTime),
                    new SqlParameter("@IsDelete", SqlDbType.Int,4),
                    new SqlParameter("@Express", SqlDbType.NVarChar,3000)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.BeginTime;
            parameters[2].Value = model.EndTime;
            parameters[3].Value = model.EquipmentStatus;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.EquipmentId;
            parameters[6].Value = model.AddPid;
            parameters[7].Value = model.AddTime;
            parameters[8].Value = model.OperationPid;
            parameters[9].Value = model.OperationTime;
            parameters[10].Value = model.IsDelete;
            parameters[11].Value = model.Express;

            DbHelperSQL.RunProcedure("Proc_Tb_Eq_EquipmentStatus_Update", parameters, out rowsAffected);
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

            DbHelperSQL.RunProcedure("Proc_Tb_Eq_EquipmentStatus_Delete", parameters, out rowsAffected);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HM.Model.Eq.Tb_Eq_EquipmentStatus GetModel(string Id)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,50)};
            parameters[0].Value = Id;

            HM.Model.Eq.Tb_Eq_EquipmentStatus model = new HM.Model.Eq.Tb_Eq_EquipmentStatus();
            DataSet ds = DbHelperSQL.RunProcedure("Proc_Tb_Eq_EquipmentStatus_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.Id = ds.Tables[0].Rows[0]["Id"].ToString();
                if (ds.Tables[0].Rows[0]["BeginTime"].ToString() != "")
                {
                    model.BeginTime = DateTime.Parse(ds.Tables[0].Rows[0]["BeginTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndTime"].ToString() != "")
                {
                    model.EndTime = DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
                }
                model.EquipmentStatus = ds.Tables[0].Rows[0]["EquipmentStatus"].ToString();
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                model.EquipmentId = ds.Tables[0].Rows[0]["EquipmentId"].ToString();
                model.AddPid = ds.Tables[0].Rows[0]["AddPid"].ToString();
                if (ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
                }
                model.OperationPid = ds.Tables[0].Rows[0]["OperationPid"].ToString();
                if (ds.Tables[0].Rows[0]["OperationTime"].ToString() != "")
                {
                    model.OperationTime = DateTime.Parse(ds.Tables[0].Rows[0]["OperationTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDelete"].ToString() != "")
                {
                    model.IsDelete = int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
                }
                model.Express = ds.Tables[0].Rows[0]["Express"].ToString();
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
            strSql.Append("select Id,BeginTime,EndTime,EquipmentStatus,Remark,EquipmentId,AddPid,AddTime,OperationPid,OperationTime,IsDelete,Express ");
            strSql.Append(" FROM Tb_Eq_EquipmentStatus ");
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
            strSql.Append(" Id,BeginTime,EndTime,EquipmentStatus,Remark,EquipmentId,AddPid,AddTime,OperationPid,OperationTime,IsDelete,Express ");
            strSql.Append(" FROM Tb_Eq_EquipmentStatus ");
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
            parameters[5].Value = "SELECT * FROM Tb_Eq_EquipmentStatus WHERE 1=1 " + StrCondition;
            parameters[6].Value = "Id";
            DataSet Ds = DbHelperSQL.RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        #endregion  成员方法
    }
}

