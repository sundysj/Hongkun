using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Management;
namespace MobileSoft.BLL.Management
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Management_Type ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Management_Type
	{
		private readonly MobileSoft.DAL.Management.Dal_Tb_Management_Type dal=new MobileSoft.DAL.Management.Dal_Tb_Management_Type();
		public Bll_Tb_Management_Type()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string SortCode)
		{
			return dal.Exists(SortCode);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.Management.Tb_Management_Type model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Management.Tb_Management_Type model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string SortCode)
		{
			
			dal.Delete(SortCode);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Management.Tb_Management_Type GetModel(string SortCode)
		{
			
			return dal.GetModel(SortCode);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Management.Tb_Management_Type GetModelByCache(string SortCode)
		{
			
			string CacheKey = "Tb_Management_TypeModel-" + SortCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(SortCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Management.Tb_Management_Type)objModel;
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
		public List<MobileSoft.Model.Management.Tb_Management_Type> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Management.Tb_Management_Type> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Management.Tb_Management_Type> modelList = new List<MobileSoft.Model.Management.Tb_Management_Type>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Management.Tb_Management_Type model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Management.Tb_Management_Type();
					model.Code=dt.Rows[n]["Code"].ToString();
					model.SortCode=dt.Rows[n]["SortCode"].ToString();
					if(dt.Rows[n]["Sort"].ToString()!="")
					{
						model.Sort=int.Parse(dt.Rows[n]["Sort"].ToString());
					}
					model.TypeName=dt.Rows[n]["TypeName"].ToString();
					model.TypeCode=dt.Rows[n]["TypeCode"].ToString();
					model.Memo=dt.Rows[n]["Memo"].ToString();
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

