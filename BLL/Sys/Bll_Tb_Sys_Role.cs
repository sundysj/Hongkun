using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Sys;
namespace MobileSoft.BLL.Sys
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Sys_Role ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Sys_Role
	{
		private readonly MobileSoft.DAL.Sys.Dal_Tb_Sys_Role dal=new MobileSoft.DAL.Sys.Dal_Tb_Sys_Role();
		public Bll_Tb_Sys_Role()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string RoleCode)
		{
			return dal.Exists(RoleCode);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.Sys.Tb_Sys_Role model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_Role model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string RoleCode)
		{
			
			dal.Delete(RoleCode);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_Role GetModel(string RoleCode)
		{
			
			return dal.GetModel(RoleCode);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_Role GetModelByCache(string RoleCode)
		{
			
			string CacheKey = "Tb_Sys_RoleModel-" + RoleCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(RoleCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Sys.Tb_Sys_Role)objModel;
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string fieldOrder)
		{
			return dal.GetList(Top,strWhere,fieldOrder);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Sys.Tb_Sys_Role> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Sys.Tb_Sys_Role> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Sys.Tb_Sys_Role> modelList = new List<MobileSoft.Model.Sys.Tb_Sys_Role>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Sys.Tb_Sys_Role model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Sys.Tb_Sys_Role();
					model.RoleCode=dt.Rows[n]["RoleCode"].ToString();
					model.RoleName=dt.Rows[n]["RoleName"].ToString();
					model.RoleDescribe=dt.Rows[n]["RoleDescribe"].ToString();
					if(dt.Rows[n]["IsSysRole"].ToString()!="")
					{
						model.IsSysRole=int.Parse(dt.Rows[n]["IsSysRole"].ToString());
					}
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.DepCode=dt.Rows[n]["DepCode"].ToString();
					model.SysRoleCode=dt.Rows[n]["SysRoleCode"].ToString();
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize,string SortField,int Sort)
		{
			return dal.GetList(out PageCount, out Counts, StrCondition, PageIndex, PageSize,SortField,Sort);
		}

		#endregion  ��Ա����
	}
}
