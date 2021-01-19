using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// 实体类Tb_Sys_Message 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Sys_Message
	{
		public Tb_Sys_Message()
		{}
		#region Model
		private Guid _messagecode;
		private long _cutid;
		private string _usercode;
		private string _msgtitle;
		private string _content;
		private DateTime _sendtime;
		private int _msgtype;
		private string _sendman;
		private int _msgstate;
		private int? _isdeletesend;
		private int? _isdeleteread;
		private string _url;
		private string _havesendusers;
		private int? _isremind;
		/// <summary>
		/// 
		/// </summary>
		public Guid MessageCode
		{
			set{ _messagecode=value;}
			get{return _messagecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long CutID
		{
			set{ _cutid=value;}
			get{return _cutid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MsgTitle
		{
			set{ _msgtitle=value;}
			get{return _msgtitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime SendTime
		{
			set{ _sendtime=value;}
			get{return _sendtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int MsgType
		{
			set{ _msgtype=value;}
			get{return _msgtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SendMan
		{
			set{ _sendman=value;}
			get{return _sendman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int MsgState
		{
			set{ _msgstate=value;}
			get{return _msgstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDeleteSend
		{
			set{ _isdeletesend=value;}
			get{return _isdeletesend;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDeleteRead
		{
			set{ _isdeleteread=value;}
			get{return _isdeleteread;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string URL
		{
			set{ _url=value;}
			get{return _url;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HaveSendUsers
		{
			set{ _havesendusers=value;}
			get{return _havesendusers;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsRemind
		{
			set{ _isremind=value;}
			get{return _isremind;}
		}
		#endregion Model

	}
}

