using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MobileSoft.Model.Resources
{
    /// <summary>
    /// 实体类Tb_ResourcesSpecificationsPrice 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_ResourcesSpecificationsPrice
    {
        public Tb_ResourcesSpecificationsPrice()
        { }
        #region Model
        private string _id;
        private long _bussid;
        private string _resourcesid;
        private string _propertyid;
        private string _specid;
        private decimal? _price;
        [DisplayName("")]
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        [DisplayName("")]
        public long BussId
        {
            set { _bussid = value; }
            get { return _bussid; }
        }
        [DisplayName("")]
        public string ResourcesID
        {
            set { _resourcesid = value; }
            get { return _resourcesid; }
        }
        [DisplayName("")]
        public string PropertyId
        {
            set { _propertyid = value; }
            get { return _propertyid; }
        }
        [DisplayName("")]
        public string SpecId
        {
            set { _specid = value; }
            get { return _specid; }
        }
        [DisplayName("")]
        public decimal? Price
        {
            set { _price = value; }
            get { return _price; }
        }
        #endregion Model

    }
}