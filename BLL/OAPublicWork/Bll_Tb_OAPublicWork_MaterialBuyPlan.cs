using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.OAPublicWork;
namespace MobileSoft.BLL.OAPublicWork
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_OAPublicWork_MaterialBuyPlan ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_OAPublicWork_MaterialBuyPlan
	{
		private readonly MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_MaterialBuyPlan dal=new MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_MaterialBuyPlan();
		public Bll_Tb_OAPublicWork_MaterialBuyPlan()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int InfoId)
		{
			return dal.Exists(InfoId);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_MaterialBuyPlan model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_MaterialBuyPlan model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int InfoId)
		{
			
			dal.Delete(InfoId);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_MaterialBuyPlan GetModel(int InfoId)
		{
			
			return dal.GetModel(InfoId);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_MaterialBuyPlan GetModelByCache(int InfoId)
		{
			
			string CacheKey = "Tb_OAPublicWork_MaterialBuyPlanModel-" + InfoId;
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
			return (MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_MaterialBuyPlan)objModel;
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
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_MaterialBuyPlan> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_MaterialBuyPlan> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_MaterialBuyPlan> modelList = new List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_MaterialBuyPlan>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_MaterialBuyPlan model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_MaterialBuyPlan();
					if(dt.Rows[n]["InfoId"].ToString()!="")
					{
						model.InfoId=int.Parse(dt.Rows[n]["InfoId"].ToString());
					}
					if(dt.Rows[n]["Tb_WorkFlow_FlowSort_InfoId"].ToString()!="")
					{
						model.Tb_WorkFlow_FlowSort_InfoId=int.Parse(dt.Rows[n]["Tb_WorkFlow_FlowSort_InfoId"].ToString());
					}
					model.UserCode=dt.Rows[n]["UserCode"].ToString();
					model.Title=dt.Rows[n]["Title"].ToString();
					model.DepCode=dt.Rows[n]["DepCode"].ToString();
					if(dt.Rows[n]["BuyPlanDate"].ToString()!="")
					{
						model.BuyPlanDate=DateTime.Parse(dt.Rows[n]["BuyPlanDate"].ToString());
					}
					if(dt.Rows[n]["BuyPlanMoney"].ToString()!="")
					{
						model.BuyPlanMoney=decimal.Parse(dt.Rows[n]["BuyPlanMoney"].ToString());
					}
					if(dt.Rows[n]["NeedDate"].ToString()!="")
					{
						model.NeedDate=DateTime.Parse(dt.Rows[n]["NeedDate"].ToString());
					}
					model.BuyPlanReadMe=dt.Rows[n]["BuyPlanReadMe"].ToString();
					model.DocumentUrl=dt.Rows[n]["DocumentUrl"].ToString();
					if(dt.Rows[n]["WorkStartDate"].ToString()!="")
					{
						model.WorkStartDate=DateTime.Parse(dt.Rows[n]["WorkStartDate"].ToString());
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

