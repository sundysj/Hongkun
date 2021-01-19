using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.OL;
namespace MobileSoft.BLL.OL
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_OL_AlipayOrder ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_OL_AlipayOrder
	{
		private readonly MobileSoft.DAL.OL.Dal_Tb_OL_AlipayOrder dal=new MobileSoft.DAL.OL.Dal_Tb_OL_AlipayOrder();
		public Bll_Tb_OL_AlipayOrder()
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
		public void Add(MobileSoft.Model.OL.Tb_OL_AlipayOrder model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.OL.Tb_OL_AlipayOrder model)
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
		public MobileSoft.Model.OL.Tb_OL_AlipayOrder GetModel(string Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.OL.Tb_OL_AlipayOrder GetModelByCache(string Id)
		{
			
			string CacheKey = "Tb_OL_AlipayOrderModel-" + Id;
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
			return (MobileSoft.Model.OL.Tb_OL_AlipayOrder)objModel;
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
		public List<MobileSoft.Model.OL.Tb_OL_AlipayOrder> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.OL.Tb_OL_AlipayOrder> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.OL.Tb_OL_AlipayOrder> modelList = new List<MobileSoft.Model.OL.Tb_OL_AlipayOrder>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.OL.Tb_OL_AlipayOrder model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.OL.Tb_OL_AlipayOrder();
					model.Id=dt.Rows[n]["Id"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.CommunityId=dt.Rows[n]["CommunityId"].ToString();
					//model.CustId=dt.Rows[n]["CustId"].ToString();
					model.partner=dt.Rows[n]["partner"].ToString();
					model.out_trade_no=dt.Rows[n]["out_trade_no"].ToString();
					model.prepay_str=dt.Rows[n]["prepay_str"].ToString();
					model.txnTime=dt.Rows[n]["txnTime"].ToString();
					model.trade_status=dt.Rows[n]["trade_status"].ToString();
					model.trade_msg=dt.Rows[n]["trade_msg"].ToString();
					if(dt.Rows[n]["IsSucc"].ToString()!="")
					{
						model.IsSucc=int.Parse(dt.Rows[n]["IsSucc"].ToString());
					}
					model.Memo=dt.Rows[n]["Memo"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
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
