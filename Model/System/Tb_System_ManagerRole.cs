using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// 实体类Tb_System_ManagerRole 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_System_ManagerRole
	{
		public Tb_System_ManagerRole()
		{}
		#region Model
		private Guid _managerrolecode;
		private string _rolecode;
		private string _managercode;
		/// <summary>
		/// 
		/// </summary>
		public Guid ManagerRoleCode
		{
			set{ _managerrolecode=value;}
			get{return _managerrolecode;}
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
		public string ManagerCode
		{
			set{ _managercode=value;}
			get{return _managercode;}
		}
		#endregion Model

	}
}

