using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace IDS.QueryService
{
    public static class FormatterConfig
    {
        public static void Setup(HttpConfiguration config)
        {
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings =
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
        }
    }
}
