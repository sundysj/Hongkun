using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Sys;
namespace MobileSoft.BLL.Sys
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Sys_TakePic 的摘要说明。
	/// </summary>
	public class Bll_Tb_Sys_TakePic
	{
		private readonly MobileSoft.DAL.Sys.Dal_Tb_Sys_TakePic dal=new MobileSoft.DAL.Sys.Dal_Tb_Sys_TakePic();
		public Bll_Tb_Sys_TakePic()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long StatID)
		{
			return dal.Exists(StatID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MobileSoft.Model.Sys.Tb_Sys_TakePic model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_TakePic model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long StatID)
		{
			
			dal.Delete(StatID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePic GetModel(long StatID)
		{
			
			return dal.GetModel(StatID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePic GetModelByCache(long StatID)
		{
			
			string CacheKey = "Tb_Sys_TakePicModel-" + StatID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(StatID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Sys.Tb_Sys_TakePic)objModel;
		}

        public DataSet GetListFromProc(string strWhere)
        {
            return dal.GetListFromProc(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
        /// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere,string Files)
        {
            return dal.GetList(strWhere,Files);
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
		public List<MobileSoft.Model.Sys.Tb_Sys_TakePic> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Sys.Tb_Sys_TakePic> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Sys.Tb_Sys_TakePic> modelList = new List<MobileSoft.Model.Sys.Tb_Sys_TakePic>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Sys.Tb_Sys_TakePic model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Sys.Tb_Sys_TakePic();
					//model.StatID=dt.Rows[n]["StatID"].ToString();
					if(dt.Rows[n]["StatType"].ToString()!="")
					{
						model.StatType=int.Parse(dt.Rows[n]["StatType"].ToString());
					}
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.OrganCode=dt.Rows[n]["OrganCode"].ToString();
					if(dt.Rows[n]["StatDate"].ToString()!="")
					{
						model.StatDate=DateTime.Parse(dt.Rows[n]["StatDate"].ToString());
					}
					if(dt.Rows[n]["AllArea"].ToString()!="")
					{
						model.AllArea=decimal.Parse(dt.Rows[n]["AllArea"].ToString());
					}
					if(dt.Rows[n]["AllRoomNum"].ToString()!="")
					{
						model.AllRoomNum=int.Parse(dt.Rows[n]["AllRoomNum"].ToString());
					}
					if(dt.Rows[n]["RoomNum1"].ToString()!="")
					{
						model.RoomNum1=int.Parse(dt.Rows[n]["RoomNum1"].ToString());
					}
					if(dt.Rows[n]["RoomNum2"].ToString()!="")
					{
						model.RoomNum2=int.Parse(dt.Rows[n]["RoomNum2"].ToString());
					}
					if(dt.Rows[n]["RoomNum3"].ToString()!="")
					{
						model.RoomNum3=int.Parse(dt.Rows[n]["RoomNum3"].ToString());
					}
					if(dt.Rows[n]["RoomNum4"].ToString()!="")
					{
						model.RoomNum4=int.Parse(dt.Rows[n]["RoomNum4"].ToString());
					}
					if(dt.Rows[n]["RoomNum5"].ToString()!="")
					{
						model.RoomNum5=int.Parse(dt.Rows[n]["RoomNum5"].ToString());
					}
					if(dt.Rows[n]["RoomNum6"].ToString()!="")
					{
						model.RoomNum6=int.Parse(dt.Rows[n]["RoomNum6"].ToString());
					}
					if(dt.Rows[n]["ChargeRate1"].ToString()!="")
					{
						model.ChargeRate1=decimal.Parse(dt.Rows[n]["ChargeRate1"].ToString());
					}
					if(dt.Rows[n]["ChargeRate2"].ToString()!="")
					{
						model.ChargeRate2=decimal.Parse(dt.Rows[n]["ChargeRate2"].ToString());
					}
					if(dt.Rows[n]["ChargeRate3"].ToString()!="")
					{
						model.ChargeRate3=decimal.Parse(dt.Rows[n]["ChargeRate3"].ToString());
					}
					if(dt.Rows[n]["ChargeRate4"].ToString()!="")
					{
						model.ChargeRate4=decimal.Parse(dt.Rows[n]["ChargeRate4"].ToString());
					}
					if(dt.Rows[n]["FeesCreateNum"].ToString()!="")
					{
						model.FeesCreateNum=int.Parse(dt.Rows[n]["FeesCreateNum"].ToString());
					}
					if(dt.Rows[n]["FeesCancelNum"].ToString()!="")
					{
						model.FeesCancelNum=int.Parse(dt.Rows[n]["FeesCancelNum"].ToString());
					}
					if(dt.Rows[n]["IncidentNum1"].ToString()!="")
					{
						model.IncidentNum1=int.Parse(dt.Rows[n]["IncidentNum1"].ToString());
					}
					if(dt.Rows[n]["IncidentNum2"].ToString()!="")
					{
						model.IncidentNum2=int.Parse(dt.Rows[n]["IncidentNum2"].ToString());
					}
					if(dt.Rows[n]["IncidentNum3"].ToString()!="")
					{
						model.IncidentNum3=int.Parse(dt.Rows[n]["IncidentNum3"].ToString());
					}
					if(dt.Rows[n]["IncidentNum4"].ToString()!="")
					{
						model.IncidentNum4=int.Parse(dt.Rows[n]["IncidentNum4"].ToString());
					}
					if(dt.Rows[n]["IncidentNum5"].ToString()!="")
					{
						model.IncidentNum5=int.Parse(dt.Rows[n]["IncidentNum5"].ToString());
					}
					if(dt.Rows[n]["IncidentRate1"].ToString()!="")
					{
						model.IncidentRate1=decimal.Parse(dt.Rows[n]["IncidentRate1"].ToString());
					}
					if(dt.Rows[n]["IncidentRate2"].ToString()!="")
					{
						model.IncidentRate2=decimal.Parse(dt.Rows[n]["IncidentRate2"].ToString());
					}
					if(dt.Rows[n]["IncidentRate3"].ToString()!="")
					{
						model.IncidentRate3=decimal.Parse(dt.Rows[n]["IncidentRate3"].ToString());
					}
					if(dt.Rows[n]["IncidentRate4"].ToString()!="")
					{
						model.IncidentRate4=decimal.Parse(dt.Rows[n]["IncidentRate4"].ToString());
					}
					if(dt.Rows[n]["MeterChargeRate1"].ToString()!="")
					{
						model.MeterChargeRate1=decimal.Parse(dt.Rows[n]["MeterChargeRate1"].ToString());
					}
					if(dt.Rows[n]["MeterChargeRate2"].ToString()!="")
					{
						model.MeterChargeRate2=decimal.Parse(dt.Rows[n]["MeterChargeRate2"].ToString());
					}
					if(dt.Rows[n]["MeterChargeRate3"].ToString()!="")
					{
						model.MeterChargeRate3=decimal.Parse(dt.Rows[n]["MeterChargeRate3"].ToString());
					}
					if(dt.Rows[n]["MeterChargeRate4"].ToString()!="")
					{
						model.MeterChargeRate4=decimal.Parse(dt.Rows[n]["MeterChargeRate4"].ToString());
					}
					if(dt.Rows[n]["CurFeesRate_Deno"].ToString()!="")
					{
						model.CurFeesRate_Deno=decimal.Parse(dt.Rows[n]["CurFeesRate_Deno"].ToString());
					}
					if(dt.Rows[n]["CurFeesRate_Mole"].ToString()!="")
					{
						model.CurFeesRate_Mole=decimal.Parse(dt.Rows[n]["CurFeesRate_Mole"].ToString());
					}
					if(dt.Rows[n]["CurYearFeesRate_Deno"].ToString()!="")
					{
						model.CurYearFeesRate_Deno=decimal.Parse(dt.Rows[n]["CurYearFeesRate_Deno"].ToString());
					}
					if(dt.Rows[n]["CurYearFeesRate_Mole"].ToString()!="")
					{
						model.CurYearFeesRate_Mole=decimal.Parse(dt.Rows[n]["CurYearFeesRate_Mole"].ToString());
					}
					if(dt.Rows[n]["BefLastFeesRate_Deno"].ToString()!="")
					{
						model.BefLastFeesRate_Deno=decimal.Parse(dt.Rows[n]["BefLastFeesRate_Deno"].ToString());
					}
					if(dt.Rows[n]["BefLastFeesRate_Mole"].ToString()!="")
					{
						model.BefLastFeesRate_Mole=decimal.Parse(dt.Rows[n]["BefLastFeesRate_Mole"].ToString());
					}
					if(dt.Rows[n]["CurMeterRate_Deno"].ToString()!="")
					{
						model.CurMeterRate_Deno=decimal.Parse(dt.Rows[n]["CurMeterRate_Deno"].ToString());
					}
					if(dt.Rows[n]["CurMeterRate_Mole"].ToString()!="")
					{
						model.CurMeterRate_Mole=decimal.Parse(dt.Rows[n]["CurMeterRate_Mole"].ToString());
					}
					if(dt.Rows[n]["CurYearMeterRate_Deno"].ToString()!="")
					{
						model.CurYearMeterRate_Deno=decimal.Parse(dt.Rows[n]["CurYearMeterRate_Deno"].ToString());
					}
					if(dt.Rows[n]["CurYearMeterRate_Mole"].ToString()!="")
					{
						model.CurYearMeterRate_Mole=decimal.Parse(dt.Rows[n]["CurYearMeterRate_Mole"].ToString());
					}
					if(dt.Rows[n]["BefLastMeterRate_Deno"].ToString()!="")
					{
						model.BefLastMeterRate_Deno=decimal.Parse(dt.Rows[n]["BefLastMeterRate_Deno"].ToString());
					}
					if(dt.Rows[n]["BefLastMeterRate_Mole"].ToString()!="")
					{
						model.BefLastMeterRate_Mole=decimal.Parse(dt.Rows[n]["BefLastMeterRate_Mole"].ToString());
					}
					if(dt.Rows[n]["IncidFinlishMonthNum1"].ToString()!="")
					{
						model.IncidFinlishMonthNum1=int.Parse(dt.Rows[n]["IncidFinlishMonthNum1"].ToString());
					}
					if(dt.Rows[n]["IncidHappenMonthNum1"].ToString()!="")
					{
						model.IncidHappenMonthNum1=int.Parse(dt.Rows[n]["IncidHappenMonthNum1"].ToString());
					}
					if(dt.Rows[n]["IncidFinlishYearNum1"].ToString()!="")
					{
						model.IncidFinlishYearNum1=int.Parse(dt.Rows[n]["IncidFinlishYearNum1"].ToString());
					}
					if(dt.Rows[n]["IncidHappenYearNum1"].ToString()!="")
					{
						model.IncidHappenYearNum1=int.Parse(dt.Rows[n]["IncidHappenYearNum1"].ToString());
					}
					if(dt.Rows[n]["IncidFinlishMonthNum2"].ToString()!="")
					{
						model.IncidFinlishMonthNum2=int.Parse(dt.Rows[n]["IncidFinlishMonthNum2"].ToString());
					}
					if(dt.Rows[n]["IncidHappenMonthNum2"].ToString()!="")
					{
						model.IncidHappenMonthNum2=int.Parse(dt.Rows[n]["IncidHappenMonthNum2"].ToString());
					}
					if(dt.Rows[n]["IncidFinlishYearNum2"].ToString()!="")
					{
						model.IncidFinlishYearNum2=int.Parse(dt.Rows[n]["IncidFinlishYearNum2"].ToString());
					}
					if(dt.Rows[n]["IncidHappenYearNum2"].ToString()!="")
					{
						model.IncidHappenYearNum2=int.Parse(dt.Rows[n]["IncidHappenYearNum2"].ToString());
					}
					if(dt.Rows[n]["EquipmentDeviceCount"].ToString()!="")
					{
						model.EquipmentDeviceCount=int.Parse(dt.Rows[n]["EquipmentDeviceCount"].ToString());
					}
					if(dt.Rows[n]["EquipmentPeriodHappenNum"].ToString()!="")
					{
						model.EquipmentPeriodHappenNum=int.Parse(dt.Rows[n]["EquipmentPeriodHappenNum"].ToString());
					}
					if(dt.Rows[n]["EquipmentPeriodFinlishNum"].ToString()!="")
					{
						model.EquipmentPeriodFinlishNum=int.Parse(dt.Rows[n]["EquipmentPeriodFinlishNum"].ToString());
					}
					if(dt.Rows[n]["EquipmentPeriodUnFinlishNum"].ToString()!="")
					{
						model.EquipmentPeriodUnFinlishNum=int.Parse(dt.Rows[n]["EquipmentPeriodUnFinlishNum"].ToString());
					}
					if(dt.Rows[n]["EquipmentTempHappenNum"].ToString()!="")
					{
						model.EquipmentTempHappenNum=int.Parse(dt.Rows[n]["EquipmentTempHappenNum"].ToString());
					}
					if(dt.Rows[n]["EquipmentTempFinlishNum"].ToString()!="")
					{
						model.EquipmentTempFinlishNum=int.Parse(dt.Rows[n]["EquipmentTempFinlishNum"].ToString());
					}
					if(dt.Rows[n]["EquipmentTempUnFinlishNum"].ToString()!="")
					{
						model.EquipmentTempUnFinlishNum=int.Parse(dt.Rows[n]["EquipmentTempUnFinlishNum"].ToString());
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

        public DataTable Sys_TakePic_FilterTopNum(int TopNum, string SQLEx)
        {
            return dal.Sys_TakePic_FilterTopNum(TopNum, SQLEx);
        }
	}
}

