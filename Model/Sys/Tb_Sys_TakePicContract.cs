using System;
namespace MobileSoft.Model.Sys
{
	/// <summary>
	/// 实体类Tb_Sys_TakePicContract 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Sys_TakePicContract
	{
		public Tb_Sys_TakePicContract()
		{}
		#region Model
		private long _statid;
		private int? _stattype;
		private int? _commid;
		private string _organcode;
		private DateTime? _statdate;
		private int? _contractcounts;
		private int? _contractcounts1;
		private int? _contractcounts2;
		private int? _feescontcounts;
		private int? _feescontcounts1;
		private int? _feescontcounts2;
		private int? _feescontcounts3;
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
		public int? ContractCounts
		{
			set{ _contractcounts=value;}
			get{return _contractcounts;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ContractCounts1
		{
			set{ _contractcounts1=value;}
			get{return _contractcounts1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ContractCounts2
		{
			set{ _contractcounts2=value;}
			get{return _contractcounts2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FeesContCounts
		{
			set{ _feescontcounts=value;}
			get{return _feescontcounts;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FeesContCounts1
		{
			set{ _feescontcounts1=value;}
			get{return _feescontcounts1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FeesContCounts2
		{
			set{ _feescontcounts2=value;}
			get{return _feescontcounts2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FeesContCounts3
		{
			set{ _feescontcounts3=value;}
			get{return _feescontcounts3;}
		}
		#endregion Model

	}
}

