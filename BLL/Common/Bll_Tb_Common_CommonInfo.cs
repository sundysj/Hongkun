using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Common;
namespace MobileSoft.BLL.Common
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Common_CommonInfo ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Common_CommonInfo
	{
		private readonly MobileSoft.DAL.Common.Dal_Tb_Common_CommonInfo dal=new MobileSoft.DAL.Common.Dal_Tb_Common_CommonInfo();
		public Bll_Tb_Common_CommonInfo()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long IID)
		{
			return dal.Exists(IID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(MobileSoft.Model.Common.Tb_Common_CommonInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Common.Tb_Common_CommonInfo model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long IID)
		{
			
			dal.Delete(IID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Common.Tb_Common_CommonInfo GetModel(long IID)
		{
			
			return dal.GetModel(IID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Common.Tb_Common_CommonInfo GetModelByCache(long IID)
		{
			
			string CacheKey = "Tb_Common_CommonInfoModel-" + IID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(IID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Common.Tb_Common_CommonInfo)objModel;
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
		public List<MobileSoft.Model.Common.Tb_Common_CommonInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Common.Tb_Common_CommonInfo> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Common.Tb_Common_CommonInfo> modelList = new List<MobileSoft.Model.Common.Tb_Common_CommonInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Common.Tb_Common_CommonInfo model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Common.Tb_Common_CommonInfo();
					//model.IID=dt.Rows[n]["IID"].ToString();
					model.DepCode=dt.Rows[n]["DepCode"].ToString();
					model.OrganCode=dt.Rows[n]["OrganCode"].ToString();
					model.TypeCode=dt.Rows[n]["TypeCode"].ToString();
					model.Title=dt.Rows[n]["Title"].ToString();
					model.Content=dt.Rows[n]["Content"].ToString();
					model.UserName=dt.Rows[n]["UserName"].ToString();
					if(dt.Rows[n]["IssueDate"].ToString()!="")
					{
						model.IssueDate=DateTime.Parse(dt.Rows[n]["IssueDate"].ToString());
					}
					model.Type=dt.Rows[n]["Type"].ToString();
					model.ReadDepartName=dt.Rows[n]["ReadDepartName"].ToString();
					model.ReadDepartCode=dt.Rows[n]["ReadDepartCode"].ToString();
					model.ReadUserName=dt.Rows[n]["ReadUserName"].ToString();
					model.ReadUserCode=dt.Rows[n]["ReadUserCode"].ToString();
					model.HaveReadUserName=dt.Rows[n]["HaveReadUserName"].ToString();
					model.HaveReadUserCode=dt.Rows[n]["HaveReadUserCode"].ToString();
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

        public DataTable Common_CommonInfo_TopNum(int TopNum, string SQLEx)
        {
            return dal.Common_CommonInfo_TopNum(TopNum, SQLEx);
        }
	}
}

