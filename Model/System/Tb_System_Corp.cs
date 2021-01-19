using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// 实体类Tb_System_Corp 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_System_Corp
	{
		public Tb_System_Corp()
		{}
		#region Model
		private long _corpsid;
		private int? _corpid;
		private int? _regtype;
		private int? _regmode;
		private string _corpname;
		private string _corpshortname;
		private string _corptypename;
		private string _province;
		private string _city;
		private string _borough;
		private string _street;
		private string _commname;
		private string _logincode;
		private string _loginpasswd;
		private string _pwdquestion;
		private string _pwdanswer;
		private string _corpaddress;
		private string _corppost;
		private string _corpdeputy;
		private string _corplinkman;
		private string _corpmobiletel;
		private string _corpworkedtel;
		private string _corpfax;
		private string _corpemail;
		private string _corpweb;
		private string _adminusername;
		private string _adminsex;
		private string _adminusertel;
		private string _adminuseremail;
		private string _dbserver;
		private string _dbname;
		private string _dbuser;
		private string _dbpwd;
		private DateTime? _regdate;
		private int? _branchnum;
		private int? _commnum;
		private int? _isdelete;
		private string _imglogo1;
		private string _imglogo2;
		private string _sysdir;
		private string _sysversion;
		private string _qq;
		private string _storeimage;
		private int? _vspid;
		private Guid _corpsynchcode;
		private int? _synchflag;
		private string _logoimgurl;
		private int? _corpsort;
		private int? _isrecommend;
		private string _recommendindex;
		private int? _actualcommsnum;
		private string _contractmemo;
		private int? _mobilesnum;
		private int? _actualmobilesnum;
		private string _callversion;
		private int? _isvalidcode;
		private int? _isfees;
		private DateTime? _contractbegindate;
		private DateTime? _contractenddate;
		private DateTime? _usebegindate;
		private DateTime? _useenddate;
		private DateTime? _allowancedate;
		private string _yizhifunum;
		private string _yizhifukey;
		private string _yijifuhuobanid;
		private string _yijifunum;
		private string _yijifukey;
		/// <summary>
		/// 
		/// </summary>
		public long CorpSID
		{
			set{ _corpsid=value;}
			get{return _corpsid;}
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
		public int? RegType
		{
			set{ _regtype=value;}
			get{return _regtype;}
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
		public string CorpName
		{
			set{ _corpname=value;}
			get{return _corpname;}
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
		public string CorpTypeName
		{
			set{ _corptypename=value;}
			get{return _corptypename;}
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
		public string CommName
		{
			set{ _commname=value;}
			get{return _commname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LoginCode
		{
			set{ _logincode=value;}
			get{return _logincode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LoginPassWD
		{
			set{ _loginpasswd=value;}
			get{return _loginpasswd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PwdQuestion
		{
			set{ _pwdquestion=value;}
			get{return _pwdquestion;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PwdAnswer
		{
			set{ _pwdanswer=value;}
			get{return _pwdanswer;}
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
		public string AdminUserName
		{
			set{ _adminusername=value;}
			get{return _adminusername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AdminSex
		{
			set{ _adminsex=value;}
			get{return _adminsex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AdminUserTel
		{
			set{ _adminusertel=value;}
			get{return _adminusertel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AdminUserEmail
		{
			set{ _adminuseremail=value;}
			get{return _adminuseremail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DBServer
		{
			set{ _dbserver=value;}
			get{return _dbserver;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DBName
		{
			set{ _dbname=value;}
			get{return _dbname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DBUser
		{
			set{ _dbuser=value;}
			get{return _dbuser;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DBPwd
		{
			set{ _dbpwd=value;}
			get{return _dbpwd;}
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
		public int? BranchNum
		{
			set{ _branchnum=value;}
			get{return _branchnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CommNum
		{
			set{ _commnum=value;}
			get{return _commnum;}
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
		public string ImgLogo1
		{
			set{ _imglogo1=value;}
			get{return _imglogo1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImgLogo2
		{
			set{ _imglogo2=value;}
			get{return _imglogo2;}
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
		public string SysVersion
		{
			set{ _sysversion=value;}
			get{return _sysversion;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string QQ
		{
			set{ _qq=value;}
			get{return _qq;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StoreImage
		{
			set{ _storeimage=value;}
			get{return _storeimage;}
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
		public Guid CorpSynchCode
		{
			set{ _corpsynchcode=value;}
			get{return _corpsynchcode;}
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
		public string LogoImgUrl
		{
			set{ _logoimgurl=value;}
			get{return _logoimgurl;}
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
		public int? ActualCommSnum
		{
			set{ _actualcommsnum=value;}
			get{return _actualcommsnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ContractMemo
		{
			set{ _contractmemo=value;}
			get{return _contractmemo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? MobileSnum
		{
			set{ _mobilesnum=value;}
			get{return _mobilesnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ActualMobileSnum
		{
			set{ _actualmobilesnum=value;}
			get{return _actualmobilesnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CallVersion
		{
			set{ _callversion=value;}
			get{return _callversion;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsValidCode
		{
			set{ _isvalidcode=value;}
			get{return _isvalidcode;}
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
		public DateTime? AllowanceDate
		{
			set{ _allowancedate=value;}
			get{return _allowancedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string YiZhiFuNum
		{
			set{ _yizhifunum=value;}
			get{return _yizhifunum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string YiZhiFuKey
		{
			set{ _yizhifukey=value;}
			get{return _yizhifukey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string YiJiFuHuoBanId
		{
			set{ _yijifuhuobanid=value;}
			get{return _yijifuhuobanid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string YiJiFuNum
		{
			set{ _yijifunum=value;}
			get{return _yijifunum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string YiJiFuKey
		{
			set{ _yijifukey=value;}
			get{return _yijifukey;}
		}
		#endregion Model

	}
}

