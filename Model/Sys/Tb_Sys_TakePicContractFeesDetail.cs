using System;
namespace MobileSoft.Model.Sys
{
	/// <summary>
	/// 实体类Tb_Sys_TakePicContractFeesDetail 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Sys_TakePicContractFeesDetail
	{
		public Tb_Sys_TakePicContractFeesDetail()
		{}
		#region Model
		private long _detailid;
		private int? _commid;
		private string _organcode;
		private string _conttypename;
		private string _contsign;
		private string _contname;
		private string _custname;
		private string _costname;
		private DateTime? _feesduedate;
		private decimal? _debtsamount;
		/// <summary>
		/// 
		/// </summary>
		public long DetailID
		{
			set{ _detailid=value;}
			get{return _detailid;}
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
		public string OrganCode
		{
			set{ _organcode=value;}
			get{return _organcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ContTypeName
		{
			set{ _conttypename=value;}
			get{return _conttypename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ContSign
		{
			set{ _contsign=value;}
			get{return _contsign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ContName
		{
			set{ _contname=value;}
			get{return _contname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustName
		{
			set{ _custname=value;}
			get{return _custname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CostName
		{
			set{ _costname=value;}
			get{return _costname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FeesDueDate
		{
			set{ _feesduedate=value;}
			get{return _feesduedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? DebtsAmount
		{
			set{ _debtsamount=value;}
			get{return _debtsamount;}
		}
		#endregion Model

	}
}

