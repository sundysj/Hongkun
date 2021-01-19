using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Club;
namespace MobileSoft.BLL.Club
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Club_Warehouse 的摘要说明。
	/// </summary>
	public class Bll_Tb_Club_Warehouse
	{
		private readonly MobileSoft.DAL.Club.Dal_Tb_Club_Warehouse dal=new MobileSoft.DAL.Club.Dal_Tb_Club_Warehouse();
		public Bll_Tb_Club_Warehouse()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long WareHouseID)
		{
			return dal.Exists(WareHouseID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Club.Tb_Club_Warehouse model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Club.Tb_Club_Warehouse model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long WareHouseID)
		{
			
			dal.Delete(WareHouseID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Club.Tb_Club_Warehouse GetModel(long WareHouseID)
		{
			
			return dal.GetModel(WareHouseID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Club.Tb_Club_Warehouse GetModelByCache(long WareHouseID)
		{
			
			string CacheKey = "Tb_Club_WarehouseModel-" + WareHouseID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(WareHouseID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Club.Tb_Club_Warehouse)objModel;
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
		public List<MobileSoft.Model.Club.Tb_Club_Warehouse> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Club.Tb_Club_Warehouse> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Club.Tb_Club_Warehouse> modelList = new List<MobileSoft.Model.Club.Tb_Club_Warehouse>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Club.Tb_Club_Warehouse model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Club.Tb_Club_Warehouse();
					//model.WareHouseID=dt.Rows[n]["WareHouseID"].ToString();
					model.OrganCode=dt.Rows[n]["OrganCode"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.WareHouseCode=dt.Rows[n]["WareHouseCode"].ToString();
					model.WareHouseName=dt.Rows[n]["WareHouseName"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["IsDefault"].ToString()!="")
					{
						model.IsDefault=int.Parse(dt.Rows[n]["IsDefault"].ToString());
					}
					if(dt.Rows[n]["IsOrgan"].ToString()!="")
					{
						model.IsOrgan=int.Parse(dt.Rows[n]["IsOrgan"].ToString());
					}
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

