using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.WorkFlow;
namespace MobileSoft.BLL.WorkFlow
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_WorkFlow_GeneralMain 的摘要说明。
	/// </summary>
	public class Bll_Tb_WorkFlow_GeneralMain
	{
		private readonly MobileSoft.DAL.WorkFlow.Dal_Tb_WorkFlow_GeneralMain dal=new MobileSoft.DAL.WorkFlow.Dal_Tb_WorkFlow_GeneralMain();
		public Bll_Tb_WorkFlow_GeneralMain()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long CutID)
		{
			return dal.Exists(CutID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long CutID)
		{
			
			dal.Delete(CutID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain GetModel(long CutID)
		{
			
			return dal.GetModel(CutID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain GetModelByCache(long CutID)
		{
			
			string CacheKey = "Tb_WorkFlow_GeneralMainModel-" + CutID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CutID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain)objModel;
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
		public List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain> modelList = new List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain();
					if(dt.Rows[n]["GeneralMainCode"].ToString()!="")
					{
						model.GeneralMainCode=new Guid(dt.Rows[n]["GeneralMainCode"].ToString());
					}
					//model.CutID=dt.Rows[n]["CutID"].ToString();
					model.Title=dt.Rows[n]["Title"].ToString();
					model.FlowDegree=dt.Rows[n]["FlowDegree"].ToString();
					model.Content=dt.Rows[n]["Content"].ToString();
					model.Memo=dt.Rows[n]["Memo"].ToString();
					model.DrafMan=dt.Rows[n]["DrafMan"].ToString();
					if(dt.Rows[n]["DrafDate"].ToString()!="")
					{
						model.DrafDate=DateTime.Parse(dt.Rows[n]["DrafDate"].ToString());
					}
					if(dt.Rows[n]["FinishDate"].ToString()!="")
					{
						model.FinishDate=DateTime.Parse(dt.Rows[n]["FinishDate"].ToString());
					}
					if(dt.Rows[n]["WorkState"].ToString()!="")
					{
						model.WorkState=int.Parse(dt.Rows[n]["WorkState"].ToString());
					}
					model.DocumentID=dt.Rows[n]["DocumentID"].ToString();
					if(dt.Rows[n]["msrepl_tran_version"].ToString()!="")
					{
						model.msrepl_tran_version=new Guid(dt.Rows[n]["msrepl_tran_version"].ToString());
					}
					model.TYPE=dt.Rows[n]["TYPE"].ToString();
					model.DocumentTypeCode=dt.Rows[n]["DocumentTypeCode"].ToString();
					if(dt.Rows[n]["DocType"].ToString()!="")
					{
						model.DocType=int.Parse(dt.Rows[n]["DocType"].ToString());
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

