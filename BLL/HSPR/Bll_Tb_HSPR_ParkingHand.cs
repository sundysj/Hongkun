using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_HSPR_ParkingHand 的摘要说明。
	/// </summary>
	public class Bll_Tb_HSPR_ParkingHand
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_ParkingHand dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_ParkingHand();
		public Bll_Tb_HSPR_ParkingHand()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long HandID)
		{
			return dal.Exists(HandID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_ParkingHand model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_ParkingHand model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long HandID)
		{
			
			dal.Delete(HandID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_ParkingHand GetModel(long HandID)
		{
			
			return dal.GetModel(HandID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_ParkingHand GetModelByCache(long HandID)
		{
			
			string CacheKey = "Tb_HSPR_ParkingHandModel-" + HandID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(HandID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_ParkingHand)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_ParkingHand> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_ParkingHand> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_ParkingHand> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_ParkingHand>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_ParkingHand model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_ParkingHand();
					//model.HandID=dt.Rows[n]["HandID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					//model.ParkID=dt.Rows[n]["ParkID"].ToString();
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					if(dt.Rows[n]["ApplyDate"].ToString()!="")
					{
						model.ApplyDate=DateTime.Parse(dt.Rows[n]["ApplyDate"].ToString());
					}
					model.InfoSource=dt.Rows[n]["InfoSource"].ToString();
					model.UseProperty=dt.Rows[n]["UseProperty"].ToString();
					if(dt.Rows[n]["ParkStartDate"].ToString()!="")
					{
						model.ParkStartDate=DateTime.Parse(dt.Rows[n]["ParkStartDate"].ToString());
					}
					if(dt.Rows[n]["ParkEndDate"].ToString()!="")
					{
						model.ParkEndDate=DateTime.Parse(dt.Rows[n]["ParkEndDate"].ToString());
					}
					model.PayPeriod=dt.Rows[n]["PayPeriod"].ToString();
					if(dt.Rows[n]["NextPayDate"].ToString()!="")
					{
						model.NextPayDate=DateTime.Parse(dt.Rows[n]["NextPayDate"].ToString());
					}
					model.RentMode=dt.Rows[n]["RentMode"].ToString();
					model.Contact=dt.Rows[n]["Contact"].ToString();
					model.handling=dt.Rows[n]["handling"].ToString();
					if(dt.Rows[n]["HandDate"].ToString()!="")
					{
						model.HandDate=DateTime.Parse(dt.Rows[n]["HandDate"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["IsSubmit"].ToString()!="")
					{
						model.IsSubmit=int.Parse(dt.Rows[n]["IsSubmit"].ToString());
					}
					model.ParkingCarSign=dt.Rows[n]["ParkingCarSign"].ToString();
					model.CarSign=dt.Rows[n]["CarSign"].ToString();
					model.CarType=dt.Rows[n]["CarType"].ToString();
					model.FacBrands=dt.Rows[n]["FacBrands"].ToString();
					model.CarColor=dt.Rows[n]["CarColor"].ToString();
					model.CarEmission=dt.Rows[n]["CarEmission"].ToString();
					model.Phone=dt.Rows[n]["Phone"].ToString();
					if(dt.Rows[n]["IsUse"].ToString()!="")
					{
						model.IsUse=int.Parse(dt.Rows[n]["IsUse"].ToString());
					}
					if(dt.Rows[n]["IsInput"].ToString()!="")
					{
						model.IsInput=int.Parse(dt.Rows[n]["IsInput"].ToString());
					}
					if(dt.Rows[n]["NextDate"].ToString()!="")
					{
						model.NextDate=DateTime.Parse(dt.Rows[n]["NextDate"].ToString());
					}
					if(dt.Rows[n]["HandSynchCode"].ToString()!="")
					{
						model.HandSynchCode=new Guid(dt.Rows[n]["HandSynchCode"].ToString());
					}
					if(dt.Rows[n]["SynchFlag"].ToString()!="")
					{
						model.SynchFlag=int.Parse(dt.Rows[n]["SynchFlag"].ToString());
					}
					if(dt.Rows[n]["IsListing"].ToString()!="")
					{
						model.IsListing=int.Parse(dt.Rows[n]["IsListing"].ToString());
					}
					if(dt.Rows[n]["ListingData"].ToString()!="")
					{
						model.ListingData=DateTime.Parse(dt.Rows[n]["ListingData"].ToString());
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

