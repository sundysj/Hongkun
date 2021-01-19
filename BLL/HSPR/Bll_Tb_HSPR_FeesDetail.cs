using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_HSPR_FeesDetail 的摘要说明。
	/// </summary>
	public class Bll_Tb_HSPR_FeesDetail
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_FeesDetail dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_FeesDetail();
		public Bll_Tb_HSPR_FeesDetail()
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail model)
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
		public MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail GetModel(long RecdID)
		{
			
			return dal.GetModel(RecdID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail GetModelByCache(long RecdID)
		{
			
			string CacheKey = "Tb_HSPR_FeesDetailModel-" + RecdID;
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
			return (MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail();
					//model.RecdID=dt.Rows[n]["RecdID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					//model.CostID=dt.Rows[n]["CostID"].ToString();
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					//model.FeesID=dt.Rows[n]["FeesID"].ToString();
					//model.ReceID=dt.Rows[n]["ReceID"].ToString();
					model.ChargeMode=dt.Rows[n]["ChargeMode"].ToString();
					if(dt.Rows[n]["AccountWay"].ToString()!="")
					{
						model.AccountWay=int.Parse(dt.Rows[n]["AccountWay"].ToString());
					}
					if(dt.Rows[n]["ChargeAmount"].ToString()!="")
					{
						model.ChargeAmount=decimal.Parse(dt.Rows[n]["ChargeAmount"].ToString());
					}
					if(dt.Rows[n]["LateFeeAmount"].ToString()!="")
					{
						model.LateFeeAmount=decimal.Parse(dt.Rows[n]["LateFeeAmount"].ToString());
					}
					if(dt.Rows[n]["ChargeDate"].ToString()!="")
					{
						model.ChargeDate=DateTime.Parse(dt.Rows[n]["ChargeDate"].ToString());
					}
					model.FeesMemo=dt.Rows[n]["FeesMemo"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					//model.OldCostID=dt.Rows[n]["OldCostID"].ToString();
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

            public void FeesDetailDelete(long RecdID)
            {
                  dal.FeesDetailDelete(RecdID);
            }
	}
}

