using System;
namespace MobileSoft.Model.WorkFlow
{
	/// <summary>
	/// 实体类Tb_WorkFlow_GeneralDecompose 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_WorkFlow_GeneralDecompose
	{
		public Tb_WorkFlow_GeneralDecompose()
		{}
		#region Model
		private Guid _generaldecomposecode;
		private Guid _generalmaincode;
		private long _cutid;
		private string _decomposecode;
		private string _disposeman;
		private int _disposepope;
		private int _isfinishpope;
		private int _isremind;
		private DateTime? _remindtime;
		private string _remindmode;
		private DateTime _insttime;
		private DateTime? _signtime;
		private string _memo;
		private int _remindstate;
		private int _disposeresult;
		private DateTime? _disposetime;
		private int _disposestate;
		private Guid _msrepl_tran_version;
		private string _type;
		private string _disposedepcode;
		/// <summary>
		/// 
		/// </summary>
		public Guid GeneralDecomposeCode
		{
			set{ _generaldecomposecode=value;}
			get{return _generaldecomposecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid GeneralMainCode
		{
			set{ _generalmaincode=value;}
			get{return _generalmaincode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long CutID
		{
			set{ _cutid=value;}
			get{return _cutid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DecomposeCode
		{
			set{ _decomposecode=value;}
			get{return _decomposecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DisposeMan
		{
			set{ _disposeman=value;}
			get{return _disposeman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DisposePope
		{
			set{ _disposepope=value;}
			get{return _disposepope;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsFinishPope
		{
			set{ _isfinishpope=value;}
			get{return _isfinishpope;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsRemind
		{
			set{ _isremind=value;}
			get{return _isremind;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RemindTime
		{
			set{ _remindtime=value;}
			get{return _remindtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RemindMode
		{
			set{ _remindmode=value;}
			get{return _remindmode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime InstTime
		{
			set{ _insttime=value;}
			get{return _insttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SignTime
		{
			set{ _signtime=value;}
			get{return _signtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int RemindState
		{
			set{ _remindstate=value;}
			get{return _remindstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DisposeResult
		{
			set{ _disposeresult=value;}
			get{return _disposeresult;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? DisposeTime
		{
			set{ _disposetime=value;}
			get{return _disposetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DisposeState
		{
			set{ _disposestate=value;}
			get{return _disposestate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid msrepl_tran_version
		{
			set{ _msrepl_tran_version=value;}
			get{return _msrepl_tran_version;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TYPE
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DisposeDepCode
		{
			set{ _disposedepcode=value;}
			get{return _disposedepcode;}
		}
		#endregion Model

	}
}

