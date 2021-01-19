using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_OfficialDocument 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_OfficialDocument
	{
		public Tb_OAPublicWork_OfficialDocument()
		{}
		#region Model
		private int _infoid;
		private int? _tb_workflow_flowsort_infoid;
		private string _username;
		private string _filecode;
		private string _filetitle;
		private string _dispatchunits;
		private string _writer;
		private DateTime? _writedate;
		private string _reciveunits;
		private string _filesecret;
		private string _nervous;
		private string _keywords;
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
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FileCode
		{
			set{ _filecode=value;}
			get{return _filecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FileTitle
		{
			set{ _filetitle=value;}
			get{return _filetitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DispatchUnits
		{
			set{ _dispatchunits=value;}
			get{return _dispatchunits;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Writer
		{
			set{ _writer=value;}
			get{return _writer;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? WriteDate
		{
			set{ _writedate=value;}
			get{return _writedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReciveUnits
		{
			set{ _reciveunits=value;}
			get{return _reciveunits;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FileSecret
		{
			set{ _filesecret=value;}
			get{return _filesecret;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Nervous
		{
			set{ _nervous=value;}
			get{return _nervous;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string KeyWords
		{
			set{ _keywords=value;}
			get{return _keywords;}
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

