using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_HSPR_Fees 的摘要说明。
	/// </summary>
	public class Bll_Tb_HSPR_Fees
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_Fees dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_Fees();
		public Bll_Tb_HSPR_Fees()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long FeesID)
		{
			return dal.Exists(FeesID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_Fees model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_Fees model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long FeesID)
		{
			
			dal.Delete(FeesID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_Fees GetModel(long FeesID)
		{
			
			return dal.GetModel(FeesID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_Fees GetModelByCache(long FeesID)
		{
			
			string CacheKey = "Tb_HSPR_FeesModel-" + FeesID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(FeesID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_Fees)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_Fees> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_Fees> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_Fees> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_Fees>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_Fees model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_Fees();
					//model.FeesID=dt.Rows[n]["FeesID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					//model.CostID=dt.Rows[n]["CostID"].ToString();
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					if(dt.Rows[n]["FeesDueDate"].ToString()!="")
					{
						model.FeesDueDate=DateTime.Parse(dt.Rows[n]["FeesDueDate"].ToString());
					}
					if(dt.Rows[n]["FeesStateDate"].ToString()!="")
					{
						model.FeesStateDate=DateTime.Parse(dt.Rows[n]["FeesStateDate"].ToString());
					}
					if(dt.Rows[n]["FeesEndDate"].ToString()!="")
					{
						model.FeesEndDate=DateTime.Parse(dt.Rows[n]["FeesEndDate"].ToString());
					}
					if(dt.Rows[n]["DueAmount"].ToString()!="")
					{
						model.DueAmount=decimal.Parse(dt.Rows[n]["DueAmount"].ToString());
					}
					if(dt.Rows[n]["DebtsAmount"].ToString()!="")
					{
						model.DebtsAmount=decimal.Parse(dt.Rows[n]["DebtsAmount"].ToString());
					}
					if(dt.Rows[n]["WaivAmount"].ToString()!="")
					{
						model.WaivAmount=decimal.Parse(dt.Rows[n]["WaivAmount"].ToString());
					}
					if(dt.Rows[n]["PrecAmount"].ToString()!="")
					{
						model.PrecAmount=decimal.Parse(dt.Rows[n]["PrecAmount"].ToString());
					}
					if(dt.Rows[n]["PaidAmount"].ToString()!="")
					{
						model.PaidAmount=decimal.Parse(dt.Rows[n]["PaidAmount"].ToString());
					}
					if(dt.Rows[n]["RefundAmount"].ToString()!="")
					{
						model.RefundAmount=decimal.Parse(dt.Rows[n]["RefundAmount"].ToString());
					}
					if(dt.Rows[n]["IsAudit"].ToString()!="")
					{
						model.IsAudit=int.Parse(dt.Rows[n]["IsAudit"].ToString());
					}
					model.FeesMemo=dt.Rows[n]["FeesMemo"].ToString();
					if(dt.Rows[n]["AccountFlag"].ToString()!="")
					{
						model.AccountFlag=int.Parse(dt.Rows[n]["AccountFlag"].ToString());
					}
					if(dt.Rows[n]["IsBank"].ToString()!="")
					{
						model.IsBank=int.Parse(dt.Rows[n]["IsBank"].ToString());
					}
					if(dt.Rows[n]["IsCharge"].ToString()!="")
					{
						model.IsCharge=int.Parse(dt.Rows[n]["IsCharge"].ToString());
					}
					if(dt.Rows[n]["IsFreeze"].ToString()!="")
					{
						model.IsFreeze=int.Parse(dt.Rows[n]["IsFreeze"].ToString());
					}
					if(dt.Rows[n]["IsProperty"].ToString()!="")
					{
						model.IsProperty=int.Parse(dt.Rows[n]["IsProperty"].ToString());
					}
					//model.CorpStanID=dt.Rows[n]["CorpStanID"].ToString();
					//model.StanID=dt.Rows[n]["StanID"].ToString();
					//model.OwnerFeesID=dt.Rows[n]["OwnerFeesID"].ToString();
					if(dt.Rows[n]["AccountsDueDate"].ToString()!="")
					{
						model.AccountsDueDate=DateTime.Parse(dt.Rows[n]["AccountsDueDate"].ToString());
					}
					//model.HandID=dt.Rows[n]["HandID"].ToString();
					model.MeterSign=dt.Rows[n]["MeterSign"].ToString();
					if(dt.Rows[n]["CalcAmount"].ToString()!="")
					{
						model.CalcAmount=decimal.Parse(dt.Rows[n]["CalcAmount"].ToString());
					}
					if(dt.Rows[n]["CalcAmount2"].ToString()!="")
					{
						model.CalcAmount2=decimal.Parse(dt.Rows[n]["CalcAmount2"].ToString());
					}
					//model.IncidentID=dt.Rows[n]["IncidentID"].ToString();
					//model.LeaseContID=dt.Rows[n]["LeaseContID"].ToString();
					//model.ContID=dt.Rows[n]["ContID"].ToString();
					model.StanMemo=dt.Rows[n]["StanMemo"].ToString();
					//model.CommisionCostID=dt.Rows[n]["CommisionCostID"].ToString();
					if(dt.Rows[n]["CommisionAmount"].ToString()!="")
					{
						model.CommisionAmount=decimal.Parse(dt.Rows[n]["CommisionAmount"].ToString());
					}
					if(dt.Rows[n]["WaivCommisAmount"].ToString()!="")
					{
						model.WaivCommisAmount=decimal.Parse(dt.Rows[n]["WaivCommisAmount"].ToString());
					}
					if(dt.Rows[n]["PerStanAmount"].ToString()!="")
					{
						model.PerStanAmount=decimal.Parse(dt.Rows[n]["PerStanAmount"].ToString());
					}
					if(dt.Rows[n]["IsPrec"].ToString()!="")
					{
						model.IsPrec=int.Parse(dt.Rows[n]["IsPrec"].ToString());
					}
					//model.ParkID=dt.Rows[n]["ParkID"].ToString();
					//model.CarparkID=dt.Rows[n]["CarparkID"].ToString();
					//model.MeterID=dt.Rows[n]["MeterID"].ToString();
					//model.PMeterID=dt.Rows[n]["PMeterID"].ToString();
					if(dt.Rows[n]["FeesSynchCode"].ToString()!="")
					{
						model.FeesSynchCode=new Guid(dt.Rows[n]["FeesSynchCode"].ToString());
					}
					if(dt.Rows[n]["SynchFlag"].ToString()!="")
					{
						model.SynchFlag=int.Parse(dt.Rows[n]["SynchFlag"].ToString());
					}
					//model.BedID=dt.Rows[n]["BedID"].ToString();
					if(dt.Rows[n]["RoomState"].ToString()!="")
					{
						model.RoomState=int.Parse(dt.Rows[n]["RoomState"].ToString());
					}
					model.OrderCode=dt.Rows[n]["OrderCode"].ToString();
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

