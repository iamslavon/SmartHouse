using Quartz;
using SmartHouse.Services.Data;

namespace SmartHouse.Scheduler
{
    public class GetSensorDataJob : IJob
    {
        private readonly DataService dataService;

        public GetSensorDataJob()
        {
            this.dataService = new DataService();
        }

        public void Execute(IJobExecutionContext context)
        {
            // Send http request to wi-fi module here
        }
    }
}
