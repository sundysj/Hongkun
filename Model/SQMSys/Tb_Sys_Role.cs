using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// 实体类Tb_Sys_Role 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Sys_Role
	{
		public Tb_Sys_Role()
		{}
		#region Model
		private string _rolecode;
		private Guid _streetcode;
		private string _rolename;
		private string _roledescribe;
		private int? _issysrole;
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
		public Guid StreetCode
		{
			set{ _streetcode=value;}
			get{return _streetcode;}
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

