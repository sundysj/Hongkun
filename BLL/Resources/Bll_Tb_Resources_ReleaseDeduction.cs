using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Resources;
namespace MobileSoft.BLL.Resources
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Resources_ReleaseDeduction 的摘要说明。
	/// </summary>
	public class Bll_Tb_Resources_ReleaseDeduction
	{
		private readonly MobileSoft.DAL.Resources.Dal_Tb_Resources_ReleaseDeduction dal=new MobileSoft.DAL.Resources.Dal_Tb_Resources_ReleaseDeduction();
		public Bll_Tb_Resources_ReleaseDeduction()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ReleaseDeductionID)
		{
			return dal.Exists(ReleaseDeductionID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long  Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseDeduction model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseDeduction model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ReleaseDeductionID)
		{
			
			dal.Delete(ReleaseDeductionID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseDeduction GetModel(long ReleaseDeductionID)
		{
			
			return dal.GetModel(ReleaseDeductionID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseDeduction GetModelByCache(long ReleaseDeductionID)
		{
			
			string CacheKey = "Tb_Resources_ReleaseDeductionModel-" + ReleaseDeductionID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ReleaseDeductionID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Resources.Tb_Resources_ReleaseDeduction)objModel;
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
		public List<MobileSoft.Model.Resources.Tb_Resources_ReleaseDeduction> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Resources.Tb_Resources_ReleaseDeduction> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Resources.Tb_Resources_ReleaseDeduction> modelList = new List<MobileSoft.Model.Resources.Tb_Resources_ReleaseDeduction>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Resources.Tb_Resources_ReleaseDeduction model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Resources.Tb_Resources_ReleaseDeduction();
					//model.ReleaseDeductionID=dt.Rows[n]["ReleaseDeductionID"].ToString();
					model.ReleaseDeductionContent=dt.Rows[n]["ReleaseDeductionContent"].ToString();
					model.ReleaseDeductionNeedKnow=dt.Rows[n]["ReleaseDeductionNeedKnow"].ToString();
					//model.ReleaseID=dt.Rows[n]["ReleaseID"].ToString();
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

