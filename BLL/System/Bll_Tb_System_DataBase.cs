using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.System;
namespace MobileSoft.BLL.System
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_System_DataBase 的摘要说明。
	/// </summary>
	public class Bll_Tb_System_DataBase
	{
		private readonly MobileSoft.DAL.System.Dal_Tb_System_DataBase dal=new MobileSoft.DAL.System.Dal_Tb_System_DataBase();
		public Bll_Tb_System_DataBase()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int DBID)
		{
			return dal.Exists(DBID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MobileSoft.Model.System.Tb_System_DataBase model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_DataBase model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int DBID)
		{
			
			dal.Delete(DBID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_DataBase GetModel(int DBID)
		{
			
			return dal.GetModel(DBID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.System.Tb_System_DataBase GetModelByCache(int DBID)
		{
			
			string CacheKey = "Tb_System_DataBaseModel-" + DBID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(DBID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.System.Tb_System_DataBase)objModel;
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
		public List<MobileSoft.Model.System.Tb_System_DataBase> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.System.Tb_System_DataBase> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.System.Tb_System_DataBase> modelList = new List<MobileSoft.Model.System.Tb_System_DataBase>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.System.Tb_System_DataBase model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.System.Tb_System_DataBase();
					if(dt.Rows[n]["DBID"].ToString()!="")
					{
						model.DBID=int.Parse(dt.Rows[n]["DBID"].ToString());
					}
					if(dt.Rows[n]["ProvinceID"].ToString()!="")
					{
						model.ProvinceID=int.Parse(dt.Rows[n]["ProvinceID"].ToString());
					}
					if(dt.Rows[n]["CityID"].ToString()!="")
					{
						model.CityID=int.Parse(dt.Rows[n]["CityID"].ToString());
					}
					if(dt.Rows[n]["BoroughID"].ToString()!="")
					{
						model.BoroughID=int.Parse(dt.Rows[n]["BoroughID"].ToString());
					}
					if(dt.Rows[n]["StreetID"].ToString()!="")
					{
						model.StreetID=int.Parse(dt.Rows[n]["StreetID"].ToString());
					}
					model.DBServer=dt.Rows[n]["DBServer"].ToString();
					model.DBName=dt.Rows[n]["DBName"].ToString();
					model.DBUser=dt.Rows[n]["DBUser"].ToString();
					model.DBPwd=dt.Rows[n]["DBPwd"].ToString();
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

