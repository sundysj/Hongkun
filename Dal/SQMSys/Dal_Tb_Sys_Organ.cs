using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.SQMSys
{
	/// <summary>
	/// 数据访问类Dal_Tb_Sys_Organ。
	/// </summary>
	public class Dal_Tb_Sys_Organ
	{
		public Dal_Tb_Sys_Organ()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string OrganCode,Guid StreetCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,50),
					new SqlParameter("@StreetCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = OrganCode;
			parameters[1].Value = StreetCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_Organ_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_Organ model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@StreetCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@OrganName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@IsComp", SqlDbType.SmallInt,2),
					new SqlParameter("@Num", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.OrganCode;
			parameters[1].Value = model.StreetCode;
			parameters[2].Value = model.OrganName;
			parameters[3].Value = model.IsDelete;
			parameters[4].Value = model.IsComp;
			parameters[5].Value = model.Num;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Organ_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_Organ model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@StreetCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@OrganName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@IsComp", SqlDbType.SmallInt,2),
					new SqlParameter("@Num", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.OrganCode;
			parameters[1].Value = model.StreetCode;
			parameters[2].Value = model.OrganName;
			parameters[3].Value = model.IsDelete;
			parameters[4].Value = model.IsComp;
			parameters[5].Value = model.Num;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Organ_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string OrganCode,Guid StreetCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,50),
					new SqlParameter("@StreetCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = OrganCode;
			parameters[1].Value = StreetCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Organ_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Organ GetModel(string OrganCode,Guid StreetCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,50),
					new SqlParameter("@StreetCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = OrganCode;
			parameters[1].Value = StreetCode;

			MobileSoft.Model.SQMSys.Tb_Sys_Organ model=new MobileSoft.Model.SQMSys.Tb_Sys_Organ();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_Organ_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.OrganCode=ds.Tables[0].Rows[0]["OrganCode"].ToString();
				if(ds.Tables[0].Rows[0]["StreetCode"].ToString()!="")
				{
					model.StreetCode=new Guid(ds.Tables[0].Rows[0]["StreetCode"].ToString());
				}
				model.OrganName=ds.Tables[0].Rows[0]["OrganName"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsComp"].ToString()!="")
				{
					model.IsComp=int.Parse(ds.Tables[0].Rows[0]["IsComp"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Num"].ToString()!="")
				{
					model.Num=int.Parse(ds.Tables[0].Rows[0]["Num"].ToString());
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
			strSql.Append("select OrganCode,StreetCode,OrganName,IsDelete,IsComp,Num ");
			strSql.Append(" FROM Tb_Sys_Organ ");
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
			strSql.Append(" OrganCode,StreetCode,OrganName,IsDelete,IsComp,Num ");
			strSql.Append(" FROM Tb_Sys_Organ ");
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_Organ WHERE 1=1 " + StrCondition;
			parameters[6].Value = "OrganCode,StreetCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

