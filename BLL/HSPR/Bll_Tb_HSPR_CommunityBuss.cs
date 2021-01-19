using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_HSPR_CommunityBuss 的摘要说明。
	/// </summary>
	public class Bll_Tb_HSPR_CommunityBuss
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CommunityBuss dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CommunityBuss();
		public Bll_Tb_HSPR_CommunityBuss()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string CommBussID)
		{
			return dal.Exists(CommBussID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CommunityBuss model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CommunityBuss model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string CommBussID)
		{
			
			dal.Delete(CommBussID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommunityBuss GetModel(string CommBussID)
		{
			
			return dal.GetModel(CommBussID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommunityBuss GetModelByCache(string CommBussID)
		{
			
			string CacheKey = "Tb_HSPR_CommunityBussModel-" + CommBussID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CommBussID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_CommunityBuss)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CommunityBuss> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CommunityBuss> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_CommunityBuss> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_CommunityBuss>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_CommunityBuss model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_CommunityBuss();
					model.CommBussID=dt.Rows[n]["CommBussID"].ToString();
					model.BussName=dt.Rows[n]["BussName"].ToString();
					//model.BussID=dt.Rows[n]["BussID"].ToString();
					//model.CommID=dt.Rows[n]["CommID"].ToString();
					model.BussTypeName=dt.Rows[n]["BussTypeName"].ToString();
					if(dt.Rows[n]["BussIndex"].ToString()!="")
					{
						model.BussIndex=int.Parse(dt.Rows[n]["BussIndex"].ToString());
					}
					if(dt.Rows[n]["StartDate"].ToString()!="")
					{
						model.StartDate=DateTime.Parse(dt.Rows[n]["StartDate"].ToString());
					}
					if(dt.Rows[n]["EndDate"].ToString()!="")
					{
						model.EndDate=DateTime.Parse(dt.Rows[n]["EndDate"].ToString());
					}
					model.Memo=dt.Rows[n]["Memo"].ToString();
					if(dt.Rows[n]["BussType"].ToString()!="")
					{
						model.BussType=int.Parse(dt.Rows[n]["BussType"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
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

