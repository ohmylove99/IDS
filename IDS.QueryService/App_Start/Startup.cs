namespace IDS.QueryService
{
    using Microsoft.Owin.Hosting;
    using Middleware;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Owin;
    using Swashbuckle.Application;
    using System;
    using System.Net.Http.Formatting;
    using System.Web.Http;

    public class OwinService
    {
        private IDisposable _webApp;

        public void Start()
        {
            _webApp = WebApp.Start<Startup>("http://+:9000");
        }

        public void Stop()
        {
            _webApp.Dispose();
        }
    }

    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();

            //Add unity container
            UnityConfig.Setup(config);
            SwaggerConfig.Setup(config);
            FormatterConfig.Setup(config);
            RouteConfig.Setup(config);

            //appBuilder.Use<LoggingMiddleware>();
            appBuilder.Use<Logger>();

            appBuilder.UseWebApi(config);
        }
    }
}
