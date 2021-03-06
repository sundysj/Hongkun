using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.System;
namespace MobileSoft.BLL.System
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_System_Log 的摘要说明。
	/// </summary>
	public class Bll_Tb_System_Log
	{
		private readonly MobileSoft.DAL.System.Dal_Tb_System_Log dal=new MobileSoft.DAL.System.Dal_Tb_System_Log();
		public Bll_Tb_System_Log()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ManagerCode,long LogCode)
		{
			return dal.Exists(ManagerCode,LogCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MobileSoft.Model.System.Tb_System_Log model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Log model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string ManagerCode,long LogCode)
		{
			
			dal.Delete(ManagerCode,LogCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Log GetModel(string ManagerCode,long LogCode)
		{
			
			return dal.GetModel(ManagerCode,LogCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Log GetModelByCache(string ManagerCode,long LogCode)
		{
			
			string CacheKey = "Tb_System_LogModel-" + ManagerCode+LogCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ManagerCode,LogCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.System.Tb_System_Log)objModel;
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
		public List<MobileSoft.Model.System.Tb_System_Log> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.System.Tb_System_Log> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.System.Tb_System_Log> modelList = new List<MobileSoft.Model.System.Tb_System_Log>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.System.Tb_System_Log model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.System.Tb_System_Log();
					//model.LogCode=dt.Rows[n]["LogCode"].ToString();
					model.ManagerCode=dt.Rows[n]["ManagerCode"].ToString();
					model.LocationIP=dt.Rows[n]["LocationIP"].ToString();
					if(dt.Rows[n]["LogTime"].ToString()!="")
					{
						model.LogTime=DateTime.Parse(dt.Rows[n]["LogTime"].ToString());
					}
					model.PNodeName=dt.Rows[n]["PNodeName"].ToString();
					model.OperateName=dt.Rows[n]["OperateName"].ToString();
					model.OperateURL=dt.Rows[n]["OperateURL"].ToString();
					model.Memo=dt.Rows[n]["Memo"].ToString();
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

