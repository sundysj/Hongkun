using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MobileSoft.Model.DYDZ
{
    /// <summary>
    /// 实体类Tb_HSPR_ParkTempCardFees 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_HSPR_ParkTempCardFees
    {
        public Tb_HSPR_ParkTempCardFees()
        { }
        #region Model
        private string _businessid;
        private string _carnum;
        private string _cardkind;
        private string _intime;
        private string _inshebeiname;
        private string _outtime;
        private string _outshebeiname;
        private string _feenum;
        private string _CommID;


        public string CommID
        {
            set { _CommID = value; }
            get { return _CommID; }
        }

        [DisplayName("车辆临停费用")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
        public string BusinessID
        {
            set { _businessid = value; }
            get { return _businessid; }
        }
        [DisplayName("车牌号码")]
        public string CarNum
        {
            set { _carnum = value; }
            get { return _carnum; }
        }
        [DisplayName("业务类型")]
        public string Cardkind
        {
            set { _cardkind = value; }
            get { return _cardkind; }
        }
        [DisplayName("进场时间")]
        public string intime
        {
            set { _intime = value; }
            get { return _intime; }
        }
        [DisplayName("入口闸机编号")]
        public string inshebeiname
        {
            set { _inshebeiname = value; }
            get { return _inshebeiname; }
        }
        [DisplayName("出场时间 ")]
        public string outtime
        {
            set { _outtime = value; }
            get { return _outtime; }
        }
        [DisplayName("出场闸机编号")]
        public string outshebeiname
        {
            set { _outshebeiname = value; }
            get { return _outshebeiname; }
        }
        [DisplayName("临停车辆缴费金额")]
        public string feenum
        {
            set { _feenum = value; }
            get { return _feenum; }
        }
        #endregion Model

    }
}

