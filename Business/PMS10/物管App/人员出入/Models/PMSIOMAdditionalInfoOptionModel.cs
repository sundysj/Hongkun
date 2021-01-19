using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.PMS10.物管App.人员出入.Models
{
    public class PMSIOMAdditionalInfoOptionModel
    {
        public string IID { get; set; }

        [JsonProperty(PropertyName ="Value")]
        public string OptionValue { get; set; }

        public int IsAbnormal { get; set; }
    }
}
