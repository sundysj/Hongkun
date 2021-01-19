using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_SendCar 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_SendCar
	{
		public Tb_OAPublicWork_SendCar()
		{}
		#region Model
		private int _infoid;
		private int? _tb_workflow_flowsort_infoid;
		private string _usercode;
		private string _usecarman;
		private string _usemark;
		private DateTime? _startdate;
		private DateTime? _enddate;
		private string _opruser;
		private string _place;
		private string _carnum;
		private string _infocontent;
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
		public string UseCarMan
		{
			set{ _usecarman=value;}
			get{return _usecarman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UseMark
		{
			set{ _usemark=value;}
			get{return _usemark;}
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
		public string OprUser
		{
			set{ _opruser=value;}
			get{return _opruser;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Place
		{
			set{ _place=value;}
			get{return _place;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CarNum
		{
			set{ _carnum=value;}
			get{return _carnum;}
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
		public DateTime? WorkStartDate
		{
			set{ _workstartdate=value;}
			get{return _workstartdate;}
		}
		#endregion Model

	}
}

