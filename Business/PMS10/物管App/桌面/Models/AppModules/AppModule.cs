using Newtonsoft.Json;
using System.Collections.Generic;

namespace Business.PMS10.物管App.桌面.Models.AppModules
{
    public class AppModule
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }

    public class AppModule<T> : AppModule
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<T> Submodules { get; set; }
    }
}
