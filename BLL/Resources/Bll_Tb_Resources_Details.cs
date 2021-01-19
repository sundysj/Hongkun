using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Resources;
namespace MobileSoft.BLL.Resources
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Resources_Details 的摘要说明。
	/// </summary>
	public class Bll_Tb_Resources_Details
	{
		private readonly MobileSoft.DAL.Resources.Dal_Tb_Resources_Details dal=new MobileSoft.DAL.Resources.Dal_Tb_Resources_Details();
		public Bll_Tb_Resources_Details()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ResourcesID)
		{
			return dal.Exists(ResourcesID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Resources.Tb_Resources_Details model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_Details model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ResourcesID)
		{
			
			dal.Delete(ResourcesID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_Details GetModel(long ResourcesID)
		{
			
			return dal.GetModel(ResourcesID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_Details GetModelByCache(long ResourcesID)
		{
			
			string CacheKey = "Tb_Resources_DetailsModel-" + ResourcesID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ResourcesID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Resources.Tb_Resources_Details)objModel;
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
		public List<MobileSoft.Model.Resources.Tb_Resources_Details> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Resources.Tb_Resources_Details> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Resources.Tb_Resources_Details> modelList = new List<MobileSoft.Model.Resources.Tb_Resources_Details>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Resources.Tb_Resources_Details model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Resources.Tb_Resources_Details();
					//model.ResourcesID=dt.Rows[n]["ResourcesID"].ToString();
					//model.BussId=dt.Rows[n]["BussId"].ToString();
					//model.ResourcesTypeID=dt.Rows[n]["ResourcesTypeID"].ToString();
					model.ResourcesName=dt.Rows[n]["ResourcesName"].ToString();
					model.ResourcesSimple=dt.Rows[n]["ResourcesSimple"].ToString();
					if(dt.Rows[n]["ResourcesIndex"].ToString()!="")
					{
						model.ResourcesIndex=int.Parse(dt.Rows[n]["ResourcesIndex"].ToString());
					}
					model.ResourcesBarCode=dt.Rows[n]["ResourcesBarCode"].ToString();
					model.ResourcesCode=dt.Rows[n]["ResourcesCode"].ToString();
					model.ResourcesUnit=dt.Rows[n]["ResourcesUnit"].ToString();
					if(dt.Rows[n]["ResourcesCount"].ToString()!="")
					{
						model.ResourcesCount=decimal.Parse(dt.Rows[n]["ResourcesCount"].ToString());
					}
					model.ResourcesPriceUnit=dt.Rows[n]["ResourcesPriceUnit"].ToString();
					if(dt.Rows[n]["ResourcesSalePrice"].ToString()!="")
					{
						model.ResourcesSalePrice=decimal.Parse(dt.Rows[n]["ResourcesSalePrice"].ToString());
					}
					if(dt.Rows[n]["ResourcesDisCountPrice"].ToString()!="")
					{
						model.ResourcesDisCountPrice=decimal.Parse(dt.Rows[n]["ResourcesDisCountPrice"].ToString());
					}
					if(dt.Rows[n]["IsRelease"].ToString()!="")
					{
						if((dt.Rows[n]["IsRelease"].ToString()=="1")||(dt.Rows[n]["IsRelease"].ToString().ToLower()=="true"))
						{
						model.IsRelease=true;
						}
						else
						{
							model.IsRelease=false;
						}
					}
					model.ScheduleType=dt.Rows[n]["ScheduleType"].ToString();
					if(dt.Rows[n]["IsStopRelease"].ToString()!="")
					{
						if((dt.Rows[n]["IsStopRelease"].ToString()=="1")||(dt.Rows[n]["IsStopRelease"].ToString().ToLower()=="true"))
						{
						model.IsStopRelease=true;
						}
						else
						{
							model.IsStopRelease=false;
						}
					}
					model.Remark=dt.Rows[n]["Remark"].ToString();
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

