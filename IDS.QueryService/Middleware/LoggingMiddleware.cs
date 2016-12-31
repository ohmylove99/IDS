using Microsoft.Owin;
using System;
using System.Threading.Tasks;

namespace IDS.QueryService.Middleware
{
    public class LoggingMiddleware : OwinMiddleware
    {
        public LoggingMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            if (!IgnoreRoutes(context.Request.Path.ToString()))
                Console.WriteLine("Begin Request");
            await Next.Invoke(context);
            if (!IgnoreRoutes(context.Request.Path.ToString()))
                Console.WriteLine("End Request");
        }

        private bool IgnoreRoutes(string path)
        {
            if (!string.IsNullOrEmpty(path) && path.StartsWith("/swagger"))
                return true;
            return false;
        }
    }
}
