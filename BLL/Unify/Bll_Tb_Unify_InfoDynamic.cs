using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Unify;
namespace MobileSoft.BLL.Unify
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Unify_InfoDynamic 的摘要说明。
	/// </summary>
	public class Bll_Tb_Unify_InfoDynamic
	{
        private readonly MobileSoft.DAL.Unify.Dal_Tb_Unify_InfoDynamic dal = new MobileSoft.DAL.Unify.Dal_Tb_Unify_InfoDynamic();
		public Bll_Tb_Unify_InfoDynamic()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long InfoID)
		{
			return dal.Exists(InfoID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(MobileSoft.Model.Unify.Tb_Unify_InfoDynamic model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Unify.Tb_Unify_InfoDynamic model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long InfoID)
		{
			
			dal.Delete(InfoID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Unify.Tb_Unify_InfoDynamic GetModel(long InfoID)
		{
			
			return dal.GetModel(InfoID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
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
		/// 获得数据列表
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
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Unify.Tb_Unify_InfoDynamic> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
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

