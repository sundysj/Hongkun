using System;
namespace MobileSoft.Model.WorkFlow
{
	/// <summary>
	/// ʵ����Tb_WorkFlow_NodeRoleTemp ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_WorkFlow_NodeRoleTemp
	{
		public Tb_WorkFlow_NodeRoleTemp()
		{}
		#region Model
		private int? _tb_workflow_flownode_infoid;
		private string _tb_sys_role_rolecode;
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
		public string Tb_Sys_Role_RoleCode
		{
			set{ _tb_sys_role_rolecode=value;}
			get{return _tb_sys_role_rolecode;}
		}
		#endregion Model

	}
}

