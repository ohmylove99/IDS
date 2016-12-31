namespace Owin.Docker
{
    using IDS.QueryService;
    using System;
    using Topshelf;
    public class Program
    {
        public static int Main(string[] args)
        {
            return (int)HostFactory.Run(x =>
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
                x.SetDisplayName("IDS Query Service");
                x.SetDescription("IDS Query Service");
                x.SetServiceName("IDS.QueryService");
                x.StartAutomatically();
                x.EnableShutdown();
            });
        }
    }
}
