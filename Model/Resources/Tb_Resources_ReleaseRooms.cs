using System;
namespace MobileSoft.Model.Resources
{
	/// <summary>
	/// ʵ����Tb_Resources_ReleaseRooms ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Resources_ReleaseRooms
	{
		public Tb_Resources_ReleaseRooms()
		{}
		#region Model
		private long _releaseroomsid;
		private long _releaseid;
		private string _releaseroomscontent;
		private string _releaseroomsneedknow;
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseRoomsID
		{
			set{ _releaseroomsid=value;}
			get{return _releaseroomsid;}
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
		public string ReleaseRoomsContent
		{
			set{ _releaseroomscontent=value;}
			get{return _releaseroomscontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseRoomsNeedKnow
		{
			set{ _releaseroomsneedknow=value;}
			get{return _releaseroomsneedknow;}
		}
		#endregion Model

	}
}

