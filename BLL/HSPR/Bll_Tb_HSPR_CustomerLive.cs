using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_HSPR_CustomerLive 的摘要说明。
	/// </summary>
	public class Bll_Tb_HSPR_CustomerLive
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CustomerLive dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CustomerLive();
		public Bll_Tb_HSPR_CustomerLive()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long LiveID)
		{
			return dal.Exists(LiveID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CustomerLive model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CustomerLive model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long LiveID)
		{
			
			dal.Delete(LiveID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CustomerLive GetModel(long LiveID)
		{
			
			return dal.GetModel(LiveID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CustomerLive GetModelByCache(long LiveID)
		{
			
			string CacheKey = "Tb_HSPR_CustomerLiveModel-" + LiveID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(LiveID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_CustomerLive)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CustomerLive> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CustomerLive> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_CustomerLive> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_CustomerLive>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_CustomerLive model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_CustomerLive();
					//model.LiveID=dt.Rows[n]["LiveID"].ToString();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					if(dt.Rows[n]["LiveType"].ToString()!="")
					{
						model.LiveType=int.Parse(dt.Rows[n]["LiveType"].ToString());
					}
					if(dt.Rows[n]["SubmitTime"].ToString()!="")
					{
						model.SubmitTime=DateTime.Parse(dt.Rows[n]["SubmitTime"].ToString());
					}
					if(dt.Rows[n]["FittingTime"].ToString()!="")
					{
						model.FittingTime=DateTime.Parse(dt.Rows[n]["FittingTime"].ToString());
					}
					if(dt.Rows[n]["StayTime"].ToString()!="")
					{
						model.StayTime=DateTime.Parse(dt.Rows[n]["StayTime"].ToString());
					}
					model.StayType=dt.Rows[n]["StayType"].ToString();
					if(dt.Rows[n]["ChargingTime"].ToString()!="")
					{
						model.ChargingTime=DateTime.Parse(dt.Rows[n]["ChargingTime"].ToString());
					}
					model.ChargeCause=dt.Rows[n]["ChargeCause"].ToString();
					if(dt.Rows[n]["ChargeTime"].ToString()!="")
					{
						model.ChargeTime=DateTime.Parse(dt.Rows[n]["ChargeTime"].ToString());
					}
					model.ContractSign=dt.Rows[n]["ContractSign"].ToString();
					model.RightsSign=dt.Rows[n]["RightsSign"].ToString();
					model.PropertyUses=dt.Rows[n]["PropertyUses"].ToString();
					if(dt.Rows[n]["StartDate"].ToString()!="")
					{
						model.StartDate=DateTime.Parse(dt.Rows[n]["StartDate"].ToString());
					}
					if(dt.Rows[n]["EndDate"].ToString()!="")
					{
						model.EndDate=DateTime.Parse(dt.Rows[n]["EndDate"].ToString());
					}
					if(dt.Rows[n]["ReletDate"].ToString()!="")
					{
						model.ReletDate=DateTime.Parse(dt.Rows[n]["ReletDate"].ToString());
					}
					if(dt.Rows[n]["IsActive"].ToString()!="")
					{
						model.IsActive=int.Parse(dt.Rows[n]["IsActive"].ToString());
					}
					if(dt.Rows[n]["IsDelLive"].ToString()!="")
					{
						model.IsDelLive=int.Parse(dt.Rows[n]["IsDelLive"].ToString());
					}
					if(dt.Rows[n]["IsDebts"].ToString()!="")
					{
						model.IsDebts=int.Parse(dt.Rows[n]["IsDebts"].ToString());
					}
					if(dt.Rows[n]["IsSale"].ToString()!="")
					{
						model.IsSale=int.Parse(dt.Rows[n]["IsSale"].ToString());
					}
					if(dt.Rows[n]["PurchaseArea"].ToString()!="")
					{
						model.PurchaseArea=decimal.Parse(dt.Rows[n]["PurchaseArea"].ToString());
					}
					if(dt.Rows[n]["PropertyArea"].ToString()!="")
					{
						model.PropertyArea=decimal.Parse(dt.Rows[n]["PropertyArea"].ToString());
					}
					if(dt.Rows[n]["LiveState"].ToString()!="")
					{
						model.LiveState=int.Parse(dt.Rows[n]["LiveState"].ToString());
					}
					model.BankName=dt.Rows[n]["BankName"].ToString();
					model.BankIDs=dt.Rows[n]["BankIDs"].ToString();
					model.BankAccount=dt.Rows[n]["BankAccount"].ToString();
					model.BankAgreement=dt.Rows[n]["BankAgreement"].ToString();
					model.BankCode=dt.Rows[n]["BankCode"].ToString();
					model.BankNo=dt.Rows[n]["BankNo"].ToString();
					//model.OldCustID=dt.Rows[n]["OldCustID"].ToString();
					if(dt.Rows[n]["LiveSynchCode"].ToString()!="")
					{
						model.LiveSynchCode=new Guid(dt.Rows[n]["LiveSynchCode"].ToString());
					}
					if(dt.Rows[n]["SynchFlag"].ToString()!="")
					{
						model.SynchFlag=int.Parse(dt.Rows[n]["SynchFlag"].ToString());
					}
					model.BankProvince=dt.Rows[n]["BankProvince"].ToString();
					model.BankCity=dt.Rows[n]["BankCity"].ToString();
					model.BankInfo=dt.Rows[n]["BankInfo"].ToString();
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

