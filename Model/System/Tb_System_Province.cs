using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// ʵ����Tb_System_Province ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_System_Province
	{
		public Tb_System_Province()
		{}
		#region Model
		private int _provinceid;
		private string _provincename;
		/// <summary>
		/// 
		/// </summary>
		public int ProvinceID
		{
			set{ _provinceid=value;}
			get{return _provinceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProvinceName
		{
			set{ _provincename=value;}
			get{return _provincename;}
		}
		#endregion Model

	}
}

