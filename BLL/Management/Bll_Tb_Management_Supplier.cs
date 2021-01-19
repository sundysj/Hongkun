using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Management;
namespace MobileSoft.BLL.Management
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Management_Supplier 的摘要说明。
	/// </summary>
	public class Bll_Tb_Management_Supplier
	{
		private readonly MobileSoft.DAL.Management.Dal_Tb_Management_Supplier dal=new MobileSoft.DAL.Management.Dal_Tb_Management_Supplier();
		public Bll_Tb_Management_Supplier()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string SupplierCode)
		{
			return dal.Exists(SupplierCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Management.Tb_Management_Supplier model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Management.Tb_Management_Supplier model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string SupplierCode)
		{
			
			dal.Delete(SupplierCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Management.Tb_Management_Supplier GetModel(string SupplierCode)
		{
			
			return dal.GetModel(SupplierCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Management.Tb_Management_Supplier GetModelByCache(string SupplierCode)
		{
			
			string CacheKey = "Tb_Management_SupplierModel-" + SupplierCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(SupplierCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Management.Tb_Management_Supplier)objModel;
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
		public List<MobileSoft.Model.Management.Tb_Management_Supplier> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Management.Tb_Management_Supplier> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Management.Tb_Management_Supplier> modelList = new List<MobileSoft.Model.Management.Tb_Management_Supplier>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Management.Tb_Management_Supplier model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Management.Tb_Management_Supplier();
					model.SupplierCode=dt.Rows[n]["SupplierCode"].ToString();
					model.OrganCode=dt.Rows[n]["OrganCode"].ToString();
					model.CommID=dt.Rows[n]["CommID"].ToString();
					model.RefDate=dt.Rows[n]["RefDate"].ToString();
					model.BussType=dt.Rows[n]["BussType"].ToString();
					model.BussName=dt.Rows[n]["BussName"].ToString();
					model.LegalPerson=dt.Rows[n]["LegalPerson"].ToString();
					model.LegalPaperCode=dt.Rows[n]["LegalPaperCode"].ToString();
					model.LicenseNum=dt.Rows[n]["LicenseNum"].ToString();
					model.OrganizationCode=dt.Rows[n]["OrganizationCode"].ToString();
					model.RegNumber=dt.Rows[n]["RegNumber"].ToString();
					model.Province=dt.Rows[n]["Province"].ToString();
					model.City=dt.Rows[n]["City"].ToString();
					model.Street=dt.Rows[n]["Street"].ToString();
					model.Addr=dt.Rows[n]["Addr"].ToString();
					model.ZipCode=dt.Rows[n]["ZipCode"].ToString();
					model.ContactPerson=dt.Rows[n]["ContactPerson"].ToString();
					model.ContactPhone=dt.Rows[n]["ContactPhone"].ToString();
					model.MainBusiness=dt.Rows[n]["MainBusiness"].ToString();
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

