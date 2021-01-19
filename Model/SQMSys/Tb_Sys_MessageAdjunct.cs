using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// 实体类Tb_Sys_MessageAdjunct 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Sys_MessageAdjunct
	{
		public Tb_Sys_MessageAdjunct()
		{}
		#region Model
		private string _adjunctcode;
		private Guid _messagecode;
		private string _adjunctname;
		private string _filpath;
		private string _filename;
		private string _fileexname;
		private decimal _filesize;
		/// <summary>
		/// 
		/// </summary>
		public string AdjunctCode
		{
			set{ _adjunctcode=value;}
			get{return _adjunctcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid MessageCode
		{
			set{ _messagecode=value;}
			get{return _messagecode;}
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
		public string FileName
		{
			set{ _filename=value;}
			get{return _filename;}
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
		public decimal FileSize
		{
			set{ _filesize=value;}
			get{return _filesize;}
		}
		#endregion Model

	}
}

