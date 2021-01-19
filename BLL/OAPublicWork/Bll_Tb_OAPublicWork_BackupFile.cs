using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.OAPublicWork;
namespace MobileSoft.BLL.OAPublicWork
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_OAPublicWork_BackupFile 的摘要说明。
	/// </summary>
	public class Bll_Tb_OAPublicWork_BackupFile
	{
		private readonly MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_BackupFile dal=new MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_BackupFile();
		public Bll_Tb_OAPublicWork_BackupFile()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string InfoCode)
		{
			return dal.Exists(InfoCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string InfoCode)
		{
			
			dal.Delete(InfoCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile GetModel(string InfoCode)
		{
			
			return dal.GetModel(InfoCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile GetModelByCache(string InfoCode)
		{
			
			string CacheKey = "Tb_OAPublicWork_BackupFileModel-" + InfoCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(InfoCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile)objModel;
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
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile> modelList = new List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile();
					model.InfoCode=dt.Rows[n]["InfoCode"].ToString();
					model.FName=dt.Rows[n]["FName"].ToString();
					model.OriginallyFilePath=dt.Rows[n]["OriginallyFilePath"].ToString();
					model.PresentFilePath=dt.Rows[n]["PresentFilePath"].ToString();
					if(dt.Rows[n]["BackupDate"].ToString()!="")
					{
						model.BackupDate=DateTime.Parse(dt.Rows[n]["BackupDate"].ToString());
					}
					model.RestoreUserName=dt.Rows[n]["RestoreUserName"].ToString();
					if(dt.Rows[n]["RestoreDate"].ToString()!="")
					{
						model.RestoreDate=DateTime.Parse(dt.Rows[n]["RestoreDate"].ToString());
					}
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

