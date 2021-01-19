using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// 实体类Tb_System_Error 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_System_Error
	{
		public Tb_System_Error()
		{}
		#region Model
		private Guid _errorcode;
		private DateTime _errortime;
		private string _errorurl;
		private string _errorsource;
		private string _errormessage;
		/// <summary>
		/// 
		/// </summary>
		public Guid ErrorCode
		{
			set{ _errorcode=value;}
			get{return _errorcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime ErrorTime
		{
			set{ _errortime=value;}
			get{return _errortime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ErrorURL
		{
			set{ _errorurl=value;}
			get{return _errorurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ErrorSource
		{
			set{ _errorsource=value;}
			get{return _errorsource;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ErrorMessage
		{
			set{ _errormessage=value;}
			get{return _errormessage;}
		}
		#endregion Model

	}
}

