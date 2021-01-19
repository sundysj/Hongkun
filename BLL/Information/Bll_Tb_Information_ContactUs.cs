using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Information;
namespace MobileSoft.BLL.Information
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Information_ContactUs ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Information_ContactUs
	{
		private readonly MobileSoft.DAL.Information.Dal_Tb_Information_ContactUs dal=new MobileSoft.DAL.Information.Dal_Tb_Information_ContactUs();
		public Bll_Tb_Information_ContactUs()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public long Add(MobileSoft.Model.Information.Tb_Information_ContactUs model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Information.Tb_Information_ContactUs model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long ID)
		{
			
			dal.Delete(ID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Information.Tb_Information_ContactUs GetModel(long ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Information.Tb_Information_ContactUs GetModelByCache(long ID)
		{
			
			string CacheKey = "Tb_Information_ContactUsModel-" + ID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Information.Tb_Information_ContactUs)objModel;
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
		public List<MobileSoft.Model.Information.Tb_Information_ContactUs> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Information.Tb_Information_ContactUs> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Information.Tb_Information_ContactUs> modelList = new List<MobileSoft.Model.Information.Tb_Information_ContactUs>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Information.Tb_Information_ContactUs model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Information.Tb_Information_ContactUs();
					//model.ID=dt.Rows[n]["ID"].ToString();
					//model.BussId=dt.Rows[n]["BussId"].ToString();
					model.BussName=dt.Rows[n]["BussName"].ToString();
					model.Address=dt.Rows[n]["Address"].ToString();
					model.Postal=dt.Rows[n]["Postal"].ToString();
					model.LinkMan=dt.Rows[n]["LinkMan"].ToString();
					model.Tel=dt.Rows[n]["Tel"].ToString();
					model.Phone=dt.Rows[n]["Phone"].ToString();
					model.Email=dt.Rows[n]["Email"].ToString();
					model.QQ=dt.Rows[n]["QQ"].ToString();
					model.Wechat=dt.Rows[n]["Wechat"].ToString();
					model.URL=dt.Rows[n]["URL"].ToString();
					model.Map=dt.Rows[n]["Map"].ToString();
					model.ContactUsContent=dt.Rows[n]["ContactUsContent"].ToString();
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

