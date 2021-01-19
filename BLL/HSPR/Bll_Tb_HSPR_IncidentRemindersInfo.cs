using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_HSPR_IncidentRemindersInfo ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_HSPR_IncidentRemindersInfo
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_IncidentRemindersInfo dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_IncidentRemindersInfo();
		public Bll_Tb_HSPR_IncidentRemindersInfo()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long InfoID)
		{
			return dal.Exists(InfoID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public long Add(MobileSoft.Model.HSPR.Tb_HSPR_IncidentRemindersInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_IncidentRemindersInfo model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long InfoID)
		{
			
			dal.Delete(InfoID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_IncidentRemindersInfo GetModel(long InfoID)
		{
			
			return dal.GetModel(InfoID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_IncidentRemindersInfo GetModelByCache(long InfoID)
		{
			
			string CacheKey = "Tb_HSPR_IncidentRemindersInfoModel-" + InfoID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(InfoID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_IncidentRemindersInfo)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_IncidentRemindersInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_IncidentRemindersInfo> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_IncidentRemindersInfo> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_IncidentRemindersInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_IncidentRemindersInfo model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_IncidentRemindersInfo();
					//model.InfoID=dt.Rows[n]["InfoID"].ToString();
					//model.IncidentID=dt.Rows[n]["IncidentID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.RemindersType=dt.Rows[n]["RemindersType"].ToString();
					if(dt.Rows[n]["RemindersDate"].ToString()!="")
					{
						model.RemindersDate=DateTime.Parse(dt.Rows[n]["RemindersDate"].ToString());
					}
					model.UserID=dt.Rows[n]["UserID"].ToString();
					model.UserName=dt.Rows[n]["UserName"].ToString();
					model.InfoContent=dt.Rows[n]["InfoContent"].ToString();
					model.Remark=dt.Rows[n]["Remark"].ToString();
					if(dt.Rows[n]["IsSendMessage"].ToString()!="")
					{
						if((dt.Rows[n]["IsSendMessage"].ToString()=="1")||(dt.Rows[n]["IsSendMessage"].ToString().ToLower()=="true"))
						{
						model.IsSendMessage=true;
						}
						else
						{
							model.IsSendMessage=false;
						}
					}
					model.SendMessage=dt.Rows[n]["SendMessage"].ToString();
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

