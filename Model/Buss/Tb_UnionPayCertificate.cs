using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_UnionPayCertificate 。(属性说明自动提取数据库字段的描述信息)
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
		[DisplayName("银联配置表")]
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		[DisplayName("商家ID")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("私钥路径")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string signCertPath
		{
			set{ _signcertpath=value;}
			get{return _signcertpath;}
		}
		[DisplayName("私钥密码")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string signCertPwd
		{
			set{ _signcertpwd=value;}
			get{return _signcertpwd;}
		}
		[DisplayName("验签目录")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string validateCertDir
		{
			set{ _validatecertdir=value;}
			get{return _validatecertdir;}
		}
		[DisplayName("加密公钥证书路径")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string encryptCert
		{
			set{ _encryptcert=value;}
			get{return _encryptcert;}
		}
		[DisplayName("商户号")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string merId
		{
			set{ _merid=value;}
			get{return _merid;}
		}
		[DisplayName("银行帐户")]
		public string accNo
		{
			set{ _accno=value;}
			get{return _accno;}
		}
		#endregion Model

	}
}

