using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Common;
namespace MobileSoft.BLL.Common
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Common_CommunityService 的摘要说明。
	/// </summary>
	public class Bll_Tb_Common_CommunityService
	{
		private readonly MobileSoft.DAL.Common.Dal_Tb_Common_CommunityService dal=new MobileSoft.DAL.Common.Dal_Tb_Common_CommunityService();
		public Bll_Tb_Common_CommunityService()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long InfoId)
		{
			return dal.Exists(InfoId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MobileSoft.Model.Common.Tb_Common_CommunityService model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Common.Tb_Common_CommunityService model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long InfoId)
		{
			
			dal.Delete(InfoId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Common.Tb_Common_CommunityService GetModel(long InfoId)
		{
			
			return dal.GetModel(InfoId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Common.Tb_Common_CommunityService GetModelByCache(long InfoId)
		{
			
			string CacheKey = "Tb_Common_CommunityServiceModel-" + InfoId;
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
			return (MobileSoft.Model.Common.Tb_Common_CommunityService)objModel;
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
		public List<MobileSoft.Model.Common.Tb_Common_CommunityService> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Common.Tb_Common_CommunityService> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Common.Tb_Common_CommunityService> modelList = new List<MobileSoft.Model.Common.Tb_Common_CommunityService>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Common.Tb_Common_CommunityService model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Common.Tb_Common_CommunityService();
					//model.InfoId=dt.Rows[n]["InfoId"].ToString();
					model.OrganCode=dt.Rows[n]["OrganCode"].ToString();
					model.DepCode=dt.Rows[n]["DepCode"].ToString();
					model.Title=dt.Rows[n]["Title"].ToString();
					model.Content=dt.Rows[n]["Content"].ToString();
					model.UserCode=dt.Rows[n]["UserCode"].ToString();
					if(dt.Rows[n]["IssueDate"].ToString()!="")
					{
						model.IssueDate=DateTime.Parse(dt.Rows[n]["IssueDate"].ToString());
					}
					model.InfoTypeName=dt.Rows[n]["InfoTypeName"].ToString();
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

        public DataTable Common_CommunityService_TopFilter(int TopNum, string Sql)
        {
            return dal.Common_CommunityService_TopFilter(TopNum, Sql);
        }
	}
}

