using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.Buss
{
    /// <summary>
    /// 商品收藏表
    /// </summary>
    [Serializable]
    public class Tb_Collection
    {
        public Tb_Collection()
        { }

        #region Model
        private string _id;
        private string _userid;
        private string _bussid;
        private string _resourcesid;
        private DateTime? _receiptdate;        
        private int? _isdelete;
        [DisplayName("")]
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        [DisplayName("用户编号")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        [DisplayName("商家编号")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
        public string BussId
        {
            set { _bussid = value; }
            get { return _bussid; }
        }
        [DisplayName("商品编号")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
        public string ResourcesID
        {
            set { _resourcesid = value; }
            get { return _resourcesid; }
        }
       
        [DisplayName("收藏日期")]
        public DateTime? ReceiptDate
        {
            set { _receiptdate = value; }
            get { return _receiptdate; }
        }
        [DisplayName("是否删除")]
        public int? IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }
        #endregion Model
    }
}

