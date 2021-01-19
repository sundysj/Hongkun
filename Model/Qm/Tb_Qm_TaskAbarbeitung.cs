using System;
namespace HM.Model.Qm
{
    /// <summary>
    /// 实体类Tb_Qm_TaskAbarbeitung 。(属性说明自动提取数据库字段的描述信息)
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
        /// 任务编号
        /// </summary>
        public string TaskId
        {
            set { _taskid = value; }
            get { return _taskid; }
        }
        /// <summary>
        /// 点位
        /// </summary>
        public string PointIds
        {
            set { _pointids = value; }
            get { return _pointids; }
        }
        /// <summary>
        /// 添加人员,核查人员
        /// </summary>
        public string AddPId
        {
            set { _addpid = value; }
            get { return _addpid; }
        }
        /// <summary>
        /// 图片
        /// </summary>
        public string Pictures
        {
            set { _pictures = value; }
            get { return _pictures; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 核查时间
        /// </summary>
        public DateTime? CheckTime
        {
            set { _checktime = value; }
            get { return _checktime; }
        }
        /// <summary>
        /// 抽样情况
        /// </summary>
        public string CheckNote
        {
            set { _checknote = value; }
            get { return _checknote; }
        }
        /// <summary>
        /// 核查情况
        /// </summary>
        public string CheckRemark
        {
            set { _checkremark = value; }
            get { return _checkremark; }
        }
        /// <summary>
        /// 核查结论
        /// </summary>
        public string CheckResult
        {
            set { _checkresult = value; }
            get { return _checkresult; }
        }
        /// <summary>
        /// 核查结果
        /// </summary>
        public string CheckResult1
        {
            set { _checkresult1 = value; }
            get { return _checkresult1; }
        }
        /// <summary>
        /// 问题类型
        /// </summary>
        public string ProblemType
        {
            set { _problemtype = value; }
            get { return _problemtype; }
        }
        /// <summary>
        /// 整改期限（小时）
        /// </summary>
        public string RectificationPeriod
        {
            set { _rectificationperiod = value; }
            get { return _rectificationperiod; }
        }
        /// <summary>
        /// 整改意见
        /// </summary>
        public string RectificationNote
        {
            set { _rectificationnote = value; }
            get { return _rectificationnote; }
        }
        /// <summary>
        /// 整改人员
        /// </summary>
        public string AbarPId
        {
            set { _abarpid = value; }
            get { return _abarpid; }
        }
        /// <summary>
        /// 扣分
        /// </summary>
        public decimal? ReducePoint
        {
            set { _reducepoint = value; }
            get { return _reducepoint; }
        }
        /// <summary>
        /// 被扣分人员
        /// </summary>
        public string ReducePId
        {
            set { _reducepid = value; }
            get { return _reducepid; }
        }
        /// <summary>
        /// 核查审核状态（审核合格或者审核不合格）
        /// </summary>
        public string AbarCheck
        {
            set { _abarcheck = value; }
            get { return _abarcheck; }
        }
        /// <summary>
        /// 核查审核人
        /// </summary>
        public string AbarCheckPId
        {
            set { _abarcheckpid = value; }
            get { return _abarcheckpid; }
        }
        /// <summary>
        /// 核查审核时间
        /// </summary>
        public DateTime? AbarCheckTime
        {
            set { _abarchecktime = value; }
            get { return _abarchecktime; }
        }
        /// <summary>
        /// 关联专业
        /// </summary>
        public string Major
        {
            set { _major = value; }
            get { return _major; }
        }
        /// <summary>
        /// 整改情况
        /// </summary>
        public string ReduceResult
        {
            set { _reduceresult = value; }
            get { return _reduceresult; }
        }
        /// <summary>
        /// 整改时间
        /// </summary>
        public DateTime? ReduceTime
        {
            set { _reducetime = value; }
            get { return _reducetime; }
        }
        /// <summary>
        /// 整改验收情况
        /// </summary>
        public string ReduceCheckResult
        {
            set { _reducecheckresult = value; }
            get { return _reducecheckresult; }
        }
        /// <summary>
        /// 整改验收时间
        /// </summary>
        public DateTime? ReduceCheckTime
        {
            set { _reducechecktime = value; }
            get { return _reducechecktime; }
        }
        /// <summary>
        /// 是否及时（1及时0不及时）
        /// </summary>
        public int? IsInTime
        {
            set { _isintime = value; }
            get { return _isintime; }
        }
        /// <summary>
        /// 是否合格（1合格0不合格）
        /// </summary>
        public int? IsOk
        {
            set { _isok = value; }
            get { return _isok; }
        }
        /// <summary>
        /// 整改状态
        /// </summary>
        public string CheckStatus
        {
            set { _checkstatus = value; }
            get { return _checkstatus; }
        }
        /// <summary>
        /// 附件列表
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

