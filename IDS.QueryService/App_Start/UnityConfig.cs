namespace IDS.QueryService
{
    using Controller;
    using Microsoft.Practices.Unity;
    using Service;
    using System.Web.Http;
    using Unity.WebApi;
    /// <summary>
    /// 
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Setup(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<HelloController>();

            // Register interface
            container.RegisterType<IHelloService, HelloService>();

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}