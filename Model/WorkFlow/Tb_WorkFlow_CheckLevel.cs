using System;
namespace MobileSoft.Model.WorkFlow
{
	/// <summary>
	/// 实体类Tb_WorkFlow_CheckLevel 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_WorkFlow_CheckLevel
	{
		public Tb_WorkFlow_CheckLevel()
		{}
		#region Model
		private int _infoid;
		private int _instanceinfoid;
		private int _workflowinfoid;
		private string _startusercode;
		private string _checkusercode;
		private int? _checktype;
		private int? _oprstate;
		private DateTime? _recorddate;
		private int _sort;
		private int? _isgotonext;
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
		public int InstanceInfoId
		{
			set{ _instanceinfoid=value;}
			get{return _instanceinfoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int WorkFlowInfoId
		{
			set{ _workflowinfoid=value;}
			get{return _workflowinfoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StartUserCode
		{
			set{ _startusercode=value;}
			get{return _startusercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CheckUserCode
		{
			set{ _checkusercode=value;}
			get{return _checkusercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CheckType
		{
			set{ _checktype=value;}
			get{return _checktype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? OprState
		{
			set{ _oprstate=value;}
			get{return _oprstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RecordDate
		{
			set{ _recorddate=value;}
			get{return _recorddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsGoToNext
		{
			set{ _isgotonext=value;}
			get{return _isgotonext;}
		}
		#endregion Model

	}
}

