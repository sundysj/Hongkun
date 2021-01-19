using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_HSPR_CommunityInfo 的摘要说明。
	/// </summary>
	public class Bll_Tb_HSPR_CommunityInfo
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CommunityInfo dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CommunityInfo();
		public Bll_Tb_HSPR_CommunityInfo()
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CommunityInfo model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CommunityInfo model)
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
		public MobileSoft.Model.HSPR.Tb_HSPR_CommunityInfo GetModel(long InfoID)
		{
			
			return dal.GetModel(InfoID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommunityInfo GetModelByCache(long InfoID)
		{
			
			string CacheKey = "Tb_HSPR_CommunityInfoModel-" + InfoID;
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
			return (MobileSoft.Model.HSPR.Tb_HSPR_CommunityInfo)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CommunityInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CommunityInfo> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_CommunityInfo> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_CommunityInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_CommunityInfo model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_CommunityInfo();
					//model.InfoID=dt.Rows[n]["InfoID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.Heading=dt.Rows[n]["Heading"].ToString();
					if(dt.Rows[n]["IssueDate"].ToString()!="")
					{
						model.IssueDate=DateTime.Parse(dt.Rows[n]["IssueDate"].ToString());
					}
					if(dt.Rows[n]["ShowEndDate"].ToString()!="")
					{
						model.ShowEndDate=DateTime.Parse(dt.Rows[n]["ShowEndDate"].ToString());
					}
					model.InfoSource=dt.Rows[n]["InfoSource"].ToString();
					model.InfoType=dt.Rows[n]["InfoType"].ToString();
					if(dt.Rows[n]["IsAudit"].ToString()!="")
					{
						model.IsAudit=int.Parse(dt.Rows[n]["IsAudit"].ToString());
					}
					model.InfoContent=dt.Rows[n]["InfoContent"].ToString();
					model.ImageUrl=dt.Rows[n]["ImageUrl"].ToString();
					if(dt.Rows[n]["CommInfoSynchCode"].ToString()!="")
					{
						model.CommInfoSynchCode=new Guid(dt.Rows[n]["CommInfoSynchCode"].ToString());
					}
					if(dt.Rows[n]["SynchFlag"].ToString()!="")
					{
						model.SynchFlag=int.Parse(dt.Rows[n]["SynchFlag"].ToString());
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

