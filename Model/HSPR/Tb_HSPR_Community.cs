using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_Community 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_Community
	{
		public Tb_HSPR_Community()
		{}
		#region Model
		private int _commid;
		private int? _branchid;
		private int? _corpid;
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
		private string _commkind;
		private DateTime? _managetime;
		private string _managekind;
		private Guid _commsynchcode;
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
		/// <summary>
		/// 
		/// </summary>
		public int CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BranchID
		{
			set{ _branchid=value;}
			get{return _branchid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CorpID
		{
			set{ _corpid=value;}
			get{return _corpid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CommName
		{
			set{ _commname=value;}
			get{return _commname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CommAddress
		{
			set{ _commaddress=value;}
			get{return _commaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Province
		{
			set{ _province=value;}
			get{return _province;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string City
		{
			set{ _city=value;}
			get{return _city;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Borough
		{
			set{ _borough=value;}
			get{return _borough;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Street
		{
			set{ _street=value;}
			get{return _street;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GateSign
		{
			set{ _gatesign=value;}
			get{return _gatesign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CorpGroupCode
		{
			set{ _corpgroupcode=value;}
			get{return _corpgroupcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CorpRegionCode
		{
			set{ _corpregioncode=value;}
			get{return _corpregioncode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CommSpell
		{
			set{ _commspell=value;}
			get{return _commspell;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RegDate
		{
			set{ _regdate=value;}
			get{return _regdate;}
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
		public string OrganCode
		{
			set{ _organcode=value;}
			get{return _organcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UseBeginDate
		{
			set{ _usebegindate=value;}
			get{return _usebegindate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UseEndDate
		{
			set{ _useenddate=value;}
			get{return _useenddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CommKind
		{
			set{ _commkind=value;}
			get{return _commkind;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ManageTime
		{
			set{ _managetime=value;}
			get{return _managetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ManageKind
		{
			set{ _managekind=value;}
			get{return _managekind;}
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
		public int? SynchFlag
		{
			set{ _synchflag=value;}
			get{return _synchflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ContractBeginDate
		{
			set{ _contractbegindate=value;}
			get{return _contractbegindate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ContractEndDate
		{
			set{ _contractenddate=value;}
			get{return _contractenddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SysStartDate
		{
			set{ _sysstartdate=value;}
			get{return _sysstartdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SysLogDate
		{
			set{ _syslogdate=value;}
			get{return _syslogdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsFees
		{
			set{ _isfees=value;}
			get{return _isfees;}
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
		public int? CommType
		{
			set{ _commtype=value;}
			get{return _commtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Num
		{
			set{ _num=value;}
			get{return _num;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CommunityName
		{
			set{ _communityname=value;}
			get{return _communityname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CommunityCode
		{
			set{ _communitycode=value;}
			get{return _communitycode;}
		}
		#endregion Model

	}
}

