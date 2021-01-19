using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_HSPR_RefundMultiAudit ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_HSPR_RefundMultiAudit
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_RefundMultiAudit dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_RefundMultiAudit();
		public Bll_Tb_HSPR_RefundMultiAudit()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long IID)
		{
			return dal.Exists(IID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(MobileSoft.Model.HSPR.Tb_HSPR_RefundMultiAudit model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_RefundMultiAudit model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long IID)
		{
			
			dal.Delete(IID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_RefundMultiAudit GetModel(long IID)
		{
			
			return dal.GetModel(IID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_RefundMultiAudit GetModelByCache(long IID)
		{
			
			string CacheKey = "Tb_HSPR_RefundMultiAuditModel-" + IID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(IID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_RefundMultiAudit)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_RefundMultiAudit> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_RefundMultiAudit> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_RefundMultiAudit> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_RefundMultiAudit>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_RefundMultiAudit model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_RefundMultiAudit();
					//model.IID=dt.Rows[n]["IID"].ToString();
					if(dt.Rows[n]["BusinessType"].ToString()!="")
					{
						model.BusinessType=int.Parse(dt.Rows[n]["BusinessType"].ToString());
					}
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					//model.CostID=dt.Rows[n]["CostID"].ToString();
					//model.FeesID=dt.Rows[n]["FeesID"].ToString();
					//model.RefundID=dt.Rows[n]["RefundID"].ToString();
					//model.PrecID=dt.Rows[n]["PrecID"].ToString();
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

            /// <summary>
            /// ��������б�
            /// </summary>
            public void RefundMultiAuditCre(int CommID, long CustID, long RoomID, string SubdataStarttime, string SubdataEndtime, string LoginRoles, string CostIDs, int BusinessType)
            {
                  dal.RefundMultiAuditCre(CommID, CustID, RoomID, SubdataStarttime, SubdataEndtime, LoginRoles, CostIDs, BusinessType);
            }


            public void RefundSecFeesUpdateAudit(int CommID, long IID, string AuditUsercode, string UserRoles, int IsAudit)
            {
                  dal.RefundSecFeesUpdateAudit(CommID, IID, AuditUsercode, UserRoles, IsAudit);
            }
	}
}

