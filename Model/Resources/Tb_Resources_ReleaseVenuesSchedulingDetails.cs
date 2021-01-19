using System;
namespace MobileSoft.Model.Resources
{
	/// <summary>
	/// 实体类Tb_Resources_ReleaseVenuesSchedulingDetails 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Resources_ReleaseVenuesSchedulingDetails
	{
		public Tb_Resources_ReleaseVenuesSchedulingDetails()
		{}
		#region Model
		private long _releasevenuesschedulingdetailsid;
		private string _releasevenuesschedulingdetailsstarttime;
		private string _releasevenuesschedulingdetailsendtime;
		private decimal? _releasevenuesschedulingdetailssaleprice;
		private decimal? _releasevenuesschedulingdetailsdiscountprice;
		private long _releasevenuesid;
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseVenuesSchedulingDetailsID
		{
			set{ _releasevenuesschedulingdetailsid=value;}
			get{return _releasevenuesschedulingdetailsid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseVenuesSchedulingDetailsStartTime
		{
			set{ _releasevenuesschedulingdetailsstarttime=value;}
			get{return _releasevenuesschedulingdetailsstarttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseVenuesSchedulingDetailsEndTime
		{
			set{ _releasevenuesschedulingdetailsendtime=value;}
			get{return _releasevenuesschedulingdetailsendtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ReleaseVenuesSchedulingDetailsSalePrice
		{
			set{ _releasevenuesschedulingdetailssaleprice=value;}
			get{return _releasevenuesschedulingdetailssaleprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ReleaseVenuesSchedulingDetailsDiscountPrice
		{
			set{ _releasevenuesschedulingdetailsdiscountprice=value;}
			get{return _releasevenuesschedulingdetailsdiscountprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseVenuesID
		{
			set{ _releasevenuesid=value;}
			get{return _releasevenuesid;}
		}
		#endregion Model

	}
}

