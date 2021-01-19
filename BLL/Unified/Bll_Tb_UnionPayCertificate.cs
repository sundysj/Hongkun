using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Unified;
namespace MobileSoft.BLL.Unified
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_UnionPayCertificate 的摘要说明。
	/// </summary>
	public class Bll_Tb_UnionPayCertificate
	{
		private readonly MobileSoft.DAL.Unified.Dal_Tb_UnionPayCertificate dal=new MobileSoft.DAL.Unified.Dal_Tb_UnionPayCertificate();
		public Bll_Tb_UnionPayCertificate()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Unified.Tb_UnionPayCertificate model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Unified.Tb_UnionPayCertificate model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string Id)
		{
			
			dal.Delete(Id);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Unified.Tb_UnionPayCertificate GetModel(string Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Unified.Tb_UnionPayCertificate GetModelByCache(string Id)
		{
			
			string CacheKey = "Tb_UnionPayCertificateModel-" + Id;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Id);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Unified.Tb_UnionPayCertificate)objModel;
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
		public List<MobileSoft.Model.Unified.Tb_UnionPayCertificate> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Unified.Tb_UnionPayCertificate> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Unified.Tb_UnionPayCertificate> modelList = new List<MobileSoft.Model.Unified.Tb_UnionPayCertificate>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Unified.Tb_UnionPayCertificate model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Unified.Tb_UnionPayCertificate();
					model.Id=dt.Rows[n]["Id"].ToString();
					model.CommunityId=dt.Rows[n]["CommunityId"].ToString();
					model.signCertPath=dt.Rows[n]["signCertPath"].ToString();
					model.signCertPwd=dt.Rows[n]["signCertPwd"].ToString();
					model.validateCertDir=dt.Rows[n]["validateCertDir"].ToString();
					model.encryptCert=dt.Rows[n]["encryptCert"].ToString();
					model.merId=dt.Rows[n]["merId"].ToString();
					model.accNo=dt.Rows[n]["accNo"].ToString();
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

