using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_IncidentType。
	/// </summary>
	public class Dal_Tb_HSPR_IncidentType
	{
		public Dal_Tb_HSPR_IncidentType()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("TypeID", "Tb_HSPR_IncidentType"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long TypeID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@TypeID", SqlDbType.BigInt)};
			parameters[0].Value = TypeID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentType_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_IncidentType model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@TypeID", SqlDbType.BigInt,8),
					new SqlParameter("@CorpTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@TypeCode", SqlDbType.NVarChar,40),
					new SqlParameter("@TypeName", SqlDbType.NVarChar,30),
					new SqlParameter("@DealLimit", SqlDbType.Int,4),
					new SqlParameter("@ReserveHint", SqlDbType.Int,4),
					new SqlParameter("@TypeMemo", SqlDbType.NVarChar,200),
					new SqlParameter("@IsTreeRoot", SqlDbType.SmallInt,2),
					new SqlParameter("@IncidentPlace", SqlDbType.NVarChar,20),
					new SqlParameter("@DealLimit2", SqlDbType.Int,4),
					new SqlParameter("@ClassID", SqlDbType.Int,4)};
			parameters[0].Value = model.TypeID;
			parameters[1].Value = model.CorpTypeID;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.TypeCode;
			parameters[4].Value = model.TypeName;
			parameters[5].Value = model.DealLimit;
			parameters[6].Value = model.ReserveHint;
			parameters[7].Value = model.TypeMemo;
			parameters[8].Value = model.IsTreeRoot;
			parameters[9].Value = model.IncidentPlace;
			parameters[10].Value = model.DealLimit2;
			parameters[11].Value = model.ClassID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentType_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_IncidentType model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@TypeID", SqlDbType.BigInt,8),
					new SqlParameter("@CorpTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@TypeCode", SqlDbType.NVarChar,40),
					new SqlParameter("@TypeName", SqlDbType.NVarChar,30),
					new SqlParameter("@DealLimit", SqlDbType.Int,4),
					new SqlParameter("@ReserveHint", SqlDbType.Int,4),
					new SqlParameter("@TypeMemo", SqlDbType.NVarChar,200),
					new SqlParameter("@IsTreeRoot", SqlDbType.SmallInt,2),
					new SqlParameter("@IncidentPlace", SqlDbType.NVarChar,20),
					new SqlParameter("@DealLimit2", SqlDbType.Int,4),
					new SqlParameter("@ClassID", SqlDbType.Int,4)};
			parameters[0].Value = model.TypeID;
			parameters[1].Value = model.CorpTypeID;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.TypeCode;
			parameters[4].Value = model.TypeName;
			parameters[5].Value = model.DealLimit;
			parameters[6].Value = model.ReserveHint;
			parameters[7].Value = model.TypeMemo;
			parameters[8].Value = model.IsTreeRoot;
			parameters[9].Value = model.IncidentPlace;
			parameters[10].Value = model.DealLimit2;
			parameters[11].Value = model.ClassID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentType_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long TypeID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@TypeID", SqlDbType.BigInt)};
			parameters[0].Value = TypeID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentType_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_IncidentType GetModel(long TypeID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@TypeID", SqlDbType.BigInt)};
			parameters[0].Value = TypeID;

			MobileSoft.Model.HSPR.Tb_HSPR_IncidentType model=new MobileSoft.Model.HSPR.Tb_HSPR_IncidentType();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentType_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["TypeID"].ToString()!="")
				{
					model.TypeID=long.Parse(ds.Tables[0].Rows[0]["TypeID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CorpTypeID"].ToString()!="")
				{
					model.CorpTypeID=long.Parse(ds.Tables[0].Rows[0]["CorpTypeID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
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
			strSql.Append("select TypeID,CorpTypeID,CommID,TypeCode,TypeName,DealLimit,ReserveHint,TypeMemo,IsTreeRoot,IncidentPlace,DealLimit2,ClassID ");
			strSql.Append(" FROM Tb_HSPR_IncidentType ");
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
			strSql.Append(" TypeID,CorpTypeID,CommID,TypeCode,TypeName,DealLimit,ReserveHint,TypeMemo,IsTreeRoot,IncidentPlace,DealLimit2,ClassID ");
			strSql.Append(" FROM Tb_HSPR_IncidentType ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_IncidentType WHERE 1=1 " + StrCondition;
			parameters[6].Value = "TypeID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

