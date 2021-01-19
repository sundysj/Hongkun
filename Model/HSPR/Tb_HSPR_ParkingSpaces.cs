using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_ParkingSpaces 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_ParkingSpaces
	{
		public Tb_HSPR_ParkingSpaces()
		{}
		#region Model
		private long _parkingtypeid;
		private int? _commid;
		private int? _typesnum;
		private string _typename;
		private string _typememo;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public long ParkingTypeID
		{
			set{ _parkingtypeid=value;}
			get{return _parkingtypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TypeSNum
		{
			set{ _typesnum=value;}
			get{return _typesnum;}
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
		public string TypeMemo
		{
			set{ _typememo=value;}
			get{return _typememo;}
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

