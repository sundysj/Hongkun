using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_CorpIncidentType。
	/// </summary>
	public class Dal_Tb_HSPR_CorpIncidentType
	{
		public Dal_Tb_HSPR_CorpIncidentType()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("CorpTypeID", "Tb_HSPR_CorpIncidentType"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long CorpTypeID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CorpTypeID", SqlDbType.BigInt)};
			parameters[0].Value = CorpTypeID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CorpIncidentType_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CorpIncidentType model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CorpTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@TypeCode", SqlDbType.NVarChar,40),
					new SqlParameter("@TypeName", SqlDbType.NVarChar,30),
					new SqlParameter("@DealLimit", SqlDbType.Int,4),
					new SqlParameter("@ReserveHint", SqlDbType.Int,4),
					new SqlParameter("@TypeMemo", SqlDbType.NVarChar,200),
					new SqlParameter("@IsTreeRoot", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@IncidentPlace", SqlDbType.NVarChar,20),
					new SqlParameter("@DealLimit2", SqlDbType.Int,4),
					new SqlParameter("@ClassID", SqlDbType.Int,4)};
			parameters[0].Value = model.CorpTypeID;
			parameters[1].Value = model.TypeCode;
			parameters[2].Value = model.TypeName;
			parameters[3].Value = model.DealLimit;
			parameters[4].Value = model.ReserveHint;
			parameters[5].Value = model.TypeMemo;
			parameters[6].Value = model.IsTreeRoot;
			parameters[7].Value = model.IsDelete;
			parameters[8].Value = model.IncidentPlace;
			parameters[9].Value = model.DealLimit2;
			parameters[10].Value = model.ClassID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CorpIncidentType_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CorpIncidentType model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CorpTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@TypeCode", SqlDbType.NVarChar,40),
					new SqlParameter("@TypeName", SqlDbType.NVarChar,30),
					new SqlParameter("@DealLimit", SqlDbType.Int,4),
					new SqlParameter("@ReserveHint", SqlDbType.Int,4),
					new SqlParameter("@TypeMemo", SqlDbType.NVarChar,200),
					new SqlParameter("@IsTreeRoot", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@IncidentPlace", SqlDbType.NVarChar,20),
					new SqlParameter("@DealLimit2", SqlDbType.Int,4),
					new SqlParameter("@ClassID", SqlDbType.Int,4)};
			parameters[0].Value = model.CorpTypeID;
			parameters[1].Value = model.TypeCode;
			parameters[2].Value = model.TypeName;
			parameters[3].Value = model.DealLimit;
			parameters[4].Value = model.ReserveHint;
			parameters[5].Value = model.TypeMemo;
			parameters[6].Value = model.IsTreeRoot;
			parameters[7].Value = model.IsDelete;
			parameters[8].Value = model.IncidentPlace;
			parameters[9].Value = model.DealLimit2;
			parameters[10].Value = model.ClassID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CorpIncidentType_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long CorpTypeID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CorpTypeID", SqlDbType.BigInt)};
			parameters[0].Value = CorpTypeID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CorpIncidentType_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CorpIncidentType GetModel(long CorpTypeID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@CorpTypeID", SqlDbType.BigInt)};
			parameters[0].Value = CorpTypeID;

			MobileSoft.Model.HSPR.Tb_HSPR_CorpIncidentType model=new MobileSoft.Model.HSPR.Tb_HSPR_CorpIncidentType();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CorpIncidentType_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CorpTypeID"].ToString()!="")
				{
					model.CorpTypeID=long.Parse(ds.Tables[0].Rows[0]["CorpTypeID"].ToString());
				}
				model.TypeCode=ds.Tables[0].Rows[0]["TypeCode"].ToString();
				model.TypeName=ds.Tables[0].Rows[0]["TypeName"].ToString();
				if(ds.Tables[0].Rows[0]["DealLimit"].ToString()!="")
				{
					model.DealLimit=int.Parse(ds.Tables[0].Rows[0]["DealLimit"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReserveHint"].ToString()!="")
				{
					model.ReserveHint=int.Parse(ds.Tables[0].Rows[0]["ReserveHint"].ToString());
				}
				model.TypeMemo=ds.Tables[0].Rows[0]["TypeMemo"].ToString();
				if(ds.Tables[0].Rows[0]["IsTreeRoot"].ToString()!="")
				{
					model.IsTreeRoot=int.Parse(ds.Tables[0].Rows[0]["IsTreeRoot"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				model.IncidentPlace=ds.Tables[0].Rows[0]["IncidentPlace"].ToString();
				if(ds.Tables[0].Rows[0]["DealLimit2"].ToString()!="")
				{
					model.DealLimit2=int.Parse(ds.Tables[0].Rows[0]["DealLimit2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ClassID"].ToString()!="")
				{
					model.ClassID=int.Parse(ds.Tables[0].Rows[0]["ClassID"].ToString());
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
			strSql.Append("select CorpTypeID,TypeCode,TypeName,DealLimit,ReserveHint,TypeMemo,IsTreeRoot,IsDelete,IncidentPlace,DealLimit2,ClassID ");
			strSql.Append(" FROM Tb_HSPR_CorpIncidentType ");
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
			strSql.Append(" CorpTypeID,TypeCode,TypeName,DealLimit,ReserveHint,TypeMemo,IsTreeRoot,IsDelete,IncidentPlace,DealLimit2,ClassID ");
			strSql.Append(" FROM Tb_HSPR_CorpIncidentType ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_CorpIncidentType WHERE 1=1 " + StrCondition;
			parameters[6].Value = "CorpTypeID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

