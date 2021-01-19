using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.WorkFlow;
namespace MobileSoft.BLL.WorkFlow
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_WorkFlow_GeneralMain_Hasten 的摘要说明。
	/// </summary>
	public class Bll_Tb_WorkFlow_GeneralMain_Hasten
	{
		private readonly MobileSoft.DAL.WorkFlow.Dal_Tb_WorkFlow_GeneralMain_Hasten dal=new MobileSoft.DAL.WorkFlow.Dal_Tb_WorkFlow_GeneralMain_Hasten();
		public Bll_Tb_WorkFlow_GeneralMain_Hasten()
		{}
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain_Hasten model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain_Hasten model)
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
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain_Hasten GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel();
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain_Hasten GetModelByCache()
		{
			//该表无主键信息，请自定义主键/条件字段
			string CacheKey = "Tb_WorkFlow_GeneralMain_HastenModel-" ;
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
			return (MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain_Hasten)objModel;
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
		public List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain_Hasten> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain_Hasten> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain_Hasten> modelList = new List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain_Hasten>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain_Hasten model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain_Hasten();
					if(dt.Rows[n]["GeneralMainCode"].ToString()!="")
					{
						model.GeneralMainCode=new Guid(dt.Rows[n]["GeneralMainCode"].ToString());
					}
					model.Title=dt.Rows[n]["Title"].ToString();
					model.FQR=dt.Rows[n]["FQR"].ToString();
					model.CBR=dt.Rows[n]["CBR"].ToString();
					model.BCBBM=dt.Rows[n]["BCBBM"].ToString();
					model.BCBR=dt.Rows[n]["BCBR"].ToString();
					if(dt.Rows[n]["FQSJ"].ToString()!="")
					{
						model.FQSJ=DateTime.Parse(dt.Rows[n]["FQSJ"].ToString());
					}
					model.ZXBLR=dt.Rows[n]["ZXBLR"].ToString();
					if(dt.Rows[n]["ZXBLSJ"].ToString()!="")
					{
						model.ZXBLSJ=DateTime.Parse(dt.Rows[n]["ZXBLSJ"].ToString());
					}
					if(dt.Rows[n]["CutID"].ToString()!="")
					{
						model.CutID=int.Parse(dt.Rows[n]["CutID"].ToString());
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

