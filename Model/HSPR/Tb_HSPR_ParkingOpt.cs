using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_ParkingOpt 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_ParkingOpt
	{
		public Tb_HSPR_ParkingOpt()
		{}
		#region Model
		private Guid _scode;
		private long _parkid;
		private string _optusercode;
		private Guid _optcode;
		/// <summary>
		/// 
		/// </summary>
		public Guid SCode
		{
			set{ _scode=value;}
			get{return _scode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ParkID
		{
			set{ _parkid=value;}
			get{return _parkid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OptUserCode
		{
			set{ _optusercode=value;}
			get{return _optusercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid OptCode
		{
			set{ _optcode=value;}
			get{return _optcode;}
		}
		#endregion Model

	}
}

