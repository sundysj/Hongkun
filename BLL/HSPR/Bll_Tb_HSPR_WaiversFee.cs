using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_HSPR_WaiversFee ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_HSPR_WaiversFee
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_WaiversFee dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_WaiversFee();
		public Bll_Tb_HSPR_WaiversFee()
		{}

		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long WaivID)
		{
			return dal.Exists(WaivID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_WaiversFee model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_WaiversFee model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long WaivID)
		{
			
			dal.Delete(WaivID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_WaiversFee GetModel(long WaivID)
		{
			
			return dal.GetModel(WaivID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_WaiversFee GetModelByCache(long WaivID)
		{
			
			string CacheKey = "Tb_HSPR_WaiversFeeModel-" + WaivID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(WaivID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_WaiversFee)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_WaiversFee> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_WaiversFee> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_WaiversFee> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_WaiversFee>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_WaiversFee model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_WaiversFee();
					//model.WaivID=dt.Rows[n]["WaivID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					//model.CostID=dt.Rows[n]["CostID"].ToString();
					if(dt.Rows[n]["WaivAmount"].ToString()!="")
					{
						model.WaivAmount=decimal.Parse(dt.Rows[n]["WaivAmount"].ToString());
					}
					if(dt.Rows[n]["WaivedAmount"].ToString()!="")
					{
						model.WaivedAmount=decimal.Parse(dt.Rows[n]["WaivedAmount"].ToString());
					}
					if(dt.Rows[n]["WaivStateDuring"].ToString()!="")
					{
						model.WaivStateDuring=DateTime.Parse(dt.Rows[n]["WaivStateDuring"].ToString());
					}
					if(dt.Rows[n]["WaivEndDuring"].ToString()!="")
					{
						model.WaivEndDuring=DateTime.Parse(dt.Rows[n]["WaivEndDuring"].ToString());
					}
					if(dt.Rows[n]["WaivDate"].ToString()!="")
					{
						model.WaivDate=DateTime.Parse(dt.Rows[n]["WaivDate"].ToString());
					}
					if(dt.Rows[n]["WaivMonthAmount"].ToString()!="")
					{
						model.WaivMonthAmount=decimal.Parse(dt.Rows[n]["WaivMonthAmount"].ToString());
					}
					model.UserCode=dt.Rows[n]["UserCode"].ToString();
					model.WaivReason=dt.Rows[n]["WaivReason"].ToString();
					model.AuditReason=dt.Rows[n]["AuditReason"].ToString();
					model.WaivMemo=dt.Rows[n]["WaivMemo"].ToString();
					model.AuditUserCode=dt.Rows[n]["AuditUserCode"].ToString();
					if(dt.Rows[n]["IsWaiv"].ToString()!="")
					{
						model.IsWaiv=int.Parse(dt.Rows[n]["IsWaiv"].ToString());
					}
					if(dt.Rows[n]["IsAudit"].ToString()!="")
					{
						model.IsAudit=int.Parse(dt.Rows[n]["IsAudit"].ToString());
					}
					if(dt.Rows[n]["WaivType"].ToString()!="")
					{
						model.WaivType=int.Parse(dt.Rows[n]["WaivType"].ToString());
					}
					if(dt.Rows[n]["WaivRates"].ToString()!="")
					{
						model.WaivRates=decimal.Parse(dt.Rows[n]["WaivRates"].ToString());
					}
					//model.WaivCostID=dt.Rows[n]["WaivCostID"].ToString();
					//model.HandID=dt.Rows[n]["HandID"].ToString();
					model.MeterSign=dt.Rows[n]["MeterSign"].ToString();
					model.AuditUserName=dt.Rows[n]["AuditUserName"].ToString();
					if(dt.Rows[n]["WaivCreDate"].ToString()!="")
					{
						model.WaivCreDate=DateTime.Parse(dt.Rows[n]["WaivCreDate"].ToString());
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

            public void WaiversFeeUpdateAudit(int CommID, long WaivID, string AuditUsercode, string UserRoles, string AuditReason, int IsAudit)
            {
                  dal.WaiversFeeUpdateAudit(CommID, WaivID, AuditUsercode, UserRoles, AuditReason, IsAudit);
            }
            public bool WaiversFeeIsCheck(long WaivID, int CommID, long CustID, long RoomID, long CostID, string WaivStateDuring, string WaivEndDuring)
            {
                  return dal.WaiversFeeIsCheck(WaivID, CommID, CustID, RoomID, CostID, WaivStateDuring,WaivEndDuring);
            }

	}
}

