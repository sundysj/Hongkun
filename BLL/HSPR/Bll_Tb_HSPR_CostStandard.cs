using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_HSPR_CostStandard ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_HSPR_CostStandard
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CostStandard dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CostStandard();
		public Bll_Tb_HSPR_CostStandard()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long StanID)
		{
			return dal.Exists(StanID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CostStandard model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CostStandard model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long StanID)
		{
			
			dal.Delete(StanID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CostStandard GetModel(long StanID)
		{
			
			return dal.GetModel(StanID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CostStandard GetModelByCache(long StanID)
		{
			
			string CacheKey = "Tb_HSPR_CostStandardModel-" + StanID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(StanID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_CostStandard)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CostStandard> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CostStandard> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_CostStandard> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_CostStandard>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_CostStandard model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_CostStandard();
					//model.StanID=dt.Rows[n]["StanID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					//model.CostID=dt.Rows[n]["CostID"].ToString();
					model.StanSign=dt.Rows[n]["StanSign"].ToString();
					model.StanName=dt.Rows[n]["StanName"].ToString();
					model.StanExplain=dt.Rows[n]["StanExplain"].ToString();
					model.StanFormula=dt.Rows[n]["StanFormula"].ToString();
					if(dt.Rows[n]["StanAmount"].ToString()!="")
					{
						model.StanAmount=decimal.Parse(dt.Rows[n]["StanAmount"].ToString());
					}
					if(dt.Rows[n]["StanStartDate"].ToString()!="")
					{
						model.StanStartDate=DateTime.Parse(dt.Rows[n]["StanStartDate"].ToString());
					}
					if(dt.Rows[n]["StanEndDate"].ToString()!="")
					{
						model.StanEndDate=DateTime.Parse(dt.Rows[n]["StanEndDate"].ToString());
					}
					if(dt.Rows[n]["IsCondition"].ToString()!="")
					{
						model.IsCondition=int.Parse(dt.Rows[n]["IsCondition"].ToString());
					}
					model.ConditionField=dt.Rows[n]["ConditionField"].ToString();
					if(dt.Rows[n]["DelinRates"].ToString()!="")
					{
						model.DelinRates=decimal.Parse(dt.Rows[n]["DelinRates"].ToString());
					}
					if(dt.Rows[n]["DelinDelay"].ToString()!="")
					{
						model.DelinDelay=int.Parse(dt.Rows[n]["DelinDelay"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["IsStanRange"].ToString()!="")
					{
						model.IsStanRange=int.Parse(dt.Rows[n]["IsStanRange"].ToString());
					}
					if(dt.Rows[n]["ChargeCycle"].ToString()!="")
					{
						model.ChargeCycle=int.Parse(dt.Rows[n]["ChargeCycle"].ToString());
					}
					if(dt.Rows[n]["ManageFeesStyle"].ToString()!="")
					{
						model.ManageFeesStyle=int.Parse(dt.Rows[n]["ManageFeesStyle"].ToString());
					}
					if(dt.Rows[n]["ManageFeesAmount"].ToString()!="")
					{
						model.ManageFeesAmount=decimal.Parse(dt.Rows[n]["ManageFeesAmount"].ToString());
					}
					//model.CorpStanID=dt.Rows[n]["CorpStanID"].ToString();
					//model.CorpCostID=dt.Rows[n]["CorpCostID"].ToString();
					if(dt.Rows[n]["AmountRounded"].ToString()!="")
					{
						model.AmountRounded=decimal.Parse(dt.Rows[n]["AmountRounded"].ToString());
					}
					if(dt.Rows[n]["Modulus"].ToString()!="")
					{
						model.Modulus=decimal.Parse(dt.Rows[n]["Modulus"].ToString());
					}
					if(dt.Rows[n]["DelinType"].ToString()!="")
					{
						model.DelinType=int.Parse(dt.Rows[n]["DelinType"].ToString());
					}
					if(dt.Rows[n]["DelinDay"].ToString()!="")
					{
						model.DelinDay=int.Parse(dt.Rows[n]["DelinDay"].ToString());
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

