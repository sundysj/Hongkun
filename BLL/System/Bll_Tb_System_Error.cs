using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.System;
namespace MobileSoft.BLL.System
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_System_Error ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_System_Error
	{
		private readonly MobileSoft.DAL.System.Dal_Tb_System_Error dal=new MobileSoft.DAL.System.Dal_Tb_System_Error();
		public Bll_Tb_System_Error()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(Guid ErrorCode)
		{
			return dal.Exists(ErrorCode);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.System.Tb_System_Error model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Error model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(Guid ErrorCode)
		{
			
			dal.Delete(ErrorCode);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Error GetModel(Guid ErrorCode)
		{
			
			return dal.GetModel(ErrorCode);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Error GetModelByCache(Guid ErrorCode)
		{
			
			string CacheKey = "Tb_System_ErrorModel-" + ErrorCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ErrorCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.System.Tb_System_Error)objModel;
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
		public List<MobileSoft.Model.System.Tb_System_Error> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.System.Tb_System_Error> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.System.Tb_System_Error> modelList = new List<MobileSoft.Model.System.Tb_System_Error>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.System.Tb_System_Error model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.System.Tb_System_Error();
					if(dt.Rows[n]["ErrorCode"].ToString()!="")
					{
						model.ErrorCode=new Guid(dt.Rows[n]["ErrorCode"].ToString());
					}
					if(dt.Rows[n]["ErrorTime"].ToString()!="")
					{
						model.ErrorTime=DateTime.Parse(dt.Rows[n]["ErrorTime"].ToString());
					}
					model.ErrorURL=dt.Rows[n]["ErrorURL"].ToString();
					model.ErrorSource=dt.Rows[n]["ErrorSource"].ToString();
					model.ErrorMessage=dt.Rows[n]["ErrorMessage"].ToString();
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

