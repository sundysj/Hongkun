using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// 实体类Tb_Sys_PowerNodeDep 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Sys_PowerNodeDep
	{
		public Tb_Sys_PowerNodeDep()
		{}
		#region Model
		private long _iid;
		private string _usercode;
		private string _depcode;
		private string _depname;
		private string _rolecode;
		private int? _ck;
		/// <summary>
		/// 
		/// </summary>
		public long IID
		{
			set{ _iid=value;}
			get{return _iid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
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
		public string DepName
		{
			set{ _depname=value;}
			get{return _depname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RoleCode
		{
			set{ _rolecode=value;}
			get{return _rolecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Ck
		{
			set{ _ck=value;}
			get{return _ck;}
		}
		#endregion Model

	}
}

