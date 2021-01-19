using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// 实体类Tb_System_Log 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_System_Log
	{
		public Tb_System_Log()
		{}
		#region Model
		private long _logcode;
		private string _managercode;
		private string _locationip;
		private DateTime? _logtime;
		private string _pnodename;
		private string _operatename;
		private string _operateurl;
		private string _memo;
		/// <summary>
		/// 
		/// </summary>
		public long LogCode
		{
			set{ _logcode=value;}
			get{return _logcode;}
		}
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
		public string LocationIP
		{
			set{ _locationip=value;}
			get{return _locationip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LogTime
		{
			set{ _logtime=value;}
			get{return _logtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PNodeName
		{
			set{ _pnodename=value;}
			get{return _pnodename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperateName
		{
			set{ _operatename=value;}
			get{return _operatename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperateURL
		{
			set{ _operateurl=value;}
			get{return _operateurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		#endregion Model

	}
}

