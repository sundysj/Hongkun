using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.System;
namespace MobileSoft.BLL.System
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_System_Province 的摘要说明。
	/// </summary>
	public class Bll_Tb_System_Province
	{
		private readonly MobileSoft.DAL.System.Dal_Tb_System_Province dal=new MobileSoft.DAL.System.Dal_Tb_System_Province();
		public Bll_Tb_System_Province()
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
		public bool Exists(int ProvinceID,string ProvinceName)
		{
			return dal.Exists(ProvinceID,ProvinceName);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.System.Tb_System_Province model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Province model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ProvinceID,string ProvinceName)
		{
			
			dal.Delete(ProvinceID,ProvinceName);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Province GetModel(int ProvinceID,string ProvinceName)
		{
			
			return dal.GetModel(ProvinceID,ProvinceName);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Province GetModelByCache(int ProvinceID,string ProvinceName)
		{
			
			string CacheKey = "Tb_System_ProvinceModel-" + ProvinceID+ProvinceName;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ProvinceID,ProvinceName);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.System.Tb_System_Province)objModel;
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
		public List<MobileSoft.Model.System.Tb_System_Province> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.System.Tb_System_Province> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.System.Tb_System_Province> modelList = new List<MobileSoft.Model.System.Tb_System_Province>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.System.Tb_System_Province model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.System.Tb_System_Province();
					if(dt.Rows[n]["ProvinceID"].ToString()!="")
					{
						model.ProvinceID=int.Parse(dt.Rows[n]["ProvinceID"].ToString());
					}
					model.ProvinceName=dt.Rows[n]["ProvinceName"].ToString();
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

