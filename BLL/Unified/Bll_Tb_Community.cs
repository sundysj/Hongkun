using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Unified;
namespace MobileSoft.BLL.Unified
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Community 的摘要说明。
	/// </summary>
	public class Bll_Tb_Community
	{
		private readonly MobileSoft.DAL.Unified.Dal_Tb_Community dal=new MobileSoft.DAL.Unified.Dal_Tb_Community();
		public Bll_Tb_Community()
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
		public void Add(MobileSoft.Model.Unified.Tb_Community model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Unified.Tb_Community model)
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
		public MobileSoft.Model.Unified.Tb_Community GetModel(string Id)
		{
			return dal.GetModel(Id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Unified.Tb_Community GetModelByCache(string Id)
		{
			
			string CacheKey = "Tb_CommunityModel-" + Id;
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
			return (MobileSoft.Model.Unified.Tb_Community)objModel;
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
		public List<MobileSoft.Model.Unified.Tb_Community> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Unified.Tb_Community> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Unified.Tb_Community> modelList = new List<MobileSoft.Model.Unified.Tb_Community>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Unified.Tb_Community model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Unified.Tb_Community();
					model.Id=dt.Rows[n]["Id"].ToString();
					model.Province=dt.Rows[n]["Province"].ToString();
					model.Area=dt.Rows[n]["Area"].ToString();
					model.City=dt.Rows[n]["City"].ToString();
					model.DBServer=dt.Rows[n]["DBServer"].ToString();
					if(dt.Rows[n]["CorpID"].ToString()!="")
					{
						model.CorpID=int.Parse(dt.Rows[n]["CorpID"].ToString());
					}
					model.DBName=dt.Rows[n]["DBName"].ToString();
					model.DBUser=dt.Rows[n]["DBUser"].ToString();
					model.DBPwd=dt.Rows[n]["DBPwd"].ToString();
					model.CommID=dt.Rows[n]["CommID"].ToString();
					model.CorpName=dt.Rows[n]["CorpName"].ToString();
					model.CommName=dt.Rows[n]["CommName"].ToString();
					model.ModuleRights=dt.Rows[n]["ModuleRights"].ToString();
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

