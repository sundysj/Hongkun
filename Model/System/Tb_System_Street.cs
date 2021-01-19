using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// 实体类Tb_System_Street 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_System_Street
	{
		public Tb_System_Street()
		{}
		#region Model
		private int _streetid;
		private int _boroughid;
		private string _streetname;
		/// <summary>
		/// 
		/// </summary>
		public int StreetID
		{
			set{ _streetid=value;}
			get{return _streetid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int BoroughID
		{
			set{ _boroughid=value;}
			get{return _boroughid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StreetName
		{
			set{ _streetname=value;}
			get{return _streetname;}
		}
		#endregion Model

	}
}

