using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Resources;
namespace MobileSoft.BLL.Resources
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Resources_ReleaseVenuesScheduling ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Resources_ReleaseVenuesScheduling
	{
		private readonly MobileSoft.DAL.Resources.Dal_Tb_Resources_ReleaseVenuesScheduling dal=new MobileSoft.DAL.Resources.Dal_Tb_Resources_ReleaseVenuesScheduling();
		public Bll_Tb_Resources_ReleaseVenuesScheduling()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long ReleaseVenuesSchedulingID)
		{
			return dal.Exists(ReleaseVenuesSchedulingID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public long  Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesScheduling model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesScheduling model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long ReleaseVenuesSchedulingID)
		{
			
			dal.Delete(ReleaseVenuesSchedulingID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesScheduling GetModel(long ReleaseVenuesSchedulingID)
		{
			
			return dal.GetModel(ReleaseVenuesSchedulingID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesScheduling GetModelByCache(long ReleaseVenuesSchedulingID)
		{
			
			string CacheKey = "Tb_Resources_ReleaseVenuesSchedulingModel-" + ReleaseVenuesSchedulingID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ReleaseVenuesSchedulingID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesScheduling)objModel;
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
		public List<MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesScheduling> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesScheduling> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesScheduling> modelList = new List<MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesScheduling>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesScheduling model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesScheduling();
					//model.ReleaseVenuesSchedulingID=dt.Rows[n]["ReleaseVenuesSchedulingID"].ToString();
					if(dt.Rows[n]["ReleaseVenuesSchedulingStartDate"].ToString()!="")
					{
						model.ReleaseVenuesSchedulingStartDate=DateTime.Parse(dt.Rows[n]["ReleaseVenuesSchedulingStartDate"].ToString());
					}
					if(dt.Rows[n]["ReleaseVenuesSchedulingEndDate"].ToString()!="")
					{
						model.ReleaseVenuesSchedulingEndDate=DateTime.Parse(dt.Rows[n]["ReleaseVenuesSchedulingEndDate"].ToString());
					}
					if(dt.Rows[n]["ReleaseVenuesSchedulingCount"].ToString()!="")
					{
						model.ReleaseVenuesSchedulingCount=decimal.Parse(dt.Rows[n]["ReleaseVenuesSchedulingCount"].ToString());
					}
					//model.ReleaseVenuesID=dt.Rows[n]["ReleaseVenuesID"].ToString();
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

