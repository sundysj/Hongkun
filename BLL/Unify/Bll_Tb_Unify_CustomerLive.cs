using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Unify;
namespace MobileSoft.BLL.Unify
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Unify_CustomerLive 的摘要说明。
	/// </summary>
	public class Bll_Tb_Unify_CustomerLive
	{
		private readonly MobileSoft.DAL.Unify.Dal_Tb_Unify_CustomerLive dal=new MobileSoft.DAL.Unify.Dal_Tb_Unify_CustomerLive();
		public Bll_Tb_Unify_CustomerLive()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid LiveSynchCode)
		{
			return dal.Exists(LiveSynchCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Unify.Tb_Unify_CustomerLive model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Unify.Tb_Unify_CustomerLive model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Guid LiveSynchCode)
		{
			
			dal.Delete(LiveSynchCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Unify.Tb_Unify_CustomerLive GetModel(Guid LiveSynchCode)
		{
			
			return dal.GetModel(LiveSynchCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Unify.Tb_Unify_CustomerLive GetModelByCache(Guid LiveSynchCode)
		{
			
			string CacheKey = "Tb_Unify_CustomerLiveModel-" + LiveSynchCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(LiveSynchCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Unify.Tb_Unify_CustomerLive)objModel;
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
		public List<MobileSoft.Model.Unify.Tb_Unify_CustomerLive> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Unify.Tb_Unify_CustomerLive> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Unify.Tb_Unify_CustomerLive> modelList = new List<MobileSoft.Model.Unify.Tb_Unify_CustomerLive>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Unify.Tb_Unify_CustomerLive model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Unify.Tb_Unify_CustomerLive();
					if(dt.Rows[n]["LiveSynchCode"].ToString()!="")
					{
						model.LiveSynchCode=new Guid(dt.Rows[n]["LiveSynchCode"].ToString());
					}
					if(dt.Rows[n]["CommSynchCode"].ToString()!="")
					{
						model.CommSynchCode=new Guid(dt.Rows[n]["CommSynchCode"].ToString());
					}
					if(dt.Rows[n]["CustSynchCode"].ToString()!="")
					{
						model.CustSynchCode=new Guid(dt.Rows[n]["CustSynchCode"].ToString());
					}
					if(dt.Rows[n]["RoomSynchCode"].ToString()!="")
					{
						model.RoomSynchCode=new Guid(dt.Rows[n]["RoomSynchCode"].ToString());
					}
					//model.UnLiveID=dt.Rows[n]["UnLiveID"].ToString();
					if(dt.Rows[n]["LiveType"].ToString()!="")
					{
						model.LiveType=int.Parse(dt.Rows[n]["LiveType"].ToString());
					}
					model.L_BroadbandNum=dt.Rows[n]["L_BroadbandNum"].ToString();
					model.L_FixedTel=dt.Rows[n]["L_FixedTel"].ToString();
					model.L_MobilePhone=dt.Rows[n]["L_MobilePhone"].ToString();
					model.L_IPTVAccount=dt.Rows[n]["L_IPTVAccount"].ToString();
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
					if(dt.Rows[n]["SynchFlag"].ToString()!="")
					{
						model.SynchFlag=int.Parse(dt.Rows[n]["SynchFlag"].ToString());
					}
					//model.OrdSNum=dt.Rows[n]["OrdSNum"].ToString();
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

