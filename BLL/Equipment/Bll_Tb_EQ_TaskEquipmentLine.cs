using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using HM.Model.Eq;
namespace HM.BLL.Eq
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_EQ_TaskEquipmentLine 的摘要说明。
	/// </summary>
	public class Bll_Tb_EQ_TaskEquipmentLine
	{
		private readonly HM.DAL.Eq.Dal_Tb_EQ_TaskEquipmentLine dal=new HM.DAL.Eq.Dal_Tb_EQ_TaskEquipmentLine();
		public Bll_Tb_EQ_TaskEquipmentLine()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string TaskLineId)
		{
			return dal.Exists(TaskLineId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(HM.Model.Eq.Tb_EQ_TaskEquipmentLine model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(HM.Model.Eq.Tb_EQ_TaskEquipmentLine model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string TaskLineId)
		{
			
			dal.Delete(TaskLineId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public HM.Model.Eq.Tb_EQ_TaskEquipmentLine GetModel(string TaskLineId)
		{
			
			return dal.GetModel(TaskLineId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public HM.Model.Eq.Tb_EQ_TaskEquipmentLine GetModelByCache(string TaskLineId)
		{
			
			string CacheKey = "Tb_EQ_TaskEquipmentLineModel-" + TaskLineId;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(TaskLineId);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (HM.Model.Eq.Tb_EQ_TaskEquipmentLine)objModel;
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
		public List<HM.Model.Eq.Tb_EQ_TaskEquipmentLine> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<HM.Model.Eq.Tb_EQ_TaskEquipmentLine> DataTableToList(DataTable dt)
		{
			List<HM.Model.Eq.Tb_EQ_TaskEquipmentLine> modelList = new List<HM.Model.Eq.Tb_EQ_TaskEquipmentLine>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				HM.Model.Eq.Tb_EQ_TaskEquipmentLine model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new HM.Model.Eq.Tb_EQ_TaskEquipmentLine();
					model.TaskLineId=dt.Rows[n]["TaskLineId"].ToString();
					model.TaskId=dt.Rows[n]["TaskId"].ToString();
					model.StanId=dt.Rows[n]["StanId"].ToString();
					model.DetailId=dt.Rows[n]["DetailId"].ToString();
					model.EquiId=dt.Rows[n]["EquiId"].ToString();
					model.PollingNote=dt.Rows[n]["PollingNote"].ToString();
					model.ChooseValue=dt.Rows[n]["ChooseValue"].ToString();
					if(dt.Rows[n]["NumValue"].ToString()!="")
					{
						model.NumValue=decimal.Parse(dt.Rows[n]["NumValue"].ToString());
					}
					model.TextValue=dt.Rows[n]["TextValue"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["AddDate"].ToString()!="")
					{
						model.AddDate=DateTime.Parse(dt.Rows[n]["AddDate"].ToString());
					}
					model.AddPId=dt.Rows[n]["AddPId"].ToString();
					if(dt.Rows[n]["Sort"].ToString()!="")
					{
						model.Sort=int.Parse(dt.Rows[n]["Sort"].ToString());
					}
					model.ReferenceValue=dt.Rows[n]["ReferenceValue"].ToString();
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

