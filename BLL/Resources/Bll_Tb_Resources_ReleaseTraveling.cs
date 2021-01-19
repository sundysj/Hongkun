using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Resources;
namespace MobileSoft.BLL.Resources
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Resources_ReleaseTraveling ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Resources_ReleaseTraveling
	{
		private readonly MobileSoft.DAL.Resources.Dal_Tb_Resources_ReleaseTraveling dal=new MobileSoft.DAL.Resources.Dal_Tb_Resources_ReleaseTraveling();
		public Bll_Tb_Resources_ReleaseTraveling()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long ReleaseTravelingID)
		{
			return dal.Exists(ReleaseTravelingID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
        public long Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseTraveling model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseTraveling model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long ReleaseTravelingID)
		{
			
			dal.Delete(ReleaseTravelingID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseTraveling GetModel(long ReleaseTravelingID)
		{
			
			return dal.GetModel(ReleaseTravelingID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseTraveling GetModelByCache(long ReleaseTravelingID)
		{
			
			string CacheKey = "Tb_Resources_ReleaseTravelingModel-" + ReleaseTravelingID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ReleaseTravelingID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Resources.Tb_Resources_ReleaseTraveling)objModel;
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
		public List<MobileSoft.Model.Resources.Tb_Resources_ReleaseTraveling> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Resources.Tb_Resources_ReleaseTraveling> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Resources.Tb_Resources_ReleaseTraveling> modelList = new List<MobileSoft.Model.Resources.Tb_Resources_ReleaseTraveling>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Resources.Tb_Resources_ReleaseTraveling model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Resources.Tb_Resources_ReleaseTraveling();
					//model.ReleaseTravelingID=dt.Rows[n]["ReleaseTravelingID"].ToString();
					model.ReleaseTravelingContent=dt.Rows[n]["ReleaseTravelingContent"].ToString();
					model.ReleaseTravelingServicedemo=dt.Rows[n]["ReleaseTravelingServicedemo"].ToString();
					model.ReleaseTravelingNeedKnow=dt.Rows[n]["ReleaseTravelingNeedKnow"].ToString();
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

