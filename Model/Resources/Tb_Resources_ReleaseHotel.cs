using System;
namespace MobileSoft.Model.Resources
{
	/// <summary>
	/// ʵ����Tb_Resources_ReleaseHotel ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Resources_ReleaseHotel
	{
		public Tb_Resources_ReleaseHotel()
		{}
		#region Model
		private long _releasehotelid;
		private string _releasehotelcontent;
		private string _releasehotelneedknow;
		private long _releaseid;
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseHotelID
		{
			set{ _releasehotelid=value;}
			get{return _releasehotelid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseHotelContent
		{
			set{ _releasehotelcontent=value;}
			get{return _releasehotelcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseHotelNeedKnow
		{
			set{ _releasehotelneedknow=value;}
			get{return _releasehotelneedknow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseID
		{
			set{ _releaseid=value;}
			get{return _releaseid;}
		}
		#endregion Model

	}
}

