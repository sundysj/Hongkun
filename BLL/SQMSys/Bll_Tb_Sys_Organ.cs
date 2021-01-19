using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.SQMSys;
namespace MobileSoft.BLL.SQMSys
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Sys_Organ 的摘要说明。
	/// </summary>
	public class Bll_Tb_Sys_Organ
	{
		private readonly MobileSoft.DAL.SQMSys.Dal_Tb_Sys_Organ dal=new MobileSoft.DAL.SQMSys.Dal_Tb_Sys_Organ();
		public Bll_Tb_Sys_Organ()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string OrganCode,Guid StreetCode)
		{
			return dal.Exists(OrganCode,StreetCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_Organ model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_Organ model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string OrganCode,Guid StreetCode)
		{
			
			dal.Delete(OrganCode,StreetCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Organ GetModel(string OrganCode,Guid StreetCode)
		{
			
			return dal.GetModel(OrganCode,StreetCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Organ GetModelByCache(string OrganCode,Guid StreetCode)
		{
			
			string CacheKey = "Tb_Sys_OrganModel-" + OrganCode+StreetCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(OrganCode,StreetCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.SQMSys.Tb_Sys_Organ)objModel;
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
		public List<MobileSoft.Model.SQMSys.Tb_Sys_Organ> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.SQMSys.Tb_Sys_Organ> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.SQMSys.Tb_Sys_Organ> modelList = new List<MobileSoft.Model.SQMSys.Tb_Sys_Organ>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.SQMSys.Tb_Sys_Organ model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.SQMSys.Tb_Sys_Organ();
					model.OrganCode=dt.Rows[n]["OrganCode"].ToString();
					if(dt.Rows[n]["StreetCode"].ToString()!="")
					{
						model.StreetCode=new Guid(dt.Rows[n]["StreetCode"].ToString());
					}
					model.OrganName=dt.Rows[n]["OrganName"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["IsComp"].ToString()!="")
					{
						model.IsComp=int.Parse(dt.Rows[n]["IsComp"].ToString());
					}
					if(dt.Rows[n]["Num"].ToString()!="")
					{
						model.Num=int.Parse(dt.Rows[n]["Num"].ToString());
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

