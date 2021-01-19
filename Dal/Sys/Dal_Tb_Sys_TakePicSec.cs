using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Sys
{
	/// <summary>
	/// 数据访问类Dal_Tb_Sys_TakePicSec。
	/// </summary>
	public class Dal_Tb_Sys_TakePicSec
	{
		public Dal_Tb_Sys_TakePicSec()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicSec_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.Sys.Tb_Sys_TakePicSec model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt,8),
					new SqlParameter("@StatType", SqlDbType.SmallInt,2),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@StatDate", SqlDbType.DateTime),
					new SqlParameter("@ManageCount", SqlDbType.Int,4),
					new SqlParameter("@ManageArea", SqlDbType.Decimal,9),
					new SqlParameter("@ManageCount1", SqlDbType.Int,4),
					new SqlParameter("@ManageArea1", SqlDbType.Decimal,9),
					new SqlParameter("@ManageCount2", SqlDbType.Int,4),
					new SqlParameter("@ManageArea2", SqlDbType.Decimal,9),
					new SqlParameter("@ManageCount3", SqlDbType.Int,4),
					new SqlParameter("@ManageArea3", SqlDbType.Decimal,9),
					new SqlParameter("@ManageCountNew", SqlDbType.Int,4),
					new SqlParameter("@ManageAreaNew", SqlDbType.Decimal,9),
					new SqlParameter("@AllArea", SqlDbType.Decimal,9),
					new SqlParameter("@ProUseArea1", SqlDbType.Decimal,9),
					new SqlParameter("@ProUseArea2", SqlDbType.Decimal,9),
					new SqlParameter("@ProUseArea3", SqlDbType.Decimal,9),
					new SqlParameter("@ProUseRoomNum1", SqlDbType.Int,4),
					new SqlParameter("@ProUseRoomNum2", SqlDbType.Int,4),
					new SqlParameter("@ProUseRoomNum3", SqlDbType.Int,4),
					new SqlParameter("@ParkAllNum", SqlDbType.Int,4),
					new SqlParameter("@ParkSaleNum", SqlDbType.Int,4),
					new SqlParameter("@ParkLeaseNum", SqlDbType.Int,4),
					new SqlParameter("@ParkEmptyNum", SqlDbType.Int,4),
					new SqlParameter("@CommKindRoomNum", SqlDbType.Int,4),
					new SqlParameter("@CommKindArea1", SqlDbType.Decimal,9),
					new SqlParameter("@CommKindCount1", SqlDbType.Int,4),
					new SqlParameter("@CommKindRoomNum1", SqlDbType.Int,4),
					new SqlParameter("@CommKindArea2", SqlDbType.Decimal,9),
					new SqlParameter("@CommKindCount2", SqlDbType.Int,4),
					new SqlParameter("@CommKindRoomNum2", SqlDbType.Int,4),
					new SqlParameter("@ChargeRateTheMonth", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRateTheYear", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRateLastYear", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRateTheMonth1", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRateTheYear1", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRateLastYear1", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRateTheMonth2", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRateTheYear2", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRateLastYear2", SqlDbType.Decimal,9),
					new SqlParameter("@IncidCustHasNum", SqlDbType.Int,4),
					new SqlParameter("@IncidCustNoNum", SqlDbType.Int,4),
					new SqlParameter("@IncidCustDayRate", SqlDbType.Int,4),
					new SqlParameter("@IncidCustMonthRate", SqlDbType.Int,4),
					new SqlParameter("@IncidCustYearRate", SqlDbType.Int,4),
					new SqlParameter("@IncidCustAllNoNum", SqlDbType.Int,4),
					new SqlParameter("@IncidCustDayNoNum", SqlDbType.Int,4),
					new SqlParameter("@IncidCustWeekNoNum", SqlDbType.Int,4),
					new SqlParameter("@IncidCustMonthNoNum", SqlDbType.Int,4),
					new SqlParameter("@IncidCompDayNum", SqlDbType.Int,4),
					new SqlParameter("@IncidCompMonthNum", SqlDbType.Int,4),
					new SqlParameter("@IncidCompYearNum", SqlDbType.Int,4),
					new SqlParameter("@CurFeesRate_Deno", SqlDbType.Decimal,9),
					new SqlParameter("@CurFeesRate_Mole", SqlDbType.Decimal,9),
					new SqlParameter("@CurYearFeesRate_Deno", SqlDbType.Decimal,9),
					new SqlParameter("@CurYearFeesRate_Mole", SqlDbType.Decimal,9),
					new SqlParameter("@BefLastFeesRate_Deno", SqlDbType.Decimal,9),
					new SqlParameter("@BefLastFeesRate_Mole", SqlDbType.Decimal,9),
					new SqlParameter("@IncidFinlishDayNum", SqlDbType.Int,4),
					new SqlParameter("@IncidHappenDayNum", SqlDbType.Int,4),
					new SqlParameter("@IncidFinlishMonthNum", SqlDbType.Int,4),
					new SqlParameter("@IncidHappenMonthNum", SqlDbType.Int,4),
					new SqlParameter("@IncidFinlishYearNum", SqlDbType.Int,4),
					new SqlParameter("@IncidHappenYearNum", SqlDbType.Int,4)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.StatType;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.OrganCode;
			parameters[4].Value = model.StatDate;
			parameters[5].Value = model.ManageCount;
			parameters[6].Value = model.ManageArea;
			parameters[7].Value = model.ManageCount1;
			parameters[8].Value = model.ManageArea1;
			parameters[9].Value = model.ManageCount2;
			parameters[10].Value = model.ManageArea2;
			parameters[11].Value = model.ManageCount3;
			parameters[12].Value = model.ManageArea3;
			parameters[13].Value = model.ManageCountNew;
			parameters[14].Value = model.ManageAreaNew;
			parameters[15].Value = model.AllArea;
			parameters[16].Value = model.ProUseArea1;
			parameters[17].Value = model.ProUseArea2;
			parameters[18].Value = model.ProUseArea3;
			parameters[19].Value = model.ProUseRoomNum1;
			parameters[20].Value = model.ProUseRoomNum2;
			parameters[21].Value = model.ProUseRoomNum3;
			parameters[22].Value = model.ParkAllNum;
			parameters[23].Value = model.ParkSaleNum;
			parameters[24].Value = model.ParkLeaseNum;
			parameters[25].Value = model.ParkEmptyNum;
			parameters[26].Value = model.CommKindRoomNum;
			parameters[27].Value = model.CommKindArea1;
			parameters[28].Value = model.CommKindCount1;
			parameters[29].Value = model.CommKindRoomNum1;
			parameters[30].Value = model.CommKindArea2;
			parameters[31].Value = model.CommKindCount2;
			parameters[32].Value = model.CommKindRoomNum2;
			parameters[33].Value = model.ChargeRateTheMonth;
			parameters[34].Value = model.ChargeRateTheYear;
			parameters[35].Value = model.ChargeRateLastYear;
			parameters[36].Value = model.ChargeRateTheMonth1;
			parameters[37].Value = model.ChargeRateTheYear1;
			parameters[38].Value = model.ChargeRateLastYear1;
			parameters[39].Value = model.ChargeRateTheMonth2;
			parameters[40].Value = model.ChargeRateTheYear2;
			parameters[41].Value = model.ChargeRateLastYear2;
			parameters[42].Value = model.IncidCustHasNum;
			parameters[43].Value = model.IncidCustNoNum;
			parameters[44].Value = model.IncidCustDayRate;
			parameters[45].Value = model.IncidCustMonthRate;
			parameters[46].Value = model.IncidCustYearRate;
			parameters[47].Value = model.IncidCustAllNoNum;
			parameters[48].Value = model.IncidCustDayNoNum;
			parameters[49].Value = model.IncidCustWeekNoNum;
			parameters[50].Value = model.IncidCustMonthNoNum;
			parameters[51].Value = model.IncidCompDayNum;
			parameters[52].Value = model.IncidCompMonthNum;
			parameters[53].Value = model.IncidCompYearNum;
			parameters[54].Value = model.CurFeesRate_Deno;
			parameters[55].Value = model.CurFeesRate_Mole;
			parameters[56].Value = model.CurYearFeesRate_Deno;
			parameters[57].Value = model.CurYearFeesRate_Mole;
			parameters[58].Value = model.BefLastFeesRate_Deno;
			parameters[59].Value = model.BefLastFeesRate_Mole;
			parameters[60].Value = model.IncidFinlishDayNum;
			parameters[61].Value = model.IncidHappenDayNum;
			parameters[62].Value = model.IncidFinlishMonthNum;
			parameters[63].Value = model.IncidHappenMonthNum;
			parameters[64].Value = model.IncidFinlishYearNum;
			parameters[65].Value = model.IncidHappenYearNum;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicSec_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_TakePicSec model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt,8),
					new SqlParameter("@StatType", SqlDbType.SmallInt,2),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@StatDate", SqlDbType.DateTime),
					new SqlParameter("@ManageCount", SqlDbType.Int,4),
					new SqlParameter("@ManageArea", SqlDbType.Decimal,9),
					new SqlParameter("@ManageCount1", SqlDbType.Int,4),
					new SqlParameter("@ManageArea1", SqlDbType.Decimal,9),
					new SqlParameter("@ManageCount2", SqlDbType.Int,4),
					new SqlParameter("@ManageArea2", SqlDbType.Decimal,9),
					new SqlParameter("@ManageCount3", SqlDbType.Int,4),
					new SqlParameter("@ManageArea3", SqlDbType.Decimal,9),
					new SqlParameter("@ManageCountNew", SqlDbType.Int,4),
					new SqlParameter("@ManageAreaNew", SqlDbType.Decimal,9),
					new SqlParameter("@AllArea", SqlDbType.Decimal,9),
					new SqlParameter("@ProUseArea1", SqlDbType.Decimal,9),
					new SqlParameter("@ProUseArea2", SqlDbType.Decimal,9),
					new SqlParameter("@ProUseArea3", SqlDbType.Decimal,9),
					new SqlParameter("@ProUseRoomNum1", SqlDbType.Int,4),
					new SqlParameter("@ProUseRoomNum2", SqlDbType.Int,4),
					new SqlParameter("@ProUseRoomNum3", SqlDbType.Int,4),
					new SqlParameter("@ParkAllNum", SqlDbType.Int,4),
					new SqlParameter("@ParkSaleNum", SqlDbType.Int,4),
					new SqlParameter("@ParkLeaseNum", SqlDbType.Int,4),
					new SqlParameter("@ParkEmptyNum", SqlDbType.Int,4),
					new SqlParameter("@CommKindRoomNum", SqlDbType.Int,4),
					new SqlParameter("@CommKindArea1", SqlDbType.Decimal,9),
					new SqlParameter("@CommKindCount1", SqlDbType.Int,4),
					new SqlParameter("@CommKindRoomNum1", SqlDbType.Int,4),
					new SqlParameter("@CommKindArea2", SqlDbType.Decimal,9),
					new SqlParameter("@CommKindCount2", SqlDbType.Int,4),
					new SqlParameter("@CommKindRoomNum2", SqlDbType.Int,4),
					new SqlParameter("@ChargeRateTheMonth", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRateTheYear", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRateLastYear", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRateTheMonth1", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRateTheYear1", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRateLastYear1", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRateTheMonth2", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRateTheYear2", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeRateLastYear2", SqlDbType.Decimal,9),
					new SqlParameter("@IncidCustHasNum", SqlDbType.Int,4),
					new SqlParameter("@IncidCustNoNum", SqlDbType.Int,4),
					new SqlParameter("@IncidCustDayRate", SqlDbType.Int,4),
					new SqlParameter("@IncidCustMonthRate", SqlDbType.Int,4),
					new SqlParameter("@IncidCustYearRate", SqlDbType.Int,4),
					new SqlParameter("@IncidCustAllNoNum", SqlDbType.Int,4),
					new SqlParameter("@IncidCustDayNoNum", SqlDbType.Int,4),
					new SqlParameter("@IncidCustWeekNoNum", SqlDbType.Int,4),
					new SqlParameter("@IncidCustMonthNoNum", SqlDbType.Int,4),
					new SqlParameter("@IncidCompDayNum", SqlDbType.Int,4),
					new SqlParameter("@IncidCompMonthNum", SqlDbType.Int,4),
					new SqlParameter("@IncidCompYearNum", SqlDbType.Int,4),
					new SqlParameter("@CurFeesRate_Deno", SqlDbType.Decimal,9),
					new SqlParameter("@CurFeesRate_Mole", SqlDbType.Decimal,9),
					new SqlParameter("@CurYearFeesRate_Deno", SqlDbType.Decimal,9),
					new SqlParameter("@CurYearFeesRate_Mole", SqlDbType.Decimal,9),
					new SqlParameter("@BefLastFeesRate_Deno", SqlDbType.Decimal,9),
					new SqlParameter("@BefLastFeesRate_Mole", SqlDbType.Decimal,9),
					new SqlParameter("@IncidFinlishDayNum", SqlDbType.Int,4),
					new SqlParameter("@IncidHappenDayNum", SqlDbType.Int,4),
					new SqlParameter("@IncidFinlishMonthNum", SqlDbType.Int,4),
					new SqlParameter("@IncidHappenMonthNum", SqlDbType.Int,4),
					new SqlParameter("@IncidFinlishYearNum", SqlDbType.Int,4),
					new SqlParameter("@IncidHappenYearNum", SqlDbType.Int,4)};
			parameters[0].Value = model.StatID;
			parameters[1].Value = model.StatType;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.OrganCode;
			parameters[4].Value = model.StatDate;
			parameters[5].Value = model.ManageCount;
			parameters[6].Value = model.ManageArea;
			parameters[7].Value = model.ManageCount1;
			parameters[8].Value = model.ManageArea1;
			parameters[9].Value = model.ManageCount2;
			parameters[10].Value = model.ManageArea2;
			parameters[11].Value = model.ManageCount3;
			parameters[12].Value = model.ManageArea3;
			parameters[13].Value = model.ManageCountNew;
			parameters[14].Value = model.ManageAreaNew;
			parameters[15].Value = model.AllArea;
			parameters[16].Value = model.ProUseArea1;
			parameters[17].Value = model.ProUseArea2;
			parameters[18].Value = model.ProUseArea3;
			parameters[19].Value = model.ProUseRoomNum1;
			parameters[20].Value = model.ProUseRoomNum2;
			parameters[21].Value = model.ProUseRoomNum3;
			parameters[22].Value = model.ParkAllNum;
			parameters[23].Value = model.ParkSaleNum;
			parameters[24].Value = model.ParkLeaseNum;
			parameters[25].Value = model.ParkEmptyNum;
			parameters[26].Value = model.CommKindRoomNum;
			parameters[27].Value = model.CommKindArea1;
			parameters[28].Value = model.CommKindCount1;
			parameters[29].Value = model.CommKindRoomNum1;
			parameters[30].Value = model.CommKindArea2;
			parameters[31].Value = model.CommKindCount2;
			parameters[32].Value = model.CommKindRoomNum2;
			parameters[33].Value = model.ChargeRateTheMonth;
			parameters[34].Value = model.ChargeRateTheYear;
			parameters[35].Value = model.ChargeRateLastYear;
			parameters[36].Value = model.ChargeRateTheMonth1;
			parameters[37].Value = model.ChargeRateTheYear1;
			parameters[38].Value = model.ChargeRateLastYear1;
			parameters[39].Value = model.ChargeRateTheMonth2;
			parameters[40].Value = model.ChargeRateTheYear2;
			parameters[41].Value = model.ChargeRateLastYear2;
			parameters[42].Value = model.IncidCustHasNum;
			parameters[43].Value = model.IncidCustNoNum;
			parameters[44].Value = model.IncidCustDayRate;
			parameters[45].Value = model.IncidCustMonthRate;
			parameters[46].Value = model.IncidCustYearRate;
			parameters[47].Value = model.IncidCustAllNoNum;
			parameters[48].Value = model.IncidCustDayNoNum;
			parameters[49].Value = model.IncidCustWeekNoNum;
			parameters[50].Value = model.IncidCustMonthNoNum;
			parameters[51].Value = model.IncidCompDayNum;
			parameters[52].Value = model.IncidCompMonthNum;
			parameters[53].Value = model.IncidCompYearNum;
			parameters[54].Value = model.CurFeesRate_Deno;
			parameters[55].Value = model.CurFeesRate_Mole;
			parameters[56].Value = model.CurYearFeesRate_Deno;
			parameters[57].Value = model.CurYearFeesRate_Mole;
			parameters[58].Value = model.BefLastFeesRate_Deno;
			parameters[59].Value = model.BefLastFeesRate_Mole;
			parameters[60].Value = model.IncidFinlishDayNum;
			parameters[61].Value = model.IncidHappenDayNum;
			parameters[62].Value = model.IncidFinlishMonthNum;
			parameters[63].Value = model.IncidHappenMonthNum;
			parameters[64].Value = model.IncidFinlishYearNum;
			parameters[65].Value = model.IncidHappenYearNum;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicSec_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicSec_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePicSec GetModel(long StatID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt)};
			parameters[0].Value = StatID;

			MobileSoft.Model.Sys.Tb_Sys_TakePicSec model=new MobileSoft.Model.Sys.Tb_Sys_TakePicSec();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicSec_GetModel",parameters,"ds");
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
				if(ds.Tables[0].Rows[0]["ManageCount"].ToString()!="")
				{
					model.ManageCount=int.Parse(ds.Tables[0].Rows[0]["ManageCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ManageArea"].ToString()!="")
				{
					model.ManageArea=decimal.Parse(ds.Tables[0].Rows[0]["ManageArea"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ManageCount1"].ToString()!="")
				{
					model.ManageCount1=int.Parse(ds.Tables[0].Rows[0]["ManageCount1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ManageArea1"].ToString()!="")
				{
					model.ManageArea1=decimal.Parse(ds.Tables[0].Rows[0]["ManageArea1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ManageCount2"].ToString()!="")
				{
					model.ManageCount2=int.Parse(ds.Tables[0].Rows[0]["ManageCount2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ManageArea2"].ToString()!="")
				{
					model.ManageArea2=decimal.Parse(ds.Tables[0].Rows[0]["ManageArea2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ManageCount3"].ToString()!="")
				{
					model.ManageCount3=int.Parse(ds.Tables[0].Rows[0]["ManageCount3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ManageArea3"].ToString()!="")
				{
					model.ManageArea3=decimal.Parse(ds.Tables[0].Rows[0]["ManageArea3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ManageCountNew"].ToString()!="")
				{
					model.ManageCountNew=int.Parse(ds.Tables[0].Rows[0]["ManageCountNew"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ManageAreaNew"].ToString()!="")
				{
					model.ManageAreaNew=decimal.Parse(ds.Tables[0].Rows[0]["ManageAreaNew"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AllArea"].ToString()!="")
				{
					model.AllArea=decimal.Parse(ds.Tables[0].Rows[0]["AllArea"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ProUseArea1"].ToString()!="")
				{
					model.ProUseArea1=decimal.Parse(ds.Tables[0].Rows[0]["ProUseArea1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ProUseArea2"].ToString()!="")
				{
					model.ProUseArea2=decimal.Parse(ds.Tables[0].Rows[0]["ProUseArea2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ProUseArea3"].ToString()!="")
				{
					model.ProUseArea3=decimal.Parse(ds.Tables[0].Rows[0]["ProUseArea3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ProUseRoomNum1"].ToString()!="")
				{
					model.ProUseRoomNum1=int.Parse(ds.Tables[0].Rows[0]["ProUseRoomNum1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ProUseRoomNum2"].ToString()!="")
				{
					model.ProUseRoomNum2=int.Parse(ds.Tables[0].Rows[0]["ProUseRoomNum2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ProUseRoomNum3"].ToString()!="")
				{
					model.ProUseRoomNum3=int.Parse(ds.Tables[0].Rows[0]["ProUseRoomNum3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParkAllNum"].ToString()!="")
				{
					model.ParkAllNum=int.Parse(ds.Tables[0].Rows[0]["ParkAllNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParkSaleNum"].ToString()!="")
				{
					model.ParkSaleNum=int.Parse(ds.Tables[0].Rows[0]["ParkSaleNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParkLeaseNum"].ToString()!="")
				{
					model.ParkLeaseNum=int.Parse(ds.Tables[0].Rows[0]["ParkLeaseNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParkEmptyNum"].ToString()!="")
				{
					model.ParkEmptyNum=int.Parse(ds.Tables[0].Rows[0]["ParkEmptyNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommKindRoomNum"].ToString()!="")
				{
					model.CommKindRoomNum=int.Parse(ds.Tables[0].Rows[0]["CommKindRoomNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommKindArea1"].ToString()!="")
				{
					model.CommKindArea1=decimal.Parse(ds.Tables[0].Rows[0]["CommKindArea1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommKindCount1"].ToString()!="")
				{
					model.CommKindCount1=int.Parse(ds.Tables[0].Rows[0]["CommKindCount1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommKindRoomNum1"].ToString()!="")
				{
					model.CommKindRoomNum1=int.Parse(ds.Tables[0].Rows[0]["CommKindRoomNum1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommKindArea2"].ToString()!="")
				{
					model.CommKindArea2=decimal.Parse(ds.Tables[0].Rows[0]["CommKindArea2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommKindCount2"].ToString()!="")
				{
					model.CommKindCount2=int.Parse(ds.Tables[0].Rows[0]["CommKindCount2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommKindRoomNum2"].ToString()!="")
				{
					model.CommKindRoomNum2=int.Parse(ds.Tables[0].Rows[0]["CommKindRoomNum2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeRateTheMonth"].ToString()!="")
				{
					model.ChargeRateTheMonth=decimal.Parse(ds.Tables[0].Rows[0]["ChargeRateTheMonth"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeRateTheYear"].ToString()!="")
				{
					model.ChargeRateTheYear=decimal.Parse(ds.Tables[0].Rows[0]["ChargeRateTheYear"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeRateLastYear"].ToString()!="")
				{
					model.ChargeRateLastYear=decimal.Parse(ds.Tables[0].Rows[0]["ChargeRateLastYear"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeRateTheMonth1"].ToString()!="")
				{
					model.ChargeRateTheMonth1=decimal.Parse(ds.Tables[0].Rows[0]["ChargeRateTheMonth1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeRateTheYear1"].ToString()!="")
				{
					model.ChargeRateTheYear1=decimal.Parse(ds.Tables[0].Rows[0]["ChargeRateTheYear1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeRateLastYear1"].ToString()!="")
				{
					model.ChargeRateLastYear1=decimal.Parse(ds.Tables[0].Rows[0]["ChargeRateLastYear1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeRateTheMonth2"].ToString()!="")
				{
					model.ChargeRateTheMonth2=decimal.Parse(ds.Tables[0].Rows[0]["ChargeRateTheMonth2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeRateTheYear2"].ToString()!="")
				{
					model.ChargeRateTheYear2=decimal.Parse(ds.Tables[0].Rows[0]["ChargeRateTheYear2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeRateLastYear2"].ToString()!="")
				{
					model.ChargeRateLastYear2=decimal.Parse(ds.Tables[0].Rows[0]["ChargeRateLastYear2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidCustHasNum"].ToString()!="")
				{
					model.IncidCustHasNum=int.Parse(ds.Tables[0].Rows[0]["IncidCustHasNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidCustNoNum"].ToString()!="")
				{
					model.IncidCustNoNum=int.Parse(ds.Tables[0].Rows[0]["IncidCustNoNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidCustDayRate"].ToString()!="")
				{
					model.IncidCustDayRate=int.Parse(ds.Tables[0].Rows[0]["IncidCustDayRate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidCustMonthRate"].ToString()!="")
				{
					model.IncidCustMonthRate=int.Parse(ds.Tables[0].Rows[0]["IncidCustMonthRate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidCustYearRate"].ToString()!="")
				{
					model.IncidCustYearRate=int.Parse(ds.Tables[0].Rows[0]["IncidCustYearRate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidCustAllNoNum"].ToString()!="")
				{
					model.IncidCustAllNoNum=int.Parse(ds.Tables[0].Rows[0]["IncidCustAllNoNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidCustDayNoNum"].ToString()!="")
				{
					model.IncidCustDayNoNum=int.Parse(ds.Tables[0].Rows[0]["IncidCustDayNoNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidCustWeekNoNum"].ToString()!="")
				{
					model.IncidCustWeekNoNum=int.Parse(ds.Tables[0].Rows[0]["IncidCustWeekNoNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidCustMonthNoNum"].ToString()!="")
				{
					model.IncidCustMonthNoNum=int.Parse(ds.Tables[0].Rows[0]["IncidCustMonthNoNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidCompDayNum"].ToString()!="")
				{
					model.IncidCompDayNum=int.Parse(ds.Tables[0].Rows[0]["IncidCompDayNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidCompMonthNum"].ToString()!="")
				{
					model.IncidCompMonthNum=int.Parse(ds.Tables[0].Rows[0]["IncidCompMonthNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidCompYearNum"].ToString()!="")
				{
					model.IncidCompYearNum=int.Parse(ds.Tables[0].Rows[0]["IncidCompYearNum"].ToString());
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
				if(ds.Tables[0].Rows[0]["IncidFinlishDayNum"].ToString()!="")
				{
					model.IncidFinlishDayNum=int.Parse(ds.Tables[0].Rows[0]["IncidFinlishDayNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidHappenDayNum"].ToString()!="")
				{
					model.IncidHappenDayNum=int.Parse(ds.Tables[0].Rows[0]["IncidHappenDayNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidFinlishMonthNum"].ToString()!="")
				{
					model.IncidFinlishMonthNum=int.Parse(ds.Tables[0].Rows[0]["IncidFinlishMonthNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidHappenMonthNum"].ToString()!="")
				{
					model.IncidHappenMonthNum=int.Parse(ds.Tables[0].Rows[0]["IncidHappenMonthNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidFinlishYearNum"].ToString()!="")
				{
					model.IncidFinlishYearNum=int.Parse(ds.Tables[0].Rows[0]["IncidFinlishYearNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidHappenYearNum"].ToString()!="")
				{
					model.IncidHappenYearNum=int.Parse(ds.Tables[0].Rows[0]["IncidHappenYearNum"].ToString());
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
			strSql.Append("select StatID,StatType,CommID,OrganCode,StatDate,ManageCount,ManageArea,ManageCount1,ManageArea1,ManageCount2,ManageArea2,ManageCount3,ManageArea3,ManageCountNew,ManageAreaNew,AllArea,ProUseArea1,ProUseArea2,ProUseArea3,ProUseRoomNum1,ProUseRoomNum2,ProUseRoomNum3,ParkAllNum,ParkSaleNum,ParkLeaseNum,ParkEmptyNum,CommKindRoomNum,CommKindArea1,CommKindCount1,CommKindRoomNum1,CommKindArea2,CommKindCount2,CommKindRoomNum2,ChargeRateTheMonth,ChargeRateTheYear,ChargeRateLastYear,ChargeRateTheMonth1,ChargeRateTheYear1,ChargeRateLastYear1,ChargeRateTheMonth2,ChargeRateTheYear2,ChargeRateLastYear2,IncidCustHasNum,IncidCustNoNum,IncidCustDayRate,IncidCustMonthRate,IncidCustYearRate,IncidCustAllNoNum,IncidCustDayNoNum,IncidCustWeekNoNum,IncidCustMonthNoNum,IncidCompDayNum,IncidCompMonthNum,IncidCompYearNum,CurFeesRate_Deno,CurFeesRate_Mole,CurYearFeesRate_Deno,CurYearFeesRate_Mole,BefLastFeesRate_Deno,BefLastFeesRate_Mole,IncidFinlishDayNum,IncidHappenDayNum,IncidFinlishMonthNum,IncidHappenMonthNum,IncidFinlishYearNum,IncidHappenYearNum ");
			strSql.Append(" FROM Tb_Sys_TakePicSec ");
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
			strSql.Append(" StatID,StatType,CommID,OrganCode,StatDate,ManageCount,ManageArea,ManageCount1,ManageArea1,ManageCount2,ManageArea2,ManageCount3,ManageArea3,ManageCountNew,ManageAreaNew,AllArea,ProUseArea1,ProUseArea2,ProUseArea3,ProUseRoomNum1,ProUseRoomNum2,ProUseRoomNum3,ParkAllNum,ParkSaleNum,ParkLeaseNum,ParkEmptyNum,CommKindRoomNum,CommKindArea1,CommKindCount1,CommKindRoomNum1,CommKindArea2,CommKindCount2,CommKindRoomNum2,ChargeRateTheMonth,ChargeRateTheYear,ChargeRateLastYear,ChargeRateTheMonth1,ChargeRateTheYear1,ChargeRateLastYear1,ChargeRateTheMonth2,ChargeRateTheYear2,ChargeRateLastYear2,IncidCustHasNum,IncidCustNoNum,IncidCustDayRate,IncidCustMonthRate,IncidCustYearRate,IncidCustAllNoNum,IncidCustDayNoNum,IncidCustWeekNoNum,IncidCustMonthNoNum,IncidCompDayNum,IncidCompMonthNum,IncidCompYearNum,CurFeesRate_Deno,CurFeesRate_Mole,CurYearFeesRate_Deno,CurYearFeesRate_Mole,BefLastFeesRate_Deno,BefLastFeesRate_Mole,IncidFinlishDayNum,IncidHappenDayNum,IncidFinlishMonthNum,IncidHappenMonthNum,IncidFinlishYearNum,IncidHappenYearNum ");
			strSql.Append(" FROM Tb_Sys_TakePicSec ");
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_TakePicSec WHERE 1=1 " + StrCondition;
			parameters[6].Value = "StatID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

