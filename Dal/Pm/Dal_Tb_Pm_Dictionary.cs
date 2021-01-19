using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace Mobile.DAL.Pm
{
	/// <summary>
	/// 数据访问类Dal_Tb_Pm_Dictionary。
	/// </summary>
	public class Dal_Tb_Pm_Dictionary
	{
		public Dal_Tb_Pm_Dictionary()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Pm_Dictionary_Exists",parameters,out rowsAffected);
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
		public void Add(Mobile.Model.Pm.Tb_Pm_Dictionary model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@DType", SqlDbType.VarChar,36),
					new SqlParameter("@Name", SqlDbType.VarChar,36),
					new SqlParameter("@Code", SqlDbType.VarChar,36),
					new SqlParameter("@Sort", SqlDbType.VarChar,36),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.DType;
			parameters[2].Value = model.Name;
			parameters[3].Value = model.Code;
			parameters[4].Value = model.Sort;
			parameters[5].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Pm_Dictionary_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(Mobile.Model.Pm.Tb_Pm_Dictionary model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@DType", SqlDbType.VarChar,36),
					new SqlParameter("@Name", SqlDbType.VarChar,36),
					new SqlParameter("@Code", SqlDbType.VarChar,36),
					new SqlParameter("@Sort", SqlDbType.VarChar,36),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.DType;
			parameters[2].Value = model.Name;
			parameters[3].Value = model.Code;
			parameters[4].Value = model.Sort;
			parameters[5].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Pm_Dictionary_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_Pm_Dictionary_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Mobile.Model.Pm.Tb_Pm_Dictionary GetModel(string Id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			Mobile.Model.Pm.Tb_Pm_Dictionary model=new Mobile.Model.Pm.Tb_Pm_Dictionary();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Pm_Dictionary_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.Id=ds.Tables[0].Rows[0]["Id"].ToString();
				model.DType=ds.Tables[0].Rows[0]["DType"].ToString();
				model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
				model.Code=ds.Tables[0].Rows[0]["Code"].ToString();
				model.Sort=ds.Tables[0].Rows[0]["Sort"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,DType,Name,Code,Sort,IsDelete ");
			strSql.Append(" FROM Tb_Pm_Dictionary ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Id,DType,Name,Code,Sort,IsDelete ");
			strSql.Append(" FROM Tb_Pm_Dictionary ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
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
			parameters[5].Value = "SELECT * FROM Tb_Pm_Dictionary WHERE 1=1 " + StrCondition;
			parameters[6].Value = "Id";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

