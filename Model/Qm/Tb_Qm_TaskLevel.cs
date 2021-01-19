using System;
namespace HM.Model.Qm
{
    /// <summary>
    /// 实体类Tb_Qm_TaskLevel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_Qm_TaskLevel
    {
        public Tb_Qm_TaskLevel()
        { }
        #region Model
        private string _id;
        private string _tasklevelname;
        private string _taskroleid;
        private string _checkroleid;
        private int? _isdelete;
        private string _remark;
        private int? _sort;
        /// <summary>
        /// 任务级别主键
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 任务级别名称
        /// </summary>
        public string TaskLevelName
        {
            set { _tasklevelname = value; }
            get { return _tasklevelname; }
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
        /// 审查岗位
        /// </summary>
        public string CheckRoleId
        {
            set { _checkroleid = value; }
            get { return _checkroleid; }
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
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public int? Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        #endregion Model

    }
}

