using System;
namespace MobileSoft.Model.Resources
{
	/// <summary>
	/// ʵ����Tb_Resources_ReleaseTrain ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Resources_ReleaseTrain
	{
		public Tb_Resources_ReleaseTrain()
		{}
		#region Model
		private long _releasetrainid;
		private string _releasetraincontent;
		private string _releasetrainneedknow;
		private long _releaseid;
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseTrainID
		{
			set{ _releasetrainid=value;}
			get{return _releasetrainid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseTrainContent
		{
			set{ _releasetraincontent=value;}
			get{return _releasetraincontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseTrainNeedKnow
		{
			set{ _releasetrainneedknow=value;}
			get{return _releasetrainneedknow;}
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

