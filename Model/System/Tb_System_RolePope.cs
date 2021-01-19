using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// 实体类Tb_System_RolePope 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_System_RolePope
	{
		public Tb_System_RolePope()
		{}
		#region Model
		private long _iid;
		private string _pnodecode;
		private string _rolecode;
		private string _functions;
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
		public string PNodeCode
		{
			set{ _pnodecode=value;}
			get{return _pnodecode;}
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
		public string Functions
		{
			set{ _functions=value;}
			get{return _functions;}
		}
		#endregion Model

	}
}

