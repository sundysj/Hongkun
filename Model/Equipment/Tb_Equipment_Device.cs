using System;
namespace MobileSoft.Model.Equipment
{
	/// <summary>
	/// 实体类Tb_Equipment_Device 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Equipment_Device
	{
		public Tb_Equipment_Device()
		{}
		#region Model
		private long _deviceid;
		private int? _commid;
		private long _devicetypeid;
		private string _devicemodel;
		private string _setplace;
		private string _deviceorigin;
		private string _deviceprice;
		private decimal? _depreciationrate;
		private int? _depreciationperiod;
		private string _uselife;
		private DateTime? _factorytime;
		private string _factorynumber;
		private string _productionstard;
		private DateTime? _usetime;
		private string _devicesign;
		private string _cardnumber;
		private string _responsible;
		private string _prepareaccounts;
		private DateTime? _preparetime;
		private string _memo;
		private string _masteroutputpower;
		private string _masterinputpower;
		private string _mastergeometry;
		private string _masterenergyconsumption;
		private string _masterother;
		private string _motorpower;
		private string _motorratedvoltage;
		private string _motorratedcurrent;
		private string _motororigin;
		private string _motorother;
		private string _controlgeometry;
		private string _controlratedvoltage;
		private string _controlratedcurrent;
		private string _controlorigin;
		private string _controlother;
		private string _factoryinformation1;
		private string _factoryinformation2;
		private string _factoryinformation3;
		private string _testdata1;
		private string _testdata2;
		private string _testdata3;
		private string _manuname;
		private string _manulinkman;
		private string _manulinktel;
		private string _dealername;
		private string _dealerlinkman;
		private string _dealerlinktel;
		private string _erectname;
		private string _erectlinkman;
		private string _erectlinktel;
		private int? _isdelete;
		private string _identification;
		private long _genreid;
		private long _genreunitid;
		private string _genrelinktel;
		private DateTime? _genrestartdate;
		private DateTime? _genrecalcbegindate;
		private int? _genreperiod;
		private int? _genretimes;
		private int? _fixgenremonth1;
		private int? _fixgenreday1;
		private int? _fixgenremonth2;
		private int? _fixgenreday2;
		private int? _fixgenremonth3;
		private int? _fixgenreday3;
		private int? _fixgenremonth4;
		private int? _fixgenreday4;
		private string _genrereason;
		private int? _devicestatus;
		/// <summary>
		/// 
		/// </summary>
		public long DeviceID
		{
			set{ _deviceid=value;}
			get{return _deviceid;}
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
		public long DeviceTypeID
		{
			set{ _devicetypeid=value;}
			get{return _devicetypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DeviceModel
		{
			set{ _devicemodel=value;}
			get{return _devicemodel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SetPlace
		{
			set{ _setplace=value;}
			get{return _setplace;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DeviceOrigin
		{
			set{ _deviceorigin=value;}
			get{return _deviceorigin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DevicePrice
		{
			set{ _deviceprice=value;}
			get{return _deviceprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? DepreciationRate
		{
			set{ _depreciationrate=value;}
			get{return _depreciationrate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DepreciationPeriod
		{
			set{ _depreciationperiod=value;}
			get{return _depreciationperiod;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UseLife
		{
			set{ _uselife=value;}
			get{return _uselife;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FactoryTime
		{
			set{ _factorytime=value;}
			get{return _factorytime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FactoryNumber
		{
			set{ _factorynumber=value;}
			get{return _factorynumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProductionStard
		{
			set{ _productionstard=value;}
			get{return _productionstard;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UseTime
		{
			set{ _usetime=value;}
			get{return _usetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DeviceSign
		{
			set{ _devicesign=value;}
			get{return _devicesign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CardNumber
		{
			set{ _cardnumber=value;}
			get{return _cardnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Responsible
		{
			set{ _responsible=value;}
			get{return _responsible;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PrepareAccounts
		{
			set{ _prepareaccounts=value;}
			get{return _prepareaccounts;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? PrepareTime
		{
			set{ _preparetime=value;}
			get{return _preparetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MasterOutputPower
		{
			set{ _masteroutputpower=value;}
			get{return _masteroutputpower;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MasterInputPower
		{
			set{ _masterinputpower=value;}
			get{return _masterinputpower;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MasterGeometry
		{
			set{ _mastergeometry=value;}
			get{return _mastergeometry;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MasterEnergyConsumption
		{
			set{ _masterenergyconsumption=value;}
			get{return _masterenergyconsumption;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MasterOther
		{
			set{ _masterother=value;}
			get{return _masterother;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MotorPower
		{
			set{ _motorpower=value;}
			get{return _motorpower;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MotorRatedVoltage
		{
			set{ _motorratedvoltage=value;}
			get{return _motorratedvoltage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MotorRatedCurrent
		{
			set{ _motorratedcurrent=value;}
			get{return _motorratedcurrent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MotorOrigin
		{
			set{ _motororigin=value;}
			get{return _motororigin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MotorOther
		{
			set{ _motorother=value;}
			get{return _motorother;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ControlGeometry
		{
			set{ _controlgeometry=value;}
			get{return _controlgeometry;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ControlRatedVoltage
		{
			set{ _controlratedvoltage=value;}
			get{return _controlratedvoltage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ControlRatedCurrent
		{
			set{ _controlratedcurrent=value;}
			get{return _controlratedcurrent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ControlOrigin
		{
			set{ _controlorigin=value;}
			get{return _controlorigin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ControlOther
		{
			set{ _controlother=value;}
			get{return _controlother;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FactoryInformation1
		{
			set{ _factoryinformation1=value;}
			get{return _factoryinformation1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FactoryInformation2
		{
			set{ _factoryinformation2=value;}
			get{return _factoryinformation2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FactoryInformation3
		{
			set{ _factoryinformation3=value;}
			get{return _factoryinformation3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TestData1
		{
			set{ _testdata1=value;}
			get{return _testdata1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TestData2
		{
			set{ _testdata2=value;}
			get{return _testdata2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TestData3
		{
			set{ _testdata3=value;}
			get{return _testdata3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ManuName
		{
			set{ _manuname=value;}
			get{return _manuname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ManuLinkMan
		{
			set{ _manulinkman=value;}
			get{return _manulinkman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ManuLinkTel
		{
			set{ _manulinktel=value;}
			get{return _manulinktel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DealerName
		{
			set{ _dealername=value;}
			get{return _dealername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DealerLinkMan
		{
			set{ _dealerlinkman=value;}
			get{return _dealerlinkman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DealerLinkTel
		{
			set{ _dealerlinktel=value;}
			get{return _dealerlinktel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ErectName
		{
			set{ _erectname=value;}
			get{return _erectname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ErectLinkMan
		{
			set{ _erectlinkman=value;}
			get{return _erectlinkman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ErectLinkTel
		{
			set{ _erectlinktel=value;}
			get{return _erectlinktel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Identification
		{
			set{ _identification=value;}
			get{return _identification;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long GenreID
		{
			set{ _genreid=value;}
			get{return _genreid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long GenreUnitID
		{
			set{ _genreunitid=value;}
			get{return _genreunitid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GenreLinkTel
		{
			set{ _genrelinktel=value;}
			get{return _genrelinktel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? GenreStartDate
		{
			set{ _genrestartdate=value;}
			get{return _genrestartdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? GenreCalcBeginDate
		{
			set{ _genrecalcbegindate=value;}
			get{return _genrecalcbegindate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? GenrePeriod
		{
			set{ _genreperiod=value;}
			get{return _genreperiod;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? GenreTimes
		{
			set{ _genretimes=value;}
			get{return _genretimes;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FixGenreMonth1
		{
			set{ _fixgenremonth1=value;}
			get{return _fixgenremonth1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FixGenreDay1
		{
			set{ _fixgenreday1=value;}
			get{return _fixgenreday1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FixGenreMonth2
		{
			set{ _fixgenremonth2=value;}
			get{return _fixgenremonth2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FixGenreDay2
		{
			set{ _fixgenreday2=value;}
			get{return _fixgenreday2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FixGenreMonth3
		{
			set{ _fixgenremonth3=value;}
			get{return _fixgenremonth3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FixGenreDay3
		{
			set{ _fixgenreday3=value;}
			get{return _fixgenreday3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FixGenreMonth4
		{
			set{ _fixgenremonth4=value;}
			get{return _fixgenremonth4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FixGenreDay4
		{
			set{ _fixgenreday4=value;}
			get{return _fixgenreday4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GenreReason
		{
			set{ _genrereason=value;}
			get{return _genrereason;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DeviceStatus
		{
			set{ _devicestatus=value;}
			get{return _devicestatus;}
		}
		#endregion Model

	}
}

