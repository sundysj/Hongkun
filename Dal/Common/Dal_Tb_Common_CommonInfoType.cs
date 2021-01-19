using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Common
{
	/// <summary>
	/// 数据访问类Dal_Tb_Common_CommonInfoType。
	/// </summary>
	public class Dal_Tb_Common_CommonInfoType
	{
		public Dal_Tb_Common_CommonInfoType()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法


		/// <summary>
		///  增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Common.Tb_Common_CommonInfoType model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@TypeCode", SqlDbType.NVarChar,20),
					new SqlParameter("@TypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Type", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.TypeCode;
			parameters[1].Value = model.TypeName;
			parameters[2].Value = model.OrganCode;
			parameters[3].Value = model.Type;

			DbHelperSQL.RunProcedure("Proc_Tb_Common_CommonInfoType_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Common.Tb_Common_CommonInfoType model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@TypeCode", SqlDbType.NVarChar,20),
					new SqlParameter("@TypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Type", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.TypeCode;
			parameters[1].Value = model.TypeName;
			parameters[2].Value = model.OrganCode;
			parameters[3].Value = model.Type;

			DbHelperSQL.RunProcedure("Proc_Tb_Common_CommonInfoType_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete()
		{
			int rowsAffected;
			SqlParameter[] parameters = {
};

			DbHelperSQL.RunProcedure("Proc_Tb_Common_CommonInfoType_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Common.Tb_Common_CommonInfoType GetModel()
		{
			SqlParameter[] parameters = {
};

			MobileSoft.Model.Common.Tb_Common_CommonInfoType model=new MobileSoft.Model.Common.Tb_Common_CommonInfoType();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Common_CommonInfoType_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.TypeCode=ds.Tables[0].Rows[0]["TypeCode"].ToString();
				model.TypeName=ds.Tables[0].Rows[0]["TypeName"].ToString();
				model.OrganCode=ds.Tables[0].Rows[0]["OrganCode"].ToString();
				model.Type=ds.Tables[0].Rows[0]["Type"].ToString();
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
			strSql.Append("select TypeCode,TypeName,OrganCode,Type ");
			strSql.Append(" FROM Tb_Common_CommonInfoType ");
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
			strSql.Append(" TypeCode,TypeName,OrganCode,Type ");
			strSql.Append(" FROM Tb_Common_CommonInfoType ");
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
			parameters[5].Value = "SELECT * FROM Tb_Common_CommonInfoType WHERE 1=1 " + StrCondition;
			parameters[6].Value = "";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

