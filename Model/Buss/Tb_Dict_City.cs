using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_Dict_City 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Dict_City
	{
		public Tb_Dict_City()
		{}
		#region Model
		private int _cityid;
		private int? _provinceid;
		private string _cityname;
		[DisplayName("")]
		public int CityID
		{
			set{ _cityid=value;}
			get{return _cityid;}
		}
		[DisplayName("")]
		public int? ProvinceID
		{
			set{ _provinceid=value;}
			get{return _provinceid;}
		}
		[DisplayName("")]
		public string CityName
		{
			set{ _cityname=value;}
			get{return _cityname;}
		}
		#endregion Model

	}
}

