using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.OL;
namespace MobileSoft.BLL.OL
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_OL_WeiXinPayDetail ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_OL_WeiXinPayDetail
	{
		private readonly MobileSoft.DAL.OL.Dal_Tb_OL_WeiXinPayDetail dal=new MobileSoft.DAL.OL.Dal_Tb_OL_WeiXinPayDetail();
		public Bll_Tb_OL_WeiXinPayDetail()
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
		public void Add(MobileSoft.Model.OL.Tb_OL_WeiXinPayDetail model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.OL.Tb_OL_WeiXinPayDetail model)
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
		public MobileSoft.Model.OL.Tb_OL_WeiXinPayDetail GetModel(string Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.OL.Tb_OL_WeiXinPayDetail GetModelByCache(string Id)
		{
			
			string CacheKey = "Tb_OL_WeiXinPayDetailModel-" + Id;
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
			return (MobileSoft.Model.OL.Tb_OL_WeiXinPayDetail)objModel;
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
		public List<MobileSoft.Model.OL.Tb_OL_WeiXinPayDetail> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.OL.Tb_OL_WeiXinPayDetail> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.OL.Tb_OL_WeiXinPayDetail> modelList = new List<MobileSoft.Model.OL.Tb_OL_WeiXinPayDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.OL.Tb_OL_WeiXinPayDetail model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.OL.Tb_OL_WeiXinPayDetail();
					model.Id=dt.Rows[n]["Id"].ToString();
					model.PayOrderId=dt.Rows[n]["PayOrderId"].ToString();
					//model.FeesId=dt.Rows[n]["FeesId"].ToString();
					if(dt.Rows[n]["DueAmount"].ToString()!="")
					{
						model.DueAmount=decimal.Parse(dt.Rows[n]["DueAmount"].ToString());
					}
					if(dt.Rows[n]["LateFeeAmount"].ToString()!="")
					{
						model.LateFeeAmount=decimal.Parse(dt.Rows[n]["LateFeeAmount"].ToString());
					}
					if(dt.Rows[n]["PaidAmount"].ToString()!="")
					{
						model.PaidAmount=decimal.Parse(dt.Rows[n]["PaidAmount"].ToString());
					}
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

