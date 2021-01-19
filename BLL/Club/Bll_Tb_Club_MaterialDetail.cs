using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Club;
namespace MobileSoft.BLL.Club
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Club_MaterialDetail ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Club_MaterialDetail
	{
		private readonly MobileSoft.DAL.Club.Dal_Tb_Club_MaterialDetail dal=new MobileSoft.DAL.Club.Dal_Tb_Club_MaterialDetail();
		public Bll_Tb_Club_MaterialDetail()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long InfoId)
		{
			return dal.Exists(InfoId);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public long Add(MobileSoft.Model.Club.Tb_Club_MaterialDetail model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Club.Tb_Club_MaterialDetail model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long InfoId)
		{
			
			dal.Delete(InfoId);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Club.Tb_Club_MaterialDetail GetModel(long InfoId)
		{
			
			return dal.GetModel(InfoId);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Club.Tb_Club_MaterialDetail GetModelByCache(long InfoId)
		{
			
			string CacheKey = "Tb_Club_MaterialDetailModel-" + InfoId;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(InfoId);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Club.Tb_Club_MaterialDetail)objModel;
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
		public List<MobileSoft.Model.Club.Tb_Club_MaterialDetail> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Club.Tb_Club_MaterialDetail> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Club.Tb_Club_MaterialDetail> modelList = new List<MobileSoft.Model.Club.Tb_Club_MaterialDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Club.Tb_Club_MaterialDetail model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Club.Tb_Club_MaterialDetail();
					//model.InfoId=dt.Rows[n]["InfoId"].ToString();
					//model.MerchID=dt.Rows[n]["MerchID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					if(dt.Rows[n]["MerchDefaultPrice"].ToString()!="")
					{
						model.MerchDefaultPrice=decimal.Parse(dt.Rows[n]["MerchDefaultPrice"].ToString());
					}
					if(dt.Rows[n]["MerchCostPrice"].ToString()!="")
					{
						model.MerchCostPrice=decimal.Parse(dt.Rows[n]["MerchCostPrice"].ToString());
					}
					model.MerchMemo=dt.Rows[n]["MerchMemo"].ToString();
					if(dt.Rows[n]["IsPackage"].ToString()!="")
					{
						model.IsPackage=int.Parse(dt.Rows[n]["IsPackage"].ToString());
					}
					//model.MerchandiseID=dt.Rows[n]["MerchandiseID"].ToString();
					if(dt.Rows[n]["Quantity"].ToString()!="")
					{
						model.Quantity=int.Parse(dt.Rows[n]["Quantity"].ToString());
					}
					//model.CostID=dt.Rows[n]["CostID"].ToString();
					if(dt.Rows[n]["NoOutStore"].ToString()!="")
					{
						model.NoOutStore=int.Parse(dt.Rows[n]["NoOutStore"].ToString());
					}
					if(dt.Rows[n]["IsHelpSale"].ToString()!="")
					{
						model.IsHelpSale=int.Parse(dt.Rows[n]["IsHelpSale"].ToString());
					}
					if(dt.Rows[n]["GetMethod"].ToString()!="")
					{
						model.GetMethod=int.Parse(dt.Rows[n]["GetMethod"].ToString());
					}
					if(dt.Rows[n]["IsHelpSaleValue"].ToString()!="")
					{
						model.IsHelpSaleValue=decimal.Parse(dt.Rows[n]["IsHelpSaleValue"].ToString());
					}
					if(dt.Rows[n]["IsLease"].ToString()!="")
					{
						model.IsLease=int.Parse(dt.Rows[n]["IsLease"].ToString());
					}
					if(dt.Rows[n]["IsPay"].ToString()!="")
					{
						model.IsPay=int.Parse(dt.Rows[n]["IsPay"].ToString());
					}
					if(dt.Rows[n]["PayMoneyValue"].ToString()!="")
					{
						model.PayMoneyValue=decimal.Parse(dt.Rows[n]["PayMoneyValue"].ToString());
					}
					if(dt.Rows[n]["IsDeleteDetail"].ToString()!="")
					{
						model.IsDeleteDetail=int.Parse(dt.Rows[n]["IsDeleteDetail"].ToString());
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

