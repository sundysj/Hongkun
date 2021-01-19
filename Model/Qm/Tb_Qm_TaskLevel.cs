using System;
namespace HM.Model.Qm
{
    /// <summary>
    /// ʵ����Tb_Qm_TaskLevel ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// ���񼶱�����
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ���񼶱�����
        /// </summary>
        public string TaskLevelName
        {
            set { _tasklevelname = value; }
            get { return _tasklevelname; }
        }
        /// <summary>
        /// ���θ�λ
        /// </summary>
        public string TaskRoleId
        {
            set { _taskroleid = value; }
            get { return _taskroleid; }
        }
        /// <summary>
        /// ����λ
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
        /// ��ע
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// ���
        /// </summary>
        public int? Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        #endregion Model

    }
}

