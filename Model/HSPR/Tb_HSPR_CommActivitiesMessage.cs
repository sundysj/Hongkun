using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_CommActivitiesMessage 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_CommActivitiesMessage
	{
		public Tb_HSPR_CommActivitiesMessage()
		{}
		#region Model
		private string _msgcode;
		private string _msgtype;
		private string _msgtitle;
		private string _msgcontent;
		private DateTime? _msgdate;
		private string _username;
		private string _msglinkcode;
		private string _acccust;
		private string _sendcust;
		private int? _isread;
		private int? _isdelete;
		private long _id;
		private long _replayid;
		private string _iscomplete;
        private string _imglist;
		/// <summary>
		/// 
		/// </summary>
		public string MsgCode
		{
			set{ _msgcode=value;}
			get{return _msgcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MsgType
		{
			set{ _msgtype=value;}
			get{return _msgtype;}
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
		public string MsgContent
		{
			set{ _msgcontent=value;}
			get{return _msgcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? MsgDate
		{
			set{ _msgdate=value;}
			get{return _msgdate;}
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
		public string MsgLinkCode
		{
			set{ _msglinkcode=value;}
			get{return _msglinkcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AccCust
		{
			set{ _acccust=value;}
			get{return _acccust;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SendCust
		{
			set{ _sendcust=value;}
			get{return _sendcust;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsRead
		{
			set{ _isread=value;}
			get{return _isread;}
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
		public long ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ReplayID
		{
			set{ _replayid=value;}
			get{return _replayid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsComplete
		{
			set{ _iscomplete=value;}
			get{return _iscomplete;}
		}

        /// <summary>
        /// 
        /// </summary>
        public string ImgList
        {
            set { _imglist = value; }
            get { return _imglist; }
        }

		#endregion Model

	}
}

