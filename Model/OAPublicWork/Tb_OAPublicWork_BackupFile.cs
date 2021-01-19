using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// ʵ����Tb_OAPublicWork_BackupFile ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_BackupFile
	{
		public Tb_OAPublicWork_BackupFile()
		{}
		#region Model
		private string _infocode;
		private string _fname;
		private string _originallyfilepath;
		private string _presentfilepath;
		private DateTime? _backupdate;
		private string _restoreusername;
		private DateTime? _restoredate;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public string InfoCode
		{
			set{ _infocode=value;}
			get{return _infocode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FName
		{
			set{ _fname=value;}
			get{return _fname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OriginallyFilePath
		{
			set{ _originallyfilepath=value;}
			get{return _originallyfilepath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PresentFilePath
		{
			set{ _presentfilepath=value;}
			get{return _presentfilepath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? BackupDate
		{
			set{ _backupdate=value;}
			get{return _backupdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RestoreUserName
		{
			set{ _restoreusername=value;}
			get{return _restoreusername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RestoreDate
		{
			set{ _restoredate=value;}
			get{return _restoredate;}
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

