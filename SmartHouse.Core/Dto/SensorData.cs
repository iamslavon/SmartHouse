using System;
using System.Data;
using SmartHouse.Core.Entities;

namespace SmartHouse.Core.Dto
{
    public class SensorData
    {
        public SensorData()
        {
        }

        public SensorData(IDataRecord reader)
        {
            this.HouseId = (int) reader["HouseId"];
            this.RoomId = (int) reader["RoomId"];
            this.SensorId = (int) reader["SensorId"];
            this.Value = (int) reader["Value"];
            this.Time = (DateTime) reader["Time"];
        }

        public int? HouseId { get; set; }

        public int? RoomId { get; set; }

        public int? SensorId { get; set; }

        public int? Value { get; set; }

        public DateTime Time { get; set; }
    }
}
