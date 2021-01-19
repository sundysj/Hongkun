using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_CommSocialScope 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_CommSocialScope
	{
		public Tb_HSPR_CommSocialScope()
		{}
		#region Model
		private string _scopecode;
		private string _custcode;
		private string _circleid;
		private long _commid;
		private long _bussid;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public string ScopeCode
		{
			set{ _scopecode=value;}
			get{return _scopecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustCode
		{
			set{ _custcode=value;}
			get{return _custcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CircleID
		{
			set{ _circleid=value;}
			get{return _circleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long BussID
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

