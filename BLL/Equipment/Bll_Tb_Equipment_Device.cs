using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Equipment;
namespace MobileSoft.BLL.Equipment
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Equipment_Device 的摘要说明。
	/// </summary>
	public class Bll_Tb_Equipment_Device
	{
		private readonly MobileSoft.DAL.Equipment.Dal_Tb_Equipment_Device dal=new MobileSoft.DAL.Equipment.Dal_Tb_Equipment_Device();
		public Bll_Tb_Equipment_Device()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long DeviceID)
		{
			return dal.Exists(DeviceID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Equipment.Tb_Equipment_Device model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Equipment.Tb_Equipment_Device model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long DeviceID)
		{
			
			dal.Delete(DeviceID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Equipment.Tb_Equipment_Device GetModel(long DeviceID)
		{
			
			return dal.GetModel(DeviceID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Equipment.Tb_Equipment_Device GetModelByCache(long DeviceID)
		{
			
			string CacheKey = "Tb_Equipment_DeviceModel-" + DeviceID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(DeviceID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Equipment.Tb_Equipment_Device)objModel;
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
		public List<MobileSoft.Model.Equipment.Tb_Equipment_Device> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Equipment.Tb_Equipment_Device> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Equipment.Tb_Equipment_Device> modelList = new List<MobileSoft.Model.Equipment.Tb_Equipment_Device>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Equipment.Tb_Equipment_Device model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Equipment.Tb_Equipment_Device();
					//model.DeviceID=dt.Rows[n]["DeviceID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					//model.DeviceTypeID=dt.Rows[n]["DeviceTypeID"].ToString();
					model.DeviceModel=dt.Rows[n]["DeviceModel"].ToString();
					model.SetPlace=dt.Rows[n]["SetPlace"].ToString();
					model.DeviceOrigin=dt.Rows[n]["DeviceOrigin"].ToString();
					model.DevicePrice=dt.Rows[n]["DevicePrice"].ToString();
					if(dt.Rows[n]["DepreciationRate"].ToString()!="")
					{
						model.DepreciationRate=decimal.Parse(dt.Rows[n]["DepreciationRate"].ToString());
					}
					if(dt.Rows[n]["DepreciationPeriod"].ToString()!="")
					{
						model.DepreciationPeriod=int.Parse(dt.Rows[n]["DepreciationPeriod"].ToString());
					}
					model.UseLife=dt.Rows[n]["UseLife"].ToString();
					if(dt.Rows[n]["FactoryTime"].ToString()!="")
					{
						model.FactoryTime=DateTime.Parse(dt.Rows[n]["FactoryTime"].ToString());
					}
					model.FactoryNumber=dt.Rows[n]["FactoryNumber"].ToString();
					model.ProductionStard=dt.Rows[n]["ProductionStard"].ToString();
					if(dt.Rows[n]["UseTime"].ToString()!="")
					{
						model.UseTime=DateTime.Parse(dt.Rows[n]["UseTime"].ToString());
					}
					model.DeviceSign=dt.Rows[n]["DeviceSign"].ToString();
					model.CardNumber=dt.Rows[n]["CardNumber"].ToString();
					model.Responsible=dt.Rows[n]["Responsible"].ToString();
					model.PrepareAccounts=dt.Rows[n]["PrepareAccounts"].ToString();
					if(dt.Rows[n]["PrepareTime"].ToString()!="")
					{
						model.PrepareTime=DateTime.Parse(dt.Rows[n]["PrepareTime"].ToString());
					}
					model.Memo=dt.Rows[n]["Memo"].ToString();
					model.MasterOutputPower=dt.Rows[n]["MasterOutputPower"].ToString();
					model.MasterInputPower=dt.Rows[n]["MasterInputPower"].ToString();
					model.MasterGeometry=dt.Rows[n]["MasterGeometry"].ToString();
					model.MasterEnergyConsumption=dt.Rows[n]["MasterEnergyConsumption"].ToString();
					model.MasterOther=dt.Rows[n]["MasterOther"].ToString();
					model.MotorPower=dt.Rows[n]["MotorPower"].ToString();
					model.MotorRatedVoltage=dt.Rows[n]["MotorRatedVoltage"].ToString();
					model.MotorRatedCurrent=dt.Rows[n]["MotorRatedCurrent"].ToString();
					model.MotorOrigin=dt.Rows[n]["MotorOrigin"].ToString();
					model.MotorOther=dt.Rows[n]["MotorOther"].ToString();
					model.ControlGeometry=dt.Rows[n]["ControlGeometry"].ToString();
					model.ControlRatedVoltage=dt.Rows[n]["ControlRatedVoltage"].ToString();
					model.ControlRatedCurrent=dt.Rows[n]["ControlRatedCurrent"].ToString();
					model.ControlOrigin=dt.Rows[n]["ControlOrigin"].ToString();
					model.ControlOther=dt.Rows[n]["ControlOther"].ToString();
					model.FactoryInformation1=dt.Rows[n]["FactoryInformation1"].ToString();
					model.FactoryInformation2=dt.Rows[n]["FactoryInformation2"].ToString();
					model.FactoryInformation3=dt.Rows[n]["FactoryInformation3"].ToString();
					model.TestData1=dt.Rows[n]["TestData1"].ToString();
					model.TestData2=dt.Rows[n]["TestData2"].ToString();
					model.TestData3=dt.Rows[n]["TestData3"].ToString();
					model.ManuName=dt.Rows[n]["ManuName"].ToString();
					model.ManuLinkMan=dt.Rows[n]["ManuLinkMan"].ToString();
					model.ManuLinkTel=dt.Rows[n]["ManuLinkTel"].ToString();
					model.DealerName=dt.Rows[n]["DealerName"].ToString();
					model.DealerLinkMan=dt.Rows[n]["DealerLinkMan"].ToString();
					model.DealerLinkTel=dt.Rows[n]["DealerLinkTel"].ToString();
					model.ErectName=dt.Rows[n]["ErectName"].ToString();
					model.ErectLinkMan=dt.Rows[n]["ErectLinkMan"].ToString();
					model.ErectLinkTel=dt.Rows[n]["ErectLinkTel"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					model.Identification=dt.Rows[n]["Identification"].ToString();
					//model.GenreID=dt.Rows[n]["GenreID"].ToString();
					//model.GenreUnitID=dt.Rows[n]["GenreUnitID"].ToString();
					model.GenreLinkTel=dt.Rows[n]["GenreLinkTel"].ToString();
					if(dt.Rows[n]["GenreStartDate"].ToString()!="")
					{
						model.GenreStartDate=DateTime.Parse(dt.Rows[n]["GenreStartDate"].ToString());
					}
					if(dt.Rows[n]["GenreCalcBeginDate"].ToString()!="")
					{
						model.GenreCalcBeginDate=DateTime.Parse(dt.Rows[n]["GenreCalcBeginDate"].ToString());
					}
					if(dt.Rows[n]["GenrePeriod"].ToString()!="")
					{
						model.GenrePeriod=int.Parse(dt.Rows[n]["GenrePeriod"].ToString());
					}
					if(dt.Rows[n]["GenreTimes"].ToString()!="")
					{
						model.GenreTimes=int.Parse(dt.Rows[n]["GenreTimes"].ToString());
					}
					if(dt.Rows[n]["FixGenreMonth1"].ToString()!="")
					{
						model.FixGenreMonth1=int.Parse(dt.Rows[n]["FixGenreMonth1"].ToString());
					}
					if(dt.Rows[n]["FixGenreDay1"].ToString()!="")
					{
						model.FixGenreDay1=int.Parse(dt.Rows[n]["FixGenreDay1"].ToString());
					}
					if(dt.Rows[n]["FixGenreMonth2"].ToString()!="")
					{
						model.FixGenreMonth2=int.Parse(dt.Rows[n]["FixGenreMonth2"].ToString());
					}
					if(dt.Rows[n]["FixGenreDay2"].ToString()!="")
					{
						model.FixGenreDay2=int.Parse(dt.Rows[n]["FixGenreDay2"].ToString());
					}
					if(dt.Rows[n]["FixGenreMonth3"].ToString()!="")
					{
						model.FixGenreMonth3=int.Parse(dt.Rows[n]["FixGenreMonth3"].ToString());
					}
					if(dt.Rows[n]["FixGenreDay3"].ToString()!="")
					{
						model.FixGenreDay3=int.Parse(dt.Rows[n]["FixGenreDay3"].ToString());
					}
					if(dt.Rows[n]["FixGenreMonth4"].ToString()!="")
					{
						model.FixGenreMonth4=int.Parse(dt.Rows[n]["FixGenreMonth4"].ToString());
					}
					if(dt.Rows[n]["FixGenreDay4"].ToString()!="")
					{
						model.FixGenreDay4=int.Parse(dt.Rows[n]["FixGenreDay4"].ToString());
					}
					model.GenreReason=dt.Rows[n]["GenreReason"].ToString();
					if(dt.Rows[n]["DeviceStatus"].ToString()!="")
					{
						model.DeviceStatus=int.Parse(dt.Rows[n]["DeviceStatus"].ToString());
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

