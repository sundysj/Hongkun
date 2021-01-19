using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_HSPR_Parking 的摘要说明。
	/// </summary>
	public class Bll_Tb_HSPR_Parking
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_Parking dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_Parking();
		public Bll_Tb_HSPR_Parking()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ParkID)
		{
			return dal.Exists(ParkID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_Parking model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_Parking model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ParkID)
		{
			
			dal.Delete(ParkID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_Parking GetModel(long ParkID)
		{
			
			return dal.GetModel(ParkID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_Parking GetModelByCache(long ParkID)
		{
			
			string CacheKey = "Tb_HSPR_ParkingModel-" + ParkID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ParkID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_Parking)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere,string TableName)
        {
            return dal.GetList(strWhere,TableName);
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_Parking> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_Parking> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_Parking> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_Parking>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_Parking model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_Parking();
					//model.ParkID=dt.Rows[n]["ParkID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					model.ParkType=dt.Rows[n]["ParkType"].ToString();
					if(dt.Rows[n]["ParkArea"].ToString()!="")
					{
						model.ParkArea=decimal.Parse(dt.Rows[n]["ParkArea"].ToString());
					}
					if(dt.Rows[n]["CarparkID"].ToString()!="")
					{
						model.CarparkID=int.Parse(dt.Rows[n]["CarparkID"].ToString());
					}
					model.ParkName=dt.Rows[n]["ParkName"].ToString();
					if(dt.Rows[n]["ParkingNum"].ToString()!="")
					{
						model.ParkingNum=int.Parse(dt.Rows[n]["ParkingNum"].ToString());
					}
					model.PropertyRight=dt.Rows[n]["PropertyRight"].ToString();
					//model.StanID=dt.Rows[n]["StanID"].ToString();
					model.PropertyUses=dt.Rows[n]["PropertyUses"].ToString();
					model.UseState=dt.Rows[n]["UseState"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["ParkCategory"].ToString()!="")
					{
						model.ParkCategory=int.Parse(dt.Rows[n]["ParkCategory"].ToString());
					}
					if(dt.Rows[n]["ResideType"].ToString()!="")
					{
						model.ResideType=int.Parse(dt.Rows[n]["ResideType"].ToString());
					}
					if(dt.Rows[n]["ParkSynchCode"].ToString()!="")
					{
						model.ParkSynchCode=new Guid(dt.Rows[n]["ParkSynchCode"].ToString());
					}
					if(dt.Rows[n]["SynchFlag"].ToString()!="")
					{
						model.SynchFlag=int.Parse(dt.Rows[n]["SynchFlag"].ToString());
					}
					model.ParkMemo=dt.Rows[n]["ParkMemo"].ToString();
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

