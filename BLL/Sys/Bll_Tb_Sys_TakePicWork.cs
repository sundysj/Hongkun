using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Sys;
namespace MobileSoft.BLL.Sys
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Sys_TakePicWork 的摘要说明。
	/// </summary>
	public class Bll_Tb_Sys_TakePicWork
	{
		private readonly MobileSoft.DAL.Sys.Dal_Tb_Sys_TakePicWork dal=new MobileSoft.DAL.Sys.Dal_Tb_Sys_TakePicWork();
		public Bll_Tb_Sys_TakePicWork()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long StatID)
		{
			return dal.Exists(StatID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MobileSoft.Model.Sys.Tb_Sys_TakePicWork model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_TakePicWork model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long StatID)
		{
			
			dal.Delete(StatID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePicWork GetModel(long StatID)
		{
			
			return dal.GetModel(StatID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePicWork GetModelByCache(long StatID)
		{
			
			string CacheKey = "Tb_Sys_TakePicWorkModel-" + StatID;
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
			return (MobileSoft.Model.Sys.Tb_Sys_TakePicWork)objModel;
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
		public List<MobileSoft.Model.Sys.Tb_Sys_TakePicWork> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Sys.Tb_Sys_TakePicWork> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Sys.Tb_Sys_TakePicWork> modelList = new List<MobileSoft.Model.Sys.Tb_Sys_TakePicWork>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Sys.Tb_Sys_TakePicWork model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Sys.Tb_Sys_TakePicWork();
					//model.StatID=dt.Rows[n]["StatID"].ToString();
					model.UserCode=dt.Rows[n]["UserCode"].ToString();
					if(dt.Rows[n]["InsBatchFeesCount"].ToString()!="")
					{
						model.InsBatchFeesCount=int.Parse(dt.Rows[n]["InsBatchFeesCount"].ToString());
					}
					if(dt.Rows[n]["InsOneFeesCount"].ToString()!="")
					{
						model.InsOneFeesCount=int.Parse(dt.Rows[n]["InsOneFeesCount"].ToString());
					}
					if(dt.Rows[n]["OffsetPrecCount"].ToString()!="")
					{
						model.OffsetPrecCount=int.Parse(dt.Rows[n]["OffsetPrecCount"].ToString());
					}
					if(dt.Rows[n]["WaivCount"].ToString()!="")
					{
						model.WaivCount=int.Parse(dt.Rows[n]["WaivCount"].ToString());
					}
					if(dt.Rows[n]["ReceCount"].ToString()!="")
					{
						model.ReceCount=int.Parse(dt.Rows[n]["ReceCount"].ToString());
					}
					if(dt.Rows[n]["RefundCount"].ToString()!="")
					{
						model.RefundCount=int.Parse(dt.Rows[n]["RefundCount"].ToString());
					}
					if(dt.Rows[n]["PrecRefundCount"].ToString()!="")
					{
						model.PrecRefundCount=int.Parse(dt.Rows[n]["PrecRefundCount"].ToString());
					}
					if(dt.Rows[n]["LeaseContCount"].ToString()!="")
					{
						model.LeaseContCount=int.Parse(dt.Rows[n]["LeaseContCount"].ToString());
					}
					if(dt.Rows[n]["ContCount"].ToString()!="")
					{
						model.ContCount=int.Parse(dt.Rows[n]["ContCount"].ToString());
					}
					if(dt.Rows[n]["RoomStateCount"].ToString()!="")
					{
						model.RoomStateCount=int.Parse(dt.Rows[n]["RoomStateCount"].ToString());
					}
					if(dt.Rows[n]["AskForCount"].ToString()!="")
					{
						model.AskForCount=int.Parse(dt.Rows[n]["AskForCount"].ToString());
					}
					if(dt.Rows[n]["PurchaseCount"].ToString()!="")
					{
						model.PurchaseCount=int.Parse(dt.Rows[n]["PurchaseCount"].ToString());
					}
					if(dt.Rows[n]["InCount"].ToString()!="")
					{
						model.InCount=int.Parse(dt.Rows[n]["InCount"].ToString());
					}
					if(dt.Rows[n]["OutCount"].ToString()!="")
					{
						model.OutCount=int.Parse(dt.Rows[n]["OutCount"].ToString());
					}
					if(dt.Rows[n]["InventCount"].ToString()!="")
					{
						model.InventCount=int.Parse(dt.Rows[n]["InventCount"].ToString());
					}
					if(dt.Rows[n]["MaterialTracCount"].ToString()!="")
					{
						model.MaterialTracCount=int.Parse(dt.Rows[n]["MaterialTracCount"].ToString());
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

