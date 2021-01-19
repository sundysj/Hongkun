using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Resources;
namespace MobileSoft.BLL.Resources
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Resources_ReleaseImages ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Resources_ReleaseImages
	{
		private readonly MobileSoft.DAL.Resources.Dal_Tb_Resources_ReleaseImages dal=new MobileSoft.DAL.Resources.Dal_Tb_Resources_ReleaseImages();
		public Bll_Tb_Resources_ReleaseImages()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long ReleaseImagesID)
		{
			return dal.Exists(ReleaseImagesID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseImages model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseImages model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long ReleaseImagesID)
		{
			
			dal.Delete(ReleaseImagesID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseImages GetModel(long ReleaseImagesID)
		{
			
			return dal.GetModel(ReleaseImagesID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseImages GetModelByCache(long ReleaseImagesID)
		{
			
			string CacheKey = "Tb_Resources_ReleaseImagesModel-" + ReleaseImagesID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ReleaseImagesID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Resources.Tb_Resources_ReleaseImages)objModel;
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
		public List<MobileSoft.Model.Resources.Tb_Resources_ReleaseImages> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Resources.Tb_Resources_ReleaseImages> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Resources.Tb_Resources_ReleaseImages> modelList = new List<MobileSoft.Model.Resources.Tb_Resources_ReleaseImages>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Resources.Tb_Resources_ReleaseImages model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Resources.Tb_Resources_ReleaseImages();
					//model.ReleaseImagesID=dt.Rows[n]["ReleaseImagesID"].ToString();
					//model.ReleaseID=dt.Rows[n]["ReleaseID"].ToString();
					model.ReleaseImagesUrl=dt.Rows[n]["ReleaseImagesUrl"].ToString();
					model.ReleaseImagesData=dt.Rows[n]["ReleaseImagesData"].ToString();
					if(dt.Rows[n]["ReleaseImagesUpdate"].ToString()!="")
					{
						model.ReleaseImagesUpdate=DateTime.Parse(dt.Rows[n]["ReleaseImagesUpdate"].ToString());
					}
					if(dt.Rows[n]["ReleaseImagesIndex"].ToString()!="")
					{
						model.ReleaseImagesIndex=int.Parse(dt.Rows[n]["ReleaseImagesIndex"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=DateTime.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
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

