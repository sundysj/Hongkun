using System;
namespace MobileSoft.Model.WorkFlow
{
	/// <summary>
	/// ʵ����Tb_WorkFlow_OprLevelTableTemp ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_WorkFlow_OprLevelTableTemp
	{
		public Tb_WorkFlow_OprLevelTableTemp()
		{}
		#region Model
		private int? _tb_workflow_flownode_infoid;
		private string _levelcode;
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
		public string LevelCode
		{
			set{ _levelcode=value;}
			get{return _levelcode;}
		}
		#endregion Model

	}
}

