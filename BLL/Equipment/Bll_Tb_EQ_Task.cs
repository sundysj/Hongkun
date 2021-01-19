using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using HM.Model.Eq;
namespace HM.BLL.Eq
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_EQ_Task 的摘要说明。
	/// </summary>
	public class Bll_Tb_EQ_Task
	{
		private readonly HM.DAL.Eq.Dal_Tb_EQ_Task dal=new HM.DAL.Eq.Dal_Tb_EQ_Task();
		public Bll_Tb_EQ_Task()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string TaskId)
		{
			return dal.Exists(TaskId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(HM.Model.Eq.Tb_EQ_Task model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(HM.Model.Eq.Tb_EQ_Task model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string TaskId)
		{
			
			dal.Delete(TaskId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public HM.Model.Eq.Tb_EQ_Task GetModel(string TaskId)
		{
			
			return dal.GetModel(TaskId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public HM.Model.Eq.Tb_EQ_Task GetModelByCache(string TaskId)
		{
			
			string CacheKey = "Tb_EQ_TaskModel-" + TaskId;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(TaskId);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (HM.Model.Eq.Tb_EQ_Task)objModel;
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
		public List<HM.Model.Eq.Tb_EQ_Task> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<HM.Model.Eq.Tb_EQ_Task> DataTableToList(DataTable dt)
		{
			List<HM.Model.Eq.Tb_EQ_Task> modelList = new List<HM.Model.Eq.Tb_EQ_Task>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				HM.Model.Eq.Tb_EQ_Task model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new HM.Model.Eq.Tb_EQ_Task();
					model.TaskId=dt.Rows[n]["TaskId"].ToString();
					if(dt.Rows[n]["CommId"].ToString()!="")
					{
						model.CommId=int.Parse(dt.Rows[n]["CommId"].ToString());
					}
					model.PlanId=dt.Rows[n]["PlanId"].ToString();
					model.TaskNO=dt.Rows[n]["TaskNO"].ToString();
					model.EqId=dt.Rows[n]["EqId"].ToString();
					model.SpaceId=dt.Rows[n]["SpaceId"].ToString();
					model.Content=dt.Rows[n]["Content"].ToString();
					if(dt.Rows[n]["BeginTime"].ToString()!="")
					{
						model.BeginTime=DateTime.Parse(dt.Rows[n]["BeginTime"].ToString());
					}
					if(dt.Rows[n]["EndTime"].ToString()!="")
					{
						model.EndTime=DateTime.Parse(dt.Rows[n]["EndTime"].ToString());
					}
					model.WorkDayBegin=dt.Rows[n]["WorkDayBegin"].ToString();
					model.RoleCode=dt.Rows[n]["RoleCode"].ToString();
					model.Statue=dt.Rows[n]["Statue"].ToString();
					model.ClosePerson=dt.Rows[n]["ClosePerson"].ToString();
					if(dt.Rows[n]["CloseTime"].ToString()!="")
					{
						model.CloseTime=DateTime.Parse(dt.Rows[n]["CloseTime"].ToString());
					}
					model.CloseReason=dt.Rows[n]["CloseReason"].ToString();
					model.PollingPerson=dt.Rows[n]["PollingPerson"].ToString();
					if(dt.Rows[n]["PollingDate"].ToString()!="")
					{
						model.PollingDate=DateTime.Parse(dt.Rows[n]["PollingDate"].ToString());
					}
					model.Remark=dt.Rows[n]["Remark"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					model.AddPId=dt.Rows[n]["AddPId"].ToString();
					if(dt.Rows[n]["AddDate"].ToString()!="")
					{
						model.AddDate=DateTime.Parse(dt.Rows[n]["AddDate"].ToString());
					}
					model.PerfNum=dt.Rows[n]["PerfNum"].ToString();
					model.WorkTime=dt.Rows[n]["WorkTime"].ToString();
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

