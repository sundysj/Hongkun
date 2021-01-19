using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_Resources_Store 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Resources_Store
	{
		public Tb_Resources_Store()
		{}
		#region Model
		private string _id;
		private string _bussid;
		private string _resourcesid;
		private decimal _quantity;
		private DateTime _issuedate;
		[DisplayName("商品库存")]
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		[DisplayName("商家ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("资源ID")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string ResourcesID
		{
			set{ _resourcesid=value;}
			get{return _resourcesid;}
		}
		[DisplayName("数量")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public decimal Quantity
		{
			set{ _quantity=value;}
			get{return _quantity;}
		}
		[DisplayName("进货日期")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public DateTime IssueDate
		{
			set{ _issuedate=value;}
			get{return _issuedate;}
		}
		#endregion Model

	}
}

