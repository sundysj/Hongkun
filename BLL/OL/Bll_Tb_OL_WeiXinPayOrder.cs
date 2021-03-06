using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.OL;
namespace MobileSoft.BLL.OL
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_OL_WeiXinPayOrder 的摘要说明。
	/// </summary>
	public class Bll_Tb_OL_WeiXinPayOrder
	{
		private readonly MobileSoft.DAL.OL.Dal_Tb_OL_WeiXinPayOrder dal=new MobileSoft.DAL.OL.Dal_Tb_OL_WeiXinPayOrder();
		public Bll_Tb_OL_WeiXinPayOrder()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.OL.Tb_OL_WeiXinPayOrder model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OL.Tb_OL_WeiXinPayOrder model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string Id)
		{
			
			dal.Delete(Id);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OL.Tb_OL_WeiXinPayOrder GetModel(string Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.OL.Tb_OL_WeiXinPayOrder GetModelByCache(string Id)
		{
			
			string CacheKey = "Tb_OL_WeiXinPayOrderModel-" + Id;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Id);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.OL.Tb_OL_WeiXinPayOrder)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.OL.Tb_OL_WeiXinPayOrder> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.OL.Tb_OL_WeiXinPayOrder> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.OL.Tb_OL_WeiXinPayOrder> modelList = new List<MobileSoft.Model.OL.Tb_OL_WeiXinPayOrder>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.OL.Tb_OL_WeiXinPayOrder model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.OL.Tb_OL_WeiXinPayOrder();
					model.Id=dt.Rows[n]["Id"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.CommunityId=dt.Rows[n]["CommunityId"].ToString();
					//model.CustId=dt.Rows[n]["CustId"].ToString();
					model.mch_id=dt.Rows[n]["mch_id"].ToString();
					model.out_trade_no=dt.Rows[n]["out_trade_no"].ToString();
					model.prepay_id=dt.Rows[n]["prepay_id"].ToString();
					model.time_start=dt.Rows[n]["time_start"].ToString();
					model.return_code=dt.Rows[n]["return_code"].ToString();
					model.return_msg=dt.Rows[n]["return_msg"].ToString();
					if(dt.Rows[n]["IsSucc"].ToString()!="")
					{
						model.IsSucc=int.Parse(dt.Rows[n]["IsSucc"].ToString());
					}
					model.Memo=dt.Rows[n]["Memo"].ToString();
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

