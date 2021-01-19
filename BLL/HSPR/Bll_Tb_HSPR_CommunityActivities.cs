using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_HSPR_CommunityActivities 的摘要说明。
	/// </summary>
	public class Bll_Tb_HSPR_CommunityActivities
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CommunityActivities dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CommunityActivities();
		public Bll_Tb_HSPR_CommunityActivities()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long InfoID)
		{
			return dal.Exists(InfoID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(MobileSoft.Model.HSPR.Tb_HSPR_CommunityActivities model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CommunityActivities model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long InfoID)
		{
			
			dal.Delete(InfoID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommunityActivities GetModel(long InfoID)
		{
			
			return dal.GetModel(InfoID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommunityActivities GetModelByCache(long InfoID)
		{
			
			string CacheKey = "Tb_HSPR_CommunityActivitiesModel-" + InfoID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(InfoID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_CommunityActivities)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CommunityActivities> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CommunityActivities> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_CommunityActivities> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_CommunityActivities>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_CommunityActivities model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_CommunityActivities();
					//model.InfoID=dt.Rows[n]["InfoID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.ActHeading=dt.Rows[n]["ActHeading"].ToString();
					model.ActPlace=dt.Rows[n]["ActPlace"].ToString();
					model.Moderator=dt.Rows[n]["Moderator"].ToString();
					model.ActDate=dt.Rows[n]["ActDate"].ToString();
					model.ActPeople=dt.Rows[n]["ActPeople"].ToString();
					if(dt.Rows[n]["IsAudit"].ToString()!="")
					{
						model.IsAudit=int.Parse(dt.Rows[n]["IsAudit"].ToString());
					}
					model.ActContent=dt.Rows[n]["ActContent"].ToString();
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

