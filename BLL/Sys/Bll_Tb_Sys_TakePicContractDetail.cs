using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Sys;
namespace MobileSoft.BLL.Sys
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Sys_TakePicContractDetail ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Sys_TakePicContractDetail
	{
		private readonly MobileSoft.DAL.Sys.Dal_Tb_Sys_TakePicContractDetail dal=new MobileSoft.DAL.Sys.Dal_Tb_Sys_TakePicContractDetail();
		public Bll_Tb_Sys_TakePicContractDetail()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long DetailID)
		{
			return dal.Exists(DetailID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(MobileSoft.Model.Sys.Tb_Sys_TakePicContractDetail model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_TakePicContractDetail model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long DetailID)
		{
			
			dal.Delete(DetailID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePicContractDetail GetModel(long DetailID)
		{
			
			return dal.GetModel(DetailID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePicContractDetail GetModelByCache(long DetailID)
		{
			
			string CacheKey = "Tb_Sys_TakePicContractDetailModel-" + DetailID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(DetailID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Sys.Tb_Sys_TakePicContractDetail)objModel;
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
		public List<MobileSoft.Model.Sys.Tb_Sys_TakePicContractDetail> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Sys.Tb_Sys_TakePicContractDetail> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Sys.Tb_Sys_TakePicContractDetail> modelList = new List<MobileSoft.Model.Sys.Tb_Sys_TakePicContractDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Sys.Tb_Sys_TakePicContractDetail model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Sys.Tb_Sys_TakePicContractDetail();
					//model.DetailID=dt.Rows[n]["DetailID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.OrganCode=dt.Rows[n]["OrganCode"].ToString();
					model.ContTypeName=dt.Rows[n]["ContTypeName"].ToString();
					model.ContSign=dt.Rows[n]["ContSign"].ToString();
					model.ContName=dt.Rows[n]["ContName"].ToString();
					model.CustName=dt.Rows[n]["CustName"].ToString();
					if(dt.Rows[n]["ContEndDate"].ToString()!="")
					{
						model.ContEndDate=DateTime.Parse(dt.Rows[n]["ContEndDate"].ToString());
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

