using System;
namespace MobileSoft.Model.Management
{
	/// <summary>
	/// 实体类Tb_Management_Type 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Management_Type
	{
		public Tb_Management_Type()
		{}
		#region Model
		private string _code;
		private string _sortcode;
		private int? _sort;
		private string _typename;
		private string _typecode;
		private string _memo;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SortCode
		{
			set{ _sortcode=value;}
			get{return _sortcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeCode
		{
			set{ _typecode=value;}
			get{return _typecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

