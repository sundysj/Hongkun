using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.SQMSys;
namespace MobileSoft.BLL.SQMSys
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Sys_Department 的摘要说明。
	/// </summary>
	public class Bll_Tb_Sys_Department
	{
		private readonly MobileSoft.DAL.SQMSys.Dal_Tb_Sys_Department dal=new MobileSoft.DAL.SQMSys.Dal_Tb_Sys_Department();
		public Bll_Tb_Sys_Department()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string DepCode,Guid StreetCode)
		{
			return dal.Exists(DepCode,StreetCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_Department model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_Department model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string DepCode,Guid StreetCode)
		{
			
			dal.Delete(DepCode,StreetCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Department GetModel(string DepCode,Guid StreetCode)
		{
			
			return dal.GetModel(DepCode,StreetCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Department GetModelByCache(string DepCode,Guid StreetCode)
		{
			
			string CacheKey = "Tb_Sys_DepartmentModel-" + DepCode+StreetCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(DepCode,StreetCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.SQMSys.Tb_Sys_Department)objModel;
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
		public List<MobileSoft.Model.SQMSys.Tb_Sys_Department> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.SQMSys.Tb_Sys_Department> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.SQMSys.Tb_Sys_Department> modelList = new List<MobileSoft.Model.SQMSys.Tb_Sys_Department>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.SQMSys.Tb_Sys_Department model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.SQMSys.Tb_Sys_Department();
					model.DepCode=dt.Rows[n]["DepCode"].ToString();
					if(dt.Rows[n]["StreetCode"].ToString()!="")
					{
						model.StreetCode=new Guid(dt.Rows[n]["StreetCode"].ToString());
					}
					model.DepName=dt.Rows[n]["DepName"].ToString();
					model.Principal=dt.Rows[n]["Principal"].ToString();
					model.Memo=dt.Rows[n]["Memo"].ToString();
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

