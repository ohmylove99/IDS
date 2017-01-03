using IDS.JobService.Jobs;
using Quartz;
using Quartz.Impl;
using System;

namespace IDS.JobService.App_Start
{
    public class QuartzScheduler
    {
        private static readonly Lazy<QuartzScheduler> lazy = new Lazy<QuartzScheduler>(() => new QuartzScheduler());
        /// <summary>
        /// This is Singleton instance
        /// </summary>
        public static QuartzScheduler Instance { get { return lazy.Value; } }
        private QuartzScheduler()
        {
        }
        public IScheduler Scheduler
        {
            get{
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

                JobKey job1Key = new JobKey("job1", "group1");

                scheduler.DeleteJob(job1Key);

                IJobDetail job = JobBuilder.Create<GreetingMessageJob>()
                    .WithIdentity(job1Key)
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .RepeatForever())
                    .Build();

                scheduler.ScheduleJob(job, trigger);

                return scheduler;
            }
        }
    }
}
