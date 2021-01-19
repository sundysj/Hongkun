using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Unify;
namespace MobileSoft.BLL.Unify
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Unify_InfoDynamic ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Unify_InfoDynamic
	{
        private readonly MobileSoft.DAL.Unify.Dal_Tb_Unify_InfoDynamic dal = new MobileSoft.DAL.Unify.Dal_Tb_Unify_InfoDynamic();
		public Bll_Tb_Unify_InfoDynamic()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long InfoID)
		{
			return dal.Exists(InfoID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public long Add(MobileSoft.Model.Unify.Tb_Unify_InfoDynamic model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Unify.Tb_Unify_InfoDynamic model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long InfoID)
		{
			
			dal.Delete(InfoID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Unify.Tb_Unify_InfoDynamic GetModel(long InfoID)
		{
			
			return dal.GetModel(InfoID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Unify.Tb_Unify_InfoDynamic GetModelByCache(long InfoID)
		{
			
			string CacheKey = "Tb_Unify_InfoDynamicModel-" + InfoID;
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
			return (MobileSoft.Model.Unify.Tb_Unify_InfoDynamic)objModel;
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
        public DataSet GetList(string Top,string Fields, string strWhere)
        {
            return dal.GetList(Top,Fields, strWhere);
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
		public List<MobileSoft.Model.Unify.Tb_Unify_InfoDynamic> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Unify.Tb_Unify_InfoDynamic> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Unify.Tb_Unify_InfoDynamic> modelList = new List<MobileSoft.Model.Unify.Tb_Unify_InfoDynamic>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Unify.Tb_Unify_InfoDynamic model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Unify.Tb_Unify_InfoDynamic();
					//model.InfoID=dt.Rows[n]["InfoID"].ToString();
					if(dt.Rows[n]["InfoType"].ToString()!="")
					{
						model.InfoType=int.Parse(dt.Rows[n]["InfoType"].ToString());
					}
					model.TypeName=dt.Rows[n]["TypeName"].ToString();
					model.Heading=dt.Rows[n]["Heading"].ToString();
					if(dt.Rows[n]["IssueDate"].ToString()!="")
					{
						model.IssueDate=DateTime.Parse(dt.Rows[n]["IssueDate"].ToString());
					}
					model.InfoSource=dt.Rows[n]["InfoSource"].ToString();
					model.ImageUrl=dt.Rows[n]["ImageUrl"].ToString();
					if(dt.Rows[n]["Recommended"].ToString()!="")
					{
						model.Recommended=int.Parse(dt.Rows[n]["Recommended"].ToString());
					}
					if(dt.Rows[n]["HitCount"].ToString()!="")
					{
						model.HitCount=int.Parse(dt.Rows[n]["HitCount"].ToString());
					}
					model.InfoContent=dt.Rows[n]["InfoContent"].ToString();
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

