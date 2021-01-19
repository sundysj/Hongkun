using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_FixedAssetsLossTableDetail 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_FixedAssetsLossTableDetail
	{
		public Tb_OAPublicWork_FixedAssetsLossTableDetail()
		{}
		#region Model
		private int _infoid;
		private int? _fixedassetslosstableid;
		private string _pname;
		private string _model;
		private string _unit;
		private decimal? _price;
		private int? _quantity;
		private decimal? _originalvalue;
		private decimal? _networth;
		/// <summary>
		/// 
		/// </summary>
		public int InfoID
		{
			set{ _infoid=value;}
			get{return _infoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FixedAssetsLossTableID
		{
			set{ _fixedassetslosstableid=value;}
			get{return _fixedassetslosstableid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PName
		{
			set{ _pname=value;}
			get{return _pname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Model
		{
			set{ _model=value;}
			get{return _model;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Unit
		{
			set{ _unit=value;}
			get{return _unit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Quantity
		{
			set{ _quantity=value;}
			get{return _quantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? OriginalValue
		{
			set{ _originalvalue=value;}
			get{return _originalvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? NetWorth
		{
			set{ _networth=value;}
			get{return _networth;}
		}
		#endregion Model

	}
}

