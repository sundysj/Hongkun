using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_Region。
	/// </summary>
	public class Dal_Tb_HSPR_Region
	{
		public Dal_Tb_HSPR_Region()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long RegionID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegionID", SqlDbType.BigInt)};
			parameters[0].Value = RegionID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Region_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_Region model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegionID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@RegionName", SqlDbType.NVarChar,20),
					new SqlParameter("@RegionSNum", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@RegionSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.RegionID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.RegionName;
			parameters[3].Value = model.RegionSNum;
			parameters[4].Value = model.IsDelete;
			parameters[5].Value = model.RegionSynchCode;
			parameters[6].Value = model.SynchFlag;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Region_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_Region model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegionID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@RegionName", SqlDbType.NVarChar,20),
					new SqlParameter("@RegionSNum", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@RegionSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.RegionID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.RegionName;
			parameters[3].Value = model.RegionSNum;
			parameters[4].Value = model.IsDelete;
			parameters[5].Value = model.RegionSynchCode;
			parameters[6].Value = model.SynchFlag;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Region_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long RegionID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegionID", SqlDbType.BigInt)};
			parameters[0].Value = RegionID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Region_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_Region GetModel(long RegionID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@RegionID", SqlDbType.BigInt)};
			parameters[0].Value = RegionID;

			MobileSoft.Model.HSPR.Tb_HSPR_Region model=new MobileSoft.Model.HSPR.Tb_HSPR_Region();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Region_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["RegionID"].ToString()!="")
				{
					model.RegionID=long.Parse(ds.Tables[0].Rows[0]["RegionID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				model.RegionName=ds.Tables[0].Rows[0]["RegionName"].ToString();
				if(ds.Tables[0].Rows[0]["RegionSNum"].ToString()!="")
				{
					model.RegionSNum=int.Parse(ds.Tables[0].Rows[0]["RegionSNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RegionSynchCode"].ToString()!="")
				{
					model.RegionSynchCode=new Guid(ds.Tables[0].Rows[0]["RegionSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SynchFlag"].ToString()!="")
				{
					model.SynchFlag=int.Parse(ds.Tables[0].Rows[0]["SynchFlag"].ToString());
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
			strSql.Append("select RegionID,CommID,RegionName,RegionSNum,IsDelete,RegionSynchCode,SynchFlag ");
			strSql.Append(" FROM Tb_HSPR_Region ");
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
			strSql.Append(" RegionID,CommID,RegionName,RegionSNum,IsDelete,RegionSynchCode,SynchFlag ");
			strSql.Append(" FROM Tb_HSPR_Region ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_Region WHERE 1=1 " + StrCondition;
			parameters[6].Value = "RegionID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

