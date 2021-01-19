using System;
namespace HM.Model.Qm
{
    /// <summary>
    /// 实体类Tb_Qm_Point 。(属性说明自动提取数据库字段的描述信息)
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
        /// 主键Id
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            set { _projectname = value; }
            get { return _projectname; }
        }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectCode
        {
            set { _projectcode = value; }
            get { return _projectcode; }
        }
        /// <summary>
        /// 点位名称
        /// </summary>
        public string PointName
        {
            set { _pointname = value; }
            get { return _pointname; }
        }
        /// <summary>
        /// 点位编号
        /// </summary>
        public string PointCode
        {
            set { _pointcode = value; }
            get { return _pointcode; }
        }
        /// <summary>
        /// 点位地址
        /// </summary>
        public string Addr
        {
            set { _addr = value; }
            get { return _addr; }
        }
        /// <summary>
        /// 地图坐标
        /// </summary>
        public string Map
        {
            set { _map = value; }
            get { return _map; }
        }
        /// <summary>
        /// 二维码地址
        /// </summary>
        public string QRCodesPath
        {
            set { _qrcodespath = value; }
            get { return _qrcodespath; }
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

