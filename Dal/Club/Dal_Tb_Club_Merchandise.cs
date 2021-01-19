using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Club
{
	/// <summary>
	/// 数据访问类Dal_Tb_Club_Merchandise。
	/// </summary>
	public class Dal_Tb_Club_Merchandise
	{
		public Dal_Tb_Club_Merchandise()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("MerchID", "Tb_Club_Merchandise"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long MerchID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@MerchID", SqlDbType.BigInt)};
			parameters[0].Value = MerchID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Club_Merchandise_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.Club.Tb_Club_Merchandise model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@MerchID", SqlDbType.BigInt,8),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@MerchCode", SqlDbType.NVarChar,30),
					new SqlParameter("@MerchName", SqlDbType.NVarChar,50),
					new SqlParameter("@MerchTypeCode", SqlDbType.NVarChar,30),
					new SqlParameter("@MerchSpell", SqlDbType.NVarChar,20),
					new SqlParameter("@MerchDefaultPrice", SqlDbType.Decimal,9),
					new SqlParameter("@MerchCostPrice", SqlDbType.Decimal,9),
					new SqlParameter("@MerchStock", SqlDbType.Decimal,9),
					new SqlParameter("@MerchAlarmStock", SqlDbType.BigInt,8),
					new SqlParameter("@MerchDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@MerchMemo", SqlDbType.NVarChar,500),
					new SqlParameter("@SalesProp", SqlDbType.SmallInt,2),
					new SqlParameter("@CostingMethod", SqlDbType.SmallInt,2),
					new SqlParameter("@MerchUnit", SqlDbType.NVarChar,20),
					new SqlParameter("@IsAllowPurchase", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDiscount", SqlDbType.SmallInt,2),
					new SqlParameter("@PointsRedeem", SqlDbType.Decimal,9),
					new SqlParameter("@IsPointsRedeem", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@IsPackage", SqlDbType.SmallInt,2),
					new SqlParameter("@MerchandiseID", SqlDbType.BigInt,8),
					new SqlParameter("@Quantity", SqlDbType.Int,4),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@MerchCounts", SqlDbType.Decimal,9),
					new SqlParameter("@MerchState", SqlDbType.SmallInt,2),
					new SqlParameter("@IsSale", SqlDbType.SmallInt,2),
					new SqlParameter("@Standard", SqlDbType.NVarChar,50),
					new SqlParameter("@IsHelpSale", SqlDbType.Int,4),
					new SqlParameter("@GetMethod", SqlDbType.Int,4),
					new SqlParameter("@IsHelpSaleValue", SqlDbType.Decimal,9),
					new SqlParameter("@SupplierID", SqlDbType.BigInt,8),
					new SqlParameter("@OutMethod", SqlDbType.Int,4),
					new SqlParameter("@IsLease", SqlDbType.Int,4),
					new SqlParameter("@IsPurchase", SqlDbType.Int,4),
					new SqlParameter("@PurchasePrice", SqlDbType.Decimal,9),
					new SqlParameter("@UseMerchTypeCode", SqlDbType.NVarChar,30),
					new SqlParameter("@NoOutStore", SqlDbType.SmallInt,2),
					new SqlParameter("@ExpirationDate", SqlDbType.Int,4),
					new SqlParameter("@PriceCheckSort", SqlDbType.NVarChar,50),
					new SqlParameter("@PriceLimitStartDate", SqlDbType.NVarChar,50),
					new SqlParameter("@PriceLimitEndDate", SqlDbType.NVarChar,50),
					new SqlParameter("@Brand", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.MerchID;
			parameters[1].Value = model.OrganCode;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.MerchCode;
			parameters[4].Value = model.MerchName;
			parameters[5].Value = model.MerchTypeCode;
			parameters[6].Value = model.MerchSpell;
			parameters[7].Value = model.MerchDefaultPrice;
			parameters[8].Value = model.MerchCostPrice;
			parameters[9].Value = model.MerchStock;
			parameters[10].Value = model.MerchAlarmStock;
			parameters[11].Value = model.MerchDescription;
			parameters[12].Value = model.MerchMemo;
			parameters[13].Value = model.SalesProp;
			parameters[14].Value = model.CostingMethod;
			parameters[15].Value = model.MerchUnit;
			parameters[16].Value = model.IsAllowPurchase;
			parameters[17].Value = model.IsDiscount;
			parameters[18].Value = model.PointsRedeem;
			parameters[19].Value = model.IsPointsRedeem;
			parameters[20].Value = model.IsDelete;
			parameters[21].Value = model.IsPackage;
			parameters[22].Value = model.MerchandiseID;
			parameters[23].Value = model.Quantity;
			parameters[24].Value = model.CostID;
			parameters[25].Value = model.MerchCounts;
			parameters[26].Value = model.MerchState;
			parameters[27].Value = model.IsSale;
			parameters[28].Value = model.Standard;
			parameters[29].Value = model.IsHelpSale;
			parameters[30].Value = model.GetMethod;
			parameters[31].Value = model.IsHelpSaleValue;
			parameters[32].Value = model.SupplierID;
			parameters[33].Value = model.OutMethod;
			parameters[34].Value = model.IsLease;
			parameters[35].Value = model.IsPurchase;
			parameters[36].Value = model.PurchasePrice;
			parameters[37].Value = model.UseMerchTypeCode;
			parameters[38].Value = model.NoOutStore;
			parameters[39].Value = model.ExpirationDate;
			parameters[40].Value = model.PriceCheckSort;
			parameters[41].Value = model.PriceLimitStartDate;
			parameters[42].Value = model.PriceLimitEndDate;
			parameters[43].Value = model.Brand;

			DbHelperSQL.RunProcedure("Proc_Tb_Club_Merchandise_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Club.Tb_Club_Merchandise model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@MerchID", SqlDbType.BigInt,8),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@MerchCode", SqlDbType.NVarChar,30),
					new SqlParameter("@MerchName", SqlDbType.NVarChar,50),
					new SqlParameter("@MerchTypeCode", SqlDbType.NVarChar,30),
					new SqlParameter("@MerchSpell", SqlDbType.NVarChar,20),
					new SqlParameter("@MerchDefaultPrice", SqlDbType.Decimal,9),
					new SqlParameter("@MerchCostPrice", SqlDbType.Decimal,9),
					new SqlParameter("@MerchStock", SqlDbType.Decimal,9),
					new SqlParameter("@MerchAlarmStock", SqlDbType.BigInt,8),
					new SqlParameter("@MerchDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@MerchMemo", SqlDbType.NVarChar,500),
					new SqlParameter("@SalesProp", SqlDbType.SmallInt,2),
					new SqlParameter("@CostingMethod", SqlDbType.SmallInt,2),
					new SqlParameter("@MerchUnit", SqlDbType.NVarChar,20),
					new SqlParameter("@IsAllowPurchase", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDiscount", SqlDbType.SmallInt,2),
					new SqlParameter("@PointsRedeem", SqlDbType.Decimal,9),
					new SqlParameter("@IsPointsRedeem", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@IsPackage", SqlDbType.SmallInt,2),
					new SqlParameter("@MerchandiseID", SqlDbType.BigInt,8),
					new SqlParameter("@Quantity", SqlDbType.Int,4),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@MerchCounts", SqlDbType.Decimal,9),
					new SqlParameter("@MerchState", SqlDbType.SmallInt,2),
					new SqlParameter("@IsSale", SqlDbType.SmallInt,2),
					new SqlParameter("@Standard", SqlDbType.NVarChar,50),
					new SqlParameter("@IsHelpSale", SqlDbType.Int,4),
					new SqlParameter("@GetMethod", SqlDbType.Int,4),
					new SqlParameter("@IsHelpSaleValue", SqlDbType.Decimal,9),
					new SqlParameter("@SupplierID", SqlDbType.BigInt,8),
					new SqlParameter("@OutMethod", SqlDbType.Int,4),
					new SqlParameter("@IsLease", SqlDbType.Int,4),
					new SqlParameter("@IsPurchase", SqlDbType.Int,4),
					new SqlParameter("@PurchasePrice", SqlDbType.Decimal,9),
					new SqlParameter("@UseMerchTypeCode", SqlDbType.NVarChar,30),
					new SqlParameter("@NoOutStore", SqlDbType.SmallInt,2),
					new SqlParameter("@ExpirationDate", SqlDbType.Int,4),
					new SqlParameter("@PriceCheckSort", SqlDbType.NVarChar,50),
					new SqlParameter("@PriceLimitStartDate", SqlDbType.NVarChar,50),
					new SqlParameter("@PriceLimitEndDate", SqlDbType.NVarChar,50),
					new SqlParameter("@Brand", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.MerchID;
			parameters[1].Value = model.OrganCode;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.MerchCode;
			parameters[4].Value = model.MerchName;
			parameters[5].Value = model.MerchTypeCode;
			parameters[6].Value = model.MerchSpell;
			parameters[7].Value = model.MerchDefaultPrice;
			parameters[8].Value = model.MerchCostPrice;
			parameters[9].Value = model.MerchStock;
			parameters[10].Value = model.MerchAlarmStock;
			parameters[11].Value = model.MerchDescription;
			parameters[12].Value = model.MerchMemo;
			parameters[13].Value = model.SalesProp;
			parameters[14].Value = model.CostingMethod;
			parameters[15].Value = model.MerchUnit;
			parameters[16].Value = model.IsAllowPurchase;
			parameters[17].Value = model.IsDiscount;
			parameters[18].Value = model.PointsRedeem;
			parameters[19].Value = model.IsPointsRedeem;
			parameters[20].Value = model.IsDelete;
			parameters[21].Value = model.IsPackage;
			parameters[22].Value = model.MerchandiseID;
			parameters[23].Value = model.Quantity;
			parameters[24].Value = model.CostID;
			parameters[25].Value = model.MerchCounts;
			parameters[26].Value = model.MerchState;
			parameters[27].Value = model.IsSale;
			parameters[28].Value = model.Standard;
			parameters[29].Value = model.IsHelpSale;
			parameters[30].Value = model.GetMethod;
			parameters[31].Value = model.IsHelpSaleValue;
			parameters[32].Value = model.SupplierID;
			parameters[33].Value = model.OutMethod;
			parameters[34].Value = model.IsLease;
			parameters[35].Value = model.IsPurchase;
			parameters[36].Value = model.PurchasePrice;
			parameters[37].Value = model.UseMerchTypeCode;
			parameters[38].Value = model.NoOutStore;
			parameters[39].Value = model.ExpirationDate;
			parameters[40].Value = model.PriceCheckSort;
			parameters[41].Value = model.PriceLimitStartDate;
			parameters[42].Value = model.PriceLimitEndDate;
			parameters[43].Value = model.Brand;

			DbHelperSQL.RunProcedure("Proc_Tb_Club_Merchandise_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long MerchID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@MerchID", SqlDbType.BigInt)};
			parameters[0].Value = MerchID;

			DbHelperSQL.RunProcedure("Proc_Tb_Club_Merchandise_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Club.Tb_Club_Merchandise GetModel(long MerchID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@MerchID", SqlDbType.BigInt)};
			parameters[0].Value = MerchID;

			MobileSoft.Model.Club.Tb_Club_Merchandise model=new MobileSoft.Model.Club.Tb_Club_Merchandise();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Club_Merchandise_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["MerchID"].ToString()!="")
				{
					model.MerchID=long.Parse(ds.Tables[0].Rows[0]["MerchID"].ToString());
				}
				model.OrganCode=ds.Tables[0].Rows[0]["OrganCode"].ToString();
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				model.MerchCode=ds.Tables[0].Rows[0]["MerchCode"].ToString();
				model.MerchName=ds.Tables[0].Rows[0]["MerchName"].ToString();
				model.MerchTypeCode=ds.Tables[0].Rows[0]["MerchTypeCode"].ToString();
				model.MerchSpell=ds.Tables[0].Rows[0]["MerchSpell"].ToString();
				if(ds.Tables[0].Rows[0]["MerchDefaultPrice"].ToString()!="")
				{
					model.MerchDefaultPrice=decimal.Parse(ds.Tables[0].Rows[0]["MerchDefaultPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MerchCostPrice"].ToString()!="")
				{
					model.MerchCostPrice=decimal.Parse(ds.Tables[0].Rows[0]["MerchCostPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MerchStock"].ToString()!="")
				{
					model.MerchStock=decimal.Parse(ds.Tables[0].Rows[0]["MerchStock"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MerchAlarmStock"].ToString()!="")
				{
					model.MerchAlarmStock=long.Parse(ds.Tables[0].Rows[0]["MerchAlarmStock"].ToString());
				}
				model.MerchDescription=ds.Tables[0].Rows[0]["MerchDescription"].ToString();
				model.MerchMemo=ds.Tables[0].Rows[0]["MerchMemo"].ToString();
				if(ds.Tables[0].Rows[0]["SalesProp"].ToString()!="")
				{
					model.SalesProp=int.Parse(ds.Tables[0].Rows[0]["SalesProp"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CostingMethod"].ToString()!="")
				{
					model.CostingMethod=int.Parse(ds.Tables[0].Rows[0]["CostingMethod"].ToString());
				}
				model.MerchUnit=ds.Tables[0].Rows[0]["MerchUnit"].ToString();
				if(ds.Tables[0].Rows[0]["IsAllowPurchase"].ToString()!="")
				{
					model.IsAllowPurchase=int.Parse(ds.Tables[0].Rows[0]["IsAllowPurchase"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDiscount"].ToString()!="")
				{
					model.IsDiscount=int.Parse(ds.Tables[0].Rows[0]["IsDiscount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PointsRedeem"].ToString()!="")
				{
					model.PointsRedeem=decimal.Parse(ds.Tables[0].Rows[0]["PointsRedeem"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsPointsRedeem"].ToString()!="")
				{
					model.IsPointsRedeem=int.Parse(ds.Tables[0].Rows[0]["IsPointsRedeem"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
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
				if(ds.Tables[0].Rows[0]["MerchCounts"].ToString()!="")
				{
					model.MerchCounts=decimal.Parse(ds.Tables[0].Rows[0]["MerchCounts"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MerchState"].ToString()!="")
				{
					model.MerchState=int.Parse(ds.Tables[0].Rows[0]["MerchState"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsSale"].ToString()!="")
				{
					model.IsSale=int.Parse(ds.Tables[0].Rows[0]["IsSale"].ToString());
				}
				model.Standard=ds.Tables[0].Rows[0]["Standard"].ToString();
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
				if(ds.Tables[0].Rows[0]["SupplierID"].ToString()!="")
				{
					model.SupplierID=long.Parse(ds.Tables[0].Rows[0]["SupplierID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OutMethod"].ToString()!="")
				{
					model.OutMethod=int.Parse(ds.Tables[0].Rows[0]["OutMethod"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsLease"].ToString()!="")
				{
					model.IsLease=int.Parse(ds.Tables[0].Rows[0]["IsLease"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsPurchase"].ToString()!="")
				{
					model.IsPurchase=int.Parse(ds.Tables[0].Rows[0]["IsPurchase"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PurchasePrice"].ToString()!="")
				{
					model.PurchasePrice=decimal.Parse(ds.Tables[0].Rows[0]["PurchasePrice"].ToString());
				}
				model.UseMerchTypeCode=ds.Tables[0].Rows[0]["UseMerchTypeCode"].ToString();
				if(ds.Tables[0].Rows[0]["NoOutStore"].ToString()!="")
				{
					model.NoOutStore=int.Parse(ds.Tables[0].Rows[0]["NoOutStore"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ExpirationDate"].ToString()!="")
				{
					model.ExpirationDate=int.Parse(ds.Tables[0].Rows[0]["ExpirationDate"].ToString());
				}
				model.PriceCheckSort=ds.Tables[0].Rows[0]["PriceCheckSort"].ToString();
				model.PriceLimitStartDate=ds.Tables[0].Rows[0]["PriceLimitStartDate"].ToString();
				model.PriceLimitEndDate=ds.Tables[0].Rows[0]["PriceLimitEndDate"].ToString();
				model.Brand=ds.Tables[0].Rows[0]["Brand"].ToString();
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
			strSql.Append("select MerchID,OrganCode,CommID,MerchCode,MerchName,MerchTypeCode,MerchSpell,MerchDefaultPrice,MerchCostPrice,MerchStock,MerchAlarmStock,MerchDescription,MerchMemo,SalesProp,CostingMethod,MerchUnit,IsAllowPurchase,IsDiscount,PointsRedeem,IsPointsRedeem,IsDelete,IsPackage,MerchandiseID,Quantity,CostID,MerchCounts,MerchState,IsSale,Standard,IsHelpSale,GetMethod,IsHelpSaleValue,SupplierID,OutMethod,IsLease,IsPurchase,PurchasePrice,UseMerchTypeCode,NoOutStore,ExpirationDate,PriceCheckSort,PriceLimitStartDate,PriceLimitEndDate,Brand ");
			strSql.Append(" FROM Tb_Club_Merchandise ");
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
			strSql.Append(" MerchID,OrganCode,CommID,MerchCode,MerchName,MerchTypeCode,MerchSpell,MerchDefaultPrice,MerchCostPrice,MerchStock,MerchAlarmStock,MerchDescription,MerchMemo,SalesProp,CostingMethod,MerchUnit,IsAllowPurchase,IsDiscount,PointsRedeem,IsPointsRedeem,IsDelete,IsPackage,MerchandiseID,Quantity,CostID,MerchCounts,MerchState,IsSale,Standard,IsHelpSale,GetMethod,IsHelpSaleValue,SupplierID,OutMethod,IsLease,IsPurchase,PurchasePrice,UseMerchTypeCode,NoOutStore,ExpirationDate,PriceCheckSort,PriceLimitStartDate,PriceLimitEndDate,Brand ");
			strSql.Append(" FROM Tb_Club_Merchandise ");
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
			parameters[5].Value = "SELECT * FROM Tb_Club_Merchandise WHERE 1=1 " + StrCondition;
			parameters[6].Value = "MerchID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

