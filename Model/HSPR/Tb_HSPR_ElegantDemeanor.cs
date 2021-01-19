using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_ElegantDemeanor 。(属性说明自动提取数据库字段的描述信息)
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

