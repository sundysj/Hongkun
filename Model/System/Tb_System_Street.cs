using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// ʵ����Tb_System_Street ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_System_Street
	{
		public Tb_System_Street()
		{}
		#region Model
		private int _streetid;
		private int _boroughid;
		private string _streetname;
		/// <summary>
		/// 
		/// </summary>
		public int StreetID
		{
			set{ _streetid=value;}
			get{return _streetid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int BoroughID
		{
			set{ _boroughid=value;}
			get{return _boroughid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StreetName
		{
			set{ _streetname=value;}
			get{return _streetname;}
		}
		#endregion Model

	}
}

