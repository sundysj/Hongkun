using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// ʵ����Tb_Sys_RolePope ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Sys_RolePope
	{
		public Tb_Sys_RolePope()
		{}
		#region Model
		private long _iid;
		private Guid _streetcode;
		private string _rolecode;
		private string _pnodecode;
		private string _functions;
		/// <summary>
		/// 
		/// </summary>
		public long IID
		{
			set{ _iid=value;}
			get{return _iid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid StreetCode
		{
			set{ _streetcode=value;}
			get{return _streetcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RoleCode
		{
			set{ _rolecode=value;}
			get{return _rolecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PNodeCode
		{
			set{ _pnodecode=value;}
			get{return _pnodecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Functions
		{
			set{ _functions=value;}
			get{return _functions;}
		}
		#endregion Model

	}
}

