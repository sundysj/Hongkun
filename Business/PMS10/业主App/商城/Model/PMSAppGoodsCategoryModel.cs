using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.PMS10.业主App.商城.Model
{
    public class PMSAppGoodsCategoryModel
    {
        public string TypeID { get; set; }
        public string TypeName { get; set; }
        public string Icon { get; set; }
        public string Remark { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<PMSAppGoodsSimpleModel> Goods { get; set; }
    }
}
