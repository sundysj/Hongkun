using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_Unify_RelationProperty 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Unify_RelationProperty
	{
		public Tb_Unify_RelationProperty()
		{}
		#region Model
		private long _id;
		private string _bussid;
		private long _corpid;
		private string _commid;
		private int? _sort;
		[DisplayName("小区对应商家ID")]
		public long ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		[DisplayName("商家ID")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string BussID
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("物管ID")]
		public long CorpID
		{
			set{ _corpid=value;}
			get{return _corpid;}
		}
		[DisplayName("小区ID")]
		public string CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		[DisplayName("")]
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		#endregion Model

	}
}

