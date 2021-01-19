using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_IncidentTypeWarningRule 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_IncidentTypeWarningRule
	{
		public Tb_HSPR_IncidentTypeWarningRule()
		{}
		#region Model
		private long _warningruleid;
		private long _warningid;
		private int? _commid;
		private string _ruleid;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public long WarningRuleID
		{
			set{ _warningruleid=value;}
			get{return _warningruleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long WarningID
		{
			set{ _warningid=value;}
			get{return _warningid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RuleID
		{
			set{ _ruleid=value;}
			get{return _ruleid;}
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

