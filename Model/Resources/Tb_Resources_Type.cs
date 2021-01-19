using System;
namespace MobileSoft.Model.Resources
{
    /// <summary>
    /// 实体类Tb_Resources_Type 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_Resources_Type
    {
        public Tb_Resources_Type()
        { }
        #region Model
        private long _resourcestypeid;
        private long _bussid;
        private string _resourcestypename;
        private string _resourcestypeimgurl;
        private int? _resourcestypeindex;
        private string _remark;
        private int? _isdelete;
        /// <summary>
        /// 
        /// </summary>
        public long ResourcesTypeID
        {
            set { _resourcestypeid = value; }
            get { return _resourcestypeid; }
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
        public string ResourcesTypeName
        {
            set { _resourcestypename = value; }
            get { return _resourcestypename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ResourcesTypeImgUrl
        {
            set { _resourcestypeimgurl = value; }
            get { return _resourcestypeimgurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ResourcesTypeIndex
        {
            set { _resourcestypeindex = value; }
            get { return _resourcestypeindex; }
        }
        /// <summary>
        /// 
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

