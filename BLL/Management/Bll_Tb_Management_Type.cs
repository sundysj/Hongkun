using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Management;
namespace MobileSoft.BLL.Management
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Management_Type 的摘要说明。
	/// </summary>
	public class Bll_Tb_Management_Type
	{
		private readonly MobileSoft.DAL.Management.Dal_Tb_Management_Type dal=new MobileSoft.DAL.Management.Dal_Tb_Management_Type();
		public Bll_Tb_Management_Type()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string SortCode)
		{
			return dal.Exists(SortCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Management.Tb_Management_Type model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Management.Tb_Management_Type model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string SortCode)
		{
			
			dal.Delete(SortCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Management.Tb_Management_Type GetModel(string SortCode)
		{
			
			return dal.GetModel(SortCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Management.Tb_Management_Type GetModelByCache(string SortCode)
		{
			
			string CacheKey = "Tb_Management_TypeModel-" + SortCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(SortCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Management.Tb_Management_Type)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Management.Tb_Management_Type> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Management.Tb_Management_Type> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Management.Tb_Management_Type> modelList = new List<MobileSoft.Model.Management.Tb_Management_Type>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Management.Tb_Management_Type model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Management.Tb_Management_Type();
					model.Code=dt.Rows[n]["Code"].ToString();
					model.SortCode=dt.Rows[n]["SortCode"].ToString();
					if(dt.Rows[n]["Sort"].ToString()!="")
					{
						model.Sort=int.Parse(dt.Rows[n]["Sort"].ToString());
					}
					model.TypeName=dt.Rows[n]["TypeName"].ToString();
					model.TypeCode=dt.Rows[n]["TypeCode"].ToString();
					model.Memo=dt.Rows[n]["Memo"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
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

