using System;
namespace MobileSoft.Model.Resources
{
	/// <summary>
	/// ʵ����Tb_Resources_ReleaseVenuesScheduling ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Resources_ReleaseVenuesScheduling
	{
		public Tb_Resources_ReleaseVenuesScheduling()
		{}
		#region Model
		private long _releasevenuesschedulingid;
		private DateTime? _releasevenuesschedulingstartdate;
		private DateTime? _releasevenuesschedulingenddate;
		private decimal? _releasevenuesschedulingcount;
		private long _releasevenuesid;
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseVenuesSchedulingID
		{
			set{ _releasevenuesschedulingid=value;}
			get{return _releasevenuesschedulingid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReleaseVenuesSchedulingStartDate
		{
			set{ _releasevenuesschedulingstartdate=value;}
			get{return _releasevenuesschedulingstartdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReleaseVenuesSchedulingEndDate
		{
			set{ _releasevenuesschedulingenddate=value;}
			get{return _releasevenuesschedulingenddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ReleaseVenuesSchedulingCount
		{
			set{ _releasevenuesschedulingcount=value;}
			get{return _releasevenuesschedulingcount;}
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

