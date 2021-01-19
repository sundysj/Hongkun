using System;
namespace MobileSoft.Model.Common
{
	/// <summary>
	/// 实体类Tb_Common_CommonInfo 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Common_CommonInfo
	{
		public Tb_Common_CommonInfo()
		{}
		#region Model
		private long _iid;
		private string _depcode;
		private string _organcode;
		private string _typecode;
		private string _title;
		private string _content;
		private string _username;
		private DateTime? _issuedate;
		private string _type;
		private string _readdepartname;
		private string _readdepartcode;
		private string _readusername;
		private string _readusercode;
		private string _havereadusername;
		private string _havereadusercode;
		/// <summary>
		/// 
		/// </summary>
		public long IID
		{
			set{ _iid=value;}
			get{return _iid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DepCode
		{
			set{ _depcode=value;}
			get{return _depcode;}
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
		public string TypeCode
		{
			set{ _typecode=value;}
			get{return _typecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
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
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
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
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReadDepartName
		{
			set{ _readdepartname=value;}
			get{return _readdepartname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReadDepartCode
		{
			set{ _readdepartcode=value;}
			get{return _readdepartcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReadUserName
		{
			set{ _readusername=value;}
			get{return _readusername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReadUserCode
		{
			set{ _readusercode=value;}
			get{return _readusercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HaveReadUserName
		{
			set{ _havereadusername=value;}
			get{return _havereadusername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HaveReadUserCode
		{
			set{ _havereadusercode=value;}
			get{return _havereadusercode;}
		}
		#endregion Model

	}
}

