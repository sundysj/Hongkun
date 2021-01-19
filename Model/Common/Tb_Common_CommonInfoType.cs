using System;
namespace MobileSoft.Model.Common
{
	/// <summary>
	/// 实体类Tb_Common_CommonInfoType 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Common_CommonInfoType
	{
		public Tb_Common_CommonInfoType()
		{}
		#region Model
		private string _typecode;
		private string _typename;
		private string _organcode;
		private string _type;
		/// <summary>
		/// 
		/// </summary>
		public string TypeCode
		{
			set{ _typecode=value;}
			get{return _typecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
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
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		#endregion Model

	}
}

