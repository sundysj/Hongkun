using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Resources;
namespace MobileSoft.BLL.Resources
{
	/// <summary>
	/// ҵ���߼���Bll_TB_Resources_ReleaseService ��ժҪ˵����
	/// </summary>
	public class Bll_TB_Resources_ReleaseService
	{
		private readonly MobileSoft.DAL.Resources.Dal_TB_Resources_ReleaseService dal=new MobileSoft.DAL.Resources.Dal_TB_Resources_ReleaseService();
		public Bll_TB_Resources_ReleaseService()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long ReleaseServiceID)
		{
			return dal.Exists(ReleaseServiceID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void  Add(MobileSoft.Model.Resources.TB_Resources_ReleaseService model)
		{
		     dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Resources.TB_Resources_ReleaseService model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long ReleaseServiceID)
		{
			
			dal.Delete(ReleaseServiceID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Resources.TB_Resources_ReleaseService GetModel(long ReleaseServiceID)
		{
			
			return dal.GetModel(ReleaseServiceID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Resources.TB_Resources_ReleaseService GetModelByCache(long ReleaseServiceID)
		{
			
			string CacheKey = "TB_Resources_ReleaseServiceModel-" + ReleaseServiceID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ReleaseServiceID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Resources.TB_Resources_ReleaseService)objModel;
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
		public List<MobileSoft.Model.Resources.TB_Resources_ReleaseService> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Resources.TB_Resources_ReleaseService> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Resources.TB_Resources_ReleaseService> modelList = new List<MobileSoft.Model.Resources.TB_Resources_ReleaseService>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Resources.TB_Resources_ReleaseService model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Resources.TB_Resources_ReleaseService();
					//model.ReleaseServiceID=dt.Rows[n]["ReleaseServiceID"].ToString();
					//model.ReleaseID=dt.Rows[n]["ReleaseID"].ToString();
					model.ReleaseServiceContent=dt.Rows[n]["ReleaseServiceContent"].ToString();
					model.ReleaseServiceNeedKnow=dt.Rows[n]["ReleaseServiceNeedKnow"].ToString();
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

