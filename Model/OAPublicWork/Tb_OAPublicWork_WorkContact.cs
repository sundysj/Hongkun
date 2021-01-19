using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_WorkContact 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_WorkContact
	{
		public Tb_OAPublicWork_WorkContact()
		{}
		#region Model
		private int _infoid;
		private int? _tb_workflow_flowsort_infoid;
		private string _usercode;
		private string _title;
		private string _worksper;
		private string _ask;
		private DateTime? _conferenceenddate;
		private DateTime? _workenddate;
		private string _contactimportant;
		private string _infocontent;
		private string _documenturl;
		private DateTime? _arrangedate;
		private string _assumedepart;
		private string _assumeuser;
		private string _cooperdepart;
		private string _cooperuser;
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
		public string WorkSper
		{
			set{ _worksper=value;}
			get{return _worksper;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ask
		{
			set{ _ask=value;}
			get{return _ask;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ConferenceEndDate
		{
			set{ _conferenceenddate=value;}
			get{return _conferenceenddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? WorkEndDate
		{
			set{ _workenddate=value;}
			get{return _workenddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ContactImportant
		{
			set{ _contactimportant=value;}
			get{return _contactimportant;}
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
		public DateTime? ArrangeDate
		{
			set{ _arrangedate=value;}
			get{return _arrangedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AssumeDepart
		{
			set{ _assumedepart=value;}
			get{return _assumedepart;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AssumeUser
		{
			set{ _assumeuser=value;}
			get{return _assumeuser;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CooperDepart
		{
			set{ _cooperdepart=value;}
			get{return _cooperdepart;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CooperUser
		{
			set{ _cooperuser=value;}
			get{return _cooperuser;}
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

