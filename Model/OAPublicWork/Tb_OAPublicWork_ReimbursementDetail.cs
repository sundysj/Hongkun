using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_ReimbursementDetail 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_ReimbursementDetail
	{
		public Tb_OAPublicWork_ReimbursementDetail()
		{}
		#region Model
		private int _infoid;
		private int? _tb_oapublicwork_reimbursement_infoid;
		private string _subjectname;
		private string _summaryname;
		private decimal? _reimbursementmoney;
		private string _reimbursementtype;
		/// <summary>
		/// 
		/// </summary>
		public int InfoId
		{
			set{ _infoid=value;}
			get{return _infoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Tb_OAPublicWork_Reimbursement_InfoId
		{
			set{ _tb_oapublicwork_reimbursement_infoid=value;}
			get{return _tb_oapublicwork_reimbursement_infoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SubjectName
		{
			set{ _subjectname=value;}
			get{return _subjectname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SummaryName
		{
			set{ _summaryname=value;}
			get{return _summaryname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ReimbursementMoney
		{
			set{ _reimbursementmoney=value;}
			get{return _reimbursementmoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReimbursementType
		{
			set{ _reimbursementtype=value;}
			get{return _reimbursementtype;}
		}
		#endregion Model

	}
}

