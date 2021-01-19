using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_Resources_Specifications 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Resources_Specifications
	{
		public Tb_Resources_Specifications()
		{}
		#region Model
		private string _id;
		private long _bussid;
		private string _propertyid;
		private string _specname;
		private int _sort;
		[DisplayName("商品属性规格ID")]
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		[DisplayName("商家ID")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public long BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("商品属性ID")]
		public string PropertyId
		{
			set{ _propertyid=value;}
			get{return _propertyid;}
		}
		[DisplayName("商品属性规格名称")]
		public string SpecName
		{
			set{ _specname=value;}
			get{return _specname;}
		}
		[DisplayName("排列序号")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		#endregion Model

	}
}

