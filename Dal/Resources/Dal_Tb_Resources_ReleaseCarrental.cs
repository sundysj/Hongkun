using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Resources
{
	/// <summary>
	/// 数据访问类Dal_Tb_Resources_ReleaseCarrental。
	/// </summary>
	public class Dal_Tb_Resources_ReleaseCarrental
	{
		public Dal_Tb_Resources_ReleaseCarrental()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ReleaseCarrentalID", "Tb_Resources_ReleaseCarrental"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ReleaseCarrentalID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseCarrentalID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseCarrentalID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseCarrental_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseCarrental model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseCarrentalID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseCarrentalBrand", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseCarrentalModels", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseCarrentalManCount", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseCarrentalContent", SqlDbType.NText),
					new SqlParameter("@ReleaseCarrentalNeedKnow", SqlDbType.NText),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.ReleaseCarrentalBrand;
			parameters[2].Value = model.ReleaseCarrentalModels;
			parameters[3].Value = model.ReleaseCarrentalManCount;
			parameters[4].Value = model.ReleaseCarrentalContent;
			parameters[5].Value = model.ReleaseCarrentalNeedKnow;
			parameters[6].Value = model.ReleaseID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseCarrental_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseCarrental model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseCarrentalID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseCarrentalBrand", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseCarrentalModels", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseCarrentalManCount", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseCarrentalContent", SqlDbType.NText),
					new SqlParameter("@ReleaseCarrentalNeedKnow", SqlDbType.NText),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.ReleaseCarrentalID;
			parameters[1].Value = model.ReleaseCarrentalBrand;
			parameters[2].Value = model.ReleaseCarrentalModels;
			parameters[3].Value = model.ReleaseCarrentalManCount;
			parameters[4].Value = model.ReleaseCarrentalContent;
			parameters[5].Value = model.ReleaseCarrentalNeedKnow;
			parameters[6].Value = model.ReleaseID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseCarrental_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ReleaseCarrentalID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseCarrentalID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseCarrentalID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseCarrental_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseCarrental GetModel(long ReleaseCarrentalID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseCarrentalID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseCarrentalID;

			MobileSoft.Model.Resources.Tb_Resources_ReleaseCarrental model=new MobileSoft.Model.Resources.Tb_Resources_ReleaseCarrental();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseCarrental_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ReleaseCarrentalID"].ToString()!="")
				{
					model.ReleaseCarrentalID=long.Parse(ds.Tables[0].Rows[0]["ReleaseCarrentalID"].ToString());
				}
				model.ReleaseCarrentalBrand=ds.Tables[0].Rows[0]["ReleaseCarrentalBrand"].ToString();
				model.ReleaseCarrentalModels=ds.Tables[0].Rows[0]["ReleaseCarrentalModels"].ToString();
				model.ReleaseCarrentalManCount=ds.Tables[0].Rows[0]["ReleaseCarrentalManCount"].ToString();
				model.ReleaseCarrentalContent=ds.Tables[0].Rows[0]["ReleaseCarrentalContent"].ToString();
				model.ReleaseCarrentalNeedKnow=ds.Tables[0].Rows[0]["ReleaseCarrentalNeedKnow"].ToString();
				if(ds.Tables[0].Rows[0]["ReleaseID"].ToString()!="")
				{
					model.ReleaseID=long.Parse(ds.Tables[0].Rows[0]["ReleaseID"].ToString());
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
			strSql.Append("select ReleaseCarrentalID,ReleaseCarrentalBrand,ReleaseCarrentalModels,ReleaseCarrentalManCount,ReleaseCarrentalContent,ReleaseCarrentalNeedKnow,ReleaseID ");
			strSql.Append(" FROM Tb_Resources_ReleaseCarrental ");
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
			strSql.Append(" ReleaseCarrentalID,ReleaseCarrentalBrand,ReleaseCarrentalModels,ReleaseCarrentalManCount,ReleaseCarrentalContent,ReleaseCarrentalNeedKnow,ReleaseID ");
			strSql.Append(" FROM Tb_Resources_ReleaseCarrental ");
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
			parameters[5].Value = "SELECT * FROM Tb_Resources_ReleaseCarrental WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ReleaseCarrentalID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

