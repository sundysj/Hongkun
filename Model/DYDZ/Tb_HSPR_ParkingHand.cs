using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MobileSoft.Model.DYDZ
{
	/// <summary>
	/// 实体类Tb_HSPR_ParkingHand 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_ParkingHand
	{
		public Tb_HSPR_ParkingHand()
		{}
		#region Model
		private string _businessid;
		private long _handid;
		private string _commid;
		private string _parkid;
		private string _custid;
		private string _roomid;
		private DateTime? _applydate;
		private string _infosource;
		private string _useproperty;
		private DateTime? _parkstartdate;
		private DateTime? _parkenddate;
		private string _payperiod;
		private DateTime? _nextpaydate;
		private string _rentmode;
		private string _contact;
		private string _handling;
		private DateTime? _handdate;
		private int? _isdelete;
		private int? _issubmit;
		private string _parkingcarsign;
		private string _carsign;
		private string _cartype;
		private string _facbrands;
		private string _carcolor;
		private string _caremission;
		private string _phone;
		private int? _isuse;
		private int? _isinput;
		private DateTime? _nextdate;
		private Guid _handsynchcode;
		private int? _synchflag;
		private int? _islisting;
		private DateTime? _listingdata;
		private string _time;
		[DisplayName("")]
		public string BusinessID
		{
			set{ _businessid=value;}
			get{return _businessid;}
		}
		[DisplayName("")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public long HandID
		{
			set{ _handid=value;}
			get{return _handid;}
		}
		[DisplayName("")]
		public string CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		[DisplayName("")]
		public string ParkID
		{
			set{ _parkid=value;}
			get{return _parkid;}
		}
		[DisplayName("")]
		public string CustID
		{
			set{ _custid=value;}
			get{return _custid;}
		}
		[DisplayName("")]
		public string RoomID
		{
			set{ _roomid=value;}
			get{return _roomid;}
		}
		[DisplayName("")]
		public DateTime? ApplyDate
		{
			set{ _applydate=value;}
			get{return _applydate;}
		}
		[DisplayName("")]
		public string InfoSource
		{
			set{ _infosource=value;}
			get{return _infosource;}
		}
		[DisplayName("")]
		public string UseProperty
		{
			set{ _useproperty=value;}
			get{return _useproperty;}
		}
		[DisplayName("")]
		public DateTime? ParkStartDate
		{
			set{ _parkstartdate=value;}
			get{return _parkstartdate;}
		}
		[DisplayName("")]
		public DateTime? ParkEndDate
		{
			set{ _parkenddate=value;}
			get{return _parkenddate;}
		}
		[DisplayName("")]
		public string PayPeriod
		{
			set{ _payperiod=value;}
			get{return _payperiod;}
		}
		[DisplayName("")]
		public DateTime? NextPayDate
		{
			set{ _nextpaydate=value;}
			get{return _nextpaydate;}
		}
		[DisplayName("")]
		public string RentMode
		{
			set{ _rentmode=value;}
			get{return _rentmode;}
		}
		[DisplayName("")]
		public string Contact
		{
			set{ _contact=value;}
			get{return _contact;}
		}
		[DisplayName("")]
		public string handling
		{
			set{ _handling=value;}
			get{return _handling;}
		}
		[DisplayName("")]
		public DateTime? HandDate
		{
			set{ _handdate=value;}
			get{return _handdate;}
		}
		[DisplayName("")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		[DisplayName("")]
		public int? IsSubmit
		{
			set{ _issubmit=value;}
			get{return _issubmit;}
		}
		[DisplayName("")]
		public string ParkingCarSign
		{
			set{ _parkingcarsign=value;}
			get{return _parkingcarsign;}
		}
		[DisplayName("")]
		public string CarSign
		{
			set{ _carsign=value;}
			get{return _carsign;}
		}
		[DisplayName("")]
		public string CarType
		{
			set{ _cartype=value;}
			get{return _cartype;}
		}
		[DisplayName("")]
		public string FacBrands
		{
			set{ _facbrands=value;}
			get{return _facbrands;}
		}
		[DisplayName("")]
		public string CarColor
		{
			set{ _carcolor=value;}
			get{return _carcolor;}
		}
		[DisplayName("")]
		public string CarEmission
		{
			set{ _caremission=value;}
			get{return _caremission;}
		}
		[DisplayName("")]
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		[DisplayName("")]
		public int? IsUse
		{
			set{ _isuse=value;}
			get{return _isuse;}
		}
		[DisplayName("")]
		public int? IsInput
		{
			set{ _isinput=value;}
			get{return _isinput;}
		}
		[DisplayName("")]
		public DateTime? NextDate
		{
			set{ _nextdate=value;}
			get{return _nextdate;}
		}
		[DisplayName("")]
		public Guid HandSynchCode
		{
			set{ _handsynchcode=value;}
			get{return _handsynchcode;}
		}
		[DisplayName("")]
		public int? SynchFlag
		{
			set{ _synchflag=value;}
			get{return _synchflag;}
		}
		[DisplayName("")]
		public int? IsListing
		{
			set{ _islisting=value;}
			get{return _islisting;}
		}
		[DisplayName("")]
		public DateTime? ListingData
		{
			set{ _listingdata=value;}
			get{return _listingdata;}
		}
		[DisplayName("")]
		public string time
		{
			set{ _time=value;}
			get{return _time;}
		}
		#endregion Model

	}
}

