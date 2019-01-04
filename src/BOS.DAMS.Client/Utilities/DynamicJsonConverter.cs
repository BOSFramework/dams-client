using BOS.DAMS.Client.ClientModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOS.DAMS.Client.Utilities
{
    public class DynamicJsonConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IAsset);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null && reader.ValueType == typeof(string))
            {
                return JsonConvert.DeserializeObject<T>(reader.Value.ToString());
            }
            else if (reader.TokenType == JsonToken.StartObject)
            {
                return serializer.Deserialize<T>(reader);

                //existingValue = existingValue ?? serializer.ContractResolver.ResolveContract(objectType).DefaultCreator();
                //serializer.Populate(reader, existingValue);
                //return existingValue;
            }
            else if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            else
            {
                throw new JsonSerializationException();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
