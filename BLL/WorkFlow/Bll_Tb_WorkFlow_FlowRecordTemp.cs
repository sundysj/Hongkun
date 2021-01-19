using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.WorkFlow;
namespace MobileSoft.BLL.WorkFlow
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_WorkFlow_FlowRecordTemp 的摘要说明。
	/// </summary>
	public class Bll_Tb_WorkFlow_FlowRecordTemp
	{
		private readonly MobileSoft.DAL.WorkFlow.Dal_Tb_WorkFlow_FlowRecordTemp dal=new MobileSoft.DAL.WorkFlow.Dal_Tb_WorkFlow_FlowRecordTemp();
		public Bll_Tb_WorkFlow_FlowRecordTemp()
		{}
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowRecordTemp model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowRecordTemp model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			dal.Delete();
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowRecordTemp GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel();
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowRecordTemp GetModelByCache()
		{
			//该表无主键信息，请自定义主键/条件字段
			string CacheKey = "Tb_WorkFlow_FlowRecordTempModel-" ;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel();
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowRecordTemp)objModel;
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
		public List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowRecordTemp> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowRecordTemp> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowRecordTemp> modelList = new List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowRecordTemp>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowRecordTemp model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowRecordTemp();
					if(dt.Rows[n]["Tb_WorkFlow_FlowNode_InfoId"].ToString()!="")
					{
						model.Tb_WorkFlow_FlowNode_InfoId=int.Parse(dt.Rows[n]["Tb_WorkFlow_FlowNode_InfoId"].ToString());
					}
					model.UserCode=dt.Rows[n]["UserCode"].ToString();
					model.RecordContent=dt.Rows[n]["RecordContent"].ToString();
					model.OprState=dt.Rows[n]["OprState"].ToString();
					if(dt.Rows[n]["WorkDate"].ToString()!="")
					{
						model.WorkDate=DateTime.Parse(dt.Rows[n]["WorkDate"].ToString());
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

