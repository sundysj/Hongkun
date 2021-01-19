using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Resources;
namespace MobileSoft.BLL.Resources
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Resources_Details ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Resources_Details
	{
		private readonly MobileSoft.DAL.Resources.Dal_Tb_Resources_Details dal=new MobileSoft.DAL.Resources.Dal_Tb_Resources_Details();
		public Bll_Tb_Resources_Details()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long ResourcesID)
		{
			return dal.Exists(ResourcesID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.Resources.Tb_Resources_Details model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_Details model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long ResourcesID)
		{
			
			dal.Delete(ResourcesID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_Details GetModel(long ResourcesID)
		{
			
			return dal.GetModel(ResourcesID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_Details GetModelByCache(long ResourcesID)
		{
			
			string CacheKey = "Tb_Resources_DetailsModel-" + ResourcesID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ResourcesID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Resources.Tb_Resources_Details)objModel;
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
		public List<MobileSoft.Model.Resources.Tb_Resources_Details> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Resources.Tb_Resources_Details> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Resources.Tb_Resources_Details> modelList = new List<MobileSoft.Model.Resources.Tb_Resources_Details>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Resources.Tb_Resources_Details model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Resources.Tb_Resources_Details();
					//model.ResourcesID=dt.Rows[n]["ResourcesID"].ToString();
					//model.BussId=dt.Rows[n]["BussId"].ToString();
					//model.ResourcesTypeID=dt.Rows[n]["ResourcesTypeID"].ToString();
					model.ResourcesName=dt.Rows[n]["ResourcesName"].ToString();
					model.ResourcesSimple=dt.Rows[n]["ResourcesSimple"].ToString();
					if(dt.Rows[n]["ResourcesIndex"].ToString()!="")
					{
						model.ResourcesIndex=int.Parse(dt.Rows[n]["ResourcesIndex"].ToString());
					}
					model.ResourcesBarCode=dt.Rows[n]["ResourcesBarCode"].ToString();
					model.ResourcesCode=dt.Rows[n]["ResourcesCode"].ToString();
					model.ResourcesUnit=dt.Rows[n]["ResourcesUnit"].ToString();
					if(dt.Rows[n]["ResourcesCount"].ToString()!="")
					{
						model.ResourcesCount=decimal.Parse(dt.Rows[n]["ResourcesCount"].ToString());
					}
					model.ResourcesPriceUnit=dt.Rows[n]["ResourcesPriceUnit"].ToString();
					if(dt.Rows[n]["ResourcesSalePrice"].ToString()!="")
					{
						model.ResourcesSalePrice=decimal.Parse(dt.Rows[n]["ResourcesSalePrice"].ToString());
					}
					if(dt.Rows[n]["ResourcesDisCountPrice"].ToString()!="")
					{
						model.ResourcesDisCountPrice=decimal.Parse(dt.Rows[n]["ResourcesDisCountPrice"].ToString());
					}
					if(dt.Rows[n]["IsRelease"].ToString()!="")
					{
						if((dt.Rows[n]["IsRelease"].ToString()=="1")||(dt.Rows[n]["IsRelease"].ToString().ToLower()=="true"))
						{
						model.IsRelease=true;
						}
						else
						{
							model.IsRelease=false;
						}
					}
					model.ScheduleType=dt.Rows[n]["ScheduleType"].ToString();
					if(dt.Rows[n]["IsStopRelease"].ToString()!="")
					{
						if((dt.Rows[n]["IsStopRelease"].ToString()=="1")||(dt.Rows[n]["IsStopRelease"].ToString().ToLower()=="true"))
						{
						model.IsStopRelease=true;
						}
						else
						{
							model.IsStopRelease=false;
						}
					}
					model.Remark=dt.Rows[n]["Remark"].ToString();
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

