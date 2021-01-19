using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_HSPR_ParkingCarSignFeesDetail 的摘要说明。
	/// </summary>
	public class Bll_Tb_HSPR_ParkingCarSignFeesDetail
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_ParkingCarSignFeesDetail dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_ParkingCarSignFeesDetail();
		public Bll_Tb_HSPR_ParkingCarSignFeesDetail()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long IID)
		{
			return dal.Exists(IID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignFeesDetail model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignFeesDetail model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long IID)
		{
			
			dal.Delete(IID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignFeesDetail GetModel(long IID)
		{
			
			return dal.GetModel(IID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignFeesDetail GetModelByCache(long IID)
		{
			
			string CacheKey = "Tb_HSPR_ParkingCarSignFeesDetailModel-" + IID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(IID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignFeesDetail)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignFeesDetail> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignFeesDetail> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignFeesDetail> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignFeesDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignFeesDetail model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignFeesDetail();
					//model.IID=dt.Rows[n]["IID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					//model.CostID=dt.Rows[n]["CostID"].ToString();
					//model.ReceID=dt.Rows[n]["ReceID"].ToString();
					//model.RecdID=dt.Rows[n]["RecdID"].ToString();
					//model.ParkID=dt.Rows[n]["ParkID"].ToString();
					//model.HandID=dt.Rows[n]["HandID"].ToString();
					if(dt.Rows[n]["IsHisFees"].ToString()!="")
					{
						model.IsHisFees=int.Parse(dt.Rows[n]["IsHisFees"].ToString());
					}
					if(dt.Rows[n]["FeesStateDate"].ToString()!="")
					{
						model.FeesStateDate=DateTime.Parse(dt.Rows[n]["FeesStateDate"].ToString());
					}
					if(dt.Rows[n]["FeesEndDate"].ToString()!="")
					{
						model.FeesEndDate=DateTime.Parse(dt.Rows[n]["FeesEndDate"].ToString());
					}
					if(dt.Rows[n]["ChargeAmount"].ToString()!="")
					{
						model.ChargeAmount=decimal.Parse(dt.Rows[n]["ChargeAmount"].ToString());
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

