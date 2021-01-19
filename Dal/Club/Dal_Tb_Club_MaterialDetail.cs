using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Club
{
	/// <summary>
	/// 数据访问类Dal_Tb_Club_MaterialDetail。
	/// </summary>
	public class Dal_Tb_Club_MaterialDetail
	{
		public Dal_Tb_Club_MaterialDetail()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("InfoId", "Tb_Club_MaterialDetail"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt)};
			parameters[0].Value = InfoId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Club_MaterialDetail_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.Club.Tb_Club_MaterialDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt,8),
					new SqlParameter("@MerchID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@MerchDefaultPrice", SqlDbType.Decimal,9),
					new SqlParameter("@MerchCostPrice", SqlDbType.Decimal,9),
					new SqlParameter("@MerchMemo", SqlDbType.NVarChar,500),
					new SqlParameter("@IsPackage", SqlDbType.SmallInt,2),
					new SqlParameter("@MerchandiseID", SqlDbType.BigInt,8),
					new SqlParameter("@Quantity", SqlDbType.Int,4),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@NoOutStore", SqlDbType.SmallInt,2),
					new SqlParameter("@IsHelpSale", SqlDbType.Int,4),
					new SqlParameter("@GetMethod", SqlDbType.Int,4),
					new SqlParameter("@IsHelpSaleValue", SqlDbType.Decimal,9),
					new SqlParameter("@IsLease", SqlDbType.Int,4),
					new SqlParameter("@IsPay", SqlDbType.Int,4),
					new SqlParameter("@PayMoneyValue", SqlDbType.Decimal,9),
					new SqlParameter("@IsDeleteDetail", SqlDbType.Int,4)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.MerchID;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.MerchDefaultPrice;
			parameters[4].Value = model.MerchCostPrice;
			parameters[5].Value = model.MerchMemo;
			parameters[6].Value = model.IsPackage;
			parameters[7].Value = model.MerchandiseID;
			parameters[8].Value = model.Quantity;
			parameters[9].Value = model.CostID;
			parameters[10].Value = model.NoOutStore;
			parameters[11].Value = model.IsHelpSale;
			parameters[12].Value = model.GetMethod;
			parameters[13].Value = model.IsHelpSaleValue;
			parameters[14].Value = model.IsLease;
			parameters[15].Value = model.IsPay;
			parameters[16].Value = model.PayMoneyValue;
			parameters[17].Value = model.IsDeleteDetail;

			DbHelperSQL.RunProcedure("Proc_Tb_Club_MaterialDetail_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Club.Tb_Club_MaterialDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt,8),
					new SqlParameter("@MerchID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@MerchDefaultPrice", SqlDbType.Decimal,9),
					new SqlParameter("@MerchCostPrice", SqlDbType.Decimal,9),
					new SqlParameter("@MerchMemo", SqlDbType.NVarChar,500),
					new SqlParameter("@IsPackage", SqlDbType.SmallInt,2),
					new SqlParameter("@MerchandiseID", SqlDbType.BigInt,8),
					new SqlParameter("@Quantity", SqlDbType.Int,4),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@NoOutStore", SqlDbType.SmallInt,2),
					new SqlParameter("@IsHelpSale", SqlDbType.Int,4),
					new SqlParameter("@GetMethod", SqlDbType.Int,4),
					new SqlParameter("@IsHelpSaleValue", SqlDbType.Decimal,9),
					new SqlParameter("@IsLease", SqlDbType.Int,4),
					new SqlParameter("@IsPay", SqlDbType.Int,4),
					new SqlParameter("@PayMoneyValue", SqlDbType.Decimal,9),
					new SqlParameter("@IsDeleteDetail", SqlDbType.Int,4)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.MerchID;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.MerchDefaultPrice;
			parameters[4].Value = model.MerchCostPrice;
			parameters[5].Value = model.MerchMemo;
			parameters[6].Value = model.IsPackage;
			parameters[7].Value = model.MerchandiseID;
			parameters[8].Value = model.Quantity;
			parameters[9].Value = model.CostID;
			parameters[10].Value = model.NoOutStore;
			parameters[11].Value = model.IsHelpSale;
			parameters[12].Value = model.GetMethod;
			parameters[13].Value = model.IsHelpSaleValue;
			parameters[14].Value = model.IsLease;
			parameters[15].Value = model.IsPay;
			parameters[16].Value = model.PayMoneyValue;
			parameters[17].Value = model.IsDeleteDetail;

			DbHelperSQL.RunProcedure("Proc_Tb_Club_MaterialDetail_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt)};
			parameters[0].Value = InfoId;

			DbHelperSQL.RunProcedure("Proc_Tb_Club_MaterialDetail_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Club.Tb_Club_MaterialDetail GetModel(long InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.Club.Tb_Club_MaterialDetail model=new MobileSoft.Model.Club.Tb_Club_MaterialDetail();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Club_MaterialDetail_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoId"].ToString()!="")
				{
					model.InfoId=long.Parse(ds.Tables[0].Rows[0]["InfoId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MerchID"].ToString()!="")
				{
					model.MerchID=long.Parse(ds.Tables[0].Rows[0]["MerchID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MerchDefaultPrice"].ToString()!="")
				{
					model.MerchDefaultPrice=decimal.Parse(ds.Tables[0].Rows[0]["MerchDefaultPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MerchCostPrice"].ToString()!="")
				{
					model.MerchCostPrice=decimal.Parse(ds.Tables[0].Rows[0]["MerchCostPrice"].ToString());
				}
				model.MerchMemo=ds.Tables[0].Rows[0]["MerchMemo"].ToString();
				if(ds.Tables[0].Rows[0]["IsPackage"].ToString()!="")
				{
					model.IsPackage=int.Parse(ds.Tables[0].Rows[0]["IsPackage"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MerchandiseID"].ToString()!="")
				{
					model.MerchandiseID=long.Parse(ds.Tables[0].Rows[0]["MerchandiseID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Quantity"].ToString()!="")
				{
					model.Quantity=int.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CostID"].ToString()!="")
				{
					model.CostID=long.Parse(ds.Tables[0].Rows[0]["CostID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NoOutStore"].ToString()!="")
				{
					model.NoOutStore=int.Parse(ds.Tables[0].Rows[0]["NoOutStore"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsHelpSale"].ToString()!="")
				{
					model.IsHelpSale=int.Parse(ds.Tables[0].Rows[0]["IsHelpSale"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GetMethod"].ToString()!="")
				{
					model.GetMethod=int.Parse(ds.Tables[0].Rows[0]["GetMethod"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsHelpSaleValue"].ToString()!="")
				{
					model.IsHelpSaleValue=decimal.Parse(ds.Tables[0].Rows[0]["IsHelpSaleValue"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsLease"].ToString()!="")
				{
					model.IsLease=int.Parse(ds.Tables[0].Rows[0]["IsLease"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsPay"].ToString()!="")
				{
					model.IsPay=int.Parse(ds.Tables[0].Rows[0]["IsPay"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PayMoneyValue"].ToString()!="")
				{
					model.PayMoneyValue=decimal.Parse(ds.Tables[0].Rows[0]["PayMoneyValue"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDeleteDetail"].ToString()!="")
				{
					model.IsDeleteDetail=int.Parse(ds.Tables[0].Rows[0]["IsDeleteDetail"].ToString());
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
			strSql.Append("select InfoId,MerchID,CommID,MerchDefaultPrice,MerchCostPrice,MerchMemo,IsPackage,MerchandiseID,Quantity,CostID,NoOutStore,IsHelpSale,GetMethod,IsHelpSaleValue,IsLease,IsPay,PayMoneyValue,IsDeleteDetail ");
			strSql.Append(" FROM Tb_Club_MaterialDetail ");
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
			strSql.Append(" InfoId,MerchID,CommID,MerchDefaultPrice,MerchCostPrice,MerchMemo,IsPackage,MerchandiseID,Quantity,CostID,NoOutStore,IsHelpSale,GetMethod,IsHelpSaleValue,IsLease,IsPay,PayMoneyValue,IsDeleteDetail ");
			strSql.Append(" FROM Tb_Club_MaterialDetail ");
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
			parameters[5].Value = "SELECT * FROM Tb_Club_MaterialDetail WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

