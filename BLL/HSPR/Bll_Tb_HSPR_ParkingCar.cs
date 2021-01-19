using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_HSPR_ParkingCar ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_HSPR_ParkingCar
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_ParkingCar dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_ParkingCar();
		public Bll_Tb_HSPR_ParkingCar()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long CarID)
		{
			return dal.Exists(CarID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_ParkingCar model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_ParkingCar model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long CarID)
		{
			
			dal.Delete(CarID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_ParkingCar GetModel(long CarID)
		{
			
			return dal.GetModel(CarID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_ParkingCar GetModelByCache(long CarID)
		{
			
			string CacheKey = "Tb_HSPR_ParkingCarModel-" + CarID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CarID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_ParkingCar)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_ParkingCar> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_ParkingCar> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_ParkingCar> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_ParkingCar>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_ParkingCar model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_ParkingCar();
					//model.CarID=dt.Rows[n]["CarID"].ToString();
					//model.HandID=dt.Rows[n]["HandID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					//model.ParkID=dt.Rows[n]["ParkID"].ToString();
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					model.RoomSign=dt.Rows[n]["RoomSign"].ToString();
					model.ParkingCarSign=dt.Rows[n]["ParkingCarSign"].ToString();
					model.CarSign=dt.Rows[n]["CarSign"].ToString();
					model.CarType=dt.Rows[n]["CarType"].ToString();
					model.CarEngineSign=dt.Rows[n]["CarEngineSign"].ToString();
					model.CarShelfSign=dt.Rows[n]["CarShelfSign"].ToString();
					model.FacBrands=dt.Rows[n]["FacBrands"].ToString();
					model.Weight=dt.Rows[n]["Weight"].ToString();
					model.Deadweight=dt.Rows[n]["Deadweight"].ToString();
					model.Passenger=dt.Rows[n]["Passenger"].ToString();
					model.FrontPass=dt.Rows[n]["FrontPass"].ToString();
					if(dt.Rows[n]["CarRegDate"].ToString()!="")
					{
						model.CarRegDate=DateTime.Parse(dt.Rows[n]["CarRegDate"].ToString());
					}
					if(dt.Rows[n]["CarGrantDate"].ToString()!="")
					{
						model.CarGrantDate=DateTime.Parse(dt.Rows[n]["CarGrantDate"].ToString());
					}
					model.CarInsurer=dt.Rows[n]["CarInsurer"].ToString();
					model.CarInsSign=dt.Rows[n]["CarInsSign"].ToString();
					model.CarInsContent=dt.Rows[n]["CarInsContent"].ToString();
					model.CarEmission=dt.Rows[n]["CarEmission"].ToString();
					model.CarColor=dt.Rows[n]["CarColor"].ToString();
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

