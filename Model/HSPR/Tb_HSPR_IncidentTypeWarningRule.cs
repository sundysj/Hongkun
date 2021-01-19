using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// ʵ����Tb_HSPR_IncidentTypeWarningRule ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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

