using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Club
{
	/// <summary>
	/// 数据访问类Dal_Tb_Club_Warehouse。
	/// </summary>
	public class Dal_Tb_Club_Warehouse
	{
		public Dal_Tb_Club_Warehouse()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("WareHouseID", "Tb_Club_Warehouse"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long WareHouseID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@WareHouseID", SqlDbType.BigInt)};
			parameters[0].Value = WareHouseID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Club_Warehouse_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.Club.Tb_Club_Warehouse model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@WareHouseID", SqlDbType.BigInt,8),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@WareHouseCode", SqlDbType.NVarChar,20),
					new SqlParameter("@WareHouseName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDefault", SqlDbType.Int,4),
					new SqlParameter("@IsOrgan", SqlDbType.Int,4)};
			parameters[0].Value = model.WareHouseID;
			parameters[1].Value = model.OrganCode;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.WareHouseCode;
			parameters[4].Value = model.WareHouseName;
			parameters[5].Value = model.IsDelete;
			parameters[6].Value = model.IsDefault;
			parameters[7].Value = model.IsOrgan;

			DbHelperSQL.RunProcedure("Proc_Tb_Club_Warehouse_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Club.Tb_Club_Warehouse model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@WareHouseID", SqlDbType.BigInt,8),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@WareHouseCode", SqlDbType.NVarChar,20),
					new SqlParameter("@WareHouseName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDefault", SqlDbType.Int,4),
					new SqlParameter("@IsOrgan", SqlDbType.Int,4)};
			parameters[0].Value = model.WareHouseID;
			parameters[1].Value = model.OrganCode;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.WareHouseCode;
			parameters[4].Value = model.WareHouseName;
			parameters[5].Value = model.IsDelete;
			parameters[6].Value = model.IsDefault;
			parameters[7].Value = model.IsOrgan;

			DbHelperSQL.RunProcedure("Proc_Tb_Club_Warehouse_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long WareHouseID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@WareHouseID", SqlDbType.BigInt)};
			parameters[0].Value = WareHouseID;

			DbHelperSQL.RunProcedure("Proc_Tb_Club_Warehouse_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Club.Tb_Club_Warehouse GetModel(long WareHouseID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@WareHouseID", SqlDbType.BigInt)};
			parameters[0].Value = WareHouseID;

			MobileSoft.Model.Club.Tb_Club_Warehouse model=new MobileSoft.Model.Club.Tb_Club_Warehouse();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Club_Warehouse_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["WareHouseID"].ToString()!="")
				{
					model.WareHouseID=long.Parse(ds.Tables[0].Rows[0]["WareHouseID"].ToString());
				}
				model.OrganCode=ds.Tables[0].Rows[0]["OrganCode"].ToString();
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				model.WareHouseCode=ds.Tables[0].Rows[0]["WareHouseCode"].ToString();
				model.WareHouseName=ds.Tables[0].Rows[0]["WareHouseName"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDefault"].ToString()!="")
				{
					model.IsDefault=int.Parse(ds.Tables[0].Rows[0]["IsDefault"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsOrgan"].ToString()!="")
				{
					model.IsOrgan=int.Parse(ds.Tables[0].Rows[0]["IsOrgan"].ToString());
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
			strSql.Append("select WareHouseID,OrganCode,CommID,WareHouseCode,WareHouseName,IsDelete,IsDefault,IsOrgan ");
			strSql.Append(" FROM Tb_Club_Warehouse ");
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
			strSql.Append(" WareHouseID,OrganCode,CommID,WareHouseCode,WareHouseName,IsDelete,IsDefault,IsOrgan ");
			strSql.Append(" FROM Tb_Club_Warehouse ");
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
			parameters[5].Value = "SELECT * FROM Tb_Club_Warehouse WHERE 1=1 " + StrCondition;
			parameters[6].Value = "WareHouseID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

