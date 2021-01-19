using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_System_Register ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_System_Register
	{
		public Tb_System_Register()
		{}
		#region Model
		private long _regid;
		private string _regbigtype;
		private string _regsmalltype;
		private string _regusername;
		private string _regpassword;
		private string _regcorpname;
		private string _corpaddress;
		private string _zipcode;
		private string _linkman;
		private string _tel;
		private string _fax;
		private string _email;
		private string _webname;
		private string _qq;
		private string _weixin;
		private DateTime? _regdate;
		private string _isauditing;
		private string _issucc;
		private string _returnmsg;
		private string _province;
		private string _city;
		private string _borough;
		private string _bussid;
		[DisplayName("ע���̼ұ�ID")]
		public long RegID
		{
			set{ _regid=value;}
			get{return _regid;}
		}
		[DisplayName("ע�����")]
		public string RegBigType
		{
			set{ _regbigtype=value;}
			get{return _regbigtype;}
		}
		[DisplayName("ע��С��")]
		public string RegSmallType
		{
			set{ _regsmalltype=value;}
			get{return _regsmalltype;}
		}
		[DisplayName("ע���û���")]
		public string RegUserName
		{
			set{ _regusername=value;}
			get{return _regusername;}
		}
		[DisplayName("ע���û�������")]
		public string RegPassWord
		{
			set{ _regpassword=value;}
			get{return _regpassword;}
		}
		[DisplayName("ע���̼�����")]
		public string RegCorpName
		{
			set{ _regcorpname=value;}
			get{return _regcorpname;}
		}
		[DisplayName("��ַ")]
		public string CorpAddress
		{
			set{ _corpaddress=value;}
			get{return _corpaddress;}
		}
		[DisplayName("�ʱ�")]
		public string ZipCode
		{
			set{ _zipcode=value;}
			get{return _zipcode;}
		}
		[DisplayName("��ϵ��")]
		public string LinkMan
		{
			set{ _linkman=value;}
			get{return _linkman;}
		}
		[DisplayName("�绰")]
		public string Tel
		{
			set{ _tel=value;}
			get{return _tel;}
		}
		[DisplayName("����")]
		public string Fax
		{
			set{ _fax=value;}
			get{return _fax;}
		}
		[DisplayName("�ʼ�")]
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		[DisplayName("��ַ")]
		public string WebName
		{
			set{ _webname=value;}
			get{return _webname;}
		}
		[DisplayName("QQ")]
		public string QQ
		{
			set{ _qq=value;}
			get{return _qq;}
		}
		[DisplayName("΢��")]
		public string WeiXin
		{
			set{ _weixin=value;}
			get{return _weixin;}
		}
		[DisplayName("ע������")]
		public DateTime? RegDate
		{
			set{ _regdate=value;}
			get{return _regdate;}
		}
		[DisplayName("�Ƿ����")]
		public string IsAuditing
		{
			set{ _isauditing=value;}
			get{return _isauditing;}
		}
		[DisplayName("�Ƿ�ɹ�")]
		public string IsSucc
		{
			set{ _issucc=value;}
			get{return _issucc;}
		}
		[DisplayName("������Ϣ")]
		public string ReturnMsg
		{
			set{ _returnmsg=value;}
			get{return _returnmsg;}
		}
		[DisplayName("ʡ")]
		public string Province
		{
			set{ _province=value;}
			get{return _province;}
		}
		[DisplayName("��")]
		public string City
		{
			set{ _city=value;}
			get{return _city;}
		}
		[DisplayName("��")]
		public string Borough
		{
			set{ _borough=value;}
			get{return _borough;}
		}
		[DisplayName("�̼�ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		#endregion Model

	}
}

