using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_HSPR_FeesReceipts 的摘要说明。
	/// </summary>
	public class Bll_Tb_HSPR_FeesReceipts
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_FeesReceipts dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_FeesReceipts();
		public Bll_Tb_HSPR_FeesReceipts()
		{}

		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ReceID)
		{
			return dal.Exists(ReceID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_FeesReceipts model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_FeesReceipts model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ReceID)
		{
			
			dal.Delete(ReceID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_FeesReceipts GetModel(long ReceID)
		{
			
			return dal.GetModel(ReceID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_FeesReceipts GetModelByCache(long ReceID)
		{
			
			string CacheKey = "Tb_HSPR_FeesReceiptsModel-" + ReceID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ReceID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_FeesReceipts)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_FeesReceipts> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_FeesReceipts> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_FeesReceipts> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_FeesReceipts>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_FeesReceipts model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_FeesReceipts();
					//model.ReceID=dt.Rows[n]["ReceID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					model.BillsSign=dt.Rows[n]["BillsSign"].ToString();
					if(dt.Rows[n]["PrintTimes"].ToString()!="")
					{
						model.PrintTimes=int.Parse(dt.Rows[n]["PrintTimes"].ToString());
					}
					if(dt.Rows[n]["BillsDate"].ToString()!="")
					{
						model.BillsDate=DateTime.Parse(dt.Rows[n]["BillsDate"].ToString());
					}
					model.UserCode=dt.Rows[n]["UserCode"].ToString();
					model.ChargeMode=dt.Rows[n]["ChargeMode"].ToString();
					if(dt.Rows[n]["AccountWay"].ToString()!="")
					{
						model.AccountWay=int.Parse(dt.Rows[n]["AccountWay"].ToString());
					}
					model.ReceMemo=dt.Rows[n]["ReceMemo"].ToString();
					if(dt.Rows[n]["PerSurplus"].ToString()!="")
					{
						model.PerSurplus=decimal.Parse(dt.Rows[n]["PerSurplus"].ToString());
					}
					if(dt.Rows[n]["SurplusAmount"].ToString()!="")
					{
						model.SurplusAmount=decimal.Parse(dt.Rows[n]["SurplusAmount"].ToString());
					}
					if(dt.Rows[n]["PrecAmount"].ToString()!="")
					{
						model.PrecAmount=decimal.Parse(dt.Rows[n]["PrecAmount"].ToString());
					}
					if(dt.Rows[n]["PaidAmount"].ToString()!="")
					{
						model.PaidAmount=decimal.Parse(dt.Rows[n]["PaidAmount"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					//model.UseRepID=dt.Rows[n]["UseRepID"].ToString();
					model.InvoiceBill=dt.Rows[n]["InvoiceBill"].ToString();
					model.InvoiceUnit=dt.Rows[n]["InvoiceUnit"].ToString();
					model.RemitterUnit=dt.Rows[n]["RemitterUnit"].ToString();
					model.BankName=dt.Rows[n]["BankName"].ToString();
					model.BankAccount=dt.Rows[n]["BankAccount"].ToString();
					model.ChequeBill=dt.Rows[n]["ChequeBill"].ToString();
					if(dt.Rows[n]["RenderType"].ToString()!="")
					{
						model.RenderType=int.Parse(dt.Rows[n]["RenderType"].ToString());
					}
					//model.RenderCustID=dt.Rows[n]["RenderCustID"].ToString();
					model.RenderCustName=dt.Rows[n]["RenderCustName"].ToString();
					if(dt.Rows[n]["IsRefer"].ToString()!="")
					{
						model.IsRefer=int.Parse(dt.Rows[n]["IsRefer"].ToString());
					}
					model.ReferReason=dt.Rows[n]["ReferReason"].ToString();
					model.ReferUserCode=dt.Rows[n]["ReferUserCode"].ToString();
					if(dt.Rows[n]["ReferDate"].ToString()!="")
					{
						model.ReferDate=DateTime.Parse(dt.Rows[n]["ReferDate"].ToString());
					}
					model.AuditUserCode=dt.Rows[n]["AuditUserCode"].ToString();
					if(dt.Rows[n]["AuditDate"].ToString()!="")
					{
						model.AuditDate=DateTime.Parse(dt.Rows[n]["AuditDate"].ToString());
					}
					if(dt.Rows[n]["IsAudit"].ToString()!="")
					{
						model.IsAudit=int.Parse(dt.Rows[n]["IsAudit"].ToString());
					}
					//model.BillTypeID=dt.Rows[n]["BillTypeID"].ToString();
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

            public bool FeesReceiptsIsCheck(long ReceID)
            {
                  return dal.FeesReceiptsIsCheck(ReceID);
            }

            public string FeesReceiptsCheckRepeal(long ReceID)
            {
                  return dal.FeesReceiptsCheckRepeal(ReceID);
            }

            public void FeesReceiptDelete(long ReceID)
            {
                  dal.FeesReceiptDelete(ReceID);
            }

            public void FeesReceiptsUpdateAudit(long ReceID, int IsAudit, string AuditUserCode, string AuditDate)
            {
                  dal.FeesReceiptsUpdateAudit(ReceID, IsAudit, AuditUserCode, AuditDate);
            }
	}
}

