using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.OAPublicWork;
namespace MobileSoft.BLL.OAPublicWork
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_OAPublicWork_WorkContact ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_OAPublicWork_WorkContact
	{
		private readonly MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_WorkContact dal=new MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_WorkContact();
		public Bll_Tb_OAPublicWork_WorkContact()
		{}
		#region  ��Ա����
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
		public int  Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkContact model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkContact model)
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
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkContact GetModel(int InfoId)
		{
			
			return dal.GetModel(InfoId);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkContact GetModelByCache(int InfoId)
		{
			
			string CacheKey = "Tb_OAPublicWork_WorkContactModel-" + InfoId;
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
			return (MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkContact)objModel;
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
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkContact> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkContact> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkContact> modelList = new List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkContact>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkContact model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkContact();
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
					model.WorkSper=dt.Rows[n]["WorkSper"].ToString();
					model.Ask=dt.Rows[n]["Ask"].ToString();
					if(dt.Rows[n]["ConferenceEndDate"].ToString()!="")
					{
						model.ConferenceEndDate=DateTime.Parse(dt.Rows[n]["ConferenceEndDate"].ToString());
					}
					if(dt.Rows[n]["WorkEndDate"].ToString()!="")
					{
						model.WorkEndDate=DateTime.Parse(dt.Rows[n]["WorkEndDate"].ToString());
					}
					model.ContactImportant=dt.Rows[n]["ContactImportant"].ToString();
					model.InfoContent=dt.Rows[n]["InfoContent"].ToString();
					model.DocumentUrl=dt.Rows[n]["DocumentUrl"].ToString();
					if(dt.Rows[n]["ArrangeDate"].ToString()!="")
					{
						model.ArrangeDate=DateTime.Parse(dt.Rows[n]["ArrangeDate"].ToString());
					}
					model.AssumeDepart=dt.Rows[n]["AssumeDepart"].ToString();
					model.AssumeUser=dt.Rows[n]["AssumeUser"].ToString();
					model.CooperDepart=dt.Rows[n]["CooperDepart"].ToString();
					model.CooperUser=dt.Rows[n]["CooperUser"].ToString();
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

