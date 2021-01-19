using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_Region 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_Region
	{
		public Tb_HSPR_Region()
		{}
		#region Model
		private long _regionid;
		private int? _commid;
		private string _regionname;
		private int? _regionsnum;
		private int? _isdelete;
		private Guid _regionsynchcode;
		private int? _synchflag;
		/// <summary>
		/// 
		/// </summary>
		public long RegionID
		{
			set{ _regionid=value;}
			get{return _regionid;}
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
		public string RegionName
		{
			set{ _regionname=value;}
			get{return _regionname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RegionSNum
		{
			set{ _regionsnum=value;}
			get{return _regionsnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid RegionSynchCode
		{
			set{ _regionsynchcode=value;}
			get{return _regionsynchcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SynchFlag
		{
			set{ _synchflag=value;}
			get{return _synchflag;}
		}
		#endregion Model

	}
}

