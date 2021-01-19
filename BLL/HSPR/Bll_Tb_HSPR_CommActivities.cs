using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_HSPR_CommActivities ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_HSPR_CommActivities
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CommActivities dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CommActivities();
		public Bll_Tb_HSPR_CommActivities()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string ActivitiesID)
		{
			return dal.Exists(ActivitiesID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CommActivities model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CommActivities model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string ActivitiesID)
		{
			
			dal.Delete(ActivitiesID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommActivities GetModel(string ActivitiesID)
		{
			
			return dal.GetModel(ActivitiesID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommActivities GetModelByCache(string ActivitiesID)
		{
			
			string CacheKey = "Tb_HSPR_CommActivitiesModel-" + ActivitiesID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ActivitiesID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_CommActivities)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CommActivities> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CommActivities> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_CommActivities> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_CommActivities>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_CommActivities model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_CommActivities();
					model.ActivitiesID=dt.Rows[n]["ActivitiesID"].ToString();
					//model.CommID=dt.Rows[n]["CommID"].ToString();
					model.ActivitiesType=dt.Rows[n]["ActivitiesType"].ToString();
					model.ActivitiesTheme=dt.Rows[n]["ActivitiesTheme"].ToString();
					model.ActivitiesContent=dt.Rows[n]["ActivitiesContent"].ToString();
					if(dt.Rows[n]["ActivitiesStartDate"].ToString()!="")
					{
						model.ActivitiesStartDate=DateTime.Parse(dt.Rows[n]["ActivitiesStartDate"].ToString());
					}
					if(dt.Rows[n]["ActivitiesEndDate"].ToString()!="")
					{
						model.ActivitiesEndDate=DateTime.Parse(dt.Rows[n]["ActivitiesEndDate"].ToString());
					}
					model.ActivitiesPlan=dt.Rows[n]["ActivitiesPlan"].ToString();
					model.CustName=dt.Rows[n]["CustName"].ToString();
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					model.RoomSign=dt.Rows[n]["RoomSign"].ToString();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					model.LinkPhone=dt.Rows[n]["LinkPhone"].ToString();
					model.ActivitiesImages=dt.Rows[n]["ActivitiesImages"].ToString();
					if(dt.Rows[n]["IssueDate"].ToString()!="")
					{
						model.IssueDate=DateTime.Parse(dt.Rows[n]["IssueDate"].ToString());
					}
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

