using System;
namespace HM.Model.Qm
{
	/// <summary>
	/// 实体类Tb_Qm_Task 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Qm_Task
	{
		public Tb_Qm_Task()
		{}
		#region Model
		private string _id;
		private string _addpid;
		private DateTime? _adddate;
		private DateTime? _begindate;
		private DateTime? _enddate;
		private string _itemcode;
		private string _itemname;
		private string _tasktype;
		private string _tasklevelid;
		private string _taskno;
		private string _stanid;
		private string _professional;
		private string _type;
		private string _typedescription;
		private string _checkstandard;
		private string _checkway;
		private decimal _point;
		private string _taskpid;
		private string _taskdepcode;
		private string _taskroleid;
		private string _checkroleid;
		private decimal _pointcoverage;
		private decimal _pointcoveragedone;
		private string _taskstatus;
		private int? _isabarbeitung;
		private int? _isdelete;
		private string _checkresult;
        private string _closepid;
        private string _closereason;
        private DateTime? _closetime;
        /// <summary>
        /// 任务Id
        /// </summary>
        public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 添加人
		/// </summary>
		public string AddPId
		{
			set{ _addpid=value;}
			get{return _addpid;}
		}
		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime? AddDate
		{
			set{ _adddate=value;}
			get{return _adddate;}
		}
		/// <summary>
		/// 任务开始时间
		/// </summary>
		public DateTime? BeginDate
		{
			set{ _begindate=value;}
			get{return _begindate;}
		}
		/// <summary>
		/// 任务结束时间
		/// </summary>
		public DateTime? endDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		/// <summary>
		/// 项目编号
		/// </summary>
		public string ItemCode
		{
			set{ _itemcode=value;}
			get{return _itemcode;}
		}
		/// <summary>
		/// 项目名称
		/// </summary>
		public string ItemName
		{
			set{ _itemname=value;}
			get{return _itemname;}
		}
		/// <summary>
		/// 任务类型-常规核查/专项核查/临时核查
		/// </summary>
		public string TaskType
		{
			set{ _tasktype=value;}
			get{return _tasktype;}
		}
		/// <summary>
		/// 任务级别ID
		/// </summary>
		public string TaskLevelId
		{
			set{ _tasklevelid=value;}
			get{return _tasklevelid;}
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
		/// 标准id
		/// </summary>
		public string StanId
		{
			set{ _stanid=value;}
			get{return _stanid;}
		}
		/// <summary>
		/// 所属专业
		/// </summary>
		public string Professional
		{
			set{ _professional=value;}
			get{return _professional;}
		}
		/// <summary>
		/// 所属类型
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 类别描述
		/// </summary>
		public string TypeDescription
		{
			set{ _typedescription=value;}
			get{return _typedescription;}
		}
		/// <summary>
		/// 核查标准
		/// </summary>
		public string CheckStandard
		{
			set{ _checkstandard=value;}
			get{return _checkstandard;}
		}
		/// <summary>
		/// 审核标准,检查方法
		/// </summary>
		public string CheckWay
		{
			set{ _checkway=value;}
			get{return _checkway;}
		}
		/// <summary>
		/// 分值
		/// </summary>
		public decimal Point
		{
			set{ _point=value;}
			get{return _point;}
		}
		/// <summary>
		/// 责任人
		/// </summary>
		public string TaskPId
		{
			set{ _taskpid=value;}
			get{return _taskpid;}
		}
		/// <summary>
		/// 责任部门
		/// </summary>
		public string TaskDepCode
		{
			set{ _taskdepcode=value;}
			get{return _taskdepcode;}
		}
		/// <summary>
		/// 责任岗位
		/// </summary>
		public string TaskRoleId
		{
			set{ _taskroleid=value;}
			get{return _taskroleid;}
		}
		/// <summary>
		/// 审查岗位
		/// </summary>
		public string CheckRoleId
		{
			set{ _checkroleid=value;}
			get{return _checkroleid;}
		}
		/// <summary>
		/// 点位覆盖率
		/// </summary>
		public decimal PointCoverage
		{
			set{ _pointcoverage=value;}
			get{return _pointcoverage;}
		}
		/// <summary>
		/// 实际覆盖率
		/// </summary>
		public decimal PointCoverageDone
		{
			set{ _pointcoveragedone=value;}
			get{return _pointcoveragedone;}
		}
        /// <summary>
        /// 任务状态(未开始、执行中，已执行，已完成，已关闭)
        /// </summary>
        public string TaskStatus
		{
			set{ _taskstatus=value;}
			get{return _taskstatus;}
		}
		/// <summary>
		/// 是否整改（0为不整改，1为有整改）
		/// </summary>
		public int? IsAbarbeitung
		{
			set{ _isabarbeitung=value;}
			get{return _isabarbeitung;}
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
		/// 检查结果
		/// </summary>
		public string CheckResult
		{
			set{ _checkresult=value;}
			get{return _checkresult;}
		}
        /// <summary>
		/// 关闭人
		/// </summary>
		public string ClosePId
        {
            set { _closepid = value; }
            get { return _closepid; }
        }
        /// <summary>
        /// 关闭原因
        /// </summary>
        public string CloseReason
        {
            set { _closereason = value; }
            get { return _closereason; }
        }
        /// <summary>
        /// 关闭时间
        /// </summary>
        public DateTime? CloseTime
        {
            set { _closetime = value; }
            get { return _closetime; }
        }
        #endregion Model

    }
}

