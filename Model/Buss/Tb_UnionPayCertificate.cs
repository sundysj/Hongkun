using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_UnionPayCertificate ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_UnionPayCertificate
	{
		public Tb_UnionPayCertificate()
		{}
		#region Model
		private string _id;
		private string _bussid;
		private string _signcertpath;
		private string _signcertpwd;
		private string _validatecertdir;
		private string _encryptcert;
		private string _merid;
		private string _accno;
		[DisplayName("�������ñ�")]
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
		[DisplayName("˽Կ·��")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public string signCertPath
		{
			set{ _signcertpath=value;}
			get{return _signcertpath;}
		}
		[DisplayName("˽Կ����")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public string signCertPwd
		{
			set{ _signcertpwd=value;}
			get{return _signcertpwd;}
		}
		[DisplayName("��ǩĿ¼")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public string validateCertDir
		{
			set{ _validatecertdir=value;}
			get{return _validatecertdir;}
		}
		[DisplayName("���ܹ�Կ֤��·��")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public string encryptCert
		{
			set{ _encryptcert=value;}
			get{return _encryptcert;}
		}
		[DisplayName("�̻���")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public string merId
		{
			set{ _merid=value;}
			get{return _merid;}
		}
		[DisplayName("�����ʻ�")]
		public string accNo
		{
			set{ _accno=value;}
			get{return _accno;}
		}
		#endregion Model

	}
}

