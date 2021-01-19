using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_Dict_Province 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Dict_Province
	{
		public Tb_Dict_Province()
		{}
		#region Model
		private int _provinceid;
		private string _provincename;
		[DisplayName("")]
		public int ProvinceID
		{
			set{ _provinceid=value;}
			get{return _provinceid;}
		}
		[DisplayName("")]
		public string ProvinceName
		{
			set{ _provincename=value;}
			get{return _provincename;}
		}
		#endregion Model

	}
}

