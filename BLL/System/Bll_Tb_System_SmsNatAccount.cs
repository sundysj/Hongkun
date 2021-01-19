using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.System;
namespace MobileSoft.BLL.System
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_System_SmsNatAccount 的摘要说明。
	/// </summary>
	public class Bll_Tb_System_SmsNatAccount
	{
		private readonly MobileSoft.DAL.System.Dal_Tb_System_SmsNatAccount dal=new MobileSoft.DAL.System.Dal_Tb_System_SmsNatAccount();
		public Bll_Tb_System_SmsNatAccount()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid CommCode,long CommID)
		{
			return dal.Exists(CommCode,CommID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.System.Tb_System_SmsNatAccount model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_SmsNatAccount model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Guid CommCode,long CommID)
		{
			
			dal.Delete(CommCode,CommID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_SmsNatAccount GetModel(Guid CommCode,long CommID)
		{
			
			return dal.GetModel(CommCode,CommID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.System.Tb_System_SmsNatAccount GetModelByCache(Guid CommCode,long CommID)
		{
			
			string CacheKey = "Tb_System_SmsNatAccountModel-" + CommCode+CommID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CommCode,CommID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.System.Tb_System_SmsNatAccount)objModel;
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
		public List<MobileSoft.Model.System.Tb_System_SmsNatAccount> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.System.Tb_System_SmsNatAccount> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.System.Tb_System_SmsNatAccount> modelList = new List<MobileSoft.Model.System.Tb_System_SmsNatAccount>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.System.Tb_System_SmsNatAccount model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.System.Tb_System_SmsNatAccount();
					if(dt.Rows[n]["CommCode"].ToString()!="")
					{
						model.CommCode=new Guid(dt.Rows[n]["CommCode"].ToString());
					}
					//model.CommID=dt.Rows[n]["CommID"].ToString();
					model.Circle=dt.Rows[n]["Circle"].ToString();
					model.PassWord=dt.Rows[n]["PassWord"].ToString();
					if(dt.Rows[n]["Balance"].ToString()!="")
					{
						model.Balance=int.Parse(dt.Rows[n]["Balance"].ToString());
					}
					if(dt.Rows[n]["WayType"].ToString()!="")
					{
						model.WayType=int.Parse(dt.Rows[n]["WayType"].ToString());
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

