using Newtonsoft.Json;

namespace Business.PMS10.物管App.桌面.Models.AppModules
{
    public class AppWebModule : AppModule
    {
        [JsonProperty(Order = 99)]
        public string Url { get; set; }
    }
}
