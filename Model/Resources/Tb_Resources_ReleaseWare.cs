using System;
namespace MobileSoft.Model.Resources
{
	/// <summary>
	/// 实体类Tb_Resources_ReleaseWare 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Resources_ReleaseWare
	{
		public Tb_Resources_ReleaseWare()
		{}
		#region Model
		private long _releasewareid;
		private long _releaseid;
		private string _sourcearea;
		private string _factory;
		private string _brand;
		private string _version;
		private string _colors;
		private string _size;
		private string _weight;
		private DateTime? _listeddata;
		private string _shelflife;
		private string _releasewarecontent;
		private string _packinglist;
		private string _customerservice;
		private string _shippingmethod;
		private string _releasewareneedknow;
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseWareID
		{
			set{ _releasewareid=value;}
			get{return _releasewareid;}
		}
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
		public string SourceArea
		{
			set{ _sourcearea=value;}
			get{return _sourcearea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Factory
		{
			set{ _factory=value;}
			get{return _factory;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Brand
		{
			set{ _brand=value;}
			get{return _brand;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Version
		{
			set{ _version=value;}
			get{return _version;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Colors
		{
			set{ _colors=value;}
			get{return _colors;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Size
		{
			set{ _size=value;}
			get{return _size;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Weight
		{
			set{ _weight=value;}
			get{return _weight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ListedData
		{
			set{ _listeddata=value;}
			get{return _listeddata;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShelfLife
		{
			set{ _shelflife=value;}
			get{return _shelflife;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseWareContent
		{
			set{ _releasewarecontent=value;}
			get{return _releasewarecontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PackingList
		{
			set{ _packinglist=value;}
			get{return _packinglist;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustomerService
		{
			set{ _customerservice=value;}
			get{return _customerservice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShippingMethod
		{
			set{ _shippingmethod=value;}
			get{return _shippingmethod;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseWareNeedKnow
		{
			set{ _releasewareneedknow=value;}
			get{return _releasewareneedknow;}
		}
		#endregion Model

	}
}

