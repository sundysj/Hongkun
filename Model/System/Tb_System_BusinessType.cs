using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// 实体类Tb_System_BusinessType 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_System_BusinessType
	{
		public Tb_System_BusinessType()
		{}
		#region Model
		private string _businesstypecode;
		private string _businesscategory;
		private string _businesstypename;
		/// <summary>
		/// 
		/// </summary>
		public string BusinessTypeCode
		{
			set{ _businesstypecode=value;}
			get{return _businesstypecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BusinessCategory
		{
			set{ _businesscategory=value;}
			get{return _businesscategory;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BusinessTypeName
		{
			set{ _businesstypename=value;}
			get{return _businesstypename;}
		}
		#endregion Model

	}
}

