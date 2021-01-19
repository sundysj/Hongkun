using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_InsuranceCheck 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_InsuranceCheck
	{
		public Tb_OAPublicWork_InsuranceCheck()
		{}
		#region Model
		private int _infoid;
		private int? _tb_workflow_flowsort_infoid;
		private string _usercode;
		private string _title;
		private string _agentusercode;
		private string _handlecontent;
		private DateTime? _handledate;
		private string _documenturl;
		private DateTime? _workstartdate;
		private string _beforeclassrole;
		private string _afterclassrole;
		private string _naturelist;
		private string _leavedays;
		private string _startdate;
		private string _enddate;
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
		public string AgentUserCode
		{
			set{ _agentusercode=value;}
			get{return _agentusercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HandleContent
		{
			set{ _handlecontent=value;}
			get{return _handlecontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? HandleDate
		{
			set{ _handledate=value;}
			get{return _handledate;}
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
		/// <summary>
		/// 
		/// </summary>
		public string BeforeClassRole
		{
			set{ _beforeclassrole=value;}
			get{return _beforeclassrole;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AfterClassRole
		{
			set{ _afterclassrole=value;}
			get{return _afterclassrole;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NatureList
		{
			set{ _naturelist=value;}
			get{return _naturelist;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LeaveDays
		{
			set{ _leavedays=value;}
			get{return _leavedays;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StartDate
		{
			set{ _startdate=value;}
			get{return _startdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EndDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		#endregion Model

	}
}

