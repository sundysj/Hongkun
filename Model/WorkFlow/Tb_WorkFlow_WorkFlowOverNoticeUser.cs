using System;
namespace MobileSoft.Model.WorkFlow
{
	/// <summary>
	/// ʵ����Tb_WorkFlow_WorkFlowOverNoticeUser ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_WorkFlow_WorkFlowOverNoticeUser
	{
		public Tb_WorkFlow_WorkFlowOverNoticeUser()
		{}
		#region Model
		private int _infoid;
		private int? _tb_workflow_instance_infoid;
		private string _tb_sys_user_usercode;
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
		public string Tb_Sys_User_UserCode
		{
			set{ _tb_sys_user_usercode=value;}
			get{return _tb_sys_user_usercode;}
		}
		#endregion Model

	}
}

