using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// ʵ����Tb_System_Borough ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_System_Borough
	{
		public Tb_System_Borough()
		{}
		#region Model
		private int _boroughid;
		private int _cityid;
		private string _boroughname;
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
		public int CityID
		{
			set{ _cityid=value;}
			get{return _cityid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BoroughName
		{
			set{ _boroughname=value;}
			get{return _boroughname;}
		}
		#endregion Model

	}
}

