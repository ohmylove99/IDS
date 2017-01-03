using Quartz;
using Serilog;

namespace IDS.JobService.Jobs
{
    public class GreetingMessageJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Log.Information("Greetings from Hello Job!");
        }
    }
}
