using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Club;
namespace MobileSoft.BLL.Club
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Club_Store 的摘要说明。
	/// </summary>
	public class Bll_Tb_Club_Store
	{
		private readonly MobileSoft.DAL.Club.Dal_Tb_Club_Store dal=new MobileSoft.DAL.Club.Dal_Tb_Club_Store();
		public Bll_Tb_Club_Store()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int InfoId)
		{
			return dal.Exists(InfoId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Club.Tb_Club_Store model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Club.Tb_Club_Store model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int InfoId)
		{
			
			dal.Delete(InfoId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Club.Tb_Club_Store GetModel(int InfoId)
		{
			
			return dal.GetModel(InfoId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Club.Tb_Club_Store GetModelByCache(int InfoId)
		{
			
			string CacheKey = "Tb_Club_StoreModel-" + InfoId;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(InfoId);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Club.Tb_Club_Store)objModel;
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
		public List<MobileSoft.Model.Club.Tb_Club_Store> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Club.Tb_Club_Store> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Club.Tb_Club_Store> modelList = new List<MobileSoft.Model.Club.Tb_Club_Store>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Club.Tb_Club_Store model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Club.Tb_Club_Store();
					if(dt.Rows[n]["InfoId"].ToString()!="")
					{
						model.InfoId=int.Parse(dt.Rows[n]["InfoId"].ToString());
					}
					//model.WareHouseID=dt.Rows[n]["WareHouseID"].ToString();
					//model.MerchID=dt.Rows[n]["MerchID"].ToString();
					if(dt.Rows[n]["Quantity"].ToString()!="")
					{
						model.Quantity=decimal.Parse(dt.Rows[n]["Quantity"].ToString());
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

