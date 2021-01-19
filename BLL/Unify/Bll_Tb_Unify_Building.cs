using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Unify;
namespace MobileSoft.BLL.Unify
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Unify_Building ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Unify_Building
	{
        private readonly MobileSoft.DAL.Unify.Dal_Tb_Unify_Building dal = new MobileSoft.DAL.Unify.Dal_Tb_Unify_Building();
		public Bll_Tb_Unify_Building()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(Guid BuildSynchCode)
		{
			return dal.Exists(BuildSynchCode);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(ehome.Model.Unify.Tb_Unify_Building model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ehome.Model.Unify.Tb_Unify_Building model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(Guid BuildSynchCode)
		{
			
			dal.Delete(BuildSynchCode);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ehome.Model.Unify.Tb_Unify_Building GetModel(Guid BuildSynchCode)
		{
			
			return dal.GetModel(BuildSynchCode);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public ehome.Model.Unify.Tb_Unify_Building GetModelByCache(Guid BuildSynchCode)
		{
			
			string CacheKey = "Tb_Unify_BuildingModel-" + BuildSynchCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(BuildSynchCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ehome.Model.Unify.Tb_Unify_Building)objModel;
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere, string fieldOrder,string fieldList)
        {
            return dal.GetList(strWhere,fieldOrder, fieldList);
        }
		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string fieldOrder)
		{
			return dal.GetList(Top,strWhere,fieldOrder);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<ehome.Model.Unify.Tb_Unify_Building> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<ehome.Model.Unify.Tb_Unify_Building> DataTableToList(DataTable dt)
		{
			List<ehome.Model.Unify.Tb_Unify_Building> modelList = new List<ehome.Model.Unify.Tb_Unify_Building>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ehome.Model.Unify.Tb_Unify_Building model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ehome.Model.Unify.Tb_Unify_Building();
					if(dt.Rows[n]["BuildSynchCode"].ToString()!="")
					{
						model.BuildSynchCode=new Guid(dt.Rows[n]["BuildSynchCode"].ToString());
					}
					if(dt.Rows[n]["CommSynchCode"].ToString()!="")
					{
						model.CommSynchCode=new Guid(dt.Rows[n]["CommSynchCode"].ToString());
					}
					//model.UnBuildID=dt.Rows[n]["UnBuildID"].ToString();
					if(dt.Rows[n]["BuildSNum"].ToString()!="")
					{
						model.BuildSNum=int.Parse(dt.Rows[n]["BuildSNum"].ToString());
					}
					if(dt.Rows[n]["RegionSNum"].ToString()!="")
					{
						model.RegionSNum=int.Parse(dt.Rows[n]["RegionSNum"].ToString());
					}
					model.BuildSign=dt.Rows[n]["BuildSign"].ToString();
					model.BuildName=dt.Rows[n]["BuildName"].ToString();
					model.BuildType=dt.Rows[n]["BuildType"].ToString();
					model.BuildUses=dt.Rows[n]["BuildUses"].ToString();
					model.PropertyRights=dt.Rows[n]["PropertyRights"].ToString();
					model.PropertyUses=dt.Rows[n]["PropertyUses"].ToString();
					model.BuildHeight=dt.Rows[n]["BuildHeight"].ToString();
					if(dt.Rows[n]["FloorsNum"].ToString()!="")
					{
						model.FloorsNum=int.Parse(dt.Rows[n]["FloorsNum"].ToString());
					}
					if(dt.Rows[n]["UnderFloorsNum"].ToString()!="")
					{
						model.UnderFloorsNum=int.Parse(dt.Rows[n]["UnderFloorsNum"].ToString());
					}
					if(dt.Rows[n]["UnitNum"].ToString()!="")
					{
						model.UnitNum=int.Parse(dt.Rows[n]["UnitNum"].ToString());
					}
					model.NamingPatterns=dt.Rows[n]["NamingPatterns"].ToString();
					if(dt.Rows[n]["PerFloorNum"].ToString()!="")
					{
						model.PerFloorNum=int.Parse(dt.Rows[n]["PerFloorNum"].ToString());
					}
					if(dt.Rows[n]["HouseholdsNum"].ToString()!="")
					{
						model.HouseholdsNum=int.Parse(dt.Rows[n]["HouseholdsNum"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["SynchFlag"].ToString()!="")
					{
						model.SynchFlag=int.Parse(dt.Rows[n]["SynchFlag"].ToString());
					}
					//model.OrdSNum=dt.Rows[n]["OrdSNum"].ToString();
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize,string SortField,int Sort)
		{
			return dal.GetList(out PageCount, out Counts, StrCondition, PageIndex, PageSize,SortField,Sort);
		}

		#endregion  ��Ա����
	}
}

