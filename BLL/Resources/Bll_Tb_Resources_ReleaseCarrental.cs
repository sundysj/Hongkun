using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Resources;
namespace MobileSoft.BLL.Resources
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Resources_ReleaseCarrental ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Resources_ReleaseCarrental
	{
		private readonly MobileSoft.DAL.Resources.Dal_Tb_Resources_ReleaseCarrental dal=new MobileSoft.DAL.Resources.Dal_Tb_Resources_ReleaseCarrental();
		public Bll_Tb_Resources_ReleaseCarrental()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long ReleaseCarrentalID)
		{
			return dal.Exists(ReleaseCarrentalID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
        public long Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseCarrental model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseCarrental model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long ReleaseCarrentalID)
		{
			
			dal.Delete(ReleaseCarrentalID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseCarrental GetModel(long ReleaseCarrentalID)
		{
			
			return dal.GetModel(ReleaseCarrentalID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseCarrental GetModelByCache(long ReleaseCarrentalID)
		{
			
			string CacheKey = "Tb_Resources_ReleaseCarrentalModel-" + ReleaseCarrentalID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ReleaseCarrentalID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Resources.Tb_Resources_ReleaseCarrental)objModel;
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
		public List<MobileSoft.Model.Resources.Tb_Resources_ReleaseCarrental> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Resources.Tb_Resources_ReleaseCarrental> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Resources.Tb_Resources_ReleaseCarrental> modelList = new List<MobileSoft.Model.Resources.Tb_Resources_ReleaseCarrental>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Resources.Tb_Resources_ReleaseCarrental model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Resources.Tb_Resources_ReleaseCarrental();
					//model.ReleaseCarrentalID=dt.Rows[n]["ReleaseCarrentalID"].ToString();
					model.ReleaseCarrentalBrand=dt.Rows[n]["ReleaseCarrentalBrand"].ToString();
					model.ReleaseCarrentalModels=dt.Rows[n]["ReleaseCarrentalModels"].ToString();
					model.ReleaseCarrentalManCount=dt.Rows[n]["ReleaseCarrentalManCount"].ToString();
					model.ReleaseCarrentalContent=dt.Rows[n]["ReleaseCarrentalContent"].ToString();
					model.ReleaseCarrentalNeedKnow=dt.Rows[n]["ReleaseCarrentalNeedKnow"].ToString();
					//model.ReleaseID=dt.Rows[n]["ReleaseID"].ToString();
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

