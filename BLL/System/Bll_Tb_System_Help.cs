using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.System;
namespace MobileSoft.BLL.System
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_System_Help 的摘要说明。
	/// </summary>
	public class Bll_Tb_System_Help
	{
		private readonly MobileSoft.DAL.System.Dal_Tb_System_Help dal=new MobileSoft.DAL.System.Dal_Tb_System_Help();
		public Bll_Tb_System_Help()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CorpID,string PNodeCode,long IID)
		{
			return dal.Exists(CorpID,PNodeCode,IID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MobileSoft.Model.System.Tb_System_Help model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Help model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int CorpID,string PNodeCode,long IID)
		{
			
			dal.Delete(CorpID,PNodeCode,IID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Help GetModel(int CorpID,string PNodeCode,long IID)
		{
			
			return dal.GetModel(CorpID,PNodeCode,IID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Help GetModelByCache(int CorpID,string PNodeCode,long IID)
		{
			
			string CacheKey = "Tb_System_HelpModel-" + CorpID+PNodeCode+IID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CorpID,PNodeCode,IID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.System.Tb_System_Help)objModel;
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
		public List<MobileSoft.Model.System.Tb_System_Help> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.System.Tb_System_Help> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.System.Tb_System_Help> modelList = new List<MobileSoft.Model.System.Tb_System_Help>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.System.Tb_System_Help model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.System.Tb_System_Help();
					//model.IID=dt.Rows[n]["IID"].ToString();
					if(dt.Rows[n]["CorpID"].ToString()!="")
					{
						model.CorpID=int.Parse(dt.Rows[n]["CorpID"].ToString());
					}
					model.PNodeCode=dt.Rows[n]["PNodeCode"].ToString();
					model.HelpTitle=dt.Rows[n]["HelpTitle"].ToString();
					model.HelpContent=dt.Rows[n]["HelpContent"].ToString();
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

