using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BioEnumerator.RemoteHelper.ExternalTool
{
    internal class Deserializer
    {
        internal static T DeserializeStream<T>(string content) where T : new()
        {
            return ConvertJsonToObject<T>(content, null);
        }

        protected T ConvertJsonToObject<T>(string json) where T : new()
        {
            return ConvertJsonToObject<T>(json, null);
        }
        public static T ConvertJsonToObject<T>(string json, string rootElement) where T : new()
        {
            var settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                DateParseHandling = DateParseHandling.DateTime,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            try
            {
                if (null != rootElement)
                {
                    var rootObject = JObject.Parse(json);
                    var rootToken = rootObject.SelectToken(rootElement);

                    return JsonConvert.DeserializeObject<T>(rootToken.ToString(), settings);
                }
                return JsonConvert.DeserializeObject<T>(json, settings);
            }
            catch (Exception e)
            {
                return new T();
            }
        }
    }
}
