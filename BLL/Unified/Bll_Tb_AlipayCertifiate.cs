using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Unified;
namespace MobileSoft.BLL.Unified
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_AlipayCertifiate ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_AlipayCertifiate
	{
		private readonly MobileSoft.DAL.Unified.Dal_Tb_AlipayCertifiate dal=new MobileSoft.DAL.Unified.Dal_Tb_AlipayCertifiate();
		public Bll_Tb_AlipayCertifiate()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.Unified.Tb_AlipayCertifiate model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Unified.Tb_AlipayCertifiate model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string Id)
		{
			
			dal.Delete(Id);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Unified.Tb_AlipayCertifiate GetModel(string Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Unified.Tb_AlipayCertifiate GetModelByCache(string Id)
		{
			
			string CacheKey = "Tb_AlipayCertifiateModel-" + Id;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Id);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Unified.Tb_AlipayCertifiate)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Unified.Tb_AlipayCertifiate> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Unified.Tb_AlipayCertifiate> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Unified.Tb_AlipayCertifiate> modelList = new List<MobileSoft.Model.Unified.Tb_AlipayCertifiate>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Unified.Tb_AlipayCertifiate model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Unified.Tb_AlipayCertifiate();
					model.Id=dt.Rows[n]["Id"].ToString();
					model.CommunityId=dt.Rows[n]["CommunityId"].ToString();
					model.partner=dt.Rows[n]["partner"].ToString();
					model.seller_id=dt.Rows[n]["seller_id"].ToString();
					model.extern_token=dt.Rows[n]["extern_token"].ToString();
					model.alipay_public_key=dt.Rows[n]["alipay_public_key"].ToString();
					model.private_key=dt.Rows[n]["private_key"].ToString();
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

