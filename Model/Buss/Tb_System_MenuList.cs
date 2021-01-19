using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_System_MenuList ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_System_MenuList
	{
		public Tb_System_MenuList()
		{}
		#region Model
		private string _menucode;
		private string _menuname;
		private string _datapage;
		private long _menusign;
		private int? _menuisdelete;
		[DisplayName("�̼Ҳ˵�����")]
		public string MenuCode
		{
			set{ _menucode=value;}
			get{return _menucode;}
		}
		[DisplayName("�̼Ҳ˵�����")]
		public string MenuName
		{
			set{ _menuname=value;}
			get{return _menuname;}
		}
		[DisplayName("ָ��ҳ��")]
		public string DataPage
		{
			set{ _datapage=value;}
			get{return _datapage;}
		}
		[DisplayName("�˵���ʶ")]
		public long MenuSign
		{
			set{ _menusign=value;}
			get{return _menusign;}
		}
		[DisplayName("�Ƿ�ɾ��")]
		public int? MenuIsDelete
		{
			set{ _menuisdelete=value;}
			get{return _menuisdelete;}
		}
		#endregion Model

	}
}

