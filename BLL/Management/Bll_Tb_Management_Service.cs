using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Management;
namespace MobileSoft.BLL.Management
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Management_Service ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Management_Service
	{
		private readonly MobileSoft.DAL.Management.Dal_Tb_Management_Service dal=new MobileSoft.DAL.Management.Dal_Tb_Management_Service();
		public Bll_Tb_Management_Service()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string ServiceCode)
		{
			return dal.Exists(ServiceCode);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.Management.Tb_Management_Service model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Management.Tb_Management_Service model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string ServiceCode)
		{
			
			dal.Delete(ServiceCode);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Management.Tb_Management_Service GetModel(string ServiceCode)
		{
			
			return dal.GetModel(ServiceCode);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Management.Tb_Management_Service GetModelByCache(string ServiceCode)
		{
			
			string CacheKey = "Tb_Management_ServiceModel-" + ServiceCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ServiceCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Management.Tb_Management_Service)objModel;
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
		public List<MobileSoft.Model.Management.Tb_Management_Service> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Management.Tb_Management_Service> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Management.Tb_Management_Service> modelList = new List<MobileSoft.Model.Management.Tb_Management_Service>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Management.Tb_Management_Service model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Management.Tb_Management_Service();
					model.ServiceCode=dt.Rows[n]["ServiceCode"].ToString();
					model.OrganCode=dt.Rows[n]["OrganCode"].ToString();
					model.CommID=dt.Rows[n]["CommID"].ToString();
					if(dt.Rows[n]["Sort"].ToString()!="")
					{
						model.Sort=int.Parse(dt.Rows[n]["Sort"].ToString());
					}
					model.TypeCode=dt.Rows[n]["TypeCode"].ToString();
					model.ServiceName=dt.Rows[n]["ServiceName"].ToString();
					model.Adv=dt.Rows[n]["Adv"].ToString();
					model.Intro=dt.Rows[n]["Intro"].ToString();
					if(dt.Rows[n]["ServiceStartDate"].ToString()!="")
					{
						model.ServiceStartDate=DateTime.Parse(dt.Rows[n]["ServiceStartDate"].ToString());
					}
					if(dt.Rows[n]["ServiceEndDate"].ToString()!="")
					{
						model.ServiceEndDate=DateTime.Parse(dt.Rows[n]["ServiceEndDate"].ToString());
					}
					//model.CorpStanID=dt.Rows[n]["CorpStanID"].ToString();
					model.CostUnit=dt.Rows[n]["CostUnit"].ToString();
					model.ServiceTel=dt.Rows[n]["ServiceTel"].ToString();
					model.SupplierCode=dt.Rows[n]["SupplierCode"].ToString();
					model.ServiceUrl=dt.Rows[n]["ServiceUrl"].ToString();
					model.Memo=dt.Rows[n]["Memo"].ToString();
					model.ServiceState=dt.Rows[n]["ServiceState"].ToString();
					model.IsPublish=dt.Rows[n]["IsPublish"].ToString();
					model.CommIDs=dt.Rows[n]["CommIDs"].ToString();
					model.CommNames=dt.Rows[n]["CommNames"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}


        public DataSet GetGroupList()
		{
            return dal.GetGroupList("");
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

