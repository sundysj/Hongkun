using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    /// <summary>
    /// 接口统一返回格式
    /// 已测试通过支持的返回对象如下:String,DataTable,List,定义的Model,Dictionary<string, object>
    /// String用法:new ApiResult(true/false,String).toJson();
    /// DataTable:new ApiResult(true/false,DataTable).toJson();
    /// List:new ApiResult(true/false,List).toJson();
    /// Model:new ApiResult(true/false,Model).toJson();
    /// Dictionary<string, object>: Dictionary<string, object> dic = new Dictionary<string, object>();
    ///                             dic.Add("key" + i, new ApiResult(i % 2 == 0 ? true : false, "测试" + i));
    ///                             new ApiResult(true/false,dic).toJson();
    /// </summary>
    public class ApiResult
    {
        public ApiResult(bool result, object data) {
            this.Result = result ? "true" : "false";
            this.data = data;
        }
        
        public string Result { get; set; }
        public object data { get; set; }

        public string toJson() {
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(this, Formatting.None, timeFormat);
        }
        override
        public string ToString() {
            return JsonConvert.SerializeObject(this);
        }
    }
}
