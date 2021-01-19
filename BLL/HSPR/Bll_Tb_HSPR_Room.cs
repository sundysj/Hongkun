using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_HSPR_Room ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_HSPR_Room
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_Room dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_Room();
		public Bll_Tb_HSPR_Room()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long RoomID)
		{
			return dal.Exists(RoomID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_Room model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_Room model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long RoomID)
		{
			
			dal.Delete(RoomID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_Room GetModel(long RoomID)
		{
			
			return dal.GetModel(RoomID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_Room GetModelByCache(long RoomID)
		{
			
			string CacheKey = "Tb_HSPR_RoomModel-" + RoomID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(RoomID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_Room)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_Room> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_Room> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_Room> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_Room>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_Room model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_Room();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.RoomSign=dt.Rows[n]["RoomSign"].ToString();
					model.RoomName=dt.Rows[n]["RoomName"].ToString();
					if(dt.Rows[n]["RegionSNum"].ToString()!="")
					{
						model.RegionSNum=int.Parse(dt.Rows[n]["RegionSNum"].ToString());
					}
					if(dt.Rows[n]["BuildSNum"].ToString()!="")
					{
						model.BuildSNum=int.Parse(dt.Rows[n]["BuildSNum"].ToString());
					}
					if(dt.Rows[n]["UnitSNum"].ToString()!="")
					{
						model.UnitSNum=int.Parse(dt.Rows[n]["UnitSNum"].ToString());
					}
					if(dt.Rows[n]["FloorSNum"].ToString()!="")
					{
						model.FloorSNum=int.Parse(dt.Rows[n]["FloorSNum"].ToString());
					}
					if(dt.Rows[n]["RoomSNum"].ToString()!="")
					{
						model.RoomSNum=int.Parse(dt.Rows[n]["RoomSNum"].ToString());
					}
					model.RoomModel=dt.Rows[n]["RoomModel"].ToString();
					model.RoomType=dt.Rows[n]["RoomType"].ToString();
					model.PropertyRights=dt.Rows[n]["PropertyRights"].ToString();
					model.RoomTowards=dt.Rows[n]["RoomTowards"].ToString();
					if(dt.Rows[n]["BuildArea"].ToString()!="")
					{
						model.BuildArea=decimal.Parse(dt.Rows[n]["BuildArea"].ToString());
					}
					if(dt.Rows[n]["InteriorArea"].ToString()!="")
					{
						model.InteriorArea=decimal.Parse(dt.Rows[n]["InteriorArea"].ToString());
					}
					if(dt.Rows[n]["CommonArea"].ToString()!="")
					{
						model.CommonArea=decimal.Parse(dt.Rows[n]["CommonArea"].ToString());
					}
					model.RightsSign=dt.Rows[n]["RightsSign"].ToString();
					model.PropertyUses=dt.Rows[n]["PropertyUses"].ToString();
					if(dt.Rows[n]["RoomState"].ToString()!="")
					{
						model.RoomState=int.Parse(dt.Rows[n]["RoomState"].ToString());
					}
					//model.ChargeTypeID=dt.Rows[n]["ChargeTypeID"].ToString();
					if(dt.Rows[n]["UsesState"].ToString()!="")
					{
						model.UsesState=int.Parse(dt.Rows[n]["UsesState"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					model.FloorHeight=dt.Rows[n]["FloorHeight"].ToString();
					model.BuildStructure=dt.Rows[n]["BuildStructure"].ToString();
					if(dt.Rows[n]["PoolRatio"].ToString()!="")
					{
						model.PoolRatio=decimal.Parse(dt.Rows[n]["PoolRatio"].ToString());
					}
					model.BearParameters=dt.Rows[n]["BearParameters"].ToString();
					model.Renovation=dt.Rows[n]["Renovation"].ToString();
					model.Configuration=dt.Rows[n]["Configuration"].ToString();
					model.Advertising=dt.Rows[n]["Advertising"].ToString();
					if(dt.Rows[n]["IsSplitUnite"].ToString()!="")
					{
						model.IsSplitUnite=int.Parse(dt.Rows[n]["IsSplitUnite"].ToString());
					}
					if(dt.Rows[n]["GardenArea"].ToString()!="")
					{
						model.GardenArea=decimal.Parse(dt.Rows[n]["GardenArea"].ToString());
					}
					if(dt.Rows[n]["PropertyArea"].ToString()!="")
					{
						model.PropertyArea=decimal.Parse(dt.Rows[n]["PropertyArea"].ToString());
					}
					if(dt.Rows[n]["AreaType"].ToString()!="")
					{
						model.AreaType=int.Parse(dt.Rows[n]["AreaType"].ToString());
					}
					if(dt.Rows[n]["YardArea"].ToString()!="")
					{
						model.YardArea=decimal.Parse(dt.Rows[n]["YardArea"].ToString());
					}
					model.DelUser=dt.Rows[n]["DelUser"].ToString();
					if(dt.Rows[n]["DelDate"].ToString()!="")
					{
						model.DelDate=DateTime.Parse(dt.Rows[n]["DelDate"].ToString());
					}
					if(dt.Rows[n]["ResideType"].ToString()!="")
					{
						model.ResideType=int.Parse(dt.Rows[n]["ResideType"].ToString());
					}
					if(dt.Rows[n]["RoomSynchCode"].ToString()!="")
					{
						model.RoomSynchCode=new Guid(dt.Rows[n]["RoomSynchCode"].ToString());
					}
					if(dt.Rows[n]["SynchFlag"].ToString()!="")
					{
						model.SynchFlag=int.Parse(dt.Rows[n]["SynchFlag"].ToString());
					}
					//model.BedTypeID=dt.Rows[n]["BedTypeID"].ToString();
					if(dt.Rows[n]["UseType"].ToString()!="")
					{
						model.UseType=int.Parse(dt.Rows[n]["UseType"].ToString());
					}
					if(dt.Rows[n]["IsFrozen"].ToString()!="")
					{
						model.IsFrozen=int.Parse(dt.Rows[n]["IsFrozen"].ToString());
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

