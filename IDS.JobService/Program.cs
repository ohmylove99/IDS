﻿namespace IDS.QueryService
{
    using Config;
    using CrystalQuartz.Owin;
    using JobService.App_Start;
    using Microsoft.Owin.Hosting;
    using Owin;
    using Quartz;
    using Quartz.Impl;
    using Serilog;
    using System;
    using Topshelf;
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                          .ReadFrom.AppSettings()
                          .Enrich.WithThreadId()
                          .Enrich.FromLogContext()
                          .CreateLogger();

            //return (int)
                HostFactory.Run(x =>
            {
                x.RunAsLocalSystem();
                x.SetStartTimeout(TimeSpan.FromSeconds(120));
                x.SetStopTimeout(TimeSpan.FromSeconds(120));
                x.Service<OwinService>(s =>
                {
                    s.ConstructUsing(() => new OwinService());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });
                x.SetDisplayName("IDS Query Service Display");
                x.SetDescription("IDS Query Service Description");
                x.SetServiceName("IDS.QueryService");
                x.StartAutomatically();
                x.EnableShutdown();
                x.UseSerilog();
            });

            
        }

    }
    
}
