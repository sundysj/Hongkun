using System;
namespace MobileSoft.Model.Sys
{
	/// <summary>
	/// ʵ����Tb_Sys_RoleData ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Sys_RoleData
	{
		public Tb_Sys_RoleData()
		{}
		#region Model
		private long _iid;
		private string _rolecode;
		private string _organcode;
		private int? _commid;
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
		public string RoleCode
		{
			set{ _rolecode=value;}
			get{return _rolecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrganCode
		{
			set{ _organcode=value;}
			get{return _organcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		#endregion Model

	}
}

