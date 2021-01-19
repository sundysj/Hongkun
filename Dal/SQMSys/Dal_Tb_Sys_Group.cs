using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.SQMSys
{
	/// <summary>
	/// 数据访问类Dal_Tb_Sys_Group。
	/// </summary>
	public class Dal_Tb_Sys_Group
	{
		public Dal_Tb_Sys_Group()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string GroupCode,Guid StreetCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@GroupCode", SqlDbType.NVarChar,50),
					new SqlParameter("@StreetCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = GroupCode;
			parameters[1].Value = StreetCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_Group_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_Group model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@GroupCode", SqlDbType.NVarChar,20),
					new SqlParameter("@StreetCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@GroupName", SqlDbType.NVarChar,40),
					new SqlParameter("@GroupDescribe", SqlDbType.NVarChar,1000),
					new SqlParameter("@GroupLead", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.GroupCode;
			parameters[1].Value = model.StreetCode;
			parameters[2].Value = model.GroupName;
			parameters[3].Value = model.GroupDescribe;
			parameters[4].Value = model.GroupLead;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Group_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_Group model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@GroupCode", SqlDbType.NVarChar,20),
					new SqlParameter("@StreetCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@GroupName", SqlDbType.NVarChar,40),
					new SqlParameter("@GroupDescribe", SqlDbType.NVarChar,1000),
					new SqlParameter("@GroupLead", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.GroupCode;
			parameters[1].Value = model.StreetCode;
			parameters[2].Value = model.GroupName;
			parameters[3].Value = model.GroupDescribe;
			parameters[4].Value = model.GroupLead;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Group_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string GroupCode,Guid StreetCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@GroupCode", SqlDbType.NVarChar,50),
					new SqlParameter("@StreetCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = GroupCode;
			parameters[1].Value = StreetCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Group_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Group GetModel(string GroupCode,Guid StreetCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@GroupCode", SqlDbType.NVarChar,50),
					new SqlParameter("@StreetCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = GroupCode;
			parameters[1].Value = StreetCode;

			MobileSoft.Model.SQMSys.Tb_Sys_Group model=new MobileSoft.Model.SQMSys.Tb_Sys_Group();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_Group_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.GroupCode=ds.Tables[0].Rows[0]["GroupCode"].ToString();
				if(ds.Tables[0].Rows[0]["StreetCode"].ToString()!="")
				{
					model.StreetCode=new Guid(ds.Tables[0].Rows[0]["StreetCode"].ToString());
				}
				model.GroupName=ds.Tables[0].Rows[0]["GroupName"].ToString();
				model.GroupDescribe=ds.Tables[0].Rows[0]["GroupDescribe"].ToString();
				model.GroupLead=ds.Tables[0].Rows[0]["GroupLead"].ToString();
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
			strSql.Append("select GroupCode,StreetCode,GroupName,GroupDescribe,GroupLead ");
			strSql.Append(" FROM Tb_Sys_Group ");
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
			strSql.Append(" GroupCode,StreetCode,GroupName,GroupDescribe,GroupLead ");
			strSql.Append(" FROM Tb_Sys_Group ");
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_Group WHERE 1=1 " + StrCondition;
			parameters[6].Value = "GroupCode,StreetCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

