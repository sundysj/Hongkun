using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_Resources_PropertyRelation 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Resources_PropertyRelation
	{
		public Tb_Resources_PropertyRelation()
		{}
		#region Model
		private string _id;
		private long _bussid;
		private string _resourcesid;
		private string _propertyid;
		[DisplayName("商品属性列表ID")]
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
		[DisplayName("商品ID")]
		public string ResourcesID
		{
			set{ _resourcesid=value;}
			get{return _resourcesid;}
		}
		[DisplayName("属性ID")]
		public string PropertyId
		{
			set{ _propertyid=value;}
			get{return _propertyid;}
		}
		#endregion Model

	}
}

