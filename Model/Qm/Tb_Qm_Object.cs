using System;
namespace HM.Model.Qm
{
    /// <summary>
    /// ʵ����Tb_Qm_Object ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// �˲����ID
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ���
        /// </summary>
        public int? Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string ObjName
        {
            set { _objname = value; }
            get { return _objname; }
        }
        /// <summary>
        /// ����
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

