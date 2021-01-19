using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Sys;
namespace MobileSoft.BLL.Sys
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Sys_UserRole ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Sys_UserRole
	{
		private readonly MobileSoft.DAL.Sys.Dal_Tb_Sys_UserRole dal=new MobileSoft.DAL.Sys.Dal_Tb_Sys_UserRole();
		public Bll_Tb_Sys_UserRole()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(Guid UserRoleCode)
		{
			return dal.Exists(UserRoleCode);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.Sys.Tb_Sys_UserRole model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_UserRole model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(Guid UserRoleCode)
		{
			
			dal.Delete(UserRoleCode);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_UserRole GetModel(Guid UserRoleCode)
		{
			
			return dal.GetModel(UserRoleCode);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_UserRole GetModelByCache(Guid UserRoleCode)
		{
			
			string CacheKey = "Tb_Sys_UserRoleModel-" + UserRoleCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(UserRoleCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Sys.Tb_Sys_UserRole)objModel;
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
		public List<MobileSoft.Model.Sys.Tb_Sys_UserRole> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Sys.Tb_Sys_UserRole> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Sys.Tb_Sys_UserRole> modelList = new List<MobileSoft.Model.Sys.Tb_Sys_UserRole>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Sys.Tb_Sys_UserRole model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Sys.Tb_Sys_UserRole();
					if(dt.Rows[n]["UserRoleCode"].ToString()!="")
					{
						model.UserRoleCode=new Guid(dt.Rows[n]["UserRoleCode"].ToString());
					}
					model.UserCode=dt.Rows[n]["UserCode"].ToString();
					model.RoleCode=dt.Rows[n]["RoleCode"].ToString();
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

