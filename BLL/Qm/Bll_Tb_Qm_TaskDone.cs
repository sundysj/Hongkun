using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using HM.Model.Qm;
namespace HM.BLL.Qm
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Qm_TaskDone 的摘要说明。
	/// </summary>
	public class Bll_Tb_Qm_TaskDone
	{
		private readonly HM.DAL.Qm.Dal_Tb_Qm_TaskDone dal=new HM.DAL.Qm.Dal_Tb_Qm_TaskDone();
		public Bll_Tb_Qm_TaskDone()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(HM.Model.Qm.Tb_Qm_TaskDone model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(HM.Model.Qm.Tb_Qm_TaskDone model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string Id)
		{
			
			dal.Delete(Id);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public HM.Model.Qm.Tb_Qm_TaskDone GetModel(string Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public HM.Model.Qm.Tb_Qm_TaskDone GetModelByCache(string Id)
		{
			
			string CacheKey = "Tb_Qm_TaskDoneModel-" + Id;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Id);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (HM.Model.Qm.Tb_Qm_TaskDone)objModel;
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
		public List<HM.Model.Qm.Tb_Qm_TaskDone> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<HM.Model.Qm.Tb_Qm_TaskDone> DataTableToList(DataTable dt)
		{
			List<HM.Model.Qm.Tb_Qm_TaskDone> modelList = new List<HM.Model.Qm.Tb_Qm_TaskDone>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				HM.Model.Qm.Tb_Qm_TaskDone model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new HM.Model.Qm.Tb_Qm_TaskDone();
					model.Id=dt.Rows[n]["Id"].ToString();
					model.TaskId=dt.Rows[n]["TaskId"].ToString();
					model.PointIds=dt.Rows[n]["PointIds"].ToString();
					model.Pictures=dt.Rows[n]["Pictures"].ToString();
					model.AddPId=dt.Rows[n]["AddPId"].ToString();
					if(dt.Rows[n]["AddTime"].ToString()!="")
					{
						model.AddTime=DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
					}
					model.Remark=dt.Rows[n]["Remark"].ToString();
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

