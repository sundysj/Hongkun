using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Charge;
namespace MobileSoft.BLL.Charge
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Charge_ReceiptDetail ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Charge_ReceiptDetail
	{
		private readonly MobileSoft.DAL.Charge.Dal_Tb_Charge_ReceiptDetail dal=new MobileSoft.DAL.Charge.Dal_Tb_Charge_ReceiptDetail();
		public Bll_Tb_Charge_ReceiptDetail()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string RpdCode)
		{
			return dal.Exists(RpdCode);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.Charge.Tb_Charge_ReceiptDetail model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Charge.Tb_Charge_ReceiptDetail model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string RpdCode)
		{
			
			dal.Delete(RpdCode);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Charge.Tb_Charge_ReceiptDetail GetModel(string RpdCode)
		{
			
			return dal.GetModel(RpdCode);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Charge.Tb_Charge_ReceiptDetail GetModelByCache(string RpdCode)
		{
			
			string CacheKey = "Tb_Charge_ReceiptDetailModel-" + RpdCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(RpdCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Charge.Tb_Charge_ReceiptDetail)objModel;
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
		public List<MobileSoft.Model.Charge.Tb_Charge_ReceiptDetail> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Charge.Tb_Charge_ReceiptDetail> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Charge.Tb_Charge_ReceiptDetail> modelList = new List<MobileSoft.Model.Charge.Tb_Charge_ReceiptDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Charge.Tb_Charge_ReceiptDetail model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Charge.Tb_Charge_ReceiptDetail();
					model.RpdCode=dt.Rows[n]["RpdCode"].ToString();
					model.ReceiptCode=dt.Rows[n]["ReceiptCode"].ToString();
					model.ResourcesID=long.Parse(dt.Rows[n]["ResourcesID"].ToString());
					if(dt.Rows[n]["Quantity"].ToString()!="")
					{
						model.Quantity=int.Parse(dt.Rows[n]["Quantity"].ToString());
					}
					if(dt.Rows[n]["SalesPrice"].ToString()!="")
					{
						model.SalesPrice=decimal.Parse(dt.Rows[n]["SalesPrice"].ToString());
					}
					if(dt.Rows[n]["DiscountPrice"].ToString()!="")
					{
						model.DiscountPrice=decimal.Parse(dt.Rows[n]["DiscountPrice"].ToString());
					}
					if(dt.Rows[n]["MemberDiscountPrice"].ToString()!="")
					{
						model.MemberDiscountPrice=decimal.Parse(dt.Rows[n]["MemberDiscountPrice"].ToString());
					}
					if(dt.Rows[n]["GroupPrice"].ToString()!="")
					{
						model.GroupPrice=decimal.Parse(dt.Rows[n]["GroupPrice"].ToString());
					}
					if(dt.Rows[n]["DetailAmount"].ToString()!="")
					{
						model.DetailAmount=decimal.Parse(dt.Rows[n]["DetailAmount"].ToString());
					}
					model.RpdMemo=dt.Rows[n]["RpdMemo"].ToString();
					if(dt.Rows[n]["RpdIsDelete"].ToString()!="")
					{
						model.RpdIsDelete=int.Parse(dt.Rows[n]["RpdIsDelete"].ToString());
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

