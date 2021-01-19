using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_Dict_Borough 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Dict_Borough
	{
		public Tb_Dict_Borough()
		{}
		#region Model
		private int _boroughid;
		private int? _cityid;
		private string _boroughname;
		[DisplayName("")]
		public int BoroughID
		{
			set{ _boroughid=value;}
			get{return _boroughid;}
		}
		[DisplayName("")]
		public int? CityID
		{
			set{ _cityid=value;}
			get{return _cityid;}
		}
		[DisplayName("")]
		public string BoroughName
		{
			set{ _boroughname=value;}
			get{return _boroughname;}
		}
		#endregion Model

	}
}

