using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_HSPR_Customer 的摘要说明。
	/// </summary>
	public class Bll_Tb_HSPR_Customer
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_Customer dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_Customer();
		public Bll_Tb_HSPR_Customer()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long CustID)
		{
			return dal.Exists(CustID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_Customer model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_Customer model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long CustID)
		{
			
			dal.Delete(CustID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_Customer GetModel(long CustID)
		{
			
			return dal.GetModel(CustID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_Customer GetModelByCache(long CustID)
		{
			
			string CacheKey = "Tb_HSPR_CustomerModel-" + CustID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CustID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_Customer)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_Customer> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_Customer> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_Customer> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_Customer>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_Customer model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_Customer();
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					//model.CustTypeID=dt.Rows[n]["CustTypeID"].ToString();
					model.CustName=dt.Rows[n]["CustName"].ToString();
					model.FixedTel=dt.Rows[n]["FixedTel"].ToString();
					model.MobilePhone=dt.Rows[n]["MobilePhone"].ToString();
					model.FaxTel=dt.Rows[n]["FaxTel"].ToString();
					model.EMail=dt.Rows[n]["EMail"].ToString();
					model.Surname=dt.Rows[n]["Surname"].ToString();
					model.Name=dt.Rows[n]["Name"].ToString();
					model.Sex=dt.Rows[n]["Sex"].ToString();
					if(dt.Rows[n]["Birthday"].ToString()!="")
					{
						model.Birthday=DateTime.Parse(dt.Rows[n]["Birthday"].ToString());
					}
					model.Nationality=dt.Rows[n]["Nationality"].ToString();
					model.WorkUnit=dt.Rows[n]["WorkUnit"].ToString();
					model.PaperName=dt.Rows[n]["PaperName"].ToString();
					model.PaperCode=dt.Rows[n]["PaperCode"].ToString();
					model.PassSign=dt.Rows[n]["PassSign"].ToString();
					model.LegalRepr=dt.Rows[n]["LegalRepr"].ToString();
					model.LegalReprTel=dt.Rows[n]["LegalReprTel"].ToString();
					model.Charge=dt.Rows[n]["Charge"].ToString();
					model.ChargeTel=dt.Rows[n]["ChargeTel"].ToString();
					model.Linkman=dt.Rows[n]["Linkman"].ToString();
					model.LinkmanTel=dt.Rows[n]["LinkmanTel"].ToString();
					model.BankName=dt.Rows[n]["BankName"].ToString();
					model.BankIDs=dt.Rows[n]["BankIDs"].ToString();
					model.BankAccount=dt.Rows[n]["BankAccount"].ToString();
					model.BankAgreement=dt.Rows[n]["BankAgreement"].ToString();
					model.InquirePwd=dt.Rows[n]["InquirePwd"].ToString();
					model.InquireAccount=dt.Rows[n]["InquireAccount"].ToString();
					model.Memo=dt.Rows[n]["Memo"].ToString();
					if(dt.Rows[n]["IsUnit"].ToString()!="")
					{
						model.IsUnit=int.Parse(dt.Rows[n]["IsUnit"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					model.Address=dt.Rows[n]["Address"].ToString();
					model.PostCode=dt.Rows[n]["PostCode"].ToString();
					model.Recipient=dt.Rows[n]["Recipient"].ToString();
					model.Hobbies=dt.Rows[n]["Hobbies"].ToString();
					if(dt.Rows[n]["LiveType1"].ToString()!="")
					{
						model.LiveType1=int.Parse(dt.Rows[n]["LiveType1"].ToString());
					}
					if(dt.Rows[n]["LiveType2"].ToString()!="")
					{
						model.LiveType2=int.Parse(dt.Rows[n]["LiveType2"].ToString());
					}
					if(dt.Rows[n]["LiveType3"].ToString()!="")
					{
						model.LiveType3=int.Parse(dt.Rows[n]["LiveType3"].ToString());
					}
					if(dt.Rows[n]["IsDebts"].ToString()!="")
					{
						model.IsDebts=int.Parse(dt.Rows[n]["IsDebts"].ToString());
					}
					if(dt.Rows[n]["IsUsual"].ToString()!="")
					{
						model.IsUsual=int.Parse(dt.Rows[n]["IsUsual"].ToString());
					}
					model.RoomSigns=dt.Rows[n]["RoomSigns"].ToString();
					if(dt.Rows[n]["RoomCount"].ToString()!="")
					{
						model.RoomCount=int.Parse(dt.Rows[n]["RoomCount"].ToString());
					}
					model.Job=dt.Rows[n]["Job"].ToString();
					model.MGradeSign=dt.Rows[n]["MGradeSign"].ToString();
					model.BankCode=dt.Rows[n]["BankCode"].ToString();
					model.BankNo=dt.Rows[n]["BankNo"].ToString();
					if(dt.Rows[n]["CustSynchCode"].ToString()!="")
					{
						model.CustSynchCode=new Guid(dt.Rows[n]["CustSynchCode"].ToString());
					}
					if(dt.Rows[n]["SynchFlag"].ToString()!="")
					{
						model.SynchFlag=int.Parse(dt.Rows[n]["SynchFlag"].ToString());
					}
					if(dt.Rows[n]["SendCardDate"].ToString()!="")
					{
						model.SendCardDate=DateTime.Parse(dt.Rows[n]["SendCardDate"].ToString());
					}
					if(dt.Rows[n]["IsSendCard"].ToString()!="")
					{
						model.IsSendCard=int.Parse(dt.Rows[n]["IsSendCard"].ToString());
					}
					//model.UnCustID=dt.Rows[n]["UnCustID"].ToString();
					if(dt.Rows[n]["IsSelfPay"].ToString()!="")
					{
						model.IsSelfPay=int.Parse(dt.Rows[n]["IsSelfPay"].ToString());
					}
					model.PayEnPass=dt.Rows[n]["PayEnPass"].ToString();
					//model.PersonRole=dt.Rows[n]["PersonRole"].ToString();
					model.OrganizeCode=dt.Rows[n]["OrganizeCode"].ToString();
					if(dt.Rows[n]["JoinDate"].ToString()!="")
					{
						model.JoinDate=DateTime.Parse(dt.Rows[n]["JoinDate"].ToString());
					}
					if(dt.Rows[n]["IsHostel"].ToString()!="")
					{
						model.IsHostel=int.Parse(dt.Rows[n]["IsHostel"].ToString());
					}
					model.BindMobile=dt.Rows[n]["BindMobile"].ToString();
					if(dt.Rows[n]["CustBedLiveState"].ToString()!="")
					{
						model.CustBedLiveState=int.Parse(dt.Rows[n]["CustBedLiveState"].ToString());
					}
					//model.CustBedLiveNum=dt.Rows[n]["CustBedLiveNum"].ToString();
					if(dt.Rows[n]["CustBedLiveDate"].ToString()!="")
					{
						model.CustBedLiveDate=DateTime.Parse(dt.Rows[n]["CustBedLiveDate"].ToString());
					}
					if(dt.Rows[n]["CustBedExitDate"].ToString()!="")
					{
						model.CustBedExitDate=DateTime.Parse(dt.Rows[n]["CustBedExitDate"].ToString());
					}
					if(dt.Rows[n]["RegisterDate"].ToString()!="")
					{
						model.RegisterDate=DateTime.Parse(dt.Rows[n]["RegisterDate"].ToString());
					}
					//model.ArrearsSubID=dt.Rows[n]["ArrearsSubID"].ToString();
					model.BankProvince=dt.Rows[n]["BankProvince"].ToString();
					model.BankCity=dt.Rows[n]["BankCity"].ToString();
					model.BankInfo=dt.Rows[n]["BankInfo"].ToString();
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

