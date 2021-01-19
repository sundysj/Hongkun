using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_BuyPlanCheckDetail 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_BuyPlanCheckDetail
	{
		public Tb_OAPublicWork_BuyPlanCheckDetail()
		{}
		#region Model
		private int _infoid;
		private int? _tb_oapublicwork_buyplancheck_infoid;
		private int? _num;
		private string _subjectname;
		private string _subjecttype;
		private decimal? _buyplanmoney;
		private decimal? _howmany;
		private DateTime? _needdate;
		private string _howuse;
		private string _mark;
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
		public int? Tb_OAPublicWork_BuyPlanCheck_InfoId
		{
			set{ _tb_oapublicwork_buyplancheck_infoid=value;}
			get{return _tb_oapublicwork_buyplancheck_infoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Num
		{
			set{ _num=value;}
			get{return _num;}
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
		public string SubjectType
		{
			set{ _subjecttype=value;}
			get{return _subjecttype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? BuyPlanMoney
		{
			set{ _buyplanmoney=value;}
			get{return _buyplanmoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? HowMany
		{
			set{ _howmany=value;}
			get{return _howmany;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? NeedDate
		{
			set{ _needdate=value;}
			get{return _needdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HowUse
		{
			set{ _howuse=value;}
			get{return _howuse;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mark
		{
			set{ _mark=value;}
			get{return _mark;}
		}
		#endregion Model

	}
}

