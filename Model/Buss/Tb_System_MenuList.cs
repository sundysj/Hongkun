using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_System_MenuList 。(属性说明自动提取数据库字段的描述信息)
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
		[DisplayName("商家菜单编码")]
		public string MenuCode
		{
			set{ _menucode=value;}
			get{return _menucode;}
		}
		[DisplayName("商家菜单名称")]
		public string MenuName
		{
			set{ _menuname=value;}
			get{return _menuname;}
		}
		[DisplayName("指向页面")]
		public string DataPage
		{
			set{ _datapage=value;}
			get{return _datapage;}
		}
		[DisplayName("菜单标识")]
		public long MenuSign
		{
			set{ _menusign=value;}
			get{return _menusign;}
		}
		[DisplayName("是否删除")]
		public int? MenuIsDelete
		{
			set{ _menuisdelete=value;}
			get{return _menuisdelete;}
		}
		#endregion Model

	}
}

