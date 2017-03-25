using System;
using System.Configuration;
using Quartz;
using Quartz.Impl;

namespace SmartHouse.Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            Services.Settings.ConnectionString =
                ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            var job = JobBuilder.Create<GetSensorDataJob>().Build();
            var trigger = TriggerBuilder.Create()
                .WithIdentity("GetData")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(30)
                    .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);
            Console.WriteLine("Scheduler started");
        }
    }
}
