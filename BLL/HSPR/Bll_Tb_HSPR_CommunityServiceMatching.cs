using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_HSPR_CommunityServiceMatching 的摘要说明。
	/// </summary>
	public class Bll_Tb_HSPR_CommunityServiceMatching
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CommunityServiceMatching dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CommunityServiceMatching();
		public Bll_Tb_HSPR_CommunityServiceMatching()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ScID)
		{
			return dal.Exists(ScID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CommunityServiceMatching model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CommunityServiceMatching model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ScID)
		{
			
			dal.Delete(ScID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommunityServiceMatching GetModel(long ScID)
		{
			
			return dal.GetModel(ScID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommunityServiceMatching GetModelByCache(long ScID)
		{
			
			string CacheKey = "Tb_HSPR_CommunityServiceMatchingModel-" + ScID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ScID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_CommunityServiceMatching)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CommunityServiceMatching> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CommunityServiceMatching> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_CommunityServiceMatching> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_CommunityServiceMatching>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_CommunityServiceMatching model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_CommunityServiceMatching();
					//model.ScID=dt.Rows[n]["ScID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					if(dt.Rows[n]["ScType"].ToString()!="")
					{
						model.ScType=int.Parse(dt.Rows[n]["ScType"].ToString());
					}
					model.ScTyCode=dt.Rows[n]["ScTyCode"].ToString();
					if(dt.Rows[n]["ScNum"].ToString()!="")
					{
						model.ScNum=int.Parse(dt.Rows[n]["ScNum"].ToString());
					}
					model.ScName=dt.Rows[n]["ScName"].ToString();
					model.ScAddress=dt.Rows[n]["ScAddress"].ToString();
					model.ScLinkMan=dt.Rows[n]["ScLinkMan"].ToString();
					model.ScLinkPhone=dt.Rows[n]["ScLinkPhone"].ToString();
					model.ScMemo=dt.Rows[n]["ScMemo"].ToString();
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

