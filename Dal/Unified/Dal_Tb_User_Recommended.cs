using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Unified
{
    /// <summary>
    /// 数据访问类Dal_Tb_User_Recommended。
    /// </summary>
    public class Dal_Tb_User_Recommended
    {
        public Dal_Tb_User_Recommended()
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

            int result = DbHelperSQL.RunProcedure("Proc_Tb_User_Recommended_Exists", parameters, out rowsAffected);
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
        public void Add(MobileSoft.Model.Unified.Tb_User_Recommended model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,36),
                    new SqlParameter("@UserId", SqlDbType.VarChar,36),
                    new SqlParameter("@CommunityId", SqlDbType.VarChar,36),
                    new SqlParameter("@RecommendedContent", SqlDbType.VarChar),
                    new SqlParameter("@SubmitTime", SqlDbType.DateTime),
                    new SqlParameter("@ReplyContent", SqlDbType.VarChar),
                    new SqlParameter("@ReplyState", SqlDbType.VarChar,50),
                    new SqlParameter("@EvaluationLevel", SqlDbType.Int,4),
                    new SqlParameter("@ReplyTime", SqlDbType.DateTime),
                    new SqlParameter("@ReplyPeople", SqlDbType.VarChar,36),
                    new SqlParameter("@IsDelete", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.CommunityId;
            parameters[3].Value = model.RecommendedContent;
            parameters[4].Value = model.SubmitTime;
            parameters[5].Value = model.ReplyContent;
            parameters[6].Value = model.ReplyState;
            parameters[7].Value = model.EvaluationLevel;
            parameters[8].Value = model.ReplyTime;
            parameters[9].Value = model.ReplyPeople;
            parameters[10].Value = model.IsDelete;

            DbHelperSQL.RunProcedure("Proc_Tb_User_Recommended_ADD", parameters, out rowsAffected);
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public void Update(MobileSoft.Model.Unified.Tb_User_Recommended model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,36),
                    new SqlParameter("@UserId", SqlDbType.VarChar,36),
                    new SqlParameter("@CommunityId", SqlDbType.VarChar,36),
                    new SqlParameter("@RecommendedContent", SqlDbType.VarChar),
                    new SqlParameter("@SubmitTime", SqlDbType.DateTime),
                    new SqlParameter("@ReplyContent", SqlDbType.VarChar),
                    new SqlParameter("@ReplyState", SqlDbType.VarChar,50),
                    new SqlParameter("@EvaluationLevel", SqlDbType.Int,4),
                    new SqlParameter("@ReplyTime", SqlDbType.DateTime),
                    new SqlParameter("@ReplyPeople", SqlDbType.VarChar,36),
                    new SqlParameter("@IsDelete", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.CommunityId;
            parameters[3].Value = model.RecommendedContent;
            parameters[4].Value = model.SubmitTime;
            parameters[5].Value = model.ReplyContent;
            parameters[6].Value = model.ReplyState;
            parameters[7].Value = model.EvaluationLevel;
            parameters[8].Value = model.ReplyTime;
            parameters[9].Value = model.ReplyPeople;
            parameters[10].Value = model.IsDelete;

            DbHelperSQL.RunProcedure("Proc_Tb_User_Recommended_Update", parameters, out rowsAffected);
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

            DbHelperSQL.RunProcedure("Proc_Tb_User_Recommended_Delete", parameters, out rowsAffected);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MobileSoft.Model.Unified.Tb_User_Recommended GetModel(string Id)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,50)};
            parameters[0].Value = Id;

            MobileSoft.Model.Unified.Tb_User_Recommended model = new MobileSoft.Model.Unified.Tb_User_Recommended();
            DataSet ds = DbHelperSQL.RunProcedure("Proc_Tb_User_Recommended_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.Id = ds.Tables[0].Rows[0]["Id"].ToString();
                model.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                model.CommunityId = ds.Tables[0].Rows[0]["CommunityId"].ToString();
                model.RecommendedContent = ds.Tables[0].Rows[0]["RecommendedContent"].ToString();
                if (ds.Tables[0].Rows[0]["SubmitTime"].ToString() != "")
                {
                    model.SubmitTime = ds.Tables[0].Rows[0]["SubmitTime"].ToString();
                }
                model.ReplyContent = ds.Tables[0].Rows[0]["ReplyContent"].ToString();
                model.ReplyState = ds.Tables[0].Rows[0]["ReplyState"].ToString();
                if (ds.Tables[0].Rows[0]["EvaluationLevel"].ToString() != "")
                {
                    model.EvaluationLevel = int.Parse(ds.Tables[0].Rows[0]["EvaluationLevel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReplyTime"].ToString() != "")
                {
                    model.ReplyTime = DateTime.Parse(ds.Tables[0].Rows[0]["ReplyTime"].ToString());
                }
                model.ReplyPeople = ds.Tables[0].Rows[0]["ReplyPeople"].ToString();
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
            strSql.Append(" FROM View_USER_Recommended_Filter ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where  IsDelete=0 and " + strWhere);
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
            strSql.Append(" * ");
            strSql.Append(" FROM View_USER_Recommended_Filter ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where  IsDelete=0 and " + strWhere);
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
            parameters[5].Value = "SELECT * FROM View_USER_Recommended_Filter WHERE IsDelete=0 and  " + StrCondition;
            parameters[6].Value = "Id";
            DataSet Ds = DbHelperSQL.RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        #endregion  成员方法
    }
}

