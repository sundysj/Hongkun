using System;
namespace MobileSoft.Model.Unify
{
	/// <summary>
	/// 实体类Tb_Unify_Customer 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Unify_Customer
	{
		public Tb_Unify_Customer()
		{}
		#region Model
		private Guid _custsynchcode;
		private Guid _commsynchcode;
		private long _uncustid;
		private string _custtypename;
		private string _custname;
		private string _nickname;
		private string _fixedtel;
		private string _mobilephone;
		private string _faxtel;
		private string _email;
		private string _loginpwd;
		private string _surname;
		private string _name;
		private string _sex;
		private DateTime? _birthday;
		private string _nationality;
		private string _papername;
		private string _papercode;
		private string _passsign;
		private string _address;
		private string _postcode;
		private string _mgradesign;
		private string _recipient;
		private string _interests;
		private string _hobbies;
		private string _workunit;
		private string _job;
		private string _linkman;
		private string _linkmantel;
		private string _smstel;
		private int? _isunit;
		private string _legalrepr;
		private string _legalreprtel;
		private string _charge;
		private string _chargetel;
		private int? _provinceid;
		private int? _cityid;
		private int? _boroughid;
		private int? _roomcount;
		private string _roomsigns;
		private string _memo;
		private int? _livetype1;
		private int? _livetype2;
		private int? _livetype3;
		private int? _isdebts;
		private int? _isusual;
		private int? _isdelete;
		private int? _synchflag;
		private string _headimgurl;
		private string _introduce;
		private string _hometown;
		private string _biranimal;
		private string _constellation;
		private string _bloodgroup;
		private string _stature;
		private string _bodilyform;
		private string _maritalstatus;
		private string _qualification;
		private string _graduateschool;
		private string _occupation;
		private string _telphone;
		private string _mobiletel;
		private string _emailsign;
		private string _homepage;
		private string _qqsign;
		private string _msnsign;
		private string _broadbandnum;
		private string _telefixedtel;
		private string _telemobile;
		private string _iptvaccount;
		private int? _totalposts;
		private int? _experience;
		private string _custcardsign;
		private DateTime? _cardsenddate;
		private DateTime? _cardactivedate;
		private DateTime? _cardlossdates;
		private int? _iscardsend;
		private int? _iscardactive;
		private int? _iscardloss;
		private long _ordsnum;
		private string _wanqinid;
		private string _wanqinpass;
		private string _shipingid;
		private string _shipingpass;
		private string _zhilenid;
		private string _zhilenpass;
		private string _watercardnum;
		private string _watercardpass;
		private string _electricitynum;
		private string _electricitypass;
		private string _gasnum;
		private string _gaspass;
		/// <summary>
		/// 
		/// </summary>
		public Guid CustSynchCode
		{
			set{ _custsynchcode=value;}
			get{return _custsynchcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid CommSynchCode
		{
			set{ _commsynchcode=value;}
			get{return _commsynchcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long UnCustID
		{
			set{ _uncustid=value;}
			get{return _uncustid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustTypeName
		{
			set{ _custtypename=value;}
			get{return _custtypename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustName
		{
			set{ _custname=value;}
			get{return _custname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NickName
		{
			set{ _nickname=value;}
			get{return _nickname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FixedTel
		{
			set{ _fixedtel=value;}
			get{return _fixedtel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MobilePhone
		{
			set{ _mobilephone=value;}
			get{return _mobilephone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FaxTel
		{
			set{ _faxtel=value;}
			get{return _faxtel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EMail
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LoginPwd
		{
			set{ _loginpwd=value;}
			get{return _loginpwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Surname
		{
			set{ _surname=value;}
			get{return _surname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Birthday
		{
			set{ _birthday=value;}
			get{return _birthday;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Nationality
		{
			set{ _nationality=value;}
			get{return _nationality;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PaperName
		{
			set{ _papername=value;}
			get{return _papername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PaperCode
		{
			set{ _papercode=value;}
			get{return _papercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PassSign
		{
			set{ _passsign=value;}
			get{return _passsign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PostCode
		{
			set{ _postcode=value;}
			get{return _postcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MGradeSign
		{
			set{ _mgradesign=value;}
			get{return _mgradesign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Recipient
		{
			set{ _recipient=value;}
			get{return _recipient;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Interests
		{
			set{ _interests=value;}
			get{return _interests;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Hobbies
		{
			set{ _hobbies=value;}
			get{return _hobbies;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WorkUnit
		{
			set{ _workunit=value;}
			get{return _workunit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Job
		{
			set{ _job=value;}
			get{return _job;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Linkman
		{
			set{ _linkman=value;}
			get{return _linkman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LinkmanTel
		{
			set{ _linkmantel=value;}
			get{return _linkmantel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SmsTel
		{
			set{ _smstel=value;}
			get{return _smstel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsUnit
		{
			set{ _isunit=value;}
			get{return _isunit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LegalRepr
		{
			set{ _legalrepr=value;}
			get{return _legalrepr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LegalReprTel
		{
			set{ _legalreprtel=value;}
			get{return _legalreprtel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Charge
		{
			set{ _charge=value;}
			get{return _charge;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ChargeTel
		{
			set{ _chargetel=value;}
			get{return _chargetel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ProvinceID
		{
			set{ _provinceid=value;}
			get{return _provinceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CityID
		{
			set{ _cityid=value;}
			get{return _cityid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BoroughID
		{
			set{ _boroughid=value;}
			get{return _boroughid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RoomCount
		{
			set{ _roomcount=value;}
			get{return _roomcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RoomSigns
		{
			set{ _roomsigns=value;}
			get{return _roomsigns;}
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
		public int? LiveType1
		{
			set{ _livetype1=value;}
			get{return _livetype1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LiveType2
		{
			set{ _livetype2=value;}
			get{return _livetype2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LiveType3
		{
			set{ _livetype3=value;}
			get{return _livetype3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDebts
		{
			set{ _isdebts=value;}
			get{return _isdebts;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsUsual
		{
			set{ _isusual=value;}
			get{return _isusual;}
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
		public int? SynchFlag
		{
			set{ _synchflag=value;}
			get{return _synchflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HeadImgURL
		{
			set{ _headimgurl=value;}
			get{return _headimgurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Introduce
		{
			set{ _introduce=value;}
			get{return _introduce;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Hometown
		{
			set{ _hometown=value;}
			get{return _hometown;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BirAnimal
		{
			set{ _biranimal=value;}
			get{return _biranimal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Constellation
		{
			set{ _constellation=value;}
			get{return _constellation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BloodGroup
		{
			set{ _bloodgroup=value;}
			get{return _bloodgroup;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Stature
		{
			set{ _stature=value;}
			get{return _stature;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BodilyForm
		{
			set{ _bodilyform=value;}
			get{return _bodilyform;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MaritalStatus
		{
			set{ _maritalstatus=value;}
			get{return _maritalstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Qualification
		{
			set{ _qualification=value;}
			get{return _qualification;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GraduateSchool
		{
			set{ _graduateschool=value;}
			get{return _graduateschool;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Occupation
		{
			set{ _occupation=value;}
			get{return _occupation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Telphone
		{
			set{ _telphone=value;}
			get{return _telphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MobileTel
		{
			set{ _mobiletel=value;}
			get{return _mobiletel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmailSign
		{
			set{ _emailsign=value;}
			get{return _emailsign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HomePage
		{
			set{ _homepage=value;}
			get{return _homepage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string QQSign
		{
			set{ _qqsign=value;}
			get{return _qqsign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MSNSign
		{
			set{ _msnsign=value;}
			get{return _msnsign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BroadbandNum
		{
			set{ _broadbandnum=value;}
			get{return _broadbandnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TeleFixedTel
		{
			set{ _telefixedtel=value;}
			get{return _telefixedtel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TeleMobile
		{
			set{ _telemobile=value;}
			get{return _telemobile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IPTVAccount
		{
			set{ _iptvaccount=value;}
			get{return _iptvaccount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TotalPosts
		{
			set{ _totalposts=value;}
			get{return _totalposts;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Experience
		{
			set{ _experience=value;}
			get{return _experience;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustCardSign
		{
			set{ _custcardsign=value;}
			get{return _custcardsign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CardSendDate
		{
			set{ _cardsenddate=value;}
			get{return _cardsenddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CardActiveDate
		{
			set{ _cardactivedate=value;}
			get{return _cardactivedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CardLossDates
		{
			set{ _cardlossdates=value;}
			get{return _cardlossdates;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsCardSend
		{
			set{ _iscardsend=value;}
			get{return _iscardsend;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsCardActive
		{
			set{ _iscardactive=value;}
			get{return _iscardactive;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsCardLoss
		{
			set{ _iscardloss=value;}
			get{return _iscardloss;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long OrdSNum
		{
			set{ _ordsnum=value;}
			get{return _ordsnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WanQinId
		{
			set{ _wanqinid=value;}
			get{return _wanqinid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WanQinPass
		{
			set{ _wanqinpass=value;}
			get{return _wanqinpass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShiPingId
		{
			set{ _shipingid=value;}
			get{return _shipingid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShiPingPass
		{
			set{ _shipingpass=value;}
			get{return _shipingpass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZhiLenId
		{
			set{ _zhilenid=value;}
			get{return _zhilenid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZhiLenPass
		{
			set{ _zhilenpass=value;}
			get{return _zhilenpass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WaterCardNum
		{
			set{ _watercardnum=value;}
			get{return _watercardnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WaterCardPass
		{
			set{ _watercardpass=value;}
			get{return _watercardpass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ElectricityNum
		{
			set{ _electricitynum=value;}
			get{return _electricitynum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ElectricityPass
		{
			set{ _electricitypass=value;}
			get{return _electricitypass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GasNum
		{
			set{ _gasnum=value;}
			get{return _gasnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GasPass
		{
			set{ _gaspass=value;}
			get{return _gaspass;}
		}
		#endregion Model

	}
}

