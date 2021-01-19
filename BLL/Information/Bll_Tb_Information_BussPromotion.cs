using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Information;
namespace MobileSoft.BLL.Information
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Information_BussPromotion ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Information_BussPromotion
	{
		private readonly MobileSoft.DAL.Information.Dal_Tb_Information_BussPromotion dal=new MobileSoft.DAL.Information.Dal_Tb_Information_BussPromotion();
		public Bll_Tb_Information_BussPromotion()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long ProID)
		{
			return dal.Exists(ProID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public long Add(MobileSoft.Model.Information.Tb_Information_BussPromotion model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Information.Tb_Information_BussPromotion model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long ProID)
		{
			
			dal.Delete(ProID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Information.Tb_Information_BussPromotion GetModel(long ProID)
		{
			
			return dal.GetModel(ProID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Information.Tb_Information_BussPromotion GetModelByCache(long ProID)
		{
			
			string CacheKey = "Tb_Information_BussPromotionModel-" + ProID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ProID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Information.Tb_Information_BussPromotion)objModel;
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
		public List<MobileSoft.Model.Information.Tb_Information_BussPromotion> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Information.Tb_Information_BussPromotion> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Information.Tb_Information_BussPromotion> modelList = new List<MobileSoft.Model.Information.Tb_Information_BussPromotion>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Information.Tb_Information_BussPromotion model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Information.Tb_Information_BussPromotion();
					//model.ProID=dt.Rows[n]["ProID"].ToString();
					//model.BussId=dt.Rows[n]["BussId"].ToString();
					model.Project=dt.Rows[n]["Project"].ToString();
					model.Publisher=dt.Rows[n]["Publisher"].ToString();
					if(dt.Rows[n]["PublishDate"].ToString()!="")
					{
						model.PublishDate=DateTime.Parse(dt.Rows[n]["PublishDate"].ToString());
					}
					model.Reason=dt.Rows[n]["Reason"].ToString();
					model.ProImage=dt.Rows[n]["ProImage"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					//model.NumID=dt.Rows[n]["NumID"].ToString();
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

