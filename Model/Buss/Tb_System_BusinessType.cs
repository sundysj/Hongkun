using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
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
		[DisplayName("商品大类表ID")]
		public string BusinessTypeCode
		{
			set{ _businesstypecode=value;}
			get{return _businesstypecode;}
		}
		[DisplayName("商品目录类别标识")]
		public string BusinessCategory
		{
			set{ _businesscategory=value;}
			get{return _businesscategory;}
		}
		[DisplayName("商品大类类别名称")]
		public string BusinessTypeName
		{
			set{ _businesstypename=value;}
			get{return _businesstypename;}
		}
		#endregion Model

	}
}

