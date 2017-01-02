namespace IDS.QueryService
{
    using Config;
    using Microsoft.Owin.Hosting;
    using Middleware;
    using Owin;
    using Serilog;
    using System;
    using System.Diagnostics;
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
            var port = ConfigManager.Instance.Port;
            try
            {
                _webApp = WebApp.Start<Startup>(string.Format("http://+:{0}", port));
                Log.Information("Server is running at http://{0}:{1}", Environment.MachineName, port);
            }
            catch(Exception e){
                Log.Error(e, string.Format("App can't start at {0}", port));
            }
            finally
            {
                Process.Start(string.Format("{0}metrics/", string.Format("http://localhost:{0}/", port)));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            if(_webApp != null)
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

            appBuilder.Use<LoggingMiddleware>(true);

            appBuilder.Use<Logger>();

            Metrics.Register(appBuilder);

            appBuilder.UseWebApi(config);
        }
    }
}
