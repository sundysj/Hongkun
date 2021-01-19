using System;
namespace HM.Model.Qm
{
    /// <summary>
    /// 实体类Tb_Qm_Object 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_Qm_Object
    {
        public Tb_Qm_Object()
        { }
        #region Model
        private string _id;
        private int? _sort;
        private string _objname;
        private string _remark;
        private int? _isdelete;
        /// <summary>
        /// 核查对象ID
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
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
        /// 对象名称
        /// </summary>
        public string ObjName
        {
            set { _objname = value; }
            get { return _objname; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
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

