using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Management;
namespace MobileSoft.BLL.Management
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Management_Order 的摘要说明。
	/// </summary>
	public class Bll_Tb_Management_Order
	{
		private readonly MobileSoft.DAL.Management.Dal_Tb_Management_Order dal=new MobileSoft.DAL.Management.Dal_Tb_Management_Order();
		public Bll_Tb_Management_Order()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string OrderCode)
		{
			return dal.Exists(OrderCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Management.Tb_Management_Order model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Management.Tb_Management_Order model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string OrderCode)
		{
			
			dal.Delete(OrderCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Management.Tb_Management_Order GetModel(string OrderCode)
		{
			
			return dal.GetModel(OrderCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Management.Tb_Management_Order GetModelByCache(string OrderCode)
		{
			
			string CacheKey = "Tb_Management_OrderModel-" + OrderCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(OrderCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Management.Tb_Management_Order)objModel;
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
		public List<MobileSoft.Model.Management.Tb_Management_Order> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Management.Tb_Management_Order> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Management.Tb_Management_Order> modelList = new List<MobileSoft.Model.Management.Tb_Management_Order>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Management.Tb_Management_Order model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Management.Tb_Management_Order();
					model.OrderCode=dt.Rows[n]["OrderCode"].ToString();
					model.CommID=dt.Rows[n]["CommID"].ToString();
					model.ServiceCode=dt.Rows[n]["ServiceCode"].ToString();
					model.OrderNum=dt.Rows[n]["OrderNum"].ToString();
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					if(dt.Rows[n]["OrderDate"].ToString()!="")
					{
						model.OrderDate=DateTime.Parse(dt.Rows[n]["OrderDate"].ToString());
					}
					model.OrderMethod=dt.Rows[n]["OrderMethod"].ToString();
					model.Contact=dt.Rows[n]["Contact"].ToString();
					model.ContactTel=dt.Rows[n]["ContactTel"].ToString();
					if(dt.Rows[n]["StartDate"].ToString()!="")
					{
						model.StartDate=DateTime.Parse(dt.Rows[n]["StartDate"].ToString());
					}
					if(dt.Rows[n]["EndDate"].ToString()!="")
					{
						model.EndDate=DateTime.Parse(dt.Rows[n]["EndDate"].ToString());
					}
					if(dt.Rows[n]["OrderCount"].ToString()!="")
					{
						model.OrderCount=int.Parse(dt.Rows[n]["OrderCount"].ToString());
					}
					if(dt.Rows[n]["OrderAmount"].ToString()!="")
					{
						model.OrderAmount=decimal.Parse(dt.Rows[n]["OrderAmount"].ToString());
					}
					if(dt.Rows[n]["TotalAmount"].ToString()!="")
					{
						model.TotalAmount=decimal.Parse(dt.Rows[n]["TotalAmount"].ToString());
					}
					model.IsPay=dt.Rows[n]["IsPay"].ToString();
					model.Memo=dt.Rows[n]["Memo"].ToString();
					model.IsAssign=dt.Rows[n]["IsAssign"].ToString();
					model.AssignName=dt.Rows[n]["AssignName"].ToString();
					if(dt.Rows[n]["AssignDate"].ToString()!="")
					{
						model.AssignDate=DateTime.Parse(dt.Rows[n]["AssignDate"].ToString());
					}
					if(dt.Rows[n]["IsDeal"].ToString()!="")
					{
						model.IsDeal=dt.Rows[n]["IsDeal"].ToString();
					}
					model.DealName=dt.Rows[n]["DealName"].ToString();
					if(dt.Rows[n]["DealDate"].ToString()!="")
					{
						model.DealDate=DateTime.Parse(dt.Rows[n]["DealDate"].ToString());
					}
					if(dt.Rows[n]["DeliveryCount"].ToString()!="")
					{
						model.DeliveryCount=int.Parse(dt.Rows[n]["DeliveryCount"].ToString());
					}
					model.IsVisit=dt.Rows[n]["IsVisit"].ToString();
					model.VisitName=dt.Rows[n]["VisitName"].ToString();
					if(dt.Rows[n]["VisitDate"].ToString()!="")
					{
						model.VisitDate=DateTime.Parse(dt.Rows[n]["VisitDate"].ToString());
					}
					model.Evaluation=dt.Rows[n]["Evaluation"].ToString();
					model.CustOpinion=dt.Rows[n]["CustOpinion"].ToString();
					model.IsExit=dt.Rows[n]["IsExit"].ToString();
					model.ExitName=dt.Rows[n]["ExitName"].ToString();
					if(dt.Rows[n]["ExitDate"].ToString()!="")
					{
						model.ExitDate=DateTime.Parse(dt.Rows[n]["ExitDate"].ToString());
					}
					model.ExitReason=dt.Rows[n]["ExitReason"].ToString();
					model.DealMemo=dt.Rows[n]["DealMemo"].ToString();
					model.StanName=dt.Rows[n]["StanName"].ToString();
					model.StanFormulaName=dt.Rows[n]["StanFormulaName"].ToString();
					if(dt.Rows[n]["StanAmount"].ToString()!="")
					{
						model.StanAmount=decimal.Parse(dt.Rows[n]["StanAmount"].ToString());
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

