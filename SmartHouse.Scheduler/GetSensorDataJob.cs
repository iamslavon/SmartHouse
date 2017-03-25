using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Quartz;
using SmartHouse.Core.Dto;
using SmartHouse.Core.Entities;
using SmartHouse.Services.Data;

namespace SmartHouse.Scheduler
{
    public class GetSensorDataJob : IJob
    {
        private readonly DataService dataService;
        private readonly HttpClient httpClient;
        private readonly IEnumerable<Module> modules;

        public GetSensorDataJob()
        {
            this.dataService = new DataService();
            this.modules = dataService.GetModules();
            this.httpClient = new HttpClient();
        }

        public void Execute(IJobExecutionContext context)
        {
            foreach (var module in modules)
            {
                var response = httpClient.GetAsync($"http://{module.Ip}/getdata");
                var json = response.Result.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<IEnumerable<SensorData>>(json);
                foreach (var sensorData in data)
                {
                    dataService.SaveSensorData(sensorData);
                }
            }
        }
    }
}
