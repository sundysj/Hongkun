using System;
namespace MobileSoft.Model.WorkFlow
{
	/// <summary>
	/// 实体类Tb_WorkFlow_FlowNode 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_WorkFlow_FlowNode
	{
		public Tb_WorkFlow_FlowNode()
		{}
		#region Model
		private int _infoid;
		private int? _tb_workflow_instance_infoid;
		private string _flownodename;
		private int? _flowsort;
		private int? _timeoutday;
		private int? _timeoutdays;
		private string _tb_dictionary_nodeoprmethod_dictionarycode;
		private string _tb_dictionary_nodeoprtype_dictionarycode;
		private int? _jumpflowsort;
		private int? _isupdateflow;
		private string _tb_dictionary_oprstate_dictionarycode;
		private int? _isprint;
		private int? _isstartuser;
		private DateTime? _workflowstartdate;
		private int? _returnnode;
		private int? _checklevel;
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
		public int? Tb_WorkFlow_Instance_InfoId
		{
			set{ _tb_workflow_instance_infoid=value;}
			get{return _tb_workflow_instance_infoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FlowNodeName
		{
			set{ _flownodename=value;}
			get{return _flownodename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FlowSort
		{
			set{ _flowsort=value;}
			get{return _flowsort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TimeOutDay
		{
			set{ _timeoutday=value;}
			get{return _timeoutday;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TimeOutDays
		{
			set{ _timeoutdays=value;}
			get{return _timeoutdays;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tb_Dictionary_NodeOprMethod_DictionaryCode
		{
			set{ _tb_dictionary_nodeoprmethod_dictionarycode=value;}
			get{return _tb_dictionary_nodeoprmethod_dictionarycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tb_Dictionary_NodeOprType_DictionaryCode
		{
			set{ _tb_dictionary_nodeoprtype_dictionarycode=value;}
			get{return _tb_dictionary_nodeoprtype_dictionarycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? JumpFlowSort
		{
			set{ _jumpflowsort=value;}
			get{return _jumpflowsort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsUpdateFlow
		{
			set{ _isupdateflow=value;}
			get{return _isupdateflow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tb_Dictionary_OprState_DictionaryCode
		{
			set{ _tb_dictionary_oprstate_dictionarycode=value;}
			get{return _tb_dictionary_oprstate_dictionarycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsPrint
		{
			set{ _isprint=value;}
			get{return _isprint;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsStartUser
		{
			set{ _isstartuser=value;}
			get{return _isstartuser;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? WorkFlowStartDate
		{
			set{ _workflowstartdate=value;}
			get{return _workflowstartdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ReturnNode
		{
			set{ _returnnode=value;}
			get{return _returnnode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CheckLevel
		{
			set{ _checklevel=value;}
			get{return _checklevel;}
		}
		#endregion Model

	}
}

