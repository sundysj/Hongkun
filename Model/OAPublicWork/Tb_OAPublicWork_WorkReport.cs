using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_WorkReport 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_WorkReport
	{
		public Tb_OAPublicWork_WorkReport()
		{}
		#region Model
		private int _infoid;
		private int? _tb_workflow_flowsort_infoid;
		private string _usercode;
		private string _title;
		private string _contactnervous;
		private string _infocontent;
		private string _reportexplain;
		private DateTime? _reportdate;
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
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ContactNervous
		{
			set{ _contactnervous=value;}
			get{return _contactnervous;}
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
		public string ReportExplain
		{
			set{ _reportexplain=value;}
			get{return _reportexplain;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReportDate
		{
			set{ _reportdate=value;}
			get{return _reportdate;}
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

