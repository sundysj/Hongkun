using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Resources;
namespace MobileSoft.BLL.Resources
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Resources_ReleaseVenuesSet ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Resources_ReleaseVenuesSet
	{
		private readonly MobileSoft.DAL.Resources.Dal_Tb_Resources_ReleaseVenuesSet dal=new MobileSoft.DAL.Resources.Dal_Tb_Resources_ReleaseVenuesSet();
		public Bll_Tb_Resources_ReleaseVenuesSet()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long ReleaseVenuesSetID)
		{
			return dal.Exists(ReleaseVenuesSetID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
        public void Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSet model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSet model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long ReleaseVenuesSetID)
		{
			
			dal.Delete(ReleaseVenuesSetID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSet GetModel(long ReleaseVenuesSetID)
		{
			
			return dal.GetModel(ReleaseVenuesSetID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSet GetModelByCache(long ReleaseVenuesSetID)
		{
			
			string CacheKey = "Tb_Resources_ReleaseVenuesSetModel-" + ReleaseVenuesSetID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ReleaseVenuesSetID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSet)objModel;
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
		public List<MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSet> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSet> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSet> modelList = new List<MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSet>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSet model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSet();
					//model.ReleaseVenuesSetID=dt.Rows[n]["ReleaseVenuesSetID"].ToString();
					model.ReleaseVenuesSetStartTime=dt.Rows[n]["ReleaseVenuesSetStartTime"].ToString();
					model.ReleaseVenuesSetEndTime=dt.Rows[n]["ReleaseVenuesSetEndTime"].ToString();
					//model.BussId=dt.Rows[n]["BussId"].ToString();
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

