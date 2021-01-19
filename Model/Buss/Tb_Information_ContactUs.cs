using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_Information_ContactUs 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Information_ContactUs
	{
		public Tb_Information_ContactUs()
		{}
		#region Model
		private long _id;
		private string _bussid;
		private string _bussname;
		private string _address;
		private string _postal;
		private string _linkman;
		private string _tel;
		private string _phone;
		private string _email;
		private string _qq;
		private string _wechat;
		private string _url;
		private string _map;
		private string _contactuscontent;
		[DisplayName("联系我们ID")]
		public long ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		[DisplayName("商家ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("商家名称")]
		public string BussName
		{
			set{ _bussname=value;}
			get{return _bussname;}
		}
		[DisplayName("地址")]
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		[DisplayName("邮编")]
		public string Postal
		{
			set{ _postal=value;}
			get{return _postal;}
		}
		[DisplayName("联系人")]
		public string LinkMan
		{
			set{ _linkman=value;}
			get{return _linkman;}
		}
		[DisplayName("办公电话")]
		public string Tel
		{
			set{ _tel=value;}
			get{return _tel;}
		}
		[DisplayName("移动电话")]
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		[DisplayName("邮件")]
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		[DisplayName("QQ")]
		public string QQ
		{
			set{ _qq=value;}
			get{return _qq;}
		}
		[DisplayName("微信号")]
		public string Wechat
		{
			set{ _wechat=value;}
			get{return _wechat;}
		}
		[DisplayName("URL")]
		public string URL
		{
			set{ _url=value;}
			get{return _url;}
		}
		[DisplayName("地址")]
		public string Map
		{
			set{ _map=value;}
			get{return _map;}
		}
		[DisplayName("联系我们内容")]
		public string ContactUsContent
		{
			set{ _contactuscontent=value;}
			get{return _contactuscontent;}
		}
		#endregion Model

	}
}

