using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// ʵ����Tb_HSPR_ElegantDemeanor ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_ElegantDemeanor
	{
		public Tb_HSPR_ElegantDemeanor()
		{}
		#region Model
		private long _iid;
		private int? _commid;
		private string _imagename;
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
		public int? CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImageName
		{
			set{ _imagename=value;}
			get{return _imagename;}
		}
		#endregion Model

	}
}

