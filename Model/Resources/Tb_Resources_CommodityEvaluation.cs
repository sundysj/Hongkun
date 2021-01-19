using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Resources
{
    /// <summary>
    /// 实体类Tb_Resources_CommodityEvaluation 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_Resources_CommodityEvaluation
    {
        public Tb_Resources_CommodityEvaluation()
        { }
        #region Model
        private string _id;
        private string _rpdcode;
        private string _resourcesid;
        private string _evaluatecontent;
        private string _uploadimg;
        private decimal? _star;
        private DateTime? _evaluatedate;
        private int? _isdelete;
        [DisplayName("")]
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        [DisplayName("订单明细表ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
        public string RpdCode
        {
            set { _rpdcode = value; }
            get { return _rpdcode; }
        }
        [DisplayName("资源ID")]
        public string ResourcesID
        {
            set { _resourcesid = value; }
            get { return _resourcesid; }
        }
        [DisplayName("评价内容")]
        public string EvaluateContent
        {
            set { _evaluatecontent = value; }
            get { return _evaluatecontent; }
        }
        [DisplayName("上传图片")]
        public string UploadImg
        {
            set { _uploadimg = value; }
            get { return _uploadimg; }
        }
        [DisplayName("商品评星")]
        public decimal? Star
        {
            set { _star = value; }
            get { return _star; }
        }
        [DisplayName("评价时间")]
        public DateTime? EvaluateDate
        {
            set { _evaluatedate = value; }
            get { return _evaluatedate; }
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
