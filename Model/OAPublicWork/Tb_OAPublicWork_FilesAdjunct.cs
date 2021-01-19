using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_FilesAdjunct 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_FilesAdjunct
	{
		public Tb_OAPublicWork_FilesAdjunct()
		{}
		#region Model
		private long _infoid;
		private string _dictionarycode;
		private int? _instanceid;
		private string _userfilescode;
		private string _adjunctname;
		private string _filpath;
		private string _fileexname;
		private string _filesize;
		private string _iscandown;
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
		public string DictionaryCode
		{
			set{ _dictionarycode=value;}
			get{return _dictionarycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? InstanceId
		{
			set{ _instanceid=value;}
			get{return _instanceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserFilesCode
		{
			set{ _userfilescode=value;}
			get{return _userfilescode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AdjunctName
		{
			set{ _adjunctname=value;}
			get{return _adjunctname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FilPath
		{
			set{ _filpath=value;}
			get{return _filpath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FileExName
		{
			set{ _fileexname=value;}
			get{return _fileexname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FileSize
		{
			set{ _filesize=value;}
			get{return _filesize;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsCanDown
		{
			set{ _iscandown=value;}
			get{return _iscandown;}
		}
		#endregion Model

	}
}

