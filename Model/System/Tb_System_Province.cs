using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// 实体类Tb_System_Province 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_System_Province
	{
		public Tb_System_Province()
		{}
		#region Model
		private int _provinceid;
		private string _provincename;
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
		public string ProvinceName
		{
			set{ _provincename=value;}
			get{return _provincename;}
		}
		#endregion Model

	}
}

