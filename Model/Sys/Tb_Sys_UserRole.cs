using System;
namespace MobileSoft.Model.Sys
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

