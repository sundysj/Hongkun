using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.OAPublicWork;
namespace MobileSoft.BLL.OAPublicWork
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_OAPublicWork_FixedAssetsLossTable ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_OAPublicWork_FixedAssetsLossTable
	{
		private readonly MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_FixedAssetsLossTable dal=new MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_FixedAssetsLossTable();
		public Bll_Tb_OAPublicWork_FixedAssetsLossTable()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int InfoID)
		{
			return dal.Exists(InfoID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTable model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTable model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int InfoID)
		{
			
			dal.Delete(InfoID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTable GetModel(int InfoID)
		{
			
			return dal.GetModel(InfoID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTable GetModelByCache(int InfoID)
		{
			
			string CacheKey = "Tb_OAPublicWork_FixedAssetsLossTableModel-" + InfoID;
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
			return (MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTable)objModel;
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
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTable> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTable> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTable> modelList = new List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTable>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTable model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTable();
					if(dt.Rows[n]["InfoID"].ToString()!="")
					{
						model.InfoID=int.Parse(dt.Rows[n]["InfoID"].ToString());
					}
					if(dt.Rows[n]["Tb_WorkFlow_FlowSort_InfoId"].ToString()!="")
					{
						model.Tb_WorkFlow_FlowSort_InfoId=int.Parse(dt.Rows[n]["Tb_WorkFlow_FlowSort_InfoId"].ToString());
					}
					model.UserCode=dt.Rows[n]["UserCode"].ToString();
					model.LossUserName=dt.Rows[n]["LossUserName"].ToString();
					model.LossAssets=dt.Rows[n]["LossAssets"].ToString();
					model.LossDepart=dt.Rows[n]["LossDepart"].ToString();
					if(dt.Rows[n]["LossDate"].ToString()!="")
					{
						model.LossDate=DateTime.Parse(dt.Rows[n]["LossDate"].ToString());
					}
					model.LossMark=dt.Rows[n]["LossMark"].ToString();
					if(dt.Rows[n]["WorkStartDate"].ToString()!="")
					{
						model.WorkStartDate=DateTime.Parse(dt.Rows[n]["WorkStartDate"].ToString());
					}
					model.DocumentUrl=dt.Rows[n]["DocumentUrl"].ToString();
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

