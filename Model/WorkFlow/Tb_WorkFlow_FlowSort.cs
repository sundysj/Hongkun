using System;
namespace MobileSoft.Model.WorkFlow
{
	/// <summary>
	/// 实体类Tb_WorkFlow_FlowSort 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_WorkFlow_FlowSort
	{
		public Tb_WorkFlow_FlowSort()
		{}
		#region Model
		private int _infoid;
		private int? _pid;
		private string _flowsortname;
		private int? _isupdate;
		private int? _isflow;
		private string _documenturl;
		private int? _systemsign;
		private string _directionarycode;
		private int? _sort;
		private int? _isdelete;
		private string _usestartdate;
		private string _useenddate;
		private string _useuserlist;
		private string _useusernamelist;
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
		public int? Pid
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FlowSortName
		{
			set{ _flowsortname=value;}
			get{return _flowsortname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsUpdate
		{
			set{ _isupdate=value;}
			get{return _isupdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsFlow
		{
			set{ _isflow=value;}
			get{return _isflow;}
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
		public int? SystemSign
		{
			set{ _systemsign=value;}
			get{return _systemsign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DirectionaryCode
		{
			set{ _directionarycode=value;}
			get{return _directionarycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UseStartDate
		{
			set{ _usestartdate=value;}
			get{return _usestartdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UseEndDate
		{
			set{ _useenddate=value;}
			get{return _useenddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UseUserList
		{
			set{ _useuserlist=value;}
			get{return _useuserlist;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UseUserNameList
		{
			set{ _useusernamelist=value;}
			get{return _useusernamelist;}
		}
		#endregion Model

	}
}

