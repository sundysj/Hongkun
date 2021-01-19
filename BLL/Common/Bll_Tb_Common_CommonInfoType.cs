using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Common;
namespace MobileSoft.BLL.Common
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Common_CommonInfoType 的摘要说明。
	/// </summary>
	public class Bll_Tb_Common_CommonInfoType
	{
		private readonly MobileSoft.DAL.Common.Dal_Tb_Common_CommonInfoType dal=new MobileSoft.DAL.Common.Dal_Tb_Common_CommonInfoType();
		public Bll_Tb_Common_CommonInfoType()
		{}
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Common.Tb_Common_CommonInfoType model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Common.Tb_Common_CommonInfoType model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			dal.Delete();
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Common.Tb_Common_CommonInfoType GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel();
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Common.Tb_Common_CommonInfoType GetModelByCache()
		{
			//该表无主键信息，请自定义主键/条件字段
			string CacheKey = "Tb_Common_CommonInfoTypeModel-" ;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel();
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Common.Tb_Common_CommonInfoType)objModel;
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
		public List<MobileSoft.Model.Common.Tb_Common_CommonInfoType> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Common.Tb_Common_CommonInfoType> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Common.Tb_Common_CommonInfoType> modelList = new List<MobileSoft.Model.Common.Tb_Common_CommonInfoType>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Common.Tb_Common_CommonInfoType model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Common.Tb_Common_CommonInfoType();
					model.TypeCode=dt.Rows[n]["TypeCode"].ToString();
					model.TypeName=dt.Rows[n]["TypeName"].ToString();
					model.OrganCode=dt.Rows[n]["OrganCode"].ToString();
					model.Type=dt.Rows[n]["Type"].ToString();
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

