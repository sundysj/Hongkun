using System;
namespace HM.Model.Qm
{
    /// <summary>
    /// 实体类Tb_Qm_StandardDetail 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_Qm_StandardDetail
    {
        public Tb_Qm_StandardDetail()
        { }
        #region Model
        private string _id;
        private string _stanid;
        private string _tasklevelid;
        private string _taskroleid;
        private string _checkfrequency;
        private string _iscontroldate;
        private decimal _pointcoverage;
        private int? _isdelete;
        private int? _sort;
        /// <summary>
        /// 标准规则主键
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 标准id
        /// </summary>
        public string StanId
        {
            set { _stanid = value; }
            get { return _stanid; }
        }
        /// <summary>
        /// 任务级别
        /// </summary>
        public string TaskLevelId
        {
            set { _tasklevelid = value; }
            get { return _tasklevelid; }
        }
        /// <summary>
        /// 责任岗位
        /// </summary>
        public string TaskRoleId
        {
            set { _taskroleid = value; }
            get { return _taskroleid; }
        }
        /// <summary>
        /// 核查频率
        /// </summary>
        public string CheckFrequency
        {
            set { _checkfrequency = value; }
            get { return _checkfrequency; }
        }
        /// <summary>
        /// 是否控制时间
        /// </summary>
        public string IsControlDate
        {
            set { _iscontroldate = value; }
            get { return _iscontroldate; }
        }
        /// <summary>
        /// 点位覆盖率
        /// </summary>
        public decimal PointCoverage
        {
            set { _pointcoverage = value; }
            get { return _pointcoverage; }
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
        public int? Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        #endregion Model

    }
}

