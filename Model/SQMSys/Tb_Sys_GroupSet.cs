using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// ʵ����Tb_Sys_GroupSet ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Sys_GroupSet
	{
		public Tb_Sys_GroupSet()
		{}
		#region Model
		private long _iid;
		private string _usercode;
		private Guid _streetcode;
		private string _groupcode;
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
		public Guid StreetCode
		{
			set{ _streetcode=value;}
			get{return _streetcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GroupCode
		{
			set{ _groupcode=value;}
			get{return _groupcode;}
		}
		#endregion Model

	}
}

