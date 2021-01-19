using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Equipment
{
	/// <summary>
	/// 数据访问类Dal_Tb_Equipment_Device。
	/// </summary>
	public class Dal_Tb_Equipment_Device
	{
		public Dal_Tb_Equipment_Device()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("DeviceID", "Tb_Equipment_Device"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long DeviceID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DeviceID", SqlDbType.BigInt)};
			parameters[0].Value = DeviceID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Equipment_Device_Exists",parameters,out rowsAffected);
			if(result==1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		///  增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Equipment.Tb_Equipment_Device model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DeviceID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@DeviceTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@DeviceModel", SqlDbType.NVarChar,100),
					new SqlParameter("@SetPlace", SqlDbType.NVarChar,100),
					new SqlParameter("@DeviceOrigin", SqlDbType.NVarChar,100),
					new SqlParameter("@DevicePrice", SqlDbType.NVarChar,100),
					new SqlParameter("@DepreciationRate", SqlDbType.Decimal,9),
					new SqlParameter("@DepreciationPeriod", SqlDbType.Int,4),
					new SqlParameter("@UseLife", SqlDbType.NVarChar,100),
					new SqlParameter("@FactoryTime", SqlDbType.DateTime),
					new SqlParameter("@FactoryNumber", SqlDbType.NVarChar,100),
					new SqlParameter("@ProductionStard", SqlDbType.NVarChar,100),
					new SqlParameter("@UseTime", SqlDbType.DateTime),
					new SqlParameter("@DeviceSign", SqlDbType.NVarChar,100),
					new SqlParameter("@CardNumber", SqlDbType.NVarChar,100),
					new SqlParameter("@Responsible", SqlDbType.NVarChar,100),
					new SqlParameter("@PrepareAccounts", SqlDbType.NVarChar,100),
					new SqlParameter("@PrepareTime", SqlDbType.DateTime),
					new SqlParameter("@Memo", SqlDbType.NVarChar,200),
					new SqlParameter("@MasterOutputPower", SqlDbType.NVarChar,100),
					new SqlParameter("@MasterInputPower", SqlDbType.NVarChar,100),
					new SqlParameter("@MasterGeometry", SqlDbType.NVarChar,100),
					new SqlParameter("@MasterEnergyConsumption", SqlDbType.NVarChar,100),
					new SqlParameter("@MasterOther", SqlDbType.NVarChar,100),
					new SqlParameter("@MotorPower", SqlDbType.NVarChar,100),
					new SqlParameter("@MotorRatedVoltage", SqlDbType.NVarChar,100),
					new SqlParameter("@MotorRatedCurrent", SqlDbType.NVarChar,100),
					new SqlParameter("@MotorOrigin", SqlDbType.NVarChar,100),
					new SqlParameter("@MotorOther", SqlDbType.NVarChar,100),
					new SqlParameter("@ControlGeometry", SqlDbType.NVarChar,100),
					new SqlParameter("@ControlRatedVoltage", SqlDbType.NVarChar,100),
					new SqlParameter("@ControlRatedCurrent", SqlDbType.NVarChar,100),
					new SqlParameter("@ControlOrigin", SqlDbType.NVarChar,100),
					new SqlParameter("@ControlOther", SqlDbType.NVarChar,100),
					new SqlParameter("@FactoryInformation1", SqlDbType.NVarChar,200),
					new SqlParameter("@FactoryInformation2", SqlDbType.NVarChar,200),
					new SqlParameter("@FactoryInformation3", SqlDbType.NVarChar,200),
					new SqlParameter("@TestData1", SqlDbType.NVarChar,200),
					new SqlParameter("@TestData2", SqlDbType.NVarChar,200),
					new SqlParameter("@TestData3", SqlDbType.NVarChar,200),
					new SqlParameter("@ManuName", SqlDbType.NVarChar,50),
					new SqlParameter("@ManuLinkMan", SqlDbType.NVarChar,50),
					new SqlParameter("@ManuLinkTel", SqlDbType.NVarChar,50),
					new SqlParameter("@DealerName", SqlDbType.NVarChar,50),
					new SqlParameter("@DealerLinkMan", SqlDbType.NVarChar,50),
					new SqlParameter("@DealerLinkTel", SqlDbType.NVarChar,50),
					new SqlParameter("@ErectName", SqlDbType.NVarChar,50),
					new SqlParameter("@ErectLinkMan", SqlDbType.NVarChar,50),
					new SqlParameter("@ErectLinkTel", SqlDbType.NVarChar,50),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@Identification", SqlDbType.NVarChar,500),
					new SqlParameter("@GenreID", SqlDbType.BigInt,8),
					new SqlParameter("@GenreUnitID", SqlDbType.BigInt,8),
					new SqlParameter("@GenreLinkTel", SqlDbType.NVarChar,50),
					new SqlParameter("@GenreStartDate", SqlDbType.DateTime),
					new SqlParameter("@GenreCalcBeginDate", SqlDbType.DateTime),
					new SqlParameter("@GenrePeriod", SqlDbType.Int,4),
					new SqlParameter("@GenreTimes", SqlDbType.Int,4),
					new SqlParameter("@FixGenreMonth1", SqlDbType.Int,4),
					new SqlParameter("@FixGenreDay1", SqlDbType.Int,4),
					new SqlParameter("@FixGenreMonth2", SqlDbType.Int,4),
					new SqlParameter("@FixGenreDay2", SqlDbType.Int,4),
					new SqlParameter("@FixGenreMonth3", SqlDbType.Int,4),
					new SqlParameter("@FixGenreDay3", SqlDbType.Int,4),
					new SqlParameter("@FixGenreMonth4", SqlDbType.Int,4),
					new SqlParameter("@FixGenreDay4", SqlDbType.Int,4),
					new SqlParameter("@GenreReason", SqlDbType.NVarChar,200),
					new SqlParameter("@DeviceStatus", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.DeviceID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.DeviceTypeID;
			parameters[3].Value = model.DeviceModel;
			parameters[4].Value = model.SetPlace;
			parameters[5].Value = model.DeviceOrigin;
			parameters[6].Value = model.DevicePrice;
			parameters[7].Value = model.DepreciationRate;
			parameters[8].Value = model.DepreciationPeriod;
			parameters[9].Value = model.UseLife;
			parameters[10].Value = model.FactoryTime;
			parameters[11].Value = model.FactoryNumber;
			parameters[12].Value = model.ProductionStard;
			parameters[13].Value = model.UseTime;
			parameters[14].Value = model.DeviceSign;
			parameters[15].Value = model.CardNumber;
			parameters[16].Value = model.Responsible;
			parameters[17].Value = model.PrepareAccounts;
			parameters[18].Value = model.PrepareTime;
			parameters[19].Value = model.Memo;
			parameters[20].Value = model.MasterOutputPower;
			parameters[21].Value = model.MasterInputPower;
			parameters[22].Value = model.MasterGeometry;
			parameters[23].Value = model.MasterEnergyConsumption;
			parameters[24].Value = model.MasterOther;
			parameters[25].Value = model.MotorPower;
			parameters[26].Value = model.MotorRatedVoltage;
			parameters[27].Value = model.MotorRatedCurrent;
			parameters[28].Value = model.MotorOrigin;
			parameters[29].Value = model.MotorOther;
			parameters[30].Value = model.ControlGeometry;
			parameters[31].Value = model.ControlRatedVoltage;
			parameters[32].Value = model.ControlRatedCurrent;
			parameters[33].Value = model.ControlOrigin;
			parameters[34].Value = model.ControlOther;
			parameters[35].Value = model.FactoryInformation1;
			parameters[36].Value = model.FactoryInformation2;
			parameters[37].Value = model.FactoryInformation3;
			parameters[38].Value = model.TestData1;
			parameters[39].Value = model.TestData2;
			parameters[40].Value = model.TestData3;
			parameters[41].Value = model.ManuName;
			parameters[42].Value = model.ManuLinkMan;
			parameters[43].Value = model.ManuLinkTel;
			parameters[44].Value = model.DealerName;
			parameters[45].Value = model.DealerLinkMan;
			parameters[46].Value = model.DealerLinkTel;
			parameters[47].Value = model.ErectName;
			parameters[48].Value = model.ErectLinkMan;
			parameters[49].Value = model.ErectLinkTel;
			parameters[50].Value = model.IsDelete;
			parameters[51].Value = model.Identification;
			parameters[52].Value = model.GenreID;
			parameters[53].Value = model.GenreUnitID;
			parameters[54].Value = model.GenreLinkTel;
			parameters[55].Value = model.GenreStartDate;
			parameters[56].Value = model.GenreCalcBeginDate;
			parameters[57].Value = model.GenrePeriod;
			parameters[58].Value = model.GenreTimes;
			parameters[59].Value = model.FixGenreMonth1;
			parameters[60].Value = model.FixGenreDay1;
			parameters[61].Value = model.FixGenreMonth2;
			parameters[62].Value = model.FixGenreDay2;
			parameters[63].Value = model.FixGenreMonth3;
			parameters[64].Value = model.FixGenreDay3;
			parameters[65].Value = model.FixGenreMonth4;
			parameters[66].Value = model.FixGenreDay4;
			parameters[67].Value = model.GenreReason;
			parameters[68].Value = model.DeviceStatus;

			DbHelperSQL.RunProcedure("Proc_Tb_Equipment_Device_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Equipment.Tb_Equipment_Device model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DeviceID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@DeviceTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@DeviceModel", SqlDbType.NVarChar,100),
					new SqlParameter("@SetPlace", SqlDbType.NVarChar,100),
					new SqlParameter("@DeviceOrigin", SqlDbType.NVarChar,100),
					new SqlParameter("@DevicePrice", SqlDbType.NVarChar,100),
					new SqlParameter("@DepreciationRate", SqlDbType.Decimal,9),
					new SqlParameter("@DepreciationPeriod", SqlDbType.Int,4),
					new SqlParameter("@UseLife", SqlDbType.NVarChar,100),
					new SqlParameter("@FactoryTime", SqlDbType.DateTime),
					new SqlParameter("@FactoryNumber", SqlDbType.NVarChar,100),
					new SqlParameter("@ProductionStard", SqlDbType.NVarChar,100),
					new SqlParameter("@UseTime", SqlDbType.DateTime),
					new SqlParameter("@DeviceSign", SqlDbType.NVarChar,100),
					new SqlParameter("@CardNumber", SqlDbType.NVarChar,100),
					new SqlParameter("@Responsible", SqlDbType.NVarChar,100),
					new SqlParameter("@PrepareAccounts", SqlDbType.NVarChar,100),
					new SqlParameter("@PrepareTime", SqlDbType.DateTime),
					new SqlParameter("@Memo", SqlDbType.NVarChar,200),
					new SqlParameter("@MasterOutputPower", SqlDbType.NVarChar,100),
					new SqlParameter("@MasterInputPower", SqlDbType.NVarChar,100),
					new SqlParameter("@MasterGeometry", SqlDbType.NVarChar,100),
					new SqlParameter("@MasterEnergyConsumption", SqlDbType.NVarChar,100),
					new SqlParameter("@MasterOther", SqlDbType.NVarChar,100),
					new SqlParameter("@MotorPower", SqlDbType.NVarChar,100),
					new SqlParameter("@MotorRatedVoltage", SqlDbType.NVarChar,100),
					new SqlParameter("@MotorRatedCurrent", SqlDbType.NVarChar,100),
					new SqlParameter("@MotorOrigin", SqlDbType.NVarChar,100),
					new SqlParameter("@MotorOther", SqlDbType.NVarChar,100),
					new SqlParameter("@ControlGeometry", SqlDbType.NVarChar,100),
					new SqlParameter("@ControlRatedVoltage", SqlDbType.NVarChar,100),
					new SqlParameter("@ControlRatedCurrent", SqlDbType.NVarChar,100),
					new SqlParameter("@ControlOrigin", SqlDbType.NVarChar,100),
					new SqlParameter("@ControlOther", SqlDbType.NVarChar,100),
					new SqlParameter("@FactoryInformation1", SqlDbType.NVarChar,200),
					new SqlParameter("@FactoryInformation2", SqlDbType.NVarChar,200),
					new SqlParameter("@FactoryInformation3", SqlDbType.NVarChar,200),
					new SqlParameter("@TestData1", SqlDbType.NVarChar,200),
					new SqlParameter("@TestData2", SqlDbType.NVarChar,200),
					new SqlParameter("@TestData3", SqlDbType.NVarChar,200),
					new SqlParameter("@ManuName", SqlDbType.NVarChar,50),
					new SqlParameter("@ManuLinkMan", SqlDbType.NVarChar,50),
					new SqlParameter("@ManuLinkTel", SqlDbType.NVarChar,50),
					new SqlParameter("@DealerName", SqlDbType.NVarChar,50),
					new SqlParameter("@DealerLinkMan", SqlDbType.NVarChar,50),
					new SqlParameter("@DealerLinkTel", SqlDbType.NVarChar,50),
					new SqlParameter("@ErectName", SqlDbType.NVarChar,50),
					new SqlParameter("@ErectLinkMan", SqlDbType.NVarChar,50),
					new SqlParameter("@ErectLinkTel", SqlDbType.NVarChar,50),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@Identification", SqlDbType.NVarChar,500),
					new SqlParameter("@GenreID", SqlDbType.BigInt,8),
					new SqlParameter("@GenreUnitID", SqlDbType.BigInt,8),
					new SqlParameter("@GenreLinkTel", SqlDbType.NVarChar,50),
					new SqlParameter("@GenreStartDate", SqlDbType.DateTime),
					new SqlParameter("@GenreCalcBeginDate", SqlDbType.DateTime),
					new SqlParameter("@GenrePeriod", SqlDbType.Int,4),
					new SqlParameter("@GenreTimes", SqlDbType.Int,4),
					new SqlParameter("@FixGenreMonth1", SqlDbType.Int,4),
					new SqlParameter("@FixGenreDay1", SqlDbType.Int,4),
					new SqlParameter("@FixGenreMonth2", SqlDbType.Int,4),
					new SqlParameter("@FixGenreDay2", SqlDbType.Int,4),
					new SqlParameter("@FixGenreMonth3", SqlDbType.Int,4),
					new SqlParameter("@FixGenreDay3", SqlDbType.Int,4),
					new SqlParameter("@FixGenreMonth4", SqlDbType.Int,4),
					new SqlParameter("@FixGenreDay4", SqlDbType.Int,4),
					new SqlParameter("@GenreReason", SqlDbType.NVarChar,200),
					new SqlParameter("@DeviceStatus", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.DeviceID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.DeviceTypeID;
			parameters[3].Value = model.DeviceModel;
			parameters[4].Value = model.SetPlace;
			parameters[5].Value = model.DeviceOrigin;
			parameters[6].Value = model.DevicePrice;
			parameters[7].Value = model.DepreciationRate;
			parameters[8].Value = model.DepreciationPeriod;
			parameters[9].Value = model.UseLife;
			parameters[10].Value = model.FactoryTime;
			parameters[11].Value = model.FactoryNumber;
			parameters[12].Value = model.ProductionStard;
			parameters[13].Value = model.UseTime;
			parameters[14].Value = model.DeviceSign;
			parameters[15].Value = model.CardNumber;
			parameters[16].Value = model.Responsible;
			parameters[17].Value = model.PrepareAccounts;
			parameters[18].Value = model.PrepareTime;
			parameters[19].Value = model.Memo;
			parameters[20].Value = model.MasterOutputPower;
			parameters[21].Value = model.MasterInputPower;
			parameters[22].Value = model.MasterGeometry;
			parameters[23].Value = model.MasterEnergyConsumption;
			parameters[24].Value = model.MasterOther;
			parameters[25].Value = model.MotorPower;
			parameters[26].Value = model.MotorRatedVoltage;
			parameters[27].Value = model.MotorRatedCurrent;
			parameters[28].Value = model.MotorOrigin;
			parameters[29].Value = model.MotorOther;
			parameters[30].Value = model.ControlGeometry;
			parameters[31].Value = model.ControlRatedVoltage;
			parameters[32].Value = model.ControlRatedCurrent;
			parameters[33].Value = model.ControlOrigin;
			parameters[34].Value = model.ControlOther;
			parameters[35].Value = model.FactoryInformation1;
			parameters[36].Value = model.FactoryInformation2;
			parameters[37].Value = model.FactoryInformation3;
			parameters[38].Value = model.TestData1;
			parameters[39].Value = model.TestData2;
			parameters[40].Value = model.TestData3;
			parameters[41].Value = model.ManuName;
			parameters[42].Value = model.ManuLinkMan;
			parameters[43].Value = model.ManuLinkTel;
			parameters[44].Value = model.DealerName;
			parameters[45].Value = model.DealerLinkMan;
			parameters[46].Value = model.DealerLinkTel;
			parameters[47].Value = model.ErectName;
			parameters[48].Value = model.ErectLinkMan;
			parameters[49].Value = model.ErectLinkTel;
			parameters[50].Value = model.IsDelete;
			parameters[51].Value = model.Identification;
			parameters[52].Value = model.GenreID;
			parameters[53].Value = model.GenreUnitID;
			parameters[54].Value = model.GenreLinkTel;
			parameters[55].Value = model.GenreStartDate;
			parameters[56].Value = model.GenreCalcBeginDate;
			parameters[57].Value = model.GenrePeriod;
			parameters[58].Value = model.GenreTimes;
			parameters[59].Value = model.FixGenreMonth1;
			parameters[60].Value = model.FixGenreDay1;
			parameters[61].Value = model.FixGenreMonth2;
			parameters[62].Value = model.FixGenreDay2;
			parameters[63].Value = model.FixGenreMonth3;
			parameters[64].Value = model.FixGenreDay3;
			parameters[65].Value = model.FixGenreMonth4;
			parameters[66].Value = model.FixGenreDay4;
			parameters[67].Value = model.GenreReason;
			parameters[68].Value = model.DeviceStatus;

			DbHelperSQL.RunProcedure("Proc_Tb_Equipment_Device_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long DeviceID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DeviceID", SqlDbType.BigInt)};
			parameters[0].Value = DeviceID;

			DbHelperSQL.RunProcedure("Proc_Tb_Equipment_Device_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Equipment.Tb_Equipment_Device GetModel(long DeviceID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@DeviceID", SqlDbType.BigInt)};
			parameters[0].Value = DeviceID;

			MobileSoft.Model.Equipment.Tb_Equipment_Device model=new MobileSoft.Model.Equipment.Tb_Equipment_Device();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Equipment_Device_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["DeviceID"].ToString()!="")
				{
					model.DeviceID=long.Parse(ds.Tables[0].Rows[0]["DeviceID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DeviceTypeID"].ToString()!="")
				{
					model.DeviceTypeID=long.Parse(ds.Tables[0].Rows[0]["DeviceTypeID"].ToString());
				}
				model.DeviceModel=ds.Tables[0].Rows[0]["DeviceModel"].ToString();
				model.SetPlace=ds.Tables[0].Rows[0]["SetPlace"].ToString();
				model.DeviceOrigin=ds.Tables[0].Rows[0]["DeviceOrigin"].ToString();
				model.DevicePrice=ds.Tables[0].Rows[0]["DevicePrice"].ToString();
				if(ds.Tables[0].Rows[0]["DepreciationRate"].ToString()!="")
				{
					model.DepreciationRate=decimal.Parse(ds.Tables[0].Rows[0]["DepreciationRate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DepreciationPeriod"].ToString()!="")
				{
					model.DepreciationPeriod=int.Parse(ds.Tables[0].Rows[0]["DepreciationPeriod"].ToString());
				}
				model.UseLife=ds.Tables[0].Rows[0]["UseLife"].ToString();
				if(ds.Tables[0].Rows[0]["FactoryTime"].ToString()!="")
				{
					model.FactoryTime=DateTime.Parse(ds.Tables[0].Rows[0]["FactoryTime"].ToString());
				}
				model.FactoryNumber=ds.Tables[0].Rows[0]["FactoryNumber"].ToString();
				model.ProductionStard=ds.Tables[0].Rows[0]["ProductionStard"].ToString();
				if(ds.Tables[0].Rows[0]["UseTime"].ToString()!="")
				{
					model.UseTime=DateTime.Parse(ds.Tables[0].Rows[0]["UseTime"].ToString());
				}
				model.DeviceSign=ds.Tables[0].Rows[0]["DeviceSign"].ToString();
				model.CardNumber=ds.Tables[0].Rows[0]["CardNumber"].ToString();
				model.Responsible=ds.Tables[0].Rows[0]["Responsible"].ToString();
				model.PrepareAccounts=ds.Tables[0].Rows[0]["PrepareAccounts"].ToString();
				if(ds.Tables[0].Rows[0]["PrepareTime"].ToString()!="")
				{
					model.PrepareTime=DateTime.Parse(ds.Tables[0].Rows[0]["PrepareTime"].ToString());
				}
				model.Memo=ds.Tables[0].Rows[0]["Memo"].ToString();
				model.MasterOutputPower=ds.Tables[0].Rows[0]["MasterOutputPower"].ToString();
				model.MasterInputPower=ds.Tables[0].Rows[0]["MasterInputPower"].ToString();
				model.MasterGeometry=ds.Tables[0].Rows[0]["MasterGeometry"].ToString();
				model.MasterEnergyConsumption=ds.Tables[0].Rows[0]["MasterEnergyConsumption"].ToString();
				model.MasterOther=ds.Tables[0].Rows[0]["MasterOther"].ToString();
				model.MotorPower=ds.Tables[0].Rows[0]["MotorPower"].ToString();
				model.MotorRatedVoltage=ds.Tables[0].Rows[0]["MotorRatedVoltage"].ToString();
				model.MotorRatedCurrent=ds.Tables[0].Rows[0]["MotorRatedCurrent"].ToString();
				model.MotorOrigin=ds.Tables[0].Rows[0]["MotorOrigin"].ToString();
				model.MotorOther=ds.Tables[0].Rows[0]["MotorOther"].ToString();
				model.ControlGeometry=ds.Tables[0].Rows[0]["ControlGeometry"].ToString();
				model.ControlRatedVoltage=ds.Tables[0].Rows[0]["ControlRatedVoltage"].ToString();
				model.ControlRatedCurrent=ds.Tables[0].Rows[0]["ControlRatedCurrent"].ToString();
				model.ControlOrigin=ds.Tables[0].Rows[0]["ControlOrigin"].ToString();
				model.ControlOther=ds.Tables[0].Rows[0]["ControlOther"].ToString();
				model.FactoryInformation1=ds.Tables[0].Rows[0]["FactoryInformation1"].ToString();
				model.FactoryInformation2=ds.Tables[0].Rows[0]["FactoryInformation2"].ToString();
				model.FactoryInformation3=ds.Tables[0].Rows[0]["FactoryInformation3"].ToString();
				model.TestData1=ds.Tables[0].Rows[0]["TestData1"].ToString();
				model.TestData2=ds.Tables[0].Rows[0]["TestData2"].ToString();
				model.TestData3=ds.Tables[0].Rows[0]["TestData3"].ToString();
				model.ManuName=ds.Tables[0].Rows[0]["ManuName"].ToString();
				model.ManuLinkMan=ds.Tables[0].Rows[0]["ManuLinkMan"].ToString();
				model.ManuLinkTel=ds.Tables[0].Rows[0]["ManuLinkTel"].ToString();
				model.DealerName=ds.Tables[0].Rows[0]["DealerName"].ToString();
				model.DealerLinkMan=ds.Tables[0].Rows[0]["DealerLinkMan"].ToString();
				model.DealerLinkTel=ds.Tables[0].Rows[0]["DealerLinkTel"].ToString();
				model.ErectName=ds.Tables[0].Rows[0]["ErectName"].ToString();
				model.ErectLinkMan=ds.Tables[0].Rows[0]["ErectLinkMan"].ToString();
				model.ErectLinkTel=ds.Tables[0].Rows[0]["ErectLinkTel"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				model.Identification=ds.Tables[0].Rows[0]["Identification"].ToString();
				if(ds.Tables[0].Rows[0]["GenreID"].ToString()!="")
				{
					model.GenreID=long.Parse(ds.Tables[0].Rows[0]["GenreID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GenreUnitID"].ToString()!="")
				{
					model.GenreUnitID=long.Parse(ds.Tables[0].Rows[0]["GenreUnitID"].ToString());
				}
				model.GenreLinkTel=ds.Tables[0].Rows[0]["GenreLinkTel"].ToString();
				if(ds.Tables[0].Rows[0]["GenreStartDate"].ToString()!="")
				{
					model.GenreStartDate=DateTime.Parse(ds.Tables[0].Rows[0]["GenreStartDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GenreCalcBeginDate"].ToString()!="")
				{
					model.GenreCalcBeginDate=DateTime.Parse(ds.Tables[0].Rows[0]["GenreCalcBeginDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GenrePeriod"].ToString()!="")
				{
					model.GenrePeriod=int.Parse(ds.Tables[0].Rows[0]["GenrePeriod"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GenreTimes"].ToString()!="")
				{
					model.GenreTimes=int.Parse(ds.Tables[0].Rows[0]["GenreTimes"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FixGenreMonth1"].ToString()!="")
				{
					model.FixGenreMonth1=int.Parse(ds.Tables[0].Rows[0]["FixGenreMonth1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FixGenreDay1"].ToString()!="")
				{
					model.FixGenreDay1=int.Parse(ds.Tables[0].Rows[0]["FixGenreDay1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FixGenreMonth2"].ToString()!="")
				{
					model.FixGenreMonth2=int.Parse(ds.Tables[0].Rows[0]["FixGenreMonth2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FixGenreDay2"].ToString()!="")
				{
					model.FixGenreDay2=int.Parse(ds.Tables[0].Rows[0]["FixGenreDay2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FixGenreMonth3"].ToString()!="")
				{
					model.FixGenreMonth3=int.Parse(ds.Tables[0].Rows[0]["FixGenreMonth3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FixGenreDay3"].ToString()!="")
				{
					model.FixGenreDay3=int.Parse(ds.Tables[0].Rows[0]["FixGenreDay3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FixGenreMonth4"].ToString()!="")
				{
					model.FixGenreMonth4=int.Parse(ds.Tables[0].Rows[0]["FixGenreMonth4"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FixGenreDay4"].ToString()!="")
				{
					model.FixGenreDay4=int.Parse(ds.Tables[0].Rows[0]["FixGenreDay4"].ToString());
				}
				model.GenreReason=ds.Tables[0].Rows[0]["GenreReason"].ToString();
				if(ds.Tables[0].Rows[0]["DeviceStatus"].ToString()!="")
				{
					model.DeviceStatus=int.Parse(ds.Tables[0].Rows[0]["DeviceStatus"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
            strSql.Append(" FROM view_Equipment_Device_Filter ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string fieldOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
            strSql.Append(" FROM view_Equipment_Device_Filter ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + fieldOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize,string SortField,int Sort)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@FldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@FldSort", SqlDbType.VarChar, 1000),
					new SqlParameter("@Sort", SqlDbType.Int),
					new SqlParameter("@StrCondition", SqlDbType.VarChar, 8000),
					new SqlParameter("@Id", SqlDbType.VarChar, 50),
					new SqlParameter("@PageCount", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
					new SqlParameter("@Counts", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
					};
			parameters[0].Value = "*";
			parameters[1].Value = PageSize;
			parameters[2].Value = PageIndex;
			parameters[3].Value = SortField;
			parameters[4].Value = Sort;
            parameters[5].Value = "SELECT * FROM view_Equipment_Device_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "DeviceID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

