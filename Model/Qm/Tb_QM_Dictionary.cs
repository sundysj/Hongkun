using System;
namespace HM.Model.Qm
{
	/// <summary>
	/// 实体类Tb_QM_Dictionary 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_QM_Dictionary
	{
		public Tb_QM_Dictionary()
		{}
		#region Model
		private string _id;
		private string _dtype;
		private string _name;
		private string _code;
		private int? _sort;
		private int? _isdelete;
		/// <summary>
		/// 品质字典设置
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 字典类别
		/// </summary>
		public string DType
		{
			set{ _dtype=value;}
			get{return _dtype;}
		}
		/// <summary>
		/// 名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 代码
		/// </summary>
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 序号
		/// </summary>
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
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

