using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_Customer_List 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Customer_List
	{
		public Tb_Customer_List()
		{}
		#region Model
		private string _id;
		private string _bussid;
		private string _userid;
		private string _custname;
		private string _mobile;
		[DisplayName("")]
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		[DisplayName("")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("")]
		public string UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		[DisplayName("")]
		public string CustName
		{
			set{ _custname=value;}
			get{return _custname;}
		}
		[DisplayName("")]
		public string Mobile
		{
			set{ _mobile=value;}
			get{return _mobile;}
		}
		#endregion Model

	}
}

