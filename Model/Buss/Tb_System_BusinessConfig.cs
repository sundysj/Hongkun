using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_System_BusinessConfig 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_System_BusinessConfig
	{
		public Tb_System_BusinessConfig()
		{}
		#region Model
		private string _id;
		private string _communityid;
		private string _bussid;
		private string _value;
		[DisplayName("")]
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		[DisplayName("运营系统小区ID")]
		public string CommunityId
		{
			set{ _communityid=value;}
			get{return _communityid;}
		}
		[DisplayName("商家Id")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("")]
		public string Value
		{
			set{ _value=value;}
			get{return _value;}
		}
		#endregion Model

	}
}

