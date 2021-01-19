using System;
namespace MobileSoft.Model.Unified
{
	/// <summary>
	/// 实体类Tb_AlipayCertifiate 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_AlipayCertifiate
	{
		public Tb_AlipayCertifiate()
		{}
		#region Model
		private string _id;
		private string _communityid;
		private string _partner;
		private string _seller_id;
		private string _extern_token;
		private string _alipay_public_key;
		private string _private_key;
		/// <summary>
		/// 
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CommunityId
		{
			set{ _communityid=value;}
			get{return _communityid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string partner
		{
			set{ _partner=value;}
			get{return _partner;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string seller_id
		{
			set{ _seller_id=value;}
			get{return _seller_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string extern_token
		{
			set{ _extern_token=value;}
			get{return _extern_token;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string alipay_public_key
		{
			set{ _alipay_public_key=value;}
			get{return _alipay_public_key;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string private_key
		{
			set{ _private_key=value;}
			get{return _private_key;}
		}
		#endregion Model

	}
}

