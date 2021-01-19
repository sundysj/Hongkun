using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.OAPublicWork;
namespace MobileSoft.BLL.OAPublicWork
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_OAPublicWork_FixedAssetsLossTableDetail 的摘要说明。
	/// </summary>
	public class Bll_Tb_OAPublicWork_FixedAssetsLossTableDetail
	{
		private readonly MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_FixedAssetsLossTableDetail dal=new MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_FixedAssetsLossTableDetail();
		public Bll_Tb_OAPublicWork_FixedAssetsLossTableDetail()
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
		public bool Exists(int InfoID)
		{
			return dal.Exists(InfoID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTableDetail model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTableDetail model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int InfoID)
		{
			
			dal.Delete(InfoID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTableDetail GetModel(int InfoID)
		{
			
			return dal.GetModel(InfoID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTableDetail GetModelByCache(int InfoID)
		{
			
			string CacheKey = "Tb_OAPublicWork_FixedAssetsLossTableDetailModel-" + InfoID;
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
			return (MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTableDetail)objModel;
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
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTableDetail> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTableDetail> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTableDetail> modelList = new List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTableDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTableDetail model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTableDetail();
					if(dt.Rows[n]["InfoID"].ToString()!="")
					{
						model.InfoID=int.Parse(dt.Rows[n]["InfoID"].ToString());
					}
					if(dt.Rows[n]["FixedAssetsLossTableID"].ToString()!="")
					{
						model.FixedAssetsLossTableID=int.Parse(dt.Rows[n]["FixedAssetsLossTableID"].ToString());
					}
					model.PName=dt.Rows[n]["PName"].ToString();
					model.Model=dt.Rows[n]["Model"].ToString();
					model.Unit=dt.Rows[n]["Unit"].ToString();
					if(dt.Rows[n]["Price"].ToString()!="")
					{
						model.Price=decimal.Parse(dt.Rows[n]["Price"].ToString());
					}
					if(dt.Rows[n]["Quantity"].ToString()!="")
					{
						model.Quantity=int.Parse(dt.Rows[n]["Quantity"].ToString());
					}
					if(dt.Rows[n]["OriginalValue"].ToString()!="")
					{
						model.OriginalValue=decimal.Parse(dt.Rows[n]["OriginalValue"].ToString());
					}
					if(dt.Rows[n]["NetWorth"].ToString()!="")
					{
						model.NetWorth=decimal.Parse(dt.Rows[n]["NetWorth"].ToString());
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

