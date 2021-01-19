using System;
namespace MobileSoft.Model.Common
{
	/// <summary>
	/// 实体类Tb_Common_TelList 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Common_TelList
	{
		public Tb_Common_TelList()
		{}
		#region Model
		private long _infoid;
		private string _usercode;
		private string _telnum;
		private string _mark;
		private string _duty;
		private int? _sort;
		private string _phone;
		private string _mail;
		private string _companyname;
		private string _commname;
		private string _oprname;
		private int? _companynamesort;
		private int? _commnamesort;
		private int? _dutysort;
		/// <summary>
		/// 
		/// </summary>
		public long InfoId
		{
			set{ _infoid=value;}
			get{return _infoid;}
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
		public string TelNum
		{
			set{ _telnum=value;}
			get{return _telnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mark
		{
			set{ _mark=value;}
			get{return _mark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Duty
		{
			set{ _duty=value;}
			get{return _duty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mail
		{
			set{ _mail=value;}
			get{return _mail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CompanyName
		{
			set{ _companyname=value;}
			get{return _companyname;}
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
		public string OprName
		{
			set{ _oprname=value;}
			get{return _oprname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CompanyNameSort
		{
			set{ _companynamesort=value;}
			get{return _companynamesort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CommNameSort
		{
			set{ _commnamesort=value;}
			get{return _commnamesort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DutySort
		{
			set{ _dutysort=value;}
			get{return _dutysort;}
		}
		#endregion Model

	}
}

