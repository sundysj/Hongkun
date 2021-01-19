using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
    /// <summary>
    /// 实体类Tb_ShoppingCar 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_ShoppingCar
    {
        public Tb_ShoppingCar()
        { }

        #region Model
        private string _id;
        private string _userid;
        private string _bussid;
        private string _resourcesid;
        private int _number;
        private decimal _subtotalmoney;
        private int? _isdelete;
        [DisplayName("")]
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 物业公司id
        /// </summary>
        public int? CorpId { get; set; }

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
        [DisplayName("数量")]
        public int Number
        {
            set { _number = value; }
            get { return _number; }
        }
        [DisplayName("小计")]
        public decimal SubtotalMoney
        {
            set { _subtotalmoney = value; }
            get { return _subtotalmoney; }
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

