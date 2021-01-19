using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_HSPR_CommActivitiesPerson 的摘要说明。
	/// </summary>
	public class Bll_Tb_HSPR_CommActivitiesPerson
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CommActivitiesPerson dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CommActivitiesPerson();
		public Bll_Tb_HSPR_CommActivitiesPerson()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ActivitiesPersonID)
		{
			return dal.Exists(ActivitiesPersonID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesPerson model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesPerson model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string ActivitiesPersonID)
		{
			
			dal.Delete(ActivitiesPersonID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesPerson GetModel(string ActivitiesPersonID)
		{
			
			return dal.GetModel(ActivitiesPersonID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesPerson GetModelByCache(string ActivitiesPersonID)
		{
			
			string CacheKey = "Tb_HSPR_CommActivitiesPersonModel-" + ActivitiesPersonID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ActivitiesPersonID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesPerson)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesPerson> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesPerson> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesPerson> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesPerson>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesPerson model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesPerson();
					model.ActivitiesPersonID=dt.Rows[n]["ActivitiesPersonID"].ToString();
					model.ActivitiesID=dt.Rows[n]["ActivitiesID"].ToString();
					model.CustName=dt.Rows[n]["CustName"].ToString();
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					model.RoomName=dt.Rows[n]["RoomName"].ToString();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					model.LinkPhone=dt.Rows[n]["LinkPhone"].ToString();
					if(dt.Rows[n]["PersonCount"].ToString()!="")
					{
						model.PersonCount=int.Parse(dt.Rows[n]["PersonCount"].ToString());
					}
					//model.CommID=dt.Rows[n]["CommID"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
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

