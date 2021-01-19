using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Sys;
namespace MobileSoft.BLL.Sys
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Sys_TakePicPark 的摘要说明。
	/// </summary>
	public class Bll_Tb_Sys_TakePicPark
	{
		private readonly MobileSoft.DAL.Sys.Dal_Tb_Sys_TakePicPark dal=new MobileSoft.DAL.Sys.Dal_Tb_Sys_TakePicPark();
		public Bll_Tb_Sys_TakePicPark()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long StatID)
		{
			return dal.Exists(StatID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MobileSoft.Model.Sys.Tb_Sys_TakePicPark model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_TakePicPark model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long StatID)
		{
			
			dal.Delete(StatID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePicPark GetModel(long StatID)
		{
			
			return dal.GetModel(StatID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePicPark GetModelByCache(long StatID)
		{
			
			string CacheKey = "Tb_Sys_TakePicParkModel-" + StatID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(StatID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Sys.Tb_Sys_TakePicPark)objModel;
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
		public List<MobileSoft.Model.Sys.Tb_Sys_TakePicPark> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Sys.Tb_Sys_TakePicPark> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Sys.Tb_Sys_TakePicPark> modelList = new List<MobileSoft.Model.Sys.Tb_Sys_TakePicPark>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Sys.Tb_Sys_TakePicPark model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Sys.Tb_Sys_TakePicPark();
					//model.StatID=dt.Rows[n]["StatID"].ToString();
					if(dt.Rows[n]["StatType"].ToString()!="")
					{
						model.StatType=int.Parse(dt.Rows[n]["StatType"].ToString());
					}
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.OrganCode=dt.Rows[n]["OrganCode"].ToString();
					if(dt.Rows[n]["StatDate"].ToString()!="")
					{
						model.StatDate=DateTime.Parse(dt.Rows[n]["StatDate"].ToString());
					}
					//model.CarparkID=dt.Rows[n]["CarparkID"].ToString();
					model.ParkType=dt.Rows[n]["ParkType"].ToString();
					if(dt.Rows[n]["Counts"].ToString()!="")
					{
						model.Counts=int.Parse(dt.Rows[n]["Counts"].ToString());
					}
					if(dt.Rows[n]["Area"].ToString()!="")
					{
						model.Area=decimal.Parse(dt.Rows[n]["Area"].ToString());
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

