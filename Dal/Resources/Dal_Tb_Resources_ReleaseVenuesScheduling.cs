using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Resources
{
	/// <summary>
	/// 数据访问类Dal_Tb_Resources_ReleaseVenuesScheduling。
	/// </summary>
	public class Dal_Tb_Resources_ReleaseVenuesScheduling
	{
		public Dal_Tb_Resources_ReleaseVenuesScheduling()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ReleaseVenuesSchedulingID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesSchedulingID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseVenuesSchedulingID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenuesScheduling_Exists",parameters,out rowsAffected);
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
        public long Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesScheduling model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesSchedulingID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseVenuesSchedulingStartDate", SqlDbType.DateTime),
					new SqlParameter("@ReleaseVenuesSchedulingEndDate", SqlDbType.DateTime),
					new SqlParameter("@ReleaseVenuesSchedulingCount", SqlDbType.Float,8),
					new SqlParameter("@ReleaseVenuesID", SqlDbType.BigInt,8)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.ReleaseVenuesSchedulingStartDate;
			parameters[2].Value = model.ReleaseVenuesSchedulingEndDate;
			parameters[3].Value = model.ReleaseVenuesSchedulingCount;
			parameters[4].Value = model.ReleaseVenuesID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenuesScheduling_ADD",parameters,out rowsAffected);
            return (long)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesScheduling model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesSchedulingID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseVenuesSchedulingStartDate", SqlDbType.DateTime),
					new SqlParameter("@ReleaseVenuesSchedulingEndDate", SqlDbType.DateTime),
					new SqlParameter("@ReleaseVenuesSchedulingCount", SqlDbType.Float,8),
					new SqlParameter("@ReleaseVenuesID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.ReleaseVenuesSchedulingID;
			parameters[1].Value = model.ReleaseVenuesSchedulingStartDate;
			parameters[2].Value = model.ReleaseVenuesSchedulingEndDate;
			parameters[3].Value = model.ReleaseVenuesSchedulingCount;
			parameters[4].Value = model.ReleaseVenuesID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenuesScheduling_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ReleaseVenuesSchedulingID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesSchedulingID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseVenuesSchedulingID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenuesScheduling_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesScheduling GetModel(long ReleaseVenuesSchedulingID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesSchedulingID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseVenuesSchedulingID;

			MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesScheduling model=new MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesScheduling();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenuesScheduling_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ReleaseVenuesSchedulingID"].ToString()!="")
				{
					model.ReleaseVenuesSchedulingID=long.Parse(ds.Tables[0].Rows[0]["ReleaseVenuesSchedulingID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReleaseVenuesSchedulingStartDate"].ToString()!="")
				{
					model.ReleaseVenuesSchedulingStartDate=DateTime.Parse(ds.Tables[0].Rows[0]["ReleaseVenuesSchedulingStartDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReleaseVenuesSchedulingEndDate"].ToString()!="")
				{
					model.ReleaseVenuesSchedulingEndDate=DateTime.Parse(ds.Tables[0].Rows[0]["ReleaseVenuesSchedulingEndDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReleaseVenuesSchedulingCount"].ToString()!="")
				{
					model.ReleaseVenuesSchedulingCount=decimal.Parse(ds.Tables[0].Rows[0]["ReleaseVenuesSchedulingCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReleaseVenuesID"].ToString()!="")
				{
					model.ReleaseVenuesID=long.Parse(ds.Tables[0].Rows[0]["ReleaseVenuesID"].ToString());
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
			strSql.Append("select ReleaseVenuesSchedulingID,ReleaseVenuesSchedulingStartDate,ReleaseVenuesSchedulingEndDate,ReleaseVenuesSchedulingCount,ReleaseVenuesID ");
			strSql.Append(" FROM Tb_Resources_ReleaseVenuesScheduling ");
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
			strSql.Append(" ReleaseVenuesSchedulingID,ReleaseVenuesSchedulingStartDate,ReleaseVenuesSchedulingEndDate,ReleaseVenuesSchedulingCount,ReleaseVenuesID ");
			strSql.Append(" FROM Tb_Resources_ReleaseVenuesScheduling ");
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
			parameters[5].Value = "SELECT * FROM Tb_Resources_ReleaseVenuesScheduling WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ReleaseVenuesSchedulingID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

