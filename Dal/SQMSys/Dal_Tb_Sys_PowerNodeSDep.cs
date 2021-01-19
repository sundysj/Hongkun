using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.SQMSys
{
	/// <summary>
	/// 数据访问类Dal_Tb_Sys_PowerNodeSDep。
	/// </summary>
	public class Dal_Tb_Sys_PowerNodeSDep
	{
		public Dal_Tb_Sys_PowerNodeSDep()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long IID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt)};
			parameters[0].Value = IID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_PowerNodeSDep_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSDep model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt,8),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@DepCode", SqlDbType.NVarChar,20),
					new SqlParameter("@DepName", SqlDbType.NVarChar,50),
					new SqlParameter("@RoleCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Ck", SqlDbType.SmallInt,2)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.DepCode;
			parameters[3].Value = model.DepName;
			parameters[4].Value = model.RoleCode;
			parameters[5].Value = model.Ck;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_PowerNodeSDep_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSDep model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt,8),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@DepCode", SqlDbType.NVarChar,20),
					new SqlParameter("@DepName", SqlDbType.NVarChar,50),
					new SqlParameter("@RoleCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Ck", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.IID;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.DepCode;
			parameters[3].Value = model.DepName;
			parameters[4].Value = model.RoleCode;
			parameters[5].Value = model.Ck;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_PowerNodeSDep_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long IID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt)};
			parameters[0].Value = IID;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_PowerNodeSDep_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSDep GetModel(long IID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt)};
			parameters[0].Value = IID;

			MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSDep model=new MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSDep();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_PowerNodeSDep_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["IID"].ToString()!="")
				{
					model.IID=long.Parse(ds.Tables[0].Rows[0]["IID"].ToString());
				}
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				model.DepCode=ds.Tables[0].Rows[0]["DepCode"].ToString();
				model.DepName=ds.Tables[0].Rows[0]["DepName"].ToString();
				model.RoleCode=ds.Tables[0].Rows[0]["RoleCode"].ToString();
				if(ds.Tables[0].Rows[0]["Ck"].ToString()!="")
				{
					model.Ck=int.Parse(ds.Tables[0].Rows[0]["Ck"].ToString());
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
			strSql.Append("select IID,UserCode,DepCode,DepName,RoleCode,Ck ");
			strSql.Append(" FROM Tb_Sys_PowerNodeSDep ");
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
			strSql.Append(" IID,UserCode,DepCode,DepName,RoleCode,Ck ");
			strSql.Append(" FROM Tb_Sys_PowerNodeSDep ");
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_PowerNodeSDep WHERE 1=1 " + StrCondition;
			parameters[6].Value = "IID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

