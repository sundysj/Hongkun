using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_Customer 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_Customer
	{
		public Tb_HSPR_Customer()
		{}
		#region Model
		private long _custid;
		private int? _commid;
		private long _custtypeid;
		private string _custname;
		private string _fixedtel;
		private string _mobilephone;
		private string _faxtel;
		private string _email;
		private string _surname;
		private string _name;
		private string _sex;
		private DateTime? _birthday;
		private string _nationality;
		private string _workunit;
		private string _papername;
		private string _papercode;
		private string _passsign;
		private string _legalrepr;
		private string _legalreprtel;
		private string _charge;
		private string _chargetel;
		private string _linkman;
		private string _linkmantel;
		private string _bankname;
		private string _bankids;
		private string _bankaccount;
		private string _bankagreement;
		private string _inquirepwd;
		private string _inquireaccount;
		private string _memo;
		private int? _isunit;
		private int? _isdelete;
		private string _address;
		private string _postcode;
		private string _recipient;
		private string _hobbies;
		private int? _livetype1;
		private int? _livetype2;
		private int? _livetype3;
		private int? _isdebts;
		private int? _isusual;
		private string _roomsigns;
		private int? _roomcount;
		private string _job;
		private string _mgradesign;
		private string _bankcode;
		private string _bankno;
		private Guid _custsynchcode;
		private int? _synchflag;
		private DateTime? _sendcarddate;
		private int? _issendcard;
		private long _uncustid;
		private int? _isselfpay;
		private string _payenpass;
		private long _personrole;
		private string _organizecode;
		private DateTime? _joindate;
		private int? _ishostel;
		private string _bindmobile;
		private int? _custbedlivestate;
		private long _custbedlivenum;
		private DateTime? _custbedlivedate;
		private DateTime? _custbedexitdate;
		private DateTime? _registerdate;
		private long _arrearssubid;
		private string _bankprovince;
		private string _bankcity;
		private string _bankinfo;
		/// <summary>
		/// 
		/// </summary>
		public long CustID
		{
			set{ _custid=value;}
			get{return _custid;}
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
		public long CustTypeID
		{
			set{ _custtypeid=value;}
			get{return _custtypeid;}
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
		public string WorkUnit
		{
			set{ _workunit=value;}
			get{return _workunit;}
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
		public string BankName
		{
			set{ _bankname=value;}
			get{return _bankname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BankIDs
		{
			set{ _bankids=value;}
			get{return _bankids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BankAccount
		{
			set{ _bankaccount=value;}
			get{return _bankaccount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BankAgreement
		{
			set{ _bankagreement=value;}
			get{return _bankagreement;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InquirePwd
		{
			set{ _inquirepwd=value;}
			get{return _inquirepwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InquireAccount
		{
			set{ _inquireaccount=value;}
			get{return _inquireaccount;}
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
		public int? IsUnit
		{
			set{ _isunit=value;}
			get{return _isunit;}
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
		public string Recipient
		{
			set{ _recipient=value;}
			get{return _recipient;}
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
		public string RoomSigns
		{
			set{ _roomsigns=value;}
			get{return _roomsigns;}
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
		public string Job
		{
			set{ _job=value;}
			get{return _job;}
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
		public string BankCode
		{
			set{ _bankcode=value;}
			get{return _bankcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BankNo
		{
			set{ _bankno=value;}
			get{return _bankno;}
		}
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
		public int? SynchFlag
		{
			set{ _synchflag=value;}
			get{return _synchflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SendCardDate
		{
			set{ _sendcarddate=value;}
			get{return _sendcarddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsSendCard
		{
			set{ _issendcard=value;}
			get{return _issendcard;}
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
		public int? IsSelfPay
		{
			set{ _isselfpay=value;}
			get{return _isselfpay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PayEnPass
		{
			set{ _payenpass=value;}
			get{return _payenpass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long PersonRole
		{
			set{ _personrole=value;}
			get{return _personrole;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrganizeCode
		{
			set{ _organizecode=value;}
			get{return _organizecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? JoinDate
		{
			set{ _joindate=value;}
			get{return _joindate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsHostel
		{
			set{ _ishostel=value;}
			get{return _ishostel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BindMobile
		{
			set{ _bindmobile=value;}
			get{return _bindmobile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CustBedLiveState
		{
			set{ _custbedlivestate=value;}
			get{return _custbedlivestate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long CustBedLiveNum
		{
			set{ _custbedlivenum=value;}
			get{return _custbedlivenum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CustBedLiveDate
		{
			set{ _custbedlivedate=value;}
			get{return _custbedlivedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CustBedExitDate
		{
			set{ _custbedexitdate=value;}
			get{return _custbedexitdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RegisterDate
		{
			set{ _registerdate=value;}
			get{return _registerdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ArrearsSubID
		{
			set{ _arrearssubid=value;}
			get{return _arrearssubid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BankProvince
		{
			set{ _bankprovince=value;}
			get{return _bankprovince;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BankCity
		{
			set{ _bankcity=value;}
			get{return _bankcity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BankInfo
		{
			set{ _bankinfo=value;}
			get{return _bankinfo;}
		}
		#endregion Model

	}
}

