using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.WorkFlow;
namespace MobileSoft.BLL.WorkFlow
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_WorkFlow_WorkFlowOverNoticeUser ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_WorkFlow_WorkFlowOverNoticeUser
	{
		private readonly MobileSoft.DAL.WorkFlow.Dal_Tb_WorkFlow_WorkFlowOverNoticeUser dal=new MobileSoft.DAL.WorkFlow.Dal_Tb_WorkFlow_WorkFlowOverNoticeUser();
		public Bll_Tb_WorkFlow_WorkFlowOverNoticeUser()
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
		public int  Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_WorkFlowOverNoticeUser model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_WorkFlowOverNoticeUser model)
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
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_WorkFlowOverNoticeUser GetModel(int InfoId)
		{
			
			return dal.GetModel(InfoId);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_WorkFlowOverNoticeUser GetModelByCache(int InfoId)
		{
			
			string CacheKey = "Tb_WorkFlow_WorkFlowOverNoticeUserModel-" + InfoId;
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
			return (MobileSoft.Model.WorkFlow.Tb_WorkFlow_WorkFlowOverNoticeUser)objModel;
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
		public List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_WorkFlowOverNoticeUser> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_WorkFlowOverNoticeUser> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_WorkFlowOverNoticeUser> modelList = new List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_WorkFlowOverNoticeUser>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.WorkFlow.Tb_WorkFlow_WorkFlowOverNoticeUser model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.WorkFlow.Tb_WorkFlow_WorkFlowOverNoticeUser();
					if(dt.Rows[n]["InfoId"].ToString()!="")
					{
						model.InfoId=int.Parse(dt.Rows[n]["InfoId"].ToString());
					}
					if(dt.Rows[n]["Tb_WorkFlow_Instance_InfoId"].ToString()!="")
					{
						model.Tb_WorkFlow_Instance_InfoId=int.Parse(dt.Rows[n]["Tb_WorkFlow_Instance_InfoId"].ToString());
					}
					model.Tb_Sys_User_UserCode=dt.Rows[n]["Tb_Sys_User_UserCode"].ToString();
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

