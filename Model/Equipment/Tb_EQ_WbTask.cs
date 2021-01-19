using System;
namespace HM.Model.Eq
{
	/// <summary>
	/// ʵ����Tb_EQ_WbTask ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_EQ_WbTask
	{
		public Tb_EQ_WbTask()
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
		private int? _isentrust;
		private string _entrustcompany;
		private string _checkrolecode;
		private string _personincharge;
		private DateTime? _dotime;
		private string _checkman;
		private string _checknoto;
		private string _checkrusult;
		private DateTime? _checktime;
		/// <summary>
		/// ά����������Id
		/// </summary>
		public string TaskId
		{
			set{ _taskid=value;}
			get{return _taskid;}
		}
		/// <summary>
		/// ��Ŀ���
		/// </summary>
		public int? CommId
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// �ƻ�����
		/// </summary>
		public string PlanId
		{
			set{ _planid=value;}
			get{return _planid;}
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
		/// �豸ϵͳ��ʮ��ϵͳ
		/// </summary>
		public string EqId
		{
			set{ _eqid=value;}
			get{return _eqid;}
		}
		/// <summary>
		/// �豸�ռ�����Id
		/// </summary>
		public string SpaceId
		{
			set{ _spaceid=value;}
			get{return _spaceid;}
		}
		/// <summary>
		/// ά������
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// ��Чʱ�俪ʼ
		/// </summary>
		public DateTime? BeginTime
		{
			set{ _begintime=value;}
			get{return _begintime;}
		}
		/// <summary>
		/// ��Чʱ�����
		/// </summary>
		public DateTime? EndTime
		{
			set{ _endtime=value;}
			get{return _endtime;}
		}
		/// <summary>
		/// ��Ч�ܼ�
		/// </summary>
		public string WorkDayBegin
		{
			set{ _workdaybegin=value;}
			get{return _workdaybegin;}
		}
		/// <summary>
		/// ���θ�λ
		/// </summary>
		public string RoleCode
		{
			set{ _rolecode=value;}
			get{return _rolecode;}
		}
		/// <summary>
		/// ״̬  ��δ���,�ѹ���,�����,�ѹرգ�
		/// </summary>
		public string Statue
		{
			set{ _statue=value;}
			get{return _statue;}
		}
		/// <summary>
		/// �ر���
		/// </summary>
		public string ClosePerson
		{
			set{ _closeperson=value;}
			get{return _closeperson;}
		}
		/// <summary>
		/// �ر�ʱ��
		/// </summary>
		public DateTime? CloseTime
		{
			set{ _closetime=value;}
			get{return _closetime;}
		}
		/// <summary>
		/// �ر�ԭ��
		/// </summary>
		public string CloseReason
		{
			set{ _closereason=value;}
			get{return _closereason;}
		}
		/// <summary>
		/// ά����(������)
		/// </summary>
		public string PollingPerson
		{
			set{ _pollingperson=value;}
			get{return _pollingperson;}
		}
		/// <summary>
		/// ά������ִ��ʱ��
		/// </summary>
		public DateTime? PollingDate
		{
			set{ _pollingdate=value;}
			get{return _pollingdate;}
		}
		/// <summary>
		/// ��ע
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
		/// ��Чϵ��
		/// </summary>
		public string PerfNum
		{
			set{ _perfnum=value;}
			get{return _perfnum;}
		}
		/// <summary>
		/// ���ʱ
		/// </summary>
		public string WorkTime
		{
			set{ _worktime=value;}
			get{return _worktime;}
		}
		/// <summary>
		/// �Ƿ���ί 0��1��
		/// </summary>
		public int? IsEntrust
		{
			set{ _isentrust=value;}
			get{return _isentrust;}
		}
		/// <summary>
		/// ��ί��λ
		/// </summary>
		public string EntrustCompany
		{
			set{ _entrustcompany=value;}
			get{return _entrustcompany;}
		}
		/// <summary>
		/// ���ո�λ
		/// </summary>
		public string CheckRoleCode
		{
			set{ _checkrolecode=value;}
			get{return _checkrolecode;}
		}
		/// <summary>
		/// ������(�ϳ�)
		/// </summary>
		public string PersonInCharge
		{
			set{ _personincharge=value;}
			get{return _personincharge;}
		}
		/// <summary>
		/// ά��ʱ��
		/// </summary>
		public DateTime? DoTime
		{
			set{ _dotime=value;}
			get{return _dotime;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string CheckMan
		{
			set{ _checkman=value;}
			get{return _checkman;}
		}
		/// <summary>
		/// �������
		/// </summary>
		public string CheckNoto
		{
			set{ _checknoto=value;}
			get{return _checknoto;}
		}
		/// <summary>
		/// ���ս�� �ϸ�/���ϸ�
		/// </summary>
		public string CheckRusult
		{
			set{ _checkrusult=value;}
			get{return _checkrusult;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? CheckTime
		{
			set{ _checktime=value;}
			get{return _checktime;}
		}
		#endregion Model

	}
}
