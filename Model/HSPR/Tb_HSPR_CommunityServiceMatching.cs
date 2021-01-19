using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_CommunityServiceMatching 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_CommunityServiceMatching
	{
		public Tb_HSPR_CommunityServiceMatching()
		{}
		#region Model
		private long _scid;
		private int? _commid;
		private int? _sctype;
		private string _sctycode;
		private int? _scnum;
		private string _scname;
		private string _scaddress;
		private string _sclinkman;
		private string _sclinkphone;
		private string _scmemo;
		/// <summary>
		/// 
		/// </summary>
		public long ScID
		{
			set{ _scid=value;}
			get{return _scid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ScType
		{
			set{ _sctype=value;}
			get{return _sctype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ScTyCode
		{
			set{ _sctycode=value;}
			get{return _sctycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ScNum
		{
			set{ _scnum=value;}
			get{return _scnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ScName
		{
			set{ _scname=value;}
			get{return _scname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ScAddress
		{
			set{ _scaddress=value;}
			get{return _scaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ScLinkMan
		{
			set{ _sclinkman=value;}
			get{return _sclinkman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ScLinkPhone
		{
			set{ _sclinkphone=value;}
			get{return _sclinkphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ScMemo
		{
			set{ _scmemo=value;}
			get{return _scmemo;}
		}
		#endregion Model

	}
}

