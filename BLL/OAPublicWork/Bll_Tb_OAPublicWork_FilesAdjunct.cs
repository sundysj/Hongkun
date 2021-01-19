using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.OAPublicWork;
namespace MobileSoft.BLL.OAPublicWork
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_OAPublicWork_FilesAdjunct ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_OAPublicWork_FilesAdjunct
	{
		private readonly MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_FilesAdjunct dal=new MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_FilesAdjunct();
		public Bll_Tb_OAPublicWork_FilesAdjunct()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long InfoId)
		{
			return dal.Exists(InfoId);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FilesAdjunct model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FilesAdjunct model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long InfoId)
		{
			
			dal.Delete(InfoId);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FilesAdjunct GetModel(long InfoId)
		{
			
			return dal.GetModel(InfoId);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FilesAdjunct GetModelByCache(long InfoId)
		{
			
			string CacheKey = "Tb_OAPublicWork_FilesAdjunctModel-" + InfoId;
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
			return (MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FilesAdjunct)objModel;
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
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FilesAdjunct> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FilesAdjunct> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FilesAdjunct> modelList = new List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FilesAdjunct>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FilesAdjunct model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FilesAdjunct();
					//model.InfoId=dt.Rows[n]["InfoId"].ToString();
					model.DictionaryCode=dt.Rows[n]["DictionaryCode"].ToString();
					if(dt.Rows[n]["InstanceId"].ToString()!="")
					{
						model.InstanceId=int.Parse(dt.Rows[n]["InstanceId"].ToString());
					}
					model.UserFilesCode=dt.Rows[n]["UserFilesCode"].ToString();
					model.AdjunctName=dt.Rows[n]["AdjunctName"].ToString();
					model.FilPath=dt.Rows[n]["FilPath"].ToString();
					model.FileExName=dt.Rows[n]["FileExName"].ToString();
					model.FileSize=dt.Rows[n]["FileSize"].ToString();
					model.IsCanDown=dt.Rows[n]["IsCanDown"].ToString();
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

            public DataTable OAPublicWork_FilesAdjunct_GetFilter(string InstanceId, string DictionaryCode)
            {
                  return dal.OAPublicWork_FilesAdjunct_GetFilter(InstanceId, DictionaryCode);
            }
	}
}

