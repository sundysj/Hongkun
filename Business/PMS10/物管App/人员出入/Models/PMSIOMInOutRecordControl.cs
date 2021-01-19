using Newtonsoft.Json;

namespace Business.PMS10.物管App.人员出入.Models
{
    public class PMSIOMInOutRecordControl
    {
        [JsonIgnore]
        public string PassTypeCode { get; set; }
        public int InOutLimit { get; set; }
        public int InOutWarningInterval { get; set; }
        public int CollectHealthData { get; set; }

        [JsonIgnore]
        public int ControlObject { get; set; }


        public static PMSIOMInOutRecordControl Default => new PMSIOMInOutRecordControl()
        {
            PassTypeCode = "0000",
            InOutLimit = -1,
            InOutWarningInterval = -1,
            CollectHealthData = 1,
            ControlObject = 1
        };
    }
}
