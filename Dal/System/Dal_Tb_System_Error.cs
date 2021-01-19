using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.System
{
	/// <summary>
	/// 数据访问类Dal_Tb_System_Error。
	/// </summary>
	public class Dal_Tb_System_Error
	{
		public Dal_Tb_System_Error()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid ErrorCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ErrorCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = ErrorCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_System_Error_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.System.Tb_System_Error model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ErrorCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ErrorTime", SqlDbType.DateTime),
					new SqlParameter("@ErrorURL", SqlDbType.NText),
					new SqlParameter("@ErrorSource", SqlDbType.NText),
					new SqlParameter("@ErrorMessage", SqlDbType.NText)};
			parameters[0].Value = model.ErrorCode;
			parameters[1].Value = model.ErrorTime;
			parameters[2].Value = model.ErrorURL;
			parameters[3].Value = model.ErrorSource;
			parameters[4].Value = model.ErrorMessage;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Error_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Error model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ErrorCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ErrorTime", SqlDbType.DateTime),
					new SqlParameter("@ErrorURL", SqlDbType.NText),
					new SqlParameter("@ErrorSource", SqlDbType.NText),
					new SqlParameter("@ErrorMessage", SqlDbType.NText)};
			parameters[0].Value = model.ErrorCode;
			parameters[1].Value = model.ErrorTime;
			parameters[2].Value = model.ErrorURL;
			parameters[3].Value = model.ErrorSource;
			parameters[4].Value = model.ErrorMessage;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Error_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Guid ErrorCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ErrorCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = ErrorCode;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Error_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Error GetModel(Guid ErrorCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ErrorCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = ErrorCode;

			MobileSoft.Model.System.Tb_System_Error model=new MobileSoft.Model.System.Tb_System_Error();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_System_Error_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ErrorCode"].ToString()!="")
				{
					model.ErrorCode=new Guid(ds.Tables[0].Rows[0]["ErrorCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ErrorTime"].ToString()!="")
				{
					model.ErrorTime=DateTime.Parse(ds.Tables[0].Rows[0]["ErrorTime"].ToString());
				}
				model.ErrorURL=ds.Tables[0].Rows[0]["ErrorURL"].ToString();
				model.ErrorSource=ds.Tables[0].Rows[0]["ErrorSource"].ToString();
				model.ErrorMessage=ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
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
			strSql.Append("select ErrorCode,ErrorTime,ErrorURL,ErrorSource,ErrorMessage ");
			strSql.Append(" FROM Tb_System_Error ");
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
			strSql.Append(" ErrorCode,ErrorTime,ErrorURL,ErrorSource,ErrorMessage ");
			strSql.Append(" FROM Tb_System_Error ");
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
			parameters[5].Value = "SELECT * FROM Tb_System_Error WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ErrorCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

