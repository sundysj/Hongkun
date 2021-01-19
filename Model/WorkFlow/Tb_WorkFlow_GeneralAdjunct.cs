using System;
namespace MobileSoft.Model.WorkFlow
{
	/// <summary>
	/// 实体类Tb_WorkFlow_GeneralAdjunct 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_WorkFlow_GeneralAdjunct
	{
		public Tb_WorkFlow_GeneralAdjunct()
		{}
		#region Model
		private string _generaladjunctcode;
		private Guid _generalcode;
		private string _adjunctname;
		private string _filpath;
		private string _fileexname;
		private decimal _filesize;
		private Guid _msrepl_tran_version;
		/// <summary>
		/// 
		/// </summary>
		public string GeneralAdjunctCode
		{
			set{ _generaladjunctcode=value;}
			get{return _generaladjunctcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid GeneralCode
		{
			set{ _generalcode=value;}
			get{return _generalcode;}
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
		public decimal FileSize
		{
			set{ _filesize=value;}
			get{return _filesize;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid msrepl_tran_version
		{
			set{ _msrepl_tran_version=value;}
			get{return _msrepl_tran_version;}
		}
		#endregion Model

	}
}

