using System;
namespace MobileSoft.Model.Club
{
	/// <summary>
	/// 实体类Tb_Club_Warehouse 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Club_Warehouse
	{
		public Tb_Club_Warehouse()
		{}
		#region Model
		private long _warehouseid;
		private string _organcode;
		private int? _commid;
		private string _warehousecode;
		private string _warehousename;
		private int? _isdelete;
		private int? _isdefault;
		private int? _isorgan;
		/// <summary>
		/// 
		/// </summary>
		public long WareHouseID
		{
			set{ _warehouseid=value;}
			get{return _warehouseid;}
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
		public int? CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WareHouseCode
		{
			set{ _warehousecode=value;}
			get{return _warehousecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WareHouseName
		{
			set{ _warehousename=value;}
			get{return _warehousename;}
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
		public int? IsDefault
		{
			set{ _isdefault=value;}
			get{return _isdefault;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsOrgan
		{
			set{ _isorgan=value;}
			get{return _isorgan;}
		}
		#endregion Model

	}
}

