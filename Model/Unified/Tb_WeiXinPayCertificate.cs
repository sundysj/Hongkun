using System;
namespace MobileSoft.Model.Unified
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
		private string _communityid;
		private string _appid;
		private string _mch_id;
		private string _appkey;
		private string _appsecret;
		private string _sslcert_path;
		private string _sslcert_password;
		/// <summary>
		/// 微信配置表
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
		/// AppID
		/// </summary>
		public string appid
		{
			set{ _appid=value;}
			get{return _appid;}
		}
		/// <summary>
		/// 商户号
		/// </summary>
		public string mch_id
		{
			set{ _mch_id=value;}
			get{return _mch_id;}
		}
		/// <summary>
		/// 密钥
		/// </summary>
		public string appkey
		{
			set{ _appkey=value;}
			get{return _appkey;}
		}
		/// <summary>
		/// APP AppSecret
		/// </summary>
		public string appsecret
		{
			set{ _appsecret=value;}
			get{return _appsecret;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SSLCERT_PATH
		{
			set{ _sslcert_path=value;}
			get{return _sslcert_path;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SSLCERT_PASSWORD
		{
			set{ _sslcert_password=value;}
			get{return _sslcert_password;}
		}
		#endregion Model

	}
}

