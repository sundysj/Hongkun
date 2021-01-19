using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
    /// <summary>
    /// 实体类Tb_ShoppingDetailed 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_ShoppingDetailed
    {
        public Tb_ShoppingDetailed()
        { }
        #region Model
        private string _id;
        private string _bussid;
        private string _propertysid;
        private string _specid;
        private string _shoppingid;
        [DisplayName("")]
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        [DisplayName("商家编号")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
        public string BussId
        {
            set { _bussid = value; }
            get { return _bussid; }
        }
        [DisplayName("属性名称")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
        public string PropertysId
        {
            set { _propertysid = value; }
            get { return _propertysid; }
        }
        [DisplayName("归格名称")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
        public string SpecId
        {
            set { _specid = value; }
            get { return _specid; }
        }
        [DisplayName("购物车ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
        public string ShoppingId
        {
            set { _shoppingid = value; }
            get { return _shoppingid; }
        }
        #endregion Model

    }
}

