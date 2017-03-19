using Quartz;
using Quartz.Impl;

namespace SmartHouse.Scheduler
{
    public class JobScheduler
    {
        public static void Start()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            var job = JobBuilder.Create<GetSensorDataJob>().Build();

            var trigger = TriggerBuilder.Create()
            .WithIdentity("GetData")
            .StartNow()
            .WithSimpleSchedule(x => x
            .WithIntervalInMinutes(60)
            .RepeatForever())
            .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
