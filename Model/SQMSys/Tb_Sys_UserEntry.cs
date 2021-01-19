using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// ʵ����Tb_Sys_UserEntry ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Sys_UserEntry
	{
		public Tb_Sys_UserEntry()
		{}
		#region Model
		private string _entrycode;
		private string _usercode;
		private long _entryid;
		private int? _entrytype;
		/// <summary>
		/// 
		/// </summary>
		public string EntryCode
		{
			set{ _entrycode=value;}
			get{return _entrycode;}
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
		public long EntryID
		{
			set{ _entryid=value;}
			get{return _entryid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EntryType
		{
			set{ _entrytype=value;}
			get{return _entrytype;}
		}
		#endregion Model

	}
}

