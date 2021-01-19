using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Club;
namespace MobileSoft.BLL.Club
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Club_Merchandise 的摘要说明。
	/// </summary>
	public class Bll_Tb_Club_Merchandise
	{
		private readonly MobileSoft.DAL.Club.Dal_Tb_Club_Merchandise dal=new MobileSoft.DAL.Club.Dal_Tb_Club_Merchandise();
		public Bll_Tb_Club_Merchandise()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long MerchID)
		{
			return dal.Exists(MerchID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Club.Tb_Club_Merchandise model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Club.Tb_Club_Merchandise model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long MerchID)
		{
			
			dal.Delete(MerchID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Club.Tb_Club_Merchandise GetModel(long MerchID)
		{
			
			return dal.GetModel(MerchID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Club.Tb_Club_Merchandise GetModelByCache(long MerchID)
		{
			
			string CacheKey = "Tb_Club_MerchandiseModel-" + MerchID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(MerchID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Club.Tb_Club_Merchandise)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string fieldOrder)
		{
			return dal.GetList(Top,strWhere,fieldOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Club.Tb_Club_Merchandise> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Club.Tb_Club_Merchandise> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Club.Tb_Club_Merchandise> modelList = new List<MobileSoft.Model.Club.Tb_Club_Merchandise>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Club.Tb_Club_Merchandise model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Club.Tb_Club_Merchandise();
					//model.MerchID=dt.Rows[n]["MerchID"].ToString();
					model.OrganCode=dt.Rows[n]["OrganCode"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.MerchCode=dt.Rows[n]["MerchCode"].ToString();
					model.MerchName=dt.Rows[n]["MerchName"].ToString();
					model.MerchTypeCode=dt.Rows[n]["MerchTypeCode"].ToString();
					model.MerchSpell=dt.Rows[n]["MerchSpell"].ToString();
					if(dt.Rows[n]["MerchDefaultPrice"].ToString()!="")
					{
						model.MerchDefaultPrice=decimal.Parse(dt.Rows[n]["MerchDefaultPrice"].ToString());
					}
					if(dt.Rows[n]["MerchCostPrice"].ToString()!="")
					{
						model.MerchCostPrice=decimal.Parse(dt.Rows[n]["MerchCostPrice"].ToString());
					}
					if(dt.Rows[n]["MerchStock"].ToString()!="")
					{
						model.MerchStock=decimal.Parse(dt.Rows[n]["MerchStock"].ToString());
					}
					//model.MerchAlarmStock=dt.Rows[n]["MerchAlarmStock"].ToString();
					model.MerchDescription=dt.Rows[n]["MerchDescription"].ToString();
					model.MerchMemo=dt.Rows[n]["MerchMemo"].ToString();
					if(dt.Rows[n]["SalesProp"].ToString()!="")
					{
						model.SalesProp=int.Parse(dt.Rows[n]["SalesProp"].ToString());
					}
					if(dt.Rows[n]["CostingMethod"].ToString()!="")
					{
						model.CostingMethod=int.Parse(dt.Rows[n]["CostingMethod"].ToString());
					}
					model.MerchUnit=dt.Rows[n]["MerchUnit"].ToString();
					if(dt.Rows[n]["IsAllowPurchase"].ToString()!="")
					{
						model.IsAllowPurchase=int.Parse(dt.Rows[n]["IsAllowPurchase"].ToString());
					}
					if(dt.Rows[n]["IsDiscount"].ToString()!="")
					{
						model.IsDiscount=int.Parse(dt.Rows[n]["IsDiscount"].ToString());
					}
					if(dt.Rows[n]["PointsRedeem"].ToString()!="")
					{
						model.PointsRedeem=decimal.Parse(dt.Rows[n]["PointsRedeem"].ToString());
					}
					if(dt.Rows[n]["IsPointsRedeem"].ToString()!="")
					{
						model.IsPointsRedeem=int.Parse(dt.Rows[n]["IsPointsRedeem"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["IsPackage"].ToString()!="")
					{
						model.IsPackage=int.Parse(dt.Rows[n]["IsPackage"].ToString());
					}
					//model.MerchandiseID=dt.Rows[n]["MerchandiseID"].ToString();
					if(dt.Rows[n]["Quantity"].ToString()!="")
					{
						model.Quantity=int.Parse(dt.Rows[n]["Quantity"].ToString());
					}
					//model.CostID=dt.Rows[n]["CostID"].ToString();
					if(dt.Rows[n]["MerchCounts"].ToString()!="")
					{
						model.MerchCounts=decimal.Parse(dt.Rows[n]["MerchCounts"].ToString());
					}
					if(dt.Rows[n]["MerchState"].ToString()!="")
					{
						model.MerchState=int.Parse(dt.Rows[n]["MerchState"].ToString());
					}
					if(dt.Rows[n]["IsSale"].ToString()!="")
					{
						model.IsSale=int.Parse(dt.Rows[n]["IsSale"].ToString());
					}
					model.Standard=dt.Rows[n]["Standard"].ToString();
					if(dt.Rows[n]["IsHelpSale"].ToString()!="")
					{
						model.IsHelpSale=int.Parse(dt.Rows[n]["IsHelpSale"].ToString());
					}
					if(dt.Rows[n]["GetMethod"].ToString()!="")
					{
						model.GetMethod=int.Parse(dt.Rows[n]["GetMethod"].ToString());
					}
					if(dt.Rows[n]["IsHelpSaleValue"].ToString()!="")
					{
						model.IsHelpSaleValue=decimal.Parse(dt.Rows[n]["IsHelpSaleValue"].ToString());
					}
					//model.SupplierID=dt.Rows[n]["SupplierID"].ToString();
					if(dt.Rows[n]["OutMethod"].ToString()!="")
					{
						model.OutMethod=int.Parse(dt.Rows[n]["OutMethod"].ToString());
					}
					if(dt.Rows[n]["IsLease"].ToString()!="")
					{
						model.IsLease=int.Parse(dt.Rows[n]["IsLease"].ToString());
					}
					if(dt.Rows[n]["IsPurchase"].ToString()!="")
					{
						model.IsPurchase=int.Parse(dt.Rows[n]["IsPurchase"].ToString());
					}
					if(dt.Rows[n]["PurchasePrice"].ToString()!="")
					{
						model.PurchasePrice=decimal.Parse(dt.Rows[n]["PurchasePrice"].ToString());
					}
					model.UseMerchTypeCode=dt.Rows[n]["UseMerchTypeCode"].ToString();
					if(dt.Rows[n]["NoOutStore"].ToString()!="")
					{
						model.NoOutStore=int.Parse(dt.Rows[n]["NoOutStore"].ToString());
					}
					if(dt.Rows[n]["ExpirationDate"].ToString()!="")
					{
						model.ExpirationDate=int.Parse(dt.Rows[n]["ExpirationDate"].ToString());
					}
					model.PriceCheckSort=dt.Rows[n]["PriceCheckSort"].ToString();
					model.PriceLimitStartDate=dt.Rows[n]["PriceLimitStartDate"].ToString();
					model.PriceLimitEndDate=dt.Rows[n]["PriceLimitEndDate"].ToString();
					model.Brand=dt.Rows[n]["Brand"].ToString();
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize,string SortField,int Sort)
		{
			return dal.GetList(out PageCount, out Counts, StrCondition, PageIndex, PageSize,SortField,Sort);
		}

		#endregion  成员方法
	}
}

