using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// ʵ����Tb_OAPublicWork_MaterialBuyPlan ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_MaterialBuyPlan
	{
		public Tb_OAPublicWork_MaterialBuyPlan()
		{}
		#region Model
		private int _infoid;
		private int? _tb_workflow_flowsort_infoid;
		private string _usercode;
		private string _title;
		private string _depcode;
		private DateTime? _buyplandate;
		private decimal? _buyplanmoney;
		private DateTime? _needdate;
		private string _buyplanreadme;
		private string _documenturl;
		private DateTime? _workstartdate;
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
		public int? Tb_WorkFlow_FlowSort_InfoId
		{
			set{ _tb_workflow_flowsort_infoid=value;}
			get{return _tb_workflow_flowsort_infoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DepCode
		{
			set{ _depcode=value;}
			get{return _depcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? BuyPlanDate
		{
			set{ _buyplandate=value;}
			get{return _buyplandate;}
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
		public DateTime? NeedDate
		{
			set{ _needdate=value;}
			get{return _needdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BuyPlanReadMe
		{
			set{ _buyplanreadme=value;}
			get{return _buyplanreadme;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DocumentUrl
		{
			set{ _documenturl=value;}
			get{return _documenturl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? WorkStartDate
		{
			set{ _workstartdate=value;}
			get{return _workstartdate;}
		}
		#endregion Model

	}
}

