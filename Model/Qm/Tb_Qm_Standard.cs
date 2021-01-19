using System;
namespace HM.Model.Qm
{
	/// <summary>
	/// 实体类Tb_Qm_Standard 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Qm_Standard
	{
		public Tb_Qm_Standard()
		{}
		#region Model
		private string _id;
		private string _code;
		private string _suitableitemtypeid;
		private string _professional;
		private string _type;
		private string _typedescription;
		private string _checkstandard;
		private string _checkway;
		private decimal _point;
		private int _iscoerce;
		private int _isuse;
		private int? _isdelete;
		private int _sort;
		/// <summary>
		/// 标准主键Id
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
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
		/// 适用项目类型
		/// </summary>
		public string SuitableItemTypeId
		{
			set{ _suitableitemtypeid=value;}
			get{return _suitableitemtypeid;}
		}
		/// <summary>
		/// 所属专业
		/// </summary>
		public string Professional
		{
			set{ _professional=value;}
			get{return _professional;}
		}
		/// <summary>
		/// 所属类型
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 类别描述
		/// </summary>
		public string TypeDescription
		{
			set{ _typedescription=value;}
			get{return _typedescription;}
		}
		/// <summary>
		/// 核查标准
		/// </summary>
		public string CheckStandard
		{
			set{ _checkstandard=value;}
			get{return _checkstandard;}
		}
		/// <summary>
		/// 审核标准
		/// </summary>
		public string CheckWay
		{
			set{ _checkway=value;}
			get{return _checkway;}
		}
		/// <summary>
		/// 分值
		/// </summary>
		public decimal Point
		{
			set{ _point=value;}
			get{return _point;}
		}
		/// <summary>
		/// 是否强制
		/// </summary>
		public int IsCoerce
		{
			set{ _iscoerce=value;}
			get{return _iscoerce;}
		}
		/// <summary>
		/// 是否启用
		/// </summary>
		public int IsUse
		{
			set{ _isuse=value;}
			get{return _isuse;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		#endregion Model

	}
}

