using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_HSPR_IncidentTypeWarningRule ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_HSPR_IncidentTypeWarningRule
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_IncidentTypeWarningRule dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_IncidentTypeWarningRule();
		public Bll_Tb_HSPR_IncidentTypeWarningRule()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long WarningRuleID)
		{
			return dal.Exists(WarningRuleID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarningRule model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarningRule model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long WarningRuleID)
		{
			
			dal.Delete(WarningRuleID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarningRule GetModel(long WarningRuleID)
		{
			
			return dal.GetModel(WarningRuleID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarningRule GetModelByCache(long WarningRuleID)
		{
			
			string CacheKey = "Tb_HSPR_IncidentTypeWarningRuleModel-" + WarningRuleID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(WarningRuleID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarningRule)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarningRule> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarningRule> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarningRule> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarningRule>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarningRule model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarningRule();
					//model.WarningRuleID=dt.Rows[n]["WarningRuleID"].ToString();
					//model.WarningID=dt.Rows[n]["WarningID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.RuleID=dt.Rows[n]["RuleID"].ToString();
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

