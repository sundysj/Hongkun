using System;
namespace HM.Model.Qm
{
    /// <summary>
    /// ʵ����Tb_Qm_StandardDetail ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// ��׼��������
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ��׼id
        /// </summary>
        public string StanId
        {
            set { _stanid = value; }
            get { return _stanid; }
        }
        /// <summary>
        /// ���񼶱�
        /// </summary>
        public string TaskLevelId
        {
            set { _tasklevelid = value; }
            get { return _tasklevelid; }
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
        /// �˲�Ƶ��
        /// </summary>
        public string CheckFrequency
        {
            set { _checkfrequency = value; }
            get { return _checkfrequency; }
        }
        /// <summary>
        /// �Ƿ����ʱ��
        /// </summary>
        public string IsControlDate
        {
            set { _iscontroldate = value; }
            get { return _iscontroldate; }
        }
        /// <summary>
        /// ��λ������
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

