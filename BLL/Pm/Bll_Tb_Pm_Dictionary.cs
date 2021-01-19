using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using Mobile.Model.Pm;
namespace Mobile.BLL.Pm
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Pm_Dictionary ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Pm_Dictionary
	{
		private readonly Mobile.DAL.Pm.Dal_Tb_Pm_Dictionary dal=new Mobile.DAL.Pm.Dal_Tb_Pm_Dictionary();
		public Bll_Tb_Pm_Dictionary()
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
		public void Add(Mobile.Model.Pm.Tb_Pm_Dictionary model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(Mobile.Model.Pm.Tb_Pm_Dictionary model)
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
		public Mobile.Model.Pm.Tb_Pm_Dictionary GetModel(string Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public Mobile.Model.Pm.Tb_Pm_Dictionary GetModelByCache(string Id)
		{
			
			string CacheKey = "Tb_Pm_DictionaryModel-" + Id;
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
			return (Mobile.Model.Pm.Tb_Pm_Dictionary)objModel;
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
		public List<Mobile.Model.Pm.Tb_Pm_Dictionary> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<Mobile.Model.Pm.Tb_Pm_Dictionary> DataTableToList(DataTable dt)
		{
			List<Mobile.Model.Pm.Tb_Pm_Dictionary> modelList = new List<Mobile.Model.Pm.Tb_Pm_Dictionary>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Mobile.Model.Pm.Tb_Pm_Dictionary model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Mobile.Model.Pm.Tb_Pm_Dictionary();
					model.Id=dt.Rows[n]["Id"].ToString();
					model.DType=dt.Rows[n]["DType"].ToString();
					model.Name=dt.Rows[n]["Name"].ToString();
					model.Code=dt.Rows[n]["Code"].ToString();
					model.Sort=dt.Rows[n]["Sort"].ToString();
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

