using System;
namespace MobileSoft.Model.Sys
{
	/// <summary>
	/// 实体类Tb_Sys_TakePic 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Sys_TakePic
	{
		public Tb_Sys_TakePic()
		{}
		#region Model
		private long _statid;
		private int? _stattype;
		private int? _commid;
		private string _organcode;
		private DateTime? _statdate;
		private decimal? _allarea;
		private int? _allroomnum;
		private int? _roomnum1;
		private int? _roomnum2;
		private int? _roomnum3;
		private int? _roomnum4;
		private int? _roomnum5;
		private int? _roomnum6;
		private decimal? _chargerate1;
		private decimal? _chargerate2;
		private decimal? _chargerate3;
		private decimal? _chargerate4;
		private int? _feescreatenum;
		private int? _feescancelnum;
		private int? _incidentnum1;
		private int? _incidentnum2;
		private int? _incidentnum3;
		private int? _incidentnum4;
		private int? _incidentnum5;
		private decimal? _incidentrate1;
		private decimal? _incidentrate2;
		private decimal? _incidentrate3;
		private decimal? _incidentrate4;
		private decimal? _meterchargerate1;
		private decimal? _meterchargerate2;
		private decimal? _meterchargerate3;
		private decimal? _meterchargerate4;
		private decimal? _curfeesrate_deno;
		private decimal? _curfeesrate_mole;
		private decimal? _curyearfeesrate_deno;
		private decimal? _curyearfeesrate_mole;
		private decimal? _beflastfeesrate_deno;
		private decimal? _beflastfeesrate_mole;
		private decimal? _curmeterrate_deno;
		private decimal? _curmeterrate_mole;
		private decimal? _curyearmeterrate_deno;
		private decimal? _curyearmeterrate_mole;
		private decimal? _beflastmeterrate_deno;
		private decimal? _beflastmeterrate_mole;
		private int? _incidfinlishmonthnum1;
		private int? _incidhappenmonthnum1;
		private int? _incidfinlishyearnum1;
		private int? _incidhappenyearnum1;
		private int? _incidfinlishmonthnum2;
		private int? _incidhappenmonthnum2;
		private int? _incidfinlishyearnum2;
		private int? _incidhappenyearnum2;
		private int? _equipmentdevicecount;
		private int? _equipmentperiodhappennum;
		private int? _equipmentperiodfinlishnum;
		private int? _equipmentperiodunfinlishnum;
		private int? _equipmenttemphappennum;
		private int? _equipmenttempfinlishnum;
		private int? _equipmenttempunfinlishnum;
		/// <summary>
		/// 
		/// </summary>
		public long StatID
		{
			set{ _statid=value;}
			get{return _statid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? StatType
		{
			set{ _stattype=value;}
			get{return _stattype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrganCode
		{
			set{ _organcode=value;}
			get{return _organcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? StatDate
		{
			set{ _statdate=value;}
			get{return _statdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? AllArea
		{
			set{ _allarea=value;}
			get{return _allarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AllRoomNum
		{
			set{ _allroomnum=value;}
			get{return _allroomnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RoomNum1
		{
			set{ _roomnum1=value;}
			get{return _roomnum1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RoomNum2
		{
			set{ _roomnum2=value;}
			get{return _roomnum2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RoomNum3
		{
			set{ _roomnum3=value;}
			get{return _roomnum3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RoomNum4
		{
			set{ _roomnum4=value;}
			get{return _roomnum4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RoomNum5
		{
			set{ _roomnum5=value;}
			get{return _roomnum5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RoomNum6
		{
			set{ _roomnum6=value;}
			get{return _roomnum6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ChargeRate1
		{
			set{ _chargerate1=value;}
			get{return _chargerate1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ChargeRate2
		{
			set{ _chargerate2=value;}
			get{return _chargerate2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ChargeRate3
		{
			set{ _chargerate3=value;}
			get{return _chargerate3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ChargeRate4
		{
			set{ _chargerate4=value;}
			get{return _chargerate4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FeesCreateNum
		{
			set{ _feescreatenum=value;}
			get{return _feescreatenum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FeesCancelNum
		{
			set{ _feescancelnum=value;}
			get{return _feescancelnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IncidentNum1
		{
			set{ _incidentnum1=value;}
			get{return _incidentnum1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IncidentNum2
		{
			set{ _incidentnum2=value;}
			get{return _incidentnum2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IncidentNum3
		{
			set{ _incidentnum3=value;}
			get{return _incidentnum3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IncidentNum4
		{
			set{ _incidentnum4=value;}
			get{return _incidentnum4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IncidentNum5
		{
			set{ _incidentnum5=value;}
			get{return _incidentnum5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? IncidentRate1
		{
			set{ _incidentrate1=value;}
			get{return _incidentrate1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? IncidentRate2
		{
			set{ _incidentrate2=value;}
			get{return _incidentrate2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? IncidentRate3
		{
			set{ _incidentrate3=value;}
			get{return _incidentrate3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? IncidentRate4
		{
			set{ _incidentrate4=value;}
			get{return _incidentrate4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? MeterChargeRate1
		{
			set{ _meterchargerate1=value;}
			get{return _meterchargerate1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? MeterChargeRate2
		{
			set{ _meterchargerate2=value;}
			get{return _meterchargerate2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? MeterChargeRate3
		{
			set{ _meterchargerate3=value;}
			get{return _meterchargerate3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? MeterChargeRate4
		{
			set{ _meterchargerate4=value;}
			get{return _meterchargerate4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CurFeesRate_Deno
		{
			set{ _curfeesrate_deno=value;}
			get{return _curfeesrate_deno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CurFeesRate_Mole
		{
			set{ _curfeesrate_mole=value;}
			get{return _curfeesrate_mole;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CurYearFeesRate_Deno
		{
			set{ _curyearfeesrate_deno=value;}
			get{return _curyearfeesrate_deno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CurYearFeesRate_Mole
		{
			set{ _curyearfeesrate_mole=value;}
			get{return _curyearfeesrate_mole;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? BefLastFeesRate_Deno
		{
			set{ _beflastfeesrate_deno=value;}
			get{return _beflastfeesrate_deno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? BefLastFeesRate_Mole
		{
			set{ _beflastfeesrate_mole=value;}
			get{return _beflastfeesrate_mole;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CurMeterRate_Deno
		{
			set{ _curmeterrate_deno=value;}
			get{return _curmeterrate_deno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CurMeterRate_Mole
		{
			set{ _curmeterrate_mole=value;}
			get{return _curmeterrate_mole;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CurYearMeterRate_Deno
		{
			set{ _curyearmeterrate_deno=value;}
			get{return _curyearmeterrate_deno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CurYearMeterRate_Mole
		{
			set{ _curyearmeterrate_mole=value;}
			get{return _curyearmeterrate_mole;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? BefLastMeterRate_Deno
		{
			set{ _beflastmeterrate_deno=value;}
			get{return _beflastmeterrate_deno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? BefLastMeterRate_Mole
		{
			set{ _beflastmeterrate_mole=value;}
			get{return _beflastmeterrate_mole;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IncidFinlishMonthNum1
		{
			set{ _incidfinlishmonthnum1=value;}
			get{return _incidfinlishmonthnum1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IncidHappenMonthNum1
		{
			set{ _incidhappenmonthnum1=value;}
			get{return _incidhappenmonthnum1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IncidFinlishYearNum1
		{
			set{ _incidfinlishyearnum1=value;}
			get{return _incidfinlishyearnum1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IncidHappenYearNum1
		{
			set{ _incidhappenyearnum1=value;}
			get{return _incidhappenyearnum1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IncidFinlishMonthNum2
		{
			set{ _incidfinlishmonthnum2=value;}
			get{return _incidfinlishmonthnum2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IncidHappenMonthNum2
		{
			set{ _incidhappenmonthnum2=value;}
			get{return _incidhappenmonthnum2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IncidFinlishYearNum2
		{
			set{ _incidfinlishyearnum2=value;}
			get{return _incidfinlishyearnum2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IncidHappenYearNum2
		{
			set{ _incidhappenyearnum2=value;}
			get{return _incidhappenyearnum2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EquipmentDeviceCount
		{
			set{ _equipmentdevicecount=value;}
			get{return _equipmentdevicecount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EquipmentPeriodHappenNum
		{
			set{ _equipmentperiodhappennum=value;}
			get{return _equipmentperiodhappennum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EquipmentPeriodFinlishNum
		{
			set{ _equipmentperiodfinlishnum=value;}
			get{return _equipmentperiodfinlishnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EquipmentPeriodUnFinlishNum
		{
			set{ _equipmentperiodunfinlishnum=value;}
			get{return _equipmentperiodunfinlishnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EquipmentTempHappenNum
		{
			set{ _equipmenttemphappennum=value;}
			get{return _equipmenttemphappennum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EquipmentTempFinlishNum
		{
			set{ _equipmenttempfinlishnum=value;}
			get{return _equipmenttempfinlishnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EquipmentTempUnFinlishNum
		{
			set{ _equipmenttempunfinlishnum=value;}
			get{return _equipmenttempunfinlishnum;}
		}
		#endregion Model

	}
}

