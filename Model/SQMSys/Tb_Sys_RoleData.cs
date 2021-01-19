using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// 实体类Tb_Sys_RoleData 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Sys_RoleData
	{
		public Tb_Sys_RoleData()
		{}
		#region Model
		private long _iid;
		private Guid _streetcode;
		private string _rolecode;
		private long _commid;
		private Guid _commcode;
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
		public Guid StreetCode
		{
			set{ _streetcode=value;}
			get{return _streetcode;}
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
		public long CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid CommCode
		{
			set{ _commcode=value;}
			get{return _commcode;}
		}
		#endregion Model

	}
}

