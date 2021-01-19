using System;
namespace MobileSoft.Model.Order
{
    /// <summary>
    /// 实体类Tb_Order_BusCar 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_Order_BusCar
    {
        public Tb_Order_BusCar()
        { }
        #region Model
        private string _buscarid;
        private long _buscustid;
        private long _busreleaseid;
        private long _busbussid;
        private DateTime? _created;
        /// <summary>
        /// 
        /// </summary>
        public string BusCarID
        {
            set { _buscarid = value; }
            get { return _buscarid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long BusCustID
        {
            set { _buscustid = value; }
            get { return _buscustid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long BusReleaseID
        {
            set { _busreleaseid = value; }
            get { return _busreleaseid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long BusBussId
        {
            set { _busbussid = value; }
            get { return _busbussid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Created
        {
            set { _created = value; }
            get { return _created; }
        }
        #endregion Model

    }
}

