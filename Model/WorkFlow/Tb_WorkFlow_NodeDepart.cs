using System;
namespace MobileSoft.Model.WorkFlow
{
	/// <summary>
	/// ʵ����Tb_WorkFlow_NodeDepart ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_WorkFlow_NodeDepart
	{
		public Tb_WorkFlow_NodeDepart()
		{}
		#region Model
		private int? _tb_workflow_flownode_infoid;
		private string _tb_sys_department_depcode;
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
		public string Tb_Sys_Department_DepCode
		{
			set{ _tb_sys_department_depcode=value;}
			get{return _tb_sys_department_depcode;}
		}
		#endregion Model

	}
}

