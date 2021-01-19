using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Resources
{
	/// <summary>
	/// 数据访问类Dal_Tb_Resources_Details。
	/// </summary>
	public class Dal_Tb_Resources_Details
	{
		public Dal_Tb_Resources_Details()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ResourcesID", "Tb_Resources_Details"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ResourcesID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ResourcesID", SqlDbType.BigInt)};
			parameters[0].Value = ResourcesID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Resources_Details_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.Resources.Tb_Resources_Details model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ResourcesID", SqlDbType.BigInt,8),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@ResourcesTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@ResourcesName", SqlDbType.NVarChar,100),
					new SqlParameter("@ResourcesSimple", SqlDbType.NVarChar,50),
					new SqlParameter("@ResourcesIndex", SqlDbType.SmallInt,2),
					new SqlParameter("@ResourcesBarCode", SqlDbType.NVarChar,100),
					new SqlParameter("@ResourcesCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ResourcesUnit", SqlDbType.NVarChar,20),
					new SqlParameter("@ResourcesCount", SqlDbType.Float,8),
					new SqlParameter("@ResourcesPriceUnit", SqlDbType.NVarChar,20),
					new SqlParameter("@ResourcesSalePrice", SqlDbType.Float,8),
					new SqlParameter("@ResourcesDisCountPrice", SqlDbType.Float,8),
					new SqlParameter("@IsRelease", SqlDbType.Bit,1),
					new SqlParameter("@ScheduleType", SqlDbType.NVarChar,20),
					new SqlParameter("@IsStopRelease", SqlDbType.Bit,1),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.BussId;
			parameters[2].Value = model.ResourcesTypeID;
			parameters[3].Value = model.ResourcesName;
			parameters[4].Value = model.ResourcesSimple;
			parameters[5].Value = model.ResourcesIndex;
			parameters[6].Value = model.ResourcesBarCode;
			parameters[7].Value = model.ResourcesCode;
			parameters[8].Value = model.ResourcesUnit;
			parameters[9].Value = model.ResourcesCount;
			parameters[10].Value = model.ResourcesPriceUnit;
			parameters[11].Value = model.ResourcesSalePrice;
			parameters[12].Value = model.ResourcesDisCountPrice;
			parameters[13].Value = model.IsRelease;
			parameters[14].Value = model.ScheduleType;
			parameters[15].Value = model.IsStopRelease;
			parameters[16].Value = model.Remark;
			parameters[17].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_Details_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_Details model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ResourcesID", SqlDbType.BigInt,8),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@ResourcesTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@ResourcesName", SqlDbType.NVarChar,100),
					new SqlParameter("@ResourcesSimple", SqlDbType.NVarChar,50),
					new SqlParameter("@ResourcesIndex", SqlDbType.SmallInt,2),
					new SqlParameter("@ResourcesBarCode", SqlDbType.NVarChar,100),
					new SqlParameter("@ResourcesCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ResourcesUnit", SqlDbType.NVarChar,20),
					new SqlParameter("@ResourcesCount", SqlDbType.Float,8),
					new SqlParameter("@ResourcesPriceUnit", SqlDbType.NVarChar,20),
					new SqlParameter("@ResourcesSalePrice", SqlDbType.Float,8),
					new SqlParameter("@ResourcesDisCountPrice", SqlDbType.Float,8),
					new SqlParameter("@IsRelease", SqlDbType.Bit,1),
					new SqlParameter("@ScheduleType", SqlDbType.NVarChar,20),
					new SqlParameter("@IsStopRelease", SqlDbType.Bit,1),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.ResourcesID;
			parameters[1].Value = model.BussId;
			parameters[2].Value = model.ResourcesTypeID;
			parameters[3].Value = model.ResourcesName;
			parameters[4].Value = model.ResourcesSimple;
			parameters[5].Value = model.ResourcesIndex;
			parameters[6].Value = model.ResourcesBarCode;
			parameters[7].Value = model.ResourcesCode;
			parameters[8].Value = model.ResourcesUnit;
			parameters[9].Value = model.ResourcesCount;
			parameters[10].Value = model.ResourcesPriceUnit;
			parameters[11].Value = model.ResourcesSalePrice;
			parameters[12].Value = model.ResourcesDisCountPrice;
			parameters[13].Value = model.IsRelease;
			parameters[14].Value = model.ScheduleType;
			parameters[15].Value = model.IsStopRelease;
			parameters[16].Value = model.Remark;
			parameters[17].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_Details_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ResourcesID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ResourcesID", SqlDbType.BigInt)};
			parameters[0].Value = ResourcesID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_Details_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_Details GetModel(long ResourcesID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ResourcesID", SqlDbType.BigInt)};
			parameters[0].Value = ResourcesID;

			MobileSoft.Model.Resources.Tb_Resources_Details model=new MobileSoft.Model.Resources.Tb_Resources_Details();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Resources_Details_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ResourcesID"].ToString()!="")
				{
					model.ResourcesID=long.Parse(ds.Tables[0].Rows[0]["ResourcesID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BussId"].ToString()!="")
				{
					model.BussId=long.Parse(ds.Tables[0].Rows[0]["BussId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ResourcesTypeID"].ToString()!="")
				{
					model.ResourcesTypeID=long.Parse(ds.Tables[0].Rows[0]["ResourcesTypeID"].ToString());
				}
				model.ResourcesName=ds.Tables[0].Rows[0]["ResourcesName"].ToString();
				model.ResourcesSimple=ds.Tables[0].Rows[0]["ResourcesSimple"].ToString();
				if(ds.Tables[0].Rows[0]["ResourcesIndex"].ToString()!="")
				{
					model.ResourcesIndex=int.Parse(ds.Tables[0].Rows[0]["ResourcesIndex"].ToString());
				}
				model.ResourcesBarCode=ds.Tables[0].Rows[0]["ResourcesBarCode"].ToString();
				model.ResourcesCode=ds.Tables[0].Rows[0]["ResourcesCode"].ToString();
				model.ResourcesUnit=ds.Tables[0].Rows[0]["ResourcesUnit"].ToString();
				if(ds.Tables[0].Rows[0]["ResourcesCount"].ToString()!="")
				{
					model.ResourcesCount=decimal.Parse(ds.Tables[0].Rows[0]["ResourcesCount"].ToString());
				}
				model.ResourcesPriceUnit=ds.Tables[0].Rows[0]["ResourcesPriceUnit"].ToString();
				if(ds.Tables[0].Rows[0]["ResourcesSalePrice"].ToString()!="")
				{
					model.ResourcesSalePrice=decimal.Parse(ds.Tables[0].Rows[0]["ResourcesSalePrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ResourcesDisCountPrice"].ToString()!="")
				{
					model.ResourcesDisCountPrice=decimal.Parse(ds.Tables[0].Rows[0]["ResourcesDisCountPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsRelease"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsRelease"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsRelease"].ToString().ToLower()=="true"))
					{
						model.IsRelease=true;
					}
					else
					{
						model.IsRelease=false;
					}
				}
				model.ScheduleType=ds.Tables[0].Rows[0]["ScheduleType"].ToString();
				if(ds.Tables[0].Rows[0]["IsStopRelease"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsStopRelease"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsStopRelease"].ToString().ToLower()=="true"))
					{
						model.IsStopRelease=true;
					}
					else
					{
						model.IsStopRelease=false;
					}
				}
				model.Remark=ds.Tables[0].Rows[0]["Remark"].ToString();
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
			strSql.Append("select ResourcesID,BussId,ResourcesTypeID,ResourcesName,ResourcesSimple,ResourcesIndex,ResourcesBarCode,ResourcesCode,ResourcesUnit,ResourcesCount,ResourcesPriceUnit,ResourcesSalePrice,ResourcesDisCountPrice,IsRelease,ScheduleType,IsStopRelease,Remark,IsDelete ");
			strSql.Append(" FROM Tb_Resources_Details ");
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
			strSql.Append(" ResourcesID,BussId,ResourcesTypeID,ResourcesName,ResourcesSimple,ResourcesIndex,ResourcesBarCode,ResourcesCode,ResourcesUnit,ResourcesCount,ResourcesPriceUnit,ResourcesSalePrice,ResourcesDisCountPrice,IsRelease,ScheduleType,IsStopRelease,Remark,IsDelete ");
			strSql.Append(" FROM Tb_Resources_Details ");
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
			parameters[5].Value = "SELECT * FROM Tb_Resources_Details WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ResourcesID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

