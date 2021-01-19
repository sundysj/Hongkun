using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_CommunityCircle。
	/// </summary>
	public class Dal_Tb_HSPR_CommunityCircle
	{
		public Dal_Tb_HSPR_CommunityCircle()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string CircleID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CircleID", SqlDbType.NVarChar,50)};
			parameters[0].Value = CircleID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityCircle_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CommunityCircle model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CircleID", SqlDbType.NVarChar,50),
					new SqlParameter("@CircleName", SqlDbType.NVarChar,200),
					new SqlParameter("@CircleMemo", SqlDbType.NVarChar,500),
					new SqlParameter("@CircleIndex", SqlDbType.Int,4),
					new SqlParameter("@BussName", SqlDbType.NVarChar,200),
					new SqlParameter("@BussID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.BigInt,8),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.CircleID;
			parameters[1].Value = model.CircleName;
			parameters[2].Value = model.CircleMemo;
			parameters[3].Value = model.CircleIndex;
			parameters[4].Value = model.BussName;
			parameters[5].Value = model.BussID;
			parameters[6].Value = model.CommID;
			parameters[7].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityCircle_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CommunityCircle model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CircleID", SqlDbType.NVarChar,50),
					new SqlParameter("@CircleName", SqlDbType.NVarChar,200),
					new SqlParameter("@CircleMemo", SqlDbType.NVarChar,500),
					new SqlParameter("@CircleIndex", SqlDbType.Int,4),
					new SqlParameter("@BussName", SqlDbType.NVarChar,200),
					new SqlParameter("@BussID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.BigInt,8),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.CircleID;
			parameters[1].Value = model.CircleName;
			parameters[2].Value = model.CircleMemo;
			parameters[3].Value = model.CircleIndex;
			parameters[4].Value = model.BussName;
			parameters[5].Value = model.BussID;
			parameters[6].Value = model.CommID;
			parameters[7].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityCircle_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string CircleID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CircleID", SqlDbType.NVarChar,50)};
			parameters[0].Value = CircleID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityCircle_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommunityCircle GetModel(string CircleID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@CircleID", SqlDbType.NVarChar,50)};
			parameters[0].Value = CircleID;

			MobileSoft.Model.HSPR.Tb_HSPR_CommunityCircle model=new MobileSoft.Model.HSPR.Tb_HSPR_CommunityCircle();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityCircle_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.CircleID=ds.Tables[0].Rows[0]["CircleID"].ToString();
				model.CircleName=ds.Tables[0].Rows[0]["CircleName"].ToString();
				model.CircleMemo=ds.Tables[0].Rows[0]["CircleMemo"].ToString();
				if(ds.Tables[0].Rows[0]["CircleIndex"].ToString()!="")
				{
					model.CircleIndex=int.Parse(ds.Tables[0].Rows[0]["CircleIndex"].ToString());
				}
				model.BussName=ds.Tables[0].Rows[0]["BussName"].ToString();
				if(ds.Tables[0].Rows[0]["BussID"].ToString()!="")
				{
					model.BussID=long.Parse(ds.Tables[0].Rows[0]["BussID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=long.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
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
			strSql.Append("select CircleID,CircleName,CircleMemo,CircleIndex,BussName,BussID,CommID,IsDelete,UserCount=ISNULL((SELECT COUNT(*) FROM Tb_HSPR_CommSocialScope WHERE CircleID=A.CircleID),0)");
			strSql.Append(" FROM Tb_HSPR_CommunityCircle A");
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
			strSql.Append(" CircleID,CircleName,CircleMemo,CircleIndex,BussName,BussID,CommID,IsDelete ");
			strSql.Append(" FROM Tb_HSPR_CommunityCircle ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_CommunityCircle WHERE 1=1 " + StrCondition;
			parameters[6].Value = "CircleID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

