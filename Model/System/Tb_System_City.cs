using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// 实体类Tb_System_City 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_System_City
	{
		public Tb_System_City()
		{}
		#region Model
		private int _cityid;
		private int _provinceid;
		private string _cityname;
		/// <summary>
		/// 
		/// </summary>
		public int CityID
		{
			set{ _cityid=value;}
			get{return _cityid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ProvinceID
		{
			set{ _provinceid=value;}
			get{return _provinceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CityName
		{
			set{ _cityname=value;}
			get{return _cityname;}
		}
		#endregion Model

	}
}

