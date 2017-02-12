using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace IDS.QueryService
{
    /// <summary>
    /// 
    /// </summary>
    public static class FormatterConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Setup(HttpConfiguration config)
        {
            config.Formatters.Clear();
            // This is Default Order choosen: http://stackoverflow.com/questions/26385474/how-do-i-tell-webapi-to-show-json-results-by-default

            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.Add(new XmlMediaTypeFormatter());

            //for xml : http://localhost:8080/api/?type=xml
            //for json: http://localhost:8080/api/?type=json
            //or Add Header ContentType as application/json or application/xml
        }

        private static void SetupXmlFormat(HttpConfiguration config)
        {
            // XmlFormatter Setting
            config.Formatters.XmlFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("type", "xml", new MediaTypeHeaderValue("application/xml")));
        }
        private static void SetupJsonFormat(HttpConfiguration config)
        {
            // JsonFormatter Setting
            var defaultSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter>
                        {
                            new StringEnumConverter{ CamelCaseText = true },
                        }
            };
            JsonConvert.DefaultSettings = () => { return defaultSettings; };
            config.Formatters.JsonFormatter.SerializerSettings = defaultSettings;

            config.Formatters.JsonFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json")));
        }
    }
}
