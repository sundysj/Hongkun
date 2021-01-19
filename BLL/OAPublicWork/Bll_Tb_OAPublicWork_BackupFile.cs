using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.OAPublicWork;
namespace MobileSoft.BLL.OAPublicWork
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_OAPublicWork_BackupFile ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_OAPublicWork_BackupFile
	{
		private readonly MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_BackupFile dal=new MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_BackupFile();
		public Bll_Tb_OAPublicWork_BackupFile()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string InfoCode)
		{
			return dal.Exists(InfoCode);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string InfoCode)
		{
			
			dal.Delete(InfoCode);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile GetModel(string InfoCode)
		{
			
			return dal.GetModel(InfoCode);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile GetModelByCache(string InfoCode)
		{
			
			string CacheKey = "Tb_OAPublicWork_BackupFileModel-" + InfoCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(InfoCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile)objModel;
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
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile> modelList = new List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile();
					model.InfoCode=dt.Rows[n]["InfoCode"].ToString();
					model.FName=dt.Rows[n]["FName"].ToString();
					model.OriginallyFilePath=dt.Rows[n]["OriginallyFilePath"].ToString();
					model.PresentFilePath=dt.Rows[n]["PresentFilePath"].ToString();
					if(dt.Rows[n]["BackupDate"].ToString()!="")
					{
						model.BackupDate=DateTime.Parse(dt.Rows[n]["BackupDate"].ToString());
					}
					model.RestoreUserName=dt.Rows[n]["RestoreUserName"].ToString();
					if(dt.Rows[n]["RestoreDate"].ToString()!="")
					{
						model.RestoreDate=DateTime.Parse(dt.Rows[n]["RestoreDate"].ToString());
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

