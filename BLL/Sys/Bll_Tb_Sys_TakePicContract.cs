using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Sys;
namespace MobileSoft.BLL.Sys
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Sys_TakePicContract ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Sys_TakePicContract
	{
		private readonly MobileSoft.DAL.Sys.Dal_Tb_Sys_TakePicContract dal=new MobileSoft.DAL.Sys.Dal_Tb_Sys_TakePicContract();
		public Bll_Tb_Sys_TakePicContract()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long StatID)
		{
			return dal.Exists(StatID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(MobileSoft.Model.Sys.Tb_Sys_TakePicContract model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_TakePicContract model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long StatID)
		{
			
			dal.Delete(StatID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePicContract GetModel(long StatID)
		{
			
			return dal.GetModel(StatID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePicContract GetModelByCache(long StatID)
		{
			
			string CacheKey = "Tb_Sys_TakePicContractModel-" + StatID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(StatID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Sys.Tb_Sys_TakePicContract)objModel;
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
		public List<MobileSoft.Model.Sys.Tb_Sys_TakePicContract> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Sys.Tb_Sys_TakePicContract> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Sys.Tb_Sys_TakePicContract> modelList = new List<MobileSoft.Model.Sys.Tb_Sys_TakePicContract>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Sys.Tb_Sys_TakePicContract model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Sys.Tb_Sys_TakePicContract();
					//model.StatID=dt.Rows[n]["StatID"].ToString();
					if(dt.Rows[n]["StatType"].ToString()!="")
					{
						model.StatType=int.Parse(dt.Rows[n]["StatType"].ToString());
					}
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.OrganCode=dt.Rows[n]["OrganCode"].ToString();
					if(dt.Rows[n]["StatDate"].ToString()!="")
					{
						model.StatDate=DateTime.Parse(dt.Rows[n]["StatDate"].ToString());
					}
					if(dt.Rows[n]["ContractCounts"].ToString()!="")
					{
						model.ContractCounts=int.Parse(dt.Rows[n]["ContractCounts"].ToString());
					}
					if(dt.Rows[n]["ContractCounts1"].ToString()!="")
					{
						model.ContractCounts1=int.Parse(dt.Rows[n]["ContractCounts1"].ToString());
					}
					if(dt.Rows[n]["ContractCounts2"].ToString()!="")
					{
						model.ContractCounts2=int.Parse(dt.Rows[n]["ContractCounts2"].ToString());
					}
					if(dt.Rows[n]["FeesContCounts"].ToString()!="")
					{
						model.FeesContCounts=int.Parse(dt.Rows[n]["FeesContCounts"].ToString());
					}
					if(dt.Rows[n]["FeesContCounts1"].ToString()!="")
					{
						model.FeesContCounts1=int.Parse(dt.Rows[n]["FeesContCounts1"].ToString());
					}
					if(dt.Rows[n]["FeesContCounts2"].ToString()!="")
					{
						model.FeesContCounts2=int.Parse(dt.Rows[n]["FeesContCounts2"].ToString());
					}
					if(dt.Rows[n]["FeesContCounts3"].ToString()!="")
					{
						model.FeesContCounts3=int.Parse(dt.Rows[n]["FeesContCounts3"].ToString());
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

