using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.SQMSys;
namespace MobileSoft.BLL.SQMSys
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Sys_UserCalendar 的摘要说明。
	/// </summary>
	public class Bll_Tb_Sys_UserCalendar
	{
		private readonly MobileSoft.DAL.SQMSys.Dal_Tb_Sys_UserCalendar dal=new MobileSoft.DAL.SQMSys.Dal_Tb_Sys_UserCalendar();
		public Bll_Tb_Sys_UserCalendar()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid UserCalendarCode)
		{
			return dal.Exists(UserCalendarCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_UserCalendar model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_UserCalendar model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Guid UserCalendarCode)
		{
			
			dal.Delete(UserCalendarCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_UserCalendar GetModel(Guid UserCalendarCode)
		{
			
			return dal.GetModel(UserCalendarCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_UserCalendar GetModelByCache(Guid UserCalendarCode)
		{
			
			string CacheKey = "Tb_Sys_UserCalendarModel-" + UserCalendarCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(UserCalendarCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.SQMSys.Tb_Sys_UserCalendar)objModel;
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
		public List<MobileSoft.Model.SQMSys.Tb_Sys_UserCalendar> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.SQMSys.Tb_Sys_UserCalendar> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.SQMSys.Tb_Sys_UserCalendar> modelList = new List<MobileSoft.Model.SQMSys.Tb_Sys_UserCalendar>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.SQMSys.Tb_Sys_UserCalendar model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.SQMSys.Tb_Sys_UserCalendar();
					if(dt.Rows[n]["UserCalendarCode"].ToString()!="")
					{
						model.UserCalendarCode=new Guid(dt.Rows[n]["UserCalendarCode"].ToString());
					}
					model.UserCode=dt.Rows[n]["UserCode"].ToString();
					model.Title=dt.Rows[n]["Title"].ToString();
					model.Place=dt.Rows[n]["Place"].ToString();
					if(dt.Rows[n]["StartTime"].ToString()!="")
					{
						model.StartTime=DateTime.Parse(dt.Rows[n]["StartTime"].ToString());
					}
					if(dt.Rows[n]["EndTime"].ToString()!="")
					{
						model.EndTime=DateTime.Parse(dt.Rows[n]["EndTime"].ToString());
					}
					model.Scratchpad=dt.Rows[n]["Scratchpad"].ToString();
					if(dt.Rows[n]["IsRemind"].ToString()!="")
					{
						model.IsRemind=int.Parse(dt.Rows[n]["IsRemind"].ToString());
					}
					if(dt.Rows[n]["RemindHours"].ToString()!="")
					{
						model.RemindHours=decimal.Parse(dt.Rows[n]["RemindHours"].ToString());
					}
					if(dt.Rows[n]["RemindState"].ToString()!="")
					{
						model.RemindState=int.Parse(dt.Rows[n]["RemindState"].ToString());
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

