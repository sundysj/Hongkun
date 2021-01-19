using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// 实体类Tb_Sys_UserRole 。(属性说明自动提取数据库字段的描述信息)
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

