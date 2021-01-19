using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_ContractInfo 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_ContractInfo
	{
		public Tb_OAPublicWork_ContractInfo()
		{}
		#region Model
		private int _infoid;
		private int? _tb_workflow_flowsort_infoid;
		private string _usercode;
		private string _contractcode;
		private string _contractname;
		private string _companyname;
		private string _contractmoney;
		private string _writer;
		private DateTime? _writedate;
		private DateTime? _overdate;
		private DateTime? _contractdate;
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
		public string ContractCode
		{
			set{ _contractcode=value;}
			get{return _contractcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ContractName
		{
			set{ _contractname=value;}
			get{return _contractname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CompanyName
		{
			set{ _companyname=value;}
			get{return _companyname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ContractMoney
		{
			set{ _contractmoney=value;}
			get{return _contractmoney;}
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
		public DateTime? OverDate
		{
			set{ _overdate=value;}
			get{return _overdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ContractDate
		{
			set{ _contractdate=value;}
			get{return _contractdate;}
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

