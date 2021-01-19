using System;
namespace HM.Model.Qm
{
    /// <summary>
    /// 实体类Tb_Qm_Problem 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_Qm_Problem
    {
        public Tb_Qm_Problem()
        { }
        #region Model
        private string _id;
        private string _problemtype;
        private string _rectificationperiod;
        private string _major;
        private string _remarks;
        private int? _sort;
        private int? _isdelete;
        /// <summary>
        /// 
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 问题类别
        /// </summary>
        public string ProblemType
        {
            set { _problemtype = value; }
            get { return _problemtype; }
        }
        /// <summary>
        /// 整改期限（天）
        /// </summary>
        public string RectificationPeriod
        {
            set { _rectificationperiod = value; }
            get { return _rectificationperiod; }
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
        /// 备注
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public int? Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }
        #endregion Model

    }
}

