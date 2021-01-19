using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Business.PMS10.报事.Models
{
    public class PMSIncidentLifeCycle
    {
        public string PhaseName { get; set; }

        public string Title { get; set; }

        public string MainInfo { get; set; }

        public string SubInfo { get; set; }

        public bool SubInfoHighlight { get; set; }

        [JsonIgnore]
        public DateTime ActionTime { get; set; }

        public string DateTitle { get; set; }

        public List<ContactInfo> Mobile { get; set; }

        public string[] Files { get; set; }
    }

    public class ContactInfo
    {
        public string UserName { get; set; }

        public string Mobile { get; set; }
    }
}
