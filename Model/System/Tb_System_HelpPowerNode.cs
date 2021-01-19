using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// 实体类Tb_System_HelpPowerNode 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_System_HelpPowerNode
	{
		public Tb_System_HelpPowerNode()
		{}
		#region Model
		private long _iid;
		private int _corpid;
		private string _pnodecode;
		private string _pnodename;
		private int? _inpopedom;
		private int? _nodetype;
		private int? _isdelete;
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
		public string PNodeName
		{
			set{ _pnodename=value;}
			get{return _pnodename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? InPopedom
		{
			set{ _inpopedom=value;}
			get{return _inpopedom;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? NodeType
		{
			set{ _nodetype=value;}
			get{return _nodetype;}
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

