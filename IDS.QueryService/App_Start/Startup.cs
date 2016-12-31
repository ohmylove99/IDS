namespace IDS.QueryService
{
    using Middleware;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Owin;
    using System.Net.Http.Formatting;
    using System.Web.Http;

    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            //Add unity container
            UnityConfig.Setup(config);

            // Add Console Logger
            

            appBuilder.Use<LoggingMiddleware>(); appBuilder.Use<Logger>();

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.Formatters.JsonFormatter.SerializerSettings =
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

            config.Routes.MapHttpRoute(
                name: "api",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }
    }
}
