using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Unified
{
    /// <summary>
    /// 数据访问类Dal_Tb_User_Relation。
    /// </summary>
    public class Dal_Tb_User_Relation
    {
        public Dal_Tb_User_Relation()
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

            int result = DbHelperSQL.RunProcedure("Proc_Tb_User_Relation_Exists", parameters, out rowsAffected);
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
        public void Add(MobileSoft.Model.Unified.Tb_User_Relation model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,36),
                    new SqlParameter("@UserId", SqlDbType.VarChar,36),
                    new SqlParameter("@CommunityId", SqlDbType.VarChar,36),
                    new SqlParameter("@CustId", SqlDbType.VarChar,36),
                    new SqlParameter("@CustHoldId", SqlDbType.VarChar,36),
                    new SqlParameter("@RoomId", SqlDbType.VarChar,36),
                    new SqlParameter("@ParkingId", SqlDbType.VarChar,36),
                    new SqlParameter("@IsCurr", SqlDbType.VarChar,2),
                    new SqlParameter("@RegDate", SqlDbType.DateTime),
                    new SqlParameter("@CustName", SqlDbType.VarChar),
                    new SqlParameter("@RoomSign", SqlDbType.VarChar),
                    new SqlParameter("@Custmobile", SqlDbType.VarChar),
            };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.CommunityId;
            parameters[3].Value = model.CustId;
            parameters[4].Value = model.CustHoldId;
            parameters[5].Value = model.RoomId;
            parameters[6].Value = model.ParkingId;
            parameters[7].Value = model.IsCurr;
            parameters[8].Value = model.RegDate;
            parameters[9].Value = model.CustName;
            parameters[10].Value = model.RoomSign;
            parameters[11].Value = model.Custmobile;

            DbHelperSQL.RunProcedure("Proc_Tb_User_Relation_ADD", parameters, out rowsAffected);
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public void Update(MobileSoft.Model.Unified.Tb_User_Relation model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,36),
                    new SqlParameter("@UserId", SqlDbType.VarChar,36),
                    new SqlParameter("@CommunityId", SqlDbType.VarChar,36),
                    new SqlParameter("@CustId", SqlDbType.VarChar,36),
                    new SqlParameter("@CustHoldId", SqlDbType.VarChar,36),
                    new SqlParameter("@RoomId", SqlDbType.VarChar,36),
                    new SqlParameter("@ParkingId", SqlDbType.VarChar,36),
                    new SqlParameter("@IsCurr", SqlDbType.VarChar,2)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.CommunityId;
            parameters[3].Value = model.CustId;
            parameters[4].Value = model.CustHoldId;
            parameters[5].Value = model.RoomId;
            parameters[6].Value = model.ParkingId;
            parameters[7].Value = model.IsCurr;

            DbHelperSQL.RunProcedure("Proc_Tb_User_Relation_Update", parameters, out rowsAffected);
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

            DbHelperSQL.RunProcedure("Proc_Tb_User_Relation_Delete", parameters, out rowsAffected);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MobileSoft.Model.Unified.Tb_User_Relation GetModel(string Id)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,50)};
            parameters[0].Value = Id;

            MobileSoft.Model.Unified.Tb_User_Relation model = new MobileSoft.Model.Unified.Tb_User_Relation();
            DataSet ds = DbHelperSQL.RunProcedure("Proc_Tb_User_Relation_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.Id = ds.Tables[0].Rows[0]["Id"].ToString();
                model.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                model.CommunityId = ds.Tables[0].Rows[0]["CommunityId"].ToString();
                model.CustId = ds.Tables[0].Rows[0]["CustId"].ToString();
                model.CustHoldId = ds.Tables[0].Rows[0]["CustHoldId"].ToString();
                model.RoomId = ds.Tables[0].Rows[0]["RoomId"].ToString();
                model.ParkingId = ds.Tables[0].Rows[0]["ParkingId"].ToString();
                model.IsCurr = ds.Tables[0].Rows[0]["IsCurr"].ToString();
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
            strSql.Append("select Id,UserId,CommunityId,CustId,CustHoldId,RoomId,ParkingId,IsCurr ");
            strSql.Append(" FROM Tb_User_Relation ");
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
            strSql.Append(" Id,UserId,CommunityId,CustId,CustHoldId,RoomId,ParkingId,IsCurr ");
            strSql.Append(" FROM Tb_User_Relation ");
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
            parameters[5].Value = "SELECT * FROM Tb_User_Relation WHERE 1=1 " + StrCondition;
            parameters[6].Value = "Id";
            DataSet Ds = DbHelperSQL.RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        #endregion  成员方法
    }
}

