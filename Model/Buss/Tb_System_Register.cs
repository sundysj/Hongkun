using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_System_Register 。(属性说明自动提取数据库字段的描述信息)
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
		[DisplayName("注册商家表ID")]
		public long RegID
		{
			set{ _regid=value;}
			get{return _regid;}
		}
		[DisplayName("注册大类")]
		public string RegBigType
		{
			set{ _regbigtype=value;}
			get{return _regbigtype;}
		}
		[DisplayName("注册小类")]
		public string RegSmallType
		{
			set{ _regsmalltype=value;}
			get{return _regsmalltype;}
		}
		[DisplayName("注册用户名")]
		public string RegUserName
		{
			set{ _regusername=value;}
			get{return _regusername;}
		}
		[DisplayName("注册用户名密码")]
		public string RegPassWord
		{
			set{ _regpassword=value;}
			get{return _regpassword;}
		}
		[DisplayName("注册商家名称")]
		public string RegCorpName
		{
			set{ _regcorpname=value;}
			get{return _regcorpname;}
		}
		[DisplayName("地址")]
		public string CorpAddress
		{
			set{ _corpaddress=value;}
			get{return _corpaddress;}
		}
		[DisplayName("邮编")]
		public string ZipCode
		{
			set{ _zipcode=value;}
			get{return _zipcode;}
		}
		[DisplayName("联系人")]
		public string LinkMan
		{
			set{ _linkman=value;}
			get{return _linkman;}
		}
		[DisplayName("电话")]
		public string Tel
		{
			set{ _tel=value;}
			get{return _tel;}
		}
		[DisplayName("传真")]
		public string Fax
		{
			set{ _fax=value;}
			get{return _fax;}
		}
		[DisplayName("邮件")]
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		[DisplayName("网址")]
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
		[DisplayName("微信")]
		public string WeiXin
		{
			set{ _weixin=value;}
			get{return _weixin;}
		}
		[DisplayName("注册日期")]
		public DateTime? RegDate
		{
			set{ _regdate=value;}
			get{return _regdate;}
		}
		[DisplayName("是否审核")]
		public string IsAuditing
		{
			set{ _isauditing=value;}
			get{return _isauditing;}
		}
		[DisplayName("是否成功")]
		public string IsSucc
		{
			set{ _issucc=value;}
			get{return _issucc;}
		}
		[DisplayName("返回信息")]
		public string ReturnMsg
		{
			set{ _returnmsg=value;}
			get{return _returnmsg;}
		}
		[DisplayName("省")]
		public string Province
		{
			set{ _province=value;}
			get{return _province;}
		}
		[DisplayName("市")]
		public string City
		{
			set{ _city=value;}
			get{return _city;}
		}
		[DisplayName("区")]
		public string Borough
		{
			set{ _borough=value;}
			get{return _borough;}
		}
		[DisplayName("商家ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		#endregion Model

	}
}

