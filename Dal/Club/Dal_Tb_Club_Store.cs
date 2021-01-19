using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Club
{
	/// <summary>
	/// 数据访问类Dal_Tb_Club_Store。
	/// </summary>
	public class Dal_Tb_Club_Store
	{
		public Dal_Tb_Club_Store()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("InfoId", "Tb_Club_Store"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Club_Store_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.Club.Tb_Club_Store model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@WareHouseID", SqlDbType.BigInt,8),
					new SqlParameter("@MerchID", SqlDbType.BigInt,8),
					new SqlParameter("@Quantity", SqlDbType.Decimal,9)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.WareHouseID;
			parameters[2].Value = model.MerchID;
			parameters[3].Value = model.Quantity;

			DbHelperSQL.RunProcedure("Proc_Tb_Club_Store_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Club.Tb_Club_Store model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@WareHouseID", SqlDbType.BigInt,8),
					new SqlParameter("@MerchID", SqlDbType.BigInt,8),
					new SqlParameter("@Quantity", SqlDbType.Decimal,9)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.WareHouseID;
			parameters[2].Value = model.MerchID;
			parameters[3].Value = model.Quantity;

			DbHelperSQL.RunProcedure("Proc_Tb_Club_Store_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			DbHelperSQL.RunProcedure("Proc_Tb_Club_Store_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Club.Tb_Club_Store GetModel(int InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.Club.Tb_Club_Store model=new MobileSoft.Model.Club.Tb_Club_Store();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Club_Store_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoId"].ToString()!="")
				{
					model.InfoId=int.Parse(ds.Tables[0].Rows[0]["InfoId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WareHouseID"].ToString()!="")
				{
					model.WareHouseID=long.Parse(ds.Tables[0].Rows[0]["WareHouseID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MerchID"].ToString()!="")
				{
					model.MerchID=long.Parse(ds.Tables[0].Rows[0]["MerchID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Quantity"].ToString()!="")
				{
					model.Quantity=decimal.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString());
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
			strSql.Append("select * ");
            strSql.Append(" FROM View_Club_Store_Filter ");
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
			strSql.Append(" * ");
			strSql.Append(" FROM View_Club_Store_Filter ");
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
			parameters[5].Value = "SELECT * FROM Tb_Club_Store WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

