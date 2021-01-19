using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_System_Error ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		[DisplayName("")]
		public Guid ErrorCode
		{
			set{ _errorcode=value;}
			get{return _errorcode;}
		}
		[DisplayName("")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public DateTime ErrorTime
		{
			set{ _errortime=value;}
			get{return _errortime;}
		}
		[DisplayName("")]
		public string ErrorURL
		{
			set{ _errorurl=value;}
			get{return _errorurl;}
		}
		[DisplayName("")]
		public string ErrorSource
		{
			set{ _errorsource=value;}
			get{return _errorsource;}
		}
		[DisplayName("")]
		public string ErrorMessage
		{
			set{ _errormessage=value;}
			get{return _errormessage;}
		}
		#endregion Model

	}
}

