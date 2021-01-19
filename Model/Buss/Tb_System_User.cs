using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_System_User ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		[DisplayName("�̼�ϵͳ�û�ID")]
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		[DisplayName("�̼�ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("���")]
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		[DisplayName("����")]
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		[DisplayName("��ɫ����")]
		public string RoleName
		{
			set{ _rolename=value;}
			get{return _rolename;}
		}
		[DisplayName("��¼�˺�")]
		public string LoginCode
		{
			set{ _logincode=value;}
			get{return _logincode;}
		}
		[DisplayName("��¼����")]
		public string LoginPwd
		{
			set{ _loginpwd=value;}
			get{return _loginpwd;}
		}
		[DisplayName("��ע")]
		public string UserMark
		{
			set{ _usermark=value;}
			get{return _usermark;}
		}
		[DisplayName("�˵�Ȩ��")]
		public string MenuList
		{
			set{ _menulist=value;}
			get{return _menulist;}
		}
		[DisplayName("�Ƿ�ɾ��")]
		public int? UserIsDelete
		{
			set{ _userisdelete=value;}
			get{return _userisdelete;}
		}
		#endregion Model

	}
}

