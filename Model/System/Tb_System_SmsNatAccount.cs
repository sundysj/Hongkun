using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// 实体类Tb_System_SmsNatAccount 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_System_SmsNatAccount
	{
		public Tb_System_SmsNatAccount()
		{}
		#region Model
		private Guid _commcode;
		private long _commid;
		private string _circle;
		private string _password;
		private int? _balance;
		private int? _waytype;
		/// <summary>
		/// 
		/// </summary>
		public Guid CommCode
		{
			set{ _commcode=value;}
			get{return _commcode;}
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
		public string Circle
		{
			set{ _circle=value;}
			get{return _circle;}
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
		public int? Balance
		{
			set{ _balance=value;}
			get{return _balance;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? WayType
		{
			set{ _waytype=value;}
			get{return _waytype;}
		}
		#endregion Model

	}
}

