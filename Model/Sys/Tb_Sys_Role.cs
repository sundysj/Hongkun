using System;
namespace MobileSoft.Model.Sys
{
	/// <summary>
	/// ʵ����Tb_Sys_Role ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Sys_Role
	{
		public Tb_Sys_Role()
		{}
		#region Model
		private string _rolecode;
		private string _rolename;
		private string _roledescribe;
		private int? _issysrole;
		private int? _commid;
		private string _depcode;
		private string _sysrolecode;
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
		public string RoleName
		{
			set{ _rolename=value;}
			get{return _rolename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RoleDescribe
		{
			set{ _roledescribe=value;}
			get{return _roledescribe;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsSysRole
		{
			set{ _issysrole=value;}
			get{return _issysrole;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DepCode
		{
			set{ _depcode=value;}
			get{return _depcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SysRoleCode
		{
			set{ _sysrolecode=value;}
			get{return _sysrolecode;}
		}
		#endregion Model

	}
}

