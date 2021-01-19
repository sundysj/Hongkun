using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Sys
{
	/// <summary>
	/// 数据访问类Dal_Tb_Sys_TakePic。
	/// </summary>
	public class Dal_Tb_Sys_TakePic
	{
		public Dal_Tb_Sys_TakePic()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long StatID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt)};
			parameters[0].Value = StatID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePic_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.Sys.Tb_Sys_TakePic model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt,8),
					new SqlParameter("@StatType", SqlDbType.SmallInt,2),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@StatDate", SqlDbType.DateTime),
					new SqlParameter("@AllArea", SqlDbType.Decimal,9),
					new SqlParameter("@AllRoomNum", SqlDbType.Int,4),
					new SqlParameter("@RoomNum1", SqlDbType.Int,4),
					new SqlParameter("@RoomNum2", SqlDbType.Int,4),
					new SqlParameter("@RoomNum3", SqlDbType.Int,4),
					new SqlParameter("@RoomNum4", SqlDbType.Int,4),
					new SqlParameter("@RoomNum5", SqlDbType.Int,4),
					new SqlParameter("@RoomNum6", SqlDbType.Int,4),
					new SqlParameter("@ChargeRate1", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRate2", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRate3", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRate4", SqlDbType.Decimal,9),
					new SqlParameter("@FeesCreateNum", SqlDbType.Int,4),
					new SqlParameter("@FeesCancelNum", SqlDbType.Int,4),
					new SqlParameter("@IncidentNum1", SqlDbType.Int,4),
					new SqlParameter("@IncidentNum2", SqlDbType.Int,4),
					new SqlParameter("@IncidentNum3", SqlDbType.Int,4),
					new SqlParameter("@IncidentNum4", SqlDbType.Int,4),
					new SqlParameter("@IncidentNum5", SqlDbType.Int,4),
					new SqlParameter("@IncidentRate1", SqlDbType.Decimal,9),
					new SqlParameter("@IncidentRate2", SqlDbType.Decimal,9),
					new SqlParameter("@IncidentRate3", SqlDbType.Decimal,9),
					new SqlParameter("@IncidentRate4", SqlDbType.Decimal,9),
					new SqlParameter("@MeterChargeRate1", SqlDbType.Decimal,9),
					new SqlParameter("@MeterChargeRate2", SqlDbType.Decimal,9),
					new SqlParameter("@MeterChargeRate3", SqlDbType.Decimal,9),
					new SqlParameter("@MeterChargeRate4", SqlDbType.Decimal,9),
					new SqlParameter("@CurFeesRate_Deno", SqlDbType.Decimal,9),
					new SqlParameter("@CurFeesRate_Mole", SqlDbType.Decimal,9),
					new SqlParameter("@CurYearFeesRate_Deno", SqlDbType.Decimal,9),
					new SqlParameter("@CurYearFeesRate_Mole", SqlDbType.Decimal,9),
					new SqlParameter("@BefLastFeesRate_Deno", SqlDbType.Decimal,9),
					new SqlParameter("@BefLastFeesRate_Mole", SqlDbType.Decimal,9),
					new SqlParameter("@CurMeterRate_Deno", SqlDbType.Decimal,9),
					new SqlParameter("@CurMeterRate_Mole", SqlDbType.Decimal,9),
					new SqlParameter("@CurYearMeterRate_Deno", SqlDbType.Decimal,9),
					new SqlParameter("@CurYearMeterRate_Mole", SqlDbType.Decimal,9),
					new SqlParameter("@BefLastMeterRate_Deno", SqlDbType.Decimal,9),
					new SqlParameter("@BefLastMeterRate_Mole", SqlDbType.Decimal,9),
					new SqlParameter("@IncidFinlishMonthNum1", SqlDbType.Int,4),
					new SqlParameter("@IncidHappenMonthNum1", SqlDbType.Int,4),
					new SqlParameter("@IncidFinlishYearNum1", SqlDbType.Int,4),
					new SqlParameter("@IncidHappenYearNum1", SqlDbType.Int,4),
					new SqlParameter("@IncidFinlishMonthNum2", SqlDbType.Int,4),
					new SqlParameter("@IncidHappenMonthNum2", SqlDbType.Int,4),
					new SqlParameter("@IncidFinlishYearNum2", SqlDbType.Int,4),
					new SqlParameter("@IncidHappenYearNum2", SqlDbType.Int,4),
					new SqlParameter("@EquipmentDeviceCount", SqlDbType.Int,4),
					new SqlParameter("@EquipmentPeriodHappenNum", SqlDbType.Int,4),
					new SqlParameter("@EquipmentPeriodFinlishNum", SqlDbType.Int,4),
					new SqlParameter("@EquipmentPeriodUnFinlishNum", SqlDbType.Int,4),
					new SqlParameter("@EquipmentTempHappenNum", SqlDbType.Int,4),
					new SqlParameter("@EquipmentTempFinlishNum", SqlDbType.Int,4),
					new SqlParameter("@EquipmentTempUnFinlishNum", SqlDbType.Int,4)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.StatType;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.OrganCode;
			parameters[4].Value = model.StatDate;
			parameters[5].Value = model.AllArea;
			parameters[6].Value = model.AllRoomNum;
			parameters[7].Value = model.RoomNum1;
			parameters[8].Value = model.RoomNum2;
			parameters[9].Value = model.RoomNum3;
			parameters[10].Value = model.RoomNum4;
			parameters[11].Value = model.RoomNum5;
			parameters[12].Value = model.RoomNum6;
			parameters[13].Value = model.ChargeRate1;
			parameters[14].Value = model.ChargeRate2;
			parameters[15].Value = model.ChargeRate3;
			parameters[16].Value = model.ChargeRate4;
			parameters[17].Value = model.FeesCreateNum;
			parameters[18].Value = model.FeesCancelNum;
			parameters[19].Value = model.IncidentNum1;
			parameters[20].Value = model.IncidentNum2;
			parameters[21].Value = model.IncidentNum3;
			parameters[22].Value = model.IncidentNum4;
			parameters[23].Value = model.IncidentNum5;
			parameters[24].Value = model.IncidentRate1;
			parameters[25].Value = model.IncidentRate2;
			parameters[26].Value = model.IncidentRate3;
			parameters[27].Value = model.IncidentRate4;
			parameters[28].Value = model.MeterChargeRate1;
			parameters[29].Value = model.MeterChargeRate2;
			parameters[30].Value = model.MeterChargeRate3;
			parameters[31].Value = model.MeterChargeRate4;
			parameters[32].Value = model.CurFeesRate_Deno;
			parameters[33].Value = model.CurFeesRate_Mole;
			parameters[34].Value = model.CurYearFeesRate_Deno;
			parameters[35].Value = model.CurYearFeesRate_Mole;
			parameters[36].Value = model.BefLastFeesRate_Deno;
			parameters[37].Value = model.BefLastFeesRate_Mole;
			parameters[38].Value = model.CurMeterRate_Deno;
			parameters[39].Value = model.CurMeterRate_Mole;
			parameters[40].Value = model.CurYearMeterRate_Deno;
			parameters[41].Value = model.CurYearMeterRate_Mole;
			parameters[42].Value = model.BefLastMeterRate_Deno;
			parameters[43].Value = model.BefLastMeterRate_Mole;
			parameters[44].Value = model.IncidFinlishMonthNum1;
			parameters[45].Value = model.IncidHappenMonthNum1;
			parameters[46].Value = model.IncidFinlishYearNum1;
			parameters[47].Value = model.IncidHappenYearNum1;
			parameters[48].Value = model.IncidFinlishMonthNum2;
			parameters[49].Value = model.IncidHappenMonthNum2;
			parameters[50].Value = model.IncidFinlishYearNum2;
			parameters[51].Value = model.IncidHappenYearNum2;
			parameters[52].Value = model.EquipmentDeviceCount;
			parameters[53].Value = model.EquipmentPeriodHappenNum;
			parameters[54].Value = model.EquipmentPeriodFinlishNum;
			parameters[55].Value = model.EquipmentPeriodUnFinlishNum;
			parameters[56].Value = model.EquipmentTempHappenNum;
			parameters[57].Value = model.EquipmentTempFinlishNum;
			parameters[58].Value = model.EquipmentTempUnFinlishNum;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePic_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_TakePic model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt,8),
					new SqlParameter("@StatType", SqlDbType.SmallInt,2),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@StatDate", SqlDbType.DateTime),
					new SqlParameter("@AllArea", SqlDbType.Decimal,9),
					new SqlParameter("@AllRoomNum", SqlDbType.Int,4),
					new SqlParameter("@RoomNum1", SqlDbType.Int,4),
					new SqlParameter("@RoomNum2", SqlDbType.Int,4),
					new SqlParameter("@RoomNum3", SqlDbType.Int,4),
					new SqlParameter("@RoomNum4", SqlDbType.Int,4),
					new SqlParameter("@RoomNum5", SqlDbType.Int,4),
					new SqlParameter("@RoomNum6", SqlDbType.Int,4),
					new SqlParameter("@ChargeRate1", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRate2", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRate3", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRate4", SqlDbType.Decimal,9),
					new SqlParameter("@FeesCreateNum", SqlDbType.Int,4),
					new SqlParameter("@FeesCancelNum", SqlDbType.Int,4),
					new SqlParameter("@IncidentNum1", SqlDbType.Int,4),
					new SqlParameter("@IncidentNum2", SqlDbType.Int,4),
					new SqlParameter("@IncidentNum3", SqlDbType.Int,4),
					new SqlParameter("@IncidentNum4", SqlDbType.Int,4),
					new SqlParameter("@IncidentNum5", SqlDbType.Int,4),
					new SqlParameter("@IncidentRate1", SqlDbType.Decimal,9),
					new SqlParameter("@IncidentRate2", SqlDbType.Decimal,9),
					new SqlParameter("@IncidentRate3", SqlDbType.Decimal,9),
					new SqlParameter("@IncidentRate4", SqlDbType.Decimal,9),
					new SqlParameter("@MeterChargeRate1", SqlDbType.Decimal,9),
					new SqlParameter("@MeterChargeRate2", SqlDbType.Decimal,9),
					new SqlParameter("@MeterChargeRate3", SqlDbType.Decimal,9),
					new SqlParameter("@MeterChargeRate4", SqlDbType.Decimal,9),
					new SqlParameter("@CurFeesRate_Deno", SqlDbType.Decimal,9),
					new SqlParameter("@CurFeesRate_Mole", SqlDbType.Decimal,9),
					new SqlParameter("@CurYearFeesRate_Deno", SqlDbType.Decimal,9),
					new SqlParameter("@CurYearFeesRate_Mole", SqlDbType.Decimal,9),
					new SqlParameter("@BefLastFeesRate_Deno", SqlDbType.Decimal,9),
					new SqlParameter("@BefLastFeesRate_Mole", SqlDbType.Decimal,9),
					new SqlParameter("@CurMeterRate_Deno", SqlDbType.Decimal,9),
					new SqlParameter("@CurMeterRate_Mole", SqlDbType.Decimal,9),
					new SqlParameter("@CurYearMeterRate_Deno", SqlDbType.Decimal,9),
					new SqlParameter("@CurYearMeterRate_Mole", SqlDbType.Decimal,9),
					new SqlParameter("@BefLastMeterRate_Deno", SqlDbType.Decimal,9),
					new SqlParameter("@BefLastMeterRate_Mole", SqlDbType.Decimal,9),
					new SqlParameter("@IncidFinlishMonthNum1", SqlDbType.Int,4),
					new SqlParameter("@IncidHappenMonthNum1", SqlDbType.Int,4),
					new SqlParameter("@IncidFinlishYearNum1", SqlDbType.Int,4),
					new SqlParameter("@IncidHappenYearNum1", SqlDbType.Int,4),
					new SqlParameter("@IncidFinlishMonthNum2", SqlDbType.Int,4),
					new SqlParameter("@IncidHappenMonthNum2", SqlDbType.Int,4),
					new SqlParameter("@IncidFinlishYearNum2", SqlDbType.Int,4),
					new SqlParameter("@IncidHappenYearNum2", SqlDbType.Int,4),
					new SqlParameter("@EquipmentDeviceCount", SqlDbType.Int,4),
					new SqlParameter("@EquipmentPeriodHappenNum", SqlDbType.Int,4),
					new SqlParameter("@EquipmentPeriodFinlishNum", SqlDbType.Int,4),
					new SqlParameter("@EquipmentPeriodUnFinlishNum", SqlDbType.Int,4),
					new SqlParameter("@EquipmentTempHappenNum", SqlDbType.Int,4),
					new SqlParameter("@EquipmentTempFinlishNum", SqlDbType.Int,4),
					new SqlParameter("@EquipmentTempUnFinlishNum", SqlDbType.Int,4)};
			parameters[0].Value = model.StatID;
			parameters[1].Value = model.StatType;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.OrganCode;
			parameters[4].Value = model.StatDate;
			parameters[5].Value = model.AllArea;
			parameters[6].Value = model.AllRoomNum;
			parameters[7].Value = model.RoomNum1;
			parameters[8].Value = model.RoomNum2;
			parameters[9].Value = model.RoomNum3;
			parameters[10].Value = model.RoomNum4;
			parameters[11].Value = model.RoomNum5;
			parameters[12].Value = model.RoomNum6;
			parameters[13].Value = model.ChargeRate1;
			parameters[14].Value = model.ChargeRate2;
			parameters[15].Value = model.ChargeRate3;
			parameters[16].Value = model.ChargeRate4;
			parameters[17].Value = model.FeesCreateNum;
			parameters[18].Value = model.FeesCancelNum;
			parameters[19].Value = model.IncidentNum1;
			parameters[20].Value = model.IncidentNum2;
			parameters[21].Value = model.IncidentNum3;
			parameters[22].Value = model.IncidentNum4;
			parameters[23].Value = model.IncidentNum5;
			parameters[24].Value = model.IncidentRate1;
			parameters[25].Value = model.IncidentRate2;
			parameters[26].Value = model.IncidentRate3;
			parameters[27].Value = model.IncidentRate4;
			parameters[28].Value = model.MeterChargeRate1;
			parameters[29].Value = model.MeterChargeRate2;
			parameters[30].Value = model.MeterChargeRate3;
			parameters[31].Value = model.MeterChargeRate4;
			parameters[32].Value = model.CurFeesRate_Deno;
			parameters[33].Value = model.CurFeesRate_Mole;
			parameters[34].Value = model.CurYearFeesRate_Deno;
			parameters[35].Value = model.CurYearFeesRate_Mole;
			parameters[36].Value = model.BefLastFeesRate_Deno;
			parameters[37].Value = model.BefLastFeesRate_Mole;
			parameters[38].Value = model.CurMeterRate_Deno;
			parameters[39].Value = model.CurMeterRate_Mole;
			parameters[40].Value = model.CurYearMeterRate_Deno;
			parameters[41].Value = model.CurYearMeterRate_Mole;
			parameters[42].Value = model.BefLastMeterRate_Deno;
			parameters[43].Value = model.BefLastMeterRate_Mole;
			parameters[44].Value = model.IncidFinlishMonthNum1;
			parameters[45].Value = model.IncidHappenMonthNum1;
			parameters[46].Value = model.IncidFinlishYearNum1;
			parameters[47].Value = model.IncidHappenYearNum1;
			parameters[48].Value = model.IncidFinlishMonthNum2;
			parameters[49].Value = model.IncidHappenMonthNum2;
			parameters[50].Value = model.IncidFinlishYearNum2;
			parameters[51].Value = model.IncidHappenYearNum2;
			parameters[52].Value = model.EquipmentDeviceCount;
			parameters[53].Value = model.EquipmentPeriodHappenNum;
			parameters[54].Value = model.EquipmentPeriodFinlishNum;
			parameters[55].Value = model.EquipmentPeriodUnFinlishNum;
			parameters[56].Value = model.EquipmentTempHappenNum;
			parameters[57].Value = model.EquipmentTempFinlishNum;
			parameters[58].Value = model.EquipmentTempUnFinlishNum;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePic_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long StatID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt)};
			parameters[0].Value = StatID;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePic_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePic GetModel(long StatID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt)};
			parameters[0].Value = StatID;

			MobileSoft.Model.Sys.Tb_Sys_TakePic model=new MobileSoft.Model.Sys.Tb_Sys_TakePic();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePic_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["StatID"].ToString()!="")
				{
					model.StatID=long.Parse(ds.Tables[0].Rows[0]["StatID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["StatType"].ToString()!="")
				{
					model.StatType=int.Parse(ds.Tables[0].Rows[0]["StatType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				model.OrganCode=ds.Tables[0].Rows[0]["OrganCode"].ToString();
				if(ds.Tables[0].Rows[0]["StatDate"].ToString()!="")
				{
					model.StatDate=DateTime.Parse(ds.Tables[0].Rows[0]["StatDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AllArea"].ToString()!="")
				{
					model.AllArea=decimal.Parse(ds.Tables[0].Rows[0]["AllArea"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AllRoomNum"].ToString()!="")
				{
					model.AllRoomNum=int.Parse(ds.Tables[0].Rows[0]["AllRoomNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RoomNum1"].ToString()!="")
				{
					model.RoomNum1=int.Parse(ds.Tables[0].Rows[0]["RoomNum1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RoomNum2"].ToString()!="")
				{
					model.RoomNum2=int.Parse(ds.Tables[0].Rows[0]["RoomNum2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RoomNum3"].ToString()!="")
				{
					model.RoomNum3=int.Parse(ds.Tables[0].Rows[0]["RoomNum3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RoomNum4"].ToString()!="")
				{
					model.RoomNum4=int.Parse(ds.Tables[0].Rows[0]["RoomNum4"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RoomNum5"].ToString()!="")
				{
					model.RoomNum5=int.Parse(ds.Tables[0].Rows[0]["RoomNum5"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RoomNum6"].ToString()!="")
				{
					model.RoomNum6=int.Parse(ds.Tables[0].Rows[0]["RoomNum6"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeRate1"].ToString()!="")
				{
					model.ChargeRate1=decimal.Parse(ds.Tables[0].Rows[0]["ChargeRate1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeRate2"].ToString()!="")
				{
					model.ChargeRate2=decimal.Parse(ds.Tables[0].Rows[0]["ChargeRate2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeRate3"].ToString()!="")
				{
					model.ChargeRate3=decimal.Parse(ds.Tables[0].Rows[0]["ChargeRate3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeRate4"].ToString()!="")
				{
					model.ChargeRate4=decimal.Parse(ds.Tables[0].Rows[0]["ChargeRate4"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FeesCreateNum"].ToString()!="")
				{
					model.FeesCreateNum=int.Parse(ds.Tables[0].Rows[0]["FeesCreateNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FeesCancelNum"].ToString()!="")
				{
					model.FeesCancelNum=int.Parse(ds.Tables[0].Rows[0]["FeesCancelNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidentNum1"].ToString()!="")
				{
					model.IncidentNum1=int.Parse(ds.Tables[0].Rows[0]["IncidentNum1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidentNum2"].ToString()!="")
				{
					model.IncidentNum2=int.Parse(ds.Tables[0].Rows[0]["IncidentNum2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidentNum3"].ToString()!="")
				{
					model.IncidentNum3=int.Parse(ds.Tables[0].Rows[0]["IncidentNum3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidentNum4"].ToString()!="")
				{
					model.IncidentNum4=int.Parse(ds.Tables[0].Rows[0]["IncidentNum4"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidentNum5"].ToString()!="")
				{
					model.IncidentNum5=int.Parse(ds.Tables[0].Rows[0]["IncidentNum5"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidentRate1"].ToString()!="")
				{
					model.IncidentRate1=decimal.Parse(ds.Tables[0].Rows[0]["IncidentRate1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidentRate2"].ToString()!="")
				{
					model.IncidentRate2=decimal.Parse(ds.Tables[0].Rows[0]["IncidentRate2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidentRate3"].ToString()!="")
				{
					model.IncidentRate3=decimal.Parse(ds.Tables[0].Rows[0]["IncidentRate3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidentRate4"].ToString()!="")
				{
					model.IncidentRate4=decimal.Parse(ds.Tables[0].Rows[0]["IncidentRate4"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MeterChargeRate1"].ToString()!="")
				{
					model.MeterChargeRate1=decimal.Parse(ds.Tables[0].Rows[0]["MeterChargeRate1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MeterChargeRate2"].ToString()!="")
				{
					model.MeterChargeRate2=decimal.Parse(ds.Tables[0].Rows[0]["MeterChargeRate2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MeterChargeRate3"].ToString()!="")
				{
					model.MeterChargeRate3=decimal.Parse(ds.Tables[0].Rows[0]["MeterChargeRate3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MeterChargeRate4"].ToString()!="")
				{
					model.MeterChargeRate4=decimal.Parse(ds.Tables[0].Rows[0]["MeterChargeRate4"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CurFeesRate_Deno"].ToString()!="")
				{
					model.CurFeesRate_Deno=decimal.Parse(ds.Tables[0].Rows[0]["CurFeesRate_Deno"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CurFeesRate_Mole"].ToString()!="")
				{
					model.CurFeesRate_Mole=decimal.Parse(ds.Tables[0].Rows[0]["CurFeesRate_Mole"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CurYearFeesRate_Deno"].ToString()!="")
				{
					model.CurYearFeesRate_Deno=decimal.Parse(ds.Tables[0].Rows[0]["CurYearFeesRate_Deno"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CurYearFeesRate_Mole"].ToString()!="")
				{
					model.CurYearFeesRate_Mole=decimal.Parse(ds.Tables[0].Rows[0]["CurYearFeesRate_Mole"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BefLastFeesRate_Deno"].ToString()!="")
				{
					model.BefLastFeesRate_Deno=decimal.Parse(ds.Tables[0].Rows[0]["BefLastFeesRate_Deno"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BefLastFeesRate_Mole"].ToString()!="")
				{
					model.BefLastFeesRate_Mole=decimal.Parse(ds.Tables[0].Rows[0]["BefLastFeesRate_Mole"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CurMeterRate_Deno"].ToString()!="")
				{
					model.CurMeterRate_Deno=decimal.Parse(ds.Tables[0].Rows[0]["CurMeterRate_Deno"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CurMeterRate_Mole"].ToString()!="")
				{
					model.CurMeterRate_Mole=decimal.Parse(ds.Tables[0].Rows[0]["CurMeterRate_Mole"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CurYearMeterRate_Deno"].ToString()!="")
				{
					model.CurYearMeterRate_Deno=decimal.Parse(ds.Tables[0].Rows[0]["CurYearMeterRate_Deno"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CurYearMeterRate_Mole"].ToString()!="")
				{
					model.CurYearMeterRate_Mole=decimal.Parse(ds.Tables[0].Rows[0]["CurYearMeterRate_Mole"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BefLastMeterRate_Deno"].ToString()!="")
				{
					model.BefLastMeterRate_Deno=decimal.Parse(ds.Tables[0].Rows[0]["BefLastMeterRate_Deno"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BefLastMeterRate_Mole"].ToString()!="")
				{
					model.BefLastMeterRate_Mole=decimal.Parse(ds.Tables[0].Rows[0]["BefLastMeterRate_Mole"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidFinlishMonthNum1"].ToString()!="")
				{
					model.IncidFinlishMonthNum1=int.Parse(ds.Tables[0].Rows[0]["IncidFinlishMonthNum1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidHappenMonthNum1"].ToString()!="")
				{
					model.IncidHappenMonthNum1=int.Parse(ds.Tables[0].Rows[0]["IncidHappenMonthNum1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidFinlishYearNum1"].ToString()!="")
				{
					model.IncidFinlishYearNum1=int.Parse(ds.Tables[0].Rows[0]["IncidFinlishYearNum1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidHappenYearNum1"].ToString()!="")
				{
					model.IncidHappenYearNum1=int.Parse(ds.Tables[0].Rows[0]["IncidHappenYearNum1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidFinlishMonthNum2"].ToString()!="")
				{
					model.IncidFinlishMonthNum2=int.Parse(ds.Tables[0].Rows[0]["IncidFinlishMonthNum2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidHappenMonthNum2"].ToString()!="")
				{
					model.IncidHappenMonthNum2=int.Parse(ds.Tables[0].Rows[0]["IncidHappenMonthNum2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidFinlishYearNum2"].ToString()!="")
				{
					model.IncidFinlishYearNum2=int.Parse(ds.Tables[0].Rows[0]["IncidFinlishYearNum2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidHappenYearNum2"].ToString()!="")
				{
					model.IncidHappenYearNum2=int.Parse(ds.Tables[0].Rows[0]["IncidHappenYearNum2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EquipmentDeviceCount"].ToString()!="")
				{
					model.EquipmentDeviceCount=int.Parse(ds.Tables[0].Rows[0]["EquipmentDeviceCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EquipmentPeriodHappenNum"].ToString()!="")
				{
					model.EquipmentPeriodHappenNum=int.Parse(ds.Tables[0].Rows[0]["EquipmentPeriodHappenNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EquipmentPeriodFinlishNum"].ToString()!="")
				{
					model.EquipmentPeriodFinlishNum=int.Parse(ds.Tables[0].Rows[0]["EquipmentPeriodFinlishNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EquipmentPeriodUnFinlishNum"].ToString()!="")
				{
					model.EquipmentPeriodUnFinlishNum=int.Parse(ds.Tables[0].Rows[0]["EquipmentPeriodUnFinlishNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EquipmentTempHappenNum"].ToString()!="")
				{
					model.EquipmentTempHappenNum=int.Parse(ds.Tables[0].Rows[0]["EquipmentTempHappenNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EquipmentTempFinlishNum"].ToString()!="")
				{
					model.EquipmentTempFinlishNum=int.Parse(ds.Tables[0].Rows[0]["EquipmentTempFinlishNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EquipmentTempUnFinlishNum"].ToString()!="")
				{
					model.EquipmentTempUnFinlishNum=int.Parse(ds.Tables[0].Rows[0]["EquipmentTempUnFinlishNum"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

        public DataSet GetListFromProc(string strWhere)
        {
            SqlParameter[] parameters = 
            {
                new SqlParameter("@SQLEx", SqlDbType.VarChar)
            };
            parameters[0].Value = strWhere;

            DataSet Ds = DbHelperSQL.RunProcedure("Proc_Sys_TakePic_Filter", parameters, "RetDataSet");

            return Ds;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM view_Sys_TakePic_Filter ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere,string Files)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  "+ Files);
            
            strSql.Append(" FROM view_Sys_TakePic_Filter ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
			strSql.Append(" FROM view_Sys_TakePic_Filter ");
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
			parameters[5].Value = "SELECT * FROM view_Sys_TakePic_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "StatID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

        public DataTable Sys_TakePic_FilterTopNum(int TopNum, string SQLEx)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@TopNum", SqlDbType.NVarChar,50),
                              new SqlParameter("@SQLEx", SqlDbType.VarChar,3999)
                                              };
            parameters[0].Value = TopNum;
            parameters[1].Value = SQLEx;

            DataTable result = DbHelperSQL.RunProcedure("Proc_Sys_TakePic_FilterTopNum", parameters, "RetDataSet").Tables[0];

            return result;
        }


	}
}

