using System;
namespace HM.Model.Qm
{
    /// <summary>
    /// ʵ����Tb_Qm_TaskAbarbeitung ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class Tb_Qm_TaskAbarbeitung
    {
        public Tb_Qm_TaskAbarbeitung()
        { }
        #region Model
        private string _id;
        private string _taskid;
        private string _pointids;
        private string _addpid;
        private string _pictures;
        private DateTime? _addtime;
        private DateTime? _checktime;
        private string _checknote;
        private string _checkremark;
        private string _checkresult;
        private string _checkresult1;
        private string _problemtype;
        private string _rectificationperiod;
        private string _rectificationnote;
        private string _abarpid;
        private decimal? _reducepoint;
        private string _reducepid;
        private string _abarcheck;
        private string _abarcheckpid;
        private DateTime? _abarchecktime;
        private string _major;
        private string _reduceresult;
        private DateTime? _reducetime;
        private string _reducecheckresult;
        private DateTime? _reducechecktime;
        private int? _isintime;
        private int? _isok;
        private string _checkstatus;
        private string _files;
        private string _coordinate;
        private int? _isdelete;
        private DateTime? _uploadtime;
        private DateTime? _uploadtime1;
        private string _extendstr;
        private string _extendstr1;
        /// <summary>
        /// 
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string TaskId
        {
            set { _taskid = value; }
            get { return _taskid; }
        }
        /// <summary>
        /// ��λ
        /// </summary>
        public string PointIds
        {
            set { _pointids = value; }
            get { return _pointids; }
        }
        /// <summary>
        /// �����Ա,�˲���Ա
        /// </summary>
        public string AddPId
        {
            set { _addpid = value; }
            get { return _addpid; }
        }
        /// <summary>
        /// ͼƬ
        /// </summary>
        public string Pictures
        {
            set { _pictures = value; }
            get { return _pictures; }
        }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// �˲�ʱ��
        /// </summary>
        public DateTime? CheckTime
        {
            set { _checktime = value; }
            get { return _checktime; }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string CheckNote
        {
            set { _checknote = value; }
            get { return _checknote; }
        }
        /// <summary>
        /// �˲����
        /// </summary>
        public string CheckRemark
        {
            set { _checkremark = value; }
            get { return _checkremark; }
        }
        /// <summary>
        /// �˲����
        /// </summary>
        public string CheckResult
        {
            set { _checkresult = value; }
            get { return _checkresult; }
        }
        /// <summary>
        /// �˲���
        /// </summary>
        public string CheckResult1
        {
            set { _checkresult1 = value; }
            get { return _checkresult1; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string ProblemType
        {
            set { _problemtype = value; }
            get { return _problemtype; }
        }
        /// <summary>
        /// �������ޣ�Сʱ��
        /// </summary>
        public string RectificationPeriod
        {
            set { _rectificationperiod = value; }
            get { return _rectificationperiod; }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string RectificationNote
        {
            set { _rectificationnote = value; }
            get { return _rectificationnote; }
        }
        /// <summary>
        /// ������Ա
        /// </summary>
        public string AbarPId
        {
            set { _abarpid = value; }
            get { return _abarpid; }
        }
        /// <summary>
        /// �۷�
        /// </summary>
        public decimal? ReducePoint
        {
            set { _reducepoint = value; }
            get { return _reducepoint; }
        }
        /// <summary>
        /// ���۷���Ա
        /// </summary>
        public string ReducePId
        {
            set { _reducepid = value; }
            get { return _reducepid; }
        }
        /// <summary>
        /// �˲����״̬����˺ϸ������˲��ϸ�
        /// </summary>
        public string AbarCheck
        {
            set { _abarcheck = value; }
            get { return _abarcheck; }
        }
        /// <summary>
        /// �˲������
        /// </summary>
        public string AbarCheckPId
        {
            set { _abarcheckpid = value; }
            get { return _abarcheckpid; }
        }
        /// <summary>
        /// �˲����ʱ��
        /// </summary>
        public DateTime? AbarCheckTime
        {
            set { _abarchecktime = value; }
            get { return _abarchecktime; }
        }
        /// <summary>
        /// ����רҵ
        /// </summary>
        public string Major
        {
            set { _major = value; }
            get { return _major; }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string ReduceResult
        {
            set { _reduceresult = value; }
            get { return _reduceresult; }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? ReduceTime
        {
            set { _reducetime = value; }
            get { return _reducetime; }
        }
        /// <summary>
        /// �����������
        /// </summary>
        public string ReduceCheckResult
        {
            set { _reducecheckresult = value; }
            get { return _reducecheckresult; }
        }
        /// <summary>
        /// ��������ʱ��
        /// </summary>
        public DateTime? ReduceCheckTime
        {
            set { _reducechecktime = value; }
            get { return _reducechecktime; }
        }
        /// <summary>
        /// �Ƿ�ʱ��1��ʱ0����ʱ��
        /// </summary>
        public int? IsInTime
        {
            set { _isintime = value; }
            get { return _isintime; }
        }
        /// <summary>
        /// �Ƿ�ϸ�1�ϸ�0���ϸ�
        /// </summary>
        public int? IsOk
        {
            set { _isok = value; }
            get { return _isok; }
        }
        /// <summary>
        /// ����״̬
        /// </summary>
        public string CheckStatus
        {
            set { _checkstatus = value; }
            get { return _checkstatus; }
        }
        /// <summary>
        /// �����б�
        /// </summary>
        public string Files
        {
            set { _files = value; }
            get { return _files; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Coordinate
        {
            set { _coordinate = value; }
            get { return _coordinate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UploadTime
        {
            set { _uploadtime = value; }
            get { return _uploadtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UploadTime1
        {
            set { _uploadtime1 = value; }
            get { return _uploadtime1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtendStr
        {
            set { _extendstr = value; }
            get { return _extendstr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtendStr1
        {
            set { _extendstr1 = value; }
            get { return _extendstr1; }
        }
        #endregion Model

    }
}

