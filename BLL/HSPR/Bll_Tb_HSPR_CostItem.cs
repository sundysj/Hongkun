using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_HSPR_CostItem 的摘要说明。
	/// </summary>
	public class Bll_Tb_HSPR_CostItem
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CostItem dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CostItem();
		public Bll_Tb_HSPR_CostItem()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long CostID)
		{
			return dal.Exists(CostID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CostItem model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CostItem model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long CostID)
		{
			
			dal.Delete(CostID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CostItem GetModel(long CostID)
		{
			
			return dal.GetModel(CostID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CostItem GetModelByCache(long CostID)
		{
			
			string CacheKey = "Tb_HSPR_CostItemModel-" + CostID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CostID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_CostItem)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CostItem> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CostItem> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_CostItem> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_CostItem>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_CostItem model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_CostItem();
					//model.CostID=dt.Rows[n]["CostID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					if(dt.Rows[n]["CostSNum"].ToString()!="")
					{
						model.CostSNum=int.Parse(dt.Rows[n]["CostSNum"].ToString());
					}
					model.CostName=dt.Rows[n]["CostName"].ToString();
					if(dt.Rows[n]["CostType"].ToString()!="")
					{
						model.CostType=int.Parse(dt.Rows[n]["CostType"].ToString());
					}
					if(dt.Rows[n]["CostGeneType"].ToString()!="")
					{
						model.CostGeneType=int.Parse(dt.Rows[n]["CostGeneType"].ToString());
					}
					//model.CollUnitID=dt.Rows[n]["CollUnitID"].ToString();
					if(dt.Rows[n]["DueDate"].ToString()!="")
					{
						model.DueDate=int.Parse(dt.Rows[n]["DueDate"].ToString());
					}
					model.AccountsSign=dt.Rows[n]["AccountsSign"].ToString();
					model.AccountsName=dt.Rows[n]["AccountsName"].ToString();
					if(dt.Rows[n]["ChargeCycle"].ToString()!="")
					{
						model.ChargeCycle=int.Parse(dt.Rows[n]["ChargeCycle"].ToString());
					}
					if(dt.Rows[n]["RoundingNum"].ToString()!="")
					{
						model.RoundingNum=int.Parse(dt.Rows[n]["RoundingNum"].ToString());
					}
					if(dt.Rows[n]["IsBank"].ToString()!="")
					{
						model.IsBank=int.Parse(dt.Rows[n]["IsBank"].ToString());
					}
					if(dt.Rows[n]["DelinDelay"].ToString()!="")
					{
						model.DelinDelay=int.Parse(dt.Rows[n]["DelinDelay"].ToString());
					}
					if(dt.Rows[n]["DelinRates"].ToString()!="")
					{
						model.DelinRates=decimal.Parse(dt.Rows[n]["DelinRates"].ToString());
					}
					model.PreCostSign=dt.Rows[n]["PreCostSign"].ToString();
					model.Memo=dt.Rows[n]["Memo"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					//model.CorpCostID=dt.Rows[n]["CorpCostID"].ToString();
					model.CostCode=dt.Rows[n]["CostCode"].ToString();
					model.SysCostSign=dt.Rows[n]["SysCostSign"].ToString();
					if(dt.Rows[n]["DuePlotDate"].ToString()!="")
					{
						model.DuePlotDate=int.Parse(dt.Rows[n]["DuePlotDate"].ToString());
					}
					//model.HighCorpCostID=dt.Rows[n]["HighCorpCostID"].ToString();
					if(dt.Rows[n]["CostBigType"].ToString()!="")
					{
						model.CostBigType=int.Parse(dt.Rows[n]["CostBigType"].ToString());
					}
					if(dt.Rows[n]["DelinType"].ToString()!="")
					{
						model.DelinType=int.Parse(dt.Rows[n]["DelinType"].ToString());
					}
					if(dt.Rows[n]["DelinDay"].ToString()!="")
					{
						model.DelinDay=int.Parse(dt.Rows[n]["DelinDay"].ToString());
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

