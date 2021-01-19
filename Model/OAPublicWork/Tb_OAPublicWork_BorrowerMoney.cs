using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_BorrowerMoney 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_BorrowerMoney
	{
		public Tb_OAPublicWork_BorrowerMoney()
		{}
		#region Model
		private int _infoid;
		private int? _tb_workflow_flowsort_infoid;
		private string _usercode;
		private string _paymethod;
		private decimal? _borrowermoney;
		private DateTime? _askfordate;
		private string _howuse;
		private string _mark;
		private string _documenturl;
		private DateTime? _workstartdate;
		/// <summary>
		/// 
		/// </summary>
		public int InfoId
		{
			set{ _infoid=value;}
			get{return _infoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Tb_WorkFlow_FlowSort_InfoId
		{
			set{ _tb_workflow_flowsort_infoid=value;}
			get{return _tb_workflow_flowsort_infoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PayMethod
		{
			set{ _paymethod=value;}
			get{return _paymethod;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? BorrowerMoney
		{
			set{ _borrowermoney=value;}
			get{return _borrowermoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AskForDate
		{
			set{ _askfordate=value;}
			get{return _askfordate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HowUse
		{
			set{ _howuse=value;}
			get{return _howuse;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mark
		{
			set{ _mark=value;}
			get{return _mark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DocumentUrl
		{
			set{ _documenturl=value;}
			get{return _documenturl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? WorkStartDate
		{
			set{ _workstartdate=value;}
			get{return _workstartdate;}
		}
		#endregion Model

	}
}

