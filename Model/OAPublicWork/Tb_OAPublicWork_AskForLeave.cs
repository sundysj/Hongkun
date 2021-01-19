using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_AskForLeave 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_AskForLeave
	{
		public Tb_OAPublicWork_AskForLeave()
		{}
		#region Model
		private int _infoid;
		private int? _tb_workflow_flowsort_infoid;
		private string _usercode;
		private string _whatthing;
		private string _nature;
		private string _days;
		private DateTime? _startdate;
		private DateTime? _enddate;
		private string _mark;
		private string _documenturl;
		private DateTime? _workstartdate;
		private string _askforleaveuser;
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
		public string WhatThing
		{
			set{ _whatthing=value;}
			get{return _whatthing;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Nature
		{
			set{ _nature=value;}
			get{return _nature;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Days
		{
			set{ _days=value;}
			get{return _days;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? StartDate
		{
			set{ _startdate=value;}
			get{return _startdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
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
		/// <summary>
		/// 
		/// </summary>
		public string AskForLeaveUser
		{
			set{ _askforleaveuser=value;}
			get{return _askforleaveuser;}
		}
		#endregion Model

	}
}

