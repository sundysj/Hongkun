using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_AlipayCertifiate ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_AlipayCertifiate
	{
		public Tb_AlipayCertifiate()
		{}
		#region Model
		private string _id;
		private string _bussid;
		private string _partner;
		private string _seller_id;
		private string _extern_token;
		private string _alipay_public_key;
		private string _private_key;
		[DisplayName("")]
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		[DisplayName("�̼�ID")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("�������ID")]
		public string partner
		{
			set{ _partner=value;}
			get{return _partner;}
		}
		[DisplayName("����ID")]
		public string seller_id
		{
			set{ _seller_id=value;}
			get{return _seller_id;}
		}
		[DisplayName("��չ��")]
		public string extern_token
		{
			set{ _extern_token=value;}
			get{return _extern_token;}
		}
		[DisplayName("֧������Կ")]
		public string alipay_public_key
		{
			set{ _alipay_public_key=value;}
			get{return _alipay_public_key;}
		}
		[DisplayName("�̻�˽Կ")]
		public string private_key
		{
			set{ _private_key=value;}
			get{return _private_key;}
		}
		#endregion Model

	}
}

