using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model.Buss
{
    /// <summary>
    /// 广告图片
    /// </summary>
    [Serializable]
    public class Tb_AdvertisingPicture
    {
        public Tb_AdvertisingPicture()
        { }
        #region Model
        private string _id;
        private string _corpid;
        private string _commid;
        private string _bussid;
        private string _imglogotitle;
        private string _imglogourl;
        private string _imglogo;
        private DateTime? _begintime;
        private DateTime? _endtime;
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
        /// 公司编码
        /// </summary>
        public string CorpId
        {
            set { _corpid = value; }
            get { return _corpid; }
        }
        /// <summary>
        /// 小区编号
        /// </summary>
        public string CommID
        {
            set { _commid = value; }
            get { return _commid; }
        }
        /// <summary>
        /// 商家编号
        /// </summary>
        public string BussId
        {
            set { _bussid = value; }
            get { return _bussid; }
        }
        /// <summary>
        /// 店招标题
        /// </summary>
        public string ImgLogoTitle
        {
            set { _imglogotitle = value; }
            get { return _imglogotitle; }
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string ImgLogoUrl
        {
            set { _imglogourl = value; }
            get { return _imglogourl; }
        }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgLogo
        {
            set { _imglogo = value; }
            get { return _imglogo; }
        }
        /// <summary>
        /// 有效开始时间
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
        /// 排序
        /// </summary>
        public int? Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int? IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }
        #endregion Model
    }
}
