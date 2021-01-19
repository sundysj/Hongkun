using System;
namespace MobileSoft.Model.Resources
{
    /// <summary>
    /// 实体类Tb_Resources_Attr 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_Resources_Attr
    {
        public Tb_Resources_Attr()
        { }
        #region Model
        private long _attrid;
        private string _attrname;
        private int? _attrindex;
        private string _attrtype;
        private string _attrcolor;
        private long _bussid;
        private int? _isdelete;
        /// <summary>
        /// 
        /// </summary>
        public long AttrID
        {
            set { _attrid = value; }
            get { return _attrid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AttrName
        {
            set { _attrname = value; }
            get { return _attrname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AttrIndex
        {
            set { _attrindex = value; }
            get { return _attrindex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AttrType
        {
            set { _attrtype = value; }
            get { return _attrtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AttrColor
        {
            set { _attrcolor = value; }
            get { return _attrcolor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long BussId
        {
            set { _bussid = value; }
            get { return _bussid; }
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

