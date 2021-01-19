using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_System_User 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_System_User
	{
		public Tb_System_User()
		{}
		#region Model
		private string _usercode;
		private string _bussid;
		private int? _sort;
		private string _name;
		private string _rolename;
		private string _logincode;
		private string _loginpwd;
		private string _usermark;
		private string _menulist;
		private int? _userisdelete;
		[DisplayName("商家系统用户ID")]
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		[DisplayName("商家ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("序号")]
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		[DisplayName("名称")]
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		[DisplayName("角色名称")]
		public string RoleName
		{
			set{ _rolename=value;}
			get{return _rolename;}
		}
		[DisplayName("登录账号")]
		public string LoginCode
		{
			set{ _logincode=value;}
			get{return _logincode;}
		}
		[DisplayName("登录密码")]
		public string LoginPwd
		{
			set{ _loginpwd=value;}
			get{return _loginpwd;}
		}
		[DisplayName("备注")]
		public string UserMark
		{
			set{ _usermark=value;}
			get{return _usermark;}
		}
		[DisplayName("菜单权限")]
		public string MenuList
		{
			set{ _menulist=value;}
			get{return _menulist;}
		}
		[DisplayName("是否删除")]
		public int? UserIsDelete
		{
			set{ _userisdelete=value;}
			get{return _userisdelete;}
		}
		#endregion Model

	}
}

