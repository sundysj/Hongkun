using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Common
{
    /// <summary>
    /// 数据访问类Dal_Tb_SendMessageRecord。
    /// </summary>
    public class Dal_Tb_SendMessageRecord
    {
        public Dal_Tb_SendMessageRecord()
        {
            DbHelperSQL.ConnectionString = PubConstant.UnifiedContionString;
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

            int result = DbHelperSQL.RunProcedure("Proc_Tb_SendMessageRecord_Exists", parameters, out rowsAffected);
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
        public void Add(MobileSoft.Model.Common.Tb_SendMessageRecord model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,36),
                    new SqlParameter("@Mobile", SqlDbType.VarChar,11),
                    new SqlParameter("@SendContent", SqlDbType.VarChar,3999),
                    new SqlParameter("@SendTime", SqlDbType.DateTime),
                    new SqlParameter("@MacCode", SqlDbType.VarChar,500),
                    new SqlParameter("@SendType", SqlDbType.VarChar,50),
                    new SqlParameter("@SendState",SqlDbType.VarChar,50)
            };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Mobile;
            parameters[2].Value = model.SendContent;
            parameters[3].Value = model.SendTime;
            parameters[4].Value = model.MacCode;
            parameters[5].Value = model.SendType;
            parameters[6].Value = model.SendState;

            DbHelperSQL.RunProcedure("Proc_Tb_SendMessageRecord_ADD", parameters, out rowsAffected);
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public void Update(MobileSoft.Model.Common.Tb_SendMessageRecord model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,36),
                    new SqlParameter("@Mobile", SqlDbType.VarChar,11),
                    new SqlParameter("@SendContent", SqlDbType.VarChar,3999),
                    new SqlParameter("@SendTime", SqlDbType.DateTime),
                    new SqlParameter("@MacCode", SqlDbType.VarChar,500),
                    new SqlParameter("@SendType", SqlDbType.VarChar,50),
            new SqlParameter("@SendState",SqlDbType.VarChar,50)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Mobile;
            parameters[2].Value = model.SendContent;
            parameters[3].Value = model.SendTime;
            parameters[4].Value = model.MacCode;
            parameters[5].Value = model.SendType;
            parameters[6].Value = model.SendState;
            DbHelperSQL.RunProcedure("Proc_Tb_SendMessageRecord_Update", parameters, out rowsAffected);
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

            DbHelperSQL.RunProcedure("Proc_Tb_SendMessageRecord_Delete", parameters, out rowsAffected);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MobileSoft.Model.Common.Tb_SendMessageRecord GetModel(string Id)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,50)};
            parameters[0].Value = Id;

            MobileSoft.Model.Common.Tb_SendMessageRecord model = new MobileSoft.Model.Common.Tb_SendMessageRecord();
            DataSet ds = DbHelperSQL.RunProcedure("Proc_Tb_SendMessageRecord_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.Id = ds.Tables[0].Rows[0]["Id"].ToString();
                model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                model.SendContent = ds.Tables[0].Rows[0]["SendContent"].ToString();
                if (ds.Tables[0].Rows[0]["SendTime"].ToString() != "")
                {
                    model.SendTime = DateTime.Parse(ds.Tables[0].Rows[0]["SendTime"].ToString());
                }
                model.MacCode = ds.Tables[0].Rows[0]["MacCode"].ToString();
                model.SendType = ds.Tables[0].Rows[0]["SendType"].ToString();
                model.SendState = ds.Tables[0].Rows[0]["SendState"].ToString();
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
            strSql.Append("select Id,Mobile,SendContent,SendTime,MacCode,SendType,SendState ");
            strSql.Append(" FROM Tb_SendMessageRecord ");
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
            strSql.Append(" Id,Mobile,SendContent,SendTime,MacCode,SendType,SendState ");
            strSql.Append(" FROM Tb_SendMessageRecord ");
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
            parameters[5].Value = "SELECT * FROM Tb_SendMessageRecord WHERE 1=1 " + StrCondition;
            parameters[6].Value = "Id";
            DataSet Ds = DbHelperSQL.RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        #endregion  成员方法
    }
}

