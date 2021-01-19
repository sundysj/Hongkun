using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_Information_ContactUs ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		[DisplayName("��ϵ����ID")]
		public long ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		[DisplayName("�̼�ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("�̼�����")]
		public string BussName
		{
			set{ _bussname=value;}
			get{return _bussname;}
		}
		[DisplayName("��ַ")]
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		[DisplayName("�ʱ�")]
		public string Postal
		{
			set{ _postal=value;}
			get{return _postal;}
		}
		[DisplayName("��ϵ��")]
		public string LinkMan
		{
			set{ _linkman=value;}
			get{return _linkman;}
		}
		[DisplayName("�칫�绰")]
		public string Tel
		{
			set{ _tel=value;}
			get{return _tel;}
		}
		[DisplayName("�ƶ��绰")]
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		[DisplayName("�ʼ�")]
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
		[DisplayName("΢�ź�")]
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
		[DisplayName("��ַ")]
		public string Map
		{
			set{ _map=value;}
			get{return _map;}
		}
		[DisplayName("��ϵ��������")]
		public string ContactUsContent
		{
			set{ _contactuscontent=value;}
			get{return _contactuscontent;}
		}
		#endregion Model

	}
}

