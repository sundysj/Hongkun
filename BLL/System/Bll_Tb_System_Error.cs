using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.System;
namespace MobileSoft.BLL.System
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_System_Error 的摘要说明。
	/// </summary>
	public class Bll_Tb_System_Error
	{
		private readonly MobileSoft.DAL.System.Dal_Tb_System_Error dal=new MobileSoft.DAL.System.Dal_Tb_System_Error();
		public Bll_Tb_System_Error()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid ErrorCode)
		{
			return dal.Exists(ErrorCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.System.Tb_System_Error model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Error model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Guid ErrorCode)
		{
			
			dal.Delete(ErrorCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Error GetModel(Guid ErrorCode)
		{
			
			return dal.GetModel(ErrorCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Error GetModelByCache(Guid ErrorCode)
		{
			
			string CacheKey = "Tb_System_ErrorModel-" + ErrorCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ErrorCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.System.Tb_System_Error)objModel;
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
		public List<MobileSoft.Model.System.Tb_System_Error> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.System.Tb_System_Error> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.System.Tb_System_Error> modelList = new List<MobileSoft.Model.System.Tb_System_Error>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.System.Tb_System_Error model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.System.Tb_System_Error();
					if(dt.Rows[n]["ErrorCode"].ToString()!="")
					{
						model.ErrorCode=new Guid(dt.Rows[n]["ErrorCode"].ToString());
					}
					if(dt.Rows[n]["ErrorTime"].ToString()!="")
					{
						model.ErrorTime=DateTime.Parse(dt.Rows[n]["ErrorTime"].ToString());
					}
					model.ErrorURL=dt.Rows[n]["ErrorURL"].ToString();
					model.ErrorSource=dt.Rows[n]["ErrorSource"].ToString();
					model.ErrorMessage=dt.Rows[n]["ErrorMessage"].ToString();
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

