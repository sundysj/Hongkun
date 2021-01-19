using System;
namespace MobileSoft.Model.Unified
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
		private string _communityid;
		private string _signcertpath;
		private string _signcertpwd;
		private string _validatecertdir;
		private string _encryptcert;
		private string _merid;
		private string _accno;
		/// <summary>
		/// 银联配置表
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 小区ID
		/// </summary>
		public string CommunityId
		{
			set{ _communityid=value;}
			get{return _communityid;}
		}
		/// <summary>
		/// 银联私钥路径
		/// </summary>
		public string signCertPath
		{
			set{ _signcertpath=value;}
			get{return _signcertpath;}
		}
		/// <summary>
		/// 银联私钥密码
		/// </summary>
		public string signCertPwd
		{
			set{ _signcertpwd=value;}
			get{return _signcertpwd;}
		}
		/// <summary>
		/// 验签目录
		/// </summary>
		public string validateCertDir
		{
			set{ _validatecertdir=value;}
			get{return _validatecertdir;}
		}
		/// <summary>
		/// 加密公钥证书路径
		/// </summary>
		public string encryptCert
		{
			set{ _encryptcert=value;}
			get{return _encryptcert;}
		}
		/// <summary>
		/// 商户号
		/// </summary>
		public string merId
		{
			set{ _merid=value;}
			get{return _merid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string accNo
		{
			set{ _accno=value;}
			get{return _accno;}
		}
		#endregion Model

	}
}

