using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_Resources_Property 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Resources_Property
	{
		public Tb_Resources_Property()
		{}
		#region Model
		private string _id;
		private long _bussid;
		private string _propertyname;
		private int _sort;
		[DisplayName("商品属性表ID")]
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		[DisplayName("商家ID")]
		public long BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("商品属性名称")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string PropertyName
		{
			set{ _propertyname=value;}
			get{return _propertyname;}
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

