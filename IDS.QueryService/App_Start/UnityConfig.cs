namespace IDS.QueryService
{
    using Microsoft.Practices.Unity;
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
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}