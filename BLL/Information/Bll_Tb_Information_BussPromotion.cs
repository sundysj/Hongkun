using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Information;
namespace MobileSoft.BLL.Information
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Information_BussPromotion 的摘要说明。
	/// </summary>
	public class Bll_Tb_Information_BussPromotion
	{
		private readonly MobileSoft.DAL.Information.Dal_Tb_Information_BussPromotion dal=new MobileSoft.DAL.Information.Dal_Tb_Information_BussPromotion();
		public Bll_Tb_Information_BussPromotion()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ProID)
		{
			return dal.Exists(ProID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(MobileSoft.Model.Information.Tb_Information_BussPromotion model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Information.Tb_Information_BussPromotion model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ProID)
		{
			
			dal.Delete(ProID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Information.Tb_Information_BussPromotion GetModel(long ProID)
		{
			
			return dal.GetModel(ProID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Information.Tb_Information_BussPromotion GetModelByCache(long ProID)
		{
			
			string CacheKey = "Tb_Information_BussPromotionModel-" + ProID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ProID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Information.Tb_Information_BussPromotion)objModel;
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
		public List<MobileSoft.Model.Information.Tb_Information_BussPromotion> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Information.Tb_Information_BussPromotion> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Information.Tb_Information_BussPromotion> modelList = new List<MobileSoft.Model.Information.Tb_Information_BussPromotion>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Information.Tb_Information_BussPromotion model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Information.Tb_Information_BussPromotion();
					//model.ProID=dt.Rows[n]["ProID"].ToString();
					//model.BussId=dt.Rows[n]["BussId"].ToString();
					model.Project=dt.Rows[n]["Project"].ToString();
					model.Publisher=dt.Rows[n]["Publisher"].ToString();
					if(dt.Rows[n]["PublishDate"].ToString()!="")
					{
						model.PublishDate=DateTime.Parse(dt.Rows[n]["PublishDate"].ToString());
					}
					model.Reason=dt.Rows[n]["Reason"].ToString();
					model.ProImage=dt.Rows[n]["ProImage"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					//model.NumID=dt.Rows[n]["NumID"].ToString();
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

