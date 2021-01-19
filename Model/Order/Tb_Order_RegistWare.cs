using System;
namespace MobileSoft.Model.Order
{
	/// <summary>
	/// 实体类Tb_Order_RegistWare 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Order_RegistWare
	{
		public Tb_Order_RegistWare()
		{}
		#region Model
		private Guid _registwareid;
		private long _releaseid;
		private decimal? _count;
		private string _registid;
		private int? _isdelete;
        private decimal? _resourcessaleprice;
        private decimal? _resourcesdiscountprice;
        private decimal? _groupbuyprice;
		/// <summary>
		/// 
		/// </summary>
		public Guid RegistWareID
		{
			set{ _registwareid=value;}
			get{return _registwareid;}
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
		public decimal? Count
		{
			set{ _count=value;}
			get{return _count;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegistID
		{
			set{ _registid=value;}
			get{return _registid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
        /// <summary>
        /// 
        /// </summary>
        public decimal? GroupBuyPrice
        {
            set { _groupbuyprice = value; }
            get { return _groupbuyprice; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? ResourcesDisCountPrice
        {
            set { _resourcesdiscountprice = value; }
            get { return _resourcesdiscountprice; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? ResourcesSalePrice
        {
            set { _resourcessaleprice = value; }
            get { return _resourcessaleprice; }
        }

		#endregion Model

	}
}

