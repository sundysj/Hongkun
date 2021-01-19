using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Club;
namespace MobileSoft.BLL.Club
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Club_Warehouse ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Club_Warehouse
	{
		private readonly MobileSoft.DAL.Club.Dal_Tb_Club_Warehouse dal=new MobileSoft.DAL.Club.Dal_Tb_Club_Warehouse();
		public Bll_Tb_Club_Warehouse()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long WareHouseID)
		{
			return dal.Exists(WareHouseID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.Club.Tb_Club_Warehouse model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Club.Tb_Club_Warehouse model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long WareHouseID)
		{
			
			dal.Delete(WareHouseID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Club.Tb_Club_Warehouse GetModel(long WareHouseID)
		{
			
			return dal.GetModel(WareHouseID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Club.Tb_Club_Warehouse GetModelByCache(long WareHouseID)
		{
			
			string CacheKey = "Tb_Club_WarehouseModel-" + WareHouseID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(WareHouseID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Club.Tb_Club_Warehouse)objModel;
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
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
		public List<MobileSoft.Model.Club.Tb_Club_Warehouse> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Club.Tb_Club_Warehouse> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Club.Tb_Club_Warehouse> modelList = new List<MobileSoft.Model.Club.Tb_Club_Warehouse>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Club.Tb_Club_Warehouse model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Club.Tb_Club_Warehouse();
					//model.WareHouseID=dt.Rows[n]["WareHouseID"].ToString();
					model.OrganCode=dt.Rows[n]["OrganCode"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.WareHouseCode=dt.Rows[n]["WareHouseCode"].ToString();
					model.WareHouseName=dt.Rows[n]["WareHouseName"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["IsDefault"].ToString()!="")
					{
						model.IsDefault=int.Parse(dt.Rows[n]["IsDefault"].ToString());
					}
					if(dt.Rows[n]["IsOrgan"].ToString()!="")
					{
						model.IsOrgan=int.Parse(dt.Rows[n]["IsOrgan"].ToString());
					}
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

