using System;
namespace MobileSoft.Model.Information
{
	/// <summary>
	/// 实体类Tb_Information_BussPromotion 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Information_BussPromotion
	{
		public Tb_Information_BussPromotion()
		{}
		#region Model
		private long _proid;
		private long _bussid;
		private string _project;
		private string _publisher;
		private DateTime? _publishdate;
		private string _reason;
		private string _proimage;
		private int? _isdelete;
		private long _numid;
		/// <summary>
		/// 
		/// </summary>
		public long ProID
		{
			set{ _proid=value;}
			get{return _proid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Project
		{
			set{ _project=value;}
			get{return _project;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Publisher
		{
			set{ _publisher=value;}
			get{return _publisher;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? PublishDate
		{
			set{ _publishdate=value;}
			get{return _publishdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Reason
		{
			set{ _reason=value;}
			get{return _reason;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProImage
		{
			set{ _proimage=value;}
			get{return _proimage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long NumID
		{
			set{ _numid=value;}
			get{return _numid;}
		}
		#endregion Model

	}
}

