using System;
namespace MobileSoft.Model.Resources
{
	/// <summary>
	/// 实体类Tb_Resources_ReleaseDeduction 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Resources_ReleaseDeduction
	{
		public Tb_Resources_ReleaseDeduction()
		{}
		#region Model
		private long _releasedeductionid;
		private string _releasedeductioncontent;
		private string _releasedeductionneedknow;
		private long _releaseid;
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseDeductionID
		{
			set{ _releasedeductionid=value;}
			get{return _releasedeductionid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseDeductionContent
		{
			set{ _releasedeductioncontent=value;}
			get{return _releasedeductioncontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseDeductionNeedKnow
		{
			set{ _releasedeductionneedknow=value;}
			get{return _releasedeductionneedknow;}
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

