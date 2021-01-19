using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// 实体类Tb_Sys_Department 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Sys_Department
	{
		public Tb_Sys_Department()
		{}
		#region Model
		private string _depcode;
		private Guid _streetcode;
		private string _depname;
		private string _principal;
		private string _memo;
		private int? _isdelete;
		private int? _sort;
		private int? _ishide;
		/// <summary>
		/// 
		/// </summary>
		public string DepCode
		{
			set{ _depcode=value;}
			get{return _depcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid StreetCode
		{
			set{ _streetcode=value;}
			get{return _streetcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DepName
		{
			set{ _depname=value;}
			get{return _depname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Principal
		{
			set{ _principal=value;}
			get{return _principal;}
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
		public int? IsHide
		{
			set{ _ishide=value;}
			get{return _ishide;}
		}
		#endregion Model

	}
}

