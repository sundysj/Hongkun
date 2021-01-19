using System;
namespace MobileSoft.Model.Sys
{
	/// <summary>
	/// 实体类Tb_Sys_TakePicContractDetail 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Sys_TakePicContractDetail
	{
		public Tb_Sys_TakePicContractDetail()
		{}
		#region Model
		private long _detailid;
		private int? _commid;
		private string _organcode;
		private string _conttypename;
		private string _contsign;
		private string _contname;
		private string _custname;
		private DateTime? _contenddate;
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
		public DateTime? ContEndDate
		{
			set{ _contenddate=value;}
			get{return _contenddate;}
		}
		#endregion Model

	}
}

