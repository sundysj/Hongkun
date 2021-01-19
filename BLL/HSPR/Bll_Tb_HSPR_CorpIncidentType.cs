using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_HSPR_CorpIncidentType 的摘要说明。
	/// </summary>
	public class Bll_Tb_HSPR_CorpIncidentType
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CorpIncidentType dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CorpIncidentType();
		public Bll_Tb_HSPR_CorpIncidentType()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long CorpTypeID)
		{
			return dal.Exists(CorpTypeID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CorpIncidentType model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CorpIncidentType model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long CorpTypeID)
		{
			
			dal.Delete(CorpTypeID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CorpIncidentType GetModel(long CorpTypeID)
		{
			
			return dal.GetModel(CorpTypeID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CorpIncidentType GetModelByCache(long CorpTypeID)
		{
			
			string CacheKey = "Tb_HSPR_CorpIncidentTypeModel-" + CorpTypeID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CorpTypeID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_CorpIncidentType)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string fieldOrder)
		{
			return dal.GetList(Top,strWhere,fieldOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CorpIncidentType> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CorpIncidentType> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_CorpIncidentType> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_CorpIncidentType>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_CorpIncidentType model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_CorpIncidentType();
					//model.CorpTypeID=dt.Rows[n]["CorpTypeID"].ToString();
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
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
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
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize,string SortField,int Sort)
		{
			return dal.GetList(out PageCount, out Counts, StrCondition, PageIndex, PageSize,SortField,Sort);
		}

		#endregion  成员方法
	}
}

