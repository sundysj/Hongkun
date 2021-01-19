using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.支付配置模型
{
    public class Config
    {
       public static T GetConfig<T>(JObject jObject)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(jObject));
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
