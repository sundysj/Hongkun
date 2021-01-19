using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Resources
{
	/// <summary>
	/// 数据访问类Dal_Tb_Resources_ReleaseVenuesSet。
	/// </summary>
	public class Dal_Tb_Resources_ReleaseVenuesSet
	{
		public Dal_Tb_Resources_ReleaseVenuesSet()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ReleaseVenuesSetID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesSetID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseVenuesSetID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenuesSet_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSet model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesSetID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseVenuesSetStartTime", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseVenuesSetEndTime", SqlDbType.NVarChar,50),
					new SqlParameter("@BussId", SqlDbType.BigInt,8)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.ReleaseVenuesSetStartTime;
			parameters[2].Value = model.ReleaseVenuesSetEndTime;
			parameters[3].Value = model.BussId;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenuesSet_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSet model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesSetID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseVenuesSetStartTime", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseVenuesSetEndTime", SqlDbType.NVarChar,50),
					new SqlParameter("@BussId", SqlDbType.BigInt,8)};
			parameters[0].Value = model.ReleaseVenuesSetID;
			parameters[1].Value = model.ReleaseVenuesSetStartTime;
			parameters[2].Value = model.ReleaseVenuesSetEndTime;
			parameters[3].Value = model.BussId;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenuesSet_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ReleaseVenuesSetID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesSetID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseVenuesSetID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenuesSet_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSet GetModel(long ReleaseVenuesSetID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesSetID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseVenuesSetID;

			MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSet model=new MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSet();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenuesSet_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ReleaseVenuesSetID"].ToString()!="")
				{
					model.ReleaseVenuesSetID=long.Parse(ds.Tables[0].Rows[0]["ReleaseVenuesSetID"].ToString());
				}
				model.ReleaseVenuesSetStartTime=ds.Tables[0].Rows[0]["ReleaseVenuesSetStartTime"].ToString();
				model.ReleaseVenuesSetEndTime=ds.Tables[0].Rows[0]["ReleaseVenuesSetEndTime"].ToString();
				if(ds.Tables[0].Rows[0]["BussId"].ToString()!="")
				{
					model.BussId=long.Parse(ds.Tables[0].Rows[0]["BussId"].ToString());
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
			strSql.Append("select ReleaseVenuesSetID,ReleaseVenuesSetStartTime,ReleaseVenuesSetEndTime,BussId ");
			strSql.Append(" FROM Tb_Resources_ReleaseVenuesSet ");
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
			strSql.Append(" ReleaseVenuesSetID,ReleaseVenuesSetStartTime,ReleaseVenuesSetEndTime,BussId ");
			strSql.Append(" FROM Tb_Resources_ReleaseVenuesSet ");
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
			parameters[5].Value = "SELECT * FROM Tb_Resources_ReleaseVenuesSet WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ReleaseVenuesSetID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

