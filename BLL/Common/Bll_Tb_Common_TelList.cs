using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Common;
namespace MobileSoft.BLL.Common
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Common_TelList 的摘要说明。
	/// </summary>
	public class Bll_Tb_Common_TelList
	{
		private readonly MobileSoft.DAL.Common.Dal_Tb_Common_TelList dal=new MobileSoft.DAL.Common.Dal_Tb_Common_TelList();
		public Bll_Tb_Common_TelList()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long InfoId)
		{
			return dal.Exists(InfoId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Common.Tb_Common_TelList model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Common.Tb_Common_TelList model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long InfoId)
		{
			
			dal.Delete(InfoId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Common.Tb_Common_TelList GetModel(long InfoId)
		{
			
			return dal.GetModel(InfoId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Common.Tb_Common_TelList GetModelByCache(long InfoId)
		{
			
			string CacheKey = "Tb_Common_TelListModel-" + InfoId;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(InfoId);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Common.Tb_Common_TelList)objModel;
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
		public List<MobileSoft.Model.Common.Tb_Common_TelList> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Common.Tb_Common_TelList> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Common.Tb_Common_TelList> modelList = new List<MobileSoft.Model.Common.Tb_Common_TelList>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Common.Tb_Common_TelList model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Common.Tb_Common_TelList();
					//model.InfoId=dt.Rows[n]["InfoId"].ToString();
					model.UserCode=dt.Rows[n]["UserCode"].ToString();
					model.TelNum=dt.Rows[n]["TelNum"].ToString();
					model.Mark=dt.Rows[n]["Mark"].ToString();
					model.Duty=dt.Rows[n]["Duty"].ToString();
					if(dt.Rows[n]["Sort"].ToString()!="")
					{
						model.Sort=int.Parse(dt.Rows[n]["Sort"].ToString());
					}
					model.Phone=dt.Rows[n]["Phone"].ToString();
					model.Mail=dt.Rows[n]["Mail"].ToString();
					model.CompanyName=dt.Rows[n]["CompanyName"].ToString();
					model.CommName=dt.Rows[n]["CommName"].ToString();
					model.OprName=dt.Rows[n]["OprName"].ToString();
					if(dt.Rows[n]["CompanyNameSort"].ToString()!="")
					{
						model.CompanyNameSort=int.Parse(dt.Rows[n]["CompanyNameSort"].ToString());
					}
					if(dt.Rows[n]["CommNameSort"].ToString()!="")
					{
						model.CommNameSort=int.Parse(dt.Rows[n]["CommNameSort"].ToString());
					}
					if(dt.Rows[n]["DutySort"].ToString()!="")
					{
						model.DutySort=int.Parse(dt.Rows[n]["DutySort"].ToString());
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

