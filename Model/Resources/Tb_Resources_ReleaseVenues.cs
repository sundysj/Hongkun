using System;
using System.Collections.Generic;
namespace MobileSoft.Model.Resources
{
	/// <summary>
	/// 实体类Tb_Resources_ReleaseVenues 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Resources_ReleaseVenues
	{
		public Tb_Resources_ReleaseVenues()
		{}
		#region Model
		private long _releasevenuesid;
		private long _releaseid;
		private string _releasevenuescontent;
		private string _releasevenuesneedknow;
        /// <summary>
        /// 排班设置主表
        /// </summary>
        private Tb_Resources_ReleaseVenuesScheduling _releasevenuesscheduling=new Tb_Resources_ReleaseVenuesScheduling ();
        /// <summary>
        /// 排班设置明细
        /// </summary>
        private List<Tb_Resources_ReleaseVenuesSchedulingDetails> _releasevenuesschedulingdetails = new List<Tb_Resources_ReleaseVenuesSchedulingDetails>();

        public List<Tb_Resources_ReleaseVenuesSchedulingDetails> ReleaseVenuesSchedulingDetails
        {
            set { _releasevenuesschedulingdetails = value; }
            get { return _releasevenuesschedulingdetails; }
        }

        public Tb_Resources_ReleaseVenuesScheduling ReleaseVenuesScheduling
        {
            set { _releasevenuesscheduling = value; }
            get { return _releasevenuesscheduling; }
        }
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseVenuesID
		{
			set{ _releasevenuesid=value;}
			get{return _releasevenuesid;}
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
		public string ReleaseVenuesContent
		{
			set{ _releasevenuescontent=value;}
			get{return _releasevenuescontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseVenuesNeedKnow
		{
			set{ _releasevenuesneedknow=value;}
			get{return _releasevenuesneedknow;}
		}
		#endregion Model

	}
}

