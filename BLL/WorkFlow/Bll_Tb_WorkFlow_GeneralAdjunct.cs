using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.WorkFlow;
namespace MobileSoft.BLL.WorkFlow
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_WorkFlow_GeneralAdjunct 的摘要说明。
	/// </summary>
	public class Bll_Tb_WorkFlow_GeneralAdjunct
	{
		private readonly MobileSoft.DAL.WorkFlow.Dal_Tb_WorkFlow_GeneralAdjunct dal=new MobileSoft.DAL.WorkFlow.Dal_Tb_WorkFlow_GeneralAdjunct();
		public Bll_Tb_WorkFlow_GeneralAdjunct()
		{}
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralAdjunct model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralAdjunct model)
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
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralAdjunct GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel();
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralAdjunct GetModelByCache()
		{
			//该表无主键信息，请自定义主键/条件字段
			string CacheKey = "Tb_WorkFlow_GeneralAdjunctModel-" ;
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
			return (MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralAdjunct)objModel;
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
		public List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralAdjunct> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralAdjunct> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralAdjunct> modelList = new List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralAdjunct>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralAdjunct model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralAdjunct();
					model.GeneralAdjunctCode=dt.Rows[n]["GeneralAdjunctCode"].ToString();
					if(dt.Rows[n]["GeneralCode"].ToString()!="")
					{
						model.GeneralCode=new Guid(dt.Rows[n]["GeneralCode"].ToString());
					}
					model.AdjunctName=dt.Rows[n]["AdjunctName"].ToString();
					model.FilPath=dt.Rows[n]["FilPath"].ToString();
					model.FileExName=dt.Rows[n]["FileExName"].ToString();
					if(dt.Rows[n]["FileSize"].ToString()!="")
					{
						model.FileSize=decimal.Parse(dt.Rows[n]["FileSize"].ToString());
					}
					if(dt.Rows[n]["msrepl_tran_version"].ToString()!="")
					{
						model.msrepl_tran_version=new Guid(dt.Rows[n]["msrepl_tran_version"].ToString());
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

