using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// ʵ����Tb_System_Role ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_System_Role
	{
		public Tb_System_Role()
		{}
		#region Model
		private string _rolecode;
		private string _rolename;
		private string _roledescribe;
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
		#endregion Model

	}
}

