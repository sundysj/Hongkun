using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// 实体类Tb_Sys_UserEntry 。(属性说明自动提取数据库字段的描述信息)
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

