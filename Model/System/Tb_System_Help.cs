using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// 实体类Tb_System_Help 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_System_Help
	{
		public Tb_System_Help()
		{}
		#region Model
		private long _iid;
		private int _corpid;
		private string _pnodecode;
		private string _helptitle;
		private string _helpcontent;
		/// <summary>
		/// 
		/// </summary>
		public long IID
		{
			set{ _iid=value;}
			get{return _iid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CorpID
		{
			set{ _corpid=value;}
			get{return _corpid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PNodeCode
		{
			set{ _pnodecode=value;}
			get{return _pnodecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HelpTitle
		{
			set{ _helptitle=value;}
			get{return _helptitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HelpContent
		{
			set{ _helpcontent=value;}
			get{return _helpcontent;}
		}
		#endregion Model

	}
}

