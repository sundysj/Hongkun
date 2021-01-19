using System;
namespace MobileSoft.Model.Sys
{
	/// <summary>
	/// 实体类Tb_Sys_Organ 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Sys_Organ
	{
		public Tb_Sys_Organ()
		{}
		#region Model
		private string _organcode;
		private string _organname;
		private int? _isdelete;
		private int? _iscomp;
		private int? _num;
		/// <summary>
		/// 
		/// </summary>
		public string OrganCode
		{
			set{ _organcode=value;}
			get{return _organcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrganName
		{
			set{ _organname=value;}
			get{return _organname;}
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
		public int? IsComp
		{
			set{ _iscomp=value;}
			get{return _iscomp;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Num
		{
			set{ _num=value;}
			get{return _num;}
		}
		#endregion Model

	}
}

