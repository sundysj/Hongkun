using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// ʵ����Tb_Sys_PowerClick ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Sys_PowerClick
	{
		public Tb_Sys_PowerClick()
		{}
		#region Model
		private long _iid;
		private string _pnodecode;
		private string _usercode;
		private long _clickcount;
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
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ClickCount
		{
			set{ _clickcount=value;}
			get{return _clickcount;}
		}
		#endregion Model

	}
}

