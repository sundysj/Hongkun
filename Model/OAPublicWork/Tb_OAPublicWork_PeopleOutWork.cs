using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_PeopleOutWork 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_PeopleOutWork
	{
		public Tb_OAPublicWork_PeopleOutWork()
		{}
		#region Model
		private long _infoid;
		private string _usercode;
		private DateTime? _recorddate;
		private DateTime? _outdate;
		private DateTime? _planretdate;
		private DateTime? _realretdate;
		private string _outwhere;
		private string _outthing;
		private string _outresult;
		private string _isreturn;
		private string _leavemanlist;
		/// <summary>
		/// 
		/// </summary>
		public long InfoId
		{
			set{ _infoid=value;}
			get{return _infoid;}
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
		public DateTime? RecordDate
		{
			set{ _recorddate=value;}
			get{return _recorddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? OutDate
		{
			set{ _outdate=value;}
			get{return _outdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? PlanRetDate
		{
			set{ _planretdate=value;}
			get{return _planretdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RealRetDate
		{
			set{ _realretdate=value;}
			get{return _realretdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OutWhere
		{
			set{ _outwhere=value;}
			get{return _outwhere;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OutThing
		{
			set{ _outthing=value;}
			get{return _outthing;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OutResult
		{
			set{ _outresult=value;}
			get{return _outresult;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsReturn
		{
			set{ _isreturn=value;}
			get{return _isreturn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LeaveManList
		{
			set{ _leavemanlist=value;}
			get{return _leavemanlist;}
		}
		#endregion Model

	}
}

