using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_OfficialConference 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_OfficialConference
	{
		public Tb_OAPublicWork_OfficialConference()
		{}
		#region Model
		private int _infoid;
		private int? _tb_workflow_flowsort_infoid;
		private string _conferencename;
		private string _usercode;
		private string _conferencecode;
		private string _conferencetitle;
		private string _conferenceexplain;
		private string _startdate;
		private string _enddate;
		private string _moderator;
		private string _convenor;
		private string _conferencelist;
		private string _draftinguser;
		private string _documenturl;
		private string _conferenceplace;
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
		public string ConferenceName
		{
			set{ _conferencename=value;}
			get{return _conferencename;}
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
		public string ConferenceCode
		{
			set{ _conferencecode=value;}
			get{return _conferencecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ConferenceTitle
		{
			set{ _conferencetitle=value;}
			get{return _conferencetitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ConferenceExplain
		{
			set{ _conferenceexplain=value;}
			get{return _conferenceexplain;}
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
		/// <summary>
		/// 
		/// </summary>
		public string Moderator
		{
			set{ _moderator=value;}
			get{return _moderator;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Convenor
		{
			set{ _convenor=value;}
			get{return _convenor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ConferenceList
		{
			set{ _conferencelist=value;}
			get{return _conferencelist;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DraftingUser
		{
			set{ _draftinguser=value;}
			get{return _draftinguser;}
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
		public string ConferencePlace
		{
			set{ _conferenceplace=value;}
			get{return _conferenceplace;}
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

