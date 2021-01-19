using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_FixedAssetsLossTable 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_FixedAssetsLossTable
	{
		public Tb_OAPublicWork_FixedAssetsLossTable()
		{}
		#region Model
		private int _infoid;
		private int? _tb_workflow_flowsort_infoid;
		private string _usercode;
		private string _lossusername;
		private string _lossassets;
		private string _lossdepart;
		private DateTime? _lossdate;
		private string _lossmark;
		private DateTime? _workstartdate;
		private string _documenturl;
		/// <summary>
		/// 
		/// </summary>
		public int InfoID
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
		public string LossUserName
		{
			set{ _lossusername=value;}
			get{return _lossusername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LossAssets
		{
			set{ _lossassets=value;}
			get{return _lossassets;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LossDepart
		{
			set{ _lossdepart=value;}
			get{return _lossdepart;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LossDate
		{
			set{ _lossdate=value;}
			get{return _lossdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LossMark
		{
			set{ _lossmark=value;}
			get{return _lossmark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? WorkStartDate
		{
			set{ _workstartdate=value;}
			get{return _workstartdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DocumentUrl
		{
			set{ _documenturl=value;}
			get{return _documenturl;}
		}
		#endregion Model

	}
}

