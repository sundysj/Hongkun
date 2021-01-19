using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// ʵ����Tb_Sys_UserRole ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Sys_UserRole
	{
		public Tb_Sys_UserRole()
		{}
		#region Model
		private Guid _userrolecode;
		private Guid _streetcode;
		private string _usercode;
		private string _rolecode;
		/// <summary>
		/// 
		/// </summary>
		public Guid UserRoleCode
		{
			set{ _userrolecode=value;}
			get{return _userrolecode;}
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
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RoleCode
		{
			set{ _rolecode=value;}
			get{return _rolecode;}
		}
		#endregion Model

	}
}

