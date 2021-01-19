using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_HSPR_IncidentType ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_HSPR_IncidentType
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_IncidentType dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_IncidentType();
		public Bll_Tb_HSPR_IncidentType()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long TypeID)
		{
			return dal.Exists(TypeID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_IncidentType model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_IncidentType model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long TypeID)
		{
			
			dal.Delete(TypeID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_IncidentType GetModel(long TypeID)
		{
			
			return dal.GetModel(TypeID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_IncidentType GetModelByCache(long TypeID)
		{
			
			string CacheKey = "Tb_HSPR_IncidentTypeModel-" + TypeID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(TypeID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_IncidentType)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_IncidentType> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_IncidentType> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_IncidentType> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_IncidentType>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_IncidentType model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_IncidentType();
					//model.TypeID=dt.Rows[n]["TypeID"].ToString();
					//model.CorpTypeID=dt.Rows[n]["CorpTypeID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.TypeCode=dt.Rows[n]["TypeCode"].ToString();
					model.TypeName=dt.Rows[n]["TypeName"].ToString();
					if(dt.Rows[n]["DealLimit"].ToString()!="")
					{
						model.DealLimit=int.Parse(dt.Rows[n]["DealLimit"].ToString());
					}
					if(dt.Rows[n]["ReserveHint"].ToString()!="")
					{
						model.ReserveHint=int.Parse(dt.Rows[n]["ReserveHint"].ToString());
					}
					model.TypeMemo=dt.Rows[n]["TypeMemo"].ToString();
					if(dt.Rows[n]["IsTreeRoot"].ToString()!="")
					{
						model.IsTreeRoot=int.Parse(dt.Rows[n]["IsTreeRoot"].ToString());
					}
					model.IncidentPlace=dt.Rows[n]["IncidentPlace"].ToString();
					if(dt.Rows[n]["DealLimit2"].ToString()!="")
					{
						model.DealLimit2=int.Parse(dt.Rows[n]["DealLimit2"].ToString());
					}
					if(dt.Rows[n]["ClassID"].ToString()!="")
					{
						model.ClassID=int.Parse(dt.Rows[n]["ClassID"].ToString());
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

