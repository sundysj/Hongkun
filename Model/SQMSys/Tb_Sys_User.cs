using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// 实体类Tb_Sys_User 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Sys_User
	{
		public Tb_Sys_User()
		{}
		#region Model
		private string _usercode;
		private Guid _streetcode;
		private string _username;
		private string _logincode;
		private string _password;
		private string _depcode;
		private string _employeecode;
		private string _mobiletel;
		private string _memo;
		private int? _ismobile;
		private int? _isdelete;
		private DateTime? _deletedate;
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
		public Guid StreetCode
		{
			set{ _streetcode=value;}
			get{return _streetcode;}
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
		public string LoginCode
		{
			set{ _logincode=value;}
			get{return _logincode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PassWord
		{
			set{ _password=value;}
			get{return _password;}
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
		public string EmployeeCode
		{
			set{ _employeecode=value;}
			get{return _employeecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MobileTel
		{
			set{ _mobiletel=value;}
			get{return _mobiletel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsMobile
		{
			set{ _ismobile=value;}
			get{return _ismobile;}
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
		public DateTime? DeleteDate
		{
			set{ _deletedate=value;}
			get{return _deletedate;}
		}
		#endregion Model

	}
}

