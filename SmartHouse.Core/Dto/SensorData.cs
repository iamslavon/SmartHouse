using System;
using System.Data;

namespace SmartHouse.Core.Dto
{
    public class SensorData
    {
        public SensorData()
        {
        }

        public SensorData(IDataRecord reader)
        {
            this.RoomId = (int) reader["RoomId"];
            this.SensorId = (int) reader["SensorId"];
            this.Value = (int) reader["Value"];
            this.Time = (DateTime) reader["Time"];
        }

        public int? RoomId { get; set; }

        public int? SensorId { get; set; }

        public int? Value { get; set; }

        public DateTime Time { get; set; }
    }
}
