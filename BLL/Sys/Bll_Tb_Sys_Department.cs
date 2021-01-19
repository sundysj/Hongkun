using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Sys;
namespace MobileSoft.BLL.Sys
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Sys_Department 的摘要说明。
	/// </summary>
	public class Bll_Tb_Sys_Department
	{
		private readonly MobileSoft.DAL.Sys.Dal_Tb_Sys_Department dal=new MobileSoft.DAL.Sys.Dal_Tb_Sys_Department();
		public Bll_Tb_Sys_Department()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string DepCode)
		{
			return dal.Exists(DepCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Sys.Tb_Sys_Department model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_Department model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string DepCode)
		{
			
			dal.Delete(DepCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_Department GetModel(string DepCode)
		{
			
			return dal.GetModel(DepCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_Department GetModelByCache(string DepCode)
		{
			
			string CacheKey = "Tb_Sys_DepartmentModel-" + DepCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(DepCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Sys.Tb_Sys_Department)objModel;
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
		public List<MobileSoft.Model.Sys.Tb_Sys_Department> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Sys.Tb_Sys_Department> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Sys.Tb_Sys_Department> modelList = new List<MobileSoft.Model.Sys.Tb_Sys_Department>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Sys.Tb_Sys_Department model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Sys.Tb_Sys_Department();
					model.DepCode=dt.Rows[n]["DepCode"].ToString();
					model.DepName=dt.Rows[n]["DepName"].ToString();
					model.Principal=dt.Rows[n]["Principal"].ToString();
					model.Memo=dt.Rows[n]["Memo"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["Sort"].ToString()!="")
					{
						model.Sort=int.Parse(dt.Rows[n]["Sort"].ToString());
					}
					if(dt.Rows[n]["IsHide"].ToString()!="")
					{
						model.IsHide=int.Parse(dt.Rows[n]["IsHide"].ToString());
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

