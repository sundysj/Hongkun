using System;
using Newtonsoft.Json;

namespace Business
{
    public class BoolConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(bool))
            {
                return true;
            }

            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return (int)reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue((bool)value);
        }
    }
}
