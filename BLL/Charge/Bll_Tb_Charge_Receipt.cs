using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Charge;
namespace MobileSoft.BLL.Charge
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Charge_Receipt 的摘要说明。
	/// </summary>
	public class Bll_Tb_Charge_Receipt
	{
		private readonly MobileSoft.DAL.Charge.Dal_Tb_Charge_Receipt dal=new MobileSoft.DAL.Charge.Dal_Tb_Charge_Receipt();
		public Bll_Tb_Charge_Receipt()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ReceiptCode)
		{
			return dal.Exists(ReceiptCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Charge.Tb_Charge_Receipt model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Charge.Tb_Charge_Receipt model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string ReceiptCode)
		{
			
			dal.Delete(ReceiptCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Charge.Tb_Charge_Receipt GetModel(string ReceiptCode)
		{
			
			return dal.GetModel(ReceiptCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Charge.Tb_Charge_Receipt GetModelByCache(string ReceiptCode)
		{
			
			string CacheKey = "Tb_Charge_ReceiptModel-" + ReceiptCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ReceiptCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Charge.Tb_Charge_Receipt)objModel;
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
		public List<MobileSoft.Model.Charge.Tb_Charge_Receipt> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Charge.Tb_Charge_Receipt> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Charge.Tb_Charge_Receipt> modelList = new List<MobileSoft.Model.Charge.Tb_Charge_Receipt>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Charge.Tb_Charge_Receipt model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Charge.Tb_Charge_Receipt();
					model.ReceiptCode=dt.Rows[n]["ReceiptCode"].ToString();
					model.BussId=long.Parse(dt.Rows[n]["BussId"].ToString());
					model.OrderId=dt.Rows[n]["OrderId"].ToString();
					model.ReceiptSign=dt.Rows[n]["ReceiptSign"].ToString();
					if(dt.Rows[n]["Receivables"].ToString()!="")
					{
						model.Receivables=decimal.Parse(dt.Rows[n]["Receivables"].ToString());
					}
					model.Method=dt.Rows[n]["Method"].ToString();
					model.CardNum=dt.Rows[n]["CardNum"].ToString();
					model.MemberCardCode=dt.Rows[n]["MemberCardCode"].ToString();
					model.CustCode=dt.Rows[n]["CustCode"].ToString();
					if(dt.Rows[n]["Balance"].ToString()!="")
					{
						model.Balance=decimal.Parse(dt.Rows[n]["Balance"].ToString());
					}
					if(dt.Rows[n]["Discount"].ToString()!="")
					{
						model.Discount=decimal.Parse(dt.Rows[n]["Discount"].ToString());
					}
					if(dt.Rows[n]["Amount"].ToString()!="")
					{
						model.Amount=decimal.Parse(dt.Rows[n]["Amount"].ToString());
					}
					model.ReceiptMemo=dt.Rows[n]["ReceiptMemo"].ToString();
					model.ReceiptType=dt.Rows[n]["ReceiptType"].ToString();
					if(dt.Rows[n]["ReceiptDate"].ToString()!="")
					{
						model.ReceiptDate=DateTime.Parse(dt.Rows[n]["ReceiptDate"].ToString());
					}
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

