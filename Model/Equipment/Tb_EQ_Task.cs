using System;
namespace HM.Model.Eq
{
	/// <summary>
	/// 实体类Tb_EQ_Task 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_EQ_Task
	{
		public Tb_EQ_Task()
		{}
		#region Model
		private string _taskid;
		private int? _commid;
		private string _planid;
		private string _taskno;
		private string _eqid;
		private string _spaceid;
		private string _content;
		private DateTime? _begintime;
		private DateTime? _endtime;
		private string _workdaybegin;
		private string _rolecode;
		private string _statue;
		private string _closeperson;
		private DateTime? _closetime;
		private string _closereason;
		private string _pollingperson;
		private DateTime? _pollingdate;
		private string _remark;
		private int? _isdelete;
		private string _addpid;
		private DateTime? _adddate;
		private string _perfnum;
		private string _worktime;
		/// <summary>
		/// 任务生成Id
		/// </summary>
		public string TaskId
		{
			set{ _taskid=value;}
			get{return _taskid;}
		}
		/// <summary>
		/// 项目编号
		/// </summary>
		public int? CommId
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 计划名称
		/// </summary>
		public string PlanId
		{
			set{ _planid=value;}
			get{return _planid;}
		}
		/// <summary>
		/// 任务编号
		/// </summary>
		public string TaskNO
		{
			set{ _taskno=value;}
			get{return _taskno;}
		}
		/// <summary>
		/// 设备系统，十大系统
		/// </summary>
		public string EqId
		{
			set{ _eqid=value;}
			get{return _eqid;}
		}
		/// <summary>
		/// 设备空间主键Id
		/// </summary>
		public string SpaceId
		{
			set{ _spaceid=value;}
			get{return _spaceid;}
		}
		/// <summary>
		/// 巡检内容
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 有效时间开始
		/// </summary>
		public DateTime? BeginTime
		{
			set{ _begintime=value;}
			get{return _begintime;}
		}
		/// <summary>
		/// 有效时间结束
		/// </summary>
		public DateTime? EndTime
		{
			set{ _endtime=value;}
			get{return _endtime;}
		}
		/// <summary>
		/// 有效周几
		/// </summary>
		public string WorkDayBegin
		{
			set{ _workdaybegin=value;}
			get{return _workdaybegin;}
		}
		/// <summary>
		/// 责任岗位
		/// </summary>
		public string RoleCode
		{
			set{ _rolecode=value;}
			get{return _rolecode;}
		}
		/// <summary>
		/// 状态  （未完成,已过期,已完成,已关闭）
		/// </summary>
		public string Statue
		{
			set{ _statue=value;}
			get{return _statue;}
		}
		/// <summary>
		/// 关闭人
		/// </summary>
		public string ClosePerson
		{
			set{ _closeperson=value;}
			get{return _closeperson;}
		}
		/// <summary>
		/// 关闭时间
		/// </summary>
		public DateTime? CloseTime
		{
			set{ _closetime=value;}
			get{return _closetime;}
		}
		/// <summary>
		/// 关闭原因
		/// </summary>
		public string CloseReason
		{
			set{ _closereason=value;}
			get{return _closereason;}
		}
		/// <summary>
		/// 巡检人
		/// </summary>
		public string PollingPerson
		{
			set{ _pollingperson=value;}
			get{return _pollingperson;}
		}
		/// <summary>
		/// 巡检时间
		/// </summary>
		public DateTime? PollingDate
		{
			set{ _pollingdate=value;}
			get{return _pollingdate;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
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
		public string AddPId
		{
			set{ _addpid=value;}
			get{return _addpid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AddDate
		{
			set{ _adddate=value;}
			get{return _adddate;}
		}
		/// <summary>
		/// 绩效系数
		/// </summary>
		public string PerfNum
		{
			set{ _perfnum=value;}
			get{return _perfnum;}
		}
		/// <summary>
		/// 额定工时
		/// </summary>
		public string WorkTime
		{
			set{ _worktime=value;}
			get{return _worktime;}
		}
		#endregion Model

	}
}

