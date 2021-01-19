using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.System;
namespace MobileSoft.BLL.System
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_System_SmsNatAccount ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_System_SmsNatAccount
	{
		private readonly MobileSoft.DAL.System.Dal_Tb_System_SmsNatAccount dal=new MobileSoft.DAL.System.Dal_Tb_System_SmsNatAccount();
		public Bll_Tb_System_SmsNatAccount()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(Guid CommCode,long CommID)
		{
			return dal.Exists(CommCode,CommID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.System.Tb_System_SmsNatAccount model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_SmsNatAccount model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(Guid CommCode,long CommID)
		{
			
			dal.Delete(CommCode,CommID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.System.Tb_System_SmsNatAccount GetModel(Guid CommCode,long CommID)
		{
			
			return dal.GetModel(CommCode,CommID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.System.Tb_System_SmsNatAccount GetModelByCache(Guid CommCode,long CommID)
		{
			
			string CacheKey = "Tb_System_SmsNatAccountModel-" + CommCode+CommID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CommCode,CommID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.System.Tb_System_SmsNatAccount)objModel;
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
		public List<MobileSoft.Model.System.Tb_System_SmsNatAccount> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.System.Tb_System_SmsNatAccount> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.System.Tb_System_SmsNatAccount> modelList = new List<MobileSoft.Model.System.Tb_System_SmsNatAccount>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.System.Tb_System_SmsNatAccount model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.System.Tb_System_SmsNatAccount();
					if(dt.Rows[n]["CommCode"].ToString()!="")
					{
						model.CommCode=new Guid(dt.Rows[n]["CommCode"].ToString());
					}
					//model.CommID=dt.Rows[n]["CommID"].ToString();
					model.Circle=dt.Rows[n]["Circle"].ToString();
					model.PassWord=dt.Rows[n]["PassWord"].ToString();
					if(dt.Rows[n]["Balance"].ToString()!="")
					{
						model.Balance=int.Parse(dt.Rows[n]["Balance"].ToString());
					}
					if(dt.Rows[n]["WayType"].ToString()!="")
					{
						model.WayType=int.Parse(dt.Rows[n]["WayType"].ToString());
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

