using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.SQMSys;
namespace MobileSoft.BLL.SQMSys
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Sys_Field 的摘要说明。
	/// </summary>
	public class Bll_Tb_Sys_Field
	{
		private readonly MobileSoft.DAL.SQMSys.Dal_Tb_Sys_Field dal=new MobileSoft.DAL.SQMSys.Dal_Tb_Sys_Field();
		public Bll_Tb_Sys_Field()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid FieldCode)
		{
			return dal.Exists(FieldCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_Field model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_Field model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Guid FieldCode)
		{
			
			dal.Delete(FieldCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Field GetModel(Guid FieldCode)
		{
			
			return dal.GetModel(FieldCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Field GetModelByCache(Guid FieldCode)
		{
			
			string CacheKey = "Tb_Sys_FieldModel-" + FieldCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(FieldCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.SQMSys.Tb_Sys_Field)objModel;
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
		public List<MobileSoft.Model.SQMSys.Tb_Sys_Field> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.SQMSys.Tb_Sys_Field> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.SQMSys.Tb_Sys_Field> modelList = new List<MobileSoft.Model.SQMSys.Tb_Sys_Field>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.SQMSys.Tb_Sys_Field model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.SQMSys.Tb_Sys_Field();
					if(dt.Rows[n]["FieldCode"].ToString()!="")
					{
						model.FieldCode=new Guid(dt.Rows[n]["FieldCode"].ToString());
					}
					model.TemplateCode=dt.Rows[n]["TemplateCode"].ToString();
					model.FieldSign=dt.Rows[n]["FieldSign"].ToString();
					model.FieldName=dt.Rows[n]["FieldName"].ToString();
					model.DefaultName=dt.Rows[n]["DefaultName"].ToString();
					if(dt.Rows[n]["FieldOrderID"].ToString()!="")
					{
						model.FieldOrderID=int.Parse(dt.Rows[n]["FieldOrderID"].ToString());
					}
					if(dt.Rows[n]["FieldType"].ToString()!="")
					{
						model.FieldType=int.Parse(dt.Rows[n]["FieldType"].ToString());
					}
					if(dt.Rows[n]["FieldLength"].ToString()!="")
					{
						model.FieldLength=int.Parse(dt.Rows[n]["FieldLength"].ToString());
					}
					if(dt.Rows[n]["IsUsed"].ToString()!="")
					{
						model.IsUsed=int.Parse(dt.Rows[n]["IsUsed"].ToString());
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

