using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Sys;
namespace MobileSoft.BLL.Sys
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Sys_TakePicContractFeesDetail 的摘要说明。
	/// </summary>
	public class Bll_Tb_Sys_TakePicContractFeesDetail
	{
		private readonly MobileSoft.DAL.Sys.Dal_Tb_Sys_TakePicContractFeesDetail dal=new MobileSoft.DAL.Sys.Dal_Tb_Sys_TakePicContractFeesDetail();
		public Bll_Tb_Sys_TakePicContractFeesDetail()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long DetailID)
		{
			return dal.Exists(DetailID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MobileSoft.Model.Sys.Tb_Sys_TakePicContractFeesDetail model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_TakePicContractFeesDetail model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long DetailID)
		{
			
			dal.Delete(DetailID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePicContractFeesDetail GetModel(long DetailID)
		{
			
			return dal.GetModel(DetailID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePicContractFeesDetail GetModelByCache(long DetailID)
		{
			
			string CacheKey = "Tb_Sys_TakePicContractFeesDetailModel-" + DetailID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(DetailID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Sys.Tb_Sys_TakePicContractFeesDetail)objModel;
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
		public List<MobileSoft.Model.Sys.Tb_Sys_TakePicContractFeesDetail> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Sys.Tb_Sys_TakePicContractFeesDetail> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Sys.Tb_Sys_TakePicContractFeesDetail> modelList = new List<MobileSoft.Model.Sys.Tb_Sys_TakePicContractFeesDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Sys.Tb_Sys_TakePicContractFeesDetail model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Sys.Tb_Sys_TakePicContractFeesDetail();
					//model.DetailID=dt.Rows[n]["DetailID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.OrganCode=dt.Rows[n]["OrganCode"].ToString();
					model.ContTypeName=dt.Rows[n]["ContTypeName"].ToString();
					model.ContSign=dt.Rows[n]["ContSign"].ToString();
					model.ContName=dt.Rows[n]["ContName"].ToString();
					model.CustName=dt.Rows[n]["CustName"].ToString();
					model.CostName=dt.Rows[n]["CostName"].ToString();
					if(dt.Rows[n]["FeesDueDate"].ToString()!="")
					{
						model.FeesDueDate=DateTime.Parse(dt.Rows[n]["FeesDueDate"].ToString());
					}
					if(dt.Rows[n]["DebtsAmount"].ToString()!="")
					{
						model.DebtsAmount=decimal.Parse(dt.Rows[n]["DebtsAmount"].ToString());
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

