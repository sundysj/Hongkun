using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_Reimbursement 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_Reimbursement
	{
		public Tb_OAPublicWork_Reimbursement()
		{}
		#region Model
		private int _infoid;
		private int? _tb_workflow_flowsort_infoid;
		private string _usercode;
		private string _title;
		private DateTime? _reimbursementdate;
		private string _infocontent;
		private string _documenturl;
		private string _realusercode;
		private decimal? _realmoney;
		private DateTime? _workstartdate;
		private string _ispay;
		private int? _printcount;
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
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReimbursementDate
		{
			set{ _reimbursementdate=value;}
			get{return _reimbursementdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InfoContent
		{
			set{ _infocontent=value;}
			get{return _infocontent;}
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
		public string RealUserCode
		{
			set{ _realusercode=value;}
			get{return _realusercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? RealMoney
		{
			set{ _realmoney=value;}
			get{return _realmoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? WorkStartDate
		{
			set{ _workstartdate=value;}
			get{return _workstartdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsPay
		{
			set{ _ispay=value;}
			get{return _ispay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PrintCount
		{
			set{ _printcount=value;}
			get{return _printcount;}
		}
		#endregion Model

	}
}

