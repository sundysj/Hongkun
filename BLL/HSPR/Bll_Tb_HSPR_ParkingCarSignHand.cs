using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_HSPR_ParkingCarSignHand ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_HSPR_ParkingCarSignHand
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_ParkingCarSignHand dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_ParkingCarSignHand();
		public Bll_Tb_HSPR_ParkingCarSignHand()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long CarSignID)
		{
			return dal.Exists(CarSignID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public long Add(MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignHand model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignHand model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long CarSignID)
		{
			
			dal.Delete(CarSignID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignHand GetModel(long CarSignID)
		{
			
			return dal.GetModel(CarSignID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignHand GetModelByCache(long CarSignID)
		{
			
			string CacheKey = "Tb_HSPR_ParkingCarSignHandModel-" + CarSignID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CarSignID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignHand)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignHand> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignHand> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignHand> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignHand>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignHand model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignHand();
					//model.CarSignID=dt.Rows[n]["CarSignID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					//model.ReceID=dt.Rows[n]["ReceID"].ToString();
					//model.ParkID=dt.Rows[n]["ParkID"].ToString();
					//model.HandID=dt.Rows[n]["HandID"].ToString();
					//model.CostID=dt.Rows[n]["CostID"].ToString();
					if(dt.Rows[n]["FeesStateDate"].ToString()!="")
					{
						model.FeesStateDate=DateTime.Parse(dt.Rows[n]["FeesStateDate"].ToString());
					}
					if(dt.Rows[n]["FeesEndDate"].ToString()!="")
					{
						model.FeesEndDate=DateTime.Parse(dt.Rows[n]["FeesEndDate"].ToString());
					}
					if(dt.Rows[n]["ChargeAmount"].ToString()!="")
					{
						model.ChargeAmount=decimal.Parse(dt.Rows[n]["ChargeAmount"].ToString());
					}
					model.SerialNum=dt.Rows[n]["SerialNum"].ToString();
					model.BarrierSign=dt.Rows[n]["BarrierSign"].ToString();
					if(dt.Rows[n]["IsHisFees"].ToString()!="")
					{
						model.IsHisFees=int.Parse(dt.Rows[n]["IsHisFees"].ToString());
					}
					if(dt.Rows[n]["IsHand"].ToString()!="")
					{
						model.IsHand=int.Parse(dt.Rows[n]["IsHand"].ToString());
					}
					model.AcceptUserCode=dt.Rows[n]["AcceptUserCode"].ToString();
					model.HandUserCode=dt.Rows[n]["HandUserCode"].ToString();
					if(dt.Rows[n]["HandDate"].ToString()!="")
					{
						model.HandDate=DateTime.Parse(dt.Rows[n]["HandDate"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["SubmitDate"].ToString()!="")
					{
						model.SubmitDate=DateTime.Parse(dt.Rows[n]["SubmitDate"].ToString());
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

