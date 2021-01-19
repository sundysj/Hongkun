using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// ʵ����Tb_HSPR_ParkingOpt ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_ParkingOpt
	{
		public Tb_HSPR_ParkingOpt()
		{}
		#region Model
		private Guid _scode;
		private long _parkid;
		private string _optusercode;
		private Guid _optcode;
		/// <summary>
		/// 
		/// </summary>
		public Guid SCode
		{
			set{ _scode=value;}
			get{return _scode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ParkID
		{
			set{ _parkid=value;}
			get{return _parkid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OptUserCode
		{
			set{ _optusercode=value;}
			get{return _optusercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid OptCode
		{
			set{ _optcode=value;}
			get{return _optcode;}
		}
		#endregion Model

	}
}

