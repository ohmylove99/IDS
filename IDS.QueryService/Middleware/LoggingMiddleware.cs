using Microsoft.Owin;
using Newtonsoft.Json;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDS.QueryService.Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public class LoggingMiddleware : OwinMiddleware
    {
        private readonly ILogger _log = Log.ForContext<LoggingMiddleware>();

        public bool IsEnableTrace {get;set;}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public LoggingMiddleware(OwinMiddleware next) : base(next)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="isEnableTrace"></param>
        public LoggingMiddleware(OwinMiddleware next, bool isEnableTrace) : base(next)
        {
            IsEnableTrace = isEnableTrace;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async override Task Invoke(IOwinContext context)
        {
            if (!Logger.IgnoreLogRoutes(context.Request.Path.ToString()))
                _log.Information("Begin Request");

            if (!Logger.IgnoreLogRoutes(context.Request.Path.ToString()) && IsEnableTrace)
            {
                _log.Debug(JsonConvert.SerializeObject(context.Request.Headers));
                TraceRequestIp(context);
            }

            await Next.Invoke(context);

            if (!Logger.IgnoreLogRoutes(context.Request.Path.ToString()))
                _log.Information("End Request");
        }

        private void TraceRequestIp(IOwinContext context)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("Request.RemoteIpAddress", context.Request.RemoteIpAddress);
            dict.Add("Request.RemotePort", context.Request.RemotePort.ToString());
            dict.Add("Request.LocalIpAddress", context.Request.LocalIpAddress);
            dict.Add("Request.LocalPort", context.Request.LocalPort.ToString());
            _log.Debug(JsonConvert.SerializeObject(dict));
        }
    }
}
