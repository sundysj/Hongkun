using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Common
{
	/// <summary>
	/// 数据访问类Dal_Tb_Common_CommunityService。
	/// </summary>
	public class Dal_Tb_Common_CommunityService
	{
		public Dal_Tb_Common_CommunityService()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt)};
			parameters[0].Value = InfoId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Common_CommunityService_Exists",parameters,out rowsAffected);
			if(result==1)
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
		public int Add(MobileSoft.Model.Common.Tb_Common_CommunityService model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt,8),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@DepCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Content", SqlDbType.NText),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@IssueDate", SqlDbType.DateTime),
					new SqlParameter("@InfoTypeName", SqlDbType.NVarChar,20)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.OrganCode;
			parameters[2].Value = model.DepCode;
			parameters[3].Value = model.Title;
			parameters[4].Value = model.Content;
			parameters[5].Value = model.UserCode;
			parameters[6].Value = model.IssueDate;
			parameters[7].Value = model.InfoTypeName;

			DbHelperSQL.RunProcedure("Proc_Tb_Common_CommunityService_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Common.Tb_Common_CommunityService model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt,8),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@DepCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Content", SqlDbType.NText),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@IssueDate", SqlDbType.DateTime),
					new SqlParameter("@InfoTypeName", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.OrganCode;
			parameters[2].Value = model.DepCode;
			parameters[3].Value = model.Title;
			parameters[4].Value = model.Content;
			parameters[5].Value = model.UserCode;
			parameters[6].Value = model.IssueDate;
			parameters[7].Value = model.InfoTypeName;

			DbHelperSQL.RunProcedure("Proc_Tb_Common_CommunityService_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt)};
			parameters[0].Value = InfoId;

			DbHelperSQL.RunProcedure("Proc_Tb_Common_CommunityService_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Common.Tb_Common_CommunityService GetModel(long InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.Common.Tb_Common_CommunityService model=new MobileSoft.Model.Common.Tb_Common_CommunityService();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Common_CommunityService_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoId"].ToString()!="")
				{
					model.InfoId=long.Parse(ds.Tables[0].Rows[0]["InfoId"].ToString());
				}
				model.OrganCode=ds.Tables[0].Rows[0]["OrganCode"].ToString();
				model.DepCode=ds.Tables[0].Rows[0]["DepCode"].ToString();
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				if(ds.Tables[0].Rows[0]["IssueDate"].ToString()!="")
				{
					model.IssueDate=DateTime.Parse(ds.Tables[0].Rows[0]["IssueDate"].ToString());
				}
				model.InfoTypeName=ds.Tables[0].Rows[0]["InfoTypeName"].ToString();
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select InfoId,OrganCode,DepCode,Title,Content,UserCode,IssueDate,InfoTypeName ");
			strSql.Append(" FROM Tb_Common_CommunityService ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string fieldOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" InfoId,OrganCode,DepCode,Title,Content,UserCode,IssueDate,InfoTypeName ");
			strSql.Append(" FROM Tb_Common_CommunityService ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + fieldOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize,string SortField,int Sort)
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
			parameters[5].Value = "SELECT * FROM Tb_Common_CommunityService WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

        public DataTable Common_CommunityService_TopFilter(int TopNum, string Sql)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@TopNum", SqlDbType.NVarChar,50),
                              new SqlParameter("@SQLEx", SqlDbType.VarChar,3999)
                                              };
            parameters[0].Value = TopNum;
            parameters[1].Value = Sql;

            DataTable result = DbHelperSQL.RunProcedure("Proc_Common_CommunityService_TopFilter", parameters, "RetDataSet").Tables[0];

            return result;
        }
	}
}

