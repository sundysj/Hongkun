using System;
namespace MobileSoft.Model.Sys
{
	/// <summary>
	/// 实体类Tb_Sys_TakePicPark 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Sys_TakePicPark
	{
		public Tb_Sys_TakePicPark()
		{}
		#region Model
		private long _statid;
		private int? _stattype;
		private int? _commid;
		private string _organcode;
		private DateTime? _statdate;
		private long _carparkid;
		private string _parktype;
		private int? _counts;
		private decimal? _area;
		/// <summary>
		/// 
		/// </summary>
		public long StatID
		{
			set{ _statid=value;}
			get{return _statid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? StatType
		{
			set{ _stattype=value;}
			get{return _stattype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrganCode
		{
			set{ _organcode=value;}
			get{return _organcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? StatDate
		{
			set{ _statdate=value;}
			get{return _statdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long CarparkID
		{
			set{ _carparkid=value;}
			get{return _carparkid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ParkType
		{
			set{ _parktype=value;}
			get{return _parktype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Counts
		{
			set{ _counts=value;}
			get{return _counts;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Area
		{
			set{ _area=value;}
			get{return _area;}
		}
		#endregion Model

	}
}

