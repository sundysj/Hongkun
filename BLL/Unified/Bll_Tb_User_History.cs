using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Unified;
namespace MobileSoft.BLL.Unified
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_User_History 的摘要说明。
	/// </summary>
	public class Bll_Tb_User_History
	{
		private readonly MobileSoft.DAL.Unified.Dal_Tb_User_History dal=new MobileSoft.DAL.Unified.Dal_Tb_User_History();
		public Bll_Tb_User_History()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Unified.Tb_User_History model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Unified.Tb_User_History model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string Id)
		{
			
			dal.Delete(Id);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Unified.Tb_User_History GetModel(string Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Unified.Tb_User_History GetModelByCache(string Id)
		{
			
			string CacheKey = "Tb_User_HistoryModel-" + Id;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Id);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Unified.Tb_User_History)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Unified.Tb_User_History> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Unified.Tb_User_History> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Unified.Tb_User_History> modelList = new List<MobileSoft.Model.Unified.Tb_User_History>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Unified.Tb_User_History model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Unified.Tb_User_History();
					model.Id=dt.Rows[n]["Id"].ToString();
					model.UserId=dt.Rows[n]["UserId"].ToString();
					model.LogContent=dt.Rows[n]["LogContent"].ToString();
					if(dt.Rows[n]["OprDate"].ToString()!="")
					{
						model.OprDate=DateTime.Parse(dt.Rows[n]["OprDate"].ToString());
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

