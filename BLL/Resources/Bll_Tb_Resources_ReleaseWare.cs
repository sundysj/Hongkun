using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Resources;
namespace MobileSoft.BLL.Resources
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Resources_ReleaseWare 的摘要说明。
	/// </summary>
	public class Bll_Tb_Resources_ReleaseWare
	{
		private readonly MobileSoft.DAL.Resources.Dal_Tb_Resources_ReleaseWare dal=new MobileSoft.DAL.Resources.Dal_Tb_Resources_ReleaseWare();
		public Bll_Tb_Resources_ReleaseWare()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ReleaseWareID)
		{
			return dal.Exists(ReleaseWareID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public void Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseWare model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseWare model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ReleaseWareID)
		{
			
			dal.Delete(ReleaseWareID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseWare GetModel(long ReleaseWareID)
		{
			
			return dal.GetModel(ReleaseWareID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseWare GetModelByCache(long ReleaseWareID)
		{
			
			string CacheKey = "Tb_Resources_ReleaseWareModel-" + ReleaseWareID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ReleaseWareID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Resources.Tb_Resources_ReleaseWare)objModel;
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
		public List<MobileSoft.Model.Resources.Tb_Resources_ReleaseWare> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Resources.Tb_Resources_ReleaseWare> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Resources.Tb_Resources_ReleaseWare> modelList = new List<MobileSoft.Model.Resources.Tb_Resources_ReleaseWare>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Resources.Tb_Resources_ReleaseWare model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Resources.Tb_Resources_ReleaseWare();
					//model.ReleaseWareID=dt.Rows[n]["ReleaseWareID"].ToString();
					//model.ReleaseID=dt.Rows[n]["ReleaseID"].ToString();
					model.SourceArea=dt.Rows[n]["SourceArea"].ToString();
					model.Factory=dt.Rows[n]["Factory"].ToString();
					model.Brand=dt.Rows[n]["Brand"].ToString();
					model.Version=dt.Rows[n]["Version"].ToString();
					model.Colors=dt.Rows[n]["Colors"].ToString();
					model.Size=dt.Rows[n]["Size"].ToString();
					model.Weight=dt.Rows[n]["Weight"].ToString();
					if(dt.Rows[n]["ListedData"].ToString()!="")
					{
						model.ListedData=DateTime.Parse(dt.Rows[n]["ListedData"].ToString());
					}
					model.ShelfLife=dt.Rows[n]["ShelfLife"].ToString();
					model.ReleaseWareContent=dt.Rows[n]["ReleaseWareContent"].ToString();
					model.PackingList=dt.Rows[n]["PackingList"].ToString();
					model.CustomerService=dt.Rows[n]["CustomerService"].ToString();
					model.ShippingMethod=dt.Rows[n]["ShippingMethod"].ToString();
					model.ReleaseWareNeedKnow=dt.Rows[n]["ReleaseWareNeedKnow"].ToString();
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

