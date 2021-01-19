using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.OAPublicWork;
namespace MobileSoft.BLL.OAPublicWork
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_OAPublicWork_Reimbursement 的摘要说明。
	/// </summary>
	public class Bll_Tb_OAPublicWork_Reimbursement
	{
		private readonly MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_Reimbursement dal=new MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_Reimbursement();
		public Bll_Tb_OAPublicWork_Reimbursement()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int InfoId)
		{
			return dal.Exists(InfoId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_Reimbursement model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_Reimbursement model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int InfoId)
		{
			
			dal.Delete(InfoId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_Reimbursement GetModel(int InfoId)
		{
			
			return dal.GetModel(InfoId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_Reimbursement GetModelByCache(int InfoId)
		{
			
			string CacheKey = "Tb_OAPublicWork_ReimbursementModel-" + InfoId;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(InfoId);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_Reimbursement)objModel;
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
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_Reimbursement> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_Reimbursement> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_Reimbursement> modelList = new List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_Reimbursement>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_Reimbursement model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_Reimbursement();
					if(dt.Rows[n]["InfoId"].ToString()!="")
					{
						model.InfoId=int.Parse(dt.Rows[n]["InfoId"].ToString());
					}
					if(dt.Rows[n]["Tb_WorkFlow_FlowSort_InfoId"].ToString()!="")
					{
						model.Tb_WorkFlow_FlowSort_InfoId=int.Parse(dt.Rows[n]["Tb_WorkFlow_FlowSort_InfoId"].ToString());
					}
					model.UserCode=dt.Rows[n]["UserCode"].ToString();
					model.Title=dt.Rows[n]["Title"].ToString();
					if(dt.Rows[n]["ReimbursementDate"].ToString()!="")
					{
						model.ReimbursementDate=DateTime.Parse(dt.Rows[n]["ReimbursementDate"].ToString());
					}
					model.InfoContent=dt.Rows[n]["InfoContent"].ToString();
					model.DocumentUrl=dt.Rows[n]["DocumentUrl"].ToString();
					model.RealUserCode=dt.Rows[n]["RealUserCode"].ToString();
					if(dt.Rows[n]["RealMoney"].ToString()!="")
					{
						model.RealMoney=decimal.Parse(dt.Rows[n]["RealMoney"].ToString());
					}
					if(dt.Rows[n]["WorkStartDate"].ToString()!="")
					{
						model.WorkStartDate=DateTime.Parse(dt.Rows[n]["WorkStartDate"].ToString());
					}
					model.IsPay=dt.Rows[n]["IsPay"].ToString();
					if(dt.Rows[n]["PrintCount"].ToString()!="")
					{
						model.PrintCount=int.Parse(dt.Rows[n]["PrintCount"].ToString());
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

