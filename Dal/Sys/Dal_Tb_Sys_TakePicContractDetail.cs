using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Sys
{
	/// <summary>
	/// 数据访问类Dal_Tb_Sys_TakePicContractDetail。
	/// </summary>
	public class Dal_Tb_Sys_TakePicContractDetail
	{
		public Dal_Tb_Sys_TakePicContractDetail()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long DetailID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DetailID", SqlDbType.BigInt)};
			parameters[0].Value = DetailID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicContractDetail_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.Sys.Tb_Sys_TakePicContractDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DetailID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ContTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@ContSign", SqlDbType.NVarChar,20),
					new SqlParameter("@ContName", SqlDbType.NVarChar,50),
					new SqlParameter("@CustName", SqlDbType.NVarChar,50),
					new SqlParameter("@ContEndDate", SqlDbType.DateTime)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.OrganCode;
			parameters[3].Value = model.ContTypeName;
			parameters[4].Value = model.ContSign;
			parameters[5].Value = model.ContName;
			parameters[6].Value = model.CustName;
			parameters[7].Value = model.ContEndDate;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicContractDetail_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_TakePicContractDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DetailID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ContTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@ContSign", SqlDbType.NVarChar,20),
					new SqlParameter("@ContName", SqlDbType.NVarChar,50),
					new SqlParameter("@CustName", SqlDbType.NVarChar,50),
					new SqlParameter("@ContEndDate", SqlDbType.DateTime)};
			parameters[0].Value = model.DetailID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.OrganCode;
			parameters[3].Value = model.ContTypeName;
			parameters[4].Value = model.ContSign;
			parameters[5].Value = model.ContName;
			parameters[6].Value = model.CustName;
			parameters[7].Value = model.ContEndDate;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicContractDetail_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long DetailID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DetailID", SqlDbType.BigInt)};
			parameters[0].Value = DetailID;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicContractDetail_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePicContractDetail GetModel(long DetailID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@DetailID", SqlDbType.BigInt)};
			parameters[0].Value = DetailID;

			MobileSoft.Model.Sys.Tb_Sys_TakePicContractDetail model=new MobileSoft.Model.Sys.Tb_Sys_TakePicContractDetail();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicContractDetail_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["DetailID"].ToString()!="")
				{
					model.DetailID=long.Parse(ds.Tables[0].Rows[0]["DetailID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				model.OrganCode=ds.Tables[0].Rows[0]["OrganCode"].ToString();
				model.ContTypeName=ds.Tables[0].Rows[0]["ContTypeName"].ToString();
				model.ContSign=ds.Tables[0].Rows[0]["ContSign"].ToString();
				model.ContName=ds.Tables[0].Rows[0]["ContName"].ToString();
				model.CustName=ds.Tables[0].Rows[0]["CustName"].ToString();
				if(ds.Tables[0].Rows[0]["ContEndDate"].ToString()!="")
				{
					model.ContEndDate=DateTime.Parse(ds.Tables[0].Rows[0]["ContEndDate"].ToString());
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
			strSql.Append("select DetailID,CommID,OrganCode,ContTypeName,ContSign,ContName,CustName,ContEndDate ");
			strSql.Append(" FROM Tb_Sys_TakePicContractDetail ");
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
			strSql.Append(" DetailID,CommID,OrganCode,ContTypeName,ContSign,ContName,CustName,ContEndDate ");
			strSql.Append(" FROM Tb_Sys_TakePicContractDetail ");
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_TakePicContractDetail WHERE 1=1 " + StrCondition;
			parameters[6].Value = "DetailID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

