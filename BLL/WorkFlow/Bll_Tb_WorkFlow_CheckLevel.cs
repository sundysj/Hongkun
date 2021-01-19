using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.WorkFlow;
namespace MobileSoft.BLL.WorkFlow
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_WorkFlow_CheckLevel ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_WorkFlow_CheckLevel
	{
		private readonly MobileSoft.DAL.WorkFlow.Dal_Tb_WorkFlow_CheckLevel dal=new MobileSoft.DAL.WorkFlow.Dal_Tb_WorkFlow_CheckLevel();
		public Bll_Tb_WorkFlow_CheckLevel()
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
		public bool Exists(int InfoId)
		{
			return dal.Exists(InfoId);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_CheckLevel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_CheckLevel model)
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
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_CheckLevel GetModel(int InfoId)
		{
			
			return dal.GetModel(InfoId);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_CheckLevel GetModelByCache(int InfoId)
		{
			
			string CacheKey = "Tb_WorkFlow_CheckLevelModel-" + InfoId;
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
			return (MobileSoft.Model.WorkFlow.Tb_WorkFlow_CheckLevel)objModel;
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
		public List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_CheckLevel> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_CheckLevel> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_CheckLevel> modelList = new List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_CheckLevel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.WorkFlow.Tb_WorkFlow_CheckLevel model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.WorkFlow.Tb_WorkFlow_CheckLevel();
					if(dt.Rows[n]["InfoId"].ToString()!="")
					{
						model.InfoId=int.Parse(dt.Rows[n]["InfoId"].ToString());
					}
					if(dt.Rows[n]["InstanceInfoId"].ToString()!="")
					{
						model.InstanceInfoId=int.Parse(dt.Rows[n]["InstanceInfoId"].ToString());
					}
					if(dt.Rows[n]["WorkFlowInfoId"].ToString()!="")
					{
						model.WorkFlowInfoId=int.Parse(dt.Rows[n]["WorkFlowInfoId"].ToString());
					}
					model.StartUserCode=dt.Rows[n]["StartUserCode"].ToString();
					model.CheckUserCode=dt.Rows[n]["CheckUserCode"].ToString();
					if(dt.Rows[n]["CheckType"].ToString()!="")
					{
						model.CheckType=int.Parse(dt.Rows[n]["CheckType"].ToString());
					}
					if(dt.Rows[n]["OprState"].ToString()!="")
					{
						model.OprState=int.Parse(dt.Rows[n]["OprState"].ToString());
					}
					if(dt.Rows[n]["RecordDate"].ToString()!="")
					{
						model.RecordDate=DateTime.Parse(dt.Rows[n]["RecordDate"].ToString());
					}
					if(dt.Rows[n]["Sort"].ToString()!="")
					{
						model.Sort=int.Parse(dt.Rows[n]["Sort"].ToString());
					}
					if(dt.Rows[n]["IsGoToNext"].ToString()!="")
					{
						model.IsGoToNext=int.Parse(dt.Rows[n]["IsGoToNext"].ToString());
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

            public DataTable IsHavePrintCheck(int InstanceId, string UserCode)
            {
                  return dal.IsHavePrintCheck(InstanceId, UserCode);
            }

            public DataTable CheckLevelFilter(int InstanceInfoId)
            {
                  return dal.CheckLevelFilter(InstanceInfoId);
            }

            public void WorkFlowCheckLevelUpdate(int InstanceInfoId, int WorkFlowInfoId, string UserCode)
            {
                  dal.WorkFlowCheckLevelUpdate(InstanceInfoId, WorkFlowInfoId, UserCode);
            }
	}
}

