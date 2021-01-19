using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using HM.Model.Eq;
namespace HM.BLL.Eq
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_EQ_TaskEquipment ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_EQ_TaskEquipment
	{
		private readonly HM.DAL.Eq.Dal_Tb_EQ_TaskEquipment dal=new HM.DAL.Eq.Dal_Tb_EQ_TaskEquipment();
		public Bll_Tb_EQ_TaskEquipment()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string TaskEqId)
		{
			return dal.Exists(TaskEqId);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(HM.Model.Eq.Tb_EQ_TaskEquipment model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(HM.Model.Eq.Tb_EQ_TaskEquipment model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string TaskEqId)
		{
			
			dal.Delete(TaskEqId);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public HM.Model.Eq.Tb_EQ_TaskEquipment GetModel(string TaskEqId)
		{
			
			return dal.GetModel(TaskEqId);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public HM.Model.Eq.Tb_EQ_TaskEquipment GetModelByCache(string TaskEqId)
		{
			
			string CacheKey = "Tb_EQ_TaskEquipmentModel-" + TaskEqId;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(TaskEqId);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (HM.Model.Eq.Tb_EQ_TaskEquipment)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<HM.Model.Eq.Tb_EQ_TaskEquipment> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<HM.Model.Eq.Tb_EQ_TaskEquipment> DataTableToList(DataTable dt)
		{
			List<HM.Model.Eq.Tb_EQ_TaskEquipment> modelList = new List<HM.Model.Eq.Tb_EQ_TaskEquipment>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				HM.Model.Eq.Tb_EQ_TaskEquipment model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new HM.Model.Eq.Tb_EQ_TaskEquipment();
					model.TaskEqId=dt.Rows[n]["TaskEqId"].ToString();
					model.TaskId=dt.Rows[n]["TaskId"].ToString();
					model.EquiId=dt.Rows[n]["EquiId"].ToString();
					model.PollingNote=dt.Rows[n]["PollingNote"].ToString();
					model.PollingResult=dt.Rows[n]["PollingResult"].ToString();
					model.IsMend=dt.Rows[n]["IsMend"].ToString();
					model.BSBH=dt.Rows[n]["BSBH"].ToString();
					model.IsHandle=dt.Rows[n]["IsHandle"].ToString();
					model.HandlePId=dt.Rows[n]["HandlePId"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["AddDate"].ToString()!="")
					{
						model.AddDate=DateTime.Parse(dt.Rows[n]["AddDate"].ToString());
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

