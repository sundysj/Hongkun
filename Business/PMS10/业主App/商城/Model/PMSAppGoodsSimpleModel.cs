using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.PMS10.业主App.商城.Model
{
    public class PMSAppGoodsSimpleModel
    {
        public int BussID { get; set; }
        public string GoodsID { get; set; }
        public string GoodsName { get; set; }
        public string GoodsSimpleName { get; set; }
        public string GoodsImg { get; set; }
        public string IsGroupBuy { get; set; }
        public decimal SalePrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal ActualPrice { get; set; }
        public int IsHot { get; set; }
        public int IsRecommend { get; set; }

        public int SoldCount { get; set; }
    }
}
