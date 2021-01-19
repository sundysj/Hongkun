using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_CommunitySuggestions 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_CommunitySuggestions
	{
		public Tb_HSPR_CommunitySuggestions()
		{}
		#region Model
		private string _suggestionsid;
		private long _commid;
		private long _custid;
		private string _custname;
		private long _roomid;
		private string _roomsign;
		private string _suggestionstype;
		private string _suggestionstitle;
		private string _suggestionscontent;
		private DateTime? _issuedate;
		private string _linkphone;
		private string _suggestionsimages;
		private string _replystats;
		private DateTime? _replydate;
		private string _replycontent;
		private string _custevaluation;
		private DateTime? _evaluationdate;
		private int? _isdelete;
        private int? _evaluationLevel;

        /// <summary>
        /// 评价级别
        /// </summary>
        public int? EvaluationLevel
        {
            set { _evaluationLevel = value; }
            get { return _evaluationLevel; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SuggestionsID
		{
			set{ _suggestionsid=value;}
			get{return _suggestionsid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
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
		public string CustName
		{
			set{ _custname=value;}
			get{return _custname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long RoomID
		{
			set{ _roomid=value;}
			get{return _roomid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RoomSign
		{
			set{ _roomsign=value;}
			get{return _roomsign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SuggestionsType
		{
			set{ _suggestionstype=value;}
			get{return _suggestionstype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SuggestionsTitle
		{
			set{ _suggestionstitle=value;}
			get{return _suggestionstitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SuggestionsContent
		{
			set{ _suggestionscontent=value;}
			get{return _suggestionscontent;}
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
		public string LinkPhone
		{
			set{ _linkphone=value;}
			get{return _linkphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SuggestionsImages
		{
			set{ _suggestionsimages=value;}
			get{return _suggestionsimages;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReplyStats
		{
			set{ _replystats=value;}
			get{return _replystats;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReplyDate
		{
			set{ _replydate=value;}
			get{return _replydate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReplyContent
		{
			set{ _replycontent=value;}
			get{return _replycontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustEvaluation
		{
			set{ _custevaluation=value;}
			get{return _custevaluation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EvaluationDate
		{
			set{ _evaluationdate=value;}
			get{return _evaluationdate;}
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

