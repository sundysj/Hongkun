using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Unified
{
    /// <summary>
    /// 数据访问类Dal_Tb_Notice。
    /// </summary>
    public class Dal_Tb_Notice
    {
        public Dal_Tb_Notice()
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

            int result = DbHelperSQL.RunProcedure("Proc_Tb_Notice_Exists", parameters, out rowsAffected);
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
        public void Add(MobileSoft.Model.Unified.Tb_Notice model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,36),
                    new SqlParameter("@UserId", SqlDbType.VarChar,36),
                    new SqlParameter("@NoticeType", SqlDbType.Int,4),
                    new SqlParameter("@Title", SqlDbType.VarChar,3999),
                    new SqlParameter("@Content", SqlDbType.Text),
                    new SqlParameter("@IssueDate", SqlDbType.DateTime),
                    new SqlParameter("@CommunityId", SqlDbType.VarChar,36),
                    new SqlParameter("@IsDelete", SqlDbType.Int,4),
                    new SqlParameter("@ImageURL", SqlDbType.NVarChar,200),
                    new SqlParameter("@ContentURL", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.NoticeType;
            parameters[3].Value = model.Title;
            parameters[4].Value = model.Content;
            parameters[5].Value = model.IssueDate;
            parameters[6].Value = model.CommunityId;
            parameters[7].Value = model.IsDelete;
            parameters[8].Value = model.ImageURL;
            parameters[9].Value = model.ContentURL;

            DbHelperSQL.RunProcedure("Proc_Tb_Notice_ADD", parameters, out rowsAffected);
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public void Update(MobileSoft.Model.Unified.Tb_Notice model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,36),
                    new SqlParameter("@UserId", SqlDbType.VarChar,36),
                    new SqlParameter("@NoticeType", SqlDbType.Int,4),
                    new SqlParameter("@Title", SqlDbType.VarChar,3999),
                    new SqlParameter("@Content", SqlDbType.Text),
                    new SqlParameter("@IssueDate", SqlDbType.DateTime),
                    new SqlParameter("@CommunityId", SqlDbType.VarChar,36),
                    new SqlParameter("@IsDelete", SqlDbType.Int,4),
                    new SqlParameter("@ImageURL", SqlDbType.NVarChar,200),
                    new SqlParameter("@ContentURL", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.NoticeType;
            parameters[3].Value = model.Title;
            parameters[4].Value = model.Content;
            parameters[5].Value = model.IssueDate;
            parameters[6].Value = model.CommunityId;
            parameters[7].Value = model.IsDelete;
            parameters[8].Value = model.ImageURL;
            parameters[9].Value = model.ContentURL;

            DbHelperSQL.RunProcedure("Proc_Tb_Notice_Update", parameters, out rowsAffected);
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

            DbHelperSQL.RunProcedure("Proc_Tb_Notice_Delete", parameters, out rowsAffected);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MobileSoft.Model.Unified.Tb_Notice GetModel(string Id)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,50)};
            parameters[0].Value = Id;

            MobileSoft.Model.Unified.Tb_Notice model = new MobileSoft.Model.Unified.Tb_Notice();
            DataSet ds = DbHelperSQL.RunProcedure("Proc_Tb_Notice_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.Id = ds.Tables[0].Rows[0]["Id"].ToString();
                model.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                if (ds.Tables[0].Rows[0]["NoticeType"].ToString() != "")
                {
                    model.NoticeType = int.Parse(ds.Tables[0].Rows[0]["NoticeType"].ToString());
                }
                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                model.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                if (ds.Tables[0].Rows[0]["IssueDate"].ToString() != "")
                {
                    model.IssueDate = DateTime.Parse(ds.Tables[0].Rows[0]["IssueDate"].ToString());
                }
                model.CommunityId = ds.Tables[0].Rows[0]["CommunityId"].ToString();
                if (ds.Tables[0].Rows[0]["IsDelete"].ToString() != "")
                {
                    model.IsDelete = int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
                }
                model.ImageURL = ds.Tables[0].Rows[0]["ImageURL"].ToString();
                model.ContentURL = ds.Tables[0].Rows[0]["ContentURL"].ToString();
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
            strSql.Append("select Id,UserId,NoticeType,Title,Content,IssueDate,CommunityId,IsDelete,ImageURL,ContentURL ");
            strSql.Append(" FROM Tb_Notice ");
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
            strSql.Append(" Id,UserId,NoticeType,Title,Content,IssueDate,CommunityId,IsDelete,ImageURL,ContentURL ");
            strSql.Append(" FROM Tb_Notice ");
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
            parameters[5].Value = "SELECT * FROM Tb_Notice WHERE 1=1 " + StrCondition;
            parameters[6].Value = "Id";
            DataSet Ds = DbHelperSQL.RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        #endregion  成员方法
    }
}

