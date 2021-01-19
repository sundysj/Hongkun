using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_CommunityInfo 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_CommunityInfo
	{
		public Tb_HSPR_CommunityInfo()
		{}
		#region Model
		private long _infoid;
		private int _commid;
		private string _heading;
		private DateTime? _issuedate;
		private DateTime? _showenddate;
		private string _infosource;
		private string _infotype;
		private int? _isaudit;
		private string _infocontent;
		private string _imageurl;
		private Guid _comminfosynchcode;
		private int? _synchflag;
		/// <summary>
		/// 
		/// </summary>
		public long InfoID
		{
			set{ _infoid=value;}
			get{return _infoid;}
		}
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
		public string Heading
		{
			set{ _heading=value;}
			get{return _heading;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? IssueDate
		{
			set{ _issuedate=value;}
			get{return _issuedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ShowEndDate
		{
			set{ _showenddate=value;}
			get{return _showenddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InfoSource
		{
			set{ _infosource=value;}
			get{return _infosource;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InfoType
		{
			set{ _infotype=value;}
			get{return _infotype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsAudit
		{
			set{ _isaudit=value;}
			get{return _isaudit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InfoContent
		{
			set{ _infocontent=value;}
			get{return _infocontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid CommInfoSynchCode
		{
			set{ _comminfosynchcode=value;}
			get{return _comminfosynchcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SynchFlag
		{
			set{ _synchflag=value;}
			get{return _synchflag;}
		}
		#endregion Model

	}
}

