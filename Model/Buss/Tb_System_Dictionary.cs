using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_System_Dictionary 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_System_Dictionary
	{
		public Tb_System_Dictionary()
		{}
		#region Model
		private string _dictionarycode;
		private string _bussid;
		private string _dictionarytype;
		private string _dictionaryname;
		private int? _sort;
		private int? _isdelete;
		[DisplayName("字典表ID")]
		public string DictionaryCode
		{
			set{ _dictionarycode=value;}
			get{return _dictionarycode;}
		}
		[DisplayName("商家ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("字典类别")]
		public string DictionaryType
		{
			set{ _dictionarytype=value;}
			get{return _dictionarytype;}
		}
		[DisplayName("字典名称")]
		public string DictionaryName
		{
			set{ _dictionaryname=value;}
			get{return _dictionaryname;}
		}
		[DisplayName("序号")]
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		[DisplayName("是否删除")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

