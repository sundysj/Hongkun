using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// 实体类Tb_Sys_PowerNodeTemp 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Sys_PowerNodeTemp
	{
		public Tb_Sys_PowerNodeTemp()
		{}
		#region Model
		private string _userroles;
		private string _pnodecode;
		private string _pnodename;
		private string _urlpage;
		private string _urltarget;
		private string _backtitleimg;
		private string _narrate;
		private int _inpopedom;
		private string _functions;
		private int _nodetype;
		private int? _pnodesort;
		private int? _isdelete;
		private string _prentpnodecode;
		/// <summary>
		/// 
		/// </summary>
		public string UserRoles
		{
			set{ _userroles=value;}
			get{return _userroles;}
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
		public string URLPage
		{
			set{ _urlpage=value;}
			get{return _urlpage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string URLTarget
		{
			set{ _urltarget=value;}
			get{return _urltarget;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BackTitleImg
		{
			set{ _backtitleimg=value;}
			get{return _backtitleimg;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Narrate
		{
			set{ _narrate=value;}
			get{return _narrate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int InPopedom
		{
			set{ _inpopedom=value;}
			get{return _inpopedom;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Functions
		{
			set{ _functions=value;}
			get{return _functions;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int NodeType
		{
			set{ _nodetype=value;}
			get{return _nodetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PNodeSort
		{
			set{ _pnodesort=value;}
			get{return _pnodesort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PrentPNodeCode
		{
			set{ _prentpnodecode=value;}
			get{return _prentpnodecode;}
		}
		#endregion Model

	}
}

