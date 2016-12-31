namespace IDS.QueryService
{
    using System.Web.Http;
    /// <summary>
    /// 
    /// </summary>
    public static class RouteConfig
    {
        public static void Setup(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                 name: "DefaultApi",
                 routeTemplate: "api/{controller}/{id}",
                 defaults: new { id = RouteParameter.Optional }
             );

            config.Routes.MapHttpRoute(
                name: "ApiWithActionAndName",
                routeTemplate: "api/{controller}/{action}/{name}",
                defaults: null,
                constraints: new { name = @"^[a-z]+$" }
            );

            config.Routes.MapHttpRoute(
                name: "ApiWithAction",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "Get" }
            );

            config.Routes.IgnoreRoute("favicon.ico", "{*favicon.ico}");
        }
    }
}