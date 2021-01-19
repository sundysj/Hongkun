using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Resources;
namespace MobileSoft.BLL.Resources
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Resources_ReleaseMember ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Resources_ReleaseMember
	{
		private readonly MobileSoft.DAL.Resources.Dal_Tb_Resources_ReleaseMember dal=new MobileSoft.DAL.Resources.Dal_Tb_Resources_ReleaseMember();
		public Bll_Tb_Resources_ReleaseMember()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long ReleaseMemberID)
		{
			return dal.Exists(ReleaseMemberID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
        public long Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseMember model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseMember model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long ReleaseMemberID)
		{
			
			dal.Delete(ReleaseMemberID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseMember GetModel(long ReleaseMemberID)
		{
			
			return dal.GetModel(ReleaseMemberID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseMember GetModelByCache(long ReleaseMemberID)
		{
			
			string CacheKey = "Tb_Resources_ReleaseMemberModel-" + ReleaseMemberID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ReleaseMemberID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Resources.Tb_Resources_ReleaseMember)objModel;
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
		public List<MobileSoft.Model.Resources.Tb_Resources_ReleaseMember> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Resources.Tb_Resources_ReleaseMember> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Resources.Tb_Resources_ReleaseMember> modelList = new List<MobileSoft.Model.Resources.Tb_Resources_ReleaseMember>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Resources.Tb_Resources_ReleaseMember model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Resources.Tb_Resources_ReleaseMember();
					//model.ReleaseMemberID=dt.Rows[n]["ReleaseMemberID"].ToString();
					model.ReleaseMemberContent=dt.Rows[n]["ReleaseMemberContent"].ToString();
					model.ReleaseMemberServiceText=dt.Rows[n]["ReleaseMemberServiceText"].ToString();
					model.ReleaseMemberNeedKnow=dt.Rows[n]["ReleaseMemberNeedKnow"].ToString();
					//model.ReleaseID=dt.Rows[n]["ReleaseID"].ToString();
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

