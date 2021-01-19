using System;
namespace MobileSoft.Model.WorkFlow
{
	/// <summary>
	/// 实体类Tb_WorkFlow_InstanceTemp 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_WorkFlow_InstanceTemp
	{
		public Tb_WorkFlow_InstanceTemp()
		{}
		#region Model
		private int _infoid;
		private int? _instanceid;
		private string _tb_dictionary_instancetype_dictionarycode;
		private string _instancemark;
		private int? _noticemsg;
		private int? _noticemail;
		private int? _noticephone;
		private int? _noticehavedeal;
		private int? _noticestartdeal;
		private int? _noticeotheruser;
		private int? _iscomplete;
		private int? _issuccess;
		/// <summary>
		/// 
		/// </summary>
		public int InfoId
		{
			set{ _infoid=value;}
			get{return _infoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? InstanceId
		{
			set{ _instanceid=value;}
			get{return _instanceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tb_Dictionary_InstanceType_DictionaryCode
		{
			set{ _tb_dictionary_instancetype_dictionarycode=value;}
			get{return _tb_dictionary_instancetype_dictionarycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InstanceMark
		{
			set{ _instancemark=value;}
			get{return _instancemark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? NoticeMsg
		{
			set{ _noticemsg=value;}
			get{return _noticemsg;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? NoticeMail
		{
			set{ _noticemail=value;}
			get{return _noticemail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? NoticePhone
		{
			set{ _noticephone=value;}
			get{return _noticephone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? NoticeHaveDeal
		{
			set{ _noticehavedeal=value;}
			get{return _noticehavedeal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? NoticeStartDeal
		{
			set{ _noticestartdeal=value;}
			get{return _noticestartdeal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? NoticeOtherUser
		{
			set{ _noticeotheruser=value;}
			get{return _noticeotheruser;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsComplete
		{
			set{ _iscomplete=value;}
			get{return _iscomplete;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsSuccess
		{
			set{ _issuccess=value;}
			get{return _issuccess;}
		}
		#endregion Model

	}
}

