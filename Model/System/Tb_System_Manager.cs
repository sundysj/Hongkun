using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// 实体类Tb_System_Manager 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_System_Manager
	{
		public Tb_System_Manager()
		{}
		#region Model
		private string _managercode;
		private string _managertype;
		private string _managername;
		private string _logincode;
		private string _loginpasswd;
		private string _ipaddress;
		private string _mobiletel;
		private string _email;
		private string _province;
		private string _city;
		private string _borough;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public string ManagerCode
		{
			set{ _managercode=value;}
			get{return _managercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ManagerType
		{
			set{ _managertype=value;}
			get{return _managertype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ManagerName
		{
			set{ _managername=value;}
			get{return _managername;}
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
		public string IPAddress
		{
			set{ _ipaddress=value;}
			get{return _ipaddress;}
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
		public string EMail
		{
			set{ _email=value;}
			get{return _email;}
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
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

