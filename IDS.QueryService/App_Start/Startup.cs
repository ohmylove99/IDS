namespace IDS.QueryService
{
    using Microsoft.Owin.Hosting;
    using Owin;
    using System;
    using System.Web.Http;
    /// <summary>
    /// 
    /// </summary>
    public class OwinService
    {
        private IDisposable _webApp;
        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            _webApp = WebApp.Start<Startup>("http://+:9000");
        }
        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            _webApp.Dispose();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appBuilder"></param>
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
