using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_IncidentRemindersInfo 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_IncidentRemindersInfo
	{
		public Tb_HSPR_IncidentRemindersInfo()
		{}
		#region Model
		private long _infoid;
		private long _incidentid;
		private int? _commid;
		private string _reminderstype;
		private DateTime? _remindersdate;
		private string _userid;
		private string _username;
		private string _infocontent;
		private string _remark;
		private bool _issendmessage;
		private string _sendmessage;
		private int? _isdelete;
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
		public long IncidentID
		{
			set{ _incidentid=value;}
			get{return _incidentid;}
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
		public string RemindersType
		{
			set{ _reminderstype=value;}
			get{return _reminderstype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RemindersDate
		{
			set{ _remindersdate=value;}
			get{return _remindersdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
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
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsSendMessage
		{
			set{ _issendmessage=value;}
			get{return _issendmessage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SendMessage
		{
			set{ _sendmessage=value;}
			get{return _sendmessage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

