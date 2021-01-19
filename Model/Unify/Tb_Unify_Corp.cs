using System;
namespace MobileSoft.Model.Unify
{
	/// <summary>
	/// 实体类Tb_Unify_Corp 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Unify_Corp
	{
		public Tb_Unify_Corp()
		{}
		#region Model
		private Guid _corpsynchcode;
		private long _uncorpid;
		private int? _vspid;
		private int? _corpid;
		private string _corpname;
		private string _province;
		private string _city;
		private string _borough;
		private int? _regmode;
		private string _corptypename;
		private string _corpaddress;
		private string _corppost;
		private string _corpdeputy;
		private string _corplinkman;
		private string _corpmobiletel;
		private string _corpworkedtel;
		private string _corpfax;
		private string _corpemail;
		private string _corpweb;
		private DateTime? _regdate;
		private string _sysversion;
		private int? _isdelete;
		private int? _synchflag;
		private string _serverip;
		private string _sysdir;
		private int? _corpsort;
		private int? _isrecommend;
		private string _recommendindex;
		private string _logoimgurl;
		private string _corpshortname;
		private int? _corpsnum;
		private string _street;
		private string _communityname;
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
		public long UnCorpID
		{
			set{ _uncorpid=value;}
			get{return _uncorpid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? VSPID
		{
			set{ _vspid=value;}
			get{return _vspid;}
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
		public string CorpName
		{
			set{ _corpname=value;}
			get{return _corpname;}
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
		public int? RegMode
		{
			set{ _regmode=value;}
			get{return _regmode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CorpTypeName
		{
			set{ _corptypename=value;}
			get{return _corptypename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CorpAddress
		{
			set{ _corpaddress=value;}
			get{return _corpaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CorpPost
		{
			set{ _corppost=value;}
			get{return _corppost;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CorpDeputy
		{
			set{ _corpdeputy=value;}
			get{return _corpdeputy;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CorpLinkMan
		{
			set{ _corplinkman=value;}
			get{return _corplinkman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CorpMobileTel
		{
			set{ _corpmobiletel=value;}
			get{return _corpmobiletel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CorpWorkedTel
		{
			set{ _corpworkedtel=value;}
			get{return _corpworkedtel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CorpFax
		{
			set{ _corpfax=value;}
			get{return _corpfax;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CorpEmail
		{
			set{ _corpemail=value;}
			get{return _corpemail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CorpWeb
		{
			set{ _corpweb=value;}
			get{return _corpweb;}
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
		public string SysVersion
		{
			set{ _sysversion=value;}
			get{return _sysversion;}
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
		public string ServerIP
		{
			set{ _serverip=value;}
			get{return _serverip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SysDir
		{
			set{ _sysdir=value;}
			get{return _sysdir;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CorpSort
		{
			set{ _corpsort=value;}
			get{return _corpsort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsRecommend
		{
			set{ _isrecommend=value;}
			get{return _isrecommend;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RecommendIndex
		{
			set{ _recommendindex=value;}
			get{return _recommendindex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LogoImgUrl
		{
			set{ _logoimgurl=value;}
			get{return _logoimgurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CorpShortName
		{
			set{ _corpshortname=value;}
			get{return _corpshortname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CorpSNum
		{
			set{ _corpsnum=value;}
			get{return _corpsnum;}
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
		public string CommunityName
		{
			set{ _communityname=value;}
			get{return _communityname;}
		}
		#endregion Model

	}
}

