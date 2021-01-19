using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.SQMSys;
namespace MobileSoft.BLL.SQMSys
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Sys_Role 的摘要说明。
	/// </summary>
	public class Bll_Tb_Sys_Role
	{
		private readonly MobileSoft.DAL.SQMSys.Dal_Tb_Sys_Role dal=new MobileSoft.DAL.SQMSys.Dal_Tb_Sys_Role();
		public Bll_Tb_Sys_Role()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string RoleCode,Guid StreetCode)
		{
			return dal.Exists(RoleCode,StreetCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_Role model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_Role model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string RoleCode,Guid StreetCode)
		{
			
			dal.Delete(RoleCode,StreetCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Role GetModel(string RoleCode,Guid StreetCode)
		{
			
			return dal.GetModel(RoleCode,StreetCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Role GetModelByCache(string RoleCode,Guid StreetCode)
		{
			
			string CacheKey = "Tb_Sys_RoleModel-" + RoleCode+StreetCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(RoleCode,StreetCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.SQMSys.Tb_Sys_Role)objModel;
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
		public List<MobileSoft.Model.SQMSys.Tb_Sys_Role> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.SQMSys.Tb_Sys_Role> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.SQMSys.Tb_Sys_Role> modelList = new List<MobileSoft.Model.SQMSys.Tb_Sys_Role>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.SQMSys.Tb_Sys_Role model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.SQMSys.Tb_Sys_Role();
					model.RoleCode=dt.Rows[n]["RoleCode"].ToString();
					if(dt.Rows[n]["StreetCode"].ToString()!="")
					{
						model.StreetCode=new Guid(dt.Rows[n]["StreetCode"].ToString());
					}
					model.RoleName=dt.Rows[n]["RoleName"].ToString();
					model.RoleDescribe=dt.Rows[n]["RoleDescribe"].ToString();
					if(dt.Rows[n]["IsSysRole"].ToString()!="")
					{
						model.IsSysRole=int.Parse(dt.Rows[n]["IsSysRole"].ToString());
					}
					model.DepCode=dt.Rows[n]["DepCode"].ToString();
					model.SysRoleCode=dt.Rows[n]["SysRoleCode"].ToString();
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

