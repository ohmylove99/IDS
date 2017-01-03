namespace IDS.QueryService
{
    using Config;
    using CrystalQuartz.Owin;
    using JobService.App_Start;
    using Microsoft.Owin.Hosting;
    using Owin;
    using Quartz;
    using Serilog;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// 
    /// </summary>
    public class OwinService
    {
        private IDisposable _webApp;
        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            var port = ConfigManager.Instance.Port;
            try
            {
                Log.Information("Starting self-hosted server...");
                _webApp = WebApp.Start<Startup>(string.Format("http://+:{0}", port));
                Log.Information(string.Format("Check http://localhost:{0}/CrystalQuartzPanel.axd to see jobs information", port));
                Log.Information("Starting scheduler...");
                QuartzScheduler.Instance.Scheduler.Start();
                Log.Information("Scheduler is started");
                Log.Information("Press [ENTER] to close");
            }
            catch (Exception e){
                Log.Error(e, string.Format("App can't start at {0}", port));
            }
            finally
            {
                Process.Start(string.Format("http://localhost:{0}/CrystalQuartzPanel.axd/", port));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            if(_webApp != null)
                _webApp.Dispose();
            Log.Information("Shutting down...");
            QuartzScheduler.Instance.Scheduler.Shutdown(waitForJobsToComplete: true);
            Log.Information("Scheduler has been stopped");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appBuilder"></param>
        public void Configuration(IAppBuilder appBuilder)
        {
            IScheduler scheduler = QuartzScheduler.Instance.Scheduler;
            appBuilder.UseCrystalQuartz(scheduler);
        }
    }
}
