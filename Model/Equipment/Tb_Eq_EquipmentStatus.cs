using System;
namespace HM.Model.Eq
{
    /// <summary>
    /// 实体类Tb_Eq_EquipmentStatus 。(属性说明自动提取数据库字段的描述信息)
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
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime
        {
            set { _begintime = value; }
            get { return _begintime; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 设备状态
        /// </summary>
        public string EquipmentStatus
        {
            set { _equipmentstatus = value; }
            get { return _equipmentstatus; }
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
        /// 设备ID
        /// </summary>
        public string EquipmentId
        {
            set { _equipmentid = value; }
            get { return _equipmentid; }
        }
        /// <summary>
        /// 添加人
        /// </summary>
        public string AddPid
        {
            set { _addpid = value; }
            get { return _addpid; }
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
        /// 操作人
        /// </summary>
        public string OperationPid
        {
            set { _operationpid = value; }
            get { return _operationpid; }
        }
        /// <summary>
        /// 操作时间
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

