using System;
namespace MobileSoft.Model.Resources
{
	/// <summary>
	/// ʵ����Tb_Resources_ReleaseMember ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Resources_ReleaseMember
	{
		public Tb_Resources_ReleaseMember()
		{}
		#region Model
		private long _releasememberid;
		private string _releasemembercontent;
		private string _releasememberservicetext;
		private string _releasememberneedknow;
		private long _releaseid;
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseMemberID
		{
			set{ _releasememberid=value;}
			get{return _releasememberid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseMemberContent
		{
			set{ _releasemembercontent=value;}
			get{return _releasemembercontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseMemberServiceText
		{
			set{ _releasememberservicetext=value;}
			get{return _releasememberservicetext;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseMemberNeedKnow
		{
			set{ _releasememberneedknow=value;}
			get{return _releasememberneedknow;}
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

