using System;
namespace ehome.Model.Unify
{
	/// <summary>
	/// 实体类Tb_Unify_Community 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Unify_Community
	{
		public Tb_Unify_Community()
		{}
		#region Model
		private Guid _commsynchcode;
		private Guid _corpsynchcode;
		private long _uncommid;
		private int? _commid;
		private string _commname;
		private string _commkind;
		private DateTime? _managetime;
		private string _managekind;
		private int? _provinceid;
		private int? _cityid;
		private int? _boroughid;
		private string _street;
		private string _gatesign;
		private string _commaddress;
		private string _commpost;
		private DateTime? _regdate;
		private int? _isdelete;
		private int? _synchflag;
		private int? _commsnum;
		private string _communityname;
		private string _communitycode;
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
		public Guid CorpSynchCode
		{
			set{ _corpsynchcode=value;}
			get{return _corpsynchcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long UnCommID
		{
			set{ _uncommid=value;}
			get{return _uncommid;}
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
		public string CommName
		{
			set{ _commname=value;}
			get{return _commname;}
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
		public string CommAddress
		{
			set{ _commaddress=value;}
			get{return _commaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CommPost
		{
			set{ _commpost=value;}
			get{return _commpost;}
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
		public int? SynchFlag
		{
			set{ _synchflag=value;}
			get{return _synchflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CommSNum
		{
			set{ _commsnum=value;}
			get{return _commsnum;}
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

