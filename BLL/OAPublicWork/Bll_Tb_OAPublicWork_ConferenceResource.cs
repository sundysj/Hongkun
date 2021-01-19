using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.OAPublicWork;
namespace MobileSoft.BLL.OAPublicWork
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_OAPublicWork_ConferenceResource ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_OAPublicWork_ConferenceResource
	{
		private readonly MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_ConferenceResource dal=new MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_ConferenceResource();
		public Bll_Tb_OAPublicWork_ConferenceResource()
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
		public int  Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ConferenceResource model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ConferenceResource model)
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
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ConferenceResource GetModel(int InfoId)
		{
			
			return dal.GetModel(InfoId);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ConferenceResource GetModelByCache(int InfoId)
		{
			
			string CacheKey = "Tb_OAPublicWork_ConferenceResourceModel-" + InfoId;
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
			return (MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ConferenceResource)objModel;
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
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ConferenceResource> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ConferenceResource> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ConferenceResource> modelList = new List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ConferenceResource>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ConferenceResource model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ConferenceResource();
					if(dt.Rows[n]["InfoId"].ToString()!="")
					{
						model.InfoId=int.Parse(dt.Rows[n]["InfoId"].ToString());
					}
					model.ConferenceName=dt.Rows[n]["ConferenceName"].ToString();
					model.ConferencePlace=dt.Rows[n]["ConferencePlace"].ToString();
					model.ConferenceMj=dt.Rows[n]["ConferenceMj"].ToString();
					model.ConferenceHowMany=dt.Rows[n]["ConferenceHowMany"].ToString();
					model.ConferenceThing=dt.Rows[n]["ConferenceThing"].ToString();
					model.ConferencePrice=dt.Rows[n]["ConferencePrice"].ToString();
					model.ConferencePassWord=dt.Rows[n]["ConferencePassWord"].ToString();
					model.Mark=dt.Rows[n]["Mark"].ToString();
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

