using System;
namespace MobileSoft.Model.Resources
{
	/// <summary>
	/// 实体类Tb_Resources_ReleaseVenuesSet 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Resources_ReleaseVenuesSet
	{
		public Tb_Resources_ReleaseVenuesSet()
		{}
		#region Model
		private long _releasevenuessetid;
		private string _releasevenuessetstarttime;
		private string _releasevenuessetendtime;
		private long _bussid;
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseVenuesSetID
		{
			set{ _releasevenuessetid=value;}
			get{return _releasevenuessetid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseVenuesSetStartTime
		{
			set{ _releasevenuessetstarttime=value;}
			get{return _releasevenuessetstarttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseVenuesSetEndTime
		{
			set{ _releasevenuessetendtime=value;}
			get{return _releasevenuessetendtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		#endregion Model

	}
}

