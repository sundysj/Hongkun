using System;
namespace MobileSoft.Model.WorkFlow
{
	/// <summary>
	/// 实体类Tb_WorkFlow_FlowRecordTemp 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_WorkFlow_FlowRecordTemp
	{
		public Tb_WorkFlow_FlowRecordTemp()
		{}
		#region Model
		private int? _tb_workflow_flownode_infoid;
		private string _usercode;
		private string _recordcontent;
		private string _oprstate;
		private DateTime? _workdate;
		/// <summary>
		/// 
		/// </summary>
		public int? Tb_WorkFlow_FlowNode_InfoId
		{
			set{ _tb_workflow_flownode_infoid=value;}
			get{return _tb_workflow_flownode_infoid;}
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
		public string RecordContent
		{
			set{ _recordcontent=value;}
			get{return _recordcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OprState
		{
			set{ _oprstate=value;}
			get{return _oprstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? WorkDate
		{
			set{ _workdate=value;}
			get{return _workdate;}
		}
		#endregion Model

	}
}

