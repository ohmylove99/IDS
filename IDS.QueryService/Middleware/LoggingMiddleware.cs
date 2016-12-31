﻿using Microsoft.Owin;
using System;
using System.Threading.Tasks;

namespace IDS.QueryService.Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public class LoggingMiddleware : OwinMiddleware
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
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
