using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Unified
{
    public class Tb_CommunityEvaluation
    {
        public string ID { get; set; }
        public string UserId { get; set; }

        [JsonIgnore]
        public string CommunityId { get; set; }

        [JsonIgnore]
        public string InfoId { get; set; }

        [JsonIgnore]
        public string ReviewTheme { get; set; }
        public string ReviewContent { get; set; }
        public string EvaluationTime { get; set; }

        [JsonIgnore]
        public string Shielding { get; set; }

        [JsonIgnore]
        public int Identification { get; set; }

    }
}
