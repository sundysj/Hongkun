using System;
namespace MobileSoft.Model.WorkFlow
{
	/// <summary>
	/// 实体类Tb_WorkFlow_GeneralMain 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_WorkFlow_GeneralMain
	{
		public Tb_WorkFlow_GeneralMain()
		{}
		#region Model
		private Guid _generalmaincode;
		private long _cutid;
		private string _title;
		private string _flowdegree;
		private string _content;
		private string _memo;
		private string _drafman;
		private DateTime _drafdate;
		private DateTime? _finishdate;
		private int _workstate;
		private string _documentid;
		private Guid _msrepl_tran_version;
		private string _type;
		private string _documenttypecode;
		private int? _doctype;
		/// <summary>
		/// 
		/// </summary>
		public Guid GeneralMainCode
		{
			set{ _generalmaincode=value;}
			get{return _generalmaincode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long CutID
		{
			set{ _cutid=value;}
			get{return _cutid;}
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
		public string FlowDegree
		{
			set{ _flowdegree=value;}
			get{return _flowdegree;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DrafMan
		{
			set{ _drafman=value;}
			get{return _drafman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime DrafDate
		{
			set{ _drafdate=value;}
			get{return _drafdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FinishDate
		{
			set{ _finishdate=value;}
			get{return _finishdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int WorkState
		{
			set{ _workstate=value;}
			get{return _workstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DocumentID
		{
			set{ _documentid=value;}
			get{return _documentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid msrepl_tran_version
		{
			set{ _msrepl_tran_version=value;}
			get{return _msrepl_tran_version;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TYPE
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DocumentTypeCode
		{
			set{ _documenttypecode=value;}
			get{return _documenttypecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DocType
		{
			set{ _doctype=value;}
			get{return _doctype;}
		}
		#endregion Model

	}
}

