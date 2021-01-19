using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_WeiXinPayCertificate 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_WeiXinPayCertificate
	{
		public Tb_WeiXinPayCertificate()
		{}
		#region Model
		private string _id;
		private string _bussid;
		private string _appid;
		private string _mch_id;
		private string _appkey;
		private string _appsecret;
		private string _sslcert_path;
		private string _sslcert_password;
		[DisplayName("微信配置表")]
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
		[DisplayName("AppID")]
		public string appid
		{
			set{ _appid=value;}
			get{return _appid;}
		}
		[DisplayName("商户号")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string mch_id
		{
			set{ _mch_id=value;}
			get{return _mch_id;}
		}
		[DisplayName("APP支付密钥")]
		public string appkey
		{
			set{ _appkey=value;}
			get{return _appkey;}
		}
		[DisplayName("APP AppSecret")]
		public string appsecret
		{
			set{ _appsecret=value;}
			get{return _appsecret;}
		}
		[DisplayName("SSL证书路径")]
		public string SSLCERT_PATH
		{
			set{ _sslcert_path=value;}
			get{return _sslcert_path;}
		}
		[DisplayName("SSL证书密码")]
		public string SSLCERT_PASSWORD
		{
			set{ _sslcert_password=value;}
			get{return _sslcert_password;}
		}
		#endregion Model

	}
}

