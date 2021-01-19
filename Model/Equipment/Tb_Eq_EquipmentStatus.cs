using System;
namespace HM.Model.Eq
{
    /// <summary>
    /// ʵ����Tb_Eq_EquipmentStatus ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class Tb_Eq_EquipmentStatus
    {
        public Tb_Eq_EquipmentStatus()
        { }
        #region Model
        private string _id;
        private DateTime? _begintime;
        private DateTime? _endtime;
        private string _equipmentstatus;
        private string _remark;
        private string _equipmentid;
        private string _addpid;
        private DateTime? _addtime;
        private string _operationpid;
        private DateTime? _operationtime;
        private int? _isdelete;
        private string _express;
        /// <summary>
        /// 
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime? BeginTime
        {
            set { _begintime = value; }
            get { return _begintime; }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// �豸״̬
        /// </summary>
        public string EquipmentStatus
        {
            set { _equipmentstatus = value; }
            get { return _equipmentstatus; }
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
        /// �豸ID
        /// </summary>
        public string EquipmentId
        {
            set { _equipmentid = value; }
            get { return _equipmentid; }
        }
        /// <summary>
        /// �����
        /// </summary>
        public string AddPid
        {
            set { _addpid = value; }
            get { return _addpid; }
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
        /// ������
        /// </summary>
        public string OperationPid
        {
            set { _operationpid = value; }
            get { return _operationpid; }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? OperationTime
        {
            set { _operationtime = value; }
            get { return _operationtime; }
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
        public string Express
        {
            set { _express = value; }
            get { return _express; }
        }
        #endregion Model

    }
}

