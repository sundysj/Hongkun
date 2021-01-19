using System;
namespace MobileSoft.Model.Resources
{
	/// <summary>
	/// 实体类Tb_Resources_Release 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Resources_Release
	{
		public Tb_Resources_Release()
		{}
		#region Model
		private long _releaseid;
		private long _resourcesid;
		private string _releaseadcontent;
		private decimal? _releasecount;
		private bool _isgroupbuy;
		private decimal? _groupbuyprice;
		private DateTime? _groupbuystartdata;
		private DateTime? _groupbuyenddata;
		private string _paymenttype;
		private long _bussid;
		private bool _isstop;
		private int _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseID
		{
			set{ _releaseid=value;}
			get{return _releaseid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ResourcesID
		{
			set{ _resourcesid=value;}
			get{return _resourcesid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseAdContent
		{
			set{ _releaseadcontent=value;}
			get{return _releaseadcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ReleaseCount
		{
			set{ _releasecount=value;}
			get{return _releasecount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsGroupBuy
		{
			set{ _isgroupbuy=value;}
			get{return _isgroupbuy;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? GroupBuyPrice
		{
			set{ _groupbuyprice=value;}
			get{return _groupbuyprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? GroupBuyStartData
		{
			set{ _groupbuystartdata=value;}
			get{return _groupbuystartdata;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? GroupBuyEndData
		{
			set{ _groupbuyenddata=value;}
			get{return _groupbuyenddata;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PaymentType
		{
			set{ _paymenttype=value;}
			get{return _paymenttype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsStop
		{
			set{ _isstop=value;}
			get{return _isstop;}
		}
		/// <summary>
		/// 
		/// </summary>
        public int IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

