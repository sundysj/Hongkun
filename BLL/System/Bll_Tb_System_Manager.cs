using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.System;
namespace MobileSoft.BLL.System
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_System_Manager 的摘要说明。
	/// </summary>
	public class Bll_Tb_System_Manager
	{
		private readonly MobileSoft.DAL.System.Dal_Tb_System_Manager dal=new MobileSoft.DAL.System.Dal_Tb_System_Manager();
		public Bll_Tb_System_Manager()
		{}
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.System.Tb_System_Manager model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Manager model)
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
		public MobileSoft.Model.System.Tb_System_Manager GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel();
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Manager GetModelByCache()
		{
			//该表无主键信息，请自定义主键/条件字段
			string CacheKey = "Tb_System_ManagerModel-" ;
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
			return (MobileSoft.Model.System.Tb_System_Manager)objModel;
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
		public List<MobileSoft.Model.System.Tb_System_Manager> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.System.Tb_System_Manager> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.System.Tb_System_Manager> modelList = new List<MobileSoft.Model.System.Tb_System_Manager>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.System.Tb_System_Manager model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.System.Tb_System_Manager();
					model.ManagerCode=dt.Rows[n]["ManagerCode"].ToString();
					model.ManagerType=dt.Rows[n]["ManagerType"].ToString();
					model.ManagerName=dt.Rows[n]["ManagerName"].ToString();
					model.LoginCode=dt.Rows[n]["LoginCode"].ToString();
					model.LoginPassWD=dt.Rows[n]["LoginPassWD"].ToString();
					model.IPAddress=dt.Rows[n]["IPAddress"].ToString();
					model.MobileTel=dt.Rows[n]["MobileTel"].ToString();
					model.EMail=dt.Rows[n]["EMail"].ToString();
					model.Province=dt.Rows[n]["Province"].ToString();
					model.City=dt.Rows[n]["City"].ToString();
					model.Borough=dt.Rows[n]["Borough"].ToString();
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

