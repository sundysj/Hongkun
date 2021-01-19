using System;
namespace HM.Model.Qm
{
    /// <summary>
    /// ʵ����Tb_Qm_Point ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class Tb_Qm_Point
    {
        public Tb_Qm_Point()
        { }
        #region Model
        private string _id;
        private string _projectname;
        private string _projectcode;
        private string _pointname;
        private string _pointcode;
        private string _addr;
        private string _map;
        private string _qrcodespath;
        private string _remark;
        private int _sort;
        private int? _isdelete;
        /// <summary>
        /// ����Id
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string ProjectName
        {
            set { _projectname = value; }
            get { return _projectname; }
        }
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string ProjectCode
        {
            set { _projectcode = value; }
            get { return _projectcode; }
        }
        /// <summary>
        /// ��λ����
        /// </summary>
        public string PointName
        {
            set { _pointname = value; }
            get { return _pointname; }
        }
        /// <summary>
        /// ��λ���
        /// </summary>
        public string PointCode
        {
            set { _pointcode = value; }
            get { return _pointcode; }
        }
        /// <summary>
        /// ��λ��ַ
        /// </summary>
        public string Addr
        {
            set { _addr = value; }
            get { return _addr; }
        }
        /// <summary>
        /// ��ͼ����
        /// </summary>
        public string Map
        {
            set { _map = value; }
            get { return _map; }
        }
        /// <summary>
        /// ��ά���ַ
        /// </summary>
        public string QRCodesPath
        {
            set { _qrcodespath = value; }
            get { return _qrcodespath; }
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
        public int Sort
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

