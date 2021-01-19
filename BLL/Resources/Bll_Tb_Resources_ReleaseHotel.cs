using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Resources;
namespace MobileSoft.BLL.Resources
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Resources_ReleaseHotel ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Resources_ReleaseHotel
	{
		private readonly MobileSoft.DAL.Resources.Dal_Tb_Resources_ReleaseHotel dal=new MobileSoft.DAL.Resources.Dal_Tb_Resources_ReleaseHotel();
		public Bll_Tb_Resources_ReleaseHotel()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long ReleaseHotelID)
		{
			return dal.Exists(ReleaseHotelID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
        public long Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseHotel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseHotel model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long ReleaseHotelID)
		{
			
			dal.Delete(ReleaseHotelID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseHotel GetModel(long ReleaseHotelID)
		{
			
			return dal.GetModel(ReleaseHotelID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseHotel GetModelByCache(long ReleaseHotelID)
		{
			
			string CacheKey = "Tb_Resources_ReleaseHotelModel-" + ReleaseHotelID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ReleaseHotelID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Resources.Tb_Resources_ReleaseHotel)objModel;
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
		public List<MobileSoft.Model.Resources.Tb_Resources_ReleaseHotel> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Resources.Tb_Resources_ReleaseHotel> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Resources.Tb_Resources_ReleaseHotel> modelList = new List<MobileSoft.Model.Resources.Tb_Resources_ReleaseHotel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Resources.Tb_Resources_ReleaseHotel model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Resources.Tb_Resources_ReleaseHotel();
					//model.ReleaseHotelID=dt.Rows[n]["ReleaseHotelID"].ToString();
					model.ReleaseHotelContent=dt.Rows[n]["ReleaseHotelContent"].ToString();
					model.ReleaseHotelNeedKnow=dt.Rows[n]["ReleaseHotelNeedKnow"].ToString();
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

