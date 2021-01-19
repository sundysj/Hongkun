using System;
namespace MobileSoft.Model.Unified
{
	/// <summary>
	/// ʵ����Tb_WeiXinPayCertificate ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		/// ΢�����ñ�
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// С��ID
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
		/// �̻���
		/// </summary>
		public string mch_id
		{
			set{ _mch_id=value;}
			get{return _mch_id;}
		}
		/// <summary>
		/// ��Կ
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

