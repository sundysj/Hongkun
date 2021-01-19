using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.System
{
	/// <summary>
	/// 数据访问类Dal_Tb_System_BusinessType。
	/// </summary>
	public class Dal_Tb_System_BusinessType
	{
		public Dal_Tb_System_BusinessType()
		{
			DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string BusinessTypeCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@BusinessTypeCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = BusinessTypeCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_System_BusinessType_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.System.Tb_System_BusinessType model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@BusinessTypeCode", SqlDbType.NVarChar,20),
					new SqlParameter("@BusinessCategory", SqlDbType.NVarChar,20),
					new SqlParameter("@BusinessTypeName", SqlDbType.NVarChar,30)};
			parameters[0].Value = model.BusinessTypeCode;
			parameters[1].Value = model.BusinessCategory;
			parameters[2].Value = model.BusinessTypeName;

			DbHelperSQL.RunProcedure("Proc_Tb_System_BusinessType_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_BusinessType model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@BusinessTypeCode", SqlDbType.NVarChar,20),
					new SqlParameter("@BusinessCategory", SqlDbType.NVarChar,20),
					new SqlParameter("@BusinessTypeName", SqlDbType.NVarChar,30)};
			parameters[0].Value = model.BusinessTypeCode;
			parameters[1].Value = model.BusinessCategory;
			parameters[2].Value = model.BusinessTypeName;

			DbHelperSQL.RunProcedure("Proc_Tb_System_BusinessType_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string BusinessTypeCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@BusinessTypeCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = BusinessTypeCode;

			DbHelperSQL.RunProcedure("Proc_Tb_System_BusinessType_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_BusinessType GetModel(string BusinessTypeCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@BusinessTypeCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = BusinessTypeCode;

			MobileSoft.Model.System.Tb_System_BusinessType model=new MobileSoft.Model.System.Tb_System_BusinessType();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_System_BusinessType_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.BusinessTypeCode=ds.Tables[0].Rows[0]["BusinessTypeCode"].ToString();
				model.BusinessCategory=ds.Tables[0].Rows[0]["BusinessCategory"].ToString();
				model.BusinessTypeName=ds.Tables[0].Rows[0]["BusinessTypeName"].ToString();
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
            strSql.Append("select * ");
			strSql.Append(" FROM Tb_System_BusinessType ");
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
			strSql.Append(" BusinessTypeCode,BusinessCategory,BusinessTypeName ");
			strSql.Append(" FROM Tb_System_BusinessType ");
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
			parameters[5].Value = "SELECT * FROM Tb_System_BusinessType WHERE 1=1 " + StrCondition;
			parameters[6].Value = "BusinessTypeCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

