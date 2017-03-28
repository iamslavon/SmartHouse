using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            dataService = new DataService();
            modules = dataService.GetModules();
            httpClient = new HttpClient();
        }

        public void Execute(IJobExecutionContext context)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var module in modules)
            {
                Console.WriteLine($"* {DateTime.Now:t} Getting data from {module.Ip} ...");
                var response = httpClient.GetAsync($"http://{module.Ip}/getdata");
                var json = response.Result.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<IEnumerable<SensorData>>(json);
                foreach (var sensorData in data)
                {
                    sensorData.RoomId = module.RoomId;
                    dataService.SaveSensorData(sensorData);
                }
            }
            stopWatch.Stop();
            Console.WriteLine($"Done in {stopWatch.ElapsedMilliseconds} ms");
        }
    }
}