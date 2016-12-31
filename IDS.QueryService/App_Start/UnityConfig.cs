namespace IDS.QueryService
{
    using Microsoft.Practices.Unity;
    using System.Web.Http;
    using Unity.WebApi;

    public static class UnityConfig
    {
        public static void Setup(HttpConfiguration config)
        {
            var container = new UnityContainer();
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}