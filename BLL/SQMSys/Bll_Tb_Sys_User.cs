using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.SQMSys;
namespace MobileSoft.BLL.SQMSys
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Sys_User 的摘要说明。
	/// </summary>
	public class Bll_Tb_Sys_User
	{
		private readonly MobileSoft.DAL.SQMSys.Dal_Tb_Sys_User dal=new MobileSoft.DAL.SQMSys.Dal_Tb_Sys_User();
		public Bll_Tb_Sys_User()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string UserCode)
		{
			return dal.Exists(UserCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_User model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_User model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string UserCode)
		{
			
			dal.Delete(UserCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_User GetModel(string UserCode)
		{
			
			return dal.GetModel(UserCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_User GetModelByCache(string UserCode)
		{
			
			string CacheKey = "Tb_Sys_UserModel-" + UserCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(UserCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.SQMSys.Tb_Sys_User)objModel;
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
		public List<MobileSoft.Model.SQMSys.Tb_Sys_User> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.SQMSys.Tb_Sys_User> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.SQMSys.Tb_Sys_User> modelList = new List<MobileSoft.Model.SQMSys.Tb_Sys_User>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.SQMSys.Tb_Sys_User model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.SQMSys.Tb_Sys_User();
					model.UserCode=dt.Rows[n]["UserCode"].ToString();
					if(dt.Rows[n]["StreetCode"].ToString()!="")
					{
						model.StreetCode=new Guid(dt.Rows[n]["StreetCode"].ToString());
					}
					model.UserName=dt.Rows[n]["UserName"].ToString();
					model.LoginCode=dt.Rows[n]["LoginCode"].ToString();
					model.PassWord=dt.Rows[n]["PassWord"].ToString();
					model.DepCode=dt.Rows[n]["DepCode"].ToString();
					model.EmployeeCode=dt.Rows[n]["EmployeeCode"].ToString();
					model.MobileTel=dt.Rows[n]["MobileTel"].ToString();
					model.Memo=dt.Rows[n]["Memo"].ToString();
					if(dt.Rows[n]["IsMobile"].ToString()!="")
					{
						model.IsMobile=int.Parse(dt.Rows[n]["IsMobile"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["DeleteDate"].ToString()!="")
					{
						model.DeleteDate=DateTime.Parse(dt.Rows[n]["DeleteDate"].ToString());
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

