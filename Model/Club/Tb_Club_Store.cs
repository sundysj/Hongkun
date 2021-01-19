using System;
namespace MobileSoft.Model.Club
{
	/// <summary>
	/// 实体类Tb_Club_Store 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Club_Store
	{
		public Tb_Club_Store()
		{}
		#region Model
		private int _infoid;
		private long _warehouseid;
		private long _merchid;
		private decimal _quantity;
		/// <summary>
		/// 
		/// </summary>
		public int InfoId
		{
			set{ _infoid=value;}
			get{return _infoid;}
		}
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
		public long MerchID
		{
			set{ _merchid=value;}
			get{return _merchid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Quantity
		{
			set{ _quantity=value;}
			get{return _quantity;}
		}
		#endregion Model

	}
}

