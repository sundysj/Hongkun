using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.SQMSys
{
	/// <summary>
	/// 数据访问类Dal_Tb_Sys_UserEntry。
	/// </summary>
	public class Dal_Tb_Sys_UserEntry
	{
		public Dal_Tb_Sys_UserEntry()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string EntryCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@EntryCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = EntryCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_UserEntry_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_UserEntry model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@EntryCode", SqlDbType.NVarChar,20),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@EntryID", SqlDbType.BigInt,8),
					new SqlParameter("@EntryType", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.EntryCode;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.EntryID;
			parameters[3].Value = model.EntryType;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_UserEntry_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_UserEntry model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@EntryCode", SqlDbType.NVarChar,20),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@EntryID", SqlDbType.BigInt,8),
					new SqlParameter("@EntryType", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.EntryCode;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.EntryID;
			parameters[3].Value = model.EntryType;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_UserEntry_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string EntryCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@EntryCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = EntryCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_UserEntry_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_UserEntry GetModel(string EntryCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@EntryCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = EntryCode;

			MobileSoft.Model.SQMSys.Tb_Sys_UserEntry model=new MobileSoft.Model.SQMSys.Tb_Sys_UserEntry();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_UserEntry_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.EntryCode=ds.Tables[0].Rows[0]["EntryCode"].ToString();
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				if(ds.Tables[0].Rows[0]["EntryID"].ToString()!="")
				{
					model.EntryID=long.Parse(ds.Tables[0].Rows[0]["EntryID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EntryType"].ToString()!="")
				{
					model.EntryType=int.Parse(ds.Tables[0].Rows[0]["EntryType"].ToString());
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
			strSql.Append("select EntryCode,UserCode,EntryID,EntryType ");
			strSql.Append(" FROM Tb_Sys_UserEntry ");
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
			strSql.Append(" EntryCode,UserCode,EntryID,EntryType ");
			strSql.Append(" FROM Tb_Sys_UserEntry ");
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_UserEntry WHERE 1=1 " + StrCondition;
			parameters[6].Value = "EntryCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

