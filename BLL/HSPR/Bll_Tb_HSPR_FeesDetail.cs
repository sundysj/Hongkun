using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_HSPR_FeesDetail ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_HSPR_FeesDetail
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_FeesDetail dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_FeesDetail();
		public Bll_Tb_HSPR_FeesDetail()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long RecdID)
		{
			return dal.Exists(RecdID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long RecdID)
		{
			
			dal.Delete(RecdID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail GetModel(long RecdID)
		{
			
			return dal.GetModel(RecdID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail GetModelByCache(long RecdID)
		{
			
			string CacheKey = "Tb_HSPR_FeesDetailModel-" + RecdID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(RecdID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail();
					//model.RecdID=dt.Rows[n]["RecdID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					//model.CostID=dt.Rows[n]["CostID"].ToString();
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					//model.FeesID=dt.Rows[n]["FeesID"].ToString();
					//model.ReceID=dt.Rows[n]["ReceID"].ToString();
					model.ChargeMode=dt.Rows[n]["ChargeMode"].ToString();
					if(dt.Rows[n]["AccountWay"].ToString()!="")
					{
						model.AccountWay=int.Parse(dt.Rows[n]["AccountWay"].ToString());
					}
					if(dt.Rows[n]["ChargeAmount"].ToString()!="")
					{
						model.ChargeAmount=decimal.Parse(dt.Rows[n]["ChargeAmount"].ToString());
					}
					if(dt.Rows[n]["LateFeeAmount"].ToString()!="")
					{
						model.LateFeeAmount=decimal.Parse(dt.Rows[n]["LateFeeAmount"].ToString());
					}
					if(dt.Rows[n]["ChargeDate"].ToString()!="")
					{
						model.ChargeDate=DateTime.Parse(dt.Rows[n]["ChargeDate"].ToString());
					}
					model.FeesMemo=dt.Rows[n]["FeesMemo"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					//model.OldCostID=dt.Rows[n]["OldCostID"].ToString();
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

            public void FeesDetailDelete(long RecdID)
            {
                  dal.FeesDetailDelete(RecdID);
            }
	}
}

