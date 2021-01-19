using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.OAPublicWork;
namespace MobileSoft.BLL.OAPublicWork
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_OAPublicWork_OfficialDocument 的摘要说明。
	/// </summary>
	public class Bll_Tb_OAPublicWork_OfficialDocument
	{
		private readonly MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_OfficialDocument dal=new MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_OfficialDocument();
		public Bll_Tb_OAPublicWork_OfficialDocument()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int InfoId)
		{
			return dal.Exists(InfoId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialDocument model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialDocument model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int InfoId)
		{
			
			dal.Delete(InfoId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialDocument GetModel(int InfoId)
		{
			
			return dal.GetModel(InfoId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialDocument GetModelByCache(int InfoId)
		{
			
			string CacheKey = "Tb_OAPublicWork_OfficialDocumentModel-" + InfoId;
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
			return (MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialDocument)objModel;
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
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialDocument> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialDocument> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialDocument> modelList = new List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialDocument>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialDocument model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialDocument();
					if(dt.Rows[n]["InfoId"].ToString()!="")
					{
						model.InfoId=int.Parse(dt.Rows[n]["InfoId"].ToString());
					}
					if(dt.Rows[n]["Tb_WorkFlow_FlowSort_InfoId"].ToString()!="")
					{
						model.Tb_WorkFlow_FlowSort_InfoId=int.Parse(dt.Rows[n]["Tb_WorkFlow_FlowSort_InfoId"].ToString());
					}
					model.UserName=dt.Rows[n]["UserName"].ToString();
					model.FileCode=dt.Rows[n]["FileCode"].ToString();
					model.FileTitle=dt.Rows[n]["FileTitle"].ToString();
					model.DispatchUnits=dt.Rows[n]["DispatchUnits"].ToString();
					model.Writer=dt.Rows[n]["Writer"].ToString();
					if(dt.Rows[n]["WriteDate"].ToString()!="")
					{
						model.WriteDate=DateTime.Parse(dt.Rows[n]["WriteDate"].ToString());
					}
					model.ReciveUnits=dt.Rows[n]["ReciveUnits"].ToString();
					model.FileSecret=dt.Rows[n]["FileSecret"].ToString();
					model.Nervous=dt.Rows[n]["Nervous"].ToString();
					model.KeyWords=dt.Rows[n]["KeyWords"].ToString();
					model.InfoContent=dt.Rows[n]["InfoContent"].ToString();
					model.DocumentUrl=dt.Rows[n]["DocumentUrl"].ToString();
					if(dt.Rows[n]["WorkStartDate"].ToString()!="")
					{
						model.WorkStartDate=DateTime.Parse(dt.Rows[n]["WorkStartDate"].ToString());
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

            public DataTable GetWaitWorkList(string LoginUserCode, string Type)
            {
                  return dal.GetWaitWorkList(LoginUserCode, Type);
            }
	}
}

