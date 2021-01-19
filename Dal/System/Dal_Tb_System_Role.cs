using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.System
{
	/// <summary>
	/// 数据访问类Dal_Tb_System_Role。
	/// </summary>
	public class Dal_Tb_System_Role
	{
		public Dal_Tb_System_Role()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法


		/// <summary>
		///  增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.System.Tb_System_Role model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RoleCode", SqlDbType.NVarChar,20),
					new SqlParameter("@RoleName", SqlDbType.NVarChar,50),
					new SqlParameter("@RoleDescribe", SqlDbType.NVarChar,1000)};
			parameters[0].Value = model.RoleCode;
			parameters[1].Value = model.RoleName;
			parameters[2].Value = model.RoleDescribe;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Role_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Role model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RoleCode", SqlDbType.NVarChar,20),
					new SqlParameter("@RoleName", SqlDbType.NVarChar,50),
					new SqlParameter("@RoleDescribe", SqlDbType.NVarChar,1000)};
			parameters[0].Value = model.RoleCode;
			parameters[1].Value = model.RoleName;
			parameters[2].Value = model.RoleDescribe;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Role_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete()
		{
			int rowsAffected;
			SqlParameter[] parameters = {
};

			DbHelperSQL.RunProcedure("Proc_Tb_System_Role_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Role GetModel()
		{
			SqlParameter[] parameters = {
};

			MobileSoft.Model.System.Tb_System_Role model=new MobileSoft.Model.System.Tb_System_Role();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_System_Role_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.RoleCode=ds.Tables[0].Rows[0]["RoleCode"].ToString();
				model.RoleName=ds.Tables[0].Rows[0]["RoleName"].ToString();
				model.RoleDescribe=ds.Tables[0].Rows[0]["RoleDescribe"].ToString();
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
			strSql.Append("select RoleCode,RoleName,RoleDescribe ");
			strSql.Append(" FROM Tb_System_Role ");
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
			strSql.Append(" RoleCode,RoleName,RoleDescribe ");
			strSql.Append(" FROM Tb_System_Role ");
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
			parameters[5].Value = "SELECT * FROM Tb_System_Role WHERE 1=1 " + StrCondition;
			parameters[6].Value = "";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

