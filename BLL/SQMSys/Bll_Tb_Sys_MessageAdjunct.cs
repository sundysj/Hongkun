using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.SQMSys;
namespace MobileSoft.BLL.SQMSys
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Sys_MessageAdjunct 的摘要说明。
	/// </summary>
	public class Bll_Tb_Sys_MessageAdjunct
	{
		private readonly MobileSoft.DAL.SQMSys.Dal_Tb_Sys_MessageAdjunct dal=new MobileSoft.DAL.SQMSys.Dal_Tb_Sys_MessageAdjunct();
		public Bll_Tb_Sys_MessageAdjunct()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string AdjunctCode)
		{
			return dal.Exists(AdjunctCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_MessageAdjunct model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_MessageAdjunct model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string AdjunctCode)
		{
			
			dal.Delete(AdjunctCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_MessageAdjunct GetModel(string AdjunctCode)
		{
			
			return dal.GetModel(AdjunctCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_MessageAdjunct GetModelByCache(string AdjunctCode)
		{
			
			string CacheKey = "Tb_Sys_MessageAdjunctModel-" + AdjunctCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(AdjunctCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.SQMSys.Tb_Sys_MessageAdjunct)objModel;
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
		public List<MobileSoft.Model.SQMSys.Tb_Sys_MessageAdjunct> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.SQMSys.Tb_Sys_MessageAdjunct> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.SQMSys.Tb_Sys_MessageAdjunct> modelList = new List<MobileSoft.Model.SQMSys.Tb_Sys_MessageAdjunct>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.SQMSys.Tb_Sys_MessageAdjunct model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.SQMSys.Tb_Sys_MessageAdjunct();
					model.AdjunctCode=dt.Rows[n]["AdjunctCode"].ToString();
					if(dt.Rows[n]["MessageCode"].ToString()!="")
					{
						model.MessageCode=new Guid(dt.Rows[n]["MessageCode"].ToString());
					}
					model.AdjunctName=dt.Rows[n]["AdjunctName"].ToString();
					model.FilPath=dt.Rows[n]["FilPath"].ToString();
					model.FileName=dt.Rows[n]["FileName"].ToString();
					model.FileExName=dt.Rows[n]["FileExName"].ToString();
					if(dt.Rows[n]["FileSize"].ToString()!="")
					{
						model.FileSize=decimal.Parse(dt.Rows[n]["FileSize"].ToString());
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

