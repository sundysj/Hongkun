using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MobileSoft.Model.DYDZ
{
	/// <summary>
	/// 实体类Tb_HSPR_CommunityHistory 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_CommunityHistory
	{
		public Tb_HSPR_CommunityHistory()
		{}
		#region Model
		private string _commid;
		private string _branchid;
		private string _corpid;
		private string _commname;
		private string _commaddress;
		private string _province;
		private string _city;
		private string _borough;
		private string _street;
		private string _gatesign;
		private string _corpgroupcode;
		private string _corpregioncode;
		private string _commspell;
		private DateTime? _regdate;
		private int? _isdelete;
		private string _organcode;
		private DateTime? _usebegindate;
		private DateTime? _useenddate;
		private string _commimgurl;
		private string _commkind;
		private DateTime? _managetime;
		private string _managekind;
		private string _commsynchcode;
		private int? _synchflag;
		private DateTime? _contractbegindate;
		private DateTime? _contractenddate;
		private DateTime? _sysstartdate;
		private DateTime? _syslogdate;
		private int? _isfees;
		private string _memo;
		private int? _commtype;
		private int? _num;
		private string _communityname;
		private string _communitycode;
		private string _crmcode;
		private string _fdno;
		private string _time;
		[DisplayName("")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		[DisplayName("")]
		public string BranchID
		{
			set{ _branchid=value;}
			get{return _branchid;}
		}
		[DisplayName("")]
		public string CorpID
		{
			set{ _corpid=value;}
			get{return _corpid;}
		}
		[DisplayName("")]
		public string CommName
		{
			set{ _commname=value;}
			get{return _commname;}
		}
		[DisplayName("")]
		public string CommAddress
		{
			set{ _commaddress=value;}
			get{return _commaddress;}
		}
		[DisplayName("")]
		public string Province
		{
			set{ _province=value;}
			get{return _province;}
		}
		[DisplayName("")]
		public string City
		{
			set{ _city=value;}
			get{return _city;}
		}
		[DisplayName("")]
		public string Borough
		{
			set{ _borough=value;}
			get{return _borough;}
		}
		[DisplayName("")]
		public string Street
		{
			set{ _street=value;}
			get{return _street;}
		}
		[DisplayName("")]
		public string GateSign
		{
			set{ _gatesign=value;}
			get{return _gatesign;}
		}
		[DisplayName("")]
		public string CorpGroupCode
		{
			set{ _corpgroupcode=value;}
			get{return _corpgroupcode;}
		}
		[DisplayName("")]
		public string CorpRegionCode
		{
			set{ _corpregioncode=value;}
			get{return _corpregioncode;}
		}
		[DisplayName("")]
		public string CommSpell
		{
			set{ _commspell=value;}
			get{return _commspell;}
		}
		[DisplayName("")]
		public DateTime? RegDate
		{
			set{ _regdate=value;}
			get{return _regdate;}
		}
		[DisplayName("")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		[DisplayName("")]
		public string OrganCode
		{
			set{ _organcode=value;}
			get{return _organcode;}
		}
		[DisplayName("")]
		public DateTime? UseBeginDate
		{
			set{ _usebegindate=value;}
			get{return _usebegindate;}
		}
		[DisplayName("")]
		public DateTime? UseEndDate
		{
			set{ _useenddate=value;}
			get{return _useenddate;}
		}
		[DisplayName("")]
		public string CommImgURL
		{
			set{ _commimgurl=value;}
			get{return _commimgurl;}
		}
		[DisplayName("")]
		public string CommKind
		{
			set{ _commkind=value;}
			get{return _commkind;}
		}
		[DisplayName("")]
		public DateTime? ManageTime
		{
			set{ _managetime=value;}
			get{return _managetime;}
		}
		[DisplayName("")]
		public string ManageKind
		{
			set{ _managekind=value;}
			get{return _managekind;}
		}
		[DisplayName("")]
		public string CommSynchCode
		{
			set{ _commsynchcode=value;}
			get{return _commsynchcode;}
		}
		[DisplayName("")]
		public int? SynchFlag
		{
			set{ _synchflag=value;}
			get{return _synchflag;}
		}
		[DisplayName("")]
		public DateTime? ContractBeginDate
		{
			set{ _contractbegindate=value;}
			get{return _contractbegindate;}
		}
		[DisplayName("")]
		public DateTime? ContractEndDate
		{
			set{ _contractenddate=value;}
			get{return _contractenddate;}
		}
		[DisplayName("")]
		public DateTime? SysStartDate
		{
			set{ _sysstartdate=value;}
			get{return _sysstartdate;}
		}
		[DisplayName("")]
		public DateTime? SysLogDate
		{
			set{ _syslogdate=value;}
			get{return _syslogdate;}
		}
		[DisplayName("")]
		public int? IsFees
		{
			set{ _isfees=value;}
			get{return _isfees;}
		}
		[DisplayName("")]
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		[DisplayName("")]
		public int? CommType
		{
			set{ _commtype=value;}
			get{return _commtype;}
		}
		[DisplayName("")]
		public int? Num
		{
			set{ _num=value;}
			get{return _num;}
		}
		[DisplayName("")]
		public string CommunityName
		{
			set{ _communityname=value;}
			get{return _communityname;}
		}
		[DisplayName("")]
		public string CommunityCode
		{
			set{ _communitycode=value;}
			get{return _communitycode;}
		}
		[DisplayName("")]
		public string CRMCode
		{
			set{ _crmcode=value;}
			get{return _crmcode;}
		}
		[DisplayName("")]
		public string fdNo
		{
			set{ _fdno=value;}
			get{return _fdno;}
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

