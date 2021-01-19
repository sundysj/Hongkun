using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Sys;
namespace MobileSoft.BLL.Sys
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Sys_RoleData 的摘要说明。
	/// </summary>
	public class Bll_Tb_Sys_RoleData
	{
		private readonly MobileSoft.DAL.Sys.Dal_Tb_Sys_RoleData dal=new MobileSoft.DAL.Sys.Dal_Tb_Sys_RoleData();
		public Bll_Tb_Sys_RoleData()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long IID)
		{
			return dal.Exists(IID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MobileSoft.Model.Sys.Tb_Sys_RoleData model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_RoleData model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long IID)
		{
			
			dal.Delete(IID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_RoleData GetModel(long IID)
		{
			
			return dal.GetModel(IID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_RoleData GetModelByCache(long IID)
		{
			
			string CacheKey = "Tb_Sys_RoleDataModel-" + IID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(IID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Sys.Tb_Sys_RoleData)objModel;
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
		public List<MobileSoft.Model.Sys.Tb_Sys_RoleData> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Sys.Tb_Sys_RoleData> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Sys.Tb_Sys_RoleData> modelList = new List<MobileSoft.Model.Sys.Tb_Sys_RoleData>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Sys.Tb_Sys_RoleData model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Sys.Tb_Sys_RoleData();
					//model.IID=dt.Rows[n]["IID"].ToString();
					model.RoleCode=dt.Rows[n]["RoleCode"].ToString();
					model.OrganCode=dt.Rows[n]["OrganCode"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
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

        public DataTable Sys_User_RoleData_Filter(string SQLEx)
        {
            return dal.Sys_User_RoleData_Filter(SQLEx);
        }
	}
}

