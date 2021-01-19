using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_HSPR_PreCostsDetail 的摘要说明。
	/// </summary>
	public class Bll_Tb_HSPR_PreCostsDetail
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_PreCostsDetail dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_PreCostsDetail();
		public Bll_Tb_HSPR_PreCostsDetail()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long RecdID)
		{
			return dal.Exists(RecdID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_PreCostsDetail model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_PreCostsDetail model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long RecdID)
		{
			
			dal.Delete(RecdID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_PreCostsDetail GetModel(long RecdID)
		{
			
			return dal.GetModel(RecdID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_PreCostsDetail GetModelByCache(long RecdID)
		{
			
			string CacheKey = "Tb_HSPR_PreCostsDetailModel-" + RecdID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(RecdID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_PreCostsDetail)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_PreCostsDetail> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_PreCostsDetail> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_PreCostsDetail> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_PreCostsDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_PreCostsDetail model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_PreCostsDetail();
					//model.RecdID=dt.Rows[n]["RecdID"].ToString();
					//model.PrecID=dt.Rows[n]["PrecID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					//model.CostID=dt.Rows[n]["CostID"].ToString();
					if(dt.Rows[n]["PrecAmount"].ToString()!="")
					{
						model.PrecAmount=decimal.Parse(dt.Rows[n]["PrecAmount"].ToString());
					}
					if(dt.Rows[n]["PrecDate"].ToString()!="")
					{
						model.PrecDate=DateTime.Parse(dt.Rows[n]["PrecDate"].ToString());
					}
					model.PrecMemo=dt.Rows[n]["PrecMemo"].ToString();
					model.UserCode=dt.Rows[n]["UserCode"].ToString();
					model.BillsSign=dt.Rows[n]["BillsSign"].ToString();
					model.ChargeMode=dt.Rows[n]["ChargeMode"].ToString();
					if(dt.Rows[n]["AccountWay"].ToString()!="")
					{
						model.AccountWay=int.Parse(dt.Rows[n]["AccountWay"].ToString());
					}
					if(dt.Rows[n]["IsAudit"].ToString()!="")
					{
						model.IsAudit=int.Parse(dt.Rows[n]["IsAudit"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					//model.ReceID=dt.Rows[n]["ReceID"].ToString();
					if(dt.Rows[n]["OldPrecAmount"].ToString()!="")
					{
						model.OldPrecAmount=decimal.Parse(dt.Rows[n]["OldPrecAmount"].ToString());
					}
					if(dt.Rows[n]["NewPrecAmount"].ToString()!="")
					{
						model.NewPrecAmount=decimal.Parse(dt.Rows[n]["NewPrecAmount"].ToString());
					}
					//model.FeesID=dt.Rows[n]["FeesID"].ToString();
					if(dt.Rows[n]["SourceType"].ToString()!="")
					{
						model.SourceType=int.Parse(dt.Rows[n]["SourceType"].ToString());
					}
					if(dt.Rows[n]["FeesDueDate"].ToString()!="")
					{
						model.FeesDueDate=DateTime.Parse(dt.Rows[n]["FeesDueDate"].ToString());
					}
					if(dt.Rows[n]["IsProperty"].ToString()!="")
					{
						model.IsProperty=int.Parse(dt.Rows[n]["IsProperty"].ToString());
					}
					//model.CommisionCostID=dt.Rows[n]["CommisionCostID"].ToString();
					if(dt.Rows[n]["DueAmount"].ToString()!="")
					{
						model.DueAmount=decimal.Parse(dt.Rows[n]["DueAmount"].ToString());
					}
					if(dt.Rows[n]["WaivAmount"].ToString()!="")
					{
						model.WaivAmount=decimal.Parse(dt.Rows[n]["WaivAmount"].ToString());
					}
					if(dt.Rows[n]["CommisionAmount"].ToString()!="")
					{
						model.CommisionAmount=decimal.Parse(dt.Rows[n]["CommisionAmount"].ToString());
					}
					if(dt.Rows[n]["WaivCommisAmount"].ToString()!="")
					{
						model.WaivCommisAmount=decimal.Parse(dt.Rows[n]["WaivCommisAmount"].ToString());
					}
					//model.HandID=dt.Rows[n]["HandID"].ToString();
					//model.CarParkID=dt.Rows[n]["CarParkID"].ToString();
					//model.ParkID=dt.Rows[n]["ParkID"].ToString();
					model.CostIDs=dt.Rows[n]["CostIDs"].ToString();
					model.CostNames=dt.Rows[n]["CostNames"].ToString();
					model.HandIDs=dt.Rows[n]["HandIDs"].ToString();
					model.ParkNames=dt.Rows[n]["ParkNames"].ToString();
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

            //预交撤销
            public void PreCostsDetailRepeal(long ReceID)
            {
                  dal.PreCostsDetailRepeal(ReceID);
            }

            //预交登记是否可以撤销
            public bool PreCostsDetailCheckRepeal(long RecdID)
            {
                  return dal.PreCostsDetailCheckRepeal(RecdID);
            }

            public void PreCostsDetailDelete(long RecdID)
            {
                  dal.PreCostsDetailDelete(RecdID);
            }
	}
}

