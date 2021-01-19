using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_ParkingOpt。
	/// </summary>
	public class Dal_Tb_HSPR_ParkingOpt
	{
		public Dal_Tb_HSPR_ParkingOpt()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid SCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@SCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = SCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingOpt_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_ParkingOpt model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@SCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@OptUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@OptCode", SqlDbType.UniqueIdentifier,16)};
			parameters[0].Value = model.SCode;
			parameters[1].Value = model.ParkID;
			parameters[2].Value = model.OptUserCode;
			parameters[3].Value = model.OptCode;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingOpt_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_ParkingOpt model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@SCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@OptUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@OptCode", SqlDbType.UniqueIdentifier,16)};
			parameters[0].Value = model.SCode;
			parameters[1].Value = model.ParkID;
			parameters[2].Value = model.OptUserCode;
			parameters[3].Value = model.OptCode;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingOpt_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Guid SCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@SCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = SCode;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingOpt_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_ParkingOpt GetModel(Guid SCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@SCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = SCode;

			MobileSoft.Model.HSPR.Tb_HSPR_ParkingOpt model=new MobileSoft.Model.HSPR.Tb_HSPR_ParkingOpt();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingOpt_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["SCode"].ToString()!="")
				{
					model.SCode=new Guid(ds.Tables[0].Rows[0]["SCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParkID"].ToString()!="")
				{
					model.ParkID=long.Parse(ds.Tables[0].Rows[0]["ParkID"].ToString());
				}
				model.OptUserCode=ds.Tables[0].Rows[0]["OptUserCode"].ToString();
				if(ds.Tables[0].Rows[0]["OptCode"].ToString()!="")
				{
					model.OptCode=new Guid(ds.Tables[0].Rows[0]["OptCode"].ToString());
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
			strSql.Append("select SCode,ParkID,OptUserCode,OptCode ");
			strSql.Append(" FROM Tb_HSPR_ParkingOpt ");
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
			strSql.Append(" SCode,ParkID,OptUserCode,OptCode ");
			strSql.Append(" FROM Tb_HSPR_ParkingOpt ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_ParkingOpt WHERE 1=1 " + StrCondition;
			parameters[6].Value = "SCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

