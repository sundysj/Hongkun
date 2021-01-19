using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using HM.Model.Eq;
namespace HM.BLL.Eq
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_EQ_EquipmentFile ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_EQ_EquipmentFile
	{
		private readonly HM.DAL.Eq.Dal_Tb_EQ_EquipmentFile dal=new HM.DAL.Eq.Dal_Tb_EQ_EquipmentFile();
		public Bll_Tb_EQ_EquipmentFile()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(HM.Model.Eq.Tb_EQ_EquipmentFile model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(HM.Model.Eq.Tb_EQ_EquipmentFile model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string Id)
		{
			
			dal.Delete(Id);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public HM.Model.Eq.Tb_EQ_EquipmentFile GetModel(string Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public HM.Model.Eq.Tb_EQ_EquipmentFile GetModelByCache(string Id)
		{
			
			string CacheKey = "Tb_EQ_EquipmentFileModel-" + Id;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Id);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (HM.Model.Eq.Tb_EQ_EquipmentFile)objModel;
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
		public List<HM.Model.Eq.Tb_EQ_EquipmentFile> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<HM.Model.Eq.Tb_EQ_EquipmentFile> DataTableToList(DataTable dt)
		{
			List<HM.Model.Eq.Tb_EQ_EquipmentFile> modelList = new List<HM.Model.Eq.Tb_EQ_EquipmentFile>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				HM.Model.Eq.Tb_EQ_EquipmentFile model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new HM.Model.Eq.Tb_EQ_EquipmentFile();
					model.Id=dt.Rows[n]["Id"].ToString();
					model.EquiId=dt.Rows[n]["EquiId"].ToString();
					model.FileName=dt.Rows[n]["FileName"].ToString();
					model.Fix=dt.Rows[n]["Fix"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					model.FilePath=dt.Rows[n]["FilePath"].ToString();
					model.PhoneName=dt.Rows[n]["PhoneName"].ToString();
					if(dt.Rows[n]["PhotoTime"].ToString()!="")
					{
						model.PhotoTime=DateTime.Parse(dt.Rows[n]["PhotoTime"].ToString());
					}
					model.PhotoPId=dt.Rows[n]["PhotoPId"].ToString();
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

