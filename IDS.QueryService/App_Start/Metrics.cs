using Metrics;
using Metrics.Endpoints;
using Owin;
using Owin.Metrics;
using System.Text.RegularExpressions;

namespace IDS.QueryService
{
    /// <summary>
    /// 
    /// </summary>
    public static class Metrics
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appBuilder"></param>
        public static void Register(IAppBuilder appBuilder)
        {
            //config.MessageHandlers.Add(new SetOwinRouteTemplateMessageHandler());
            Metric.Config
                //.WithReporting(r => r.WithConsoleReport(TimeSpan.FromSeconds(30)))
                .WithOwin(middleware => appBuilder.Use(middleware), myconfig => myconfig
                    .WithRequestMetricsConfig(c => c.WithAllOwinMetrics(), new[]
                    {
                        new Regex("(?i)^metrics"),
                        new Regex("(?i)^health"),
                        new Regex("(?i)^json")
                     })
                    .WithMetricsEndpoint(conf => conf.WithEndpointReport("/metrics", (d, h, r) => new MetricsEndpointResponse("metrics", "text/plain")))
                );
        }
    }
}
