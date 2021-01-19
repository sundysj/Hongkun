using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using HM.Model.Qm;
namespace HM.BLL.Qm
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Qm_Standard 的摘要说明。
	/// </summary>
	public class Bll_Tb_Qm_Standard
	{
		private readonly HM.DAL.Qm.Dal_Tb_Qm_Standard dal=new HM.DAL.Qm.Dal_Tb_Qm_Standard();
		public Bll_Tb_Qm_Standard()
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
		public void Add(HM.Model.Qm.Tb_Qm_Standard model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(HM.Model.Qm.Tb_Qm_Standard model)
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
        public void updateIsUse(string strWhere, int Is)
        {
            dal.updateIsUse(strWhere, Is);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HM.Model.Qm.Tb_Qm_Standard GetModel(string Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public HM.Model.Qm.Tb_Qm_Standard GetModelByCache(string Id)
		{
			
			string CacheKey = "Tb_Qm_StandardModel-" + Id;
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
			return (HM.Model.Qm.Tb_Qm_Standard)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
        public DataSet GetMaxList(string strWhere)
        {
            return dal.GetMaxList(strWhere);
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
		public List<HM.Model.Qm.Tb_Qm_Standard> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<HM.Model.Qm.Tb_Qm_Standard> DataTableToList(DataTable dt)
		{
			List<HM.Model.Qm.Tb_Qm_Standard> modelList = new List<HM.Model.Qm.Tb_Qm_Standard>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				HM.Model.Qm.Tb_Qm_Standard model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new HM.Model.Qm.Tb_Qm_Standard();
					model.Id=dt.Rows[n]["Id"].ToString();
					model.Code=dt.Rows[n]["Code"].ToString();
					model.SuitableItemTypeId=dt.Rows[n]["SuitableItemTypeId"].ToString();
					model.Professional=dt.Rows[n]["Professional"].ToString();
					model.Type=dt.Rows[n]["Type"].ToString();
					model.TypeDescription=dt.Rows[n]["TypeDescription"].ToString();
					model.CheckStandard=dt.Rows[n]["CheckStandard"].ToString();
					model.CheckWay=dt.Rows[n]["CheckWay"].ToString();
					if(dt.Rows[n]["Point"].ToString()!="")
					{
						model.Point=decimal.Parse(dt.Rows[n]["Point"].ToString());
					}
					if(dt.Rows[n]["IsCoerce"].ToString()!="")
					{
						model.IsCoerce=int.Parse(dt.Rows[n]["IsCoerce"].ToString());
					}
					if(dt.Rows[n]["IsUse"].ToString()!="")
					{
						model.IsUse=int.Parse(dt.Rows[n]["IsUse"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["Sort"].ToString()!="")
					{
						model.Sort=int.Parse(dt.Rows[n]["Sort"].ToString());
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

