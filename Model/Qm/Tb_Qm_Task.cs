using System;
namespace HM.Model.Qm
{
	/// <summary>
	/// ʵ����Tb_Qm_Task ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// ����Id
        /// </summary>
        public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// �����
		/// </summary>
		public string AddPId
		{
			set{ _addpid=value;}
			get{return _addpid;}
		}
		/// <summary>
		/// ���ʱ��
		/// </summary>
		public DateTime? AddDate
		{
			set{ _adddate=value;}
			get{return _adddate;}
		}
		/// <summary>
		/// ����ʼʱ��
		/// </summary>
		public DateTime? BeginDate
		{
			set{ _begindate=value;}
			get{return _begindate;}
		}
		/// <summary>
		/// �������ʱ��
		/// </summary>
		public DateTime? endDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		/// <summary>
		/// ��Ŀ���
		/// </summary>
		public string ItemCode
		{
			set{ _itemcode=value;}
			get{return _itemcode;}
		}
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		public string ItemName
		{
			set{ _itemname=value;}
			get{return _itemname;}
		}
		/// <summary>
		/// ��������-����˲�/ר��˲�/��ʱ�˲�
		/// </summary>
		public string TaskType
		{
			set{ _tasktype=value;}
			get{return _tasktype;}
		}
		/// <summary>
		/// ���񼶱�ID
		/// </summary>
		public string TaskLevelId
		{
			set{ _tasklevelid=value;}
			get{return _tasklevelid;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string TaskNO
		{
			set{ _taskno=value;}
			get{return _taskno;}
		}
		/// <summary>
		/// ��׼id
		/// </summary>
		public string StanId
		{
			set{ _stanid=value;}
			get{return _stanid;}
		}
		/// <summary>
		/// ����רҵ
		/// </summary>
		public string Professional
		{
			set{ _professional=value;}
			get{return _professional;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// �������
		/// </summary>
		public string TypeDescription
		{
			set{ _typedescription=value;}
			get{return _typedescription;}
		}
		/// <summary>
		/// �˲��׼
		/// </summary>
		public string CheckStandard
		{
			set{ _checkstandard=value;}
			get{return _checkstandard;}
		}
		/// <summary>
		/// ��˱�׼,��鷽��
		/// </summary>
		public string CheckWay
		{
			set{ _checkway=value;}
			get{return _checkway;}
		}
		/// <summary>
		/// ��ֵ
		/// </summary>
		public decimal Point
		{
			set{ _point=value;}
			get{return _point;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string TaskPId
		{
			set{ _taskpid=value;}
			get{return _taskpid;}
		}
		/// <summary>
		/// ���β���
		/// </summary>
		public string TaskDepCode
		{
			set{ _taskdepcode=value;}
			get{return _taskdepcode;}
		}
		/// <summary>
		/// ���θ�λ
		/// </summary>
		public string TaskRoleId
		{
			set{ _taskroleid=value;}
			get{return _taskroleid;}
		}
		/// <summary>
		/// ����λ
		/// </summary>
		public string CheckRoleId
		{
			set{ _checkroleid=value;}
			get{return _checkroleid;}
		}
		/// <summary>
		/// ��λ������
		/// </summary>
		public decimal PointCoverage
		{
			set{ _pointcoverage=value;}
			get{return _pointcoverage;}
		}
		/// <summary>
		/// ʵ�ʸ�����
		/// </summary>
		public decimal PointCoverageDone
		{
			set{ _pointcoveragedone=value;}
			get{return _pointcoveragedone;}
		}
        /// <summary>
        /// ����״̬(δ��ʼ��ִ���У���ִ�У�����ɣ��ѹر�)
        /// </summary>
        public string TaskStatus
		{
			set{ _taskstatus=value;}
			get{return _taskstatus;}
		}
		/// <summary>
		/// �Ƿ����ģ�0Ϊ�����ģ�1Ϊ�����ģ�
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
		/// �����
		/// </summary>
		public string CheckResult
		{
			set{ _checkresult=value;}
			get{return _checkresult;}
		}
        /// <summary>
		/// �ر���
		/// </summary>
		public string ClosePId
        {
            set { _closepid = value; }
            get { return _closepid; }
        }
        /// <summary>
        /// �ر�ԭ��
        /// </summary>
        public string CloseReason
        {
            set { _closereason = value; }
            get { return _closereason; }
        }
        /// <summary>
        /// �ر�ʱ��
        /// </summary>
        public DateTime? CloseTime
        {
            set { _closetime = value; }
            get { return _closetime; }
        }
        #endregion Model

    }
}

