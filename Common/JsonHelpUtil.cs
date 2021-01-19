using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Common
{
    /// <summary>
    /// JSON 格式数据序列化与反序列化
    /// </summary>
    public class JsonHelp
    {
        /// <summary>
        /// JSON 序列化 将实体转换成字符串
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="t">实体数据</param>
        /// <returns>json字符串</returns>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }

        /// <summary>
        /// JSON 反序列化 将字符串转换成实体
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="jsonString">需转换的字符串</param>
        /// <returns>实体数据</returns>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            ms.Close();
            return obj;
        }
    }
}
