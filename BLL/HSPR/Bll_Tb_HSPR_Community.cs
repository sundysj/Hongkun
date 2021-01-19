using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_HSPR_Community 的摘要说明。
	/// </summary>
	public class Bll_Tb_HSPR_Community
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_Community dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_Community();
		public Bll_Tb_HSPR_Community()
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
		public bool Exists(int CommID)
		{
			return dal.Exists(CommID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_Community model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_Community model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int CommID)
		{
			
			dal.Delete(CommID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_Community GetModel(int CommID)
		{
			
			return dal.GetModel(CommID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_Community GetModelByCache(int CommID)
		{
			
			string CacheKey = "Tb_HSPR_CommunityModel-" + CommID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CommID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_Community)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_Community> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_Community> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_Community> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_Community>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_Community model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_Community();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					if(dt.Rows[n]["BranchID"].ToString()!="")
					{
						model.BranchID=int.Parse(dt.Rows[n]["BranchID"].ToString());
					}
					if(dt.Rows[n]["CorpID"].ToString()!="")
					{
						model.CorpID=int.Parse(dt.Rows[n]["CorpID"].ToString());
					}
					model.CommName=dt.Rows[n]["CommName"].ToString();
					model.CommAddress=dt.Rows[n]["CommAddress"].ToString();
					model.Province=dt.Rows[n]["Province"].ToString();
					model.City=dt.Rows[n]["City"].ToString();
					model.Borough=dt.Rows[n]["Borough"].ToString();
					model.Street=dt.Rows[n]["Street"].ToString();
					model.GateSign=dt.Rows[n]["GateSign"].ToString();
					model.CorpGroupCode=dt.Rows[n]["CorpGroupCode"].ToString();
					model.CorpRegionCode=dt.Rows[n]["CorpRegionCode"].ToString();
					model.CommSpell=dt.Rows[n]["CommSpell"].ToString();
					if(dt.Rows[n]["RegDate"].ToString()!="")
					{
						model.RegDate=DateTime.Parse(dt.Rows[n]["RegDate"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					model.OrganCode=dt.Rows[n]["OrganCode"].ToString();
					if(dt.Rows[n]["UseBeginDate"].ToString()!="")
					{
						model.UseBeginDate=DateTime.Parse(dt.Rows[n]["UseBeginDate"].ToString());
					}
					if(dt.Rows[n]["UseEndDate"].ToString()!="")
					{
						model.UseEndDate=DateTime.Parse(dt.Rows[n]["UseEndDate"].ToString());
					}
					model.CommKind=dt.Rows[n]["CommKind"].ToString();
					if(dt.Rows[n]["ManageTime"].ToString()!="")
					{
						model.ManageTime=DateTime.Parse(dt.Rows[n]["ManageTime"].ToString());
					}
					model.ManageKind=dt.Rows[n]["ManageKind"].ToString();
					if(dt.Rows[n]["CommSynchCode"].ToString()!="")
					{
						model.CommSynchCode=new Guid(dt.Rows[n]["CommSynchCode"].ToString());
					}
					if(dt.Rows[n]["SynchFlag"].ToString()!="")
					{
						model.SynchFlag=int.Parse(dt.Rows[n]["SynchFlag"].ToString());
					}
					if(dt.Rows[n]["ContractBeginDate"].ToString()!="")
					{
						model.ContractBeginDate=DateTime.Parse(dt.Rows[n]["ContractBeginDate"].ToString());
					}
					if(dt.Rows[n]["ContractEndDate"].ToString()!="")
					{
						model.ContractEndDate=DateTime.Parse(dt.Rows[n]["ContractEndDate"].ToString());
					}
					if(dt.Rows[n]["SysStartDate"].ToString()!="")
					{
						model.SysStartDate=DateTime.Parse(dt.Rows[n]["SysStartDate"].ToString());
					}
					if(dt.Rows[n]["SysLogDate"].ToString()!="")
					{
						model.SysLogDate=DateTime.Parse(dt.Rows[n]["SysLogDate"].ToString());
					}
					if(dt.Rows[n]["IsFees"].ToString()!="")
					{
						model.IsFees=int.Parse(dt.Rows[n]["IsFees"].ToString());
					}
					model.Memo=dt.Rows[n]["Memo"].ToString();
					if(dt.Rows[n]["CommType"].ToString()!="")
					{
						model.CommType=int.Parse(dt.Rows[n]["CommType"].ToString());
					}
					if(dt.Rows[n]["Num"].ToString()!="")
					{
						model.Num=int.Parse(dt.Rows[n]["Num"].ToString());
					}
					model.CommunityName=dt.Rows[n]["CommunityName"].ToString();
					model.CommunityCode=dt.Rows[n]["CommunityCode"].ToString();
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


            public DataTable OrganGetEntryNodes(string UserCode)
            {
                  return dal.OrganGetEntryNodes(UserCode);
            }
	}
}

