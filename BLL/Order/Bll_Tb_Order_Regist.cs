using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Order;
namespace MobileSoft.BLL.Order
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Order_Regist 的摘要说明。
	/// </summary>
	public class Bll_Tb_Order_Regist
	{
        private readonly MobileSoft.DAL.Order.Dal_Tb_Order_Regist dal = new MobileSoft.DAL.Order.Dal_Tb_Order_Regist();
		public Bll_Tb_Order_Regist()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string RegistID)
		{
			return dal.Exists(RegistID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Order.Tb_Order_Regist model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Order.Tb_Order_Regist model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string RegistID)
		{
			
			dal.Delete(RegistID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Order.Tb_Order_Regist GetModel(string RegistID)
		{
			
			return dal.GetModel(RegistID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Order.Tb_Order_Regist GetModelByCache(string RegistID)
		{
			
			string CacheKey = "Tb_Order_RegistModel-" + RegistID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(RegistID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Order.Tb_Order_Regist)objModel;
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
		public List<MobileSoft.Model.Order.Tb_Order_Regist> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Order.Tb_Order_Regist> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Order.Tb_Order_Regist> modelList = new List<MobileSoft.Model.Order.Tb_Order_Regist>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Order.Tb_Order_Regist model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Order.Tb_Order_Regist();
					model.RegistID=dt.Rows[n]["RegistID"].ToString();
					model.RegistNumber=dt.Rows[n]["RegistNumber"].ToString();
					model.CustomerType=dt.Rows[n]["CustomerType"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.CustName=dt.Rows[n]["CustName"].ToString();
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					model.MobilePhone=dt.Rows[n]["MobilePhone"].ToString();
					model.Address=dt.Rows[n]["Address"].ToString();
					if(dt.Rows[n]["DocumentDate"].ToString()!="")
					{
						model.DocumentDate=DateTime.Parse(dt.Rows[n]["DocumentDate"].ToString());
					}
					model.RegistMessage=dt.Rows[n]["RegistMessage"].ToString();
					model.ProcessingStatus=dt.Rows[n]["ProcessingStatus"].ToString();
					model.ProcessingExplain=dt.Rows[n]["ProcessingExplain"].ToString();
					//model.ReleaseID=dt.Rows[n]["ReleaseID"].ToString();
					model.ScheduleType=dt.Rows[n]["ScheduleType"].ToString();
					if(dt.Rows[n]["ReleaseServiceComDate"].ToString()!="")
					{
						model.ReleaseServiceComDate=DateTime.Parse(dt.Rows[n]["ReleaseServiceComDate"].ToString());
					}
					model.ReleaseServiceComtime=dt.Rows[n]["ReleaseServiceComtime"].ToString();
					if(dt.Rows[n]["ReleaseStartTime"].ToString()!="")
					{
						model.ReleaseStartTime=DateTime.Parse(dt.Rows[n]["ReleaseStartTime"].ToString());
					}
					if(dt.Rows[n]["ReleaseEndTime"].ToString()!="")
					{
						model.ReleaseEndTime=DateTime.Parse(dt.Rows[n]["ReleaseEndTime"].ToString());
					}
					model.ReleasePeriodOfStarttime=dt.Rows[n]["ReleasePeriodOfStarttime"].ToString();
					model.ReleasePeriodOfEndtime=dt.Rows[n]["ReleasePeriodOfEndtime"].ToString();
					model.UserCode=dt.Rows[n]["UserCode"].ToString();
					if(dt.Rows[n]["IssueDate"].ToString()!="")
					{
						model.IssueDate=DateTime.Parse(dt.Rows[n]["IssueDate"].ToString());
					}
					//model.BussId=dt.Rows[n]["BussId"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["SumCount"].ToString()!="")
					{
						model.SumCount=decimal.Parse(dt.Rows[n]["SumCount"].ToString());
					}
					if(dt.Rows[n]["SumPrice"].ToString()!="")
					{
						model.SumPrice=decimal.Parse(dt.Rows[n]["SumPrice"].ToString());
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

