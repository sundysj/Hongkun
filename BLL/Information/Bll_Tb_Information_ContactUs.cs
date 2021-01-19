using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Information;
namespace MobileSoft.BLL.Information
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Information_ContactUs 的摘要说明。
	/// </summary>
	public class Bll_Tb_Information_ContactUs
	{
		private readonly MobileSoft.DAL.Information.Dal_Tb_Information_ContactUs dal=new MobileSoft.DAL.Information.Dal_Tb_Information_ContactUs();
		public Bll_Tb_Information_ContactUs()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(MobileSoft.Model.Information.Tb_Information_ContactUs model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Information.Tb_Information_ContactUs model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ID)
		{
			
			dal.Delete(ID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Information.Tb_Information_ContactUs GetModel(long ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Information.Tb_Information_ContactUs GetModelByCache(long ID)
		{
			
			string CacheKey = "Tb_Information_ContactUsModel-" + ID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Information.Tb_Information_ContactUs)objModel;
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
		public List<MobileSoft.Model.Information.Tb_Information_ContactUs> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Information.Tb_Information_ContactUs> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Information.Tb_Information_ContactUs> modelList = new List<MobileSoft.Model.Information.Tb_Information_ContactUs>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Information.Tb_Information_ContactUs model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Information.Tb_Information_ContactUs();
					//model.ID=dt.Rows[n]["ID"].ToString();
					//model.BussId=dt.Rows[n]["BussId"].ToString();
					model.BussName=dt.Rows[n]["BussName"].ToString();
					model.Address=dt.Rows[n]["Address"].ToString();
					model.Postal=dt.Rows[n]["Postal"].ToString();
					model.LinkMan=dt.Rows[n]["LinkMan"].ToString();
					model.Tel=dt.Rows[n]["Tel"].ToString();
					model.Phone=dt.Rows[n]["Phone"].ToString();
					model.Email=dt.Rows[n]["Email"].ToString();
					model.QQ=dt.Rows[n]["QQ"].ToString();
					model.Wechat=dt.Rows[n]["Wechat"].ToString();
					model.URL=dt.Rows[n]["URL"].ToString();
					model.Map=dt.Rows[n]["Map"].ToString();
					model.ContactUsContent=dt.Rows[n]["ContactUsContent"].ToString();
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

