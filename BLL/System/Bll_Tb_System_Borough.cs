using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.System;
namespace MobileSoft.BLL.System
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_System_Borough 的摘要说明。
	/// </summary>
	public class Bll_Tb_System_Borough
	{
		private readonly MobileSoft.DAL.System.Dal_Tb_System_Borough dal=new MobileSoft.DAL.System.Dal_Tb_System_Borough();
		public Bll_Tb_System_Borough()
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
		public bool Exists(int BoroughID,int CityID,string BoroughName)
		{
			return dal.Exists(BoroughID,CityID,BoroughName);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.System.Tb_System_Borough model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Borough model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int BoroughID,int CityID,string BoroughName)
		{
			
			dal.Delete(BoroughID,CityID,BoroughName);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Borough GetModel(int BoroughID,int CityID,string BoroughName)
		{
			
			return dal.GetModel(BoroughID,CityID,BoroughName);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Borough GetModelByCache(int BoroughID,int CityID,string BoroughName)
		{
			
			string CacheKey = "Tb_System_BoroughModel-" + BoroughID+CityID+BoroughName;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(BoroughID,CityID,BoroughName);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.System.Tb_System_Borough)objModel;
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
		public List<MobileSoft.Model.System.Tb_System_Borough> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.System.Tb_System_Borough> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.System.Tb_System_Borough> modelList = new List<MobileSoft.Model.System.Tb_System_Borough>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.System.Tb_System_Borough model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.System.Tb_System_Borough();
					if(dt.Rows[n]["BoroughID"].ToString()!="")
					{
						model.BoroughID=int.Parse(dt.Rows[n]["BoroughID"].ToString());
					}
					if(dt.Rows[n]["CityID"].ToString()!="")
					{
						model.CityID=int.Parse(dt.Rows[n]["CityID"].ToString());
					}
					model.BoroughName=dt.Rows[n]["BoroughName"].ToString();
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

