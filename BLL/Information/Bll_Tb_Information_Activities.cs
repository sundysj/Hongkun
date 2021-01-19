using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Information;
namespace MobileSoft.BLL.Information
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Information_Activities 的摘要说明。
	/// </summary>
	public class Bll_Tb_Information_Activities
	{
		private readonly MobileSoft.DAL.Information.Dal_Tb_Information_Activities dal=new MobileSoft.DAL.Information.Dal_Tb_Information_Activities();
		public Bll_Tb_Information_Activities()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ActId)
		{
			return dal.Exists(ActId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(MobileSoft.Model.Information.Tb_Information_Activities model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Information.Tb_Information_Activities model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ActId)
		{
			
			dal.Delete(ActId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Information.Tb_Information_Activities GetModel(long ActId)
		{
			
			return dal.GetModel(ActId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Information.Tb_Information_Activities GetModelByCache(long ActId)
		{
			
			string CacheKey = "Tb_Information_ActivitiesModel-" + ActId;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ActId);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Information.Tb_Information_Activities)objModel;
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
		public List<MobileSoft.Model.Information.Tb_Information_Activities> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Information.Tb_Information_Activities> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Information.Tb_Information_Activities> modelList = new List<MobileSoft.Model.Information.Tb_Information_Activities>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Information.Tb_Information_Activities model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Information.Tb_Information_Activities();
					//model.ActId=dt.Rows[n]["ActId"].ToString();
					//model.BussId=dt.Rows[n]["BussId"].ToString();
					model.Title=dt.Rows[n]["Title"].ToString();
					model.ActPublisher=dt.Rows[n]["ActPublisher"].ToString();
					if(dt.Rows[n]["PublishDate"].ToString()!="")
					{
						model.PublishDate=DateTime.Parse(dt.Rows[n]["PublishDate"].ToString());
					}
					model.ActContent=dt.Rows[n]["ActContent"].ToString();
					model.ActImage=dt.Rows[n]["ActImage"].ToString();
					//model.NumID=dt.Rows[n]["NumID"].ToString();
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

